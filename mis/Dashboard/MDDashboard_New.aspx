<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MDDashboard_New.aspx.cs" Inherits="mis_Dashboard_MDDashboard_New" %>

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
            <h1>Dashboard 
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Dashboard</li>
            </ol>
        </section>

        <section class="content">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>

            <div class="row">
                <div class="col-md-12">

                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box bg-info" style="background: #93cbe6">
                            <div class="inner">
                                <h3>
                                    <asp:Label ID="lblds" runat="server"></asp:Label>
                                </h3>

                                <p>DS</p>
                            </div>
                            
                            <div class="icon">
                                <%--<i class="fa fa-home"></i>--%>
                                <img src="images/DS.png" style="height:70px;width:93px;" />
                            </div>
                            <asp:LinkButton ID="LinkButton1" ToolTip="Receive" class="small-box-footer" OnClick="LinkButton1_Click" runat="server"> 
                                            More info <i class="fas fa-arrow-circle-right"></i>
                            </asp:LinkButton>

                        </div>
                    </div>
                    <!-- ./col -->
                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box bg-success" style="background: #f19f9f">
                            <div class="inner">
                                <h3>
                                    <asp:Label ID="lblCC" runat="server"></asp:Label>
                                </h3>

                                <p>CC</p>
                            </div>
                            <div class="icon">
                                <%--<i class="fa fa-home"></i>--%>
                                <img src="images/CC.png" style="height:71px;width:105px;" />
                            </div>
                            <asp:LinkButton ID="LinkButton3" ToolTip="Receive" class="small-box-footer" OnClick="LinkButton3_Click" runat="server"> 
                                            More info <i class="fas fa-arrow-circle-right"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <!-- ./col -->
                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box bg-warning" style="background: #88da66">
                            <div class="inner">
                                <h3>

                                    <asp:Label ID="lblBMC" runat="server"></asp:Label>
                                </h3>
                                <p>BMC</p>
                            </div>
                            <div class="icon">
                                <%--<i class="fa fa-home"></i>--%>
                                <img src="images/BMC.png" style="height:65px;width:118px;" />
                            </div>
                            <asp:LinkButton ID="LinkButton4" ToolTip="Receive" class="small-box-footer" OnClick="LinkButton4_Click" runat="server"> 
                                            More info <i class="fas fa-arrow-circle-right"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <!-- ./col -->
                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box bg-danger" style="background: #f3db60">
                            <div class="inner">
                                <h3>
                                    <asp:Label ID="lblDCS" runat="server"></asp:Label>
                                </h3>

                                <p>DCS</p>
                            </div>
                            <div class="icon">
                                <%--<i class="fa fa-home"></i>--%>
                                <img src="images/DCS.jpg" style="height:58px;width:88px;" />
                            </div>
                            <asp:LinkButton ID="LinkButton6" ToolTip="Receive" class="small-box-footer" OnClick="LinkButton6_Click" runat="server"> 
                                            More info <i class="fas fa-arrow-circle-right"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <!-- ./col -->

                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box bg-danger" style="background: #0affb0de;">
                            <div class="inner">
                                <h3>
                                    <asp:Label ID="lblMDP" runat="server"></asp:Label>
                                </h3>

                                <p>MDP</p>
                            </div>
                            <div class="icon">
                                <%--<i class="fa fa-home"></i>--%>
                                <img src="images/MDP.png" style="height:79px;width:140px;" />
                            </div>
                            <asp:LinkButton ID="LinkButton9" ToolTip="Receive" class="small-box-footer" OnClick="LinkButton9_Click" runat="server"> 
                                            More info <i class="fas fa-arrow-circle-right"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <!-- ./col -->

                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box bg-danger" style="background: #0073b7;">
                            <div class="inner">
                                <h3>
                                    <asp:Label ID="lblProducer" runat="server"></asp:Label>
                                </h3>

                                <p>Reg. Producer</p>
                            </div>
                            <div class="icon">
                                <%--<i class="fa fa-home"></i>--%>
                                <img src="images/Producer.jpg" style="height:48px;width:93px;" />
                            </div>
                            <asp:LinkButton ID="LinkButton11" ToolTip="Receive" class="small-box-footer" OnClick="LinkButton11_Click" runat="server"> 
                                            More info <i class="fas fa-arrow-circle-right"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
					 <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box bg-danger" style="background: #00b7af;">
                            <div class="inner">
                                <h3>
                                    <asp:Label ID="lblUnRegProducer" Text="140890" runat="server"></asp:Label>
                                </h3>

                                <p>UnReg. Producer</p>
                            </div>
                            <div class="icon">
                                <%--<i class="fa fa-home"></i>--%>
                                <img src="images/Producer.jpg" style="height:48px;width:93px;" />
                            </div>
                            <asp:LinkButton ID="LnkUnRegProducer" ToolTip="Receive" class="small-box-footer" runat="server"> 
                                            More info <i class="fas fa-arrow-circle-right"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <!-- ./col -->
                      <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box bg-info" style="background: #cfa0d8">
                            <div class="inner">
                                <h3>
                                    <span id="fff">
                                        <%--  <asp:Label ID="lblSuperStockist" runat="server"></asp:Label></span>--%>
										 <asp:Label ID="lblSuperStockistDistributor" runat="server"></asp:Label></span>
                                </h3>

                                 <p>Distributor & SS</p>
                            </div>
                            <div class="icon">
                                 <img src="images/Distributor.png" style="height:58px;width:128px;" />
                                
                            </div>
                            <asp:LinkButton ID="lnkSSorDist" runat="server" data-toggle="modal" data-target="#SSORDistModel" class="small-box-footer">
                                    More info <i class="fas fa-arrow-circle-right"></i>
                            </asp:LinkButton>
                          <%-- <asp:LinkButton ID="lnkSuperStockist" OnClick="lnkSuperStockist_Click" ToolTip="Receive" class="small-box-footer" runat="server"> 
                                            More info <i class="fas fa-arrow-circle-right"></i>
                            </asp:LinkButton>--%>

                        </div>
                    </div>

                    <%--<div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box bg-info" style="background: #00c0ef">
                            <div class="inner">
                                <h3>
                                    <span id="fff">
                                        <asp:Label ID="lbltotalmilkcollection" runat="server"></asp:Label></span>
                                </h3>

                                <p>Daily Milk Collection [In Kg / day]</p>
                            </div>
                            <div class="icon">
                                <i class="fa fa-home"></i>
                                
                            </div>
                             <asp:LinkButton ID="LinkButton18" ToolTip="Receive" class="small-box-footer" OnClick="LinkButton18_Click" runat="server"> 
                                            More info <i class="fas fa-arrow-circle-right"></i>
                            </asp:LinkButton>

                        </div>
                    </div>--%>
                    <!-- ./col -->

                     <%--<div class="col-lg-3 col-6">
                        <div class="small-box bg-info" style="background: #93cbe6">
                            <div class="inner">
                                <h3>
                                    <span id="fff">
                                        <asp:Label ID="lblDistributor" runat="server"></asp:Label></span>
                                </h3>

                                <p>Distributor</p>
                            </div>
                            <div class="icon">
                        
                                <img src="images/Distributor.png" style="height:58px;width:128px;" />
                            </div>

                            <asp:LinkButton ID="lbDistributor" OnClick="lbDistributor_Click" ToolTip="Receive" class="small-box-footer" runat="server"> 
                                            More info <i class="fas fa-arrow-circle-right"></i>
                            </asp:LinkButton>

                        </div>
                    </div>--%>
                    <!-- ./col -->

                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box bg-info" style="background: #d2a176">
                            <div class="inner">
                                <h3>
                                    <span id="fff">
                                        <asp:Label ID="lblParlour" runat="server"></asp:Label></span>
                                </h3>

                                <p>Parlour </p>
                            </div>
                            <div class="icon">
                            <img src="images/Parlour.png" style="height:69px;width:127px;" />
                                <%--<i class="fa fa-home"></i>--%>
                            </div>
                            <asp:LinkButton ID="lbParlour" OnClick="lbParlour_Click" ToolTip="Receive" class="small-box-footer" runat="server"> 
                                            More info <i class="fas fa-arrow-circle-right"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <!-- ./col -->

                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box bg-info" style="background: #f3db60">
                            <div class="inner">
                                <h3>
                                    <span id="fff">
                                        <asp:Label ID="lblRTI" runat="server"></asp:Label></span>
                                </h3>

                                <p>RTI</p>
                            </div>
                            <div class="icon">
                                <%--<i class="fa fa-home"></i>--%>
                                <img src="images/RTI.PNG" style="height:52px;width:104px;" />
                            </div>
                            <asp:LinkButton ID="lbRTI" OnClick="lbRTI_Click" ToolTip="Receive" class="small-box-footer" runat="server"> 
                                            More info <i class="fas fa-arrow-circle-right"></i>
                            </asp:LinkButton>
                        </div>
                    </div>

                    <!-- ./col -->

                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box bg-info" style="background: #819877">
                            <div class="inner">
                                <h3>
                                    <span id="fff">
                                        <asp:Label ID="lblLegalCases" runat="server"></asp:Label></span>
                                </h3>

                                <p>Legal Cases</p>
                            </div>
                            <div class="icon">
                                <%--<i class="fa fa-home"></i>--%>
                                <img src="images/LegalCases.png" style="height:58px;width:102px;" />
                            </div>
                            <asp:LinkButton ID="lbLegalCases" OnClick="lbLegalCases_Click" ToolTip="Receive" class="small-box-footer" runat="server"> 
                                            More info <i class="fas fa-arrow-circle-right"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <!-- ./col -->




                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box bg-info" style="background: #00c0ef">
                            <div class="inner">
                                <h3>
                                    <span id="fff">
                                        <asp:Label ID="lblGrievance" runat="server"></asp:Label></span>
                                </h3>

                                <p>Grievance</p>
                            </div>
                            <div class="icon">
                                <%--<i class="fa fa-home"></i>--%>
                                <img src="images/Grievance.png" style="height:66px;width:115px;" />
                            </div>
                            <asp:LinkButton ID="lbGrv" OnClick="lbGrv_Click" ToolTip="Receive" class="small-box-footer" runat="server"> 
                                            More info <i class="fas fa-arrow-circle-right"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <!-- ./col -->

                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box bg-info" style="background: #93cbe6">
                            <div class="inner">
                                <h3>
                                    <span id="DDD">
                                        <asp:Label ID="lblemployee" runat="server"></asp:Label></span>
                                </h3>

                                <p>Our Strength</p>
                            </div>
                            <div class="icon">
                                <%--<i class="fa fa-home"></i>--%>
                                <img src="images/OurStrength.png" style="height:64px;width:119px;" />
                            </div>
                            <asp:LinkButton ID="lbempcount" OnClick="lbempcount_Click" ToolTip="Receive" class="small-box-footer" runat="server"> 
                                            More info <i class="fas fa-arrow-circle-right"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <!-- ./col -->

                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box bg-info" style="background: #ff0a21de">
                            <div class="inner">
                                <h3>
                                    <span id="fff">  <asp:Label ID="lblMilkVariantCount" runat="server"></asp:Label></span>
                                </h3>

                                <p>Milk Variant</p>
                            </div>
                            <div class="icon">
                                <%--<i class="fa fa-home"></i>--%>
                                <img src="images/MilkVariant.png" style="height:69px;width:85px;" />
                            </div>
                           <asp:LinkButton ID="lnkMilkVariant" OnClick="lnkMilkVariant_Click" ToolTip="Receive" class="small-box-footer" runat="server"> 
                                            More info <i class="fas fa-arrow-circle-right"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <!-- ./col -->

