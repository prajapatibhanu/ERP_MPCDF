<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ProductMaster.aspx.cs" Inherits="mis_Common_ProductMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="Server">      
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
                        <div class="col-md-4">
                            <div class="box box-Manish">
                                <div class="box-header">
                                    <h3 class="box-title">Milk Product Master</h3>
                                </div>
                                <!-- /.box-header -->
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <label>Product Category<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                        ErrorMessage="Select Product Category" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Product Category !'></i>"
                                                        ControlToValidate="ddlProductCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlProductCategory" CssClass="form-control select2" OnInit="ddlProductCategory_Init" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <label>Product Name    <span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                        ErrorMessage="Enter Product Name" Text="<i class='fa fa-exclamation-circle' title='Enter Product Name !'></i>"
                                                        ControlToValidate="txtProductName" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtProductName"
                                                        ErrorMessage="Only alphabet allow" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Only alphabet allow. !'></i>"
                                                        SetFocusOnError="true" ValidationExpression="^[a-zA-Z\s]+$">
                                                    </asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtProductName" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <label>Abbreviation<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                        ErrorMessage="Enter Abbreviation" Text="<i class='fa fa-exclamation-circle' title='Enter Abbreviation !'></i>"
                                                        ControlToValidate="txtAbbreviation" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtAbbreviation"
                                                        ErrorMessage="Only alphabet allow" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Only alphabet allow. !'></i>"
                                                        SetFocusOnError="true" ValidationExpression="^[a-zA-Z]+$">
                                                    </asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtAbbreviation" CssClass="form-control" MaxLength="5" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <label>Description<%--<span style="color: red;"> *</span>--%></label>
                                                <span class="pull-right">
                                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                        ErrorMessage="Enter Description" Text="<i class='fa fa-exclamation-circle' title='Enter Description !'></i>"
                                                        ControlToValidate="txtDescription" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>--%>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDescription"
                                                        ErrorMessage="Only alphabnumeric and space allow." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Only alphabnumeric and space allow. !'></i>"
                                                        SetFocusOnError="true" ValidationExpression="^[a-zA-Z0-9\s]+$">
                                                    </asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtDescription" CssClass="form-control" MaxLength="90" runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <hr />
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-block btn-primary" ValidationGroup="a" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Button ID="btnClear" OnClick="btnClear_Click" runat="server" Text="Clear" CssClass="btn btn-block btn-default" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="col-md-8">
                            <div class="box box-Manish">
                                <div class="box-header">
                                    <h3 class="box-title">Milk Product Details</h3>
                                </div>
                                <!-- /.box-header -->
                                <div class="box-body">
                                    <div class="table-responsive">
                                <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                    EmptyDataText="No Record Found." DataKeyNames="ProductId">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Product Category">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductId" Visible="false" runat="server" Text='<%# Eval("ProductId") %>' />
                                                    <asp:Label ID="lblCat_id" Visible="false" runat="server" Text='<%# Eval("Cat_id") %>' />
                                                    <asp:Label ID="lblCategory_Name" runat="server" Text='<%# Eval("Category_Name") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Abbreviation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPAbbreviation" runat="server" Text='<%# Eval("PAbbreviation") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                  <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                               <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("ProductId") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("ProductId") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <!-- /.box-body -->
                </section>
                <!-- /.content -->
            </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="Server">
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

