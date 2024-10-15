using System.Text;
using System.Text.RegularExpressions;

namespace fishsay;

#pragma warning disable CA1416

class Program
{
    static void Main(string[] args)
    {
        Utils.EnableVTMode();

        (int left, int top) = Console.GetCursorPosition();
        string fish = FishDB.GetFish();
        string lastOutput = "";

        StringBuilder text = new();

        string GetSbText()
        {
            return Color(text.ToString());
        }

        DateTime start = DateTime.Now;
        bool fast = false;

        while (ReadLine() is { } s)
        {
            text.AppendLine(s);

            if ((DateTime.Now - start).TotalMilliseconds < 500)
            {
                Console.Write(Shadow(lastOutput));
                fast = true;
                continue;
            }

            start = DateTime.Now;
            fast = false;

            Console.SetCursorPosition(left, top);
            Console.Write(Shadow(lastOutput));

            Console.SetCursorPosition(left, top);
            string output = FishDB.Say(fish, GetSbText());
            lastOutput = output;
            PrintFish(output);
        }

        if (fast)
        {
            string output = FishDB.Say(fish, GetSbText());
            PrintFish(output);
        }

        Console.WriteLine();
    }

    private static void PrintFish(string output)
    {
        // Clean up if the user makes a mess 
        Console.Write(output + "\x1b[0m");
    }

    private static string? ReadLine()
    {
        return Console.ReadLine();
    }

    static string Shadow(string text)
    {
        // StringBuilder sb = new();
        // string[] lines = text.Split('\n');
        // int maxLength = lines.Max(x => x.Length);
        // foreach (string line in lines)
        //     sb.AppendLine(new(' ', maxLength));
        // return sb.ToString();

        StringBuilder sb = new();
        foreach (char c in text)
            sb.Append(c == '\n' ? c : ' ');
        return sb.ToString();
    }

    static string Color(string text)
    {
        const string esc = "\x1b";

        Dictionary<string, (int, int)> colors = new()
        {
            { "black", (30, 40) },
            { "red", (31, 41) },
            { "green", (32, 42) },
            { "yellow", (33, 43) },
            { "blue", (34, 44) },
            { "magenta", (35, 45) },
            { "cyan", (36, 46) },
            { "white", (37, 47) },
            { "lightblack", (90, 100) },
            { "lightred", (91, 101) },
            { "lightgreen", (92, 102) },
            { "lightyellow", (93, 103) },
            { "lightblue", (94, 104) },
            { "lightmagenta", (95, 105) },
            { "lightcyan", (96, 106) },
            { "lightwhite", (97, 107) }
        };

        Dictionary<string, int> styles = new()
        {
            { "bold", 1 },
            { "dim", 2 },
            { "italic", 3 },
            { "underline", 4 },
            { "blink", 5 },
            { "reverse", 7 },
            { "hidden", 8 },
            { "strikethrough", 9 }
        };

        int reset = 0;

        string result = text;
        // find "%red%"
        foreach (Match match in Regex.Matches(text, "%(.*?)%"))
        {
            string code = match.Groups[1].Value;
            if (colors.TryGetValue(code.Replace("#", ""), out (int, int) color))
            {
                // if it starts with a hash, it's a background color
                bool isBackground = code[0] == '#';
                result = result.Replace(match.Value, $"{esc}[{(isBackground ? color.Item2 : color.Item1)}m");
            }
            else if (styles.TryGetValue(code, out int style))
                result = result.Replace(match.Value, $"{esc}[{style}m");
            else if (code == "reset")
                result = result.Replace(match.Value, $"{esc}[{reset}m");
            else if (code == "")
                result = result.Replace(match.Value, "%");
        }

        return result;
    }
}