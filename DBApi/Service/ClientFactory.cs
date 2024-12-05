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
            return new Client("localhost",6379,"");
        }
    }
}