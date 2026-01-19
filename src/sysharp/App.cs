public class App
{
    private bool _running;
    private Computer? computer;

    public void Run()
    {
        Console.WriteLine("App starting...");

        _running = true;
        computer = Computer.Collect();

        while (_running)
        {
            Console.WriteLine("Hello!");
            Console.WriteLine(computer.Cpu);
            Console.ReadKey(true);
        }
    }
}
