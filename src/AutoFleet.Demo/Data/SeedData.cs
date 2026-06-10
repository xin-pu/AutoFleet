using AutoFleet.Demo.Models;

namespace AutoFleet.Demo.Data;

public static class SeedData
{
    public const string InstrumentDimension = "DCA类型";

    public static IReadOnlyList<Tag> Tags { get; } =
    [
        new Tag { Id = 1, Dimension = InstrumentDimension, Value = "K0000" },
        new Tag { Id = 2, Dimension = InstrumentDimension, Value = "A0001" },
        new Tag { Id = 3, Dimension = "良率档", Value = "高" },
        new Tag { Id = 4, Dimension = "良率档", Value = "低" },
        new Tag { Id = 5, Dimension = "负载", Value = "空闲" },
        new Tag { Id = 6, Dimension = "标记", Value = "刚测失败" },
        new Tag { Id = 7, Dimension = "区域", Value = "Fail区" }
    ];

    public static IReadOnlyList<Machine> Machines { get; } =
    [
        new Machine
        {
            Id = 1, Code = "M-A-01", Name = "A线-K0000-高良率", ProcessCode = "A", ProcessName = "工序A",
            Status = MachineStatus.Online, TagIds = [1, 3]
        },
        new Machine
        {
            Id = 2, Code = "M-A-02", Name = "A线-K0000-低良率", ProcessCode = "A", ProcessName = "工序A",
            Status = MachineStatus.Online, TagIds = [1, 4, 5]
        },
        new Machine
        {
            Id = 3, Code = "M-A-03", Name = "A线-A0001-高良率", ProcessCode = "A", ProcessName = "工序A",
            Status = MachineStatus.Online, TagIds = [2, 3]
        },
        new Machine
        {
            Id = 4, Code = "M-B-01", Name = "B线-A0001-低良率", ProcessCode = "B", ProcessName = "工序B",
            Status = MachineStatus.Online, TagIds = [2, 4]
        },
        new Machine
        {
            Id = 5, Code = "M-B-02", Name = "B线-K0000-高良率", ProcessCode = "B", ProcessName = "工序B",
            Status = MachineStatus.Online, TagIds = [1, 3]
        },
        new Machine
        {
            Id = 6, Code = "M-FAIL-01", Name = "Fail区-通用接收", ProcessCode = "FAIL", ProcessName = "Fail区",
            Status = MachineStatus.Online, TagIds = [7]
        }
    ];

