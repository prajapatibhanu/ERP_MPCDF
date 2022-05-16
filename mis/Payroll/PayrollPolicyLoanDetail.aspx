<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollPolicyLoanDetail.aspx.cs" Inherits="mis_Payroll_PayrollPolicyLoanDetail" %>

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
                    <h3 class="box-title">Add Loan Details</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlOfficeName" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlOfficeName_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Employee Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlEmployee" runat="server" class="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Loan Head Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlEarnDeducHead" runat="server" class="form-control select2">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                </asp:DropDownList>
                                <small><span id="valddlEarnDeducHead" class="text-danger"></span></small>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Loan No</label>
                                <asp:TextBox ID="txtLoan_No" runat="server" CssClass="form-control" placeholder="Enter Loan Refrence No" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>

                    </div>


                    <div class="row">


                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Total Loan Amount</label>
                                <asp:TextBox ID="txtTotalAmount" runat="server" CssClass="form-control" placeholder="Enter Total Loan Amount" MaxLength="12" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Remaining Loan Amount<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtRemainingLoanAmount" runat="server" CssClass="form-control" placeholder="Remaining Loan Amount" MaxLength="12" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblLoanStartDate" runat="server" Text="Remaining As On Date"></asp:Label><span style="color: red;">*</span>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtLoan_StartDate" runat="server" placeholder="Select As On Date" class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Loan Premium Amount<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtLoan_PremiumAmount" runat="server" CssClass="form-control" placeholder="Enter Amount..." onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)" MaxLength="12"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>No. Of Install Paid<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtNoOfPaidInstallment" runat="server" CssClass="form-control" placeholder="Enter No..." onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)" MaxLength="3"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblLoanEndDate" runat="server" Text="Loan End Date"></asp:Label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtLoan_EndDate" runat="server" placeholder="Select End Date..." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Status<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlLoan_IsActive" runat="server" class="form-control">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="0">Close</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblLoanRemark" runat="server" Text="Loan Remark"></asp:Label>
                                <asp:TextBox ID="txtLoanRemark" runat="server" CssClass="form-control" placeholder="Loan Remark"></asp:TextBox>
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
                            <asp:GridView ID="GridView1" PageSize="50" runat="server" class="table table-hover table-bordered table-striped pagination-ys" AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="Loan_ID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Emp_Name" HeaderText="Officer/Employee Name" />
                                    <asp:BoundField DataField="EarnDeduction_Name" HeaderText="Head Name" />
                                    <asp:BoundField DataField="Loan_No" HeaderText="Loan No" />
                                    <asp:BoundField DataField="Loan_Premium" HeaderText="Loan Premium Amount" />
                                    <asp:BoundField DataField="Loan_TotalAmount" HeaderText="Loan Total Amount" />
                                    <asp:BoundField DataField="Loan_PaidInstallment" HeaderText="Total Paid Installment As On Selected Date" />
                                    <asp:BoundField DataField="Loan_BalanceAmount" HeaderText="Loan Balance Amount As On Date" />


                                    <asp:BoundField DataField="Loan_StartDate" HeaderText="As On Date" />
                                    <asp:BoundField DataField="Loan_EndDate" HeaderText="Loan End Date" />
                                    <asp:BoundField DataField="Loan_IsActive" HeaderText="Status" />
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
            if (document.getElementById('<%=ddlOfficeName.ClientID%>').selectedIndex == 0) {
                msg += "Select Office Name. \n";
            }
            if (document.getElementById('<%=ddlEmployee.ClientID%>').selectedIndex == 0) {
                msg += "Select Employee Name. \n";
            }
            if (document.getElementById('<%=ddlEarnDeducHead.ClientID%>').selectedIndex == 0) {
                msg += "Select Head Name. \n";
            }

            if (document.getElementById('<%=txtRemainingLoanAmount.ClientID%>').value.trim() == "") {
                msg += "Enter Remaining Loan Amount. \n";
            } else if (!objdb.isDecimal(txtRemainingLoanAmount.Text)) {
                msg += "Enter Correct Remaining Loan Amount. \n";
            }

            if (document.getElementById('<%=txtLoan_PremiumAmount.ClientID%>').value.trim() == "") {
                msg += "Enter Premium Amount. \n";
            }
            else if (!objdb.isDecimal(txtLoan_PremiumAmount.Text)) {
                msg += "Enter Correct Premium Amount. \n";
            }

            if (document.getElementById('<%=txtLoan_StartDate.ClientID%>').value.trim() == "") {
                msg += "Select Remaining As On Date. \n";
            }
            if (document.getElementById('<%=txtLoan_EndDate.ClientID%>').value.trim() == "") {
                msg += "Enter End Date. \n";
            }
            if (document.getElementById('<%=ddlLoan_IsActive.ClientID%>').selectedIndex == 0) {
                msg += "Select Status. \n";
            }
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

