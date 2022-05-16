<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CarriageInwardEntryAtSupply.aspx.cs" Inherits="mis_DemandSupply_CarriageInwardEntryAtSupply" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">    
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
            <div class="row">


                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Crate Stock Entry At Supply </h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <fieldset>
                                <legend>Carriage Mode,Crate Color,Date ,Qty etc.
                                </legend>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">                                  
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Carriage Mode </label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Carriage Mode" Text="<i class='fa fa-exclamation-circle' title='Select Carriage Mode !'></i>"
                                                    ControlToValidate="ddlCarriageMode" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlCarriageMode" AutoPostBack="true" OnInit="ddlCarriageMode_Init" CssClass="form-control select2" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Crate Colour </label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvcratecolour" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Crate Color" Text="<i class='fa fa-exclamation-circle' title='Select Crate Color !'></i>"
                                                    ControlToValidate="ddlCrateColor" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlCrateColor" OnInit="ddlCrateColor_Init" CssClass="form-control select2" runat="server">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class="col-md-2">
                                     <div class="form-group">
                                        <label> Date <span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                ControlToValidate="txtDate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtDate"
                                                ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDate" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-end-date="1d" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                     </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Quantity <span style="color: red;"> *</span></label>
                                             <span class="pull-right">
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a"
                                                    ErrorMessage="Enter Quantity" Text="<i class='fa fa-exclamation-circle' title='Enter Quantity !'></i>"
                                                    ControlToValidate="txtQtyPerCarriageType" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                 <asp:RegularExpressionValidator ID="rev1" runat="server" Display="Dynamic" ValidationGroup="a"
                                                        ErrorMessage="Enter Valid Number In Quantity Field & First digit can't be 0(Zero):!" ForeColor="Red" 
                                                     Text="<i class='fa fa-exclamation-circle' title='Enter Valid Number In Quantity Field & First digit can't be 0(Zero)'></i>" ControlToValidate="txtQtyPerCarriageType"
                                                         ValidationExpression="^[1-9][0-9]*$">
                                                  </asp:RegularExpressionValidator>
                                             </span>
                                            <asp:TextBox ID="txtQtyPerCarriageType" autocomplete="off" onpaste="return false;" onkeypress="return validateNum(event);" CssClass="form-control" MaxLength="6" placeorder="ex:20" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Specification <span style="color: red;"> *</span></label>
                                             <span class="pull-right">
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                    ErrorMessage="Enter Specification" Text="<i class='fa fa-exclamation-circle' title='Enter Specification !'></i>"
                                                    ControlToValidate="txtSpecification" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtSpecification"
                                                        ErrorMessage="Only Alphanumeric & some special symbol ',()-_.' allow " Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric & some special symbol ',()-_.' !'></i>"
                                                        SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z\s,-_.)(]+$">
                                                    </asp:RegularExpressionValidator>
                                             </span>
                                              <asp:TextBox runat="server" autocomplete="off" CausesValidation="true"  CssClass="form-control" ID="txtSpecification" MaxLength="100" placeholder="Enter specification" ClientIDMode="Static"></asp:TextBox>
                                        </div>

                                    </div>
                                     <div class="col-md-1" style="margin-top:20px;">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSubmit"
                                                Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                                        </div>
                                    </div>
                                     <div class="col-md-1" style="margin-top:20px;">
                                        <div class="form-group">
                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Reset" CssClass="btn btn-block btn-secondary" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                            <fieldset>
                                <legend>Crate Stock Entry Details at Supply</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" OnRowCommand="GridView1_RowCommand" runat="server" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                AutoGenerateColumns="False" EmptyDataText="No Record Found." EnableModelValidation="True" DataKeyNames="CarriageInwardId">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                            <asp:Label ID="lblCarriageModeID" Visible="false" Text='<%# Eval("CarriageModeID")%>' runat="server" />
                                                            <asp:Label ID="lblCrateColorID" Visible="false" Text='<%# Eval("CrateColorID")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                  <%--  <asp:TemplateField HeaderText="Dugdh Sang">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOffice_Name" Text='<%# Eval("CarriageModeName")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                         <asp:TemplateField HeaderText="Carriage Mode">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCarriageModeName" Text='<%# Eval("CarriageModeName")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOpeningDate" Text='<%# Eval("OpeningDate")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Carriage Specification (Crate Color) ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSpecification" Text='<%# Eval("CarriageModeID").ToString()=="1" ? Eval("Specification") + " (" + Eval("V_SealColor") + ")" : Eval("Specification")  %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemQtyByCarriageMode" Text='<%# Eval("NoOfCarriageQty")  %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Actions">
                                                        <ItemTemplate>
                                                           <asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("CarriageInwardId") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete this record?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>


                            </fieldset>
                        </div>

                    </div>
                </div>
            </div>
        </section>
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
                    title: ('Crate Stock Entry At Supply').bold().fontsize(5).toUpperCase(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4]
                    },
                    footer: false,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    filename: 'CarriageStockEntryAtSupply',
                    title: ('Crate Stock Entry At Supply').bold().fontsize(5).toUpperCase(),
                    text: '<i class="fa fa-file-excel-o"></i> Excel',

                    exportOptions: {
                        columns: [0, 1, 2, 3, 4]
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

        function myItemDetailsModal() {
            $("#ItemDetailsModal").modal('show');

        }
    </script>
</asp:Content>
