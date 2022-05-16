<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HRPostMaster.aspx.cs" Inherits="mis_HR_HRPostMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <style>
        .lblheadingFirst {
            padding: 7px 0px;
            margin: 13px 0px;
            display: block;
            font-size: 15px;
        }

        .lblSanctionPost {
            font-size: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Corporation Setup</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Name</label>
                                <asp:DropDownList ID="ddlOfficeName" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Class (श्रेणी)<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlClass" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>&nbsp;</label>
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success btn-block" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateformSearch();"></asp:Button>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblheadingFirst" CssClass="lblheadingFirst" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="col-md-12">
                            <asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-bordered table-striped pagination-ys" AutoGenerateColumns="False" DataKeyNames="Designation_ID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ClientIDMode="Static">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%#Eval("Designation_ID")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Designation_Name" HeaderText="Designation Name" />
                                    <asp:TemplateField HeaderText="Sanction Post" ItemStyle-CssClass="RemovePadding" ItemStyle-Width="150">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" CssClass="form-control" placeholder="0" MaxLength="8" onblur="return CalculatePost(this);" onkeypress="return validateNum(event)" Value='<%#Eval("SanctionPost")%>'></asp:TextBox>
                                            <asp:Label ID="lblSanctionPost" CssClass="lblSanctionPost" runat="server" Text='<%#Eval("SanctionPost")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  --%>
                                    <asp:TemplateField HeaderText="Filled General" ItemStyle-CssClass="RemovePadding" ItemStyle-Width="150">
                                        <ItemTemplate>
                                            <%--<asp:TextBox runat="server" CssClass="form-control" placeholder="0" MaxLength="8" Value='<%#Eval("FilledPostGEN")%>' ReadOnly="true"></asp:TextBox>--%>
                                            <asp:Label ID="lblFilledPostGEN" runat="server" Text='<%#Eval("FilledPostGEN")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Filled OBC" ItemStyle-CssClass="RemovePadding" ItemStyle-Width="150">
                                        <ItemTemplate>
                                            <%--  <asp:TextBox runat="server" CssClass="form-control" placeholder="0" MaxLength="8" Value='<%#Eval("FilledPostOBC")%>' ReadOnly="true"></asp:TextBox>--%>
                                            <asp:Label ID="lblFilledPostOBC" runat="server" Text='<%#Eval("FilledPostOBC")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Filled SC" ItemStyle-CssClass="RemovePadding" ItemStyle-Width="150">
                                        <ItemTemplate>
                                            <%--<asp:TextBox runat="server" CssClass="form-control" placeholder="0" MaxLength="8" Value='<%#Eval("FilledPostSC")%>' ReadOnly="true"></asp:TextBox>--%>
                                            <asp:Label ID="lblFilledPostSC" runat="server" Text='<%#Eval("FilledPostSC")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Filled ST" ItemStyle-CssClass="RemovePadding" ItemStyle-Width="150">
                                        <ItemTemplate>
                                            <%-- <asp:TextBox runat="server" CssClass="form-control" placeholder="0" MaxLength="8" Value='<%#Eval("FilledPostST")%>' ReadOnly="true"></asp:TextBox>--%>
                                            <asp:Label ID="lblFilledPostST" runat="server" Text='<%#Eval("FilledPostST")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  --%>
                                    <asp:TemplateField HeaderText="Total Filled Post" ItemStyle-CssClass="RemovePadding" ItemStyle-Width="150">
                                        <ItemTemplate>
                                            <%--<asp:TextBox runat="server" CssClass="form-control" placeholder="0" MaxLength="8" Value='<%#Eval("FilledPost")%>' ReadOnly="true"></asp:TextBox>--%>
                                            <asp:Label ID="lblFilledPost" runat="server" Text='<%#Eval("FilledPost")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vacant Post" ItemStyle-CssClass="RemovePadding" ItemStyle-Width="150">
                                        <ItemTemplate>
                                            <%-- <asp:TextBox runat="server" CssClass="form-control" placeholder="0" MaxLength="8" Value='<%#Eval("VaccantPost")%>' ReadOnly="true"></asp:TextBox>--%>
                                            <asp:Label ID="lblVaccantPost" runat="server" Text='<%#Eval("VaccantPost")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Select" runat="server" CssClass="label label-default" CausesValidation="False" CommandName="Select" Text="View Detail"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-8"></div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" CssClass="btn btn-block btn-success" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <div class="modal fade" id="ShowDetailModal" role="dialog">
            <div class="modal-dialog modal-lg">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Filled Post Detail</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="GridView2" runat="server" class="datatable2 table table-hover table-bordered table-striped pagination-ys" AutoGenerateColumns="False" DataKeyNames="Emp_ID" EmptyDataText="No Record Found..!">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="Emp_FatherHusbandName" HeaderText="Father/Husband Name" />
                                        <asp:BoundField DataField="Emp_MobileNo" HeaderText="Mobile No." />
                                        <asp:BoundField DataField="Department_Name" HeaderText="Department Name" />
                                        <asp:BoundField DataField="Emp_Category" HeaderText="Category" />
                                        <asp:BoundField DataField="Office_Name" HeaderText="Office Name" />

                                    </Columns>
                                </asp:GridView>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        function ShowModal() {
            $('#ShowDetailModal').modal('show');
        };
        function CalculatePost() {
            return true;
        }
        //function CalculatePost() {
        //    debugger;
        //    var i = 0;
        //    $('#GridView1 tr').each(function (index) {
        //        if (i > 0) {
        //            var SanctionPost = $(this).children("td").eq(2).find('input[type="text"]').val();
        //            var FilledPost = $(this).children("td").eq(3).find('input[type="text"]').val();

        //            if (isNaN(SanctionPost) || SanctionPost == "") {
        //                SanctionPost = '0';
        //                $(this).children("td").eq(2).find('input[type="text"]').val(0)
        //            }
        //            if (isNaN(FilledPost) || FilledPost == "") {
        //                FilledPost = '0';
        //                $(this).children("td").eq(3).find('input[type="text"]').val(0);
        //            }
        //            var VaccantPost = parseInt(SanctionPost) - parseInt(FilledPost);

        //            $(this).children("td").eq(4).find('input[type="text"]').val(VaccantPost);

        //        }
        //        i++;
        //    });

        //    return true;
        //}
        function validateformSearch() {
            var msg = "";
            if (document.getElementById('<%=ddlClass.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Class. \n";
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;

            }
        }
        function validateform() {
            var msg = "";
            if (document.getElementById('<%=ddlClass.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Class. \n";
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (confirm("Do you really want to Save Details ?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
    </script>

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
                        columns: [0, 1, 2, 3, 4, 5, 6,7,8]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('.lblheadingFirst').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6,7,8]
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


        $('.datatable2').DataTable({
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
                        columns: [0, 1, 2, 3, 4, 5, 6]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('.lblheadingFirst').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6]
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



