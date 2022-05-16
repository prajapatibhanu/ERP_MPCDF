<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RTIDashboard.aspx.cs" Inherits="mis_RTIDashboard_RTIDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="assets/css/Dashboard.css" rel="stylesheet" />
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
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header with-border">

                            <div class="row">
                                <div class="col-md-10">
                                    <h3 class="box-title" id="Label2">RTI DASHBOARD</h3>
                                </div>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-lg-4 col-xs-12">
                                            <!-- small box -->
                                            <div class="info-box bg-yellow">
                                                <span class="info-box-icon"><i class="fa fa-users"></i></span>

                                                <div class="info-box-content">
                                                    <span class="info-box-text">TOTAL RTI</span>
                                                    <asp:Label ID="lbltotalRTI" runat="server" class="info-box-number" Text=""></asp:Label>
                                                    <span class="progress-description"><a href="../../mis/RTI/RTIDashboardDetail.aspx?Parameter1=TotalRTI" target="_self" style="color: white;">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                    </span>
                                                </div>
                                                <!-- /.info-box-content -->
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-xs-12">
                                            <!-- small box -->
                                            <div class="info-box bg-yellow">
                                                <span class="info-box-icon"><i class="fa  fa-search"></i></span>

                                                <div class="info-box-content">
                                                    <span class="info-box-text">OPEN RTI</span>
                                                    <asp:Label ID="lblOpenRTI" runat="server" class="info-box-number" Text=""></asp:Label>
                                                    <span class="progress-description"><a href="../../mis/RTI/RTIDashboardDetail.aspx?Parameter1=OpenRTI" target="_self" style="color: white;">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                    </span>
                                                </div>
                                                <!-- /.info-box-content -->
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-xs-12">
                                            <!-- small box -->
                                            <div class="info-box bg-green">
                                                <span class="info-box-icon"><i class="ion ion-ios-pricetag-outline"></i></span>

                                                <div class="info-box-content">
                                                    <span class="info-box-text">CLOSED RTI</span>
                                                    <asp:Label ID="lblcloseRTI" runat="server" class="info-box-number" Text=""></asp:Label>
                                                    <span class="progress-description"><a href="../../mis/RTI/RTIDashboardDetail.aspx?Parameter1=CloseRTI" target="_self" style="color: white;">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                    </span>
                                                </div>
                                                <!-- /.info-box-content -->
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row">

                                        <div class="col-lg-4 col-xs-12">
                                            <!-- small box -->
                                            <div class="info-box bg-yellow">
                                                <span class="info-box-icon"><i class="ion ion-ios-pricetag-outline"></i></span>

                                                <div class="info-box-content">
                                                    <span class="info-box-text">TOTAL FIRST APPEAL</span>
                                                    <asp:Label ID="lblTotalAppeal" runat="server" class="info-box-number" Text=""></asp:Label>
                                                    <span class="progress-description"><a href="../../mis/RTI/RTIDashboardDetail.aspx?Parameter1=TotalFirstAppeal" target="_self" style="color: white;">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                    </span>
                                                </div>
                                                <!-- /.info-box-content -->
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-xs-12">
                                            <!-- small box -->
                                            <div class="info-box bg-yellow">
                                                <span class="info-box-icon"><i class="ion ion-ios-pricetag-outline"></i></span>

                                                <div class="info-box-content">
                                                    <span class="info-box-text">OPEN FIRST APPEAL</span>
                                                    <asp:Label ID="lblOpenAppeal" runat="server" class="info-box-number" Text=""></asp:Label>
                                                    <span class="progress-description"><a href="../../mis/RTI/RTIDashboardDetail.aspx?Parameter1=OpenFirstAppeal" target="_self" style="color: white;">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                    </span>
                                                </div>
                                                <!-- /.info-box-content -->
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-xs-12">
                                            <!-- small box -->
                                            <div class="info-box bg-green">
                                                <span class="info-box-icon"><i class="ion ion-ios-pricetag-outline"></i></span>

                                                <div class="info-box-content">
                                                    <span class="info-box-text">CLOSED FIRST APPEAL</span>
                                                    <asp:Label ID="lblCloseAppeal" runat="server" class="info-box-number" Text=""></asp:Label>
                                                    <span class="progress-description"><a href="../../mis/RTI/RTIDashboardDetail.aspx?Parameter1=CloseFirstAppeal" target="_self" style="color: white;">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                    </span>
                                                </div>
                                                <!-- /.info-box-content -->
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box box-success" id="divGrid" runat="server">
                        <div class="box-header with-border">

                            <div class="row">
                                <div class="col-md-10">
                                    <h3 class="box-title" id="Label1">RTI USER WISE</h3>
                                </div>
                            </div>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <label>User Type (आवेदक का प्रकार)<span style="color: red;"> *</span></label>
                                    <asp:DropDownList ID="ddlApp_UserType" runat="server" CssClass="form-control" ClientIDMode="Static">
                                        <asp:ListItem Value="0">All</asp:ListItem>
                                        <asp:ListItem Value="Individual">Individual</asp:ListItem>
                                        <asp:ListItem Value="Other Organization">Other Organization</asp:ListItem>
                                        <asp:ListItem Value="NGO">NGO</asp:ListItem>
                                        <asp:ListItem Value="Common Service Center">Common Service Center</asp:ListItem>
                                    </asp:DropDownList>
                                    <small><span id="valddlApp_UserType" class="text-danger"></span></small>
                                </div>
                                <div class="col-md-3"></div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button runat="server" CssClass="btn btn-success btn-block" Text="Show" ID="btnShow" OnClick="btnShow_Click" OnClientClick="return validateform();" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <a href="RTIDashboard.aspx" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>
                            <%--CODE CHANGES START BY CHINMAY ON 11-JUL-2019--%>
                            <%--<div class="row">
                                <div class="col-md-12">
                                    <div id="DivTable" runat="server"></div>
                                </div>
                            </div>--%>
                            <div class="row">
                                <div class="col-md-12">
                                    <%--CODE CHANGES START BY CHINMAY ON 11-JUL-2019--%>
                                    <%--<asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" ShowFooter="true" FooterStyle-CssClass="text-center">--%>
                                    <asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" ShowFooter="true" FooterStyle-CssClass="text-center" OnRowCommand="GridView1_RowCommand">
                                        <%--CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019--%>
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    <%--CODE CHANGES START BY CHINMAY ON 11-JUL-2019--%>
                                                    <%--<asp:HiddenField ID="hdnRTI_ByOfficeID" runat="server" Value='<%# Eval("Office_ID").ToString()%>' />--%>
                                                    <asp:Label ID="lblOfficeID" Text='<%# Eval("Office_ID").ToString()%>' Visible="false" runat="server" />
                                                    <asp:Label ID="lblApp_UserType" Text='<%# Eval("App_UserType").ToString()%>' Visible="false" runat="server" />
                                                    <%--CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="State_Name" HeaderText="STATE NAME" />
                                            <asp:BoundField DataField="District_Name" HeaderText="DISTRICT NAME" />
                                            <asp:BoundField DataField="Office_Name" HeaderText="OFFICE NAME" />
                                            <asp:TemplateField HeaderText="OPEN RTI" ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <%--CODE CHANGES START BY CHINMAY ON 11-JUL-2019--%>
                                                    <%--<asp:HyperLink ID="lblOpenRTI" CssClass="label label-warning" runat="server" Text='<%# Eval("OpenRTI").ToString()%>' NavigateUrl='<%# "RTIDashboardDetail.aspx?RTI_ByOfficeID=" + APIProcedure.Client_Encrypt(Eval("Office_ID").ToString())+
