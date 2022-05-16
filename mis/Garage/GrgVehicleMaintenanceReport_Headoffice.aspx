<%@ Page Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="GrgVehicleMaintenanceReport_Headoffice.aspx.cs" Inherits="mis_Garage_GrgVehicleMaintenanceReport_Headoffice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">

    <style>
        .pay-sheet table tr th, .pay-sheet table tr td {
            font-size: 12px;
            width: 10%;
            border: 1px dashed #ddd;
            padding-left: 1px;
            padding-top: 1px;
            line-height: 14px;
            font-family: monospace;
            overflow: hidden;
        }

        .pay-sheet table {
            width: 100%;
        }

            .pay-sheet table thead {
                background: #eee;
            }


        span.Ledger_Amt {
            max-width: 30%;
            display: inline;
            float: right;
        }

        span.Ledger_Name {
            max-width: 70%;
            display: inline;
            /*float: left;*/
        }

        p.subledger {
            border-top: 1px solid #ccc;
            margin: 0px;
        }

        .report-title {
            font-weight: 600;
            font-size: 15px;
            color: #123456;
        }

        .align-right {
            text-align: right !important;
        }

        .Scut {
            color: tomato;
        }

        .Childtd td {
            padding-left: 15px !important;
        }

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

        .voucherColumn {
            width: 150px !important;
        }

        .datepicker-dropdown {
            z-index: 10000 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->

        <section class="content">
            <asp:Label ID="lblTime" runat="server" CssClass="Dtime" Style="font-weight: 800;" Text="" ClientIDMode="Static"></asp:Label>
            <div class="box box-success">
                <div class="box-header Hiderow hide_print">
                    <h3 class="box-title">Vehicle Maintenance Report</h3>

                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

                </div>

                <div class="box-body">
                    <div class="hide_print">

                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Year<span style="color: red;"> *</span></label>
                                    <asp:DropDownList ID="ddlYear" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Month<span style="color: red;"> *</span></label>
                                    <asp:DropDownList ID="ddlMonth" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3" style="display:none">
                                <label>Vehicle Owned Type<span style="color: red;"> *</span></label>
                                <div class="form-group">
                                    <asp:RadioButtonList ID="rbtVehicleOwnedType" runat="server" RepeatColumns="2" CssClass="form-control" OnSelectedIndexChanged="rbtVehicleOwnedType_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="Hired" >&nbsp;Hired&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="Owned" Selected="True">&nbsp;Owned</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Vehicle No</label><span style="color: red">*</span>
                                    <asp:DropDownList ID="ddlVehicleNo" runat="server" class="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <%--<div class="col-md-3" runat="server" id="divAgency">
                                <div class="form-group">
                                    <label>Agency</label><span style="color: red">*</span>
                                    <asp:DropDownList ID="ddlAgency" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>--%>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-block btn-success" Style="margin-top: 18px;" Text="Search" OnClick="btnSearch_Click" />
                            </div>
                            <div class="col-md-2">
                                <a class="btn btn-block btn-default" href="GrgVehicleMaintenanceReport_Headoffice.aspx" style="margin-top: 18px;">Cancel</a>
                            </div>
                        </div>
                    </div>
                    <div class="form-group"></div>
                    <div class="row">
                        <div class="col-md-12 table-responsive">


                            <asp:GridView ID="GridViewOwned" runat="server" ClientIDMode="Static" AutoGenerateColumns="false" class="datatableOwned table table-hover table-bordered" DataKeyNames="MaintenanceID" EmptyDataText="No Record Found" OnRowDeleting="GridViewOwned_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="VehicleOwnedType" Visible="false" HeaderText="Vehicle Owned Type" />
                                    <asp:BoundField DataField="VehicleNo" HeaderText="Vehicle No" />
                                    <asp:BoundField DataField="Year" HeaderText="Year" />
                                    <asp:BoundField DataField="MonthName" HeaderText="Month" />
                                    <asp:BoundField DataField="OwnedTotRunupMonthly" HeaderText="Total Run-up Monthly" />

                                    <asp:BoundField DataField="OwnedFuelConsumption" HeaderText="Fuel Consumption" />
                                    <asp:BoundField DataField="OwnedFuelRate" HeaderText="Fuel Rate" />

                                    <asp:BoundField DataField="OwnedFuelExpences" HeaderText="Fuel Expences" />
                                    <asp:BoundField DataField="OwnedOtherExpensesOnVehicle" HeaderText="Other Expenses On Vehicle" />
                                    <asp:BoundField DataField="OwnedExpensesDetails" HeaderText="Expenses Details" />
                                    <asp:BoundField DataField="OwnedServicingDate" HeaderText="Servicing Date" />
                                    <asp:BoundField DataField="OwnedTotalKM" HeaderText="Total KM" />
                                    <asp:BoundField DataField="OwnedTotalExpensesInServicing" HeaderText="Total Expenses In Servicing" />
                                    <asp:BoundField DataField="OwnedExpensesBrief" HeaderText="Expenses Brief" />
                                    <asp:BoundField DataField="OwnedNextServicingDueDate" HeaderText="Next Servicing Due Date" />
                                    <asp:BoundField DataField="OwnedOtherInfo" HeaderText="Other Info" />
                                    <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Delete" runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Item Name will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:GridView ID="GridViewHired" runat="server" ClientIDMode="Static" AutoGenerateColumns="false" class="datatableHired table table-hover table-bordered" DataKeyNames="MaintenanceID" EmptyDataText="No Record Found" OnRowDeleting="GridViewHired_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="VehicleOwnedType" HeaderText="Vehicle Owned Type" />
                                    <asp:BoundField DataField="VehicleNo" HeaderText="Vehicle No" />
                                    <asp:BoundField DataField="Agency" HeaderText="Agency" />

                                    <asp:BoundField DataField="HiredTotalRunupMonthly" HeaderText="Total Run-up Monthly KM" />
                                    <asp:BoundField DataField="BillNo" HeaderText="Bill No" />
                                    <asp:BoundField DataField="BillAmount" HeaderText="Bill Amount" />
                                    <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Item Name will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <%--<asp:GridView ID="GridView1" runat="server" ClientIDMode="Static" AutoGenerateColumns="false" class="datatable table table-hover table-bordered" DataKeyNames="MaintenanceID" EmptyDataText="No Record Found" OnRowDeleting="GridView1_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="VehicleOwnedType" HeaderText="Vehicle Owned Type" />
                                    <asp:BoundField DataField="VehicleNo" HeaderText="Vehicle No" />
                                    <asp:BoundField DataField="Agency" HeaderText="Agency" />
                                    <asp:BoundField DataField="OwnedTotRunupMonthly" HeaderText="Total Run-up Monthly" />
                                    <asp:BoundField DataField="OwnedFuelExpences" HeaderText="Fuel Expences" />
                                    <asp:BoundField DataField="OwnedOtherExpensesOnVehicle" HeaderText="Other Expenses On Vehicle" />
                                    <asp:BoundField DataField="OwnedExpensesDetails" HeaderText="Expenses Details" />
                                    <asp:BoundField DataField="OwnedServicingDate" HeaderText="Servicing Date" />
                                    <asp:BoundField DataField="OwnedTotalKM" HeaderText="Total KM" />
                                    <asp:BoundField DataField="OwnedTotalExpensesInServicing" HeaderText="Total Expenses In Servicing" />
                                    <asp:BoundField DataField="OwnedExpensesBrief" HeaderText="Expenses Brief" />
                                    <asp:BoundField DataField="OwnedNextServicingDueDate" HeaderText="Next Servicing Due Date" />
                                    <asp:BoundField DataField="OwnedOtherInfo" HeaderText="Other Info" />
                                    <asp:BoundField DataField="HiredTotalRunupMonthly" HeaderText="Total Run-up Monthly KM" />
                                    <asp:BoundField DataField="BillNo" HeaderText="Bill No" />
                                    <asp:BoundField DataField="BillAmount" HeaderText="Bill Amount" />
                                    <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Delete" runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Item Name will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>--%>
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






        $(document).ready(function () {
            $('.datatableOwned').DataTable({

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
                        title: $('h3').text(),
                        exportOptions: {
                            //columns: ':not(.no-print)'
                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14,15]
                        },
                        footer: true,
                        autoPrint: true
                    }, {
                        extend: 'excel',
                        text: '<i class="fa fa-file-excel-o"></i> Excel',
                        title: $('h3').text(),
                        //title: 'Supplier Order Bank Payment',
                        exportOptions: {
                            orthogonal: 'sort',
                            format: {
                                body: function (data, row, column, node) {
                                    data = data.trim();
                                    data = column === 4 ? "\0" + data : data;
                                    return data.replace(/(&nbsp;|<([^>]+)>)/ig, "");
                                    //var data = data.find("span").text();
                                    //return data;
                                }
                            },
                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14,15]
                        },
                        //customizeData: function (data) {
                        //    for (var i = 0; i < data.body.length; i++) {
                        //        for (var j = 0; j < data.body[i].length; j++) {
                        //            if (j ==4) {
                        //                data.body[i][j] = '\u200C' + data.body[i][j];
                        //            }

                        //        }
                        //    }
                        //},
                        // footer: true
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
            //t.on('order.dt search.dt', function () {
            //    t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            //        cell.innerHTML = i + 1;
            //    });
            //}).draw();
        });




        $(document).ready(function () {
            $('.datatableHired').DataTable({

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
                        title: $('h3').text(),
                        exportOptions: {
                            //columns: ':not(.no-print)'
                            columns: [0, 1, 2, 3, 4, 5, 6]
                        },
                        footer: true,
                        autoPrint: true
                    }, {
                        extend: 'excel',
                        text: '<i class="fa fa-file-excel-o"></i> Excel',
                        title: $('h3').text(),
                        // title: 'Vehicle Maintenance Report',
                        exportOptions: {
                            orthogonal: 'sort',
                            format: {
                                body: function (data, row, column, node) {
                                    data = data.trim();
                                    data = column === 4 ? "\0" + data : data;
                                    return data.replace(/(&nbsp;|<([^>]+)>)/ig, "");
                                    //var data = data.find("span").text();
                                    //return data;
                                }
                            },
                            columns: [0, 1, 2, 3, 4, 5, 6]
                        },

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
        });
    </script>



</asp:Content>

