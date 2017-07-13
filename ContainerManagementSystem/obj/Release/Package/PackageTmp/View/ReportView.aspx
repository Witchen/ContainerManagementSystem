<%@ Page Title="Shipment Reports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportView.aspx.cs" Inherits="ContainerManagementSystem.View.ReportView" %>

<asp:Content ID="ShipmentContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.6.0/Chart.bundle.js"></script>
	<script src="http://www.chartjs.org/samples/latest/utils.js"></script>
    <div class="container" style="margin-top:20px">
        <div class="row">
			<div class="col-sm-8 col-md-offset-1">
				<canvas id="SourceChartID" width="400" height="200"></canvas>
                
                <%--Set the data column--%>
                <script>
                    var fullSourceData = []
                    var sourceLabel = [];
                    var fullValueData = []
                    var dataValue = [];
                </script>
                <asp:ListView ID="ListViewForSourceName" ItemType="ContainerManagementSystem.Models.ShipmentActivity" runat="server" SelectMethod="GetFilteredSource">
                    <ItemTemplate>
                        <script>
                            fullSourceData.push("<%#: Item.Source %>");
                            sourceLabel.push("<%#: Item.Source %>");
                        </script>
                    </ItemTemplate>
                </asp:ListView>
                <asp:ListView ID="ListViewForSourceCount" ItemType="System.Int32" runat="server" SelectMethod="GetFilteredSourceNumber">
                    <ItemTemplate>
                        <script>
                            fullValueData.push("<%#: Item %>");
                            dataValue.push("<%#: Item %>");
                        </script>
                    </ItemTemplate>
                </asp:ListView>

				<script>
                    var titleLabel = "Number of Shipment based on Source"
                    //var sourceLabel = ["Red", "Blue", "Yellow", "Green", "Purple", "Orange"]
                    //var dataValue = [12, 19, 3, 5, 2, 5, 0 ,10]
                    var horizontalBarChartData = {
                        labels: sourceLabel,
                        datasets: [{
                            label: titleLabel,
                            data: dataValue,
                            backgroundColor: [
                                'rgba(255, 99, 132, 0.2)',
                                'rgba(54, 162, 235, 0.2)',
                                'rgba(255, 206, 86, 0.2)',
                                'rgba(75, 192, 192, 0.2)',
                                'rgba(153, 102, 255, 0.2)',
                                'rgba(255, 159, 64, 0.2)',
                                'rgba(51, 255, 255, 0.2)',
                                'rgba(0, 153, 51, 0.2)',
                                'rgb(51, 102, 255, 0.2)',
                                'rgb(255, 51, 133, 0.2)'
                            ],
                            borderColor: [
                                'rgba(255,99,132,1)',
                                'rgba(54, 162, 235, 1)',
                                'rgba(255, 206, 86, 1)',
                                'rgba(75, 192, 192, 1)',
                                'rgba(153, 102, 255, 1)',
                                'rgba(255, 159, 64, 1)',
                                'rgba(51, 255, 255, 1)',
                                'rgba(0, 153, 51, 1)',
                                'rgb(51, 102, 255, 1)',
                                'rgb(255, 51, 133, 1)'
                            ],
                            borderWidth: 1
                        }]
                    }
                    var ctx = document.getElementById("SourceChartID").getContext('2d');
                    //var myChart = new Chart(ctx, {
                    window.myChart = new Chart(ctx, {
					    type: 'bar',
                        data: horizontalBarChartData,
					    options: {
						    scales: {
							    yAxes: [{
								    ticks: {
									    beginAtZero:true
								    }
							    }]
						    }
					    }
				    });
				</script>
			</div>
            <div class="col-sm-2" style="padding-left:10px; padding-top:50px">
                <button class="btn btn-primary dropdown-toggle" type="button" data-toggle="dropdown" style="width:180px">
                    Choose Source
                    <span class="caret"></span>
                </button>
                <ul class="dropdown-menu" style="padding-left: 10px;margin-left: 10px;"> <%--style="min-width:0px"--%>
                    <% System.Diagnostics.Debug.WriteLine("CMS Debug: 111111111111111111111111111111111111111111111111"); %>
                    <asp:ListView ID="SourceListViewID" ItemType="ContainerManagementSystem.Models.ShipmentActivity" runat="server" SelectMethod="GetFilteredSource">
                        <ItemTemplate>
                            <li>
                                <div class="checkbox" id="checkboxsource" data-go="go">
                                    <label data-up="upper"><input type="checkbox" checked="checked" id="<%#: Item.Source %>ID" value="<%#: Item.Source %>"><p><%#: Item.Source %></p></label>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </ul>
            </div>
		</div>
    </div>
</asp:Content>