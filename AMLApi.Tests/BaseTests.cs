using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core;
using AMLApi.Core.Base;
using AMLApi.Core.Cached;
using AMLApi.Core.Data;
using AMLApi.Core.Enums;

using Xunit.Abstractions;

namespace AMLApi.Tests
{
    public class BaseTests : IClassFixture<AmlClientFixture>
    {
        private readonly AmlClientFixture clientFixture;
        private readonly ITestOutputHelper output;

        public BaseTests(AmlClientFixture clientFixture, ITestOutputHelper output)
        {
            this.clientFixture = clientFixture;
            this.output = output;
        }

        [Fact]
        public void SkillSet_ValidSkillSetParsing()
        {
            MaxModeData data = new()
            {
                AimSkillSet = 1,
                BrainSkillSet = 2,
                GreenrunSkillSet = 4,
                SpeedSkillSet = 8,
                EnduranceSkillSet = 16,
                KeyboardSkillSet = 32,
            };

            Assert.Equal(1, data.GetSkillSetValue(SkillSetType.Aim));
            Assert.Equal(2, data.GetSkillSetValue(SkillSetType.Brain));
            Assert.Equal(4, data.GetSkillSetValue(SkillSetType.Greenrun));
            Assert.Equal(8, data.GetSkillSetValue(SkillSetType.Speed));
            Assert.Equal(16, data.GetSkillSetValue(SkillSetType.Endurance));
            Assert.Equal(32, data.GetSkillSetValue(SkillSetType.Keyboard));
        }
    }
}
