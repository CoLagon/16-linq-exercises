using LinqExercises.Infrastructure;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace LinqExercises.Controllers
{
    public class CustomersController : ApiController
    {
        private NORTHWNDEntities _db;

        public CustomersController()
        {
            _db = new NORTHWNDEntities();
        }

        // GET: api/customers/city/London
        [HttpGet, Route("api/customers/city/{city}"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetAll(string city)
        {
            var LondonResults = from Customers in _db.Customers
                                where Customers.City.Contains(city)
                                select Customers;
            return Ok(LondonResults);

            //foreach (var Customers in _db.Customers)
            //{
            //    Console.Read(Customers.City.Contains(city));
            //}
            //Console.WriteLine(city)
        }

        // GET: api/customers/mexicoSwedenGermany
        [HttpGet, Route("api/customers/mexicoSwedenGermany"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetAllFromMexicoSwedenGermany()
        {
            var MSGCustomers = from customer in _db.Customers
                               where customer.Country.Contains("Mexico") || customer.Country.Contains("Sweden") || customer.Country.Contains("Germany")
                               select customer;
            return Ok(MSGCustomers);
        }

        // GET: api/customers/shippedUsing/Speedy Express
        [HttpGet, Route("api/customers/shippedUsing/{shipperName}"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetCustomersThatShipWith(string shipperName)
        {
            var result = _db.Customers.Where(c => c.Orders.Any(o => o.Shipper.CompanyName == shipperName));

            return Ok(result);

            //var shipperResult = from shipment in _db.Orders
            //                    where shipment.Shipper.CompanyName == shipperName
            //                    select shipment.Customer;

            //return Ok(shipperResult);

        }

        // GET: api/customers/withoutOrders
        [HttpGet, Route("api/customers/withoutOrders"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetCustomersWithoutOrders()
        {
            var result = _db.Customers.Where(c => c.Orders.Count() == 0);
            return Ok(result);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }
    }
}
