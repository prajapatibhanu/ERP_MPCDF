<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Rpt_Distributor_Merge_dues_BGJDS.aspx.cs" Inherits="mis_DemandSupply_Rpt_Distributor_Merge_dues_BGJDS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <style>
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
            .noprint {
                display: none;
            }

            .pagebreak {
                page-break-before: always;
            }
        }


        .table1-bordered > thead > tr > th, .table1-bordered > tbody > tr > th, .table1-bordered > tfoot > tr > th, .table1-bordered > thead > tr > td, .table1-bordered > tbody > tr > td, .table1-bordered > tfoot > tr > td {
            border: 1px solid #000000 !important;
        }

        .thead {
            display: table-header-group;
        }

        .text-center {
            text-align: center;
        }

        .text-right {
            text-align: right;
        }

        .table1 > tbody > tr > td, .table1 > tbody > tr > th, .table1 > tfoot > tr > td, .table1 > tfoot > tr > th, .table1 > thead > tr > td, .table1 > thead > tr > th {
            padding: 5px;
            font-size: 15px;
            text-align: center;
        }
        
        .table1 > tbody > tr > td, .table1 > tbody > tr > th, .table1 > tfoot > tr > td, .table1 > tfoot > tr > th, .table1 > thead > tr > td, .table1 > thead > tr > th {
    padding: 1px;
    font-size: 10px;
    border: 1px solid black !important;
    font-family: verdana;
}
        .table1 td {
            word-break: break-word;
            padding:3px;
            min-width:90px !important;
        }
    </style>
    <div class="loader"></div>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content noprint">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Distributor Merge Dues Report</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">

                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <fieldset>
                                    <legend>Distributor Merge Dues Report
                                    </legend>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>From Date<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a" ControlToValidate="txtFromDate"
                                                    ErrorMessage="Enter mDate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtFromDate"
                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtFromDate" MaxLength="10" placeholder="Enter FromDate" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>To Date<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a" ControlToValidate="txtToDate"
                                                    ErrorMessage="Enter ToDate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtToDate"
                                                    ErrorMessage="To Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtToDate" MaxLength="10" placeholder="Enter ToDate" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>

                                    </div>
                                  
                                    <div class="col-md-1">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:Button runat="server" OnClientClick="return ValidatePage();" CssClass="btn btn-block btn-primary" ValidationGroup="a" OnClick="btnSearch_Click" ID="btnSearch" Text="Search" AccessKey="S" />
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                           
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12" id="pnlData" runat="server" visible="false">

                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Distributor Merge Dues Report</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblReportMsg" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-2 pull-right">
                                    <div class="form-group">
                                        <asp:Button ID="btnprint" runat="server" CssClass="btn btn-primary" Text="Print" OnClick="btnprint_Click" />
                                        <asp:Button ID="btnExportAll" runat="server" CssClass="btn btn-success" OnClick="btnExportAll_Click" Text="Export" />
                                    </div>

                                </div>
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <div id="div_page_content" runat="server" class="page_content"></div>
                                  </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                    </div>
                </div>
            </div>
            <%-- </div>--%>
        </section>

        <!-- /.content -->
        <section class="content">
            <div id="Print" runat="server" class="NonPrintable"></div> 
        </section>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSearch.ClientID%>').value.trim() == "Update") {
                    <%-- document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";--%>
                    $('#myModal').modal('show');
                    return false;
                }
            }
            function Print() {
                debugger;
                $("#ctl00_ContentBody_Print").show();
                $("#ctl00_ContentBody_Print1").hide();
                window.print();
                $("#ctl00_ContentBody_Print1").hide();
                $("#ctl00_ContentBody_Print").hide();
            }
            function Print1() {

                $("#ctl00_ContentBody_Print1").show();
                $("#ctl00_ContentBody_Print").hide();
                window.print();
                $("#ctl00_ContentBody_Print1").hide();
                $("#ctl00_ContentBody_Print").hide();
            }
            function myItemDetailsModal() {
                $("#ItemDetailsModal").modal('show');

            }
        }

    </script>
</asp:Content>
