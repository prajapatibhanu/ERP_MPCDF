<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Frm_RokadBahiEntry.aspx.cs" Inherits="mis_Production_Frm_RokadBahiEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Rokad Bahi Entry Form</h3>

                            <span id="ctl00_ContentBody_lblmsg"></span>
                        </div>
                        <div class="box-body">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Date<span style="color: red;">*</span></label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtDate" onkeypress="return false;" runat="server" placeholder="dd/mm/yyyy" class="form-control" data-date-end-date="+7d" autocomplete="off" data-date-format="dd/mm/yyyy" data-date-autoclose="true" data-provide="datepicker" data-date-start-date="-365d" onpaste="return false" ClientIDMode="Static" OnTextChanged="txtDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <fieldset>
                                            <legend>जमा विवरण</legend>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <label>खाते का नाम<span style="color: red;">*</span></label>
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtBankAccountName" runat="server" placeholder="खाते का नाम" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <label>विवरण</label>
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtDescription" runat="server" placeholder="विवरण" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <label>राशि<span style="color: red;">*</span></label>
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtAmount" runat="server" placeholder="राशि" class="form-control" onchange="OpeningAmountCalculate();"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Button ID="btnOpAdd" runat="server" Text="Add" CssClass="btn btn-warning" OnClick="btnOpAdd_Click" Style="margin-top: 20px;" OnClientClick="return validateformOpAdd();" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="table table-responsive">
                                                        <asp:GridView ID="GvOpeningDetail" runat="server" CssClass="table table-bordered table-striped table-hover" ShowFooter="true" AutoGenerateColumns="False" DataKeyNames="OpRowNo" OnRowDeleting="GvOpeningDetail_RowDeleting">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S. No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="खाते का नाम">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAccountName" runat="server" Text='<%# Eval("AccountName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="विवरण">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("OpDescription") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="राशि">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("OpAmount") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkdelete1" OnClientClick="return confirm('Are you sure want to delete this record?')" runat="server" CommandName="Delete" Style="color: red;"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-8"></div>
                                                <div class="col-md-4">
                                                    <label>कुल राशि<span style="color: red;">*</span></label>
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtTotalAmount" runat="server" placeholder="कुल राशि" Text="0" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <fieldset>
                                                <legend>नामे विवरण</legend>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <label>खाते का नाम<span style="color: red;">*</span></label>
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtClosingAccountName" runat="server" placeholder="खाते का नाम" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-8">
                                                        <table class="table table-bordered">
                                                            <tr>
                                                                <td colspan="2" style="text-align: center;">आज का संग्रह (Today's Collection)</td>
                                                                <td style="text-align: center;">राशि (Amount)</td>
                                                            </tr>
                                                            <tr>
                                                                <td>Morning Cow</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtMCCollection" runat="server" class="form-control" Text="0"></asp:TextBox></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtMCAmount" runat="server" class="form-control" Text="0" onchange="CollectionCalculate();"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td>Morning Buffalo</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtMBCollection" runat="server" class="form-control" Text="0"></asp:TextBox></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtMBAmount" runat="server" class="form-control" Text="0" onchange="CollectionCalculate();"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td>Evening Cow</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtECCollection" runat="server" class="form-control" Text="0"></asp:TextBox></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtECAmount" runat="server" class="form-control" Text="0" onchange="CollectionCalculate();"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td>Evening Buffalo</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtEBCollection" runat="server" class="form-control" Text="0"></asp:TextBox></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtEBAmount" runat="server" class="form-control" Text="0" onchange="CollectionCalculate();"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="text-align: right;">कुल राशि (Total Amount)</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTAmount" runat="server" Text="0" class="form-control"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                                <%-- <div class="row">
                                                    <div class="col-md-3 pull-left">
                                                        <div class="form-group">
                                                            <asp:CheckBox ID="chkAddExpense" runat="server" RepeatDirection="Horizontal" Text="Add Expense" OnCheckedChanged="chkAddExpense_CheckedChanged" AutoPostBack="true" />
                                                        </div>
                                                    </div>
                                                </div>--%>
                                                <div class="row" id="ExpenseDetail" runat="server">
                                                    <div class="col-md-12">
                                                        <fieldset>
                                                            <legend>Add Expenses</legend>
                                                            <%--  <div class="row">
                                                                <div class="col-md-3">
                                                                    <label>खाते का नाम<span style="color: red;">*</span></label>
                                                                    <div class="form-group">
                                                                        <asp:TextBox ID="txtExpensesAccName" runat="server" placeholder="खाते का नाम" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <label>विवरण</label>
                                                                    <div class="form-group">
                                                                        <asp:TextBox ID="txtExpDescription" runat="server" placeholder="विवरण" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <label>राशि<span style="color: red;">*</span></label>
                                                                    <div class="form-group">
                                                                        <asp:TextBox ID="txtExpAmount" runat="server" placeholder="राशि" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>--%>
                                                            <div class="row">
                                                                <%--<div class="col-md-4">
                                                                    <label>Date<span style="color: red;">*</span></label>
                                                                    <div class="form-group">
                                                                        <span class="pull-right">
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="red" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtDate" ErrorMessage="Enter Date." Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"></asp:RequiredFieldValidator>
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" ForeColor="Red" runat="server" Display="Dynamic" ControlToValidate="txtDate" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                                                        </span>
                                                                        <div class="input-group date">
                                                                            <div class="input-group-addon">
                                                                                <i class="fa fa-calendar"></i>
                                                                            </div>
                                                                            <asp:TextBox ID="txtExpensesDate" onkeypress="return false;" runat="server" placeholder="dd/mm/yyyy" class="form-control" data-date-end-date="+7d" autocomplete="off" data-date-format="dd/mm/yyyy" data-date-autoclose="true" data-provide="datepicker" data-date-start-date="-365d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>--%>
                                                                <div class="col-md-3">
                                                                    <label>खाते का नाम<span style="color: red;">*</span></label>
                                                                    <div class="form-group">
                                                                        <asp:TextBox ID="txtExpensesAccName" runat="server" placeholder="खाते का नाम" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <label>विवरण</label>
                                                                    <div class="form-group">
                                                                        <asp:TextBox ID="txtExpensesDescription" runat="server" placeholder="विवरण" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <label>राशि<span style="color: red;">*</span></label>
                                                                    <div class="form-group">
                                                                        <asp:TextBox ID="txtExpenseAmount" runat="server" placeholder="राशि" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <label>&nbsp;</label>
                                                                    <div class="form-group">
                                                                        <asp:Button ID="btnAdd" runat="server" class="btn btn-warning" Text="Add" OnClick="btnAdd_Click" OnClientClick="return validateformAdd();"></asp:Button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="table table-responsive">
                                                                        <asp:GridView ID="GrExpenseTable" runat="server" CssClass="table table-bordered table-striped table-hover" ShowFooter="true" AutoGenerateColumns="False" DataKeyNames="RowNo" OnRowDeleting="GrExpenseTable_RowDeleting">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="S. No">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="खाते का नाम">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblAccountName" runat="server" Text='<%# Eval("ClAccountName") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="विवरण">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="राशि">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Action">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnkdelete" OnClientClick="return confirm('Are you sure want to delete this record?')" runat="server" CommandName="Delete" Style="color: red;"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-8"></div>
                                                                <div class="col-md-4">
                                                                    <label>व्यय योग<span style="color: red;">*</span></label>
                                                                    <div class="form-group">
                                                                        <asp:TextBox ID="txtDebitTotal" runat="server" placeholder="व्यय योग" Text="0" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-8"></div>
                                                                <div class="col-md-4">
                                                                    <label>अंतिम रोकड़<span style="color: red;">*</span></label>
                                                                    <div class="form-group">
                                                                        <asp:TextBox ID="txtLastAmount" runat="server" placeholder="व्यय योग" Text="0" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </fieldset>
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 pull-right">
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-block" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateform();" />
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
    <script type="text/javascript">
        function validateform() {
            debugger;
            var msg = "";
            if (document.getElementById('<%=txtDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select Date. \n";
            }
            if (document.getElementById('<%=txtTotalAmount.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Total Amount. \n";
            }
            if (document.getElementById('<%=txtClosingAccountName.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Closing Account Name. \n";
            }
            if (document.getElementById('<%=txtMCCollection.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Morning Cow Collection. \n";
            }
            if (document.getElementById('<%=txtMCAmount.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Morning Cow Collection Amount \n";
            }
            if (document.getElementById('<%=txtMBCollection.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Morning Buffalo Collection \n";
            }
            if (document.getElementById('<%=txtMBAmount.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Morning Buffalo Collection Amount. \n";
            }
            if (document.getElementById('<%=txtECCollection.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Evening Cow Collection \n";
            }
            if (document.getElementById('<%=txtECAmount.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Evening Cow Collection Amount \n";
            }
            if (document.getElementById('<%=txtEBCollection.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Evening Buffalo Collection. \n";
            }
            if (document.getElementById('<%=txtEBAmount.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Evening Buffalo Collection Amount. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
            }
        }

        function validateformAdd() {
            debugger;
            var msg = "";
            if (document.getElementById('<%=txtExpensesAccName.ClientID%>').value.trim() == "") {
                msg = msg + "Select Date. \n";
            }
            if (document.getElementById('<%=txtExpenseAmount.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Amount. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
            }
        }

        function validateformOpAdd() {
            debugger;
            var msg = "";
            if (document.getElementById('<%=txtBankAccountName.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Account number. \n";
            }
            if (document.getElementById('<%=txtDescription.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Description. \n";
            }
            if (document.getElementById('<%=txtAmount.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Amount. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
            }
        }

        function CollectionCalculate() {
            debugger;
            var MCAmount = document.getElementById('<%=txtMCAmount.ClientID%>').value.trim();
            var MBAmount = document.getElementById('<%=txtMBAmount.ClientID%>').value.trim();
            var ECAmount = document.getElementById('<%=txtECAmount.ClientID%>').value.trim();
            var EBAmount = document.getElementById('<%=txtEBAmount.ClientID%>').value.trim();
            if (MCAmount == "") {
                MCAmount = "0";
            }
            if (MBAmount == "") {
                MBAmount = "0";
            }
            if (ECAmount == "") {
                ECAmount = "0";
            }
            if (EBAmount == "") {
                EBAmount = "0";
            }
            var TotalCollAmt = parseFloat(MCAmount) + parseFloat(MBAmount) + parseFloat(ECAmount) + parseFloat(EBAmount)
            document.getElementById('<%=txtTAmount.ClientID%>').value = TotalCollAmt.toString();
            document.getElementById('<%=txtExpenseAmount.ClientID%>').value = TotalCollAmt.toString();
        }

        function OpeningAmountCalculate() {
            debugger;
            var Amount = document.getElementById('<%=txtAmount.ClientID%>').value.trim();
            if (Amount == "") {
                Amount = "0";
            }
            if (TAmt == "") {
                TAmt = "0";
            }
            var TotalAmt = parseFloat(Amount) + parseFloat(TAmt)
            document.getElementById('<%=txtTotalAmount.ClientID%>').value = TotalAmt.toString();
        }

    </script>
</asp:Content>

