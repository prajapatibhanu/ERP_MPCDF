<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="AdminBlock.aspx.cs" Inherits="mis_Admin_AdminBlock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <script src="../../js/jquery.min.js"></script>
    <script src="../../js/datatables.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".datatable").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <div class="card-header">
                    <h3 class="card-title">Block Master Entry</h3>
                </div>
                <div class="box-body">
                    <div class="col-md-6">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Division Name<span style="color: red;"> *</span></label>
                                <asp:RequiredFieldValidator ID="rfvDivision" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlDivision_Name" InitialValue="0" ErrorMessage="Select Division" Text="<i class='fa fa-exclamation-circle' title='Please Select Division !'></i>"></asp:RequiredFieldValidator>
                                <asp:DropDownList runat="server" ID="ddlDivision_Name" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDivisionName_SelectedIndexChanged">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>District Name<span style="color: red;"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlDistrict_Name" CssClass="form-control" AutoPostBack="true">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Block Name<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtBlock_Name" ErrorMessage="Enter Block Name." Text="<i class='fa fa-exclamation-circle' title='Enter Block Name !'></i>"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="rev" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtBlock_Name"
                                        ValidationExpression="^[a-zA-z\s]+$"
                                        Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Only Alphabet Allow !'></i>" ErrorMessage="Only Alphabet Allow" />
                                </span>
                                <asp:TextBox ID="txtBlock_Name" runat="server" placeholder="Enter Block Name..." class="form-control" MaxLength="100" onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <asp:Button ID="btnSave" CssClass="btn btn-primary mt-2" ValidationGroup="a" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="container-fluid">
                            <div class="box box-primary">
                                <div class="card-header">
                                    <h3 class="card-title">Block Master List</h3>
                                </div>
                                <div class="card-body">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" class="table table-bordered datatable" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" DataKeyNames="Block_ID" OnPageIndexChanging="GridView1_PageIndexChanging" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Block_ID").ToString()%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Division_Name" HeaderText="Division Name" />
                                                <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                                                <asp:BoundField DataField="Block_Name" HeaderText="Block Name" />
                                                <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="Select" runat="server" CssClass="fa fa-edit" CausesValidation="False" CommandName="Select" Text=""></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <%-- <script type="text/javascript">
        function validateform() {
            var msg = "";
            if (document.getElementById('<%=ddlDivision_Name.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Division Name. \n";
            }
            if (document.getElementById('<%=ddlDistrict_Name.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select District Name. \n";
            }
            if (document.getElementById('<%=txtBlock_Name.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Block Name. \n";
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
    </script>--%>
</asp:Content>

