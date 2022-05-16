<%@ Page Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CrateReturnEntry_BDS_Urben.aspx.cs" Inherits="mis_Demand_CrateReturnEntry_BDS_DS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../Finance/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <style>
        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('../image/loader/ProgressImage.gif') 50% 50% no-repeat rgba(0, 0, 0, 0.3);
        }
    </style>
    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }

        function chkselect(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < 1; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        // inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }
         </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="loader"></div>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #d9d9d9;">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                        </button>
                        <h4 class="modal-title" id="myModalLabel">Confirmation</h4>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <p>
                            <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp;
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSubmit_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Crate Return Entry</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <fieldset>
                                <legend>Date,Slip No,Qty ,Party Name
                                </legend>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </div>
                                </div>


                                <div class="row">

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Date <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    ControlToValidate="txtReturnDate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtReturnDate"
                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtReturnDate" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static" OnTextChanged="txtReturnDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Party Name<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                    ErrorMessage="Select Party Name" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Party Name !'></i>"
                                                    ControlToValidate="ddlPartyName" InitialValue="0" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlPartyName" runat="server" CssClass="form-control select2" OnTextChanged="ddlPartyName_TextChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Shift <%--/ शिफ्ट--%></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Shift" Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"
                                                    ControlToValidate="ddlShift" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2" OnTextChanged="ddlShift_TextChanged" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Issue Crate<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                    ErrorMessage="Enter Return Crate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Return Crate !'></i>"
                                                    ControlToValidate="txtReturnCrate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="Dynamic" ValidationGroup="a"
                                                    ErrorMessage="Enter Valid number, Return Crate. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Valid number, Return Crate. !'></i>" ControlToValidate="txtReturnCrate"
                                                    ValidationExpression="^[-]?\d+$">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:label runat="server" autocomplete="off" CssClass="form-control" ID="lblissuecaret" ></asp:label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Return Crate<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                    ErrorMessage="Enter Return Crate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Return Crate !'></i>"
                                                    ControlToValidate="txtReturnCrate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ValidationGroup="a"
                                                    ErrorMessage="Enter Valid number, Return Crate. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Valid number, Return Crate. !'></i>" ControlToValidate="txtReturnCrate"
                                                    ValidationExpression="^[-]?\d+$">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtReturnCrate" MaxLength="5"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Slip No.</label>
                                            <span class="pull-right">
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ValidationGroup="a"
                                                    ErrorMessage="Enter Valid Challan No. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Challan No. !'></i>" ControlToValidate="txtChallano"
                                                    ValidationExpression="^[a-zA-Z0-9\s,-/]+$">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtChallano" MaxLength="50"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Remark</label>
                                            <span class="pull-right">
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" Display="Dynamic" ValidationGroup="a"
                                                    ErrorMessage="Enter Valid Remark !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Remark !'></i>" ControlToValidate="txtCrateRemark"
                                                    ValidationExpression="^[a-zA-Z0-9\s./-]+$">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtCrateRemark" MaxLength="200"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:Button runat="server" CssClass="btn btn-primary" OnClientClick="return ValidatePage();" ValidationGroup="a" ID="btnSubmit" Text="Save" />
                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-default" />
                                        </div>
                                    </div>

                                </div>



                            </fieldset>

                        </div>

                    </div>
                </div>

                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Crate Return Details</h3>

                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblReportMsg" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="row">
                                <fieldset>
                                    <legend>Date ,Party Name</legend>
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Date <%--/ दिनांक--%><span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="b"
                                                        ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                        ControlToValidate="txtSerchDate" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>

                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="b" runat="server" Display="Dynamic" ControlToValidate="txtSerchDate"
                                                        ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                        ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtSerchDate" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Party Name</label>

                                                <asp:DropDownList ID="ddlPartyNameSearch" runat="server" CssClass="form-control select2">
                                                </asp:DropDownList>
                                            </div>

                                        </div>

                                        <div class="col-md-1">
                                            <div class="form-group" style="margin-top: 20px;">
                                                <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="b" OnClick="btnSearch_Click" ID="btnSearch" Text="Search" />

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" runat="server"  ShowFooter="true" DataKeyNames="MilkandProductCrateMgmtId" class="datatable table table-striped table-bordered"
                                                ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" AutoGenerateColumns="false" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            <asp:Label ID="lblSDRAId" Visible="false" Text='<%#Eval("SDRAId") %>' runat="server"></asp:Label>
                                                            <asp:Label ID="lblMilkandProductCrateMgmtId" Visible="false" Text='<%#Eval("MilkandProductCrateMgmtId") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Party Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl" runat="server" Text='<%# Eval("DName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Shift">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblShiftName" runat="server" Text='<%# Eval("ShiftName") %>'></asp:Label>
                                                            <asp:Label ID="lblShiftId" runat="server" Visible="false" Text='<%# Eval("ShiftId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Return Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReturnDate" runat="server" Text='<%# Eval("ReturnDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <b>Total</b>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Return Crate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReturnCrate" runat="server" Text='<%# Eval("ReturnCrate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFAmount" Text="0"  runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Slip No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSlipNo" runat="server" Text='<%# Eval("SlipNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remark">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCrateRemark" runat="server" Text='<%# Eval("CrateRemark") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="EditRecord" CommandArgument='<%# Eval("MilkandProductCrateMgmtId") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Select">
                                                         <HeaderTemplate>
                                                              <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);"/>
                                                         </HeaderTemplate>
                                                        <ItemTemplate>
                                                           <asp:CheckBox ID="chkselect" runat="server"  onclick="chkselect(this);"/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                        <div class="col-md-1">
                                            <div class="form-group" style="margin-top: 20px;">
                                                <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="b" OnClick="Button1_Click" ID="Button1" Text="Delete" />

                                            </div>
                                        </div>
                                    </div>
                                </fieldset>

                            </div>


                        </div>

                    </div>
                </div>
            </div>
        </section>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <link href="../Finance/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="../Finance/js/jquery.dataTables.min.js"></script>
    <script src="../Finance/js/dataTables.bootstrap.min.js"></script>
    <script src="../Finance/js/dataTables.buttons.min.js"></script>
    <script src="../Finance/js/buttons.flash.min.js"></script>
    <script src="../Finance/js/jszip.min.js"></script>
    <script src="../Finance/js/pdfmake.min.js"></script>
    <script src="../Finance/js/vfs_fonts.js"></script>
    <script src="../Finance/js/buttons.html5.min.js"></script>
    <script src="../Finance/js/buttons.print.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.loader').fadeOut();

            function ValidatePage() {
                if (Page_IsValid) {
                    $('.loader').fadeOut();
                    return false;
                }

            }
            // }

        });
        $('.datatable').DataTable({
            paging: true,
            iDisplayLength: 100,
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
                    title: ('Crate Return Entry Details').bold().fontsize(3).toUpperCase(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
                    },
                    footer: false,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    title: ('Crate Return Entry Details').bold().fontsize(3).toUpperCase(),
                    filename: 'CrateReturnEntry ',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',

                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
                    },
                    footer: false
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
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
    </script>

</asp:Content>