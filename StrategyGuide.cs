using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ZapretLauncher
{
    public sealed class ProbeResult
    {
        public string Host { get; set; }
        public long? Milliseconds { get; set; }
        public bool Success { get { return Milliseconds.HasValue; } }
        public static string Summarize(IEnumerable<ProbeResult> results)
        {
            var all = results.ToArray();
            var replies = all.Where(r => r.Success).ToArray();
            if (replies.Length == 0) return "Нет ответа";
            long average = (long)Math.Round(replies.Average(r => r.Milliseconds.Value), MidpointRounding.AwayFromZero);
            return average + " мс" + (replies.Length < all.Length ? " · частично" : "");
        }
        public override string ToString()
        {
            string label = Host == "www.youtube.com" ? "YouTube" : Host == "discord.com" ? "Discord"
                : Host == "i.ytimg.com" ? "Картинки" : Host;
            return label + ": " + (Success ? Milliseconds + " мс" : "нет ответа");
        }
    }

    public static class StrategyGuide
    {
        public static string Describe(string path)
        {
            string name = Path.GetFileNameWithoutExtension(path).ToUpperInvariant();
            if (name == "GENERAL") return "Основной вариант для YouTube и Discord. Начните с него.";
            if (name.Contains("EXP")) return "Пробный вариант, если остальные не помогли.";
            if (name.Contains("SIMPLE FAKE")) return "Упрощённый вариант — попробуйте, если другие работают с перебоями.";
            if (name.Contains("FAKE TLS AUTO")) return "Другой способ подключения, если сайты всё ещё не открываются.";
            if (name.Contains("ALT")) return "Запасной вариант, если основной не подошёл вашему подключению.";
            return "Дополнительный вариант. Проверьте его через автоподбор.";
        }
    }
}
