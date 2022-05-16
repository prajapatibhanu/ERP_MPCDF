<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PlantCCDCSPayment.aspx.cs" Inherits="mis_Payment_PlantCCDCSPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <%--Confirmation Modal Start --%>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #d9d9d9;">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                        </button>
                        <h4 class="modal-title" id="myModalLabel">Confirmation</h4>

                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <p>
                            <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp;
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>

        </div>
    </div>
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Plant CCD/CS Payment</h3>
                            <span id="ctl00_ContentBody_lblmsg"></span>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>DCS/CC code<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvDCSCCcode" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlDCSCCcode" InitialValue="0" ErrorMessage="Select DCS/CC code" Text="<i class='fa fa-exclamation-circle' title='Select DCS/CC code !'></i>">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlDCSCCcode" runat="server" CssClass="form-control select2" ClientIDMode="Static">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>DCS code</asp:ListItem>
                                            <asp:ListItem>CC code</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>DCS/CC Name<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvDCSCCName" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlDCSCCName" InitialValue="0" ErrorMessage="Select DCS/CC Name" Text="<i class='fa fa-exclamation-circle' title='Select DCS/CC Name !'></i>">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlDCSCCName" runat="server" CssClass="form-control select2" ClientIDMode="Static">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>DCS Name</asp:ListItem>
                                            <asp:ListItem>CC Name</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Route No.<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvRouteNo" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlRouteNo" InitialValue="0" ErrorMessage="Select Route No." Text="<i class='fa fa-exclamation-circle' title='Select Route No !'></i>">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlRouteNo" runat="server" CssClass="form-control select2">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>R1</asp:ListItem>
                                            <asp:ListItem>R2</asp:ListItem>
                                            <asp:ListItem>R3</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>From Date<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ForeColor="red" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtFromDate" ErrorMessage="Enter From Date." Text="<i class='fa fa-exclamation-circle' title='Enter From Date !'></i>"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" ForeColor="Red" runat="server" Display="Dynamic" ControlToValidate="txtFromDate" ErrorMessage="Invalid From Date" Text="<i class='fa fa-exclamation-circle' title='Invalid From Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                        </span>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" onkeypress="return false;" runat="server" placeholder="dd/mm/yyyy" class="form-control" data-date-end-date="+7d" autocomplete="off" data-date-format="dd/mm/yyyy" data-date-autoclose="true" data-provide="datepicker" data-date-start-date="-365d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>To Date<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ForeColor="red" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtToDate" ErrorMessage="Enter To Date." Text="<i class='fa fa-exclamation-circle' title='Enter To Date !'></i>"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" ForeColor="Red" runat="server" Display="Dynamic" ControlToValidate="txtToDate" ErrorMessage="Invalid To Date" Text="<i class='fa fa-exclamation-circle' title='Invalid To Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                        </span>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtToDate" onkeypress="return false;" runat="server" placeholder="dd/mm/yyyy" class="form-control" data-date-end-date="+7d" autocomplete="off" data-date-format="dd/mm/yyyy" data-date-autoclose="true" data-provide="datepicker" data-date-start-date="-365d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:Button runat="server" class="btn btn-success btn-block" ValidationGroup="a" ID="btnSubmit" Text="Fetch" OnClientClick="return ValidatePage();" AccessKey="S" />

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table">
                                        <div>
                                            <table class="table table-striped table-bordered" cellspacing="0" rules="all" border="1" id="ctl00_ContentBody_gvOpeningStock">
                                                <thead>
                                                    <tr>
                                                        <th>
                                                            <input type="checkbox" id="chkAll" /></th>
                                                        <th scope="col">Date</th>
                                                        <th scope="col">Shift</th>
                                                        <th scope="col">Milk Type<br />
                                                        </th>
                                                        <th scope="col">Fat %<br />
                                                        </th>
                                                        <th scope="col">SNF %<br />
                                                        </th>
                                                        <th scope="col">Milk Qty. (Kg)<br />
                                                        </th>
                                                        <th scope="col">Fat (Kg)<br />
                                                        </th>
                                                        <th scope="col">SNF (Kg)<br />
                                                        </th>
                                                        <th scope="col">Milk Quality<br />
                                                        </th>
                                                        <th scope="col">Value<br />
                                                        </th>
                                                        <th scope="col">Status<br />
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <input type="checkbox" id="chk1" />
                                                        </td>
                                                        <td>13/02/2019
                                                        </td>
                                                        <td>Evening
                                                        </td>
                                                        <td>Cow
                                                        </td>
                                                        <td>45
                                                        </td>
                                                        <td>60
                                                        </td>
                                                        <td>78
                                                        </td>
                                                        <td>76
                                                        </td>
                                                        <td>40
                                                        </td>
                                                        <td>Good
                                                        </td>
                                                        <td>2000
                                                        </td>
                                                        <td>
                                                            <p class="label label-success">Processed</p>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <input type="checkbox" id="chk1" />
                                                        </td>
                                                        <td>14/02/2019
                                                        </td>
                                                        <td>Morning
                                                        </td>
                                                        <td>Buffalo
                                                        </td>
                                                        <td>90
                                                        </td>
                                                        <td>54
                                                        </td>
                                                        <td>80
                                                        </td>
                                                        <td>60
                                                        </td>
                                                        <td>50
                                                        </td>
                                                        <td>Curd
                                                        </td>
                                                        <td>3000
                                                        </td>
                                                        <td>
                                                            <p class="label label-danger">Not Processed</p>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <input type="checkbox" id="chk1" />
                                                        </td>
                                                        <td>15/02/2019
                                                        </td>
                                                        <td>Evening
                                                        </td>
                                                        <td>Mix
                                                        </td>
                                                        <td>80
                                                        </td>
                                                        <td>80
                                                        </td>
                                                        <td>40
                                                        </td>
                                                        <td>70
                                                        </td>
                                                        <td>63
                                                        </td>
                                                        <td>Sour
                                                        </td>
                                                        <td>6000
                                                        </td>
                                                        <td>
                                                            <p class="label label-danger">Not Processed</p>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td colspan="9" style="text-align: right; font-weight: bold">Grand Total</td>
                                                        <td>11000</td>
                                                        <td></td>
                                                    </tr>

                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" class="btn btn-primary btn-block" ValidationGroup="a" ID="btnSave" Text="Pay" OnClientClick="return ValidatePage();" AccessKey="S" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
       <script type="text/javascript">
           function ValidatePage() {

               if (typeof (Page_ClientValidate) == 'function') {
                   Page_ClientValidate('a');
               }

               if (Page_IsValid) {

                   if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Update") {
                     document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                     $('#myModal').modal('show');
                     return false;
                 }
                 if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Save") {
                     document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                      $('#myModal').modal('show');
                      return false;
                  }
              }
          }
    </script>
</asp:Content>