"&UserType=" +  APIProcedure.Client_Encrypt(Eval("App_UserType").ToString()) + 
"&Parameter=" + APIProcedure.Client_Encrypt("Open")%>'
                                                        Target="_blank"></asp:HyperLink>--%>
                                                    <asp:LinkButton ID="lbOpenRTI" runat="server" CssClass="label label-warning" Text='<%# Eval("OpenRTI").ToString()%>' CommandArgument='<%# Container.DataItemIndex  %>' CommandName="Open"></asp:LinkButton>
                                                    <%--CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CLOSED RTI" ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <%--CODE CHANGES START BY CHINMAY ON 11-JUL-2019--%>
                                                    <%--<asp:HyperLink ID="lblCloseRTI" CssClass="label label-success" runat="server" Text='<%# Eval("CloseRTI").ToString()%>' NavigateUrl='<%# "RTIDashboardDetail.aspx?RTI_ByOfficeID=" + APIProcedure.Client_Encrypt(Eval("Office_ID").ToString())+
"&UserType=" +  APIProcedure.Client_Encrypt(Eval("App_UserType").ToString()) + 
"&Parameter=" + APIProcedure.Client_Encrypt("Close")%>'
                                                        Target="_blank"></asp:HyperLink>--%>
                                                    <asp:LinkButton ID="lbCloseRTI" runat="server" CssClass="label label-success" Text='<%# Eval("CloseRTI").ToString()%>' CommandArgument='<%# Container.DataItemIndex  %>' CommandName="Close"></asp:LinkButton>
                                                    <%--CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TOTAL FIRST APPEAL" ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <%--CODE CHANGES START BY CHINMAY ON 11-JUL-2019--%>
                                                    <%--<asp:HyperLink ID="lblTotalFirstAppeal" CssClass="label label-warning" runat="server" Text='<%# Eval("TotalFirstAppeal").ToString()%>' NavigateUrl='<%# "RTIDashboardDetail.aspx?RTI_ByOfficeID=" + APIProcedure.Client_Encrypt(Eval("Office_ID").ToString())+
