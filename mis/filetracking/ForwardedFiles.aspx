<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ForwardedFiles.aspx.cs" Inherits="mis_filetracking_ForwardedFiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>

        .table > tbody > tr > th {
            padding: 5px;
        }

        table.dataTable tbody th, table.dataTable thead th {
            padding: 2px 3px !important;
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
        a.dt-button.buttons-collection.buttons-colvis, a.dt-button.buttons-collection.buttons-colvis:hover {
            background: #EF5350;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.dt-button.buttons-excel.buttons-html5, a.dt-button.buttons-excel.buttons-html5:hover {
            background: #ff5722c2;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.dt-button.buttons-print, a.dt-button.buttons-print:hover {
            background: #e91e639e;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) --
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">
                    <!-- general form elements -->
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">Forwarded Files </h3>
                            <asp:Label ID="lblMsg" runat="server" Text="" Visible="true"></asp:Label>
                        </div>
                        <div class="box-body">
                            <div class="row">
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
                                            <asp:Button ID="btn" CssClass="btn btn-md btn-primary show_detail Aselect1 btn-flat" runat="server" Text="Search" style="margin-top:25px;" OnClientClick="return validateform();" OnClick="btn_Click"/>
                                        </div>
                                    </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 table-responsive">
                                    <asp:Label ID="lblMsg1" runat="server" Text="" Visible="true" Style="color: red; font-size: 17px;"></asp:Label>
                                    <asp:GridView ID="GridView1" PageSize="50" runat="server" class="datatable table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" DataKeyNames="File_ID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderText="क्रमांक" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="प्राथमिकता">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("File_Priority".ToString())%>' runat="server" ID="FilePriority"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="File_Type" HeaderText="फाइल का प्रकार " />
                                            <asp:BoundField DataField="File_No" HeaderText="फ़ाइल / नोट शीट/ पत्र संख्या" />
                                            <asp:BoundField DataField="File_Title" HeaderText="दस्तावेज़ का शीर्षक" />
                                            <asp:BoundField DataField="QRCode" HeaderText="बार कोड" />
                                            <asp:BoundField DataField="File_UpdatedOn" HeaderText="फाइल / पत्र बनने की दिनांक" />
                                            <asp:BoundField DataField="FileReceivingDate" HeaderText="फाइल / पत्र प्राप्त करने की दिनांक" />
                                            <asp:BoundField DataField="Forwarded_UpdatedOn" HeaderText="फाइल / पत्र आगे बढ़ाने की दिनांक" />
                                            <asp:TemplateField HeaderText="फाइल / पत्र का विवरण">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" CssClass="label label-default" runat="server" CommandName="select"> View More</asp:LinkButton>
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
    <link href="../finance/css/dataTables.bootstrap.min.css" rel="stylesheet" />
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
            dom: 'Bfrtip',
            ordering: false,
            buttons: [
                 {
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: $('h3').text(),
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h3').text(),
                    footer: true
                }
            ]
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

