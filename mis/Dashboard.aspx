<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="mis_Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <link rel="stylesheet" href="js/Lightweight-Chart/cssCharts.css">
    <link href="css/font-awesome.css" rel="stylesheet" />
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
            <ul class="nav navbar-top-links navbar-right">
                <li class="dropdown">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#" aria-expanded="false">
                        <i class="fa fa-envelope fa-fw"></i> <i class="fa fa-caret-down"></i>
                    </a>
                    <ul class="dropdown-menu dropdown-messages">
                        <li>
                            <a href="#">
                                <div>
                                    <strong>John Doe</strong>
                                    <span class="pull-right text-muted">
                                        <em>Today</em>
                                    </span>
                                </div>
                                <div>Lorem Ipsum has been the industry's standard dummy text ever since the 1500s...</div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a href="#">
                                <div>
                                    <strong>John Smith</strong>
                                    <span class="pull-right text-muted">
                                        <em>Yesterday</em>
                                    </span>
                                </div>
                                <div>Lorem Ipsum has been the industry's standard dummy text ever since an kwilnw...</div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a href="#">
                                <div>
                                    <strong>John Smith</strong>
                                    <span class="pull-right text-muted">
                                        <em>Yesterday</em>
                                    </span>
                                </div>
                                <div>Lorem Ipsum has been the industry's standard dummy text ever since the...</div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a class="text-center" href="#">
                                <strong>Read All Messages</strong>
                                <i class="fa fa-angle-right"></i>
                            </a>
                        </li>
                    </ul>
                </li>
                <li class="dropdown">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#" aria-expanded="false">
                        <i class="fa fa-bell fa-fw"></i> <i class="fa fa-caret-down"></i>
                    </a>
                    <ul class="dropdown-menu dropdown-alerts">
                        <li>
                            <a href="#">
                                <div>
                                    <i class="fa fa-comment fa-fw"></i> New Comment
                                    <span class="pull-right text-muted small">4 min</span>
                                </div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a href="#">
                                <div>
                                    <i class="fa fa-twitter fa-fw"></i> 3 New Followers
                                    <span class="pull-right text-muted small">12 min</span>
                                </div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a href="#">
                                <div>
                                    <i class="fa fa-envelope fa-fw"></i> Message Sent
                                    <span class="pull-right text-muted small">4 min</span>
                                </div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a href="#">
                                <div>
                                    <i class="fa fa-tasks fa-fw"></i> New Task
                                    <span class="pull-right text-muted small">4 min</span>
                                </div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a href="#">
                                <div>
                                    <i class="fa fa-upload fa-fw"></i> Server Rebooted
                                    <span class="pull-right text-muted small">4 min</span>
                                </div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a class="text-center" href="#">
                                <strong>See All Alerts</strong>
                                <i class="fa fa-angle-right"></i>
                            </a>
                        </li>
                    </ul>
                    <!-- /.dropdown-alerts -->
                </li>
                <!-- /.dropdown -->
                <li class="dropdown">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#" aria-expanded="false">
                        <i class="fa fa-user fa-fw"></i> <i class="fa fa-caret-down"></i>
                    </a>
                    <ul class="dropdown-menu dropdown-user">
                        <li>
                            <a href="#"><i class="fa fa-user fa-fw"></i> User Profile</a>
                        </li>
                        <li>
                            <a href="#"><i class="fa fa-gear fa-fw"></i> Settings</a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a href="#"><i class="fa fa-sign-out fa-fw"></i> Logout</a>
                        </li>
                    </ul>
                </li>
            </ul>
        </nav>

        <div id="page-wrapper">
            <div class="header">
                <h1 class="page-header">
                    <span style="color: #006eb3;">Analytical Dashboard</span>
                </h1>
            </div>
            <div id="page-inner">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="fancy-title  title-dotted-border">
                            <h3>Current Status</h3>
                        </div>
                    </div>
                    
                    <div class="col-md-3 col-sm-12 col-xs-12">
                        <div class="board">
                            <div class="panel panel-primary">
                                <div class="number">
                                    <h3>
                                        <h3>44,023</h3>
                                        <strong><small style="font-size:medium">Total Sales</small></strong>
                                    </h3>
                                </div>
                                <div class="icon">
                                    <img src="../images/sale1.jpg" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-3 col-sm-12 col-xs-12">
                        <div class="board">
                            <div class="panel panel-primary">
                                <div class="number">
                                    <h3>
                                        <h3>32,850 Ltr</h3>
                                        <strong><small style="font-size:medium">Milk Production</small></strong>
                                    </h3>
                                </div>
                                <div class="icon">
                                   
                                    <img src="../images/milk.jpg" />
                                </div>

                            </div>
                        </div>
                    </div>

                    <div class="col-md-3 col-sm-12 col-xs-12">
                        <div class="board">
                            <div class="panel panel-primary">
                                <div class="number">
                                    <h3>
                                        <h3>56,150 Nos</h3>
                                       <strong> <small style="font-size:medium">Product Production</small></strong>
                                    </h3>
                                </div>
                                <div class="icon">
                                    <%--<i class="fa fa-check-circle fa-5x green"></i>--%>
                                    <img src="../images/MilkProduct4.gif" />
                                </div>

                            </div>
                        </div>
                    </div>

                    <div class="col-md-3 col-sm-12 col-xs-12">
                        <div class="board">
                            <div class="panel panel-primary">
                                <div class="number">
                                    <h3>
                                        <h3>450</h3>
                                        <strong><small style="font-size:medium">Total Employees</small></strong>
                                    </h3>
                                </div>
                                <div class="icon">
                                    <img src="../images/Employee3.jpg" />
                                </div>

                            </div>
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="fancy-title  title-dotted-border">
                            <h3>Corporation Position</h3>
                        </div>
                    </div>
                    <div class="col-sm-4 col-xs-12">
                        <div class="panel panel-default">
                            <div class="panel-heading red">
                                <div class="card-title">
                                    <div class="title">Turnover in Last 5 Year</div>
                                </div>
                            </div>
                            <div class="panel-body">
                                <!--<canvas id="bar-chart" class="chart"></canvas>-->
                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>Financial Year</th>
                                                <th>Turnover in INR</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>2018-19</td>
                                                <td>221329688.41</td>
                                            </tr>
                                            <tr>
                                                <td>2017-18</td>
                                                <td>131329688.41</td>
                                            </tr>
                                            <tr>
                                                <td>2016-17</td>
                                                <td>220899083.99</td>
                                            </tr>
                                            <tr>
                                                <td>2015-16</td>
                                                <td>257770721.91</td>
                                            </tr>
                                            <tr>
                                                <td>2014-15</td>
                                                <td>213192018.15</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4 col-xs-12">
                        <div class="panel panel-default">
                            <div class="panel-heading red">
                                <div class="card-title">
                                    <div class="title">Sales (In Last 5 Year)</div>
                                </div>
                            </div>
                            <div class="panel-body">
                                <table class="table table-striped table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>Financial Year</th>
                                            <th>INR</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>2018-19</td>
                                            <td>-------</td>
                                        </tr>
                                        <tr>
                                            <td>2017-18</td>
                                            <td>-------</td>
                                        </tr>
                                        <tr>
                                            <td>2016-17</td>
                                            <td>-------</td>
                                        </tr>
                                        <tr>
                                            <td>2015-16</td>
                                            <td>-------</td>
                                        </tr>
                                        <tr>
                                            <td>2014-15</td>
                                            <td>-------</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4 col-xs-12">
                        <div class="panel panel-default">
                            <div class="panel-heading red">
                                <div class="card-title">
                                    <div class="title">Profits (In Last 5 Year)</div>
                                </div>
                            </div>
                            <div class="panel-body">
                                <table class="table table-striped table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>Financial Year</th>
                                            <th>INR</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>2018-19</td>
                                            <td>25</td>
                                        </tr>
                                        <tr>
                                            <td>2017-18</td>
                                            <td>22</td>
                                        </tr>
                                        <tr>
                                            <td>2016-17</td>
                                            <td>21</td>
                                        </tr>
                                        <tr>
                                            <td>2015-16</td>
                                            <td>21</td>
                                        </tr>
                                        <tr>
                                            <td>2014-15</td>
                                            <td>21</td>
                                        </tr>
                                    </tbody>
                                </table>
                                <!--<canvas id="line-chart" class="chart"></canvas>-->
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <div class="fancy-title  title-dotted-border">
                            <h3>Top 5 Branches (In Last Year)</h3>
                        </div>
                        <div class="row branches">
                            <div class="col-xs-6 col-md-2">
                                <div class="panel panel-default">
                                    <div class="panel-body easypiechart-panel PT10">
                                        <h4>Bhopal</h4>
                                        <div class="easypiechart" id="easypiechart-blue" data-percent="82">
                                            <span class="percent">82%</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-6 col-md-2">
                                <div class="panel panel-default">
                                    <div class="panel-body easypiechart-panel PT10">
                                        <h4>Dhar</h4>
                                        <div class="easypiechart" id="easypiechart-orange" data-percent="94">
                                            <span class="percent">94%</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-6 col-md-2">
                                <div class="panel panel-default">
                                    <div class="panel-body easypiechart-panel PT10">
                                        <h4>Gwalior</h4>
                                        <div class="easypiechart" id="easypiechart-teal" data-percent="84">
                                            <span class="percent">84%</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-6 col-md-2">
                                <div class="panel panel-default">
                                    <div class="panel-body easypiechart-panel PT10">
                                        <h4>Vidisha</h4>
                                        <div class="easypiechart" id="easypiechart-red" data-percent="82">
                                            <span class="percent">82%</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-6 col-md-2">
                                <div class="panel panel-default">
                                    <div class="panel-body easypiechart-panel PT10">
                                        <h4>Rajgarh</h4>
                                        <div class="easypiechart" id="easypiechart-purple" data-percent="85">
                                            <span class="percent">85%</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-8 col-xs-12">
                        <div class="panel panel-default">
                            <div class="panel-heading green">
                                <div class="card-title">
                                    <div class="title">Production - Last 5 Year</div>
                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-4 col-xs-12">
                                        <h4>Indrapuri</h4>
                                        <table class="table table-striped table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>Financial Year</th>
                                                    <th>Mt. Tn.</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>2018-19</td>
                                                    <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#myModal">1000</a> </td>
                                                </tr>
                                                <tr>
                                                    <td>2017-18</td>
                                                    <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#myModal">875</a></td>
                                                </tr>
                                                <tr>
                                                    <td>2016-17</td>
                                                    <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#myModal">852</a></td>
                                                </tr>
                                                <tr>
                                                    <td>2015-16</td>
                                                    <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#myModal">865</a></td>
                                                </tr>
                                                <tr>
                                                    <td>2014-15</td>
                                                    <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#myModal">849</a></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-sm-4 col-xs-12">
                                        <h4>Badai</h4>
                                        <table class="table table-striped table-bordered table-hover">
                                                    <thead>
                                                        <tr>
                                                            <th>Financial Year</th>
                                                            <th>Mt. Tn.</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>2018-19</td>
                                                            <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#myModal">1000</a> </td>
                                                        </tr>
                                                        <tr>
                                                            <td>2017-18</td>
                                                            <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#myModal">875</a></td>
                                                        </tr>
                                                        <tr>
                                                            <td>2016-17</td>
                                                            <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#myModal">852</a></td>
                                                        </tr>
                                                        <tr>
                                                            <td>2015-16</td>
                                                            <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#myModal">865</a></td>
                                                        </tr>
                                                        <tr>
                                                            <td>2014-15</td>
                                                            <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#myModal">849</a></td>
                                                        </tr>
                                                    </tbody>
                                                </table>                                             
                                    </div>
                                    <div class="col-sm-4 col-xs-12">
                                                    <h4>Badi</h4>
                                                
                                                <table class="table table-striped table-bordered table-hover">
                                                    <thead>
                                                        <tr>
                                                            <th>Financial Year</th>
                                                            <th>Mt. Tn.</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>2018-19</td>
                                                            <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#myModal">1000</a> </td>
                                                        </tr>
                                                        <tr>
                                                            <td>2017-18</td>
                                                            <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#myModal">875</a></td>
                                                        </tr>
                                                        <tr>
                                                            <td>2016-17</td>
                                                            <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#myModal">852</a></td>
                                                        </tr>
                                                        <tr>
                                                            <td>2015-16</td>
                                                            <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#myModal">865</a></td>
                                                        </tr>
                                                        <tr>
                                                            <td>2014-15</td>
                                                            <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#myModal">849</a></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                    <div class="col-sm-4 col-xs-12">
                        <div class="panel panel-default">
                            <div class="panel-heading green">
                                <div class="card-title">
                                    <div class="title">Balance Sheet Last 5 Year</div>
                                </div>
                            </div>
                            <div class="panel-body">
                                <table class="table table-striped table-hover">
                                    <thead>
                                        <tr>
                                            <th>Financial Year</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td><a href="#">2018-19</a></td>
                                        </tr>
                                        <tr>
                                            <td><a href="#">2017-18</a></td>
                                        </tr>
                                        <tr>
                                            <td><a href="#">2016-17</a></td>
                                        </tr>
                                        <tr>
                                            <td><a href="#">2015-16</a></td>
                                        </tr>
                                        <tr>
                                            <td><a href="#">2014-15</a></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    
                </div>
                <div class="row">
                    <div class="col-sm-3 col-xs-12">
                        <div class="panel panel-default">
                            <div class="panel-heading blue">
                                <div class="card-title">
                                    <div class="title">Legal Cases</div>
                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="list-group">
                                    <a href="#" class="list-group-item">
                                        <span class="badge blue" style="color:#fff;">15,896</span>
                                        <i class="fa fa-list"></i> Total Cases
                                    </a>
                                    <a href="#" class="list-group-item">
                                        <span class="badge red">5,795</span>
                                        <i class="fa fa-list"></i> Open Cases
                                    </a>
                                    <a href="#" class="list-group-item">
                                        <span class="badge">10,101</span>
                                        <i class="fa fa-list"></i> Closed Cases
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3 col-xs-12">
                        <div class="panel panel-default">
                            <div class="panel-heading blue">
                                <div class="card-title">
                                    <div class="title">RTI Cases</div>
                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="list-group">
                                    <a href="#" class="list-group-item">
                                        <span class="badge blue" style="color:#fff;">15,896</span>
                                        <i class="fa fa-list-alt"></i> Total RTI
                                    </a>
                                    <a href="#" class="list-group-item">
                                        <span class="badge red">5,795</span>
                                        <i class="fa fa-list-alt"></i> Pending RTI
                                    </a>
                                    <a href="#" class="list-group-item">
                                        <span class="badge">10,101</span>
                                        <i class="fa fa-list-alt"></i> Closed Cases
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6 col-xs-12" >
                        <div class="panel panel-default">
                            <div class="panel-heading blue">
                                <div class="card-title">
                                    <div class="title">MD'S Session at Corporation</div>
                                </div>
                            </div>
                            <div class="panel-body">
                                <table class="table table-striped table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>Name</th>
                                            <th>Designation</th>
                                            <th>From Date</th>
                                            <th>To Date</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>Shri Alok Kumar Singh I.A.S</td>
                                            <td>M.D.</td>
                                            <td>-----</td>
                                            <td>22/07/2019</td>
                                        </tr>
                                        <tr>
                                            <td>-----</td>
                                            <td>M.D.</td>
                                            <td>-----</td>
                                            <td>22/07/2019</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-3 col-xs-12">
                        <div class="panel panel-default">
                            <div class="panel-heading red">
                                <div class="card-title">
                                    <div class="title">Scheme Details</div>
                                </div>
                            </div>
                            <div class="panel-body">
                                <table class="table table-striped table-bordered table-hover">
                                    <tbody>
                                        <tr>
                                            <th>Total Balance</th>
                                            <th>------</th>
                                        </tr>
                                        <tr>
                                            <td>SC</td>
                                            <td>------</td>
                                        </tr>
                                        <tr>
                                            <td>ST</td>
                                            <td>------</td>
                                        </tr>
                                        <tr>
                                            <td>Genral</td>
                                            <td>------</td>
                                        </tr>
                                        <tr>
                                            <th>Total Disbursement</th>
                                            <th>------</th>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3 col-xs-12">
                        <div class="panel panel-default">
                            <div class="panel-heading red">
                                <div class="card-title">
                                    <div class="title">Employees Age Details</div>
                                </div>
                            </div>
                            <div class="panel-body">
                                <table class="table table-striped table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>Age greater than 50 Years</th>
                                            <th>Age more than 20 Years</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>-----</td>
                                            <td>-----</td>
                                        </tr>
                                        <tr>
                                            <td>-----</td>
                                            <td>-----</td>
                                        </tr>
                                        <tr>
                                            <td>-----</td>
                                            <td>-----</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6 col-xs-12">
                        <div class="panel panel-default">
                            <div class="panel-heading red">
                                <div class="card-title">
                                    <div class="title">Leaves Details</div>
                                </div>
                            </div>
                            <div class="panel-body">
                                <table class="table table-striped table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th colspan="6">Total Availed Leaves</th>
                                        </tr>
                                        <tr>
                                            <th colspan="6">Employee Name : Sambhav Jain</th>
                                        </tr>
                                        <tr>
                                            <th>CL</th>
                                            <th>ML</th>
                                            <th>EL</th>
                                            <th>OL</th>
                                            <th>Others</th>
                                            <th>Total</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>5</td>
                                            <td>2</td>
                                            <td>4</td>
                                            <td>2</td>
                                            <td>5</td>
                                            <td>18</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <footer>
                    <p>Design & Developed by: <a href="#">Crips</a></p>
                </footer>
            </div>
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

    <!-- End Modals-->
    <!-- /. WRAPPER  -->
    <!-- JS Scripts-->
    <!-- jQuery Js -->
    <script src="js/jquery-1.10.2.js"></script>
    <!-- Bootstrap Js -->
    <script src="js/bootstrap.min.js"></script>

    <!-- Metis Menu Js -->
    <script src="js/jquery.metisMenu.js"></script>
    <!-- Morris Chart Js -->
    <script src="js/morris/raphael-2.1.0.min.js"></script>
    <script src="js/morris/morris.js"></script>


    <script src="js/easypiechart.js"></script>
    <script src="js/easypiechart-data.js"></script>

    <script src="js/Lightweight-Chart/jquery.chart.js"></script>

    <!-- Custom Js -->
    <script src="js/custom-scripts.js"></script>

    <!-- Chart Js -->
    <script type="text/javascript" src="js/chart.min.js"></script>
    <script type="text/javascript" src="js/chartjs.js"></script>
</body>

<!-- Mirrored from webthemez.com/demo/brilliant-free-bootstrap-admin-template/index.html by HTTrack Website Copier/3.x [XR&CO'2014], Mon, 22 Jul 2019 06:23:40 GMT -->
</html>
