<%@ Page Language="C#" AutoEventWireup="true" Culture="en-IN" CodeFile="View_Invoice_DcsLocalSale.aspx.cs" Inherits="mis_MilkCollection_View_Invoice_DcsLocalSale" %>

<!DOCTYPE html>
 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Local Sale Bill</title>
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
            <div style="width: 70%; margin: auto; border-radius: 5px; border: 1px solid #b5b5b5; padding: 5px">
                <asp:Button runat="server" Text="Back" CssClass="btn btn-success no-print" Style="position: fixed; z-index: 999999" OnClick="Unnamed_Click" />
                <!-- Main content -->
                <section class="invoice">
                    <!-- title row -->
                    <div class="row">
                        <div class="col-xs-12" style="border-bottom: 1px solid #eee;">
                            <div class="col-xs-3 col-md-3">
                                <%-- <img src="../../images/bds_logo.png" /><img src="../../images/Logo/logo_dcs.jpg" width="300" />--%>
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
                                    <p style="font-size: 20px;"><b>Invoice</b></p>
                                </h2>
                            </div>
                            <div class="col-xs-4 col-md-4"></div>
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                        <!-- /.col -->
                    </div>
                    <!-- info row -->
                    <div class="row invoice-info">
                        <div class="col-sm-4 invoice-col">

                            <address>
                                <br />
                                <b>Date</b>
                                <asp:Label ID="lblInvDT" runat="server"></asp:Label>
                                <br />

                            </address>
                        </div>
                        <!-- /.col -->
                        <div class="col-sm-4 invoice-col">
                        </div>
                        <!-- /.col -->
                        <div class="col-sm-4 invoice-col">

                            <br />

                            <b>Invoice No:&nbsp;
                                <asp:Label ID="lblInvoiceno" runat="server"></asp:Label></b><br />
                        </div>
                        <!-- /.col -->
                    </div>
                    <!-- /.row -->

                    <!-- Table row -->
                    <div class="row">
                        <div class="col-xs-12 table-responsive">
                            <b>खरीदार का नाम </b>-
                            <asp:Label ID="lblProducerName" runat="server"></asp:Label>
                            <br />
                            <br />
                            <asp:GridView ID="gv_localsaleINV" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover"
                                EmptyDataText="No Record Found." ShowFooter="true">
                                <Columns>

                                    <asp:TemplateField HeaderText="Sr. No." HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Item Name" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Item Qty" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblI_Quantity" runat="server" Text='<%# Eval("I_Quantity") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit Name" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="bllUnitName" runat="server" Text='<%# Eval("UnitName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                    </asp:TemplateField>
                                     

                                    <asp:TemplateField HeaderText="MRP" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSaleRate" runat="server" Text='<%# Eval("MRP") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblNetReceivedBagQty" runat="server" Text="Net Amount" Font-Bold="true"></asp:Label>
                                        </FooterTemplate> 
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Rupees" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNetAmount" runat="server" Text='<%# Eval("NetAmount") %>'></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                        <FooterTemplate>
                                            <asp:Label ID="lbltotalamount" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>

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
                            <asp:Label ID="lblInword" Font-Bold="true" runat="server"></asp:Label>
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
