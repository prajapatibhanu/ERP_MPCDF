<%@ Page Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MilkOrProductInstitutePaymentEntry.aspx.cs" Inherits="mis_DemandSupply_MilkOrProductInstitutePaymentEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSave_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Institute Payment Entry</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <div class="box-body">
                    <fieldset>
                        <legend>Institute Payment Entry</legend>
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Date <%--/ दिनांक--%><span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                            ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                            ControlToValidate="txtDeliveryDate" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtDeliveryDate"
                                            ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <%--<asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOrderDate" OnTextChanged="txtOrderDate_TextChanged" AutoPostBack="true" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>--%>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDeliveryDate" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static" OnTextChanged="txtDeliveryDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>
                             <div class="col-md-3">
                            <div class="form-group">
                                <label>Item Category</label>
                                <asp:DropDownList ID="ddlItemCategory" Enabled="false"  AutoPostBack="true" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Location</label>
                                    <asp:DropDownList ID="ddlLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                    </asp:DropDownList>
                                </div>

                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Institute Name <span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select Distributor Name" Text="<i class='fa fa-exclamation-circle' title='Select Distributor Name !'></i>"
                                            ControlToValidate="ddlInstitute" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlInstitute" AutoPostBack="true" OnSelectedIndexChanged="ddlInstitute_SelectedIndexChanged"  runat="server" CssClass="form-control select2">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <asp:Panel ID="pnlMilk" visible="false" runat="server">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Milk Amount<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                            ErrorMessage="Enter Milk Amount" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Amount !'></i>"
                                            ControlToValidate="txtMilkAmount" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:TextBox ID="txtMilkAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <label>Remark</label>
                                    <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ForeColor="Red"
                                            ValidationGroup="a" Display="Dynamic" runat="server"
                                            ControlToValidate="txtRemark"
                                            ErrorMessage="Alphanumeric ,space and some special symbols like '.,/-:' allow"
                                            Text="<i class='fa fa-exclamation-circle' title='Alphanumeric ,space and some special symbols like '.,/-:' allow !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^[0-9a-zA-Z\s.,/-:]+$">

                                        </asp:RegularExpressionValidator>

                                    </span>
                                    <asp:TextBox ID="txtRemark" autocomplete="off" runat="server" MaxLength="200" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                                 </asp:Panel>
                           <asp:Panel ID="pnlProduct" visible="false" runat="server">
                             <div class="col-md-2">
                                <div class="form-group">
                                    <label>Total Bill Amount<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                            ErrorMessage="Enter Total Bill Amount" Text="<i class='fa fa-exclamation-circle' title='Enter Total Bill Amount !'></i>"
                                            ControlToValidate="txtTotalBillAmount" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <asp:TextBox ID="txtTotalBillAmount" autocomplete="off" MaxLength="10" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Total GST Amount<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="a"
                                            ErrorMessage="Enter Total GST Amount" Text="<i class='fa fa-exclamation-circle' title='Enter Total GST Amount !'></i>"
                                            ControlToValidate="txtTotalGSTAmount" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <asp:TextBox ID="txtTotalGSTAmount" autocomplete="off" MaxLength="10" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Total TcsTax Amount<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a"
                                            ErrorMessage="Enter Total TcsTax Amount" Text="<i class='fa fa-exclamation-circle' title='Enter Total TcsTax Amount !'></i>"
                                            ControlToValidate="txtTotalTcsTaxAmount" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <asp:TextBox ID="txtTotalTcsTaxAmount" autocomplete="off" MaxLength="10" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>DM No<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="a"
                                            ErrorMessage="Enter DM No" Text="<i class='fa fa-exclamation-circle' title='Enter DM No !'></i>"
                                            ControlToValidate="txtDMNo" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <asp:TextBox ID="txtDMNo" MaxLength="100" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                             <div class="col-md-12">
                                <div class="form-group">
                                    <label>Remark</label>
                                    <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ForeColor="Red"
                                            ValidationGroup="a" Display="Dynamic" runat="server"
                                            ControlToValidate="txtRemarkProd"
                                            ErrorMessage="Alphanumeric ,space and some special symbols like '.,/-:' allow"
                                            Text="<i class='fa fa-exclamation-circle' title='Alphanumeric ,space and some special symbols like '.,/-:' allow !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^[0-9a-zA-Z\s.,/-:]+$">

                                        </asp:RegularExpressionValidator>

                                    </span>
                                    <asp:TextBox ID="txtRemarkProd" autocomplete="off" runat="server" MaxLength="200" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                                    
                                </asp:Panel>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>1) Payment Mode</label>
                                    <asp:DropDownList ID="ddlPaymentMode1" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>1) Payment No</label>
                                    <asp:TextBox ID="txtPaymentNo1" runat="server" CssClass="form-control" MaxLength="80" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>1) Payment Amt</label>
                                    <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ForeColor="Red"
                                            ValidationGroup="a" Display="Dynamic" runat="server"
                                            ControlToValidate="txtPaymentAmt1"
                                            ErrorMessage="Invalid Payment Amt1"
                                            Text="<i class='fa fa-exclamation-circle' title='Invalid Payment Amt1 !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^[0-9.-]+$">

                                        </asp:RegularExpressionValidator>

                                    </span>
                                    <asp:TextBox ID="txtPaymentAmt1" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>1) Payment Date <span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtPaymentDate1"
                                            ErrorMessage="Invalid Payment Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Payment Date !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtPaymentDate1" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>2) Payment Mode</label>
                                    <asp:DropDownList ID="ddlPaymentMode2" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>2) Payment No</label>
                                    <asp:TextBox ID="txtPaymentNo2" runat="server" CssClass="form-control" MaxLength="80" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>2) Payment Amt</label>
                                     <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" ForeColor="Red"
                                            ValidationGroup="a" Display="Dynamic" runat="server"
                                            ControlToValidate="txtPaymentAmt2"
                                            ErrorMessage="Invalid Payment Amt2"
                                            Text="<i class='fa fa-exclamation-circle' title='Invalid Payment Amt2 !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^[0-9.-]+$">

                                        </asp:RegularExpressionValidator>

                                    </span>
                                    <asp:TextBox ID="txtPaymentAmt2" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>2) Payment Date <span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtPaymentDate2"
                                            ErrorMessage="Invalid Payment Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Payment Date !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtPaymentDate2" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>3) Payment Mode</label>
                                    <asp:DropDownList ID="ddlPaymentMode3" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>3) Payment No</label>
                                    <asp:TextBox ID="txtPaymentNo3" runat="server" CssClass="form-control" MaxLength="80" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>3) Payment Amt</label>
                                     <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator11" ForeColor="Red"
                                            ValidationGroup="a" Display="Dynamic" runat="server"
                                            ControlToValidate="txtPaymentAmt3"
                                            ErrorMessage="Invalid Payment Amt3"
                                            Text="<i class='fa fa-exclamation-circle' title='Invalid Payment Amt3 !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^[0-9.-]+$">

                                        </asp:RegularExpressionValidator>

                                    </span>
                                    <asp:TextBox ID="txtPaymentAmt3" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>3) Payment Date <span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtPaymentDate3"
                                            ErrorMessage="Invalid Payment Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Payment Date !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtPaymentDate3" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>4) Payment Mode</label>
                                    <asp:DropDownList ID="ddlPaymentMode4" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>4) Payment No</label>
                                    <asp:TextBox ID="txtPaymentNo4" runat="server" CssClass="form-control" MaxLength="80" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>4) Payment Amt</label>
                                     <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator12" ForeColor="Red"
                                            ValidationGroup="a" Display="Dynamic" runat="server"
                                            ControlToValidate="txtPaymentAmt4"
                                            ErrorMessage="Invalid Payment Amt4"
                                            Text="<i class='fa fa-exclamation-circle' title='Invalid Payment Amt4 !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^[0-9.-]+$">

                                        </asp:RegularExpressionValidator>

                                    </span>
                                    <asp:TextBox ID="txtPaymentAmt4" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>4) Payment Date <span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtPaymentDate4"
                                            ErrorMessage="Invalid Payment Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Payment Date !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtPaymentDate4" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSave" Text="Save" OnClientClick="return ValidatePage();"  />

                            </div>
                        </div>
                        <div class="col-md-1" style="margin-top: 20px;">
                            <div class="form-group">

                                <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-block btn btn-default" />
                            </div>
                        </div>
                    </fieldset>

                </div>
                <div class="box-body">
                    <fieldset>
                        <legend>Institute Payment Entry</legend>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Date <%--/ दिनांक--%></label>
                                <span class="pull-right">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtDateFilter"
                                        ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                        ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                </span>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDateFilter" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                         <div class="col-md-2">
                            <div class="form-group">
                                <label>Item Category</label>
                                <asp:DropDownList ID="ddlFilterItemCategory" Enabled="false"  AutoPostBack="true" OnSelectedIndexChanged="ddlFilterItemCategory_SelectedIndexChanged"  runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Location</label>
                                <asp:DropDownList ID="ddlLocationfilter" AutoPostBack="true" OnSelectedIndexChanged="ddlLocationfilter_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div class="col-md-3">
                                <div class="form-group">
                                    <label>Institute Name</label>
                                    <asp:DropDownList ID="ddlFilterInstitute" runat="server" CssClass="form-control select2">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        <div class="col-md-2">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:Button runat="server" CssClass="btn btn-primary" CausesValidation="true" ValidationGroup="vgsa" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
                                <asp:Button runat="server" CssClass="btn btn-success" Visible="false" ID="btnExport" OnClick="btnExport_Click" Text="Export" />

                            </div>
                        </div>
                        <div class="col-md-12">
                            <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound"
                                DataKeyNames="DistPaymentSheet_ID" CssClass="table table-bordered" ShowFooter="true"
                                ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" AutoGenerateColumns="false" OnRowCommand="GridView1_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                             <asp:Label ID="lblAreaId" Visible="false" Text='<%#Eval("AreaId") %>' runat="server"></asp:Label>
                                             <asp:Label ID="lblRouteId" Visible="false" Text='<%#Eval("RouteId") %>' runat="server"></asp:Label>
                                             <asp:Label ID="lblDistributorId" Visible="false" Text='<%#Eval("DistributorId") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPaymentModeId1" Visible="false" Text='<%#Eval("PaymentModeId1") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPaymentNo1" Visible="false" Text='<%#Eval("PaymentNo1") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPaymentDate1" Visible="false" Text='<%#Eval("PaymentDate1") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPaymentModeId2" Visible="false" Text='<%#Eval("PaymentModeId2") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPaymentNo2" Visible="false" Text='<%#Eval("PaymentNo2") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPaymentDate2" Visible="false" Text='<%#Eval("PaymentDate2") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPaymentModeId3" Visible="false" Text='<%#Eval("PaymentModeId3") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPaymentNo3" Visible="false" Text='<%#Eval("PaymentNo3") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPaymentDate3" Visible="false" Text='<%#Eval("PaymentDate3") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPaymentModeId4" Visible="false" Text='<%#Eval("PaymentModeId4") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPaymentNo4" Visible="false" Text='<%#Eval("PaymentNo4") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPaymentDate4" Visible="false" Text='<%#Eval("PaymentDate4") %>' runat="server"></asp:Label>
                                             <asp:Label ID="lblRemark" Visible="false"  runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                            <asp:Label ID="lblSSRDId" Visible="false"  runat="server" Text='<%# Eval("SSRDId") %>'></asp:Label>
                                            <asp:Label ID="lblItemCat_id" Visible="false"  runat="server" Text='<%# Eval("ItemCat_id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDelivary_Date" runat="server" Text='<%# Eval("Delivary_Date") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <%-- <asp:TemplateField HeaderText="Route">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRName" runat="server" Text='<%# Eval("RName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Distributor">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDistributor" runat="server" Text='<%# Eval("BName") + " -" + Eval("BName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                             <b>Total</b>
                                       </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Milk Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                       <FooterTemplate>
                                            <asp:Label ID="lblFAmount" runat="server"></asp:Label>
                                       </FooterTemplate>
                                    </asp:TemplateField>
                                   <%-- <asp:TemplateField HeaderText="Remark">
                                        <ItemTemplate>
                                           
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Payment Amt1" HeaderStyle-Width="5px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaymentAmount1" Text='<%#Eval("PaymentAmount1") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                            <asp:Label ID="lblFPA1" runat="server"></asp:Label>
                                       </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment Amt2" HeaderStyle-Width="5px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaymentAmount2" Text='<%#Eval("PaymentAmount2") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                            <asp:Label ID="lblFPA2" runat="server"></asp:Label>
                                       </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment Amt3" HeaderStyle-Width="5px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaymentAmount3" Text='<%#Eval("PaymentAmount3") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                            <asp:Label ID="lblFPA3" runat="server"></asp:Label>
                                       </FooterTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Payment Amt4" HeaderStyle-Width="5px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaymentAmount4" Text='<%#Eval("PaymentAmount4") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                          <FooterTemplate>
                                            <asp:Label ID="lblFPA4" runat="server"></asp:Label>
                                       </FooterTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Total Payment" HeaderStyle-Width="5px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalPayment" Text='<%#Eval("TotalPaitAmt") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                           <FooterTemplate>
                                            <asp:Label ID="lblFTPA" runat="server"></asp:Label>
                                       </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="EditRecord" CommandArgument='<%# Eval("DistPaymentSheet_ID") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="Balance">
                                <ItemTemplate>
                                    <asp:Label ID="lblBalance" runat="server" Text='<%# Eval("Balance") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>
                            <asp:GridView ID="GridView2" OnRowCommand="GridView2_RowCommand" ShowFooter="true" OnRowDataBound="GridView2_RowDataBound" runat="server" class="table table-striped table-bordered" AllowPaging="false"
                                            AutoGenerateColumns="false" EmptyDataText="No Record Found." DataKeyNames="ProductPaymentSheet_ID" EnableModelValidation="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        <asp:Label ID="lblDMChallanNo" Visible="false" Text='<%#Eval("DMChallanNo") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="lblCatId" Visible="false" Text='<%#Eval("ItemCat_id") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="lblDelivaryShift_id" Visible="false" Text='<%#Eval("DelivaryShift_id") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="lblIsActive" Visible="false" Text='<%#Eval("IsActive") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="PlblPaymentModeId1" Visible="false" Text='<%#Eval("PaymentModeId1") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="PlblPaymentNo1" Visible="false" Text='<%#Eval("PaymentNo1") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="PlblPaymentDate1" Visible="false" Text='<%#Eval("PaymentDate1") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="PlblPaymentModeId2" Visible="false" Text='<%#Eval("PaymentModeId2") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="PlblPaymentNo2" Visible="false" Text='<%#Eval("PaymentNo2") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="PlblPaymentDate2" Visible="false" Text='<%#Eval("PaymentDate2") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="PlblPaymentModeId3" Visible="false" Text='<%#Eval("PaymentModeId3") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="PlblPaymentNo3" Visible="false" Text='<%#Eval("PaymentNo3") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="PlblPaymentDate3" Visible="false" Text='<%#Eval("PaymentDate3") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="PlblPaymentModeId4" Visible="false" Text='<%#Eval("PaymentModeId4") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="PlblPaymentNo4" Visible="false" Text='<%#Eval("PaymentNo4") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="PlblPaymentDate4" Visible="false" Text='<%#Eval("PaymentDate4") %>' runat="server"></asp:Label>
                                                         <asp:Label ID="PlblSSRDId" Visible="false"  runat="server" Text='<%# Eval("SSRDId") %>'></asp:Label>
                                                        <asp:Label ID="PlblRemark" Visible="false"  runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PlblDelivary_Date" Text='<%#Eval("Delivary_Date") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Distributor Name" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDName" Text='<%# Eval("DName") + "-  " + Eval("RName") %>' runat="server"></asp:Label>
                                                         <asp:Label ID="PlblAreaID" Visible="false" Text='<%#Eval("AreaId") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="PlblRouteId" Visible="false" Text='<%#Eval("RouteId") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="PlblDistributorId" Visible="false" Text='<%#Eval("DistributorId") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="lblSuperStockistId" Visible="false" Text='<%#Eval("SuperStockistId") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                              <%--  <asp:TemplateField HeaderText="Route" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRName" Text='<%#Eval("RName") %>' runat="server"></asp:Label>
                                                       
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="BillNo" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBillNo" Text='<%#Eval("BillNo") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Bill Amount" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalBillAmount" Text='<%#Eval("TotalBillAmount") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTBillAmt" Font-Bold="true" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total GST Amount" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalGSTAmount" Text='<%#Eval("TotalGSTAmount") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblFGSTAmt" Font-Bold="true" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total TcsTax Amount" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalTcsTaxAmt" Text='<%#Eval("TotalTcsTaxAmt") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblFTcsTaxAmt" Font-Bold="true" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Payment Amt1" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PlblPaymentAmount1" Text='<%#Eval("PaymentAmount1") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="PlblFPA1" Font-Bold="true" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Payment Amt2" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PlblPaymentAmount2" Text='<%#Eval("PaymentAmount2") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="PlblFPA2" Font-Bold="true" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Payment Amt3" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PlblPaymentAmount3" Text='<%#Eval("PaymentAmount3") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="PlblFPA3" Font-Bold="true" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Payment Amt4" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PlblPaymentAmount4" Text='<%#Eval("PaymentAmount4") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="PlblFPA4" Font-Bold="true" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Payment" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalPaidPayment" Text='<%#Eval("TotalPaidAmt") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="FlblFTPA" Font-Bold="true" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("ProductPaymentSheet_ID") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
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
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
    </script>
</asp:Content>


