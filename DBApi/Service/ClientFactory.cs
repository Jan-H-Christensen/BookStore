using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBApi.Service
{
    public static class ClientFactory
    {
        public static Client Create()
        {
            var client = new Client("redis", 6379, "");
            client.Connect();
            return client;
        }
    }
}