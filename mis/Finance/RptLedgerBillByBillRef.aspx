<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptLedgerBillByBillRef.aspx.cs" Inherits="mis_Finance_RptLedgerBillByBillRef" %>

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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->

        <section class="content">

            <div class="box box-success">
                <div class="box-header Hiderow">
                    <h3 class="box-title">Ledger Wise Pending References</h3>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">

                    <div class="row Hiderow">
                        <div class="col-md-5">
                            <div class="form-group">
                                <label>Office Name</label><span style="color: red">*</span>
                                <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control select1 select2" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="form-group">
                                <label>List Of Ledger</label><span style="color: red">*</span>
                                <asp:DropDownList runat="server" ID="ddlLedger" CssClass="form-control select2" AutoPostBack="false" OnSelectedIndexChanged="ddlLedger_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-block btn-success Aselect1" Style="margin-top: 24px;" Text="Search" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblGrid" runat="server" Style="font-size: 22px;" Text=""></asp:Label>
                            <br />
                            <asp:Label ID="lblExecTime" runat="server" CssClass="ExecTime"></asp:Label>
                            <br />
                            <br />  
                            <asp:GridView runat="server" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" ID="GridViewRefDetail" AutoGenerateColumns="false" ShowFooter="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%" HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="VoucherTx_Date" HeaderText="Date" ItemStyle-Width="10%" HeaderStyle-Width="10%" />
                                    <asp:BoundField DataField="BillByBillTx_Ref" HeaderText="Name" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="20%" />

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

    <script>
        function validateform() {
            var msg = "";
            if (document.getElementById('<%=ddlOffice.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Office. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
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
    <script src="../../../mis/HR/js/jquery.dataTables.min.js"></script>
    <script src="../../../mis/HR/js/dataTables.bootstrap.min.js"></script>
    <script src="../../../mis/HR/js/dataTables.buttons.min.js"></script>
    <%-- <script src="js/buttons.flash.min.js"></script>--%>
    <script src="../../../mis/HR/js/jszip.min.js"></script>
    <%-- <script src="js/pdfmake.min.js"></script>
    <script src="js/vfs_fonts.js"></script>--%>
    <script src="../../../mis/HR/js/buttons.html5.min.js"></script>
    <script src="../../../mis/HR/js/buttons.print.min.js"></script>
    <script src="https://cdn.datatables.net/plug-ins/1.10.19/api/sum().js"></script>


    <script>
        //$("input:checkbox:not(:checked)").each(function () {
        //    var column = "table ." + $(this).attr("name");
        //    $(column).hide();
        //});

        //$("input:checkbox").click(function () {
        //    var column = "table ." + $(this).attr("name");
        //    $(column).toggle();
        //});



        $(document).ready(function () {

            //if ($("#chkClosingBal").prop("checked") == true) {
            //    var column = "table ." + $("#chkClosingBal").attr("name");
            //    $(column).hide();
            //}
            //else if ($("#chkClosingBal").prop("checked") == false) {


            //    var column = "table ." + $("#chkClosingBal").attr("name");
            //    $(column).toggle();
            // }


            $('.datatable').DataTable({
                /******Footer sum********/


                //drawCallback: function () {
                //    var api2 = this.api();
                //    $(api2.table().footer()).html(
                //      api2.column(4, { page: 'current' }).data().sum()
                //    );
                //    var api = this.api();
                //    $(api.table().footer()).html(
                //      api.column(5, { page: 'current' }).data().sum()
                //    );

                //},


                /**************/

                paging: false,

                columnDefs: [{
                    orderable: false
                }],
                "bSort": false,
                // "order": [[0, 'asc']],

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
                        title: $('.report-title').text(),
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6]
                        },
                        footer: true,
                        autoPrint: true
                    }, {
                        extend: 'excel', footer: true,
                        text: '<i class="fa fa-file-excel-o"></i> Excel',
                        title: $('.report-title').text(),
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6],
                            stripHtml: false,
                            format: {
                                body: function (data, column, row) {
                                    return (column === 1 && column === 5) ? data.replace(/\n/g, '"&CHAR(10)&CHAR(13)&"') : data.replace(/(&nbsp;|<([^>]+)>)/ig, "");;
                                }
                            }
                        },
                        customize: function (xlsx) {
                            var sheet = xlsx.xl.worksheets['sheet1.xml'];
                            $('row c', sheet).attr('s', '55');
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
                },

            });
        });



        //if ($("#chkClosingBal").prop("checked") == true) {
        //    var column = "table ." + $("#chkClosingBal").attr("name");
        //    $(column).hide();
        //}
        //else if ($("#chkClosingBal").prop("checked") == false) {


        //    var column = "table ." + $("#chkClosingBal").attr("name");
        //    $(column).toggle();
        //}
    </script>
</asp:Content>


