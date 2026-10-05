
namespace ShareData;

public class ShareAlgoData
{

}

public class CommonDataHistory
{
    public List<DVector2> StopwatchTime { get; set; } = new List<DVector2>();
    public List<double> UsagePercentageCpu { get; set; } = new List<double>();
    public List<double> UsagePercentageRamMb { get; set; } = new List<double>();
    public List<double> UsagePercentageGpu { get; set; } = new List<double>();
    public List<SlotSearchedWithTime> SlotSearchedHistoryWithTime { get; set; } = new List<SlotSearchedWithTime>();
}