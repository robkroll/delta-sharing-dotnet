namespace DeltaSharingConnector.Client.Models
{
    public class DeltaTableMetadata
    {
        public long Version { get; }
        public Protocol Protocol { get; }
        public Metadata Metadata { get; }

        public DeltaTableMetadata(long version, Protocol protocol, Metadata metadata)
        {
            Version = version;
            Protocol = protocol;
            Metadata = metadata;
        }
    }
}
