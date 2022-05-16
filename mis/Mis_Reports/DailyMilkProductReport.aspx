<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DailyMilkProductReport.aspx.cs" Inherits="mis_MilkCollection_DailyMilkProductReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Daily Milk / Product Report</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row no-print">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Date<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Select Date!'></i>" ErrorMessage="Select Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <i class="far fa-calendar-alt"></i>
                                                </span>
                                            </div>
                                            <asp:TextBox ID="txtDate" autocomplete="off" placeholder="Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server" OnTextChanged="txtDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2 pull-right">
                                    <div class="form-group">
                                        <asp:Button ID="btnPrint" runat="server" style="margin-top : 22px;" Text="Print Report" CssClass="btn btn-info btn-block" OnClientClick="window.print();" />
                                    </div>
                                </div>
                            </div>
                            <div id="ReportDetail" runat="server">
                                <div class="row">
                                    <div class="col-md-6">
                                        <fieldset>
                                            <legend>A : Milk Procurement (KGPD)</legend>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:GridView ID="gvMilkProcurement" runat="server" ShowFooter="true" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S.No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Office Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOfficeName" runat="server" Text='<%#Eval("Office_Name").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Quantity">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtMProQuantity" runat="server" Text='<%# Eval("Quantity").ToString() %>' onkeypress="return validateDec(this,event)"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="% Charge *">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtMProCharge1" runat="server" Text='<%# Eval("Charge1").ToString() %>' onkeypress="return validateDec(this,event)"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="% Charge #">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtMProCharge2" runat="server" Text='<%# Eval("Charge2").ToString() %>' onkeypress="return validateDec(this,event)"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="col-md-6">
                                        <fieldset>
                                            <legend>B : Packet Milk Sale (LPD)</legend>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:GridView ID="gvPacketMilkSale" runat="server" ShowFooter="true" CssClass="table table-bordered" AutoGenerateColumns="false">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S.No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Office Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOfficeName" runat="server" Text='<%#Eval("Office_Name").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Quantity">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtPMQuantity" runat="server" Text='<%# Eval("Quantity").ToString() %>' onkeypress="return validateDec(this,event)"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="% Charge *">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtPMCharge1" runat="server" Text='<%# Eval("Charge1").ToString() %>' onkeypress="return validateDec(this,event)"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="% Charge #">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtPMCharge2" runat="server" Text='<%# Eval("Charge2").ToString() %>' onkeypress="return validateDec(this,event)"></asp:Label>
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
                                <div class="row">
                                    <div class="col-md-6">
                                        <fieldset>
                                            <legend>D : Main Product (KG & Ltr)</legend>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:GridView ID="gvMainProduct" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S.No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Product">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProduct" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Quantity">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtMPQuantity" runat="server" onkeypress="return validateDec(this,event)" Text='<%# Eval("Quantity").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="% Charge *">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtMPCharge1" runat="server" onkeypress="return validateDec(this,event)" Text='<%# Eval("Charge1").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="% Charge #">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtMPCharge2" runat="server" onkeypress="return validateDec(this,event)" Text='<%# Eval("Charge2").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="col-md-6">
                                        <fieldset>
                                            <legend>C : Bulk Milk Sale</legend>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:GridView ID="gvBulkMilkSale" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S.No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Office Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOfficeName" runat="server" Text='<%#Eval("Office_Name").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Quantity">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtBMQuantity" runat="server" onkeypress="return validateDec(this,event)" Text='<%# Eval("Quantity").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Remark">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtRemark" runat="server" Text='<%# Eval("Remark").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                        <fieldset>
                                            <legend>E : Closing Stock Union Wise (Kgs)</legend>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:GridView ID="GvClosingStock" runat="server" ShowFooter="true" CssClass="table table-bordered" AutoGenerateColumns="false">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S.No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Office Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOfficeName" runat="server" Text='<%#Eval("Office_Name").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SMP">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtSMP" runat="server" onkeypress="return validateDec(this,event)" Text='<%# Eval("SMP").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="White Butter">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtWhiteButter" runat="server" onkeypress="return validateDec(this,event)" Text='<%# Eval("WhiteButter").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Ghee">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtGhee" runat="server" onkeypress="return validateDec(this,event)" Text='<%# Eval("Ghee").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                        <fieldset>
                                            <legend>F : SMP Production (KG)</legend>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:GridView ID="gvSMPProduction" runat="server" ShowFooter="true" CssClass="table table-bordered" AutoGenerateColumns="false">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S.No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Office Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOfficeName" runat="server" Text='<%#Eval("Office_Name").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Capacity">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtCapacity" runat="server" onkeypress="return validateDec(this,event)" Text='<%# Eval("Capacity").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Production">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtProduction" runat="server" onkeypress="return validateDec(this,event)" Text='<%# Eval("Production").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="WMP Prod (KG)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtWMPProd" runat="server" onkeypress="return validateDec(this,event)" Text='<%# Eval("WMPProdKG").ToString() %>'></asp:Label>
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
                                <div class="row">
                                    <div class="col-md-12">
                                        <fieldset>
                                            <legend>G : Kisan Credit Card Progress</legend>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:GridView ID="gvKisanCredit" runat="server" ShowFooter="true" CssClass="table table-bordered" AutoGenerateColumns="false">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S.No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Name of Milk Union">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNameOfMilkUnion" runat="server" Text='<%#Eval("Office_Name").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="No of Farmer Members">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtNoOfFarmerMembers" runat="server" onkeypress="return validateNum(event)" Text='<%# Eval("NoOfFarmerMembers").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="No of forms Filled by member's">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtNoOfFormsFilledByMember" runat="server" onkeypress="return validateNum(event)" Text='<%# Eval("NoOfFormsFilledByMember").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="% of Unfilled Forms">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtPer_UnfilledForms" runat="server" onkeypress="return validateDec(this,event)" Text='<%# Eval("PerOfUnfilledForms").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="No of form certified by DCS">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtNofFormCertified" runat="server" onkeypress="return validateNum(event)" Text='<%# Eval("NoOfFormCertifiedByDCS").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="No of form Submitted to Banks">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtNoOfFormSubmitted" runat="server" onkeypress="return validateNum(event)" Text='<%# Eval("NoOfFormSubmittedToBanks").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="No of Acknowledgement received from Banks">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtNoOfAcknowledgement" runat="server" onkeypress="return validateNum(event)" Text='<%# Eval("NoOfAcknowledgementReceivedFromBanks").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="New Cards Issued">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtNewCardIssued" runat="server" onkeypress="return validateNum(event)" Text='<%# Eval("NewCardsIssued").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Limit Extended">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtLimitExtended" runat="server" onkeypress="return validateNum(event)" Text='<%# Eval("LimitExtended").ToString() %>'></asp:Label>
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
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

