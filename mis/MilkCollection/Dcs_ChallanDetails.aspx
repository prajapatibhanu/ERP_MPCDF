<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dcs_ChallanDetails.aspx.cs" Inherits="mis_MilkCollection_Dcs_ChallanDetails" %>
 
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
                                <img src="../../images/bds_logo.png" /> 
                            </div>
                            <div class="col-xs-9 col-md-6">
                                <h2 class="page-header text-center" style="border-bottom: 1px solid #FFF!important;">
                                    <asp:Label ID="lblcofficename" runat="server"></asp:Label><br />
                                    <small>&nbsp;<br />
                                        <span style="margin-top: 10px">
                                            <h5>
                                                <asp:Label ID="lblofficeaddress" runat="server"></asp:Label>
                                                -
                                                <asp:Label ID="lblpincode" runat="server"></asp:Label></h5>
                                        </span></small>
                                    <br />
                                    <p style="font-size: 20px;"><b>Delivery Challan</b></p>
                                </h2>
                            </div>
                            <div class="col-xs-0 col-md-3"></div>
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                        <!-- /.col -->
                    </div>
                    <!-- info row -->
                    <div class="row invoice-info">
                        <div class="col-sm-4 invoice-col">
                            <b>From</b>
                            <address>
                                <b>Name</b>
                                <asp:Label ID="lblProduUnitName" runat="server"></asp:Label>
                                <br />
                                <b>Address</b>
                                <asp:Label ID="lblpuAddress" runat="server"></asp:Label>
                                - 
              <asp:Label ID="lblPuPincode" runat="server"></asp:Label><br />
                                <b>Mobile</b> :&nbsp;
              <asp:Label ID="lblPuContactNo" runat="server"></asp:Label><br />
                                <b>Email:</b>
                                <asp:Label ID="lblPuEmail" runat="server"></asp:Label><br />

                            </address>
                        </div>
                        <!-- /.col -->
                        <div class="col-sm-4 invoice-col">
                            <b>To</b>
                            <address>
                                <b>Office Name</b>
                                <asp:Label ID="lblRProduUnitName" runat="server"></asp:Label>
                                <br />
                                <b>Office Address</b>
                                <asp:Label ID="lblRpuAddress" runat="server"></asp:Label>
                                - 
              <asp:Label ID="lblRPuPincode" runat="server"></asp:Label><br />
                                <b>Mobile</b> :&nbsp;
              <asp:Label ID="lblRPuContactNo" runat="server"></asp:Label><br />
                                <b>Email:</b>
                                <asp:Label ID="lblRPuEmail" runat="server"></asp:Label><br />

                            </address>

                        </div>
                        <!-- /.col -->
                        <div class="col-sm-4 invoice-col">
                            <b>Challan Details</b>
                            <br />
                              
                            <b>Challan No:&nbsp;
                                <asp:Label ID="lblchallanno" runat="server"></asp:Label></b><br />

                            <b>Dispatch Date Time:</b>
                            <asp:Label ID="lblDT_TankerDispatchDate" runat="server"></asp:Label><br />

                            <b>Vehicle No:</b>
                            <asp:Label ID="lblV_VehicleNo" runat="server"></asp:Label><br />

                            <b>Driver Details:</b>
                            <asp:Label ID="lblV_DriverName" runat="server"></asp:Label>&nbsp;&nbsp;(<asp:Label ID="lblV_DriverMobileNo" runat="server"></asp:Label>)<br />
                            
                        </div>
                        <!-- /.col -->
                    </div>
                    <!-- /.row -->

                    <!-- Table row -->
                    <div class="row">
                        <div class="col-xs-12 table-responsive">
                            <b>Milk Dispatch Details </b>
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

                                    <asp:TemplateField HeaderText="Milk Quality" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblD_MilkQuality" runat="server" Text='<%# Eval("D_MilkQuality") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Milk Quantity (In Ltr)" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblD_MilkQuantity" runat="server" Text='<%# Eval("D_MilkQuantity_Ltr") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Milk Quantity (In Kg)" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblD_MilkQuantityKg" runat="server" Text='<%# Eval("NetMilkQtyInKG_KG") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="FAT %" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFAT" runat="server" Text='<%# Eval("Fat_Per") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="CLR" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("CLR_Avg") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="SNF %" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("Snf_Per") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Milk Dispatch Mode" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lvlV_MilkDispatchType" runat="server" Text='<%# Eval("V_MilkDispatchType") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Cans" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblI_TotalCans" runat="server" Text='<%# Eval("I_TotalCans") %>'></asp:Label>
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
