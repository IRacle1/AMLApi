using System.Reflection;

using AMLApi.Core.Base;
using AMLApi.Core.Data;
using AMLApi.Core.Enums;

namespace AMLApi.Core
{
    public static class Extensions
    {
        private static readonly Dictionary<SkillSetType, Func<MaxModeData, int>> skillSetFunctions = new();

        static Extensions()
        {
            string endWord = "SkillSet";

            foreach (SkillSetType skillSet in Enum.GetValues<SkillSetType>())
            {
                if (skillSet is SkillSetType.None or SkillSetType.All)
                    continue;

                string name = skillSet.ToString();

                PropertyInfo prop = typeof(MaxModeData).GetProperty(name + endWord)!;
                var @delegate = (Func<MaxModeData, int>)Delegate.CreateDelegate(typeof(Func<MaxModeData, int>), prop.GetGetMethod()!);
                skillSetFunctions.Add(skillSet, @delegate);
            }
        }

        public static string ToRoute(this StatType stat) => stat switch
        {
            StatType.Skill => "skill",
            StatType.Rng => "rng",
            StatType.Overall => "rating",
            StatType.MaxModeBeaten => "modesbeaten",
            _ => "",
        };

        public static Continent ContinentFromString(this string? raw) => (raw ?? string.Empty).ToLowerInvariant() switch
        {
            "europe" => Continent.Europe,
            "america" => Continent.America,
            "asia" => Continent.Asia,
            "africa" => Continent.Africa,
            "oceania" => Continent.Oceania,
            "trans-continental" => Continent.TransContinental,
            _ => Continent.None,
        };

        public static int GetSkillSetValue(this MaxModeData maxModeData, SkillSetType type)
        {
            int value = (int)type;
            if (!skillSetFunctions.TryGetValue(type, out var func))
                return 0;

            return func(maxModeData);
        }
    }
}
