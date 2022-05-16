<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="InvoiceGenerateDistOrInst_GDS.aspx.cs" Inherits="mis_DemandSupply_InvoiceGenerateDistOrInst_GDS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .NonPrintable {
                  display: none;
              }
        @media print {
            .NonPrintable {
                display: block;
            }

            .noprint {
                display: none;
            }
        }
        .table1-bordered > thead > tr > th, .table1-bordered > tbody > tr > th, .table1-bordered > tfoot > tr > th, .table1-bordered > thead > tr > td, .table1-bordered > tbody > tr > td, .table1-bordered > tfoot > tr > td {
            border: 1px solid #000000 !important;
        }
        .thead
        {
            display:table-header-group;
        }
        .text-center{
            text-align: center;
        }
        .text-right{
            text-align: right;
        }
        .table1 > tbody > tr > td, .table1 > tbody > tr > th, .table1 > tfoot > tr > td, .table1 > tfoot > tr > th, .table1 > thead > tr > td, .table1 > thead > tr > th {
            padding: 2px 5px;
           
        } 
    </style>
     </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnPrint_Click"  Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content noprint">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">
                <div class="col-md-12 no-print">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Invoice Generate</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <fieldset>
                                <legend>Invoice  Generate
                                </legend>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </div>
                                </div>


                                <div class="row no-print">

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Date <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    ControlToValidate="txtDeliveryDate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtDeliveryDate"
                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDeliveryDate" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Shift </label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Shift" Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"
                                                    ControlToValidate="ddlShift" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                     <%-- <div class="col-md-3">
                                      <div class="form-group">
                                            <label>Item Category<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvic" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Item Category" Text="<i class='fa fa-exclamation-circle' title='Select Item Category !'></i>"
                                                    ControlToValidate="ddlItemCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlItemCategory" Enabled="false" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>--%>
                                     <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Location<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Location" Text="<i class='fa fa-exclamation-circle' title='Select Location !'></i>"
                                                    ControlToValidate="ddlLocation" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Route<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Route" Text="<i class='fa fa-exclamation-circle' title='Select Route !'></i>"
                                                    ControlToValidate="ddlRoute" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlRoute" AutoPostBack="true" OnSelectedIndexChanged="ddlRoute_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                                 <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Invoice For<span style="color: red;"> *</span></label>
                                             <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvif" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Invoice For" Text="<i class='fa fa-exclamation-circle' title='Select Invoice For !'></i>"
                                                    ControlToValidate="ddlInvoiceFor" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlInvoiceFor" AutoPostBack="true" OnSelectedIndexChanged="ddlInvoiceFor_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Distributor" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Institution" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-2" id="pnldistorss" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>Distributor Name <span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Distributor/Superstockist Name" Text="<i class='fa fa-exclamation-circle' title='Select Distributor/Superstockist Name !'></i>"
                                                    ControlToValidate="ddlDitributor" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlDitributor" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                       <div class="col-md-2" id="pnlInstitution" runat="server" visible="false">
                                        <div class="form-group">
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Institution" Text="<i class='fa fa-exclamation-circle' title='Select Institution !'></i>"
                                                    ControlToValidate="ddlInstitution" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <label>Institution <span style="color: red;"> *</span></label>
                                            <asp:DropDownList ID="ddlInstitution" runat="server" CssClass="form-control select2">
                                                 <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                              </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" ValidationGroup="a" ID="btnSearch" Text="Generate" />

                                        </div>
                                    </div>
                                    <div class="col-md-1" style="margin-top: 20px;">
                                        <div class="form-group">

                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-default" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                        </div>

                    </div>
                </div>
                <div class="col-md-12" id="pnlData" runat="server" visible="false">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Invoice Details</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblReportMsg" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <%-- <div class="col-md-10"></div>--%>
                                <div class="col-md-2 pull-right">
                                    <div class="form-group">
                                        <asp:Button ID="btnPrint" Visible="false" runat="server" Text="Save & Print" CssClass="btn btn-primary btn-block no-print" ValidationGroup="a" OnClick="btnPrint_Click" OnClientClick="return ValidatePage();" />

                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div id="pnlprint" runat="server">
                                    <div class="col-md-12">
                                        <%-- <fieldset>
                                        <legend>Invoice Details</legend>--%>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <%--<img src="../image/ds_logo_icon.png" />--%>
                                            </div>
                                            <div class="col-md-4">
                                                <div style="text-align: center">
                                                    <b><span style="text-align: center"> <asp:Label ID="lblOName1" runat="server"></asp:Label><%--Bhopal Sahakari Dugdha Sang Maryadit--%></span><br />
                                                        <span style="text-align: center">Bill Book</span><br />
                                                        <span style="text-align: center">G.S.T/U.I.N NO:- <asp:Label ID="lblGST" runat="server"></asp:Label> <%--23AAAB0221D1ZE--%></span></b>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <b>No.<asp:Label ID="lblDelivarydate" runat="server"></asp:Label></b>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <b>Shri/M/s
                                            <asp:Label ID="lblMsName" runat="server"></asp:Label></b>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <b>
                                                   <asp:Label ID="lblDelishift" runat="server" class="pull-left"></asp:Label><asp:Label class="pull-right" ID="lblVehicleNo" runat="server"></asp:Label></b>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <b>
                                                    <asp:Label ID="lbldelidate" runat="server" class="pull-left"></asp:Label><asp:Label class="pull-right" ID="lblRouteName" runat="server"></asp:Label></b>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="table-responsive">

                                          
                                            <div class="col-md-12">
                                                        <asp:GridView ID="GridView1" runat="server" Width="100%" ShowFooter="true" class="table table-striped table-bordered" AllowPaging="false"
                                                            AutoGenerateColumns="False" EmptyDataText="No Record Found" OnRowDataBound="GridView1_RowDataBound" EnableModelValidation="True">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Particulars" HeaderStyle-Width="10px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblitemDistOrSSRate" Visible="false" Text='<%# Eval("DistOrSSRate")%>' runat="server" />
                                                                         <asp:Label ID="lblTotalReturnQtyInLtr" Visible="false" Text='<%# Eval("TotalReturnQtyInLtr")%>' runat="server" />
                                                                        <asp:Label ID="lblItem_id" Visible="false" Text='<%# Eval("Item_id")%>' runat="server" />
                                                                        <asp:Label ID="lblRouteId" Visible="false" Text='<%# Eval("RouteId")%>' runat="server" />
                                                                        <asp:Label ID="lblIName" Text='<%# Eval("IName")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <b>Total</b>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qty (In Pkt)" HeaderStyle-Width="10px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSupplyTotalQty" Text='<%# Eval("SupplyTotalQty")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Return Qty (In Pkt.)" HeaderStyle-Width="10px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotalReturnQty" Text='<%# Eval("TotalReturnQty")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Adv. Card Qty (In Pkt.)" HeaderStyle-Width="10px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotalAdvCardQty" Text='<%# Eval("TotalAdvCardQty")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Adv. Card Qty (In Ltr.)" HeaderStyle-Width="10px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotalAdvCardQtyInLtr" Text='<%# Eval("TotalAdvCardQtyInLtr")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Adv. Card Margin" HeaderStyle-Width="10px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAdvCardComm" Text='<%# Eval("AdvCardComm")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Adv. Card Margin Amt" HeaderStyle-Width="10px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAdvCardAmt" Text='<%# Eval("AdvCardAmt")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTotalAdvCardAmt" runat="server"></asp:Label>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Inst. Qty" HeaderStyle-Width="10px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblInstQty" Text='<%# Eval("TotalInstSupplyQty")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Inst. Qty (In Ltr.)" HeaderStyle-Width="10px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblInstLtr" Text='<%# Eval("TotalInstSupplyQtyInLtr") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                     <FooterTemplate>
                                                                        <asp:Label ID="lblFITAmt" runat="server"></asp:Label>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Inst. Margin" HeaderStyle-Width="10px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblInstTransComm" Text='<%# Eval("InstTransComm")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Inst. Tran Margin Amt" HeaderStyle-Width="10px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblInstTransCommAmt" Text='<%# Eval("InstTransCommAmt")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                        <FooterTemplate>
                                                                        <asp:Label ID="lblFInstMarAmt" runat="server"></asp:Label>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Billing Qty (In Pkt.)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBillingQty" Text='<%# Eval("BillingQty")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Billing Qty (In Ltr.)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBillingQtyInLtr" Text='<%# Eval("BillingQtyInLtr")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Rate (Per Ltr.)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRatePerLtr" Text='<%# Eval("RatePerLtr")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAmount" Text='<%# Eval("Amount")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTAmount" runat="server"></asp:Label>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Net Payble Amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFAmount" Text='<%# Convert.ToDecimal(Eval("Amount")) - Convert.ToDecimal(Eval("AdvCardAmt"))- Convert.ToDecimal(Eval("InstTransCommAmt")) %>' runat="server" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblFinalAmount" runat="server"></asp:Label>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>

                                                        <asp:GridView ID="GridView2" runat="server"  Width="100%" ShowFooter="true" class="table table-striped table-bordered" AllowPaging="false"
                                                            AutoGenerateColumns="False" EmptyDataText="No Record Found" OnRowDataBound="GridView2_RowDataBound" EnableModelValidation="True">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Particulars">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblitemDistOrSSRate" Visible="false" Text='<%# Eval("DistOrSSRate")%>' runat="server" />
                                                                        <asp:Label ID="lblItem_id" Visible="false" Text='<%# Eval("Item_id")%>' runat="server" />
                                                                        <asp:Label ID="lblRouteId" Visible="false" Text='<%# Eval("RouteId")%>' runat="server" />
                                                                        <asp:Label ID="lblIName" Text='<%# Eval("IName")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <b>Total</b>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qty(In Pkt)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSupplyTotalQty" Text='<%# Eval("SupplyTotalQty")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Advanced Card Qty (In Pkt.)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotalAdvCardQty" Text='<%# Eval("TotalAdvCardQty")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Return Qty (In Pkt.)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotalReturnQty" Text='<%# Eval("TotalReturnQty")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                   <asp:TemplateField HeaderText="Advanced Card Qty (In Ltr.)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotalAdvCardQtyInLtr" Text='<%# Eval("TotalAdvCardQtyInLtr")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Billing Qty (In Pkt.)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBillingQty" Text='<%# Eval("BillingQty")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Billing Qty (In Ltr.)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBillingQtyInLtr" Text='<%# Eval("BillingQtyInLtr")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Rate (Per Ltr)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRatePerLtr" Text='<%# Eval("RatePerLtr")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Payble Amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAmount" Text='<%# Eval("Amount")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblFinalAmount" runat="server"></asp:Label>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                            </div>
                                                  </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3 pull-right">
                                                <table class="table table1-bordered">
                                                    <tr>
                                                        <td>
                                                            Tcs on Sales @ <asp:Label ID="lblTcsTax" Font-Bold="true" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblTcsTaxAmt" Font-Bold="true" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                           Final Payble Amount
                                                        </td>
                                                        <td>
                                                           <asp:Label ID="lblFinalPaybleAmount" Font-Bold="true" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                 
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <span class="pull-left">Prepared & Checked by </span><span class="pull-right">For  <asp:Label ID="lblOName2" runat="server"></asp:Label></span>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div style="text-align: center; padding-top: 15px;">
                                                    <span>General Manager (Marketing)/Asst.Gen.Manager (Marketing)</span>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row" style="padding-top: 8px;">
                                            <div class="col-md-12">
                                                <ul type="none">
                                                    <li>Note: 1 . Bills not Paid within 15 Days of presentation will be liable an Interest @18% per annum.</li>
                                                    <li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2 . Please quote our Bill No. while remiting the amount.</li>
                                                    <li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3 . All Payment to be made by Bank Draft payable to <asp:Label ID="lblOName3" runat="server"></asp:Label></li>
                                                </ul>
                                            </div>
                                        </div>
                                        <%-- </fieldset>--%>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </section>
        <!-- /.content -->
        <section class="content">
               <div id="div_page_content" runat="server" class="NonPrintable"></div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
     <asp:Label ID="lbldistributerMONO" runat="server" Visible="false"></asp:Label>
    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);

        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {


                if (document.getElementById('<%=btnPrint.ClientID%>').value.trim() == "Save & Print") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save and Print this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
        function Print() {
            window.print();
        }
    </script>
</asp:Content>

