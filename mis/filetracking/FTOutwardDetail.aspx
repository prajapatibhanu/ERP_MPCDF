<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FTOutwardDetail.aspx.cs" Inherits="mis_filetracking_FTOutwardDetail" %>

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
                            <h3 class="box-title" id="Label1">Outward Detail</h3>
                            <asp:Label ID="lblMsg" runat="server" Text="" Visible="true"></asp:Label>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblMsg2" runat="server" Text="" Visible="true" Style="color: red; font-size: 17px;"></asp:Label>
                                    <asp:GridView ID="GridView1" DataKeyNames="Outward_ID" PageSize="50" runat="server" class="datatable table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderText="क्रमांक" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="LetterNo" HeaderText="पत्र संख्या" />
                                            <asp:BoundField DataField="LetterSubject" HeaderText="पत्र का विषय" />
                                            <asp:BoundField DataField="DispatchDate" HeaderText="Dispatch Date" />
                                            <asp:BoundField DataField="EndorsementNo" HeaderText="Endorsement Number" />
                                            <asp:BoundField DataField="LetterReceiveFrom" HeaderText="पत्र प्राप्त किया गया" />
                                            <asp:BoundField DataField="AddressTo" HeaderText="पत्र भेजा गया" />
                                            <asp:TemplateField HeaderText="पत्र का विवरण">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" CssClass="label label-default" runat="server" CommandName="select">View More</asp:LinkButton>
                                                    <asp:LinkButton ID="LinkButton2" CssClass="label label-default" runat="server" CommandName="select" OnClick="LinkButton2_Click">Edit</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div id="myModal" class="modal fade" role="dialog">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Outward Letter Detail</h4>
                                    </div>
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <fieldset>
                                                    <legend>Letter Detail</legend>
                                                    <div class="table-responsive">
                                                        <asp:DetailsView ID="DetailsView1" runat="server" CssClass="table table-bordered table-striped table-hover" AutoGenerateRows="false">
                                                            <Fields>
                                                                <asp:BoundField DataField="LetterNo" HeaderText="पत्र संख्या" />
                                                                <asp:BoundField DataField="LetterSubject" HeaderText="पत्र का विषय" />
                                                                <asp:BoundField DataField="DispatchDate" HeaderText="Dispatch Date" />
                                                                <asp:BoundField DataField="EndorsementNo" HeaderText="Endorsement Number" />
                                                                <asp:BoundField DataField="LetterReceiveFrom" HeaderText="पत्र प्राप्त किया गया" />
                                                                <asp:BoundField DataField="AddressTo" HeaderText="पत्र भेजा गया" />
                                                                <asp:BoundField DataField="Remark" HeaderText="टिप्पणी" />
                                                                <asp:TemplateField HeaderText="पत्र दस्तावेज़">
                                                                    <ItemTemplate>
                                                                        <a href='<%# Eval("Doc1") %>' target="_blank" class="label label-info"><%# Eval("Doc1").ToString() != "" ? "View" : "" %></a>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="ForwardToDepartment" HeaderText="विभाग को भेजा गया" />
                                                                <asp:BoundField DataField="ForwardToOfficer" HeaderText="अधिकारी को भेजा गया" />
                                                            </Fields>
                                                        </asp:DetailsView>
                                                    </div>
                                                </fieldset>
                                                <fieldset>
                                                    <legend>Letter Copy To</legend>
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="GridView2" PageSize="50" runat="server" class="datatable table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="क्रमांक">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="CopyTo" HeaderText="लैटर कॉपी" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </fieldset>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
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
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4]
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

        function callalert() {
            $("#myModal").modal('show');
        }
    </script>
</asp:Content>

