using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ampifan.Models.Expo
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PushReceiptRequest
    {
        [JsonProperty(PropertyName ="ids")]
        public List<string> PushTicketIds { get; set; } = default!;
    }
}
