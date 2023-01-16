using System.Collections.Generic;
using DeltaSharingConnector.Client.Models;

namespace DeltaSharingConnector.Client
{
    public interface IDeltaSharingClient
    {
        IEnumerable<Share> ListShares();
        IEnumerable<Table> ListAllTables();
        IEnumerable<Table> ListAllTablesInShare(Share share);
        long GetTableVersion(Table table);
        Share GetShare(string name);
    }
}