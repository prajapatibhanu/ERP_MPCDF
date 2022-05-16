<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HREmpIncrement.aspx.cs" Inherits="mis_HR_HREmpIncrement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Add Increment</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>कार्यालय / Office<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlOldOffice" runat="server" class="form-control select2" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlOldOffice_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>आदेश नंबर / Order No.<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtOrderNo" runat="server" placeholder="Order No..." class="form-control" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>आदेश तारीख / Order Date<span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtOrderDate" runat="server" placeholder="Select Date..." class="form-control DateAdd" data-date-end-date="0d" autocomplete="off" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>प्रभावी तारीख <small>(Effective Date)</small><span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtEffectiveDate" runat="server" placeholder="Select Effective Date..." class="form-control DateAdd" autocomplete="off" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <br />
                    <br />

                    <div class="row">
                        <div class="col-md-7">
                            <fieldset>
                                <legend>Current PayScale</legend>
                                <div class="row">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label>कर्मचारी का नाम <small>(Employee Name)</small><span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlEmployee" runat="server" class="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>स्तर <small>(Level)</small><span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlOldLevel" runat="server" class="form-control" Enabled="false"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>मूल वेतन <small>(Basic Salary)</small><span style="color: red;">*</span></label>
                                            <asp:TextBox ID="txtOldBasicSalary" runat="server" placeholder="" class="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>


                        <div class="col-md-5">
                            <fieldset>
                                <legend>New Increment Detail</legend>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>स्तर <small>(Level)</small><span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlNewLevel" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlNewLevel_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>मूल वेतन <small>(Basic Salary)</small><span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlNewBasicSalary" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Add Employee" OnClientClick="return validateform();" OnClick="btnSave_Click" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a class="btn btn-block btn-default" style="margin-top: 23px;" href="HREmpIncrement.aspx">Clear</a>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <hr />
                            <div class="table table-responsive">
                                <asp:GridView ID="GridView1" DataKeyNames="Increament_ID" class="datatable table table-hover table-bordered" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDeleting="GridView1_RowDeleting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="Order_No" HeaderText="Order No" />
                                        <asp:BoundField DataField="Order_Date" HeaderText="Order Date" />
                                        <asp:BoundField DataField="OldLevel_Name" HeaderText="Existing Level" />
                                        <asp:BoundField DataField="Old_BasicSalary" HeaderText="Existing Basic Salary" />
                                        <asp:BoundField DataField="NewLevel_Name" HeaderText="New Level" />
                                        <asp:BoundField DataField="New_BasicSalary" HeaderText="New Basic Salary" />
                                        <asp:BoundField DataField="EffectiveDate" HeaderText="Effective Date" />
                                        <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Select" runat="server" CssClass="label label-default" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
                                                <asp:LinkButton ID="Delete" runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Increment Detail will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
    <script type="text/javascript">
        function validateform() {
            var msg = "";
            if (document.getElementById('<%=txtOrderNo.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Order No.\n";
            }
            if (document.getElementById('<%=txtOrderDate.ClientID%>').value.trim() == "") {
                msg += "Select Order Date. \n";
            }
            if (document.getElementById('<%=ddlEmployee.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Employee Name. \n";
            }
            if (document.getElementById('<%=ddlOldOffice.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Office. \n";
            }
            if (document.getElementById('<%=ddlOldLevel.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Current Level. \n";
            }
            if (document.getElementById('<%=txtOldBasicSalary.ClientID%>').value.trim() == "") {
                msg += "Enter Current Basic Salary. \n";
            }
            if (document.getElementById('<%=ddlNewLevel.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select New Level. \n";
            }
<%--            if (document.getElementById('<%=ddlNewBasicSalary.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select New Basic Salary. \n";
            }--%>
            if (document.getElementById('<%=txtEffectiveDate.ClientID%>').value.trim() == "") {
                msg += "select New Effective Date. \n";
            }
            if (msg != "") {
                alert(msg);
                return false
            }
            else {
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    if (confirm("Do you really want to Save Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Edit") {
                    if (confirm("Do you really want to Edit Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }

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
    <style>
        .dt-buttons {
            margin: 0px 0px 10px 0px;
        }
    </style>
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
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('.lblheadingFirst').text(),
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

