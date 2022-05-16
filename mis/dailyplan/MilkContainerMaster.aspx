<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MilkContainerMaster.aspx.cs" Inherits="mis_dailyplan_MilkContainerMaster" %>

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
                    <h3 class="box-title">Milk Container Master</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <fieldset>
                        <legend>Container Detail</legend>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Milk Container Type<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlMCType" runat="server"  class="form-control select2">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem>PMT</asp:ListItem>
                                    <asp:ListItem>RMT</asp:ListItem>
                                    <asp:ListItem>Silo</asp:ListItem>
                                    <asp:ListItem>Tank</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                          <div class="col-md-2">
                            <div class="form-group">
                                <label>Milk Container Name</label>
                                <asp:TextBox ID="txtMCName" runat="server" MaxLength="50" placeholder="Enter Milk Container Name..." autocomplete="off" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Milk Container Capacity<span style="color: red;">*</span></label>
                                 <asp:TextBox ID="txtMCCapacity" runat="server" placeholder="Enter Milk Container Capacity..." MaxLength="30" autocomplete="off" class="form-control" onkeypress="return validateDec(this,event);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Unit<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlUnit_id" runat="server"  class="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Button ID="btnSave" CssClass="btn btn-block btn-success" Style="margin-top: 20px;" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
      
                    </div>
                        </hr>
                        </fieldset>
                    
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="GridView1" PageSize="50" runat="server" class="table table-hover table-bordered table-striped pagination-ys" AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="I_MCID" OnPageIndexChanging="GridView1_PageIndexChanging"  OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="V_MCType" HeaderText="Milk Container Type" />
                                     <asp:BoundField DataField="V_MCName" HeaderText="Milk Container Name" />
                                    <asp:BoundField DataField="V_MCCapacity" HeaderText="Milk Container Capacity" />
                                    <asp:BoundField DataField="UnitName" HeaderText="Milk Container Capacity" />
                                    <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Select" runat="server" CssClass="label label-default" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
                                           
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  <asp:TemplateField ItemStyle-Width="30" HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" OnCheckedChanged="chkSelect_CheckedChanged" runat="server" ToolTip='<%# Eval("I_MCID").ToString()%>' Checked='<%# Eval("B_MCStatus").ToString()=="True" ? true : false %>' AutoPostBack="true" />
                                        </ItemTemplate>
                                        <ItemStyle Width="30px"></ItemStyle>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
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
            if (document.getElementById('<%=ddlMCType.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Milk Container Type \n";
            }
            if (document.getElementById('<%=txtMCName.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Milk Container Name. \n";
            }
            if (document.getElementById('<%=txtMCCapacity.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Milk Container Capacity. \n";
            }
            if (document.getElementById('<%=ddlUnit_id.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Unit. \n";
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

