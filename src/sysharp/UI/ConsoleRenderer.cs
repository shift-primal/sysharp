using System.Collections;

public static class ConsoleRenderer
{
    public static void Init()
    {
        Console.Clear();
    }

    public static void PrintPart(object? part, string? name = null)
    {
        if (part == null)
            return;

        var type = part.GetType();

        if (part is IEnumerable enumerable and not string)
        {
            var items = enumerable.Cast<object>().ToList();
            var elementType = type.IsGenericType ? type.GetGenericArguments()[0].Name : "Items";

            Console.WriteLine($"=== {name ?? elementType} ({items.Count}) ===");

            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"  [{i + 1}]");
                PrintProperties(items[i], "    ");
            }
        }
        else
        {
            Console.WriteLine($"=== {name ?? type.Name} ===");
            PrintProperties(part, "  ");
        }

        Console.WriteLine();
    }

    private static void PrintProperties(object obj, string indent)
    {
        foreach (var p in obj.GetType().GetProperties())
        {
            if (p.GetIndexParameters().Length > 0)
                continue;

            var value = p.GetValue(obj);
            Console.WriteLine($"{indent}{p.Name}: {value}");
        }
    }
}
