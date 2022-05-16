<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MonitoringDashboard.aspx.cs" Inherits="mis_Dashboard_MonitoringDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../../mis/Dashboard/Dashboard1/fontawesome-free/css/all.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700" rel="stylesheet" />

    <link href="../css/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <style>
        /*.bg-info {
            background-color: #93cbe6;
        }

        .bg-success {
            background-color: #88da66;
        }

        .bg-danger {
            background-color: #f19f9f;
        }

        .bg-warning {
            background-color: #f3db60;
        }

        .bg-fuchsia {
            background-color: #ff0a21de !important;
        }*/

        .small-box h3 {
            font-size: 24px;
        }

        .card-header {
            padding: 5px 10px;
            margin-bottom: 0;
            background-color: #2e9eff;
            border-bottom: 1px solid #2677bd;
            font-weight: bold;
            color: #fff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <section class="content-header">
            <h1>Bhopal Plant Dashboard
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Dashboard</li>
            </ol>
        </section>
        <section class="content">

            <div class="container-fluid">
                <div class="row">
                    <%--   <div class="col-md-12" style="text-align: center; font-size:22px;">
                        Monitoring board
                    </div>--%>
                    <fieldset>
                        <legend style="font-size: 22px;">Monitoring board</legend>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Date<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtOrderDate" Text="<i class='fa fa-exclamation-circle' title='Select Date!'></i>" ErrorMessage="Enter Select Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtOrderDate" runat="server" Style="background-color: white;" placeholder="Select Date..." class="form-control DateAdd" onpaste="return false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-danger" Style="margin-top: 19px;" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-lg-4 col-6" style="border-radius: 25px;">
                                <!-- small box -->
                                <div class="small-box bg-aqua" style="border-radius: 25px;">
                                    <div class="card-header">
                                        <h3 class="card-title" style="font-size: 20px;">E-CHALLAN</h3>
                                    </div>
                                    <div class="inner" style="min-height: 80px;">
                                        <div class="row">
                                            <div class="col-md-8">
                                                <b>E-CHALLAN Till Date</b> :
                                                <asp:Label ID="lblchallanentrydate" runat="server" Text="05/10/2020" Font-Size="15px"></asp:Label>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-8">
                                                <b>No of E-CHALLAN</b> :
                                                <asp:Label ID="lblchallanentryRecord" runat="server" Text="27" Font-Size="15px"></asp:Label>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="icon">
                                        <i class="ion ion-bag"></i>
                                    </div>
                                    <asp:LinkButton ID="Linkchallan" ToolTip="Receive" class="small-box-footer" runat="server" OnClientClick="document.forms[0].target = '_blank';" OnClick="Linkchallan_Click"> 
                                            More info <i class="fa fa-arrow-circle-right"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="col-lg-4 col-6">
                                <!-- small box -->
                                <div class="small-box bg-green" style="border-radius: 25px;">
                                    <div class="card-header">
                                        <h3 class="card-title" style="font-size: 20px;">RAW MILK RECEPTION DOCK</h3>
                                    </div>
                                    <div class="inner" style="min-height: 80px;">
                                        <div class="row">
                                            <div class="col-md-8">
                                                <b>RMRD Date</b> :
                                               <asp:Label ID="lblRMRDDate" runat="server" Text="05/10/2020"></asp:Label>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-8">
                                                <b>Milk Collection</b> :
                                                 <asp:Label ID="lblRMRDRecord" runat="server" Text="222"></asp:Label>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="icon">
                                        <i class="ion ion-bag"></i>
                                    </div>
                                     <asp:LinkButton ID="lnkRMRD" ToolTip="Receive" class="small-box-footer" runat="server" OnClick="lnkRMRD_Click" OnClientClick="document.forms[0].target='_blank';"> 
                                            More info <i class="fa fa-arrow-circle-right"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="col-lg-4 col-6">
                                <!-- small box -->
                                <div class="small-box bg-yellow" style="border-radius: 25px;">
                                    <div class="card-header">
                                        <h3 class="card-title" style="font-size: 20px;">MARKETING</h3>
                                    </div>
                                    <div class="inner" style="min-height: 80px;">
                                        <div class="row">
                                            <div class="col-md-8">
                                                <b>Milk Demand  Date</b> :
                                              <asp:Label ID="lblmarketingentry" runat="server" Text="05/10/2020"></asp:Label>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-6" style="text-align: right;">
                                                    Quantity: 
                                            <asp:Label ID="lblDemand" runat="server" Text="222"></asp:Label>
                                                </div>

                                                <div class="col-md-6" style="text-align: right; font-weight: bold;">
                                                    Quantity(Ltr): 
                                            <asp:Label ID="lblDemandltr" runat="server" Text="222"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-8">
                                                <b>Product  Date</b> :
                                              <asp:Label ID="lblProductDate" runat="server" Text="05/10/2020"></asp:Label>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-6" style="text-align: right;">
                                                    Quantity(Demand): 
                                            <asp:Label ID="lblProductDemand" runat="server" Text="222"></asp:Label>
                                                </div>

                                                <div class="col-md-6" style="text-align: right; font-weight: bold;">
                                                    Quantity(Supply): 
                                            <asp:Label ID="lblProductSupply" runat="server" Text="222"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="icon">
                                        <i class="ion ion-bag"></i>
                                    </div>
                                     <asp:LinkButton ID="LinkButton4" ToolTip="Receive" class="small-box-footer" runat="server"  OnClientClick="document.forms[0].target = '_blank';" OnClick="LinkButton4_Click"> 
                                            More info <i class="fa fa-arrow-circle-right"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>

                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-4 col-6">
                                <!-- small box -->
                                <div class="small-box bg-info" style="border-radius: 25px;">
                                    <div class="card-header">
                                        <h3 class="card-title" style="font-size: 20px;">PLANT OPERATION</h3>
                                    </div>
                                    <div class="inner" style="min-height: 80px;">
                                        <div class="row">
                                            <div class="col-md-8">
                                                <b>Whole Milk Date</b> :
                                               <asp:Label ID="lblPlantDate" runat="server" Text="05/10/2020"></asp:Label>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <b>Milk </b> :
                                                 <asp:Label ID="lblPlantRecord" runat="server" Text="222"></asp:Label>
                                            </div>
                                            <div class="col-md-4">
                                                <b>Fat</b> :
                                                 <asp:Label ID="lblFat" runat="server" Text="222"></asp:Label>
                                            </div>
                                            <div class="col-md-4">
                                                <b>SNF</b> :
                                                 <asp:Label ID="lblSNF" runat="server" Text="222"></asp:Label>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="icon">
                                        <i class="ion ion-bag"></i>
                                    </div>
                                       <asp:LinkButton ID="LinkButton1" ToolTip="Receive" class="small-box-footer" runat="server" OnClick="LinkButton1_Click" Visible="true" OnClientClick="document.forms[0].target = '_blank';"> 
                                            More info <i class="fa fa-arrow-circle-right"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="col-lg-4 col-6">
                                <!-- small box -->
                                <div class="small-box bg-blue" style="border-radius: 25px;">
                                    <div class="card-header">
                                        <h3 class="card-title" style="font-size: 20px;">QUALITY CONTROL</h3>
                                    </div>
                                    <div class="inner" style="min-height: 80px;">
                                        <div class="row">
                                            <div class="col-md-8">
                                                <b>Current QC Date</b> :
                                               <asp:Label ID="lblQCDate" runat="server" Text="05/10/2020"></asp:Label>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-8">
                                                <b>QC Records</b> :
                                                 <asp:Label ID="lblQCRecord" runat="server" Text="222"></asp:Label>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="icon">
                                        <i class="ion ion-bag"></i>
                                    </div>
                                    <asp:LinkButton ID="lnkQC" ToolTip="Receive" class="small-box-footer" runat="server" OnClientClick="document.forms[0].target = '_blank';" OnClick="lnkQC_Click"> 
                                            More info <i class="fa fa-arrow-circle-right"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="col-lg-4 col-6">
                                <!-- small box -->
                                <div class="small-box bg-teal" style="border-radius: 25px;">
                                    <div class="card-header">
                                        <h3 class="card-title" style="font-size: 20px;">PAYROLL</h3>
                                    </div>
                                    <div class="inner" style="min-height: 80px;">
                                        <div class="row">
                                            <div class="col-md-8">
                                                <b>PAYROLL Month</b> :
                                               <asp:Label ID="lblPAYROLLDate" runat="server" Text="N.A"></asp:Label>
                                            </div>
                                            <div class="col-md-8">
                                                <b>PAYROLL Year</b> :
                                               <asp:Label ID="lblPayrolyear" runat="server" Text="N.A"></asp:Label>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-8">
                                                <b>PAYROLL Generated</b> :
                                                 <asp:Label ID="lblPAYROLLrecord" runat="server" Text="N.A"></asp:Label>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="icon">
                                        <i class="ion ion-bag"></i>
                                    </div>
                                       <asp:LinkButton ID="lnkPayrol" ToolTip="Receive" class="small-box-footer" runat="server" OnClick="lnkPayrol_Click"  Visible="true" OnClientClick="document.forms[0].target = '_blank';"> 
                                            More info <i class="fa fa-arrow-circle-right"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>

                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-4 col-6">
                                <!-- small box -->
                                <div class="small-box bg-gray" style="border-radius: 25px;">
                                    <div class="card-header">
                                        <h3 class="card-title" style="font-size: 20px;">FILE TRACKING</h3>
                                    </div>
                                    <div class="inner" style="min-height: 80px;">
                                        <div class="row">
                                            <div class="col-md-8">
                                                <b>FILE Created Date</b> :
                                               <asp:Label ID="lblfiletrackingDate" runat="server" Text="05/10/2020"></asp:Label>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-8">
                                                <b>FILE Created</b> :
                                                 <asp:Label ID="lblfiletrackingRecord" runat="server" Text="222"></asp:Label>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="icon">
                                        <i class="ion ion-bag"></i>
                                    </div>
                                    <asp:LinkButton ID="LinkButton7" ToolTip="Receive" class="small-box-footer" runat="server" OnClientClick="document.forms[0].target = '_blank';" OnClick="LinkButton7_Click"> 
                                            More info <i class="fa fa-arrow-circle-right"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="col-lg-4 col-6">
                                <!-- small box -->
                                <div class="small-box  bg-db3" style="border-radius: 25px;">
                                    <div class="card-header">
                                        <h3 class="card-title" style="font-size: 20px;">HUMAN RESOURCE</h3>
                                    </div>
                                    <div class="inner" style="min-height: 80px;">
                                      <%--  <div class="row">
                                            <div class="col-md-8">
                                                <b>Current Date</b> :
                                               <asp:Label ID="lblHRDate" runat="server" Text="N.A"></asp:Label>
                                            </div>

                                        </div>--%>
                                        <div class="row">
                                            <div class="col-md-8">
                                                <b>Registered Employees</b> :
                                                 <asp:Label ID="lblHRRecord" runat="server" Text="N.A"></asp:Label>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="icon">
                                        <i class="ion ion-bag"></i>
                                    </div>
                                      <asp:LinkButton ID="lnkEmp" ToolTip="Receive" class="small-box-footer" runat="server" OnClick="lnkEmp_Click"> 
                                            More info <i class="fa fa-arrow-circle-right"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="col-lg-4 col-6">
                                <!-- small box -->
                                <div class="small-box  bg-db4" style="border-radius: 25px;">
                                    <div class="card-header">
                                        <h3 class="card-title" style="font-size: 20px;">FINANCE</h3>
                                    </div>
                                    <div class="inner" style="min-height: 80px;">
                                        <div class="row">
                                            <div class="col-md-8">
                                                <b>Current Voucher Date</b> :
                                               <asp:Label ID="lbFINANCEDate" runat="server" Text="05/10/2020"></asp:Label>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-8">
                                                <b>Voucher Records</b> :
                                                 <asp:Label ID="lbFINANCERecord" runat="server" Text="222"></asp:Label>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="icon">
                                        <i class="ion ion-bag"></i>
                                    </div>
                                    <asp:LinkButton ID="lnkVoucher" ToolTip="Receive" class="small-box-footer" runat="server" OnClientClick="document.forms[0].target = '_blank';" OnClick="lnkVoucher_Click"> 
                                            More info <i class="fa fa-arrow-circle-right"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