<%--                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box bg-info" style="background: #0073b7">
                            <div class="inner">
                                <h3>
                                    <span id="fff">--</span>
                                </h3>

                                <p>Product</p>
                            </div>
                            <div class="icon">
                                <i class="fa fa-home"></i>
                            </div>
                            <a class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                  

                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box bg-info" style="background: #93cbe6">
                            <div class="inner">
                                <h3>
                                    <span id="fff">--</span>
                                </h3>

                                <p>Board Agenda</p>
                            </div>
                            <div class="icon">
                                <i class="fa fa-home"></i>
                            </div>
                            <a class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                   

                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box bg-info" style="background: #f3db60">
                            <div class="inner">
                                <h3>
                                    <span id="fff">--</span>
                                </h3>

                                <p>Distributor Sale</p>
                            </div>
                            <div class="icon">
                                <i class="fa fa-home"></i>
                            </div>
                            <a class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                  

                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box bg-info" style="background: #88da66">
                            <div class="inner">
                                <h3>
                                    <span id="fff">-- </span>
                                </h3>

                                <p>Turnover(in Cr)</p>
                            </div>
                            <div class="icon">
                                <i class="fa fa-home"></i>
                            </div>
                            <a class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                        </div>
                    </div>--%>
                   

                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box bg-info" style="background: #f19f9f">
                            <div class="inner">
                                <h3>
                                    <span id="fff"><asp:Label ID="lblProductVariantCount" runat="server"></asp:Label></span>
                                </h3>

                                <p>Product Variant</p>
                            </div>
                            <div class="icon">
                                <%--<i class="fa fa-home"></i>--%>
                                <img src="images/ProductVariant.png" style="height:84px;width:89px;" />
                            </div>
                           <asp:LinkButton ID="lnkProductVariant" OnClick="lnkProductVariant_Click" ToolTip="Receive" class="small-box-footer" runat="server"> 
                                            More info <i class="fas fa-arrow-circle-right"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <!-- ./col -->


                </div>
            </div>


            <div class="modal" id="DSModel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 520px;">
                        <div class="modal-header">
                            <asp:LinkButton runat="server" class="close" ID="btnCrossButton" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>

                            <h4 class="modal-title">DS DETAILS</h4>
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
                                                            <h3 class="box-title">DS DETAILS</h3>
                                                        </div>
                                                        <!-- /.box-header -->
                                                        <div class="box-body">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <asp:Label ID="lblPopupMsg" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="table-responsive">

                                                                <asp:GridView ID="GrdDS" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                    EmptyDataText="No Record Found.">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sr.No.">
                                                                            <ItemTemplate>
                                                                                <%#Container.DataItemIndex+1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="DS Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("DSNAME") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Address">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Office_Address") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                </asp:GridView>

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

            <div class="modal" id="CCModel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 520px;">
                        <div class="modal-header">
                            <asp:LinkButton runat="server" class="close" ID="LinkButton2" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>

                            <h4 class="modal-title">CC DETAILS</h4>
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
                                                            <h3 class="box-title">CC DETAILS</h3>
                                                        </div>
                                                        <!-- /.box-header -->
                                                        <div class="box-body">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="table-responsive">
                                                                <asp:GridView ID="GVCC" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                    EmptyDataText="No Record Found." ShowFooter="true">
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
                                                                            <FooterTemplate>
                                                                                <asp:Label Font-Bold="true" ID="lblDTotal" Text="Total" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="CC COUNT">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblcount" runat="server" Text='<%# Eval("counts") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label Font-Bold="true" ID="lbltotalcount" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                </asp:GridView>
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

            <div class="modal" id="BMCModel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 520px;">
                        <div class="modal-header">
                            <asp:LinkButton runat="server" class="close" ID="LinkButton5" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>

                            <h4 class="modal-title">BMC DETAILS</h4>
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
                                                            <h3 class="box-title">BMC DETAILS</h3>
                                                        </div>
                                                        <!-- /.box-header -->
                                                        <div class="box-body">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <asp:Label ID="Label2" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="table-responsive">

                                                                <asp:GridView ID="GVBMC" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                    EmptyDataText="No Record Found." ShowFooter="true">
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
                                                                            <FooterTemplate>
                                                                                <asp:Label Font-Bold="true" ID="lblDTotal" Text="Total" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="BMC COUNT">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblcount" runat="server" Text='<%# Eval("counts") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label Font-Bold="true" ID="lbltotalcount" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                </asp:GridView>

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

            <div class="modal" id="DCSModel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 520px;">
                        <div class="modal-header">
                            <asp:LinkButton runat="server" class="close" ID="LinkButton7" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>

                            <h4 class="modal-title">DCS DETAILS</h4>
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
                                                            <h3 class="box-title">DCS DETAILS</h3>
                                                        </div>
                                                        <!-- /.box-header -->
                                                        <div class="box-body">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <asp:Label ID="Label3" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="table-responsive">

                                                                <asp:GridView ID="GVDCS" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                    EmptyDataText="No Record Found." ShowFooter="true">
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
                                                                            <FooterTemplate>
                                                                                <asp:Label Font-Bold="true" ID="lblDTotal" Text="Total" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="DCS COUNT">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblcount" runat="server" Text='<%# Eval("counts") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label Font-Bold="true" ID="lbltotalcount" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                </asp:GridView>

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

            <div class="modal" id="MDPModel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 520px;">
                        <div class="modal-header">
                            <asp:LinkButton runat="server" class="close" ID="LinkButton8" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>

                            <h4 class="modal-title">MDP DETAILS</h4>
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
                                                            <h3 class="box-title">MDP DETAILS</h3>
                                                        </div>
                                                        <!-- /.box-header -->
                                                        <div class="box-body">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <asp:Label ID="Label4" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="table-responsive">

                                                                <asp:GridView ID="GVMDP" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                    EmptyDataText="No Record Found." ShowFooter="true">
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
                                                                            <FooterTemplate>
                                                                                <asp:Label Font-Bold="true" ID="lblDTotal" Text="Total" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="MDP COUNT">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblcount" runat="server" Text='<%# Eval("counts") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label Font-Bold="true" ID="lbltotalcount" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                </asp:GridView>


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

            <div class="modal" id="PModel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 520px;">
                        <div class="modal-header">
                            <asp:LinkButton runat="server" class="close" ID="LinkButton10" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>

                            <h4 class="modal-title">PRODUCER DETAILS</h4>
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
                                                            <h3 class="box-title">PRODUCER DETAILS</h3>
                                                        </div>
                                                        <!-- /.box-header -->
                                                        <div class="box-body">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <asp:Label ID="Label5" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="table-responsive">

                                                                <asp:GridView ID="GVPC" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                    EmptyDataText="No Record Found." ShowFooter="true">
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
                                                                            <FooterTemplate>
                                                                                <asp:Label Font-Bold="true" ID="lblDTotal" Text="Total" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="PRODUCER COUNT">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblcount" runat="server" Text='<%# Eval("counts") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label Font-Bold="true" ID="lbltotalcount" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                </asp:GridView>

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

            <div class="modal" id="DstModel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 520px;">
                        <div class="modal-header">
                            <asp:LinkButton runat="server" class="close" ID="LinkButton15" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>

                            <h4 class="modal-title">Distributor Details</h4>
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
                                                            <h3 class="box-title">Distributor Details</h3>
                                                        </div>
                                                        <!-- /.box-header -->
                                                        <div class="box-body">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <asp:Label ID="Label7" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="table-responsive">

                                                                <asp:GridView ID="GVDSIT" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                    EmptyDataText="No Record Found." ShowFooter="true">
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
                                                                            <FooterTemplate>
                                                                                <asp:Label Font-Bold="true" ID="lblDTotal" Text="Total" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Distributor COUNT">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblcount" runat="server" Text='<%# Eval("counts") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label Font-Bold="true" ID="lbltotalcount" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                </asp:GridView>

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

            <div class="modal" id="ParlourModel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 520px;">
                        <div class="modal-header">
                            <asp:LinkButton runat="server" class="close" ID="LinkButton12" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>

                            <h4 class="modal-title">Parlour Details</h4>
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
                                                            <h3 class="box-title">Parlour Details</h3>
                                                        </div>
                                                        <!-- /.box-header -->
                                                        <div class="box-body">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <asp:Label ID="Label6" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="table-responsive">

                                                                <asp:GridView ID="GVParlour" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                    EmptyDataText="No Record Found." ShowFooter="true">
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
                                                                            <FooterTemplate>
                                                                                <asp:Label Font-Bold="true" ID="lblDTotal" Text="Total" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Parlour COUNT">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblcount" runat="server" Text='<%# Eval("counts") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label Font-Bold="true" ID="lbltotalcount" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                </asp:GridView>

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

            <div class="modal" id="RTIModel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 380px;">
                        <div class="modal-header">
                            <asp:LinkButton runat="server" class="close" ID="LinkButton13" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>

                            <h4 class="modal-title">RTI Details</h4>
                        </div>
                        <div class="modal-body">

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">

                                        <asp:GridView ID="GVRTI" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                            EmptyDataText="No Record Found." ShowFooter="true">
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
                                                    <FooterTemplate>
                                                        <asp:Label Font-Bold="true" ID="lblDTotal" Text="Total" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="RTI COUNT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcount" runat="server" Text='<%# Eval("counts") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label Font-Bold="true" ID="lbltotalcount" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>


                                        <!-- /.box-body -->

                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>

            <div class="modal" id="LegalCasesModel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 410px;">
                        <div class="modal-header">
                            <asp:LinkButton runat="server" class="close" ID="LinkButton14" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>

                            <h4 class="modal-title">Legal Cases Details</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="box-body">

                                        <div class="table-responsive">

                                            <asp:GridView ID="gvLegalCases" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                EmptyDataText="No Record Found." ShowFooter="true">
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
                                                        <FooterTemplate>
                                                            <asp:Label Font-Bold="true" ID="lblDTotal" Text="Total" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Legal Cases COUNT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcount" runat="server" Text='<%# Eval("counts") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label Font-Bold="true" ID="lbltotalcount" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>

                                        </div>

                                    </div>
                                </div>
                                <!-- /.box-body -->
                            </div>
                        </div>


                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>

            <div class="modal" id="GrievanceModel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 410px;">
                        <div class="modal-header">
                            <asp:LinkButton runat="server" class="close" ID="LinkButton16" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>

                            <h4 class="modal-title">Grievance Details</h4>
                        </div>
                        <div class="modal-body">

                            <div class="row">
                                <div class="col-md-12">

                                    <div class="box-body">

                                        <div class="table-responsive">

                                            <asp:GridView ID="GVGrievance" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                EmptyDataText="No Record Found." ShowFooter="true">
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
                                                        <FooterTemplate>
                                                            <asp:Label Font-Bold="true" ID="lblDTotal" Text="Total" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Grievance COUNT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcount" runat="server" Text='<%# Eval("counts") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label Font-Bold="true" ID="lbltotalcount" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>

                                        </div>

                                    </div>

                                </div>
                                <!-- /.box-body -->


                            </div>
                        </div>

                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>


            <div class="modal" id="EMPModel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 410px;">
                        <div class="modal-header">
                            <asp:LinkButton runat="server" class="close" ID="LinkButton17" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>

                            <h4 class="modal-title">Employee Details</h4>
                        </div>
                        <div class="modal-body">

                            <div class="row">
                                <div class="col-md-12">

                                    <div class="box-body">

                                        <div class="table-responsive">

                                            <asp:GridView ID="GVEMP" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                EmptyDataText="No Record Found." ShowFooter="true">
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
                                                        <FooterTemplate>
                                                            <asp:Label Font-Bold="true" ID="lblDTotal" Text="Total" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Employee COUNT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcount" runat="server" Text='<%# Eval("counts") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label Font-Bold="true" ID="lbltotalcount" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>

                                        </div>

                                    </div>

                                </div>
                                <!-- /.box-body -->
                            </div>
                        </div>

                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
			
			
			
			
			<%--<div class="modal" id="MilkCollectionModel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 410px;">
                        <div class="modal-header">
                            <asp:LinkButton runat="server" class="close" ID="LinkButton19" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>

                            <h4 class="modal-title">Milk Collection Details</h4>
                        </div>
                        <div class="modal-body">

                            <div class="row">
                                <div class="col-md-12">

                                    <div class="box-body">

                                        <div class="table-responsive">

                                            <asp:GridView ID="gvMilkCollectionDetails" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                EmptyDataText="No Record Found." ShowFooter="true">
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
                                                        <FooterTemplate>
                                                            <asp:Label Font-Bold="true" ID="lblDTotal" Text="Total" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Milk Collection (In Kg)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcount" runat="server" Text='<%# Eval("MilkInKg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label Font-Bold="true" ID="lbltotalMilkInKg" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>

                                        </div>

                                    </div>

                                </div>
                                <!-- /.box-body -->
                            </div>
                        </div>

                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>--%>
			
			
			 <div class="modal" id="SSModel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 520px;">
                        <div class="modal-header">
                            <asp:LinkButton runat="server" class="close" ID="LinkButton20" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>

                            <h4 class="modal-title">SuperStockist Details</h4>
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
                                                            <h3 class="box-title">SuperStockist Details</h3>
                                                        </div>
                                                        <!-- /.box-header -->
                                                        <div class="box-body">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <asp:Label ID="Label8" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="table-responsive">

                                                                <asp:GridView ID="gvSS" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                    EmptyDataText="No Record Found." ShowFooter="true">
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
                                                                            <FooterTemplate>
                                                                                <asp:Label Font-Bold="true" ID="lblDTotal" Text="Total" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="SuperStockist COUNT">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblcount" runat="server" Text='<%# Eval("counts") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label Font-Bold="true" ID="lbltotalcount" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                </asp:GridView>

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

            <div class="modal" id="MilkVariantModel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 520px;">
                        <div class="modal-header">
                            <asp:LinkButton runat="server" class="close" ID="LinkButton18" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>

                            <h4 class="modal-title">Milk Variant Details</h4>
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
                                                            <h3 class="box-title">Milk Variant Details</h3>
                                                        </div>
                                                        <!-- /.box-header -->
                                                        <div class="box-body">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <asp:Label ID="Label9" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="table-responsive">

                                                                <asp:GridView ID="gvMilkVaraiant" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                    EmptyDataText="No Record Found." ShowFooter="true">
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
                                                                            <FooterTemplate>
                                                                                <asp:Label Font-Bold="true" ID="lblDTotal" Text="Total" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Item Count">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblcount" runat="server" Text='<%# Eval("ItemCount") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label Font-Bold="true" ID="lbltotalcount" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                          <asp:TemplateField HeaderText="Item Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblIName" runat="server" Text='<%# Eval("IName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>

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

            <div class="modal" id="ProductVariantModel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 520px;">
                        <div class="modal-header">
                            <asp:LinkButton runat="server" class="close" ID="LinkButton19" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>

                            <h4 class="modal-title">Product Variant Details</h4>
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
                                                            <h3 class="box-title">Product Variant Details</h3>
                                                        </div>
                                                        <!-- /.box-header -->
                                                        <div class="box-body">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <asp:Label ID="Label10" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="table-responsive">

                                                                <asp:GridView ID="gvProduct" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                    EmptyDataText="No Record Found." ShowFooter="true">
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
                                                                            <FooterTemplate>
                                                                                <asp:Label Font-Bold="true" ID="lblDTotal" Text="Total" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Item Count">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblcount" runat="server" Text='<%# Eval("ItemCount") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label Font-Bold="true" ID="lbltotalcount" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                          <asp:TemplateField HeaderText="Item Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblIName" runat="server" Text='<%# Eval("IName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>

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
			<div class="modal" id="SSORDistModel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 520px;">
                        <div class="modal-header">
                           <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                        </button>

                            <h4 class="modal-title">SuperStockist and Distributor Details</h4>
                        </div>
                        <div class="modal-body">

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row" style="height: 400px; overflow: scroll;">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <section class="content">
                                                    <div class="box box-Manish" style="min-height: 300px;">
                                                        <div class="box-header">
                                                            <h3 class="box-title">SuperStockist and Distributor Details</h3>
                                                        </div>
                                                        <!-- /.box-header -->
                                                        <div class="box-body">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <asp:Label ID="Label88" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="table-responsive">

                                                                <table class="datatable table table-striped table-bordered table-hover">
                                                                    <tr>
                                                                        <th>S.No</th>
                                                                         <th>DS Name</th>
                                                                         <th>SuperStockist Count</th>
                                                                         <th>Distributor Count</th>
                                                                      
                                                                    </tr>
                                                                    <tr>
                                                                        <td>1</td>
                                                                         <td>भोपाल सहकारी दुग्ध संघ मर्यादित</td>
                                                                         <td>0</td>
                                                                         <td>261</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>2</td>
                                                                         <td>ग्वालियर सहकारी दुग्ध संघ मर्यादित</td>
                                                                         <td>2</td>
                                                                         <td>20</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>3</td>
                                                                         <td>इंदौर सहकारी दुग्ध संघ मर्यादित</td>
                                                                         <td>5</td>
                                                                         <td>30</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>4</td>
                                                                         <td>जबलपुर सहकारी दुग्ध संघ मर्यादित</td>
                                                                         <td>1</td>
                                                                         <td>22</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>5</td>
                                                                         <td>उज्जैन सहकारी दुग्ध संघ मर्यादित</td>
                                                                         <td>2</td>
                                                                         <td>32</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>6</td>
                                                                         <td>बुंदेलखंड सहकारी दुग्ध संघ मर्यादित</td>
                                                                         <td>1</td>
                                                                         <td>64</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td></td>
                                                                         <td><b>Total</b></td>
                                                                        <td><b>11</b></td>
                                                                         <td><b>429</b></td>
                                                                    </tr>
                                                                     <tr>
                                                                        <td></td>
                                                                         <td><b>Grant Total</b></td>
                                                                       
                                                                         <td colspan="2" style="text-align:center"><b>440</b></td>
                                                                    </tr>
                                                                </table>

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
    <script src="assets/js/Dashboard.js"></script>
    <script>
        function DSModelF() {
            $("#DSModel").modal('show');

        }

        function CCModelF() {
            $("#CCModel").modal('show');

        }

        function BMCModelF() {
            $("#BMCModel").modal('show');

        }

        function DCSModelF() {
            $("#DCSModel").modal('show');

        }

        function MDPModelF() {
            $("#MDPModel").modal('show');

        }

        function PModelF() {
            $("#PModel").modal('show');

        }


       //function DstModelF() {
        //    $("#DstModel").modal('show');

        //}

        function ParlourModelF() {
            $("#ParlourModel").modal('show');

        }

        function RTIModelF() {
            $("#RTIModel").modal('show');

        }

        function LegalCasesModelF() {
            $("#LegalCasesModel").modal('show');

        }

        function GrievanceModelF() {
            $("#GrievanceModel").modal('show');

        }

        function EMPModelF() {
            $("#EMPModel").modal('show');

        }

       function MilkCollectionModel() {
            $("#MilkCollectionModel").modal('show');

       }
       //function SSModel() {
       //    $("#SSModel").modal('show');

       //}
       function MilkVariant() {
           $("#MilkVariantModel").modal('show');

       }
       function ProductVariant() {
           $("#ProductVariantModel").modal('show');

       }

    </script>
</asp:Content>
