<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Trn_MIS_ReportEntry.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="mis_Mis_Reports_Trn_MIS_ReportEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="mpr" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Daily MIS</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

                <div class="box-body">
                    <div class="row">

                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Date <span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="mpr" ControlToValidate="txtEntryDate"
                                            ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                            Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="mpr" runat="server" Display="Dynamic" ControlToValidate="txtEntryDate"
                                            ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtEntryDate" OnTextChanged="txtEntryDate_TextChanged" AutoPostBack="true" onkeypress="javascript: return false;" Width="100%" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Office Name<span style="color: red;">*</span></label>
                                    <asp:DropDownList ID="ddlOffice" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div id="MISFrom" runat="server">
                            <fieldset>
                                <legend>Daily MIS</legend>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblMilkPurchaseRate" runat="server" Text=""></asp:Label>
                                        </div>

                                        <table class="table table-bordered table-hover">
                                            <tr>
                                                <th colspan="6">MILK PURCHASE RATE</th>

                                            </tr>
                                            <tr>
                                                <th>Buff (Kg FAT) <span style="color: red;">*</span>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="mpr" ControlToValidate="txtBuff_KgFAT"
                                                            ErrorMessage="Enter Buff (Kg FAT)" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Buff (Kg FAT) !'></i>"
                                                            Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>
                                                    </span>
                                                </th>
                                                <th>Cow (Kg TS)<span style="color: red;"> *</span>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="mpr" ControlToValidate="txtCow_KgTS"
                                                            ErrorMessage="Enter Cow (Kg TS)" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Cow (Kg TS) !'></i>"
                                                            Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>
                                                    </span>
                                                </th>
                                                <th>Comm (Kg FAT)<span style="color: red;"> *</span>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="mpr" ControlToValidate="txtComm_KgFAT"
                                                            ErrorMessage="Enter Comm (Kg FAT)" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Comm (Kg FAT) !'></i>"
                                                            Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>
                                                    </span>
                                                </th>
                                                <th>Payment Upto <span style="color: red;">*</span>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="mpr" ControlToValidate="txtPaymentUpto"
                                                            ErrorMessage="Enter Payment Upto" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Payment Upto !'></i>"
                                                            Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>

                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="mpr" runat="server" Display="Dynamic" ControlToValidate="txtPaymentUpto"
                                                            ErrorMessage="Invalid Payment Upto" Text="<i class='fa fa-exclamation-circle' title='Invalid Payment Upto !'></i>" SetFocusOnError="true"
                                                            ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                                    </span>
                                                </th>
                                                <th>Dues Amount (Rs in Lkh) <span style="color: red;">*</span>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="mpr" ControlToValidate="txtDuesAmount"
                                                            ErrorMessage="Enter Dues Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Dues Amount !'></i>"
                                                            Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>
                                                    </span>
                                                </th>
                                                <th>Effective Date <span style="color: red;">*</span>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="mpr" ControlToValidate="txtEffectiveDate"
                                                            ErrorMessage="Select Effective Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Effective Date !'></i>"
                                                            Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>

                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="mpr" runat="server" Display="Dynamic" ControlToValidate="txtEffectiveDate"
                                                            ErrorMessage="Invalid Effective Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Effective Date!'></i>" SetFocusOnError="true"
                                                            ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                                    </span>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtBuff_KgFAT" CssClass="form-control" runat="server" MaxLength="15"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtCow_KgTS" CssClass="form-control" runat="server" MaxLength="15"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtComm_KgFAT" CssClass="form-control" runat="server" MaxLength="15"></asp:TextBox></td>
                                                <td>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtPaymentUpto" onkeypress="javascript: return false;" Width="100%" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDuesAmount" CssClass="form-control" runat="server" MaxLength="15"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtEffectiveDate" onkeypress="javascript: return false;" Width="100%" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                                    </div>
                                                </td>
                                            </tr>


                                        </table>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:LinkButton ID="lnkMilkPurchaseRate" OnClick="lnkMilkPurchaseRate_Click" ClientIDMode="Static" ValidationGroup="mpr" CssClass="btn btn-info" Text="Save" runat="server"></asp:LinkButton>
                                        </div>
                                    </div>

                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblMilkSaleRate" runat="server" Text=""></asp:Label>
                                        </div>
                                        <asp:GridView ID="gvMilkSaleRate" DataKeyNames="Item_id" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"
                                            CssClass="table table-bordered table-hover" runat="server" OnRowCreated="gvMilkSaleRate_RowCreated">
                                            <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PARTICULARS" HeaderStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItem_id" Visible="false" Text='<%# Eval("Item_id") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="lblItemName" Text='<%# Eval("ItemName") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtItemRate" Text='<%# Eval("ItemRate") %>' autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Button ID="btnMilkSaleRate" OnClick="btnMilkSaleRate_Click" runat="server" CssClass="btn btn-info" Text="Save" />
                                        </div>

                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblOwnProcurement" runat="server" Text=""></asp:Label>
                                        </div>
                                        <asp:GridView ID="gvOwnProcurement" DataKeyNames="Office_ID" ClientIDMode="Static" ShowHeaderWhenEmpty="true" OnRowCreated="gvOwnProcurement_RowCreated" AutoGenerateColumns="false" ShowFooter="true"
                                            CssClass="table table-bordered table-hover" runat="server">
                                            <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PARTICULARS" HeaderStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOffice_ID" Visible="false" Text='<%# Eval("Office_ID") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="lblOfficeName" Text='<%# Eval("Office_Name") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <b>Total</b>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="QUANTITY (In KG)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQty" onfocusout="FetchData(this)" Text='<%# Eval("Qty").ToString() %>' autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblFTotalQty" runat="server" Font-Bold="true"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FAT %">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtFAT" onfocusout="FetchData(this)" autocomplete="off" Text='<%# Eval("FAT_Per").ToString() %>' onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblFAvgFat" runat="server" Font-Bold="true"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SNF %">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtSNF" onfocusout="FetchData(this)" autocomplete="off" Text='<%# Eval("SNF_Per").ToString() %>' onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblFAvgSNF" runat="server" Font-Bold="true"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Button ID="btnSaveForOwnProcurement" OnClick="btnSaveForOwnProcurement_Click" runat="server" CssClass="btn btn-info" Text="Save" />
                                        </div>

                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblOtherUnionPartyProcurement" runat="server" Text=""></asp:Label>
                                        </div>
                                        <asp:GridView ID="gvOtherUnionProcurement" ClientIDMode="Static" ShowHeaderWhenEmpty="true" OnRowCreated="gvOtherUnionProcurement_RowCreated" DataKeyNames="ThirdPartyUnion_Id" AutoGenerateColumns="false" ShowFooter="true"
                                            CssClass="table table-bordered table-hover" runat="server">
                                            <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PARTICULARS" HeaderStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblThirdPartyUnion_Id" Visible="false" Text='<%# Eval("ThirdPartyUnion_Id") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="lblThirdPartyUnion_Name" Text='<%# Eval("ThirdPartyUnion_Name") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <b>Total</b>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="QUANTITY (In KG)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQty" onfocusout="FetchData1(this)" Text='<%# Eval("Qty").ToString() %>' autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblFTotalQty" runat="server" Font-Bold="true"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FAT %">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtFAT" onfocusout="FetchData1(this)" autocomplete="off" Text='<%# Eval("FAT_Per").ToString() %>' onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblFAvgFat" runat="server" Font-Bold="true"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SNF %">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtSNF" onfocusout="FetchData1(this)" autocomplete="off" Text='<%# Eval("SNF_Per").ToString() %>' onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblFAvgSNF" runat="server" Font-Bold="true"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Button ID="btnOtherUnionProcureMent" OnClick="btnOtherUnionProcureMent_Click" runat="server" CssClass="btn btn-info" Text="Save" />
                                        </div>

                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblConversion" runat="server" Text=""></asp:Label>
                                        </div>
                                        <asp:GridView ID="gvConversion" DataKeyNames="ThirdPartyUnion_Id" ShowHeaderWhenEmpty="true" ClientIDMode="Static" OnRowCreated="gvConversion_RowCreated" AutoGenerateColumns="false" ShowFooter="true"
                                            CssClass="table table-bordered table-hover" runat="server">
                                            <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PARTICULARS" HeaderStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblThirdPartyUnion_Id" Visible="false" Text='<%# Eval("ThirdPartyUnion_Id") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="lblThirdPartyUnion_Name" Text='<%# Eval("ThirdPartyUnion_Name") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <b>Total</b>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="QUANTITY (In KG)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQty" onfocusout="FetchData2(this)" Text='<%# Eval("Qty").ToString() %>' autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblFTotalQty" runat="server" Font-Bold="true"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SMP QTY (In KG)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtSMPQTY" onfocusout="FetchData2(this)" Text='<%# Eval("SMP_QTY").ToString() %>' autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblFSMPQTY" runat="server" Font-Bold="true"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="WB QTY (In KG)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtWBQTY" onfocusout="FetchData2(this)" autocomplete="off" Text='<%# Eval("WB_QTY").ToString() %>' onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblFWBQTY" runat="server" Font-Bold="true"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Button ID="btnConverstion" OnClick="btnConverstion_Click" runat="server" CssClass="btn btn-info" Text="Save" />
                                        </div>

                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblCFPAccounting" runat="server" Text=""></asp:Label>
                                        </div>
                                        <asp:GridView ID="gvCFPAccouting" ClientIDMode="Static" ShowHeaderWhenEmpty="true" OnRowCreated="gvCFPAccouting_RowCreated" AutoGenerateColumns="false" ShowFooter="true"
                                            CssClass="table table-bordered table-hover" runat="server">
                                            <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PARTICULARS" HeaderStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemName" Text='<%# Eval("ItemName") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <b>Total</b>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PRODUCTION">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtProduction" onfocusout="FetchData3(this)" autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblFProduction" runat="server" Font-Bold="true"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DCS (Sale)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDCS_Sale" onfocusout="FetchData3(this)" autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblFDCS_Sale" runat="server" Font-Bold="true"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OTHERS (Sale)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtOther_Sale" onfocusout="FetchData3(this)" autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblOtherSale" runat="server" Font-Bold="true"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Button ID="btnCFPAccounting" OnClick="btnCFPAccounting_Click" runat="server" CssClass="btn btn-info" Text="Save" />
                                        </div>

                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblGheeAccount" runat="server" Text=""></asp:Label>
                                        </div>
                                        <table class="table table-bordered table-hover">
                                            <tr>
                                                <th colspan="2">GHEE ACCOUNT</th>

                                            </tr>
                                            <tr>
                                                <th>Particular</th>
                                                <th>Stock Position (In KG)</th>
                                            </tr>
                                            <tr>
                                                <td>Ghee Pkt</td>
                                                <td>
                                                    <asp:TextBox ID="txtGheePkt" onfocusout="return gheesum();" ClientIDMode="Static" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>Ghee Loose</td>
                                                <td>
                                                    <asp:TextBox ID="txtGheeLosse" onfocusout="return gheesum();" ClientIDMode="Static" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td><b>Total</b></td>
                                                <td>
                                                    <asp:Label ID="lblGheeAccountTotal" Font-Bold="true" ClientIDMode="Static" runat="server"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Button ID="btnGheeAccount" OnClick="btnGheeAccount_Click" runat="server" CssClass="btn btn-info" Text="Save" />
                                        </div>

                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblComoditiesConsumption" runat="server" Text=""></asp:Label>
                                        </div>
                                        <table class="table table-bordered table-hover">
                                            <tr>
                                                <th colspan="3">COMODITIES CONSUMPTION</th>

                                            </tr>
                                            <tr>
                                                <th>Particular</th>
                                                <th>SMP QTY.(In KG)</th>
                                                <th>WB QTY.(In KG)</th>
                                            </tr>
                                            <tr>
                                                <td>For Milk</td>
                                                <td>
                                                    <asp:TextBox ID="txtSMP_QTY1" autocomplete="off" onfocusout="return comodotiesconsumption();" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtWB_QTY1" autocomplete="off" onfocusout="return comodotiesconsumption();" runat="server" CssClass="form-control"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>For Other</td>
                                                <td>
                                                    <asp:TextBox ID="txtSMP_QTY2" autocomplete="off" onfocusout="return comodotiesconsumption();" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtWB_QTY2" autocomplete="off" onfocusout="return comodotiesconsumption();" runat="server" CssClass="form-control"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>Total</td>
                                                <td><b>
                                                    <asp:Label ID="lblFSMP_QTY" runat="server"></asp:Label></b></td>
                                                <td><b>
                                                    <asp:Label ID="lblWB_QTY2" runat="server"></asp:Label></b></td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Button ID="btnCommoditiesConsumption" OnClick="btnCommoditiesConsumption_Click" runat="server" CssClass="btn btn-info" Text="Save" />
                                        </div>

                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblComoditiesstockposition" runat="server" Text=""></asp:Label>
                                        </div>
                                        <asp:GridView ID="gvComoditiesStockPosition" ShowHeaderWhenEmpty="true" OnRowCreated="gvComoditiesStockPosition_RowCreated" DataKeyNames="ThirdPartyUnion_Id" AutoGenerateColumns="false" ShowFooter="true"
                                            CssClass="table table-bordered table-hover" runat="server">
                                            <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PARTICULARS" HeaderStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblThirdPartyUnion_Id" Visible="false" Text='<%# Eval("ThirdPartyUnion_Id") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="lblThirdPartyUnion_Name" Text='<%# Eval("ThirdPartyUnion_Name") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <b>Total</b>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SMP QTY (In KG)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtSMPQTY" onfocusout="FetchData4(this)" Text='<%# Eval("Qty").ToString() %>' autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblFSMPQTY" runat="server" Font-Bold="true"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="WB QTY (In KG)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtWBQTY" onfocusout="FetchData4(this)" Text='<%# Eval("SMP_QTY").ToString() %>' autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblFWBQTY" runat="server" Font-Bold="true"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="WMP QTY (In KG)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtWMPQTY" onfocusout="FetchData4(this)" Text='<%# Eval("WB_QTY").ToString() %>' autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblFWMPQTY" runat="server" Font-Bold="true"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Button ID="btnComoditiesStockPosition" OnClick="btnComoditiesStockPosition_Click" runat="server" CssClass="btn btn-info" Text="Save" />
                                        </div>

                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblMilkConsumed" runat="server" Text=""></asp:Label>
                                        </div>
                                        <table class="table table-bordered table-hover">
                                            <tr>
                                                <th colspan="2">MILK CONSUMED IN INDIGNEOS PRODUCT MAKING (IN Kgs)</th>

                                            </tr>
                                            <tr>
                                                <th>Particular</th>
                                                <th>Quanity (In KG)</th>
                                            </tr>
                                            <tr>
                                                <td>Milk</td>
                                                <td>
                                                    <asp:TextBox ID="txtMilkConsumedIndigneos" ClientIDMode="Static" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox></td>
                                            </tr>


                                        </table>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Button ID="btnMilkConsumed" OnClick="btnMilkConsumed_Click" runat="server" CssClass="btn btn-info" Text="Save" />
                                        </div>

                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblIPProductSale" runat="server" Text=""></asp:Label>
                                        </div>
                                        <asp:GridView ID="gvIPProduct" ShowHeaderWhenEmpty="true" OnRowCreated="gvIPProduct_RowCreated" DataKeyNames="ProductId" ShowFooter="true" AutoGenerateColumns="false"
                                            CssClass="table table-bordered table-hover" runat="server">
                                            <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PARTICULARS" HeaderStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductId" Visible="false" Text='<%# Eval("ProductId") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="lblProductName" Text='<%# Eval("ProductName") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <b>Total</b>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sale (In KG)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtProductSaleInKG" onfocusout="IPproductSale(this)" autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblFTotalProductSale" runat="server" Font-Bold="true"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Button ID="btnIPProductSale" OnClick="btnIPProductSale_Click" runat="server" CssClass="btn btn-info" Text="Save" />
                                        </div>

                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblTownWiseMilkSale" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="table table-responsive">
                                            <asp:GridView ID="gvTownwiseMilkSale" ShowHeaderWhenEmpty="true" OnRowCreated="gvTownwiseMilkSale_RowCreated" OnRowDataBound="gvTownwiseMilkSale_RowDataBound" AutoGenerateColumns="false"
                                                CssClass="table table-bordered table-hover" runat="server">
                                                <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="CITY NAME" HeaderStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRouteHeadId" Visible="false" Text='<%# Eval("RouteHeadId") %>' runat="server"></asp:Label>
                                                            <asp:Label ID="lblRouteHeadName" Text='<%# Eval("RouteHeadName") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <b>Total</b>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Button ID="btnTownWiseMilkSale" OnClick="btnTownWiseMilkSale_Click" runat="server" CssClass="btn btn-info" Text="Save" />
                                            <%--  <asp:Button ID="btnGetTownWiseMilkSale" OnClientClick="return CalculateGrandTotal();" runat="server" CssClass="btn btn-info" Text="GetTotal" />--%>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">

        // for product for CC
        function FetchData(button) {
            debugger;
            var Qtytotal = 0, Fattotal = 0, SNFtotal = 0, Qty = 0, Fat = 0, SNF = 0, r = 0, FatPer = 0, SNFPer = 0;
            var rowcount = $('#gvOwnProcurement tr').length;
            $('#gvOwnProcurement tr').each(function (index) {
                debugger;
                if (r > 1 && r < (rowcount) - 1) {

                    debugger;
                    var Qty = $(this).children("td").eq(2).find('input[type="text"]').val();
                    var Fat = $(this).children("td").eq(3).find('input[type="text"]').val();
                    var SNF = $(this).children("td").eq(4).find('input[type="text"]').val();
                    if (Qty == "")
                        Qty = 0;

                    if (Fat == "")
                        Fat = 0;

                    if (SNF == "")
                        SNF = 0;

                    Qtytotal = parseFloat(parseFloat(Qty) + parseFloat(Qtytotal))
                    FatPer = parseFloat(parseFloat(Qty) * parseFloat(Fat)).toFixed(2)
                    Fattotal = parseFloat(parseFloat(FatPer) + parseFloat(Fattotal)).toFixed(2)
                    SNFPer = parseFloat(parseFloat(Qty) * parseFloat(SNF)).toFixed(2)
                    SNFtotal = parseFloat(parseFloat(SNFPer) + parseFloat(SNFtotal)).toFixed(2)
                }
                r++;
            });
            $("[id*=gvOwnProcurement] [id*=lblFTotalQty]").html(Qtytotal.toFixed(2));
            $("[id*=gvOwnProcurement] [id*=lblFAvgFat]").html((Fattotal / Qtytotal).toFixed(2));
            $("[id*=gvOwnProcurement] [id*=lblFAvgSNF]").html((SNFtotal / Qtytotal).toFixed(2));
        };

        function FetchData1(button) {
            debugger;
            var Qtytotal = 0, Fattotal = 0, SNFtotal = 0, Qty = 0, Fat = 0, SNF = 0, r = 0, FatPer = 0, SNFPer = 0;
            var rowcount1 = $('#gvOtherUnionProcurement tr').length;
            $('#gvOtherUnionProcurement tr').each(function (index) {
                debugger;
                if (r > 1 && r < (rowcount1) - 1) {
                    var Qty = $(this).children("td").eq(2).find('input[type="text"]').val();
                    var Fat = $(this).children("td").eq(3).find('input[type="text"]').val();
                    var SNF = $(this).children("td").eq(4).find('input[type="text"]').val();
                    if (Qty == "")
                        Qty = 0;

                    if (Fat == "")
                        Fat = 0;

                    if (SNF == "")
                        SNF = 0;

                    Qtytotal = parseFloat(parseFloat(Qty) + parseFloat(Qtytotal))
                    FatPer = parseFloat(parseFloat(Qty) * parseFloat(Fat)).toFixed(2)
                    Fattotal = parseFloat(parseFloat(FatPer) + parseFloat(Fattotal)).toFixed(2)
                    SNFPer = parseFloat(parseFloat(Qty) * parseFloat(SNF)).toFixed(2)
                    SNFtotal = parseFloat(parseFloat(SNFPer) + parseFloat(SNFtotal)).toFixed(2)
                }
                r++;
            });
            $("[id*=gvOtherUnionProcurement] [id*=lblFTotalQty]").html(Qtytotal.toFixed(2));
            $("[id*=gvOtherUnionProcurement] [id*=lblFAvgFat]").html((Fattotal / Qtytotal).toFixed(2));
            $("[id*=gvOtherUnionProcurement] [id*=lblFAvgSNF]").html((SNFtotal / Qtytotal).toFixed(2));
        };

        function FetchData2(button) {
            debugger;
            var Qtytotal = 0, SMPtotal = 0, WBtotal = 0, Qty = 0, r = 0;
            var rowcount2 = $('#gvConversion tr').length;
            $('#gvConversion tr').each(function (index) {
                debugger;
                if (r > 1 && r < (rowcount2) - 1) {

                    debugger;
                    var Qty = $(this).children("td").eq(2).find('input[type="text"]').val();
                    var SMP = $(this).children("td").eq(3).find('input[type="text"]').val();
                    var WB = $(this).children("td").eq(4).find('input[type="text"]').val();
                    if (Qty == "")
                        Qty = 0;

                    if (SMP == "")
                        SMP = 0;

                    if (WB == "")
                        WB = 0;

                    Qtytotal = parseFloat(parseFloat(Qty) + parseFloat(Qtytotal))
                    SMPtotal = parseFloat(parseFloat(SMP) + parseFloat(SMPtotal))
                    WBtotal = parseFloat(parseFloat(WB) + parseFloat(WBtotal))
                }
                r++;
            });
            $("[id*=gvConversion] [id*=lblFTotalQty]").html(Qtytotal.toFixed(2));
            $("[id*=gvConversion] [id*=lblFSMPQTY]").html(SMPtotal.toFixed(2));
            $("[id*=gvConversion] [id*=lblFWBQTY]").html(WBtotal.toFixed(2));
        };

        function FetchData3(button) {
            debugger;
            var Qtytotal = 0, TotalFDCS_Sale = 0, TotalOtherSale = 0, Qty = 0, r = 0;
            var rowcount3 = $('#gvCFPAccouting tr').length;
            $('#gvCFPAccouting tr').each(function (index) {
                debugger;
                if (r > 1 && r < (rowcount3) - 1) {

                    debugger;
                    var Qty = $(this).children("td").eq(2).find('input[type="text"]').val();
                    var FDCS_Sale = $(this).children("td").eq(3).find('input[type="text"]').val();
                    var OtherSale = $(this).children("td").eq(4).find('input[type="text"]').val();
                    if (Qty == "")
                        Qty = 0;

                    if (FDCS_Sale == "")
                        FDCS_Sale = 0;

                    if (OtherSale == "")
                        OtherSale = 0;

                    Qtytotal = parseFloat(parseFloat(Qty) + parseFloat(Qtytotal))
                    TotalFDCS_Sale = parseFloat(parseFloat(FDCS_Sale) + parseFloat(TotalFDCS_Sale))
                    TotalOtherSale = parseFloat(parseFloat(OtherSale) + parseFloat(TotalOtherSale))
                }
                r++;
            });
            $("[id*=gvCFPAccouting] [id*=lblFProduction]").html(Qtytotal.toFixed(2));
            $("[id*=gvCFPAccouting] [id*=lblFDCS_Sale]").html(TotalFDCS_Sale.toFixed(2));
            $("[id*=gvCFPAccouting] [id*=lblOtherSale]").html(TotalOtherSale.toFixed(2));
        };

        function FetchData4(button) {
            debugger;
            var SMPtotal = 0, WBtotal = 0, WMPtotal = 0, Qty = 0, r = 0;
            var rowcount4 = $('#gvComoditiesStockPosition tr').length;
            $('#gvComoditiesStockPosition tr').each(function (index) {
                debugger;
                if (r > 1 && r < (rowcount4) - 1) {

                    debugger;
                    var SMP = $(this).children("td").eq(2).find('input[type="text"]').val();
                    var WB = $(this).children("td").eq(3).find('input[type="text"]').val();
                    var WMP = $(this).children("td").eq(4).find('input[type="text"]').val();
                    if (SMP == "")
                        SMP = 0;

                    if (WB == "")
                        WB = 0;

                    if (WMP == "")
                        WMP = 0;

                    SMPtotal = parseFloat(parseFloat(SMP) + parseFloat(SMPtotal))
                    WBtotal = parseFloat(parseFloat(WB) + parseFloat(WBtotal))
                    WMPtotal = parseFloat(parseFloat(WMP) + parseFloat(WMPtotal))
                }
                r++;
            });
            $("[id*=gvComoditiesStockPosition] [id*=lblFTotalQty]").html(SMPtotal.toFixed(2));
            $("[id*=gvComoditiesStockPosition] [id*=lblFAvgFat]").html(WBtotal.toFixed(2));
            $("[id*=gvComoditiesStockPosition] [id*=lblFAvgSNF]").html(WMPtotal.toFixed(2));
        };

        function gheesum() {
            debugger;
            var txtFirstNumberValue = document.getElementById('<%= txtGheePkt.ClientID %>').value;
            var txtSecondNumberValue = document.getElementById('<%= txtGheeLosse.ClientID %>').value;

            if (txtFirstNumberValue == "") {
                txtFirstNumberValue = 0;
            }
            if (txtSecondNumberValue == "") {
                txtSecondNumberValue = 0;
            }

            var result = parseFloat(txtFirstNumberValue) + parseFloat(txtSecondNumberValue);
            if (!isNaN(result)) {
                document.getElementById('<%= lblGheeAccountTotal.ClientID %>').innerHTML = result;
            }

        }

        function comodotiesconsumption() {
            debugger;
            var SMP_QTY1 = document.getElementById('<%= txtSMP_QTY1.ClientID %>').value;
            var SMP_QTY2 = document.getElementById('<%= txtSMP_QTY2.ClientID %>').value;
            var WB_QTY1 = document.getElementById('<%= txtWB_QTY1.ClientID %>').value;
            var WB_QTY2 = document.getElementById('<%= txtWB_QTY2.ClientID %>').value;

            if (SMP_QTY1 == "") {
                SMP_QTY1 = 0;
            }
            if (SMP_QTY2 == "") {
                SMP_QTY2 = 0;
            }

            if (WB_QTY1 == "") {
                WB_QTY1 = 0;
            }
            if (WB_QTY2 == "") {
                WB_QTY2 = 0;
            }

            var result1 = parseFloat(SMP_QTY1) + parseFloat(SMP_QTY2);
            if (!isNaN(result1)) {
                document.getElementById('<%= lblFSMP_QTY.ClientID %>').innerHTML = result1;
            }

            var result2 = parseFloat(WB_QTY1) + parseFloat(WB_QTY2);
            if (!isNaN(result2)) {
                document.getElementById('<%= lblWB_QTY2.ClientID %>').innerHTML = result2;
            }
        }

        function IPproductSale(button) {
            debugger;


            // start IP Product Sale QTY total in footer
            var totalProductSale = 0;
            $($("[id*=gvIPProduct] [id*=txtProductSaleInKG]")).each(function () {
                if (!isNaN(parseFloat($(this).val()))) {
                    totalProductSale += parseFloat($(this).val());
                }
            });
            $("[id*=gvIPProduct] [id*=lblFTotalProductSale]").html(totalProductSale);

            // end of IP Product Sale total in footer

            return false;
        };
        function CalculateGrandTotal() {
            debugger;
            var i = 0;
            var Tval = 0;
            var Tval1 = 0;
            var rowcount = $('#gvTownwiseMilkSale tr').length;
            $('#gvTownwiseMilkSale tr').each(function (index) {

                if (i > 0 && i < rowcount - 1) {
                    var temp = Tval;
                    var val = $(this).children("td").eq(1).find('input[type="text"]').val();

                    if (val == "")
                        val = 0;

                    Tval = parseFloat(parseFloat(temp) + parseFloat(val)).toFixed(2)

                }
                i++;
            });
        }

    </script>
</asp:Content>

