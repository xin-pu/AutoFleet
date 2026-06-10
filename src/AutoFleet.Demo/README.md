# AutoFleet Demo（WinForms）

场景驱动的简易调度演示。详细说明见 [docs/demo.md](../../docs/demo.md)、[策略配置指南](../../docs/策略配置指南.md)。

## 运行

```powershell
dotnet run --project src/AutoFleet.Demo
```

## 六个场景

| 场景 | 下一工序 | 预期机台 |
|------|----------|----------|
| 首次进A | A | M-A-02 |
| A通过去B A0001 | B | M-B-01 |
| A失败复测 | A | M-A-01 |
| A失败换机 | A | M-A-02 |
| A失败去Fail区 | FAIL | M-FAIL-01 |
| A通过去B K0000 | B | M-B-02 |

正式版见 `docs/设计手册.md`。