"&UserType=" +  APIProcedure.Client_Encrypt(Eval("App_UserType").ToString()) + 
"&Parameter=" + APIProcedure.Client_Encrypt("Total")%>'
                                                        Target="_blank"></asp:HyperLink>--%>
                                                    <asp:LinkButton ID="lbTotalFirstAppeal" runat="server" CssClass="label label-warning" Text='<%# Eval("TotalFirstAppeal").ToString()%>' CommandArgument='<%# Container.DataItemIndex  %>' CommandName="Total"></asp:LinkButton>
                                                    <%--CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="OPEN FIRST APPEAL" ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <%--CODE CHANGES START BY CHINMAY ON 11-JUL-2019--%>
                                                    <%--<asp:HyperLink ID="lblOpenFirstAppeal" CssClass="label label-success" runat="server" Text='<%# Eval("OpenFirstAppeal").ToString()%>' NavigateUrl='<%# "RTIDashboardDetail.aspx?RTI_ByOfficeID=" + APIProcedure.Client_Encrypt(Eval("Office_ID").ToString())+
"&UserType=" +  APIProcedure.Client_Encrypt(Eval("App_UserType").ToString()) + 
"&Parameter=" + APIProcedure.Client_Encrypt("AppealOpen")%>'
                                                        Target="_blank"></asp:HyperLink>--%>
                                                    <asp:LinkButton ID="lbOpenFirstAppeal" runat="server" CssClass="label label-success" Text='<%# Eval("OpenFirstAppeal").ToString()%>' CommandArgument='<%# Container.DataItemIndex  %>' CommandName="AppealOpen"></asp:LinkButton>
                                                    <%--CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CLOSED FIRST APPEAL" ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <%--CODE CHANGES START BY CHINMAY ON 11-JUL-2019--%>
                                                    <%--<asp:HyperLink ID="HyperLink1" CssClass="label label-warning" runat="server" Text='<%# Eval("CloseFirstAppeal").ToString()%>' NavigateUrl='<%# "RTIDashboardDetail.aspx?RTI_ByOfficeID=" + APIProcedure.Client_Encrypt(Eval("Office_ID").ToString())+
"&UserType=" +  APIProcedure.Client_Encrypt(Eval("App_UserType").ToString()) + 
"&Parameter=" + APIProcedure.Client_Encrypt("AppealClose")%>'
                                                        Target="_blank"></asp:HyperLink>--%>
                                                    <asp:LinkButton ID="lbClosedFirstAppeal" runat="server" CssClass="label label-warning" Text='<%# Eval("CloseFirstAppeal").ToString()%>' CommandArgument='<%# Container.DataItemIndex  %>' CommandName="AppealClose"></asp:LinkButton>
                                                    <%--CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <%--CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019--%>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>
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
                        columns: [0, 1, 2, 3, 4, 5, 6]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
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

        function validateform1() {
            var msg = "";
            $("#valddlApp_UserType").html("");

            if (document.getElementById('<%=ddlApp_UserType.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select User Type. \n";
                $("#valddlApp_UserType").html("Select User Type.");
            }
            if (msg != "") {
                alert(msg);
                return false;
            }

        }
    </script>
</asp:Content>

