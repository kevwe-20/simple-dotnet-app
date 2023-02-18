using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ampifan.Models.Expo
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PushTicketRequest
    {
        [JsonProperty(PropertyName = "to")]
        public List<string> PushTo { get; set; } = default!;

        [JsonProperty(PropertyName = "data")]
        public object PushData { get; set; } = default!;

        [JsonProperty(PropertyName = "title")]
        public string PushTitle { get; set; } = default!;

        [JsonProperty(PropertyName = "body")]
        public string PushBody { get; set; } = default!;

        [JsonProperty(PropertyName = "ttl")]
        public int? PushTTL { get; set; } = default!;

        [JsonProperty(PropertyName = "expiration")]
        public int? PushExpiration { get; set; } = default!;

        [JsonProperty(PropertyName = "priority")]  //'default' | 'normal' | 'high'
        public string PushPriority { get; set; } = default!;

        [JsonProperty(PropertyName = "subtitle")]
        public string PushSubTitle { get; set; } = default!;

        [JsonProperty(PropertyName = "sound")] //'default' | null	
        public string PushSound { get; set; } = default!;

        [JsonProperty(PropertyName = "badge")]
        public int? PushBadgeCount { get; set; } = default!;

        [JsonProperty(PropertyName = "channelId")]
        public string PushChannelId { get; set; } = default!;

        [JsonProperty(PropertyName = "categoryId")]
        public string PushCategoryId { get; set; } = default!;
    }
}
