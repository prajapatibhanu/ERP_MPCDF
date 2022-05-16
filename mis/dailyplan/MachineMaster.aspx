<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MachineMaster.aspx.cs" Inherits="mis_dailyplan_MachineMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSave_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="save" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Machine Master</h3>
                </div>
                <asp:Label ID="lblMsg" Text="" runat="server"></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Machine Name<span style="color: red">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvMachineName" runat="server" Display="Dynamic" ControlToValidate="txtMachineName" Text="<i class='fa fa-exclamation-circle' title='Enter Machine Name!'></i>" ErrorMessage="Enter Machine Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtMachineName" CssClass="form-control" runat="server" Text="" placeholder="Enter Machine Name"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <fieldset>
                        <legend>Add Heads</legend>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Head Name<span style="color: red">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvHeadName" runat="server" Display="Dynamic" ControlToValidate="txtHeadName" Text="<i class='fa fa-exclamation-circle' title='Enter Head Name!'></i>" ErrorMessage="Enter Head Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:TextBox ID="txtHeadName" autocomplete="off" MaxLength="50" runat="server" CssClass="form-control" Text="" placeholder="Enter Head Name"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" Style="margin-top: 20px;" ValidationGroup="Add" Text="Add" OnClick="btnAdd_Click" />
                                </div>
                            </div>
                            <div class="col-md-12">
                                <asp:GridView ID="gvHeads" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="gvHeads_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Head Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHeadName" autocomplete="off" Maxlength="50" runat="server" Text='<%# Eval("HeadName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteRow" CommandArgument='<%# Eval("RowNo") %>' OnClientClick="return confirm('Do you really want To delete Record?');"><i class="fa fa-trash"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </fieldset>
                    <div class="col-md-2">
                        <div class="form-group">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" ValidationGroup="save" Text="Save" OnClick="btnSave_Click" OnClientClick="return ValidatePage();" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Reports</h3>
                </div>
                <asp:Label ID="lblReportMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <asp:GridView ID="gvMachineDetail" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" OnRowCommand="gvMachineDetail_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="S No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                    <asp:Label ID="lblMachine_ID" CssClass="hidden" runat="server" Text='<%# Eval("Machine_ID") %>'></asp:Label>
                                    <asp:Label ID="lblHead_ID" CssClass="hidden" runat="server" Text='<%# Eval("Head_ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Machine Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblMachineName" runat="server" Text='<%# Eval("Machine_Name") %>'></asp:Label>
                                     <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfv_MachineName" runat="server" Display="Dynamic" ControlToValidate="txt_MachineName" Text="<i class='fa fa-exclamation-circle' title='Enter Head Name!'></i>" ErrorMessage="Enter Head Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Update"></asp:RequiredFieldValidator>
                                    </span>
                                     <asp:TextBox ID="txt_MachineName" runat="server" Visible="false" Text='<%# Eval("Machine_Name") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Head Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblHeadName" runat="server" Text='<%# Eval("Head_Name") %>'></asp:Label>
                                      <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfv_HeadName" runat="server" Display="Dynamic" ControlToValidate="txt_HeadName" Text="<i class='fa fa-exclamation-circle' title='Enter Head Name!'></i>" ErrorMessage="Enter Head Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Update"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:TextBox ID="txt_HeadName" runat="server" Visible="false" Text='<%# Eval("Head_Name") %>'></asp:TextBox>
                                    </br>
                                      <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvAddHeadName" runat="server" Display="Dynamic" ControlToValidate="txtAddHeadName" Text="<i class='fa fa-exclamation-circle' title='Enter Head Name!'></i>" ErrorMessage="Enter Head Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Update"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:TextBox ID="txtAddHeadName" runat="server" Visible="false" ></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server"  CommandName="EditRecord"><i class="fa fa-edit"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lnkAdd" runat="server"  CommandName="AddRecord" CssClass="label label-default">Add New Head</asp:LinkButton>
                                    <asp:LinkButton ID="lnkSave" runat="server" Visible="false" CssClass="label label-default" ValidationGroup="Update" OnClientClick="return confirm('do you really want to Save?')"  CommandName="SaveRecord">Save</asp:LinkButton>
                                    <asp:LinkButton ID="lnkUpdate" runat="server" Visible="false" ValidationGroup="Update" CssClass="label label-default" OnClientClick="return confirm('do you really want to update?')" CommandName="UpdateRecord"  Text="Update"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="View Heads">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkView" runat="server" CssClass="label label-info" CommandName="ViewRecord" CommandArgument='<%# Eval("Machine_ID").ToString() %>' Text="View"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <div class="modal" id="ViewHeadDetail">
                <div style="display: table; height: 100%; width: 100%;">
                    <div class="modal-dialog modal-sm">
                        <div class="modal-content">
                            <div class="modal-header"> <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Machine Head  Details</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row" style="height: 350px; overflow: scroll;">
                                    <asp:Label ID="Label12" runat="server" Text=""></asp:Label>
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="true" ShowFooter="true"
                                                EmptyDataText="No Record Found" class="table table-hover table-bordered pagination-ys"
                                                AutoGenerateColumns="False" AllowPaging="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S No." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- <asp:TemplateField HeaderText="Machine Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMachineName" Text='<%# Eval("Machine_Name").ToString()%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderText="Head Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHeadName" Text='<%# Eval("Head_Name").ToString()%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                                   
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                </div>
                            </div>

                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('save');
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
        function ViewHeadDetailModal() {
            $("#ViewHeadDetail").modal('show');

        }
    </script>
</asp:Content>

