using System;
using System.Collections.Generic;
using DeltaSharingConnector.Client.Models;
using RestSharp;
using RestSharp.Authenticators;

namespace DeltaSharingConnector.Client
{
    public class DeltaSharingRestClient : IDeltaSharingClient, IDisposable
    {
        private readonly RestClient _client;

        public DeltaSharingRestClient(string prefix, string bearerToken)
        {
            _client = new RestClient(prefix)
            {
                Authenticator = new JwtAuthenticator(bearerToken)
            };
        }

        public DeltaSharingRestClient(DeltaSharingProfile profile) : this(profile.Endpoint, profile.BearerToken)
        {
        }

        public IEnumerable<Share> ListShares()
        {
            return _client.GetJson<Share[]>("shares");
        }

        public Share GetShare(string name)
        {
            return _client.GetJson<Share>("shares", new { share = name });
        }

        public IEnumerable<Table> ListAllTables()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Table> ListAllTablesInShare(Share share)
        {
            throw new NotImplementedException();
        }

        public long GetTableVersion(Table table)
        {
            throw new NotImplementedException();
        }

        #region IDisposable

        public void Dispose()
        {
            _client.Dispose();
        }

        #endregion
    }
}
