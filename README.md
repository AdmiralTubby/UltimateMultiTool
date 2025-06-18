# UltimateMultiTool
A tool for finding information, comparing files, transferring files, visualizing information, etc. More stuff to come

You need AT LEAST:
Visual Studio 2022 v17.8
.NET 8.0
Although the above only matters if you want to open up the project and work on it.

Need those versions mainly because I wanted to be able to publish a single .exe file.
To install .NET 8 you need VS 2022 17.8 or higher (https://learn.microsoft.com/en-us/dotnet/core/install/windows), and I want .NET 8 because it bundles runtimeconfig.json instead of having it loose.
Reason I want to do the single file publish is just because I think it's easier to send someone the .exe, without missing .dlls and shit like that.
The program DOES generate a .json user config file (used to store the user-created exceptions for file diff), but it generates it AFTER it has started to be used, so the point still stands.
Another good thing about VS 2022 17.8 is that you avoid having a blurry form because it provides support for DPI-unaware tabs within a DPI-aware app (https://learn.microsoft.com/en-us/visualstudio/designers/disable-dpi-awareness?view=vs-2022)
