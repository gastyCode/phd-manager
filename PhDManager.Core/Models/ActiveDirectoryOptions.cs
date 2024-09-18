using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhDManager.Core.Models
{
    public class ActiveDirectoryOptions
    {
        public const string ActiveDirectory = "ActiveDirectory";

        public string LdapPath { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
