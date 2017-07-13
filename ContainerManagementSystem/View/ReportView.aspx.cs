using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ContainerManagementSystem.Models;

namespace ContainerManagementSystem.View
{
    public partial class ReportView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetFilteredSourceNumber();
        }
        public IQueryable<ShipmentActivity> GetShipmentActivities()
        {
            var db = new ContainerManagementSystem.Models.ShipmentActivityContext();
            IQueryable<ShipmentActivity> query = db.ShipmentActivities;
            return query;
        }

        public List<ShipmentActivity> GetFilteredSource()
        {
            List<ShipmentActivity> shipmentList = GetShipmentActivities().ToList();
            List<ShipmentActivity> filteredShipmentList = new List<ShipmentActivity>();
            for (int i = 0; i < shipmentList.Count(); i++)
            {
                string ShipmentID1 = shipmentList.ElementAt(i).Source.ToString();
                bool exists = false;
                for (int j = 0; j < filteredShipmentList.Count(); j++)
                {
                    string ShipmentID2 = shipmentList.ElementAt(j).Source.ToString();
                    if (ShipmentID1.Equals(ShipmentID2))
                    {
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    filteredShipmentList.Add(shipmentList.ElementAt(i));
                }
            }
            return filteredShipmentList;
        }

        public List<Int32> GetFilteredSourceNumber()
        {
            List<ShipmentActivity> shipmentList = GetShipmentActivities().ToList();
            List<ShipmentActivity> filteredShipmentList = GetFilteredSource();
            List<Int32> filteredShipmentCountList = new List<Int32>();

            //Populate 0 count on each source count
            for (int i = 0; i < filteredShipmentList.Count(); i++)
            {
                filteredShipmentCountList.Insert(i, 0);
            }

            for (int i = 0; i < shipmentList.Count(); i++)
            {
                string ShipmentID1 = shipmentList.ElementAt(i).Source.ToString();
                
                for (int j = 0; j < filteredShipmentList.Count(); j++)
                {
                    string ShipmentID2 = filteredShipmentList.ElementAt(j).Source.ToString();

                    System.Diagnostics.Debug.WriteLine("CMS Debug: Compare between " + ShipmentID1 + " and " + ShipmentID2);
                    if (ShipmentID1.Equals(ShipmentID2))
                    {
                        int currentNumber = filteredShipmentCountList.ElementAt(j);
                        currentNumber++;
                        filteredShipmentCountList.RemoveAt(j);
                        filteredShipmentCountList.Insert(j, currentNumber);
                        System.Diagnostics.Debug.WriteLine("CMS Debug: It is same and the value is updated to " + currentNumber);
                        break;
                    }
                }
            }

            System.Diagnostics.Debug.WriteLine("CMS Debug: Filtered Shipment Count = " + filteredShipmentList.Count() + " and " + filteredShipmentCountList.Count());
            for (int i = 0; i < filteredShipmentList.Count(); i++)
            {
                System.Diagnostics.Debug.WriteLine("CMS Debug: Filtered Shipment Source Name = " + filteredShipmentList.ElementAt(i).Source.ToString() + " with count = " + filteredShipmentCountList.ElementAt(i).ToString());
            }

            return filteredShipmentCountList;
        }

        public List<Int32> testGetInt()
        {
            List<System.Int32> intlist = new List<Int32>();
            intlist.Add(10);
            intlist.Add(20);
            intlist.Add(50);
            return intlist;
        }
    }
}