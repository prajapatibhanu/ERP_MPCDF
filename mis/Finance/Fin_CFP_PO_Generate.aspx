<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Fin_CFP_PO_Generate.aspx.cs" Inherits="mis_CattleFeed_Fin_CFP_PO_Generate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .mainborder {
            border: 1px solid #2f93d6;
            padding: 15px;
            margin-bottom: 15px;
        }
    </style>
    <script type="text/javascript">
        function ShowReport() {
            $('#ReportModal').modal('show');
        }
        function Closeeport() {
            $('#ReportModal').modal('hide');
        }
        function CalculateAmount() {
            debugger;
            var Quantity = document.getElementById('<%=txtItemQuantity.ClientID%>').value.trim();
            var Rate = document.getElementById('<%=txtItemRate.ClientID%>').value.trim();
            if (Quantity == "")
                Quantity = "0";
            if (Rate == "")
                Rate = "0";

            document.getElementById('<%=txtAmount.ClientID%>').value = (Quantity * Rate).toFixed(2);
            <%--document.getElementById('<%=hdnamt.ClientID%>').value = (Quantity * Rate).toFixed(2);--%>
        }

        function printDiv() {
            var divName = 'printableArea';
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <%--Confirmation Modal Start --%>

    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                        <h4 class="modal-title" id="myModalLabel">Confirmation</h4>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <p>
                            <i class="fa fa-2x fa-question-circle"></i>
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer" style="text-align: center;">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSave_Click" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">

                        <div class="box-header">
                            <h3 class="box-title">Purchase Order Generate (क्रय आदेश)</h3>
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Purchase Order Entry (क्रय आदेश की जानकारी प्रविष्टि)
                                </legend>
                                <%--  <div class="col-md-12" style="color: red; font-size: 15px;">
                                    नोट: जिस वस्तु की इकाई किलोग्राम में है उसकी quantity मेट्रिक टन में प्रविष्ट कर PO बनाये.
                                </div>--%>
                                <br />
                                <br />
                                <br />
                                <div class="col-md-12">
                                    <asp:Label ID="lblMsg" CssClass="Autoclr" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Production Unit<span class="hindi">(उत्पादन इकाई)</span><span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvpro" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlcfp" InitialValue="0" ErrorMessage="Please Select Product Name." Text="<i class='fa fa-exclamation-circle' title='Please Select Production Unit !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlcfp" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Text="-- Select Prodution Unit --" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>PO No.<span class="hindi">(प ओ न.)</span><span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlcfp" InitialValue="0" ErrorMessage="Please Select Product Name." Text="<i class='fa fa-exclamation-circle' title='Please Select Production Unit !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox ID="txtPONo" runat="server" MaxLength="30" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>PO Date  </label>
                                            <span style="color: red">*</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtPODt" ErrorMessage="Please Enter Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter Date !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revdate" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtPODt" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtPODt" onkeypress="javascript: return false;" Width="100%" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>PO End Date  </label>
                                            <span style="color: red">*</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtPOendDt" ErrorMessage="Please Enter Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter Date !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtPOendDt" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtPOendDt" onkeypress="javascript: return false;" Width="100%" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Reference No<span style="color: red;"> *</span></label>

                                            <asp:TextBox ID="txtReferenceNo" runat="server" placeholder="Reference No...." class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Tender/Telephonic Date<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtTender" ErrorMessage="Please Enter Tender/Telephonic Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter Tender/Telephonic Date !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtTender" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtTender" onkeypress="javascript: return false;" Width="100%" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Vendor Name<span class="hindi">(विक्रेता का नाम)</span><span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtVendor" ErrorMessage="Please Enter Vendor." Text="<i class='fa fa-exclamation-circle' title='Please Enter Vendor !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="a"
                                                    ErrorMessage="Invalid Name" Text="<i class='fa fa-exclamation-circle' title='Invalid Name !'></i>"
                                                    ControlToValidate="txtVendor" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^[A-Za-z0-9? ,_-]+$">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox ID="txtVendor" runat="server" placeholder="Vendor Name...." class="form-control" MaxLength="150"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Vendor Contact No<span class="hindi">(विक्रेता संपर्क नं)</span><span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtVendorcontact" ErrorMessage="Please Enter Vendor Contact No." Text="<i class='fa fa-exclamation-circle' title='Please Enter Vendor Contact No !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox ID="txtVendorcontact" runat="server" onpaste="return false;" placeholder="Conatct No...." class="form-control" MaxLength="10" onkeypress="return onlyNumberKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>GSTN No<%--<span style="color: red;"> *</span>--%></label>
                                            <span class="pull-right">
                                                <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtGSTN" ErrorMessage="Please Enter GSTN No." Text="<i class='fa fa-exclamation-circle' title='Please Enter GSTN No !'></i>"></asp:RequiredFieldValidator>--%>
                                                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator7" ValidationGroup="a"
                                                    ErrorMessage="Invalid GSTN" Text="<i class='fa fa-exclamation-circle' title='Invalid GSTN !'></i>"
                                                    ControlToValidate="txtGSTN" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^(?=.*[A-Z])(?=.*[0-9])[A-Z0-9]+$">
                                                </asp:RegularExpressionValidator>--%>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                                                    ForeColor="Red" ControlToValidate="txtGSTN"
                                                    ValidationExpression="^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$"
                                                    Display="Dynamic" ErrorMessage="Invalid GSTIN" ValidationGroup="a"
                                                    Text="<i class='fa fa-exclamation-circle' title='Invalid GSTIN !'></i>" />
                                            </span>
                                            <asp:TextBox ID="txtGSTN" runat="server" placeholder="GSTN No...." class="form-control" MaxLength="15"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Email Address<span class="hindi">(ईमेल)</span><span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtEmail" ErrorMessage="Please Email." Text="<i class='fa fa-exclamation-circle' title='Please Email !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a"
                                                    ErrorMessage="Invalid Email" Text="<i class='fa fa-exclamation-circle' title='Invalid Email !'></i>"
                                                    ControlToValidate="txtEmail" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox ID="txtEmail" runat="server" placeholder="Email...." class="form-control" MaxLength="150"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Vendor Address</label>
                                            <span class="pull-right">
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ValidationGroup="a"
                                                    ErrorMessage="Invalid Address" Text="<i class='fa fa-exclamation-circle' title='Invalid Address !'></i>"
                                                    ControlToValidate="txtvendorAddress" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^[A-Za-z0-9? ,_-]+$">
                                                </asp:RegularExpressionValidator></span>
                                            <asp:TextBox ID="txtvendorAddress" runat="server" placeholder="Address...." class="form-control" MaxLength="200"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-12 mainborder">
                                    <h4 style="color: blue">Item Details</h4>
                                    <asp:UpdatePanel ID="pnladddata" runat="server">
                                        <ContentTemplate>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Item Category<span class="hindi">(श्रेणी)</span><span style="color: red;"> *</span></label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ValidationGroup="b" runat="server" ControlToValidate="ddlCategory" InitialValue="0" ErrorMessage="Please Select Item Category." Text="<i class='fa fa-exclamation-circle' title='Please Select Item Category !'></i>"></asp:RequiredFieldValidator>
                                                    </span>
                                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                        <asp:ListItem Text="-- Select Category --" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Item Type<span class="hindi">(प्रकार)</span><span style="color: red;"> *</span></label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ValidationGroup="b" runat="server" ControlToValidate="ddlType" InitialValue="0" ErrorMessage="Please Select Item Type." Text="<i class='fa fa-exclamation-circle' title='Please Select Item Type !'></i>"></asp:RequiredFieldValidator>
                                                    </span>
                                                    <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                                        <asp:ListItem Text="-- Select Type --" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Items<span class="hindi">(वस्तु)</span><span style="color: red;"> *</span></label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" ValidationGroup="b" runat="server" ControlToValidate="ddlItems" InitialValue="0" ErrorMessage="Please Select Item." Text="<i class='fa fa-exclamation-circle' title='Please Select Item !'></i>"></asp:RequiredFieldValidator>
                                                    </span>
                                                    <asp:DropDownList ID="ddlItems" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text="-- Select Items --" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Unit<span class="hindi">(इकाई)</span><span style="color: red;"> *</span></label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" Display="Dynamic" ValidationGroup="b" runat="server" ControlToValidate="ddlItemUnit" InitialValue="0" ErrorMessage="Please Select Unit." Text="<i class='fa fa-exclamation-circle' title='Please Select Unit !'></i>"></asp:RequiredFieldValidator>
                                                    </span>
                                                    <asp:DropDownList ID="ddlItemUnit" runat="server" CssClass="form-control select2">
                                                        <asp:ListItem Text="-- Select Unit --" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Item Commodity</label>
                                                    <asp:TextBox ID="txtitemCommodity" runat="server" placeholder="Item Commodity...." class="form-control" MaxLength="300"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Item Quantity<span class="hindi">(मात्रा प्रति यूनिट)</span><span style="color: red;"> *</span></label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" Display="Dynamic" ValidationGroup="b" runat="server" ControlToValidate="txtItemQuantity" ErrorMessage="Please Enter Item Quantity." Text="<i class='fa fa-exclamation-circle' title='Please Enter Item Quantity !'></i>"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator runat="server" ValidationGroup="a" ErrorMessage="Decimal Only" Text="<i class='fa fa-exclamation-circle' title='Enter Quantity !'></i>" ControlToValidate="txtItemQuantity"
                                                            ValidationExpression="^[1-9]\d*(\.\d+)?$" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                                                    </span>
                                                    <asp:TextBox ID="txtItemQuantity" runat="server" placeholder="Item Quantity...." class="form-control" MaxLength="15" onchange="CalculateAmount();"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Item Rate<span style="color: red;"> *</span></label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" Display="Dynamic" ValidationGroup="b" runat="server" ControlToValidate="txtItemRate" ErrorMessage="Please Enter Item Rate." Text="<i class='fa fa-exclamation-circle' title='Please Enter Item Rate !'></i>"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator runat="server" ValidationGroup="b" ErrorMessage="Decimal Only" Text="<i class='fa fa-exclamation-circle' title='Enter Rate !'></i>" ControlToValidate="txtItemRate"
                                                            ValidationExpression="^[1-9]\d*(\.\d+)?$" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                                                    </span>
                                                    <asp:TextBox ID="txtItemRate" runat="server" placeholder="Item Rate...." class="form-control" MaxLength="10" onchange="CalculateAmount();"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Amount<span class="hindi">(Rs.)</span><span style="color: red;"> *</span></label>
                                                    <asp:TextBox ID="txtAmount" runat="server" placeholder="Amount...." class="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-md-3" style="margin-top: 25px;">
                                                <div class="form-group">
                                                    <asp:LinkButton ID="lnkAdd" OnClick="lnkAdd_Click" ClientIDMode="Static" CssClass="btn btn-info" Text="Add" ValidationGroup="b" runat="server"></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkClear" OnClick="lnkClear_Click" ClientIDMode="Static" CssClass="btn btn-danger" Text="Clear" ValidationGroup="b" runat="server"></asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="table table-responsive">
                                                    <asp:GridView ID="GridView1" AutoGenerateColumns="false" OnRowCommand="GridView1_RowCommand"
                                                        CssClass="table table-bordered table-hover" runat="server">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5px">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ItemCategory">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItemCat" runat="server" Text='<%# Eval("ItemCat") %>'></asp:Label>
                                                                    <asp:Label ID="lblItemCat_id" Visible="false" runat="server" Text='<%# Eval("ItemCat_id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItemType" runat="server" Text='<%# Eval("ItemType") %>'></asp:Label>
                                                                    <asp:Label ID="lblItemType_id" Visible="false" runat="server" Text='<%# Eval("ItemType_id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Items">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItem_Name" runat="server" Text='<%# Eval("Item_Name") %>'></asp:Label>
                                                                    <asp:Label ID="lblItem_id" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Unit">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItemUnit_Name" runat="server" Text='<%# Eval("ItemUnit_Name") %>'></asp:Label>
                                                                    <asp:Label ID="lblItemUnit_id" Visible="false" runat="server" Text='<%# Eval("ItemUnit_id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item Commodity">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItem_Commodity" runat="server" Text='<%# Eval("Item_Commodity") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item Quantity">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItem_Quantity" runat="server" Text='<%# Eval("Item_Quantity") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item Rate">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItem_Rate" runat="server" Text='<%# Eval("Item_Rate") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Actions">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" CommandName="RowDelete" CssClass="btn btn-danger" runat="server"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>

                                                    <asp:GridView ID="GridView2" DataKeyNames="CFP_Purchase_Order_ChildID" AutoGenerateColumns="false" OnRowCommand="GridView2_RowCommand"
                                                        CssClass="table table-bordered table-hover" runat="server">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5px">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ItemCategory">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ItemCatName") %>'></asp:Label>
                                                                    <asp:Label ID="lblItemCat_id" Visible="false" runat="server" Text='<%# Eval("ItemCat_id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("ItemTypeName") %>'></asp:Label>
                                                                    <asp:Label ID="lblItemType_id" Visible="false" runat="server" Text='<%# Eval("ItemType_id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Items">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                    <asp:Label ID="lblItem_id" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Unit">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("UnitName") %>'></asp:Label>
                                                                    <asp:Label ID="lblItemUnit_id" Visible="false" runat="server" Text='<%# Eval("ItemUnit_id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item Commodity">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItem_Commodity" runat="server" Text='<%# Eval("Item_Commodity") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item Quantity">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItem_Quantity" runat="server" Text='<%# Eval("Item_Quantity") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item Rate">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItem_Rate" runat="server" Text='<%# Eval("Item_Rate") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Actions">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="RecordUpdate" CommandArgument='<%#Eval("CFP_Purchase_Order_ChildID") %>' Text="Edit" ToolTip="Edit" OnClientClick="return confirm('CFP Item Entry will be edit. Are you sure want to continue?');"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CommandName="RecordDelete" CommandArgument='<%#Eval("CFP_Purchase_Order_ChildID") %>' Text="Delete" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('PO Item Entry will be deleted. Are you sure want to continue?');"> <i class="fa fa-trash"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Remark</label>

                                        <asp:TextBox ID="txtRemark" runat="server" placeholder="Remark...." class="form-control" MaxLength="200"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Supply Scheduling</label>

                                        <asp:TextBox ID="txtSupplyScheduling" runat="server" placeholder="Supply Scheduling...." class="form-control" MaxLength="200"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12" style="text-align: center;">

                                    <asp:Button ID="btnSave" runat="server" OnClientClick="return ValidatePage();" Text="Save" CssClass="btn btn-success" CausesValidation="true" ValidationGroup="a" />
                                    &nbsp;
                                    <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Reset" CssClass="btn btn-success" CausesValidation="false" />
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-body">
                            <fieldset>
                                <legend>PURCHASE ORDER Detail (प्रविष्टित वस्तु की सूची )
                                </legend>
                                <div class="row">
                                    <asp:GridView ID="Gridview3" DataKeyNames="CFP_Purchase_Order_ID" PageSize="20" runat="server" class="datatable table table-hover table-bordered pagination-ys"
                                        AutoGenerateColumns="False" OnRowCommand="Gridview3_RowCommand" OnPageIndexChanging="Gridview3_PageIndexChanging" AllowPaging="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkLock" runat="server" CausesValidation="False" CommandName="RecordLock" CommandArgument='<%#Eval("CFP_Purchase_Order_ID") %>' Text="Lock" ToolTip="Lock" Visible='<%#!Convert.ToBoolean( Eval("Is_Lock")) %>' Style="color: red;" OnClientClick="return confirm('PO Entry will be locked. Are you sure want to continue?');"> <i class="fa fa-lock"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PO No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPO_NO" Text='<%# Eval("PO_NO")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PO Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPO_Date" Text='<%# Eval("PO_Date")%>' runat="server" />
                                                    <asp:Label ID="lblPO_End_Date" Visible="false" Text='<%# Eval("PO_End_Date")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tender Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTenderDate" Text='<%# Eval("TenderDate")%>' runat="server" />
                                                    <asp:Label ID="lblSupplyScheduling" Visible="false" Text='<%# Eval("SupplyScheduling")%>' runat="server" />
                                                    <asp:Label ID="lblRemark" Visible="false" Text='<%# Eval("Remark")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vendor Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVendorName" Text='<%# Eval("VendorName")%>' runat="server" />
                                                    <asp:Label ID="lblVendorAddress" Visible="false" Text='<%# Eval("VendorAddress")%>' runat="server" />
                                                    <asp:Label ID="lblVendorContactNo" Visible="false" Text='<%# Eval("VendorContactNo")%>' runat="server" />
                                                    <asp:Label ID="lblGSTNNO" Visible="false" Text='<%# Eval("GSTNNO")%>' runat="server" />
                                                    <asp:Label ID="lblEmailAddress" Visible="false" Text='<%# Eval("EmailAddress")%>' runat="server" />
                                                    <asp:Label ID="lblReference_NO" Visible="false" Text='<%# Eval("Reference_NO")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LnkSelect" runat="server" CausesValidation="False" CommandName="RecordUpdate" CommandArgument='<%#Eval("CFP_Purchase_Order_ID") %>' Text="Edit" ToolTip="Edit" Visible='<%#!Convert.ToBoolean( Eval("Is_Lock")) %>' OnClientClick="return confirm('CFP Entry will be edit. Are you sure want to continue?');"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="LnkDelete" runat="server" CausesValidation="False" CommandName="RecordDelete" CommandArgument='<%#Eval("CFP_Purchase_Order_ID") %>' Text="Delete" ToolTip="Delete" Visible='<%#!Convert.ToBoolean( Eval("Is_Lock")) %>' Style="color: red;" OnClientClick="return confirm('PO Entry will be deleted. Are you sure want to continue?');"> <i class="fa fa-trash"></i></asp:LinkButton>

                                                    &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" CommandArgument='<%#Eval("CFP_Purchase_Order_ID") %>' CommandName="RecordReport" CausesValidation="false" runat="server" ToolTip="Report" Style="color: red;"><i class="fa fa-file"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="ReportModal" tabindex="-1" role="dialog" aria-labelledby="ReportModal" aria-hidden="true">
                <div class="modal-dialog" style="height: 100%; width: 55%;">

                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <span style="color: white">Invoice Details</span>
                            <%--<button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                    </button>--%>
                        </div>
                        <div class="modal-body" id="printableArea">

                            <div class="row" style="text-align: center; margin-left: 5px;">
                                <asp:Panel ID="pnl" runat="server" BorderColor="Black" BorderWidth="2" Width="99%">
                                    <div>
                                        <table align='center' style="width: 100%" border='1'>

                                            <tr>

                                                <td style="text-align: left; padding: 3px;">
                                                    <img src="../../mis/image/bdsnew_blacklogo.png" style="width: 100px;" />
                                                    <%--  <img src="../image/ds_logo_icon.png" style="border: 1" />--%>
                                                </td>
                                                <td style="text-align: left; padding-top: 5px;">&nbsp;<b>पशुआहार संयंत्र, <span id="lblCFP" runat="server"></span>, <br /><span id="lblCFPAddress" runat="server"></span><%--जिला सीहोर--%></b><br />
                                                    <br />
                                                    &nbsp;<b><%--CATTLE FEED FACTORY,--%> <span id="lblCFP1" runat="server"></span><span id="lblCFPAddress1" runat="server"></span><span id="lblCFPincode" runat="server"></span><%--Sehore 466001--%></b><br />
                                                    <br />
                                                    &nbsp;E-mail : <span id="lblCFPEmail" runat="server"></span><%--cff.Pachama@gmail.com--%><br />
                                                    <br />
                                                    &nbsp;H.O.: <span id="lblCFPName_HO" runat="server"></span><span id="lblCFPAddress_HO" runat="server"></span><span id="lblCFPPincode_HO" runat="server"></span><%--Bhopal Sahkari Dugdh Sangh Maryadit Habibganj Bhopal (MP) 462024--%> <%--<span id="lblCFPAddress" runat="server">fdf</span>--%><br />
                                                    <br />
                                                    &nbsp;<b>Email: <%--bsds@sanchar.net--%> <span id="lblCFPEmailHO" runat="server"></span></b>&nbsp;&nbsp;&nbsp;&nbsp;<b>GSTN NO: <%--23AAAAB0221D1ZW--%> <span id="lblCFPGSTN" runat="server"></span></b>
                                                    <br />
                                                </td>
                                                <td style="width: 150px; text-align: right; padding-right: 2%;">
                                                    <asp:PlaceHolder ID="PlaceHolderQRCode" runat="server"></asp:PlaceHolder>
                                                </td>
                                            </tr>

                                        </table>
                                        <table align='center' style="width: 100%" border='1'>
                                            <tr>
                                                <td class="td1" style="text-align: left; font-weight: bold; width: 175px; height: 30px;">PO No.:<span id="lblPurchaseorder" runat="server">PURCHASE ORDER</span></td>
                                                <td class="td1" style="text-align: right; font-weight: bold; height: 30px;">PO Date: &nbsp;<span id="lblPurchaseorderdate" runat="server"></span>  </td>

                                            </tr>
                                            <tr>
                                                <td class="td1" style="text-align: left; font-weight: bold; width: 175px; height: 30px;">REF. NO: <span id="lblRefNo" runat="server">REF. NO</span></td>
                                                <td class="td1" style="text-align: left; font-weight: bold; height: 30px;"><%--DATE: &nbsp;<span id="lblDate" runat="server"></span>--%>  </td>

                                            </tr>
                                        </table>
                                        <table align='center' style="width: 100%" border='0'>
                                            <tr>
                                                <td style="text-align: center; width: 175px; padding-top: 20px;"><u><b>Purchase Order</b></u></td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left; width: 175px; padding-top: 3px;">To ,<br />
                                                    <br />
                                                    &nbsp; &nbsp; &nbsp; M/S <span id="lblVendor" runat="server"></span>
                                                    <br />
                                                    &nbsp; &nbsp; &nbsp; <span id="lblVendorAddress" runat="server"></span>
                                                    <br />
                                                    &nbsp; &nbsp; &nbsp; Email: <span id="lblEmail" runat="server"></span>
                                                    <br />
                                                    &nbsp; &nbsp; &nbsp; GSTN No: <span id="lblGSTNNO" runat="server"></span>
                                                    <br />
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr style="text-align: left; text-align: justify">
                                                <%--<td>&nbsp;<span id="lblfirstmessage" runat="server"></span>This refers your offer-dated- <span id="lblTender" runat="server"></span>for the supply of raw material required for cattle feed factory at pachama .We confirm to purchase from you the following good, subject to terms and condition and specification mentioned in registration document.<br />--%>
                                                <td><%--&nbsp;<span id="lblfirstmessage" runat="server"></span>--%>Purchase Order is beging placed herewith for supply of raw materials at
                                                    at  <span id="lblCFP2" runat="server"></span>against the NCDFI e-Market rates offer received on
                                                     <span id="lblTender" runat="server"></span>Following conditions shall be applcable.<br />
                                                    <br />
                                                    &nbsp;1. As laid dowm under the terms and conditions of the tender, which have been accepted by you. The approval of rates
                                                     will be counted as valid executed agreement between the two parties with out further neecssity of executing separate agreement.<br />
                                                    <br />
                                                    &nbsp; 2. The below mentioned rates are approved FOR <span id="lblCFP3" runat="server"></span>including all expeneses and terms & conditions of tender including specification, rebate, 
                                                    schedule, duly accepted by you shall be applicable and binding on all approved suppliers.<br />
                                                    <br />
                                                    &nbsp; Kindly arrange to supply the following material as per the following schedule in working days.
                                                    <br />
                                                    <br />
                                                    <%-- &nbsp; 2 The excess/less quantity up to +/- 10% or maximum one truckload, Whichever is less shaff be acceptable<br />
                                                    <br />--%>
                                                </td>
                                            </tr>

                                        </table>
                                        <div id="div_page_content" runat="server"></div>
                                        <table align='center' style="width: 100%" border='0'>
                                            <tr>
                                                <td colspan="6">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="text-align: left"><b>Remark:-</b> <span id="remark" runat="server"></span><%--माल का प्रदाय समय अवधि एवं सेडुल एवं गुणवत्ता माप दण्ड अनुसार ही करे | समय अवधि के पूर्व माल का प्रदाय न करे |--%></td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <%--<td colspan="6" style="text-align: left">All the concern suppliers: please supply the material strictly as per the supply schedule given against their name</td>--%>
                                                <td colspan="6" style="text-align: left"><b>Supply Scheduling :-</b> <span id="supplyscheduleing" runat="server"></span></td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="text-align: right">

                                                    <br />
                                                    <br />
                                                    General Manager</td>
                                            </tr>
                                        </table>

                                    </div>

                                </asp:Panel>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" CssClass="btn btn-default" ID="btnclose" OnClientClick="Closeeport();" CausesValidation="false" Text="Close" />
                            <asp:Button runat="server" CssClass="btn btn-primary" ID="btnprint" OnClientClick="printDiv();" CausesValidation="false" Text="Print" />
                        </div>
                    </div>

                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
        function ValidatePage() {
            debugger;
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }
            if (Page_IsValid) {

                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
                     document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                     $('#myModal').modal('show');
                     return false;
                 }
                 if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                     document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
    </script>
    <style type='text/css'>
        .td1 {
            border: none;
            line-height: 15px;
            font-size: 12px;
        }

        .heading {
            line-height: 10px;
            border-spacing: 0px;
            padding: 0px;
        }

        .medicinlDetail {
            width: 100%;
            border-collapse: collapse;
        }

            .medicinlDetail tr td {
                line-height: 0.9;
                border: 1px solid black;
            }

        td {
            line-height: 12px;
            text-align: initial;
        }

        .thcss {
            text-align: center;
            font-weight: bold;
            font-size: 14px;
            border: 1px solid black;
        }

        .tdcss {
            border: 1px solid black;
            padding: 3px;
        }

        element.style {
            text-align: justify;
            margin-left: 299px;
            margin-right: 299px;
        }
    </style>
    <script>

        function onlyNumberKey(evt) {

            // Only ASCII charactar in that range allowed 
            var ASCIICode = (evt.which) ? evt.which : evt.keyCode
            if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57))
                return false;
            return true;
        }
    </script>
</asp:Content>

