<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MilkRetailerPaymentSheetEntry.aspx.cs" Inherits="mis_Demand_MilkRetailerPaymentSheetEntry" %>

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
                    <h3 class="box-title">Retailer Payment Sheet</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <div class="box-body">
                    <fieldset>
                        <legend>Retailer Payment Sheet</legend>
                       <div class="row">

                     
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Date<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                            ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                            ControlToValidate="txtDeliveryDate" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtDeliveryDate"
                                            ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDeliveryDate" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                             <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Retailer</label>
                                             <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                        InitialValue="0" ErrorMessage="Select Retailer" Text="<i class='fa fa-exclamation-circle' title='Select Retailer !'></i>"
                                        ControlToValidate="ddlRetailer" ForeColor="Red" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlRetailer" AutoPostBack="true" OnSelectedIndexChanged="ddlRetailer_SelectedIndexChanged" runat="server" CssClass="form-control select2"></asp:DropDownList>
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
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Payment Mode<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvPaymentMode" ValidationGroup="a"
                                            ErrorMessage="Select Payment Mode" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Payment Mode !'></i>"
                                            ControlToValidate="ddlPaymentMode" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Payment No<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvNo" ValidationGroup="a"
                                            ErrorMessage="Enter Payment No." Text="<i class='fa fa-exclamation-circle' title='Enter Payment No. !'></i>"
                                            ControlToValidate="txtPaymentNo" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:TextBox ID="txtPaymentNo" runat="server" CssClass="form-control" MaxLength="80" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                   <label>Payment Amt<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvAmount" ValidationGroup="a"
                                            ErrorMessage="Enter Payment Amt" Text="<i class='fa fa-exclamation-circle' title='Enter Payment Amt !'></i>"
                                            ControlToValidate="txtPaymentAmt" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:TextBox ID="txtPaymentAmt" runat="server" CssClass="form-control" onkeypress="return validateDec(this,event)" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Payment Date<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a"
                                            ErrorMessage="Enter Payment Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Payment Date !'></i>"
                                            ControlToValidate="txtPaymentDate" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtPaymentDate"
                                            ErrorMessage="Invalid Payment Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Payment Date !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtPaymentDate" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
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
                                    <asp:TextBox ID="txtRemark" autocomplete="off" runat="server" MaxLength="200" TextMode="MultiLine" Rows="1" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                             </div>
                       <div class="row">
                        <div class="col-md-1" style="margin-top: 20px;">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSave" Text="Save" OnClientClick="return ValidatePage();" />

                            </div>
                        </div>
                        <div class="col-md-1" style="margin-top: 20px;">
                            <div class="form-group">
                                <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-block btn btn-default" />
                            </div>
                        </div>
                             </div>
                    </fieldset>

                </div>
                <div class="box-body">
                    <fieldset>
                        <legend>Retailer Payment Sheet Detail</legend>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Date </label>
                                <span class="pull-right">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtDateFilter"
                                        ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                        ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                </span>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDateFilter" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Retailer Name </label>
                                <asp:DropDownList ID="ddlFilterRetailer" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:Button runat="server" CssClass="btn btn-primary" CausesValidation="true" ValidationGroup="vgsa" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />

                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered" DataKeyNames="RetailerPaymentSheet_ID"
                             ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" AutoGenerateColumns="false" OnRowCommand="GridView1_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delivery Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDelivary_Date" runat="server" Text='<%# Eval("Delivary_Date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Retailer">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRetailer" runat="server" Text='<%# Eval("BName") %>'></asp:Label>
                                         <asp:Label ID="lblBoothId" Visible="false" runat="server" Text='<%# Eval("BoothId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Milk Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMilkAmount" runat="server" Text='<%# Eval("MilkAmount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Mode">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPaymentModeName" runat="server" Text='<%# Eval("PaymentModeName") %>'></asp:Label>
                                         <asp:Label ID="lblPaymentModeId" Visible="false" runat="server" Text='<%# Eval("PaymentModeId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPaymentNo" runat="server" Text='<%# Eval("PaymentNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPaymentAmount" runat="server" Text='<%# Eval("PaymentAmount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPaymentDate" runat="server" Text='<%# Eval("PaymentDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remark">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="EditRecord" CommandArgument='<%# Eval("RetailerPaymentSheet_ID") %>'><i class="fa fa-edit"></i></asp:LinkButton>
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
               debugger;
               if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
               }

               if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
                   document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                   $('#myModal').modal('show');
                   return false;
               }
            }
       }
       </script>
</asp:Content>
