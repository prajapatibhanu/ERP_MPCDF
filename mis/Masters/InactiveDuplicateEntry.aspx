<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="InactiveDuplicateEntry.aspx.cs" Inherits="mis_Masters_InactiveDuplicateEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Inactive Office Registration Data</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1"  runat="server" class="datatable table table-hover table-bordered pagination-ys"
                            ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"  DataKeyNames="Office_ID" OnRowCommand="GridView1_RowCommand" >
                            <Columns>
                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Office_ID").ToString()%>' runat="server"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Milk Collection Type" DataField="OfficeTypeName" />
                                <asp:BoundField HeaderText="Name" DataField="Office_Name" />
                                <asp:BoundField HeaderText="Name In English" DataField="Office_Name_E" />
                                <asp:BoundField HeaderText="Office Code" DataField="Office_Code" />
								<asp:BoundField HeaderText="Society Code" DataField="SocietyCode" />
                                <%--<asp:BoundField HeaderText="User Name" DataField="UserName" />--%>
                                <asp:BoundField HeaderText="Milk Supply to" DataField="MilkSupplyto" />
                                <asp:BoundField HeaderText="Supply Unit" DataField="SupplyUnit" />
                                <%--<asp:BoundField HeaderText="Total Producer" DataField="P_count" />--%>

                                <asp:BoundField HeaderText="Bank Name" DataField="BankName" />
                                <asp:BoundField HeaderText="Bank AccountNo" DataField="BankAccountNo" />
                                <asp:BoundField HeaderText="IFSC" DataField="IFSC" />
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" CommandName="DeleteRecord" CommandArgument='<%#Eval("Office_ID") %>' Visible='<%#Eval("Status").ToString()==""?true:false %>' runat="server" OnClientClick="return confirm('Do you really want to delete record?');"><i class="fa fa-trash"></i></asp:LinkButton>

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
    <script src="../../js/jquery-1.10.2.js"></script>
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
            pageLength: 50,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],
            dom: '<"row"<"col-sm-6"B><"col-sm-6"f>>' +
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
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: 'Office Registration Report',
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11]
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

