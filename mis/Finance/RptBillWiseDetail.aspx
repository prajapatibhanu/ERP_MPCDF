<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptBillWiseDetail.aspx.cs" Inherits="mis_Finance_RptBillWiseDetail" %>

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

        /*.pay-sheet table {
            border: 1px solid #ddd;
        }*/

        @media print {
            .Hiderow, .main-footer {
                display: none;
            }

            .box {
                border: none;
            }

            th {
                background-color: #ddd;
                text-decoration: solid;
            }

            .tblheadingslip {
                font-size: 8px !important;
                background: black;
                color: red;
            }
        }

        .align-right {
            text-align: right !important;
            width: 10% !important;
        }

        span.Ledger_Amt {
            max-width: 30%;
            display: inline;
            float: right;
        }

        span.Ledger_Name {
            max-width: 70%;
            display: inline;
            float: left;
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

        .Scut {
            color: tomato;
        }

        .aa {
            font-weight: bold;
            font-size: 16px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->

        <section class="content">

            <div class="box box-success">
                <div class="box-header Hiderow">
                    <h3 class="box-title">Debtor's Pending Reference</h3>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">

                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Office Name</label><span style="color: red">*</span>
                                <asp:DropDownList runat="server" ID="ddlOffice" ClientIDMode="Static" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Financial Year</label><span style="color: red">*</span>
                                <asp:DropDownList runat="server" ID="ddlYear" ClientIDMode="Static" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                        </div>                        
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Item Group</label><span style="color: red">*</span>
                                <asp:DropDownList runat="server" ID="ddlGroup" ClientIDMode="Static" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Yojna</label><span style="color: red">*</span>
                                <asp:DropDownList runat="server" ID="ddlYojna" ClientIDMode="Static" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnSearch" ClientIDMode="Static" runat="server" CssClass="btn btn-block btn-success" Style="margin-top: 24px;" Text="Search" OnClientClick="return validateform();" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblheadingFirst" runat="server" Text=""></asp:Label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblGrid" runat="server" Style="font-size: 22px;" Text=""></asp:Label>
                            <asp:Label ID="lblExecTime" runat="server" CssClass="ExecTime"></asp:Label>
                            <br /><br />
                            <asp:GridView runat="server" CssClass="datatable table table-bordered" ShowHeaderWhenEmpty="true" ID="GridViewRefDetail" AutoGenerateColumns="false"
                                OnDataBound="GridViewRefDetail_DataBound" OnRowCreated="GridViewRefDetail_RowCreated">
                                <Columns>
                                    <asp:TemplateField HeaderText="क्रं" ItemStyle-Width="5%" HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="BillByBillTx_FY" HeaderText="वर्ष" ItemStyle-Width="10%" HeaderStyle-Width="10%" />
                                    <asp:BoundField DataField="BillByBillTx_OrderNo" HeaderText="विभाग का आदेश क्रं" />
                                    <asp:BoundField DataField="BillByBillTx_OrderDate" HeaderText="आदेश दिंनाक" />
                                    <asp:BoundField DataField="Ledger_Name" HeaderText="सामग्री प्राप्तकर्ता एस.ए.डी.ओ." />
                                    <asp:BoundField DataField="BillByBillTx_ItemGroup" HeaderText="सामग्री का नाम" />
                                    <asp:BoundField DataField="BillByBillTx_Ref" HeaderText="निगम देयक क्रं" />
                                    <asp:BoundField DataField="BillByBillTx_Date" HeaderText="बिल-दिनांक" />
                                    <asp:BoundField DataField="BillByBillTx_Amount" HeaderText="शेष राशि" DataFormatString="{0:N2}" />
                                    <asp:BoundField DataField="SchemeTx_Name" HeaderText="योजना" />
                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>

                </div>

            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <%-- <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>--%>
    <link href="css/dataTables.bootstrap.min.css" rel="stylesheet" />
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
    <script>
        $('.datatable').DataTable({
            paging: true,
            "pageLength": 100,
            "searching": false,
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
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
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

        function validateform() {
            debugger;
            var msg = "";
            if (document.getElementById('<%=ddlOffice.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Office. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=btnSearch.ClientID%>').value.trim() == "Search") {

                    document.querySelector('.popup-wrapper').style.display = 'block';
                    return true

                }
                else {
                    return false;
                }

            }

        }
        function PrintPage() {
            window.print();
        }



        $('#txtFromDate').change(function () {
            debugger;
            var start = $('#txtFromDate').datepicker('getDate');
            var end = $('#txtToDate').datepicker('getDate');

            if ($('#txtToDate').val() != "") {
                if (start > end) {

                    if ($('#txtFromDate').val() != "") {
                        alert("From date should not be greater than To Date.");
                        $('#txtFromDate').val("");
                    }
                }
            }
        });
        $('#txtToDate').change(function () {
            debugger;
            var start = $('#txtFromDate').datepicker('getDate');
            var end = $('#txtToDate').datepicker('getDate');

            if (start > end) {

                if ($('#txtToDate').val() != "") {
                    alert("To Date can not be less than From Date.");
                    $('#txtToDate').val("");
                }
            }

        });
    </script>

</asp:Content>
