<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DailyProductionEntry.aspx.cs" Inherits="mis_Finance_DailyProductionEntry" %>

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
            .Hiderow, .hide_print, .main-footer, .dataTables_filter, .dataTables_length, .dt-buttons, .dataTables_info, .dataTables_paginate paging_simple_numbers, .pagination {
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

            .lblheadingFirst p {
                text-align: center !important;
                font-size: 10px !important;
            }
        }
          .inline-rb label {
            margin-left: 5px;
        }

    </style>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <div class="box box-success">
                <div class="box-header hidden-print">
                    <h3 class="box-title">Daily Production Entry</h3>
                     <input type="button"  class="btn btn-default pull-right" onclick="tableToExcel('tableData', 'W3C Example Table')" value="Export to Excel" />
                    <input type="button" class="btn btn-default pull-right" onclick="window.print();" value="Print" />

                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row hidden-print">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Date<span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtTxnDate" runat="server" placeholder="Select Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static" AutoPostBack="true" OnTextChanged="txtTxnDate_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Item Name<span style="color: red;"> *</span></label>
                                <asp:DropDownList ID="ddlitems" runat="server" Width="100%" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Item Unit</label>
                                <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control select2">
                                    <asp:ListItem Text="Select"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row hidden-print">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Production Quantity<span style="color: red;">*</span></label>

                                <asp:TextBox ID="txtProductionQuantity" runat="server" placeholder="Enter Production Quantity.." MaxLength="18" onkeypress="return validateDecUnit(this,event);" class="form-control" autocomplete="off" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Production Cumulative Quantity<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtProductionCumulativeQuantity" runat="server" placeholder="Enter Production Cumulative Qty..." MaxLength="18" onkeypress="return validateDecUnit(this,event);" class="form-control" autocomplete="off" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Sale Quantity<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtSaleQuantity" runat="server" placeholder="Enter Sale Quantity.." MaxLength="18" onkeypress="return validateDecUnit(this,event);" class="form-control" autocomplete="off" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Sale Cumulative Quantity<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtSaleCumulativeQuantity" runat="server" placeholder="Enter Sale Cumulative Qty.." MaxLength="18" onkeypress="return validateDecUnit(this,event);" class="form-control" autocomplete="off" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success btn-block" Style="margin-top: 21px;" OnClick="btnSave_Click" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a class="btn btn-default btn-block" href="DailyProductionEntry.aspx" style="margin-top: 21px;">Cancel</a>

                            </div>
                        </div>
                    </div>
                    <div class="form-group">  
                        <script type="text/javascript">
                            var tableToExcel = (function () {
                                var uri = 'data:application/vnd.ms-excel;base64,'
                                  , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--><meta http-equiv="content-type" content="text/plain; charset=UTF-8"/></head><body><table>{table}</table></body></html>'
                                  , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
                                  , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
                                return function (table, name) {
                                    if (!table.nodeType) table = document.getElementById(table)
                                    var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
                                    window.location.href = uri + base64(format(template, ctx))
                                }
                            })()
                        </script>
                    </div>
                    <div class="row" id="tableData">
                        <div class="col-md-12" style="text-align:center;">
                            <table style="width:100%;">
                                <tr>
                                    <td colspan="1"></td>
                                     <td colspan="4"><asp:Label ID="lblheadingFirst" style="font-size:18px;"  runat="server" Text=""></asp:Label></td>
                                     <td colspan="1"></td>
                                </tr>
                            </table>
                            
                        </div>
                        <div class="col-md-12">

                            <asp:GridView ID="GridView1" DataKeyNames="ID" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" ClientIDMode="Static" OnRowDeleting="GridView1_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="Date" ItemStyle-Width="12%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Eval("TxnDate").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" Text='<%# Eval("ItemName").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnitName" Text='<%# Eval("UnitName").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Production Quantity" ItemStyle-Width="13%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProductionQuantity" Text='<%# Eval("ProductionQuantity").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Production Cumulative Quantity" ItemStyle-Width="13%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProductionCumulativeQuantity" Text='<%# Eval("ProductionCumulativeQuantity").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sale Quantity" ItemStyle-Width="13%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSaleQuantity" Text='<%# Eval("SaleQuantity").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sale Cumulative Quantity" ItemStyle-Width="13%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSaleCumulativeQuantity" Text='<%# Eval("SaleCumulativeQuantity").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="13%" HeaderStyle-CssClass="hidden-print" ItemStyle-CssClass="Hiderow" ControlStyle-CssClass="hidden-print">
                                        <ItemTemplate>

                                            <%-- <asp:LinkButton ID="Delete" runat="server" CommandName="Delete" Text="Delete" Visible='<%# Eval("Status").ToString() == "0" ? true :false  %>' OnClientClick="return confirm('The Record will be deleted. Are you sure want to continue?');" CssClass="label label-danger"></asp:LinkButton>--%>
                                            <asp:LinkButton ID="Delete" runat="server" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Record will be deleted. Are you sure want to continue?');" CssClass="label label-danger"></asp:LinkButton>

                                        </ItemTemplate>
                                    </asp:TemplateField>
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
        $('.datatable').DataTable({
            paging: true,
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
                    title: $('.lblheadingFirst').html(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6]
                    },
                    footer: true,
                    autoPrint: true
                }
                //,
                //{
                //    extend: 'excel',
                //    text: '<i class="fa fa-file-excel-o"></i> Excel',
                //    title: $('.lblheadingFirst').text(),
                //    exportOptions: {
                //        columns: [0, 1, 2, 3, 4, 5, 6]
                //    },
                //    footer: true
                //}
                ],
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


        function validateDecUnit(el, evt) {
            var digit = 5;
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (digit == 0 && charCode == 46) {
                return false;
            }

            var number = el.value.split('.');
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            //just one dot (thanks ddlab)
            if (number.length > 1 && charCode == 46) {
                return false;
            }
            //get the carat position
            var caratPos = getSelectionStart(el);
            var dotPos = el.value.indexOf(".");
            if (caratPos > dotPos && dotPos > -1 && (number[1].length > digit - 1)) {
                return false;
            }
            return true;
        }

    </script>
</asp:Content>
