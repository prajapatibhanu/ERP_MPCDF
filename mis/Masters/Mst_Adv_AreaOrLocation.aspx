<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Mst_Adv_AreaOrLocation.aspx.cs" Inherits="mis_Masters_Mst_Adv_AreaOrLocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
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
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <!-- Default box -->
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Area</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Route<span style="color: red;">*</span></label>
                                          <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                 ErrorMessage="Select Route" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Route !'></i>"
                                                ControlToValidate="ddlRoute" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlRoute" runat="server" CssClass="form-control select2">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Area Name<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                 ErrorMessage="Enter Area Name" Text="<i class='fa fa-exclamation-circle' title='Enter Area Name !'></i>"
                                                ControlToValidate="txtAreaName" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtAreaName" runat="server" class="form-control" autocomplete="off" MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSubmit" ValidationGroup="a" OnClientClick="return ValidatePage();" CssClass="btn btn-primary" Style="margin-top: 21px;" runat="server" Text="Save" />
                                        <asp:Button ID="btnClear" CssClass="btn btn-default" OnClick="btnClear_Click" Style="margin-top: 21px;" runat="server" Text="Clear" />
                                        <asp:HiddenField ID="hfiid" runat="server" />
                                    </div>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Area Details</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Area <span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlAreaSearch" runat="server" CssClass="form-control select2">
                                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSearch" CssClass="btn btn-primary" OnClick="btnSearch_Click" Style="margin-top: 21px;" runat="server" Text="Search" />
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                                          <HeaderTemplate>
                                                <table class="table table-bordered">
                                                    <tr>
                                                        <th>S.No
                                                        </th>
                                                        <th>Area Name
                                                        </th>
                                                        <th>Route
                                                        </th>
                                                        <th>Status
                                                        </th>
                                                        <th>Action
                                                        </th>
                                                    </tr>
                                                     </HeaderTemplate>
                                                      <ItemTemplate>
                                                    <tr runat="server" id="row">
                                                        <td><asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" /></td>
                                                          <td>
                                                            <asp:Label ID="lblAreaName" runat="server" Text='<%# Eval("Adv_AreaName") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblRouteName" runat="server" Text='<%# Eval("RName") %>'></asp:Label>
                                                            <asp:Label ID="lblIsActive" Visible="false" runat="server" Text='<%# Eval("IsActive") %>'></asp:Label>
                                                             <asp:Label ID="lblRouteId" Visible="false" runat="server" Text='<%# Eval("RouteId") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("RStatus") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                          <asp:LinkButton ID="lnkUpdate" CommandName="Update" runat="server" CssClass="button button-mini button-green" 
                                                              CommandArgument='<%#Eval("Adv_AreaId") %>'><i class="btn btn-info fa fa-edit"></i></asp:LinkButton>
                                                             <asp:LinkButton ID="LinkButton1" OnClientClick="return confirm('Are you sure to Delete?')"
                                                                  CommandName="DeleteRecord" runat="server" CssClass="btn button-mini button-green" 
                                                              CommandArgument='<%#Eval("Adv_AreaId") %>'><i class='<%# (Eval("IsActive").ToString()=="True" ? "btn btn-success fa fa-toggle-on" : "btn btn-danger fa fa-toggle-off") %>'></i></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                             </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                         
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
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

