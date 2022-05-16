<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="InvoiceGenerationSSOrDistwise_IDS.aspx.cs" Inherits="mis_DemandSupply_InvoiceGenerationSSOrDistwise_IDS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
         .columngreen {
            background-color: #aee6a3 !important;
        }

        .columnred {
            background-color: #f05959 !important;
        }

        .columnmilk {
            background-color: #bfc7c5 !important;
        }

        .columnproduct {
            background-color: #f5f376 !important;
        }
        .NonPrintable {
                  display: none;
              }
         @media print {
              .NonPrintable {
                  display: block;
              }
              @page {
                size: landscape;
            }
              @page {
                margin: 0 0 0 0;
            }
              
    
              .noprint {
                display: none;
            }
               .pagebreak { page-break-before: always; }
          }

       
         .table1-bordered > thead > tr > th, .table1-bordered > tbody > tr > th, .table1-bordered > tfoot > tr > th, .table1-bordered > thead > tr > td, .table1-bordered > tbody > tr > td, .table1-bordered > tfoot > tr > td {
            border: 1px solid black !important;
           
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
           padding: 5px ;
           
           font-size:15px;
              border: 1px solid black !important;
        }   
        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('../image/loader/ProgressImage.gif') 50% 50% no-repeat rgba(0, 0, 0, 0.3);
        }  
        .table1 > tbody > tr > td, .table1 > tbody > tr > th, .table1 > tfoot > tr > td, .table1 > tfoot > tr > th, .table1 > thead > tr > td, .table1 > thead > tr > th {
    padding: 1px;
    font-size: 10px;
    border: 1px solid black !important;
    font-family: verdana;
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
                            <h3 class="box-title">Milk Invoice Generate</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <fieldset>
                                <legend>Milk Invoice Generate
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
                                            <asp:TextBox runat="server" autocomplete="off" OnTextChanged="txtDeliveryDate_TextChanged" AutoPostBack="true" CssClass="form-control" ID="txtDeliveryDate" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Shift </label>
                                            <asp:DropDownList ID="ddlShift" OnSelectedIndexChanged="ddlShift_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Location</label>
                                            <asp:DropDownList ID="ddlLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Invoice For</label>
                                            <asp:DropDownList ID="ddlInvoiceFor" AutoPostBack="true" OnSelectedIndexChanged="ddlInvoiceFor_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Text="SuperStockist / Distributor" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Institution" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-3" id="pnlSSorDist" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>Superstockist/Distributor Name <span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Superstockist/Distributor Name" Text="<i class='fa fa-exclamation-circle' title='Select Superstockist/Distributor Name !'></i>"
                                                    ControlToValidate="ddlSuperStockist" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlSuperStockist" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                       <div class="col-md-3" id="pnlInstitution" runat="server" visible="false">
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

                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-block btn btn-default" />
                                        </div>
                                    </div>
                                    
                                </div>
                                <div class="row no-print">
                                    <div class="col-md-10" id="pnlremark" runat="server" visible="false">
                                    <div class="form-group">
                                         <label>Remark</label>
                                        <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ForeColor="Red"
                                            ValidationGroup="a" Display="Dynamic" runat="server"
                                            ControlToValidate="txtRemark"
                                            ErrorMessage="Alphanumeric ,space and some special symbols like '.,/-:' allow"
                                            Text="<i class='fa fa-exclamation-circle' title='Alphanumeric ,space and some special symbols like '.,/-:' allow !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^[0-9a-zA-Z\s.,'%/:-]+$">

                                        </asp:RegularExpressionValidator>

                                    </span>
                                      <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtRemark" MaxLength="200" placeholder="Enter Remark" ClientIDMode="Static"></asp:TextBox>

                                    </div>
                                </div>
                                     <div class="col-md-2" style="margin-top: 20px;">
                                    <div class="form-group">
                                        <asp:Button ID="btnPrint" Visible="false" runat="server" Text="Save & Print" CssClass="btn btn-success btn-block no-print" ValidationGroup="a" OnClick="btnPrint_Click" OnClientClick="return ValidatePage();" />

                                    </div>
                                </div>
                                </div>
                                <div class="row no-print" id="pnlData" runat="server" visible="false">
                                            <div class="col-md-12">
                                                        <asp:GridView ID="GridView1" runat="server" Width="100%" ShowFooter="true" class="table table-striped table-bordered" AllowPaging="false"
                                                            AutoGenerateColumns="False" EmptyDataText="No Record Found" OnRowDataBound="GridView1_RowDataBound" EnableModelValidation="True">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Particulars">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblitemDistOrSSRate" Visible="false" Text='<%# Eval("FinalSSRate")%>' runat="server" />
                                                                         <asp:Label ID="lblTotalReturnQtyInLtr" Visible="false" Text='<%# Eval("TotalReturnQtyInLtr")%>' runat="server" />
                                                                        <asp:Label ID="lblItem_id" Visible="false" Text='<%# Eval("Item_id")%>' runat="server" />
                                                                        <asp:Label ID="lblIName" Text='<%# Eval("IName")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <b>Total</b>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qty (In Pkt)">
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
                                                                  <asp:TemplateField HeaderText="Inst Qty (In Pkt.)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblInstQty" Text='<%# Eval("TotalInstSupplyQty")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Advanced Card Qty (In Ltr.)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotalAdvCardQtyInLtr" Text='<%# Eval("TotalAdvCardQtyInLtr")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Advanced Card Margin">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAdvCardComm" Text='<%# Eval("AdvCardComm")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Advanced Card Margin Amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAdvCardAmt" Text='<%# Eval("AdvCardAmt")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTotalAdvCardAmt" runat="server"></asp:Label>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Advanced Card Amount">
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="lblAdvCardTotalAmount" Text='<%# (Session["Office_ID"].ToString()=="6"? Eval("AdVCardTotalAmount_UDS") : Eval("AdVCardTotalAmount"))%>' runat="server" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblFAdvCardTotalAmount" runat="server"></asp:Label>
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
                                                                 <asp:TemplateField HeaderText="Payble Amount">
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="lblFAmount" Text='<%# (Session["Office_ID"].ToString()=="6"? Eval("Amount") : Convert.ToDecimal(Eval("Amount")) - Convert.ToDecimal((Eval("AdvCardAmt"))) ) %>' runat="server" />
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
                                 <div class="row" id="pnltcxdata" runat="server" visible="false">
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
                            </fieldset>

                        </div>

                    </div>
                </div>
            </div>
        </section>
        <!-- /.content -->
        <section class="content">
               <div id="Print" runat="server" class="NonPrintable"></div>
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

