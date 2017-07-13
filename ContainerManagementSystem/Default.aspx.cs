using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ContainerManagementSystem.Models;

namespace ContainerManagementSystem
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var db = new ContainerManagementSystem.Models.ShipmentActivityContext();
            IQueryable<ShipmentActivity> query = db.ShipmentActivities;
            List<ShipmentActivity> ShipmentList = query.ToList();

            //Get the total of shipment - overall
            totalofshipmentoverallid.InnerText = ShipmentList.Count().ToString() + " Shipments";

            //Get the total of shipment - this month
            int thismonth = 0;
            string month = DateTime.Now.ToString("MM");
            for (int i = 0; i < ShipmentList.Count(); i++)
            {
                string shipmentid = ShipmentList.ElementAt(i).ShipmentID.ToString();
                string monthofshipment = ShipmentList.ElementAt(i).ShippingDate.ToString("MM");
                if (month.Equals(monthofshipment))
                {
                    thismonth++;
                }
                //System.Diagnostics.Debug.WriteLine("CMS Debug: i = " + i + ", id = " + shipmentid + ", month = " + monthofshipment);
                //System.Diagnostics.Debug.WriteLine("CMS Debug: Name = " + query.Skip(i).First().ShipmentID.ToString());
            }
            totalofshipmentmonthid.InnerText = thismonth + " Shipments";

            //Get the total of shipment - today
            int todayshipcount = 0;
            string date = DateTime.Now.ToString("dd-MMM-yyyy");
            for (int i = 0; i < ShipmentList.Count(); i++)
            {
                string shipmentid = ShipmentList.ElementAt(i).ShipmentID.ToString();
                string dateofshipment = ShipmentList.ElementAt(i).ShippingDate.ToString("dd-MMM-yyyy");
                if (date.Equals(dateofshipment))
                {
                    todayshipcount++;
                }
            }
            totalofshipmenttodayid.InnerText = todayshipcount + " Shipments";
        }

    }
}