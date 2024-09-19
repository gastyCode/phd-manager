using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhDManager.Core.Models
{
    public class AuthResponse
    {
        public User User { get; set; } = default!;
        public string Token { get; set; } = string.Empty;
    }
}
