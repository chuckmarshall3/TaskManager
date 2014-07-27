document.getElementById('saving').className = 'hide';

function login() {
    document.getElementById('saving').className = 'show';

    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;


    $.ajax({
        type: "POST",
        url: "../TasksWebService.asmx/LoginUser",
        data: "{'username':'" + username + "','password':'" + password + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {


            var data = response.d;
            //alert(data);

            if (data == "1") {
                window.location = "TaskMain.html";
            }
            else {
                alert("Invalid Username/Password, please try again.")
            }

            document.getElementById('saving').className = 'hide';


        },
        failure: function (msg) {
            alert(msg);
        }
    });
}
