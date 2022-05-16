<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CFP_Item_PO_Generate.aspx.cs" Inherits="mis_CattleFeed_CFP_Item_PO_Generate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <style>
        .spaced input[type="radio"] {
            margin-left: 5px; /* Or any other value */
        }
    </style>
    <script type="text/javascript">
        function CalculateAmount() {
            debugger;
            var Quantity = document.getElementById('<%=txtItemQuantity.ClientID%>').value.trim();
            var Rate = document.getElementById('<%=txtItemRate.ClientID%>').value.trim();
            if (Quantity == "")
                Quantity = "0";
            if (Rate == "")
                Rate = "0";

            document.getElementById('<%=lblAmount.ClientID%>').value = (Quantity * Rate).toFixed(2);
            document.getElementById('<%=hdnamt.ClientID%>').value = (Quantity * Rate).toFixed(2);
        }
        function ShowReport() {
            $('#ReportModal').modal('show');
        }
        function Closeeport() {
            $('#ReportModal').modal('hide');
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
                                <div class="col-md-12" style="color: red; font-size: 15px;">
                                    नोट: जिस वस्तु की इकाई किलोग्राम में है उसकी quantity मेट्रिक टन में प्रविष्ट कर PO बनाये.
                                </div>
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
                                            <asp:DropDownList ID="ddlcfp" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlcfp_SelectedIndexChanged">
                                                <asp:ListItem Text="-- Select Prodution Unit --" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Item Category<span class="hindi">(श्रेणी)</span><span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlCategory" InitialValue="0" ErrorMessage="Please Select Item Category." Text="<i class='fa fa-exclamation-circle' title='Please Select Item Category !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                <asp:ListItem Text="-- Select Category --" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Item Type<span class="hindi">(प्रकार)</span><span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlType" InitialValue="0" ErrorMessage="Please Select Item Type." Text="<i class='fa fa-exclamation-circle' title='Please Select Item Type !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                                <asp:ListItem Text="-- Select Type --" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Items<span class="hindi">(वस्तु)</span><span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlItems" InitialValue="0" ErrorMessage="Please Select Item." Text="<i class='fa fa-exclamation-circle' title='Please Select Item !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlItems" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlItems_SelectedIndexChanged">
                                                <asp:ListItem Text="-- Select Items --" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">

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
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtVendorcontact" ErrorMessage="Please Enter Vendor." Text="<i class='fa fa-exclamation-circle' title='Please Enter Vendor !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox ID="txtVendorcontact" runat="server" onpaste="return false;" placeholder="Conatct No...." class="form-control" MaxLength="10" onkeypress="return onlyNumberKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>GSTN No<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtGSTN" ErrorMessage="Please Enter Vendor." Text="<i class='fa fa-exclamation-circle' title='Please Enter Vendor !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ValidationGroup="a"
                                                    ErrorMessage="Invalid GSTN" Text="<i class='fa fa-exclamation-circle' title='Invalid GSTN !'></i>"
                                                    ControlToValidate="txtGSTN" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^(?=.*[a-zA-Z])(?=.*[0-9])[A-Za-z0-9]+$">
                                                </asp:RegularExpressionValidator>
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
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Vendor Address</label>
                                            <span class="pull-right">
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ValidationGroup="a"
                                                    ErrorMessage="Invalid Address" Text="<i class='fa fa-exclamation-circle' title='Invalid Address !'></i>"
                                                    ControlToValidate="txtvendorAddress" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^[A-Za-z0-9? ,_-]+$">
                                                </asp:RegularExpressionValidator></span>
                                            <asp:TextBox ID="txtvendorAddress" runat="server" placeholder="Address...." class="form-control" MaxLength="200" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Unit<span class="hindi">(इकाई)</span><span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlUnit" InitialValue="0" ErrorMessage="Please Select Unit." Text="<i class='fa fa-exclamation-circle' title='Please Select Unit !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlUnit" Enabled="false" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Text="-- Select Unit --" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Tender/Telephonic Date<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtTender" ErrorMessage="Please Enter Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter Date !'></i>"></asp:RequiredFieldValidator>
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
                                </div>
                                <div class="col-md-12">

                                    <%-- <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Item Commodity<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtitemCommodity" ErrorMessage="Please Enter item Commodity." Text="<i class='fa fa-exclamation-circle' title='Please Enter iItem Commodity !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ValidationGroup="a"
                                                    ErrorMessage="Invalid Commodity" Text="<i class='fa fa-exclamation-circle' title='Invalid Commodity !'></i>"
                                                    ControlToValidate="txtitemCommodity" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^[A-Za-z0-9? ,_-]+$">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox ID="txtitemCommodity" runat="server" placeholder="Item Commodity...." class="form-control" MaxLength="150" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                        </div>
                                    </div>--%>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Item Commodity</label>
                                            <asp:TextBox ID="txtitemCommodity" runat="server" placeholder="Item Commodity...." class="form-control" MaxLength="300" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Item Quantity<span class="hindi">(मात्रा प्रति यूनिट)</span><span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtItemQuantity" ErrorMessage="Please Enter Vendor." Text="<i class='fa fa-exclamation-circle' title='Please Enter Vendor !'></i>"></asp:RequiredFieldValidator>
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
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtItemRate" ErrorMessage="Please Enter Item Rate." Text="<i class='fa fa-exclamation-circle' title='Please Enter Item Rate !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator runat="server" ValidationGroup="a" ErrorMessage="Decimal Only" Text="<i class='fa fa-exclamation-circle' title='Enter Rate !'></i>" ControlToValidate="txtItemRate"
                                                    ValidationExpression="^[1-9]\d*(\.\d+)?$" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox ID="txtItemRate" runat="server" placeholder="Item Rate...." class="form-control" MaxLength="10" onchange="CalculateAmount();"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>GST Included<span style="color: red;"> *</span></label>
                                            <asp:RadioButtonList runat="server" ID="rbtnGST" RepeatDirection="Horizontal" CellPadding="3" CellSpacing="2" CssClass="spaced">
                                                <asp:ListItem Text="Included" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Excluded" Value="0" Selected="True"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Amount<span class="hindi">(Rs.)</span><span style="color: red;"> *</span></label>

                                            <asp:TextBox ID="lblAmount" runat="server" Enabled="false" placeholder="Amount...." class="form-control" enable="false"></asp:TextBox>
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
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Remark</label>

                                        <asp:TextBox ID="txtRemark" runat="server" placeholder="Remark...." class="form-control" MaxLength="200" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>First Line Message</label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtfirstlinemssage" ErrorMessage="Enter First Line of message." Text="<i class='fa fa-exclamation-circle' title='Enter First Line of message.!'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox ID="txtfirstlinemssage" runat="server" placeholder="Message...." class="form-control" MaxLength="200"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Partial<span style="color: red;"> *</span></label>
                                            <asp:RadioButtonList runat="server" ID="rblpartial" CssClass="spaced" RepeatDirection="Horizontal" CellPadding="3" CellSpacing="2" AutoPostBack="true" OnSelectedIndexChanged="rblpartial_SelectedIndexChanged">
                                                <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-12" id="Firstinstallment" runat="server" visible="false">


                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>First Form Date  </label>

                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtfirstfromdt" onkeypress="javascript: return false;" Width="100%" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>First TO Date </label>
                                            <span style="color: red">*</span>

                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtfirstTOdt" onkeypress="javascript: return false;" Width="100%" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>First Item Quantity<span style="color: red;"> *</span></label>

                                           <%-- <asp:TextBox ID="txtfirstitemQuantity" runat="server" Text="0" placeholder="First item Quantity...." class="form-control" AutoPostBack="true" OnTextChanged="txtfirstitemQuantity_TextChanged"></asp:TextBox>--%>
                                             <asp:TextBox ID="txtfirstitemQuantity" runat="server" Text="0" placeholder="First item Quantity...." class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12" id="Secinstallment" runat="server" visible="false">


                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Second Form Date  </label>

                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtSecfromdt" onkeypress="javascript: return false;" Width="100%" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Second TO Date </label>

                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtSecTodt" onkeypress="javascript: return false;" Width="100%" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Sec Item Quantity<%--<span style="color: red;"> *</span>--%></label>

                                           <%-- <asp:TextBox ID="txtSecitemQuantity" runat="server" placeholder="Second item Quantity...." class="form-control" AutoPostBack="true" OnTextChanged="txtSecitemQuantity_TextChanged"></asp:TextBox>--%>
                                             <asp:TextBox ID="txtSecitemQuantity" runat="server" placeholder="Second item Quantity...." class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12" id="ThirdInstallment" runat="server" visible="false">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Third Form Date  </label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtThirdFromdt" onkeypress="javascript: return false;" Width="100%" onpaste="return false;" placeholder="Select Third From Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Second TO Date </label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtThirdTodt" onkeypress="javascript: return false;" Width="100%" onpaste="return false;" placeholder="Select Third To Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Third Item Quantity<%--<span style="color: red;"> *</span>--%></label>
                                            <%--<asp:TextBox ID="txtThirdItemQuantity" runat="server" placeholder="Third item Quantity...." class="form-control" AutoPostBack="true" OnTextChanged="TxtThirdItemQuan_TextChanged"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtThirdItemQuantity" runat="server" placeholder="Third item Quantity...." class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12" style="text-align: center;">

                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSave_Click" CausesValidation="true" ValidationGroup="a" />
                                    &nbsp;
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-success" OnClick="btnReset_Click" CausesValidation="false" />
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

                                    <div class="col-md-12">
                                        <asp:HiddenField ID="hdnamt" runat="server" Value="0" />
                                        <asp:HiddenField ID="hdnvalue" runat="server" Value="0" />

                                        <asp:GridView ID="grdCatlist" PageSize="20" runat="server" class="datatable table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" AllowPaging="True" OnRowCommand="grdCatlist_RowCommand" OnPageIndexChanging="grdCatlist_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkLock" runat="server" CausesValidation="False" CommandName="RecordLock" CommandArgument='<%#Eval("CFPITEMPurchaseOrderID") %>' Text="Lock" ToolTip="Lock" Visible='<%#!Convert.ToBoolean( Eval("Is_Lock")) %>' Style="color: red;" OnClientClick="return confirm('PO Entry will be locked. Are you sure want to continue?');"> <i class="fa fa-lock"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ItemPONO" HeaderText="PO No" />
                                                <asp:BoundField DataField="ItemName" HeaderText="Item Name (वस्तु का नाम)" />
                                                <asp:BoundField DataField="VendorName" HeaderText="Vendor Name (विक्रेता का नाम)" />
                                                <asp:BoundField DataField="ItemCommodity" HeaderText="Item Commodity" />
                                                <asp:BoundField DataField="ItemQuantity" HeaderText="Item Quantity" />
                                                <asp:BoundField DataField="ItemRate" HeaderText="Rate" />
                                                <asp:BoundField DataField="ItemPODate" HeaderText="PO Date" />
                                                <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LnkSelect" runat="server" CausesValidation="False" CommandName="RecordUpdate" CommandArgument='<%#Eval("CFPITEMPurchaseOrderID") %>' Text="Edit" ToolTip="Edit" Visible='<%#!Convert.ToBoolean( Eval("Is_Lock")) %>' OnClientClick="return confirm('CFP Entry will be edit. Are you sure want to continue?');"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LnkDelete" runat="server" CausesValidation="False" CommandName="RecordDelete" CommandArgument='<%#Eval("CFPITEMPurchaseOrderID") %>' Text="Delete" ToolTip="Delete" Visible='<%#!Convert.ToBoolean( Eval("Is_Lock")) %>' Style="color: red;" OnClientClick="return confirm('PO Entry will be deleted. Are you sure want to continue?');"> <i class="fa fa-trash"></i></asp:LinkButton>

                                                        &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" CommandArgument='<%#Eval("CFPITEMPurchaseOrderID") %>' CommandName="RecordReport" CausesValidation="false" runat="server" ToolTip="Report" Style="color: red;"><i class="fa fa-file"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
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
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body" id="printableArea">

                            <div class="row" style="text-align: center; margin-left: 5px;">
                                <asp:Panel ID="pnl" runat="server" BorderColor="Black" BorderWidth="2" Width="99%">
                                    <div>
                                        <table align='center' style="width: 100%" border='1'>

                                            <tr>

                                                <td style="text-align: left; padding: 3px;">
                                                    <%--<img src="/images/bdsnew_blacklogo.png" />--%>
                                                    <img src="../image/ds_logo_icon.png" style="border: 1" />
                                                </td>
                                                <td style="width: 90%; text-align: left; padding-top: 5px;">&nbsp;<b>पशुआहार संयंत्र, <span id="lblCFP" runat="server"></span>, जिला सीहोर</b><br />
                                                    <br />
                                                    &nbsp;<b>CATTLE FEED FACTORY, <span id="lblCFP1" runat="server"></span>Sehore 466001</b><br />
                                                    <br />
                                                    &nbsp;E-mail : cff.Pachama@gmail.com<br />
                                                    <br />
                                                    &nbsp;H.O.: Bhopal Sahkari Dugdh Sangh Maryadit Habibganj Bhopal (MP) 462024 <%--<span id="lblCFPAddress" runat="server">fdf</span>--%><br />
                                                    <br />
                                                    &nbsp;<b>Email: bsds@sanchar.net, <%--<span id="lblCFPEmail" runat="server"></span>--%></b>&nbsp;&nbsp;&nbsp;&nbsp;<b>GSTN NO: 23AAAAB0221D1ZW <%--<span id="lblCFPGSTN" runat="server"></span>--%></b>
                                                    <br />
                                                </td>

                                            </tr>

                                        </table>
                                        <table align='center' style="width: 100%" border='1'>
                                            <tr>
                                                <td class="td1" style="text-align: left; font-weight: bold; width: 175px;"><span id="lblPurchaseorder" runat="server">PURCHASE ORDER</span></td>
                                                <td class="td1" style="text-align: left; font-weight: bold;">Date: &nbsp;<span id="lblPurchaseorderdate" runat="server"></span>  </td>

                                            </tr>
                                            <tr>
                                                <td class="td1" style="text-align: left; font-weight: bold; width: 175px;">REF. NO: <span id="lblRefNo" runat="server">REF. NO</span></td>
                                                <td class="td1" style="text-align: left; font-weight: bold;">PACHAMA DATE: &nbsp;<span id="lblDate" runat="server"></span>  </td>

                                            </tr>
                                        </table>
                                        <table align='center' style="width: 100%" border='0'>
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
                                                <td>&nbsp;<span id="lblfirstmessage" runat="server"></span>This refers your offer-dated- <span id="lblTender" runat="server"></span>for the supply of raw material required for cattle feed factory at pachama .We confirm to purchase from you the following good, subject to terms and condition and specification mentioned in registration document.<br />
                                                    <br />
                                                    &nbsp;1. As lay dowm under the terms and condition the registration. Which you have accepted, this approval of rate will be considered as agreement between the two parties without further neecssity of executing separate agreement.<br />
                                                    <br />
                                                    &nbsp; 2. The below mentioned rates are approved on the basis of F.O.R Cattle Feed Factory pachama,Sehore inclusive of all expenese and taxes, terms and condition of registration, including specification, rate, schedule, duly accepted by you, shall be applicable and binding on the approved suppliers. Kindly arrange to supply the material as per the following schedule.<br />
                                                    <br />
                                                    &nbsp; 3. The excess/less quantity up to +/- 10% or maximum one truckload, Whichever is less shaff be acceptable<br />
                                                    <br />
                                                </td>
                                            </tr>

                                        </table>
                                        <table align='center' style="width: 100%" border='1'>
                                            <tr>
                                                <td class="thcss" style="width: 2%">S.No. </td>
                                                <td class="thcss" style="width: 10%">Name Of Items </td>
                                                <td class="thcss" style="width: 10%">Commodity</td>
                                                <td class="thcss" style="width: 10%">Rate Per MT </td>
                                                <td class="thcss" style="width: 10%">Qty.MT </td>
                                                <td class="thcss" style="width: 40%">schedule period</td>
                                            </tr>
                                            <tr>
                                                <td class="thcss" style="width: 2%">1. </td>
                                                <td class="thcss" style="width: 10%"><span id="lblItemName" runat="server"></span></td>
                                                <td class="thcss" style="width: 10%"><span id="lblCommodity" runat="server"></span></td>
                                                <td class="thcss" style="width: 10%"><span id="lblRate" runat="server"></span></td>
                                                <td class="thcss" style="width: 10%"><span id="lblQty" runat="server"></span></td>
                                                <td style="width: 40%">
                                                    <table align='center' style="width: 100%" border='1'>
                                                        <%-- <tr>
                                                            <td colspan="2">&nbsp;</td>
                                                        </tr>--%>
                                                        <tr>
                                                            <td style="padding: 5px;"><span id="lblfirstsp1dt" runat="server"></span><span id="Span2" runat="server"> To </span><span id="lblfirstsp1dt1" runat="server"></span>
                                                                <br />
                                                            </td>
                                                            <td style="padding: 5px;">
                                                                <span id="lblSecsp1dt1" runat="server"></span><span id="Span1" runat="server">  </span> To <span id="lblSecsp1dt2" runat="server"></span>
                                                                <br />
                                                            </td>
                                                            <td style="padding: 5px;">
                                                                <span id="lblThirdsp1dt1" runat="server"></span><span id="Span3" runat="server"> </span> To <span id="lblThirdsp1dt2" runat="server"></span>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px;"><span id="lblfirstQty" runat="server"></span></td>
                                                            <td style="padding: 5px;"><span id="lblSecQty" runat="server"></span></td>
                                                            <td style="padding: 5px;"><span id="lblThirdQty" runat="server"></span></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table align='center' style="width: 100%" border='0'>
                                            <tr>
                                                <td colspan="6">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="text-align: left"><b>Remark:-</b> माल का प्रदाय समय अवधि एवं सेडुल एवं गुणवत्ता माप दण्ड अनुसार ही करे | समय अवधि के पूर्व माल का प्रदाय न करे |</td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="text-align: left">All the concern suppliers: please supply the material strictly as per the supply schedule given against their name</td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="text-align: right">

                                                    <br />
                                                    <br />
                                                    I/c General Manager</td>
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

