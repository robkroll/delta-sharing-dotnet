using System.Collections.Generic;

namespace DeltaSharingConnector.Client.Models
{
    public class Metadata
    {
        public string Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string Format { get; }
        public string SchemaString { get; }
        public IEnumerable<string> PartitionColumns { get; }

        public Metadata(string id, string name, string description, string format, string schemaString, IEnumerable<string> partitionColumns)
        {
            Id = id;
            Name = name;
            Description = description;
            Format = format;
            SchemaString = schemaString;
            PartitionColumns = partitionColumns;
        }
    }
}
