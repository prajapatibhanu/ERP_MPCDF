<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Head_SubHeadMapping.aspx.cs" Inherits="mis_EngneeringDepartment_Head_SubHeadMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
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
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Head & Sub Head Mapping</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Head Name<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a" InitialValue="0"
                                                ErrorMessage="Select Head Name" Text="<i class='fa fa-exclamation-circle' title='Select Head Name !'></i>"
                                                ControlToValidate="ddlHeadName" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlHeadName" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <%--<asp:ListItem>Electrical</asp:ListItem>
                                            <asp:ListItem>Maintenance & Rubber Goods</asp:ListItem>
                                            <asp:ListItem>Homogenizer / Refrigeration</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Sub Head Name<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a" InitialValue="0"
                                                ErrorMessage="Select Sub Head Name" Text="<i class='fa fa-exclamation-circle' title='Select Sub Head Name !'></i>"
                                                ControlToValidate="ddlSubHeadName" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <br/>
                                        <asp:ListBox ID="ddlSubHeadName" CssClass="form-control" SelectionMode="Multiple" runat="server">
                                            <%--<asp:ListItem>Electricity Bill</asp:ListItem>--%>
                                        </asp:ListBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSubmit" runat="server" Text="SUBMIT" Style="margin-top: 20px;" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btnSubmit_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Engineering Head & Sub Head Mapping Details</h3>

                        </div>
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        <asp:DropDownList ID="ddlSearchRouteHead" Visible="false" runat="server" CssClass="form-control select2">
                                        </asp:DropDownList>
                        <div class="box-body">
                            <div class="table table-responsive">
                                <asp:GridView ID="GridView1" OnRowCommand="GridView1_RowCommand" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                            EmptyDataText="No Record Found." DataKeyNames="ENGHeadandSubHeadId">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" Head Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRouteHeadName" runat="server" Text='<%# Eval("ENGHeadName") %>'></asp:Label>
                                                        <asp:Label ID="lblIsActive" Visible="false" runat="server" Text='<%# Eval("IsActive") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sub  Head Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubRouteHeadName" runat="server" Text='<%# Eval("ENGSubHeadName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("ENGHeadandSubHeadId") %>' Visible='<%# Eval("ENGSectionEntrycount").ToString() == "0"? true : false  %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../js/bootstrap-multiselect.js"></script>
    <script>
        $(function () {
            $('[id*=ddlSubHeadName]').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>
</asp:Content>

