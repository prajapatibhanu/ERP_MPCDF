<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FrmTankerRouteMapping.aspx.cs" Inherits="mis_DutyChart_FrmTankerRouteMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper" style="min-height: 327px;">
        <section class="content-header">
            <h1>Tanker Route Mapping</h1>
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </section>
        <section class="content">
            <div class="box box-primary">
                <div class="box-body">
                    <div class="col-md-12">
                        <fieldset>
                            <legend>Tanker Route Mapping</legend>
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <span class="text-bold">Tanker No</span>
                                            <asp:DropDownList ID="ddlTankerNo" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <span class="text-bold">Route No</span>
                                            <asp:DropDownList ID="ddlRouteNo" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <span class="text-bold">Driver Name</span>
                                            <asp:DropDownList ID="ddlDriver" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" Style="margin-top: 20px;" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <fieldset>
                                            <legend>Tanker Route Mapping List</legend>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="table table-responsive">
                                                        <asp:GridView ID="GvMappingDetail" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Record Found !!" OnRowCommand="GvMappingDetail_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S.No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSNo" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="V_VehicleNo" HeaderText="Tanker No" />
                                                                <asp:BoundField DataField="BMCTankerRootName" HeaderText="Route No" />
                                                                <asp:BoundField DataField="Driver_Name" HeaderText="Driver Name" />
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="RecordEdit" CommandArgument='<%# Eval("MappingID").ToString() %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                                        <asp:LinkButton ID="lblDelete" runat="server" CommandName="RecordDelete" CommandArgument='<%# Eval("MappingID").ToString() %>'><i class="fa fa-trash"></i></asp:LinkButton>
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
            if (document.getElementById('<%=ddlTankerNo.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Tanker No. \n";
            }
            if (document.getElementById('<%=ddlRouteNo.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Route No. \n";
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

