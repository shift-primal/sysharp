public class Cpu
{
    public string Name { get; private set; }
    public int Cores { get; private set; }
    public int Threads { get; private set; }
    public double ClockSpeed { get; private set; }

    public Cpu(List<Hardware.Info.CPU> cpuList)
    {
        var cpu = cpuList.FirstOrDefault();

        if (cpu != null)
        {
            Name = cpu.Name;
            Cores = (int)cpu.NumberOfCores;
            Threads = (int)cpu.NumberOfLogicalProcessors;
            ClockSpeed = cpu.CurrentClockSpeed;
        }
    }
}
