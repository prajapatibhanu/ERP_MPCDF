<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CrateReturnEntry_BDS.aspx.cs" Inherits="mis_Demand_CrateReturnEntry_BDS" %>

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
                            <h3 class="box-title">Crates Return Entry</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <fieldset>
                                <legend>Date,challan No,Qty ,Party Name
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
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtReturnDate" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Shift</label>
                                            <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                       <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Item Category</label>
                                        <asp:DropDownList ID="ddlItemCategory" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                                     <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Location<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Location" Text="<i class='fa fa-exclamation-circle' title='Select Location !'></i>"
                                                    ControlToValidate="ddlLocation" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Route<span style="color: red;"> *</span></label>
                                             <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                    ErrorMessage="Select Route" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Route !'></i>"
                                                    ControlToValidate="ddlRoute" Initialvalue="0" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                 </span>
                                            <asp:DropDownList ID="ddlRoute" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
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
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Challan No.</label>
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
                            <h3 class="box-title">Crates Return Details</h3>

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
                                        <label>Item Category</label>
                                        <asp:DropDownList ID="ddlItemCategorySearch" ClientIDMode="Static" OnSelectedIndexChanged="ddlItemCategorySearch_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                                        <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Location</label>
                                                    <asp:DropDownList ID="ddlLocationSearch" runat="server" OnSelectedIndexChanged="ddlItemCategorySearch_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control select2">
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Route</label>
                                                    <asp:DropDownList ID="ddlRouteSearch" runat="server" CssClass="form-control select2">
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
                                            <asp:GridView ID="GridView1" Visible="false" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" runat="server" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                    AutoGenerateColumns="false" ShowFooter="true" EmptyDataText="No Record Found." DataKeyNames="MilkCrateMgmtId" EnableModelValidation="True">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                <asp:Label ID="lblCatId" Visible="false" Text='<%#Eval("ItemCat_id") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIRDate" Text='<%#Eval("IRDate") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Shift" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblShiftName" Text='<%#Eval("ShiftName") %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblShiftId" Visible="false" Text='<%#Eval("ShiftId") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Location" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAreaName" Text='<%#Eval("AreaName") %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblAreaID" Visible="false" Text='<%#Eval("AreaID") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Route" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRName" Text='<%#Eval("RName") %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblRouteId" Visible="false" Text='<%#Eval("RouteId") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        <FooterTemplate>
                                                            <b>Total</b>
                                                        </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Return Crate" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblReturnCrate" Text='<%#Eval("ReturnCrate") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                            <asp:Label ID="lblFTotalReturnCrate"  runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Challan No." HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblChallanNo" Text='<%#Eval("ChallanNo") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Remark" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCrateRemark" Text='<%#Eval("CrateRemark") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" CommandName="RecordUpdate" CommandArgument='<%#Eval("MilkCrateMgmtId") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            <asp:GridView ID="GridView2" Visible="false" ShowFooter="true" OnRowDataBound="GridView2_RowDataBound" OnRowCommand="GridView2_RowCommand" runat="server" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                    AutoGenerateColumns="false" EmptyDataText="No Record Found." DataKeyNames="ProductCrateMgmtId" EnableModelValidation="True">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIRDate" Text='<%#Eval("IRDate") %>' runat="server"></asp:Label>
                                                                 <asp:Label ID="lblCatId" Visible="false" Text='<%#Eval("ItemCat_id") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Shift" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblShiftName" Text='<%#Eval("ShiftName") %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblShiftId" Visible="false" Text='<%#Eval("ShiftId") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Location" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAreaName" Text='<%#Eval("AreaName") %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblAreaID" Visible="false" Text='<%#Eval("AreaID") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Route" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRName" Text='<%#Eval("RName") %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblRouteId" Visible="false" Text='<%#Eval("RouteId") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                               <FooterTemplate>
                                                            <b>Total</b>
                                                        </FooterTemplate> 
                                                            
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Return Crate" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblReturnCrate" Text='<%#Eval("ReturnCrate") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                             <FooterTemplate>
                                                            <asp:Label ID="lblFTotalReturnCrate"  runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Challan No." HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblChallanNo" Text='<%#Eval("ChallanNo") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Remark" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCrateRemark" Text='<%#Eval("CrateRemark") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("ProductCrateMgmtId") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
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
            ordering: false,
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

