console.log('testing')


$(document).ready(
    function () {
        $('#submit').click(function () {
            console.log('Test');
       
             
            $.ajax({
                url: 'https://localhost:44301/Users/authenticate',
                dataType: 'json',
                type: 'post',
                async: false,
                contentType: 'application/json',
                data: JSON.stringify({
                    username: $('#username').val(),
                    password: $('#password').val()
                    
                }),
               
                success: function (data, textStatus, jQxhr) {
                    debugger;
                    var response = data
                    var token = data.token;
                    localStorage.setItem("manager", ($('#username').val()));
                    alert('Login Successfull');
                    EmployeeList()
                    console.log(token)
                    function EmployeeList() {
                        $.ajax({
                            url: "https://localhost:44378/api/Employees",
                            type: 'GET',
                            
                            //passing token to get employee details
                            headers: {"Authorization": 'Bearer ' + token },
                            success: function (data, textStatus, jQxhr) {
                            debugger;                              
                            console.log(JSON.stringify(data));
                            if (response.role == "Manager") {
                                window.location.replace('https://localhost:44378/Home/ManagerHome')
                            }
                            if (response.role == "WFM_Manger") {
                                window.location.replace('https://localhost:44378/Home/WfmManagerHome')
                            } 
                            },
                           
                            error: function (jqXhr, textStatus, errorThrown) {
                                console.log('error')
                            }


                        });
 
                    }
                   
                    
                    
                   
                    
                },
                error: function (jqXhr, textStatus, errorThrown) {             
                    $("#message").html("<b>You have entered an invalid username or password</b>");
                   

                }
            }); 


        })

    }
)


