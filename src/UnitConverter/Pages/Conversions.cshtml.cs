using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UnitConverter.Models;

namespace UnitConverter.Pages;

public class ConversionsModel : PageModel
{
    private readonly ILogger<ConversionsModel> _logger;

    public ConversionsModel(ILogger<ConversionsModel> logger)
    {
        _logger = logger;
    }

    [BindProperty(SupportsGet = true)]
    public ConversionModel Conversion { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string ConversionType
    {
        get => Conversion.ConversionType;
        set => Conversion.ConversionType = value;
    }

    [BindProperty(SupportsGet = true)]
    public string Input
    {
        get => Conversion.Input;
        set => Conversion.Input = value;
    }

    public string Output
    {
        get => Conversion.Output;
        set => Conversion.Output = value;
    }

    public void OnGet()
    {
        // Sets default value when conversions page is opened without parameters.
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

        // Converts the submitted string into a numeric value
        try
        {
            conversionInput = Convert.ToDouble(Conversion.Input);
        }
        catch (FormatException)
        {
            ViewData["ErrorMessage"] = "Input must be a valid number.";

            _logger.LogWarning(
                "Invalid conversion input {Input} for {ConversionType}",
                Conversion.Input,
                Conversion.ConversionType);

            return;
        }

        _logger.LogInformation(
            "Conversion requested using input {Input} and type {ConversionType}",
            Conversion.Input,
            Conversion.ConversionType);

        double? conversionOutput;

        try
        {
            conversionOutput = Conversion.ConversionType switch
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
        }
        catch (Exception ex)
        {
            _logger.LogError(
            ex,
            "Conversion failed for {Input} using {ConversionType}",
            conversionInput,
            Conversion.ConversionType);

            ViewData["ErrorMessage"] = "An error occurred during the conversion.";
            return;
        }

        if (conversionOutput is null)
        {
            ViewData["ErrorMessage"] = "Unknown conversion type.";

            _logger.LogWarning(
                "Unknown conversion type {ConversionType} with input {Input}",
                Conversion.ConversionType,
                Conversion.Input);

            return;
        }

        Conversion.Output = conversionOutput.Value.ToString();

        _logger.LogInformation(
            "Converted {Input} using {ConversionType} with result {Result}",
            conversionInput,
            Conversion.ConversionType,
            conversionOutput.Value);
    }
}
