<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DashboardNew.aspx.cs" Inherits="mis_Dashboard_DashboardNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">


    <link href="../../mis/Dashboard/Dashboard1/fontawesome-free/css/all.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700" rel="stylesheet" />

    <link href="../css/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <style>
        .bg-info {
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
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            <div class="container-fluid">

                <%--<div class="row">
                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box bg-info">
                            <div class="inner">
                                <h3>
                                    <asp:Label ID="lblDCS" runat="server" Text=""></asp:Label>
                                </h3>

                                <p>DCS</p>
                            </div>
                            <div class="icon">
                                <i class="fa fa-home"></i>
                            </div>       
                            <a id="HL_DCS" runat="server" target="_blank" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                    <!-- ./col -->
                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box bg-success">
                            <div class="inner">
                                <h3>
                                    <asp:Label ID="lblBMC" runat="server" Text=""></asp:Label>
                                </h3>

                                <p>BMC</p>
                            </div>
                            <div class="icon">
                                <i class="ion ion-stats-bars"></i>
                            </div>
                            <a id="HL_BMC" runat="server" target="_blank" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                    <!-- ./col -->
                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box bg-warning">
                            <div class="inner">
                                <h3>
                                    <asp:Label ID="lblMDP" runat="server" Text=""></asp:Label>
                                </h3>

                                <p>MDP</p>
                            </div>
                            <div class="icon">
                                <i class="fa fa-home"></i>
                            </div>
                            <a id="HL_MDP" runat="server" target="_blank" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                    <!-- ./col -->
                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box bg-danger">
                            <div class="inner">
                                <h3>
                                    <asp:Label ID="lblCC" runat="server" Text=""></asp:Label>
                                </h3>

                                <p>CC</p>
                            </div>
                            <div class="icon">
                                <i class="fa fa-home"></i>
                            </div>
                            <a id="HL_CC" runat="server" target="_blank" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                    <!-- ./col -->
                </div>
                <div class="row">
                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box bg-fuchsia">
                            <div class="inner">
                                <h3>
                                    <asp:Label ID="lblProducer" runat="server" Text=""></asp:Label>
                                </h3>

                                <p>Producer</p>
                            </div>
                            <div class="icon">
                                <i class="ion ion-person-add"></i>
                            </div>
                            <a id="HL_Producer" href="DS_ProducerDetail.aspx"  target="_blank" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                    <!-- ./col -->
                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box bg-blue">
                            <div class="inner">
                                <h3>
                                    <asp:Label ID="lblDistributor" runat="server" Text=""></asp:Label>
                                </h3>

                                <p>Distributor</p>
                            </div>
                            <div class="icon">
                                <i class="ion ion-person-add"></i>
                            </div>
                            <a id="HL_Distributor" href="DS_DistributorDetails.aspx" target="_blank" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box bg-aqua-gradient">
                            <div class="inner">
                                <h3>
                                    <asp:Label ID="lblParlour" runat="server" Text=""></asp:Label>
                                </h3>

                                <p>Parlour</p>
                            </div>
                            <div class="icon">
                                <i class="fa fa-home"></i>
                            </div>
                            <a id="HL_Parlour" href="DS_ParlourDetails.aspx" target="_blank" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box bg-aqua">
                            <div class="inner">
                                <h3>
                                    <asp:Label ID="lblCitizenCardHolder" runat="server" Text=""></asp:Label>
                                </h3>

                                <p>Citizen Card Holder</p>
                            </div>
                            <div class="icon">
                                <i class="ion ion-person-add"></i>
                            </div>
                            <a id="HL_CitizenCardHolder" href="DS_CitizenCardHolderDetails.aspx" target="_blank" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                    <!-- ./col -->



                </div>--%>

                <%--        <div class="row">
                    <div class="col-12 col-sm-6 col-md-3">
                        <div class="info-box">
                            <span class="info-box-icon bg-info elevation-1"><i class="fas fa-cog"></i></span>

                            <div class="info-box-content">
                                <span class="info-box-text">CPU Traffic</span>
                                <span class="info-box-number">10
                 
                                    <small>%</small>
                                </span>
                            </div>
                        </div>
                       
                    </div>
                   
                    <div class="col-12 col-sm-6 col-md-3">
                        <div class="info-box mb-3">
                            <span class="info-box-icon bg-danger elevation-1"><i class="fas fa-thumbs-up"></i></span>

                            <div class="info-box-content">
                                <span class="info-box-text">Likes</span>
                                <span class="info-box-number">41,410</span>
                            </div>
                            
                        </div>
                       
                    </div>
                    <div class="col-12 col-sm-6 col-md-3">
                        <div class="info-box mb-3">
                            <span class="info-box-icon bg-success elevation-1"><i class="fas fa-shopping-cart"></i></span>

                            <div class="info-box-content">
                                <span class="info-box-text">Sales</span>
                                <span class="info-box-number">760</span>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-6 col-md-3">
                        <div class="info-box mb-3">
                            <span class="info-box-icon bg-warning elevation-1"><i class="fas fa-users"></i></span>

                            <div class="info-box-content">
                                <span class="info-box-text">New Members</span>
                                <span class="info-box-number">2,000</span>
                            </div>
                           
                        </div>
                    </div>
                </div>--%>
            </div>

            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label>Date<span style="color: red;">*</span></label>
                        <div class="input-group date">
                            <div class="input-group-addon">
                                <i class="fa fa-calendar"></i>
                            </div>
                            <asp:TextBox ID="txtOrderDate" runat="server" Style="background-color: white;" placeholder="Select Date..." class="form-control DateAdd" onpaste="return false" AutoPostBack="true" OnTextChanged="txtOrderDate_TextChanged"></asp:TextBox>

                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label>Shift<span style="color: red;">*</span></label>
                        <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlShift_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                <div class="col-md-3">
                    <div class="form-group">
                        <label>Chart Type<span style="color: red;">*</span></label>
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control select2" ClientIDMode="Static">
                            <asp:ListItem Value="1" Text="Pie Chart"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Area Chart"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Line Chart"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Bar Chart"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>


            </div>

            <div class="form-group"></div>
            <div class="row">

                <div class="col-md-6">
                    <div class="card" style="padding-right: 1px;">
                        <div class="card-header">
                            <h3 class="card-title">MILK COLLECTION</h3>
                        </div>
                        <b>&nbsp;Quantity :
                            <asp:Label ID="lblCL_MilkQuantity" runat="server" Text=""></asp:Label>
                            Kg.  &nbsp;&nbsp;|&nbsp;&nbsp;
                            Milk FAT :
                            <asp:Label ID="lblCL_MilkFat" runat="server" Text=""></asp:Label>
                            Kg. &nbsp;&nbsp;|&nbsp;&nbsp;
                            Milk SNF :
                            <asp:Label ID="lblCL_MilkSnf" runat="server" Text=""></asp:Label>
                            Kg. </b>
                        <div id="piechartMilkCollection" style="min-height: 270px;"></div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="card" style="padding-right: 1px;">
                        <div class="card-header">
                            <h3 class="card-title">DEMAND</h3>
                        </div>
                        <b>&nbsp;Milk [ Quantity :
                            <asp:Label ID="lblD_MilkQuantity" runat="server" Text=""></asp:Label>
                            Ltr.  &nbsp;&nbsp;|&nbsp;&nbsp;
                           <%-- FAT :
                            <asp:Label ID="lblD_MilkFat" runat="server" Text=""></asp:Label>
                            Kg. &nbsp;&nbsp;|&nbsp;&nbsp;
                            SNF :
                            <asp:Label ID="lblD_MilkSnf" runat="server" Text=""></asp:Label>
                            Kg. ]<br />--%>
                            &nbsp;Product [ Quantity :
                            <asp:Label ID="lblD_ProductQuantity" runat="server" Text=""></asp:Label>
                            Kg.  &nbsp;&nbsp;|&nbsp;&nbsp;
                             <%--FAT :
                           <asp:Label ID="lblD_ProductFat" runat="server" Text=""></asp:Label>
                            Kg. ]</b>--%>
                            <div id="donutchartDimandMilkProduct" style="min-height: 270px;"></div>
                    </div>

                </div>

            </div>
            <div class="form-group"></div>
            <div class="row">
                <div class="col-md-6">
                    <div class="card" style="padding-right: 1px;">
                        <div class="card-header">
                            <h3 class="card-title">PRODUCTION</h3>
                        </div>
                        <b>&nbsp;Milk [ Quantity : 
                            <asp:Label ID="lblMilkQuantity" runat="server" Text=""></asp:Label>
                            Kg.  &nbsp;&nbsp;|&nbsp;&nbsp;
                           FAT : 
                            <asp:Label ID="lblMilkFat" runat="server" Text=""></asp:Label>
                            Kg. &nbsp;&nbsp;|&nbsp;&nbsp;
                           SNF : 
                            <asp:Label ID="lblMilkSnf" runat="server" Text=""></asp:Label>
                            Kg. ]<br />
                            &nbsp;Product [ Quantity : 
                            <asp:Label ID="lblProductQuantity" runat="server" Text=""></asp:Label>
                            Kg.  &nbsp;&nbsp;|&nbsp;&nbsp;
                           FAT : 
                            <asp:Label ID="lblProductFat" runat="server" Text=""></asp:Label>
                            Kg. ]</b>
                        <div id="piechart_3dProduction" style="min-height: 270px;"></div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="card" style="padding-right: 1px;">
                        <div class="card-header">
                            <h3 class="card-title">QUALITY CONTROL</h3>
                        </div>
                        <b>&nbsp;Total Tested Batch & Lot No. :
                            <asp:Label ID="lblTestCases" runat="server" Text=""></asp:Label>
                            &nbsp;&nbsp;|&nbsp;&nbsp;Pass:
                            <asp:Label ID="lblPassCases" runat="server" Text=""></asp:Label>
                            &nbsp;&nbsp;|&nbsp;&nbsp;Fail :
                            <asp:Label ID="lblFailCases" runat="server" Text=""></asp:Label>
                        </b>
                        <div id="piechartQualityControl" style="min-height: 270px;"></div>
                    </div>
                </div>
            </div>
            <div class="form-group"></div>
            <div class="row">
                <div class="col-md-6">
                    <div class="card" style="padding-right: 1px;">
                        <div class="card-header">
                            <h3 class="card-title">SUPPLY STATUS</h3>
                        </div>
                        <b>&nbsp;Milk [ Quantity :
                            <asp:Label ID="lblSup_MilkQuantity" runat="server" Text=""></asp:Label>
                            Kg.  &nbsp;&nbsp;|&nbsp;&nbsp;
                            FAT :
                            <asp:Label ID="lblSup_MilkFat" runat="server" Text=""></asp:Label>
                            Kg. &nbsp;&nbsp;|&nbsp;&nbsp;
                            SNF :
                            <asp:Label ID="lblSup_MilkSnf" runat="server" Text=""></asp:Label>
                            Kg. ]<br />
                            &nbsp;Product [ Quantity :
                            <asp:Label ID="lblSup_ProductQuantity" runat="server" Text=""></asp:Label>
                            Kg.  &nbsp;&nbsp;|&nbsp;&nbsp;
                            FAT :
                            <asp:Label ID="lblSup_ProductFat" runat="server" Text=""></asp:Label>
                            Kg. ]</b>
                        <div id="donutchartSupply" style="min-height: 270px;"></div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="card" style="padding-right: 1px;">
                        <div class="card-header">
                            <h3 class="card-title">CRATE MANAGEMENT</h3>
                        </div>
                        <b>&nbsp;Quantity : 1700  &nbsp;&nbsp;[ Usable : 700 &nbsp;&nbsp;|&nbsp;&nbsp;Scrap : 700 &nbsp;&nbsp;|&nbsp;&nbsp;Out : 950 ]</b>
                        <div id="donutchartCrate" style="min-height: 270px;"></div>
                    </div>
                </div>
            </div>
            <div class="form-group"></div>
            <div class="row">
                <div class="col-md-6">
                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">SALE RETURN</h3>
                        </div>
                        <b>&nbsp;Sale :
                            <asp:Label ID="lblSale_Quantity" runat="server" Text=""></asp:Label>
                            Pkt.  &nbsp;&nbsp;
                            [ Milk :
                            <asp:Label ID="lblSale_Milk" runat="server" Text=""></asp:Label>
                            Pkt. &nbsp;&nbsp;|&nbsp;&nbsp;
                            Product :
                            <asp:Label ID="lblSale_Product" runat="server" Text=""></asp:Label>
                            Pkt. ]<br />
                            &nbsp;Return :
                            <asp:Label ID="lblRet_Quantity" runat="server" Text=""></asp:Label>
                            Pkt.  &nbsp;&nbsp;
                            [ Milk :
                            <asp:Label ID="lblRet_Milk" runat="server" Text=""></asp:Label>
                            Pkt. &nbsp;&nbsp;|&nbsp;&nbsp;
                            Product :
                            <asp:Label ID="lblRet_Product" runat="server" Text=""></asp:Label>
                            Pkt. ]
                        </b>
                        <div id="donutchartSaleReturn" style="min-height: 270px;"></div>
                    </div>
                </div>
                <div class="col-md-6">
                </div>
            </div>

        </section>

    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <%--  <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>--%>
    <script src="../../mis/Dashboard/DS_Chart/loader.js"></script>
    <script>
        function FunChart(Prod_MilkQuantity, Prod_ProductQuantity, QC_PassCases, QC_FailCases, D_MilkQuantity, D_ProductQuantity
            , CL_Society, CL_BMC, CL_CC, CL_MDP, Sup_MilkQuantity, Sup_ProductQuantity, Actual_Sale, Ret_Quantity) {
            FunProduction(Prod_MilkQuantity, Prod_ProductQuantity);
            FunQualityControl(QC_PassCases, QC_FailCases);
            //FunDemand(MilkDemand, ProductDemand)
            FunDemand(D_MilkQuantity, D_ProductQuantity);
            FunMilkCollection(CL_Society, CL_BMC, CL_CC, CL_MDP);
            FunSupply(Sup_MilkQuantity, Sup_ProductQuantity);
            FunSaleRet(Actual_Sale, Ret_Quantity);
        }
    </script>
    <script type="text/javascript">
        /*********MILK COLLECTION Start*********/
        function FunMilkCollection(Society, BMC, CC, MDP) {
            google.charts.load('current', { 'packages': ['corechart'] });
            google.charts.setOnLoadCallback(drawChart);

            function drawChart() {

                var data = google.visualization.arrayToDataTable([
                  ['Milk', 'In KG.'],
                  ['Society', parseFloat(Society)],
                  ['BMC', parseFloat(BMC)],
                  ['CC', parseFloat(CC)],
                  ['MDP', parseFloat(MDP)]
                ]);

                var options = {
                    // title: 'MILK COLLECTION'
                };

                var chart = new google.visualization.PieChart(document.getElementById('piechartMilkCollection'));

                chart.draw(data, options);
            }
        }
        /*********MILK COLLECTION End*********/
    </script>
    <script type="text/javascript">
        /*********DIMAND MILK Product Start*********/

        function FunDemand(MilkDemand, ProductDemand) {
            google.charts.load("current", { packages: ["corechart"] });
            google.charts.setOnLoadCallback(drawChart);
            function drawChart() {
                var data = google.visualization.arrayToDataTable([

                   ['Milk', 'In KG.'],
                  ['Milk', parseFloat(MilkDemand)],
                  ['Product', parseFloat(ProductDemand)]
                ]);

                var options = {
                    // title: 'MILK DIMAND (MILK AND Product) WISE',
                    pieHole: 0.4,
                };

                var chart = new google.visualization.PieChart(document.getElementById('donutchartDimandMilkProduct'));
                chart.draw(data, options);
            }
        }
        /*********DIMAND MILK Product End*********/
    </script>
    <script type="text/javascript">
        /*********PRODUCTION Start*********/
        function FunProduction(MValue, PValue) {
            var MilkV = parseFloat(MValue).toFixed(3);
            var ProductV = parseFloat(PValue).toFixed(3);
            google.charts.load("current", { packages: ["corechart"] });
            google.charts.setOnLoadCallback(drawChart);
            function drawChart() {
                var data = google.visualization.arrayToDataTable([
                  ['Milk', 'In KG.'],
                  ['Milk', parseFloat(MilkV)],
                  ['Product', parseFloat(ProductV)]
                ]);

                var options = {
                    // title: 'PRODUCTION',
                    is3D: true,
                };

                var chart = new google.visualization.PieChart(document.getElementById('piechart_3dProduction'));
                chart.draw(data, options);
            }
        }
        /*********PRODUCTION End*********/
    </script>
    <script type="text/javascript">
        // FunQualityControl(120,50);
        /*********QUALITY CONTROL Start*********/
        function FunQualityControl(Pass, Fail) {
            var PS = parseInt(Pass);
            var FL = parseInt(Fail);
            google.charts.load('current', { 'packages': ['corechart'] });
            google.charts.setOnLoadCallback(drawChart);

            function drawChart() {

                var data = google.visualization.arrayToDataTable([
                  ['Batch & Lot No.', 'In No.'],
                  ['Pass', PS],
                  ['Fail', FL],
                ]);

                var options = {
                    // title: 'QUALITY CONTROL'
                };

                var chart = new google.visualization.PieChart(document.getElementById('piechartQualityControl'));

                chart.draw(data, options);
            }
        }
        /*********QUALITY CONTROL End*********/
    </script>
    <script type="text/javascript">
        /*********SUPPLY STATUS Start*********/
        function FunSupply(Sup_Milk, Sup_Product) {
            google.charts.load("current", { packages: ["corechart"] });
            google.charts.setOnLoadCallback(drawChart);
            function drawChart() {
                var data = google.visualization.arrayToDataTable([

                   ['Milk', 'In KG.'],
                   ['Milk', parseFloat(Sup_Milk)],
                  ['Product', parseFloat(Sup_Product)]
                ]);

                var options = {
                    //title: 'SUPPLY STATUS',
                    pieHole: 0.4,
                };

                var chart = new google.visualization.PieChart(document.getElementById('donutchartSupply'));
                chart.draw(data, options);
            }
        }
        /*********SUPPLY STATUS End*********/
    </script>
    <script type="text/javascript">
        /*********CRATE MANAGEMENT Start*********/

        google.charts.load("current", { packages: ["corechart"] });
        google.charts.setOnLoadCallback(drawChart);
        function drawChart() {
            var data = google.visualization.arrayToDataTable([

               ['Crate', 'In No.'],
              ['Usable', 700],
              ['Scrap', 50],
              ['Out', 950]
            ]);

            var options = {
                //title: 'CRATE MANAGEMENT',
                pieHole: 0.4,
            };

            var chart = new google.visualization.PieChart(document.getElementById('donutchartCrate'));
            chart.draw(data, options);
        }

        /*********CRATE MANAGEMENT End*********/
    </script>

    <script type="text/javascript">
        /*********SALE RETURN Start*********/
        function FunSaleRet(ActualSale, Return) {
            google.charts.load("current", { packages: ["corechart"] });
            google.charts.setOnLoadCallback(drawChart);
            function drawChart() {
                var data = google.visualization.arrayToDataTable([

                   ['Milk', 'In Pkt.'],
                  ['Sale', parseFloat(ActualSale)],
                  ['Return', parseFloat(Return)]
                ]);

                var options = {
                    //title: 'SALE RETURN',
                    pieHole: 0.4,
                };

                var chart = new google.visualization.PieChart(document.getElementById('donutchartSaleReturn'));
                chart.draw(data, options);
            }
        }
        /*********SALE RETURN End*********/
    </script>
</asp:Content>


