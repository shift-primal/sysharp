using System.Diagnostics;
using System.Text.RegularExpressions;

public static class CommandRunner
{
  public static string RunCommand(string cmd, string args)
  {
    var psi = new ProcessStartInfo(cmd, args)
    {
      RedirectStandardOutput = true,
      RedirectStandardError = true,
      UseShellExecute = false,
      CreateNoWindow = true,
    };

    using var proc = Process.Start(psi)!;
    proc.WaitForExit();
    return proc.StandardOutput.ReadToEnd();
  }

  public static string RunCommand(CommandDef def)
  {
    if (def.RequiresSudo)
      return RunCommand("sudo", $"{def.Cmd} {def.Args}");

    return RunCommand(def.Cmd, def.Args);
  }
}
