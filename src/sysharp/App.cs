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
            Console.WriteLine(computer.Cpu);

            foreach (var ms in computer.Memory)
                Console.WriteLine(ms);

            foreach (var d in computer.Drives)
                Console.WriteLine(d);

            Console.WriteLine(computer.Gpu);

            Console.ReadKey(true);
        }
    }
}
