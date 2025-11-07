using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMLApi.Core.Enums
{
    [Flags]
    public enum SkillsetType
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
