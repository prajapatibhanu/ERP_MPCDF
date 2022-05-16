<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollEarn_DeductionOfficeWise.aspx.cs" Inherits="mis_Payroll_PayrollEarn_DeductionOfficeWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .deduction_list, .earning_list {
            font-size: 12px;
        }

        .Earningstable {
            background: #00968829 !important;
        }

        .Deductionstable {
            background: #ff63472b;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Set Applicable Earning & Deduction Office Wise</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office Name</label>
                                        <asp:DropDownList ID="ddlOfficeName" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Type<span style="color: red;"> *</span></label>
                                        <asp:DropDownList ID="ddlEarnDeduction_Type" runat="server" CssClass="form-control">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem Value="Earning">Earning</asp:ListItem>
                                            <asp:ListItem Value="Deduction">Deduction</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success btn-block" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateformSearch();"></asp:Button>
                                    </div>
                                </div>

                            </div>
                            <div class="form-group"></div>
                            <div class="form-group" id="DivDetail" runat="server">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-striped" ClientIDMode="Static" AutoGenerateColumns="False" AllowPaging="false" DataKeyNames="EarnDeduction_ID">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-Width="5%">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="checkAll" runat="server" ClientIDMode="static" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" Checked='<%# Eval("Status").ToString() == "0" ? false : true %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="4" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("EarnDeduction_ID").ToString()%>' runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="5" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Width="15%">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblHeader" runat="server" Text='Earning'></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEarnDeduction_Name" runat="server" Text='<%# Eval("EarnDeduction_NameActual").ToString()%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Ded/Contri" ItemStyle-Width="20%" ItemStyle-CssClass="cssPedding">
                                                        <ItemTemplate>
                                                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlContribution">
                                                                <asp:ListItem Value="Deduction" Text="Deduction"></asp:ListItem>
                                                                <asp:ListItem Value="Contribution(-)" Text="Contribution(- (Loan))"></asp:ListItem>
                                                                <asp:ListItem Value="Contribution(+)" Text="Contribution(+ (Saving))"></asp:ListItem>
                                                                <asp:ListItem Value="Contribution" Text="Contribution"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:Label ID="lblEarnDeduction_Calculation" CssClass="hidden" runat="server" Text='<%# Eval("EarnDeduction_Calculation").ToString()%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Head Name In Selected Office" ItemStyle-Width="25%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtHeadNameInOffice" runat="server" Text='<%# Eval("EarnDeduction_Name") %>' CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="200" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Order No" ItemStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtHeadNameOrderNo" runat="server" Text='<%# Eval("EarnDeduction_OrderNo") %>' CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="10" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Calculation Type" ItemStyle-Width="25%" ItemStyle-CssClass="cssPedding">
                                                        <ItemTemplate>
                                                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCalculationType">
                                                                <asp:ListItem Value="Auto Calculated (By Attendance)" Text="Auto Calculated (By Attendance)"></asp:ListItem>
                                                                <asp:ListItem Value="Copy Same From Previous Month" Text="Copy Same From Previous Month"></asp:ListItem>
                                                                <asp:ListItem Value="Set Zero Every Month" Text="Set Zero Every Month"></asp:ListItem>
                                                                <asp:ListItem Value="Loan Calculate" Text="Loan Calculate"></asp:ListItem>
                                                                <asp:ListItem Value="Other" Text="Other"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:Label ID="lblCalculationMethod" CssClass="hidden" runat="server" Text='<%# Eval("CalculationMethod").ToString()%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-success" ID="btnSave" Text="Save" OnClientClick="return validateform()" OnClick="btnSave_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <!-------------->
                                    <div class="col-md-12">
                                        <div class="earning_list table-responsive">

                                            <asp:GridView ID="GridView2" runat="server" class="table table-hover table-bordered Earningstable table-striped pagination-ys" EmptyDataText="No Record Found..!" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo2" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="EarnDeduction_Name" HeaderText="Earning Head Name In My Office" />
                                                    <asp:BoundField DataField="EarnDeduction_NameActual" HeaderText="Earning Head In Master" />
                                                    <asp:BoundField DataField="EarnDeduction_OrderNo" HeaderText="Order No" />
                                                    <asp:BoundField DataField="CalculationMethod" HeaderText="Calculation Method" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <!-------------->
                                    <div class="col-md-12">
                                        <div class="deduction_list table-responsive">
                                            <asp:GridView ID="GridView3" runat="server" class="table table-hover table-bordered Deductionstable table-striped pagination-ys" EmptyDataText="No Record Found..!" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo3" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="EarnDeduction_Name" HeaderText="Deduction Head Name In My Office" />
                                                    <asp:BoundField DataField="ContributionType" HeaderText="Contribution Type" />
                                                    <asp:BoundField DataField="EarnDeduction_NameActual" HeaderText="Deduction Head In Master" />
                                                    <asp:BoundField DataField="EarnDeduction_OrderNo" HeaderText="Order No" />
                                                    <asp:BoundField DataField="CalculationMethod" HeaderText="Calculation Method" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
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
    <script>
        $('#checkAll').click(function () {
            var inputList = document.querySelectorAll('#GridView1 tbody input[type="checkbox"]:not(:disabled)');
            for (var i = 0; i < inputList.length; i++) {
                if (document.getElementById('checkAll').checked) {
                    inputList[i].checked = true;
                }
                else {
                    inputList[i].checked = false;
                }
            }
        });


        function validateformSearch() {
            var msg = "";
            if (document.getElementById('<%=ddlOfficeName.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Office. \n";
            }
            if (document.getElementById('<%=ddlEarnDeduction_Type.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Earning Or Deduction. \n";
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;

            }
        }

        function validateform() {
            var msg = "";
            if (document.getElementById('<%=ddlOfficeName.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Office. \n";
            }
            if (document.getElementById('<%=ddlEarnDeduction_Type.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Earning Or Deduction. \n";
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (confirm("Do you really want to Save Details ?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
    </script>
</asp:Content>

