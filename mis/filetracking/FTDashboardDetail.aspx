<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FTDashboardDetail.aspx.cs" Inherits="mis_filetracking_FTDashboardDetailNew" %>

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

        .show_detail {
            margin-top: 24px;
        }
        
        /*table {
            white-space: nowrap;
            font-size: 13px;
        }


        table.dataTable td, table.dataTable th {
            border-color: rgba(158, 158, 158, 0.32);
            word-wrap: break-word !important;
            width: 50px !important;
        }

        tr th {
            border: 1px solid #e0e0e0 !important;
        }*/
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
                                    <h3 class="box-title" id="Label1">FILE TRACKING DASHBOARD DETAIL</h3>
                                </div>
                            </div>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div id="dvSearchForm" runat="server">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>From Date<span style="color: red;">*</span></label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" runat="server" placeholder="Select From Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>To Date</label><span style="color: red">*</span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control DateAdd" placeholder="Select To Date.." autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Button ID="btn" CssClass="btn btn-md btn-primary show_detail Aselect1 btn-flat" runat="server" Text="Search" OnClick="btn_Click" OnClientClick="return validateform();" />
                                        </div>
                                    </div>
                                </div>

                                <div id="FileDetail" runat="server">
                                    <div class="col-md-12">
                                        <asp:Label ID="lblMsg" runat="server" Text="" Style="font-size: 17px;"></asp:Label>
                                        <asp:GridView ID="GridView1" runat="server" DataKeyNames="File_ID" class="datatable table table-striped table-hover table-bordered pagination-ys" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FILE PRIORITY">
                                                    <ItemTemplate>
                                                        <asp:Label Text='<%# Eval("File_Priority".ToString())%>' runat="server" ID="FilePriority"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="File_No" HeaderText="FILE NO" />
                                                <%--<asp:BoundField DataField="File_Type" HeaderText="FILE / NOTE SHEET TYPE" />--%>
                                                <asp:BoundField DataField="File_Title" HeaderText="FILE TITLE" />
                                                <%--<asp:BoundField DataField="StatusOfFile" HeaderText="TYPE OF FILE" />--%>
                                                <asp:BoundField DataField="ReceiveDate" HeaderText="RECEIVING DATE" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hylnkViewDetail" Target="_blank" runat="server" CssClass="label label-default" Text="View Detail" NavigateUrl='<%# "FTDashboardComents.aspx?File_ID=" + APIProcedure.Client_Encrypt(Eval("File_ID").ToString())%>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                                <div id="OutwardDetail" runat="server">
                                    <div class="col-md-12">
                                        <asp:Label ID="lblOutwardmsg" runat="server" Text="" Style="font-size: 17px;"></asp:Label>
                                        <asp:GridView ID="GridView2" runat="server" DataKeyNames="Outward_ID" class="datatable table table-striped table-hover table-bordered pagination-ys" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="LetterNo" HeaderText="LETTER NO" />
                                               <%-- <asp:BoundField DataField="File_Type" HeaderText="DOCUMENT TYPE" />--%>
                                                <asp:BoundField DataField="DispatchDate" HeaderText="DISPATCH DATE" />
                                                <asp:BoundField DataField="EndorsementNo" HeaderText="CC NUMBER" />
                                                <asp:BoundField DataField="LetterSubject" HeaderText="LETTER SUBJECT" />
                                                <asp:BoundField DataField="LetterReceiveFrom" HeaderText="LETTER RECEIVE FROM" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hylnkViewDetail" ToolTip='<%# Eval("Outward_ID").ToString() %>' Target="_blank" runat="server" CssClass="label label-default" Text="View Detail" NavigateUrl='<%# "FTDashboardComents.aspx?Outward_ID=" + APIProcedure.Client_Encrypt(Eval("Outward_ID").ToString())%>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                                <div id="InwardDetail" runat="server">
                                    <div class="col-md-12">
                                        <asp:Label ID="lblInwardmsg" runat="server" Text="" Style="font-size: 17px;"></asp:Label>
                                        <asp:GridView ID="GridView3" runat="server" DataKeyNames="File_ID" class="datatable table table-striped table-hover table-bordered pagination-ys" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FILE PRIORITY">
                                                    <ItemTemplate>
                                                        <asp:Label Text='<%# Eval("File_Priority".ToString())%>' runat="server" ID="FilePriority"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="File_No" HeaderText="LETTER NO" />
                                                <%--<asp:BoundField DataField="File_Type" HeaderText="DOCUMENT TYPE" />--%>
                                                <asp:BoundField DataField="File_Title" HeaderText="LETTER TITLE" />
                                                <%--<asp:BoundField DataField="StatusOfFile" HeaderText="TYPE OF LETTER" />--%>
                                                <asp:BoundField DataField="ReceiveDate" HeaderText="RECEIVING DATE" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hylnkViewDetail" Target="_blank" runat="server" CssClass="label label-default" Text="View Detail" NavigateUrl='<%# "FTDashboardComents.aspx?File_ID=" + APIProcedure.Client_Encrypt(Eval("File_ID").ToString())%>'></asp:HyperLink>
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
            info: false,
            ordering: false,
            lengthChange: false,
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
                        columns: [0, 1, 2, 3, 4, 5]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
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



        $('#txtFromDate').change(function () {
            //debugger;
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
            //debugger;
            var start = $('#txtFromDate').datepicker('getDate');
            var end = $('#txtToDate').datepicker('getDate');
            if (start > end) {
                if ($('#txtToDate').val() != "") {
                    alert("To Date can not be less than From Date.");
                    $('#txtToDate').val("");
                }
            }

        });


        function validateform() {
            var msg = "";
            $("#valtxtDate").html("");
            if (document.getElementById('<%=txtToDate.ClientID%>').value.trim() == "") {
                msg = msg + "Please Select To Date. \n";
            }
            if (document.getElementById('<%=txtFromDate.ClientID%>').value.trim() == "") {
                msg = msg + "Please Select From Date. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
        }
    </script>
</asp:Content>



