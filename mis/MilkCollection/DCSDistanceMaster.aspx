<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DCSDistanceMaster.aspx.cs" Inherits="mis_MilkCollection_DCSDistanceMaster" %>

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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" Style="margin-top: 20px; width: 50px;" OnClick="btnSave_Click"/>
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">DCS Distance Master</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>Filter</legend>
                                <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>DCS<span style="color: red">*</span></label>
                                         <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvDCS" ValidationGroup="Save"
                                                InitialValue="0" ErrorMessage="Select DCS" Text="<i class='fa fa-exclamation-circle' title='Select DCS !'></i>"
                                                ControlToValidate="ddlDCS" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlDCS" runat="server" CssClass="form-control select2">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                    <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Distance(In Km from one side)<span style="color: red">*</span></label>
                                         <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvDistance" ValidationGroup="Save"
                                                 ErrorMessage="Enter Distance" Text="<i class='fa fa-exclamation-circle' title='Enter Distance !'></i>"
                                                ControlToValidate="txtDistance" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:TextBox ID="txtDistance" autocomplete="off" MaxLength="20" onkeypress="return validateDec(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button ID="btnSave" runat="server" style="margin-top:22px;" Text="Save" CssClass="btn btn-success" ValidationGroup="Save" OnClientClick="return ValidatePage();" OnClick="btnSave_Click"/>
                                        </div>
                                    </div>
                            </div>
                            </fieldset>                           
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>DCS Distance Details</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvReports" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnRowCommand="gvReports_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText ="S.No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText ="DCS">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDCS" runat="server" Text='<%# Eval("DCS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText ="Distance(In Km from one side)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDistance" runat="server" Text='<%# Eval("Distance") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText ="Edit">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditRecord" CausesValidation="false" CommandArgument='<%# Eval("DCSDistance_ID") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
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
     <script type="text/javascript">
         function ValidatePage() {

             if (typeof (Page_ClientValidate) == 'function') {
                 Page_ClientValidate('Save');
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

    </script>
</asp:Content>

