using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Enums;

namespace AMLApi.Core
{
    public static class Extensions
    {
        public static string ToRoute(this StatType stat) => stat switch
        {
            StatType.Skill => "skill",
            StatType.Rng => "rng",
            StatType.Overall => "rating",
            StatType.MaxModeBeaten => "modesbeaten",
            _ => "",
        };

        public static Continent ContinentFromString(string? raw) => (raw ?? string.Empty).ToLowerInvariant() switch
        {
            "europe" => Continent.Europe,
            "america" => Continent.America,
            "asia" => Continent.Asia,
            "africa" => Continent.Africa,
            "oceania" => Continent.Oceania,
            "trans-continental" => Continent.TransContinental,
            _ => Continent.None,
        };
    }
}
