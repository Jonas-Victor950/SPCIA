using BRCSystem.ClassLibrary.EnrollmentSystem.Entities;

namespace BRCSystem.ClassLibrary.GetEntities.EnrollmentSystem
{
    public static class MachineCreate
    {
        public static Machine GetMachine()
        {
            Random rand = new();
            var stations = new List<Station>();
            var stationsIds = new List<int>();

            for (int k = 1; k <= 3; ++k)
            {
                var station = StationCreate.GetStation();
                station.Id = k;
                stations.Add(station);
                stationsIds.Add(station.Id.Value);
            }

            return new Machine
            {
                Name = $"Machine {rand.Next(99)}",
                Description = $"Machine Desc {rand.Next(99)}",
                Active = true,
                StationsIds = stationsIds,
                Stations = stations
            };
        }

        public static List<Machine> GetMachineList()
        {
            var machines = new List<Machine>();

            for (int i = 0; i < 3; i++)
            {
                var machine = new Machine
                {
                    Name = $"Machine {i}",
                    Description = $"Machine Desc {i}",
                    Active = true
                };
                machines.Add(machine);
            }
            return machines;
        }
    }
}