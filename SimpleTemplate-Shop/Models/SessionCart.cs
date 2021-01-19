using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SimpleTemplate_Shop.Infrastructure;

namespace SimpleTemplate_Shop.Models
{
    public class SessionCart : Cart
    {
        public static Cart GetCart(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            SessionCart cart = session?.GetJson<SessionCart>("Cart")
                ?? new SessionCart();
            cart.Session = session;
            return cart;
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddItem(Product product, int quantity, decimal sum)
        {
            base.AddItem(product, quantity, sum);
            Session.SetJson("Cart", this);
        }

        public override void RemoveItem(Product product, int quantity, decimal sum)
        {
            base.RemoveItem(product, quantity, sum);
            Session.SetJson("Cart", this);
        }

        public override void RemoveLine(Product product)
        {
            base.RemoveLine(product);
            Session.SetJson("Cart", this);
        }

        public override void Clear()
        {
            base.Clear();
            Session.Remove("Cart");
        }
    }
}
