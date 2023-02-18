using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Ampifan.Models.Expo
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PushTicketResponse
    {
        [JsonProperty(PropertyName = "data")]
        public List<PushTicketStatus> PushTicketStatuses { get; set; } = default!;

        [JsonProperty(PropertyName = "errors")]
        public List<PushTicketErrors> PushTicketErrors { get; set; } = default!;

    }

    [JsonObject(MemberSerialization.OptIn)]
    public class PushTicketStatus
    {

        [JsonProperty(PropertyName = "status")] //"error" | "ok",
        public string TicketStatus { get; set; } = default!;

        [JsonProperty(PropertyName = "id")]
        public string TicketId { get; set; } = default!;

        [JsonProperty(PropertyName = "message")]
        public string TicketMessage { get; set; } = default!;

        [JsonProperty(PropertyName = "details")]
        public object TicketDetails { get; set; } = default!;
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class PushTicketErrors
    {
        [JsonProperty(PropertyName = "code")]
        public string ErrorCode { get; set; } = default!;

        [JsonProperty(PropertyName = "message")]
        public string ErrorMessage { get; set; } = default!;
    }
}