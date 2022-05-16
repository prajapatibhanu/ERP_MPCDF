<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CFP_Received_Item_Invoice_Payment.aspx.cs" Inherits="mis_CattleFeed_CFP_Received_Item_Invoice_Payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <script type="text/javascript">
        function CalculateAmount() {
            debugger;
            var payperc = document.getElementById('<%=txtPayablepercentage.ClientID%>').value.trim();
            var lblpayableAmt = document.getElementById('<%=lblpayableAmt.ClientID%>').value.trim();
            if (payperc == "")
                payperc = "0";
            if (lblpayableAmt == "")
                lblpayableAmt = "0";

            document.getElementById('<%=lblPaidAmt.ClientID%>').value = ((payperc * lblpayableAmt) / 100).toFixed(2);
        }
    </script>
    <div class="content-wrapper">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-Manish">
                    <div class="box-header">
                        <h3 class="box-title">Received Item for Payment (भुगतान के लिए प्राप्त वस्तु)</h3>
                    </div>
                    <div class="box-body">
                        <fieldset>
                            <legend>Item Payment Entry(वस्तु का भुगतान संबंधित जानकारी प्रविष्ट करें)
                            </legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblMsg" CssClass="Autoclr" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-4">
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
                                    <div class="col-md-4">
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
                                    <div class="col-md-4">
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
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Item Name (वस्तु का नाम)<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv5" runat="server" Display="Dynamic" ControlToValidate="ddlitems" InitialValue="0" ValidationGroup="vgdmissue" ErrorMessage="Select Item Name." Text="<i class='fa fa-exclamation-circle' title='Select Item Name !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlitems" runat="server" Width="100%" AutoPostBack="true" CssClass="form-control select2" OnSelectedIndexChanged="ddlitems_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Item Invoice (वस्तु का चालान)<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="ddlitems" InitialValue="0" ValidationGroup="vgdmissue" ErrorMessage="Select Item Name." Text="<i class='fa fa-exclamation-circle' title='Select Item Name !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlItemChallan" runat="server" Width="100%" CssClass="form-control select2">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12" style="text-align: center;">
                                    <div class="form-group">
                                        <asp:Button Text="Generate Payment" ID="btnSave" ValidationGroup="vgdmissue" CausesValidation="true" Width="30%" CssClass="btn btn-block btn-success" runat="server" OnClick="btnSave_Click" />
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset id="paypnl" runat="server" visible="false">
                            <legend>Item Payment(वस्तु का भुगतान संबंधित जानकारी प्रविष्ट करें)
                            </legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Invoice Date (चालान दिनांक)</label>
                                            <asp:Label ID="lblInvoiceDT" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Quantity (वस्तु की मात्रा)</label>
                                            <asp:Label ID="lblQuantity" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Rate</label>
                                            <asp:Label ID="lblrate" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Total Amount</label>
                                            <asp:Label ID="lblTotalAmt" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Payable Amount</label>
                                            <asp:Label ID="lblpayableAmt" runat="server" CssClass="form-control" Text="0"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Payable % (भुगतान प्रतिशत)</label>
                                            <%--<span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="txtPayablepercentage" ValidationGroup="a" ErrorMessage="Enter Payable % ." Text="<i class='fa fa-exclamation-circle' title='Enter Payable % !'></i>"></asp:RequiredFieldValidator>
                                            </span>--%>
                                            <asp:TextBox ID="txtPayablepercentage" placeholder="Enter Payable %" autocomplete="off" AutoPostBack="true" onpaste="return false;" CssClass="form-control Number" runat="server" OnTextChanged="txtPayablepercentage_TextChanged"></asp:TextBox>
                                              
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Paid Amount(कुल राशि)</label>
                                            <asp:Label ID="lblPaidAmt" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Remaining Balance(शेष राशि)</label>
                                            <asp:Label ID="lblrembalance" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Payment Status (भुगतान की स्थिति)<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" ControlToValidate="ddlStatus" InitialValue="0" ValidationGroup="a" ErrorMessage="Select Payment Mode." Text="<i class='fa fa-exclamation-circle' title='Select Item Name !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlStatus" runat="server" Width="100%" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12" id="paymntsection" runat="server" visible="false">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Payment Mode (भुगतान का प्रकार)<span style="color: red;"> *</span></label>

                                            <asp:DropDownList ID="ddlpaymentmode" runat="server" Width="100%" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlpaymentmode_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label id="payno" runat="server">Payment No</label>

                                            <asp:TextBox ID="txtneftno" placeholder="Enter No...." autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label id="paydate" runat="server">Payment Date </label>
                                            <span style="color: red">*</span>
                                            <span class="pull-right">

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtneftDate" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtneftDate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label id="payAmt" runat="server">Payment Amount</label>
                                            <asp:TextBox ID="txtneftamt" placeholder="Enter Amount" Enabled="false" autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Remark (रिमार्क)</label> 
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtRemark" ValidationGroup="a" ErrorMessage="Enter Payable % ." Text="<i class='fa fa-exclamation-circle' title='Enter Payable % !'></i>"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtRemark" placeholder="Enter Remark" autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server" Width="100%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12" style="text-align: center;">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button Text="Payment" ID="btnSubmit" ValidationGroup="a" CausesValidation="true" CssClass="btn btn-block btn-success" runat="server" OnClick="btnSubmit_Click"  OnClientClick="return confirm('Once Payment is done it will not change.Are you sure want to do Payment?');" />
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:HiddenField ID="hdnstatus" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnitemrecid" runat="server" Value="0" />
                                            <asp:Button Text="Reset" ID="btnReset" CssClass="btn btn-block btn-default" runat="server" OnClick="btnReset_Click" CausesValidation="false" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset id="recv" runat="server">
                            <legend>Received Payment for Item 
                            </legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:HiddenField ID="hdnvalue" runat="server" Value="0" />
                                    <asp:GridView ID="grdCatlist" runat="server" class="datatable table table-hover table-bordered pagination-ys" EmptyDataText="No Record Available" AutoGenerateColumns="False"  OnRowCommand="grdCatlist_RowCommand" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                            <asp:BoundField DataField="CFP_Invoice_No" HeaderText="Invoice No" />
                                            <asp:BoundField DataField="CFP_Invoice_Date" HeaderText="Invoice Date" />
                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                            <asp:BoundField DataField="CFP_Payment_Status" HeaderText="Payment Status" />
                                            <asp:BoundField DataField="Payable_Percentage" HeaderText="Percentage" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="Payment_Amount" HeaderText="Payment_Amount" ItemStyle-Width="10%" />
                                           
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

