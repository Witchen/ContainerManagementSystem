using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContainerManagementSystem.Models
{
    public class ShipmentActivity
    {
        [Key]
        [ScaffoldColumn(false)]
        public int ShipmentID { get; set; }

        public string Source { get; set; }

        public string Destination { get; set; }

        public string ItemName { get; set; }

        public string ItemQuantity { get; set; }

        [Column(TypeName="Date")]
        public DateTime ShippingDate { get; set; }
        
        public string ExpectedDuration { get; set; }

        public string ShipmentStatus { get; set; }
    }

}