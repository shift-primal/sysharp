public static class HardwareCollector
{
    // === CPU ===

    public static Cpu GetCpu()
    {
        var raw = CommandRunner.RunCommand(Commands.CpuInfo);

        return new Cpu(
            Name: GetCpuName(raw),
            Cores: GetCpuCores(raw),
            Threads: GetCpuThreads(raw),
            MaxClockRate: GetCpuMaxClockRate(raw)
        );
    }

    private static string GetCpuName(string cpu) =>
        Formatting.ExtractValue(cpu, @"Model name:\s*(.+)") ?? "Unknown";

    private static int GetCpuCores(string cpu) =>
        int.TryParse(Formatting.ExtractValue(cpu, @"CPU\(s\):\s*(\d+)"), out var c) ? c : 0;

    private static int GetCpuThreads(string cpu) =>
        int.TryParse(Formatting.ExtractValue(cpu, @"Core\(s\) per socket:\s*(\d+)"), out var t)
            ? t
            : 0;

    private static int GetCpuMaxClockRate(string cpu) =>
        int.TryParse(Formatting.ExtractValue(cpu, @"CPU max MHz:\s*(\d+)"), out var cr) ? cr : 0;

    // === RAM ===

    public static List<MemoryStick> GetMemory()
    {
        List<MemoryStick> ramSticks = [];

        var raw = CommandRunner.RunCommand(Commands.RamSticks);
        var sticksArr = raw.Split("Memory Device");

        foreach (var stick in sticksArr)
        {
            if (GetMemorySlot(stick) == "Unknown")
                continue;

            ramSticks.Add(
                new MemoryStick(
                    Manufacturer: GetMemoryManufacturer(stick),
                    RamType: GetMemoryType(stick),
                    Capacity: new Capacity(GetMemoryCapacity(stick)),
                    Speed: GetMemorySpeed(stick),
                    Slot: GetMemorySlot(stick)
                )
            );
        }

        return ramSticks;
    }

    private static string GetMemoryManufacturer(string stick) =>
        Formatting.ExtractValue(stick, @"Manufacturer:\s*(.+)") ?? "Unknown";

    private static string GetMemoryType(string stick) =>
        int.TryParse(Formatting.ExtractValue(stick, @"Type:\s*(\d+)"), out var t)
            ? "DDR" + t
            : "Unknown";

    private static int GetMemoryCapacity(string stick) =>
        int.TryParse(Formatting.ExtractValue(stick, @"Size:\s*(\d+)"), out var c) ? c : 0;

    private static int GetMemorySpeed(string stick) =>
        int.TryParse(Formatting.ExtractValue(stick, @"Speed:\s*(\d+)"), out var s) ? s : 0;

    private static string GetMemorySlot(string stick) =>
        Formatting.ExtractValue(stick, @"Locator:\s*(.+)")?[^2..] ?? "Unknown";

    // === DISKS ===

    public static List<Drive> GetDrives()
    {
        List<Drive> drives = [];

        var raw = CommandRunner.RunCommand(Commands.Drives);
        var drivesArr = raw.Split(["\r\n", "\r", "\n"], StringSplitOptions.None);

        foreach (var drive in drivesArr)
        {
            if (GetDriveCapacity(drive) <= 0)
                continue;

            drives.Add(
                new Drive(
                    Name: GetDriveName(drive),
                    Capacity: new Capacity(GetDriveCapacity(drive))
                )
            );
        }

        return drives;
    }

    private static string GetDriveName(string drive) =>
        Formatting.ExtractValue(drive, @"^\S+\s+(.+?)\s+\d+$") ?? "Unknown";

    private static int GetDriveCapacity(string drive) =>
        long.TryParse(Formatting.ExtractValue(drive, @"(\d+)$"), out long c)
            ? (int)(c / 1_000_000_000)
            : 0;

    // === GPU ===

    public static Gpu GetGpu()
    {
        var raw = CommandRunner.RunCommand(Commands.GpuInfo);

        return new Gpu(Name: GetGpuName(raw), Manufacturer: GetGpuManufacturer(raw));
    }

    private static string GetGpuName(string gpu) =>
        Formatting.ExtractValue(gpu, @"\[([^\]]+)\]") ?? "Unknown";

    private static string GetGpuManufacturer(string gpu) =>
        Formatting.ExtractValue(gpu, @"controller:\s*(\w+)") ?? "Unknown";
}
