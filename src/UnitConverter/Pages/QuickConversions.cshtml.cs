using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UnitConverter.Models;
using UnitConverter.Services;

namespace UnitConverter.Pages;

public class QuickConversions : PageModel
{
    private readonly IConversionService _conversionService;

    public QuickConversions(IConversionService conversionService)
    {
        _conversionService = conversionService;
    }

    public decimal? Output { get; set; }
    public string? ErrorMessage { get; set; }


    public IEnumerable<SelectListItem> PoundOptions =>
    [
        new("1 pound", "1"),
        new("5 pounds", "5"),
        new("10 pounds", "10"),
        new("25 pounds", "25"),
        new("50 pounds", "50")
    ];

    public IActionResult OnGetMilesToKilometers(string input)
        => PerformConversion(input, ConversionTypes.MilesToKilometers);

    public IActionResult OnGetKilometersToMiles(string input)
        => PerformConversion(input, ConversionTypes.KilometersToMiles);

    public IActionResult OnGetFahrenheitToCelsius(string input)
        => PerformConversion(input, ConversionTypes.FahrenheitToCelsius);

    public IActionResult OnGetCelsiusToFahrenheit(string input)
        => PerformConversion(input, ConversionTypes.CelsiusToFahrenheit);

    public IActionResult OnGetPoundsToKilograms(string input)
        => PerformConversion(input, ConversionTypes.PoundsToKilograms);

    public IActionResult OnGetKilogramsToPounds(string input)
        => PerformConversion(input, ConversionTypes.KilogramsToPounds);

    public IActionResult OnGetMphToMach(string input)
        => PerformConversion(input, ConversionTypes.MphToMach);

    public IActionResult OnGetMachToMph(string input)
        => PerformConversion(input, ConversionTypes.MachToMph);

    private IActionResult PerformConversion(string input, string conversionType)
    {
        if (!decimal.TryParse(input, out decimal value))
        {
            ErrorMessage = "Please enter a valid number.";
            return Page();
        }

        Output = _conversionService.Convert(value, conversionType);

        return Page();
    }
}


