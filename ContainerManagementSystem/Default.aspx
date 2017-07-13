<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ContainerManagementSystem._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron" style="background: url('Image/MaerskPhoto.jpg') no-repeat center bottom;background-size: cover;color: white; height:350px">
        <h1 style="padding-top:125px">Maersk Line</h1>
        <p class="lead">Container Management System</p>
    </div>

    <div class="container row">
        <div class="col-md-3 panel panel-default" style="margin-left:66px; padding-left:0px; padding-right:0px; margin-bottom:0px">
            <div class="panel-heading">Total of Shipments - Overall</div>
            <div class="panel-body">
                <p runat="server" ClientIDMode="static" id="totalofshipmentoverallid">Shipments</p>
            </div>
        </div>
        <div class="col-md-3 col-md-offset-1 panel panel-default" style="padding-left:0px; padding-right:0px; margin-bottom:0px">
            <div class="panel-heading">Total of Shipments - This Month</div>
            <div class="panel-body">
                <p runat="server" ClientIDMode="static" id="totalofshipmentmonthid">Shipments</p>
            </div>
        </div>
        <div class="col-md-3 col-md-offset-1 panel panel-default" style="padding-left:0px; padding-right:0px; margin-bottom:0px">
            <div class="panel-heading">Total of Shipments - Today</div>
            <div class="panel-body">
                <p runat="server" ClientIDMode="static" id="totalofshipmenttodayid">Shipments</p>
            </div>
        </div>
    </div>

</asp:Content>
