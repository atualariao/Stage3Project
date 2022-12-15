using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Data
{
    public class TestAuthHandlerOptions : AuthenticationSchemeOptions
    {
        public Guid UserID { get; set; }
        public string Username { get; set; } = "Troy";
    }
}
