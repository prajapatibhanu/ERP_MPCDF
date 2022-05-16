<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MilkOutWard.aspx.cs" Inherits="mis_MilkCollection_MilkOutWard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
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
                                <h3 class="box-title">Milk Processing Outward</h3>
                                <span id="ctl00_ContentBody_lblmsg"></span>
                            </div>
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-3">
                                       <div class="form-group">
                                            <label>Date<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="red" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtDate" ErrorMessage="Enter Date." Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" ForeColor="Red" runat="server" Display="Dynamic" ControlToValidate="txtDate" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtDate" onkeypress="return false;" runat="server" placeholder="dd/mm/yyyy" class="form-control" data-date-end-date="+7d" autocomplete="off" data-date-format="dd/mm/yyyy" data-date-autoclose="true" data-provide="datepicker" data-date-start-date="-365d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                        <label>Shift<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvShift" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlShift" InitialValue="0" ErrorMessage="Select Shift." Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Morning</asp:ListItem>
                                            <asp:ListItem>Evening</asp:ListItem>
                                            <asp:ListItem>Special</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    </div>
                                    <div class="col-md-3">
                                           <div class="form-group">
                                        <asp:Label ID="lblBatchNo" Text="Batch No" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvBatchNo" ValidationGroup="a"
                                                ErrorMessage="Enter Batch No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Batch No.!'></i>"
                                                ControlToValidate="txtBatchNo" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtBatchNo" MaxLength="150" placeholder="Enter Batch No" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                        <label>Milk Type<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvMilkType" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlMilkType" InitialValue="0" ErrorMessage="Select Milk Type." Text="<i class='fa fa-exclamation-circle' title='Select Milk Type !'></i>">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlMilkType" runat="server" CssClass="form-control select2">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>FCM</asp:ListItem>
                                            <asp:ListItem>TM</asp:ListItem>
                                         </asp:DropDownList>
                                    </div>
                                    </div>
                                    <div class="col-md-3">
                                          <div class="form-group">
                                        <label>Milk Variant<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvMilkVariant" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlMilkVariant" InitialValue="0" ErrorMessage="Select Milk Variant." Text="<i class='fa fa-exclamation-circle' title='Select Milk Variant !'></i>">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlMilkVariant" runat="server" CssClass="form-control select2">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>STD (500)</asp:ListItem>
                                            <asp:ListItem>DTM(500)</asp:ListItem>
                                               <asp:ListItem>DTM(200)</asp:ListItem>
                                               <asp:ListItem>DTM(500)</asp:ListItem>
                                               <asp:ListItem>Light 500</asp:ListItem>
                                               <asp:ListItem>FCM(500)</asp:ListItem>
                                               <asp:ListItem>TM (500)</asp:ListItem>
                                            <asp:ListItem>CCM(500)</asp:ListItem>
                                            <asp:ListItem>Chaha</asp:ListItem>
                                            <asp:ListItem>Chai Special</asp:ListItem>
                                         </asp:DropDownList>
                                    </div>
                                    </div>
                                    <div class="col-md-3">
                                          <div class="form-group">
                                        <asp:Label ID="lblPackets" Text="Packets(in No)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvPackets" ValidationGroup="a"
                                                ErrorMessage="Enter Packets(in No)." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Packets(in No).!'></i>"
                                                ControlToValidate="txtPackets" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtPackets" MaxLength="150" placeholder="Enter Packets(in No)" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    </div>
                                    <div class="col-md-3">
                                             <div class="form-group">
                                        <asp:Label ID="lblMilkQty" Text="Milk Qty (ltr)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvMilkQty" ValidationGroup="a"
                                                ErrorMessage="Enter Milk Qty (ltr)." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Qty (ltr).!'></i>"
                                                ControlToValidate="txtMilkQty" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtMilkQty" MaxLength="150" placeholder="Enter Milk Qty (ltr)" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    </div>
                                    <div class="col-md-3">
                                           <div class="form-group">
                                        <asp:Label ID="lblValue" Text="Value(Rs.)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvValue" ValidationGroup="a"
                                                ErrorMessage="Enter Value(Rs.)" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Value(Rs.). !'></i>"
                                                ControlToValidate="txtValue" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtValue" MaxLength="150" placeholder="Enter Value(Rs.)" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    </div>
                                </div>
                                  <div class="row">
                                <hr />
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-block btn-default" />
                                    </div>
                                </div>
                            </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="box box-primary">
                    <div class="box-header">
                        <h3 class="box-title">Milk Production Details </h3>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <div>
                                        <table class="datatable table table-striped table-bordered table-hover" cellspacing="0" rules="all" border="1" id="ctl00_ContentBody_gvOpeningStock" style="border-collapse:collapse;">
                                            <thead>
                                                <tr>
                                                    <th scope="col">S.No.<br /></th>
                                                    <th scope="col">Date <br /></th>
                                                    <th scope="col">Shift<br /></th>
                                                    <th scope="col">Batch No<br /></th>
                                                    <th scope="col">Milk Type<br /></th>
                                                    <th scope="col">Milk Variety<br /></th>
                                                    <th scope="col">Packets(in No)<br /></th>
                                                    <th scope="col">Milk Qty (ltr)<br /></th>
                                                    <th scope="col">Action <br /></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        1
                                                    </td>
                                                    <td>
                                                        28/01/2019
                                                    </td>
                                                    <td>
                                                        Morning
                                                    </td>
                                                    <td>
                                                        FCM0002
                                                    </td>
                                                    <td>
                                                        FCM
                                                    </td>
                                                    <td>
                                                        FCM (500)
                                                    </td>
                                                    <td>
                                                        450
                                                    </td>
                                                    <td>
                                                        225 ltr
                                                    </td>
                                                    <td>
                                                        <a id="ctl00_ContentBody_gvOpeningStock_ctl02_lnkUpdate" title="Update" href="javascript:__doPostBack(&#39;ctl00$ContentBody$gvOpeningStock$ctl02$lnkUpdate&#39;,&#39;&#39;)"><i class="fa fa-pencil"></i></a>
                                                        &nbsp;&nbsp;&nbsp;<a onclick="return confirm(&#39;Are you sure to Delete?&#39;);" id="ctl00_ContentBody_gvOpeningStock_ctl02_lnkDelete" title="Delete" href="javascript:__doPostBack(&#39;ctl00$ContentBody$gvOpeningStock$ctl02$lnkDelete&#39;,&#39;&#39;)" style="color: red;"><i class="fa fa-trash"></i></a>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        2
                                                    </td>
                                                    <td>
                                                        28/01/2019
                                                    </td>
                                                    <td>
                                                        Morning
                                                    </td>
                                                    <td>
                                                        FCM0009
                                                    </td>
                                                    <td>
                                                        STD
                                                    </td>
                                                    <td>
                                                        STD (500)
                                                    </td>
                                                    <td>
                                                        200
                                                    </td>
                                                    <td>
                                                        100 ltr
                                                    </td>
                                                    <td>
                                                        <a id="ctl00_ContentBody_gvOpeningStock_ctl02_lnkUpdate" title="Update" href="javascript:__doPostBack(&#39;ctl00$ContentBody$gvOpeningStock$ctl02$lnkUpdate&#39;,&#39;&#39;)"><i class="fa fa-pencil"></i></a>
                                                        &nbsp;&nbsp;&nbsp;<a onclick="return confirm(&#39;Are you sure to Delete?&#39;);" id="ctl00_ContentBody_gvOpeningStock_ctl02_lnkDelete" title="Delete" href="javascript:__doPostBack(&#39;ctl00$ContentBody$gvOpeningStock$ctl02$lnkDelete&#39;,&#39;&#39;)" style="color: red;"><i class="fa fa-trash"></i></a>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </section>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
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

