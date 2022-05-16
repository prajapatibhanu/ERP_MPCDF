<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InventoryDashboard.aspx.cs" Inherits="mis_InventoryDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta content="" name="description" />
    <meta content="webthemez" name="author" />
    <title>::MPCDF::</title>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/font-awesome.css" rel="stylesheet" />

    <link href="js/morris/morris-0.4.3.min.css" rel="stylesheet" />
    <link href="css/custom-styles.css" rel="stylesheet" />
    <link href='https://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css' />
    <link href="css/font-awesome.css" rel="stylesheet" />
    <script src="../js/select2.min.js"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/data.js"></script>
    <script src="https://code.highcharts.com/modules/drilldown.js"></script>

    <script src="js/highcharts.js"></script>

    <link href="css/all.css" rel="stylesheet" />




    <script type="text/javascript">
        $(function () {
            var chartype = {
                type: 'pie'
            }
            var chartitle = {
                text: ''
            }
            var charsubtitle = {
                text: 'Click the slices to view versions.'
            }
            var chartooltip = {
                headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.0f}</b>total<br/>'
            }
            var chartplotoptions = {
                series: {
                    dataLabels: {
                        enabled: true,
                        format: '{point.name}: {point.y:.0f}'
                    }
                }
            }
            var chartseries = [{
                name: 'RC HOLDERS',
                colorByPoint: true,
                data: [{
                    name: 'KRISHNA ENTERPRISES',
                    y: 100,
                    drilldown: 'KRISHNA ENTERPRISES'
                }, {
                    name: 'SUPER SERVICES',
                    y: 200,
                    drilldown: 'SUPER SERVICES'
                }, {
                    name: 'SODEXO',
                    y: 150,
                    drilldown: 'SODEXO'
                }, {
                    name: 'PAL & SONS',
                    y: 250,
                    drilldown: 'PAL & SONS'
                }, {
                    name: 'Arch Enterprises',
                    y: 350,
                    drilldown: 'Arch Enterprises'
                }]
            }]
            var chartdrilldown = {
                series: [{
                    name: 'KRISHNA ENTERPRISES',
                    id: 'KRISHNA ENTERPRISES',
                    data: [
                        ['MILK', 15],
                        ['GHEE', 25],
                        ['CUP', 10],
                        ['BOTTLE', 20],
                        ['POLY', 20],
                        ['BAG', 10]
                    ]
                }, {
                    name: 'SUPER SERVICES',
                    id: 'SUPER SERVICES',
                    data: [
                        ['MILK RC', 30],
                        ['GHEE RC', 30],
                        ['CUP RC', 40],
                        ['BOTTLE RC', 40],
                        ['POLY RC', 40],
                        ['BAG RC', 20]
                    ]
                }, {
                    name: 'SODEXO',
                    id: 'SODEXO',
                    data: [
                     ['MILK RC', 30],
                        ['GHEE RC', 30],
                        ['CUP RC', 40],
                        ['BOTTLE RC', 40],
                        ['POLY RC', 10]

                    ]
                }, {
                    name: 'PAL & SONS',
                    id: 'PAL & SONS',
                    data: [
                       ['MILK RC', 100],
                        ['GHEE RC', 50],
                        ['CUP RC', 100],

                    ]
                }, {
                    name: 'Arch Enterprises',
                    id: 'Arch Enterprises',
                    data: [
                       ['MILK RC', 50],
                        ['GHEE RC', 30],
                        ['CUP RC', 40],
                        ['BOTTLE RC', 80],
                        ['POLY RC', 100],
                        ['BAG RC', 50]
                    ]
                }]
            }
            $('#container').highcharts({
                chart: chartype,
                title: chartitle,
                tooltip: chartooltip,
                plotOptions: chartplotoptions,
                series: chartseries,
                drilldown: chartdrilldown
            });
        });
    </script>
    <script type="text/javascript">
        debugger;
        $(function () {
            var chartype = {
                type: 'pie'
            }
            var chartitle = {
                text: ''
            }
            var charsubtitle = {
                text: 'Click the slices to view versions.'
            }
            var chartooltip = {
                headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.0f}</b> RC<br/>'
            }
            var chartplotoptions = {
                series: {
                    dataLabels: {
                        enabled: true,
                        format: '{point.name}: {point.y:.0f}'
                    }
                }
            }
            var chartseries = [{
                name: 'ITEMS',
                colorByPoint: true,
                data: [{
                    name: 'SUGAR',
                    y: 10,
                    drilldown: 'SUGAR'
                }, {
                    name: 'PRINTED CUPS',
                    y: 20,
                    drilldown: 'PRINTED CUPS'
                }, {
                    name: 'SMP BAGS',
                    y: 15,
                    drilldown: 'SMP BAGS'
                }, {
                    name: 'POLY BAGS',
                    y: 25,
                    drilldown: 'POLY BAGS'
                }, {
                    name: 'BOTTLE',
                    y: 20,
                    drilldown: 'BOTTLE'
                }]
            }]
            var chartdrilldown = {
                series: [{
                    name: 'SUGAR',
                    id: 'SUGAR',
                    data: [
                        ['KRISHNA ENTERPRISES', 2],
                        ['SUPER SERVICES', 3],
                        ['SODEXO', 1],
                        ['PAL & SONS', 2],
                        ['Arch Enterprises', 2]

                    ]
                }, {
                    name: 'PRINTED CUPS',
                    id: 'PRINTED CUPS',
                    data: [
                         ['KRISHNA ENTERPRISES', 2],
                        ['SUPER SERVICES', 5],
                        ['SODEXO', 10],
                        ['PAL & SONS', 1],
                        ['Arch Enterprises', 2]
                    ]
                }, {
                    name: 'SMP BAGS',
                    id: 'SMP BAGS',
                    data: [
                     ['KRISHNA ENTERPRISES', 2],
                        ['SUPER SERVICES', 5],
                        ['SODEXO', 5],
                        ['PAL & SONS', 3],
                        ['Arch Enterprises', 2]

                    ]
                }, {
                    name: 'POLY BAGS',
                    id: 'POLY BAGS',
                    data: [
                      ['KRISHNA ENTERPRISES', 10],
                        ['SUPER SERVICES', 5],
                        ['SODEXO', 5],
                        ['PAL & SONS', 3],
                        ['Arch Enterprises', 2]

                    ]
                }, {
                    name: 'BOTTLE',
                    id: 'BOTTLE',
                    data: [
                       ['KRISHNA ENTERPRISES', 10],
                        ['SUPER SERVICES', 1],
                        ['SODEXO', 4],
                        ['PAL & SONS', 2],
                        ['Arch Enterprises', 3]
                    ]
                }]
            }
            $('#Itemcontainer').highcharts({
                chart: chartype,
                title: chartitle,
                tooltip: chartooltip,
                plotOptions: chartplotoptions,
                series: chartseries,
                drilldown: chartdrilldown
            });
        });
    </script>



    <style>
        .navbar {
            background: #0f62ac !important;
        }

        .navbar-brand {
            background: #0f62ac !important;
        }
    </style>

