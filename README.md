# ThinkTwice keyboard fix
ThinkTwice is a Windows app which replaces {PrtSc} key press with right mouse click.

More precisely - with the {Shift}{F10} key combination, which corresponds to the {Menu} button,
which was replaced with Print Screen button on newer ThinkPad models.

## Use
Launch the ThinkTwiceKeyboardFix.exe
The key stays remapped as long as the windows remains open.
You might wish to launch it on startup.

You can put a shortcut to the startup folder, so it starts during user login.
This is how you can open the startup shortcuts folder on Windows 10:
Press Windows+R, then type shell:startup

## Credits
This work is based on [Low-Level Keyboard Hook in C#](https://blogs.msdn.microsoft.com/toub/2006/05/03/low-level-keyboard-hook-in-c/) by Stephen Toub.
