using System.Text;

namespace fishsay;

public class FishDB
{
    private static readonly Random _rand = new();
    private static readonly string[] _fish =
    [
        """
           -----
          %s
           ------
             \
              \
           ><((((°>  ><((((°>  ><((((°>  ><((((°>  ><((((°> 
        """,

        """
         %s
           \
            \         /"*._         _
             \    .-*'`    `*-.._.-'/
                < * ))     ,       ( 
                  `*-._`._(__.--*"`.\
        """,

        """
           __v_
          (____\/{
        """,

        """
                .
               ":"
             ___:____     |"\/"|
           ,'        `.    \  /
           |  O        \___/  |
         ~^~^~^~^~^~^~^~^~^~^~^~^~
        """,

        """
                    %s
                      o
              |\    o
             |  \    o
         |\ /    .\ o
         | |       (
         |/ \     /
             |  /
              |/
        """,

        """
                     ___
                  _ / __)_   °
         _  mrf .'_'-'\ /-'-. o ° 
         \'-._.'-'\ / _\-(O)_: O
          \ (__\/_ \ '._)  _\ o
          /.' (_.'----''./' 
                        '
        """,

        """""
                                     _.'.__
                                  _.'      .
            ':'.               .''   __ __  .
              '.:._          ./  _ ''     "-'.__
            .'''-: """-._    | .                "-"._
             '.     .    "._.'                       "
                '.   "-.___ .        .'          .  :o'.
                  |   .----  .      .           .'     (
                   '|  ----. '   ,.._                _-'
                    .' .---  |.""  .-:;.. _____.----'
                    |   .-""""    |      '
                  .'  _'         .'    _'
                 |_.-'    -cat-   '-.'
        """"",

        """
         _________         .    .
        (..       \_    ,  |\  /|
         \       O  \  /|  \ \/ /
          \______    \/ |   \  / 
             vvvv\    \ |   /  |
             \^^^^  ==   \_/   |
              `\_   ===    \.  |
              / /\_   \ /      |
              |/   \_  \|      /
         snd         \________/
        """
    ];

    public string GetFish()
    {
        return _fish[_rand.Next(_fish.Length)];
    }
    
    public string GetFish(string text)
    {
        // Get the fish
        string fish = this.GetFish();
        string[] fishLines = fish.Split('\n');
        
        string wrappedText = this.WrapText(text, 40);
        string[] textLines = wrappedText.Split('\n');
        
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
                sb.AppendLine(fishLine);
        }
        
        return sb.ToString();
    }
    
    public string WrapText(string text, int width)
    {
        // Wrap the text to the width
        List<string> lines = new();
        string[] words = text.Split(' ');
        string line = "";
        foreach (string word in words)
        {
            if (line.Length + word.Length > width)
            {
                lines.Add(line);
                line = "";
            }
            line += word + " ";
        }
        
        return string.Join("\n", lines);
    }
}