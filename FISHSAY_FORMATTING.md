## fishsay - Formatting
Color codes are surrounded with `%` characters. The color code is the name of the color or style you want to apply. For example, `%red%` will make the text red. You can chain multiple color codes together to apply multiple colors and styles to your text. For example, `%red%%bold%Bold red text!%reset%` will make the text bold and red. Using a hash tag before the color, it'll apply to the background instead: `%#red%Red background!%reset%`.

If you trail a color or style, it will be applied to the fish and then cleaned up after to prevent spilling over the rest of your terminal. For example, `%red%Some red text!%reset%%blue%` will make the text red, and the fish will be blue.

**Supported colors:**
- `black`
- `red`
- `green`
- `yellow`
- `blue`
- `magenta`
- `cyan`
- `white`
- `lightblack`
- `lightred`
- `lightgreen`
- `lightyellow`
- `lightblue`
- `lightmagenta`
- `lightcyan`
- `lightwhite`

**Supported styles:**
- `bold`
- `dim`
- `italic`
- `underline`
- `blink`
- `reverse`
- `hidden`
- `strikethrough`

**Extras:**
- `reset` - Resets the color formatting
- `%%` - Escapes the `%` character