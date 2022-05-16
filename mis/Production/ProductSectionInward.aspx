<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ProductSectionInward.aspx.cs" Inherits="mis_MilkCollection_ProductSectionInward" %>

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
                            <h3 class="box-title">Milk Product Processing - Inward</h3>
                            <span id="ctl00_ContentBody_lblmsg"></span>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
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
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Product Section Name<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvProductSectionName" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlProductSectionName" InitialValue="0" ErrorMessage="Select Product Section Name." Text="<i class='fa fa-exclamation-circle' title='Select Product Section Name !'></i>">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlProductSectionName" runat="server" CssClass="form-control select2">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Plain Curd Section</asp:ListItem>
                                            <asp:ListItem>Sweet Curd Section</asp:ListItem>
                                            <asp:ListItem>Ghee Section</asp:ListItem>
                                            <asp:ListItem>Butter Section</asp:ListItem>
                                            <asp:ListItem>Shreekhand Section</asp:ListItem>
                                            <asp:ListItem>Peda Section</asp:ListItem>
                                            <asp:ListItem>Flavoured Milk Section</asp:ListItem>
                                            <asp:ListItem>lassy Section</asp:ListItem>
                                            <asp:ListItem>Milk Cake Section</asp:ListItem>
                                            <asp:ListItem>Casein Section</asp:ListItem>
                                            <asp:ListItem>Mawa Section</asp:ListItem>
                                            <asp:ListItem>Paneer Section</asp:ListItem>
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
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
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="lblPreparedQuantity" Text="Prepared Quantity(Pkt)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvPreparedQuantity" ValidationGroup="a"
                                                ErrorMessage="Enter Prepared Quantity." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Prepared Quantity.!'></i>"
                                                ControlToValidate="txtPreparedQuantity" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtPreparedQuantity" MaxLength="150" placeholder="Enter Prepared Quantity" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="lblIssuedQualityCheckSection" Text="Issued to Quality Check Section(Pkt)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvIssuedQualityCheckSection" ValidationGroup="a"
                                                ErrorMessage="Enter Issued to Quality Check Section(Pkt)" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Issued to Quality Check Section(Pkt).!'></i>"
                                                ControlToValidate="txtIssuedQualitySection" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtIssuedQualitySection" MaxLength="150" placeholder="Enter Issued to Quality Check Section(Pkt)" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="lblLosses" Text="Losses(kg)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvLosses" ValidationGroup="a"
                                                ErrorMessage="Enter Losses(kg)" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Losses(kg).!'></i>"
                                                ControlToValidate="txtLosses" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtLosses" MaxLength="150" placeholder="Enter Losses(kg)" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="lblDiscardedQuantity" Text="Discarded Quantity(kg)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvDiscardedQuantity" ValidationGroup="a"
                                                ErrorMessage="Enter Prepared Quantity." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Discarded Quantity.!'></i>"
                                                ControlToValidate="txtDiscardedQuantity" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDiscardedQuantity" MaxLength="150" placeholder="Enter Discarded Quantity" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="lblAvailableQuantity" Text="Available Quantity(kg)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvAvailableQuantity" ValidationGroup="a"
                                                ErrorMessage="Enter Available Quantity." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Available Quantity.!'></i>"
                                                ControlToValidate="txtAvailableQuantity" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtAvailableQuantity" MaxLength="150" placeholder="Enter Available Quantity" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Add" OnClientClick="return ValidatePage();" AccessKey="S" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <div>
                                            <table class="datatable table table-striped table-bordered table-hover" cellspacing="0" rules="all" border="1" id="ctl00_ContentBody_gvOpeningStock" style="border-collapse: collapse;">
                                                <thead>
                                                    <tr>
                                                        <th scope="col">S.No.<br />
                                                        </th>

                                                        <th scope="col">Product
                                                            <br />
                                                            Section Name<br />
                                                        </th>
                                                        <th scope="col">Opening
                                                            <br />
                                                            Quantity(kg)<br />
                                                        </th>
                                                        <th scope="col">Prepared
                                                            <br />
                                                            Quantity(Pkt)<br />
                                                        </th>
                                                        <th scope="col">Issued to Quality
                                                            <br />
                                                            Check Section(Pkt)<br />
                                                        </th>
                                                        <th scope="col">Losses(kg)<br />
                                                        </th>
                                                        <th scope="col">Discarded
                                                            <br />
                                                            Quantity(kg)<br />
                                                        </th>
                                                        <th scope="col">Available
                                                            <br />
                                                            Quantity(kg)<br />
                                                        </th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>1
                                                        </td>

                                                        <td>Sweet Curd Section
                                                        </td>
                                                        <td>50
                                                        </td>
                                                        <td>60
                                                        </td>
                                                        <td>40
                                                        </td>
                                                        <td>04
                                                        </td>
                                                        <td>30
                                                        </td>
                                                        <td>10
                                                        </td>

                                                    </tr>

                                                    <tr>
                                                        <td>2
                                                        </td>

                                                        <td>Paneer Section
                                                        </td>
                                                        <td>60
                                                        </td>
                                                        <td>909
                                                        </td>
                                                        <td>305
                                                        </td>
                                                        <td>350
                                                        </td>
                                                        <td>460
                                                        </td>
                                                        <td>560
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td>3
                                                        </td>

                                                        <td>Shreekhand Section
                                                        </td>
                                                        <td>506
                                                        </td>
                                                        <td>670
                                                        </td>
                                                        <td>490
                                                        </td>
                                                        <td>204
                                                        </td>
                                                        <td>305
                                                        </td>
                                                        <td>106
                                                        </td>

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
                                        <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="Button1" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
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
                    <h3 class="box-title">Processing  Plant (Milk-Section Inward) </h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <div>
                                    <table class="datatable table table-striped table-bordered table-hover" cellspacing="0" rules="all" border="1" id="ctl00_ContentBody_gvOpeningStock" style="border-collapse: collapse;">
                                        <thead>
                                            <tr>
                                                <th scope="col">S.No.<br />
                                                </th>
                                                <th scope="col">Date
                                                    <br />
                                                </th>
                                                <th scope="col">Product
                                                    <br />
                                                    Section Name<br />
                                                </th>
                                                <th scope="col">Opening
                                                    <br />
                                                    Quantity(kg)<br />
                                                </th>
                                                <th scope="col">Prepared
                                                    <br />
                                                    Quantity(Pkt)<br />
                                                </th>
                                                <th scope="col">Issued to Quality
                                                    <br />
                                                    Check Section(Pkt)<br />
                                                </th>
                                                <th scope="col">Losses(kg)<br />
                                                </th>
                                                <th scope="col">Discarded
                                                    <br />
                                                    Quantity(kg)<br />
                                                </th>
                                                <th scope="col">Returned
                                                    <br />
                                                    Quantity(kg)<br />
                                                </th>

                                                <th scope="col">Action
                                                    <br />
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>1
                                                </td>
                                                <td>28/01/2019
                                                </td>
                                                <td>Sweet Curd Section
                                                </td>
                                                <td>50
                                                </td>
                                                <td>60
                                                </td>
                                                <td>40
                                                </td>
                                                <td>04
                                                </td>
                                                <td>30
                                                </td>
                                                <td>10
                                                </td>
                                                <td>
                                                    <a id="ctl00_ContentBody_gvOpeningStock_ctl02_lnkUpdate" title="Update" href="javascript:__doPostBack(&#39;ctl00$ContentBody$gvOpeningStock$ctl02$lnkUpdate&#39;,&#39;&#39;)"><i class="fa fa-pencil"></i></a>
                                                    &nbsp;&nbsp;&nbsp;<a onclick="return confirm(&#39;Are you sure to Delete?&#39;);" id="ctl00_ContentBody_gvOpeningStock_ctl02_lnkDelete" title="Delete" href="javascript:__doPostBack(&#39;ctl00$ContentBody$gvOpeningStock$ctl02$lnkDelete&#39;,&#39;&#39;)" style="color: red;"><i class="fa fa-trash"></i></a>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>2
                                                </td>
                                                <td>28/01/2019
                                                </td>
                                                <td>Paneer Section
                                                </td>
                                                <td>60
                                                </td>
                                                <td>909
                                                </td>
                                                <td>305
                                                </td>
                                                <td>350
                                                </td>
                                                <td>460
                                                </td>
                                                <td>560
                                                </td>
                                                <td>
                                                    <a id="ctl00_ContentBody_gvOpeningStock_ctl02_lnkUpdate" title="Update" href="javascript:__doPostBack(&#39;ctl00$ContentBody$gvOpeningStock$ctl02$lnkUpdate&#39;,&#39;&#39;)"><i class="fa fa-pencil"></i></a>
                                                    &nbsp;&nbsp;&nbsp;<a onclick="return confirm(&#39;Are you sure to Delete?&#39;);" id="ctl00_ContentBody_gvOpeningStock_ctl02_lnkDelete" title="Delete" href="javascript:__doPostBack(&#39;ctl00$ContentBody$gvOpeningStock$ctl02$lnkDelete&#39;,&#39;&#39;)" style="color: red;"><i class="fa fa-trash"></i></a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>3
                                                </td>
                                                <td>28/01/2019
                                                </td>
                                                <td>Shreekhand Section
                                                </td>
                                                <td>506
                                                </td>
                                                <td>670
                                                </td>
                                                <td>490
                                                </td>
                                                <td>204
                                                </td>
                                                <td>305
                                                </td>
                                                <td>106
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

