<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FTDashboard.aspx.cs" Inherits="mis_filetracking_FTDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .box {
            min-height: 100px;
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
        <%-- <section class="content-header">
            <h1>File Tracking DASHBOARD
                    <small></small>
            </h1>
          
        </section>--%>
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">

                  
                    <div class="box box-success">
                        <div class="box-header with-border">

                            <div class="row">
                                <div class="col-md-10">
                                    <h3 class="box-title" id="Label1"><b>File Tracking Report</b></h3>
                                </div>
                            </div>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                          <div class="row">
                        <div class="col-lg-4 col-xs-8">
                            <!-- small box -->
                            <div class="info-box bg-yellow">
                                <span class="info-box-icon"><i class="fa fa-files-o"></i></span>
                                <div class="info-box-content">
                                    <span class="info-box-text">TOTAL CREATED FILES</span>
                                    <asp:Label ID="lbltotalFiles" runat="server" class="info-box-number" Text=""></asp:Label>
                                    <span class="progress-description"><a href="../../mis/filetracking/FTDashboardDetail.aspx?Parameter1=TotalFiles" class="Aselect1" style="color: white;">More info <i class="fa fa-arrow-circle-right"></i></a>
                                    </span>
                                </div>
                                <!-- /.info-box-content -->
                            </div>
                        </div>
                        <div class="col-lg-4 col-xs-8">
                            <!-- small box -->
                            <div class="info-box bg-aqua">
                                <span class="info-box-icon"><i class="fa fa-files-o"></i></span>
                                <div class="info-box-content">
                                    <span class="info-box-text">TOTAL INWARD LETTERS</span>
                                    <asp:Label ID="lblTotalInward" runat="server" class="info-box-number" Text=""></asp:Label>
                                    <span class="progress-description"><a href="../../mis/filetracking/FTDashboardDetail.aspx?Parameter1=InwardLetter" class="Aselect1" style="color: white;">More info <i class="fa fa-arrow-circle-right"></i></a>
                                    </span>
                                </div>
                                <!-- /.info-box-content -->
                            </div>
                        </div>
                        <div class="col-lg-4 col-xs-8">
                            <!-- small box -->
                            <div class="info-box bg-gray-active">
                                <span class="info-box-icon"><i class="fa fa-files-o"></i></span>
                                <div class="info-box-content">
                                    <span class="info-box-text">TOTAL OUTWARD LETTERS</span>
                                    <asp:Label ID="lblTotalOutward" runat="server" class="info-box-number" Text=""></asp:Label>
                                    <span class="progress-description"><a href="../../mis/filetracking/FTDashboardDetail.aspx?Parameter1=OutwardLetter" class="Aselect1" style="color: white;">More info <i class="fa fa-arrow-circle-right"></i></a>
                                    </span>
                                </div>
                                <!-- /.info-box-content -->
                            </div>
                        </div>
                    </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Office Name<span style="color: red;"> *</span></label>
                                        <asp:DropDownList ID="ddlOfficeName" CssClass="form-control select2 select1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOfficeName_SelectedIndexChanged"></asp:DropDownList>
                                        <small><span id="valOfficeName" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3"></div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <%--CODE CHANGES START BY CHINMAY ON 11-JUL-2019--%>
                                    <%--<asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" ShowFooter="true">--%>
                                    <asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" ShowFooter="true" OnRowCommand="GridView1_RowCommand">
                                        <%--CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019--%>
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    <%--CODE CHANGES START BY CHINMAY ON 11-JUL-2019--%>
                                                    <asp:Label ID="lblEmpID" Text='<%# Eval("Emp_ID").ToString()%>' runat="server" Visible="false" />
                                                    <%--CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Emp_Name" HeaderText="EMPLOYEE NAME" />
                                            <asp:TemplateField HeaderText="INWARD LETTERS">
                                                <ItemTemplate>
                                                    <%--CODE CHANGES START BY CHINMAY ON 11-JUL-2019--%>
                                                    <%--<asp:HyperLink ID="hylnkInwardFile" Target="_blank" runat="server" CssClass="label label-info" Text='<%# Eval("InwardFile").ToString()%>' NavigateUrl='<%# "FTDashboardDetail.aspx?Emp_ID=" + APIProcedure.Client_Encrypt(Eval("Emp_ID").ToString()) + "&Parameter=Inward"%>'></asp:HyperLink>--%>
                                                    <asp:LinkButton ID="lnkInwardFile" runat="server" CssClass="label label-info" Text='<%# Eval("InwardFile").ToString()%>' CommandArgument='<%# Container.DataItemIndex %>' CommandName="Inward"></asp:LinkButton>
                                                    <%--CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019--%>
                                                    <%--<asp:LinkButton ID="lblInwardFiles" CssClass="label label-warning" runat="server" Text='<%# Eval("InwardFile").ToString()%>' ToolTip='<%# Eval("Emp_ID").ToString()%>' OnClick="lblInwardFiles_Click" >LinkButton</asp:LinkButton>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CREATED FILES">
                                                <ItemTemplate>
                                                    <%--CODE CHANGES START BY CHINMAY ON 11-JUL-2019--%>
                                                    <%--<asp:HyperLink ID="hylnkCreateFile" Target="_blank" runat="server" CssClass="label label-warning" Text='<%# Eval("CreteFiles").ToString()%>' NavigateUrl='<%# "FTDashboardDetail.aspx?Emp_ID=" + APIProcedure.Client_Encrypt(Eval("Emp_ID").ToString()) + "&Parameter=Create"%>'></asp:HyperLink>--%>
                                                    <asp:LinkButton ID="lnkCreateFile" runat="server" CssClass="label label-warning" Text='<%# Eval("CreteFiles").ToString()%>' CommandArgument='<%# Container.DataItemIndex %>' CommandName="Create"></asp:LinkButton>
                                                    <%--CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019--%>
                                                    <%--<asp:LinkButton ID="lblcreateFiles" CssClass="label label-warning" runat="server" Text='<%# Eval("CreteFiles").ToString()%>' ToolTip='<%# Eval("Emp_ID").ToString()%>' OnClick="lblcreateFiles_Click">LinkButton</asp:LinkButton>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ON MY DESK FILES">
                                                <ItemTemplate>
                                                    <%--CODE CHANGES START BY CHINMAY ON 11-JUL-2019--%>
                                                    <%--<asp:HyperLink ID="hylnkCreateFile" Target="_blank" runat="server" CssClass="label label-success" Text='<%# Eval("FilesOnMyDesk").ToString()%>' NavigateUrl='<%# "FTDashboardDetail.aspx?Emp_ID=" + APIProcedure.Client_Encrypt(Eval("Emp_ID").ToString()) + "&Parameter=OnMyDesk"%>'></asp:HyperLink>--%>
                                                    <asp:LinkButton ID="lnkOnMyDesk" runat="server" CssClass="label label-success" Text='<%# Eval("FilesOnMyDesk").ToString()%>' CommandArgument='<%# Container.DataItemIndex %>' CommandName="MyDesk"></asp:LinkButton>
                                                    <%--CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019--%>
                                                    <%--<asp:LinkButton ID="lblOnMyDesk" CssClass="label label-success" runat="server" Text='<%# Eval("FilesOnMyDesk").ToString()%>' ToolTip='<%# Eval("Emp_ID").ToString()%>' OnClick="lblOnMyDesk_Click">LinkButton</asp:LinkButton>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="OUTWARD LETTER">
                                                <ItemTemplate>
                                                    <%--CODE CHANGES START BY CHINMAY ON 11-JUL-2019--%>
                                                    <%--<asp:HyperLink ID="hylnkOutwardLetter" Target="_blank" runat="server" CssClass="label label-default" Text='<%# Eval("OutwardLetter").ToString()%>' NavigateUrl='<%# "FTDashboardDetail.aspx?Emp_ID=" + APIProcedure.Client_Encrypt(Eval("Emp_ID").ToString()) + "&Parameter=Outward"%>'></asp:HyperLink>--%>
                                                    <asp:LinkButton ID="lnkOutwardLetter" runat="server" CssClass="label label-default" Text='<%# Eval("OutwardLetter").ToString()%>' CommandArgument='<%# Container.DataItemIndex %>' CommandName="Outword"></asp:LinkButton>
                                                    <%--CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019--%>
                                                    <%--<asp:LinkButton ID="lblOnMyDesk" CssClass="label label-success" runat="server" Text='<%# Eval("FilesOnMyDesk").ToString()%>' ToolTip='<%# Eval("Emp_ID").ToString()%>' OnClick="lblOnMyDesk_Click">LinkButton</asp:LinkButton>--%>
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
                orderable: false
            }],
            "bSort": false,
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
    </script>
</asp:Content>

