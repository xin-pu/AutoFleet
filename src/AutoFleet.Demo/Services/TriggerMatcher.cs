using AutoFleet.Demo.Models;

namespace AutoFleet.Demo.Services;

public static class TriggerMatcher
{
    public static bool IsMatch(TriggerCondition trigger, ProductContext context)
    {
        if (trigger.Conditions.Count == 0)
            return true;

        return trigger.Logic switch
        {
            LogicOperator.And => trigger.Conditions.All(c => Evaluate(c, context)),
            _ => false
        };
    }

    private static bool Evaluate(TriggerConditionItem condition, ProductContext context)
    {
        var actual = ResolveField(condition.Field, context);
        return condition.Op switch
        {
            ConditionOperator.Eq => EqualsNormalized(actual, condition.Value),
            ConditionOperator.Ne => !EqualsNormalized(actual, condition.Value),
            ConditionOperator.In => IsIn(actual, condition.Value),
            ConditionOperator.NotIn => !IsIn(actual, condition.Value),
            _ => false
        };
    }

    private static object? ResolveField(string field, ProductContext context) =>
        field.ToLowerInvariant() switch
        {
            "current_process" => context.CurrentProcess,
            "last_process" => context.LastProcess,
            "last_test_result" => context.LastTestResult switch
            {
                TestResult.Pass => "pass",
                TestResult.Fail => "fail",
                _ => null
            },
            "fail_type" => context.FailType,
            "instrument_type" => context.InstrumentType,
            "test_count_on_process" => context.TestCountOnCurrentProcess,
            _ => null
        };

    private static bool EqualsNormalized(object? actual, object expected)
    {
        if (actual is null)
            return expected is null || (expected is string es && string.IsNullOrEmpty(es));

        return string.Equals(
            Convert.ToString(actual),
            Convert.ToString(expected),
            StringComparison.OrdinalIgnoreCase);
    }

    private static bool IsIn(object? actual, object expected)
    {
        if (actual is null)
            return false;

        var actualText = Convert.ToString(actual);
        var values = expected switch
        {
            string[] sa => sa.Cast<object>(),
            object[] oa => oa,
            IEnumerable<string> ss => ss.Cast<object>(),
            IEnumerable<object> objs => objs,
            System.Collections.IEnumerable e => e.Cast<object>(),
            _ => Enumerable.Empty<object>()
        };

        return values.Any(v =>
            string.Equals(actualText, Convert.ToString(v), StringComparison.OrdinalIgnoreCase));
    }
}
