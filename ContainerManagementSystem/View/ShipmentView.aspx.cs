using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ContainerManagementSystem.Models;
using System.Data.Entity;

namespace ContainerManagementSystem.View
{
    public partial class ShipmentView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("CMS Debug: Shipment Page is loaded");

            //Set the latest Shipment ID for new Shipment
            string latestShipmentID = GetLatestShipmentID().ToString();
            //shipmentidtextbox.Text = latestShipmentID;
            //shipmentidtextbox.Attributes.Add("readonly", "readonly");
        }

        public void AddNewShipment(object sender, EventArgs e)
        {
            bool ErrorExists = false;
            string ErrorMessage = "";

            //string c1 = Request.Form.Get("sourcename");
            //string c3 = Request.Form["sourcename"];
            //string ShipmentIdValue = shipmentidtextbox.Text;
            string SourceValue = sourceid.Value;
            string DestinationValue = destinationid.Value;
            string ItemNameValue = itemnameid.Value;
            string ItemQuantityValue = itemquantityid.Value;
            string ShippingDateValue = shippingdateid.Value;
            string ExpectedDurationValue = expecteddurationid.Value;
            string ShipmentStatusValue = shipmentstatusid.Value;
            System.Diagnostics.Debug.WriteLine("CMS Debug: The shipping value is " + ShippingDateValue);

            System.Diagnostics.Debug.WriteLine("CMS Debug: Adding new shipment");
            //Validate ShipmentIdValue
            //if (!String.IsNullOrWhiteSpace(ShipmentIdValue))
            //{
            //    int i;
            //    if (int.TryParse(ShipmentIdValue, out i))
            //    {
            //        ErrorExists = false;
            //    }
            //    else
            //    {
            //        ErrorExists = true;
            //        ErrorMessage = "The Shipment ID value is not a number, Please input number value.";
            //    }
            //}
            //else
            //{
            //    ErrorExists = true;
            //    ErrorMessage = "The Shipment ID field is empty.";
            //    //HttpContext.Current.Session["ShipmentErrorMessage"] = "The field is empty";
            //}
            //Validate Sourcevalue
            if ((!ErrorExists) && String.IsNullOrWhiteSpace(SourceValue))
            {
                ErrorExists = true;
                ErrorMessage = "The Source value field is empty.";
            }
            //Validate DestinationValue
            if ((!ErrorExists) && String.IsNullOrWhiteSpace(DestinationValue))
            {
                ErrorExists = true;
                ErrorMessage = "The Destination value field is empty.";
            }
            //Validate ItemNameValue
            if ((!ErrorExists) && String.IsNullOrWhiteSpace(ItemNameValue))
            {
                ErrorExists = true;
                ErrorMessage = "The Item Name value field is empty.";
            }
            //Validate ItemQuantityValue
            if ((!ErrorExists) && String.IsNullOrWhiteSpace(ItemQuantityValue))
            { 
                ErrorExists = true;
                ErrorMessage = "The Item Quantity value field is empty."; 
            }
            //Validate ShippingDateValue
            if ((!ErrorExists) && String.IsNullOrWhiteSpace(ShippingDateValue))
            {
                ErrorExists = true;
                ErrorMessage = "The Shipping Date is not chosen yet.";
            }
            //Validate ExpectedDurationValue
            if ((!ErrorExists) && String.IsNullOrWhiteSpace(ExpectedDurationValue))
            {
                ErrorExists = true;
                ErrorMessage = "The Expected Duration value field is empty.";
            }
            //Validate ShipmentStatusValue
            if ((!ErrorExists) && String.IsNullOrWhiteSpace(ShipmentStatusValue))
            {
                ErrorExists = true;
                ErrorMessage = "The Shipment Status value field is empty.";
            }

            //Error message print
            if (ErrorExists)
            {
                //Open the add shipment page
                Page.ClientScript.RegisterStartupScript(this.GetType(), "addKey1", "showAddShipmentPanel();", true);
                System.Diagnostics.Debug.WriteLine("CMS Debug: There is error, failed to add new shipment");
                //Remove the hidden class of tr to show the error message
                var newClassValue = validationrow.Attributes["class"].Replace("hidden", "");
                validationrow.Attributes.Remove("class");
                validationrow.Attributes.Add("class", newClassValue);
                //Set the error message
                validationmessage.InnerText = ErrorMessage;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("CMS Debug: Successfully add new shipment");
                //Create the new shipment activity
                var newShipmentActivity = new ShipmentActivity
                {
                    //ShipmentID = Int32.Parse(ShipmentIdValue),
                    Source = SourceValue,
                    Destination = DestinationValue,
                    ItemName = ItemNameValue,
                    ItemQuantity = ItemQuantityValue,
                    ShippingDate = Convert.ToDateTime(ShippingDateValue),
                    ExpectedDuration = ExpectedDurationValue,
                    ShipmentStatus = ShipmentStatusValue
                };

                //Add to the database
                var db = new ContainerManagementSystem.Models.ShipmentActivityContext();
                db.ShipmentActivities.Add(newShipmentActivity);
                db.SaveChanges();

                Response.Redirect(Request.RawUrl);
            }
        }
        
        public void LoadShipmentData(object sender, EventArgs e)
        {
            string shipmentID = loadshipmentid.Value;
            LoadShipmentDataWithID(shipmentID);
        }

        public void LoadShipmentData2(object sender, EventArgs e)
        {
            HtmlAnchor anchor = (HtmlAnchor)sender;
            LoadShipmentDataWithID(anchor.Attributes["data-shipmentid"].ToString());
        }

        public void LoadShipmentDataWithID(string ShipmentIDValue)
        {
            bool ErrorExists = false;
            string ErrorMessage = "";

            string shipmentID = ShipmentIDValue;
            System.Diagnostics.Debug.WriteLine("CMS Debug: Load Shipment ID = " + shipmentID);

            //Validate Value
            if (!String.IsNullOrWhiteSpace(shipmentID))
            {
                int i;
                if (!int.TryParse(shipmentID, out i))
                {
                    ErrorExists = true;
                    ErrorMessage = "The Shipment ID must be a number";
                }
            }
            else
            {
                ErrorExists = true;
                ErrorMessage = "The Shipment ID value is empty";
            }

            ShipmentActivity ShipmentActivityData = null;
            //Check whether shipment id exists
            if (!ErrorExists)
            {
                IQueryable<ShipmentActivity> query = GetShipmentActivities();
                int shipmentIDinInt = Int32.Parse(shipmentID);
                query = query.Where(p => p.ShipmentID == shipmentIDinInt);
                if (query.Count() != 0)
                {
                    ShipmentActivityData = query.First();
                }
                else
                {
                    ErrorExists = true;
                    ErrorMessage = "The specified Shipment ID does not exists";
                }
            }

            //Refresh update field
            RefreshUpdateField();

            if (!ErrorExists)
            {
                System.Diagnostics.Debug.WriteLine("CMS Debug: Chosen Item Name = " + ShipmentActivityData.ItemName);
                updateshipmentid.Text = ShipmentActivityData.ShipmentID.ToString();
                updatesourceid.Value = ShipmentActivityData.Source;
                updatedestinationid.Value = ShipmentActivityData.Destination;
                updateitemnameid.Value = ShipmentActivityData.ItemName;
                updateitemquantityid.Value = ShipmentActivityData.ItemQuantity.ToString();
                updateshippingdateid.Value = ShipmentActivityData.ShippingDate.ToString("dd-MMM-yyyy");
                updateexpecteddurationid.Value = ShipmentActivityData.ExpectedDuration.ToString();
                updateshipmentstatusid.Value = ShipmentActivityData.ShipmentStatus;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "editKey2", "enableUpdateField();", true);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("CMS Debug: There is error, failed to load shipment data");
                //Remove the hidden class of tr to show the error message
                var newClassValue = loaddataerrormessageid.Attributes["class"].Replace("hidden", "");
                loaddataerrormessageid.Attributes.Remove("class");
                loaddataerrormessageid.Attributes.Add("class", newClassValue);
                //Set the error message
                loaddataerrormessageid.InnerText = ErrorMessage;
            }
        }

        private void RefreshUpdateField()
        {
            //Open the add shipment page and clear previous submitted value
            Page.ClientScript.RegisterStartupScript(this.GetType(), "editKey1", "showEditShipmentPanel();", true);
            updateshipmentid.Text = "";
            updatesourceid.Value = "";
            updatedestinationid.Value = "";
            updateitemnameid.Value = "";
            updateitemquantityid.Value = "";
            updateshippingdateid.Value = "";
            updateexpecteddurationid.Value = "";
            updateshipmentstatusid.Value = "";
            //Add the hidden class if the previous submit is error and the hidden class is removed
            loaddataerrormessageid.Attributes.Add("class", "hidden");
            updatevalidationrow.Attributes.Add("class", "hidden");
        }

        public void UpdateShipment(object sender, EventArgs e)
        {
            bool ErrorExists = false;
            string ErrorMessage = "";
            
            string ShipmentIdValue = updateshipmentid.Text;
            string SourceValue = updatesourceid.Value;
            string DestinationValue = updatedestinationid.Value;
            string ItemNameValue = updateitemnameid.Value;
            string ItemQuantityValue = updateitemquantityid.Value;
            string ShippingDateValue = updateshippingdateid.Value;
            string ExpectedDurationValue = updateexpecteddurationid.Value;
            string ShipmentStatusValue = updateshipmentstatusid.Value;

            System.Diagnostics.Debug.WriteLine("CMS Debug: Updating shipment data");
            //Validate ShipmentIdValue
            if (!String.IsNullOrWhiteSpace(ShipmentIdValue))
            {
                int i;
                if (int.TryParse(ShipmentIdValue, out i))
                {
                    ErrorExists = false;
                }
                else
                {
                    ErrorExists = true;
                    ErrorMessage = "The Shipment ID value is not a number, Please input number value.";
                }
            }
            else
            {
                ErrorExists = true;
                ErrorMessage = "The Shipment ID field is empty.";
                //HttpContext.Current.Session["ShipmentErrorMessage"] = "The field is empty";
            }
            //Validate Sourcevalue
            if ((!ErrorExists) && String.IsNullOrWhiteSpace(SourceValue))
            {
                ErrorExists = true;
                ErrorMessage = "The Source value field is empty.";
            }
            //Validate DestinationValue
            if ((!ErrorExists) && String.IsNullOrWhiteSpace(DestinationValue))
            {
                ErrorExists = true;
                ErrorMessage = "The Destination value field is empty.";
            }
            //Validate ItemNameValue
            if ((!ErrorExists) && String.IsNullOrWhiteSpace(ItemNameValue))
            {
                ErrorExists = true;
                ErrorMessage = "The Item Name value field is empty.";
            }
            //Validate ItemQuantityValue
            if ((!ErrorExists) && String.IsNullOrWhiteSpace(ItemQuantityValue))
            {
                ErrorExists = true;
                ErrorMessage = "The Item Quantity value field is empty.";
            }
            //Validate ShippingDateValue
            if ((!ErrorExists) && String.IsNullOrWhiteSpace(ShippingDateValue))
            {
                ErrorExists = true;
                ErrorMessage = "The Shipping Date is not chosen yet.";
            }
            //Validate ExpectedDurationValue
            if ((!ErrorExists) && String.IsNullOrWhiteSpace(ExpectedDurationValue))
            {
                ErrorExists = true;
                ErrorMessage = "The Expected Duration value field is empty.";
            }
            //Validate ShipmentStatusValue
            if ((!ErrorExists) && String.IsNullOrWhiteSpace(ShipmentStatusValue))
            {
                ErrorExists = true;
                ErrorMessage = "The Shipment Status value field is empty.";
            }
            
            //Error message print
            if (ErrorExists)
            {
                //Open the update shipment page
                Page.ClientScript.RegisterStartupScript(this.GetType(), "updateKey1", "showEditShipmentPanel();", true);
                System.Diagnostics.Debug.WriteLine("CMS Debug: There is error, failed to update the shipment data");
                //Remove the hidden class of tr to show the error message
                var newClassValue = updatevalidationrow.Attributes["class"].Replace("hidden", "");
                updatevalidationrow.Attributes.Remove("class");
                updatevalidationrow.Attributes.Add("class", newClassValue);
                //Set the error message
                updatevalidationmessage.InnerText = ErrorMessage;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("CMS Debug: Successfully update the shipment data");
                //Update the shipment activity
                var updatedShipmentActivity = new ShipmentActivity
                {
                    ShipmentID = Int32.Parse(ShipmentIdValue),
                    Source = SourceValue,
                    Destination = DestinationValue,
                    ItemName = ItemNameValue,
                    ItemQuantity = ItemQuantityValue,
                    ShippingDate = Convert.ToDateTime(ShippingDateValue),
                    ExpectedDuration = ExpectedDurationValue,
                    ShipmentStatus = ShipmentStatusValue
                };

                //Update to the database
                var db = new ContainerManagementSystem.Models.ShipmentActivityContext();
                db.Entry(updatedShipmentActivity).State = EntityState.Modified;
                
                //Save the changes
                db.SaveChanges();

                //Reload the page
                Response.Redirect(Request.RawUrl);
            }
        }
        
        public void DeleteShipmentConfirm(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "deleteKey1", "showEditShipmentPanel();", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "deleteKey2", "confirmDeleteShipment();", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "deleteKey3", "enableUpdateField();", true);
        }

        public void DeleteShipment(object sender, EventArgs e)
        {
            string ShipmentIdValue = updateshipmentid.Text;
            DeleteShipmentWithID(ShipmentIdValue);
        }

        public void DeleteShipment2(object sender, EventArgs e)
        {
            HtmlAnchor anchor = (HtmlAnchor) sender;
            DeleteShipmentWithID(anchor.Attributes["data-shipmentid"].ToString());
        }

        public void DeleteShipmentWithID(string ShipmentIdValue)
        {
            System.Diagnostics.Debug.WriteLine("CMS Debug: Deleting the shipment data");
            ShipmentActivity ShipmentActivityData = null;
            IQueryable<ShipmentActivity> query = GetShipmentActivities();
            int shipmentIDinInt = Int32.Parse(ShipmentIdValue);
            query = query.Where(p => p.ShipmentID == shipmentIDinInt);
            if (query.Count() != 0)
            {
                ShipmentActivityData = query.First();
            }

            //Update to the database
            var db = new ContainerManagementSystem.Models.ShipmentActivityContext();
            db.Entry(ShipmentActivityData).State = EntityState.Deleted;

            //Save the changes
            db.SaveChanges();

            //Reload the page
            Response.Redirect(Request.RawUrl);
        }

        public IQueryable<ShipmentActivity> GetShipmentActivities()
        {
            var db = new ContainerManagementSystem.Models.ShipmentActivityContext();
            IQueryable<ShipmentActivity> query = db.ShipmentActivities;
            System.Diagnostics.Debug.WriteLine("CMS Debug: Querying Shipment Activities");
            return query;
        }

        public int GetLatestShipmentID()
        {
            var db = new ContainerManagementSystem.Models.ShipmentActivityContext();
            IQueryable<ShipmentActivity> query = db.ShipmentActivities;
            return query.Count() + 1;
        }

    }
}