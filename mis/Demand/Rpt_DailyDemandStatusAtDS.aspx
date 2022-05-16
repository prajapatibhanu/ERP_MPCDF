﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Rpt_DailyDemandStatusAtDS.aspx.cs" Inherits="mis_Demand_Rpt_DailyDemandStatusAtDS" EnableEventValidation="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .exportborder
        {
            border: 1px solid black;
        }
        .columngreen {
            background-color: #aee6a3 !important;
        }
        .columnred {
            background-color: #f05959 !important;
        }
         .columnmilk {
            background-color: #bfc7c5 !important;
        }
        .columnproduct {
            background-color: #f5f376 !important;
        }
        .NonPrintable {
                  display: none;
              }
        .NonPrintable1 {
                  display: none;
              }
        @media print {
              .NonPrintable {
                  display: block;
              }
              .NonPrintable1 {
                  display: block;
              }

              .noprint {
                display: none;
            }
               .pagebreak { page-break-before: always; }
          }

        
         .table1-bordered > thead > tr > th, .table1-bordered > tbody > tr > th, .table1-bordered > tfoot > tr > th, .table1-bordered > thead > tr > td, .table1-bordered > tbody > tr > td, .table1-bordered > tfoot > tr > td {
            border: 1px dashed #000000 !important;
        }
        .thead
        {
            display:table-header-group;
        }
        .text-center{
            text-align: center;
        }
        .text-right{
            text-align: right;
        }
        .table1 > tbody > tr > td, .table1 > tbody > tr > th, .table1 > tfoot > tr > td, .table1 > tfoot > tr > th, .table1 > thead > tr > td, .table1 > thead > tr > th {
            padding: 2px 5px;
           
           
        }
        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('../image/loader/ProgressImage.gif') 50% 50% no-repeat rgba(0, 0, 0, 0.3);
        }  
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
     <div class="loader"></div>
  <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content noprint">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">

           
            <div class="col-md-12">
                <div class="box box-Manish">
                    <div class="box-header">
                        <h3 class="box-title">Demand Status Report</h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <fieldset>
                            <legend>Date ,Shift / दिनांक ,शिफ्ट 
                            </legend>
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>
                            </div>


                            <div class="row">
                               
                                 <div class="col-md-2">
                                     <div class="form-group">
                                        <label>Date / दिनांक<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                ControlToValidate="txtOrderDate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtOrderDate"
                                                ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                        </span>
                                          <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOrderDate" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                     </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Shift / शिफ्ट</label>
                                       <%-- <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                InitialValue="0" ErrorMessage="Select Shift" Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"
                                                ControlToValidate="ddlShift" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>--%>
                                         <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div> 
                                 <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Location / लोकेशन </label>
                                        
                                            <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="col-md-4" id="pnlSearchBy" runat="server" visible="true">
                                <div class="form-group">
                                        <label></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                               ErrorMessage="Select Route/Distributor or Not Submitted" Text="<i class='fa fa-exclamation-circle' title='Select Route/SuperStockist/Distributor or Not Submitted !'></i>"
                                                ControlToValidate="rblReportType" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                     <asp:RadioButtonList ID="rblReportType"  runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem class="radio-inline" Text="Route wise&nbsp;&nbsp;" Value="1"></asp:ListItem> 
                                            <asp:ListItem class="radio-inline" Text="Distributor wise&nbsp;&nbsp;" Value="2"></asp:ListItem>
                                             <asp:ListItem class="radio-inline" Text="Not Submitted" Value="3"></asp:ListItem>
                                            
                                        </asp:RadioButtonList>
                                    </div>
                                </div> 
                                    <div class="col-md-1">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" OnClick="btnSearch_Click" ValidationGroup="a" ID="btnSearch" Text="Search" />

                                        </div>
                                    </div>
                                    <div class="col-md-1" style="margin-top: 20px;">
                                        <div class="form-group">

                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-default" />
                                        </div>
                                    </div>                      
                            </div>
                        </fieldset>
                               
                    </div>

                </div>
            </div>
                 <div class="col-md-12" id="pnlData" runat="server" visible="false">
                <div class="box box-Manish">
                    <div class="box-header">
                        <h3 class="box-title">Demand Status Report</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblReportMsg" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="row" id="pnlrouteOrDistOrNotSubmitteddata" runat="server" visible="false">
                                <div class="col-md-2 pull-right">
                                     <asp:Button ID="btnPrintRoutWise" runat="server" CssClass="btn btn-primary" Text="Print" OnClick="btnPrintRoutWise_Click" />   
                                    <asp:Button ID="btnExportAll" runat="server" CssClass="btn btn-success" OnClick="btnExportAll_Click" Text="Export"  /> &nbsp;&nbsp;  
                                </div>
                               
                                <div class="col-md-12" >
                                        <fieldset>
                                            <legend><span id="pnllegand" runat="server"></span></legend>
                                           
                                                <div class="col-md-12">
                                                   <div class="table-responsive">
                                            <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand" ShowFooter="true" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                AutoGenerateColumns="true" EmptyDataText="No Record Found." EnableModelValidation="True">
                                               <Columns>
                                                   <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Route">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnRoute" CssClass="btn btn-block btn-secondary" Text='<%#Eval("Route") %>' CommandName="RoutwiseBooth" CommandArgument='<%#Eval("RouteId") %>' runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               </Columns>
                                            </asp:GridView>
                                                        <asp:GridView ID="GridView2" runat="server" OnRowDataBound="GridView2_RowDataBound" OnRowCommand="GridView2_RowCommand" ShowFooter="true" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                AutoGenerateColumns="true" EmptyDataText="No Record Found."  EnableModelValidation="True">
                                                <Columns>
                                                   <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Distributor Name">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnDistributor" CssClass="btn btn-block btn-secondary" Text='<%#Eval("Distributor Name") %>' CommandName="DistwiseBooth" CommandArgument='<%#Eval("DistributorId") %>' runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               </Columns>
                                               
                                            </asp:GridView>
                                                   <asp:GridView ID="GridView3" runat="server" class="datatable table table-striped table-bordered" AllowPaging="false"
                                            AutoGenerateColumns="False" EmptyDataText="No Record Found." EnableModelValidation="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Route">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" Text='<%# Eval("RName") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Retailer">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPAI" Text='<%# Eval("BoothName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField> 
                                            </Columns>
                                        </asp:GridView>   
                                                </div>
                                            </div> 
                                            <div class="col-md-12" id="pnldemand" runat="server" visible="false">
                                            <label>Total Demand In Pkt:</label>
                                            <asp:Label ID="lblTotalDemandValue" Font-Bold="true" runat="server"></asp:Label>
                                        </div>
                                          <div class="col-md-12" id="pnltotalDemandInLitre" runat="server" visible="false">
                                            <label>Demand In Ltr. : </label>
                                            <asp:Label ID="lblTotalDemandInLtrValue" Font-Bold="true" runat="server"></asp:Label>

                                        </div>                                
                                        </fieldset>
                                    </div>

                                 
                            </div>

                      
                               
                    </div>

                </div>

                     <div class="modal" id="ItemDetailsModal">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Item Details for <span id="modalRootOrDistName" style="color:red" runat="server"></span>&nbsp;&nbsp;Date :<span id="modaldate" style="color:red" runat="server"></span>&nbsp;&nbsp;Shift : <span id="modelShift" style="color:red" runat="server"></span></h4>
                        </div>
                        <div class="modal-body">
                            <div id="divitem" runat="server">
                                <asp:Label ID="lblModalMsg" runat="server" Text=""></asp:Label>
                                <div class="row">
                                   <div class="col-md-12" id="pnlpopupdata" runat="server" visible="false">
                                        <fieldset>
                                            <legend>Item Details</legend>
                                            <div class="row" style="height:250px;overflow:scroll;">
                                                <div class="col-md-12">
                                                    <div class="table-responsive">
                                                      <asp:GridView ID="GridView4" runat="server" ShowFooter="true" OnRowDataBound="GridView4_RowDataBound" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                          AutoGenerateColumns="true" EmptyDataText="No Record Found."  EnableModelValidation="True">
                                                <Columns>
                                                     <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Retailer/Institution Name" HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBandOName" Text='<%# Eval("BandOName") %>' runat="server" />
                                                                        </ItemTemplate>
                                                       </asp:TemplateField>
                                                </Columns>
                                                    </asp:GridView>
                                                        

                                                         <div id="divtable" runat="server"></div>
                                                    </div>
                                                </div>
                                            </div>                                          
                                        </fieldset>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">CLOSE </button>
                           <%-- <button type="button" class="btn btn-primary" id="btnParlorWisePrint" runat="server"  onclick="Print()">Individual Print </button>
                            <button type="button" class="btn btn-primary" id="btnConsRoutePrint" runat="server"  onclick="Print1();">Consolidated Print </button>--%>
                            <asp:Button ID="btnParlorWisePrint" runat="server" OnClick="btnParlorWisePrint_Click"  CssClass="btn btn-primary" Text="Individual Print" />
                            <asp:Button ID="btnConsRoutePrint" runat="server" OnClick="btnConsRoutePrint_Click"  CssClass="btn btn-primary" Text="Consolidated Print" />
                            <asp:Button ID="btnCExoprt" runat="server" OnClick="btnCExoprt_Click"  CssClass="btn btn-success" Text="Consolidated Export" />
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
            </div>
                 </div>
        </section>
        <!-- /.content -->
        <section class="content">
            <div id="Print" runat="server" class="NonPrintable"></div>
            <div id="Print1" runat="server" class="NonPrintable1"></div>

            <div id="ExportAllData" runat="server"></div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            $('.loader').fadeOut();
        });
        function myItemDetailsModal() {
            $("#ItemDetailsModal").modal('show');

        }
        function Print() {
            debugger;
            $("#ctl00_ContentBody_Print").show();
            $("#ctl00_ContentBody_Print1").hide();
            window.print();
            $("#ctl00_ContentBody_Print1").hide();
            $("#ctl00_ContentBody_Print").hide();
        }
        function Print1() {

            $("#ctl00_ContentBody_Print1").show();
            $("#ctl00_ContentBody_Print").hide();
            window.print();
            $("#ctl00_ContentBody_Print1").hide();
            $("#ctl00_ContentBody_Print").hide();
        }

        function ExportConsolidated() {
            PageMethods.Name(Success, Failure);
        }

        function Success(result) {
            alert(result);
        }

        function Failure(error) {
            alert(error);
        }

    </script>
</asp:Content>
