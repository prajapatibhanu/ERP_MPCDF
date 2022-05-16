<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InventoryDashboardDetail.aspx.cs" Inherits="mis_InventoryDashboardDetail" %>

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

        <div id="page-wrapper">

            <div id="page-inner">
                <div class="row" >
                    <asp:Panel runat="server" ID="Rc7" Visible="false" >
                          <div class="col-md-3"></div>
                        <div class="col-md-6">
                            <div class="panel panel-default">
                                <div class="panel-heading blue">
                                    <div class="card-title">
                                        <div class="title">RC To Be Expire In Next 7 Days</div>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <!--<canvas id="bar-chart" class="chart"></canvas>-->
                                    <div class="table-responsive">
                                        <table class="table table-striped table-bordered table-hover" style="min-width: 310px; height: 200px; max-width: 600px; margin: 0 auto">
                                            <thead>
                                                <tr>
                                                    <th>RC HOLDER NAME</th>
                                                    <th>NO OF RC</th>
                                                    <th>EXPIRE DATE</th>
                                                </tr>
                                            </thead>
                                            <tbody>

                                                <tr>
                                                    <td>SUPER SERVICES</td>
                                                    <td>2</td>
                                                    <td>15-08-2019</td>
                                                </tr>
                                                <tr>
                                                    <td>SODEXO</td>
                                                    <td>5</td>
                                                    <td>15-08-2019</td>
                                                </tr>
                                                <tr>
                                                    <td>PAL & SONS</td>
                                                    <td>1</td>
                                                    <td>15-08-2019</td>
                                                </tr>
                                                <tr>
                                                    <td>Arch Enterprises</td>
                                                    <td>10</td>
                                                    <td>15-08-2019</td>
                                                </tr>
                                                <tr>
                                                    <td>KRISHNA ENTERPRISES</td>
                                                    <td>6</td>
                                                    <td>15-08-2019</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3"></div>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="Rc30" Visible="false">
                           <div class="col-md-3"></div>
                        <div class="col-md-6">
                            <div class="panel panel-default">
                                <div class="panel-heading blue">
                                    <div class="card-title">
                                        <div class="title">RC To Be Expire In Next 30 Days</div>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <!--<canvas id="bar-chart" class="chart"></canvas>-->
                                    <div class="table-responsive">
                                        <table class="table table-striped table-bordered table-hover" style="min-width: 310px; height: 200px; max-width: 600px; margin: 0 auto">
                                            <thead>
                                                <tr>
                                                    <th>RC HOLDER NAME</th>
                                                    <th>NO OF RC</th>
                                                    <th>EXPIRE DATE</th>
                                                </tr>
                                            </thead>
                                            <tbody>

                                                <tr>
                                                    <td>SUPER SERVICES</td>
                                                    <td>20</td>
                                                    <td>15-09-2019</td>
                                                </tr>
                                                <tr>
                                                    <td>SODEXO</td>
                                                    <td>50</td>
                                                    <td>15-09-2019</td>
                                                </tr>
                                                <tr>
                                                    <td>PAL & SONS</td>
                                                    <td>10</td>
                                                    <td>15-09-2019</td>
                                                </tr>
                                                <tr>
                                                    <td>Arch Enterprises</td>
                                                    <td>10</td>
                                                    <td>15-09-2019</td>
                                                </tr>
                                                <tr>
                                                    <td>KRISHNA ENTERPRISES</td>
                                                    <td>16</td>
                                                    <td>15-09-2019</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                           <div class="col-md-3"></div>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="Item7" Visible="false">
                           <div class="col-md-3"></div>
                        <div class="col-md-6">
                            <div class="panel panel-default">
                                <div class="panel-heading blue">
                                    <div class="card-title">
                                        <div class="title">ITEM TO BE ORDER IN NEXT 7 DAYS</div>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <!--<canvas id="bar-chart" class="chart"></canvas>-->
                                    <div class="table-responsive">
                                        <table class="table table-striped table-bordered table-hover" style="min-width: 310px; height: 200px; max-width: 600px; margin: 0 auto">
                                            <thead>
                                                <tr>
                                                    <th>ITEM NAME</th>
                                                    <th>QUANTITY</th>
                                                    <th>UNIT</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>MILK</td>
                                                    <td>35000</td>
                                                    <td>Ltr</td>
                                                </tr>
                                                <tr>
                                                    <td>GHEE</td>
                                                    <td>15000</td>
                                                    <td>Ltr</td>
                                                </tr>
                                                <tr>
                                                    <td>Panner</td>
                                                    <td>25000</td>
                                                    <td>Kg</td>
                                                </tr>
                                                <tr>
                                                    <td>Shrikhand</td>
                                                    <td>30000</td>
                                                    <td>Cups</td>
                                                </tr>
                                                <tr>
                                                    <td>Curd</td>
                                                    <td>15000</td>
                                                    <td>Cups</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                           <div class="col-md-3"></div>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="Item30" Visible="false">
                           <div class="col-md-3"></div>
                        <div class="col-md-6">
                            <div class="panel panel-default">
                                <div class="panel-heading blue">
                                    <div class="card-title">
                                        <div class="title">ITEM TO BE ORDER IN NEXT 30 DAYS</div>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="table-responsive">
                                        <table class="table table-striped table-bordered table-hover" style="min-width: 310px; height: 200px; max-width: 600px; margin: 0 auto">
                                            <thead>
                                                <tr>
                                                    <th>ITEM NAME</th>
                                                    <th>QUANTITY</th>
                                                    <th>UNIT</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>MILK</td>
                                                    <td>100000</td>
                                                    <td>Ltr</td>
                                                </tr>
                                                <tr>
                                                    <td>GHEE</td>
                                                    <td>35000</td>
                                                    <td>Ltr</td>
                                                </tr>
                                                <tr>
                                                    <td>Panner</td>
                                                    <td>55000</td>
                                                    <td>Kg</td>
                                                </tr>
                                                <tr>
                                                    <td>Shrikhand</td>
                                                    <td>40000</td>
                                                    <td>Cups</td>
                                                </tr>
                                                <tr>
                                                    <td>Curd</td>
                                                    <td>65000</td>
                                                    <td>Cups</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                           <div class="col-md-3"></div>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="TodayMilk" Visible="false">
                           <div class="col-md-3"></div>
                        <div class="col-md-6">
                            <div class="panel panel-default">
                                <div class="panel-heading blue">
                                    <div class="card-title">
                                        <div class="title">Today's Milk Collection</div>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="table-responsive">
                                        <table class="table table-striped table-bordered table-hover" style="min-width: 310px; height: 200px; max-width: 600px; margin: 0 auto">
                                            <thead>
                                                <tr>
                                                    <th>ITEM NAME</th>
                                                    <th>QUANTITY</th>
                                                    <th>UNIT</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>MILK</td>
                                                    <td>100000</td>
                                                    <td>Ltr</td>
                                                </tr>
                                                <tr>
                                                    <td>GHEE</td>
                                                    <td>35000</td>
                                                    <td>Ltr</td>
                                                </tr>
                                                <tr>
                                                    <td>Panner</td>
                                                    <td>55000</td>
                                                    <td>Kg</td>
                                                </tr>
                                                <tr>
                                                    <td>Shrikhand</td>
                                                    <td>40000</td>
                                                    <td>Cups</td>
                                                </tr>
                                                <tr>
                                                    <td>Curd</td>
                                                    <td>65000</td>
                                                    <td>Cups</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                           <div class="col-md-3"></div>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="Stock" Visible="false">
                           <div class="col-md-3"></div>
                        <div class="col-md-6">
                            <div class="panel panel-default">
                                <div class="panel-heading blue">
                                    <div class="card-title">
                                        <div class="title">Today's Current Stock</div>
                                    </div>
                                </div>
                                <div class="panel-body">

                                    <!--<canvas id="bar-chart" class="chart"></canvas>-->
                                    <div class="table-responsive">
                                        <table class="table table-striped table-bordered table-hover" style="min-width: 310px; height: 200px; max-width: 600px; margin: 0 auto">
                                            <thead>
                                                <tr>
                                                    <th>ITEM NAME</th>
                                                    <th>QUANTITY</th>
                                                    <th>UNIT</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>MILK</td>
                                                    <td>2500</td>
                                                    <td>Ltr</td>
                                                </tr>
                                                <tr>
                                                    <td>GHEE</td>
                                                    <td>2000</td>
                                                    <td>Ltr</td>
                                                </tr>
                                                <tr>
                                                    <td>Panner</td>
                                                    <td>1000</td>
                                                    <td>Kg</td>
                                                </tr>
                                                <tr>
                                                    <td>Shrikhand</td>
                                                    <td>10000</td>
                                                    <td>Cups</td>
                                                </tr>
                                                <tr>
                                                    <td>Curd</td>
                                                    <td>1500</td>
                                                    <td>Cups</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                           <div class="col-md-3"></div>
                    </asp:Panel>



                    <asp:Panel runat="server" ID="Report" Visible="false">

                           <div class="col-md-3"></div>
                        <div class="col-md-6">

                            <div class="col-md-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading blue">
                                        <div class="card-title">
                                            <div class="title">ITEM CONSUPTION REPORT</div>
                                        </div>
                                    </div>
                                    <form runat="server">
                                        <div class="col-md-4" id="fdate" runat="server">
                                            <div class="form-group">
                                                <label>From Date<span id="pnlfdat" visible="false" runat="server" style="color: red;"> *</span></label>

                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <asp:TextBox ID="TextBox1" AutoComplete="off" placeholder="DD/MM/YYYY" ClientIDMode="Static" runat="server" class="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-md-4" id="tdate" runat="server">
                                            <div class="form-group">
                                                <label>To Date<span id="pnltdat" visible="false" runat="server" style="color: red;"> *</span></label>

                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <asp:TextBox ID="TextBox2" AutoComplete="off" placeholder="DD/MM/YYYY" ClientIDMode="Static" runat="server" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" class="form-control"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-md-4" id="Div1" runat="server">
                                            <div class="form-group">

                                                <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-info btn-block" Style="margin-top: 26px;" AutoPostBack="false" AutoComplete="off" />
                                            </div>
                                        </div>

                                    </form>


                                </div>

                                <div class="col-md-12">
                                    <div class="panel-body">
                                        <!--<canvas id="bar-chart" class="chart"></canvas>-->
                                        <div class="table-responsive">
                                            <table class="table table-striped table-bordered table-hover" style="min-width: 310px; height: 100px; max-width: 600px; margin: 0 auto">
                                                <thead>
                                                    <tr>
                                                        <th>ITEM NAME</th>
                                                        <th>QUANTITY</th>
                                                        <th>UNIT</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>MILK</td>
                                                        <td>35000</td>
                                                        <td>Ltr</td>
                                                    </tr>
                                                    <tr>
                                                        <td>GHEE</td>
                                                        <td>15000</td>
                                                        <td>Ltr</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Panner</td>
                                                        <td>25000</td>
                                                        <td>Kg</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Shrikhand</td>
                                                        <td>30000</td>
                                                        <td>Cups</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Curd</td>
                                                        <td>15000</td>
                                                        <td>Cups</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                           <div class="col-md-3"></div>

                    </asp:Panel>
                      <asp:Panel runat="server" ID="MilkProduction" Visible="false">
                             <div class="col-md-3"></div>
                        <div class="col-md-6">
                            <div class="panel panel-default">
                                <div class="panel-heading blue">
                                    <div class="card-title">
                                        <div class="title">Product Production Report</div>
                                    </div>
                                </div>
                                <div class="panel-body">

                                    <!--<canvas id="bar-chart" class="chart"></canvas>-->
                                    <div class="table-responsive">
                                        <table class="table table-striped table-bordered table-hover" style="min-width: 310px; height: 200px; max-width: 600px; margin: 0 auto">
                                            <thead>
                                                <tr>
                                                    <th>ITEM NAME</th>
                                                    <th>QUANTITY</th>
                                                    <th>UNIT</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>MILK</td>
                                                    <td>2500</td>
                                                    <td>Ltr</td>
                                                </tr>
                                                <tr>
                                                    <td>GHEE</td>
                                                    <td>2000</td>
                                                    <td>Ltr</td>
                                                </tr>
                                                <tr>
                                                    <td>Panner</td>
                                                    <td>1000</td>
                                                    <td>Kg</td>
                                                </tr>
                                                <tr>
                                                    <td>Shrikhand</td>
                                                    <td>10000</td>
                                                    <td>Cups</td>
                                                </tr>
                                                <tr>
                                                    <td>Curd</td>
                                                    <td>1500</td>
                                                    <td>Cups</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                             <div class="col-md-3"></div>
                    </asp:Panel>
                     <asp:Panel runat="server" ID="PanelLowStock" Visible="false">
                             <div class="col-md-3"></div>
                        <div class="col-md-6">
                            <div class="panel panel-default">
                                <div class="panel-heading blue">
                                    <div class="card-title">
                                        <div class="title">Low Stock Item</div>
                                    </div>
                                </div>
                                <div class="panel-body">

                                    <!--<canvas id="bar-chart" class="chart"></canvas>-->
                                    <div class="table-responsive">
                                        <table class="table table-striped table-bordered table-hover" style="min-width: 310px; height: 200px; max-width: 600px; margin: 0 auto">
                                            <thead>
                                                <tr>
                                                    <th>ITEM NAME</th>
                                                    <th>Low Qty Limit</th>
                                                    <th>Actual Qty</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>SUGAR</td>
                                                    <td>500 KG</td>
                                                    <td>450 KG</td>
                                                </tr>
                                                <tr>
                                                    <td>PRINTED CUPS</td>
                                                    <td>2000 Nos</td>
                                                    <td>1000 Nos</td>
                                                </tr>
                                                <tr>
                                                    <td>SMP BAGS</td>
                                                    <td>100 Nos</td>
                                                    <td>800 Nos</td>
                                                </tr>
                                                <tr>
                                                    <td>POLY BAGS</td>
                                                    <td>10000 NOS</td>
                                                    <td>8000 Nos</td>
                                                </tr>
                                                <tr>
                                                    <td>BOTTLE</td>
                                                    <td>1500 Nos</td>
                                                    <td>1000 Nos</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                             <div class="col-md-3"></div>
                    </asp:Panel>
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
</body>
</html>
