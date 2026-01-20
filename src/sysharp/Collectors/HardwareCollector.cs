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

    private static string GetCpuName(string raw) =>
        Formatting.ExtractValue(raw, @"Model name:\s*(.+)") ?? "Unknown";

    private static int GetCpuCores(string raw) =>
        int.TryParse(Formatting.ExtractValue(raw, @"CPU\(s\):\s*(\d+)"), out var c) ? c : 0;

    private static int GetCpuThreads(string raw) =>
        int.TryParse(Formatting.ExtractValue(raw, @"Core\(s\) per socket:\s*(\d+)"), out var t)
            ? t
            : 0;

    private static int GetCpuMaxClockRate(string raw) =>
        int.TryParse(Formatting.ExtractValue(raw, @"CPU max MHz:\s*(\d+)"), out var cr) ? cr : 0;

    // === RAM ===

    public static List<MemoryStick> GetMemory()
    {
        List<MemoryStick> ramSticks = new();
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
                    Capacity: GetMemoryCapacity(stick),
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
        List<Drive> drives = new();
        var rawDrives = CommandRunner.RunCommand(Commands.Drives);
        var rawModels = CommandRunner.RunCommand(Commands.DriveModels);

        var rawDrivesArr = rawDrives.Split(@"\n");

        foreach (var drive in rawDrivesArr)
            Console.WriteLine(drive);

        // Console.WriteLine(rawModels);

        return [new Drive(Manufacturer: "Hei")];
    }
}
