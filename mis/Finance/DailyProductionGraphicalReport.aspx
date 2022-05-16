<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DailyProductionGraphicalReport.aspx.cs" Inherits="mis_Finance_DailyProductionGraphicalReport" %>

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
                            <table style="width: 100%; text-align: center;">
                                <tr>
                                    <td colspan="1"></td>
                                    <td colspan="4">
                                        <asp:Label ID="lblheadingFirst" Style="font-size: 15px;" runat="server" Text=""></asp:Label></td>
                                    <td colspan="1"></td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-md-12">
                            <%--  <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>--%>
                            <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.5.0/Chart.min.js"></script>
                            <div id="DivChart" runat="server" style="width: 100%;"></div>

                            <%-- <canvas id="mixed-chart" width="800" height="450"></canvas>--%>
                        </div>
                    </div>

                </div>

            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.5.0/Chart.min.js"></script>
    <script>
        new Chart(document.getElementById("line-chart"), {
            type: 'line',
            data: {
                labels: [1500, 1600, 1700, 1750, 1800, 1850, 1900, 1950, 1999, 2050],
                datasets: [{
                    data: [86.25, 114, 106, 106, 107, 111, 133, 221.22, 783, 2478.124],
                    label: "Africa",
                    borderColor: "#3e95cd",
                    fill: false
                }, {
                    data: [282, 350, 411, 502, 635, 809, 947, 1402, 3700, 5267],
                    label: "Asia",
                    borderColor: "#8e5ea2",
                    fill: false
                }, {
                    data: [168, 170, 178, 190, 203, 276, 408, 547, 675, 734],
                    label: "Europe",
                    borderColor: "#3cba9f",
                    fill: false
                }, {
                    data: [40, 20, 10, 16, 24, 38, 74, 167, 508, 784],
                    label: "Latin America",
                    borderColor: "#e8c3b9",
                    fill: false
                }, {
                    data: [6, 3, 2, 2, 7, 26, 82, 172, 312, 433],
                    label: "North America",
                    borderColor: "#c45850",
                    fill: false
                }
                ]
            },
            options: {
                title: {
                    display: true,
                    text: 'World population per region (in millions)'
                }
            }
        });
    </script>--%>
    <%--<script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>--%>
    <%-- <script type="text/javascript">
          google.charts.load('current', { 'packages': ['corechart'] });
          google.charts.setOnLoadCallback(drawChart);

          function drawChart() {
              var data = google.visualization.arrayToDataTable([
                ['Year', 'Sales', 'Expenses'],
                ['2004', 1000, 400],
                ['2005', 1170, 460],
                ['2006', 660, 1120],
                ['2007', 660, 1120],
                ['2008', 660, 1120],
                ['2009', 660, 1120],
                ['2010', 660, 1120],
                ['2011', 660, 1120],

                ['2012', 1030, 540]
              ]);

              var options = {
                  title: 'Company Performance',
                  curveType: 'function',
                  legend: { position: 'bottom' }
              };

              var chart = new google.visualization.LineChart(document.getElementById('curve_chart'));

              chart.draw(data, options);
          }
        
    </script>--%>
    <%-- <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.5.0/Chart.min.js"></script>
    <script>
        new Chart(document.getElementById("mixed-chart"), {
            type: 'bar',
            data: {
                labels: ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17"],
                datasets: [{
                    label: "Europe",
                    type: "line",
                    borderColor: "#8e5ea2",
                    data: [408, 547, 675, 734, 408, 547, 675, 734, 408, 547, 675, 734, 408, 547, 675, 734,0],
                    fill: false
                }, {
                    label: "Africa",
                    type: "line",
                    borderColor: "#3e95cd",
                    data: [133, 221, 783, 2478, 133, 10, 783, 2478, 20, 40, 783, 2478, 133, 221, 783, 2478,5],
                    fill: false
                }
                ]
            },
            options: {
                title: {
                    display: true,
                    text: 'Population growth (millions): Europe & Africa'
                },
                legend: { display: false }
            }
        });--%>


    </script>
</asp:Content>



