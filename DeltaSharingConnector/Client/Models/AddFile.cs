using System.Collections.Generic;

namespace DeltaSharingConnector.Client.Models
{
    public class AddFile
    {
        public string Url { get; }
        public string Id { get; }
        public Dictionary<string, string> PartitionValues { get; }
        public long Size { get; }
        public string Stats { get; }

        public AddFile(string url, string id, Dictionary<string, string> partitionValues, long size, string stats)
        {
            Url = url;
            Id = id;
            PartitionValues = partitionValues;
            Size = size;
            Stats = stats;
        }
    }
}
