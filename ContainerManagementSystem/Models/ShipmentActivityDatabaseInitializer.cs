using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ContainerManagementSystem.Models
{
    //public class ShipmentActivityDatabaseInitializer : DropCreateDatabaseAlways<ShipmentActivityContext>
    public class ShipmentActivityDatabaseInitializer : DropCreateDatabaseIfModelChanges<ShipmentActivityContext>
    {
        protected override void Seed(ShipmentActivityContext context)
        {
            GetShipmentActivities().ForEach(c => context.ShipmentActivities.Add(c));
        }

        private static List<ShipmentActivity> GetShipmentActivities()
        {
            var ShipmentActitivies = new List<ShipmentActivity>
            {
                new ShipmentActivity
                {
                    ShipmentID = 1,
                    Source = "Indonesia",
                    Destination = "Malaysia",
                    ItemName = "Car",
                    ItemQuantity = "100 Units",
                    ShippingDate = Convert.ToDateTime("2017-06-25"),
                    ExpectedDuration = "15 days",
                    ShipmentStatus = "Delievered"
                },
                new ShipmentActivity
                {
                    ShipmentID = 2,
                    Source = "Indonesia",
                    Destination = "Malaysia",
                    ItemName = "Planes",
                    ItemQuantity = "100 Units",
                    ShippingDate = Convert.ToDateTime("2017-06-06"),
                    ExpectedDuration = "3 Months",
                    ShipmentStatus = "In Delivery"
                },
                new ShipmentActivity
                {
                    ShipmentID = 3,
                    Source = "Australia",
                    Destination = "Malaysia",
                    ItemName = "Motorcycle",
                    ItemQuantity = "150 Units",
                    ShippingDate = Convert.ToDateTime("2017-04-20"),
                    ExpectedDuration = "1 Months",
                    ShipmentStatus = "Delivered"
                },
                new ShipmentActivity
                {
                    ShipmentID = 4,
                    Source = "London",
                    Destination = "Malaysia",
                    ItemName = "Books",
                    ItemQuantity = "350 Exemplars",
                    ShippingDate = Convert.ToDateTime("2017-08-24"),
                    ExpectedDuration = "1 Months",
                    ShipmentStatus = "In Delivery"
                },
                new ShipmentActivity
                {
                    ShipmentID = 5,
                    Source = "Ireland",
                    Destination = "Japan",
                    ItemName = "Toys",
                    ItemQuantity = "300 Units",
                    ShippingDate = Convert.ToDateTime("2017-08-25"),
                    ExpectedDuration = "1 Months",
                    ShipmentStatus = "In Delivery"
                },
                new ShipmentActivity
                {
                    ShipmentID = 6,
                    Source = "Korea",
                    Destination = "Japan",
                    ItemName = "Laptop",
                    ItemQuantity = "30 Units",
                    ShippingDate = Convert.ToDateTime("2017-08-25"),
                    ExpectedDuration = "1 Months",
                    ShipmentStatus = "Cancelled"
                }
            };

            return ShipmentActitivies;
        }
    }
}