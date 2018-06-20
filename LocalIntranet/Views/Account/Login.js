function docInit(sPath, sReturnUrl) {
    var frmLogin = $("#frmLogin");

    $(frmLogin).keyup(function (event) {
        if (event.keyCode == 13) {
            $("#btnLogin", frmLogin).click();
        }
    });

    $("#btnLogin", frmLogin).click(function () {
        $.ajax({
            url: sPath + 'Account/Login',
            type: "POST",
            data: $(frmLogin).serialize(),
            success: function (result) {
                window.location.href = sReturnUrl;
                return false;
            }
        });
    });
}