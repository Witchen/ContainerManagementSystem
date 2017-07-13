//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using System.Data.Entity;

namespace ContainerManagementSystem.Models
{
    public class ShipmentActivityContext : DbContext
    {
        public ShipmentActivityContext() : base("DefaultConnection")
            //AzureConnection
        {
            System.Diagnostics.Debug.WriteLine("Database is initialized");
        }

        public DbSet<ShipmentActivity> ShipmentActivities { get; set; }
    }
}