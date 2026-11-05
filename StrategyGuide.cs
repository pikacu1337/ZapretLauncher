using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ZapretLauncher
{
    public sealed class ProbeResult
    {
        public string Host { get; set; }
        public long? Milliseconds { get; set; }
        public bool Success { get { return Milliseconds.HasValue; } }
        public override string ToString() { return Host + ": " + (Success ? Milliseconds + " мс" : "нет ответа"); }
    }

    public static class StrategyGuide
    {
        public static string Describe(string path)
        {
            string name = Path.GetFileNameWithoutExtension(path).ToUpperInvariant();
            string purpose = name.Contains("SIMPLE FAKE")
                ? "Поддельные пакеты без сложного разбиения. Кому: если варианты с разбиением вызывают обрывы."
                : name.Contains("FAKE TLS AUTO")
                ? "Автоматическое формирование поддельного TLS-приветствия. Кому: если обычные варианты не открывают HTTPS-сайты."
                : name.Contains("EXP")
                ? "Экспериментальный вариант обхода. Кому: для проверки, когда стандартная и альтернативные стратегии не помогли."
                : name.Contains("ALT")
                ? "Альтернативные параметры обхода. Кому: если базовая general не работает или соединение нестабильно; номера ALT не означают рейтинг."
                : name == "GENERAL"
                ? "Базовая стратегия для сайтов из списков, YouTube и Discord. Кому: начните с неё при первом запуске."
                : "Дополнительная стратегия из установленной сборки. Кому: для проверки через автоподбор, если основные варианты не помогли.";
            try
            {
                string args = Engine.BuildArgs(new Strategy { BatPath = path }, false);
                var details = new[] {
                    Values(args, "dpi-desync", "Методы"),
                    Values(args, "dpi-desync-split-pos", "Точки разбиения"),
                    Values(args, "dpi-desync-fooling", "Маскировка"),
                    Values(args, "dpi-desync-repeats", "Повторы"),
                    Values(args, "dpi-desync-split-seqovl", "Перекрытие"),
                    Values(args, "dpi-desync-fake-tls-mod", "TLS")
                }.Where(x => x.Length > 0);
                return purpose + "\n" + string.Join(" · ", details)
                    + "\nПодходит ли вашему провайдеру, определяет проверка доступности.";
            }
            catch { return purpose + "\nПараметры файла прочитать не удалось. Обновите компоненты."; }
        }

        static string Values(string args, string option, string title)
        {
            var values = Regex.Matches(args, @"--" + Regex.Escape(option) + @"=([^\s]+)")
                .Cast<Match>().Select(m => m.Groups[1].Value).Distinct().ToArray();
            return values.Length == 0 ? "" : title + ": " + string.Join(" / ", values);
        }
    }
}
