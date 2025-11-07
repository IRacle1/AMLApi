namespace AMLApi.Core.Enums
{
    [Flags]
    public enum PointType
    {
        None = 0,
        Skill = 1,
        Rng = 2,
        All = Skill | Rng,
    }
}
