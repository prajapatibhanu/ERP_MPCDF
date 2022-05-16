<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CrateReceiving.aspx.cs" Inherits="mis_DemandSupply_CrateReceiving" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <%--Confirmation Modal Start --%>
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
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Crate Receiving</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <fieldset>
                                <legend>Crate Receiving 
                                </legend>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </div>
                                </div>


                                <div class="row">

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Date<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    ControlToValidate="txtDate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtDate"
                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDate" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Shift<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Shift" Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"
                                                    ControlToValidate="ddlShift" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlShift" runat="server" OnInit="ddlShift_Init" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Item Category<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvic" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Item Category" Text="<i class='fa fa-exclamation-circle' title='Select Item Category !'></i>"
                                                    ControlToValidate="ddlItemCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlItemCategory" OnInit="ddlItemCategory_Init" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Route No <span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a" InitialValue="0"
                                                    ErrorMessage="Select Route" Text="<i class='fa fa-exclamation-circle' title='Select Route !'></i>"
                                                    ControlToValidate="ddlRoute" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlRoute" AutoPostBack="true" OnSelectedIndexChanged="ddlRoute_SelectedIndexChanged" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class="col-md-2">
                                        <div class="form-group">
                                            <label>For Institution <span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a" InitialValue="0"
                                                    ErrorMessage="Select anyone from Type" Text="<i class='fa fa-exclamation-circle' title='Select anyone from Type !'></i>"
                                                    ControlToValidate="ddlType" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlType" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" CssClass="form-control select2" runat="server">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2" id="pnlinst" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>Institution <span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a" InitialValue="0"
                                                    ErrorMessage="Select Institution" Text="<i class='fa fa-exclamation-circle' title='Select Institution !'></i>"
                                                    ControlToValidate="ddlInstitution" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlInstitution" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>No. of Crate Received <span style="color: red;"> *</span></label>

                                            <span class="pull-right">
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a" s
                                                    ErrorMessage="Enter No. of Crate Received" Text="<i class='fa fa-exclamation-circle' title='Enter No. of Crate Received !'></i>"
                                                    ControlToValidate="txttotalcratereceived" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revcrInst" Enabled="false" runat="server" Display="Dynamic" ValidationGroup="c"
                                                    ErrorMessage="Invalid No. of Crate Received, Accept Number only. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Invalid No. of Crate Received, Accept Number only. !'></i>" ControlToValidate="txttotalcratereceived"
                                                    ValidationExpression="^[0-9]*$">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txttotalcratereceived" MaxLength="5" onkeypress="return validateNum(event);" placeholder="Enter No. of Crate Received." ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>No. of Crate Broken <span style="color: red;"> *</span></label>

                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a" s
                                                    ErrorMessage="Enter No. of Crate Broken" Text="<i class='fa fa-exclamation-circle' title='Enter No. of Crate Broken !'></i>"
                                                    ControlToValidate="txttotalcratebroken" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revcbInst" Enabled="false" runat="server" Display="Dynamic" ValidationGroup="c"
                                                    ErrorMessage="Invalid No. of Crate Broken, Accept Number only. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Invalid No. of Crate Broken, Accept Number only. !'></i>" ControlToValidate="txttotalcratebroken"
                                                    ValidationExpression="^[0-9]*$">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txttotalcratebroken" MaxLength="10" onkeypress="return validateNum(event);" placeholder="Enter No. of Crate Broken." ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>No. of Crate Missing <span style="color: red;"> *</span></label>

                                            <span class="pull-right">
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="a" s
                                                    ErrorMessage="Enter No. of Crate Missing" Text="<i class='fa fa-exclamation-circle' title='Enter No. of Crate Missing !'></i>"
                                                    ControlToValidate="txttotalcratemissing" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revcmInst" Enabled="false" runat="server" Display="Dynamic" ValidationGroup="c"
                                                    ErrorMessage="Invalid No. of Crate Missing, Accept Number only. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Invalid No. of Crate Missing, Accept Number only. !'></i>" ControlToValidate="txttotalcratemissing"
                                                    ValidationExpression="^[0-9]*$">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txttotalcratemissing" MaxLength="10" onkeypress="return validateNum(event);" placeholder="Enter No. of Crate Missing." ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <%-- <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Not Received Crate <span style="color: red;"> *</span></label>

                                            <span class="pull-right">
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a" s
                                                    ErrorMessage="Enter Not Received Crate" Text="<i class='fa fa-exclamation-circle' title='Enter No. of Crate Missing !'></i>"
                                                    ControlToValidate="txtNotReceived" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Enabled="false" runat="server" Display="Dynamic" ValidationGroup="c"
                                                    ErrorMessage="Invalid No. of Crate Missing, Accept Number only. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Invalid Not Received Crate, Accept Number only. !'></i>" ControlToValidate="txtNotReceived"
                                                    ValidationExpression="^[0-9]*$">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtNotReceived" MaxLength="10" onkeypress="return validateNum(event);" placeholder="Enter Not Received Crate" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>--%>
                                     <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Remark</label>

                                            <span class="pull-right">
                                               
                                               <asp:RegularExpressionValidator ID="revcremarkInst" ValidationGroup="c" Display="Dynamic" runat="server" ControlToValidate="txtRemark"
                                                                    ErrorMessage="In Remark Only Alphanumeric & some special symbol ',()-_.' allow " Text="<i class='fa fa-exclamation-circle' title='In Remark Only Alphanumeric & some special symbol ',()-_.' !'></i>"
                                                                    SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z\s,-_.)(]+$">
                                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtRemark" MaxLength="100"  placeholder="Enter Remark" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" OnClientClick="return ValidatePage();" ValidationGroup="a" ID="btnSubmit" Text="Save" />

                                        </div>
                                    </div>
                                    <div class="col-md-1" style="margin-top: 20px;">
                                        <div class="form-group">

                                            <asp:Button ID="btnClear" OnClick="btnClear_Click" runat="server" Text="Clear" CssClass="btn btn-default" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                        </div>

                    </div>
                </div>
                <div class="col-md-12" id="pnlData">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Crate Receiving Details</h3>

                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblReportMsg" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend>Crate Receiving Details</legend>

                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                AutoGenerateColumns="False" EmptyDataText="No Record Found." EnableModelValidation="True" DataKeyNames="ItmStock_id">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                            <asp:Label ID="lblShiftid" Visible="false" Text='<%# Eval("Shift_id")%>' runat="server" />
                                                            <asp:Label ID="lblRouteID" Visible="false" Text='<%# Eval("TransactionID")%>' runat="server" />
                                                            <asp:Label ID="lblmpitemcatid" Visible="false" Text='<%# Eval("mpitemcat_id")%>' runat="server" />
                                                            <asp:Label ID="lblOrganizationId" Visible="false" Text='<%# Eval("OrganizationId")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTranDt" Text='<%# Eval("TranDt")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shift">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblShiftName" Text='<%# Eval("ShiftName")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Category ">
                                                        <ItemTemplate>
                                                           <asp:Label ID="lblItemCatName" Text='<%# Eval("ItemCatName")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Route">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRName" Text='<%# Eval("RName")  %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Institution">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblInstName" Text='<%# Eval("InstName")  %>' runat="server" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No. of Received Crate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReceivedCrate" Text='<%# Eval("Cr")  %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="No. of Broken Crate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblbrokencrate" Text='<%# Eval("broken_crate")  %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="No. of Missing Crate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmissingcrate" Text='<%# Eval("missing_crate")  %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <%--  <asp:TemplateField HeaderText="No. of Not Recevied Crate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnoteceivedcrate" Text='<%# Eval("not_received_crate")  %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                       <asp:TemplateField HeaderText="Remark">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemark" Text='<%# Eval("Remark")  %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Actions">
                                                        <ItemTemplate>
                                                           <asp:LinkButton ID="lnkEdit" CommandArgument='<%#Eval("ItmStock_id") %>' CommandName="RecordUpdate" runat="server" ToolTip="Edit" Style="color: red;" OnClientClick="return confirm('Are you sure to Edit this record?')"><i class="fa fa-pencil"></i></asp:LinkButton>
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
            </div>
        </section>
        <!-- /.content -->

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">  
     <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script> 
    <script type="text/javascript">
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
                    title: ('Crate Receiving').bold().fontsize(5).toUpperCase(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
                    },
                    footer: false,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    filename: 'CrateReceiving',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: ('Crate Receiving').bold().fontsize(5).toUpperCase(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
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


                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }

                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                
            }
        }

      
    </script>
</asp:Content>