    public static IReadOnlyList<ScheduleRule> Rules { get; } =
    [
        new ScheduleRule
        {
            Id = 1, Code = "RULE-A-FAIL-RETEST", Name = "A失败-项点不良复测",
            Priority = 5, IsActive = true,
            Trigger = new TriggerCondition
            {
                Conditions =
                [
                    new TriggerConditionItem { Field = "current_process", Op = ConditionOperator.Eq, Value = "A" },
                    new TriggerConditionItem { Field = "last_test_result", Op = ConditionOperator.Eq, Value = "fail" },
                    new TriggerConditionItem { Field = "fail_type", Op = ConditionOperator.In, Value = new[] { "项点不良" } }
                ]
            },
            ScoreItems =
            [
                new ScoreItem { TagId = 1, MatchType = TagMatchType.ContainsAdd, WeightScore = 40, RequireProductInstrumentMatch = true },
                new ScoreItem { TagId = 2, MatchType = TagMatchType.ContainsAdd, WeightScore = 40, RequireProductInstrumentMatch = true },
                new ScoreItem { TagId = 3, MatchType = TagMatchType.ContainsAdd, WeightScore = 50 },
                new ScoreItem { TagId = 4, MatchType = TagMatchType.ContainsSubtract, WeightScore = 30 },
                new ScoreItem { TagId = 5, MatchType = TagMatchType.ContainsAdd, WeightScore = 10 }
            ]
        },
        new ScheduleRule
        {
            Id = 2, Code = "RULE-A-FAIL-COMM", Name = "A失败-通信异常换机",
            Priority = 8, IsActive = true,
            Trigger = new TriggerCondition
            {
                Conditions =
                [
                    new TriggerConditionItem { Field = "current_process", Op = ConditionOperator.Eq, Value = "A" },
                    new TriggerConditionItem { Field = "last_test_result", Op = ConditionOperator.Eq, Value = "fail" },
                    new TriggerConditionItem { Field = "fail_type", Op = ConditionOperator.In, Value = new[] { "通信异常" } }
                ]
            },
            ScoreItems =
            [
                new ScoreItem { TagId = 1, MatchType = TagMatchType.ContainsAdd, WeightScore = 30, RequireProductInstrumentMatch = true },
                new ScoreItem { TagId = 2, MatchType = TagMatchType.ContainsAdd, WeightScore = 30, RequireProductInstrumentMatch = true },
                new ScoreItem { TagId = 6, MatchType = TagMatchType.ContainsSubtract, WeightScore = 100 },
                new ScoreItem { TagId = 5, MatchType = TagMatchType.ContainsAdd, WeightScore = 15 }
            ]
        },
        new ScheduleRule
        {
            Id = 5, Code = "RULE-FAIL-SCRAP", Name = "A失败-直接报废去Fail区",
            Priority = 6, IsActive = true,
            Trigger = new TriggerCondition
            {
                Conditions =
                [
                    new TriggerConditionItem { Field = "current_process", Op = ConditionOperator.Eq, Value = "FAIL" },
                    new TriggerConditionItem { Field = "last_test_result", Op = ConditionOperator.Eq, Value = "fail" },
                    new TriggerConditionItem { Field = "fail_type", Op = ConditionOperator.In, Value = new[] { "直接报废" } }
                ]
            },
            ScoreItems =
            [
                new ScoreItem { TagId = 7, MatchType = TagMatchType.ContainsAdd, WeightScore = 100 }
            ]
        },
        new ScheduleRule
        {
            Id = 3, Code = "RULE-A-FIRST", Name = "A首测/正常进站",
            Priority = 10, IsActive = true,
            Trigger = new TriggerCondition
            {
                Conditions =
                [
                    new TriggerConditionItem { Field = "current_process", Op = ConditionOperator.Eq, Value = "A" },
                    new TriggerConditionItem { Field = "last_test_result", Op = ConditionOperator.Ne, Value = "fail" }
                ]
            },
            ScoreItems =
            [
                new ScoreItem { TagId = 1, MatchType = TagMatchType.ContainsAdd, WeightScore = 40, RequireProductInstrumentMatch = true },
                new ScoreItem { TagId = 2, MatchType = TagMatchType.ContainsAdd, WeightScore = 40, RequireProductInstrumentMatch = true },
                new ScoreItem { TagId = 4, MatchType = TagMatchType.ContainsAdd, WeightScore = 15 },
                new ScoreItem { TagId = 5, MatchType = TagMatchType.ContainsAdd, WeightScore = 20 }
            ]
        },
        new ScheduleRule
        {
            Id = 4, Code = "RULE-B-PASS", Name = "A通过-流转B",
            Priority = 10, IsActive = true,
            Trigger = new TriggerCondition
            {
                Conditions =
                [
                    new TriggerConditionItem { Field = "current_process", Op = ConditionOperator.Eq, Value = "B" },
                    new TriggerConditionItem { Field = "last_process", Op = ConditionOperator.Eq, Value = "A" },
                    new TriggerConditionItem { Field = "last_test_result", Op = ConditionOperator.Eq, Value = "pass" }
                ]
            },
            ScoreItems =
            [
                new ScoreItem { TagId = 1, MatchType = TagMatchType.ContainsAdd, WeightScore = 40, RequireProductInstrumentMatch = true },
                new ScoreItem { TagId = 2, MatchType = TagMatchType.ContainsAdd, WeightScore = 40, RequireProductInstrumentMatch = true },
                new ScoreItem { TagId = 3, MatchType = TagMatchType.ContainsAdd, WeightScore = 20 }
            ]
        }
    ];

