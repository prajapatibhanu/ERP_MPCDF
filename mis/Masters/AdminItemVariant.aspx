
<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="AdminItemVariant.aspx.cs" Inherits="mis_Masters_AdminItemVariant" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
      <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <style>
        .columngreen {
            background-color: #aee6a3 !important;
        }
        .columnred {
            background-color: #f05959 !important;
        }
         .columnmilk {
            background-color: #bfc7c5 !important;
        }
        .columnproduct {
            background-color:#f5f376 !important;
        }
    </style>
  
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <%--Confirmation Modal Start --%>
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" OnClick="btnSubmit_Click" ID="btnYes" Style="margin-top: 20px; width: 50px;" />
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
                    <h3 class="box-title">Item Variant Registration</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" CssClass="Autoclr" runat="server"></asp:Label>
                        </div>
                    </div>
                    <fieldset>
                        
                        <div class="row">
                           
                         
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Item Category<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfv2" ValidationGroup="a"
                                            InitialValue="0" ForeColor="Red" ErrorMessage="Select Item Category" 
                                            Text="<i class='fa fa-exclamation-circle' title='Select Item Category !'></i>"
                                            ControlToValidate="ddlItemCategory" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlItemCategory" 
                                        OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged" 
                                        runat="server" CssClass="form-control select2" AutoPostBack="true"
                                         ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Item Name<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                            InitialValue="0" ForeColor="Red" ErrorMessage="Select Item " Text="<i class='fa fa-exclamation-circle' title='Select Item Category !'></i>"
                                            ControlToValidate="ddlItem" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-control select2" AutoPostBack="true" ClientIDMode="Static">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                              <div class="col-md-2">
                        <div class="form-group">
                            <label>Item Variant Name<span class="text-danger">*</span></label>
                            <span class="pull-right">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ForeColor="Red" Display="Dynamic" ValidationGroup="a"  runat="server" ControlToValidate="txtItemVariantName" ErrorMessage="Enter Item Variant Name" Text="<i class='fa fa-exclamation-circle' title='Please Enter Item Variant Name !'></i>"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="save" runat="server" ControlToValidate="txtItemVariantName" ForeColor="Red" Display="Dynamic"
                                    ValidationExpression="^[a-zA-Z\s]+$" ErrorMessage="*Valid characters: Alphabets." Text="<i class='fa fa-exclamation-circle' title='Please Enter Item Variant Name !'></i>" />
                            </span>
                            <asp:TextBox ID="txtItemVariantName" autocomplete="off" Width="100%" runat="server" placeholder="Enter Item Variant Name" CssClass="form-control" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>

                            
                              <div class="col-md-2">
                        <div class="form-group">
                            <label>Item Packaging Size<span class="text-danger">*</span></label>
                            <span class="pull-right">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" Display="Dynamic" ValidationGroup="a" 
                                     runat="server" ControlToValidate="txtItemVariantName" ErrorMessage="Enter Item Packaging Size" Text="<i class='fa fa-exclamation-circle' title='Please Enter Item Variant Name !'></i>"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="save" runat="server" 
                                    ControlToValidate="txtPackSize" ForeColor="Red" Display="Dynamic"
                                    ValidationExpression="^[0-9]+$" ErrorMessage="Alphabets not allowed." Text="<i class='fa fa-exclamation-circle' title='Please Enter Item Variant Name !'></i>" />
                            </span>
                            <asp:TextBox ID="txtPackSize" autocomplete="off" Width="100%" runat="server" placeholder="In numbers(eg.500)" CssClass="form-control" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                                <div class="col-md-2">
                        <div class="form-group">
                            <label>Item Unit<span class="text-danger">*</span></label>
                            <span class="pull-right">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ForeColor="Red" Display="Dynamic" ValidationGroup="a"  runat="server" 
                                    ControlToValidate="ddlUnit" InitialValue="0" ErrorMessage="Select Unit" Text="<i class='fa fa-exclamation-circle' title='Please Select Unit !'></i>"></asp:RequiredFieldValidator>
                            </span>
                            <asp:DropDownList ID="ddlUnit" runat="server" Width="100%" CssClass="form-control select2">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                               <div class="col-md-2">
                        <div class="form-group">
                            <label>Packaging Mode <span class="text-danger">*</span></label>
                            <span class="pull-right">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ForeColor="Red" Display="Dynamic"
                                     ValidationGroup="a"  runat="server" ControlToValidate="ddlPackMode" InitialValue="0"
                                     ErrorMessage="Select Packaging Mode" Text="<i class='fa fa-exclamation-circle' title='Select Packaging Mode !'></i>"></asp:RequiredFieldValidator>
                            </span>
                            <asp:DropDownList ID="ddlPackMode" OnInit="ddlPackMode_Init" runat="server" Width="100%" CssClass="form-control select2">
                               
                            </asp:DropDownList>
                        </div>
                    </div>

                            
                                  <div class="col-md-2">
                        <div class="form-group">
                            <label>Item Specification</label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" Display="Dynamic" runat="server" ControlToValidate="txtItemSpecification"
                                ErrorMessage="Only alphabet allow" ValidationGroup="a"  ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Only alphabet allow. !'></i>"
                                SetFocusOnError="true" ValidationExpression="^[a-zA-Z\s]+$">
                            </asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtItemSpecification" autocomplete="off" Width="100%" runat="server" placeholder="Item Specification" CssClass="form-control" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div> 

                             <div class="col-md-2">
                        <div class="form-group">
                            <label>HSN Code</label>
                           <%-- <span class="pull-right">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ForeColor="Red" Display="Dynamic" ValidationGroup="save" runat="server" ControlToValidate="ddlHsnCode" InitialValue="0" ErrorMessage="Select HSN Code" Text="<i class='fa fa-exclamation-circle' title='Please Select HSN Code !'></i>"></asp:RequiredFieldValidator>
                            </span>--%>
                            <asp:DropDownList ID="ddlHsnCode" runat="server" Width="100%" CssClass="form-control select2">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                         
                    
                          
                        </div>
                    </fieldset>
                    
                    <div class="row">
                        <hr />
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnClear" OnClick="btnClear_Click" runat="server"  Text="Clear" CssClass="btn btn-block btn-default" />
                            </div>
                        </div>
                    </div>

                </div>

            </div>
            <!-- /.box-body -->
            <div class="row" id="pnlVariant" runat="server">
            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Item Variant Registration Details</h3>
                    
      
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" OnRowCommand="GridView1_RowCommand" EmptyDataText="No Record Found." PageSize="50" runat="server" class="table table-hover table-bordered pagination-ys"
                            ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"  DataKeyNames="Item_id" >
                            <Columns>
                                
                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                     <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" Visible="false" >
                                    <ItemTemplate>
                                          <asp:Label ID="lblItem_id" Visible="false" runat="server" Text='<%# Eval("Item_id") %>' />    

                                         <asp:Label ID="lblItemType_id" Visible="false" runat="server" Text='<%# Eval("ItemType_id") %>' />
                                        <asp:Label ID="lblItemCat_id" Visible="false" runat="server" Text='<%# Eval("ItemCat_id") %>' />
                                         <asp:Label ID="lblUnit_id" Visible="false" runat="server" Text='<%# Eval("Unit_id") %>' />
                                        <asp:Label ID="lblPackMode" Visible="false" runat="server" Text='<%# Eval("Packaging_Mode") %>' />
                                       
                                       
                                      
                                       
                                         <asp:Label ID="lblIsActive" Text='<%# Eval("Item_IsActive")%>' runat="server" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Category Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemCatName" runat="server" Text='<%# Eval("ItemCatName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Item Type Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemTypeName" runat="server" Text='<%# Eval("ItemTypeName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <%--  <asp:TemplateField HeaderText="Organization Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrganization_TypeName" runat="server" Text='<%# Eval("Organization_Type").ToString()=="1" ? "Government" :"Private" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Item Variant Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Packaging Size">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPackagingSize" runat="server" Text='<%# Eval("PackagingSize") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUQCCode" runat="server" Text='<%# Eval("UQCCode") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Packaging Mode">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPackaging_Mode" runat="server" Text='<%# Eval("PackagingModeName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="HSN Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHSNCode" runat="server" Text='<%# Eval("HSNCode").ToString()== "0"? "NA": Eval("HSNCode") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Actions">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("Item_id") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                 
                           
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

            </div>
                </div>
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
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







