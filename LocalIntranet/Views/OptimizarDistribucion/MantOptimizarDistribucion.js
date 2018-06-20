function docInit(sPath, sReturnUrl) {
    var frmOptimizar = $("#frmOptimizar");

    $('#btnBuscar', frmOptimizar).click(function () {

        var anio = $("#departamento").val();
        //var mes = $("#mes").val();

        //alert(anio);
        //alert(mes);

        var tipoBusqueda, tipoReporte;

        url = "../webFrom/MantOptimizarDistribucion.aspx?departamento=" + $("#departamento").val(); //+ "&mes=" + $("#mes").val()
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



