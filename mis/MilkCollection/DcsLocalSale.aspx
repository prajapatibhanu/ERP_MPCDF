<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DcsLocalSale.aspx.cs" Inherits="mis_MilkCollection_DcsLocalSale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnYes_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="vs1" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="vs2" runat="server" ValidationGroup="c" ShowMessageBox="true" ShowSummary="false" />

    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Local Sale - Milk / Item </h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">

                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Office Detail</legend>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office Name</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtsocietyName" Text="<i class='fa fa-exclamation-circle' title='Enter society Name!'></i>" ErrorMessage="Enter society Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtsocietyName" Enabled="false" autocomplete="off" placeholder="Enter society Name" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Block<span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtBlock" CssClass="form-control" MaxLength="20" placeholder="Block" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <%--<div class="col-md-3">
                                    <div class="form-group">
                                        <label>Date</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtDate" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>--%>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Date (दिनांक)</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtDate" AutoPostBack="true" OnTextChanged="txtDate_TextChanged" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>

                                    </div>
                                </div>

                            </fieldset>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-6">
                            <fieldset>
                                <legend>Producer Type</legend>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                        <asp:RadioButtonList ID="rblpt" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblpt_SelectedIndexChanged" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Selected="True" style="padding-left: 5px; padding-right: 5px;">Registered</asp:ListItem>
                                            <asp:ListItem Value="2" style="padding-left: 5px; padding-right: 5px;">Open Sale</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>

                            </fieldset>
                        </div>

                        <div class="col-md-6">
                            <fieldset>
                                <legend>Detail</legend>

                                <div class="col-md-4" runat="server" visible="false" id="DivProducerID">
                                    <label>Producer<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvProducer" runat="server" Display="Dynamic" ControlToValidate="ddlFarmer" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Producer'></i>" ErrorMessage="Select Producer" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlFarmer" AutoPostBack="true" CssClass="form-control select2" runat="server" OnSelectedIndexChanged="ddlFarmer_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-4" runat="server" visible="false" id="DivProducerCode">
                                    <div class="form-group">
                                        <label>
                                            Code</label>
                                        <asp:TextBox ID="txtProducerCode" Enabled="false" placeholder="Enter Producer Code" MaxLength="3" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-4" runat="server" visible="false" id="DivPname">
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvPName" runat="server" Display="Dynamic" ControlToValidate="txtProducerName" Text="<i class='fa fa-exclamation-circle' title='Enter Name'></i>" ErrorMessage="Enter Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <label>
                                            Name</label>
                                        <asp:TextBox ID="txtProducerName" MaxLength="50" placeholder="Enter  Name" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>



                            </fieldset>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Milk / Items Detail</legend>

                                <div class="col-md-3">
                                    <label>Item Category<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="ddlitemcategory" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Item Category!'></i>" ErrorMessage="Select Item Category" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlitemcategory" OnSelectedIndexChanged="ddlitemcategory_SelectedIndexChanged" OnInit="ddlitemcategory_Init" AutoPostBack="true" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <label>Item Type<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ControlToValidate="ddlitemtype" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Item Type!'></i>" ErrorMessage="Select Item Type" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlitemtype" OnInit="ddlitemtype_Init" OnSelectedIndexChanged="ddlitemtype_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                    </div>

                                </div>

                                <div class="col-md-3">
                                    <label>Item Name<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ControlToValidate="ddlitemname" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Item Name!'></i>" ErrorMessage="Select Item Name" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlitemname" OnSelectedIndexChanged="ddlitemname_SelectedIndexChanged" OnInit="ddlitemname_Init" AutoPostBack="true" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Available Stock<span style="color: red;">*</span>
                                        </label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="a"
                                                ErrorMessage="Available Stock" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Available Stock!'></i>"
                                                ControlToValidate="txtAvailableStock" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtAvailableStock" Enabled="false" AutoPostBack="true" placeholder="Available Stock" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Item Unit<span style="color: red;">*</span>
                                        </label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ControlToValidate="txtItemUnit" Text="<i class='fa fa-exclamation-circle' title='Enter Item Unit!'></i>" ErrorMessage="Enter Item Unit" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtItemUnit" Enabled="false" placeholder="Item Unit" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Item Quantity<span style="color: red;">*</span>
                                        </label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="a"
                                                ErrorMessage="Enter Net Quantity" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Net Quantity!'></i>"
                                                ControlToValidate="txtitemqty" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtitemqty" ErrorMessage="Enter Valid Quantity"></asp:RegularExpressionValidator>

                                        </span>
                                        <asp:TextBox ID="txtitemqty" Enabled="false" AutoPostBack="true" OnTextChanged="txtitemqty_TextChanged" MaxLength="10" placeholder="Enter Item Quantity" Text="0" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            MRP<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic" ControlToValidate="txtMRP" Text="<i class='fa fa-exclamation-circle' title='Enter MRP!'></i>" ErrorMessage="Enter MRP" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtMRP" Enabled="false" placeholder="Enter MRP" Text="0" CssClass="form-control" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblDistributorDCSPrice" Visible="false" runat="server"></asp:Label>
                                        <asp:Label ID="lblDCSMargine" Visible="false" runat="server"></asp:Label>
                                        <asp:Label ID="lblSecretaryPrice" Visible="false" runat="server"></asp:Label>
                                        <asp:Label ID="lblSecretaryMargine" Visible="false" runat="server"></asp:Label>

                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Total Amount<span style="color: red;">*</span>
                                        </label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="Dynamic" ControlToValidate="txttotalamount" Text="<i class='fa fa-exclamation-circle' title='Enter Total Amount!'></i>" ErrorMessage="Enter Total Amount" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txttotalamount" Enabled="false" Text="0.0" placeholder="Item Unit" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnAddItemInfo_Click" Style="margin-top: 20px;" ValidationGroup="a" ID="btnAddItemInfo" Text="Add Item" />
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <hr />
                                    <asp:GridView ID="gv_SealInfo" ShowHeader="true" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server">
                                        <Columns>

                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSealNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item Category">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemCategory" runat="server" Text='<%# Eval("ItemCatName") %>'></asp:Label>
                                                    <asp:Label ID="lblItemCat_id" Visible="false" runat="server" Text='<%# Eval("ItemCat_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemTypeName" runat="server" Text='<%# Eval("ItemTypeName") %>'></asp:Label>
                                                    <asp:Label ID="lblItemType_id" Visible="false" runat="server" Text='<%# Eval("ItemType_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                    <asp:Label ID="lblItem_id" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unit Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUnitName" runat="server" Text='<%# Eval("UnitName") %>'></asp:Label>
                                                    <asp:Label ID="lblUnit_id" Visible="false" runat="server" Text='<%# Eval("Unit_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Item Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblI_Quantity" runat="server" Text='<%# Eval("I_Quantity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="MRP">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMRP" runat="server" Text='<%# Eval("MRP") %>'></asp:Label>
                                                    <asp:Label ID="lblDistributorDCSPrice" Visible="false" runat="server" Text='<%# Eval("DistributorDCSPrice") %>'></asp:Label>
                                                    <asp:Label ID="lblDCSMargine" Visible="false" runat="server" Text='<%# Eval("DCSMargine") %>'></asp:Label>
                                                    <asp:Label ID="lblSecretaryPrice" Visible="false" runat="server" Text='<%# Eval("SecretaryPrice") %>'></asp:Label>
                                                    <asp:Label ID="lblSecretaryMargine" Visible="false" runat="server" Text='<%# Eval("SecretaryMargine") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNetAmount" runat="server" Text='<%# Eval("NetAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDeleteCC" OnClick="lnkDeleteCC_Click" runat="server" ToolTip="DeleteCC" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete this record?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </fieldset>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Action</legend>
                                <div class="col-md-1" style="margin-top: 20px;">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-primary" Enabled="false" ValidationGroup="b" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />

                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-1" style="margin-top: 20px;">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-default" />
                                        </div>
                                    </div>
                                </div>

                            </fieldset>
                        </div>
                    </div>


                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Local Sale Details</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Date</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDateF" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <i class="far fa-calendar-alt"></i>
                                                </span>
                                            </div>
                                            <asp:TextBox ID="txtDateF" autocomplete="off" AutoPostBack="true" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server" OnTextChanged="txtDateF_TextChanged"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="table-responsive">

                                    <asp:GridView ID="gv_localsaleINV" OnRowDataBound="gv_localsaleINV_RowDataBound" ShowHeader="true" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server" OnRowCommand="gv_localsaleINV_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    <asp:Label ID="lblShiftTime" Visible="false" runat="server" Text='<%# Eval("ShiftTime") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Generate Invoice Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDT_InvoiceDt" runat="server" Text='<%# (Convert.ToDateTime(Eval("InvoiceDt"))).ToString("dd-MM-yyyy") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoiceNo" runat="server" Text='<%# Eval("InvoiceNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Producer Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPType" runat="server" Text='<%# Eval("PType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProducerName" runat="server" Text='<%# Eval("ProducerName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Shift">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblV_Shift" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNetAmount" runat="server" Text='<%# Eval("NetAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <a href='../MilkCollection/View_Invoice_DcsLocalSale.aspx?Invid=<%# new APIProcedure().Encrypt(Eval("DcsLocalSale_Id").ToString()) %>' target="_blank" title="Print Sale Invoice"><i class="fa fa-print"></i></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit/Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditRecord"  Visible='<%# Eval("Count").ToString()=="0"?true:false %>' CommandArgument='<%# Eval("DcsLocalSale_Id") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteRecord"  Visible='<%# Eval("Count").ToString()=="0"?true:false %>' CommandArgument='<%# Eval("DcsLocalSale_Id") %>' OnClientClick="return confirm('Do you really want to delete record?')"><i class="fa fa-trash"></i></asp:LinkButton>
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

    <script type="text/javascript">
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('b');
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


        function allnumeric(inputtxt) {
            var numbers = /^[0-9]+$/;
            if (inputtxt.value.match(numbers)) {
                alert('only number has accepted....');
                document.form1.text1.focus();
                return true;
            }
            else {
                alert('Please input numeric value only');
                document.form1.text1.focus();
                return false;
            }
        }
        $(document).on('focus', '.select2-selection.select2-selection--single', function (e) {
            $(this).closest(".select2-container").siblings('select:enabled').select2('open');
        });
    </script>
</asp:Content>


