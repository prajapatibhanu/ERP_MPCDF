<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SocietywiseMilkCollectionPassbook.aspx.cs" Inherits="mis_MilkCollection_SocietywiseMilkCollectionPassbook" %>

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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnYes_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="vs1" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="vs2" runat="server" ValidationGroup="c" ShowMessageBox="true" ShowSummary="false" />

    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Society Wise Milk Dispatch Passbook</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">

                    <fieldset>
                        <legend>Filter</legend>
                        <div class="row">
                            <div class="col-md-12">

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>From Date  </label>
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
                                        <label>To Date  </label>
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

                                <div class="col-md-3">
                                    <label>Society<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvFarmer" runat="server" Display="Dynamic" ValidationGroup="a" ControlToValidate="ddlSociety" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Society!'></i>" ErrorMessage="Select Society" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlSociety" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>


                                <div class="col-md-1" style="margin-top: 20px;">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnaddhead_Click" ValidationGroup="a" ID="btnaddhead" Text="Add & Search" AccessKey="S" />
                                        </div>
                                    </div>
                                </div>



                                <div class="col-md-1" style="margin-top: 20px;" runat="server" visible="false">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <asp:Button runat="server" Visible="false" CssClass="btn btn-primary" OnClick="btnSubmit_Click" ValidationGroup="a" ID="btnSubmit" Text="Search" AccessKey="S" />

                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </fieldset>

                    <asp:Label ID="lblmsgshow" Visible="false" runat="server"></asp:Label>

                    <fieldset runat="server" visible="false" id="FS_DailyReport">
                        <legend>Detail</legend>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">

                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Society Name And Code </label>
                                            :
                                    <asp:Label ID="lblSociety" Font-Bold="true" runat="server"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Attached Office Name And Code </label>
                                            :
                                    <asp:Label ID="lblOfficename" Font-Bold="true" runat="server"></asp:Label>
                                        </div>
                                    </div>

                                </div>

                                <div class="col-md-6">

                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Bank Code / Name / Account </label>
                                            :
                                    <asp:Label ID="lblbankInfo" Font-Bold="true" runat="server"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Billing Period </label>
                                            : 
                                    <asp:Label ID="lblBillingPeriod" Font-Bold="true" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-md-12">

                                <asp:GridView ID="gv_SocietyMilkDispatchDetail" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover"
                                    ShowFooter="true">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sr. No.">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblmsg" Text="Total" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDt_Date" runat="server" Text='<%# Eval("DT_DispatchDate") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblmsgDt_Date" Text="-" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Shift">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_Shift" runat="server" Text='<%# Eval("V_Shift") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblmsgV_Shift" Text="-" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Fat %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFat" runat="server" Text='<%# Eval("FAT_Per") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotal_Fat" Text="-" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SNF %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("SNF_Per") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblTotal_SNF" Text="-" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk Quantity (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblI_MilkSupplyQty" runat="server" Text='<%# Eval("MilkQty_InKG") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblI_MilkSupplyQtyTotal" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="FAT (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFAT_IN_KG" runat="server" Text='<%# Eval("FAT_IN_KG") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblTotal_FAT_IN_KG" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SNF (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF_IN_KG" runat="server" Text='<%# Eval("SNF_IN_KG") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblTotal_SNF_IN_KG" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Value (In INR)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblValue" runat="server" Text='<%# Eval("Value") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblNetValue" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CLR">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("CLR") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblTotal_CLR" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk Quality">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuality" runat="server" Text='<%# Eval("D_MilkQuality") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblmsgQuality" Text="-" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Rate Per Ltr">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRate_Per_Ltr" runat="server" Text='<%# Eval("Rate_Per_Ltr") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblmsgRate_Per_Ltr" Text="-" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>




                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>

                    </fieldset>

                    <fieldset runat="server" visible="false" id="FS_DailyReport_Shift">

                        <legend>Particulars : </legend>

                        <div class="row">
                            <div class="col-md-12">

                                <asp:GridView ID="gv_SocietyMorningData" runat="server" OnRowDataBound="gv_SocietyMorningData_RowDataBound" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover"
                                    ShowHeader="True">
                                    <Columns>


                                        <asp:TemplateField HeaderText="Shift">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_Shift" runat="server" Text="Morning"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk Quality">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmsgQuality" Text='<%# Eval("D_MilkQuality") %>' runat="server" Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Fat %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFat" runat="server" Text='<%# Eval("FAT_Per") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SNF  %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("SNF_Per") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk Quantity (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblI_MilkSupplyQty" runat="server" Text='<%# Eval("MilkQty_InKG") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="FAT (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFAT_IN_KG" runat="server" Text='<%# Eval("FAT_IN_KG") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="SNF (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF_IN_KG" runat="server" Text='<%# Eval("SNF_IN_KG") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CLR">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("CLR") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Value (In INR)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblValue" runat="server" Text='<%# Eval("Value") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>

                                <hr />

                                <asp:GridView ID="gv_SocietyEveningData" OnRowDataBound="gv_SocietyEveningData_RowDataBound" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover"
                                    ShowHeader="True">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Shift">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_Shift" runat="server" Text="Evening"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk Quality">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmsgQuality" Text='<%# Eval("D_MilkQuality") %>' runat="server" Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Fat %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFat" runat="server" Text='<%# Eval("FAT_Per") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SNF %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("SNF_Per") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Milk Quantity (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblI_MilkSupplyQty" runat="server" Text='<%# Eval("MilkQty_InKG") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="FAT (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFAT_IN_KG" runat="server" Text='<%# Eval("FAT_IN_KG") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="SNF (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF_IN_KG" runat="server" Text='<%# Eval("SNF_IN_KG") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CLR">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("CLR") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Value (In INR)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblValue" runat="server" Text='<%# Eval("Value") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>

                                <hr />
                                <b style="padding-bottom: 10px;">Deduction / Addition</b>

                                <asp:GridView ID="grhradsdetails" ShowFooter="true" ShowHeader="true" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server">
                                    <Columns>

                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Head Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemBillingHead_Type" runat="server" Text='<%# Eval("ItemBillingHead_Type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Head Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemBillingHead_Name" runat="server" Text='<%# Eval("ItemBillingHead_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div>
                                                    <asp:Label Text="Grand Total" runat="server" />
                                                </div>
                                            </FooterTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>

                                                <asp:Label ID="Label1" CssClass='<%# Eval("ItemBillingHead_Type").ToString() == "ADDITION" ? "label label-success" : "label label-danger" %>' runat="server" Text='<%# Eval("TotalAmount") %>'></asp:Label>
                                                <asp:Label ID="lblTotalPrice" Visible="false" runat="server" Text='<%# Eval("TotalAmount") %>'></asp:Label>
                                            </ItemTemplate>


                                            <FooterTemplate>

                                                <div>
                                                    <asp:Label ID="lblGrandTotal" Font-Bold="true" runat="server" />
                                                </div>
                                            </FooterTemplate>

                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>




                            </div>
                        </div>
                        <hr />

                        <div class="row">
                            <div class="col-md-12">

                              

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Milk Value </label>
                                        :
                                    <asp:Label ID="lblMilkValue" Font-Bold="true" runat="server"></asp:Label>
                                    </div>
                                </div>

                                  <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Commission -  </label>
                                        :
                                    <asp:Label ID="lblCommission" Font-Bold="true" runat="server"></asp:Label>
                                    </div>
                                </div>
                                  
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Gross Earning </label>
                                        :
                                    <asp:Label ID="lblGrossEarning" Font-Bold="true" runat="server"></asp:Label>
                                    </div>
                                </div>


                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Deduction/Addition Value </label>
                                        :
                                    <asp:Label ID="lbldeductionadditionValue" Font-Bold="true" runat="server"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Net Amount </label>
                                        :
                                    <asp:Label ID="lblnetamount" Font-Bold="true" runat="server"></asp:Label>
                                    </div>
                                </div>


                            </div>
                        </div>

                    </fieldset>

                </div>
            </div>


            <asp:ValidationSummary ID="v1" runat="server" ValidationGroup="save" ShowMessageBox="true" ShowSummary="false" />

            <div class="modal" id="ItemDetailsModal">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 520px;">
                        <div class="modal-header">
                            <asp:LinkButton runat="server" class="close" ID="btnCrossButton" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>
                            <%-- <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                </button>--%>
                            <h4 class="modal-title">Additions / Deduction Head</h4>
                        </div>
                        <div class="modal-body">

                            <asp:Label ID="lblModalMsg" runat="server" Text=""></asp:Label>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row" style="height: 350px; overflow: scroll;">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <section class="content">
                                                    <!-- SELECT2 EXAMPLE -->
                                                    <div class="box box-Manish" style="min-height: 350px;">
                                                        <div class="box-header">
                                                            <h3 class="box-title">Head Details</h3>
                                                        </div>
                                                        <!-- /.box-header -->
                                                        <div class="box-body">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <asp:Label ID="lblPopupMsg" runat="server"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <fieldset>
                                                                <legend>Heads </legend>
                                                                <div class="row">
                                                                    <div class="col-md-3">
                                                                        <div class="form-group">
                                                                            <label>Head Type<span style="color: red;"> *</span></label>
                                                                            <span class="pull-right">
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="b"
                                                                                    InitialValue="0" ErrorMessage="Select Head Type" Text="<i class='fa fa-exclamation-circle' title='Select Head Type !'></i>"
                                                                                    ControlToValidate="ddlItemBillingHead_Type" ForeColor="Red" Display="Dynamic" runat="server">
                                                                                </asp:RequiredFieldValidator></span>
                                                                            <asp:DropDownList runat="server" ID="ddlItemBillingHead_Type" CssClass="form-control select2" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="ddlItemBillingHead_Type_SelectedIndexChanged">
                                                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                                                <asp:ListItem Value="ADDITION">ADDITIONS</asp:ListItem>
                                                                                <asp:ListItem Value="DEDUCTION">DEDUCTIONS</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <div class="form-group">
                                                                            <label>Head Name<span style="color: red;"> *</span></label>
                                                                            <span class="pull-right">
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="b"
                                                                                    InitialValue="0" ErrorMessage="Select Head Name" Text="<i class='fa fa-exclamation-circle' title='Select Head Name !'></i>"
                                                                                    ControlToValidate="ddlHeaddetails" ForeColor="Red" Display="Dynamic" runat="server">
                                                                                </asp:RequiredFieldValidator></span>
                                                                            <asp:DropDownList ID="ddlHeaddetails" OnInit="ddlHeaddetails_Init" Width="150px" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-3">
                                                                        <div class="form-group">
                                                                            <label>Amount [In INR] <span style="color: red;">*</span></label>
                                                                            <span class="pull-right">
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="b"
                                                                                    ErrorMessage="Enter Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Amount !'></i>"
                                                                                    ControlToValidate="txtHeadAmount" Display="Dynamic" runat="server">
                                                                                </asp:RequiredFieldValidator>
                                                                            </span>
                                                                            <asp:TextBox autocomplete="off" ClientIDMode="Static" onkeypress="return validateDec(this,event)" MaxLength="10" runat="server" CssClass="form-control" ID="txtHeadAmount" placeholder="Enter Amount"></asp:TextBox>
                                                                        </div>
                                                                    </div>


                                                                    <div class="col-md-1">
                                                                        <div class="form-group">
                                                                            <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnAddHeadsDetails_Click" Style="margin-top: 20px;" ValidationGroup="b" ID="btnAddHeadsDetails" Text="Add" />
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-12">
                                                                        <hr />
                                                                        <asp:GridView ID="gv_HeadDetails" ShowFooter="true" ShowHeader="true" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server">
                                                                            <Columns>

                                                                                <asp:TemplateField HeaderText="S.No.">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Head Type">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblItemBillingHead_Type" runat="server" Text='<%# Eval("ItemBillingHead_Type") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Head Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblItemBillingHead_Name" runat="server" Text='<%# Eval("ItemBillingHead_Name") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <div>
                                                                                            <asp:Label Text="Grand Total" runat="server" />
                                                                                        </div>
                                                                                    </FooterTemplate>

                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Amount">
                                                                                    <ItemTemplate>

                                                                                        <asp:Label ID="Label1" CssClass='<%# Eval("ItemBillingHead_Type").ToString() == "ADDITION" ? "label label-success" : "label label-danger" %>' runat="server" Text='<%# Eval("TotalAmount") %>'></asp:Label>
                                                                                        <asp:Label ID="lblTotalPrice" Visible="false" runat="server" Text='<%# Eval("TotalAmount") %>'></asp:Label>
                                                                                    </ItemTemplate>


                                                                                    <FooterTemplate>

                                                                                        <div>
                                                                                            <asp:Label ID="lblGrandTotal" Font-Bold="true" runat="server" />
                                                                                        </div>
                                                                                    </FooterTemplate>

                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Action">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnkDeleteHead" OnClick="lnkDeleteHead_Click" runat="server" ToolTip="DeleteHead" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete this record?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </fieldset>

                                                        </div>

                                                    </div>
                                                    <!-- /.box-body -->

                                                </section>
                                                <!-- /.content -->
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnSearchHeadDetails_Click" ValidationGroup="save" ID="btnSearchHeadDetails" Text="Search" />
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
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

        function myItemDetailsModal() {
            $("#ItemDetailsModal").modal('show');

        }

    </script>


</asp:Content>
