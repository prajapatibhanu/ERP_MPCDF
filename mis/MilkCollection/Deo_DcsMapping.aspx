<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Deo_DcsMapping.aspx.cs" Inherits="mis_MilkCollection_Deo_DcsMapping" %>

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

    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Deo Mapping Details</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">

                    <div class="col-md-3">
                        <label>DEO Emp Name<span style="color: red;">*</span></label>
                        <span class="pull-right">
                            <asp:RequiredFieldValidator ID="rfvFarmer" runat="server" Display="Dynamic" ControlToValidate="ddldeoemp" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Deo Emp!'></i>" ErrorMessage="Select Deo" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                        </span>
                        <div class="form-group">
                            <asp:DropDownList ID="ddldeoemp" CssClass="form-control select2" runat="server"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <label>Office Task<span style="color: red;">*</span></label>
                        <span class="pull-right">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="ddltask" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Office Task!'></i>" ErrorMessage="Select Office Task" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                        </span>
                        <div class="form-group">
                            <asp:DropDownList ID="ddltask" AutoPostBack="true" OnSelectedIndexChanged="ddltask_SelectedIndexChanged" CssClass="form-control select2" runat="server">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="5">BMC</asp:ListItem>
                                <asp:ListItem Value="6">DCS</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Task Assign By<span style="color: red;"> *</span></label>
                            <span class="pull-right">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                    ErrorMessage="Enter Assign By Name" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Assign By Name !'></i>"
                                    ControlToValidate="txtTaskAssignBy" Display="Dynamic" runat="server">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic" ValidationExpression="^[^'@%#$&=^!~?]+$" ValidationGroup="a" runat="server" ControlToValidate="txtTaskAssignBy" ErrorMessage="Enter Valid Assign By Name." Text="<i class='fa fa-exclamation-circle' title='Enter Valid Assign By Name. !'></i>"></asp:RegularExpressionValidator>
                            </span>
                            <asp:TextBox autocomplete="off" runat="server" CssClass="form-control" ID="txtTaskAssignBy" MaxLength="40" placeholder="Enter DEO Name"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-md-12" runat="server" id="dv_gvMilkQualityDeails">
                        <hr />
                        <div class="form-group table-responsive">
                            <asp:GridView ID="gvDeoDetail" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                EmptyDataText="No Record Found." DataKeyNames="Office_ID">
                                <Columns>

                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("Office_ID").ToString()%>' Checked='<%# Eval("MappingStatus").ToString()=="1" ? true : false %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sr.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Office_ID").ToString()%>' runat="server" />
                                            <asp:Label ID="lblDeo_Id" Visible="false" runat="server" Text='<%# Eval("Deo_Id") %>'></asp:Label>
                                            <asp:Label ID="lblOffice_Id" Visible="false" runat="server" Text='<%# Eval("Office_Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Office Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Office_Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOffice_Code" runat="server" Text='<%# Eval("Office_Code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="User Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Password">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPassword" runat="server" Text='<%# Eval("Password") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>


                    <div class="col-md-12">
                        <div class="form-group">
                            <asp:Button ID="btnSubmit" CssClass="btn btn-success" runat="server" OnClientClick="return ValidatePage();" Text="Save" ValidationGroup="a" />
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

