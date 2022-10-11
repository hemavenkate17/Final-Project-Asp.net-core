console.log('script test')

$.get("https://localhost:44301/api/Employees", function (data, status) {
   
    console.log(data);
    let code = "";
    for (let x in data) {
        var id = data[x].employee_id;
        code += "<tr>"
        code += "<td>" + data[x].employee_id + "</td>"
        code += "<td>" + data[x].name + "</td>"
        code += "<td>" + data[x].manager + "</td>"
        code += "<td>" + data[x].email + "</td>"
        code += "<td style='border:solid; display:inline-block; border-color:brown ; background-color:greenyellow'>"
        for (let y in data[x].skills)
            code += data[x].skills[y] + ","+" "
        code += "</td></td>"
       
        code += "<td> <button style='background-color:blue' class='openButton' onclick='openForm("+id+")'><i class='fa fa-lock'></i> Request</button></td></tr>"
           
    }
    $('#tbody').html(code)
})

function openForm(id) {
    document.getElementById("popupForm").style.display = "block";
    document.getElementById("div").style.display = "none";
    document.getElementById("emp").innerText = +id;

}
function closeForm() {
    document.getElementById("popupForm").style.display = "none";
    document.getElementById("div").style.display = "block";
}





function Requestlock() {

    $.ajax({
        url: 'https://localhost:44301/api/softlocks',
        dataType: 'json',
        type: 'post',
        async: false,
        contentType: 'application/json',
        data: JSON.stringify({
            employee_id: document.getElementById("emp").innerText,
            manager: document.getElementById("manager").innerText,
            status: Requested,
            requestmessage: $('#message').val()
        }),


        success: function (data, textstatus, jqxhr) {
            console.log('softlock success')
            alert("Your Employee Request Submitted Successfully")
         
            $.ajax({
                url: 'https://localhost:44301/api/Employees/' + $('#emp').val(),
                dataType: 'json',
                type: 'put',
                async: false,
                contentType: 'application/json',
                data: JSON.stringify({
                    employee_id: $('#emp').val(),
                    manager: $('#manager').val(),
                    status: "waiting",
                    lockstatus: "Request waiting"
                }),

                success: function (data, textStatus, jQxhr) {
                    alert('Employee table updated');
                },

                error: function (jqXhr, textStatus, errorThrown) {
                    alert('Employee table not updated')




                }
            });





        },
        error: function (jqxhr, textstatus, errorthrown) {
            console.log('softlock failed')
            alert("Your Employee Request is not Submitted")
        }
    })



}


