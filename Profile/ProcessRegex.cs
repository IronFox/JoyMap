using JoyMap.Windows;
using System.Text.RegularExpressions;

namespace JoyMap.Profile
{
    public class ProcessRegex
    {
        private Regex? PRegex { get; }
        private Regex? WRegex { get; }

        public ProcessRegex(string? processNameRegex, string? windowNameRegex)
        {
            PRegex = string.IsNullOrEmpty(processNameRegex) ? null : new Regex(processNameRegex, RegexOptions.Compiled);
            WRegex = string.IsNullOrEmpty(windowNameRegex) ? null : new Regex(windowNameRegex, RegexOptions.Compiled);
        }

        internal bool IsMatch(WindowReference wr)
        {
            var m0 = PRegex?.IsMatch(wr.ProcessName ?? "") ?? true;
            var m1 = WRegex?.IsMatch(wr.WindowTitle) ?? true;
            return (PRegex is not null || WRegex is not null) && m0 && m1;
        }
    }
}
