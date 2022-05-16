<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ProducerPaymentDetail.aspx.cs" Inherits="mis_MilkCollection_ProducerPaymentDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <script type="text/javascript">
        function CheckAll(oCheckbox) {
            var GridView2 = document.getElementById("<%=gvItemDetails.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
    </script>

    <script type="text/javascript">
        function confirmation() {
            if (confirm('Are you sure you want to save Producer Payment on excel format ?')) {
                return true;
            } else {
                return false;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #d9d9d9;">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                        <h4 class="modal-title" id="myModalLabel">Confirmation</h4>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <p>
                            <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp;
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnYes_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>

    <asp:ValidationSummary ID="vs1" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />

    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">
                        <asp:Label ID="lblUnitName" Text="Producer Payment Process" ForeColor="White" ClientIDMode="Static" Font-Size="Medium" Font-Bold="true" runat="server"></asp:Label>
                    </h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">

                    <div id="div_print">
                        <fieldset>
                            <legend>Filter</legend>
                            <div class="row">
                                <div class="col-md-12">

                                    <div class="col-md-2">
                                        <label>Milk Collection Unit<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ValidationGroup="a" ControlToValidate="ddlMilkCollectionUnit" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Milk Collection Unit!'></i>" ErrorMessage="Select Milk Collection Unit" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlMilkCollectionUnit" AutoPostBack="true" OnSelectedIndexChanged="ddlMilkCollectionUnit_SelectedIndexChanged" CssClass="form-control select2" runat="server">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="5">BMC</asp:ListItem>
                                                <asp:ListItem Value="6">DCS</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <label>Society<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvFarmer" runat="server" Display="Dynamic" ValidationGroup="a" ControlToValidate="ddlSociety" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Society!'></i>" ErrorMessage="Select Society" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlSociety" CssClass="form-control select2" runat="server">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Payment From Date  </label>
                                            <span style="color: red">*</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv1" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtFdt" ErrorMessage="Please Enter From Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter From Date !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revdate" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtFdt" ErrorMessage="Invalid From Date" Text="<i class='fa fa-exclamation-circle' title='Invalid From Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtFdt" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select From Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Payment To Date  </label>
                                            <span style="color: red">*</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtTdt" ErrorMessage="Please Enter To Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter To Date !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtTdt" ErrorMessage="Invalid To Date" Text="<i class='fa fa-exclamation-circle' title='Invalid To Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtTdt" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select To Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-1" style="margin-top: 20px;">
                                        <div class="form-group">
                                            <div class="form-group">
                                                <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click" ValidationGroup="a" ID="btnSubmit" Text="Search" AccessKey="S" />

                                            </div>
                                        </div>
                                    </div>



                                </div>
                            </div>
                        </fieldset>
                        <div class="row" runat="server" visible="false" id="divIteminfo">

                            <div class="col-md-12">
                                <fieldset>
                                    <legend>Producer's Detail</legend>

                                    <div class="row">

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:RadioButtonList ID="rbppaymentMethod" AutoPostBack="true" OnSelectedIndexChanged="rbppaymentMethod_SelectedIndexChanged" CssClass="form-control" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="True" Value="1"><label style="padding-left: 10px; padding-right: 10px;"> Payment By Online</label></asp:ListItem>
                                                    <asp:ListItem Value="2"><label style="padding-left: 10px; padding-right: 10px;">Payment By Cash</label></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
										
										
										 <div class="col-md-2 pull-right">
                                            <div class="form-group">
                                                <asp:Button ID="btnh2hFile" runat="server" Text="Generate H2H File" OnClientClick="return confirmationH2H();" CssClass="btn btn-primary btn-block" OnClick="btnh2hFile_Click" />
                                            </div>
                                        </div>
										
										
                                        <div class="col-md-2 pull-right">
                                            <div class="form-group">
                                                <asp:Button ID="btnSave" runat="server" Text="Export To Excel" OnClientClick="return confirmation();" CssClass="btn btn-primary btn-block" OnClick="btnSave_Click" />
                                            </div>
                                        </div>
                                        <div class="col-md-2 pull-right">
                                            <div class="form-group">
                                                <asp:Button runat="server" ID="btnPrint" Text="Print (Ctrl + P)" CssClass="btn btn-default pull-right" AccessKey="p" OnClientClick="printdiv('div_print');" />
                                            </div>
                                        </div>

                                    </div>

                                    <asp:GridView ID="gvItemDetails" Visible="false" runat="server" EmptyDataText="No records Found" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover">
                                        <Columns>

                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <input id="Checkbox2" type="checkbox" onclick="CheckAll(this)" runat="server" />
                                                    All
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSealNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Producer Name">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblProducerName" runat="server" Text='<%# Eval("ProducerName") %>'></asp:Label>(<%# Eval("ProducerCardNo") %>)

                                                <asp:Label ID="lblMobile" Visible="false" runat="server" Text='<%# Eval("Mobile") %>'></asp:Label>
                                                    <asp:Label ID="lblProducerId" Visible="false" runat="server" Text='<%# Eval("ProducerId") %>'></asp:Label>
                                                    <asp:Label ID="lblMilkValue" Visible="false" runat="server" Text='<%# Eval("MilkValue") %>'></asp:Label>
                                                    <asp:Label ID="lblSaleValue" Visible="false" runat="server" Text='<%# Eval("SaleValue") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Payable Amount">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPayableAmount" ToolTip='<%# "MilkValue-SaleValue = (" +Eval("MilkValue") + " - "+ Eval("SaleValue") +" )" %>' Enabled="false" onkeypress="return validateDec(this,event)" MaxLength="10" placeholder="Payable Amount" ValidationGroup="a" Text='<%# Eval("PayableAmount") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator101A" ValidationGroup="b"
                                                            ErrorMessage="Payable Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Payable Amount!'></i>"
                                                            ControlToValidate="txtPayableAmount" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator51B" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="b" runat="server" ControlToValidate="txtPayableAmount" ErrorMessage="Enter Payable Amount" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Payable Amount!'></i>"></asp:RegularExpressionValidator>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Paid Amount">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPaidAmount" onkeypress="return validateDec(this,event)" MaxLength="10" Text='<%# Eval("PayableAmount") %>' placeholder="Paid Amount" ValidationGroup="b" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator102C" ValidationGroup="b"
                                                            ErrorMessage="Enter Paid Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Net Paid Amount!'></i>"
                                                            ControlToValidate="txtPaidAmount" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator52D" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="b" runat="server" ControlToValidate="txtPaidAmount" ErrorMessage="Enter Valid Paid Amount" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Paid Amount!'></i>"></asp:RegularExpressionValidator>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bank Name">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtBank_Name" Enabled="false" MaxLength="10" placeholder="Enter Bank Name" Text='<%# Eval("BankName") %>' ValidationGroup="b" CssClass="form-control" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="A/C No.">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAccountNo" Enabled="false" MaxLength="10" placeholder="Enter A/c No" Text='<%# Eval("AccountNo") %>' ValidationGroup="b" CssClass="form-control" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Payment Date">
                                                <ItemTemplate>
                                                    <div class="form-group">
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtpaymentDate" Text='<%# Eval("T_Date") %>' data-date-start-date="0d" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Payment Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="UTR NO.">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtUTRNo" MaxLength="10" placeholder="Enter UTRNo" ValidationGroup="b" CssClass="form-control" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Remark">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRemark" MaxLength="10" placeholder="Enter Remark" ValidationGroup="b" CssClass="form-control" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>

                                    <asp:GridView ID="gvItemDetails_Cash" Visible="false" runat="server" EmptyDataText="No records Found" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover">
                                        <Columns>

                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <input id="Checkbox2" type="checkbox" onclick="CheckAll(this)" runat="server" />
                                                    All
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSealNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Producer Name">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblProducerName" runat="server" Text='<%# Eval("ProducerName") %>'></asp:Label>(<%# Eval("ProducerCardNo") %>)

                                                <asp:Label ID="lblMobile" Visible="false" runat="server" Text='<%# Eval("Mobile") %>'></asp:Label>
                                                    <asp:Label ID="lblProducerId" Visible="false" runat="server" Text='<%# Eval("ProducerId") %>'></asp:Label>
                                                    <asp:Label ID="lblMilkValue" Visible="false" runat="server" Text='<%# Eval("MilkValue") %>'></asp:Label>
                                                    <asp:Label ID="lblSaleValue" Visible="false" runat="server" Text='<%# Eval("SaleValue") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Payable Amount">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPayableAmount" ToolTip='<%# "MilkValue-SaleValue = (" +Eval("MilkValue") + " - "+ Eval("SaleValue") +" )" %>' Enabled="false" onkeypress="return validateDec(this,event)" MaxLength="10" placeholder="Payable Amount" ValidationGroup="a" Text='<%# Eval("PayableAmount") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator101A" ValidationGroup="b"
                                                            ErrorMessage="Payable Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Payable Amount!'></i>"
                                                            ControlToValidate="txtPayableAmount" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator51B" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="b" runat="server" ControlToValidate="txtPayableAmount" ErrorMessage="Enter Payable Amount" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Payable Amount!'></i>"></asp:RegularExpressionValidator>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Paid Amount">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPaidAmount" onkeypress="return validateDec(this,event)" MaxLength="10" Text='<%# Eval("PayableAmount") %>' placeholder="Paid Amount" ValidationGroup="b" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator102C" ValidationGroup="b"
                                                            ErrorMessage="Enter Paid Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Net Paid Amount!'></i>"
                                                            ControlToValidate="txtPaidAmount" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator52D" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="b" runat="server" ControlToValidate="txtPaidAmount" ErrorMessage="Enter Valid Paid Amount" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Paid Amount!'></i>"></asp:RegularExpressionValidator>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Payment Date">
                                                <ItemTemplate>
                                                    <div class="form-group">
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtpaymentDate" Text='<%# Eval("T_Date") %>' data-date-start-date="0d" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Payment Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cash Paument">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtUTRNo" MaxLength="10" placeholder="Enter Reference No" ValidationGroup="b" CssClass="form-control" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Remark">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRemark" MaxLength="10" placeholder="Enter Remark" ValidationGroup="b" CssClass="form-control" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>


                                    <div class="row">
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <div class="form-group">
                                                    <asp:Button runat="server" CssClass="btn btn-primary" OnClientClick="return ValidatePage();" ValidationGroup="a" ID="btnPayAmount" Text="Proceed To Pay" AccessKey="S" />
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                </fieldset>
                            </div>

                        </div>

                    </div>


                </div>
            </div>


            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Producer Payment History</h3>
                </div>
                <div class="box-body">
                    <fieldset>
                        <legend>Payment History</legend>
                        <div class="row">
                            <div class="col-md-12">

                                <asp:GridView ID="GV_PPHistory" runat="server" EmptyDataText="No records Found" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover">
                                    <Columns>

                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSealNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Producer Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProducerName" ToolTip='<%# Eval("SmsText") %>' runat="server" Text='<%# Eval("ProducerName") %>'></asp:Label>(<%# Eval("ProducerCardNo") %>)
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Payable Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPayableAmount" runat="server" Text='<%# Eval("PayableAmount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Paid Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPaidAmount" runat="server" Text='<%# Eval("PaidAmount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bank Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBank_Name" runat="server" Text='<%# Eval("Bank_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="A/C No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccountNo" runat="server" Text='<%# Eval("AccountNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Payment Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPaymentDt" runat="server" Text='<%# Eval("PaymentDt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="UTR NO.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUTRNo" runat="server" Text='<%# Eval("UTRNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remark">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>


                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>




        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('b');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Search") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }


        function allnumeric(inputtxt) {
            var numbers = /^[0-9]+$/;
            if (inputtxt.value.match(numbers)) {
                alert('only number has accepted....');
                document.form1.text1.focus();
                return true;
            }
            else {
                alert('Please input numeric value only');
                document.form1.text1.focus();
                return false;
            }
        }
         

    </script>


    <script lang="javascript">
        function printdiv(printpage) {
            var headstr = "<html><head><title>Producer Payment Invoice</title></head><body>";
            var footstr = "</body>";
            var newstr = "<center><h5>" + document.getElementById('<%= lblUnitName.ClientID %>').textContent + "</h5></center><br/>" + document.all.item(printpage).innerHTML;
            var oldstr = document.body.innerHTML;
            document.body.innerHTML = headstr + newstr + footstr;
            window.print();
            document.body.innerHTML = oldstr;
            return false;
        }
    </script>


</asp:Content>
