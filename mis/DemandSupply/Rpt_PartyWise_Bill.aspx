<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Rpt_PartyWise_Bill.aspx.cs" Inherits="mis_DemandSupply_Rpt_PartyWise_Bill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <style>
        .NonPrintable {
            display: none;
        }

        @media print {
            .NonPrintable {
                display: block;
            }

            .noprint {
                display: none;
            }

            .pagebreak {
                page-break-before: always;
            }
        }


        .table1-bordered > thead > tr > th, .table1-bordered > tbody > tr > th, .table1-bordered > tfoot > tr > th, .table1-bordered > thead > tr > td, .table1-bordered > tbody > tr > td, .table1-bordered > tfoot > tr > td {
            border: 1px solid #000000 !important;
        }

        .thead {
            display: table-header-group;
        }

        .text-center {
            text-align: center;
        }

        .text-right {
            text-align: right;
        }

        .table1 > tbody > tr > td, .table1 > tbody > tr > th, .table1 > tfoot > tr > td, .table1 > tfoot > tr > th, .table1 > thead > tr > td, .table1 > thead > tr > th {
            padding: 5px;
            font-size: 15px;
            text-align: center;
        }

       .tmptd td {
            word-break: break-word;
            padding:3px;
            min-width:90px !important;
        }
    </style>
    <div class="loader"></div>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content noprint">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Partywise Bill Report</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">

                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <fieldset>
                                    <legend>Partywise Bill Report
                                    </legend>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>From Date<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a" ControlToValidate="txtFromDate"
                                                    ErrorMessage="Enter mDate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtFromDate"
                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtFromDate" MaxLength="10" placeholder="Enter FromDate" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>To Date<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a" ControlToValidate="txttodate"
                                                    ErrorMessage="Enter mDate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txttodate"
                                                    ErrorMessage="To Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txttodate" MaxLength="10" placeholder="Enter FromDate" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Location</label>
                                            <asp:DropDownList ID="ddlLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Route</label>
                                            <asp:DropDownList ID="ddlRoute" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <%-- <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Distributor/Superstockist Name</label>
                                            <asp:DropDownList ID="ddlDitributor" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>--%>
                                    <div class="col-md-1">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:Button runat="server" OnClientClick="return ValidatePage();" CssClass="btn btn-block btn-primary" ValidationGroup="a" OnClick="btnSearch_Click" ID="btnSearch" Text="Search" AccessKey="S" />
                                        </div>
                                    </div>
                                    <div class="col-md-1" style="margin-top: 20px;">
                                        <div class="form-group">
                                            <asp:Button ID="btnClear" OnClick="btnClear_Click" runat="server" Text="Clear" CssClass="btn btn-default" />
                                        </div>
                                    </div>


                                </fieldset>
                            </div>
                            <%--<div class="row">
                                <div class="col-md-1" style="margin-top: 20px;">
                                    <div class="form-group">
                                        <asp:Button ID="btnPrintRoutWise" Visible="false" runat="server" CssClass="btn btn-success" Text="Print" OnClick="btnPrintRoutWise_Click" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">

                                            <div id="div_page_content" runat="server" class="page_content"></div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">

                                            <div id="div_page_contnt2" runat="server" class="page_content"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12" id="pnlData" runat="server" visible="false">

                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Party Wise Report</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblReportMsg" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-2 pull-right">
                                    <div class="form-group">
                                        <asp:Button ID="btnprint" runat="server" CssClass="btn btn-primary" Text="Print" OnClick="btnprint_Click" />
                                        <asp:Button ID="btnExportAll" runat="server" CssClass="btn btn-success" OnClick="btnExportAll_Click" Text="Export" />
                                    </div>

                                </div>
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <div id="div_page_content" runat="server" class="page_content"></div>
                                        <%-- <div id="ExportAllData" runat="server"></div>--%>
                                        <%--  <div id="divtable" runat="server" class="page_content"></div>--%>
                                    </div>
                                </div>
                                <div class="col-md-12">

                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GridView1" Visible="false" runat="server" class="table table-striped table-bordered" AllowPaging="false"
                                                    AutoGenerateColumns="false" EmptyDataText="No Record Found." DataKeyNames="ProductPaymentSheet_ID" EnableModelValidation="True">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                <asp:Label Visible="false" ID="Payment_id" Text='<%#Eval("ProductPaymentSheet_ID") %>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Opening" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOpening" Text='<%#Eval("Opening") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="BillNo" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBillNo" Text='<%#Eval("BillNo") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText=" Bill Date" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbill_Date" Text='<%#Eval("Payment_Date") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DM NO." HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDM_no" Text='<%#Eval("DMChallanNo") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Party Code" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDCode" Text='<%#Eval("DCode") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Party Name" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDName" Text='<%#Eval("DName") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Bill Amount" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTotalBillAmount" Text='<%#Eval("TotalBillAmount") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Pay Mode 1" DataField="PaymentModeId1" />
                                                        <asp:BoundField HeaderText="Pay No. 1" DataField="PaymentNo1" />
                                                        <asp:BoundField HeaderText="Pay Amount 1" DataField="PaymentAmount1" />
                                                         <asp:BoundField HeaderText="Pay Date 1" DataField="PaymentDate1" />


                                                        <asp:BoundField HeaderText="Pay Mode 2" DataField="PaymentModeId2" />
                                                        <asp:BoundField HeaderText="Pay No. 2" DataField="PaymentNo2" />
                                                        <asp:BoundField HeaderText="Pay Amount 2" DataField="PaymentAmount2" />
                                                        <asp:BoundField HeaderText="Pay Date 2" DataField="PaymentDate2" />

                                                        <asp:BoundField HeaderText="Pay Mode 3" DataField="PaymentModeId3" />
                                                        <asp:BoundField HeaderText="Pay No. 3" DataField="PaymentNo3" />
                                                        <asp:BoundField HeaderText="Pay Amount 3" DataField="PaymentAmount3" />
                                                         <asp:BoundField HeaderText="Pay Date 3" DataField="PaymentDate3" />

                                                         <asp:BoundField HeaderText="Pay Mode 4" DataField="PaymentModeId4" />
                                                        <asp:BoundField HeaderText="Pay No. 4" DataField="PaymentNo4" />
                                                        <asp:BoundField HeaderText="Pay Amount 4" DataField="PaymentAmount4" />
                                                         <asp:BoundField HeaderText="Pay Date 4" DataField="PaymentDate4" />

                                                        <asp:BoundField HeaderText="Closing" DataField="closing" />
                                                        <%-- <asp:TemplateField HeaderText="Total GST Amount" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTotalGSTAmount" Text='<%#Eval("TotalGSTAmount") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <%--<asp:TemplateField HeaderText="Route" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRName" Text='<%#Eval("RName") %>' runat="server"></asp:Label>
                                                                 <asp:Label ID="lblAreaId" Visible="false" Text='<%#Eval("AreaId") %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblRouteId" Visible="false" Text='<%#Eval("RouteId") %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblDistributorId" Visible="false" Text='<%#Eval("DistributorId") %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblSuperStockistId" Visible="false" Text='<%#Eval("SuperStockistId") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Distributor / SuperStockist Name" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDName" Text='<%#Eval("SuperStockistId").ToString()=="" ? Eval("DName").ToString() : ""  %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("ProductPaymentSheet_ID") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                    </Columns>
                                                </asp:GridView>

                                            </div>
                                        </div>

                                        <%-- <div class="col-md-12" id="pnltotalcrate" runat="server" visible="false">
                                            <label>Total Crate Required : </label>
                                            <asp:Label ID="lblTotalCrateValue" Font-Bold="true" runat="server"></asp:Label>

                                        </div>--%>
                                    </div>
                                </div>

                                <div class="col-md-12" style="display: none">

                                    <div class="row">
                                        <div class="col-md-12">

                                            <asp:GridView ID="GridView2" runat="server"
                                                AutoGenerateColumns="false" EmptyDataText="No Record Found." DataKeyNames="ProductPaymentSheet_ID" EnableModelValidation="True">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                            <asp:Label Visible="false" ID="Payment_id" Text='<%#Eval("ProductPaymentSheet_ID") %>' runat="server" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Opening">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOpening" Text='<%#Eval("Opening") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="BillNo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBillNo" Text='<%#Eval("BillNo") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText=" Bill Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblbill_Date" Text='<%#Eval("Payment_Date") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DM NO.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDM_no" Text='<%#Eval("DMChallanNo") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Party Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDCode" Text='<%#Eval("DCode") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Party Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDName" Text='<%#Eval("DName") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Bill Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalBillAmount" Text='<%#Eval("TotalBillAmount") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Pay Mode 1" DataField="PaymentModeId1" />
                                                        <asp:BoundField HeaderText="Pay No. 1" DataField="PaymentNo1" />
                                                        <asp:BoundField HeaderText="Pay Amount 1" DataField="PaymentAmount1" />
                                                         <asp:BoundField HeaderText="Pay Date 1" DataField="PaymentDate1" />


                                                        <asp:BoundField HeaderText="Pay Mode 2" DataField="PaymentModeId2" />
                                                        <asp:BoundField HeaderText="Pay No. 2" DataField="PaymentNo2" />
                                                        <asp:BoundField HeaderText="Pay Amount 2" DataField="PaymentAmount2" />
                                                        <asp:BoundField HeaderText="Pay Date 2" DataField="PaymentDate2" />

                                                        <asp:BoundField HeaderText="Pay Mode 3" DataField="PaymentModeId3" />
                                                        <asp:BoundField HeaderText="Pay No. 3" DataField="PaymentNo3" />
                                                        <asp:BoundField HeaderText="Pay Amount 3" DataField="PaymentAmount3" />
                                                         <asp:BoundField HeaderText="Pay Date 3" DataField="PaymentDate3" />

                                                         <asp:BoundField HeaderText="Pay Mode 4" DataField="PaymentModeId4" />
                                                        <asp:BoundField HeaderText="Pay No. 4" DataField="PaymentNo4" />
                                                        <asp:BoundField HeaderText="Pay Amount 4" DataField="PaymentAmount4" />
                                                         <asp:BoundField HeaderText="Pay Date 4" DataField="PaymentDate4" />

                                                    <asp:BoundField HeaderText="Closing" DataField="closing" />
                                                    <%-- <asp:TemplateField HeaderText="Total GST Amount" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTotalGSTAmount" Text='<%#Eval("TotalGSTAmount") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                    <%--<asp:TemplateField HeaderText="Route" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRName" Text='<%#Eval("RName") %>' runat="server"></asp:Label>
                                                                 <asp:Label ID="lblAreaId" Visible="false" Text='<%#Eval("AreaId") %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblRouteId" Visible="false" Text='<%#Eval("RouteId") %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblDistributorId" Visible="false" Text='<%#Eval("DistributorId") %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblSuperStockistId" Visible="false" Text='<%#Eval("SuperStockistId") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Distributor / SuperStockist Name" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDName" Text='<%#Eval("SuperStockistId").ToString()=="" ? Eval("DName").ToString() : ""  %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("ProductPaymentSheet_ID") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>


                                        </div>
                                    </div>
                                    <div class="col-md-12" id="pnltotalPDM" runat="server" visible="false">
                                        <label>Total DM :</label>
                                        <asp:Label ID="lblTotalSupplyValue" Font-Bold="true" runat="server"></asp:Label>
                                    </div>
                                    <%-- <div class="col-md-12" id="pnltotalcrate" runat="server" visible="false">
                                            <label>Total Crate Required : </label>
                                            <asp:Label ID="lblTotalCrateValue" Font-Bold="true" runat="server"></asp:Label>

                                        </div>--%>
                                    <div class="col-md-12" id="pnltotalcost" runat="server" visible="false">
                                        <label>Cost Of Product. : </label>
                                        <asp:Label ID="lblCostOfProduct" Font-Bold="true" runat="server"></asp:Label>

                                    </div>

                                    <%-- <div class="col-md-12" id="pnltotalcrate" runat="server" visible="false">
                                            <label>Total Crate Required : </label>
                                            <asp:Label ID="lblTotalCrateValue" Font-Bold="true" runat="server"></asp:Label>

                                        </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                    </div>
                </div>
            </div>
            <%-- </div>--%>
        </section>

        <!-- /.content -->
        <section class="content">
            <div id="Print" runat="server" class="NonPrintable"></div>
        </section>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSearch.ClientID%>').value.trim() == "Update") {
                    <%-- document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";--%>
                    $('#myModal').modal('show');
                    return false;
                }
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
            function myItemDetailsModal() {
                $("#ItemDetailsModal").modal('show');

            }
        }

    </script>
</asp:Content>

