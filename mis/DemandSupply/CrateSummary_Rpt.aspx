<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CrateSummary_Rpt.aspx.cs" Inherits="mis_DemandSupply_CrateSummary_Rpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
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
            border: 1px dashed #000000 !important;
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
            padding: 2px 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content noprint">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Crate Summary Report</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <fieldset>
                                <legend>Crate Summary Report
                                </legend>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>From Date <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                    ErrorMessage="Enter FromDate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter FromDate !'></i>"
                                                    ControlToValidate="txtFromDate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtFromDate"
                                                    ErrorMessage="Invalid FromDate" Text="<i class='fa fa-exclamation-circle' title='Invalid FromDate !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtFromDate" MaxLength="10" placeholder="Select From Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>To Date <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter ToDate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter ToDate !'></i>"
                                                    ControlToValidate="txtToDate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtToDate"
                                                    ErrorMessage="Invalid ToDate" Text="<i class='fa fa-exclamation-circle' title='Invalid ToDate !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtToDate" MaxLength="10" placeholder="Select To Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <%--<div class="col-md-2">
                                        <div class="form-group">
                                            <label>Item Category/वस्तु वर्ग<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Category" Text="<i class='fa fa-exclamation-circle' title='Select Category !'></i>"
                                                    ControlToValidate="ddlItemCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>

                                            <asp:DropDownList ID="ddlItemCategory" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>--%>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Location<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Location" Text="<i class='fa fa-exclamation-circle' title='Select Location !'></i>"
                                                    ControlToValidate="ddlLocation" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Route No </label>
                                            <asp:DropDownList ID="ddlRoute" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Shift</label>
                                            <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
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
                                <%-- <div class="row">
                                   
                            </div>  --%>
                            </fieldset>

                        </div>

                    </div>
                </div>
                <div class="col-md-12" id="pnlData" runat="server" visible="false">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Crate Report</h3>

                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblReportMsg" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="row">

                                <%-- <div class="col-md-1 pull-right">
                                 <asp:Button ID="btnExport" runat="server" CssClass="btn btn-primary" OnClick="btnExport_Click" Text="Export" /> &nbsp;&nbsp;&nbsp;&nbsp;
                                     </div>--%>
                                <div class="col-md-1 pull-right">
                                    <asp:Button ID="btnPrintRoutWise" runat="server" CssClass="btn btn-primary" Text="Print" OnClick="btnPrintRoutWise_Click" />
                                </div>
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend><span>Crate Report</span></legend>
                                        <div class="col-md-12" id="pnlgriddata" runat="server">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" ShowFooter="true" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                    AutoGenerateColumns="False" EmptyDataText="No Record Found." EnableModelValidation="True">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Route">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRName" Text='<%# Eval("RName")  %>' runat="server" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblGrandTotal" runat="server" Font-Bold="true" Text="Grand Total"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Crate Issued">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIssueCrate" Text='<%# Eval("IssueCrate")  %>' runat="server" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalIssueCrate" runat="server" Font-Bold="true"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Crate Return ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblReturnCrate" Text='<%# Eval("ReturnCrate")  %>' runat="server" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalReturnCrate" runat="server" Font-Bold="true"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Short / Excess">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblShortExcessCrate" Text='<%# Eval("ShortExcessCrate")  %>' runat="server" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalShortExcessCrate" runat="server" Font-Bold="true"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </section>
        <!-- /.content -->
        <section class="content">
            <div id="Print" runat="server" class="NonPrintable"></div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>
