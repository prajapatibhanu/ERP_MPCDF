<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RootMaster.aspx.cs" Inherits="mis_Masters_RootMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
     <%--ConfirmationModal Start --%>
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
                        <asp:Button runat="server" CssClass="btn btn-success" OnClick="btnSave_Click" Text="Yes" ID="btnYes" Style="margin-top: 20px; width: 50px;" />
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
                            <h3 class="box-title">Root Master</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                       
                            <div class="box-body">
                                <fieldset>
                                    <legend>Root Master</legend>
                                    <div class="row">
                               <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                                    ControlToValidate="ddlDS" InitialValue="0"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select DS!'></i>"
                                                    ErrorMessage="Select DS." SetFocusOnError="true" ForeColor="Red"
                                                    ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlDS" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Root Name<span class="text-danger">*</span></label>
                                         <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"
                                                        ControlToValidate="txtRootName"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter Root Name!'></i>"
                                                        ErrorMessage="Enter Root Name." SetFocusOnError="true" ForeColor="Red"
                                                        ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                </span>
                                        <asp:TextBox ID="txtRootName" runat="server" CssClass="form-control"></asp:TextBox>
                                   
                                         </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Root Description</label>
                                        <asp:TextBox ID="txtRootDescription" runat="server"  CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" STYLE="margin-top:21px;" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="Save" OnClientClick="return ValidatePage();" OnClick="btnSave_Click"/>
                                    </div>
                                </div>
                            </div>
                                </fieldset>
                            
                        </div>
                        
                      
                        <div class="box-body">
                              <fieldset>
                            <legend>Root Details</legend>
                                  <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvDetails" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnRowCommand="gvDetails_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No" ItemStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Root Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBMCTankerRootName" runat="server" Text='<%# Eval("BMCTankerRootName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Root Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBMCTankerRootDescription" runat="server" Text='<%# Eval("BMCTankerRootDescription") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("BMCTankerRoot_Id") %>' CommandName="EditRecord"><i class="fa fa-edit"></i></asp:LinkButton>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
     <script>
         function ValidatePage() {

             if (typeof (Page_ClientValidate) == 'function') {
                 Page_ClientValidate('Save');
             }
             debugger;
             if (Page_IsValid) {

                 if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }

            }
        }

    </script>
</asp:Content>

