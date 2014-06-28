<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TaskMain.aspx.cs" Inherits="Views_TaskMain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<style type="text/css">
table, th, td
{
font-size: 12px;
padding:0px 0px 0px 0px;

}

</style>
    <h3 style="cursor:pointer" onclick="gettasks()">Tasks Main</h3>
    <br />
    <div ><table class="table table-bordered table-condensed table-responsive table-hover" id="dataresults" style=" overflow: auto; display:block;"></table></div>
    <script src="../includes/jquery/jquery-1.8.2.js"></script>
    
    
    <script>

        function gettasks() {

            $.ajax({
                type: "POST",
                url: "../TasksWebService.asmx/GetTasks",
                data: "",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    //alert(response);
                    var data = response.d;
               
                
                    var datahtml = "<tr><th>ID</th><th>Description</th><th>Created By</th><th>Created Date</th><th>Completed Date</th><th>Final Approval</th><th>Priority</th></tr>";
                    
                    jQuery.each(data, function(i, val) {
                     
                        datahtml += "<tr>";
                        datahtml += "<td>" + val.id_tasks + "</td>";
                        datahtml += "<td>" + val.description + "</td>";
                        datahtml += "<td>" + val.createdby + "</td>";
                        datahtml += "<td>" + val.createddate + "</td>";
                        datahtml += "<td>" + val.completeddate + "</td>";
                        datahtml += "<td>" + val.finalapprovaldate + "</td>";
                        datahtml += "<td>" + val.priority + "</td>";
                        datahtml += "</tr>";

                    });

                    
                    $("#dataresults").html(datahtml);
                    
                      
                    

                },
                failure: function (msg) {
                    alert(msg);
                }
            });
            
        }



    </script>

</asp:Content>

