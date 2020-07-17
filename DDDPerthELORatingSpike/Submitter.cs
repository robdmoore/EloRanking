using System;
using System.Collections.Generic;
using System.Text.Json;

namespace DDDPerthELORatingSpike
{
    public class Submitter
    {
        public string PartitionKey { get; set; }
        public string Year => PartitionKey;
        public string RowKey { get; set; }
        public string SubmitterId => RowKey;
        public DateTimeOffset Timestamp { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public string Presenter { get; set; }
        public Presenter DecodedPresenter => JsonSerializer.Deserialize<Presenter>(Presenter);
    }

    public class Presenter
    {
        public Guid Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string Tagline { get; set; }
        public string Bio { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public string WebsiteUrl { get; set; }
        public string TwitterHandle { get; set; }
        public Dictionary<string, string> DataFields { get; set; }
    }
}
