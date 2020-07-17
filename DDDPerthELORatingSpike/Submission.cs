using System;
using System.Collections.Generic;
using System.Text.Json;

namespace DDDPerthELORatingSpike
{
    class Submission
    {
        public string PartitionKey { get; set; }
        public string Year => PartitionKey;
        public string RowKey { get; set; }
        public string SubmitterId => RowKey;
        public DateTimeOffset Timestamp { get; set; }

        public string ExternalId { get; set; }

        public string Session { get; set; }

        public Session DecodedSession => JsonSerializer.Deserialize<Session>(Session);
    }

    public class Session
    {
        public Guid Id { get; set; }
        public string ExternalId { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public Guid[] PresenterIds { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string Format { get; set; }
        public string Level { get; set; }
        public string[] Tags { get; set; }
        public Dictionary<string, string> DataFields { get; set; }
    }
}
