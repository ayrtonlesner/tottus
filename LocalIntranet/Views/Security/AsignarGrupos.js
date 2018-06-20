
function initTable(wUsuarioAsignar, frmGrupo, sPath) {
    
    var oTable = $("#tblUserGroup", wUsuarioAsignar).dataTable({
        /* Disable initial sort */
        "aaSorting": [],
        "sScrollX": "98%",
        "sScrollY": "300px",
        "bPaginate": false,
        "bInfo": false,
        oLanguage: {
            sSearch: "Buscar(Enter): "
        }
    });

    $("#tblUserGroup_filter input", wUsuarioAsignar).unbind();
    $("#tblUserGroup_filter input", wUsuarioAsignar).bind("keyup", function (e) {
        if (e.keyCode == 13) {
            oTable.fnFilter(this.value);
        }
    });

    // Al dar check, actualizar inclusión/exclusión del grupo
    $("input[type=checkbox]", wUsuarioAsignar).change(function () {
        // Tomar parametros para asignar/desasignar usuario del grupo
        var c_codigogrupo = $("#c_codigogrupo", frmGrupo).val();
        var c_codigousuario = $(this).closest("tr").attr("data-codigousuario");
        var c_accion = (this.checked ? "I" : "D");

        $.ajax({
            url: sPath + "Security/AsignarGruposAccion",
            type: "POST",
            data: $("#frmAsignarGrupo", wUsuarioAsignar).serialize() + "&c_codigogrupo=" +
                  c_codigogrupo + "&c_codigousuario=" + c_codigousuario + "&c_accion=" + c_accion,
            success: function (result) {
                /*$(wUsuarioAsignar).html(result);
                initTable(wUsuarioAsignar);*/
            }
        });
    });
}

function docInit(sPath, sReturnUrl) {
    var frmGrupo = $("#frmGrupo");

    var wUsuarioAsignar = $("#wUsuariosAsignar");

    /* === Busqueda al seleccionar grupo (cambio en la lista desplegable) === */
    
    // Listar usuarios por asignar
    $("#c_codigogrupo", frmGrupo).change(function () {
        $.ajax({
            url: sPath + "Security/AsignarGruposRes",
            type: "POST",
            data: $(frmGrupo).serialize(),
            success: function (result) {
                $(wUsuarioAsignar).html(result);
                initTable(wUsuarioAsignar, frmGrupo, sPath);
            }
        });
    });

    $("#c_codigogrupo", frmGrupo).change();
    
}