public class Computer
{
    public Cpu Cpu { get; private set; } = null!;
    public List<Memory> Memory { get; private set; } = null!;

    // public Drive Drive { get; private set; }
    // public Gpu Gpu { get; private set; }

    private Computer() { }

    public static Computer Collect()
    {
        return new Computer { Cpu = HardwareCollector.GetCpu() };
    }
}
