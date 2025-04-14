using BRCSystem.ClassLibrary.SPCIA.Entities;

namespace BRCSystem.ClassLibrary.GetEntities.SPCIA
{
    public static class UnitMeasurementCreate
    {
        public static UnitMeasurement Get()
        {
            return new UnitMeasurement
            {
                Measurement = "Length",
                Symbol = "m"
            };
        }
        public static List<UnitMeasurement> GetList()
        {
            return new List<UnitMeasurement>
            {
                new() { Measurement = "Length", Symbol = "m" },
                new() { Measurement = "Weight", Symbol = "kg" },
                new() { Measurement = "Time", Symbol = "s" },
                new() { Measurement = "Volume", Symbol = "L" },
                new() { Measurement = "Temperature", Symbol = "°C" },
            };
        }
    }
}
