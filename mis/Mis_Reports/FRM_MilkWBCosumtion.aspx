﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FRM_MilkWBCosumtion.aspx.cs" Inherits="mis_Mis_Reports_FRM_MilkWBCosumtion" %>

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
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="mpr" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="box box-primary">
                    <div class="box-header">
                        <h3 class="box-title">SMP consumtion in Milk</h3>
                        <asp:Label ID="lblMsg" runat="server" />
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">
                                <fieldset>
                                    <legend>SMP consumtion in Milk</legend>
                                    <%--  <div class="row">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>--%>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Year<span style="color: red">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                        ErrorMessage="Select Year" InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Year !'></i>"
                                                        ControlToValidate="ddlYear" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control select2">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Month<span style="color: red">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                        ErrorMessage="Select Month" InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Month !'></i>"
                                                        ControlToValidate="ddlMonth" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control select2">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">January</asp:ListItem>
                                                    <asp:ListItem Value="2">February</asp:ListItem>
                                                    <asp:ListItem Value="3">March</asp:ListItem>
                                                    <asp:ListItem Value="4">April</asp:ListItem>
                                                    <asp:ListItem Value="5">May</asp:ListItem>
                                                    <asp:ListItem Value="6">June</asp:ListItem>
                                                    <asp:ListItem Value="7">July</asp:ListItem>
                                                    <asp:ListItem Value="8">August</asp:ListItem>
                                                    <asp:ListItem Value="9">September</asp:ListItem>
                                                    <asp:ListItem Value="10">October</asp:ListItem>
                                                    <asp:ListItem Value="11">November</asp:ListItem>
                                                    <asp:ListItem Value="12">December</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Button ID="btnSearch" runat="server" Style="margin-top: 22px;" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                                            </div>
                                        </div>
                                    </div>

                                </fieldset>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table table-responsive">
                                            <asp:GridView ID="GrDetail" runat="server" ShowFooter="true" CssClass="datatable table table-bordered" EmptyDataText="No Record Found!!"></asp:GridView>
                                        </div>
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
                        columns: [0, 1, 2, 3, 4, 5, 6, 7]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7]
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

