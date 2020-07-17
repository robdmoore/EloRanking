using System;
using System.Text.Json;

namespace DDDPerthELORatingSpike
{
    public class Vote
    {
        public string PartitionKey { get; set; }
        public string Year => PartitionKey;
        public string RowKey { get; set; }
        public string VoteId => RowKey;
        public DateTimeOffset Timestamp { get; set; }
        public string Indices { get; set; }
        public int[] DecodedIndices => JsonSerializer.Deserialize<int[]>(Indices);
        public string IpAddress { get; set; }
        public string SessionIds { get; set; }
        public string[] DecodedSessionIds => JsonSerializer.Deserialize<string[]>(SessionIds);
        public string TicketNumber { get; set; }
        public string VoterSessionId { get; set; }
        public DateTimeOffset VotingStartTime { get; set; }
        public DateTimeOffset VotingSubmittedTime { get; set; }
    }
}
