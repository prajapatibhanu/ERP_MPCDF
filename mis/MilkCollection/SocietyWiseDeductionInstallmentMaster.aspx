<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SocietyWiseDeductionInstallmentMaster.aspx.cs" Inherits="mis_MilkCollection_SocietyWiseDeductionInstallmentMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <%--Confirmation Modal Start --%>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSubmit_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Deduction Installment Master</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>Deduction Installment Master</legend>
                                <div class="row">
                                      <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Date  </label>
                                        <span style="color: red">*</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ValidationGroup="Save" Display="Dynamic" ControlToValidate="txtEntryDate" ErrorMessage="Please Enter  Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter  Date !'></i>"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revdate" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtEntryDate" ErrorMessage="Invalid  Date" Text="<i class='fa fa-exclamation-circle' title='Invalid  Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                        </span>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtEntryDate"  data-date-end-date="0d" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select From Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                     <div class="col-md-2">
                                    <label>Milk Collection Unit<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ValidationGroup="Save" ControlToValidate="ddlMilkCollectionUnit" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Milk Collection Unit!'></i>" ErrorMessage="Select Milk Collection Unit" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
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
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Save"
                                                    InitialValue="0" ErrorMessage="Select Society"  Text="<i class='fa fa-exclamation-circle' title='Select Society !'></i>"
                                                    ControlToValidate="ddlSociety" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                    <div class="form-group">
                                      
                                        <asp:DropDownList ID="ddlSociety" CssClass="form-control select2" runat="server">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                     <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Head Name<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Save"
                                                    InitialValue="0" ErrorMessage="Select Head Name" Text="<i class='fa fa-exclamation-circle' title='Select Head Name !'></i>"
                                                    ControlToValidate="ddlHeaddetails" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlHeaddetails" OnInit="ddlHeaddetails_Init" Width="150px" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Amount<span style="color: red">*</span></label>
                                            <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ValidationGroup="Save" Display="Dynamic" ControlToValidate="txtAmount" ErrorMessage="Please Enter Amount." Text="<i class='fa fa-exclamation-circle' title='Please Enter Amount !'></i>"></asp:RequiredFieldValidator>  
                                        </span>
                                             <asp:TextBox ID="txtAmount" ClientIDMode="Static" runat="server" CssClass="form-control" onkeypress="return validateDec(this,event)" onchange="AmountperInstallment(),NoofInstallment()"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Amount Per Installment<span style="color: red">*</span></label>
                                             <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Save" Display="Dynamic" ControlToValidate="txtAmountperInstallment" ErrorMessage="Please Enter Amount per Installment." Text="<i class='fa fa-exclamation-circle' title='Please Enter No of Installment !'></i>"></asp:RequiredFieldValidator>  
                                        </span>
                                            <asp:TextBox ID="txtAmountperInstallment" ClientIDMode="Static" runat="server" CssClass="form-control" onkeypress="return validateNum(event)" onchange="NoofInstallment()"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>No of Installment<span style="color: red">*</span></label>
                                             <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvnoofinstallment" runat="server" ValidationGroup="Save" Display="Dynamic" ControlToValidate="txtnoofinstallment" ErrorMessage="Please Enter No of Installment." Text="<i class='fa fa-exclamation-circle' title='Please Enter No of Installment !'></i>"></asp:RequiredFieldValidator>  
                                        </span>
                                            <asp:TextBox ID="txtnoofinstallment" ClientIDMode="Static" runat="server" CssClass="form-control" onkeypress="return validateNum(event)" onchange="AmountperInstallment()"></asp:TextBox>
                                        </div>
                                    </div>
                                      <div class="col-md-2" style="margin-top: 20px;" runat="server">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-primary" OnClientClick="return ValidatePage();" OnClick="btnSubmit_Click" ValidationGroup="Save" ID="btnSubmit" Text="Save" AccessKey="S" />
                                    </div>
                            </div>
                                </div>
                            </fieldset>
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Deduction Installment Detail</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvDetail" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRow" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Entry Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEntryDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Society">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                            <asp:Label ID="lblOffice_ID" Visible="false" runat="server" Text='<%# Eval("Office_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Head Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemBillingHead_Name" runat="server" Text='<%# Eval("ItemBillingHead_Name") %>'></asp:Label>
                                                            <asp:Label ID="lblItemBillingHead_ID" Visible="false" runat="server" Text='<%# Eval("ItemBillingHead_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Amount Per Installment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("AmountPerInstallment") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total No of Installment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalNoofInstallment" runat="server" Text='<%# Eval("TotalNoofInstallment") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No of Installment Deducted">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNoofInstallmentDeducted" runat="server" Text='<%# Eval("NoofInstallmentDeducted") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No of Installment Pending">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNoofInstallmentPending" runat="server" Text='<%# Eval("NoofInstallmentPending") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
     <script type="text/javascript">
         function ValidatePage() {

             if (typeof (Page_ClientValidate) == 'function') {
                 Page_ClientValidate('Save');
             }

             if (Page_IsValid) {

                 if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
         }
         function NoofInstallment()
         {
             debugger;
             var Amount = document.getElementById('<%=txtAmount.ClientID%>').value;
             var AmountperInstallment = document.getElementById('<%=txtAmountperInstallment.ClientID%>').value;
             var NoofInstallment = document.getElementById('<%=txtnoofinstallment.ClientID%>').value;

             if (Amount == "")
                 Amount = 0;
             if (AmountperInstallment == "")
                 AmountperInstallment = 0;
             if (AmountperInstallment != "0" && Amount !="0")
             {
                 NoofInstallment = Amount / AmountperInstallment
                 document.getElementById('<%=txtnoofinstallment.ClientID%>').value = NoofInstallment;
             }
                 
            
             
         }
         function AmountperInstallment() {
             debugger;
             var Amount = document.getElementById('<%=txtAmount.ClientID%>').value;
             var AmountperInstallment = document.getElementById('<%=txtAmountperInstallment.ClientID%>').value;
             var NoofInstallment = document.getElementById('<%=txtnoofinstallment.ClientID%>').value;

             if (Amount == "")
                 Amount = 0;
             if (NoofInstallment == "")
                 NoofInstallment = 0;
             if (NoofInstallment != "0" && Amount != "0")
             {
                 AmountperInstallment = Amount / NoofInstallment
                 document.getElementById('<%=txtAmountperInstallment.ClientID%>').value = AmountperInstallment;
             }



         }
    </script>
</asp:Content>

