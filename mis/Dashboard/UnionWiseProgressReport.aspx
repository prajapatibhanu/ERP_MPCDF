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
            <h1 style="display: inline;">Progress @glance
            </h1>
            <h4 style="display: inline;"><b>(Date :
                <label id="Curr" runat="server"></label>
                )
            </b></h4>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Progress @glance</li>
            </ol>

        </section>

        <section class="content">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            <div class="row">
                <%--<div class="col-md-6">
                    <fieldset>
                        <legend>MILK COLLECTION(TRUCK SHEET)</legend>
                        <asp:GridView ID="GvTruckSheet" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                            EmptyDataText="No Record Found." OnRowCommand="GvTruckSheet_RowCommand">
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
                                <asp:TemplateField HeaderText="MilkCollection (In Ltr.)">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblMilkCollection" runat="server" CommandName="TruckMilkColl" CssClass="label label-info" Text='<%# Eval("MilkCollection") %>' CommandArgument='<%# Eval("Office_ID") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Collection Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLastDate" runat="server" Text='<%# Eval("LastDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </fieldset>
                </div>--%>
                <div class="col-md-6">
                    <fieldset>
                        <legend>MCMS</legend>
                        <asp:GridView ID="GrdMCMS" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                            EmptyDataText="No Record Found." OnRowCommand="GrdMCMS_RowCommand">
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
                                <asp:TemplateField HeaderText="Total CC">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblTotalCC" runat="server" CommandName="TotalCC" CssClass="label label-info" CommandArgument='<%# Eval("Office_ID") %>' Text='<%# Eval("TotalCC") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total GatePass">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblTotalGP" runat="server" CommandName="TotalGP" CssClass="label label-info" CommandArgument='<%# Eval("Office_ID") %>' Text='<%# Eval("TotalCCGatepass") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reported CC">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblTotalChallan" runat="server" CommandName="ReportedCC" CssClass="label label-info" CommandArgument='<%# Eval("Office_ID") %>' Text='<%# Eval("TotalChallan") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Challan Entry Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLastDate" runat="server" Text='<%# Eval("LastDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </fieldset>
                </div>
                <div class="col-md-6">
                    <fieldset>
                        <legend>Marketing</legend>
                        <asp:GridView ID="GrdMarketing" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                            EmptyDataText="No Record Found." OnRowCommand="GrdMarketing_RowCommand">
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

                                <asp:TemplateField HeaderText="Demand Date">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblLastDate" runat="server" CommandName="Marketing" CssClass="label label-info" CommandArgument='<%# Eval("Office_ID") %>' Text='<%# Eval("LastDate") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Invoice Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLastInvoiceDate" runat="server" Text='<%# Eval("LastInvoiceDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </fieldset>
                </div>
            </div>
            <div class="row">
                
                <%--<div class="col-md-6">
                    <fieldset>
                        <legend>MILK COLLECTION(CANES COLLECTION/RMRD CHALLAN ENTRY)</legend>
                        <asp:GridView ID="GrdRMRD" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                            EmptyDataText="No Record Found." OnRowCommand="GrdRMRD_RowCommand">
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
                                <asp:TemplateField HeaderText="MilkCollection (In Ltr.)">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblMilkCollection" runat="server" CommandName="RMRDMilkColl" CssClass="label label-info" ToolTip='<%# Eval("TableNum") %>' Text='<%# Eval("MilkCollection") %>' CommandArgument='<%# Eval("Office_ID") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Collection Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLastDate" runat="server" Text='<%# Eval("LastDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </fieldset>
                </div>--%>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <fieldset>
                        <legend>Field Operation</legend>
                        <asp:GridView ID="GrdFieldOperation" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                            EmptyDataText="No Record Found." OnRowCommand="GrdFieldOperation_RowCommand">
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
                                <asp:TemplateField HeaderText="Last Bill Cycle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBillCycle" runat="server" Text='<%# Eval("LastBillCycle") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Society">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblSociety" runat="server" CssClass="label label-info" CommandName="TotalSociety" CommandArgument='<%# Eval("Office_ID") %>' Text='<%# Eval("TotalSociety") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </fieldset>
                </div>
                <div class="col-md-6">
                    <fieldset>
                        <legend>Plant Operation</legend>
                        <asp:GridView ID="GrdPlantOperation" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                            EmptyDataText="No Record Found.">
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
                                  <asp:TemplateField HeaderText="Qty (In KG)" ItemStyle-Width="40%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMC_QtyInKg" runat="server" Text='<%# Eval("MC_QtyInKg") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLastDate" runat="server" Text='<%# Eval("LastDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </fieldset>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <fieldset>
                        <legend>Inventory</legend>
                        <asp:GridView ID="GrdInventory" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                            EmptyDataText="No Record Found.">
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
                                <asp:TemplateField HeaderText="No of Indent" ItemStyle-Width="40%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCount" runat="server" Text='<%# Eval("indent_count") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLastDate" runat="server" Text='<%# Eval("LastDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </fieldset>
                </div>
                <div class="col-md-6">
                    <fieldset>
                        <legend>Finance</legend>

                        <asp:GridView ID="GrdFinance" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                            EmptyDataText="No Record Found.">
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

                                <asp:TemplateField HeaderText="Last Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLastDate" runat="server" Text='<%# Eval("LastDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </fieldset>
                </div>
                <div class="col-md-12">
                    <div id="divDashboard" runat="server"></div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <fieldset>
                        <legend>Payroll</legend>
                        <asp:GridView ID="GrdPayroll" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                            EmptyDataText="No Record Found.">
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
                                <asp:TemplateField HeaderText="Salary Month">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLastDate" runat="server" Text='<%# Eval("Salary_Month") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </fieldset>
                </div>
				<div class="col-md-6">
                    <fieldset>
                        <legend>Online Producer Milk Collection Record</legend>
                        <asp:GridView ID="gvOnlineCollection" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                            EmptyDataText="No Record Found.">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="DS Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Entry Type" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEntryType" runat="server" Text='<%# Eval("EntryType") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
								  <asp:TemplateField HeaderText="Total Producer Registered">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProducer" runat="server" Text='<%# Eval("ProducerCount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Society Count">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSocietyCount" runat="server" Text='<%# Eval("SocietyCount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderText="Producer Milk Count">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalProducerMilkCollection" runat="server" Text='<%# Eval("ProducerMilkCount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLastDate" runat="server" Text='<%# Eval("LastDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </fieldset>
                </div>
            </div>
        </section>

    </div>
    <div class="modal" id="ViewDetailsModel">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true"></span>
                    </button>
                    <h5 style="color: red; font-weight: bold;" id="h5" runat="server"></h5>
                </div>
                <div class="modal-body">
                    <div id="divitem" runat="server">
                        <asp:Label ID="lblModalMsg" runat="server" Text=""></asp:Label>
                        <div class="row">
                            <div class="col-md-12">
                                <fieldset class="box-body">
                                    <legend id="MCMS" runat="server">File Format Detail</legend>
                                    <div class="row" style="overflow: scroll;">
                                        <div class="col-md-12" id="pnl_Repeater">
                                            <div class="table-responsive">
                                                <div class="col-md-12">
                                                    <asp:GridView ID="gvMCMSCC" runat="server" EmptyDataText="No Record Found."
                                                        class="table table-striped table-bordered" AllowPaging="false"
                                                        AutoGenerateColumns="False" EnableModelValidation="True">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="CC Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOffice_Name" Text='<%#Eval("Office_Name")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:GridView ID="GridView1" runat="server" EmptyDataText="No Record Found."
                                                        class="table table-striped table-bordered" AllowPaging="false"
                                                        AutoGenerateColumns="False" EnableModelValidation="True">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="CC Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOffice_Name" Text='<%#Eval("Office_Name")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Gatepass No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblC_ReferenceNo" Text='<%#Eval("C_ReferenceNo")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:GridView ID="GridView2" runat="server" EmptyDataText="No Record Found."
                                                        class="table table-striped table-bordered" AllowPaging="false"
                                                        AutoGenerateColumns="False" EnableModelValidation="True">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="CC Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOffice_Name" Text='<%#Eval("Office_Name")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Challan No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblV_ReferenceCode" Text='<%#Eval("V_ReferenceCode")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:GridView ID="GridView3" runat="server" EmptyDataText="No Record Found."
                                                        class="table table-striped table-bordered" AllowPaging="false"
                                                        AutoGenerateColumns="False" EnableModelValidation="True">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Society Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOffice_Name" Text='<%#Eval("Office_Name")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Last Bill Cycle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLastBillCycle" Text='<%#Eval("LastBillCycle")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:GridView ID="GridView4" runat="server" EmptyDataText="No Record Found."
                                                        class="table table-striped table-bordered" AllowPaging="false"
                                                        AutoGenerateColumns="False" EnableModelValidation="True">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Society Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOffice_Name" Text='<%#Eval("Office_Name")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Milk Collection">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMilkCollection" Text='<%#Eval("MilkCollection")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:GridView ID="GridView5" runat="server" EmptyDataText="No Record Found."
                                                        class="table table-striped table-bordered" AllowPaging="false"
                                                        AutoGenerateColumns="False" EnableModelValidation="True">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItemName" Text='<%#Eval("ItemName")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Demand (In Packets)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCurrent_Demand_InPkt" Text='<%#Eval("Current_Demand_InPkt")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
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
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function myFileModal() {
            $("#ViewDetailsModel").modal('show');
        }
    </script>
</asp:Content>
