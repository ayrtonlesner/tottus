function docInit(sPath, sReturnUrl) {
    var frmMantPredecir = $("#frmMantPredecir");

    $('#btnPredecir', frmMantPredecir).click(function () {

        url = "../webFrom/MantPredecirAbastecimiento.aspx?anio=" + $("#anio").val() + "&mes=" + $("#mes").val();
        var myframe = document.getElementById("idFrame");
        if (myframe !== null) {
            if (myframe.src) {
                myframe.src = url;
            }
            else if (myframe.contentWindow !== null && myframe.contentWindow.location !== null) {
                myframe.contentWindow.location = url;
            }
            else { myframe.setAttribute('src', url); }
        }
        return false;



    });


}



