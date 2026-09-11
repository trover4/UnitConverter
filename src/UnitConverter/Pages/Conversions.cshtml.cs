using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UnitConverter.Pages;

public class ConversionsModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string ConversionType { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)]
    public string Input { get; set; } = string.Empty;

    public string Output { get; set; } = string.Empty;

    public string ConversionTypeName => ConversionType switch
    {
        "MilesToKilometers" => "Miles To Kilometers",
        "KilometersToMiles" => "Kilometers To Miles",
        "FahrenheitToCelsius" => "Fahrenheit To Celsius",
        "CelsiusToFahrenheit" => "Celsius To Fahrenheit",
        "PoundsToKilograms" => "Pounds To Kilograms",
        "KilogramsToPounds" => "Kilograms To Pounds",
        "MphToMach" => "MPH To Mach",
        "MachToMph" => "Mach To MPH",
        _ => ConversionType
    };

    public void OnGet()
    {
        if (string.IsNullOrEmpty(ConversionType))
        {
            ConversionType = "MilesToKilometers";
        }

        if (string.IsNullOrEmpty(Input))
        {
            Input = "3.1415";
        }

        ViewData["ConversionType"] = "Miles to Kilometers";
        ViewData["Title"] = "Conversions";

        double conversionInput;

        try
        {
            conversionInput = Convert.ToDouble(Input);
        }
        catch (FormatException)
        {
            ViewData["ErrorMessage"] = "Input must be a valid number.";
            return;
        }

        double? conversionOutput = ConversionType switch
        {
            "MilesToKilometers" => new UnitOf.Length().FromMiles(conversionInput).ToKilometers(),
            "KilometersToMiles" => new UnitOf.Length().FromKilometers(conversionInput).ToMiles(),
            "FahrenheitToCelsius" => new UnitOf.Temperature().FromFahrenheit(conversionInput).ToCelsius(),
            "CelsiusToFahrenheit" => new UnitOf.Temperature().FromCelsius(conversionInput).ToFahrenheit(),
            "PoundsToKilograms" => new UnitOf.Mass().FromPounds(conversionInput).ToKilograms(),
            "KilogramsToPounds" => new UnitOf.Mass().FromKilograms(conversionInput).ToPounds(),
            "MphToMach" => new UnitOf.Speed().FromMilesPerHour(conversionInput).ToMach(),
            "MachToMph" => new UnitOf.Speed().FromMach(conversionInput).ToMilesPerHour(),
            _ => null
        };

        if (conversionOutput is null)
        {
            ViewData["ErrorMessage"] = "Unknown conversion type.";
            return;
        }

        Output = conversionOutput.Value.ToString();
    }
}
