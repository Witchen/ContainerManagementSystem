<%@ Page Title="Shipment Actions" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShipmentView.aspx.cs" Inherits="ContainerManagementSystem.View.ShipmentView" %>

<asp:Content ID="ShipmentContent" ContentPlaceHolderID="MainContent" runat="server">
    <%--CSS for DatePicker--%>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css" type="text/css" />
    <div class="container" style="margin-top:20px">
        <div class="row">
            <div class="col-md-12 dropdown">
                <button class="btn btn-primary dropdown-toggle" type="button" data-toggle="dropdown" style="width:100%">
                    Shipment Activities
                    <span class="caret"></span>
                </button>
                <ul class="dropdown-menu">
                    <li><a id="ListShipmentID">List Shipment Activities</a></li>
                    <li><a id="AddShipmentID">Add Shipment Activities</a></li>
                    <li><a id="UpdateShipmentID">Update Shipment Actitivites</a></li>
                </ul>
            </div>
        </div>
        <div style="margin-top:20px">
            <div id="ListShipmentPanel" class="panel panel-default">
                <div class="panel-heading">Shipment Actitvites List</div>
                <div class="panel-body">
                    <table class="table table-striped">
                        <thead>
                            <tr>
                                <th>Shipment ID</th>
                                <th>Source</th>
                                <th>Destination</th>
                                <th>Item Name</th>
                                <th>Item Quantity</th>
                                <th>Shipping Date</th>
                                <th>Expected Duration</th>
                                <th>Shipment Status</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:ListView ID="ListView" ItemType="ContainerManagementSystem.Models.ShipmentActivity" runat="server" SelectMethod="GetShipmentActivities">
                                <ItemTemplate>
                                    <tr>
                                        <td id="shipmentid<%#: Item.ShipmentID %>"><%#: Item.ShipmentID %></td>
                                        <td><%#: Item.Source %></td>
                                        <td><%#: Item.Destination %></td>
                                        <td><%#: Item.ItemName %></td>
                                        <td><%#: Item.ItemQuantity %></td>
                                        <td><%#: Item.ShippingDate.ToString("dd-MMM-yyyy") %></td>
                                        <td><%#: Item.ExpectedDuration %></td>
                                        <td><%#: Item.ShipmentStatus %></td>
                                        <td>
                                            <a id="update" data-shipmentid="<%#: Item.ShipmentID %>" runat="server" onserverclick="LoadShipmentData2" href="#"><span class="glyphicon glyphicon-edit"></span> Edit</a>
                                            <a id="delete" data-shipmentid="<%#: Item.ShipmentID %>" runat="server" onserverclick="DeleteShipment2" href="#"><span class="glyphicon glyphicon-remove"></span> Delete</a>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </tbody>
                    </table>
                </div>
            </div>
            <div id="AddShipmentPanel" class="panel panel-default hidden" style="width:500px; margin:auto">
                <div class="panel-heading text-center">Add New Shipment Actitvites</div>
                <div class="panel-body">
                    <table class="table table-striped" style="margin-bottom: 0px">
                        <tbody>
                            <%--<tr>
                                <td>Shipment ID: </td>
                                <td align="right"><asp:TextBox id="shipmentidtextbox" runat="server" class="form-control" name="shipmentname" ReadOnly="true"/></td>
                            </tr>--%>
                            <tr>
                                <td>Source: </td>
                                <td align="right"><input type="text" class="form-control" id="sourceid" runat="server"></td>
                            </tr>
                            <tr>
                                <td>Destination: </td>
                                <td align="right"><input type="text" class="form-control" id="destinationid" runat="server"></td>
                            </tr>
                            <tr>
                                <td>Item Name: </td>
                                <td align="right"><input type="text" class="form-control" id="itemnameid" runat="server"></td>
                            </tr>
                            <tr>
                                <td>Item Quantity: </td>
                                <td align="right"><input type="text" class="form-control" id="itemquantityid" runat="server"></td>
                            </tr>
                            <tr>
                                <td>Shipping Date: </td>
                                <td align="right">
                                    <div class="input-group date" data-provide="datepicker">
                                        <input type="text" class="form-control" id="shippingdateid" runat="server">
                                        <div class="input-group-addon">
                                            <span class="glyphicon glyphicon-th"></span>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>Expected Duration: </td>
                                <td align="right"><input type="text" class="form-control" id="expecteddurationid" runat="server"></td>
                            </tr>
                            <tr>
                                <td>Shipment Status: </td>
                                <td align="right"><input type="text" class="form-control" id="shipmentstatusid" runat="server"></td>
                            </tr>
                            <tr id="validationrow" runat="server" class="hidden">
                                <td colspan="2">
                                    <p id="validationmessage" runat="server" style="color:red;">Error</p>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color:white;" ></td>
                                <td align="right" style="background-color:white;">
                                    <%--<asp:Button ID="addButton" class="btn btn-primary" OnClick="AddNewShipment" runat="server" text="Add New"/>--%>
                                    <button type="button" class="btn btn-primary" runat="server" onserverclick="AddNewShipment">Add New</button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div id="UpdateShipmentPanel" class="row hidden">
                <div class="col-md-1">
                </div>
                <div class="col-md-4">
                    <div id="LoadShipmentDataPanel" class="panel panel-default" style="width:300px; margin:auto">
                        <div class="panel-heading text-center">Load Shipment Data</div>
                        <div class="panel-body">
                            <input type="text" class="form-control" id="loadshipmentid" runat="server" name="loadshipmentname" style="margin-bottom:15px">
                            <p class="hidden" id="loaddataerrormessageid" runat="server" style="margin-bottom:15px; color:red;">Error</p>
                            <button type="button" class="btn btn-primary" runat="server" onserverclick="LoadShipmentData">Load Shipment Data</button>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div id="ShipmentDataPanel" class="panel panel-default" style="width:500px; margin:auto">
                        <div class="panel-heading text-center">Update Shipment Actitvites</div>
                        <div class="panel-body">
                            <table class="table table-striped">
                                <tbody>
                                    <tr>
                                        <td>Shipment ID: </td>
                                        <td align="right"><asp:TextBox id="updateshipmentid" runat="server" class="form-control" name="shipmentname" readonly="true"/></td>
                                    </tr>
                                    <tr>
                                        <td>Source: </td>
                                        <td align="right"><input type="text" ClientIDMode="Static" class="form-control" id="updatesourceid" runat="server" name="shipmentsourcename" readonly="readonly"></td>
                                    </tr>
                                    <tr>
                                        <td>Destination: </td>
                                        <td align="right"><input type="text" ClientIDMode="Static"  class="form-control" id="updatedestinationid" runat="server" readonly="readonly"></td>
                                    </tr>
                                    <tr>
                                        <td>Item Name: </td>
                                        <td align="right"><input type="text" ClientIDMode="Static"  class="form-control" id="updateitemnameid" runat="server" readonly="readonly"></td>
                                    </tr>
                                    <tr>
                                        <td>Item Quantity: </td>
                                        <td align="right"><input type="text" ClientIDMode="Static"  class="form-control" id="updateitemquantityid" runat="server" readonly="readonly"></td>
                                    </tr>
                                        <tr>
                                        <td>Shipping Date: </td>
                                        <td align="right">
                                            <div class="input-group date" data-provide="datepicker">
                                                <input type="text" ClientIDMode="Static" class="form-control" id="updateshippingdateid" runat="server" readonly="readonly">
                                                <div class="input-group-addon">
                                                    <span class="glyphicon glyphicon-th"></span>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Expected Duration: </td>
                                        <td align="right"><input type="text" ClientIDMode="Static" class="form-control" id="updateexpecteddurationid" runat="server" readonly="readonly"></td>
                                    </tr>
                                    <tr>
                                        <td>Shipment Status: </td>
                                        <td align="right"><input type="text" ClientIDMode="Static" class="form-control" id="updateshipmentstatusid" runat="server" readonly="readonly"></td>
                                    </tr>
                                    <tr id="updatevalidationrow" runat="server" class="hidden">
                                        <td colspan="2">
                                            <p id="updatevalidationmessage" runat="server" style="color:red;">Error</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background-color:white;" ></td>
                                        <td align="right" style="background-color:white;padding-right:14px">
                                            <button type="button" class="btn btn-primary" runat="server" id="updateshipmentbutton" ClientIDMode="Static" disabled="disabled" onserverclick="UpdateShipment">Update</button>
                                            <button type="button" class="btn btn-primary" runat="server" id="deleteshipmentbutton" ClientIDMode="Static" disabled="disabled" onserverclick="DeleteShipmentConfirm">Delete</button>
                                            <button type="button" class="btn btn-primary hidden" runat="server" ClientIDMode="Static" id="deleteconfimerbutton" disabled="disabled" onserverclick="DeleteShipment"></button>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                </div>
            </div>
        </div>
    </div>

</asp:Content>
