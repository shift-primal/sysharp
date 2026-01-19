using Hardware.Info;

public class App
{
    static IHardwareInfo? hardwareInfo;
    private bool _running;

    public void Run()
    {
        try
        {
            hardwareInfo = new HardwareInfo();
            hardwareInfo.RefreshAll();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        Console.WriteLine("App starting...");

        if (hardwareInfo != null)
        {
            _running = true;
            Computer computer = new(hardwareInfo);
        }

        while (_running)
        {
            Console.WriteLine("Hello!");
            Console.ReadLine();
        }
    }
}