</head>

<body>
    <div id="wrapper">
        <nav class="navbar navbar-default top-navbar" role="navigation">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".sidebar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="index.html"><strong>MPCDF</strong></a>
            </div>

        </nav>
        <%--style="background-size: 100%; height: 120vh; background-image: url('../images/BG11.jpg'); width: 100%; z-index: 1000;"--%>
        <div id="page-wrapper">

            <div id="page-inner">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="fancy-title  title-dotted-border">
                            <h3>Current Status <span style="font-size: 15px;">Date
                                <label id="date"></label></spa></h3>

                        </div>
                    </div>
                    <div class="col-lg-12">
                        <div class="alert alert-warning">
                            <marquee behavior="scroll" direction="left" onmouseover="this.stop();" onmouseout="this.start();">
                             <asp:Label ID="Label5m" runat="server" Font-Bold="True" Font-Names="Arial Black" ForeColor="#000000" Text=" CURRENT STOCK  - SUGAR :- 10000 KG , BOTTLE :- 2500 Nos  , PRINTED CUPS :- 2000 Nos, SMP BAGS :- 1000 Nos , POLY BAGS :- 10000 Nos , LIQUID SOAP :-  2500 Ltr"></asp:Label></marquee>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="panel panel-default">
                            <div class="panel-heading blue">
                                <div class="title">RC HOLDERS / VENDOR DETAILS</div>
                            </div>
                            <div class="panel-body">
                                <div id="container"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="panel panel-default">
                            <div class="panel-heading blue">
                                <div class="title">RC HOLDERS ITEMS DETAILS</div>
                            </div>
                            <div class="panel-body">
                                <div id="Itemcontainer"></div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="panel panel-default">
                            <div class="panel-heading blue">
                                <div class="title">TOTAL PRODUCT SALE IN A YEAR (In Nos.)</div>
                            </div>
                            <div class="panel-body">
                                <div id="container2" style="min-width: 200px; height: 400px; margin: 0 auto"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="panel panel-default">
                            <div class="panel-heading blue">
                                <div class="title">TODAY'S TOTAL PRODUCTS DEMAND (In Nos.)</div>
                            </div>
                            <div class="panel-body">
                                <div id="container3" style="min-width: 200px; height: 400px; margin: 0 auto"></div>
                            </div>
                        </div>
                    </div>
                    <%--   <div class="col-md-6">
                        <div class="board">
                            <div class="panel panel-primary">
                                <div id="containerSale1" style="min-width: 200px; height: 400px; margin: 0 auto"></div>
                            </div>
                        </div>
                    </div>--%>



                    <div class="col-md-3 col-sm-12 col-xs-12">
                        <div class="board">
                            <div class="panel panel-primary">
                                <div class="number">
                                    <h3><a href="InventoryDashboardDetail.aspx?TableID=Rc7" class="small-box-footer">10 Nos</a></h3>
                                    <small>RC Expire In Next 7 Days</small>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-eject fa-5x red"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12">
                        <div class="board">
                            <div class="panel panel-primary">
                                <div class="number">
                                    <h3><a href="InventoryDashboardDetail.aspx?TableID=Rc30" class="small-box-footer">35 Nos</a></h3>
                                    <small>RC Expire In Next 30 Days</small>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-eject fa-5x red"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12">
                        <div class="board">
                            <div class="panel panel-primary">
                                <div class="number">
                                    <h3><a href="InventoryDashboardDetail.aspx?TableID=Item7" class="small-box-footer">235 Nos</a></h3>
                                    <small>Item to be Order In Next 7 Days</small>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-cart-arrow-down fa-5x green"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12">
                        <div class="board">
                            <div class="panel panel-primary">
                                <div class="number">
                                    <h3><a href="InventoryDashboardDetail.aspx?TableID=Item30" class="small-box-footer">552 Nos</a></h3>
                                    <small>Item to be Order In Next 30 Day</small>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-cart-arrow-down fa-5x green"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12">
                        <div class="board">
                            <div class="panel panel-primary">
                                <div class="number">
                                    <h3><a href="InventoryDashboardDetail.aspx?TableID=Stock" class="small-box-footer"></a></h3>
                                    <small>Today's<br />
                                        Current Stock</small>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-tags fa-5x green"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12">
                        <div class="board">
                            <div class="panel panel-primary">
                                <div class="number">
                                    <h3><a href="InventoryDashboardDetail.aspx?TableID=Report" class="small-box-footer"></a></h3>
                                    <small>Today Item
                                        <br />
                                        Consuptions Report</small>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-receipt fa-5x blue"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12">
                        <div class="board">
                            <div class="panel panel-primary">
                                <div class="number">
                                    <h3><a href="InventoryDashboardDetail.aspx?TableID=Report" class="small-box-footer"></a></h3>
                                    <small>Payment To<br />
                                        Be Paid</small>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-rupee-sign fa-5x green"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12">
                        <div class="board">
                            <div class="panel panel-primary">
                                <div class="number">
                                    <h3><a href="InventoryDashboardDetail.aspx?TableID=Report" class="small-box-footer"></a></h3>
                                    <small>Rejected Item</small>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-times-circle fa-5x red"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12">
                        <div class="board">
                            <div class="panel panel-primary">
                                <div class="number">
                                    <h3><a href="InventoryDashboardDetail.aspx?TableID=Report" class="small-box-footer"></a></h3>
                                    <small>Engineering<br />
                                        Section items</small>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-toolbox fa-5x blue"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12">
                        <div class="board">
                            <div class="panel panel-primary">
                                <div class="number">
                                    <h3><a href="InventoryDashboardDetail.aspx?TableID=Report" class="small-box-footer"></a></h3>
                                    <small>Plant Items</small>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-list-alt fa-5x blue"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12">
                        <div class="board">
                            <div class="panel panel-primary">
                                <div class="number">
                                    <h3><a href="InventoryDashboardDetail.aspx?TableID=Report" class="small-box-footer"></a></h3>
                                    <small>Dcs Items</small>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-list-alt fa-5x blue"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12">
                        <div class="board">
                            <div class="panel panel-primary">
                                <div class="number">
                                    <h3><a href="InventoryDashboardDetail.aspx?TableID=LowStock" class="small-box-footer">25 Items</a></h3>
                                    <small>Low Stock Items</small>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-arrow-alt-circle-down red"></i>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <footer>
                <p>Design & Developed by: <a href="#">SFA TECHNOLOGIES</a></p>
            </footer>
        </div>
    </div>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="myModalLabel">Production (Badai) In Last 5 Year</h4>
                </div>
                <div class="modal-body">
                    Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <!--<button type="button" class="btn btn-primary">Save changes</button>-->
                </div>
            </div>
        </div>
    </div>
    <script src="../js/Script.js"></script>
    <script src="../js/bootstrap-datepicker.js"></script>
    <script src="https://code.highcharts.com/highcharts-3d.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
    <script src="https://code.highcharts.com/modules/export-data.js"></script>
    <script src="https://code.highcharts.com/modules/cylinder.js"></script>
    <script>
        Highcharts.chart('container2', {
            chart: {
                type: 'column'
            },
            title: {
                text: ''
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                type: 'category'
            },
            yAxis: {
                title: {
                    text: 'TOTAL PRODUCT SALE (In Nos.)'
                }

            },
            legend: {
                enabled: false
            },
            plotOptions: {
                series: {
                    borderWidth: 0,
                    dataLabels: {
                        enabled: true,
                        format: '{point.y:.0f}'
                    }
                }
            },

            tooltip: {
                headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.0f}</b><br/>'
            },

            series: [
                {
                    name: "PRODUCTS",
                    colorByPoint: true,
                    data: [
                        {
                            name: "BUTTER MILK",
                            y: 5000,
                            drilldown: "BUTTER MILK"
                        },
                        {
                            name: "PANNER",
                            y: 4000,
                            drilldown: "PANNER"
                        },
                        {
                            name: "SANCHI GHEE",
                            y: 6000,
                            drilldown: "PANNER"
                        },
                        {
                            name: "CURD",
                            y: 7000,
                            drilldown: "CURD"
                        },
                        {
                            name: "SHRIKHAND",
                            y: 4000,
                            drilldown: "SHRIKHAND"
                        },
                        {
                            name: "SWEETS",
                            y: 3000,
                            drilldown: "SWEETS"
                        },
                        {
                            name: "FLAVORED MILK",
                            y: 2000,
                            drilldown: null
                        }
                    ]
                }
            ],
            drilldown: {
                series: [
                    {
                        name: "BUTTER MILK",
                        id: "BUTTER MILK",
                        data: [
                            [
                                "JAN",
                                400
                            ],
                            [
                                "FEB",
                                200
                            ],
                            [
                                "MAR",
                                400
                            ],
                            [
                                "APR",
                                500
                            ],
                            [
                                "MAY",
                                500
                            ],
                            [
                                "JUNE",
                                300
                            ],
                            [
                                "JULY",
                                700
                            ],
                            [
                                "AUG",
                                600
                            ],
                            [
                                "SEP",
                                400
                            ],
                            [
                                "OCT",
                                200
                            ],
                            [
                                "NOV",
                                800
                            ],
                            [
                                "DEC",
                                100
                            ]
                        ]
                    },
                    {
                        name: "PANNER",
                        id: "PANNER",
                        data: [
                            [
                                "JAN",
                                400
                            ],
                            [
                                "FEB",
                                300
                            ],
                            [
                                "MAR",
                                200
                            ],
                            [
                                "APR",
                                100
                            ],
                            [
                                "MAY",
                                200
                            ],
                            [
                                "JUN",
                                300
                            ],
                            [
                                "JULY",
                                600
                            ],
                            [
                                "AUG",
                                700
                            ],
                            [
                                "SEP",
                                800
                            ],
                            [
                                "OCT",
                                900
                            ],
                             [
                                "NOV",
                                800
                             ],
                              [
                                "DEC",
                                300
                              ]
                        ]
                    },
                    {
                        name: "SANCHI GHEE",
                        id: "SANCHI GHEE",
                        data: [
                            [
                                "JAN",
                                800
                            ],
                            [
                                "FEB",
                                300
                            ],
                            [
                                "MAR",
                                200
                            ],
                            [
                                "APR",
                                100
                            ],
                            [
                                "MAY",
                                200
                            ],
                            [
                                "JUN",
                                500
                            ],
                            [
                                "JULY",
                                700
                            ],
                            [
                                "AUG",
                                80
                            ],
                            [
                                "SEP",
                                800
                            ],
                            [
                                "OCT",
                                500
                            ],
                             [
                                "NOV",
                                700
                             ],
                              [
                                "DEC",
                                300
                              ]
                        ]
                    },
                    {
                        name: "CURD",
                        id: "CURD",
                        data: [
                            [
                                "JAN",
                                400
                            ],
                            [
                                "FEB",
                                300
                            ],
                            [
                                "MAR",
                                200
                            ],
                            [
                                "APR",
                                100
                            ],
                            [
                                "MAY",
                                200
                            ],
                            [
                                "JUN",
                                500
                            ],
                            [
                                "JULY",
                                500
                            ],
                            [
                                "AUG",
                                80
                            ],
                            [
                                "SEP",
                                600
                            ],
                            [
                                "OCT",
                                500
                            ],
                             [
                                "NOV",
                                500
                             ],
                              [
                                "DEC",
                                300
                              ]
                        ]
                    },
                    {
                        name: "SHRIKHAND",
                        id: "SHRIKHAND",
                        data: [
                            [
                                "JAN",
                                600
                            ],
                            [
                                "FEB",
                                500
                            ],
                            [
                                "MAR",
                                200
                            ],
                            [
                                "APR",
                                100
                            ],
                            [
                                "MAY",
                                200
                            ],
                            [
                                "JUN",
                                500
                            ],
                            [
                                "JULY",
                                500
                            ],
                            [
                                "AUG",
                                80
                            ],
                            [
                                "SEP",
                                200
                            ],
                            [
                                "OCT",
                                500
                            ],
                             [
                                "NOV",
                                400
                             ],
                              [
                                "DEC",
                                300
                              ]
                        ]
                    },
                    {
                        name: "SWEETS",
                        id: "SWEETS",
                        data: [
                            [
                                "JAN",
                                200
                            ],
                            [
                                "FEB",
                                300
                            ],
                            [
                                "MAR",
                                200
                            ],
                            [
                                "APR",
                                100
                            ],
                            [
                                "MAY",
                                200
                            ],
                            [
                                "JUN",
                                500
                            ],
                            [
                                "JULY",
                                500
                            ],
                            [
                                "AUG",
                                80
                            ],
                            [
                                "SEP",
                                200
                            ],
                            [
                                "OCT",
                                200
                            ],
                             [
                                "NOV",
                                400
                             ],
                              [
                                "DEC",
                                100
                              ]
                        ]
                    }
                ]
            }
        });
    </script>
    <script>
        Highcharts.chart('container3', {
            chart: {
                type: 'column'
            },
            title: {
                text: ''
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                type: 'category'
            },
            yAxis: {
                title: {
                    text: 'TOTAL PRODUCTS DEMAND (In Nos.)'
                }

            },
            legend: {
                enabled: false
            },
            plotOptions: {
                series: {
                    borderWidth: 0,
                    dataLabels: {
                        enabled: true,
                        format: '{point.y:.0f}'
                    }
                }
            },

            tooltip: {
                headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.0f}</b><br/>'
            },

            series: [
                {
                    name: "PRODUCTS",
                    colorByPoint: true,
                    data: [
                        {
                            name: "BUTTER MILK",
                            y: 7000,
                            drilldown: "BUTTER MILK"
                        },
                        {
                            name: "PANNER",
                            y: 3000,
                            drilldown: "PANNER"
                        },
                        {
                            name: "SANCHI GHEE",
                            y: 5000,
                            drilldown: "PANNER"
                        },
                        {
                            name: "CURD",
                            y: 2000,
                            drilldown: "CURD"
                        },
                        {
                            name: "SHRIKHAND",
                            y: 1000,
                            drilldown: "SHRIKHAND"
                        },
                        {
                            name: "SWEETS",
                            y: 4000,
                            drilldown: "SWEETS"
                        },
                        {
                            name: "FLAVORED MILK",
                            y: 2000,
                            drilldown: null
                        }
                    ]
                }
            ],
            drilldown: {
                series: [
                    {
                        name: "BUTTER MILKk",
                        id: "BUTTER MILKk",
                        data: [
                            [
                                "JAN",
                                400
                            ],
                            [
                                "FEB",
                                200
                            ],
                            [
                                "MAR",
                                400
                            ],
                            [
                                "APR",
                                500
                            ],
                            [
                                "MAY",
                                500
                            ],
                            [
                                "JUNE",
                                300
                            ],
                            [
                                "JULY",
                                700
                            ],
                            [
                                "AUG",
                                600
                            ],
                            [
                                "SEP",
                                400
                            ],
                            [
                                "OCT",
                                200
                            ],
                            [
                                "NOV",
                                800
                            ],
                            [
                                "DEC",
                                100
                            ]
                        ]
                    },
                    {
                        name: "PANNERR",
                        id: "PANNERR",
                        data: [
                            [
                                "JAN",
                                400
                            ],
                            [
                                "FEB",
                                300
                            ],
                            [
                                "MAR",
                                200
                            ],
                            [
                                "APR",
                                100
                            ],
                            [
                                "MAY",
                                200
                            ],
                            [
                                "JUN",
                                300
                            ],
                            [
                                "JULY",
                                600
                            ],
                            [
                                "AUG",
                                700
                            ],
                            [
                                "SEP",
                                800
                            ],
                            [
                                "OCT",
                                900
                            ],
                             [
                                "NOV",
                                800
                             ],
                              [
                                "DEC",
                                300
                              ]
                        ]
                    },
                    {
                        name: "SANCHI GHEEE",
                        id: "SANCHI GHEEE",
                        data: [
                            [
                                "JAN",
                                800
                            ],
                            [
                                "FEB",
                                300
                            ],
                            [
                                "MAR",
                                200
                            ],
                            [
                                "APR",
                                100
                            ],
                            [
                                "MAY",
                                200
                            ],
                            [
                                "JUN",
                                500
                            ],
                            [
                                "JULY",
                                700
                            ],
                            [
                                "AUG",
                                80
                            ],
                            [
                                "SEP",
                                800
                            ],
                            [
                                "OCT",
                                500
                            ],
                             [
                                "NOV",
                                700
                             ],
                              [
                                "DEC",
                                300
                              ]
                        ]
                    },
                    {
                        name: "CURDD",
                        id: "CURDD",
                        data: [
                            [
                                "JAN",
                                400
                            ],
                            [
                                "FEB",
                                300
                            ],
                            [
                                "MAR",
                                200
                            ],
                            [
                                "APR",
                                100
                            ],
                            [
                                "MAY",
                                200
                            ],
                            [
                                "JUN",
                                500
                            ],
                            [
                                "JULY",
                                500
                            ],
                            [
                                "AUG",
                                80
                            ],
                            [
                                "SEP",
                                600
                            ],
                            [
                                "OCT",
                                500
                            ],
                             [
                                "NOV",
                                500
                             ],
                              [
                                "DEC",
                                300
                              ]
                        ]
                    },
                    {
                        name: "SHRIKHANDD",
                        id: "SHRIKHANDD",
                        data: [
                            [
                                "JAN",
                                600
                            ],
                            [
                                "FEB",
                                500
                            ],
                            [
                                "MAR",
                                200
                            ],
                            [
                                "APR",
                                100
                            ],
                            [
                                "MAY",
                                200
                            ],
                            [
                                "JUN",
                                500
                            ],
                            [
                                "JULY",
                                500
                            ],
                            [
                                "AUG",
                                80
                            ],
                            [
                                "SEP",
                                200
                            ],
                            [
                                "OCT",
                                500
                            ],
                             [
                                "NOV",
                                400
                             ],
                              [
                                "DEC",
                                300
                              ]
                        ]
                    },
                    {
                        name: "SWEETSS",
                        id: "SWEETSS",
                        data: [
                            [
                                "JAN",
                                200
                            ],
                            [
                                "FEB",
                                300
                            ],
                            [
                                "MAR",
                                200
                            ],
                            [
                                "APR",
                                100
                            ],
                            [
                                "MAY",
                                200
                            ],
                            [
                                "JUN",
                                500
                            ],
                            [
                                "JULY",
                                500
                            ],
                            [
                                "AUG",
                                80
                            ],
                            [
                                "SEP",
                                200
                            ],
                            [
                                "OCT",
                                200
                            ],
                             [
                                "NOV",
                                400
                             ],
                              [
                                "DEC",
                                100
                              ]
                        ]
                    }
                ]
            }
        });
    </script>
    <script>
        Highcharts.chart('containerSale', {
            chart: {
                type: 'cylinder',
                options3d: {
                    enabled: true,
                    alpha: 15,
                    beta: 15,
                    depth: 50,
                    viewDistance: 25
                }
            },
            title: {
                text: 'MILK PRODUCTION IN A YEAR 2019'
            },
            plotOptions: {
                series: {
                    depth: 25,
                    colorByPoint: true
                }
            },
            series: [{
                data: [29.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4],
                name: 'Cylinders',
                showInLegend: false
            }]
        });
    </script>
    <script>
        Highcharts.chart('containerSale1', {
            chart: {
                type: 'cylinder',
                options3d: {
                    enabled: true,
                    alpha: 15,
                    beta: 15,
                    depth: 50,
                    viewDistance: 25
                }
            },
            title: {
                text: 'MILK PRODUCT PRODUCTION IN A YEAR 2019'
            },
            plotOptions: {
                series: {
                    depth: 25,
                    colorByPoint: true
                }
            },
            series: [{
                data: [40, 70, 80, 129.2, 144.0, 176.0, 135.6, 165, 250, 194.1, 95.6, 68],
                name: 'Cylinders',
                showInLegend: false
            }]
        });
    </script>
    <script type="text/javascript">
        $(function () {

            var dt = new Date();
            dt.setFullYear(new Date().getFullYear() - 18);

            $(".addDate").datepicker({

                autoclose: true

            });
            $(".DOB").datepicker({

                autoclose: true,
                format: 'dd/mm/yyyy',
                endDate: dt
            });

            $(".YearDate").datepicker({
                minViewMode: 2,
                changeMonth: false,
                changeYear: true,


                format: 'yyyy',
                autoclose: true
            });

        });
    </script>
    <script>
        n = new Date();
        y = n.getFullYear();
        m = n.getMonth() + 1;
        d = n.getDate();
        document.getElementById("date").innerHTML = d + "/" + m + "/" + y;
    </script>
</body>
</html>
