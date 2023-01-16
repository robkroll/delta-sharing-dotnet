using System;
using System.IO;
using System.Text.Json;

namespace DeltaSharingConnector.Client
{
    public class DeltaSharingProfile
    {
        public long? ShareCredentialsVersion { get; }
        public string Endpoint { get; }
        public string BearerToken { get; }
        public DateTime? ExpirationTime { get; }

        public DeltaSharingProfile(string endpoint, string bearerToken)
        {
            Endpoint = endpoint;
            BearerToken = bearerToken;
        }

        public static DeltaSharingProfile FromFile(string file)
        {
            var text = File.ReadAllText(file);
            return FromJson(text);
        }

        private static DeltaSharingProfile FromJson(string json)
        {
            var profile = JsonSerializer.Deserialize<DeltaSharingProfile>(json, new JsonSerializerOptions(JsonSerializerDefaults.Web));
            if (profile is null)
                throw new InvalidOperationException("Unable to parse json.");

            return profile;
        }
    }
}
