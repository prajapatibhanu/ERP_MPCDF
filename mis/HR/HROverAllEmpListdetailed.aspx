<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HROverAllEmpListdetailed.aspx.cs" Inherits="mis_HR_HROverAllEmpListdetailed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <link href="css/hrcustom.css" rel="stylesheet" />
    <link href="css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <style>
        table {
            white-space: nowrap;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Employee Information in Detail
        <small></small>
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home ( होम)</a></li>
                <li class="active">Employee Information in Detail</li>
            </ol>
        </section>
        <section class="content">
            <div class="box box-pramod">
                <div class="box-header">
                    Employee Information in Detail
                </div>
                <asp:label id="lblMsg" runat="server" text=""></asp:label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Office Name<span style="color: red;">*</span></label>
                                <asp:dropdownlist id="ddlOffice" runat="server" class="form-control select2" enabled="false">
                                </asp:dropdownlist>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Payroll/Posting Wise<span style="color: red;">*</span></label>
                                <asp:dropdownlist id="ddlDetailType" runat="server" class="form-control">
                                    <asp:ListItem Value="Salary">Payroll Office Wise</asp:ListItem>
                                    <asp:ListItem Value="Posting">Posting Office Wise</asp:ListItem>
                                </asp:dropdownlist>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>&nbsp;</label>
                                <asp:button id="btnSearch" runat="server" cssclass="btn btn-success btn-block" text="Search" onclick="btnSearch_Click"></asp:button>
                            </div>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-12">
                            <asp:gridview id="GridView1" runat="server" class="datatable  table table-hover table-bordered pagination-ys" autogeneratecolumns="true" showheaderwhenempty="true" emptydatatext="No Record Found">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>

                                        <ItemStyle Width="5%"></ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:gridview>
                        </div>
                    </div>
                </div>
            </div>

        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/dataTables.bootstrap.min.js"></script>
    <script src="js/dataTables.buttons.min.js"></script>
    <%-- <script src="js/buttons.flash.min.js"></script>--%>
    <script src="js/jszip.min.js"></script>
    <%-- <script src="js/pdfmake.min.js"></script>
    <script src="js/vfs_fonts.js"></script>--%>
    <script src="js/buttons.html5.min.js"></script>
    <script src="js/buttons.print.min.js"></script>


    <script>
        $(document).ready(function () {
            $('.datatable').DataTable({

                paging: true,
                pageLength: 50,

                columnDefs: [{
                    targets: 'no-sort',
                    orderable: false
                }],
                "order": [[0, 'asc']],

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
                            columns: ':not(.no-print)'
                        },
                        footer: true,
                        autoPrint: true
                    }, {
                        extend: 'excel',
                        text: '<i class="fa fa-file-excel-o"></i> Excel',
                        title: $('h1').text(),
                        exportOptions: {
                            columns: ':not(.no-print)'
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
            t.on('order.dt search.dt', function () {
                t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                    cell.innerHTML = i + 1;
                });
            }).draw();
        });
    </script>
</asp:Content>
