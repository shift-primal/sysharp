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

    // === DRIVES ===
    public static List<MemoryStick> GetMemory() { }
}
