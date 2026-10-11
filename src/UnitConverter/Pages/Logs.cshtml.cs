using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UnitConverter.Models;
using UnitConverter.Services;

namespace UnitConverter.Pages;

public class Logs : PageModel
{
    private readonly ILogReader _logReader;

    public Logs(ILogReader logReader)
    {
        _logReader = logReader;
    }

    public List<LogEntry> Entries { get; set; } = [];

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? Level { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? ConversionType { get; set; }

    public void OnGet()
    {
        List<LogEntry> filteredEntries = new();

        foreach (LogEntry entry in _logReader.Read())
        {
            if (!string.IsNullOrEmpty(Search) &&
                !(entry.Message?.Contains(Search, StringComparison.OrdinalIgnoreCase) ?? false))
            {
                continue;
            }

            if (!string.IsNullOrEmpty(Level) &&
                !string.Equals(entry.Level, Level, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (!string.IsNullOrEmpty(ConversionType) &&
                !string.Equals(entry.ConversionType, ConversionType, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            filteredEntries.Add(entry);
        }

        Entries = filteredEntries;
    }
}
