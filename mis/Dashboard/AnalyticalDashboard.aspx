<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="AnalyticalDashboard.aspx.cs" Inherits="mis_Dashboard_AnalyticalDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
     <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
    <script src="https://code.highcharts.com/modules/export-data.js"></script>
    <script src="https://code.highcharts.com/modules/accessibility.js"></script>

    <style>
        .highcharts-figure, .highcharts-data-table table {
            min-width: 320px;
            max-width: 660px;
            margin: 1em auto;
        }

        .highcharts-data-table table {
            font-family: Verdana, sans-serif;
            border-collapse: collapse;
            border: 1px solid #EBEBEB;
            margin: 10px auto;
            text-align: center;
            width: 100%;
            max-width: 500px;
        }

        .highcharts-data-table caption {
            padding: 1em 0;
            font-size: 1.2em;
            color: #555;
        }

        .highcharts-data-table th {
            font-weight: 600;
            padding: 0.5em;
        }

        .highcharts-data-table td, .highcharts-data-table th, .highcharts-data-table caption {
            padding: 0.5em;
        }

        .highcharts-data-table thead tr, .highcharts-data-table tr:nth-child(even) {
            background: #f8f8f8;
        }

        .highcharts-data-table tr:hover {
            background: #f1f7ff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">


        <section class="content-header">
            <h1 style="display: inline;">Analytical Dashboard
            </h1>
            <h4 style="display: inline;"><b>As Per Last Record :
                <asp:label ID="Curr" Visible="true" runat="server"></asp:label>
               <%-- b>(Current Date :
                <asp:label ID="lblCurrentDate" runat="server"></asp:label>
                )--%>
            </b></h4>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <h4 style="display: inline;">Current Date : 
                <asp:Label ID="lblCDate" runat="server"></asp:Label>
              
           </h4>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Analytical Dashboard</li>
            </ol>

        </section>
        <section class="content">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            <div class="row">
                <div class="col-md-6">
                                    <div id="div_table2" runat="server" visible="false">
                                      <fieldset>
                                            <legend>Milk Collection Details</legend>                                        
                                            <div id="divTruckDetentionData"></div>

                                            <div id="divTruckDetentionData1" runat="server"></div>
                                        </fieldset>
                                    </div>
               </div>
                <div class="col-md-6">
                                    <div id="div_table3" runat="server" visible="false">
                                      <fieldset>
                                            <legend>Milk Sale Details</legend>                                        
                                            <div id="divMilkSale"></div>

                                            <div id="divMilkSale1" runat="server"></div>
                                        </fieldset>
                                    </div>
               </div>
                <div class="col-md-6">
                                    <div id="div_table4" runat="server" visible="false">
                                      <fieldset>
                                            <legend>Society Billing (In Rs)</legend>                                        
                                            <div id="divSC"></div>

                                           <div id="divSC1" runat="server"></div>
                                        </fieldset>
                                    </div>
               </div>
                 <div class="col-md-6">
                                    <div id="div_table5" runat="server" visible="false">
                                      <fieldset>
                                            <legend>Milk Processing (In KG)</legend>                                        
                                            <div id="divie"></div>

                                            <div id="div2" runat="server"></div>
                                        </fieldset>
                                    </div>
               </div>

                 <div class="col-md-6">
                                    <div id="div_table6" runat="server" visible="false">
                                      <fieldset>
                                            <legend>Sales Turnover</legend>                                        
                                            <div id="divprofit"></div>

                                            <div id="div3" runat="server"></div>
                                        </fieldset>
                                    </div>
               </div>
			   <div class="col-md-6">
                    <div id="div_table7" runat="server">
                        <fieldset>
                            <legend>Cattle Feed Plant (MT)</legend>
                                      <script type='text/javascript' src='https://www.gstatic.com/charts/loader.js'></script>
                            <div id="divCFP1" runat="server" style='width: 500px; height: 400px;'></div>
                            <%--<div id="columnchart_material" style="width: 500px; height: 400px;"></div>
                            <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
                            <script type="text/javascript">
                                google.charts.load('current', { 'packages': ['bar'] });
                                google.charts.setOnLoadCallback(drawChart);

                                function drawChart() {
                                    var data = google.visualization.arrayToDataTable([
                                      ['Office', 'Production', 'Sales'],
                                      ['मांगलिया', 4099.05, 4018.95],
                                      ['पचामा', 3558, 3638.6],
                                      ['सिरोंजा', 0, 0],
                                      ['शिवपुरी', 0, 0]
                                    ]);

                                    var options = {
                                        chart: {
                                            title: 'Cattle Feed Plant (In MT)',
                                            //subtitle: 'Sales, Expenses, and Profit: 2014-2017',
                                        }
                                    };

                                    var chart = new google.charts.Bar(document.getElementById('columnchart_material'));

                                    chart.draw(data, google.charts.Bar.convertOptions(options));
                                }
                            </script>--%>
                        </fieldset>
                    </div>
                </div>
            </div>
            <div class="row">
              <div class="col-lg-3 col-xs-6">
                <!-- small box -->
                <div class="small-box btn-dropbox">
                    <div class="inner">
                        <h3>
                            <asp:Label ID="lblMilkCollectionInLtr" Font-Size="XX-Large" runat="server"></asp:Label></h3>
                        <p>Milk Collection (In KG)</p>
                    </div>
                    <%--<div class="icon">

                        <i class="fa fa-money"></i>
                    </div>--%>
                    <asp:LinkButton ID="lnkMilkCollection" OnClick="lnkMilkCollection_Click" CssClass="small-box-footer" runat="server">More info<i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                </div>
            </div>
                <div class="col-lg-3 col-xs-6">
                <!-- small box -->
                <div class="small-box btn-soundcloud">
                    <div class="inner">
                        <h3>
                            <asp:Label ID="lblPlantMilkSale" Font-Size="XX-Large" runat="server"></asp:Label></h3>
                        <p>Milk Sale (In Ltr)</p>
                    </div>
                    <%--<div class="icon">

                        <i class="ion ion-bag"></i>
                    </div>--%>
                    <asp:LinkButton ID="lnkPlantMilkSale" OnClick="lnkPlantMilkSale_Click" CssClass="small-box-footer" runat="server">More info<i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                </div>
            </div>
              <div class="col-lg-3 col-xs-6">
                <!-- small box -->
                <div class="small-box btn-foursquare">
                    <div class="inner">
                        <h3>
                            <asp:Label ID="lblSocietyBilling" Font-Size="XX-Large" runat="server"></asp:Label></h3>
                        <p>Society Billing (In Rs.)</p>
                    </div>
                    <div class="icon">

                        <i class="fa fa-rupee"></i>
                    </div>
                    <asp:LinkButton ID="lnkSocietyBilling" OnClick="lnkSocietyBilling_Click" CssClass="small-box-footer" runat="server">More info<i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                </div>
            </div>
                   <div class="col-lg-3 col-xs-6">
                <!-- small box -->
                <div class="small-box btn-facebook">
                    <div class="inner">
                        <h3>
                            <asp:Label ID="lblInventoryPOCount" Font-Size="XX-Large" runat="server"></asp:Label></h3>
                        <p>Inventory PO</p>
                    </div>
                  <%--  <div class="icon">

                        <i class="ion ion-bag"></i>
                    </div>--%>
                    <asp:LinkButton ID="lnkPOOfficeWise" OnClick="lnkPOOfficeWise_Click" CssClass="small-box-footer" runat="server">More info<i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                </div>
            </div>
                </div>
             
         
           
             
          
             <%--<div class="col-lg-3 col-xs-6">
                <!-- small box -->
                <div class="small-box btn-info">
                    <div class="inner">
                        <h3>
                            <asp:Label ID="lblProducerPayment" Font-Size="XX-Large" runat="server"></asp:Label></h3>
                        <p>Producer Payment (In Rs.)</p>
                    </div>
                    <div class="icon">

                        <i class="fa fa-rupee"></i>
                    </div>
                    <asp:LinkButton ID="lnkProducerPayment" OnClick="lnkProducerPayment_Click" CssClass="small-box-footer" runat="server">More info<i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                </div>
            </div>--%>
         

            <div class="modal" id="MilkCollectionModel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 350px;">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">
                                <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                            </button>
                            <h4 class="modal-title">Milk Collection(In KG)</h4>
                        </div>
                        <div class="modal-body">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GVMilkCollection" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                            EmptyDataText="No Record Found." ShowFooter="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DS Name" ItemStyle-Width="40%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label Font-Bold="true" ID="lblDTotal" Text="Total" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Milk Collection (In Ltr.)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMilkCollectionInLtr" runat="server" Text='<%# Eval("MC_QtyInLtr") %>'></asp:Label>
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
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>

            <div class="modal" id="PlantMilkSaleModel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 350px;">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">
                                <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                            </button>
                            <h4 class="modal-title">Milk Sale(In Ltr)</h4>
                        </div>
                        <div class="modal-body">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GvPlantMilkSale" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                            EmptyDataText="No Record Found." ShowFooter="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DS Name" ItemStyle-Width="40%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label Font-Bold="true" ID="lblDTotal" Text="Total" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Milk Sale (In Ltr.)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPlantMilkSaleInLtr" runat="server" Text='<%# Eval("PlantMilkSale") %>'></asp:Label>
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
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
            
            <div class="modal" id="SocietyBillingModel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 350px;">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">
                                <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                            </button>
                            <h4 class="modal-title">Society Billing</h4>
                        </div>
                        <div class="modal-body">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GVSocietyBilling" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                            EmptyDataText="No Record Found." ShowFooter="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DS Name" ItemStyle-Width="40%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cycle">
                                                    <ItemTemplate>
                                                         <asp:Label ID="lblBillingCycle" runat="server" Text='<%# Eval("BillingCycle") %>'></asp:Label>
                                                    </ItemTemplate>
                                                       <FooterTemplate>
                                                        <asp:Label Font-Bold="true" ID="lblDTotal" Text="Total" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Society Count">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSocietyCount" runat="server" Text='<%# Eval("SocietyCount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                     <FooterTemplate>
                                                        <asp:Label Font-Bold="true" ID="lblSocietyCount" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Net Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNetAmount" runat="server" Text='<%# Eval("NetAmount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label Font-Bold="true" ID="lblTotalNetAmount" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                        </div>

                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>

            <div class="modal" id="ProducerPaymentModel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 350px;">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">
                                <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                            </button>
                            <h4 class="modal-title">Producer Payment</h4>
                        </div>
                        <div class="modal-body">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GVProducerPayment" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                            EmptyDataText="No Record Found." ShowFooter="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DS Name" ItemStyle-Width="40%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cycle">
                                                    <ItemTemplate>
                                                         <asp:Label ID="lblBillingCycle" runat="server" Text='<%# Eval("BillingCycle") %>'></asp:Label>
                                                    </ItemTemplate>
                                                       <FooterTemplate>
                                                        <asp:Label Font-Bold="true" ID="lblDTotal" Text="Total" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Producer Count">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProducerCount" runat="server" Text='<%# Eval("ProducerCount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                     <FooterTemplate>
                                                        <asp:Label Font-Bold="true" ID="lblProducerCount" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Net Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNetAmount" runat="server" Text='<%# Eval("NetAmount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label Font-Bold="true" ID="lblTotalNetAmount" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                        </div>

                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>

            <div class="modal" id="InventoryPOData">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 350px;">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">
                                <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                            </button>
                            <h4 class="modal-title">Inventory PO Count</h4>
                        </div>
                        <div class="modal-body">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GVInventoryPOData" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                            EmptyDataText="No Record Found." ShowFooter="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DS Name" ItemStyle-Width="40%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label Font-Bold="true" ID="lblDTotal" Text="Total" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PO Count">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPOCount" runat="server" Text='<%# Eval("POCount") %>'></asp:Label>
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
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function MilkCollection() {
            $("#MilkCollectionModel").modal('show');

        }
        function PlantMilkSale() {
            $("#PlantMilkSaleModel").modal('show');

        }
        function SocietyBillingData() {
            $("#SocietyBillingModel").modal('show');

        }
        //function ProducerPaymentData() {
        //    $("#ProducerPaymentModel").modal('show');

        //}
        function InventoryPOData() {
            $("#InventoryPOData").modal('show');

        }
    </script>
    
</asp:Content>

