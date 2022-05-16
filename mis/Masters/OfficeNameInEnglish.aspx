<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="OfficeNameInEnglish.aspx.cs" Inherits="mis_Masters_OfficeNameInEnglish" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
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
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title"></h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server">
                        </asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>FILTER</legend>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Milk Collection Unit<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ValidationGroup="a"
                                                InitialValue="0" ErrorMessage="Select Office Type" Text="<i class='fa fa-exclamation-circle' title='Select Office Type !'></i>"
                                                ControlToValidate="ddlOfficeType" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlOfficeType" AutoPostBack="true" Width="100%" runat="server" OnSelectedIndexChanged="ddlOfficeType_SelectedIndexChanged" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Name<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a"
                                                InitialValue="0" ErrorMessage="Select Name" Text="<i class='fa fa-exclamation-circle' title='Select Name !'></i>"
                                                ControlToValidate="ddlOfficeName" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlOfficeName" runat="server" CssClass="form-control select2" ClientIDMode="Static" OnSelectedIndexChanged="ddlOfficeName_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0  ">Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Name(In English)<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                ErrorMessage="Enter Name(In English)" Text="<i class='fa fa-exclamation-circle' title='Enter Name(In English) !'></i>"
                                                ControlToValidate="txtOffice_Name_E" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:TextBox ID="txtOffice_Name_E" runat="server" autocomplete="off" CssClass="form-control" ClientIDMode="Static">                                          
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Society Code</label>
                                       <%-- <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                ErrorMessage="Enter Society Code" Text="<i class='fa fa-exclamation-circle' title='Enter Society Code !'></i>"
                                                ControlToValidate="txtSocietyCode" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>--%>
                                        <asp:TextBox ID="txtSocietyCode" runat="server" CssClass="form-control" autocomplete="off" ClientIDMode="Static">                                          
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Billing Code</label>
                                      <%--  <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                ErrorMessage="Enter Billing Code" Text="<i class='fa fa-exclamation-circle' title='Enter Billing Code !'></i>"
                                                ControlToValidate="txtBillingCode" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>--%>
                                        <asp:TextBox ID="txtBillingCode" runat="server" CssClass="form-control" autocomplete="off" ClientIDMode="Static">                                          
                                        </asp:TextBox>
                                    </div>
                                </div>
                                
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" runat="server" OnClientClick="return ValidatePage();" CssClass="btn btn-primary" ValidationGroup="a" style="margin-top:20px;" Text="Update" OnClick="btnSave_Click"/>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
    <script>
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
               
            }
        }

    </script>
</asp:Content>

