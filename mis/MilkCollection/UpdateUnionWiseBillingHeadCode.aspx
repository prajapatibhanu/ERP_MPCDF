<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="UpdateUnionWiseBillingHeadCode.aspx.cs" Inherits="mis_MilkCollection_UpdateUnionWiseBillingHeadCode" %>

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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSave_Click"  Style="margin-top: 20px; width: 50px;" />
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
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Update Union Wise Billing Head Code</h3>
                        </div>
                        <asp:Label id="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                 <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Billing Head Type<span style="color: red;"> *</span></label>
                                         <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvddlItemBillingHead_Type" runat="server" Display="Dynamic" ControlToValidate="ddlItemBillingHead_Type" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Billing Head Type!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                        <asp:DropDownList runat="server" CssClass="form-control select2" ID="ddlItemBillingHead_Type" OnSelectedIndexChanged="ddlItemBillingHead_Type_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="ADDITION">ADDITIONS</asp:ListItem>
                                            <asp:ListItem Value="DEDUCTION">DEDUCTIONS</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Billing Head Name<span style="color: red;"> *</span></label>
                                         <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvddlBillingHeadName" runat="server" Display="Dynamic" ControlToValidate="ddlBillingHeadName" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Billing Head Name!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                        <asp:DropDownList runat="server" CssClass="form-control select2" ID="ddlBillingHeadName" OnSelectedIndexChanged="ddlBillingHeadName_SelectedIndexChanged" AutoPostBack="true">
                                            
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Billing Head Alias Name<span style="color: red;"> *</span></label>
                                         <span class="pull-right">
                                               <asp:RequiredFieldValidator ID="rfvtxtBillingHeadNameAlias" runat="server" Display="Dynamic" ControlToValidate="txtBillingHeadNameAlias" Text="<i class='fa fa-exclamation-circle' title='Enter Billing Head Alias Name!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtBillingHeadNameAlias" placeholder="Enter Head Code" autocomplete="off" MaxLength="150" onkeypress="return ValidateNum(event)"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Billing Head Code<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                               <asp:RequiredFieldValidator ID="rfvtxtItemBillingHead_Code" runat="server" Display="Dynamic" ControlToValidate="txtItemBillingHead_Code" Text="<i class='fa fa-exclamation-circle' title='Enter Billing Head Alias Name!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtItemBillingHead_Code" placeholder="Enter Head Code" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                
                                
                            </div>
                            <div class="row">
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:Button runat="server" ValidationGroup="Save" CssClass="btn btn-block btn-primary" ID="btnSave" Text="Save" OnClick="btnSave_Click" OnClientClick="return ValidatePage()" />
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <a href="UpdateUnionWiseBillingHeadCode.aspx" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gvDetails" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:BoundField HeaderText="Head Type" DataField="ItemBillingHead_Type" />
                                            <asp:BoundField HeaderText="Head Name" DataField="ItemBillingHead_Name" />
                                            <asp:BoundField HeaderText=" Head Alias Name" DataField="ItemBillingHeadAlias_Name" />
                                           
                                            <asp:BoundField HeaderText="Head Code" DataField="ItemBillingHead_Code" />
                                        </Columns>
                                    </asp:GridView>
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
    <script>
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

