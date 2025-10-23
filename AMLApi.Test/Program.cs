using AMLApi.Core;

namespace AMLApi.Test
{
    internal class Program
    {
        static void Main(string[] args)
            => MainAsync(args).GetAwaiter().GetResult();

        static async Task MainAsync(string[] args)
        {
            var client = await AmlClient.CreateClient();

            var list = client.GetMaxModeListByRatio(90).Take(300);

            int it = 1;
            foreach (var item in list)
            {
                await item.GetOrFetchRecords();
                Console.WriteLine($"{it++}/300");
            }

            var ordered = client.Players.Select(p =>
            {
                var list = p.RecordsCache.OrderByDescending(r => r.MaxMode.GetPoints(Core.Enums.PointType.Skill)).Take(3).ToList();
                var mid = list.Sum(m => m.MaxMode.GetPoints(Core.Enums.PointType.Skill)) / 3.0;

                return Tuple.Create(p, list, Math.Round(mid, 2));
            }).OrderByDescending(i => i.Item3);

            it = 1;
            foreach (var item in ordered)
            {
                Console.WriteLine($"{it++}({item.Item3}): {item.Item1.Name}");
                foreach (var record in item.Item2)
                {
                    Console.WriteLine($" - {record.MaxMode.Name} | {record.MaxMode.GameName}");
                }
            }
        }
    }
}
