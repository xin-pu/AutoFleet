using AutoFleet.Demo.Data;
using AutoFleet.Demo.Models;
using AutoFleet.Demo.Services;

namespace AutoFleet.Demo;

public partial class MainForm : Form
{
    private readonly SchedulingEngine _engine = new();

    public MainForm()
    {
        InitializeComponent();
        ConfigureScoreGrid();
        LoadSeedDataViews();
        LoadScenarios();
    }

    private void ConfigureScoreGrid()
    {
        gridScores.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        gridScores.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        gridScores.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
        gridScores.ScrollBars = ScrollBars.Both;
    }

    private void LoadScenarios()
    {
        cboScenario.Items.Clear();
        foreach (var scenario in SeedData.DemoScenarios)
            cboScenario.Items.Add(scenario);

        cboScenario.DisplayMember = nameof(DemoScenario.Title);
        cboScenario.SelectedIndex = 0;
        cboScenario.SelectedIndexChanged += (_, _) => UpdateScenarioPreview();
        UpdateScenarioPreview();
    }

    private void UpdateScenarioPreview()
    {
        if (cboScenario.SelectedItem is not DemoScenario scenario)
            return;

        var processLabel = scenario.ResolvedProcessCode == "FAIL"
            ? "Fail区"
            : scenario.ResolvedProcessCode;

        lblNextProcessValue.Text = processLabel;
        lblDcaValue.Text = scenario.InstrumentType;
        lblSnValue.Text = scenario.Sn;
    }

    private void LoadSeedDataViews()
    {
        gridMachines.DataSource = SeedData.Machines
            .Select(m => new
            {
                编码 = m.Code,
                名称 = m.Name,
                工序 = m.ProcessCode,
                状态 = m.Status switch
                {
                    MachineStatus.Online => "在线",
                    MachineStatus.Offline => "离线",
                    MachineStatus.Maintenance => "维护中",
                    _ => m.Status.ToString()
                },
                标签 = string.Join(", ", SeedData.GetTagsForMachine(m).Select(t => t.Display))
            })
            .ToList();

        gridRules.DataSource = SeedData.Rules
            .OrderBy(r => r.Priority)
            .Select(r => new
            {
                优先级 = r.Priority,
                编码 = r.Code,
                名称 = r.Name,
                启用 = r.IsActive ? "是" : "否",
                触发条件 = FormatTrigger(r.Trigger),
                打分项 = FormatScoreItems(r.ScoreItems)
            })
            .ToList();

        gridTags.DataSource = SeedData.Tags
            .Select(t => new { t.Id, 维度 = t.Dimension, 值 = t.Value, 显示 = t.Display })
            .ToList();

        lblInstrumentHint.Text = "标签说明：DCA类型(K0000/A0001) 区分仪表；良率档(高/低) 模拟机台历史良率；区域=Fail区 为报废接收。";
    }

    private void btnSchedule_Click(object sender, EventArgs e)
    {
        if (cboScenario.SelectedItem is not DemoScenario scenario)
            return;

        UpdateScenarioPreview();

        var result = _engine.Schedule(scenario);

        txtResult.Text = result.Message;
        lblRuleValue.Text = result.MatchedRule?.Name ?? "-";
        lblMachineValue.Text = result.SelectedMachine is null
            ? "-"
            : $"{result.SelectedMachine.Code}（{result.SelectedMachine.Name}）";

        gridScores.DataSource = null;
        if (result.Candidates.Count == 0)
            return;

        gridScores.DataSource = result.Candidates
            .Select(c => new
            {
                机台 = c.Machine.Code,
                名称 = c.Machine.Name,
                总分 = c.TotalScore,
                打分明细 = string.Join(Environment.NewLine, c.Items.Select(i => $"· {i.Description} => {i.Score}"))
            })
            .ToList();

        ApplyScoreGridColumnLayout();
    }

    private void ApplyScoreGridColumnLayout()
    {
        if (gridScores.Columns.Count == 0)
            return;

        gridScores.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

        foreach (DataGridViewColumn col in gridScores.Columns)
            col.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

        if (gridScores.Columns["机台"] is { } colCode)
        {
            colCode.Width = 80;
            colCode.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colCode.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        }

        if (gridScores.Columns["名称"] is { } colName)
        {
            colName.Width = 130;
            colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colName.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        }

        if (gridScores.Columns["总分"] is { } colScore)
        {
            colScore.Width = 56;
            colScore.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colScore.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        }

        if (gridScores.Columns["打分明细"] is { } colDetail)
        {
            colDetail.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDetail.MinimumWidth = 200;
            colDetail.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        gridScores.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        gridScores.ClearSelection();
    }

    private static string FormatTrigger(TriggerCondition trigger)
    {
        var parts = trigger.Conditions.Select(c =>
        {
            var op = c.Op switch
            {
                ConditionOperator.Eq => "=",
                ConditionOperator.Ne => "!=",
                ConditionOperator.In => " IN ",
                ConditionOperator.NotIn => " NOT IN ",
                _ => "?"
            };

            var val = c.Value switch
            {
                string[] sa => $"({string.Join(", ", sa)})",
                object[] oa => $"({string.Join(", ", oa)})",
                System.Collections.IEnumerable ie and not string =>
                    $"({string.Join(", ", ie.Cast<object>())})",
                _ => c.Value?.ToString() ?? "null"
            };

            return $"{c.Field} {op} {val}";
        });

        return string.Join(" AND ", parts);
    }

    private static string FormatScoreItems(List<ScoreItem> items)
    {
        return string.Join("；", items.Select(i =>
        {
            var tag = SeedData.GetTag(i.TagId);
            var tagName = tag?.Display ?? $"标签{i.TagId}";
            var action = i.MatchType switch
            {
                TagMatchType.ContainsAdd => i.RequireProductInstrumentMatch ? "匹配产品DCA则+" : "有则+",
                TagMatchType.NotContainsAdd => "无则+",
                TagMatchType.ContainsSubtract => "有则-",
                TagMatchType.NotContainsSubtract => "无则-",
                _ => "?"
            };
            return $"{tagName} [{action}{i.WeightScore}]";
        }));
    }
}
