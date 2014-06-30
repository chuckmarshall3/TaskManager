<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TaskMain.aspx.cs" Inherits="Views_TaskMain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<style type="text/css">

    table, th, td
    {
    font-size: 12px;
    padding:0px 0px 0px 0px;
    }

</style>


    <h3 style="cursor:pointer" onclick="gettasks()">Current Tasks</h3>
    <br />
    <div ><table class="table table-condensed table-responsive table-hover" id="dataresults" style=" overflow: auto; display:block;"></table></div>
    <script src="../includes/jquery/jquery-1.8.2.js"></script>
    
    
    <script>
        window.onload = gettasks;

        function gettasks() {

            $.ajax({
                type: "POST",
                url: "../TasksWebService.asmx/GetUserTasks",
                data: "",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    //alert(response);
                    var data = response.d;
               
                
                    var datahtml = "<tr><th>Priority</th><th>Description</th><th>Created By</th><th>Created Date</th><th>Completed Date</th><th>Final Approval</th><th></th></tr>";
                    
                    jQuery.each(data, function(i, val) {
                     
                        datahtml += "<tr>";
                        datahtml += "<td>" + val.priority + "</td>";
                        datahtml += "<td>" + val.description + "</td>";
                        datahtml += "<td>" + val.createdby + "</td>";
                        datahtml += "<td>" + val.createddate + "</td>";
                        datahtml += "<td>" + val.completeddate + "</td>";
                        datahtml += "<td>" + val.finalapprovaldate + "</td>";
                        
                        datahtml += "<td style='cursor:pointer' onclick='deletetask(" + val.priority + ")'><img src='../images/delete.png' height='16px' width='16px'></td>";
                        datahtml += "</tr>";

                    });

                    
                    $("#dataresults").html(datahtml);
                    
                      
                    

                },
                failure: function (msg) {
                    alert(msg);
                }
            });
            
        }
        function addtask() {

            $.ajax({
                type: "POST",
                url: "../TasksWebService.asmx/AddTask",
                data: "",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    alert(response);





                },
                failure: function (msg) {
                    alert(msg);
                }
            });


        }
        function edittask(recid) {

            $.ajax({
                type: "POST",
                url: "../TasksWebService.asmx/EditTask",
                data: "",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    alert(response);





                },
                failure: function (msg) {
                    alert(msg);
                }
            });


        }

        function deletetask(recid) {

            alert(recid);
            $.ajax({
                type: "POST",
                url: "../TasksWebService.asmx/DeleteTask",
                data: "",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    alert(response);





                },
                failure: function (msg) {
                    alert(msg);
                }
            });


        }



    </script>

</asp:Content>

