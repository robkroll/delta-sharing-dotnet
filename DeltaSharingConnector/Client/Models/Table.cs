namespace DeltaSharingConnector.Client.Models
{
    public class Table
    {
        public string Name { get; }
        public string Schema { get; }
        public string Share { get; }

        public Table(string name, string schema, string share)
        {
            Name = name;
            Schema = schema;
            Share = share;
        }
    }
}
