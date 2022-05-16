<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SetDA_OfficeWise.aspx.cs" Inherits="mis_Payroll_SetDA_OfficeWise" %>

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
        <!-- Main content -->
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">DA Master </h3>

                </div>
                <div class="box-body">
                    <div class="row">
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="row">

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlOfficeName" runat="server" class="form-control" Enabled="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>DA %</label><span style="color: red;">*</span>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a" ControlToValidate="txtDA"
                                        ErrorMessage="Enter DA %" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DA % !'></i>"
                                        Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtDA" runat="server" autocomplete="off" CssClass="form-control" placeholder="Enter DA Percentage" onkeypress="return validateNum(event)" MaxLength="2"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Applicable Date<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a" ControlToValidate="txtApplicableDate"
                                        ErrorMessage="Enter Applicable Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Applicable Date !'></i>"
                                        Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtApplicableDate"
                                        ErrorMessage="Invalid From Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                        ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                </span>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtApplicableDate" MaxLength="10" placeholder="Enter Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2" style="margin-top: 20px;">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                                <asp:Button runat="server" CssClass="btn btn-default" ID="btnClear" OnClick="btnClear_Click" Text="Clear" />
                                <asp:HiddenField ID="hfrowid" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="table-responsive">

                            <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand" OnItemDataBound="Repeater1_ItemDataBound">
                                <HeaderTemplate>
                                    <table class="datatable table table-striped table-bordered">
                                        <tr>
                                            <th>S.No
                                            </th>
                                            <th>DA %
                                            </th>
                                            <th>Applicable Date
                                            </th>
                                            <th>Action
                                            </th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr id="trID" runat="server">
                                        <td>
                                            <%# Container.ItemIndex + 1 %>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblParollDA_Per" runat="server" Text='<%# Eval("ParollDA_Per") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEffectiveDate" runat="server" Text='<%# Eval("EffectiveDate") %>' />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("ParollDA_Id") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("ParollDA_Id") %>' CommandName="RecordView" runat="server" ToolTip="View" Style="color: red;"><i class="fa fa-eye"></i></asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Label ID="lblEmptyData" Text="No Data Found" runat="server" Visible="false">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>

                        </div>
                    </div>
                </div>
                <div class="modal" id="ItemDetailsModalPreData" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">×</span></button>
                                </div>
                                <div class="modal-body">
                                    <div id="div1" runat="server">
                                        <asp:Label ID="lbldModalMsgPreDemand" runat="server" Text=""></asp:Label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div style="height: 450px; overflow: scroll;">
                                                    <div class="col-md-12">
                                                        <div class="table-responsive">
                                                             <asp:Repeater ID="Repeater2" runat="server">
                                <HeaderTemplate>
                                    <table class="datatable table table-striped table-bordered">
                                        <tr>
                                            <th>S.No
                                            </th>
                                            <th>DA %
                                            </th>
                                            <th>Applicable Date
                                            </th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr id="trID" runat="server">
                                        <td>
                                            <%# Container.ItemIndex + 1 %>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblParollDA_Per1" runat="server" Text='<%# Eval("ParollDA_Per") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEffectiveDate1" runat="server" Text='<%# Eval("EffectiveDate") %>' />
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
                                <div class="modal-footer">
                                
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

        function myItemDetailsModal() {
            $("#ItemDetailsModalPreData").modal('show');

        }

        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {
                debugger;
                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                else if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
        }
    }
    </script>
</asp:Content>
