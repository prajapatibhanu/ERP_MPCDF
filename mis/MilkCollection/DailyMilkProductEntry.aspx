<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DailyMilkProductEntry.aspx.cs" Inherits="mis_MilkCollection_DailyMilkProductEntry" %>

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
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="Save" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Daily Milk / Product Entry</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
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
                                            <asp:TextBox ID="txtDate" autocomplete="off" placeholder="Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6 form-group">
                                    <span style="color : red;"><b>Note:</b> </span><br />
                                    <span style="color : red;"><b>1. * % Change as compared to Yesterday </b></span><br />
                                    <span style="color : red;"><b>2. # % Change as compared to 31st August- </b></span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <fieldset>
                                        <legend>A : Milk Procurement (KGPD)</legend>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <asp:GridView ID="gvMilkProcurement" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Office Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOfficeId" runat="server" Visible="false" CssClass="form-control" Text='<%#Eval("Office_ID").ToString() %>'></asp:Label>
                                                                    <asp:Label ID="lblOfficeName" runat="server" Text='<%#Eval("Office_Name").ToString() %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quantity">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtMProQuantity" runat="server" Text="0" CssClass="form-control" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="% Charge *">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtMProCharge1" runat="server" Text="0" CssClass="form-control" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="% Charge #">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtMProCharge2" runat="server" Text="0" CssClass="form-control" onkeypress="return validateDec(this,event)"></asp:TextBox>
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
                                                    <asp:GridView ID="gvPacketMilkSale" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Office Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOfficeId" runat="server" Visible="false" CssClass="form-control" Text='<%#Eval("Office_ID").ToString() %>'></asp:Label>
                                                                    <asp:Label ID="lblOfficeName" runat="server" Text='<%#Eval("Office_Name").ToString() %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quantity">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtPMQuantity" runat="server" CssClass="form-control" Text="0" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="% Charge *">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtPMCharge1" runat="server" CssClass="form-control" Text="0" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="% Charge #">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtPMCharge2" runat="server" CssClass="form-control" Text="0" onkeypress="return validateDec(this,event)"></asp:TextBox>
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
                                                                    <asp:Label ID="lblProductID" runat="server" Visible="false" Text='<%# Eval("TypeID") %>'></asp:Label>
                                                                    <asp:Label ID="lblProduct" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quantity">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtMPQuantity" runat="server" CssClass="form-control" onkeypress="return validateDec(this,event)" Text="0"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="% Charge *">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtMPCharge1" runat="server" CssClass="form-control" onkeypress="return validateDec(this,event)" Text="0"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="% Charge #">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtMPCharge2" runat="server" CssClass="form-control" onkeypress="return validateDec(this,event)" Text="0"></asp:TextBox>
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
                                                                    <asp:Label ID="lblOfficeId" runat="server" Visible="false" CssClass="form-control" Text='<%#Eval("Office_ID").ToString() %>'></asp:Label>
                                                                    <asp:Label ID="lblOfficeName" runat="server" Text='<%#Eval("Office_Name").ToString() %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quantity">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtBMQuantity" runat="server" CssClass="form-control" onkeypress="return validateDec(this,event)" Text="0"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Remark">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control"></asp:TextBox>
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
                                                    <asp:GridView ID="GvClosingStock" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Office Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOfficeId" runat="server" Visible="false" CssClass="form-control" Text='<%#Eval("Office_ID").ToString() %>'></asp:Label>
                                                                    <asp:Label ID="lblOfficeName" runat="server" Text='<%#Eval("Office_Name").ToString() %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="SMP">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtSMP" runat="server" CssClass="form-control" onkeypress="return validateDec(this,event)" Text="0"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="White Butter">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtWhiteButter" runat="server" CssClass="form-control" onkeypress="return validateDec(this,event)" Text="0"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Ghee">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtGhee" runat="server" CssClass="form-control" onkeypress="return validateDec(this,event)" Text="0"></asp:TextBox>
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
                                                    <asp:GridView ID="gvSMPProduction" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Office Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOfficeId" runat="server" Visible="false" CssClass="form-control" Text='<%#Eval("Office_ID").ToString() %>'></asp:Label>
                                                                    <asp:Label ID="lblOfficeName" runat="server" Text='<%#Eval("Office_Name").ToString() %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Capacity">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtCapacity" runat="server" CssClass="form-control" onkeypress="return validateDec(this,event)" Text="0"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Production">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtProduction" runat="server" CssClass="form-control" onkeypress="return validateDec(this,event)" Text="0"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="WMP Prod (KG)">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtWMPProd" runat="server" CssClass="form-control" onkeypress="return validateDec(this,event)" Text="0"></asp:TextBox>
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
                                                    <asp:GridView ID="gvKisanCredit" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name of Milk Union">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOfficeId" runat="server" Visible="false" CssClass="form-control" Text='<%#Eval("Office_ID").ToString() %>'></asp:Label>
                                                                    <asp:Label ID="lblNameOfMilkUnion" runat="server" Text='<%#Eval("Office_Name").ToString() %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="No of Farmer Members">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtNoOfFarmerMembers" runat="server" CssClass="form-control" onkeypress="return validateNum(event)" Text="0"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="No of forms Filled by member's">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtNoOfFormsFilledByMember" runat="server" CssClass="form-control" onkeypress="return validateNum(event)" Text="0"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="% of Unfilled Forms">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtPer_UnfilledForms" runat="server" CssClass="form-control" onkeypress="return validateDec(this,event)" Text="0"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="No of form certified by DCS">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtNofFormCertified" runat="server" CssClass="form-control" onkeypress="return validateNum(event)" Text="0"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="No of form Submitted to Banks">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtNoOfFormSubmitted" runat="server" CssClass="form-control" onkeypress="return validateNum(event)" Text="0"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="No of Acknowledgement received from Banks">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtNoOfAcknowledgement" runat="server" CssClass="form-control" onkeypress="return validateNum(event)" Text="0"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="New Cards Issued">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtNewCardIssued" runat="server" CssClass="form-control" onkeypress="return validateNum(event)" Text="0"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Limit Extended">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtLimitExtended" runat="server" CssClass="form-control" onkeypress="return validateNum(event)" Text="0"></asp:TextBox>
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
                                <div class="col-md-4 pull-right">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success btn-block" ValidationGroup="Save" OnClick="btnSave_Click" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <a id="btnClear" runat="server" href="~/mis/MilkCollection/DailyMilkProductEntry.aspx" class="btn btn-default btn-block">Clear</a>
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
</asp:Content>

