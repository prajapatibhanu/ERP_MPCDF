     <%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="AdminVillage.aspx.cs" Inherits="mis_Admin_AdminVillage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
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
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Village Master</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>State Name<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvStateName" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlState_Name" InitialValue="0" ErrorMessage="Select State Name" Text="<i class='fa fa-exclamation-circle' title='Select State Name!'></i>">
                                    </asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlState_Name" runat="server" OnInit="ddlState_Name_Init" AutoPostBack="true" ClientIDMode="Static" CssClass="form-control select2" OnSelectedIndexChanged="ddlState_Name_SelectedIndexChanged">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Division Name<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvDivisionName" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlDivision_Name" InitialValue="0" ErrorMessage="Select Division Name" Text="<i class='fa fa-exclamation-circle' title='Select Division Name!'></i>">
                                    </asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlDivision_Name" runat="server" AutoPostBack="true" ClientIDMode="Static" CssClass="form-control select2" OnSelectedIndexChanged="ddlDivision_Name_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>District Name<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvDistrictName" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlDistrict_Name" InitialValue="0" ErrorMessage="Select District Name" Text="<i class='fa fa-exclamation-circle' title='Select District Name !'></i>">
                                    </asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlDistrict_Name" runat="server" AutoPostBack="true" ClientIDMode="Static" CssClass="form-control select2" OnSelectedIndexChanged="ddlDistrict_Name_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Block Name<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvBlockName" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlBlock_Name" InitialValue="0" ErrorMessage="Select Block Name" Text="<i class='fa fa-exclamation-circle' title='Select Block Name !'></i>">
                                    </asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlBlock_Name" runat="server" AutoPostBack="true" ClientIDMode="Static" CssClass="form-control select2">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblVillageName" Text="Village Name" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvVillageName" ValidationGroup="a"
                                        ErrorMessage="Enter Village Name" Text="<i class='fa fa-exclamation-circle' title='Enter Village Name!'></i>"
                                        ControlToValidate="txtVillage_Name" ForeColor="Red" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVillage_Name"
                                        ErrorMessage="Only alphabet allow" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Only alphabet allow. !'></i>"
                                        SetFocusOnError="true" ValidationExpression="^[a-zA-Z\s]+$">
                                    </asp:RegularExpressionValidator>
                                </span>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtVillage_Name" MaxLength="150" placeholder="Enter Village Name" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a class="btn btn-block btn-default" href="AdminVillage.aspx">Clear</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="GridView1" PageSize="50" runat="server" class="table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="Village_ID" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="State_Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVillageID" Visible="false" runat="server" Text='<%# Eval("Village_ID") %>' />
                                            <asp:Label ID="lblStateName" runat="server" Text='<%# Eval("State_Name") %>' />
                                            <asp:Label ID="lblStateID" runat="server" CssClass="hidden" Text='<%# Eval("State_ID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Division Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDivisionName" runat="server" Text='<%# Eval("Division_Name") %>' />
                                            <asp:Label ID="lblDivisionID" runat="server" CssClass="hidden" Text='<%# Eval("Division_ID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="District Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDistrictName" runat="server" Text='<%# Eval("District_Name") %>' />
                                            <asp:Label ID="lblDistrictID" runat="server" CssClass="hidden" Text='<%# Eval("District_ID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Block Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBlockName" runat="server" Text='<%# Eval("Block_Name") %>' />
                                            <asp:Label ID="lblBlockID" runat="server" CssClass="hidden" Text='<%# Eval("Block_ID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Village Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVillageName" runat="server" Text='<%# Eval("Village_Name") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit" ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("Village_ID") %>' CssClass="label label-default" runat="server" ToolTip="Update" Text="Edit"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="30" HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" OnCheckedChanged="chkSelect_CheckedChanged" runat="server" ToolTip='<%# Eval("Village_ID").ToString()%>' Checked='<%# Eval("Village_IsActive").ToString()=="1" ? true : false %>' AutoPostBack="true" />
                                        </ItemTemplate>
                                        <ItemStyle Width="30px"></ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
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
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Edit") {
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
