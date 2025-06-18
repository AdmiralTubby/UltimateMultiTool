AbadUltimateTool
|---Program.cs
|---Form1.cs                      <- main window
|   |--Form1.Designer.cs
|---
|
|---Palette                       
|
|---IpScan                        
    |---IpUsage.cs                <- immutable record
    |---IpParser.cs               <- crawls + extracts
    |---IpTabFactory.cs           <- builds TabPages (all + per-file)
    |---CsvExporter.cs            <- writes All-IPs grid to .csv + available feature
	

 Todo:
 Actually finish adding the rest of these