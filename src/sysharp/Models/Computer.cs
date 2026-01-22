public class Computer
{
    public Cpu Cpu { get; private set; } = null!;
    public List<MemoryStick> Memory { get; private set; } = null!;
    public List<Drive> Drives { get; private set; } = null!;
    public Gpu Gpu { get; private set; } = null!;

    private Computer() { }

    public static Computer Collect()
    {
        return new Computer
        {
            Cpu = HardwareCollector.GetCpu(),
            Memory = HardwareCollector.GetMemory(),
            Drives = HardwareCollector.GetDrives(),
            Gpu = HardwareCollector.GetGpu(),
        };
    }
}
