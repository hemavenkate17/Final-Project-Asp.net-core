

$.get("https://localhost:44301/api/Softlocks", function (data, status) {

    console.log(data);
    let code = "";
    for (let x in data) {
        var id = data[x].lockid;
        
        code += "<tr>"
        code += "<td>" + data[x].employee_id + "</td>"
        code += "<td>" + localStorage.getItem("manager"); + "</td>"
        code += "<td>" + data[x].reqdate + "</td>"
        code += "<td>" + data[x].manager + "</td>"
        code += "<td> <button style='background-color:blue' class='openButton' onclick='Send("+id+")'>View Details</button></td></tr>"

    }
    $('#tbody').html(code)
})


function closeForm() {
    document.getElementById("popupForm").style.display = "none";
    document.getElementById("div").style.display = "block";

}





function Send(lockid) {
    document.getElementById("popupForm").style.display = "block";
    document.getElementById("div").style.display = "none";

    
    console.log('send test')
    $.ajax({
        url: 'https://localhost:44301/api/softlocks/'+ lockid,
        dataType: 'json',
        type: 'get',
        async: false,
        contentType: 'application/json',

        success: function (data, textstatus, jqxhr) {
            
            var emp = data.employee_id

            $("#emp").val(emp);
            $("#requestee").val(localStorage.getItem("manager"));
            $("#Empmanager").val(data.manager);
            $("#Reqmessage").val(data.requestmessage);
            
           
           
            $.ajax({
                url: 'https://localhost:44301/api/softlocks/' + lockid,
                dataType: 'json',
                type: 'put',
                async: false,
                contentType: 'application/json',
                data: JSON.stringify({
                    employee_id: data.employee_id,
                    lockid: lockid,
                    status: $('#province').val()
                }),

                success: function (data, textStatus, jQxhr) {
                    alert('Your Request updated');
                    $.ajax({
                        url: 'https://localhost:44301/api/Employees/' + emp,
                        dataType: 'json',
                        type: 'put',
                        async: false,
                        contentType: 'application/json',
                        data: JSON.stringify({
                            employee_id: $('#emp').val(),
                            lockstatus: "Locked"
                        }),

                        success: function (data, textStatus, jQxhr) {
                            alert('Employee table updated');
                        },

                        error: function (jqXhr, textStatus, errorThrown) {
                            alert('Employee table not updated')




                        }
                    });
                },

                error: function (jqXhr, textStatus, errorThrown) {
                    alert('Sorry!, Your request not updated')




                }
                
            });





        },
        error: function (jqxhr, textstatus, errorthrown) {
            console.log('status update failed')
           
            alert("Unable to get softlock data")
        }
    })



}


