using System.Text;
using System.Text.RegularExpressions;

namespace fishsay;



public partial class FishDB
{
    private static readonly Random _rand = new();

    public static string GetFish()
    {
        return _fish[_rand.Next(_fish.Length)];
        // return _fish[0];
    }
    
    public static string Say(string fish, string text)
    {
        // Get the fish
        string[] fishLines = fish.Split('\n');
        
        string wrappedText = WrapText(text, 40);
        string[] textLines = wrappedText.Split('\n');
        int maxWidth = textLines.Max(x => x.Length);
        
        StringBuilder sb = new();
        int textLineIndex = 0;
        for (int i = 0; i < fishLines.Length; i++)
        {
            string fishLine = fishLines[i];
            if (fishLine.Contains("%s"))
            {
                // Insert the text
                sb.AppendLine(fishLine.Replace("%s", textLines[textLineIndex]));
                if (textLineIndex < textLines.Length - 1)
                    i--;
                    
                textLineIndex++;
            }
            else
            {
                MatchCollection matches = Regex.Matches(fishLine, "{([.\\-_=+|\\\\/~])\\*([a-z])([+-])(\\d+)}");
                
                string modifiedFishLine = fishLine;
                
                foreach (Match match in matches)
                {
                    string character = match.Groups[1].Value;
                    char source = match.Groups[2].Value[0];
                    string direction = match.Groups[3].Value;
                    int count = int.Parse(match.Groups[4].Value);
                    
                    int sourceValue = source switch
                    {
                        'm' => maxWidth, // max width
                        'n' => textLines.Length, // number of lines
                        't' => textLines[0].Length, // top line length
                        'b' => textLines[^1].Length, // bottom line length
                        'c' => textLines[textLineIndex].Length, // current line length
                        _ => throw new Exception("Invalid source character")
                    };
                    
                    string newCharacter = character;
                    if (direction == "+")
                        newCharacter = new(character[0], sourceValue + count);
                    else if (direction == "-")
                        newCharacter = new(character[0], Math.Max(sourceValue - count, 0));
                    
                    modifiedFishLine = modifiedFishLine.Replace(match.Value, newCharacter);
                }
                
                sb.AppendLine(modifiedFishLine);
            }
        }
        
        return sb.ToString();
    }
    
    private static string WrapText(string text, int width)
    {
        if (text.Contains('\n'))
            return text.Replace("\r", "").Trim('\n');
        
        // Wrap the text to the width
        List<string> lines = new();
        string[] words = text
            .Replace("\r", "")
            .Split(' ');
        string line = "";
        foreach (string word in words)
        {
            if (line.Length + word.Length > width)
            {
                lines.Add(line.TrimEnd());
                line = "";
            }
            line += word + " ";
        }
        
        if (line.Length > 0)
            lines.Add(line.TrimEnd());
        
        return string.Join("\n", lines);
    }
}