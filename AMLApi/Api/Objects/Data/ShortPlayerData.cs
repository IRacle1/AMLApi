using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AMLApi.Api.Objects.Data
{
    public class ShortPlayerData
    {
        [JsonPropertyName("id")]
        public Guid Guid { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

    }
}
