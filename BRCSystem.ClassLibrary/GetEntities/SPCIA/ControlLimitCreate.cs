using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BRCSystem.ClassLibrary.SPCIA.Entities;


namespace BRCSystem.ClassLibrary.GetEntities.SPCIA
{
    public static class ControlLimitCreate
    {
        public static ControlLimit Get() {
            return new ControlLimit
            {
                Active = true,
                ApplyForDataGathering = true,
                ApplyForMonitoring = true, 
                ApplyForRequal = true,
                ApplyForSetup = true,
                CreationDate = DateTime.UtcNow,
                Limits = LimitCreate.GetList(),
                MachineId = 1,
                ProcessId = 1,
                UserId = 1
            };
        }

        public static List<ControlLimit> GetList()
        {
            var modelList = new List<ControlLimit>();

            for (int i = 0; i < 10; i++)
            {
                modelList.Add(Get());
            }

            return modelList;
        }
    }
}
