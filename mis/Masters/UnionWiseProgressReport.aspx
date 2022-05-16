<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="UnionWiseProgressReport.aspx.cs" Inherits="mis_Dashboard_UnionWiseProgressReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="assets/css/Dashboard.css" rel="stylesheet" />
    <style>
        .box {
            min-height: 100px;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">

        <section class="content-header">
            <h1>Union Wise Progress Report 
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Union Wise Progress Report</li>
            </ol>
        </section>

        <section class="content">
            <asp:label id="lblMsg" runat="server" text="" forecolor="Red"></asp:label>

            <div class="row">
                <div class="col-md-12">

                    <div class="col-lg-4 col-6">
                        <!-- small box -->
                        <div class="small-box bg-info" style="background: #93cbe6">
                            <div class="inner">
                                <h3>
                                    <asp:label id="lblPO" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:label>
                                </h3>

                                <p>Plant Operation</p>
                            </div>
                            <div class="icon">
                                <i class="fa fa-home"></i>
                            </div>
                            <asp:linkbutton id="lnkbtnPlantOperation" tooltip="Receive" class="small-box-footer" OnClick="lnkbtnPlantOperation_Click"  runat="server"> 
                                            More info <i class="fa fa-arrow-circle-right"></i>
                            </asp:linkbutton>

                        </div>
                    </div>
                    <!-- ./col -->
                    <div class="col-lg-4 col-6">
                        <!-- small box -->
                        <div class="small-box bg-success" style="background: #f19f9f">
                            <div class="inner">
                                <h3>
                                    <asp:label id="lblMCMS" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:label>
                                </h3>

                                <p>MCMS(E - Challan)<br /></p>
                            </div>
                            <div class="icon">
                                <i class="fa fa-home"></i>
                            </div>
                            <asp:linkbutton id="lnkbtnMCMS" tooltip="Receive" class="small-box-footer" OnClick="lnkbtnMCMS_Click" runat="server"> 
                                            More info <i class="fa fa-arrow-circle-right"></i>
                            </asp:linkbutton>
                        </div>
                    </div>
                    <!-- ./col -->
                    <div class="col-lg-4 col-6">
                        <!-- small box -->
                        <div class="small-box bg-warning" style="background: #88da66">
                            <div class="inner">
                                <h3>

                                    <asp:label id="lblMarketing" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:label>
                                </h3>
                                <p>Marketing<br /></p>
                            </div>
                            <div class="icon">
                                <i class="fa fa-home"></i>
                            </div>
                            <asp:linkbutton id="lnkbtnMarketing" tooltip="Receive" class="small-box-footer" OnClick="lnkbtnMarketing_Click" runat="server"> 
                                            More info <i class="fa fa-arrow-circle-right"></i>
                            </asp:linkbutton>
                        </div>
                    </div>
                    <!-- ./col -->
                    <div class="col-lg-4 col-6">
                        <!-- small box -->
                        <div class="small-box bg-danger" style="background: #f3db60">
                            <div class="inner">
                                <h3>
                                    <asp:label id="lblFinance" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:label>
                                </h3>

                                <p>Finance<br /></p>
                            </div>
                            <div class="icon">
                                <i class="fa fa-home"></i>
                            </div>
                            <asp:linkbutton id="lnkbtnFinance" tooltip="Receive" class="small-box-footer" OnClick="lnkbtnFinance_Click"  runat="server"> 
                                            More info <i class="fa fa-arrow-circle-right"></i>
                            </asp:linkbutton>
                        </div>
                    </div>
                    <!-- ./col -->

                    <div class="col-lg-4 col-6">
                        <!-- small box -->
                        <div class="small-box bg-danger" style="background: #ff0a21de;">
                            <div class="inner">
                                <h3>
                                    <asp:label id="lblFO" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:label>
                                </h3>

                                <p>Field Operation(Society)</p>
                            </div>
                            <div class="icon">
                                <i class="fa fa-home"></i>
                            </div>
                            <asp:linkbutton id="lnkbtnFO_Society" tooltip="Receive" class="small-box-footer" OnClick="lnkbtnFO_Society_Click" runat="server"> 
                                            More info <i class="fa fa-arrow-circle-right"></i>
                            </asp:linkbutton>
                        </div>
                    </div>
                    <!-- ./col -->

                    <div class="col-lg-4 col-6">
                        <!-- small box -->
                        <div class="small-box bg-danger" style="background: #0073b7;">
                            <div class="inner">
                                <h3>
                                    <asp:label id="lblFO_RMRD" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:label>
                                </h3>

                                <p>Field Operation(RMRD)</p>
                            </div>
                            <div class="icon">
                                <i class="fa fa-home"></i>
                            </div>
                            <asp:linkbutton id="lnkbtnFO_RMRD" tooltip="Receive" class="small-box-footer" OnClick="lnkbtnFO_RMRD_Click" runat="server"> 
                                            More info <i class="fa fa-arrow-circle-right"></i>
                            </asp:linkbutton>
                        </div>
                    </div>
                    <!-- ./col -->


                    <div class="col-lg-4 col-6">
                        <!-- small box -->
                        <div class="small-box bg-info" style="background: #00c0ef">
                            <div class="inner">
                                <h3>
                                    <span id="fff">
                                        <asp:label id="lblPayroll" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:label>
                                    </span>
                                </h3>

                                <p>PayRoll</p>
                            </div>
                            <div class="icon">
                                <i class="fa fa-home"></i>
                            </div>
                            <asp:linkbutton id="lnkbtnPayroll" tooltip="Receive" class="small-box-footer" OnClick="lnkbtnPayroll_Click" runat="server"> 
                                            More info <i class="fa fa-arrow-circle-right"></i>
                            </asp:linkbutton>

                        </div>
                    </div>
                    <!-- ./col -->

                    <div class="col-lg-4 col-6">
                        <!-- small box -->
                        <div class="small-box bg-info" style="background: #93cbe6">
                            <div class="inner">
                                <h3>
                                    <span id="fff">
                                        <asp:label id="lblDistributor" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:label>
                                    </span>
                                </h3>

                                <p>Inventory</p>
                            </div>
                            <div class="icon">
                                <i class="fa fa-home"></i>
                            </div>

                            <asp:linkbutton id="lbInventory"  tooltip="Receive" class="small-box-footer" OnClick="lbInventory_Click"  runat="server"> 
                                            More info <i class="fa fa-arrow-circle-right"></i>
                            </asp:linkbutton>

                        </div>
                    </div>
                    <!-- ./col -->
                    
                </div>
            </div>


            <div class="modal" id="DSModel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 520px;">
                        <div class="modal-header">
                            <asp:linkbutton runat="server" class="close" id="btnCrossButton"><span aria-hidden="true">×</span></asp:linkbutton>

                            <h4 class="modal-title">Union Wise Progress Report</h4>
                        </div>
                        <div class="modal-body">

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row" style="height: 350px; overflow: scroll;">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <section class="content">
                                                    <div class="box box-Manish" style="min-height: 300px;">
                                                        <div class="box-header">
                                                            <h3 class="box-title"><span id="spnHeader" runat="server"></span></h3>
                                                        </div>
                                                        <!-- /.box-header -->
                                                        <div class="box-body">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <asp:label id="lblPopupMsg" runat="server"></asp:label>
                                                                </div>
                                                            </div>
                                                            <div class="table-responsive">

                                                                <asp:gridview id="GrdDS" runat="server" showheaderwhenempty="true" autogeneratecolumns="false" cssclass="datatable table table-striped table-bordered table-hover"
                                                                    emptydatatext="No Record Found.">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sr.No.">
                                                                            <ItemTemplate>
                                                                                <%#Container.DataItemIndex+1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="DS Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Last Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblLastDate" runat="server" Text='<%# Eval("LastDate") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                </asp:gridview>

                                                            </div>

                                                        </div>

                                                    </div>
                                                    <!-- /.box-body -->

                                                </section>
                                                <!-- /.content -->
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>

        </section>

    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <%--<script src="assets/js/Dashboard.js"></script>
    <script src="../../js/jquery-2.2.3.min.js"></script>--%>
    <script>
        function DSModelF() {
            $("#DSModel").modal('show');

        }
       

    </script>
</asp:Content>
