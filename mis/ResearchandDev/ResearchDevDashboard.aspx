<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ResearchDevDashboard.aspx.cs" Inherits="mis_ResearchandDev_ResearchDevDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Dashboard&nbsp;&nbsp;</h3>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <asp:Panel ID="panel1" runat="server">
                    <div class="row">
                        <div class="col-md-1"></div>
                        <div class="col-md-5">
                             <div class="box" style="border:1px solid #309eff; min-height: 180px; min-width:442px;">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div id="chart1"></div>                                       
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <label style="border: 10px solid #309eff; text-align: center; background-color: #309eff; margin-bottom: 0px; color: white; width: 100%;">Total Research & Development Project</label>
                                    </div>
                                </div>
                            </div>
                       </div>
                        <div class="col-md-5">
                             <div class="box" style="border:1px solid #309eff; min-height: 180px; min-width:442px;">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div id="chart2"></div>                                       
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <label style="border: 10px solid #309eff; text-align: center; background-color: #309eff; margin-bottom: 0px; color: white; width: 100%;">New & Existing Research & Development Project</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                         <div class="col-md-1"></div>                        
                    </div>
                    <div class="row">
                        <div class="col-md-1"></div>
                        <div class="col-md-5">
                             <div class="box" style="border:1px solid #309eff; min-height: 180px; min-width:442px;">
                                <div class="row">
                                    <div class="col-md-12">

                                        <div id="chart4"></div>
                                        
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <label style="border: 10px solid #309eff; text-align: center; background-color: #309eff; margin-bottom: 0px; color: white; width: 100%;">Survey Status of Research & Development Project</label>
                                    </div>
                                </div>

                            </div>

                        </div>
                        <div class="col-md-5">
                             <div class="box" style="border:1px solid #309eff; min-height: 180px; min-width:442px;">
                                <div class="row">
                                    <div class="col-md-12">

                                        <div id="chart3"></div>
                                        
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <label style="border: 10px solid #309eff; text-align: center; background-color: #309eff; margin-bottom: 0px; color: white; width: 100%;">Research & Development Project Implement in Dugdh Sangh</label>
                                    </div>
                                </div>

                            </div>

                        </div>
                        
                         <div class="col-md-1"></div>
                        
                    </div>
                        <div class="row">
                        <div class="col-md-1"></div>
                        <div class="col-md-5">
                             <div class="box" style="border:1px solid #309eff; min-height: 180px; min-width:442px;">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div id="chart5"></div>                                        
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <label style="border: 10px solid #309eff; text-align: center; background-color: #309eff; margin-bottom: 0px; color: white; width: 100%;">Action Plan of Research & Development Project After Implementation</label>
                                    </div>
                                </div>
                            </div>
                        </div>                                               
                         <div class="col-md-1"></div>                        
                    </div>
                        </asp:Panel>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
   <script type="text/javascript">
       google.charts.load('current', { 'packages': ['corechart'] });
       google.charts.setOnLoadCallback(drawPieChart1);
       google.charts.setOnLoadCallback(drawPieChart2);
       google.charts.setOnLoadCallback(drawColumnChart);
       google.charts.setOnLoadCallback(drawPieChart3);
       google.charts.setOnLoadCallback(drawPieChart4);


       function drawPieChart1() {

           var data = google.visualization.arrayToDataTable([
             ['ProjectType', 'Count'],
             ['Product ', 7],
             ['Machinery', 3],
           ]);

           var options = {
               title: ''
           };

           var chart = new google.visualization.PieChart(document.getElementById('chart1'));

           chart.draw(data, options);
       }
       function drawPieChart2() {

           var data = google.visualization.arrayToDataTable([
             ['Project', 'Count'],
             ['New ', 6],
             ['Existing', 4],
           ]);

           var options = {
               title: ''
           };

           var chart = new google.visualization.PieChart(document.getElementById('chart2'));

           chart.draw(data, options);
       }
       function drawPieChart3() {

           var data = google.visualization.arrayToDataTable([
             ['Survey', 'Count'],
             ['Completed', 4],
             ['InCompleted', 6],
           ]);

           var options = {
               title: ''
           };

           var chart = new google.visualization.PieChart(document.getElementById('chart4'));

           chart.draw(data, options);
       }
       function drawColumnChart() {

           var data = google.visualization.arrayToDataTable([
         ['Element', 'Project', { role: 'style' }],
         ['Bhopal', 3, '#78cade'],            // RGB value
         ['Gwalior', 2, '#e3e366'],            // English color name
         ['Indore', 1, 'green'],
         ['Jabalpur', 2, 'pink'],
         ['Ujjain', 1, '#66a3c4'],
         ['BundelKhand', 1, '#e0d1e6'],


           ]);

           var options = {
               title: ''
           };

           var chart = new google.visualization.ColumnChart(document.getElementById('chart3'));

           chart.draw(data, options);
       }
       function drawPieChart4() {

           var data = google.visualization.arrayToDataTable([
             ['Action Plan', 'Count'],
             ['Under Trial', 5],
             ['Will Use After Some time', 2],
             ['Others', 3],
           ]);

           var options = {
               title: ''
           };

           var chart = new google.visualization.PieChart(document.getElementById('chart5'));

           chart.draw(data, options);
       }
       </script>
</asp:Content>

