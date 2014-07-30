


//Check Auth
$(function () {

    $.ajax({
        type: "POST",
        url: "../TasksWebService.asmx/CheckAuth",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            var data = response.d;
            //alert(data);

            if (data == "0") {
                window.location = "UserLogin.html";
            }
            else {

                gettasks();
                GetEmployeeNames();
            }

        },
        failure: function (msg) {
            alert(msg);
        }
    });

});





$(".toggle2").toggle();

$('body').off('.data-api');
$('body').off('.datepicker.data-api')

$(function () {
    $("#duedate").datepicker({ dateFormat: 'mm-dd-yy' }).on('changeDate', function () { $("#duedate").datepicker('hide') });
});
$(function () {
    $("#completiondate").datepicker({ dateFormat: 'mm-dd-yy' }).on('changeDate', function () { $("#completiondate").datepicker('hide') });
});
$(function () {
    $("#finalapprovaldate").datepicker({ dateFormat: 'mm-dd-yy' }).on('changeDate', function () { $("#finalapprovaldate").datepicker('hide') });
});

//Reset Modal Form
function formreset() {
    document.getElementById("addtask").reset();
    $("#modaltitle").html("Add Task");
    $('#addbutton').text("Add Task").button("refresh");
    $("#submittype").val("add");
}

//Get Employee Names for Select Box
function GetEmployeeNames() {

    $.ajax({
        type: "POST",
        url: "../TasksWebService.asmx/GetEmployeeNames",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {


            //alert(response);
            var data = response.d;

            var options = $("#assignedto");
            $.each(data, function () {
                options.append($("<option />").val(this.id_users).text(this.lname + ", " + this.fname));
            });



        },
        failure: function (msg) {
            alert(msg);
        }
    });

}






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


            var datahtml = "<tr><th>Priority</th><th>Description</th><th>Assigned To</th><th>Created By</th><th>Due Date</th><th>Created Date</th><th></th><th></th></tr>";

            jQuery.each(data, function (i, val) {




                datahtml += "<tr>";
                datahtml += "<td>" + val.priority + "</td>";
                datahtml += "<td>" + val.description + "</td>";
                datahtml += "<td>" + val.assignedto + "</td>";
                datahtml += "<td>" + val.createdby + "</td>";
                datahtml += "<td>" + val.duedate + "</td>";
                datahtml += "<td>" + val.createddate + "</td>";
                // datahtml += "<td>" + val.completeddate + "</td>";
                // datahtml += "<td>" + val.finalapprovaldate + "</td>";
                datahtml += "<td style='cursor:pointer' onclick='modaltoggle(); setedit(" + val.id_tasks + ")'><img src='../images/editblue.png' height='20px' width='20px'></td>";
                datahtml += "<td style='cursor:pointer' onclick='deletetask(" + val.id_tasks + ")'><img src='../images/delete.png' height='16px' width='16px'></td>";
                datahtml += "</tr>";

            });


            $("#dataresults").html(datahtml);
            document.getElementById('saving').className = 'hide';
            



        },
        failure: function (msg) {
            alert(msg);
        }
    });

}



function addtask() {

    

    if (document.getElementById("description").value == "") {
        alert("Task Description Required");
    }
    else if (document.getElementById("duedate").value == "") {
        alert("Task Due Date Required");
    }
    else if (document.getElementById("assignedto").value == "") {
        alert("Task Assignment Required");
    }
    else if (document.getElementById("priority").value == "") {
        alert("Task Priority Required");
    }
    else {

        document.getElementById('saving').className = 'show';

        $.ajax({
            type: 'POST',
            url: '../TasksWebService.asmx/AddTask',
            data: $('#addtask').serialize(),

            dataType: "json",

            success: function () {
                //alert("yes");

                $('#bodyModal4').toggle("blinds");

                $("#bodyModal5").toggle("blinds");


                setTimeout(function () {
                    $('#addtaskmodal').modal('toggle');

                    $('#bodyModal4').toggle("blinds");
                    $("#bodyModal5").toggle("blinds");
                    document.getElementById("addtask").reset();
                    // window.top.location.reload();
                    gettasks();
                    $("#modaltitle").html("Add Task");
                    $('#addbutton').text('Add Task').button("refresh");
                    $("#submittype").val("add");
                    $("#id_tasks").val("");


                    document.getElementById('saving').className = 'hide';
                }, 100);



            },
            failure: function (msg) {
                alert(msg)
            }
        });
    }


}



function setedit(id_tasks) {

    document.getElementById('saving').className = 'show';

    $("#modaltitle").html("Edit Task");
    $('#addbutton').text('Save Changes').button("refresh");
    $("#submittype").val("edit");
    $("#id_tasks").val(id_tasks);

    $.ajax({
        type: "POST",
        url: "../TasksWebService.asmx/GetTaskInfo",
        data: "{'recid':'" + id_tasks + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            //alert(response);
            var data = response.d;


            jQuery.each(data, function (i, val) {


                document.getElementById("description").value = val.description;
                document.getElementById("assignedto").value = val.assignedtoid;
                document.getElementById("priority").value = val.priority;

                if (val.duedate != "") {
                    var ddate = new Date(val.duedate);
                    var dday = ddate.getDate();
                    var dmonth = ddate.getMonth() + 1;
                    var dyear = ddate.getFullYear();
                    var duedate = dmonth + "/" + dday + "/" + dyear;
                    document.getElementById("duedate").value = duedate;
                }

                if (val.completeddate != "") {
                    var cdate = new Date(val.completeddate);
                    var cday = cdate.getDate();
                    var cmonth = cdate.getMonth() + 1;
                    var cyear = cdate.getFullYear();
                    cdate = cmonth + "/" + cday + "/" + cyear;
                    document.getElementById("completiondate").value = cdate;
                }

                if (val.finalapprovaldate != "") {
                    var fdate = new Date(val.finalapprovaldate);
                    var fday = fdate.getDate();
                    var fmonth = fdate.getMonth() + 1;
                    var fyear = fdate.getFullYear();
                    fdate = fmonth + "/" + fday + "/" + fyear;
                    document.getElementById("finalapprovaldate").value = fdate;
                }


            });



            document.getElementById('saving').className = 'hide';




        },
        failure: function (msg) {
            alert(msg);
        }
    });

}


function deletetask(recid) {

    var r = confirm("Are you sure you want to delete this task?");
    if (r == true) {
        document.getElementById('saving').className = 'show';
        //alert(recid);
        $.ajax({
            type: "POST",
            url: "../TasksWebService.asmx/DeleteTask",
            data: "{'recid':'" + recid + "','name':'james'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                var data = response.d;
                //alert(data);
                gettasks();

            },
            failure: function (msg) {
                alert(msg);
            }
        });
    }


}

function modaltoggle() {
    $('#addtaskmodal').modal('toggle');
    //document.getElementById('type').value = 'add';
}