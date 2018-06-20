function docInit(sPath, sReturnUrl) {
    var MantDashboard = $("#MantDashboard");

    $('#btnBuscar', MantDashboard).click(function () {


        url = "../reportes/gestionRequerimiento/MantDashboard.aspx?anio=" + $("#anio").val() + "&mes=" + $("#mes").val();
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



