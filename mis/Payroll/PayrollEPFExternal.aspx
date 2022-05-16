<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollEPFExternal.aspx.cs" Inherits="mis_Payroll_PayrollEPFExternal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <style>
        .PaddingLeftRight {
            padding-left: 5px;
            padding-right: 5px;
        }

        .PaddingRight {
            padding-right: 5px;
        }

        .PaddingLeft {
            padding-left: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Add External Employee For EPF Report</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Employee Name</label>
                                <asp:TextBox ID="txtEmp_Name" runat="server" CssClass="form-control" placeholder="Enter Employee Name" MaxLength="100" onkeypress="return validatename(event);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>UAN No</label>
                                <asp:TextBox ID="txtUAN" runat="server" CssClass="form-control" placeholder="Enter UAN No" MaxLength="50" onkeypress="return validateNum(event)" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>EPF No</label>
                                <asp:TextBox ID="txtEPF" runat="server" CssClass="form-control" placeholder="Enter EPF No" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblDOB" runat="server" Text="DOB"></asp:Label><span style="color: red;">*</span>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtDOB" runat="server" placeholder="DOB" class="form-control DateAdd" data-date-end-date="-5000d" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Status<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlIsEPF" runat="server" class="form-control">
                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="0">Close</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Eligible For EPS?<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlIsEPS" runat="server" class="form-control">
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                    <asp:ListItem Value="0">No</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Emp No</label>
                                <asp:TextBox ID="txtEMP" runat="server" CssClass="form-control" placeholder="Emp No" MaxLength="50" onkeypress="return validateNum(event)" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Section No</label>
                                <asp:TextBox ID="txtSection" runat="server" CssClass="form-control" placeholder="Section No" MaxLength="50" onkeypress="return validateNum(event)" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="lblOtherInfo" runat="server" Text="Emplyee Other Information"></asp:Label>
                                <asp:TextBox ID="txtOtherInfo" runat="server" CssClass="form-control" placeholder="Other Information"></asp:TextBox>
                            </div>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="GridView1" runat="server" class="table table-hover table-bordered table-striped pagination-ys" AutoGenerateColumns="False" DataKeyNames="Emp_ID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Emp_Name" HeaderText="Officer/Employee Name" />
                                    <asp:BoundField DataField="EPF" HeaderText="EPF" />
                                    <asp:BoundField DataField="UAN" HeaderText="UAN" />
                                    <asp:BoundField DataField="EmpDOB" HeaderText="DOB" />
                                    <asp:BoundField DataField="OtherInfo" HeaderText="Other Information" />
                                    <asp:BoundField DataField="EmpStatus" HeaderText="Status" HtmlEncode="false" />
                                    <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Select" runat="server" CssClass="label label-default" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
                                        </ItemTemplate>
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

            if (document.getElementById('<%=txtDOB.ClientID%>').value.trim() == "") {
                msg += "Select DOB. \n";
            }
            if (document.getElementById('<%=txtUAN.ClientID%>').value.trim() == "") {
                msg += "Enter UAN. \n";
            }
            if (document.getElementById('<%=txtEPF.ClientID%>').value.trim() == "") {
                msg += "Enter EPF. \n";
            }
            if (document.getElementById('<%=txtOtherInfo.ClientID%>').value.trim() == "") {
                msg += "Enter Some Information About Employee. \n";
            }
            if (document.getElementById('<%=txtEmp_Name.ClientID%>').value.trim() == "") {
                msg += "Enter Employee Name. \n";
            }


          <%--  if (document.getElementById('<%=ddlLoan_IsActive.ClientID%>').selectedIndex == 0) {
                msg += "Select Status. \n";
            }--%>
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    if (confirm("Do you really want to Save Details, ?")) {
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

