public class Memory
{
    public string Manufacturer { get; private set; }
    public int Capacity { get; private set; }
    public int Speed { get; private set; }

    public Memory(List<Hardware.Info.Memory> memoryList)
    {
        var memory = memoryList.FirstOrDefault();

        if (memory != null)
        {
            Manufacturer = memoryList.Manufacturer;
            Capacity = (int)memoryList.Capacity;
            Speed = (int)memoryList.Speed;
        }
    }
}
