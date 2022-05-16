<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="dashboard_dcs.aspx.cs" Inherits="mis_dashboard_dcs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper" style="min-height: 414px;">
        <section class="content">
            <div class="row">
                <div class="col-md-6">
                    <div class="box box-success">
                        <div class="box-body">
                            <figure class="highcharts-figure">
                                <div id="container"></div>
                                <table id="datatable" style="display: none;">
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th>Milk Collection</th>
                                            <th>Local Milk Sale</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <th>18 Feb</th>
                                            <td>350</td>
                                            <td>250</td>
                                        </tr>
                                        <tr>
                                            <th>17 Feb</th>
                                            <td>200</td>
                                            <td>150</td>
                                        </tr>
                                        <tr>
                                            <th>16 Feb</th>
                                            <td>455</td>
                                            <td>110</td>
                                        </tr>
                                        <tr>
                                            <th>15 Feb</th>
                                            <td>150</td>
                                            <td>100</td>
                                        </tr>
                                        <tr>
                                            <th>14 Feb</th>
                                            <td>200</td>
                                            <td>150</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </figure>
                        </div>
                        <!-- /.box-body -->
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="box box-success">
                        <div class="box-body">
                            <figure class="highcharts-figure">
                                <div id="container2"></div>
                            </figure>
                        </div>
                    </div>
                </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script src="js/highcharts.js"></script>
    <script src="js/chart_data.js"></script>

    <script>
        Highcharts.chart('container', {
            data: {
                table: 'datatable',
                datable: false
            },
            chart: {
                type: 'column'
            },
            title: {
                text: 'Last Five Day Milk Collection & Local Milk Sell at DCS'
            },
            yAxis: {
                allowDecimals: false,
                title: {
                    text: 'Milk (In Liter)'
                }
            },
            tooltip: {
                formatter: function () {
                    return '<b>' + this.series.name + '</b><br/>' +
                        this.point.y + ' ' + this.point.name.toLowerCase();
                }
            }
        });
        Highcharts.chart('container2', {
            chart: {
                type: 'column'
            },
            title: {
                text: 'Last Months Local Product Sales'
            },
            subtitle: {
                // text: 'Source: <a href="#">Wikipedia</a>'
            },
            xAxis: {
                type: 'category',
                labels: {
                    rotation: -45,
                    style: {
                        fontSize: '11px',
                        fontFamily: 'Verdana, sans-serif'
                    }
                }
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'Total Sales (In Kg)'
                }
            },
            legend: {
                enabled: false
            },
            tooltip: {
                pointFormat: 'sales in this month: <b>{point.y:.1f} Kg</b>'
            },
            series: [{
                name: 'Products',
                data: [
                    ['Ghee', 24.2],
                    ['Cattle Feed', 20.8],
                    ['Buffalo Feed', 15.9],
                    ['Guar Meal', 13.7],
                    ['Maize Cattle Feed', 14.1],
                    ['Lecithin Cattle Feed', 12.7],
                    ['Chana Churi', 12.4],
                    ['Maize Bran', 11.2],
                    ['Alfalfa Hay', 13.0]
                ],
                dataLabels: {
                    enabled: true,
                    rotation: -90,
                    color: '#FFFFFF',
                    align: 'right',
                    format: '{point.y:.1f}', // one decimal
                    y: 10, // 10 pixels down from the top
                    style: {
                        fontSize: '12px',
                        fontFamily: 'Verdana, sans-serif'
                    }
                }
            }]
        });
    </script>
</asp:Content>

