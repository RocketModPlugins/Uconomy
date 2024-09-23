using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fr34kyn01535.Uconomy.Models
{
    public class Account
    {
        public string Id { get; set; }
        public decimal Balance { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
