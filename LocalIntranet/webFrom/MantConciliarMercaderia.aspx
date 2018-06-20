<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MantConciliarMercaderia.aspx.cs" Inherits="LocalIntranet.webFrom.MantConciliarMercaderia" %>
<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }
        function openMensaje() {
            $('#myMensaje').modal('show');
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

        .auto-style2 {
            height: 430px;
        }

        .auto-style3 {
            width: 33%;
            height: 38px;
        }

        .auto-style4 {
            width: 33%;
        }

        .auto-style5 {
            margin-left: 60px;
            margin-right: 4px;
            margin-top: 0px;
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

   
</head>
<body>

    <%: Scripts.Render("~/bundles/jquery")%><%: Scripts.Render("~/bundles/bootstrap") %><%: Styles.Render("~/Content/css/allcss") %>

    <form id="form1" runat="server">
        <div>
        </div>
                <table cellpadding="4" cellspacing="4" width="100%" style="height: 488px">
                    <tbody>
                        <tr>
                            <td class="manteniemitno_tabla">
                        <!--Inicio datos-->
                                <div class="errorMsgPanel"></div>
                                <div class="successMsgPanel"></div>        
                   
                                
             <!-- Modal HTML -->
                                <table cellpadding="0" cellspacing="1" width="100%" style="margin-top: 0px">
                                                          <tbody>
                                                              <tr class="txt_label">
                                                                  <td class="auto-style4"><b>RESULTADO DE BUSQUEDA: </b></td>
                                                                  <td class="auto-style4">
                                                                      </td>
                                                                  <td class="auto-style4"></td>
                                                              </tr>

                                                              <tr class="txt_label">
                                                                  <td align="center" colspan="3" class="auto-style2" rowspan="1" aria-orientation="horizontal">
                                                                      <asp:GridView ID="gvFactura" runat="server" AutoGenerateColumns="False" DataKeyNames="nombrearchivo" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-CssClass="centerHeaderText" HeaderStyle-ForeColor="White" Width="69%" OnRowDataBound="gvFactura_RowDataBound" Height="197px" OnSelectedIndexChanged="gvFactura_SelectedIndexChanged" CssClass="auto-style5" ShowFooter="True" >
                                                                          <Columns>
                                                                              <asp:TemplateField>
                                                                                  <HeaderTemplate>
                                                                                      <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                                                                  </HeaderTemplate>
                                                                                  <ItemTemplate>
                                                                                      <asp:CheckBox ID="CheckBox2" runat="server" onclick="Check_Click(this)" />
                                                                                  </ItemTemplate>
                                                                                  <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10px" />
                                                                                  <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                                                  <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                                              </asp:TemplateField>
                                                                              <asp:BoundField DataField="numerodocumento" HeaderText="NUMERO DOCUMENTO">
                                                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="300px" />
                                                                              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="fechaemi" HeaderText="FECHA EMISION">
                                                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="tipodocumento" HeaderText="TIPO DOCUMENTO">
                                                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="Base" HeaderText="BASE">
                                                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="Exonerado" HeaderText="EXONERADO" Visible="False" >
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="Igv" HeaderText="IGV" >
                                                                                  <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="Total" HeaderText="TOTAL" >
                                                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="idProveedor" HeaderText="idProveedor" Visible="False" >
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="nombrearchivo" HeaderText="ARCHIVO" >
                                                                              <ControlStyle Width="0px" />
                                                                              <HeaderStyle Width="400px" />
                                                                              <ItemStyle Width="0px" />
                                                                              </asp:BoundField>
                                                                              <asp:ButtonField Text="Ver" CommandName="Select" ItemStyle-Width="30"  >
<ItemStyle Width="30px"></ItemStyle>
                                                                              </asp:ButtonField>
                                                                              <asp:BoundField DataField="codigotipodocumento" HeaderText="CODIGO TIPO DOC" Visible="False" />
                                                                          </Columns>
                                                                          <HeaderStyle BackColor="#3AC0F2" BorderColor="Black" ForeColor="White" />
                                                                      </asp:GridView>
                                                                      <br />
                                                                      <asp:GridView ID="gvFacturaDetalle" runat="server" AutoGenerateColumns="False" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-CssClass="centerHeaderText" HeaderStyle-ForeColor="White" Width="69%" style="margin-left: 58px; margin-bottom: 0px;" Height="193px" OnSelectedIndexChanged="gvFacturaDetalle_SelectedIndexChanged" ShowFooter="True" >
                                                                          <AlternatingRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                          <Columns>
                                                                              <asp:BoundField DataField="iddetalle" HeaderText="LINEA" Visible="False">
                                                                              <HeaderStyle Width="10px" />
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="idproducto" HeaderText="SKU">
                                                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                                              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="nombre" HeaderText="DETALLE">
                                                                              <FooterStyle Width="120px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                              <HeaderStyle Width="460px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                              <ItemStyle Width="120px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="idcantidad" HeaderText="CANTIDAD">
                                                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="idprecio" HeaderText="PRECIO">
                                                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="igv" HeaderText="IGV" Visible="False">
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="porcentaje" HeaderText="% IGV" Visible="False" />
                                                                              <asp:BoundField DataField="idprecioigv" HeaderText="PRECIO / IGV" >
                                                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                                                                              </asp:BoundField>
                                                                              <asp:BoundField DataField="total" HeaderText="TOTAL">
                                                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                                                                              </asp:BoundField>
                                                                          </Columns>
                                                                          <HeaderStyle BackColor="#3AC0F2" BorderColor="Black" ForeColor="White" Wrap="False" />
                                                                      </asp:GridView>
                                                                      <br />
                                                                  </td>
                                                              </tr>
                                                              <tr class="txt_label">
                                                                  <td class="auto-style3">
                                                                      <asp:ScriptManager ID="ScriptManager1" runat="server">
                                                                      </asp:ScriptManager>
                                                                      </td>
                                                                  <td class="auto-style3"></td>
                                                                  <td class="auto-style3">
                                                                      <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-success btn-small"   Text="Grabar" OnClick="LinkButton1_Click" >
                                                                            <span class="fa fa-floppy-o"></span>&nbsp;Conciliar mercaderia
                                                                        </asp:LinkButton>
                                                                      </td>
                                                              </tr>
                                                          </tbody>
                                </table>

                                <div id="myMensaje" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">
                                <asp:Label ID="Label1" runat="server" Text="¿Desea generar la Conciliación? "></asp:Label>
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
                                                                         <asp:LinkButton ID="lkSi" runat="server" CssClass="btn btn-primary btn-small" Text="Si" OnClick="lkSi_Click"></asp:LinkButton>
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

                            </td __designer:mapid="7a">
                        </tr __designer:mapid="7b">
                    </tbody __designer:mapid="7c">
                </table __designer:mapid="7d">
    </form>
</body>
</html>
