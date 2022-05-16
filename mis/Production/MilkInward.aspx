<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MilkInward.aspx.cs" Inherits="mis_MilkCollection_MilkInward" %>

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
                                <h3 class="box-title">Milk Processing Inward</h3>
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
                                        <asp:Label ID="lblOpeningQuantity" Text="Opening Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvOpeningQuantity" ValidationGroup="a"
                                                ErrorMessage="Enter Opening Quantity." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Opening Quantity.!'></i>"
                                                ControlToValidate="txtOpeningQuantity" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOpeningQuantity" MaxLength="150" placeholder="Enter Opening Quantity" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    </div>
                                    <div class="col-md-3">
                                          <div class="form-group">
                                        <label>Milk Quality<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvMilkQuality" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlMilkQuality" InitialValue="0" ErrorMessage="Select Milk Quality." Text="<i class='fa fa-exclamation-circle' title='Select Milk Quality !'></i>">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlMilkQuality" runat="server" CssClass="form-control select2">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Good</asp:ListItem>
                                            <asp:ListItem>sour</asp:ListItem>
                                            <asp:ListItem>curdle</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    </div>
                                     <div class="col-md-3">
                                          <div class="form-group">
                                        <asp:Label ID="lblIssued" Text="Issued to Milk Process Section" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvIssued" ValidationGroup="a"
                                                ErrorMessage="Enter Issued to Milk Process Section." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Issued to Milk Process Section.!'></i>"
                                                ControlToValidate="txtIssued" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtIssued" MaxLength="150" placeholder="Enter Issued to Milk Process Section" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    </div>
                                    <div class="col-md-3">
                                          <div class="form-group">
                                        <asp:Label ID="lblIssuedSection" Text="Issued to Product Section<" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvIssuedProductSection" ValidationGroup="a"
                                                ErrorMessage="Enter Issued to Product Section." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Issued to Product Section.!'></i>"
                                                ControlToValidate="txtIssuedProductSection" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtIssuedProductSection" MaxLength="150" placeholder="Enter Issued to Product Section" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    </div>
                                    <div class="col-md-3">
                                            <div class="form-group">
                                        <asp:Label ID="lblIssuedCheckSection" Text="Issued to Quality Check Section<" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvIssuedQualityCheckSection" ValidationGroup="a"
                                                ErrorMessage="Enter Issued to Quality Check Section." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Issued to Quality Check Section.!'></i>"
                                                ControlToValidate="txtIssuedCheckSection" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtIssuedCheckSection" MaxLength="150" placeholder="Enter Issued to Quality Check Section" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    </div>
                                    <div class="col-md-3">
                                          <div class="form-group">
                                        <asp:Label ID="lblLossesQuantity" Text="Losses Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvLossesQuantity" ValidationGroup="a"
                                                ErrorMessage="Enter Losses Quantity." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Losses Quantity<.!'></i>"
                                                ControlToValidate="txtLossesQuantity" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtLossesQuantity" MaxLength="150" placeholder="Enter  Losses Quantity" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    </div>
                                    <div class="col-md-3">
                                           <div class="form-group">
                                        <asp:Label ID="lblClosingQuantity" Text="Closing Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvClosingQuantity" ValidationGroup="a"
                                                ErrorMessage="Enter Closing Quantity." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Closing Quantity<.!'></i>"
                                                ControlToValidate="txtClosingQuantity" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtClosingQuantity" MaxLength="150" placeholder="Enter Closing Quantity" ClientIDMode="Static"></asp:TextBox>
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
                        <h3 class="box-title">Milk Processing Details </h3>
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
                                                    <th scope="col">Opening Quantity<br /></th>
                                                    <th scope="col">Issued to Milk Production Section<br /></th>
                                                    <th scope="col">Issued to Product Section<br /></th>
                                                    <th scope="col">Losses Quantity</th>
                                                    <th scope="col">Closing Quantity <br /></th>
                                                    <th>Actions</th>
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
                                                        1500 Ltr
                                                    </td>
                                                    <td>
                                                        1000 Ltr
                                                    </td>
                                                    <td>
                                                       500 Ltr
                                                    </td>
                                                    <td>
                                                        0 Ltr
                                                    </td>
                                                    <td>
                                                        0 Ltr
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
                                                        Evening
                                                    </td>
                                                    <td>
                                                        1200 Ltr
                                                    </td>
                                                    <td>
                                                       800 Ltr
                                                    </td>
                                                    <td>
                                                        300 Ltr
                                                    </td>
                                                    <td>
                                                        0 Ltr
                                                    </td>
                                                    <td>
                                                        100 Ltr
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

