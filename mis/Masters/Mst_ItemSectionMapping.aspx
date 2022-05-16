<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Mst_ItemSectionMapping.aspx.cs" Inherits="mis_Masters_Mst_ItemSectionMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>     
           .lblpagenote {
            display: block;
            background: #e0eae7;
            text-align: -webkit-center;
            margin-bottom: 12px;
            padding: 10px;
        }   
           
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
     <%--Confirmation Modal Start --%>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">        
            <div class="modal-dialog" style="width: 340px;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                        <h4 class="modal-title" id="myModalLabel">Confirmation</h4>
                   </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <p>
                            <i class="fa fa-2x fa-question-circle"></i>
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer" style="text-align:center;">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
    </div>
    <%--ConfirmationModal End --%>
      <div class="loader"></div>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ModalSave" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content no-print">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Item Section Mapping</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <fieldset>
                                <legend>Item Section Mapping
                                </legend>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Item Category<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvic" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Item Category" Text="<i class='fa fa-exclamation-circle' title='Select Item Category !'></i>"
                                                    ControlToValidate="ddlItemCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlItemCategory" AutoPostBack="true" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Section Name<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Section Name" Text="<i class='fa fa-exclamation-circle' title='Select Section Name !'></i>"
                                                    ControlToValidate="ddlSection" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlSection" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" CssClass="form-control select2">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div> 
                                     <div class="col-md-2">
                                        <div class="form-group">
                                            <label>SubSection Name<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select SubSection Name" Text="<i class='fa fa-exclamation-circle' title='Select SubSection Name !'></i>"
                                                    ControlToValidate="ddlSubSection" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlSubSection" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>    
                                   
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Item Name <span style="color: red;"> *</span></label>
                                         
                                            <asp:ListBox runat="server" ID="ddlItemName" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                    </div>
                                      
                                    <div class="col-md-2">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" OnClick="btnSubmit_Click" ValidationGroup="a" ID="btnSubmit" Text="Save" />

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
                            <h3 class="box-title">Item Section Mapping Details</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <%-- <div class="row">--%>
                                     <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Section Name<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Section Name" Text="<i class='fa fa-exclamation-circle' title='Select Section Name !'></i>"
                                                    ControlToValidate="ddlSection" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlSectionSearch" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div> 
                                  <div class="col-md-2">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" ID="btnSearch" Text="Search" />

                                        </div>
                                    </div>
                                    <%-- </div>--%>
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" OnRowCommand="GridView1_RowCommand" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                            EmptyDataText="No Record Found." DataKeyNames="ItemSectionMapping_Id">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Section Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAreaName" runat="server" Text='<%# Eval("SectionName") %>'></asp:Label>
                                                        <asp:Label ID="lblIsActive" Visible="false" runat="server" Text='<%# Eval("IsActive") %>'></asp:Label>
                                                     
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="SubSection Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubSectionName" runat="server" Text='<%# Eval("SubSectionName") %>'></asp:Label>
                                                        <asp:Label ID="lblMOrPSubSection_id" Visible="false" runat="server" Text='<%# Eval("MOrPSubSection_id") %>'></asp:Label>
                                                     
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Item Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                        <asp:Label ID="lblItemid" runat="server" Visible="false" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAStatus" runat="server" Text='<%# Eval("AStatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                   <asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("ItemSectionMapping_Id") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                                         <asp:LinkButton ID="lnkedit" CommandArgument='<%#Eval("ItemSectionMapping_Id") %>' CommandName="RecordEdit" runat="server" ToolTip="Edit" Style="color: blue;"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <div class="modal" id="ItemDetailsModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                 <div style="display: table; height: 100%; width: 100%;">
                                
                                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 230px;">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Item Detials</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row" style="height: 100px; overflow: scroll;">
                                 <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Iten Name</label>
                                        <asp:TextBox ID="txtItemName" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                        </div>
                                     </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Sub Section<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="ModalSave"
                                                InitialValue="0" ErrorMessage="Select Sub Section" Text="<i class='fa fa-exclamation-circle' title='Sub Section !'></i>"
                                                ControlToValidate="ddlSubSectionUpdate" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlSubSectionUpdate" Width="200px" runat="server" CssClass="form-control select2">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblModalMsg1" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                             <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="ModalSave" ID="btnUpdate" Text="Update" OnClick="btnUpdate_Click" />
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                     </div>
                <!-- /.modal-dialog -->
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
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
        $(document).ready(function () {
            $('.loader').fadeOut();
        });
        function myItemDetailsModal() {
            $("#ItemDetailsModal").modal('show');

        }
    </script>
<%--    <link href="../../../mis/css/bootstrap-multiselect.css" rel="stylesheet" />--%>
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../js/bootstrap-multiselect.js"></script>
   <%-- <script src="../../../mis/js/bootstrap-multiselect.js" type="text/javascript"></script>--%>

    <script>

        $(function () {
            $('[id*=ddlItemName]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });


        });
        
         </script>
</asp:Content>

