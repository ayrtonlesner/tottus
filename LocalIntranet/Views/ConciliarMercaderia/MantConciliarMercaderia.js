function docInit(sPath, sReturnUrl) {
    var frmConciliar = $("#frmConciliar");

    $('#btnBuscar', frmConciliar).click(function () {

        var proveedor = $("#Proveedores").val();

        //alert(proveedor);
        //alert(mes);

        var tipoBusqueda, tipoReporte;

        url = "../webFrom/MantConciliarMercaderia.aspx?proveedor=" + $("#Proveedores").val(); //+ "&mes=" + $("#mes").val()
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



