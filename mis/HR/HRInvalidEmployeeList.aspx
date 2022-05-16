<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HRInvalidEmployeeList.aspx.cs" Inherits="mis_HR_HRInvalidEmployeeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="css/dataTables.bootstrap.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Incomplete Employee Registration List</h3>
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                   
                                        <asp:GridView ID="GridView1" runat="server" CssClass="datatable table table-bordered table-striped" DataKeyNames="Emp_ID" AutoGenerateColumns="false" OnRowDeleting="GridView1_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.NO">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                                <asp:BoundField DataField="Emp_Gender" HeaderText="Gender" />
                                                <asp:BoundField DataField="Emp_Dob" HeaderText="Date Of Birth" />
                                                <asp:BoundField DataField="Emp_MaritalStatus" HeaderText="Marital Status" />
                                                <asp:BoundField DataField="Emp_BloodGroup" HeaderText="Blood Group" />
                                                <asp:BoundField DataField="Emp_MobileNo" HeaderText="Mobile No" />
                                                <asp:BoundField DataField="Emp_AadharNo" HeaderText="Aadhar No" />
                                                <asp:BoundField DataField="Emp_PanCardNo" HeaderText="PanCard No" />
                                                <asp:BoundField DataField="Emp_Email" HeaderText="Email" />
                                                
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" CssClass="label label-danger" CommandName="Delete" CausesValidation="false" OnClientClick="return confirm('The Record will be deleted. Are you sure want to continue?');">Delete</asp:LinkButton>
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
            paging: false,
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

                        // javascript: print(),

                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
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


