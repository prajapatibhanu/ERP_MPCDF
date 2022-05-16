<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MIS_DailyFReport.aspx.cs" Inherits="mis_Mis_Reports_MIS_DailyFReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="mpr" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="box box-primary">
                    <div class="box-header">
                        <h3 class="box-title">Daily F Report</h3>
                        <asp:Label ID="lblMsg" runat="server" />
                    </div>
                    <div class="box-body">
                        <div class="row no-print">
                            <div class="col-md-12">
                                <fieldset>
                                    <legend>Daily F Report</legend>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Date<span style="color: red;">*</span></label>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtToDate" runat="server" placeholder="Select Date" class="form-control" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Button ID="btnSearch" runat="server" Style="margin-top: 21px;" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2 pull-right">
                                <div runat="server" id="divExcel">
                                    <input type="button" class="btn btn-primary" onclick="tableToExcel('tableData', 'W3C Example Table')" value="Export to Excel">
                                    <input type="button" class="btn btn-default" onclick="window.print()" value="Print">
                                </div>
                                <script type="text/javascript">
                                    var tableToExcel = (function () {
                                        var uri = 'data:application/vnd.ms-excel;base64,'
                                          , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--><meta http-equiv="content-type" content="text/plain; charset=UTF-8"/></head><body><table>{table}</table></body></html>'
                                          , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
                                          , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
                                        return function (table, name) {
                                            if (!table.nodeType) table = document.getElementById(table)
                                            var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
                                            window.location.href = uri + base64(format(template, ctx))
                                        }
                                    })()
                                </script>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table table-responsive">
                                    <div id="tableData">
                                        <div id="DivDetail" runat="server"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script src="../Finance/js/buttons.colVis.min.js"></script>
    <script>
        $("#txtToDate").datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        });
    </script>
</asp:Content>

