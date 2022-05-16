
<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CitizenItemMapping.aspx.cs" Inherits="mis_CitizenItemMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
       <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
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
                <div class="box box-Manish">
                    <div class="box-header">
                        <h3 class="box-title">Citizen Item Mapping</h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </div>
                        </div>
                         <div class="row">
                             <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Item Category<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                InitialValue="0" ErrorMessage="Select Type" Text="<i class='fa fa-exclamation-circle' title='Select Type !'></i>"
                                                ControlToValidate="ddlItemCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                     
                                        <asp:DropDownList ID="ddlItemCategory" OnInit="ddlItemCategory_Init" OnSelectedIndexChanged ="ddlItemCategory_SelectedIndexChanged" runat="server"  AutoPostBack="true" CssClass="form-control select2"></asp:DropDownList>
                                         </div>
                             </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Item Name<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                InitialValue="0" ErrorMessage="Select Item" Text="<i class='fa fa-exclamation-circle' title='Select Item !'></i>"
                                                ControlToValidate="ddlItem" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                      <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-control select2">
                                          <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                      </asp:DropDownList>
                                </div>
                          </div>
                                <div class="col-md-4">
                                <div class="form-group">
                                        <label>Citizen Name<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                InitialValue="0" ErrorMessage="Select Citizen" Text="<i class='fa fa-exclamation-circle' title='Select Citizen !'></i>"
                                                ControlToValidate="ddlCitizen" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                      <asp:DropDownList ID="ddlCitizen" OnInit="ddlCitizen_Init" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                </div>
                                                              
                                    </div>
 
                                                           
                           
                      </div>
                         <div class="row">
                            <hr />
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-block btn-default" />
                                </div>
                            </div>
                        </div>
                 </div>
                <!-- /.box-body -->
                 <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Citizen Item Mapping Details</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                 <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                    EmptyDataText="No Record Found." DataKeyNames="CitizenItemMappingId" OnRowCommand="GridView1_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Citizen Name">
                                            <ItemTemplate>
                                                      <asp:Label ID="lblCitizenId" Visible="false" runat="server" Text='<%# Eval("CitizenId") %>' />
                                                 <asp:Label ID="lblItemCat_id" Visible="false" runat="server" Text='<%# Eval("ItemCat_id") %>' />                                                 
                                                <asp:Label ID="lblItem_id" runat="server" Visible="false" Text='<%# Eval("Item_id") %>' />
                                                 <asp:Label ID="lblCitizenName" runat="server" Text='<%# Eval("CitizenName") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Item Category">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemCatName"  runat="server" Text='<%# Eval("ItemCatName") %>' />
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Item">
                                            <ItemTemplate>
                                                  <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>' />
                                          
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("CitizenItemMappingId") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("CitizenItemMappingId") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
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
            <!-- /.content -->
         </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
       <script type="text/javascript">
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
