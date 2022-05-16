<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ReceipeMaster.aspx.cs" Inherits="mis_dailyplan_ReceipeMaster" %>

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
                    <h3 class="box-title">Recipe Master</h3>
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
                                        <label>Date (दिनांक)</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtDate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>

                                    </div>
                                </div>


                                <div class="col-md-3">
                                    <label>Section<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" Display="Dynamic" ControlToValidate="ddlProduct" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Product!'></i>" ErrorMessage="Select Product" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlProductSection" OnInit="ddlProductSection_Init" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlProductSection_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>


                                <div class="col-md-3">
                                    <label>Product<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Display="Dynamic" ControlToValidate="ddlProduct" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Product!'></i>" ErrorMessage="Select Product" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlProduct" AutoPostBack="true" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" OnInit="ddlProduct_Init" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                    </div>
                                </div>

                            </fieldset>
                        </div>
                    </div>



                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Ingredients</legend>

                                <div class="col-md-2" runat="server" visible="false">
                                    <label>Item Category<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="ddlitemcategory" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Item Category!'></i>" ErrorMessage="Select Item Category" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlitemcategory" OnSelectedIndexChanged="ddlitemcategory_SelectedIndexChanged" OnInit="ddlitemcategory_Init" AutoPostBack="true" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2" runat="server" visible="false">
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
                                            Item Ratio In % <span style="color: red;">*</span>
                                        </label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="a"
                                                ErrorMessage="Enter Item Ratio In %" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Item Ratio In %!'></i>"
                                                ControlToValidate="txtitemqty" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtitemqty" ToolTip="Do Not Use % Sign" Enabled="false" MaxLength="5" placeholder="Enter Item Ratio In %" Text="0" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <label>Mapping Product</label>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlmappedproduct" OnInit="ddlmappedproduct_Init" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                    </div>
                                </div>



                                <div class="col-md-1">
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

                                            <asp:TemplateField HeaderText="Item Category" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemCategory" runat="server" Text='<%# Eval("ItemCatName") %>'></asp:Label>
                                                    <asp:Label ID="lblItemCat_id" Visible="false" runat="server" Text='<%# Eval("ItemCat_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item Type" Visible="false">
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


                                            <asp:TemplateField HeaderText="Item Ratio In %">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblI_Quantity" runat="server" Text='<%# Eval("I_Quantity") %>'></asp:Label>
                                                    %
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item Ratio Off">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMappingProductName" runat="server" Text='<%# Eval("MappingProductName") %>'></asp:Label>
                                                    <asp:Label ID="lblMappingProductId" Visible="false" runat="server" Text='<%# Eval("MappingProductId") %>'></asp:Label>
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
                                <legend>Packaging</legend>


                                <div class="col-md-3">
                                    <label>Variants Name<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" Display="Dynamic" ControlToValidate="ddlvariantsName" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select variants Name!'></i>" ErrorMessage="Select variants Name" ValidationGroup="c"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlvariantsName" OnInit="ddlvariantsName_Init" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <label>Item Name<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" Display="Dynamic" ControlToValidate="ddlItemNamepackaging" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Item Name!'></i>" ErrorMessage="Select Item Name" ValidationGroup="c"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlItemNamepackaging" OnSelectedIndexChanged="ddlItemNamepackaging_SelectedIndexChanged" OnInit="ddlItemNamepackaging_Init" AutoPostBack="true" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Packet Qty
                                            <asp:Label ID="lblUnitN" runat="server"></asp:Label><span style="color: red;">*</span>
                                        </label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ValidationGroup="c"
                                                ErrorMessage="Enter Packets Qty" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Packets Qty!'></i>"
                                                ControlToValidate="txttotalpkts" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txttotalpkts" Enabled="false" MaxLength="5" placeholder="Enter Packets Qty" Text="0" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnaddpackaging_Click" Style="margin-top: 20px;" ValidationGroup="c" ID="btnaddpackaging" Text="Add Item" />
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <hr />
                                    <asp:GridView ID="gbpackaging" ShowHeader="true" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server">
                                        <Columns>

                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSealNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Variant Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVariant_Name" runat="server" Text='<%# Eval("Variant_Name") %>'></asp:Label>
                                                    <asp:Label ID="lblVariant_ID" Visible="false" runat="server" Text='<%# Eval("Variant_ID") %>'></asp:Label>
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

                                            <asp:TemplateField HeaderText="Packets Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblI_PktQty" runat="server" Text='<%# Eval("I_PktQty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDeleteCCP" OnClick="lnkDeleteCCP_Click" runat="server" ToolTip="DeleteCC" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete this record?')"><i class="fa fa-trash"></i></asp:LinkButton>
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
                                <legend>Loss & Outcome</legend>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>
                                            Loss In % <span style="color: red;">*</span>
                                        </label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="a"
                                                ErrorMessage="Enter Loss In %" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Loss In %!'></i>"
                                                ControlToValidate="txtLossInPer" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtLossInPer" ToolTip="Do Not Use % Sign" MaxLength="3" placeholder="Enter Loss In %" Text="0" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>
                                            Outcome In % <span style="color: red;">*</span>
                                        </label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a"
                                                ErrorMessage="Enter Outcome In %" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Outcome In %!'></i>"
                                                ControlToValidate="txtOutcome" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtOutcome" ToolTip="Do Not Use % Sign" MaxLength="3" placeholder="Enter Outcome In %" Text="0" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
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
                     
                    <div class="row" runat="server" visible="false" id="DivDispalyRecipe">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Recipe Details According To Variant </legend>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>
                                            Loss In %
                                        </label>
                                        <asp:Label ID="lblLossInPer" CssClass="form-control" runat="server"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>
                                            Outcome In %
                                        </label>

                                        <asp:Label ID="lblOutcomeInPer" CssClass="form-control" runat="server"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <asp:GridView ID="GV_Recipe_Info" ShowHeader="true" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server">
                                        <Columns>

                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSealNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Variant Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Ingredients Item">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                    <asp:Label ID="lblItem_id" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Ingredients Ratio In %">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemRatioPer" runat="server" Text='<%# Eval("ItemRatioPer") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             
                                         <%--   <asp:TemplateField HeaderText="Ingredients To">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemTypeName" runat="server" Text='<%# Eval("ItemTypeName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unit Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUQCCode" runat="server" Text='<%# Eval("UQCCode") %>'></asp:Label>
                                                    <asp:Label ID="lblUnit_id" Visible="false" runat="server" Text='<%# Eval("Unit_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>


                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <hr />

                                <div class="col-md-12">
                                    <asp:GridView ID="GV_Recipe_Info_Pkg" ShowHeader="true" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server">
                                        <Columns>

                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSealNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Packet Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPacketQty" runat="server" Text='<%# Eval("PacketQty") + " (Pkt In 1 KG)" %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             
                                        </Columns>
                                    </asp:GridView>
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

    </script>
</asp:Content>
