﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ItemListOfficeWise.aspx.cs" Inherits="mis_Finance_ItemListOfficeWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <script type="text/javascript">
        function validatename(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122) && charCode != 32) {
                return false;
            }
            return true;
        }
    </script>
    <link href="css/finanacecustom.css" rel="stylesheet" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-header">
                    <h3 class="box-title">Item Mapped In Offices </h3>
                </div>
                <div class="box-body">
                    <div class="row">

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Name</label><span style="color: red">*</span>
                                <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="btn_Click">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Button ID="btn" CssClass="btn btn-md btn-primary show_detail Aselect1" runat="server" Text="Show Items" OnClick="btn_Click" />
                            </div>
                        </div>
                    </div>

                </div>
                <div class="box-body">
                    <div class="col-md-12">
                        <p class="mainheading">
                            Items Mapped In
                            <asp:Label ID="lblOfficeName" runat="server" Text=""></asp:Label>
                        </p>
                    </div>
                    <div class="col-md-12">
                        <asp:GridView ID="GVItemDetail" ShowHeaderWhenEmpty="true" EmptyDataText="Record Not Found!" EmptyDataRowStyle-ForeColor="Red" CssClass="datatable table table-hover table-bordered" AutoGenerateColumns="False" runat="server">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="10" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                <asp:BoundField DataField="ItemTypeName" HeaderText="Item Group" />
                                <asp:BoundField DataField="UnitName" HeaderText="Item Unit" />
                                <asp:BoundField DataField="ItemAliasCode" HeaderText="Item Code(Alias)" />
                                <asp:BoundField DataField="HSNCode" HeaderText="HSN Code" />
                                <asp:BoundField DataField="Sale" HeaderText="Sales Ledger" />
                                <asp:BoundField DataField="Pur" HeaderText="Purchase Ledger" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
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
            paging: false


            ,
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
                    title: $('.mainheading').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('.mainheading').text(),
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

