<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CMApp_CitizenTrn_Rpt.aspx.cs" Inherits="mis_DemandSupply_CMApp_CitizenTrn_Rpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
     <link href="../Finance/css/jquery.dataTables.min.css" rel="stylesheet" />
    <style>
        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('../image/loader/ProgressImage.gif') 50% 50% no-repeat rgba(0, 0, 0, 0.3);
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="loader"></div>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />

    <div class="content-wrapper">
        <section class="content">
            <div class="row">

                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Citizen Order Report </h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" CssClass="Autoclr" runat="server"></asp:Label>
                                </div>
                            </div>
                            <fieldset>
                                <legend>Date,Retailer,Citizen
                                </legend>
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>From Date <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter From Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter From Date !'></i>"
                                                    ControlToValidate="txtFromDate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtFromDate"
                                                    ErrorMessage="Invalid From Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtFromDate" MaxLength="10" placeholder="Enter From Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>To Date <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                    ErrorMessage="Enter To Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter From Date !'></i>"
                                                    ControlToValidate="txtToDate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtToDate"
                                                    ErrorMessage="Invalid To Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtToDate" MaxLength="10" placeholder="Enter From Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Retailer Name</label>
                                            <asp:DropDownList ID="ddlRetailer" runat="server" ClientIDMode="Static" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Citizen Name</label>
                                            <asp:DropDownList ID="ddlCitizen" runat="server" ClientIDMode="Static" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Order Status</label>
                                            <asp:DropDownList ID="ddlOrderStatus" runat="server" ClientIDMode="Static" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" OnClick="btnSearch_Click" ValidationGroup="a" ID="btnSearch" Text="Search" />
                                        </div>
                                    </div>
                                    <div class="col-md-1" style="margin-top: 20px;">
                                        <div class="form-group">
                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Reset" CssClass="btn btn-block btn-secondary" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div class="row">


                            <div class="col-md-12" id="pnlsearch" runat="server" visible="false">
                                <fieldset>
                                    <legend>Citizen Order Report
                                    </legend>
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" class="datatable table table-striped table-bordered" AllowPaging="false"
                                            AutoGenerateColumns="False" EmptyDataText="No Record Found." EnableModelValidation="True" DataKeyNames="CitizenOrderId">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCitizenOrderId" runat="server" Text='<%# Eval("CitizenOrderId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderDatetime" runat="server" Text='<%# Eval("OrderDatetime") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderStatus" runat="server" Text='<%# Eval("OrderStatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalAmt" runat="server" Text='<%# Eval("TotalAmt") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Payment Mode">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPaymentMode" runat="server" Text='<%# Eval("PaymentMode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Transaction No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTrnId" runat="server" Text='<%# Eval("TrnId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Refund No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInitiateRefund_Id" runat="server" Text='<%# Eval("InitiateRefund_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Citzen Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCName" runat="server" Text='<%# Eval("CName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Retailer">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBName" runat="server" Text='<%# Eval("BName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Particular">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPaymentMode" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                </fieldset>


                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script src="../Finance/js/jquery.dataTables.min.js"></script>
    <script src="../Finance/js/dataTables.bootstrap.min.js"></script>
    <script src="../Finance/js/dataTables.buttons.min.js"></script>
    <script src="../Finance/js/buttons.flash.min.js"></script>
    <script src="../Finance/js/jszip.min.js"></script>
    <script src="../Finance/js/pdfmake.min.js"></script>
    <script src="../Finance/js/vfs_fonts.js"></script>
    <script src="../Finance/js/buttons.html5.min.js"></script>
    <script src="../Finance/js/buttons.print.min.js"></script>
    <script src="js/buttons.colVis.min.js"></script>

    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);

        $(document).ready(function () {
            $('.loader').fadeOut();
        });

        $('.datatable').DataTable({
            paging: true,
            lengthMenu: [10, 25, 50, 100, 200, 500],
            iDisplayLength: 500,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    title: ('Citzen Order Details').bold().fontsize(5).toUpperCase(),
                    text: '<i class="fa fa-print"></i> Print',
                    exportOptions: {
                        columns: [1, 2, 3, 4, 5, 6, 7, 8,9,10]
                    },
                    footer: false,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    title: ('Citzen Order Details').bold().fontsize(5).toUpperCase(),
                    filename: 'CitzenOrderDetails',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',

                    exportOptions: {
                        columns: [1, 2, 3, 4, 5, 6, 7, 8,9,10]
                    },
                    footer: false
                }],
                dom: {
                    container: {
                        className: 'dt-buttons'
                    },
                    button: {
                        className: 'btn btn-default'
                    }
                }
            }
        });
    </script>
</asp:Content>
