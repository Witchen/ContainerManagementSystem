$(document).ready(function () {
    //Script to change active link in navbar
    var url = window.location;
    $('#navbaractiveid').find('.active').removeClass('active');
    $('#navbaractiveid li a').each(changeNavbar);
    function changeNavbar() {
        if (this.href == url) {
            //alert('changing active nav');
            //alert(this.id)
            //alert($(this).attr('id'))
            //alert($(this).parent().attr('id'))
            $(this).parent().addClass('active');
        }
    }

    $("#ListShipmentID").click(showListShipmentPanel);
    $("#AddShipmentID").click(showAddShipmentPanel);
    $("#UpdateShipmentID").click(showEditShipmentPanel);
    $('.datepicker').datepicker();

    //Prevent dropdown auto close in report tab
    $('.dropdown-menu').click(function (e) {
        e.stopPropagation();
    });

    //Get the value of checkbox in source report
    $('#checkboxsource label input').click(printValue);
});

//Script for Shipment Page
function showListShipmentPanel() {
    //alert("listShipment");
    $("#ListShipmentPanel").removeClass("hidden");
    $("#AddShipmentPanel").addClass("hidden");
    $("#UpdateShipmentPanel").addClass("hidden");
}

function showAddShipmentPanel() {
    //alert("addShipment");
    $("#ListShipmentPanel").addClass("hidden");
    $("#AddShipmentPanel").removeClass("hidden");
    $("#UpdateShipmentPanel").addClass("hidden");
}

function showEditShipmentPanel() {
    //alert("editShipment");
    $("#ListShipmentPanel").addClass("hidden");
    $("#AddShipmentPanel").addClass("hidden");
    $("#UpdateShipmentPanel").removeClass("hidden");
}

function test() {
    alert("editShipment");
}

function disableUpdateField() {
    $("#updatesourceid").attr("readonly", "readonly");
    $("#updatedestinationid").attr("readonly", "readonly");
    $("#updateitemnameid").attr("readonly", "readonly");
    $("#updateitemquantityid").attr("readonly", "readonly");
    $("#updateshippingdateid").attr("readonly", "readonly");
    $("#updateexpecteddurationeid").attr("readonly", "readonly");
    $("#updateshipmentstatus").attr("readonly", "readonly");    
    $("#updateshipmentbutton").attr("disabled", "disabled");
    $("#deleteshipmentbutton").attr("disabled", "disabled");
}

function enableUpdateField() {
    $("#updatesourceid").removeAttr("readonly");
    $("#updatedestinationid").removeAttr("readonly");
    $("#updateitemnameid").removeAttr("readonly");
    $("#updateitemquantityid").removeAttr("readonly");
    $("#updateshippingdateid").removeAttr("readonly");
    $("#updateexpecteddurationid").removeAttr("readonly");
    $("#updateshipmentstatusid").removeAttr("readonly"); 
    $("#updateshipmentbutton").removeAttr("disabled"); 
    $("#deleteshipmentbutton").removeAttr("disabled"); 
}

function confirmDeleteShipment() {
    if (confirm('Are you sure to delete this shipment data?')) {
        alert("The shipment activity data is deleted.");
        //$('#deleteconfimerbutton').trigger('click');
        $("#deleteconfimerbutton").click();
    } else {
        alert("Delete cancelled");
    }
}

//Function for Report Page
function printValue() {
    //alert('print values');
    //alert("First method: " + $(this).val());
    //alert("Second method: " + $(this).attr("value"));
    //alert($('#checkboxsource label input').attr("value"));
    //var index = $(this).index();
    //var text = $(this).text();
    var val = $(this).val();
    var indexReducer = fullSourceData.length - horizontalBarChartData.labels.length;
    //alert('Index is: ' + index + ' and text is ' + text + ' and val is ' + val);
    for (i = 0; i < fullSourceData.length; i++) {
        if (val == fullSourceData[i]) {
            //alert("The same value is " + val + " and " + fullSourceData[i] + " with index: " + (i-indexReducer));
            //alert("Attribute of checked is " + $(this).attr("checked"));
            var attr = $(this).attr('checked');
            // For some browsers, `attr` is undefined; for others,
            // `attr` is false.  Check for both.
            if (typeof attr !== typeof undefined && attr !== false) {
                //alert('It is checked');
                $(this).removeAttr("checked");
                if ((i - indexReducer) < 0) {
                    removeData(i);
                } else {
                    removeData(i - indexReducer);
                }
            }
            else {
                //alert('It is not checked');
                $(this).attr("checked", "checked");
                addData(i, val, fullValueData[i]);
            }
        }
    }
}

function removeData(index) {
    //alert("index = " + index);
    horizontalBarChartData.labels.splice(index, 1);
    horizontalBarChartData.datasets[0].data.splice(index, 1);
    window.myChart.update();
}

function addData(index, sourcename, amountofshipment) {
    //alert("index = " + index);
    //horizontalBarChartData.labels.push(sourcename);
    horizontalBarChartData.labels.splice(index, 0, sourcename);
    horizontalBarChartData.datasets[0].data.splice(index, 0, amountofshipment);
    window.myChart.update();
}