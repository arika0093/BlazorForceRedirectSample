namespace BlazorForceRedirectSample.Services;

public class SampleSettingService
{
    // 適当な設定項目のサンプル
    public bool ForceRedirectEnabled { get; set; } = true;

    // 適当な判定関数のサンプル
    public Task<bool> Check() => Task.FromResult(ForceRedirectEnabled);
}
