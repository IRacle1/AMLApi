using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Api.Enums;
using AMLApi.Api.Objects;

namespace AMLApi.Api
{
    public static class Extensions
    {
        public static string ToRoute(this StatType stat)
        {
            return stat switch
            {
                StatType.Skill => "skill",
                StatType.Rng => "rng",
                StatType.Overall => "rating",
                StatType.MaxModeBeaten => "modesbeaten",
                _ => "",
            };
        }
    }
}
