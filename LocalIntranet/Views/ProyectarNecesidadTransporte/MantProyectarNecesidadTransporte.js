function docInit(sPath, sReturnUrl) {
    var frmProyectar = $("#frmProyectar");

    $('#btnBuscar', frmProyectar).click(function () {

        //alert(anio);
     


        url = "../webFrom/MantProyectarNecesidadTransporte.aspx?departamento=" + $("#departamento").val(); //+ "&mes=" + $("#mes").val()
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



