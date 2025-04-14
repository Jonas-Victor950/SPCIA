using BRCSystem.ClassLibrary.SPCIA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRCSystem.ClassLibrary.GetEntities.SPCIA
{
    public static class ProcessSamplingCreate
    {
        public static ProcessSampling Get()
        {
            return new ProcessSampling
            {
                Classifier = Classifier.DataGathering,
                MachineId = 1,
                OperatorId = 1,
                ProcessId = 1,
                SampleDate = DateTime.UtcNow,
                Samples = Samples()
            };
        }

        public static ProcessSampling GetWithOneCaracteristic()
        {
            return new ProcessSampling
            {
                Classifier = Classifier.DataGathering,
                MachineId = 1,
                OperatorId = 1,
                ProcessId = 1,
                SampleDate = DateTime.UtcNow,
                Samples = SamplesWithSingleCharacteristic()
            };
        }

        private static List<Samples> Samples()
        {
            var inputs = new List<Input>();

            for (int i = 0; i < 5; i++)
            {
                inputs.Add(new Input { Unit = i + 1, Value = i + 1 });
            }

            var samples = new List<Samples>();

            foreach (var characteristic in CharacteristicCreate.GetList())
            {
                samples.Add(new Samples { Characteristic = characteristic, Inputs = inputs });
            }

            return samples;
        }

        private static List<Samples> SamplesWithSingleCharacteristic()
        {
            var inputs = new List<Input>();

            for (int i = 0; i < 5; i++)
            {
                inputs.Add(new Input { Unit = i + 1, Value = i + 1 });
            }

            var samples = new List<Samples>();

            var characteristic = CharacteristicCreate.Get();

            for(int i = 0; i < 10; i++)
            {
                samples.Add(new Samples { Characteristic = characteristic, Inputs = inputs });
            }

            return samples;
        }

    }
}
