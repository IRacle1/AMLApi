namespace AMLApi.Core.Enums
{
    [Flags]
    public enum SkillSetType
    {
        None = 0,
        Aim = 1 << 1,
        Speed = 1 << 2,
        Greenrun = 1 << 3,
        Keyboard = 1 << 4,
        Brain = 1 << 5,
        Endurance = 1 << 6,

        All = Aim | Speed | Greenrun | Keyboard | Brain | Endurance,
    }
}
