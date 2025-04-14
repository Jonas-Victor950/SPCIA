using BRCSystem.ClassLibrary.Authentication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRCSystem.ClassLibrary.GetEntities.Authorization
{
    public static class ParametersCreate
    {
        public static Parameters GetParameters()
        {
            return new Parameters
            {
                EmailDomain = "smtp.gmail.com",
                EmailPort = 587,
                EmailAccount = "sigequalis@gmail.com",
                EmailPassword = "Pdbo68derCSSysgOv40ubwEYNdnZ/GDfFh8RL3UVf56Woz4K7ZgjcoY4447RVKb+",
                TokenExpireTimeMinutes = 1440,
                AbleMFA = false,
            };
        }
    }
}
