﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mis/PayrollMasterPage.master" AutoEventWireup="true" CodeFile="PayrollHomeDashboard.aspx.cs" Inherits="mis_HR_HRHomeDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="css/hrcustom.css" rel="stylesheet" />
    <link href="css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <style>
        .info-box-icon {
            height: 50px;
            width: 50px;
            font-size: 25px;
            line-height: 50px;
        }

        .info-box {
            min-height: 50px;
        }

        .box {
            border-top: 3px solid #ffffff;
        }

        .info-box-number {
            display: block;
            font-weight: bold;
            font-size: 12px;
        }

        .info-box-content {
            padding: 5px 10px;
            margin-left: 50px;
        }

        main {
            width: 100%;
            margin: 10px auto;
            padding: 10px 20px 30px;
            background: #FFF;
            box-shadow: 0 3px 5px rgba(0,0,0,0.2);
        }

        p {
            margin-top: 2rem;
            font-size: 13px;
        }

        #bar-chart {
            width: 100%;
            height: 300px;
            position: relative;
        }

        #line-chart {
            width: 500px;
            height: 300px;
            position: relative;
        }

            #bar-chart::before, #line-chart::before {
                content: "";
                position: absolute;
                display: block;
                width: 240px;
                height: 30px;
                left: 155px;
                top: 254px;
                background: #FAFAFA;
                box-shadow: 1px 1px 0 0 #DDD;
            }

        #pie-chart {
            width: 100%;
            height: 250px;
            position: relative;
        }

            #pie-chart::before {
                content: "";
                position: absolute;
                display: block;
                width: 120px;
                height: 115px;
                left: 315px;
                top: 0;
                background: #FAFAFA;
                box-shadow: 1px 1px 0 0 #DDD;
            }

            #pie-chart::after {
                content: "";
                position: absolute;
                display: block;
                top: 260px;
                left: 70px;
                width: 170px;
                height: 2px;
                background: rgba(0,0,0,0.1);
                border-radius: 50%;
                box-shadow: 0 0 3px 4px rgba(0,0,0,0.1);
            }

        .box {
            min-height: auto;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper" style="min-height: 960px;">
        <section class="content-header">
            <h1>डैशबोर्ड 
        <small>(Dashboard)</small>
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>
                    डैशबोर्ड (Dashboard)
                </a></li>
            </ol>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-pramod">
                        <div class="box-body">
                            <div class="col-md-4">
                                <div class="box-header with-border">
                                    <h3 class="box-title">निगम के कार्यालय  <small>(Corporation Offices)</small></h3>
                                </div>
                                <div class="col-md-12">
                                    <div class="info-box">
                                        <span class="info-box-icon bg-green"><i class="ion ion-ios-home-outline"></i></span>
                                        <div class="info-box-content">
                                            <span class="info-box-text">प्रधान कार्यालय</span>
                                            <span class="info-box-number">01 <small>Head Office</small></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix visible-sm-block"></div>
                                <div class="col-md-12">
                                    <div class="info-box">
                                        <span class="info-box-icon bg-aqua"><i class="ion ion-ios-people-outline"></i></span>

                                        <div class="info-box-content">
                                            <span class="info-box-text">क्षेत्रीय कार्यालय
                                            </span>
                                            <span class="info-box-number">07 <small>Regional Office</small></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="info-box">
                                        <span class="info-box-icon bg-red"><i class="ion ion-ios-people-outline"></i></span>

                                        <div class="info-box-content">
                                            <span class="info-box-text">जिला एवं शाखा कार्यालय</span>
                                            <span class="info-box-number">50 <small>Branch Office</small></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix visible-sm-block"></div>

                                <div class="col-md-12">
                                    <div class="info-box">
                                        <span class="info-box-icon bg-green"><i class="ion ion-xbox"></i></span>

                                        <div class="info-box-content">
                                            <span class="info-box-text">उत्पादन इकार्इयां</span>
                                            <span class="info-box-number">02 <small>Production Unit</small></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="info-box">
                                        <span class="info-box-icon bg-yellow"><i class="ion ion-ios-people-outline"></i></span>

                                        <div class="info-box-content">
                                            <span class="info-box-text">कृषि प्रक्षेत्र
                                            </span>
                                            <span class="info-box-number">01 <small>Agriculture Farm</small></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="info-box">
                                        <span class="info-box-icon bg-blue"><i class="ion ion-ios-people-outline"></i></span>

                                        <div class="info-box-content">
                                            <span class="info-box-text">निर्माण इकाई
                                            </span>
                                            <span class="info-box-number">01 <small>Fabrication Unit</small></span>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-8">
                                <div class="box">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">स्वीकृत पदों की संख्या/ भरे पदो की संख्या <small>(Overall Sanctioned & Filled Post In The Department) </small></h3>
                                        <script type="text/javascript" src="https://www.google.com/jsapi"></script>
                                        <main>
				  <h5></h5>
				  <div id="bar-chart"></div>
				</main>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-pramod">
                        <div class="box-header with-border">
                            <h3 class="box-title">कर्मचारी इस महीने में सेवानिवृत्त हो रहे हैं <small>(Employee's getting retired in this month)</small></h3>
                        </div>
                        <div class="box-body">
                            <asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-striped table-bordered pagination-ys" AutoGenerateColumns="False" DataKeyNames="Emp_ID">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="5%"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Emp_Name" HeaderText="कर्मचारी का नाम <br/><small>(Employee Name )</small>" HtmlEncode="False" />
                                    <asp:BoundField DataField="FromDept" HeaderText="पदनाम <br/><small>(Designation) </small>" HtmlEncode="False" />
                                    <asp:BoundField DataField="Emp_Class" HeaderText="श्रेणी <br/><small>( Class) </small>" HtmlEncode="False" />
                                    <asp:BoundField DataField="FromOffice" HeaderText="कार्यालय  <br/><small>(Office) </small>" HtmlEncode="False" />
                                    <asp:BoundField DataField="Emp_PostingDate" HeaderText="कार्यग्रहण दिनांक <br/><small>(Joining date) </small>" HtmlEncode="False" />
                                    <asp:BoundField DataField="Emp_RetirementDate" HeaderText="सेवानिवृत्ति की  दिनांक <br/><small>(Retirement Date) </small>" HtmlEncode="False" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="text-red">** इस माह कोई भी कर्मचारी सेवानिवृत्ति नहीं हो रहे हैं | **</div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script>
        /******************/
        google.load("visualization", "1", { packages: ["corechart"] });
        google.setOnLoadCallback(drawCharts);
        function drawCharts() {
            var barData = google.visualization.arrayToDataTable([
              ['पदनाम', 'स्वीकृत पदों की संख्या', 'भरे पदो की संख्या	', 'रिक्त पदों की संख्या'],
              ['प्रथम श्रेणी', 56, 26, 30],
              ['द्धितीय श्रेणी', 72, 25, 47],
              ['तृतीय श्रेणी', 528, 241, 287],
              ['चतुर्थ श्रेणी', 180, 112, 68],
              ['दैनिक वेतनभोगी व संविदा कर्मचारी', 660, 660, 0]
            ]);
            var barOptions = {
                focusTarget: 'category',
                backgroundColor: 'transparent',
                colors: ['cornflowerblue', 'darkcyan', 'coral'],
                fontName: 'Open Sans',
                chartArea: {
                    left: 50,
                    top: 10,
                    width: '100%',
                    height: '70%'
                },
                bar: {
                    groupWidth: '80%'
                },
                hAxis: {
                    textStyle: {
                        fontSize: 12
                    }
                },
                vAxis: {
                    minValue: 0,
                    maxValue: 1500,
                    baselineColor: '#DDD',
                    gridlines: {
                        color: '#DDD',
                        count: 4
                    },
                    textStyle: {
                        fontSize: 11
                    }
                },
                legend: {
                    position: 'bottom',
                    textStyle: {
                        fontSize: 12
                    }
                },
                animation: {
                    duration: 1200,
                    easing: 'out',
                    startup: true
                }
            };
            var barChart = new google.visualization.ColumnChart(document.getElementById('bar-chart'));
            barChart.draw(barData, barOptions);
        }
    </script>
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/dataTables.bootstrap.min.js"></script>
    <script src="js/dataTables.buttons.min.js"></script>
    <script src="js/buttons.flash.min.js"></script>
    <script src="js/jszip.min.js"></script>
    <script src="js/pdfmake.min.js"></script>
    <script src="js/vfs_fonts.js"></script>
    <script src="js/buttons.html5.min.js"></script>
    <script src="js/buttons.print.min.js"></script>
    <script>
        $(document).ready(function () {
            $('.datatable').DataTable({

                paging: false,

                columnDefs: [{
                    targets: 'no-sort',
                    orderable: false
                }],
                "order": [[0, 'asc']],
                oLanguage: {
                    "sZeroRecords": "कोई भी प्रविष्टी नहीं है!",
                    "sEmptyTable": "कोई भी प्रविष्टी नहीं है!",
                    "sInfo": "कुल _TOTAL_ प्रविष्टियों में से - _START_ से लेकर _END_ तक दिखा रही हैं",
                    "sSearch": "फ़िल्टर (Filter):",
                    "oPaginate": {
                        "sFirst": "पहला (First)",
                        "sLast": "आखिरी (Last)",
                        "sNext": "अगला (Next)",
                        "sPrevious": "पिछला (Previous)"
                    }
                },
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
                            columns: ':not(.no-print)'
                        },
                        footer: true,
                        autoPrint: true
                    }, {
                        extend: 'excel',
                        text: '<i class="fa fa-file-excel-o"></i> Excel',
                        title: $('h1').text(),
                        exportOptions: {
                            columns: ':not(.no-print)'
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
            t.on('order.dt search.dt', function () {
                t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                    cell.innerHTML = i + 1;
                });
            }).draw();
        });
    </script>


</asp:Content>

