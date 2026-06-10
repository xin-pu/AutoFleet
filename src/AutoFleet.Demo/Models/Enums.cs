namespace AutoFleet.Demo.Models;

public enum MachineStatus
{
    Offline = 0,
    Online = 1,
    Maintenance = 2
}

public enum TestResult
{
    Fail = 0,
    Pass = 1
}

public enum TagMatchType
{
    ContainsAdd = 1,
    NotContainsAdd = 2,
    ContainsSubtract = 3,
    NotContainsSubtract = 4
}

public enum ConditionOperator
{
    Eq,
    Ne,
    In,
    NotIn
}

public enum LogicOperator
{
    And
}
