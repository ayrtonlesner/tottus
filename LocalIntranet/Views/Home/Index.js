function docInit(sPath, sReturnUrl) {
    var frmClientes = $("#frmClientes");

    $(frmClientes).keyup(function (event) {
        if (event.keyCode == 13) {
            $("#frmClientes", frmClientes).click();
        }
    });

    $("#btnProcesar", frmClientes).click(function () {
        $.ajax({
            url: sPath + 'Home/MantDatCliente',
            type: "POST",
            data: $(frmClientes).serialize(),
            success: function (result) {
                //alert("deberia salir algo...");
               
                var strHtml = '<div class="alert alert-success alert-dismissable"> ' +
                        '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>' +
                        "Información registrada" + '</div>';
                $(".successMsgPanel").html(strHtml);
                $(frmClientes)[0].reset();
                $("#tipodocumento", frmClientes).trigger('change');
                return false;
              
            }
        });
        //window.location.href = sPath + 'Pedido/Pedido';
        //return false;
    });


    $("#depNo", frmClientes).change(function () {
        $.ajax({
            url: sPath + 'Ubigeo/ObtenerUbigeo',
            type: "POST",
            data: { accion : 'P', depNo: $("#depNo").val() , proNo : "1" },
            success: function (result) {
               //alert("deberia salir algo...");
                $("#divProvincia").html(result);
                $("#divProvincia", frmClientes).trigger('change');
            }
        });
    });

    $("#divProvincia", frmClientes).change(function () {
        $.ajax({
            url: sPath + 'Ubigeo/ObtenerUbigeo',
            type: "POST",
            data: { accion: 'D', depNo: $("#depNo").val(), proNo: $("#proNo").val() },
            success: function (result) {
                //alert("deberia salir distrito...");
                $("#divDistrito").html(result);
            }
        });
    });

    $("#tipodocumento", frmClientes).change(function () {
        $.ajax({
            url: sPath + 'TipoDocumento/ObtenerTipoDocumento',
            type: "POST",
            data: {  tipodocumento: $("#tipodocumento").val(), numdocumento: $("#numdocumento").val() },
            success: function (result) {
                $("#nombresTipoDocumento").html(result);
            }
        });
    });


    $("#numdocumento", frmClientes).change(function () {
        $.ajax({
            url: sPath + 'TipoDocumento/ObtenerTipoDocumento',
            type: "POST",
            data: {   tipodocumento: $("#tipodocumento").val(), numdocumento: $("#numdocumento").val() },
            success: function (result) {
                $("#nombresTipoDocumento").html(result);
            }
        });
    });


}