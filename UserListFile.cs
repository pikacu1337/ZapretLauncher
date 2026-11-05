using System;
using System.IO;
using System.Linq;
using System.Text;

namespace ZapretLauncher
{
    public static class UserListFile
    {
        public static string Save(string path, string text)
        {
            // An empty host/ip list can match all traffic in winws. Keep a harmless entry.
            bool hasEntry = text.Split('\n').Any(l => l.Trim().Length > 0 && !l.TrimStart().StartsWith("#"));
            if (!hasEntry)
                text = text.TrimEnd() + (text.Trim().Length > 0 ? "\r\n" : "")
                    + (Path.GetFileName(path).StartsWith("ipset-", StringComparison.OrdinalIgnoreCase)
                        ? "203.0.113.113/32\r\n" : "domain.example.abc\r\n");
            string temp = path + "." + Guid.NewGuid().ToString("N") + ".tmp";
            try
            {
                File.WriteAllText(temp, text, new UTF8Encoding(false));
                if (File.Exists(path)) File.Replace(temp, path, null);
                else File.Move(temp, path);
            }
            finally { if (File.Exists(temp)) File.Delete(temp); }
            return text;
        }
    }
}
