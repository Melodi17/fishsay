# fishsay
A standalone ascii fish generator for your terminal. Based on the original [cowsay](https://en.wikipedia.org/wiki/Cowsay) program for Windows.

- Cute fishes
- Easy to use
- Supports color formatting
- Supports piping and live updates

## Quick start
You can install fishsay using [scoop](https://scoop.sh/). Run the following command in your terminal to install fishsay.
```bash
$ scoop install https://raw.githubusercontent.com/Melodi17/fishsay/refs/heads/master/deploy/fishsay.json
```

Making a fish is as simple as piping some text into the fishsay command!
```bash
$ echo Hello world! | fishsay

   -----------
   Hello world!
   -----------
    \
     \         /"*._         _
      \    .-*'`    `*-.._.-'/
         < * ))     ,       (
           `*-._`._(__.--*"`.\
```

## Features
### Piping live commands
You can pipe live commands into fishsay to get live updates. You'll notice the fish will keep updating as the command runs.
```bash
$ ping google.com | fishsay
```
You can optionally disable this using the `--no-live` flag.

### Color formatting
You can use color formatting in your text to make the fish say things in color.
```bash
$ echo %red%Some red text!%reset% | fishsay
```
By trailing (not resetting the color) a color or style, it will be applied to the fish and then cleaned up after to prevent spilling over the rest of your terminal.
For all supported colors and styles, see the [color formatting](https://github.com/Melodi17/fishsay/blob/master/FISHSAY_FORMATTING.md) page.
Color formatting can be disabled using the `--no-color` flag.


## Contributing
This project is open to contributions. If you have any ideas or improvements, feel free to open an issue or a pull request.