using Hardware.Info;

public class Computer
{
    public Cpu Cpu { get; private set; }
    public Drive Drive { get; private set; }
    public Gpu Gpu { get; private set; }
    public Memory Memory { get; private set; }

    public Computer(IHardwareInfo hardwareInfo)
    {
        Cpu = new(hardwareInfo.CpuList);
        Drive = new(hardwareInfo.DriveList);
        Gpu = new(hardwareInfo.VideoControllerList);
        Memory = new(hardwareInfo.MemoryStatus);
    }
}
