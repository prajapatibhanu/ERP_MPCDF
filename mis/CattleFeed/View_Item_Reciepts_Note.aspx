<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="View_Item_Reciepts_Note.aspx.cs" Inherits="mis_CattleFeed_View_Item_Reciepts_Note" %>

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
                                                        <td style="text-align: center;" colspan="2">

                                                             <span id="lblcfpname" runat="server" style="color: maroon; font-size: 22px; font-weight: bold"></span>
                                                            <br />
                                                            <span id="lblcfp" runat="server" style="color: maroon; font-size: 12px; font-weight: bold"></span>
                                                            <br />

                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left;" colspan="2">
                                                           <span id="lblSmallGRNO" runat="server" style="color: maroon; font-size: 22px; font-weight: bold"></span>
                                                           &nbsp;&nbsp;<span style="font-size: 12px;">M/S</span>&nbsp; <span id="lbSupplier" runat="server" style="color: black; font-weight: bold"></span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left;"><span style="font-size: 12px;">Received</span>&nbsp; <span id="lblReceivedqty" runat="server" style="color: black; font-weight: bold"></span></td>
                                                        <td style="text-align: left;"><span style="font-size: 12px;">Bags of</span>&nbsp; <span id="lblItem" runat="server" style="color: black; font-weight: bold"></span></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left;"><span style="font-size: 12px;">On</span>&nbsp; <span id="lblItemReceivedDate" runat="server" style="color: black; font-weight: bold"></span></td>
                                                        <td style="text-align: left;"><span style="font-size: 12px;">Truck No</span>&nbsp; <span id="lblTruckNO" runat="server" style="color: black; font-weight: bold"></span></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left;"><span style="font-size: 12px;">L.R Or R.R No</span>&nbsp; <span id="lbllrrno" runat="server" style="color: black; font-weight: bold"></span></td>
                                                        <td style="text-align: left;"><span style="font-size: 12px;">Date</span>&nbsp; <span id="lblTruckloadingDate" runat="server" style="color: black; font-weight: bold"></span></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left;" colspan="2">
                                                            <span style="font-size: 17px;">Subject to Checking and Approved</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left;"><span style="font-size: 12px;">Total Gross Weight</span></td>
                                                        <td style="text-align: left;"><span id="lblTotalGrosswt" runat="server" style="color: black; font-weight: bold"></span></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left;"><span style="font-size: 12px;">Less Tare Weight</span></td>
                                                        <td style="text-align: left;"><span id="lbltarewt" runat="server" style="color: black; font-weight: bold"></span></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left;"><span style="font-size: 12px;">Net Weight</span></td>
                                                        <td style="text-align: left;"><span id="lblNetWt" runat="server" style="color: black; font-weight: bold"></span></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left;" colspan="2">
                                                            <span style="font-size: 12px;">Remark</span>&nbsp; <span id="lblRemark" runat="server" style="color: black; font-weight: bold"></span>
                                                        </td>
                                                    </tr>
                                                    <tr>
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
                                                    <tr>
                                                        <td style="text-align: left;"><span style="font-size: 12px;">Code / Invoice No</span> &nbsp;&nbsp;&nbsp;<span id="lblInvoiceNo" runat="server" style="color: black; font-weight: bold"></span></td>
                                                        <td style="text-align: left;"><span style="font-size: 12px;">BIg G.R No</span> &nbsp;&nbsp;&nbsp;<span id="lblGrNO" runat="server" style="color: black; font-weight: bold"></span></td>
                                                    </tr>
                                                    
                                                </table>

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

