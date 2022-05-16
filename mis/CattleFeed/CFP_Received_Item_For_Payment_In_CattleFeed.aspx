<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CFP_Received_Item_For_Payment_In_CattleFeed.aspx.cs" Inherits="mis_CattleFeed_CFP_Received_Item_For_Payment_In_CattleFeed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <style>
        .spaced input[type="radio"] {
            margin-left: 5px; /* Or any other value */
        }
    </style>
    <script type="text/javascript">
        function ShowPopupAddDates() {
            $('#myModal').modal('show');
        }
        function printDiv(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }
    </script>
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Items Payment Detail</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>Items Payment Detail Entry(भुगतान संबंधित जानकरी प्रविष्टि)
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Cattle Feed Plant<span class="text-danger"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a" InitialValue="0"
                                                        ErrorMessage="Select Cattle Feed Plant" Text="<i class='fa fa-exclamation-circle' title='Select Cattle Feed Plant!'></i>"
                                                        ControlToValidate="ddlCFP" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlCFP" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlCFP_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Invoice NO.<span class="text-danger"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a" InitialValue="0"
                                                        ErrorMessage="Select Invoice" Text="<i class='fa fa-exclamation-circle' title='Select Invoice!'></i>"
                                                        ControlToValidate="ddlInvoice" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlInvoice" runat="server" CssClass="form-control select2">
                                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12" style="text-align: center;">

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:Button Text="View Invoice" ID="btnSubmit" ValidationGroup="a" Width="30%" CausesValidation="true" CssClass="btn btn-block btn-success" runat="server" OnClick="btnSubmit_Click" />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:Button Text="Clear" Width="30%" ID="btnReset" CssClass="btn btn-block btn-default" runat="server" OnClick="btnReset_Click" CausesValidation="false" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </fieldset>
                            <fieldset id="paypnl" runat="server" visible="false">
                                <legend>Item Receipts Note (वस्तु रसीद नोट की जानकारी)
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
                                                        <td style="text-align: center;" colspan="3">


                                                            <span id="lblcfpname" runat="server" style="color: maroon; font-size: 22px; font-weight: bold"></span>
                                                            <%-- <br />
                                                        <span id="lblcfp" runat="server" style="color: maroon; font-size: 12px; font-weight: bold"></span>
                                                        <br />--%>

                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left;" colspan="3">
                                                            <span style="font-size: 12px;">Item Name</span><br />
                                                            <span id="lblItemName" runat="server" style="color: black; font-weight: bold"></span>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left;" colspan="3">
                                                            <span id="lblSmallGRNO" runat="server" style="color: maroon; font-size: 16px; font-weight: bold"></span>
                                                            &nbsp;<span style="font-size: 12px;">M/S</span> <span id="lbSupplier" runat="server" style="color: black; font-weight: bold"></span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left;">
                                                            <label>G.R. No.</label><br />
                                                            <span id="lblGRNO" runat="server" style="color: black; font-weight: bold"></span>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <label>Item Received Date (दिनांक) </label>
                                                            <br />
                                                            <span id="lblItemReceiveddate" runat="server" style="color: black; font-weight: bold"></span>

                                                        </td>
                                                        <td style="text-align: left;">
                                                            <span style="font-size: 12px;">Code / Invoice No</span><br />
                                                            <span id="lblInvoiceNo" runat="server" style="color: black; font-weight: bold"></span>
                                                        </td>
                                                    </tr>
                                                    <%-- <tr>
                                                        <td style="text-align: left;"><span style="font-size: 12px;">Received</span>&nbsp; <span id="lblReceivedqty" runat="server" style="color: black; font-weight: bold"></span></td>
                                                        <td style="text-align: left;"><span style="font-size: 12px;">Bags of</span>&nbsp; <span id="lblItem" runat="server" style="color: black; font-weight: bold"></span></td>
                                                    </tr>--%>
                                                    <tr>
                                                        <td style="text-align: left;"><span style="font-size: 12px;">Truck No</span><br />
                                                            <span id="lblTruckNO" runat="server" style="color: black; font-weight: bold"></span></td>
                                                        <td style="text-align: left;"><span style="font-size: 12px;">No of Bags</span><br />
                                                            <span id="lblItemBagsNo" runat="server" style="color: black; font-weight: bold"></span></td>
                                                        <td style="text-align: left;"><span style="font-size: 12px;">Truck Bilty NO & Date</span><br />
                                                            <span id="lbllrrno" runat="server" style="color: black; font-weight: bold"></span>
                                                            <br />
                                                            <span id="lblTruckSupplyDate" runat="server" style="color: black; font-weight: bold"></span></td>

                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left;"><span style="font-size: 12px;" id="tg" runat="server">Total Gross Weight(In Kg.)</span><br />
                                                            <span id="lblTotalGrosswt" runat="server" style="color: black; font-weight: bold"></span></td>
                                                        <td style="text-align: left;"><span style="font-size: 12px;" id="ltw" runat="server">Less Tare Weight(In Kg.)</span><br />
                                                            <span id="lbltarewt" runat="server" style="color: black; font-weight: bold"></span></td>
                                                        <td style="text-align: left;"><span style="font-size: 12px;" id="NW" runat="server">Net Weight(In Kg.)</span><br />
                                                            <span id="lblNetWt" runat="server" style="color: black; font-weight: bold"></span></td>

                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left;"><span style="font-size: 12px;" id="WT" runat="server">Weight(In M.T)</span><br />
                                                            <span id="lblWtMT" runat="server" style="color: black; font-weight: bold"></span></td>
                                                        <td style="text-align: left;">
                                                            <label>Rate</label><br />
                                                            <span id="lblReatAmount" runat="server" style="color: black; font-weight: bold"></span>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <label>Transportable Charges.</label><br />
                                                            <span id="lbltransportcharge" runat="server" style="color: black; font-weight: bold"></span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left;"><span style="font-size: 12px;" id="Span1" runat="server">GST Included</span><br />
                                                            <span id="lblGSTIncluded" runat="server" style="color: black; font-weight: bold"></span></td>
                                                        <td style="text-align: left;">
                                                            <label>GST %</label><br />
                                                            <span id="lblGSTpercentage" runat="server" style="color: black; font-weight: bold"></span>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <label>Total Amount<span style="color: red;"> *</span></label><br />
                                                            <span id="lblAmount" runat="server" style="color: black; font-weight: bold"></span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left;"><span style="font-size: 12px;" id="Span2" runat="server">Payable Amount</span><br />
                                                            <asp:Label ID="lblpayableAmt" runat="server" CssClass="form-control" Text="0"></asp:Label></td>
                                                        <td style="text-align: left;">
                                                            <label>Payable % (भुगतान प्रतिशत)<span class="text-danger"> *</span></label><br />

                                                            <span class="pull-right">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="b" InitialValue="0"
                                                                    ErrorMessage="Select Payable Percentage" Text="<i class='fa fa-exclamation-circle' title='Select Payable Percentage!'></i>"
                                                                    ControlToValidate="ddlpaypercentage" ForeColor="Red" Display="Dynamic" runat="server">
                                                                </asp:RequiredFieldValidator>
                                                            </span>
                                                            <asp:DropDownList ID="ddlpaypercentage" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlpaypercentage_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                                                <asp:ListItem Value="80">80 %</asp:ListItem>
                                                                <asp:ListItem Value="20">20 %</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <label>Paid Amount(कुल राशि)</label><br />
                                                            <asp:Label ID="lblPaidAmt" runat="server" CssClass="form-control" Text="0"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left;" colspan="3">
                                                            <label>Remaining Balance(शेष राशि)</label><br />
                                                            <asp:Label ID="lblrembalance" runat="server" CssClass="form-control" Width="30%"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left;" colspan="3">
                                                            <label>Remark</label>
                                                            <span class="pull-right">
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="b"
                                                                    ErrorMessage="Invalid Remark" Text="<i class='fa fa-exclamation-circle' title='Invalid Remark !'></i>"
                                                                    ControlToValidate="txtRemark" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^[A-Za-z0-9? ,_-]+$">
                                                                </asp:RegularExpressionValidator></span><br />
                                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
                                                    </tr>
                                                    <tr id="iteminkgfirst" runat="server" visible="false">
                                                        <td style="text-align: left;" colspan="2">
                                                            <asp:GridView ID="grdlist" runat="server" AutoGenerateColumns="false" ShowHeader="true" ShowFooter="true" CssClass="datatable table table-striped table-bordered table-hover"
                                                                EmptyDataText="No Record Found.">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="S.No">
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1 %>
                                                                            <asp:HiddenField ID="hdntype" runat="server" Value='<%# Eval("CFP_Item_Test_variant_ID") %>' />
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            Total (कुल) :
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Test Variables">
                                                                        <ItemTemplate>
                                                                            <%# Eval("CFP_Item_Test_variant") %>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblFooterTotal" runat="server" Font-Bold="true" Text="0"></asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtvalue" runat="server" Text="0" AutoPostBack="true" OnTextChanged="txtPercentage_TextChanged" />
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblFooterTotal1" runat="server" Font-Bold="true" Text="0"></asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <label>Rebate %</label><br />
                                                            <asp:TextBox ID="txtRebate" runat="server" CssClass="form-control" Text="0" AutoPostBack="true" OnTextChanged="txtRebate_TextChanged"></asp:TextBox>
                                                            <label>Rebate Amt.</label><br />
                                                            <asp:Label ID="txtRebateAmt" runat="server" CssClass="form-control" Text="0"></asp:Label>
                                                            <label>Other Deduction Amt.</label><br />
                                                            <asp:Label ID="txtDeduction" runat="server" CssClass="form-control" Text="0"></asp:Label>
                                                            <label>Other Panelty Amt.</label>
                                                             <span class="pull-right">
                                                              <asp:RegularExpressionValidator ID="RegularExpressionValidator6" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="b" runat="server" ControlToValidate="txtpaneltyAmt" ErrorMessage="Please Enter Valid Number or two decimal value." Text="<i class='fa fa-exclamation-circle' title='Please Enter Valid Number. !'></i>"></asp:RegularExpressionValidator>
                                                                 </span><br />
                                                            <asp:TextBox ID="txtpaneltyAmt" runat="server" CssClass="form-control" Text="0" AutoPostBack="true" OnTextChanged="txtpaneltyAmt_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>


                                                </table>

                                            </div>
                                            <div class="col-md-12" style="text-align: center;">
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:HiddenField ID="hdntobepaidamount" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hdnUnitID" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hdnItemID" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hdnCFPItemNoteID" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hdnCFPItemRecieveID" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hdnDoublebag" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hdnDamagebag" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hdnpaidAmt" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hdnvalue" runat="server" Value="0" />
                                                        <asp:Button Text="Save" ID="btnSave" ValidationGroup="b" CausesValidation="true" CssClass="btn btn-block btn-success" runat="server" OnClick="btnSave_Click" />
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:HiddenField ID="hdnpaymentid" runat="server" Value="0" />
                                                <asp:GridView ID="grdCatlist" runat="server" class="datatable table table-hover table-bordered pagination-ys" EmptyDataText="No Record Available" AutoGenerateColumns="False" OnRowCommand="grdCatlist_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="BigGRNo" HeaderText="BigGRNO" />
                                                        <asp:BoundField DataField="Total_Amount" HeaderText="Total Amount" />
                                                        <asp:BoundField DataField="TobePaid" HeaderText="To be Paid" />
                                                        <asp:BoundField DataField="Payable_Percentage" HeaderText="Percentage" />
                                                        <asp:BoundField DataField="Payment_Amount" HeaderText="Payment Amount" />
                                                        <asp:BoundField DataField="RebatePercentage" HeaderText="Rebate Percentage" />
                                                        <asp:BoundField DataField="RebateAmt" HeaderText="Rebate Amt" ItemStyle-Width="10%" />
                                                        <asp:BoundField DataField="OtherDeductonAmt" HeaderText="Other Deducton Amt" ItemStyle-Width="10%" />
                                                        <asp:TemplateField HeaderText="" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LnkSelect" runat="server" CausesValidation="false" CommandName="RecordUpdate" CommandArgument='<%#Eval("Item_Received_Payment_ID") %>' Text="Edit" ToolTip="View Test Variable" Visible='<%# Convert.ToInt32(Eval("Payable_Percentage"))==20?true:false %>'><i class="fa fa-eye">View </i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </fieldset>

                        </div>

                    </div>
                </div>
            </div>
        </section>
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog" style="height: 100%; width: 60%;">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <span style="color: white">Product Details</span>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="GridView2" runat="server" class="table table-hover table-bordered pagination-ys" EmptyDataText="No Record Available" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CFP_Item_Test_variant" HeaderText="Test Variant" ItemStyle-Width="20%" />
                                        <asp:BoundField DataField="Test_VALUE" HeaderText="VALUE" ItemStyle-Width="20%" />
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-Width="20%" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

