public class Drive
{
    public string Name { get; private set; }
    public int Size { get; private set; }
    public int Speed { get; private set; }

    public Drive(List<Hardware.Info.Drive> driveList)
    {
        var drive = driveList.FirstOrDefault();

        if (drive != null)
        {
            Name = drive.Name;
            Size = (int)drive.Size;
        }
    }
}
