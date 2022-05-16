<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CFP_Item_Received_In_CattleFeed.aspx.cs" Inherits="mis_CattleFeed_CFP_Item_Received_In_CattleFeed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <style>
        .spaced input[type="radio"] {
            margin-left: 5px; /* Or any other value */
        }
    </style>
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
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Items Receipts Note Detail</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>Items Receipts Note Detail(वस्तु रसीद नोट की जानकारी देखें)
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
                                        <div class="form-group">
                                            <asp:Button Text="View Invoice" ID="btnSubmit" ValidationGroup="a" Width="30%" CausesValidation="true" CssClass="btn btn-block btn-success" runat="server" OnClick="btnSubmit_Click" />
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
                                                        <td style="text-align: left;">
                                                            <span style="font-size: 12px;">Item Name</span><br />
                                                            <span id="lblItemName" runat="server" style="color: black; font-weight: bold"></span>
                                                        </td>
                                                        <td style="text-align: left;" colspan="2">
                                                            <span style="font-size: 12px;">On Recieved Date</span><br />
                                                            <span id="lblOnReceivedDate" runat="server" style="color: black; font-weight: bold"></span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left;" colspan="3">
                                                            <span id="lblBigGRGRNO" runat="server" style="color: maroon; font-size: 16px; font-weight: bold"></span>
                                                            &nbsp;<span style="font-size: 12px;">M/S</span> <span id="lbSupplier" runat="server" style="color: black; font-weight: bold"></span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left;">
                                                            <span style="font-size: 12px;">Purchase Order No</span><br />

                                                            <asp:Label ID="txtPOno" placeholder="Enter P.O No." autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server" MaxLength="40"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;" colspan="2">
                                                            <span style="font-size: 12px;">On Purchase Date<span style="color: red">*</span></span><br />

                                                            <asp:Label ID="txtpurchaseDate" Width="100%" runat="server" autocomplete="off" CssClass="form-control" ClientIDMode="Static"></asp:Label>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left;">
                                                            <label>Small G.R. No.</label><br />

                                                            <span id="lblsmallGR" runat="server" style="color: maroon; font-size: 16px; font-weight: bold"></span>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <label>Item Received Date (दिनांक) </label>
                                                            <span style="color: red">*</span>
                                                            <span class="pull-right">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="vgdmissue" Display="Dynamic" ControlToValidate="txtReceiveddate" ErrorMessage="Please Enter Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter Date !'></i>"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="vgdmissue" runat="server" Display="Dynamic" ControlToValidate="txtReceiveddate" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                                            </span>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtReceiveddate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                                            </div>
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
                                                            <label>Rate<span style="color: red;"> *</span></label>

                                                            <asp:Label ID="txtRate" placeholder="Enter Rate." autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server" MaxLength="10"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <label>Amount<span style="color: red;"> *</span></label>

                                                            <asp:TextBox ID="txtReatAmount" placeholder="Enter Amount." Enabled="false" autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server" MaxLength="10"></asp:TextBox>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left;">
                                                            <label>Transportable Charges.<span style="color: red;"> *</span></label>
                                                            <span class="pull-right">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txttransportcharge" Display="Dynamic" ValidationGroup="vgdmissue" ErrorMessage="Enter Transport Charges." Text="<i class='fa fa-exclamation-circle' title='Enter Transport Charges. !'></i>"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="vgdmissue" runat="server" ControlToValidate="txttransportcharge" ErrorMessage="Please Enter Valid Number or decimal value." Text="<i class='fa fa-exclamation-circle' title='Please Enter Valid Number or decimal value. !'></i>"></asp:RegularExpressionValidator>
                                                            </span>
                                                            <asp:TextBox ID="txttransportcharge" placeholder="Enter Amount." Text="0" autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server" MaxLength="10" AutoPostBack="true" OnTextChanged="txttransportcharge_TextChanged"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <label>GST<span style="color: red;"> *</span></label>
                                                            <asp:RadioButtonList runat="server" ID="rbtnGSTType" AutoPostBack="true" CssClass="spaced" RepeatDirection="Horizontal" CellPadding="3" CellSpacing="2" OnSelectedIndexChanged="rbtnGSTType_SelectedIndexChanged"></asp:RadioButtonList>
                                                        </td>
                                                        <td style="text-align: left;" id="gstpnl" runat="server" visible="false">
                                                            <label>GST %.<span style="color: red;"> *</span></label>
                                                            <span class="pull-right">
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="vgdmissue" runat="server" ControlToValidate="txttransportcharge" ErrorMessage="Please Enter Valid Number or decimal value." Text="<i class='fa fa-exclamation-circle' title='Please Enter Valid Number or decimal value. !'></i>"></asp:RegularExpressionValidator>
                                                            </span>
                                                            <asp:TextBox ID="txtGSTPercentage" placeholder="Enter GST Percentage." Text="0" autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server" MaxLength="10" AutoPostBack="true" OnTextChanged="txtGSTPercentage_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left;">
                                                            <label>Total Amount Rs.<span style="color: red;"> *</span></label>

                                                            <asp:Label ID="txtAmount" Enable="false" Text="0" CssClass="form-control" runat="server" MaxLength="10"></asp:Label>
                                                        </td>
                                                    </tr>


                                                    <tr>
                                                        <td style="text-align: left;" colspan="3">
                                                            <span style="font-size: 12px;">Remark</span>&nbsp;
                                                           <span class="pull-right">
                                                               <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="vgdmissue"
                                                                   ErrorMessage="Invalid Remak" Text="<i class='fa fa-exclamation-circle' title='Invalid Remark !'></i>"
                                                                   ControlToValidate="txtRemark" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^[A-Za-z0-9? ,_-]+$">
                                                               </asp:RegularExpressionValidator></span>
                                                            <asp:TextBox ID="txtRemark" placeholder="Enter Remark." autocomplete="off" onpaste="return false;" CssClass="form-control Number" TextMode="MultiLine" Rows="3" runat="server" MaxLength="200"></asp:TextBox>
                                                        </td>
                                                    </tr>


                                                </table>

                                            </div>
                                            <div class="col-md-12" style="text-align: center;">
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:HiddenField ID="hdnItemID" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hdnCFPPurchase_Order_ID" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hdnCFPItemNoteID" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hdnvalue" runat="server" Value="0" />
                                                        <asp:Button Text="Save" ID="btnSave" ValidationGroup="vgdmissue" CausesValidation="true" CssClass="btn btn-block btn-success" runat="server" OnClick="btnSave_Click" />
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Button Text="Clear" ID="btnReset" CssClass="btn btn-block btn-default" runat="server" OnClick="btnReset_Click" CausesValidation="false" />
                                                    </div>
                                                </div>
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

