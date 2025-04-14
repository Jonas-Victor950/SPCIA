using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BRCSystem.ClassLibrary.SPCIA.Entities;


namespace BRCSystem.ClassLibrary.GetEntities.SPCIA
{
    public static class LimitCreate
    {
        public static Limit Get()
        {
            return new Limit
            {
                CharacteristicId = 1,
                UCLx = 2.00,
                CLr = 2.00,
                CLx = 2.00,
                LCLx = 2.00,
                UCLr = 2.00,
                SetById = 1,
                SetDate = DateTime.UtcNow
            };
        }

        public static List<Limit> GetList()
        {
            var modelList = new List<Limit>();

            for (int i = 0; i < 10; i++)
            {
                modelList.Add(Get());
            }

            return modelList;
        }
    }
}
