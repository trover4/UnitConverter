using UnitConverter.Models;

namespace UnitConverter.Services;

public class UnitOfConversionService : IConversionService
{
    public decimal Convert(decimal value, string conversionType)
    {
        double input = (double)value;

        double output = conversionType switch
        {
            ConversionTypes.MilesToKilometers =>
                new UnitOf.Length().FromMiles(input).ToKilometers(),
            ConversionTypes.KilometersToMiles =>
                new UnitOf.Length().FromKilometers(input).ToMiles(),
            ConversionTypes.FahrenheitToCelsius =>
                new UnitOf.Temperature().FromFahrenheit(input).ToCelsius(),
            ConversionTypes.CelsiusToFahrenheit =>
                new UnitOf.Temperature().FromCelsius(input).ToFahrenheit(),
            ConversionTypes.PoundsToKilograms =>
                new UnitOf.Mass().FromPounds(input).ToKilograms(),
            ConversionTypes.KilogramsToPounds =>
                new UnitOf.Mass().FromKilograms(input).ToPounds(),
            ConversionTypes.MphToMach =>
                new UnitOf.Speed().FromMilesPerHour(input).ToMach(),
            ConversionTypes.MachToMph =>
                new UnitOf.Speed().FromMach(input).ToMilesPerHour(),

            _ => throw new ArgumentException("Unknown conversion type.", nameof(conversionType))
        };

        return(decimal)output;
    }
}
