![immagine](https://github.com/fraaaaa4/logix/assets/87281326/e8f651f7-9d9d-4013-bc89-9892083a16cb)

Logix is a simple and easy to use Prolog/LISP/etc. text editor with syntax highlighting and many other features.

# Current version
Current version of Logix is **Testfire**. This version aims at having all the basic features done and implementing only Prolog syntax highlighting.
![testfire1](https://github.com/fraaaaa4/logix/assets/87281326/00b75ac0-1c84-4f57-9057-354002bcf043)

It features the following (but not only) features:
- **Tabbed browsing**
- **Prolog syntax** with function highlighting, variable highlighting, and Intellisense-like autocomplete menu for system functions
- **Bookmarks**
- **AutoComplete**: automatically add header, footer of the source file, or automatically add comments
- **Document map**

  ## Roadmap
  For Logix Testfire, the following things will be added/fixed before the next development phase:
  - Printing
  - Find/Replace directly from a dialog
  - Fixes to the Prolog syntax
  - Selecting and highlighting words/functions/rows/columns
  - Recent files
  - Context menu
 
  ## Roadmap for future releases
  For the next phases of Logix, the following features are planned:
  - LISP/C#/VB/HTML/XML/SQL/PHP/JS/lua/Julia/Ruby/Haskell/Python/R/C++ syntax highlighting and Intellisense menus
  - Side-by-side "show differences"
  - Split view/MDI multi-windowing
  - (experimental) Prolog and LISP C# implementation
  - Dynamic Intellisense menu (autopopulates based on the functions you've written in your source file)
  - Command Palette, Tasks Pane, Object Browsing
  - .NET Compact Framework version (for WM2003 to WM6.5)
  - Custom toolbar editing
  - Hotkeys editing
  - Autoindent, tooltips and hints
  - Non-theme dependent light/dark mode (for all the Aero users out there)

 # Development notes and credits
 This project has been built using Visual Studio 2005, built against .NET Framework 2.0. Credits to Pavel Torgashov for 'Fast Colored Textbox' and "Autocomplete Menu'.
 - Fastcolored: https://www.codeproject.com/Articles/161871/Fast-Colored-TextBox-for-syntax-highlighting-2
 - AutocompleteMenu: https://www.codeproject.com/Articles/365974/Autocomplete-Menu

For the best experience, it's better to run it on Windows 11 running Rectify11 (for full theming support, which includes built-in accent colorization and built-in light/dark mode).

## Compatibility
Theoretically, it should be able to run on Windows XP SP2/3 or newer. Currently, it has been tested only on 10/11, and it works. Thanks to the theme-aware focused development, no matter what theme you use, the app will automatically adapt to the style of the Windows you're running (as seen here in this screenshot).
![immagine](https://github.com/fraaaaa4/logix/assets/87281326/f2bf4b20-34d5-428d-87d0-5ed88c4649ca)

It's mandatory to have .NET Framework 1.1 and 2.0 installed.
