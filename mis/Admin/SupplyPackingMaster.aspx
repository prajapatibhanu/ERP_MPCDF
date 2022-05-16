<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SupplyPackingMaster.aspx.cs" Inherits="mis_Admin_SupplyPackingMaster" %>

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
                        <div class="col-md-12">
                            <div class="box box-Manish">
                                <div class="box-header">
                                    <h3 class="box-title">Supply Packing Master</h3>
                                </div>
                                <!-- /.box-header -->
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label>Item Type<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                        ErrorMessage="Select Department Name" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Department Name !'></i>"
                                                        ControlToValidate="ddlItemType" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlItemType" CssClass="form-control select2" runat="server">
                                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Milk"></asp:ListItem>
                                                     <asp:ListItem Value="2" Text="Product"></asp:ListItem>
                                                     <asp:ListItem Value="3" Text="Cow Feed"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                         <div class="col-sm-3">
                                            <div class="form-group">
                                                <label>Item Name<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                        ErrorMessage="Select Designation Name" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Designation Name !'></i>"
                                                        ControlToValidate="ddlItemName" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlItemName" CssClass="form-control select2" runat="server">
                                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                     <asp:ListItem Value="1" Text="FCM"></asp:ListItem>
                                                     <asp:ListItem Value="2" Text="STM"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label>Item Specification<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                        ErrorMessage="Select Designation Name" Text="<i class='fa fa-exclamation-circle' title='Select Designation Name !'></i>"
                                                        ControlToValidate="txtItemSpecifcation" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox ID="txtItemSpecifcation" CssClass="form-control" runat="server" placeholder="Enter Item Specification"></asp:TextBox>
                                            </div>
                                        </div>
                                         <div class="col-sm-3">
                                            <div class="form-group">
                                                <label>Packaging Type<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a"
                                                        ErrorMessage="Select Department Name" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Department Name !'></i>"
                                                        ControlToValidate="ddlItemType" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlPackagingTpye" CssClass="form-control select2" runat="server">
                                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Cup"></asp:ListItem>
                                                     <asp:ListItem Value="2" Text="Bottle"></asp:ListItem>
                                                     <asp:ListItem Value="3" Text="Packet"></asp:ListItem>
                                                     <asp:ListItem Value="4" Text="Tincan"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label>Quantity<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                        ErrorMessage="Select Designation Name" Text="<i class='fa fa-exclamation-circle' title='Select Designation Name !'></i>"
                                                        ControlToValidate="txtQuantity" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox ID="txtQuantity" CssClass="form-control" placeholder="Enter Quantity" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                         <div class="col-sm-3">
                                            <div class="form-group">
                                                <label>Unit<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a"
                                                        ErrorMessage="Select Department Name" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Department Name !'></i>"
                                                        ControlToValidate="ddlUnit" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlUnit" CssClass="form-control select2" runat="server">
                                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="KG"></asp:ListItem>
                                                     <asp:ListItem Value="2" Text="ML"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <hr />
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-block btn-primary" ValidationGroup="a" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Button ID="btnClear" OnClick="btnClear_Click" runat="server" Text="Clear" CssClass="btn btn-block btn-default" />
                                            </div>
                                        </div>
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

