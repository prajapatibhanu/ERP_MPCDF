<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="mis_Grievance_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .info-box {
            min-height: 150px;
            /*border-bottom:50px solid white;*/
        }

        .info-box-icon {
            min-height: 150px;
            /*border-bottom:50px solid white;*/
        }

        th.sorting, th.sorting_asc, th.sorting_desc {
            background: teal !important;
            color: white !important;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
        }

        a.btn.btn-default.buttons-excel.buttons-html5 {
            background: #ff5722c2;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-pdf.buttons-html5 {
            background: #009688c9;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-print {
            background: #e91e639e;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
        }

            a.btn.btn-default.buttons-print:hover, a.btn.btn-default.buttons-pdf.buttons-html5:hover, a.btn.btn-default.buttons-excel.buttons-html5:hover {
                box-shadow: 1px 1px 1px #808080;
            }

            a.btn.btn-default.buttons-print:active, a.btn.btn-default.buttons-pdf.buttons-html5:active, a.btn.btn-default.buttons-excel.buttons-html5:active {
                box-shadow: 1px 1px 1px #808080;
            }

        .box.box-pramod {
            border-top-color: #1ca79a;
        }

        .box {
            min-height: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">

                    <!-- general form elements -->
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <div class="row">
                                <div class="col-md-10">
                                    <%--<h3 class="box-title" id="Label1">CUSTOMER COMMUNICATION DASHBOARD</h3>--%>
                                </div>
                            </div>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label style="color: #337ab7; font-size: 17px; font-weight: 900">
                                            अब तक की कुल शिकायत :
                                            <asp:LinkButton ID="lnktotal" runat="server" Style="color: red;" OnClick="lnktotal_Click"><span id="spntotal" runat="server"></span></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; अब तक हल की गई कुल शिकायत :
                                            <asp:LinkButton ID="lnkclosed" runat="server" Style="color: red;" OnClick="lnkclosed_Click"><span id="spnclosed" runat="server"></span></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; लंबित शिकायतें :
                                            <asp:LinkButton ID="lnkopen" runat="server" Style="color: red;" OnClick="lnkopen_Click"><span id="spnpending" runat="server"></span></asp:LinkButton>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;15 दिनों से अधिक लंबित शिकायतें :<asp:LinkButton ID="lnkPending" runat="server" Style="color: red;" OnClick="lnkPending_Click"><span id="spn15Dayspending" runat="server"></span></asp:LinkButton>
                                        </label>
                                    </div>
                                </div>
                            </div>
							   <div class="row">
                                <div class="col-md-12">
                                    <h4><strong>Office Wise Pending Complaints</strong></h4>
                                    <asp:GridView ID="GrvOfficeWisePendingComplaints" runat="server" CssClass="table table-bordered" showfooter="true" AutoGenerateColumns="false" OnRowCommand="grv10_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="क्रमांक">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Office Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="उत्पाद की गुणवत्ता के बारे में">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProduction" runat="server" CssClass="label label-warning" Text='<%# Eval("ProductionQualityTotal") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="उत्पाद की उपलब्धता के बारे में">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAvailability" runat="server" CssClass="label label-warning" Text='<%# Eval("ProductionAvlTotal") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="समिति भुगतान के सम्बन्ध में">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSocietyPayment" runat="server" CssClass="label label-warning" Text='<%# Eval("SocietyPaymentTotal") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="एजेंसी / बूथ / पार्लर / डीपो सम्बन्धित शिकायत">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblParlourRelated" runat="server" CssClass="label label-warning" Text='<%# Eval("ParlourRelatedTotal") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="वितरक / परिवहनकर्ता सम्बन्धित शिकायत">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDistibutorRelated" runat="server" CssClass="label label-warning" Text='<%# Eval("OtherInformationTotal") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="दूध उत्पादक समिति से सम्बंधित ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMilkProduction" runat="server" CssClass="label label-warning" Text='<%# Eval("MilkProductionTotal") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="अन्य सुझाव">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOtherSugg" runat="server" CssClass="label label-warning" Text='<%# Eval("OtherSuggestionTotal") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
											 <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalAllCategory" runat="server" CssClass="label label-warning" Text='<%# Eval("TotalAllCategory") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <h4><strong>उत्पाद की गुणवत्ता के बारे में</strong></h4>
                                    <asp:GridView ID="grv1" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="grv1_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Office Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Complaints">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblTotal" runat="server" CssClass="label label-warning" CommandArgument='<%# Eval("Office_ID") %>' CommandName="All" Text='<%# Eval("Total") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Open Complaints">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblOpen" runat="server" CssClass="label label-warning" CommandArgument='<%# Eval("Office_ID") %>' CommandName="Open" Text='<%# Eval("OpenCount") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Close Complaints">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblClose" runat="server" CssClass="label label-success" CommandArgument='<%# Eval("Office_ID") %>' CommandName="Close" Text='<%# Eval("CloseCount") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-md-4">
                                    <h4><strong>उत्पाद की उपलब्धता के बारे में</strong></h4>
                                    <asp:GridView ID="grv2" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="grv2_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Office Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Complaints">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblTotal" runat="server" CssClass="label label-warning" CommandArgument='<%# Eval("Office_ID") %>' CommandName="All" Text='<%# Eval("Total") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Open Complaints">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblOpen" runat="server" CssClass="label label-warning" CommandArgument='<%# Eval("Office_ID") %>' CommandName="Open" Text='<%# Eval("OpenCount") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Close Complaints">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblClose" runat="server" CssClass="label label-success" CommandArgument='<%# Eval("Office_ID") %>' CommandName="Close" Text='<%# Eval("CloseCount") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-md-4">
                                    <h4><strong>समिति भुगतान के सम्बन्ध में</strong></h4>
                                    <asp:GridView ID="grv3" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="grv3_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Office Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Complaints">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblTotal" runat="server" CssClass="label label-warning" CommandArgument='<%# Eval("Office_ID") %>' CommandName="All" Text='<%# Eval("Total") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Open Complaints">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblOpen" runat="server" CssClass="label label-warning" CommandArgument='<%# Eval("Office_ID") %>' CommandName="Open" Text='<%# Eval("OpenCount") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Close Complaints">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblClose" runat="server" CssClass="label label-success" CommandArgument='<%# Eval("Office_ID") %>' CommandName="Close" Text='<%# Eval("CloseCount") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <h4><strong>एजेंसी / बूथ / पार्लर / डीपो सम्बन्धित शिकायत</strong></h4>
                                        <asp:GridView ID="grv4" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="grv4_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Office Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Complaints">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblTotal" runat="server" CssClass="label label-warning" CommandArgument='<%# Eval("Office_ID") %>' CommandName="All" Text='<%# Eval("Total") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Open Complaints">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblOpen" runat="server" CssClass="label label-warning" CommandArgument='<%# Eval("Office_ID") %>' CommandName="Open" Text='<%# Eval("OpenCount") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Close Complaints">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblClose" runat="server" CssClass="label label-success" CommandArgument='<%# Eval("Office_ID") %>' CommandName="Close" Text='<%# Eval("CloseCount") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-md-4">
                                        <h4><strong>वितरक / परिवहनकर्ता सम्बन्धित शिकायत</strong></h4>
                                        <asp:GridView ID="grv5" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="grv5_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Office Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Complaints">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblTotal" runat="server" CssClass="label label-warning" CommandArgument='<%# Eval("Office_ID") %>' CommandName="All" Text='<%# Eval("Total") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Open Complaints">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblOpen" runat="server" CssClass="label label-warning" CommandArgument='<%# Eval("Office_ID") %>' CommandName="Open" Text='<%# Eval("OpenCount") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Close Complaints">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblClose" runat="server" CssClass="label label-success" CommandArgument='<%# Eval("Office_ID") %>' CommandName="Close" Text='<%# Eval("CloseCount") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-md-4">
                                        <h4><strong>दूध उत्पादक समिति से सम्बंधित &nbsp;&nbsp;&nbsp;&nbsp;</strong></h4>
                                        <asp:GridView ID="grv6" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="grv6_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Office Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Complaints">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblTotal" runat="server" CssClass="label label-warning" CommandArgument='<%# Eval("Office_ID") %>' CommandName="All" Text='<%# Eval("Total") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Open Complaints">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblOpen" runat="server" CssClass="label label-warning" CommandArgument='<%# Eval("Office_ID") %>' CommandName="Open" Text='<%# Eval("OpenCount") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Close Complaints">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblClose" runat="server" CssClass="label label-success" CommandArgument='<%# Eval("Office_ID") %>' CommandName="Close" Text='<%# Eval("CloseCount") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                  <%-- <div class="col-md-4">
                                        <h4><strong>क्रय सामग्री प्रदायक द्वारा शिकायत</strong></h4>
                                        <asp:GridView ID="grv7" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="grv7_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Office Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Complaints">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblTotal" runat="server" CssClass="label label-warning" CommandArgument='<%# Eval("Office_ID") %>' CommandName="All" Text='<%# Eval("Total") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Open Complaints">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblOpen" runat="server" CssClass="label label-warning" CommandArgument='<%# Eval("Office_ID") %>' CommandName="Open" Text='<%# Eval("OpenCount") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Close Complaints">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblClose" runat="server" CssClass="label label-success" CommandArgument='<%# Eval("Office_ID") %>' CommandName="Close" Text='<%# Eval("CloseCount") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-md-4">
                                        <h4><strong>सामग्री प्रदाय से सम्बंधित</strong></h4>
                                        <asp:GridView ID="grv8" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="grv8_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Office Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Complaints">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblTotal" runat="server" CssClass="label label-warning" CommandArgument='<%# Eval("Office_ID") %>' CommandName="All" Text='<%# Eval("Total") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Open Complaints">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblOpen" runat="server" CssClass="label label-warning" CommandArgument='<%# Eval("Office_ID") %>' CommandName="Open" Text='<%# Eval("OpenCount") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Close Complaints">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblClose" runat="server" CssClass="label label-success" CommandArgument='<%# Eval("Office_ID") %>' CommandName="Close" Text='<%# Eval("CloseCount") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>--%>
                                    <div class="col-md-4">
                                        <h4><strong>अन्य सुझाव</strong></h4>
                                        <asp:GridView ID="grv9" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="grv9_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Office Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Complaints">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblTotal" runat="server" CssClass="label label-warning" CommandArgument='<%# Eval("Office_ID") %>' CommandName="All" Text='<%# Eval("Total") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Open Complaints">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblOpen" runat="server" CssClass="label label-warning" CommandArgument='<%# Eval("Office_ID") %>' CommandName="Open" Text='<%# Eval("OpenCount") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Close Complaints">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblClose" runat="server" CssClass="label label-success" CommandArgument='<%# Eval("Office_ID") %>' CommandName="Close" Text='<%# Eval("CloseCount") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-md-4">
                                        <h4><strong>अन्य (जानकारी प्राप्त करने से सम्बंधित)</strong></h4>
                                        <asp:GridView ID="grv10" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="grv10_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Office Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Complaints">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblTotal" runat="server" CssClass="label label-warning" CommandArgument='<%# Eval("Office_ID") %>' CommandName="All" Text='<%# Eval("Total") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Open Complaints">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblOpen" runat="server" CssClass="label label-warning" CommandArgument='<%# Eval("Office_ID") %>' CommandName="Open" Text='<%# Eval("OpenCount") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Close Complaints">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblClose" runat="server" CssClass="label label-success" CommandArgument='<%# Eval("Office_ID") %>' CommandName="Close" Text='<%# Eval("CloseCount") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="AddModal" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header  bg-gray-light">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="modal-body bg-gray-light">
                            <div class="row">
                                <div class="col-md-12">
                                    <label id="lbl" runat="server"></label>
                                    <asp:GridView ID="GridView2" runat="server" ShowFooter="true" AutoGenerateColumns="False" class="datatable table table-striped table-bordered" OnRowCommand="GridView2_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Office Name">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("Office_Name").ToString()%>' runat="server" ID="lblOffice_Name"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Complaint">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" CssClass="label label-info" CommandArgument='<%# Eval("Office_ID").ToString()%>' CommandName="View" ID="lblCount"><%# Eval("Count").ToString()%></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <div id="ViewModal" class="modal fade" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header  bg-gray-light">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="modal-body bg-gray-light">
                            <div class="row">
                                <div class="col-md-12">
                                    <label id="lblOfficeName" runat="server"></label>
                                    <br />
                                    <div class="table table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" EmptyDataText="No Record Found!!" AutoGenerateColumns="False" class="datatable table table-striped table-bordered" OnSorting="GridView1_Sorting" AllowSorting="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Grievance Status" SortExpression="Application_GrvStatus">
                                                    <ItemTemplate>
                                                        <asp:Label Text='<%# Eval("Application_GrvStatus").ToString()%>' runat="server" ID="Application_GrvStatus"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Application_RefNo" HeaderText="Reference No" />
                                                <asp:BoundField DataField="Application_Subject" HeaderText="Subject" />
                                                <asp:BoundField DataField="ApplicationApply_Date" HeaderText="Apply Date" />
                                                <asp:BoundField DataField="ClosingDate" HeaderText="Closing Date" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <div id="GrvViewModal" class="modal fade" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header  bg-gray-light">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="modal-body bg-gray-light">
                            <div class="row">
                                <div class="col-md-12">
                                    Office Name :
                                    <label id="lblOfficeName1" runat="server"></label>
                                    &nbsp;&nbsp Complaint Type :
                                    <label id="lblComplaintType" runat="server"></label>
                                    <br />
                                    <div class="table table-responsive">
                                        <asp:GridView ID="GridView3" DataKeyNames="Application_ID" runat="server" EmptyDataText="No Record Found!!" AutoGenerateColumns="False" class="datatable table table-striped table-bordered" OnSelectedIndexChanged="GridView3_SelectedIndexChanged">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Grievance Status" SortExpression="Application_GrvStatus">
                                                    <ItemTemplate>
                                                        <asp:Label Text='<%# Eval("Application_GrvStatus").ToString()%>' runat="server" ID="Application_GrvStatus"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Application_RefNo" HeaderText="Reference No" />
                                                <asp:BoundField DataField="Application_Subject" HeaderText="Subject" />
                                                <asp:BoundField DataField="ApplicationApply_Date" HeaderText="Apply Date" />
                                                <asp:BoundField DataField="ClosingDate" HeaderText="Closing Date" />
                                                <asp:TemplateField HeaderText="View More Detail">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" CssClass="label label-default" runat="server" CommandName="select">View More</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <link href="../Finance/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../Finance/css/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="../Finance/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="../Finance/js/jquery.dataTables.min.js"></script>
    <script src="../Finance/js/jquery.dataTables.min.js"></script>
    <script src="../Finance/js/dataTables.bootstrap.min.js"></script>
    <script src="../Finance/js/dataTables.buttons.min.js"></script>
    <script src="../Finance/js/buttons.flash.min.js"></script>
    <script src="../Finance/js/jszip.min.js"></script>
    <script src="../Finance/js/pdfmake.min.js"></script>
    <script src="../Finance/js/vfs_fonts.js"></script>
    <script src="../Finance/js/buttons.html5.min.js"></script>
    <script src="../Finance/js/buttons.print.min.js"></script>
    <script src="../Finance/js/buttons.colVis.min.js"></script>
    <script>
        $('.datatable').DataTable({
            paging: false,
            columnDefs: [{
                targets: 'no-sort',
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
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
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

        function callalert() {
            debugger;
            $("#AddModal").modal('show');
        }
        function callalert1() {
            debugger;
            $("#ViewModal").modal('show');
        }
        function callalert2() {
            debugger;
            $("#GrvViewModal").modal('show');
        }

        //function GetDatatFromID(value) {
        //    debugger;
        //    $.ajax({
        //        url: 'Dashboard.aspx/SearchData',
        //        //data: "{ 'Office_Name': '" + $('#txtSociety').val() + "'}",
        //        data: "{ 'OfficeID': '" + value + "'}",
        //        dataType: "json",
        //        type: "POST",
        //        contentType: "application/json; charset=utf-8",
        //        success: function (data) {
        //            response($.map(data.d, function (item) {
        //                return {
        //                    label: item.split('-')[0],
        //                    val: item.split('-')[1]
        //                }
        //            }))
        //        },
        //        error: function (response) {
        //            alert(response.responseText);
        //        },
        //        failure: function (response) {
        //            alert(response.responseText);
        //        }
        //    });
        //};
    </script>
</asp:Content>

