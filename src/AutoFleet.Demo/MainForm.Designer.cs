namespace AutoFleet.Demo;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;
    private TabControl tabMain;
    private TabPage tabSchedule;
    private TabPage tabData;
    private TabControl tabDetail;
    private TabPage tabMachines;
    private TabPage tabRules;
    private TabPage tabTags;
    private Label lblHint;
    private Label lblScenario;
    private ComboBox cboScenario;
    private Button btnSchedule;
    private Label lblNextProcess;
    private Label lblNextProcessValue;
    private Label lblSn;
    private Label lblSnValue;
    private GroupBox grpResult;
    private Label lblRule;
    private Label lblRuleValue;
    private Label lblMachine;
    private Label lblMachineValue;
    private Label lblDca;
    private Label lblDcaValue;
    private TextBox txtResult;
    private DataGridView gridScores;
    private DataGridView gridMachines;
    private DataGridView gridRules;
    private DataGridView gridTags;
    private Label lblInstrumentHint;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        tabMain = new TabControl();
        tabSchedule = new TabPage();
        gridScores = new DataGridView();
        grpResult = new GroupBox();
        txtResult = new TextBox();
        lblDcaValue = new Label();
        lblDca = new Label();
        lblMachineValue = new Label();
        lblMachine = new Label();
        lblRuleValue = new Label();
        lblRule = new Label();
        lblSnValue = new Label();
        lblSn = new Label();
        lblNextProcessValue = new Label();
        lblNextProcess = new Label();
        btnSchedule = new Button();
        cboScenario = new ComboBox();
        lblScenario = new Label();
        lblHint = new Label();
        tabData = new TabPage();
        tabDetail = new TabControl();
        tabMachines = new TabPage();
        gridMachines = new DataGridView();
        tabRules = new TabPage();
        gridRules = new DataGridView();
        tabTags = new TabPage();
        gridTags = new DataGridView();
        lblInstrumentHint = new Label();
        tabMain.SuspendLayout();
        tabSchedule.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)gridScores).BeginInit();
        grpResult.SuspendLayout();
        tabData.SuspendLayout();
        tabDetail.SuspendLayout();
        tabMachines.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)gridMachines).BeginInit();
        tabRules.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)gridRules).BeginInit();
        tabTags.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)gridTags).BeginInit();
        SuspendLayout();
        // 
        // tabMain
        // 
        tabMain.Controls.Add(tabSchedule);
        tabMain.Controls.Add(tabData);
        tabMain.Dock = DockStyle.Fill;
        tabMain.Location = new Point(0, 0);
        tabMain.Margin = new Padding(4, 4, 4, 4);
        tabMain.Name = "tabMain";
        tabMain.SelectedIndex = 0;
        tabMain.Size = new Size(1265, 719);
        tabMain.TabIndex = 0;
        // 
        // tabSchedule
        // 
        tabSchedule.Controls.Add(gridScores);
        tabSchedule.Controls.Add(grpResult);
        tabSchedule.Controls.Add(lblSnValue);
        tabSchedule.Controls.Add(lblSn);
        tabSchedule.Controls.Add(lblNextProcessValue);
        tabSchedule.Controls.Add(lblNextProcess);
        tabSchedule.Controls.Add(btnSchedule);
        tabSchedule.Controls.Add(cboScenario);
        tabSchedule.Controls.Add(lblScenario);
        tabSchedule.Controls.Add(lblHint);
        tabSchedule.Location = new Point(4, 29);
        tabSchedule.Margin = new Padding(4, 4, 4, 4);
        tabSchedule.Name = "tabSchedule";
        tabSchedule.Padding = new Padding(15, 14, 15, 14);
        tabSchedule.Size = new Size(1257, 686);
        tabSchedule.TabIndex = 0;
        tabSchedule.Text = "调度模拟";
        tabSchedule.UseVisualStyleBackColor = true;
        // 
        // gridScores
        // 
        gridScores.AllowUserToAddRows = false;
        gridScores.AllowUserToDeleteRows = false;
        gridScores.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        gridScores.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        gridScores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle1.BackColor = SystemColors.Window;
        dataGridViewCellStyle1.Font = new Font("Microsoft YaHei UI", 9F);
        dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
        gridScores.DefaultCellStyle = dataGridViewCellStyle1;
        gridScores.Location = new Point(19, 280);
        gridScores.Margin = new Padding(4, 4, 4, 4);
        gridScores.Name = "gridScores";
        gridScores.ReadOnly = true;
        gridScores.RowHeadersVisible = false;
        gridScores.RowHeadersWidth = 51;
        gridScores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridScores.Size = new Size(1211, 381);
        gridScores.TabIndex = 0;
        // 
        // grpResult
        // 
        grpResult.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        grpResult.Controls.Add(txtResult);
        grpResult.Controls.Add(lblDcaValue);
        grpResult.Controls.Add(lblDca);
        grpResult.Controls.Add(lblMachineValue);
        grpResult.Controls.Add(lblMachine);
        grpResult.Controls.Add(lblRuleValue);
        grpResult.Controls.Add(lblRule);
        grpResult.Location = new Point(19, 115);
        grpResult.Margin = new Padding(4, 4, 4, 4);
        grpResult.Name = "grpResult";
        grpResult.Padding = new Padding(4, 4, 4, 4);
        grpResult.Size = new Size(1211, 153);
        grpResult.TabIndex = 4;
        grpResult.TabStop = false;
        grpResult.Text = "调度结果";
        // 
        // txtResult
        // 
        txtResult.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtResult.Location = new Point(19, 115);
        txtResult.Margin = new Padding(4, 4, 4, 4);
        txtResult.Name = "txtResult";
        txtResult.ReadOnly = true;
        txtResult.Size = new Size(1171, 27);
        txtResult.TabIndex = 0;
        // 
        // lblDcaValue
        // 
        lblDcaValue.AutoSize = true;
        lblDcaValue.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        lblDcaValue.Location = new Point(675, 33);
        lblDcaValue.Margin = new Padding(4, 0, 4, 0);
        lblDcaValue.Name = "lblDcaValue";
        lblDcaValue.Size = new Size(16, 19);
        lblDcaValue.TabIndex = 1;
        lblDcaValue.Text = "-";
        // 
        // lblDca
        // 
        lblDca.AutoSize = true;
        lblDca.Location = new Point(579, 33);
        lblDca.Margin = new Padding(4, 0, 4, 0);
        lblDca.Name = "lblDca";
        lblDca.Size = new Size(90, 20);
        lblDca.TabIndex = 2;
        lblDca.Text = "产品 DCA：";
        // 
        // lblMachineValue
        // 
        lblMachineValue.AutoSize = true;
        lblMachineValue.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        lblMachineValue.Location = new Point(116, 65);
        lblMachineValue.Margin = new Padding(4, 0, 4, 0);
        lblMachineValue.Name = "lblMachineValue";
        lblMachineValue.Size = new Size(16, 19);
        lblMachineValue.TabIndex = 3;
        lblMachineValue.Text = "-";
        // 
        // lblMachine
        // 
        lblMachine.AutoSize = true;
        lblMachine.Location = new Point(19, 65);
        lblMachine.Margin = new Padding(4, 0, 4, 0);
        lblMachine.Name = "lblMachine";
        lblMachine.Size = new Size(84, 20);
        lblMachine.TabIndex = 4;
        lblMachine.Text = "目标机台：";
        // 
        // lblRuleValue
        // 
        lblRuleValue.AutoSize = true;
        lblRuleValue.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        lblRuleValue.Location = new Point(116, 33);
        lblRuleValue.Margin = new Padding(4, 0, 4, 0);
        lblRuleValue.Name = "lblRuleValue";
        lblRuleValue.Size = new Size(16, 19);
        lblRuleValue.TabIndex = 5;
        lblRuleValue.Text = "-";
        // 
        // lblRule
        // 
        lblRule.AutoSize = true;
        lblRule.Location = new Point(19, 33);
        lblRule.Margin = new Padding(4, 0, 4, 0);
        lblRule.Name = "lblRule";
        lblRule.Size = new Size(84, 20);
        lblRule.TabIndex = 6;
        lblRule.Text = "命中规则：";
        // 
        // lblSnValue
        // 
        lblSnValue.AutoSize = true;
        lblSnValue.Location = new Point(116, 85);
        lblSnValue.Margin = new Padding(4, 0, 4, 0);
        lblSnValue.Name = "lblSnValue";
        lblSnValue.Size = new Size(15, 20);
        lblSnValue.TabIndex = 5;
        lblSnValue.Text = "-";
        // 
        // lblSn
        // 
        lblSn.AutoSize = true;
        lblSn.Location = new Point(19, 85);
        lblSn.Margin = new Padding(4, 0, 4, 0);
        lblSn.Name = "lblSn";
        lblSn.Size = new Size(79, 20);
        lblSn.TabIndex = 6;
        lblSn.Text = "产品 SN：";
        // 
        // lblNextProcessValue
        // 
        lblNextProcessValue.AutoSize = true;
        lblNextProcessValue.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        lblNextProcessValue.Location = new Point(945, 49);
        lblNextProcessValue.Margin = new Padding(4, 0, 4, 0);
        lblNextProcessValue.Name = "lblNextProcessValue";
        lblNextProcessValue.Size = new Size(16, 19);
        lblNextProcessValue.TabIndex = 7;
        lblNextProcessValue.Text = "-";
        // 
        // lblNextProcess
        // 
        lblNextProcess.AutoSize = true;
        lblNextProcess.Location = new Point(849, 49);
        lblNextProcess.Margin = new Padding(4, 0, 4, 0);
        lblNextProcess.Name = "lblNextProcess";
        lblNextProcess.Size = new Size(84, 20);
        lblNextProcess.TabIndex = 8;
        lblNextProcess.Text = "下一工序：";
        // 
        // btnSchedule
        // 
        btnSchedule.Location = new Point(675, 42);
        btnSchedule.Margin = new Padding(4, 4, 4, 4);
        btnSchedule.Name = "btnSchedule";
        btnSchedule.Size = new Size(154, 35);
        btnSchedule.TabIndex = 2;
        btnSchedule.Text = "执行调度";
        btnSchedule.UseVisualStyleBackColor = true;
        btnSchedule.Click += btnSchedule_Click;
        // 
        // cboScenario
        // 
        cboScenario.DropDownStyle = ComboBoxStyle.DropDownList;
        cboScenario.Location = new Point(116, 45);
        cboScenario.Margin = new Padding(4, 4, 4, 4);
        cboScenario.Name = "cboScenario";
        cboScenario.Size = new Size(539, 28);
        cboScenario.TabIndex = 1;
        // 
        // lblScenario
        // 
        lblScenario.AutoSize = true;
        lblScenario.Location = new Point(19, 49);
        lblScenario.Margin = new Padding(4, 0, 4, 0);
        lblScenario.Name = "lblScenario";
        lblScenario.Size = new Size(84, 20);
        lblScenario.TabIndex = 9;
        lblScenario.Text = "选择场景：";
        // 
        // lblHint
        // 
        lblHint.AutoSize = true;
        lblHint.ForeColor = SystemColors.GrayText;
        lblHint.Location = new Point(19, 14);
        lblHint.Margin = new Padding(4, 0, 4, 0);
        lblHint.Name = "lblHint";
        lblHint.Size = new Size(549, 20);
        lblHint.TabIndex = 10;
        lblHint.Text = "模拟：A 工序测完成后，搬运系统根据结果请求下一目标（工序由系统自动推导）";
        // 
        // tabData
        // 
        tabData.Controls.Add(tabDetail);
        tabData.Location = new Point(4, 29);
        tabData.Margin = new Padding(4, 4, 4, 4);
        tabData.Name = "tabData";
        tabData.Padding = new Padding(5, 5, 5, 5);
        tabData.Size = new Size(1257, 686);
        tabData.TabIndex = 1;
        tabData.Text = "示例数据";
        tabData.UseVisualStyleBackColor = true;
        // 
        // tabDetail
        // 
        tabDetail.Controls.Add(tabMachines);
        tabDetail.Controls.Add(tabRules);
        tabDetail.Controls.Add(tabTags);
        tabDetail.Dock = DockStyle.Fill;
        tabDetail.Location = new Point(5, 5);
        tabDetail.Margin = new Padding(4, 4, 4, 4);
        tabDetail.Name = "tabDetail";
        tabDetail.SelectedIndex = 0;
        tabDetail.Size = new Size(1247, 676);
        tabDetail.TabIndex = 0;
        // 
        // tabMachines
        // 
        tabMachines.Controls.Add(gridMachines);
        tabMachines.Location = new Point(4, 29);
        tabMachines.Margin = new Padding(4, 4, 4, 4);
        tabMachines.Name = "tabMachines";
        tabMachines.Padding = new Padding(8, 7, 8, 7);
        tabMachines.Size = new Size(1239, 643);
        tabMachines.TabIndex = 0;
        tabMachines.Text = "机台列表";
        tabMachines.UseVisualStyleBackColor = true;
        // 
        // gridMachines
        // 
        gridMachines.AllowUserToAddRows = false;
        gridMachines.AllowUserToDeleteRows = false;
        gridMachines.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridMachines.ColumnHeadersHeight = 29;
        gridMachines.Dock = DockStyle.Fill;
        gridMachines.Location = new Point(8, 7);
        gridMachines.Margin = new Padding(4, 4, 4, 4);
        gridMachines.Name = "gridMachines";
        gridMachines.ReadOnly = true;
        gridMachines.RowHeadersVisible = false;
        gridMachines.RowHeadersWidth = 51;
        gridMachines.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridMachines.Size = new Size(1223, 629);
        gridMachines.TabIndex = 0;
        // 
        // tabRules
        // 
        tabRules.Controls.Add(gridRules);
        tabRules.Location = new Point(4, 29);
        tabRules.Margin = new Padding(4, 4, 4, 4);
        tabRules.Name = "tabRules";
        tabRules.Padding = new Padding(8, 7, 8, 7);
        tabRules.Size = new Size(1237, 641);
        tabRules.TabIndex = 1;
        tabRules.Text = "调度规则";
        tabRules.UseVisualStyleBackColor = true;
        // 
        // gridRules
        // 
        gridRules.AllowUserToAddRows = false;
        gridRules.AllowUserToDeleteRows = false;
        gridRules.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridRules.ColumnHeadersHeight = 29;
        gridRules.Dock = DockStyle.Fill;
        gridRules.Location = new Point(8, 7);
        gridRules.Margin = new Padding(4, 4, 4, 4);
        gridRules.Name = "gridRules";
        gridRules.ReadOnly = true;
        gridRules.RowHeadersVisible = false;
        gridRules.RowHeadersWidth = 51;
        gridRules.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridRules.Size = new Size(1221, 627);
        gridRules.TabIndex = 0;
        // 
        // tabTags
        // 
        tabTags.Controls.Add(gridTags);
        tabTags.Controls.Add(lblInstrumentHint);
        tabTags.Location = new Point(4, 29);
        tabTags.Margin = new Padding(4, 4, 4, 4);
        tabTags.Name = "tabTags";
        tabTags.Padding = new Padding(8, 7, 8, 7);
        tabTags.Size = new Size(1237, 641);
        tabTags.TabIndex = 2;
        tabTags.Text = "标签定义";
        tabTags.UseVisualStyleBackColor = true;
        // 
        // gridTags
        // 
        gridTags.AllowUserToAddRows = false;
        gridTags.AllowUserToDeleteRows = false;
        gridTags.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridTags.ColumnHeadersHeight = 29;
        gridTags.Dock = DockStyle.Fill;
        gridTags.Location = new Point(8, 34);
        gridTags.Margin = new Padding(4, 4, 4, 4);
        gridTags.Name = "gridTags";
        gridTags.ReadOnly = true;
        gridTags.RowHeadersVisible = false;
        gridTags.RowHeadersWidth = 51;
        gridTags.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridTags.Size = new Size(1221, 600);
        gridTags.TabIndex = 0;
        // 
        // lblInstrumentHint
        // 
        lblInstrumentHint.AutoSize = true;
        lblInstrumentHint.Dock = DockStyle.Top;
        lblInstrumentHint.Location = new Point(8, 7);
        lblInstrumentHint.Margin = new Padding(4, 0, 4, 0);
        lblInstrumentHint.Name = "lblInstrumentHint";
        lblInstrumentHint.Padding = new Padding(0, 0, 0, 7);
        lblInstrumentHint.Size = new Size(69, 27);
        lblInstrumentHint.TabIndex = 1;
        lblInstrumentHint.Text = "标签说明";
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(9F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1265, 719);
        Controls.Add(tabMain);
        Font = new Font("Microsoft YaHei UI", 9F);
        Margin = new Padding(4, 4, 4, 4);
        MinimumSize = new Size(1152, 698);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "AutoFleet 调度 Demo（场景驱动）";
        tabMain.ResumeLayout(false);
        tabSchedule.ResumeLayout(false);
        tabSchedule.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)gridScores).EndInit();
        grpResult.ResumeLayout(false);
        grpResult.PerformLayout();
        tabData.ResumeLayout(false);
        tabDetail.ResumeLayout(false);
        tabMachines.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)gridMachines).EndInit();
        tabRules.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)gridRules).EndInit();
        tabTags.ResumeLayout(false);
        tabTags.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)gridTags).EndInit();
        ResumeLayout(false);
    }
}
