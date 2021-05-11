using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Game_Step.Models;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace Game_Step.IdentityViewModels
{
    public class PurchaseViewModel : User
    {
        public List<OrderNumber> Orders { get; set; }
        public OrderNumber OneOrder { get; set; }
    }
}
