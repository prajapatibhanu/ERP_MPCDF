<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="View_CFP_Received_Item_Invoice_Payment.aspx.cs" Inherits="mis_CattleFeed_View_CFP_Received_Item_Invoice_Payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
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
                                <legend>Item Payment Detail
                                </legend>
                                <div style="text-align: right;">
                                    <button type="submit" class="btn btn-primary" onclick="printDiv('printableArea')">Print</button>
                                </div>
                                <div id="printableArea">
                                    <asp:Panel ID="pnl" runat="server" BorderColor="Black" BorderWidth="1">
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
                                                        <asp:BoundField DataField="PaneltyAmt" HeaderText="Panelty Amt" ItemStyle-Width="10%" />
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
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

