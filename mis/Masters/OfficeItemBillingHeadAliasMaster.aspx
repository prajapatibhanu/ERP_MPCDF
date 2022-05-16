<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="OfficeItemBillingHeadAliasMaster.aspx.cs" Inherits="mis_Masters_OfficeItemBillingHeadAliasMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">DCS/BMC Billing Head Master</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Billing Head Type<span style="color: red;"> *</span></label>
                                        <asp:DropDownList runat="server" CssClass="form-control select2" ID="ddlItemBillingHead_Type" OnSelectedIndexChanged="ddlItemBillingHead_Type_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="ADDITION">ADDITIONS</asp:ListItem>
                                            <asp:ListItem Value="DEDUCTION">DEDUCTIONS</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Billing Head ID<span style="color: red;"> *</span></label>
                                        <asp:DropDownList runat="server" CssClass="form-control select2" ID="ddlHeadNameID"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Billing Head Alias Name<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtItemBillingHeadAliasName"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-primary" ID="btnSave" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateform()" />
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <a href="OfficeItemBillingHeadAliasMaster.aspx" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <asp:GridView ID="GridView1" PageSize="50" runat="server" class="table table-hover table-bordered table-striped pagination-ys " ShowHeaderWhenEmpty="true"
                                    AutoGenerateColumns="False" DataKeyNames="ItemBillingHeadAlias_ID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("ItemBillingHeadAlias_ID").ToString()%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ItemBillingHead_Type" HeaderText="Billing Head Type" />
                                        <asp:BoundField DataField="ItemBillingHead_Name" HeaderText="Billing Head Name" />
                                        <asp:BoundField DataField="ItemBillingHeadAlias_Name" HeaderText="Billing Head Alias Name" />
                                        <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Select" runat="server" CssClass="label label-default" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="30" HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" OnCheckedChanged="chkSelect_CheckedChanged" runat="server" ToolTip='<%# Eval("ItemBillingHeadAlias_ID").ToString()%>' Checked='<%# Eval("IsActive").ToString()=="1" ? true : false %>' AutoPostBack="true" />
                                            </ItemTemplate>
                                            <ItemStyle Width="30px"></ItemStyle>
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
    <script type="text/javascript">
        function tbx_fnAlphaOnly(e, cntrl) {
            if (!e) e = window.event; if (e.charCode) {
                if (e.charCode < 65 || (e.charCode > 90 && e.charCode < 97) || e.charCode > 122)
                { if (e.charCode != 95 && e.charCode != 32) { if (e.preventDefault) { e.preventDefault(); } } }
            } else if (e.keyCode) {
                if (e.keyCode < 65 || (e.keyCode > 90 && e.keyCode < 97) || e.keyCode > 122)
                { if (e.keyCode != 95 && e.keyCode != 32) { try { e.keyCode = 0; } catch (e) { } } }
            }
        }
        function validateform() {
            var msg = "";
            if (document.getElementById('<%=ddlItemBillingHead_Type.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Billing Head Type. \n";
            }
            if (document.getElementById('<%=ddlHeadNameID.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Billing Head. \n";
            }
            if (document.getElementById('<%=txtItemBillingHeadAliasName.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Billing Head Name. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    if (confirm("Do you really want to Save Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Edit") {
                    if (confirm("Do you really want to Edit Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }

                }
            }
        }
    </script>
</asp:Content>

