using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UnitConverter.Models;

namespace UnitConverter.Pages;

public class ConversionsModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public ConversionModel Conversion { get; set; } = new();

    public void OnGet()
    {
        if (string.IsNullOrEmpty(Conversion.ConversionType))
        {
            Conversion.ConversionType = "MilesToKilometers";
        }

        if (string.IsNullOrEmpty(Conversion.Input))
        {
            Conversion.Input = "3.1415";
        }

        ViewData["ConversionType"] = "Miles to Kilometers";
        ViewData["Title"] = "Conversions";

        double conversionInput;

        try
        {
            conversionInput = Convert.ToDouble(Conversion.Input);
        }
        catch (FormatException)
        {
            ViewData["ErrorMessage"] = "Input must be a valid number.";
            return;
        }

        double? conversionOutput = Conversion.ConversionType switch
        {
            ConversionTypes.MilesToKilometers =>
                new UnitOf.Length().FromMiles(conversionInput).ToKilometers(),
            ConversionTypes.KilometersToMiles =>
                new UnitOf.Length().FromKilometers(conversionInput).ToMiles(),
            ConversionTypes.FahrenheitToCelsius =>
                new UnitOf.Temperature().FromFahrenheit(conversionInput).ToCelsius(),
            ConversionTypes.CelsiusToFahrenheit =>
                new UnitOf.Temperature().FromCelsius(conversionInput).ToFahrenheit(),
            ConversionTypes.PoundsToKilograms =>
                new UnitOf.Mass().FromPounds(conversionInput).ToKilograms(),
            ConversionTypes.KilogramsToPounds =>
                new UnitOf.Mass().FromKilograms(conversionInput).ToPounds(),
            ConversionTypes.MphToMach =>
                new UnitOf.Speed().FromMilesPerHour(conversionInput).ToMach(),
            ConversionTypes.MachToMph =>
                new UnitOf.Speed().FromMach(conversionInput).ToMilesPerHour(),
            _ => null
        };

        if (conversionOutput is null)
        {
            ViewData["ErrorMessage"] = "Unknown conversion type.";
            return;
        }

        Conversion.Output = conversionOutput.Value.ToString();
    }
}
