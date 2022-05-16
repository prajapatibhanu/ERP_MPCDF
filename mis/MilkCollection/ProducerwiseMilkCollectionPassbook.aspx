<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ProducerwiseMilkCollectionPassbook.aspx.cs" Inherits="mis_MilkCollection_ProducerwiseMilkCollectionPassbook" %>

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
                    <h3 class="box-title">Producer Wise Milk Collection Passbook</h3>
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
                                    <label>Producer<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvFarmer" runat="server" Display="Dynamic" ValidationGroup="a" ControlToValidate="ddlFarmer" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Producer!'></i>" ErrorMessage="Select Producer" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlFarmer" CssClass="form-control select2" runat="server"></asp:DropDownList>
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


                    <asp:Label ID="lblmsgshow" Visible="false" runat="server"></asp:Label>


                    <fieldset runat="server" visible="false" id="FS_DailyReport">
                        <legend>Detail</legend>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">

                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Producer Name And Code </label>
                                            :
                                    <asp:Label ID="lblProducername" Font-Bold="true" runat="server"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Society Name And Code </label>
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

                                <asp:GridView ID="gv_ProducerMilkDetail" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover"
                                    ShowFooter="true" EmptyDataText="Data Not Found">
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
                                                <asp:Label ID="lblDt_Date" runat="server" Text='<%# Eval("Dt_Date") %>'></asp:Label>
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
										
										 <asp:TemplateField HeaderText="Milk Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMilkType" runat="server" Text='<%# Eval("MilkType") %>'></asp:Label>
                                            </ItemTemplate> 
                                            <FooterTemplate>
                                                <asp:Label ID="lblmsgMilkType" Text="-" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
										
										<asp:TemplateField HeaderText="Milk Quantity (In Ltr)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblI_MilkSupplyQty" runat="server" Text='<%# Eval("I_MilkSupplyQty") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblI_MilkSupplyQty" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Fat %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFat" runat="server" Text='<%# Eval("Fat") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotal_Fat" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SNF %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("SNF") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblTotal_SNF" runat="server" Font-Bold="true"></asp:Label>
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

                                        <asp:TemplateField HeaderText="Milk Value (In INR)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblValue" runat="server" Text='<%# Eval("Value") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblNetValue" runat="server" Font-Bold="true"></asp:Label>
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

                                        <asp:TemplateField HeaderText="Milk Quality">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuality" runat="server" Text='<%# Eval("Quality") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblmsgQuality" Text="-" runat="server" Font-Bold="true"></asp:Label>
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

                                <hr />
                                <b>Morning Shift Milk Collection</b>

                                <asp:GridView ID="gv_ProducerMorningData"  style="margin-top:10px;" runat="server" OnRowDataBound="gv_ProducerMorningData_RowDataBound" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover"
                                    ShowHeader="True" ShowFooter="true" EmptyDataText="Data Not Found">
                                    <Columns>


                                        <asp:TemplateField HeaderText="Shift">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_Shift" runat="server" Text="Morning"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk Quality">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmsgQuality" Text='<%# Eval("Quality") %>' runat="server" Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Fat %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFat" runat="server" Text='<%# Eval("Total_Fat") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SNF %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("Total_SNF") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CLR">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("Total_CLR") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk Quantity (In Ltr)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblI_MilkSupplyQty" runat="server" Text='<%# Eval("I_MilkSupplyQty") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="FAT (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFAT_IN_KG" runat="server" Text='<%# Eval("Total_FAT_IN_KG") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="SNF (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF_IN_KG" runat="server" Text='<%# Eval("Total_SNF_IN_KG") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk Value (In INR)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblValue" runat="server" Text='<%# Eval("NetValue") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>

                                <hr />
                                <b>Evening Shift Milk Collection</b>

                                <asp:GridView ID="gv_ProducerEveningData"  style="margin-top:10px;" OnRowDataBound="gv_ProducerEveningData_RowDataBound" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover"
                                    ShowHeader="True"  ShowFooter="true" EmptyDataText="Data Not Found">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Shift">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_Shift" runat="server" Text="Evening"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk Quality">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmsgQuality" Text='<%# Eval("Quality") %>' runat="server" Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Fat %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFat" runat="server" Text='<%# Eval("Total_Fat") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SNF %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("Total_SNF") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CLR">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("Total_CLR") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk Quantity (In Ltr)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblI_MilkSupplyQty" runat="server" Text='<%# Eval("I_MilkSupplyQty") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="FAT (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFAT_IN_KG" runat="server" Text='<%# Eval("Total_FAT_IN_KG") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SNF (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF_IN_KG" runat="server" Text='<%# Eval("Total_SNF_IN_KG") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk Value (In INR)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblValue" runat="server" Text='<%# Eval("NetValue") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>


                                <hr />
                                <b>Item Purchase Value</b>

                                <asp:GridView ID="gvGetSaleDetails" runat="server" style="margin-top:10px;" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover"
                                    ShowHeader="True" ShowFooter="true" EmptyDataText="Data Not Found">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvoiceDt" Text='<%# Eval("InvoiceDt") %>' runat="server" Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Item Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemName" Text='<%# Eval("ItemName") %>' runat="server" Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblI_Quantity" Text='<%# Eval("I_Quantity") %>' runat="server" Font-Bold="true"></asp:Label>(<asp:Label ID="lblUnitName" Text='<%# Eval("UnitName") %>' runat="server" Font-Bold="true"></asp:Label>)
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         
                                        <asp:TemplateField HeaderText="MRP">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMRP" Text='<%# Eval("MRP") %>' runat="server" Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Purchase Amount (In INR)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNetAmount" Text='<%# Eval("NetAmount") %>' runat="server" Font-Bold="true"></asp:Label> <b>(Rs)</b>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                                 <hr />
                                <b>Earning Value</b>

                                <asp:GridView ID="gv_EarningDetail" runat="server" style="margin-top:10px;" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover"
                                    ShowHeader="True" ShowFooter="true" EmptyDataText="Data Not Found">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEntryDt" Text='<%# Eval("EntryDt") %>' runat="server" Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                      
                                        <asp:TemplateField HeaderText="Amount (In INR)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNetAmount" Text='<%# Eval("NetAmount") %>' runat="server" Font-Bold="true"></asp:Label> <b>(Rs)</b>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Remark">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRemark" Text='<%# Eval("Remark") %>' runat="server" Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>


                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-md-12">

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Milk Sale Value </label>
                                        :
                                    <asp:Label ID="lblMilkValue" Font-Bold="true" runat="server"></asp:Label>  INR
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Earning Value </label>
                                        :
                                    <asp:Label ID="lblEarningValue" Font-Bold="true" runat="server"></asp:Label>  INR
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Item Purchase Value </label>
                                        :
                                    <asp:Label ID="lblProductSaleValue" Font-Bold="true" runat="server"></asp:Label>  INR
                                    </div>
                                </div>
                                 <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Adjust Amount </label>
                                        :
                                    <asp:Label ID="lblAdjustAmount" Font-Bold="true" runat="server"></asp:Label>  INR
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Gross Earning (Milk Sale Value + Earning Value - Item Purchase Value - Adjust Amount) </label>
                                        :
                                    <asp:Label ID="lblGrossEarning" Font-Bold="true" runat="server"></asp:Label>  INR
                                    </div>
                                </div>


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


</asp:Content>
