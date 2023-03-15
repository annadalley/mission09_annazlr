using Microsoft.AspNetCore.Mvc;
using mission09_annazlr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mission09_annazlr.Controllers
{
    public class PurchaseController : Controller
    {
        
        private IPurchaseRepository repo { get; set; }
        private Cart cart { get; set; }

        public PurchaseController (IPurchaseRepository temp, Cart c)
        {
            repo = temp;
            cart = c;
        }

        public IActionResult Checkout()
        {
            return View(new ActualPurchase());
        }

        [HttpPost]
        public IActionResult Checkout(ActualPurchase purchase)
        {
            if (cart.Items.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                purchase.Lines = cart.Items.ToArray();
                repo.SavePurchase(purchase);
                cart.clearCart();

                return RedirectToPage("/Confirmation");
            }
            else
            {
                return View();
            }
        }
    }
}
