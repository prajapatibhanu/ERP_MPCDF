<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HREmpTransferAllHistory.aspx.cs" Inherits="mis_HR_HREmpTransferAllHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">All Employee Transfer History</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>कार्यालय / Office<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlOldOffice" runat="server" class="form-control select2" Enabled="false">
                                </asp:DropDownList>
                            </div>
                        </div>

                       <%-- <div class="col-md-3">
                            <div class="form-group">
                                <label>From Date (आदेश तारीख / Order Date)<span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtFromDate" runat="server" placeholder="Select From Date" class="form-control DateAdd" autocomplete="off" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>To Date (आदेश तारीख / Order Date)<span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtToDate" runat="server" placeholder="Select To Date" class="form-control DateAdd" autocomplete="off" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>--%>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Search" OnClientClick="return validateform();" OnClick="btnSave_Click" />
                            </div>
                        </div>


                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table table-responsive">
                                <asp:Label ID="lblMsg2" runat="server" Text="" Style="color: red; font-size: 15px;"></asp:Label>
                                <asp:GridView ID="GridView1" class="datatable  table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" EmptyDataText="No Record Found" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="OrderNo" HeaderText="Order No." />
                                        <asp:BoundField DataField="OrderDate" HeaderText="Order Date" />
                                        <asp:BoundField DataField="OldOffice_Name" HeaderText="Existing Office Name" />
                                        <asp:BoundField DataField="OldDesignation_Name" HeaderText="Existing Designation Name" />
                                        <asp:BoundField DataField="OldPostingDate" HeaderText="Existing Posting Date" />
                                        <asp:BoundField DataField="NewOffice_Name" HeaderText="New Office Name" />
                                        <asp:BoundField DataField="NewDesignation_Name" HeaderText="New Designation Name" />
                                        <asp:BoundField DataField="NewEffectiveDate" HeaderText="New Effective Date" />
                                    </Columns>
                                </asp:GridView>
                            </div>
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
    <style>
        .dt-buttons {
            margin: 0px 0px 10px 0px;
        }
    </style>
    <script type="text/javascript">

        function validateform() {
            var msg = "";
            if (document.getElementById('<%=ddlOldOffice.ClientID%>').selectedIndex == 0) {
                msg += "Select Office Name. \n";
            }

           <%-- if (document.getElementById('<%=txtFromDate.ClientID%>').value.trim() == "") {
                msg += "Select From Date. \n";
            }
            if (document.getElementById('<%=txtToDate.ClientID%>').value.trim() == "") {
                msg += "Select To Date. \n";
            }--%>

            if (msg != "") {
                alert(msg);
                return false;
            } else {
                return true;
            }
        }

        //$('#txtFromDate').change(function () {
        //    var start = $('#txtFromDate').datepicker('getDate');
        //    var end = $('#txtToDate').datepicker('getDate');

        //    if ($('#txtToDate').val() != "") {
        //        if (start > end) {

        //            if ($('#txtFromDate').val() != "") {
        //                alert("From date should not be greater than To Date.");
        //                $('#txtFromDate').val("");
        //            }
        //        }
        //    }
        //});
        //$('#txtToDate').change(function () {
        //    var start = $('#txtFromDate').datepicker('getDate');
        //    var end = $('#txtToDate').datepicker('getDate');

        //    if (start > end) {

        //        if ($('#txtToDate').val() != "") {
        //            alert("To Date can not be less than From Date.");
        //            $('#txtToDate').val("");
        //        }
        //    }
        //});




        $('.datatable').DataTable({
            paging: true,
            pageLength: 100,
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
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('.lblheadingFirst').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
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

