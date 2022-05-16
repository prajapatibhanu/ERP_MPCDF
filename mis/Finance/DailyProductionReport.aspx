<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DailyProductionReport.aspx.cs" Inherits="mis_Finance_DailyProductionReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">

    <style>
        .pay-sheet table tr th, .pay-sheet table tr td {
            font-size: 12px;
            width: 10%;
            border: 1px dashed #ddd;
            padding-left: 1px;
            padding-top: 1px;
            line-height: 14px;
            font-family: monospace;
            overflow: hidden;
        }

        .pay-sheet table {
            width: 100%;
        }

            .pay-sheet table thead {
                background: #eee;
            }

        /*.pay-sheet table {
            border: 1px solid #ddd;
        }*/


        @media print {
            .Hiderow, .hide_print, .main-footer, .dataTables_filter, .dataTables_length, .dt-buttons, .dataTables_info, .dataTables_paginate paging_simple_numbers, .pagination {
                display: none;
            }

            .box {
                border: none;
            }

            th {
                background-color: #ddd;
                text-decoration: solid;
            }

            .tblheadingslip {
                font-size: 8px !important;
                background: black;
                color: red;
            }

            .lblheadingFirst p {
                text-align: center !important;
                font-size: 10px !important;
            }
        }

        .inline-rb label {
            margin-left: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <div class="box box-success">
                <div class="box-header Hiderow">
                    <h3 class="box-title">Production Report</h3>
                    <a href="DailyProductionGraphicalReport.aspx" class="btn btn-info pull-right">Monthly Graphical Report</a>&nbsp;
                     <input type="button" class="btn btn-default pull-right" onclick="tableToExcel('tableData', 'W3C Example Table')" value="Excel" />&nbsp;
                    <input type="button" class="btn btn-default pull-right" onclick="window.print();" value="Print" />
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row hidden-print">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Year<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Month <span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlMonth" runat="server" class="form-control">
                                    <asp:ListItem Value="0">Select Month</asp:ListItem>
                                    <asp:ListItem Value="1">January</asp:ListItem>
                                    <asp:ListItem Value="2">February</asp:ListItem>
                                    <asp:ListItem Value="3">March</asp:ListItem>
                                    <asp:ListItem Value="4">April</asp:ListItem>
                                    <asp:ListItem Value="5">May</asp:ListItem>
                                    <asp:ListItem Value="6">June</asp:ListItem>
                                    <asp:ListItem Value="7">July</asp:ListItem>
                                    <asp:ListItem Value="8">August</asp:ListItem>
                                    <asp:ListItem Value="9">September</asp:ListItem>
                                    <asp:ListItem Value="10">October</asp:ListItem>
                                    <asp:ListItem Value="11">November</asp:ListItem>
                                    <asp:ListItem Value="12">December</asp:ListItem>

                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Item Name (वस्तु का नाम)<span style="color: red;"> *</span></label>
                                <asp:DropDownList ID="ddlitems" runat="server" Width="100%" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success btn-block" Style="margin-top: 21px;" OnClick="btnSearch_Click" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a class="btn btn-default btn-block" href="DailyProductionEntryReport.aspx" style="margin-top: 21px;">Cancel</a>

                            </div>
                        </div>
                    </div>

                    <div class="form-group">

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
                    <div class="row" id="tableData">
                        <div class="col-md-12">
                            <table style="width: 100%;">
                                <tr>
                                    <td colspan="5" style="text-align: center;">
                                        <asp:Label ID="lblheadingFirst" Style="font-size: 18px;" runat="server" Text=""></asp:Label></td>
                                    <td colspan="2" style="float: right;">
                                         <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
                                        <div id="DivChart" runat="server" style="width: 100%;"></div>
                                       <%-- <div id="piechart_3d" style="width: 360px; height: 200px;"></div>--%>
                                    </td>
                                </tr>
                            </table>

                        </div>
                        <div class="col-md-12">
                            <asp:GridView ID="GridView1" DataKeyNames="ID" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" ClientIDMode="Static">
                                <Columns>
                                    <asp:TemplateField HeaderText="Date" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Eval("TxnDate").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Name" ItemStyle-Width="40%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" Text='<%# Eval("ItemName").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnitName" Text='<%# Eval("UnitName").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Production Quantity" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProductionQuantity" Text='<%# Eval("ProductionQuantity").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Production Cumulative Quantity" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProductionCumulativeQuantity" Text='<%# Eval("ProductionCumulativeQuantity").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sale Quantity" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSaleQuantity" Text='<%# Eval("SaleQuantity").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sale Cumulative Quantity" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSaleCumulativeQuantity" Text='<%# Eval("SaleCumulativeQuantity").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>

                </div>

            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <link href="css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="css/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/dataTables.bootstrap.min.js"></script>
    <script src="js/dataTables.buttons.min.js"></script>
    <script src="js/buttons.flash.min.js"></script>
    <script src="js/jszip.min.js"></script>
    <script src="js/pdfmake.min.js"></script>
    <script src="js/vfs_fonts.js"></script>
    <script src="js/buttons.html5.min.js"></script>
    <script src="js/buttons.print.min.js"></script>
    <script src="js/buttons.colVis.min.js"></script>
    <script>
        $('.datatable').DataTable({
            paging: true,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],
            "ordering": false,
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
                    },
                    footer: true
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
  <%--  <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">
        google.charts.load("current", { packages: ["corechart"] });
        google.charts.setOnLoadCallback(drawChart);
        function drawChart() {
            var data = google.visualization.arrayToDataTable([
              ['Type', 'Value'],
              ['Capacity', 254400.568],
              //['Eat', 2],
              //['Commute', 2],
              ['Production Cumulative', 251000.35]
              //,
              //['Sleep', 7]
            ]);

            var options = {
                title: 'Production Chart',
                is3D: true,
            };

            var chart = new google.visualization.PieChart(document.getElementById('piechart_3d'));
            chart.draw(data, options);
        }
    </script>--%>
</asp:Content>



