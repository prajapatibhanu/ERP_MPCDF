<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="BankwiseSocietyPaymentDetail.aspx.cs" Inherits="mis_MilkCollection_BankwiseSocietyPaymentDetail" %>

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

            .pagebreak {
                page-break-before: always;
            }
        }

        .table1 > tbody > tr > td, .table1 > tbody > tr > th, .table1 > tfoot > tr > td, .table1 > tfoot > tr > th, .table1 > thead > tr > td, .table1 > thead > tr > th {
            padding: 4px 2px;
            font-size: 10.5px;
        }
    </style>

    <script type="text/javascript">
        function confirmation() {
            if (confirm('Are you sure you want to save Society Payment In excel format ?')) {
                return true;
            } else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content noprint">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">
                        <asp:Label ID="lblUnitName" Text="Bank wise Society Payment Detail" ForeColor="White" ClientIDMode="Static" Font-Size="Medium" Font-Bold="true" runat="server"></asp:Label></h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">

                    <fieldset>
                        <legend>Filter</legend>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <label>Bank</label>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlBank" CssClass="form-control select2" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--<div class="col-md-2">
                                    <label>Milk Collection Unit<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ValidationGroup="a" ControlToValidate="ddlMilkCollectionUnit" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Milk Collection Unit!'></i>" ErrorMessage="Select Milk Collection Unit" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlMilkCollectionUnit" AutoPostBack="true" OnSelectedIndexChanged="ddlMilkCollectionUnit_SelectedIndexChanged" CssClass="form-control select2" runat="server">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="5">BMC</asp:ListItem>
                                            <asp:ListItem Value="6">DCS</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <label>Society<span style="color: red;">*</span></label>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlSociety" CssClass="form-control select2" runat="server">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>--%>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>From Date  </label>
                                        <span style="color: red">*</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtFdt" ErrorMessage="Please Enter From Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter From Date !'></i>"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revdate" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtFdt" ErrorMessage="Invalid From Date" Text="<i class='fa fa-exclamation-circle' title='Invalid From Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                        </span>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtFdt" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select From Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>To Date  </label>
                                        <span style="color: red">*</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtTdt" ErrorMessage="Please Enter To Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter To Date !'></i>"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtTdt" ErrorMessage="Invalid To Date" Text="<i class='fa fa-exclamation-circle' title='Invalid To Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                        </span>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtTdt" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select To Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>


                                <div class="col-md-1" style="margin-top: 20px;" runat="server">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click" ValidationGroup="a" ID="btnSubmit" Text="Search" AccessKey="S" />
                                    </div>
                                </div>

                            </div>
                        </div>
                    </fieldset>

                    <asp:Label ID="lblmsgshow" Visible="false" runat="server"></asp:Label>

                    <fieldset runat="server" visible="false" id="FS_DailyReport">
                        <legend>Detail</legend>

                        <div class="row">

                            <div class="col-md-4">
                                <div class="form-group">
                                    &nbsp;
                                </div>
                            </div>

                            <div class="col-md-2 pull-right">
                                <div class="form-group">
                                    <asp:Button ID="btnSave" runat="server" Text="Export To Excel" OnClientClick="return confirmation();" CssClass="btn btn-primary btn-block" OnClick="btnSave_Click" />
                                </div>
                            </div>
                            <div class="col-md-2 pull-right">
                                <div class="form-group">
                                    <asp:Button runat="server" ID="btnPrint" Text="Print (Ctrl + P)" CssClass="btn btn-default pull-right" AccessKey="p" OnClientClick="printdiv('div_print');" />
                                </div>
                            </div>

                        </div>


                        <div class="row" id="div_print">

                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="gv_SocietyMilkDispatchDetail" EmptyDataText="No Record Found" ShowHeaderWhenEmpty="true" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S. NO">
                                                <ItemTemplate>
                                                    <asp:Label ID="SNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bank Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBankName" runat="server" Text='<%# Eval("BankName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Society Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Net Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNetAmount" runat="server" Text='<%# Eval("NetAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                    </fieldset>


                </div>
            </div>
        </section>
        <%--Confirmation Modal Start --%>

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
                            <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" Style="margin-top: 20px; width: 50px;" />
                            <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
        </div>
        <%--ConfirmationModal End --%>
        <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="Save" ShowMessageBox="true" ShowSummary="false" />
        <section class="content">
            <div id="dvReport" class="NonPrintable" runat="server">
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script lang="javascript">
        function printdiv(printpage) {
            var headstr = "<html><head><title>Reported Society Invoice</title></head><body>";
            var footstr = "</body>";
            var newstr = "<center><h5>Society Invoice</h5></center><br/>" + document.all.item(printpage).innerHTML;
            var oldstr = document.body.innerHTML;
            document.body.innerHTML = headstr + newstr + footstr;
            window.print();
            document.body.innerHTML = oldstr;
            return false;
        }
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('b');
            }

            if (Page_IsValid) {


            }
        }
    </script>
</asp:Content>

