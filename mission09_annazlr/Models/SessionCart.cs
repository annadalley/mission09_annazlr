using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using mission09_annazlr.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace mission09_annazlr.Models
{
    public class SessionCart : Cart
    {
        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();

            cart.Session = session;

            return cart;
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddItem(Book bo, int qty)
        {
            base.AddItem(bo, qty);
            Session.SetJson("Cart", this);
        }

        public override void DeleteItem(Book book)
        {
            base.DeleteItem(book);
            Session.SetJson("Cart", this);
        }

        public override void clearCart()
        {
            base.clearCart();
            Session.Remove("Cart");
        }
    }
}
