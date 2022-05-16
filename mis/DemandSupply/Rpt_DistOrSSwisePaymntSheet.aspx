<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Rpt_DistOrSSwisePaymntSheet.aspx.cs" Inherits="mis_DemandSupply_Rpt_DistOrSSwisePaymntSheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .show_detail {
            margin-top: 24px;
        }

        .table > tbody > tr > th {
            padding: 5px;
        }

        a:hover {
            color: red;
        }

        /*tr:hover td {
            background-color: #fefefe !important;
        }*/
        table.dataTable tbody td, table.dataTable thead td {
            padding: 5px 5px !important;
        }

        table.dataTable tbody th, table.dataTable thead th {
            padding: 8px 10px !important;
        }

        table.dataTable thead th, table.dataTable thead td {
            padding: 5px 7px;
            border-bottom: none !important;
        }

        table.dataTable tfoot th, table.dataTable tfoot td {
            border-bottom: none !important;
        }

        table.dataTable.no-footer {
            border-bottom: none !important;
        }

        a.dt-button.buttons-collection.buttons-colvis, a.dt-button.buttons-collection.buttons-colvis:hover {
            background: #EF5350;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.dt-button.buttons-excel.buttons-html5, a.dt-button.buttons-excel.buttons-html5:hover {
            background: #ff5722c2;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.dt-button.buttons-print, a.dt-button.buttons-print:hover {
            background: #e91e639e;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
        }

        /*thead tr th {
            background: #9e9e9ea3 !important;
        }*/

        /*tbody tr td:not(:first-child), tfoot tr td:not(:first-child) {
            text-align: right !important;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">PAYMENT SHEET REPORT</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <fieldset>
                        <legend>DATE</legend>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>From Date<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                        ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                        ControlToValidate="txtFromDate" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtFromDate"
                                        ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                        ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                </span>
                                <%--<asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOrderDate" OnTextChanged="txtOrderDate_TextChanged" AutoPostBack="true" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>--%>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtFromDate" MaxLength="10" placeholder="Select From Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>To Date<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                        ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                        ControlToValidate="txtToDate" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtToDate"
                                        ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                        ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                </span>
                                <%--<asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOrderDate" OnTextChanged="txtOrderDate_TextChanged" AutoPostBack="true" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>--%>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtToDate" MaxLength="10" placeholder="Select From Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2" style="margin-top:20px;">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
                            </div>
                        </div>
                        <div class="col-md-2" style="margin-top:20px;">
                            <div class="form-group">
                                <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-block btn btn-default" />
                            </div>
                        </div>
                    </fieldset>

                </div>
                <div class="box-body" id="pnldata" runat="server" visible="false">
                    <fieldset>
                        <legend>PAYMENT SHEET REPORT</legend>
                       
                        <div class="col-md-1 pull-left">
                            <div class="form-group">
                             <asp:Button ID="btnExport" runat="server" CssClass="btn btn-success" OnClick="btnExport_Click" Text="Export"  />
                                </div>
                        </div>

                       
                      
                        <asp:GridView ID="GridView1" runat="server" CssClass="datatable table table-bordered" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" AutoGenerateColumns="false" ShowFooter="true">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Distributor">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDistributor" runat="server" Text='<%# Eval("DTName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDelivary_Date" runat="server" Text='<%# Eval("Delivary_Date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                             
                                <asp:TemplateField HeaderText="Milk Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Mode">
                                <ItemTemplate>
                                    <asp:Label ID="lblPayment" runat="server" Text='<%# Eval("PaymentMode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Payment Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPaymentDate" runat="server" Text='<%# Eval("PaymentDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque/NEFT/RTGS/DD No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChequeNo" runat="server" Text='<%# Eval("ChequeNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChequeAmount" runat="server" Text='<%# Eval("ChequeAmount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBalance" runat="server" Text='<%# Eval("Balance") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </fieldset>

                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>