public class App
{
    private Computer? computer;

    public void Run()
    {
        computer = Computer.Collect();

        ConsoleRenderer.Init();

        foreach (var p in computer.GetType().GetProperties())
            ConsoleRenderer.PrintPart(p.GetValue(computer), p.Name);
    }
}
