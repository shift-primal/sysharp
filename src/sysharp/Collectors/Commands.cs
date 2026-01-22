public record CommandDef(string Cmd, string Args = "", bool RequiresSudo = false);

public static class Commands
{
    public static CommandDef Drives => new("lsblk", "-b -d -o NAME,MODEL,SIZE --noheadings");
    public static CommandDef RamSticks => new("dmidecode", "--type 17", RequiresSudo: true);
    public static CommandDef CpuInfo => new("lscpu");
    public static CommandDef GpuInfo => new("bash", "-c \"lspci -v | grep -A 10 VGA");
}
