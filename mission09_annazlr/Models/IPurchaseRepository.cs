using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mission09_annazlr.Models
{
    //this is to connect the database.  It is setting up the interface.
    public interface IPurchaseRepository
    {
        IQueryable<ActualPurchase> Purchase {get;}
        void SavePurchase(ActualPurchase purchase);
    }
}
