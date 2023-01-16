namespace DeltaSharingConnector.Client.Models
{
    public class DeltaTableFiles
    {
        public long Version { get; }
        public Protocol Protocol { get; }
        public Metadata Metadata { get; }

        public DeltaTableFiles(long version, Protocol protocol, Metadata metadata)
        {
            Version = version;
            Protocol = protocol;
            Metadata = metadata;
        }
    }
}
