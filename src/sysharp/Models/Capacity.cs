public readonly struct Capacity(int value)
{
    public int Value { get; } = value;

    public override string ToString() => $"{Value}GB";
}
