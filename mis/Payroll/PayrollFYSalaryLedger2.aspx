<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollFYSalaryLedger2.aspx.cs" Inherits="mis_Payroll_PayrollFYSalaryLedger2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <%--<link href="../css/StyleSheet.css" rel="stylesheet" />--%>
    <%--<style>
        .show_detail {
            margin-top: 24px;
        }

        .table > tbody > tr > th {
            padding: 5px;
        }

        a:hover {
            color: red;
        }

        /*tr:hover td {
            background-color: #fefefe !important;
        }*/
        table.dataTable tbody td, table.dataTable thead td {
            padding: 5px 5px !important;
        }

        table.dataTable tbody th, table.dataTable thead th {
            padding: 8px 10px !important;
        }

        table.dataTable thead th, table.dataTable thead td {
            padding: 5px 7px;
            border-bottom: none !important;
        }

        table.dataTable tfoot th, table.dataTable tfoot td {
            border-bottom: none !important;
        }

        table.dataTable.no-footer {
            border-bottom: none !important;
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

        tbody tr td:not(:first-child), tfoot tr td:not(:first-child) {
            text-align: right !important;
        }

        .Earning, .Deduction {
            font-weight: 600;
        }


        @media print {
            table td {
                font-size: 14px !important;
            }

            table.dataTable tbody td, table.dataTable thead td, table.dataTable thead th {
                padding: 2px 2px !important;
                border: 1px dashed #ddd !important;
            }

            span#ctl00_ContentBody_lblEmpDetail {
                font-size: 14px !important;
            }

            .lblEmpDetail {
                background: red !important;
                font-style: italic;
            }

            .print_button {
                font-size: 15px !important;
            }
        }

        .print_heading {
            font-size: 14px !important;
        }

        p.heading {
            font-size: 17px;
            font-weight: 600;
            margin-bottom: 0px;
        }

        p.subheading {
            font-size: 15px !important;
        }

        .emp_detail {
            font-size: 14px !important;
        }

        .emp_detail2 {
            font-size: 14px !important;
        }

    </style>--%>

    <style>      
        .header {
  padding : 20px 0 20px 0;
  margin-bottom:20px;
  overflow :auto;
  
}
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
              @page {
                size: landscape;
            }
               @page {
                margin: 0 0 0 0;
            }
               .pagebreak { page-break-before: always; }
              
            }
          }
         .table1-bordered > thead > tr > th, .table1-bordered > tbody > tr > th, .table1-bordered > tfoot > tr > th, .table1-bordered > thead > tr > td, .table1-bordered > tbody > tr > td, .table1-bordered > tfoot > tr > td {
            border: 1px solid #000000 !important;
        }
        .thead
        {
            display:table-header-group;
        }
        .text-center{
            text-align: center;
        }
        .text-right{
            text-align: right;
        }
        .table1 > tbody > tr > td, .table1 > tbody > tr > th, .table1 > tfoot > tr > td, .table1 > tfoot > tr > th, .table1 > thead > tr > td, .table1 > thead > tr > th {
            padding: 2px 5px;
           
        }   
            
           .lblpagenote {
            display: block;
            background: #e0eae7;
            text-align: -webkit-center;
            margin-bottom: 12px;
            padding: 10px;
        }   
           
           .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('../image/loader/ProgressImage.gif') 50% 50% no-repeat rgba(0, 0, 0, 0.3);
        }   
            .table1 > tbody > tr > td, .table1 > tbody > tr > th, .table1 > tfoot > tr > td, .table1 > tfoot > tr > th, .table1 > thead > tr > td, .table1 > thead > tr > th {
    padding: 1px;
    font-size: 10px;
    border: 1px solid black !important;
    font-family: verdana;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content noprint">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Payroll Salary Details For <span style="background: #2196F3; color: #ffffff; padding: 5px;">Advance Tax Deduction</span></h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Office Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlOfficeName" runat="server" class="form-control select2" AutoPostBack="True" Enabled="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Employee<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlEmployee" runat="server" class="form-control select2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Year<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateform1();" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group"></div>
                    <div class="row">
                        <div class="col-md-12 table-responsive" style="max-height: 400px;">
                            <asp:Label ID="lblEmpDetail" class="lblEmpDetail" runat="server" Text="" Visible="true" Style="color: black; font-size: 17px;"></asp:Label>
                            <br />
                            <br />
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" class="datatable table table-hover table-bordered pagination-ys" ShowFooter="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Month" ItemStyle-CssClass="alignR">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNetsalary" CssClass='<%# Eval("Particular")%>' runat="server" Text='<%# Eval("Particular")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="MYear" HeaderText="Year" />
                                    <asp:BoundField DataField="BasicSalary" HeaderText="BASIC" />
                                    <asp:BoundField DataField="DA" HeaderText="DA" />
                                    <asp:BoundField DataField="HRA" HeaderText="HRA" />
                                    <asp:BoundField DataField="Conv" HeaderText="Conv" />
                                    <asp:BoundField DataField="Ord" HeaderText="Ord" />
                                    <asp:BoundField DataField="Wash" HeaderText="Wash" />
                                    <asp:BoundField DataField="OtherEarning" HeaderText="Other" />
                                    <asp:BoundField DataField="EarningTotal" HeaderText="Total Earning" />
                                    <asp:BoundField DataField="EPF" HeaderText="EPF" />
                                    <asp:BoundField DataField="ADA" HeaderText="ADA" />
                                    <asp:BoundField DataField="ITax" HeaderText="ITax" />
                                    <asp:BoundField DataField="PTax" HeaderText="PTax" />
                                    <asp:BoundField DataField="OtherDeduction" HeaderText="Other" />
                                    <asp:BoundField DataField="DeductionTotal" HeaderText="Total Deduction" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="row">
                                <div class="col-md-2" style="margin-top: 20px;">
                                       <div class="form-group">
                                        <asp:Button ID="btnPrint" runat="server" Visible="false" OnClick="btnPrint_Click"  Text="Print" CssClass="btn btn-primary" />
                                           <asp:Button ID="btnExcel" CssClass="btn btn-success" Visible="false" Text="Excel" runat="server" OnClick="btnExcel_Click" />
                                           </div>
                                </div>
                        <div class="row">
                        <div class="col-md-12 table-responsive">
                                 <div id="div_page_content" runat="server" class="page_content"></div>
                            </div>
                            </div>

                </div>
            </div>
        </section>
         <section class="content">
            <div id="Print" runat="server" class="NonPrintable"></div>   
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">


        function validateform() {
            var msg = "";
            if (document.getElementById('<%=ddlYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Financial Year. \n";
            }
            if (document.getElementById('<%=ddlEmployee.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Employee Name. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (confirm("Do you really want to see details ?")) {
                    return true;
                }
                else {
                    return false;
                }

            }
        }

    </script>
    <%--<link href="../finance/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../finance/css/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="../finance/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="../finance/js/jquery.dataTables.min.js"></script>
    <script src="../finance/js/jquery.dataTables.min.js"></script>
    <script src="../finance/js/dataTables.bootstrap.min.js"></script>
    <script src="../finance/js/dataTables.buttons.min.js"></script>
    <script src="../finance/js/buttons.flash.min.js"></script>
    <script src="../finance/js/jszip.min.js"></script>
    <script src="../finance/js/pdfmake.min.js"></script>
    <script src="../finance/js/vfs_fonts.js"></script>
    <script src="../finance/js/buttons.html5.min.js"></script>
    <script src="../finance/js/buttons.print.min.js"></script>
    <script src="../finance/js/buttons.colVis.min.js"></script>
    <script>
        $('.datatable').DataTable({
            paging: false,
            ordering: false,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }
            ],
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<""tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: '<p class="print_button">' + $('.print_heading').html() + '</p>',
                    exportOptions: {
                        //columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('.lblEmpDetail').text(),
                    filename: 'FYLedger',
                    //title: $('.lblEmpDetail').text(),
                    exportOptions: {
                        //columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
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


        //$(".Earning").parent().parent().css("color", "green");
        //$(".Deduction").parent().parent().css("color", "red");
        $(".Deduction,.Earning").css("color", "black");
        $(".Deduction,.Earning").parent().css("background", "#eaeaea");--%>
    </script>
</asp:Content>


