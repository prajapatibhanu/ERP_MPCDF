<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MaterialIssue.aspx.cs" Inherits="mis_CattleFeed_MaterialIssue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <script type="text/javascript">
        function CalculateAmount() {
            debugger;
            var Quantity = document.getElementById('<%=txtIssue.ClientID%>').value.trim();
            var Rate = document.getElementById('<%=txtRate.ClientID%>').value.trim();
            if (Quantity == "")
                Quantity = "0";
            if (Rate == "")
                Rate = "0";

            document.getElementById('<%=txtAmount.ClientID%>').value = (Quantity * Rate).toFixed(2);
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
                            <h3 class="box-title">Material Issue (सामग्री जारी करें)</h3>
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Material Issue (जारी किये गए सामग्री की प्रविष्टी)
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Label ID="lblMsg" CssClass="Autoclr" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Issue Date (दिनांक) </label>
                                                <span style="color: red">*</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ValidationGroup="vgdmissue" Display="Dynamic" ControlToValidate="txtTransactionDt" ErrorMessage="Please Enter Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter Date !'></i>"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="revdate" ValidationGroup="vgdmissue" runat="server" Display="Dynamic" ControlToValidate="txtTransactionDt" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                                </span>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtTransactionDt" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Cattel Feed Plant <span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ControlToValidate="ddlcfp" ValidationGroup="vgdmissue" InitialValue="0" ErrorMessage="Select CFP." Text="<i class='fa fa-exclamation-circle' title='Select CFP !'></i>"></asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlcfp" runat="server" Width="100%" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlcfp_SelectedIndexChanged">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Item Category (वस्तु का श्रेणी)<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfv3" runat="server" Display="Dynamic" ControlToValidate="ddlitemcategory" ValidationGroup="vgdmissue" InitialValue="0" ErrorMessage="Select Item Group." Text="<i class='fa fa-exclamation-circle' title='Select Item Group !'></i>"></asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlitemcategory" runat="server" Width="100%" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlitemcategory_SelectedIndexChanged">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Item Type (वस्तु की प्रकार)<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfv4" runat="server" Display="Dynamic" ControlToValidate="ddlitemtype" InitialValue="0" ValidationGroup="vgdmissue" ErrorMessage="Select Item Sub-Group." Text="<i class='fa fa-exclamation-circle' title='Select Item Sub-Group !'></i>"></asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlitemtype" runat="server" Width="100%" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlitemtype_SelectedIndexChanged">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Item Name (वस्तु का नाम)<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfv5" runat="server" Display="Dynamic" ControlToValidate="ddlitems" InitialValue="0" ValidationGroup="vgdmissue" ErrorMessage="Select Item Name." Text="<i class='fa fa-exclamation-circle' title='Select Item Name !'></i>"></asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlitems" runat="server" Width="100%" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlitems_SelectedIndexChanged">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Item Unit(इकाई)</label>
                                                <asp:DropDownList ID="ddlUnit" Enabled="false" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="Select"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label id="availunitname" runat="server">Available Stock (मौजूदा भंडार)(In MT) <span style="color: red;">*</span></label>

                                                <asp:TextBox ID="txtQty" placeholder="Quantity" autocomplete="off" Enabled="false" onpaste="return false;" CssClass="form-control Number" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label id="unitname" runat="server">Issue Quantity (जारी मात्रा)(In MT)<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtIssue" Display="Dynamic" ValidationGroup="vgdmissue" ErrorMessage="Enter Issued Quantity." Text="<i class='fa fa-exclamation-circle' title='Enter Issued Quantity. !'></i>"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="vgdmissue" runat="server" ControlToValidate="txtIssue" ErrorMessage="Please Enter Valid Number or two decimal value." Text="<i class='fa fa-exclamation-circle' title='Please Enter Valid Number or two decimal value. !'></i>"></asp:RegularExpressionValidator>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Issue qualntity should be less than equal to available quantity." Operator="LessThanEqual" ControlToCompare="txtQty" ControlToValidate="txtIssue" Type="Double" ValidationGroup="vgdmissue" Text="<i class='fa fa-exclamation-circle' title='Issue quantity should be less than equal to available quantity. !'></i>"></asp:CompareValidator>

                                                </span>
                                                <asp:TextBox ID="txtIssue" placeholder="Enter Issue Quantity" autocomplete="off" onchange="CalculateAmount();" onpaste="return false;" CssClass="form-control Number" runat="server" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label id="Label1" runat="server">Material Doc. No (सामग्री दस्तावेज़ नं)<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMaterialDocumentno" Display="Dynamic" ValidationGroup="vgdmissue" ErrorMessage="Enter Issued Quantity." Text="<i class='fa fa-exclamation-circle' title='Enter Issued Quantity. !'></i>"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" ValidationGroup="vgdmissue"
                                                        ErrorMessage="Invalid Material Document no" Text="<i class='fa fa-exclamation-circle' title='Invalid Material Document no!'></i>"
                                                        ControlToValidate="txtMaterialDocumentno" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^[a-zA-Z0-9_-]*$">
                                                    </asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtMaterialDocumentno" placeholder="Enter Material No..." autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Issue to  (जारी किसको किया)<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="ddlitems" InitialValue="0" ValidationGroup="vgdmissue" ErrorMessage="Select Item Name." Text="<i class='fa fa-exclamation-circle' title='Select Item Name !'></i>"></asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlIssue" runat="server" Width="100%" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlIssue_SelectedIndexChanged">
                                                    <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Self" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Other" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12" id="issuerdetail" runat="server" visible="false">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Issuer Name(जारीकर्ता का नाम)<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="vgdmissue"
                                                        ErrorMessage="Invalid Name" Text="<i class='fa fa-exclamation-circle' title='Invalid Name !'></i>"
                                                        ControlToValidate="txtIssuerName" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^[A-Za-z0-9? ,_-]+$">
                                                    </asp:RegularExpressionValidator></span>
                                                <asp:TextBox ID="txtIssuerName" placeholder="Issuer Name...." autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server" MaxLength="150"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Driver Name(वाहन चालक का नाम)<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="vgdmissue"
                                                        ErrorMessage="Invalid Name" Text="<i class='fa fa-exclamation-circle' title='Invalid Name !'></i>"
                                                        ControlToValidate="txtDriver" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^[A-Za-z0-9? ,_-]+$">
                                                    </asp:RegularExpressionValidator></span>
                                                <asp:TextBox ID="txtDriver" placeholder="Driver Name...." autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server" MaxLength="150"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Driver Mob. NO(वाहन चालक मोबाइल नं)<span style="color: red;"> *</span></label>
                                                <asp:TextBox ID="txtDriverContactNo" placeholder="Driver Contact No...." autocomplete="off" MaxLength="10"  onpaste="return false;" CssClass="form-control Number" runat="server" onkeypress="return onlyNumberKey(event);"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Vehicle No(वाहन का नंबर)<span style="color: red;"> *</span></label>
                                                 <span class="pull-right">
                                                  <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ValidationGroup="vgdmissue"
                                                        ErrorMessage="Invalid Vehicle No" Text="<i class='fa fa-exclamation-circle' title='Invalid Vehicle No !'></i>"
                                                        ControlToValidate="txtVehicle" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^(?=.*[a-zA-Z])(?=.*[0-9])[A-Za-z0-9]+$">
                                                    </asp:RegularExpressionValidator></span>
                                                <asp:TextBox ID="txtVehicle" placeholder="Vehicle No....." autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server" MaxLength="150"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-md-12" id="issuerPur" runat="server" visible="false">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label id="carriage" runat="server">No. of Carriage(संख्या)<span style="color: red;"> *</span></label>
                                                <asp:TextBox ID="txtnoofbags" placeholder="No. of Carriage....." autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server" MaxLength="150" onkeypress="return onlyNumberKey(event);"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Rate(रेट)(Per Unit)<span style="color: red;"> *</span></label>
                                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator4" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="vgdmissue" runat="server" ControlToValidate="txtRate" ErrorMessage="Please Enter Valid Number or two decimal value." Text="<i class='fa fa-exclamation-circle' title='Please Enter Valid Number or two decimal value. !'></i>"></asp:RegularExpressionValidator>
                                                <asp:TextBox ID="txtRate" placeholder="Rate...." autocomplete="off" onpaste="return false;" onchange="CalculateAmount();" CssClass="form-control Number" runat="server" MaxLength="150"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Amount(राशि)<span style="color: red;"> *</span></label>
                                                <asp:HiddenField ID="hdnamt" runat="server" Value="0" />
                                                <asp:TextBox ID="txtAmount" placeholder="Enter Amount" Enabled="false" autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12" style="text-align: center;">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Button Text="Save" ID="btnSubmit" CssClass="btn btn-block btn-success" ValidationGroup="vgdmissue" runat="server" OnClick="btnSubmit_Click" Visible="false" />
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-block btn-default" OnClick="btnClear_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Material Issued (जारी किये गए सामग्री का विवरण)
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:HiddenField ID="hdnvalue" runat="server" Value="0" />
                                            <asp:GridView ID="gvOpeningStock" DataKeyNames="MaterialIssueID" runat="server" EmptyDataText="No records Found" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover" OnRowCommand="gvOpeningStock_RowCommand" PageSize="20" AllowPaging="true" OnPageIndexChanging="gvOpeningStock_PageIndexChanging">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.<br />(क्रं.)">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CFP_Name" HeaderText="Cattle Feed Plant" />
                                                    <asp:BoundField DataField="TranDt" HeaderText="Issue Date (जारी करने कि तिथि)" />
                                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name (वस्तु का नाम)" />
                                                    <asp:BoundField DataField="IssuedQuantity" HeaderText="Quantity (जारी मात्रा)" />
                                                    <asp:BoundField DataField="Unit" HeaderText="Unit (इकाई)" />
                                                    <asp:BoundField DataField="IssueTo" HeaderText="Issue to (जारीकर्ता)" />
                                                    <asp:TemplateField HeaderText="Action <br />(कार्य)">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("MaterialIssueID") %>' runat="server" ToolTip="Update" Visible='<%# !Convert.ToBoolean(Eval("IsLocked")) %>' CausesValidation="false"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                            &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnklock" CommandArgument='<%#Eval("MaterialIssueID") %>' CommandName="Recordlock" CausesValidation="false" runat="server" ToolTip="Lock Record" Style="color: red;" Visible='<%# !Convert.ToBoolean(Eval("IsLocked")) %>' OnClientClick="return confirm('Are you sure to Lock? As Record you lock record it will not updated or deleted. ')"><i class="fa fa-lock"></i></asp:LinkButton>
                                                            &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("MaterialIssueID") %>' CommandName="RecordDelete" CausesValidation="false" runat="server" ToolTip="Delete" Style="color: red;" Visible='<%# !Convert.ToBoolean(Eval("IsLocked")) %>' OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                                            &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" CommandArgument='<%#Eval("MaterialIssueID") %>' CommandName="RecordReport" CausesValidation="false" runat="server" ToolTip="Report" Style="color: red;"><i class="fa fa-file"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
        </section>
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
                            <asp:Panel ID="pnl" runat="server" BorderColor="Black" BorderWidth="2" Width="90%">
                                <table class="table table-bordered">
                                    <tr>
                                        <td style="text-align: center;" colspan="2">


                                            <span id="lblcfpname" runat="server" style="color: maroon; font-size: 22px; font-weight: bold"></span>
                                            <br />
                                            <span id="lblcfp" runat="server" style="color: maroon; font-size: 12px; font-weight: bold"></span>
                                            <br />

                                        </td>

                                    </tr>
                                    <tr>
                                        <td style="text-align: left;" colspan="2">
                                            <span style="font-size: 12px;">M/S</span>&nbsp; <span id="lbSupplier" runat="server" style="color: black; font-weight: bold"></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;"><span style="font-size: 12px;">Issued Qty</span>&nbsp; <span id="lblReceivedqty" runat="server" style="color: black; font-weight: bold"></span></td>
                                        <td style="text-align: left;"><span style="font-size: 12px;">of Item </span>&nbsp; <span id="lblItem" runat="server" style="color: black; font-weight: bold"></span></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;"><span style="font-size: 12px;">On</span>&nbsp; <span id="lblItemIssuedDate" runat="server" style="color: black; font-weight: bold"></span></td>
                                        <td style="text-align: left;"><span style="font-size: 12px;">Material Document No </span>&nbsp; <span id="lblDocumentNO" runat="server" style="color: black; font-weight: bold"></span></td>

                                    </tr>
                                    <tr id="otherissuedetail" runat="server">
                                        <td style="text-align: left;"><span style="font-size: 12px;">Vehicle No</span>&nbsp; <span id="lblVehicleNo" runat="server" style="color: black; font-weight: bold"></span></td>
                                        <td style="text-align: left;"><span style="font-size: 12px;">No Of Bags</span>&nbsp; <span id="lblNoofBags" runat="server" style="color: black; font-weight: bold"></span></td>
                                    </tr>
                                    <tr id="otherissueAmt" runat="server">
                                        <td style="text-align: left;">
                                            <span style="font-size: 12px;">Rate</span><br />
                                            <span id="lblRate" runat="server" style="color: black; font-weight: bold"></span>
                                        </td>
                                        <td style="text-align: left;">
                                            <span style="font-size: 12px;">Amount</span><br />
                                            <span id="lblAmount" runat="server" style="color: black; font-weight: bold"></span>

                                        </td>
                                    </tr>
                                    <tr id="otherissueDriver" runat="server">
                                        <td style="text-align: left;">
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <span id="lblDriver" runat="server" style="color: black; font-weight: bold"></span>
                                            <br />
                                            <span id="lbDriverContact" runat="server" style="color: black; font-weight: bold"></span>
                                            <br />
                                            <span style="font-size: 12px;">(Driver Signature)</span>
                                        </td>

                                        <td style="text-align: left;">
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <span id="Span2" runat="server" style="color: black; font-weight: bold">Sig....</span><br />
                                            <span id="Span1" runat="server" style="color: black; font-weight: bold">Store Clerk / Store Supdt.</span>

                                        </td>
                                    </tr>


                                </table>
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
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
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

