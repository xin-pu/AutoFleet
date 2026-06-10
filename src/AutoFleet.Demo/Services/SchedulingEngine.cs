using AutoFleet.Demo.Data;
using AutoFleet.Demo.Models;

namespace AutoFleet.Demo.Services;

public sealed class SchedulingEngine
{
    private readonly IReadOnlyList<Machine> _machines;
    private readonly IReadOnlyList<ScheduleRule> _rules;
    private readonly IReadOnlyList<TestRecord> _history;
    private readonly IReadOnlyList<Tag> _tags;

    public SchedulingEngine()
        : this(SeedData.Machines, SeedData.Rules, SeedData.TestHistory, SeedData.Tags)
    {
    }

    public SchedulingEngine(
        IReadOnlyList<Machine> machines,
        IReadOnlyList<ScheduleRule> rules,
        IReadOnlyList<TestRecord> history,
        IReadOnlyList<Tag> tags)
    {
        _machines = machines;
        _rules = rules;
        _history = history;
        _tags = tags;
    }

    public ScheduleResult Schedule(DemoScenario scenario)
    {
        var request = new ScheduleRequest
        {
            ProductSn = scenario.Sn,
            ProcessCode = scenario.ResolvedProcessCode
        };

        var result = Schedule(request);
        if (!result.Success || string.IsNullOrEmpty(scenario.ExpectedMachineCode))
            return result;

        var expectedHint = result.SelectedMachine?.Code == scenario.ExpectedMachineCode
            ? "与场景预期一致。"
            : $"场景预期机台：{scenario.ExpectedMachineCode}。";

        return new ScheduleResult
        {
            Success = result.Success,
            Message = $"{result.Message} {expectedHint}",
            MatchedRule = result.MatchedRule,
            SelectedMachine = result.SelectedMachine,
            Candidates = result.Candidates
        };
    }

    public ScheduleResult Schedule(ScheduleRequest request)
    {
        var context = BuildContext(request);
        var rule = MatchRule(context);
        if (rule is null)
        {
            return new ScheduleResult
            {
                Success = false,
                Message = $"未匹配到任何激活的调度规则。（产品 DCA：{context.InstrumentType}，目标工序：{request.ProcessCode}）"
            };
        }

        var candidates = GetCandidateMachines(request.ProcessCode);
        if (candidates.Count == 0)
        {
            return new ScheduleResult
            {
                Success = false,
                Message = $"工序 {request.ProcessCode} 下无在线可用机台。",
                MatchedRule = rule
            };
        }

        ApplyDynamicTags(context, candidates);

        var scored = candidates
            .Select(m => ScoreMachine(m, rule, context))
            .OrderByDescending(c => c.TotalScore)
            .ThenBy(c => c.Machine.Code, StringComparer.Ordinal)
            .ToList();

        var winner = scored[0];
        var processLabel = request.ProcessCode == "FAIL" ? "Fail区" : request.ProcessCode;

        return new ScheduleResult
        {
            Success = true,
            Message = $"命中规则 [{rule.Name}]，下一工序 {processLabel}，选择 {winner.Machine.Code}（{winner.Machine.Name}），得分 {winner.TotalScore}。",
            MatchedRule = rule,
            SelectedMachine = winner.Machine,
            Candidates = scored
        };
    }

    public ProductContext BuildContext(ScheduleRequest request)
    {
        var records = _history
            .Where(r => r.ProductSn == request.ProductSn)
            .OrderByDescending(r => r.TestTime)
            .ToList();

        var last = records.FirstOrDefault();
        var countOnProcess = records.Count(r => r.ProcessCode == request.ProcessCode);

        return new ProductContext
        {
            ProductSn = request.ProductSn,
            CurrentProcess = request.ProcessCode,
            InstrumentType = SeedData.GetInstrumentType(request.ProductSn),
            LastProcess = last?.ProcessCode,
            LastTestResult = last?.Result,
            FailType = last?.FailType,
            TestCountOnCurrentProcess = countOnProcess,
            LastMachineId = last?.MachineId
        };
    }

    private ScheduleRule? MatchRule(ProductContext context) =>
        _rules
            .Where(r => r.IsActive)
            .OrderBy(r => r.Priority)
            .FirstOrDefault(r => TriggerMatcher.IsMatch(r.Trigger, context));

    private List<Machine> GetCandidateMachines(string processCode) =>
        _machines
            .Where(m => m.ProcessCode == processCode && m.Status == MachineStatus.Online)
            .ToList();

    private void ApplyDynamicTags(ProductContext context, List<Machine> candidates)
    {
        if (context.LastMachineId is null || context.LastTestResult != TestResult.Fail)
            return;

        if (context.CurrentProcess == "FAIL")
            return;

        var lastMachine = candidates.FirstOrDefault(m => m.Id == context.LastMachineId);
        if (lastMachine is null)
            return;

        var failTag = _tags.First(t => t.Id == 6);
        if (!lastMachine.TagIds.Contains(failTag.Id))
            lastMachine.TagIds.Add(failTag.Id);
    }

    private CandidateScore ScoreMachine(Machine machine, ScheduleRule rule, ProductContext context)
    {
        var machineTags = _tags.Where(t => machine.TagIds.Contains(t.Id)).ToList();
        var items = new List<ScoreBreakdownItem>();
        var total = 0;

        foreach (var scoreItem in rule.ScoreItems)
        {
            var tag = _tags.First(t => t.Id == scoreItem.TagId);
            var hasTag = machineTags.Any(t => t.Id == tag.Id);
            var tagApplicable = !scoreItem.RequireProductInstrumentMatch
                                || (hasTag && tag.Value == context.InstrumentType);

            var matched = scoreItem.MatchType switch
            {
                TagMatchType.ContainsAdd => tagApplicable && hasTag,
                TagMatchType.NotContainsAdd => tagApplicable && !hasTag,
                TagMatchType.ContainsSubtract => hasTag,
                TagMatchType.NotContainsSubtract => !hasTag,
                _ => false
            };

            if (scoreItem.RequireProductInstrumentMatch && tag.Value != context.InstrumentType)
                matched = false;

            var score = matched ? scoreItem.WeightScore : 0;
            if (scoreItem.MatchType is TagMatchType.ContainsSubtract or TagMatchType.NotContainsSubtract)
                score = matched ? -Math.Abs(scoreItem.WeightScore) : 0;

            total += score;
            items.Add(new ScoreBreakdownItem
            {
                Description = DescribeScoreItem(tag, scoreItem, hasTag, context.InstrumentType),
                Score = score,
                Matched = matched
            });
        }

        return new CandidateScore
        {
            Machine = machine,
            TotalScore = total,
            Items = items
        };
    }

    private static string DescribeScoreItem(Tag tag, ScoreItem item, bool hasTag, string instrumentType)
    {
        var action = item.MatchType switch
        {
            TagMatchType.ContainsAdd => "包含则加分",
            TagMatchType.NotContainsAdd => "不包含则加分",
            TagMatchType.ContainsSubtract => "包含则减分",
            TagMatchType.NotContainsSubtract => "不包含则减分",
            _ => "未知"
        };

        var productHint = item.RequireProductInstrumentMatch
            ? $"，需匹配产品DCA={instrumentType}"
            : string.Empty;

        return $"{tag.Display}（{action}，机台{(hasTag ? "有" : "无")}此标签{productHint}，权重 {item.WeightScore}）";
    }
}
