namespace AutoFleet.Demo.Models;

public sealed class Tag
{
    public int Id { get; init; }
    public required string Dimension { get; init; }
    public required string Value { get; init; }
    public string Display => $"{Dimension}={Value}";
}

public sealed class Machine
{
    public int Id { get; init; }
    public required string Code { get; init; }
    public required string Name { get; init; }
    public required string ProcessCode { get; init; }
    public required string ProcessName { get; init; }
    public MachineStatus Status { get; set; }
    public List<int> TagIds { get; init; } = [];
}

public sealed class TriggerConditionItem
{
    public required string Field { get; init; }
    public ConditionOperator Op { get; init; }
    public required object Value { get; init; }
}

public sealed class TriggerCondition
{
    public LogicOperator Logic { get; init; } = LogicOperator.And;
    public List<TriggerConditionItem> Conditions { get; init; } = [];
}

public sealed class ScoreItem
{
    public int TagId { get; init; }
    public TagMatchType MatchType { get; init; }
    public int WeightScore { get; init; }
    /// <summary>
    /// 为 true 时，仅当机台该标签与产品 DCA 类型一致才计分（用于 K0000 / A0001 匹配）。
    /// </summary>
    public bool RequireProductInstrumentMatch { get; init; }
}

public sealed class ScheduleRule
{
    public int Id { get; init; }
    public required string Code { get; init; }
    public required string Name { get; init; }
    public int Priority { get; init; }
    public bool IsActive { get; init; }
    public required TriggerCondition Trigger { get; init; }
    public List<ScoreItem> ScoreItems { get; init; } = [];
}

public sealed class TestRecord
{
    public required string ProductSn { get; init; }
    public required string ProcessCode { get; init; }
    public int MachineId { get; init; }
    public TestResult Result { get; init; }
    public string? FailType { get; init; }
    public DateTime TestTime { get; init; }
}

public sealed class ProductContext
{
    public required string ProductSn { get; init; }
    public required string CurrentProcess { get; init; }
    /// <summary>产品 DCA 仪表类型，如 K0000、A0001。</summary>
    public required string InstrumentType { get; init; }
    public string? LastProcess { get; init; }
    public TestResult? LastTestResult { get; init; }
    public string? FailType { get; init; }
    public int TestCountOnCurrentProcess { get; init; }
    public int? LastMachineId { get; init; }
}

/// <summary>
/// Demo 预设场景：用户选择 A 测完后的业务情况，系统自动推导下一工序。
/// </summary>
public sealed class DemoScenario
{
    public required string Id { get; init; }
    public required string Title { get; init; }
    public required string Sn { get; init; }
    public required string InstrumentType { get; init; }
    public required string ResolvedProcessCode { get; init; }
    public string? ExpectedMachineCode { get; init; }
}

public sealed class ScoreBreakdownItem
{
    public required string Description { get; init; }
    public int Score { get; init; }
    public bool Matched { get; init; }
}

public sealed class CandidateScore
{
    public required Machine Machine { get; init; }
    public int TotalScore { get; init; }
    public List<ScoreBreakdownItem> Items { get; init; } = [];
}

public sealed class ScheduleRequest
{
    public required string ProductSn { get; init; }
    public required string ProcessCode { get; init; }
}

public sealed class ScheduleResult
{
    public bool Success { get; init; }
    public string Message { get; init; } = string.Empty;
    public ScheduleRule? MatchedRule { get; init; }
    public Machine? SelectedMachine { get; init; }
    public List<CandidateScore> Candidates { get; init; } = [];
}
