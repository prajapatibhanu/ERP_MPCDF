<%@ Page Language="C#" AutoEventWireup="true" Culture="en-IN" CodeFile="BMCGatePassReferenceDetails.aspx.cs" Inherits="mis_MilkCollection_BMCGatePassReferenceDetails" %>

<!DOCTYPE html>
 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Gate Pass Reference Details</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <!-- Bootstrap 3.3.7 -->
    <link rel="stylesheet" href="../css/bootstrap.css">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css">

    <!-- Theme style -->
    <link rel="stylesheet" href="../css/AdminLTE.css">

    <!-- Google Font -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic">
    <style type="text/css">
        .btn-danger {
            color: #4b4c9d;
        }

        table th {
            color: #fff;
        }

        .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
            padding: 8px;
            line-height: 1.42857143;
            vertical-align: top;
            border-top: 1px solid #ddd;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <asp:Label ID="lblError" runat="server"></asp:Label>
        <br />
        <div>
            <div style="width: 90%; margin: auto; border-radius: 5px; border: 1px solid #b5b5b5; padding: 5px">
                <asp:Button runat="server" Text="Back" CssClass="btn btn-success no-print" Style="position: fixed; z-index: 999999" OnClick="Unnamed_Click" />
                <!-- Main content -->
                <section class="invoice">
                    <!-- title row -->
                    <div class="row">
                        <div class="col-xs-12" style="border-bottom: 1px solid #eee;">
                            <div class="col-xs-3 col-md-3">
                                <asp:Image ID="imgLogo" Width="100%" runat="server" />
                            </div>
                            <div class="col-xs-9 col-md-6">
                                <h2 class="page-header text-center" style="border-bottom: 1px solid #FFF!important;">
                                    <asp:Label ID="lblOfficeName" runat="server"></asp:Label><br />
                                    <small>&nbsp;
                                        <span style="margin-top: 10px">
                                            <asp:Label ID="lblOfficeAddress" runat="server"></asp:Label></span></small><br />
                                    <p style="font-size: 20px;">Gate Pass</p>
                                </h2>
                            </div>
                            <div class="col-xs-0 col-md-3">
                                <img src="../../images/bds_logo.png" />

                            </div>
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                        <!-- /.col -->
                    </div>
                    <!-- info row -->
                    <div class="row invoice-info">
                        <div class="col-sm-4 invoice-col">
                            From
          <address>
              <b>Name</b>
              <asp:Label ID="lblProduUnitName" runat="server"></asp:Label>
              <br />
              <b>Address</b>
              <asp:Label ID="lblpuAddress" runat="server"></asp:Label>
              - 
              <asp:Label ID="lblPuPincode" runat="server"></asp:Label><br />
              <b>Phone No.</b> :&nbsp;
              <asp:Label ID="lblPuContactNo" runat="server"></asp:Label><br />
              <b>Email:</b>
              <asp:Label ID="lblPuEmail" runat="server"></asp:Label><br />

          </address>
                        </div>
                        <!-- /.col -->
                        <div class="col-sm-4 invoice-col">
                        </div>
                        <!-- /.col -->
                        <div class="col-sm-4 invoice-col">
                            Gate Pass Details
                            <br />
                            <b>Reference No:&nbsp;
                                <asp:Label ID="lblC_ReferenceNo" runat="server"></asp:Label></b><br />

                            <b>Date Time:</b>
                            <asp:Label ID="lblDT_TankerDispatchDate" runat="server"></asp:Label><br />

                            <b>Vehicle No:</b>
                            <asp:Label ID="lblV_VehicleNo" runat="server"></asp:Label><br />

                            <b>Driver Details:</b>
                            <asp:Label ID="lblV_DriverName" runat="server"></asp:Label>&nbsp;&nbsp;(<asp:Label ID="lblV_DriverMobileNo" runat="server"></asp:Label>)<br />
                            <b>Driver Licence:</b>
                            <asp:Label ID="lblLicence" runat="server"></asp:Label><br />
                            <b>Tester Details:</b>
                            <asp:Label ID="lblV_TesterName" runat="server"></asp:Label>&nbsp;&nbsp;(<asp:Label ID="lblV_TesterMobileNo" runat="server"></asp:Label>)<br />
                            


                        </div>
                        <!-- /.col -->
                    </div>
                    <!-- /.row -->

                    <!-- Table row -->
                    <%--<div class="row">
                        <div class="col-xs-12 table-responsive">
                            <b>Tanker Milk Collection Sequence </b>
                            <br />
                            <br />
                            <asp:GridView ID="gvProductDetails" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover"
                                EmptyDataText="No Record Found." ShowFooter="true">
                                <Columns>

                                    <asp:TemplateField HeaderText="Sr. No." HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Root No" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTI_SequenceNo" runat="server" Text='<%#Eval("TI_SequenceNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="BMC Name" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOffice_Name" runat="server" Text='<%#Eval("Office_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Distance In KM" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDistanceInKm" runat="server" Text='<%# Eval("DistanceInKm") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Arrival Time" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblArrivalDateTime" runat="server" Text='<%# Eval("ArrivalDateTime") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Dispatch Time" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDispatchDateTime" runat="server" Text='<%# Eval("DispatchDateTime") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>

                        </div>
                        <!-- /.col -->
                    </div>--%>
                    <!-- /.row -->

                    <!-- Table row -->
                    <div class="row">
                        <div class="col-xs-12 table-responsive">
                            <b>Tanker Seal Details </b>
                            <br />
                            <br />
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover"
                                EmptyDataText="No Record Found." ShowFooter="true">
                                <Columns>

                                    <asp:TemplateField HeaderText="Sr. No." HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Seal No" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblV_SealNo" runat="server" Text='<%#Eval("V_SealNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Seal Location" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblV_SealLocation" runat="server" Text='<%#Eval("V_SealLocation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Seal Remark" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblV_SealRemark" runat="server" Text='<%# Eval("V_SealRemark") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    

                                </Columns>
                            </asp:GridView>

                        </div>
                        <!-- /.col -->
                    </div>
                    <!-- /.row -->
                    <div class="row">
                        <!-- accepted payments column -->
                        <div class="col-xs-6">
                            .................................................<br />
                            Store Keeper<br />
                            Dispatch Tanker in good condition<br />
                            .................................................
                        </div>
                        <div class="col-xs-6">
                            <p class="pull-right">
                                ..................................................<br />
                                Issuing Authority
                            </p>
                        </div>
                    </div>
                    <!-- /.row -->

                    <!-- this row will not appear when printing -->
                    <div class="row no-print">
                        <div class="col-xs-12">
                            <a onclick="myFunction()" target="_blank" class="btn btn-default"><i class="fa fa-print"></i>Print</a>
                        </div>
                    </div>
                </section>
                <!-- /.content -->
                <div class="clearfix"></div>
            </div>
        </div>
        <script src="../../js/jquery-2.2.3.min.js"></script>
        <script>
            function myFunction() {
                window.print();
            }
        </script>
        <!-- ./wrapper -->
        <!-- jQuery 3 -->
        <script src="../../js/jquery-2.2.3.min.js"></script>
        <!-- Bootstrap 3.3.7 -->
        <script src="../../js/bootstrap.js"></script>
        <!-- FastClick -->
    </form>
</body>
</html>
