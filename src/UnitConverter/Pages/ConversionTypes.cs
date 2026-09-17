namespace UnitConverter.Pages;

public static class ConversionTypes
{
    public const string MilesToKilometers = "MilesToKilometers";
    public const string KilometersToMiles = "KilometersToMiles";
    public const string FahrenheitToCelsius = "FahrenheitToCelsius";
    public const string CelsiusToFahrenheit = "CelsiusToFahrenheit";
    public const string PoundsToKilograms = "PoundsToKilograms";
    public const string KilogramsToPounds = "KilogramsToPounds";
    public const string MphToMach = "MphToMach";
    public const string MachToMph = "MachToMph";

    public static readonly IReadOnlyDictionary<string, string> All =
        new Dictionary<string, string>
        {
            [MilesToKilometers] = "Miles to Kilometers",
            [KilometersToMiles] = "Kilometers to Miles",
            [FahrenheitToCelsius] = "Fahrenheit to Celsius",
            [CelsiusToFahrenheit] = "Celsius to Fahrenheit",
            [PoundsToKilograms] = "Pounds to Kilograms",
            [KilogramsToPounds] = "Kilograms to Pounds",
            [MphToMach] = "MPH to Mach",
            [MachToMph] = "Mach to MPH",
        };
}
