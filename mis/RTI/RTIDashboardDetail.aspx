<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RTIDashboardDetail.aspx.cs" Inherits="mis_RTIDashboard_RTIDashboardDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
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

                    <!-- general form elements -->
                    <div class="box box-success">
                        <div class="box-header with-border">

                            <div class="row">
                                <div class="col-md-10">
                                    <%-- <h3 class="box-title" id="Label1">RTI DASHBOARD DETAIL</h3>--%>
                                    <h3 class="box-title">
                                        <asp:Label ID="lblRTIType" runat="server" Text="" Font-Size="X-Large" ForeColor="Green" Font-Bold="false"></asp:Label></h3>
                                    <asp:HiddenField ID="hdnParameterType" runat="server" />
                                </div>
                            </div>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblMsg" runat="server" Text="" Style="font-size: 17px;"></asp:Label>
                                    <%--<asp:GridView ID="GridView1" DataKeyNames="RTI_ID" runat="server" class="datatable table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true">--%>
                                    <asp:GridView ID="GridView1" DataKeyNames="RTI_ID" runat="server" class="datatable table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" OnRowCommand="GridView1_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="App_Name" HeaderText="APPLICANT NAME" />
                                            <asp:BoundField DataField="App_MobileNo" HeaderText="MOBILE NO" />
                                            <asp:BoundField DataField="App_Email" HeaderText="EMAIL" />
                                            <asp:BoundField DataField="RTI_RegistrationNo" HeaderText="REGISTRATION NO" />
                                            <asp:BoundField DataField="RTI_Subject" HeaderText="RTI SUBJECT" />
                                            <asp:BoundField DataField="RTI_Request" HeaderText="RTI REQUEST" />
                                            <asp:TemplateField HeaderText="VIEW DETAIL">
                                                <ItemTemplate>
                                                    <%--  <asp:HyperLink ID="hylnkViewDetail" runat="server" CssClass="label label-default" Text="View Detail" NavigateUrl='<%# "RTIDashboardCommentsDetail.aspx?RTI_ID=" + APIProcedure.Client_Encrypt(Eval("RTI_ID").ToString())+"&ShowHide=" + APIProcedure.Client_Encrypt("Hide")%>' Target="_blank"></asp:HyperLink>--%>

                                                    <%--<asp:HyperLink ID="hylnkViewDetail" runat="server" CssClass="label label-default" Text="View Detail" NavigateUrl='<%#Eval("RequestFor").ToString() == "FirstAppeal" ? "FirstAppealReply.aspx?RTI_ID=" + APIProcedure.Client_Encrypt(Eval("RTI_ID").ToString())+"&ShowHide=" + APIProcedure.Client_Encrypt(Eval("RequestFor").ToString()):"RTIDashboardCommentsDetail.aspx?RTI_ID=" + APIProcedure.Client_Encrypt(Eval("RTI_ID").ToString())+"&ShowHide=" + APIProcedure.Client_Encrypt("Hide") %>' Target="_self"></asp:HyperLink>--%>

                                                    <asp:LinkButton ID="hylnkViewDetail" runat="server" CssClass="label label-default" Text="View Detail" CommandArgument='<%# Container.DataItemIndex  %>' CommandName="View"></asp:LinkButton>
                                                    <asp:Label ID="lblRequestFor" Text='<%# Eval("RequestFor").ToString() %>' runat="server" Visible="false" />
                                                    <asp:Label ID="lblRTIID" Text='<%# Eval("RTI_ID").ToString() %>' runat="server" Visible="false" />
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
    </script>
</asp:Content>

