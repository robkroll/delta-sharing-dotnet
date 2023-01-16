namespace DeltaSharingConnector.Client.Models
{
    public class Protocol
    {
        public int MinReaderVersion { get; }

        public Protocol(int minReaderVersion)
        {
            MinReaderVersion = minReaderVersion;
        }
    }
}
