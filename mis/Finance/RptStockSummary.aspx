﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="RptStockSummary.aspx.cs" Inherits="mis_Finance_RptStockSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="css/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="css/jquery.dataTables.min.css" rel="stylesheet" />
    <style>
        .show_detail {
            margin-top: 21px;
        }

        .table > tbody > tr > th {
            padding: 5px;
        }

        a:hover {
            color: red;
        }

        /*tr:hover td {
            background-color: #fefefe !important;
        }*/
        table.dataTable tbody td, table.dataTable thead td {
            padding: 5px 5px !important;
        }

        table.dataTable tbody th, table.dataTable thead th {
            padding: 8px 10px !important;
        }

        table.dataTable thead th, table.dataTable thead td {
            padding: 5px 7px;
            border-bottom: none !important;
        }

        table.dataTable tfoot th, table.dataTable tfoot td {
            border-bottom: none !important;
        }

        table.dataTable.no-footer {
            border-bottom: none !important;
        }

        a.dt-button.buttons-collection.buttons-colvis, a.dt-button.buttons-collection.buttons-colvis:hover {
            background: #EF5350;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.dt-button.buttons-excel.buttons-html5, a.dt-button.buttons-excel.buttons-html5:hover {
            background: #ff5722c2;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.dt-button.buttons-print, a.dt-button.buttons-print:hover {
            background: #e91e639e;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
        }

        thead tr th {
            background: #9e9e9ea3 !important;
        }

        /*tbody tr td:not(:first-child), tfoot tr td:not(:first-child) {
            text-align: right;
        }*/

        .Dtime {
            display: none;
        }

        @media print {
            .hide_print, .main-footer, .dt-buttons, .dataTables_filter {
                display: none;
            }

            tfoot, thead {
                display: table-row-group;
                bottom: 0;
            }

            .Dtime {
                display: block;
            }
        }

        /*table.dataTable tbody td, table.dataTable thead td {
            font-weight: 600 !important;
            font-family: 'Montserrat';
            font-size: 10px;
        }*/

        table.dataTable tbody td, table.dataTable thead td {
            font-weight: 600 !important;
            font-family: system-ui;
            font-size: 12px;
            line-height: 16px;
        }

        table.dataTable tbody th, table.dataTable thead th {
            padding: 1px !important;
            font-size: 12px;
            font-family: system-ui;
        }

        table.dataTable, table {
            border-collapse: collapse !important;
        }

            table.dataTable tbody tr th {
                word-wrap: break-word;
                word-break: break-all;
            }

            table.dataTable tfoot th, table.dataTable tfoot td {
                padding: 1px;
                border-top: 1px solid #111;
                font-family: system-ui;
                font-size: 12px;
            }

        .leftAlign {
            text-align: left !important;
        }

        .rightAlign {
            text-align: right !important;
        }

        .btab {
            font-weight: 500;
            background-color: #674444 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
        <section class="content-header">
            <h1>Stock Summary
                   <small></small>
            </h1>
        </section>
        <section class="content">
            <asp:Label ID="lblTime" runat="server" CssClass="Dtime" Style="font-weight: 800;" Text="" ClientIDMode="Static"></asp:Label>
            <div class="box box-pramod" style="background-color: #FFFFFF;">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset class="box-body">
                                <legend>Stock Summary</legend>
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>From Date<span style="color: red;">*</span></label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" runat="server" placeholder="Select From Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>To Date</label><span style="color: red">*</span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Office Name</label><span style="color: red">*</span>
                                            <asp:ListBox runat="server" ID="ddlOffice" ClientIDMode="Static" CssClass="form-control"></asp:ListBox>
                                            <%-- <asp:ListBox runat="server" ID="ddlOffice" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox> --%>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Group Name</label><span style="color: red">*</span>
                                            <asp:ListBox runat="server" ID="ddlGroup" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Report Type</label><span style="color: red">*</span>
                                            <asp:DropDownList ID="ddlReportType" runat="server" class="form-control">  
                                                 <asp:ListItem Value="Group Wise" Selected="True">Group Wise</asp:ListItem>                                              
                                                <asp:ListItem Value="Item Wise">Item Wise</asp:ListItem>
                                               
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4 hidden">
                                        <div class="form-group">
                                            <br />
                                            <asp:CheckBox ID="chk0TxnOpen" runat="server" Text="0 Transaction Or Opening Display" />

                                        </div>
                                    </div>
                                  
                                    <div class="col-md-6 hidden">
                                        <div class="form-group">
                                            <asp:CheckBox ID="chkconsumption" runat="server" Text="&nbsp;Consumption, &nbsp;Gross Profit and &nbsp;Percentage(%)" Checked="false" />
                                        </div>
                                    </div>
                                    <div class="col-md-2 hidden">
                                        <div class="form-group">
                                            <asp:CheckBox ID="chkNarration" runat="server" Text="Narration" Checked="false" />
                                        </div>
                                    </div>
                                    <div class="col-md-2 hidden">
                                        <div class="form-group">
                                            <asp:CheckBox ID="chkBillByBill" runat="server" Text="Bill By Bill" Checked="false" />
                                        </div>
                                    </div>

                                    <%--                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:CheckBox ID="chkGrossProfit" runat="server" Text="&nbsp;Gross Profit" Checked="false" />
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:CheckBox ID="chkPercentage" runat="server" Text="&nbsp;Percentage(%)" Checked="false" />
                                        </div>
                                    </div>--%>
                                  

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button ID="btn" CssClass="btn btn-md btn-primary show_detail" runat="server" Text="Show Stock Summary" OnClick="btn_Click" OnClientClick="return validateform();" />

                                            <%--<asp:Button ID="Button1" CssClass="btn btn-md clearbutton" runat="server" Text="Show Report" OnClick="btn_Click" />--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Label ID="lblprinttext" CssClass="printtext" ToolTip="" ClientIDMode="Static" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="lblexceltext" CssClass="exceltext" ToolTip="" ClientIDMode="Static" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>


                                <!------ Grid 1 Group Wise Inventory--->

                                <div class="row" id="DivGrid1" runat="server">
                                    <div class="col-md-12">
                                        <fieldset class="box-body">
                                            <div class="table-responsive FirstPrint" stye="max-height:700px;">
                                                <asp:HyperLink ID="hyperlink1" runat="server" NavigateUrl="ItemCostingMethod.aspx" CssClass="badge bg-teal hidden" Visible="false">Click Here to Change Item's Costing Method</asp:HyperLink>

                                                <asp:Label ID="lblheadingmain" runat="server" Text=""></asp:Label>
                                                <asp:GridView ID="GridView1" runat="server" ShowFooter="True" AutoGenerateColumns="False" class="GridView1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ShowHeaderWhenEmpty="True" EmptyDataText="No Record Found">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Particular" ItemStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkbtnParticular" CausesValidation="false" CommandName="Select" Text='<%# Eval("Particular") %>' ToolTip='<%# Eval("ItemType_Id") %>' runat="server" CssClass="Aselect1">LinkButton</asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ControlStyle Font-Bold="True" />
                                                            <FooterStyle BackColor="#EAEAEA" Font-Bold="True" />
                                                            <HeaderStyle BackColor="#EAEAEA" />
                                                            <ItemStyle BackColor="#EAEAEA" Font-Bold="True" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="OpeningQuantity" HeaderText="Opening Quantity" HeaderStyle-BackColor="Red" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#FFFFCC" />
                                                            <HeaderStyle BackColor="#FFFFCC" />
                                                            <ItemStyle BackColor="#FFFFCC" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OpeningRate" HeaderText="Opening Rate" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#FFFFCC" />
                                                            <HeaderStyle BackColor="#FFFFCC" />
                                                            <ItemStyle BackColor="#FFFFCC" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OpeningValue" HeaderText="Opening Value" ItemStyle-HorizontalAlign="Right">

                                                            <FooterStyle BackColor="#FFFFCC" />
                                                            <HeaderStyle BackColor="#FFFFCC" />
                                                            <ItemStyle BackColor="#FFFFCC" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="InwardQuantity" HeaderText="Inward Quantity" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#cfdcc1" />
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="InwardRate" HeaderText="Inward Rate" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#cfdcc1" />
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="InwardValue" HeaderText="Inward Value" ItemStyle-HorizontalAlign="Right">

                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#cfdcc1" />
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="OutwardQuantity" HeaderText="Outward Quantity" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OutwardRate" HeaderText="Outward Rate" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OutwardValue" HeaderText="Outward Value" ItemStyle-HorizontalAlign="Right">

                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Consumption" HeaderText="Consumption" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="GrossProfit" HeaderText="Gross Profit" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Percentage" HeaderText="Percentage(%)" ItemStyle-HorizontalAlign="Right">

                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>


                                                        <asp:BoundField DataField="ClosingQuantity" HeaderText="Closing Quantity" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#FFCC99" />
                                                            <HeaderStyle BackColor="#FFCC99" />
                                                            <ItemStyle BackColor="#FFCC99" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ClosingRate" HeaderText="Closing Rate">
                                                            <FooterStyle BackColor="#FFCC99" />
                                                            <HeaderStyle BackColor="#FFCC99" />
                                                            <ItemStyle BackColor="#FFCC99" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ClosingValue" HeaderText="Closing Value" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#FFCC99" />
                                                            <HeaderStyle BackColor="#FFCC99" />
                                                            <ItemStyle BackColor="#FFCC99" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                                <!------ Grid 2 Item Wise Inventory--->

                                <div class="row" id="DivGrid2" runat="server">
                                    <div class="col-md-12">
                                        <fieldset class="box-body">
                                            <div class="table-responsive">
                                                <asp:HyperLink ID="hyperlink" runat="server" NavigateUrl="ItemCostingMethod.aspx" CssClass="badge bg-teal" Visible="false">Click Here to Change Item's Costing Method</asp:HyperLink>
                                                <asp:Label ID="lblheadingFirst" runat="server" Text=""></asp:Label>
                                                <asp:GridView ID="GridView2" runat="server" ShowFooter="True" AutoGenerateColumns="False" class="GridView2" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" ShowHeaderWhenEmpty="True" EmptyDataText="No Record Found">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Particular" ItemStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkbtnParticular" CausesValidation="false" CommandName="Select" Text='<%# Eval("Particular") %>' ToolTip='<%# Eval("Item_Id") %>' runat="server" CssClass="Aselect1">LinkButton</asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ControlStyle Font-Bold="True" />
                                                            <FooterStyle BackColor="#EAEAEA" Font-Bold="True" />
                                                            <HeaderStyle BackColor="#EAEAEA" />
                                                            <ItemStyle BackColor="#EAEAEA" Font-Bold="True" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="OpeningQuantity" HeaderText="Opening Quantity" HeaderStyle-BackColor="Red" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#FFFFCC" />
                                                            <HeaderStyle BackColor="#FFFFCC" />
                                                            <ItemStyle BackColor="#FFFFCC" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OpeningRate" HeaderText="Opening Rate" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#FFFFCC" />
                                                            <HeaderStyle BackColor="#FFFFCC" />
                                                            <ItemStyle BackColor="#FFFFCC" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OpeningValue" HeaderText="Opening Value" ItemStyle-HorizontalAlign="Right">

                                                            <FooterStyle BackColor="#FFFFCC" />
                                                            <HeaderStyle BackColor="#FFFFCC" />
                                                            <ItemStyle BackColor="#FFFFCC" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="InwardQuantity" HeaderText="Inward Quantity" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#cfdcc1" />
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="InwardRate" HeaderText="Inward Rate" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#cfdcc1" />
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="InwardValue" HeaderText="Inward Value" ItemStyle-HorizontalAlign="Right">

                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#cfdcc1" />
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="OutwardQuantity" HeaderText="Outward Quantity" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OutwardRate" HeaderText="Outward Rate" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OutwardValue" HeaderText="Outward Value" ItemStyle-HorizontalAlign="Right">

                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Consumption" HeaderText="Consumption" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="GrossProfit" HeaderText="Gross Profit" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Percentage" HeaderText="Percentage(%)" ItemStyle-HorizontalAlign="Right">

                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ClosingQuantity" HeaderText="Closing Quantity" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#FFCC99" />
                                                            <HeaderStyle BackColor="#FFCC99" />
                                                            <ItemStyle BackColor="#FFCC99" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ClosingRate" HeaderText="Closing Rate" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#FFCC99" />
                                                            <HeaderStyle BackColor="#FFCC99" />
                                                            <ItemStyle BackColor="#FFCC99" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ClosingValue" HeaderText="Closing Value" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#FFCC99" />
                                                            <HeaderStyle BackColor="#FFCC99" />
                                                            <ItemStyle BackColor="#FFCC99" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                                <!------ Grid 3 Daily, Monthly And Yearly BreakUp--->

                                <div class="row" id="DivGrid3" runat="server" visible="false">
                                    <div class="col-md-12">
                                        <fieldset class="box-body">
                                            <div class="table-responsive">
                                                <p class="text-center hidden">

                                                    <asp:Button ID="btnDaily" runat="server" Text="Daily Break-Up" CssClass="btn btn-warning btn-sm btn-flat" OnClick="btnDaily_Click" />

                                                    <asp:Button ID="btnMonthly" runat="server" Text="Monthly Break-Up" CssClass="btn btn-warning btn-sm  btn-flat" OnClick="btnMonthly_Click" />

                                                    <asp:Button ID="btnQuarterly" runat="server" Text="Quarterly Break-Up" CssClass="btn btn-warning btn-sm  btn-flat" OnClick="btnQuarterly_Click" />

                                                </p>

                                                <asp:Label ID="lblItemId" runat="server" Text="" ToolTip="" CssClass="hidden"></asp:Label>

                                                <asp:Label ID="lblheadingSecond" runat="server" Text=""></asp:Label>
                                                <asp:GridView ID="GridView3" runat="server" ShowFooter="True" AutoGenerateColumns="False" class="GridView3" OnSelectedIndexChanged="GridView3_SelectedIndexChanged" ShowHeaderWhenEmpty="True" EmptyDataText="No Record Found">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Particular" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkbtnParticular" CausesValidation="false" CommandName="Select" Text='<%# Eval("Particular") %>' ToolTip='<%# Eval("MonthDates") %>' runat="server" CssClass="Aselect1">LinkButton</asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ControlStyle Font-Bold="True" />
                                                            <FooterStyle BackColor="#EAEAEA" Font-Bold="True" />
                                                            <HeaderStyle BackColor="#EAEAEA" />
                                                            <ItemStyle BackColor="#EAEAEA" Font-Bold="True" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="OpeningQuantity" HeaderText="Opening Quantity" HeaderStyle-BackColor="Red" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#FFFFCC" />
                                                            <HeaderStyle BackColor="#FFFFCC" />
                                                            <ItemStyle BackColor="#FFFFCC" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OpeningRate" HeaderText="Opening Rate" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#FFFFCC" />
                                                            <HeaderStyle BackColor="#FFFFCC" />
                                                            <ItemStyle BackColor="#FFFFCC" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OpeningValue" HeaderText="Opening Value" ItemStyle-HorizontalAlign="Right">

                                                            <FooterStyle BackColor="#FFFFCC" />
                                                            <HeaderStyle BackColor="#FFFFCC" />
                                                            <ItemStyle BackColor="#FFFFCC" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="InwardQuantity" HeaderText="Inward Quantity" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#cfdcc1" />
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="InwardRate" HeaderText="Inward Rate" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#cfdcc1" />
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="InwardValue" HeaderText="Inward Value" ItemStyle-HorizontalAlign="Right">

                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#cfdcc1" />
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="OutwardQuantity" HeaderText="Outward Quantity" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OutwardRate" HeaderText="Outward Rate" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OutwardValue" HeaderText="Outward Value" ItemStyle-HorizontalAlign="Right">

                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="ClosingQuantity" HeaderText="Closing Quantity" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#FFCC99" />
                                                            <HeaderStyle BackColor="#FFCC99" />
                                                            <ItemStyle BackColor="#FFCC99" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ClosingRate" HeaderText="Closing Rate" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#FFCC99" />
                                                            <HeaderStyle BackColor="#FFCC99" />
                                                            <ItemStyle BackColor="#FFCC99" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ClosingValue" HeaderText="Closing Value" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#FFCC99" />
                                                            <HeaderStyle BackColor="#FFCC99" />
                                                            <ItemStyle BackColor="#FFCC99" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                                <!------ Grid 4 For Last Voucher Page--->
                                <div class="row" id="DivGrid4" runat="server" visible="false">
                                    <div class="col-md-12">
                                        <fieldset class="box-body">
                                            <div class="table-responsive">
                                                <asp:Label ID="lblheadingThird" runat="server" Text=""></asp:Label>
                                                <asp:GridView ID="GridView4" runat="server" ShowFooter="True" AutoGenerateColumns="False" class="GridView4" ShowHeaderWhenEmpty="True" EmptyDataText="No Record Found">
                                                    <Columns>
                                                        <asp:BoundField DataField="TranDt2" HeaderText="Date" />
                                                        <asp:TemplateField HeaderText="Particular">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkbtnParticular" CausesValidation="false" CommandName="Select" Text='<%# Eval("Particular") %>' ToolTip='<%# Eval("Particular") %>' runat="server" CssClass="Aselect1">LinkButton</asp:LinkButton>

                                                                <asp:Label ID="lblBillByBill" Text='' runat="server" Style="font-size: 11px;" />
                                                                <asp:Label ID="lblNarration" Text='' runat="server" Style="font-size: 11px;" />
                                                                <asp:Label ID="lblVoucherTx_ID" runat="server" CssClass="hidden" Text='<%# Eval("TransactionID") %>' />
                                                            </ItemTemplate>
                                                            <ControlStyle Font-Bold="True" />
                                                            <FooterStyle BackColor="#EAEAEA" Font-Bold="True" />
                                                            <HeaderStyle BackColor="#EAEAEA" />
                                                            <ItemStyle BackColor="#EAEAEA" Font-Bold="True" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="VoucherType" HeaderText="Voucher Type" ItemStyle-CssClass="" />
                                                        <asp:BoundField DataField="VoucherNo" HeaderText="Voucher No" />
                                                        <asp:BoundField DataField="Office_Name" HeaderText="Office Name" Visible="false" />
                                                        <asp:BoundField DataField="InwardQuantity" HeaderText="Inward Quantity">
                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#cfdcc1" />
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="InwardRate" HeaderText="Inward Rate" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#cfdcc1" />
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="InwardValue" HeaderText="Inward Value" ItemStyle-HorizontalAlign="Right">

                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#cfdcc1" />
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="OutwardQuantity" HeaderText="Outward Quantity" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OutwardRate" HeaderText="Outward Rate" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OutwardValue" HeaderText="Outward Value" ItemStyle-HorizontalAlign="Right">

                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Consumption" HeaderText="Consumption" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="GrossProfit" HeaderText="Gross Profit" ItemStyle-HorizontalAlign="Right">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Percentage" HeaderText="Percentage(%)" ItemStyle-HorizontalAlign="Right">

                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>

                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="13%">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="hpView" runat="server" Target="_blank" NavigateUrl='<%# Eval("PageURL").ToString()+ "?VoucherTx_ID=" + APIProcedure.Client_Encrypt(Eval("TransactionID").ToString())+"&Action="+ APIProcedure.Client_Encrypt("1")+"&Office_ID="+ APIProcedure.Client_Encrypt(Eval("Office_ID").ToString()) %>' CssClass="label label-info" Visible='<%# Eval("PageURL").ToString()=="" ? false: true %>'>View</asp:HyperLink>

                                                                <asp:HyperLink ID="hpEdit" runat="server" Target="_blank" NavigateUrl='<%# Eval("PageURL").ToString()+ "?VoucherTx_ID=" + APIProcedure.Client_Encrypt(Eval("TransactionID").ToString())+"&Action="+ APIProcedure.Client_Encrypt("2")+"&Office_ID="+ APIProcedure.Client_Encrypt(Eval("Office_ID").ToString()) %>' CssClass="label label-info" Visible='<%# Eval("PageURL").ToString()=="" ? false: true %>'>Edit</asp:HyperLink>


                                                                <asp:Label ID="lblOfficeID" CssClass="hidden" Text='<%# Eval("Office_ID").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                                <div class="row" runat="server" id="divTBData">
                                    <div class="col-md-3"></div>
                                    <div class="col-md-6">
                                        <table class="table table-bordered" style="font-weight: 700">
                                            <thead>
                                                <tr>
                                                    <th colspan="3" style="text-align: center">Transaction As Per Trial Balance</th>
                                                </tr>
                                                <tr>
                                                    <th>Group Name</th>
                                                    <th>Debit</th>
                                                    <th>Credit</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptTBData" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Eval("Head_Name") %></td>
                                                            <td class="rightAlign"><%# Eval("DebitAmt") %></td>
                                                            <td class="rightAlign"><%# Eval("CreditAmt") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-md-3"></div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
        </section>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <%-- start data table--%>
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/dataTables.bootstrap.min.js"></script>
    <script src="js/dataTables.buttons.min.js"></script>
    <script src="js/buttons.flash.min.js"></script>
    <script src="js/jszip.min.js"></script>
    <script src="js/pdfmake.min.js"></script>
    <script src="js/vfs_fonts.js"></script>
    <script src="js/buttons.html5.min.js"></script>
    <script src="js/buttons.print.min.js"></script>
    <script src="js/buttons.colVis.min.js"></script>
    <script>

        //alert($('.printtext').attr('title'));

        $('.datatable').DataTable({
            paging: true,
            autoWidth: false,
            columnDefs: [{
                //targets: 'no-sort',
                orderable: true
            }],
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    //title: $('h1').text(),
                    title: $('.printtext').attr('title'),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    //title: $('h1').text(),
                    title: $('.exceltext').attr('title'),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6]
                    },
                    footer: true
                }],
                dom: {
                    container: {
                        className: 'dt-buttons'
                    },
                    button: {
                        className: 'btn btn-default'
                    }
                }
            }
        });

        //end data table

        $('#txtFromDate').change(function () {
            //debugger;
            var start = $('#txtFromDate').datepicker('getDate');
            var end = $('#txtToDate').datepicker('getDate');
            if ($('#txtToDate').val() != "") {
                if (start > end) {
                    if ($('#txtFromDate').val() != "") {
                        alert("From date should not be greater than To Date.");
                        $('#txtFromDate').val("");
                    }
                }
            }
        });
        $('#txtToDate').change(function () {
            //debugger;
            var start = $('#txtFromDate').datepicker('getDate');
            var end = $('#txtToDate').datepicker('getDate');
            if (start > end) {
                if ($('#txtToDate').val() != "") {
                    alert("To Date can not be less than From Date.");
                    $('#txtToDate').val("");
                }
            }

        });


        function validateform() {
            var msg = "";
            if (document.getElementById('<%=txtFromDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select From Date. \n";
            }

            if (document.getElementById('<%=txtToDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select To Date. \n";
            }
            var Fromday = 0;
            var FromMonth = 0;
            var FromYear = 0;
            var Today = 0;
            var ToMonth = 0;
            var ToYear = 0;
            var y = document.getElementById("txtFromDate").value; //This is a STRING, not a Date
            if (y != "") {
                var dateParts = y.split("/");   //Will split in 3 parts: day, month and year
                var yday = dateParts[0];
                var ymonth = dateParts[1];
                var yyear = dateParts[2];

                Fromday = dateParts[0];
                FromMonth = dateParts[1];
                FromYear = dateParts[2];

                var yd = new Date(yyear, parseInt(ymonth, 10) - 1, yday);
            }
            else {
                var yd = "";
            }

            var z = document.getElementById("txtToDate").value; //This is a STRING, not a Date
            if (z != "") {
                var dateParts = z.split("/");   //Will split in 3 parts: day, month and year
                var zday = dateParts[0];
                var zmonth = dateParts[1];
                var zyear = dateParts[2];

                Today = dateParts[0];
                ToMonth = dateParts[1];
                ToYear = dateParts[2];

                var zd = new Date(zyear, parseInt(zmonth, 10) - 1, zday);
            }
            else {
                var zd = "";
            }
            if (yd != "" && zd != "") {
                if (yd > zd) {
                    msg += "To Date should be greater than From Date ";
                }
                else {

                    if ((FromYear == ToYear - 1) || (FromYear == ToYear)) {
                        //if (FromYear == ToYear && ToMonth <= 12 && ToMonth > 3 && FromMonth >= 4) {
                        //}
                        //if (FromYear == ToYear && FromMonth <= 3 && ToMonth <= 3) {
                        //}
                        //else if (FromYear < ToYear && ToMonth <= 3 && FromMonth >= 4) {
                        //}
                        if (FromYear == ToYear && FromMonth <= 3 && ToMonth <= 3) {
                        }
                        else if (FromYear == ToYear && FromMonth >= 4 && ToMonth <= 12) {
                        }
                        else if (FromYear != ToYear && FromMonth > 3 && ToMonth <= 3) {
                        }
                        else {
                            msg += "Selection of Dates (From Date - To Date) should be between Financial Year.";
                        }
                    }
                    else {
                        msg += "Selection of Dates (From Date - To Date) should be between Financial Year.";
                    }

                }
            }
           <%-- if (document.getElementById('<%=ddlOffice.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Office. \n";
            }--%>
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                document.querySelector('.popup-wrapper').style.display = 'block';
                return true;
            }

        }


    </script>
    <link href="../../../mis/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../mis/js/bootstrap-multiselect.js" type="text/javascript"></script>
    <script>
        $('.GridView4, .GridView2, .GridView1').DataTable({
            paging: false,
            ordering: true,
            autoWidth: false,
            oSearch: { bSmart: false, bRegex: true },
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'colvis',
                    collectionLayout: 'fixed two-column',
                    text: '<i class="fa fa-eye"></i> Columns'
                }, {
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    //title: $('h1').text(),
                    title: $('.printtext').attr('title'),
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    //title: $('h1').text(),
                    title: $('.exceltext').attr('title'),
                    footer: true
                }
            ]
        });


        $('.GridView3').DataTable({
            paging: false,
            ordering: false,
            autoWidth: false,
            oSearch: { bSmart: false, bRegex: true },
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'colvis',
                    collectionLayout: 'fixed two-column',
                    text: '<i class="fa fa-eye"></i> Columns'
                }, {
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    //title: $('h1').text(),
                    title: $('.printtext').attr('title'),
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    //title: $('h1').text(),
                    title: $('.exceltext').attr('title'),
                    footer: true
                }
            ]
        });
    </script>
    <script>

        $(function () {
            $('[id*=ddlOffice]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });


        });
        $(function () {
            $('[id*=ddlGroup]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });
        });

    </script>
    <script>
        $('table tr td').each(function () {
            if ($(this).text() == '0') {
                $(this).css('color', 'red');
            }
        });
    </script>


    <style>
        .multiselect-native-select .multiselect {
            text-align: left !important;
        }

        .multiselect-native-select .multiselect-selected-text {
            width: 100% !important;
        }

        .multiselect-native-select .checkbox, .multiselect-native-select .dropdown-menu {
            width: 100% !important;
            max-height: 200px;
        }

        .multiselect-native-select .btn .caret {
            float: right !important;
            vertical-align: middle !important;
            margin-top: 8px;
            border-top: 6px dashed;
        }

        ul.multiselect-container.dropdown-menu {
            overflow-y: scroll;
            overflow-x: hidden;
        }
    </style>
</asp:Content>
