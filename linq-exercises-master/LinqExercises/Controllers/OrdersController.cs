using LinqExercises.Infrastructure;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace LinqExercises.Controllers
{
    public class OrdersController : ApiController
    {
        private NORTHWNDEntities _db;

        public OrdersController()
        {
            _db = new NORTHWNDEntities();
        }

        //GET: api/orders/between/01.01.1997/12.31.1997
        [HttpGet, Route("api/orders/between/{startDate}/{endDate}"), ResponseType(typeof(IQueryable<Order>))]
        public IHttpActionResult GetOrdersBetween(DateTime startDate, DateTime endDate)
        {
            var getbetweendates = _db.Orders.Where(o => o.RequiredDate >= startDate && o.RequiredDate <=  endDate);
            return Ok(getbetweendates);
        }

        //GET: api/orders/reports/purchase
        [HttpGet, Route("api/orders/reports/purchase"), ResponseType(typeof(IQueryable<object>))]
        public IHttpActionResult PurchaseReport()
        {
            // See this blog post for more information about projecting to anonymous objects. https://blogs.msdn.microsoft.com/swiss_dpe_team/2008/01/25/using-your-own-defined-type-in-a-linq-query-expression/
            var report = from product in _db.Products

                          select new
                          {
                              ProductName = product.ProductName,
                              QuantityPurchased = product.Order_Details.Sum(o => o.Quantity)

                          } into anonymous
                          orderby anonymous.QuantityPurchased descending
                          select anonymous;
            return Ok(report);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }
    }
}
