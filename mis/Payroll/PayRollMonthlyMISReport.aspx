<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayRollMonthlyMISReport.aspx.cs" Inherits="mis_Payroll_PayRollMonthlyMISReport" %>

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

        table {
            white-space: nowrap;
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
                                    <h3 class="box-title" id="Label1">Monthly MIS Report <span>(<a href="../HR/AdminSectionMaster.aspx" target="_blank" style="font-size=10px !important; color:blue;">List Of Section</a>)
                                    </span></h3>
                                </div>
                            </div>
                        </div>
                        <asp:label id="lblMsg" runat="server" text=""></asp:label>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office Name</label><span style="color: red">*</span>
                                        <asp:dropdownlist runat="server" id="ddlOffice_Name" cssclass="form-control" clientidmode="Static" onselectedindexchanged="ddlOffice_Name_SelectedIndexChanged" autopostback="true" enabled="false">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:dropdownlist>
                                        <small><span id="valddlOffice_Name" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Year <span class="text-danger">*</span></label>
                                        <asp:dropdownlist id="ddlFinancialYear" runat="server" cssclass="form-control">
                                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                        </asp:dropdownlist>
                                        <small><span id="valddlFinancialYear" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Month <span style="color: red;">*</span></label>
                                        <asp:dropdownlist id="ddlMonth" runat="server" class="form-control">
                                            <asp:ListItem Value="0">Select Month</asp:ListItem>
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
                                        </asp:dropdownlist>
                                        <small><span id="valddlMonth" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:button runat="server" cssclass="btn btn-success btn-block" text="Show" id="btnShow" onclientclick="return validateform();" onclick="btnShow_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <p style="color: #123456; font-size: 15px;" runat="server">
                                        <asp:label id="lblDeductionDetails" cssclass="lblDeductionDetails" runat="server" text=""></asp:label>
                                    </p>
                                </div>
                                <div class="col-md-12">
                                    <asp:gridview id="GridView1" runat="server" class="datatable table table-hover table-bordered pagination-ys" autogeneratecolumns="False" showheaderwhenempty="true" showfooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Section_Name" HeaderText="Section" />
                                            <asp:BoundField DataField="Section_No" HeaderText="Section Code No " />
                                            <asp:BoundField DataField="class1" HeaderText="Class-I" />
                                            <asp:BoundField DataField="class2" HeaderText="Class-II" />
                                            <asp:BoundField DataField="class3" HeaderText="Class-III" />
                                            <asp:BoundField DataField="class4" HeaderText="Class-IV" />
                                            <asp:BoundField DataField="GrossSalary" HeaderText="Gross Salary " /> 
                                            <asp:BoundField DataField="EpfSalary" HeaderText="Emp. EPF Con. " />
                                            <asp:BoundField DataField="GrandTotal" HeaderText="GRAND TOTAL  (GROSS+EMP EPF CON.) " />
                                        </Columns>
                                    </asp:gridview>
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
    <link href="css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="css/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="css/jquery.dataTables.min.css" rel="stylesheet" />
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
        $("#txtmonth").datepicker({
            format: "MM",
            viewMode: "months",
            minViewMode: "months",
            autoclose: true
        });
        function validateform() {
            var msg = "";
            $("#valddlOffice_Name").html("");
            $("#valddlFinancialYear").html("");
            $("#valtxtmonth").html("");

           <%-- if (document.getElementById('<%=ddlOffice_Name.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Office Name. \n";
                $("#valddlOffice_Name").html("Select Office Name.");
            }--%>
            if (document.getElementById('<%=ddlFinancialYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Year. \n";
                $("#valddlFinancialYear").html("Select year.");
            }
            if (document.getElementById('<%=ddlMonth.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Month. \n";
                $("#valtxtmonth").html("Select Month.");
            }

           <%-- if (document.getElementById('<%=txtmonth.ClientID%>').value == "") {
                msg = msg + "Select Month. \n";
                $("#valtxtmonth").html("Select Month.");
            }--%>
            if (msg != "") {
                alert(msg);
                return false;
            }
        }
        $(document).ready(function () {
            $('.datatable').DataTable({

                paging: false,
                bSort: false,
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
                        title: $('.lblDeductionDetails').attr('title'),
                        exportOptions: {
                            columns: ':not(.no-print)'
                        },
                        footer: true,
                        autoPrint: true
                    }, {
                        extend: 'excel',
                        text: '<i class="fa fa-file-excel-o"></i> Excel',
                        title: $('.lblDeductionDetails').text(),
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

