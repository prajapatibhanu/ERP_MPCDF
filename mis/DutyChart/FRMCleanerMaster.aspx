<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FRMCleanerMaster.aspx.cs" Inherits="mis_DutyChart_FRMCleanerMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper" style="min-height: 327px;">
        <section class="content-header">
            <h1>Cleaner Master</h1>
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </section>
        <section class="content">
            <div class="box box-primary">
                <div class="box-body">
                    <div class="col-md-6">
                        <fieldset>
                            <legend>Cleaner Master</legend>
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <span class="text-bold">Cleaner Name<span style="color: red;"> *</span></span>
                                            <asp:TextBox ID="txtCleanerName" runat="server" CssClass="form-control" placeholder="Cleaner Name"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <span class="text-bold">Mobile No.<span style="color: red;"> *</span></span>
                                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" MaxLength="10" placeholder="Mobile No."></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" Style="margin-top: 17px;" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="col-md-6">
                        <fieldset>
                            <legend>Cleaner List</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive ">
                                        <asp:GridView ID="gvTesterDetail" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="gvTesterDetail_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Cleaner_Name" HeaderText="Cleaner Name" />
                                                <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblEdit" runat="server" CommandName="EditRecord" CommandArgument='<%# Eval("Cleaner_ID").ToString() %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lblDelete" runat="server" CommandName="DeleteRecord" CommandArgument='<%# Eval("Cleaner_ID").ToString() %>'><i class="fa fa-trash"></i></asp:LinkButton>
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
        </section>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        function validateform() {
            var msg = "";
            if (document.getElementById('<%=txtCleanerName.ClientID%>').value == "") {
                msg = msg + "Enter Cleaner Name. \n";
            }
            if (document.getElementById('<%=txtMobileNo.ClientID%>').value == "") {
                msg = msg + "Enter Mobile No. \n";
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
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
                    if (confirm("Do you really want to Update Details ?")) {
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

