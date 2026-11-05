# Graph Report - ZapretLauncher  (2026-11-05)

## Corpus Check
- 19 files · ~27,035 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 7 file(s) not represented in the graph (top: (none) 2, .ico 2, .manifest 1)

## Summary
- 712 nodes · 1505 edges · 50 communities (35 shown, 15 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 88 edges (avg confidence: 0.85)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `32f398e6`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- Installer
- Core.cs
- RoutedEventArgs
- Window
- MainWindow
- ProxyRunner
- Task
- steps
- TextBlock
- .MeasureAsync
- ProxyConfig
- WaveEffect
- Button
- App
- CheckBox
- Rectangle
- TrayMenu
- .Main
- Path
- Ellipse
- StackPanel
- Border
- UiState
- ProxyState
- SetupPage
- Slider
- TranslateTransform
- ScaleTransform
- Canvas
- RadioButton
- Tab
- Checks.csproj
- AccentHex
- DownloadBar
- PART_ContentHost
- AccentSwatches
- CursorFill
- PART_Track
- StrategyList
- TrayArrow
- TgProxy
- SetupMode

## God Nodes (most connected - your core abstractions)
1. `MainWindow` - 208 edges
2. `Window` - 159 edges
3. `Grid` - 35 edges
4. `ProxyRunner` - 29 edges
5. `TextBlock` - 24 edges
6. `Installer` - 21 edges
7. `ProxyConfig` - 19 edges
8. `Button` - 16 edges
9. `Strategy` - 15 edges
10. `WaveEffect` - 14 edges

## Surprising Connections (you probably didn't know these)
- `MainWindow` --references--> `Settings`  [EXTRACTED]
  FileEditor.cs → Core.cs
- `MainWindow` --references--> `Strategy`  [EXTRACTED]
  FileEditor.cs → Core.cs
- `MainWindow` --references--> `Runner`  [EXTRACTED]
  FileEditor.cs → Core.cs
- `MainWindow` --inherits--> `Window`  [EXTRACTED]
  FileEditor.cs → MainWindow.xaml
- `MainWindow` --references--> `ProxyConfig`  [EXTRACTED]
  FileEditor.cs → Proxy.cs

## Import Cycles
- None detected.

## Communities (50 total, 15 thin omitted)

### Community 0 - "Installer"
Cohesion: 0.16
Nodes (8): Settings, Installer, DefaultDir, DesktopLink, IsInstalledCopy, SelfPath, StartMenuLink, Version

### Community 1 - "Core.cs"
Cohesion: 0.11
Nodes (5): Paths, Engine, SettingsFile, VersionFile, ZapretLauncher

### Community 3 - "RoutedEventArgs"
Cohesion: 0.06
Nodes (5): Autostart, Runner, IsRunning, LastOutput, Sys

### Community 4 - "Window"
Cohesion: 0.08
Nodes (42): Description, Name, ProbeText, SelectedItem.Name, Works, Frost, FrostSky, FrostSkyHost (+34 more)

### Community 6 - "ProxyRunner"
Cohesion: 0.12
Nodes (7): IconFactory, EnumProc, PROCESS_INFORMATION, ProxyRunner, ForeignRunning, IsRunning, STARTUPINFO

### Community 7 - "Task"
Cohesion: 0.30
Nodes (3): ReleaseInfo, Updater, Web

### Community 8 - "steps"
Cohesion: 0.08
Nodes (23): runs-on, steps, jobs, build, name, on, pull_request, push (+15 more)

### Community 10 - "TextBlock"
Cohesion: 0.08
Nodes (25): BgBlurValue, BgName, BlurValue, BrightValue, FileHelp, FileStatus, FileTitle, HintText (+17 more)

### Community 12 - ".MeasureAsync"
Cohesion: 0.20
Nodes (5): Probe, ProbeResult, Host, Milliseconds, Success

### Community 14 - "ProxyConfig"
Cohesion: 0.16
Nodes (9): ProxyConfig, CfProxy, DcIp, H2, Host, Link, LinkHost, Port (+1 more)

### Community 15 - "WaveEffect"
Cohesion: 0.12
Nodes (9): Glass, Hint, WaveEffect, Amount, Aspect, Breadth, Centre, Input (+1 more)

### Community 16 - "Button"
Cohesion: 0.12
Nodes (17): AutoButton, FileCancel, FileReload, FileSave, LogButton, MaxButton, PowerButton, ProxyInfoButton (+9 more)

### Community 17 - "App"
Cohesion: 0.20
Nodes (6): Application, Bd, PART_Track, Border, Track, App

### Community 18 - "CheckBox"
Cohesion: 0.18
Nodes (11): AutostartToggle, CloseToggle, GameToggle, PxAuto, PxCf, PxH2, SetupAutostart, SetupDesktop (+3 more)

### Community 19 - "Rectangle"
Cohesion: 0.18
Nodes (11): FrostWallpaperBox, FrostWallpaperDim, HintWallpaperBox, HintWallpaperDim, HueMark, ModalWallpaperBox, ModalWallpaperDim, SvHue (+3 more)

### Community 21 - ".Main"
Cohesion: 0.05
Nodes (22): Engine, InstalledVersion, IsInstalled, Strategy, BatPath, Description, Name, ProbeText (+14 more)

### Community 23 - "Path"
Cohesion: 0.25
Nodes (8): Foreground, Halo, PowerIcon, PxIcon, TrackOff, TrackOn, TrayChevron, Path

### Community 24 - "Ellipse"
Cohesion: 0.25
Nodes (8): Disc, Dot, PxDisc, PxRing, Ring, SvMark, Thumb, Ellipse

### Community 25 - "StackPanel"
Cohesion: 0.25
Nodes (8): PageDone, PageOptions, PageRemove, PageWelcome, PageWorking, TabStrip, Tray, StackPanel

### Community 26 - "Border"
Cohesion: 0.25
Nodes (8): Bar, Bd, FilePanel, HintBox, ProxyInfoPanel, StrategyPanel, TabBar, Border

### Community 27 - "UiState"
Cohesion: 0.29
Nodes (7): UiState, Idle, Installing, Missing, Running, Starting, Testing

### Community 28 - "ProxyState"
Cohesion: 0.33
Nodes (6): ProxyState, Idle, Installing, Missing, Running, Starting

### Community 29 - "SetupPage"
Cohesion: 0.33
Nodes (6): SetupPage, Done, Options, Remove, Welcome, Working

### Community 30 - "Slider"
Cohesion: 0.40
Nodes (5): BgBlurSlider, BlurSlider, BrightSlider, WaveSlider, Slider

### Community 32 - "TranslateTransform"
Cohesion: 0.50
Nodes (4): CursorAt, TabBarAt, VersionShift, TranslateTransform

### Community 33 - "ScaleTransform"
Cohesion: 0.50
Nodes (4): CursorGlowScale, PowerGlyph, PxGlyph, ScaleTransform

### Community 34 - "Canvas"
Cohesion: 0.50
Nodes (4): CursorLayer, Sky, TrayClip, Canvas

### Community 35 - "RadioButton"
Cohesion: 0.50
Nodes (4): TabProxy, TabSettings, TabZapret, RadioButton

### Community 36 - "Tab"
Cohesion: 0.50
Nodes (4): Tab, Proxy, Settings, Zapret

### Community 37 - "Checks.csproj"
Cohesion: 0.33
Nodes (4): net48, Microsoft.NET.Sdk, net48, Microsoft.NET.Sdk

### Community 38 - "AccentHex"
Cohesion: 0.50
Nodes (4): AccentHex, FileText, SetupPath, TextBox

### Community 39 - "DownloadBar"
Cohesion: 0.67
Nodes (3): DownloadBar, SetupBar, ProgressBar

### Community 40 - "PART_ContentHost"
Cohesion: 0.67
Nodes (3): PART_ContentHost, Scroll, ScrollViewer

### Community 46 - "TgProxy"
Cohesion: 0.20
Nodes (9): TgProxy, ConfigFile, DataDir, Dir, Exe, InstalledVersion, IsInstalled, LogFile (+1 more)

### Community 49 - "SetupMode"
Cohesion: 0.33
Nodes (4): SetupMode, Install, None, Uninstall

## Knowledge Gaps
- **83 isolated node(s):** `Track`, `Border`, `Engine`, `SettingsFile`, `VersionFile` (+78 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 166 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **15 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `Installer`, `Core.cs`, `.ApplyWallpaper`, `RoutedEventArgs`, `Window`, `ProxyRunner`, `Task`, `MouseButtonEventArgs`, `Color`, `.ShowTab`, `ProxyConfig`, `WaveEffect`, `App`, `TrayMenu`, `.Main`, `FrameworkElement`, `UiState`, `ProxyState`, `SetupPage`, `.GoTo`, `Tab`, `.ApplyBackdrop`, `.StarBrush`, `SetupMode`?**
  _High betweenness centrality (0.726) - this node is a cross-community bridge._
- **Are the 3 inferred relationships involving `MainWindow` (e.g. with `.OnStartup()` and `.RunSetup()`) actually correct?**
  _`MainWindow` has 3 INFERRED edges - model-reasoned connections that need verification._
- **What connects `Track`, `Border`, `Engine` to the rest of the system?**
  _83 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Core.cs` be split into smaller, more focused modules?**
  _Cohesion score 0.10784313725490197 - nodes in this community are weakly interconnected._
- **Why does `Window` connect `Window` to `MainWindow`, `TextBlock`, `Button`, `CheckBox`, `Rectangle`, `TrayMenu`, `.Main`, `Path`, `Ellipse`, `StackPanel`, `Border`, `Slider`, `TranslateTransform`, `ScaleTransform`, `Canvas`, `RadioButton`, `AccentHex`, `DownloadBar`, `PART_ContentHost`, `AccentSwatches`, `CursorFill`, `PART_Track`, `StrategyList`, `TrayArrow`?**
  _High betweenness centrality (0.420) - this node is a cross-community bridge._
- **Should `RoutedEventArgs` be split into smaller, more focused modules?**
  _Cohesion score 0.062111801242236024 - nodes in this community are weakly interconnected._
- **Why does `ProxyRunner` connect `ProxyRunner` to `Core.cs`, `RoutedEventArgs`, `MainWindow`, `Task`?**
  _High betweenness centrality (0.074) - this node is a cross-community bridge._