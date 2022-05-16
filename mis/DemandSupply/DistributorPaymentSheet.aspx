<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DistributorPaymentSheet.aspx.cs" Inherits="mis_DemandSupply_DistributorPaymentSheet" %>

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
                    <h3 class="box-title">DISTRIBUTOR PAYMENT SHEET</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <div class="box-body">
                    <fieldset>
                        <legend>DISTRIBUTOR PAYMENT SHEET</legend>
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
                                    <label>Location<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select Location" Text="<i class='fa fa-exclamation-circle' title='Select Location !'></i>"
                                            ControlToValidate="ddlLocation" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                    </asp:DropDownList>
                                </div>

                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Route<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select Route" Text="<i class='fa fa-exclamation-circle' title='Select Route !'></i>"
                                            ControlToValidate="ddlRoute" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlRoute" AutoPostBack="true" OnSelectedIndexChanged="ddlRoute_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Distributor Name <span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select Distributor/Superstockist Name" Text="<i class='fa fa-exclamation-circle' title='Select Distributor/Superstockist Name !'></i>"
                                            ControlToValidate="ddlDitributor" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlDitributor" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlDitributor_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>


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
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ForeColor="Red"
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
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" ForeColor="Red"
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
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator11" ForeColor="Red"
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
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>5) Payment Mode</label>
                                    <asp:DropDownList ID="ddlPaymentMode5" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>5) Payment No</label>
                                    <asp:TextBox ID="txtPaymentNo5" runat="server" CssClass="form-control" MaxLength="80" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>5) Payment Amt</label>
                                    <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator12" ForeColor="Red"
                                            ValidationGroup="a" Display="Dynamic" runat="server"
                                            ControlToValidate="txtPaymentAmt5"
                                            ErrorMessage="Invalid Payment Amt1"
                                            Text="<i class='fa fa-exclamation-circle' title='Invalid Payment Amt1 !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^[0-9.-]+$">

                                        </asp:RegularExpressionValidator>

                                    </span>
                                    <asp:TextBox ID="txtPaymentAmt5" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>5) Payment Date <span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator13" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtPaymentDate5"
                                            ErrorMessage="Invalid Payment Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Payment Date !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtPaymentDate5" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>6) Payment Mode</label>
                                    <asp:DropDownList ID="ddlPaymentMode6" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>6) Payment No</label>
                                    <asp:TextBox ID="txtPaymentNo6" runat="server" CssClass="form-control" MaxLength="80" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>6) Payment Amt</label>
                                    <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator14" ForeColor="Red"
                                            ValidationGroup="a" Display="Dynamic" runat="server"
                                            ControlToValidate="txtPaymentAmt6"
                                            ErrorMessage="Invalid Payment Amt1"
                                            Text="<i class='fa fa-exclamation-circle' title='Invalid Payment Amt1 !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^[0-9.-]+$">

                                        </asp:RegularExpressionValidator>

                                    </span>
                                    <asp:TextBox ID="txtPaymentAmt6" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>6) Payment Date <span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator15" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtPaymentDate6"
                                            ErrorMessage="Invalid Payment Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Payment Date !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtPaymentDate6" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>7) Payment Mode</label>
                                    <asp:DropDownList ID="ddlPaymentMode7" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>7) Payment No</label>
                                    <asp:TextBox ID="txtPaymentNo7" runat="server" CssClass="form-control" MaxLength="80" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>7) Payment Amt</label>
                                    <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator16" ForeColor="Red"
                                            ValidationGroup="a" Display="Dynamic" runat="server"
                                            ControlToValidate="txtPaymentAmt7"
                                            ErrorMessage="Invalid Payment Amt1"
                                            Text="<i class='fa fa-exclamation-circle' title='Invalid Payment Amt1 !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^[0-9.-]+$">

                                        </asp:RegularExpressionValidator>

                                    </span>
                                    <asp:TextBox ID="txtPaymentAmt7" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>7) Payment Date <span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator17" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtPaymentDate7"
                                            ErrorMessage="Invalid Payment Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Payment Date !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtPaymentDate7" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>8) Payment Mode</label>
                                    <asp:DropDownList ID="ddlPaymentMode8" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>8) Payment No</label>
                                    <asp:TextBox ID="txtPaymentNo8" runat="server" CssClass="form-control" MaxLength="80" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>8) Payment Amt</label>
                                    <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator18" ForeColor="Red"
                                            ValidationGroup="a" Display="Dynamic" runat="server"
                                            ControlToValidate="txtPaymentAmt8"
                                            ErrorMessage="Invalid Payment Amt1"
                                            Text="<i class='fa fa-exclamation-circle' title='Invalid Payment Amt1 !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^[0-9.-]+$">

                                        </asp:RegularExpressionValidator>

                                    </span>
                                    <asp:TextBox ID="txtPaymentAmt8" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>8) Payment Date <span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator19" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtPaymentDate8"
                                            ErrorMessage="Invalid Payment Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Payment Date !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtPaymentDate8" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>9) Payment Mode</label>
                                    <asp:DropDownList ID="ddlPaymentMode9" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>9) Payment No</label>
                                    <asp:TextBox ID="txtPaymentNo9" runat="server" CssClass="form-control" MaxLength="80" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>9) Payment Amt</label>
                                    <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator20" ForeColor="Red"
                                            ValidationGroup="a" Display="Dynamic" runat="server"
                                            ControlToValidate="txtPaymentAmt9"
                                            ErrorMessage="Invalid Payment Amt1"
                                            Text="<i class='fa fa-exclamation-circle' title='Invalid Payment Amt1 !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^[0-9.-]+$">

                                        </asp:RegularExpressionValidator>

                                    </span>
                                    <asp:TextBox ID="txtPaymentAmt9" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>9) Payment Date <span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator21" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtPaymentDate9"
                                            ErrorMessage="Invalid Payment Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Payment Date !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtPaymentDate9" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>10) Payment Mode</label>
                                    <asp:DropDownList ID="ddlPaymentMode10" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>10) Payment No</label>
                                    <asp:TextBox ID="txtPaymentNo10" runat="server" CssClass="form-control" MaxLength="80" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>10) Payment Amt</label>
                                    <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator22" ForeColor="Red"
                                            ValidationGroup="a" Display="Dynamic" runat="server"
                                            ControlToValidate="txtPaymentAmt10"
                                            ErrorMessage="Invalid Payment Amt1"
                                            Text="<i class='fa fa-exclamation-circle' title='Invalid Payment Amt1 !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^[0-9.-]+$">

                                        </asp:RegularExpressionValidator>

                                    </span>
                                    <asp:TextBox ID="txtPaymentAmt10" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>10) Payment Date <span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator23" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtPaymentDate10"
                                            ErrorMessage="Invalid Payment Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Payment Date !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtPaymentDate10" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSave" Text="Save" OnClick="btnSave_Click" />

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
                        <legend>DISTRIBUTOR PAYMENT SHEET DETAIL</legend>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Date <%--/ दिनांक--%></label>
                                <span class="pull-right">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtDateFilter"
                                        ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                        ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                </span>
                                <%--<asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOrderDate" OnTextChanged="txtOrderDate_TextChanged" AutoPostBack="true" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>--%>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDateFilter" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Location</label>

                                <asp:DropDownList ID="ddlLocationfilter" AutoPostBack="true" OnSelectedIndexChanged="ddlLocationfilter_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Route</label>
                                <asp:DropDownList ID="ddlRoutefilter" runat="server" CssClass="form-control select2">
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
                                            <asp:Label ID="lblPaymentModeId5" Visible="false" Text='<%#Eval("PaymentModeId5") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPaymentNo5" Visible="false" Text='<%#Eval("PaymentNo5") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPaymentDate5" Visible="false" Text='<%#Eval("PaymentDate5") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPaymentModeId6" Visible="false" Text='<%#Eval("PaymentModeId6") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPaymentNo6" Visible="false" Text='<%#Eval("PaymentNo6") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPaymentDate6" Visible="false" Text='<%#Eval("PaymentDate6") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPaymentModeId7" Visible="false" Text='<%#Eval("PaymentModeId7") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPaymentNo7" Visible="false" Text='<%#Eval("PaymentNo7") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPaymentDate7" Visible="false" Text='<%#Eval("PaymentDate7") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPaymentModeId8" Visible="false" Text='<%#Eval("PaymentModeId8") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPaymentNo8" Visible="false" Text='<%#Eval("PaymentNo8") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPaymentDate8" Visible="false" Text='<%#Eval("PaymentDate8") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPaymentModeId9" Visible="false" Text='<%#Eval("PaymentModeId9") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPaymentNo9" Visible="false" Text='<%#Eval("PaymentNo9") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPaymentDate9" Visible="false" Text='<%#Eval("PaymentDate9") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPaymentModeId10" Visible="false" Text='<%#Eval("PaymentModeId10") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPaymentNo10" Visible="false" Text='<%#Eval("PaymentNo10") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPaymentDate10" Visible="false" Text='<%#Eval("PaymentDate10") %>' runat="server"></asp:Label>

                                            <asp:Label ID="lblRemark" Visible="false" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delivery Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDelivary_Date" runat="server" Text='<%# Eval("Delivary_Date") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Route">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRName" runat="server" Text='<%# Eval("RName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Distributor">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDistributor" runat="server" Text='<%# Eval("DName") %>'></asp:Label>
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

                                    <asp:TemplateField HeaderText="Payment Amt5" HeaderStyle-Width="5px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaymentAmount5" Text='<%#Eval("PaymentAmount5") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFPA5" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment Amt6" HeaderStyle-Width="5px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaymentAmount6" Text='<%#Eval("PaymentAmount6") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFPA6" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment Amt7" HeaderStyle-Width="5px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaymentAmount7" Text='<%#Eval("PaymentAmount7") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFPA7" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment Amt8" HeaderStyle-Width="5px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaymentAmount8" Text='<%#Eval("PaymentAmount8") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFPA8" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment Amt9" HeaderStyle-Width="5px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaymentAmount9" Text='<%#Eval("PaymentAmount9") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFPA9" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment Amt10" HeaderStyle-Width="5px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaymentAmount10" Text='<%#Eval("PaymentAmount10") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFPA10" runat="server"></asp:Label>
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
                        </div>
                    </fieldset>

                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
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

