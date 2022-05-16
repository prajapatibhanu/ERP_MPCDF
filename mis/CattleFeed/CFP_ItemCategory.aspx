<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CFP_ItemCategory.aspx.cs" Inherits="mis_CattelFeed_CFP_ItemCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-6">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Item Category Master</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

                        <div class="box-body">
                            <fieldset>
                                <legend>Item Category Entry (वस्तु की श्रेणी प्रविष्ट)
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Category(श्रेणी)<span class="text-danger"> *</span></label>
                                            <asp:TextBox ID="txtItem_Category" runat="server" placeholder="Item Category..." class="form-control" MaxLength="100" onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                                            <small><span id="valtxtItem_Category" class="text-danger"></span></small>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" ID="btnSave" Text="Save" OnClientClick="return validateform();" OnClick="btnSave_Click" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">

                                            <asp:Button runat="server" CssClass="btn btn-block btn-default" ID="btnclear" Text="Clear" OnClick="btnclear_Click" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>

                    </div>
                </div>
                <div class="col-md-6">
                    <div class="box box-Manish">

                        <div class="box-body">
                            <fieldset>
                                <legend>Item Category Detail (प्रविष्टित श्रेणी की सूची)
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:HiddenField ID="hdnvalue" runat="server" Value="0" />
                                        <asp:GridView ID="grdCatlist" PageSize="20" runat="server" class="table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" AllowPaging="True" OnRowCommand="grdCatlist_RowCommand" OnPageIndexChanging="grdCatlist_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ItemCatName" HeaderText="Category(श्रेणी) " />
                                                <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LnkSelect" runat="server"  CausesValidation="False" CommandName="Change" CommandArgument='<%#Eval("ItemCatid") %>' Text="Edit"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LnkDelete" runat="server" CausesValidation="False" CommandName="inactive" CommandArgument='<%#Eval("ItemCatid") %>' Text="Delete" OnClientClick="return confirm('Item Category will be deleted. Are you sure want to continue?');" Style="color: red;"><i class="fa fa-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        function validateform() {
            var msg = "";
            $("#valtxtItem_Category").html("");
            if (document.getElementById('<%=txtItem_Category.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Item Category. \n";
                $("#valtxtItem_Category").html("Enter Item Category");
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
        function tbx_fnAlphaOnly(e, cntrl) { if (!e) e = window.event; if (e.charCode) { if (e.charCode < 65 || (e.charCode > 90 && e.charCode < 97) || e.charCode > 122) { if (e.charCode != 95 && e.charCode != 32) { if (e.preventDefault) { e.preventDefault(); } } } } else if (e.keyCode) { if (e.keyCode < 65 || (e.keyCode > 90 && e.keyCode < 97) || e.keyCode > 122) { if (e.keyCode != 95 && e.keyCode != 32) { try { e.keyCode = 0; } catch (e) { } } } } }
    </script>
    <script src="../js/ValidationJs.js"></script>
</asp:Content>

