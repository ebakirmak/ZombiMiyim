



function ShowModal(header,body)
{
    $("#modalHeader").html(header);
    $("#modalBody").html(body);
   
    $('#modal').modal('show');

}

jQuery(document).ready(function ($) {
    $('#tabs').tab();
});


function SingUp() {

    var Mail = $("#txtRegMail").val();
    var Name = $("#txtRegName").val();
    var Password = $("#txtRegPass").val();

    $.post("/User/SingUp?Mail=" + Mail + "&Name=" + Name + "&Password=" + Password, "", function (response) {
        ShowModal("Mesaj", response);
    });
}

function Login()
{
    var m = $("#txtLoginMail").val();
    var p = $("#txtLoginPass").val();

    $.post("/User/Login?mail=" + m + "&pass=" + p, "", function (response)
    {
        ShowModal("Mesaj",response);
    });
}