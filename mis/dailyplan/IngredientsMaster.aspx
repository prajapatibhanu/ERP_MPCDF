<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="IngredientsMaster.aspx.cs" Inherits="mis_dailyplan_IngredientsMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSave_Click"  Style="margin-top: 20px; width: 50px;" />
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
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Ingredients Master</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                           <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office Name</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvsocietyName" runat="server" Display="Dynamic" ControlToValidate="txtsocietyName" Text="<i class='fa fa-exclamation-circle' title='Enter society Name!'></i>" ErrorMessage="Enter society Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtsocietyName" Enabled="false" autocomplete="off" placeholder="Enter society Name" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Date (दिनांक)</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtDate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>

                                    </div>
                                </div>
                            <div class="col-md-3">
                                    <label>Product<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvProduct" runat="server" Display="Dynamic" ControlToValidate="ddlProduct" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Product!'></i>" ErrorMessage="Select Product" ValidationGroup="Save" ></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlProduct" CssClass="form-control select2" runat="server" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                           </div>
                       <fieldset>
                           <legend>Ingredient Used for Product</legend>
                           <div class="row">
                               <div class="col-md-2">
                                    <label>Ingredients<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="ddlProduct" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Ingredients!'></i>" ErrorMessage="Select Ingredients" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlIngredients"  CssClass="form-control select2" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                               <div class="col-md-2">
                                    <label>Calculation Method<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ControlToValidate="ddlProduct" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Calculation Method!'></i>" ErrorMessage="Select Calculation Method" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlCalculationMethod"  CssClass="form-control select2" runat="server">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem>Percentage</asp:ListItem>
                                            <asp:ListItem>Multiply</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                               <div class="col-md-2">
                                    <label>Value<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvValue" runat="server" Display="Dynamic" ControlToValidate="txtValue"  Text="<i class='fa fa-exclamation-circle' title='Enter Value!'></i>" ErrorMessage="Enter Value" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtValue"  CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <label>Fat Factor<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvFatFactor" runat="server" Display="Dynamic" ControlToValidate="txtFatFactor"  Text="<i class='fa fa-exclamation-circle' title='Enter Fat Factor!'></i>" ErrorMessage="Enter Fat Factor" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtFatFactor"  CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <label>Snf Factor<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvSnfFactor" runat="server" Display="Dynamic" ControlToValidate="txtSnfFactor"  Text="<i class='fa fa-exclamation-circle' title='Enter Snf Factor!'></i>" ErrorMessage="Enter Snf Factor" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtSnfFactor"  CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                               <div class="col-md-2">
                                   <div class="form-group">
                                       <asp:Button ID="btnAdd" runat="server" ValidationGroup ="a" style="margin-top:22px;" Text="Add" CssClass="btn btn-success" OnClick="btnAdd_Click"/>
                                   </div>
                               </div>
                           </div>
                           <div class="row">
                               <div class="col-md-12">
                                   <asp:GridView ID="gvIngredientDetail" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="gvIngredientDetail_RowCommand">
                                       <Columns>
                                           <asp:TemplateField HeaderText="S.No">
                                               <ItemTemplate>
                                                   <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex +1%>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Ingredient">
                                               <ItemTemplate>
                                                   <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                    <asp:Label ID="lblItem_id" CssClass="hidden" runat="server" Text='<%# Eval("Item_id")%>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Calculation Method">
                                               <ItemTemplate>
                                                   <asp:Label ID="lblCalculationMethod" runat="server" Text='<%# Eval("CalculationMethod")%>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Value">
                                               <ItemTemplate>
                                                   <asp:Label ID="lblValue" runat="server" Text='<%# Eval("Value")%>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fat Factor">
                                               <ItemTemplate>
                                                   <asp:Label ID="lblFatFactor" runat="server" Text='<%# Eval("FatFactor")%>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Snf Factor">
                                               <ItemTemplate>
                                                   <asp:Label ID="lblSnfFactor" runat="server" Text='<%# Eval("SnfFactor")%>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Action">
                                               <ItemTemplate>
                                                   <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteRecord" CommandArgument='<%# Eval("Item_id") %>' Text="Delete"><i class="fa fa-trash"></i></asp:LinkButton>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                       </Columns>
                                   </asp:GridView>
                               </div>
                           </div>
                       </fieldset>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" ValidationGroup="Save" OnClientClick="return ValidatePage();"/>
                                <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-default" />
                                </div>
                            </div>
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Product Ingredients Detail</legend>
                                <div class="col-md-3">
                                    <label>Product<span style="color: red;">*</span></label>
                                    <%--<span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvProd_flt" runat="server" Display="Dynamic" ControlToValidate="ddlProduct" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Product!'></i>" ErrorMessage="Select Product" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>--%>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlProd_flt" CssClass="form-control select2" runat="server" OnSelectedIndexChanged="ddlProd_flt_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                   
                                <div class="col-md-12">
                                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered" EmptyDataText="No Re" ShowHeaderWhenEmpty ="true" AutoGenerateColumns="false">
                                       <Columns>
                                           <asp:TemplateField HeaderText="S.No">
                                               <ItemTemplate>
                                                   <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex +1%>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Ingredient">
                                               <ItemTemplate>
                                                   <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                           
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Calculation Method">
                                               <ItemTemplate>
                                                   <asp:Label ID="lblCalculationMethod" runat="server" Text='<%# Eval("CalculationMethod")%>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Value">
                                               <ItemTemplate>
                                                   <asp:Label ID="lblValue" runat="server" Text='<%# Eval("Value")%>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fat Factor">
                                               <ItemTemplate>
                                                   <asp:Label ID="lblFatFactor" runat="server" Text='<%# Eval("FatFactor")%>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Snf Factor">
                                               <ItemTemplate>
                                                   <asp:Label ID="lblSnfFactor" runat="server" Text='<%# Eval("SnfFactor")%>'></asp:Label>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
    <script>
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('Save');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
    </script>
</asp:Content>

