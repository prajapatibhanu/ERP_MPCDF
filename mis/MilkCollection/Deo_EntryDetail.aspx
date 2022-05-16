<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Deo_EntryDetail.aspx.cs" Inherits="mis_MilkCollection_Deo_EntryDetail" %>

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
                    <h3 class="box-title">Deo Details</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">

                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Enter Deo Name<span style="color: red;"> *</span></label>
                            <span class="pull-right">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                    ErrorMessage="Enter DEO Name" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DEO Name !'></i>"
                                    ControlToValidate="txtDeoName" Display="Dynamic" runat="server">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic" ValidationExpression="^[^'@%#$&=^!~?]+$" ValidationGroup="a" runat="server" ControlToValidate="txtDeoName" ErrorMessage="Enter Valid DEO Name." Text="<i class='fa fa-exclamation-circle' title='Enter Valid DEO Name. !'></i>"></asp:RegularExpressionValidator>
                            </span>
                            <asp:TextBox autocomplete="off" runat="server" CssClass="form-control" ID="txtDeoName" MaxLength="40" placeholder="Enter DEO Name"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Enter DEO Mobile No.<span style="color: red;"> *</span></label>
                            <span class="pull-right">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                    ErrorMessage="Enter DEO Mobile No" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DEO Mobile !'></i>"
                                    ControlToValidate="txtDeoMobile" Display="Dynamic" runat="server">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Dynamic" ValidationExpression="^[6789]\d{9}$" ValidationGroup="a" runat="server" ControlToValidate="txtDeoMobile" ErrorMessage="Enter DEO Mobile Number." Text="<i class='fa fa-exclamation-circle' title='Enter DEO Mobile Number. !'></i>"></asp:RegularExpressionValidator>
                            </span>
                            <asp:TextBox autocomplete="off" runat="server" onkeypress="return validateDec(this,event)" CssClass="form-control" ID="txtDeoMobile" MaxLength="10" placeholder="Enter Driver Mobile"></asp:TextBox>
                        </div>
                    </div>


                    <div class="col-md-1">
                        <div class="form-group">
                            <label>IsActive<span style="color: red;"> *</span></label>
                            <asp:CheckBox ID="chkIsActive" Checked="true" runat="server" />
                        </div>
                    </div>

                    <div class="col-md-1" style="margin-top: 20px;">
                        <div class="form-group">
                            <asp:Button ID="btnSubmit" CssClass="btn btn-success" runat="server" OnClientClick="return ValidatePage();" Text="Submit" ValidationGroup="a" />
                        </div>
                    </div>


                    <div class="col-md-12" style="margin-top: 20px;" runat="server" id="dv_gvMilkQualityDeails">
                        <hr />

                        <div class="form-group table-responsive">
                            <asp:GridView ID="gvDeoDetail" runat="server" OnSelectedIndexChanged="gvDeoDetail_SelectedIndexChanged" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                EmptyDataText="No Record Found." DataKeyNames="Deo_Id">
                                <Columns>

                                    <asp:TemplateField HeaderText="Sr.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Deo_Id").ToString()%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Deo Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeoName" runat="server" Text='<%# Eval("DeoName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="DeoMobile">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeoMobile" runat="server" Text='<%# Eval("DeoMobile") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Select" runat="server" CssClass="label label-default" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" OnCheckedChanged="chkSelect_CheckedChanged" runat="server" ToolTip='<%# Eval("Deo_Id").ToString()%>' Checked='<%# Eval("Status").ToString()=="1" ? true : false %>' AutoPostBack="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                </div>




            </div>


            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Filter Assign Office Task For DEO Name</h3>
                </div>
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                <div class="box-body">

                    <div class="col-md-3">
                        <label>DEO Emp Name</label>
                         
                        <div class="form-group">
                            <asp:DropDownList ID="ddldeoemp" AutoPostBack="true" OnSelectedIndexChanged="ddldeoemp_SelectedIndexChanged" CssClass="form-control select2" runat="server"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-12" runat="server" id="Div1">
                        <hr />
                        <div class="form-group table-responsive">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                EmptyDataText="No Record Found." DataKeyNames="Office_ID">
                                <Columns>
                                      
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

                                     <asp:TemplateField HeaderText="Task Assign By">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTaskAssignBy" runat="server" Text='<%# Eval("TaskAssignBy") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Task Assign Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreatedAt" runat="server" Text='<%# (Convert.ToDateTime(Eval("CreatedAt"))).ToString("dd-MM-yyyy hh:mm:ss tt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Current Producer Count">
                                        <ItemTemplate>
                                             <asp:Label ID="lblTotal_Pcount" runat="server" Text='<%# Eval("Total_Pcount") %>'></asp:Label>
                                        </ItemTemplate>
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

                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Submit") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
    </script>

</asp:Content>

