<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="AdminSectionMaster.aspx.cs" Inherits="mis_HR_AdminSectionMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Section Master</h3>
                </div>
                <asp:label id="lblMsg" runat="server" text=""></asp:label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Name</label>
                                <asp:dropdownlist id="ddlOfficeName" runat="server" cssclass="form-control"></asp:dropdownlist>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Section  Name<span style="color: red;">*</span></label>
                                <asp:textbox id="txtSection_Name" runat="server" placeholder="Enter Section Name..." class="form-control" maxlength="100" onkeypress="return validatename(event);"></asp:textbox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Section No<span style="color: red;">*</span></label>
                                <asp:textbox id="txtSection_No" runat="server" placeholder="Enter Section No..." class="form-control" maxlength="5" onkeypress="return validateNum(event);"></asp:textbox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Section Order<span style="color: red;">*</span></label>
                                <asp:textbox id="txtOrderNo" runat="server" placeholder="Enter Section Order..." class="form-control" maxlength="5" onkeypress="return validateNum(event);"></asp:textbox>
                            </div>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:button id="btnSave" cssclass="btn btn-block btn-success" style="margin-top: 23px;" runat="server" text="Save" onclick="btnSave_Click" onclientclick="return validateform();" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:gridview id="GridView1" runat="server" class="table table-hover table-bordered table-striped pagination-ys" autogeneratecolumns="False" datakeynames="Section_ID" onpageindexchanging="GridView1_PageIndexChanging" onrowdeleting="GridView1_RowDeleting" onselectedindexchanged="GridView1_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Office_Name" HeaderText="Office" />
                                    <asp:BoundField DataField="Section_Name" HeaderText="Section Name" />                                    
                                    <asp:BoundField DataField="Section_No" HeaderText="Section No" />                                    
                                    <asp:BoundField DataField="OrderNo" HeaderText="Section Order No" />

                                    <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Select" runat="server" CssClass="label label-default" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
                                            <asp:LinkButton ID="Delete" runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Both the Designation and mapped class will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:gridview>
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
            if (document.getElementById('<%=txtSection_Name.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Section Name. \n";
            }
<%--            if (document.getElementById('<%=ddlOfficeName.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Office. \n";
            }--%>
            if (document.getElementById('<%=txtOrderNo.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Section Order. \n";
            }
            if (document.getElementById('<%=txtSection_No.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Section No. \n";
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
