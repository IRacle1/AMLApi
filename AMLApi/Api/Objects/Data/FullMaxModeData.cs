using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AMLApi.Api.Objects.Data
{
    public class FullMaxModeData
    {
        [JsonPropertyName("data")]
        public MaxModeData Data { get; set; } = null!;

        [JsonPropertyName("records")]
        public RecordData[] Records { get; set; } = null!;
    }
}