    public static IReadOnlyList<TestRecord> TestHistory { get; } =
    [
        new TestRecord
        {
            ProductSn = "SN-20001", ProcessCode = "A", MachineId = 2, Result = TestResult.Fail,
            FailType = "项点不良", TestTime = DateTime.Today.AddHours(-1)
        },
        new TestRecord
        {
            ProductSn = "SN-30001", ProcessCode = "A", MachineId = 3, Result = TestResult.Pass,
            FailType = null, TestTime = DateTime.Today.AddHours(-2)
        },
        new TestRecord
        {
            ProductSn = "SN-40001", ProcessCode = "A", MachineId = 1, Result = TestResult.Fail,
            FailType = "通信异常", TestTime = DateTime.Today.AddMinutes(-30)
        },
        new TestRecord
        {
            ProductSn = "SN-50001", ProcessCode = "A", MachineId = 2, Result = TestResult.Fail,
            FailType = "直接报废", TestTime = DateTime.Today.AddMinutes(-45)
        },
        new TestRecord
        {
            ProductSn = "SN-60001", ProcessCode = "A", MachineId = 1, Result = TestResult.Pass,
            FailType = null, TestTime = DateTime.Today.AddHours(-3)
        }
    ];

    public static IReadOnlyList<DemoScenario> DemoScenarios { get; } =
    [
        new DemoScenario
        {
            Id = "first-a", Title = "1 · 首次进A（K0000，无历程）",
            Sn = "SN-10086", InstrumentType = "K0000", ResolvedProcessCode = "A",
            ExpectedMachineCode = "M-A-02"
        },
        new DemoScenario
        {
            Id = "pass-b-a0001", Title = "2 · A通过 → 去B（A0001）",
            Sn = "SN-30001", InstrumentType = "A0001", ResolvedProcessCode = "B",
            ExpectedMachineCode = "M-B-01"
        },
        new DemoScenario
        {
            Id = "fail-retest", Title = "3 · A失败 → 复测A（项点不良）",
            Sn = "SN-20001", InstrumentType = "K0000", ResolvedProcessCode = "A",
            ExpectedMachineCode = "M-A-01"
        },
        new DemoScenario
        {
            Id = "fail-switch", Title = "4 · A失败 → 换机复测A（通信异常）",
            Sn = "SN-40001", InstrumentType = "K0000", ResolvedProcessCode = "A",
            ExpectedMachineCode = "M-A-02"
        },
        new DemoScenario
        {
            Id = "fail-scrap", Title = "5 · A失败 → 去Fail区（直接报废）",
            Sn = "SN-50001", InstrumentType = "K0000", ResolvedProcessCode = "FAIL",
            ExpectedMachineCode = "M-FAIL-01"
        },
        new DemoScenario
        {
            Id = "pass-b-k0000", Title = "6 · A通过 → 去B（K0000）",
            Sn = "SN-60001", InstrumentType = "K0000", ResolvedProcessCode = "B",
            ExpectedMachineCode = "M-B-02"
        }
    ];

    public static DemoScenario? GetScenario(string id) =>
        DemoScenarios.FirstOrDefault(s => s.Id == id);

    public static string GetInstrumentType(string sn) =>
        DemoScenarios.FirstOrDefault(s => s.Sn == sn)?.InstrumentType ?? "K0000";

    public static Tag? GetTag(int tagId) => Tags.FirstOrDefault(t => t.Id == tagId);

    public static IEnumerable<Tag> GetTagsForMachine(Machine machine) =>
        Tags.Where(t => machine.TagIds.Contains(t.Id));
}
