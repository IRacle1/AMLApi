using System.Reflection;

using AMLApi.Core.Base;
using AMLApi.Core.Data.MaxModes;
using AMLApi.Core.Data.Players;
using AMLApi.Core.Enums;

namespace AMLApi.Core
{
    /// <summary>
    /// General extensions class.
    /// </summary>
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

        /// <summary>
        /// Returns route part for requesting player leaderboard.
        /// </summary>
        /// <param name="stat">Target stat to get leaderboard.</param>
        /// <returns>Route part for requesting player leaderboard.</returns>
        public static string ToRoute(this StatType stat) => stat switch
        {
            StatType.Skill => "skill",
            StatType.Rng => "rng",
            StatType.Overall => "rating",
            StatType.MaxModeBeaten => "modesbeaten",
            _ => "",
        };

        /// <summary>
        /// Returns <see cref="Continent"/> enum from raw string from request.
        /// </summary>
        /// <param name="raw">Raw string from request</param>
        /// <returns><see cref="Continent"/> enum corresponded input string.</returns>
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

        public static ClanRoleType RoleTypeFromString(this string? raw) => (raw ?? string.Empty).ToLowerInvariant() switch
        {
            "member" => ClanRoleType.Member,
            "admin" => ClanRoleType.Admin,
            "owner" => ClanRoleType.Owner,
            _ => ClanRoleType.Member,
        };

        /// <summary>
        /// Gets skillset value from <see cref="MaxModeData"/>, by <see cref="SkillSetType"/>.
        /// </summary>
        /// <param name="maxModeData">Target <see cref="MaxModeData"/>.</param>
        /// <param name="type">Target <see cref="SkillSetType"/>.</param>
        /// <returns>Skillset value.</returns>
        public static int GetSkillSetValue(this MaxModeData maxModeData, SkillSetType type)
        {
            int value = (int)type;
            if (!skillSetFunctions.TryGetValue(type, out var func))
                return 0;

            return func(maxModeData);
        }

        internal static async Task<IReadOnlyList<T>> GetAllPagesPlayers<T>(this RawAmlClient client, StatType statType, Func<PlayerData, T> selector)
        {
            int page = 1;

            List<T> ret = new();
            PlayerData[] raw;
            do
            {
                raw = await client.FetchPlayerLeaderboard(statType, page++);

                foreach (PlayerData item in raw)
                {
                    ret.Add(selector(item));
                }
            }
            while (raw.Length == 1000);

            return ret;
        }
    }
}
