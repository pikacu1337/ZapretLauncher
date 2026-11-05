using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ZapretLauncher;

class Program
{
    static int checks;
    static void Check(bool ok, string name) { if (!ok) throw new Exception(name); Console.WriteLine("PASS " + name); checks++; }
    static object Call(object o, string name, params object[] args) { return o.GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic).Invoke(o, args); }
    static T Field<T>(object o, string name) { return (T)o.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(o); }
    static void Set(object o, string name, object value) { o.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic).SetValue(o, value); }

    [STAThread]
    static int Main(string[] args)
    {
        try
        {
            string dir = Path.GetFullPath(args[0]);
            Directory.CreateDirectory(dir);
            string path = Path.Combine(dir, "list-general-user.txt");
            UserListFile.Save(path, "пример.рф\r\nexample.com\r\n");
            Check(File.ReadAllText(path).Contains("пример.рф"), "UTF-8 domain round trip");
            UserListFile.Save(path, "# comment only\r\n");
            Check(File.ReadAllText(path).Contains("domain.example.abc"), "comment-only domain list stays safe");
            string ip = Path.Combine(dir, "ipset-exclude-user.txt");
            UserListFile.Save(ip, " \r\n");
            Check(File.ReadAllText(ip).Contains("203.0.113.113/32"), "empty IP list stays safe");
            Check(!Directory.GetFiles(dir, "*.tmp").Any(), "atomic save removes temporary files");
            using (var client = new HttpClient(new FakeHandler()))
            {
                var results = Probe.MeasureAsync(client, new[] { "https://ok.test", "https://fail.test" }, CancellationToken.None).GetAwaiter().GetResult();
                Check(results[0].Success && results[0].Milliseconds >= 15, "HTTPS response timing measured");
                Check(!results[1].Success && results[1].ToString().Contains("нет ответа"), "network failure distinguished from zero ping");
                using (var cts = new CancellationTokenSource())
                {
                    cts.Cancel();
                    bool cancelled = false;
                    try { Probe.MeasureAsync(client, new[] { "https://ok.test" }, cts.Token).GetAwaiter().GetResult(); }
                    catch (OperationCanceledException) { cancelled = true; }
                    Check(cancelled, "cancellation propagated");
                }
            }
            var strategy = new Strategy(); int notifications = 0;
            strategy.PropertyChanged += (s, e) => { if (e.PropertyName == "ProbeText") notifications++; };
            strategy.Works = true; strategy.ProbeText = "123 мс"; strategy.ResetProbe();
            Check(!strategy.Works && strategy.ProbeText == "Ещё не проверена" && notifications == 2, "probe binding updates and reset");
            string[] bats = Directory.GetFiles(args[1], "general*.bat");
            foreach (string bat in bats)
            {
                string description = StrategyGuide.Describe(bat);
                Check(description.Contains("Кому:") && description.Contains("Методы:") && !description.Contains("не удалось"), Path.GetFileName(bat) + " description");
            }

            // Keep all UI checks isolated from the installed application's data and services.
            typeof(Paths).GetField("Root").SetValue(null, Path.Combine(dir, "isolated"));
            var app = new Application();
            var window = new MainWindow(false);
            try
            {
                window.WindowStartupLocation = WindowStartupLocation.Manual;
                window.Left = -10000; window.Top = -10000; window.ShowInTaskbar = false;
                window.Show();
                window.Measure(new Size(1152, 826)); window.Arrange(new Rect(0, 0, 1152, 826)); window.UpdateLayout();
                Call(window, "OpenFilePanel", path, "Мои сайты", true);
                var text = Field<TextBox>(window, "FileText");
                text.Text = "example.org\r\n";
                Check(!(bool)Call(window, "CanCloseFile"), "dirty editor cannot be dismissed accidentally");
                Call(window, "FileSave_Click", null, new RoutedEventArgs());
                Check(File.ReadAllText(path) == text.Text && (bool)Call(window, "CanCloseFile"), "Save writes the intended file and clears dirty state");
                Check(Field<TextBlock>(window, "FileStatus").Text.Contains("следующем включении"), "saving while off keeps zapret off");
                text.Text = "unsaved.test";
                Call(window, "FileCancel_Click", null, new RoutedEventArgs());
                Check(!File.ReadAllText(path).Contains("unsaved"), "Cancel does not write changes");
                Call(window, "OpenFilePanel", path, "Мои сайты", true);
                File.WriteAllText(path, "external.test"); text.Text = "overwrite.test";
                Call(window, "FileSave_Click", null, new RoutedEventArgs());
                Check(File.ReadAllText(path) == "external.test" && Field<TextBlock>(window, "FileStatus").Text.Contains("другой программой"), "external file changes are protected");
                Call(window, "FileCancel_Click", null, new RoutedEventArgs());
                Call(window, "OpenFilePanel", path, "Журнал", false);
                Check(text.IsReadOnly && Field<Button>(window, "FileSave").Visibility == Visibility.Collapsed, "logs use an internal read-only viewer");
                File.WriteAllText(path, "new log line"); Call(window, "FileReload_Click", null, new RoutedEventArgs());
                Check(text.Text == "new log line", "log refresh reads new content");
                Call(window, "FileCancel_Click", null, new RoutedEventArgs());
                Call(window, "OpenFilePanel", path, "Мои сайты", true);
                window.UpdateLayout(); double oldHeight = text.ActualHeight;
                Call(window, "FileResize_DragDelta", null, new DragDeltaEventArgs(0, 80));
                window.UpdateLayout();
                Check(text.Height > oldHeight, "vertical resize handle expands editor");
                text.Text = "example.com\r\nпример.рф\r\n";
                Set(window, "_fileOriginal", text.Text);
                var list = Field<ListBox>(window, "StrategyList");
                list.ItemsSource = bats.Select(b => new Strategy { Name = Path.GetFileNameWithoutExtension(b), Description = StrategyGuide.Describe(b), ProbeText = "Отклик HTTPS: www.youtube.com: 84 мс · discord.com: 106 мс · i.ytimg.com: нет ответа" }).ToList();
                Render(window, Path.Combine(dir, "editor.png"));
                Call(window, "OpenModal", Field<FrameworkElement>(window, "StrategyPanel"));
                Render(window, Path.Combine(dir, "strategies.png"));
            }
            finally
            {
                Set(window, "_cleaned", true);
                Field<System.Windows.Forms.NotifyIcon>(window, "_tray").Dispose();
                Set(window, "_exiting", true);
                window.Close();
            }
            Console.WriteLine("Completed " + checks + " checks. No live bypass/service operations performed.");
            return 0;
        }
        catch (Exception ex) { Console.Error.WriteLine(ex); return 1; }
    }
    static void Render(Window w, string path)
    {
        var layer = Field<FrameworkElement>(w, "ModalLayer");
        layer.BeginAnimation(UIElement.OpacityProperty, null); layer.Opacity = 1;
        var shell = Field<FrameworkElement>(w, "Shell");
        shell.Measure(new Size(1152, 826)); shell.Arrange(new Rect(0, 0, 1152, 826));
        w.UpdateLayout();
        var bitmap = new RenderTargetBitmap(1152, 826, 96, 96, PixelFormats.Pbgra32);
        bitmap.Render(shell);
        var encoder = new PngBitmapEncoder(); encoder.Frames.Add(BitmapFrame.Create(bitmap));
        using (var stream = File.Create(path)) encoder.Save(stream);
    }
    sealed class FakeHandler : HttpMessageHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
        {
            await Task.Delay(25, ct);
            if (request.RequestUri.Host == "fail.test") throw new HttpRequestException("test failure");
            return new HttpResponseMessage(HttpStatusCode.Forbidden);
        }
    }
}
