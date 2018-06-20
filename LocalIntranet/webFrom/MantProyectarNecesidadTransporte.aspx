<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MantProyectarNecesidadTransporte.aspx.cs" Inherits="LocalIntranet.webFrom.MantProyectarNecesidadTransporte" %>
<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  
    <%: Scripts.Render("~/bundles/jquery")%><%: Scripts.Render("~/bundles/bootstrap") %><%: Styles.Render("~/Content/css/allcss") %>
<%--    <link href="Styles/fecha.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/fechaEspañol.js" type="text/javascript"></script>--%><%--     <link href="Styles/fecha.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/fechaEspañol.js" type="text/javascript"></script>--%>

<link rel="stylesheet" href="Content/bootstrap.min.css" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.0/css/bootstrap-datepicker3.standalone.css" />
<script type="text/javascript" src="Scripts/jquery-1.11.1.min.js"></script>
<script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.1/js/bootstrap-datepicker.min.js"></script>

    <script type="text/javascript">
        $(function () {

            $("[id$=tx_fechahora]").datepicker({
                autoclose: true,
                format: 'dd/mm/yyyy',
                todayHighlight: true,
                clearBtn: true
            }).attr("readonly", true);
  
            //$("[id$=tx_fechahora1]").datepicker({
            //    format: 'dd/mm/yyyy',
            //    autoclose: true,
            //    defaultDate: "11/1/2013",
            //}).attr("readonly", true);

        });

    </script>

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
        .textbox
            {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 11px;
            font-style:normal;
            height:18px;
           border-radius: 5px 5px 5px 5px;
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
</script>--%>&nbsp;<script type="text/javascript">
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
    </script></head><body><form id="form1" runat="server" >
    <div>
        <asp:Panel ID="pnGrilla" runat="server" Width="100%">
            <div id="div12">
                <table cellpadding="4" cellspacing="4" width="100%">
                    <tbody>
                        <tr>
                            <td class="manteniemitno_tabla">
                                                                <!--Inicio datos-->
        <div class="errorMsgPanel">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
                                                                </div>
        <div class="successMsgPanel"></div>
                                
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
                                                                         <asp:GridView ID="gvDetalleOrdenDespacho" runat="server" AutoGenerateColumns="False" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White" Width="100%" HeaderStyle-CssClass="centerHeaderText" >
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
                                                                                 <FooterStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                 <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                 <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                 </asp:BoundField>
                                                                             </Columns>
                                                                             <HeaderStyle BackColor="#3AC0F2" BorderColor="Black" ForeColor="White" />
                                                                         </asp:GridView>
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
                                <div id="div20">
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
                                                                         <asp:LinkButton ID="lkSi" runat="server" CssClass="btn btn-primary btn-small" Text="Si" OnClick="lkSi_Click"  ></asp:LinkButton>
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
                                
                    
                                                                               
                                
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </asp:Panel>
       

         <div class="panel panel-default">
            <div class="panel-heading">VEHÍCULOS PROGRAMADOS</div>
            <div class="panel-body">
                                                                      <asp:GridView ID="gvPronostico" runat="server" AutoGenerateColumns="False" DataKeyNames="idprogramacion,vehiculo,conductor,estado" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-CssClass="centerHeaderText" HeaderStyle-ForeColor="White" Width="90%" style="text-align: center"  >
                                                                          <Columns>
                                                                              <asp:TemplateField>
                                                                                  <HeaderTemplate>
                                                                                    <%--  <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />--%>
                                                                                  </HeaderTemplate>
                                                                                  <ItemTemplate>
                                                                                      <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="chk_CheckedChanged" AutoPostBack="True" />
                                                                                  </ItemTemplate>
                                                                                  <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10px" />
                                                                                  <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10px" />
                                                                                  <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10px" />
                                                                              </asp:TemplateField>
                                                                              <asp:BoundField DataField="idprogramacion" HeaderText="idprogramacion" Visible="False">
                                                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="estadoprogramacion" HeaderText="estadoprogramacion" Visible="False">
                                                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                                                                              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="conductor" HeaderText="CONDUCTORES">
                                                                              <FooterStyle Width="120px" />
                                                                              <HeaderStyle Width="120px" />
                                                                              <ItemStyle Width="120px" />
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="vehiculo" HeaderText="VEHICULOS">
                                                                              <FooterStyle Width="100px" />
                                                                              <HeaderStyle Width="100px" />
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="cantidadOrden" HeaderText="CANT ORDEN">
                                                                              <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="estado" HeaderText="ESTADO">
                                                                              <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                                                              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="idtransportista" HeaderText="idtransportista" Visible="False" >
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="idvehiculo" HeaderText="idvehiculo" Visible="False" >
                                                                              </asp:BoundField>
                                                                          </Columns>
                                                                          <HeaderStyle BackColor="#3AC0F2" BorderColor="Black" ForeColor="White" />
                                                                      </asp:GridView>
                                                                      <br />
             </div>
          </div>
        </div>



        <div class="panel panel-default">
          <div class="panel-heading">DETALLE DE ORDENES DE DESPACHO</div>
          <div class="panel-body">
                            <asp:Panel ID="Panel2" runat="server" Width="100%">
                                <div id="div21">
                                    <table cellpadding="4" cellspacing="4" width="100%">
                                        <tbody>
                                            <tr>
                                                <td class="manteniemitno_tabla">
                                                         <!--Inicio datos-->
                                                         <table cellpadding="0" cellspacing="1" width="100%">
                                                             <tbody>
                                                                 <tr class="txt_label">
                                                                     <td style="text-align: center">
                                                                         <asp:GridView ID="gvOrdenDespacho" runat="server" AutoGenerateColumns="False" DataKeyNames="iddetalleprogramacion,idordendespacho" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-CssClass="centerHeaderText" HeaderStyle-ForeColor="White" Width="90%" OnRowCommand="gvOrdenDespacho_RowCommand">
                                                                             <Columns>
                                                                                 <asp:BoundField DataField="iddetalleprogramacion" HeaderText="iddetalleprogramacion" Visible="False">
                                                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                                 <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                                 </asp:BoundField>
                                                                                 <asp:BoundField DataField="descripcionSucursal" HeaderText="SUCURSAL">
                                                                                 <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="130px" />
                                                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="130px" />
                                                                                 <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="130px" />
                                                                                 </asp:BoundField>
                                                                                 <asp:BoundField DataField="idordendespacho" HeaderText="ORD DESPA">
                                                                                 <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                                 <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                                 </asp:BoundField>
                                                                                 <asp:BoundField DataField="idsucursal" HeaderText="idsucursal" Visible="False">
                                                                                 <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                                 <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                                 </asp:BoundField>
                                                                                 <asp:BoundField DataField="fecharegistro" HeaderText="FECHA REGISTRO">
                                                                                 <FooterStyle Width="120px" />
                                                                                 <HeaderStyle Width="120px" />
                                                                                 <ItemStyle Width="120px" />
                                                                                 </asp:BoundField>
                                                                                 <asp:BoundField DataField="responsable" HeaderText="RESPONSABLE">
                                                                                 <FooterStyle Width="100px" />
                                                                                 <HeaderStyle Width="100px" />
                                                                                 <ItemStyle Width="100px" />
                                                                                 </asp:BoundField>
                                                                                 <asp:BoundField DataField="estado" HeaderText="ESTADO">
                                                                                 <FooterStyle Width="100px" />
                                                                                 <HeaderStyle Width="100px" />
                                                                                 </asp:BoundField>
                                                                                 <asp:TemplateField HeaderText="SEL.">
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
        </div>


          <div class="panel panel-default">
            <div class="panel-heading">DATOS DE PREPROGRAMACION</div>
            <div class="panel-body">
                            <asp:Panel ID="pnEstados0" runat="server" Width="100%">
                                <div id="div22">
                                    <table cellpadding="4" cellspacing="4" width="100%">
                                        <tbody>
                                            <tr>
                                                <td class="manteniemitno_tabla">
                                                         <!--Inicio datos-->
                                                         <table cellpadding="0" cellspacing="1" width="100%">
                                                             <tbody>
                                                                 <tr class="txt_label">
                                                                     <td style="width: 25%;"><b><a class="estilo_modulos" style="font-family: 'Trebuchet MS'; color: rgb(102, 102, 102); font-size: 11px; text-decoration: none; text-align: left;">PLACA VEHICULO:</a></b></td>
                                                                     <td style="width: 25%;">
                                                                         <b><a class="estilo_modulos" style="font-family: 'Trebuchet MS'; color: rgb(102, 102, 102); font-size: 11px; text-decoration: none; text-align: left;">
                                                                         <asp:Label ID="lblVehiculo" runat="server" Text="SIN ESPECIFICAR"></asp:Label>
                                                                         </a></b>
                                                                     </td>
                                                                     <td style="width: 25%;">&nbsp;</td>
                                                                     <td style="width: 25%;">&nbsp;</td>
                                                                 </tr>
                                                                 <tr class="txt_label">
                                                                     <td style="width: 25%;"><b><a class="estilo_modulos" style="font-family: 'Trebuchet MS'; color: rgb(102, 102, 102); font-size: 11px; text-decoration: none; text-align: left;">CONDUCTOR:</a></b></td>
                                                                     <td style="width: 25%;">
                                                                         <b><a class="estilo_modulos" style="font-family: 'Trebuchet MS'; color: rgb(102, 102, 102); font-size: 11px; text-decoration: none; text-align: left;">
                                                                         <asp:Label ID="lblConductor" runat="server" Text="SIN ESPECIFICAR"></asp:Label>
                                                                         </a></b></td>
                                                                     <td style="width: 25%;">&nbsp;</td>
                                                                      <td style="width: 25%;">&nbsp;</td>
                                                                 </tr>
                                                                 <tr class="txt_label">
                                                                     <td style="width: 25%;"><b><a class="estilo_modulos" style="font-family: 'Trebuchet MS'; color: rgb(102, 102, 102); font-size: 11px; text-decoration: none; text-align: left;">FECHA:</a></b></td>
                                                                     <td style="width: 25%;">
                                                                          <asp:TextBox ID="tx_fechahora" runat="server"   CssClass="textbox"  Width="60%"></asp:TextBox>
                                                                     </td>
                                                                     <td style="width: 25%;">&nbsp;</td>
                                                                     <td style="width: 25%;">&nbsp;</td>
                                                                 </tr>
                                                                 <tr class="txt_label">
                                                                     <td style="width: 25%;"><b><a class="estilo_modulos" style="font-family: 'Trebuchet MS'; color: rgb(102, 102, 102); font-size: 11px; text-decoration: none; text-align: left;">HORA:</a></b></td>
                                                                     <td style="width: 25%;">
                                                                         
                                                                         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                              <ContentTemplate>
                                                                                  <select id="horaProgramacion" class="form-control" name="horaProgramacion">
                                                                                      <option selected="selected" value="08">08:00</option>
                                                                                      <option value="09">09:00</option>
                                                                                      <option value="10">10:00</option>
                                                                                      <option value="11">11:00</option>
                                                                                      <option value="12">12:00</option>
                                                                                      <option value="13">13:00</option>
                                                                                      <option value="14">14:00</option>
                                                                                      <option value="15">15:00</option>
                                                                                      <option value="16">16:00</option>
                                                                                      <option value="17">17:00</option>
                                                                                      <option value="18">18:00</option>
                                                                                  </select>
                                                                            </ContentTemplate>
                                                                         </asp:UpdatePanel>
                                                                        


                                                                     </td>
                                                                     <td style="width: 25%;">&nbsp;</td>
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
          </div>
        </div>


       
       <asp:LinkButton ID="lbGrabar" runat="server" CssClass="btn btn-success btn-small"   Text="Grabar" OnClick="lbGrabar_Click"  ></asp:LinkButton>
          


        <br />
        <br />
        <br />
    
    </div>
    </form>
</body>


</html>
