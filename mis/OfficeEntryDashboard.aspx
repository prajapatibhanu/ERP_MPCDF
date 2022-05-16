<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OfficeEntryDashboard.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="mis_OfficeEntryDashboard" %>

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
    <form runat="server">
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
                            <i class="fa fa-envelope fa-fw"></i><i class="fa fa-caret-down"></i>
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
                            <i class="fa fa-bell fa-fw"></i><i class="fa fa-caret-down"></i>
                        </a>
                        <ul class="dropdown-menu dropdown-alerts">
                            <li>
                                <a href="#">
                                    <div>
                                        <i class="fa fa-comment fa-fw"></i>New Comment
                                    <span class="pull-right text-muted small">4 min</span>
                                    </div>
                                </a>
                            </li>
                            <li class="divider"></li>
                            <li>
                                <a href="#">
                                    <div>
                                        <i class="fa fa-twitter fa-fw"></i>3 New Followers
                                    <span class="pull-right text-muted small">12 min</span>
                                    </div>
                                </a>
                            </li>
                            <li class="divider"></li>
                            <li>
                                <a href="#">
                                    <div>
                                        <i class="fa fa-envelope fa-fw"></i>Message Sent
                                    <span class="pull-right text-muted small">4 min</span>
                                    </div>
                                </a>
                            </li>
                            <li class="divider"></li>
                            <li>
                                <a href="#">
                                    <div>
                                        <i class="fa fa-tasks fa-fw"></i>New Task
                                    <span class="pull-right text-muted small">4 min</span>
                                    </div>
                                </a>
                            </li>
                            <li class="divider"></li>
                            <li>
                                <a href="#">
                                    <div>
                                        <i class="fa fa-upload fa-fw"></i>Server Rebooted
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
                            <i class="fa fa-user fa-fw"></i><i class="fa fa-caret-down"></i>
                        </a>
                        <ul class="dropdown-menu dropdown-user">
                            <li>
                                <a href="#"><i class="fa fa-user fa-fw"></i>User Profile</a>
                            </li>
                            <li>
                                <a href="#"><i class="fa fa-gear fa-fw"></i>Settings</a>
                            </li>
                            <li class="divider"></li>
                            <li>
                                <a href="#"><i class="fa fa-sign-out fa-fw"></i>Logout</a>
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
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
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
                                            <h3>
                                                <asp:Label ID="lbltotalCC" runat="server"></asp:Label></h3>
                                            <strong><small style="font-size: medium">Total CC</small></strong>
                                        </h3>
                                    </div>
                                    <div class="icon">
                                        <img src="../images/Building_Image.png" width="80" height="63" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-3 col-sm-12 col-xs-12">
                            <div class="board">
                                <div class="panel panel-primary">
                                    <div class="number">
                                        <h3>
                                            <h3>
                                                <asp:Label ID="lbllbltotalBMC" runat="server"></asp:Label></h3>
                                            <strong><small style="font-size: medium">Total BMC</small></strong>
                                        </h3>
                                    </div>
                                    <div class="icon">

                                        <img src="../images/Building_Image.png" width="80" height="63" />
                                    </div>

                                </div>
                            </div>
                        </div>

                        <div class="col-md-3 col-sm-12 col-xs-12">
                            <div class="board">
                                <div class="panel panel-primary">
                                    <div class="number">
                                        <h3>
                                            <h3>
                                                <asp:Label ID="lbltotalDCS" runat="server"></asp:Label></h3>
                                            <strong><small style="font-size: medium">Total DCS</small></strong>
                                        </h3>
                                    </div>
                                    <div class="icon">
                                        <%--<i class="fa fa-check-circle fa-5x green"></i>--%>
                                        <img src="../images/Building_Image.png" width="80" height="63" />
                                    </div>

                                </div>
                            </div>
                        </div>

                        <div class="col-md-3 col-sm-12 col-xs-12">
                            <div class="board">
                                <div class="panel panel-primary">
                                    <div class="number">
                                        <h3>
                                            <h3>
                                                <asp:Label ID="lbllbltotalTotalProducer" runat="server"></asp:Label></h3>
                                            <strong><small style="font-size: medium">Total Producer</small></strong>
                                        </h3>
                                    </div>
                                    <div class="icon">
                                        <img src="../images/Employee3.jpg" />
                                    </div>

                                </div>
                            </div>
                        </div>


                        <div class="col-md-3 col-sm-12 col-xs-12">
                            <div class="board">
                                <div class="panel panel-primary">
                                    <div class="number">
                                        <h3>
                                            <h3>
                                                <asp:Label ID="lblDispatchCCCount" runat="server"></asp:Label></h3>
                                            <strong><small style="font-size: medium">Milk Reported CC</small></strong>
                                        </h3>
                                    </div>
                                    <div class="icon">
                                        <img src="../images/milk.jpg" />
                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="fancy-title  title-dotted-border">
                                <h3>Deo Work</h3>
                            </div>
                        </div>
                        <div class="col-sm-4 col-xs-12">
                            <div class="panel panel-default">
                                <div class="panel-heading red">
                                    <div class="card-title">
                                        <div class="title">Today's Producer Entry By DEO</div>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-4">&nbsp;</div>
                                        <div class="col-md-4">&nbsp;</div>
                                        <div class="col-md-4">
                                            <label>Filter Date</label>
                                            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtEffectiveDate" AutoPostBack="true" OnTextChanged="txtEffectiveDate_TextChanged" data-date-end-date="d" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" Text='<%# Eval("EffectiveDate") %>' placeholder="Select Effective from Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator104" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtEffectiveDate" ErrorMessage="Please Enter Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter Effective from Date !'></i>"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator54" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtEffectiveDate" ErrorMessage="Invalid Effective from Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Effective from Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                                </span>
                                            </div>

                                        </div>
                                    </div>

                                    <!--<canvas id="bar-chart" class="chart"></canvas>-->
                                    <div class="table-responsive">

                                        <asp:GridView ID="gvDeoInfo" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover"
                                            EmptyDataText="No Record Found.">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Deo Name" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeoName" runat="server" Text='<%# Eval("DeoName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Deo Mobile" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeoMobile" runat="server" Text='<%# Eval("DeoMobile") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="P_Count" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblP_Count" runat="server" Text='<%# Eval("P_Count") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Date" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEDate" runat="server" Text='<%# Eval("EDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>

                                        <%--<table class="table table-striped table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>DeoName</th>
                                                <th>DeoMobile</th>
                                                 <th>Producer Count</th>
                                                <th>Date</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>LAXMAN  </td>
                                                <td>7489250319</td>
                                                <td>60</td>
                                                <td>10/04/2020</td>
                                            </tr>
                                             
                                        </tbody>
                                    </table>--%>
                                    </div>
                                </div>
                            </div>
                        </div>



                        <%-- <div class="col-sm-4 col-xs-12">
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
                            </div>--%>
                        <%-- <div class="panel-body">
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
                        --%>
                    </div>
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
    </form>
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

    <script src="js/bootstrap-datepicker.js"></script>
    <script src="js/jquery.datetimepicker.js"></script>

    <script type="text/javascript">
        $(function () {
            $('#txtDate').datetimepicker();
        });
    </script>


</body>
</html>
