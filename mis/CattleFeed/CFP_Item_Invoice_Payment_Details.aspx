<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CFP_Item_Invoice_Payment_Details.aspx.cs" Inherits="mis_CattleFeed_CFP_Item_Invoice_Payment_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <script type="text/javascript">
        function printDiv(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }
    </script>

    <div class="content-wrapper">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-Manish">
                    <div class="box-header">
                        <h3 class="box-title">Received Payment (वस्तु की भुगतान संबंधित जानकारी)</h3>
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
                                        <asp:Button Text="View Payment" ID="btnSave" ValidationGroup="vgdmissue" CausesValidation="true" Width="30%" CssClass="btn btn-block btn-success" runat="server" OnClick="btnSave_Click" />
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset id="paypnl" runat="server" visible="false">
                            <legend>Item Payment(वस्तु का भुगतान संबंधित जानकारी)
                            </legend>
                            <div style="text-align: right;">
                                <button type="submit" class="btn btn-primary" onclick="printDiv('printableArea')">Print</button>
                            </div>
                            <div id="printableArea">
                                <asp:Panel ID="pnl" runat="server" BorderColor="Black" BorderWidth="1">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <table class="table table-bordered">
                                                <tr>
                                                    <td style="font-size: 12px;">
                                                        <label>Item Name (वस्तु का नाम)</label><br />
                                                        <span id="lblItem" runat="server" style="color: maroon; font-weight: bold"></span>
                                                    </td>
                                                    <td style="font-size: 12px;">
                                                        <label>Purchase Order (क्रय आदेश)</label><br />
                                                        <span id="lblpurchaseorder" runat="server" style="color: maroon; font-weight: bold"></span>
                                                    </td>
                                                    <td style="font-size: 12px;">
                                                        <label>Purchase Order Date</label><br />
                                                        <span id="lblpurchaseorderdate" runat="server" style="color: maroon; font-weight: bold"></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 12px;">
                                                        <label>Invoice No</label><br />
                                                        <span id="lblInvoiceNo" runat="server" style="color: maroon; font-weight: bold"></span>
                                                    </td>
                                                    <td style="font-size: 12px;">
                                                        <label>Invoice Date (चालान दिनांक)</label><br />
                                                        <span id="lblInvoiceDT" runat="server" style="color: maroon; font-weight: bold"></span>
                                                    </td>
                                                    <td style="font-size: 12px;">
                                                        <label>Quantity (वस्तु की मात्रा)</label><br />
                                                        <span id="lblQuantity" runat="server" style="color: maroon; font-weight: bold"></span><span id="lblunit1" runat="server" style="color: maroon; font-weight: bold"></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 12px;">
                                                        <label>Rate</label><br />
                                                        <span id="lblrate" runat="server" style="color: maroon; font-weight: bold"></span> <span id="lblunit" runat="server" style="color: maroon; font-weight: bold"></span>
                                                    </td>
                                                    <td style="font-size: 12px;">
                                                        <label>Total Amount</label><br />
                                                        <span id="lblTotalAmt" runat="server" style="color: maroon; font-weight: bold"></span>
                                                    </td>
                                                    <td style="font-size: 12px;">
                                                        <label>Payable % (भुगतान प्रतिशत)</label><br />
                                                        <span id="lblPayablepercentage" runat="server" style="color: maroon; font-weight: bold"></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 12px;">
                                                        <label>Paid Amount(कुल राशि)</label><br />
                                                        <span id="lblPaidAmt" runat="server" style="color: maroon; font-weight: bold"></span>
                                                    </td>
                                                    <td style="font-size: 12px;" colspan="2">
                                                        <label>Remaining Balance(शेष राशि)</label><br />
                                                        <span id="lblrembalance" runat="server" style="color: maroon; font-weight: bold"></span>
                                                    </td>

                                                </tr>
                                            </table>

                                        </div>

                                        <div class="col-md-12">
                                            <fieldset id="Fieldset1" runat="server" >
                                                <legend> Payment Detail(भुगतान संबंधित जानकारी)
                                                </legend>
                                                <asp:GridView ID="grdCatlist" runat="server" class="datatable table table-hover table-bordered pagination-ys" EmptyDataText="No Record Available" AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="CFP_Payment_Status" HeaderText="Status" ItemStyle-Width="10%" />
                                                        <asp:BoundField DataField="Payable_Percentage" HeaderText="Percentage (%)" ItemStyle-Width="10%" />
                                                        <asp:BoundField DataField="Payment_Mode" HeaderText="Payment Mode" ItemStyle-Width="10%" />
                                                        <asp:BoundField DataField="Payment_NO" HeaderText="Payment NO" ItemStyle-Width="10%" />
                                                        <asp:BoundField DataField="Payment_Date" HeaderText="Payment Date" ItemStyle-Width="10%" />
                                                        <asp:BoundField DataField="Payment_Amount" HeaderText="Payment Amount" ItemStyle-Width="10%" />
                                                        <asp:BoundField DataField="Remark" HeaderText="Remark" ItemStyle-Width="30%" />

                                                    </Columns>
                                                </asp:GridView>
                                            </fieldset>
                                        </div>

                                    </div>
                                </asp:Panel>
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

