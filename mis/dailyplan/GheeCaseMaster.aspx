<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="GheeCaseMaster.aspx.cs" Inherits="mis_dailyplan_GheeCaseMaster" %>

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
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Ghee Carage/Case Master</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                    
                    <div class="box-body">
                        <fieldset>
                            <legend>Filter</legend>
                            <div class="row">
                            
                              <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office Name</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvsocietyName" runat="server" Display="Dynamic" ControlToValidate="txtsocietyName" Text="<i class='fa fa-exclamation-circle' title='Enter society Name!'></i>" ErrorMessage="Enter society Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtsocietyName" Enabled="false" autocomplete="off" placeholder="Enter society Name" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Variant</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvVariant" runat="server" Display="Dynamic" ControlToValidate="ddlVariant" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Ghee Variant!'></i>" ErrorMessage="Select Ghee Variant" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlVariant" autocomplete="off" placeholder="Select Ghee Variant" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Cases Size(1 Case)</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvCasesSize" runat="server" Display="Dynamic" ControlToValidate="txtCasesSize" Text="<i class='fa fa-exclamation-circle' title='Enter Cases Size!'></i>" ErrorMessage="Enter Cases Size" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtCasesSize" onkeypress="return validateDec(this,event)" MaxLength="4" autocomplete="off" placeholder="Enter Cases Size" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Button ID="btnSave" ValidationGroup="Save" runat="server" CssClass="btn btn-success" style="margin-top:21px;" OnClientClick="return ValidatePage()" OnClick="btnSave_Click" Text="Save"/>
                                </div>
                            </div>

                        </div>
                        </fieldset>
                        <fieldset>
                            <legend>Cases Size Detail</legend>
                            <div class="col-md-12">
                                <asp:GridView ID="gvcasedetail" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" OnRowCommand="gvcasedetail_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Variant">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVariant" runat="server" Text='<%# Eval("Variant") %>'></asp:Label>
                                                <asp:Label ID="lblItem_ID" runat="server" CssClass="hidden" Text='<%# Eval("Item_Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cases Size">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCasesSize" runat="server" Text='<%# Eval("CasesSize") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("GheeCase_Id") %>' CommandName="EditRecord"><i class="fa fa-edit"></i></asp:LinkButton>
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

