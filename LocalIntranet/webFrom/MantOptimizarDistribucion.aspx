<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MantOptimizarDistribucion.aspx.cs" Inherits="LocalIntranet.webFrom.MantOptimizarDistribucion" %>
<%@ Import Namespace="System.Web.Optimization" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>


    
        <script type="text/javascript">
            function openModal() {
                $('#myModal').modal('show');
            }
            function openMensaje() {
                $('#myMensaje').modal('show');
            }
            function openDetalle() {
                $('#myModalDetalle').modal('show');
            }

       </script>

         <script type="text/javascript">
             // Ejecucion general para el sitio
             (function () {
                 /* Multinivel para el menu de opciones */
                 ////$("#menu").metisMenu({ toggle: false });

                 //alert("entro");

                 /* ==== Manejador global de errores ====*/
                 function globalAjaxErrorHandler(e, jqxhr, settings, exception) {

                     $(".successMsgPanel").empty();
                     var errInfo = jQuery.parseJSON(jqxhr.responseText);

                     var strHtml = '<div class="alert alert-danger alert-dismissable"> ' +
                         '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>' +
                         errInfo.Message + '</div>';
                     $(".errorMsgPanel").html(strHtml);
                 }

                 $(document).ajaxError(globalAjaxErrorHandler);

                 $.ajaxSetup({ cache: false });

                 $(document).ajaxSuccess(function () {
                     $(".errorMsgPanel").empty();
                 });
                 /* ==== Manejador global de errores ====*/



                 /* ==== Indicador global de trabajo en segundo plano ====*/
                 //var loadingDiv = $('.loadingDiv').hide();
                 //$(document)
                 //    .ajaxStart(function () {
                 //        $(loadingDiv).show();
                 //    })
                 //    .ajaxStop(function () {
                 //        $(loadingDiv).hide();
                 //    });
             })();

             function showError(msg) {
                 $(".errorMsgPanel").empty();
                 var strHtml = '<div class="alert alert-danger alert-dismissable"> ' +
                 '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>' +
                 msg + '</div>';
                 $(".errorMsgPanel").html(strHtml);
             }

             function showErrorModal(msg) {
                 var errorModal =
                     $('<div class="modal fade">' +
                         '<div class="modal-dialog">' +
                         '<div class="modal-content">' +
                         '<div class="modal-header">' +
                           '<a class="close" data-dismiss="modal" >&times;</a>' +
                           '<h3>' + "Validar datos" + '</h3>' +
                         '</div>' +

                         '<div class="modal-body">' +
                           '<div class="alert alert-danger alert-dismissable"> ' +
                               '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>' + msg +
                            '</div>' +
                         '</div>' +

                         '<div class="modal-footer">' +
                           //'<a href="#!" class="btn" data-dismiss="modal">' +
                           //  msg +
                           //'</a>' +
                           //'<a href="#!" id="okButton" class="btn btn-primary">' +
                           //  msg +
                           //'</a>' +
                         '</div>' +
                         '</div>' +
                         '</div>' +
                       '</div>');

                 errorModal.find('#okButton').click(function (event) {
                     callback();
                     errorModal.modal('hide');
                 });

                 errorModal.modal('show');
             }



             function showSuccessModal(msg) {
                 var errorModal =
                     $('<div class="modal fade">' +
                         '<div class="modal-dialog">' +
                         '<div class="modal-content">' +
                         '<div class="modal-header">' +
                           '<a class="close" data-dismiss="modal" >&times;</a>' +
                           '<h3>' + "Validar datos" + '</h3>' +
                         '</div>' +

                         '<div class="modal-body">' +
                           '<div class="alert alert-success alert-dismissable"> ' +
                               '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>' + msg +
                            '</div>' +
                         '</div>' +

                         '<div class="modal-footer">' +
                           //'<a href="#!" class="btn" data-dismiss="modal">' +
                           //  msg +
                           //'</a>' +
                           //'<a href="#!" id="okButton" class="btn btn-primary">' +
                           //  msg +
                           //'</a>' +
                         '</div>' +
                         '</div>' +
                         '</div>' +
                       '</div>');

                 errorModal.find('#okButton').click(function (event) {
                     callback();
                     errorModal.modal('hide');
                 });

                 errorModal.modal('show');
             }

             function showSuccess(msg) {

                 $(".errorMsgPanel").empty();
                 var strHtml = '<div class="alert alert-success alert-dismissable"> ' +
                 '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>' +
                 msg + '</div>';
                 $(".successMsgPanel").html(strHtml);
             }

             /* Generic Confirm func */
             function confirm(heading, question, cancelButtonTxt, okButtonTxt, callback) {

                 var confirmModal =
                   $('<div class="modal fade">' +
                       '<div class="modal-dialog">' +
                       '<div class="modal-content">' +
                       '<div class="modal-header">' +
                         '<a class="close" data-dismiss="modal" >&times;</a>' +
                         '<h3>' + heading + '</h3>' +
                       '</div>' +

                       '<div class="modal-body">' +
                         '<p>' + question + '</p>' +
                       '</div>' +

                       '<div class="modal-footer">' +
                         '<a href="#!" class="btn" data-dismiss="modal">' +
                           cancelButtonTxt +
                         '</a>' +
                         '<a href="#!" id="okButton" class="btn btn-primary">' +
                           okButtonTxt +
                         '</a>' +
                       '</div>' +
                       '</div>' +
                       '</div>' +
                     '</div>');

                 confirmModal.find('#okButton').click(function (event) {
                     callback();
                     confirmModal.modal('hide');
                 });

                 confirmModal.modal('show');
             };
             /* END Generic Confirm func */
    </script>


     <style>
        .centerHeaderText th {
            text-align: center;
        }
    </style>


    <script type = "text/javascript">
        function Check_Click(obj) {
            var row = obj.parentNode.parentNode;
            if (obj.checked) {
                row.style.backgroundColor = "#4673C8";
                row.style.color = 'white';
            } else {
                row.style.backgroundColor = "#F4F5EB";
                row.style.color = '#4673C8';
            }


            var GridView = row.parentNode;
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                var cell = inputList[i].parentNode;
                var headerCheckBox = inputList[0];
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;

        }
    </script>
    <script type = "text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var cell = inputList[i].parentNode;
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && cell.cellIndex == 0 && objRef != inputList[i]) {
                    if (objRef.checked) {
                        row.style.backgroundColor = "#4673C8";
                        row.style.color = 'white';

                        inputList[i].checked = true;
                    }
                    else {

                        row.style.backgroundColor = "#F4F5EB";
                        row.style.color = '#4673C8';

                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>

<%--<script type = "text/javascript">
    function MouseEvents(objRef, evt) {
        var checkbox = objRef.getElementsByTagName("input")[0];
        if (evt.type == "mouseover") {

            objRef.style.backgroundColor = "#4673c8";
            objRef.style.color = 'white';
        }

        else {
            if (checkbox.checked) {
                objRef.style.backgroundColor = "#4673C8";
                objRef.style.color = 'white';
            }
            else if (evt.type == "mouseout") {
                objRef.style.backgroundColor = "#F4F5EB";
                objRef.style.color = '#4673C8';
            }

        }

    }
</script>--%>


            <script type="text/javascript">
                function ValidarEnviar() {
                    var error = 0;
                    var sStr = "Validar los datos  <br/>";
                    var valid = false;
                    var gdvw = document.getElementById('<%=gvTransportista.ClientID %>');

                if (gdvw != null) {

                    for (var i = 1; i < gdvw.rows.length; i++) {
                        var value = gdvw.rows[i].getElementsByTagName('input');
                        if (value != null) {
                            if (value[0].type == "checkbox") {
                                if (value[0].checked) {
                                    valid = true;
                                    if (valid == true) {
                                        valid = true;
                                    }
                                    else {
                                        valid = false;
                                    }
                                }
                            }
                        }
                    }
                }
                else {
                    error = 1;
                    sStr += "No Existen Registro <br/> ";
                }

                if ((valid == false) && (gdvw != null)) {
                    error = 1;
                    sStr += "Seleccionar Datos del Detalle  <br/> ";
                }


                if (error == 0) {
                    return true;
                }

                else {
                    //alert(sStr);
                    showErrorModal(sStr);
                    return false;
                }
            }
    </script>

        <script type="text/javascript">
            function ValidarAgregar() {
                var error = 0;
                var sStr = "Validar los datos  <br/>";
                var valid = false;
                var gdvw = document.getElementById('<%=gvPronostico.ClientID %>');

                if (gdvw != null) {

                    for (var i = 1; i < gdvw.rows.length; i++) {
                        var value = gdvw.rows[i].getElementsByTagName('input');
                        if (value != null) {
                            if (value[0].type == "checkbox") {
                                if (value[0].checked) {
                                    valid = true;
                                    if (valid == true) {
                                        valid = true;
                                    }
                                    else {
                                        valid = false;
                                    }
                                }
                            }
                        }
                    }
                }
                else {
                    error = 1;
                    sStr += "No Existen Registro <br/> ";
                }

                if ((valid == false) && (gdvw != null)) {
                    error = 1;
                    sStr += "Seleccionar Datos del Detalle  <br/> ";
                }


                if (error == 0) {
                    return true;
                }

                else {
                    //alert(sStr);
                    showErrorModal(sStr);
                    return false;
                }
            }
    </script>

   
</head>
<body>

     <%: Scripts.Render("~/bundles/jquery")%><%: Scripts.Render("~/bundles/bootstrap") %><%: Styles.Render("~/Content/css/allcss") %>



    <form id="form1" runat="server">
    <div>
    
        <asp:Panel ID="pnGrilla" runat="server" Width="100%">
            <div id="div12">
                <table cellpadding="4" cellspacing="4" width="100%">
                    <tbody>
                        <tr>
                            <td class="manteniemitno_tabla">
                                                                <!--Inicio datos-->
        <div class="errorMsgPanel"></div>
        <div class="successMsgPanel"></div>
                                
                   
                                
             <!-- Modal HTML -->
            <div id="myModal" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <asp:ScriptManager ID="ScriptManager2" runat="server">
                            </asp:ScriptManager>
                            <h4 class="modal-title">
                                <asp:Label ID="lblTituloPopup" runat="server" Text="TRANSPORTISTAS DISPONIBLES"></asp:Label>
                                </h4>
                        </div>
                        <div class="modal-body">
                            <asp:Panel ID="pnEstados" runat="server" Width="100%">
                                <div id="div19">
                                    <table cellpadding="4" cellspacing="4" width="100%">
                                        <tbody>
                                            <tr>
                                                <td class="manteniemitno_tabla">
                                                         <!--Inicio datos-->
                                                         <table cellpadding="0" cellspacing="1" width="100%">
                                                             <tbody>
                                                                 <tr class="txt_label">
                                                                     <td colspan="4">
                                                                         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                             <ContentTemplate>
                                                                                 <asp:GridView ID="gvTransportista" runat="server" AutoGenerateColumns="False" DataKeyNames="idtransportista,idvehiculo,nombreTransportista,placa,estado" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-CssClass="centerHeaderText" HeaderStyle-ForeColor="White"  Width="100%" OnRowDataBound="gvTransportista_RowDataBound">
                                                                                     <Columns>
                                                                                         <asp:TemplateField>
                                                                                             <ItemTemplate>
                                                                                                 <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                                                                             </ItemTemplate>
                                                                                             <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10px" />
                                                                                             <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10px" />
                                                                                             <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10px" />
                                                                                         </asp:TemplateField>
                                                                                         <asp:BoundField DataField="idtransportista" HeaderText="idtransportista" Visible="False">
                                                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                                         <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                                         </asp:BoundField>
                                                                                         <asp:BoundField DataField="nombreTransportista" HeaderText="CONDUCTORES">
                                                                                         <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="280px" />
                                                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="280px" />
                                                                                         <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="280px" />
                                                                                         </asp:BoundField>
                                                                                         <asp:BoundField DataField="idvehiculo" HeaderText="idvehiculo" Visible="False" />
                                                                                         <asp:BoundField DataField="placa" HeaderText="PLACA">
                                                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                                         <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                                         </asp:BoundField>
                                                                                         <asp:BoundField DataField="pesobruto" HeaderText="CARGA (TN)">
                                                                                         <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                                         <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                                         </asp:BoundField>
                                                                                         <asp:BoundField DataField="estado" HeaderText="ESTADO">
                                                                                         <FooterStyle Width="100px" />
                                                                                         <HeaderStyle Width="100px" />
                                                                                         <ItemStyle Width="100px" />
                                                                                         </asp:BoundField>
                                                                                     </Columns>
                                                                                     <HeaderStyle BackColor="#3AC0F2" BorderColor="Black" ForeColor="White" />
                                                                                 </asp:GridView>
                                                                             </ContentTemplate>
                                                                         </asp:UpdatePanel>
                                                                     </td>
                                                                 </tr>
                                                                 <tr class="txt_label">
                                                                     <td style="width: 25%;">&nbsp;</td>
                                                                     <td style="width: 25%;">&nbsp;</td>
                                                                     <td style="width: 25%;">
                                                                         <asp:LinkButton ID="lkOk" runat="server" CssClass="btn btn-success btn-small" OnClientClick="return ValidarEnviar();" Text="Ok" OnClick="lkOk_Click"></asp:LinkButton>
                                                                     </td>
                                                                     <td style="width: 25%;">&nbsp;</td>
                                                                 </tr>
                                                             </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                          <%--  <button type="button" class="btn btn-primary">Save changes</button>--%>
                        </div>
                    </div>
                </div>
            </div>
                                
                    
             <!-- Modal HTML -->
            <div id="myModalDetalle" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">
                                <asp:Label ID="Label2" runat="server" Text="DETALLE ORDEN DE DESPACHO"></asp:Label>
                                </h4>
                        </div>
                        <div class="modal-body">
                            <asp:Panel ID="Panel2" runat="server" Width="100%">
                                <div id="div19">
                                    <table cellpadding="4" cellspacing="4" width="100%">
                                        <tbody>
                                            <tr>
                                                <td class="manteniemitno_tabla">
                                                         <!--Inicio datos-->
                                                         <table cellpadding="0" cellspacing="1" width="100%">
                                                             <tbody>
                                                                 <tr class="txt_label">
                                                                     <td colspan="4">
                                                                         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                             <ContentTemplate>
                                                                                 <asp:GridView ID="gvDetalleOrdenDespacho" runat="server" AutoGenerateColumns="False" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-CssClass="centerHeaderText" HeaderStyle-ForeColor="White"  Width="100%">
                                                                                     <Columns>
                                                                                         <asp:BoundField DataField="descripcion" HeaderText="DESCRIPCION PRODUCTO">
                                                                                         <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="280px" />
                                                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="280px" />
                                                                                         <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="280px" />
                                                                                         </asp:BoundField>
                                                                                         <asp:BoundField DataField="cantidadorden" HeaderText="CANT. ORDEN.">
                                                                                         <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                                         <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                                         </asp:BoundField>
                                                                                         <asp:BoundField DataField="nombreunidadmedida" HeaderText="UND. MED." >
                                                                                         <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                                         <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                                         </asp:BoundField>
                                                                                         <asp:BoundField DataField="peso" HeaderText="PESO">
                                                                                         <FooterStyle Width="100px" />
                                                                                         <HeaderStyle Width="100px" />
                                                                                         <ItemStyle Width="100px" />
                                                                                         </asp:BoundField>
                                                                                         <asp:BoundField DataField="tonelada" HeaderText="PESO (TN)">
                                                                                         <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                                         <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                                         </asp:BoundField>
                                                                                     </Columns>
                                                                                     <HeaderStyle BackColor="#3AC0F2" BorderColor="Black" ForeColor="White" />
                                                                                 </asp:GridView>
                                                                             </ContentTemplate>
                                                                         </asp:UpdatePanel>
                                                                     </td>
                                                                 </tr>
                                                                 <tr class="txt_label">
                                                                     <td style="width: 25%;">&nbsp;</td>
                                                                     <td style="width: 25%;">&nbsp;</td>
                                                                     <td style="width: 25%;">
                                                                         &nbsp;</td>
                                                                     <td style="width: 25%;">&nbsp;</td>
                                                                 </tr>
                                                             </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                          <%--  <button type="button" class="btn btn-primary">Save changes</button>--%>
                        </div>
                    </div>
                </div>
            </div>
                                                    
                                
    <!-- Modal HTML -->
            <div id="myMensaje" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">
                                <asp:Label ID="Label1" runat="server" Text="¿Desea guardar la Optimización? "></asp:Label>
                                </h4>
                        </div>
                        <div class="modal-body">
                            <asp:Panel ID="Panel1" runat="server" Width="100%">
                                <div id="div19">
                                    <table cellpadding="4" cellspacing="4" width="100%">
                                        <tbody>
                                            <tr>
                                                <td class="manteniemitno_tabla">
                                                         <!--Inicio datos-->
                                                         <table cellpadding="0" cellspacing="1" width="100%">
                                                             <tbody>
                                                                 <tr class="txt_label">
                                                                     <td style="width: 25%;">&nbsp;</td>
                                                                     <td style="width: 25%;">
                                                                         &nbsp;</td>
                                                                     <td style="width: 25%;">
                                                                         &nbsp;</td>
                                                                     <td style="width: 25%;">
                                                                         <asp:LinkButton ID="lkSi" runat="server" CssClass="btn btn-primary btn-small" Text="Si" OnClick="lkSi_Click" ></asp:LinkButton>
                                                                         &nbsp;<asp:LinkButton ID="lkNo" runat="server" CssClass="btn btn-primary btn-small" Text="No"></asp:LinkButton>
                                                                     </td>
                                                                 </tr>
                                                             </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </asp:Panel>
                        </div>
                        <%--<div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                          <%--  <button type="button" class="btn btn-primary">Save changes</button>--%>
                        </div>
                    </div>
                </div>
            </div>
                                
                    
                                                                               
                                
                                <table cellpadding="0" cellspacing="1" width="100%">
                                                          <tbody>
                                                              <tr class="txt_label">
                                                                  <td style="width: 33%;"><b>RESULTADO DE BUSQUEDA: </b></td>
                                                                  <td style="width: 33%;">
                                                                      &nbsp;</td>
                                                                  <td style="width: 33%;">&nbsp;</td>
                                                              </tr>

                                                              <tr class="txt_label">
                                                                  <td align="center" colspan="3">
                                                                      <asp:GridView ID="gvPronostico" runat="server" AutoGenerateColumns="False" DataKeyNames="idordendespacho,idsucursal,descripcionSucursal,responsable,fecharegistro,carga,conductor,vehiculo,estado,idtransportista,idvehiculo,estadoGrabacion" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-CssClass="centerHeaderText" HeaderStyle-ForeColor="White" Width="90%" OnRowDataBound="gvPronostico_RowDataBound" OnRowCommand="gvPronostico_RowCommand" >
                                                                          <Columns>
                                                                              <asp:TemplateField>
                                                                                  <HeaderTemplate>
                                                                                      <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                                                                  </HeaderTemplate>
                                                                                  <ItemTemplate>
                                                                                      <asp:CheckBox ID="CheckBox1" runat="server" onclick="Check_Click(this)" />
                                                                                  </ItemTemplate>
                                                                                  <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10px" />
                                                                                  <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10px" />
                                                                                  <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10px" />
                                                                              </asp:TemplateField>
                                                                              <asp:BoundField DataField="idordendespacho" HeaderText="idordendespacho" Visible="False">
                                                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="idsucursal" HeaderText="idsucursal" Visible="False">
                                                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                                                                              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="descripcionSucursal" HeaderText="SUCURSAL">
                                                                              <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                                                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                                                                              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="responsable" HeaderText="RESPONSABLE">
                                                                              <FooterStyle Width="150px" />
                                                                              <HeaderStyle Width="150px" />
                                                                              <ItemStyle Width="150px" />
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="fecharegistro" HeaderText="FECHA REG.">
                                                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="carga" HeaderText="CARGA (TN)" >
                                                                              <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                                                              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="conductor" HeaderText="CONDUCTORES" >
                                                                              <FooterStyle Width="120px" />
                                                                              <HeaderStyle Width="120px" />
                                                                              <ItemStyle Width="120px" />
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="vehiculo" HeaderText="VEHICULOS" >
                                                                              <FooterStyle Width="100px" />
                                                                              <HeaderStyle Width="100px" />
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="estado" HeaderText="ESTADO" >
                                                                              <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                                                              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="idtransportista" HeaderText="idtransportista" Visible="False" />
                                                                              <asp:BoundField DataField="idvehiculo" HeaderText="idvehiculo" Visible="False" />
                                                                              <asp:BoundField DataField="estadoGrabacion" HeaderText="estadoGrabacion" Visible="False" />
                                                                              <asp:TemplateField HeaderText="SELEC.">
                                                                                  <ItemTemplate>
                                                                                         <div class="input-prepend">
                                                                                          <span class="add-on"><i class="fa fa-check"></i></span>
                                                                                          <asp:Button ID="Button1" runat="server" class="btn-primary" CommandName="Select" name="Button1" Text="Detalle Ord." title="Ver detalle" />
                                                                                      </div>
                                                                                  </ItemTemplate>
                                                                                  <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                                                                  <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                                                                  <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                                                              </asp:TemplateField>
                                                                          </Columns>
                                                                          <HeaderStyle BackColor="#3AC0F2" BorderColor="Black" ForeColor="White" />
                                                                      </asp:GridView>
                                                                      <br />
                                                                      <br />
                                                                  </td>
                                                              </tr>
                                                              <tr class="txt_label">
                                                                  <td style="width: 33%;">
                                                                      &nbsp;</td>
                                                                  <td style="width: 33%;">&nbsp;</td>
                                                                  <td style="width: 33%;">
                                                                      &nbsp;</td>
                                                              </tr>
                                                              <tr class="txt_label">
                                                                  <td style="width: 33%;" >
                                                                      &nbsp;</td>
                                                                  <td style="width: 33%;">
                                                                      <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-primary btn-small" OnClientClick="return ValidarAgregar();" Text="Disponibilidad de Transporte" OnClick="LinkButton2_Click">
                                                                            <span class="fa fa-user"></span>&nbsp;Disponibilidad de Transporte
                                                                        </asp:LinkButton>
                                                                  </td>
                                                                  <td style="width: 33%;">
                                                                      <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-success btn-small"   Text="Grabar" OnClick="LinkButton1_Click" >
                                                                            <span class="fa fa-floppy-o"></span>&nbsp;Grabar
                                                                        </asp:LinkButton>
                                                                  </td>
                                                              </tr>
                                                          </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </asp:Panel>
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>
