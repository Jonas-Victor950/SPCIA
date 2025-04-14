using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRCSystem.ClassLibrary.Data
{
    public class AwsSecretsConfiguration
    {
        public string Secret { get; set; }
        public string DefaultConnection { get; set; }
    }
}