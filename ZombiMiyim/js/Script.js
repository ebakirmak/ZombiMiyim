



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
    if ((Mail.indexOf('@') == -1) || (Mail.indexOf('.com') == -1)) {
        $('#alertMessageSign').show();
        $('#alertMessageSign').text("Geçerli bir mail adresi giriniz.");
    }
    else {


        $.post("/User/SingUp?Mail=" + Mail + "&Name=" + Name + "&Password=" + Password, "", function (response) {
            if (response == "Kayıt başarılı.") {

                $('#alertMessageSign').show();
                $('#alertMessageSign').text("Lütfen mailinizi kontrol ediniz. Etkinleştirme kodunuz gönderilmiştir.");
                clearInput();
            }
            else {

                $('#alertMessageSign').show();
                $('#alertMessageSign').text(response);
                clearInput();
            }

        });
    }
}
function Login()
{
  
        var m = $("#txtLoginMail").val();
        var p = $("#txtLoginPass").val();

        $.post("/User/Login?mail=" + m + "&pass=" + p, "", function (response) {
            if (response == "Hatalı kullanıcı adı veya şifre") {

                $('#alertMessage').show();
                $('#alertMessage').text(response);
                clearInput();

            }
            else {
                window.location.reload();
            }

        });  
   
}
function  LogOut()
{
    $.post("/User/LogOut", function response() {
        location.reload();
    });
}
function linkCodeMessage() {
    $.post("/Home/linkCodeMessage", function (backdata) {
       
        $('#kodModal').on('show.bs.modal', function () {
            $('#linkKodMessage').text(backdata);
        });
        $('#kodModal').modal('show');
    });
}
function controlInput() {
    $('#alertMessage').hide();
    $('#alertMessageSign').hide();
}
function clearInput() {
    $('input:not(:button) ').val('');
}
