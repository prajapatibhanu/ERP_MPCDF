<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RoutewiseMilkCollectionInvoice_Newfortest.aspx.cs" Inherits="mis_MilkCollection_RoutewiseMilkCollectionInvoice_Newfortest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <script>
        function checkAllbox(objRef) {
            var GridView = document.getElementById("<%=gv_SocietyMilkDispatchDetail.ClientID %>");
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }

    </script>
    <style>
           .NonPrintable {
            display: none;
        }

        @media print {
            .NonPrintable {
                display: block;
            }

            .noprint {
                display: none;
            }

            .pagebreak {
                page-break-before: always;
            }
        }

        .table1 > tbody > tr > td, .table1 > tbody > tr > th, .table1 > tfoot > tr > td, .table1 > tfoot > tr > th, .table1 > thead > tr > td, .table1 > thead > tr > th {
            padding: 4px 2px;
            font-size: 10.5px;
        }
    </style>
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
        <section class="content noprint ">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Generate CC Wise Society Milk Invoice</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                
                <div class="box-body">
                    <fieldset>
                        <legend>Filter</legend>
                        <div class="row">
                            <div class="col-md-12">
                               <div class="col-md-2">
                                <div class="form-group">
                                    <label>CC<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select CC" Text="<i class='fa fa-exclamation-circle' title='Select CC !'></i>"
                                            ControlToValidate="ddlccbmcdetail" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlccbmcdetail"  runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                                
                                <div class="col-md-2">
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
                                            <asp:TextBox ID="txtFdt" onkeypress="javascript: return false;" Width="100%" MaxLength="10" data-date-end-date="0d" onpaste="return false;" placeholder="Select From Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-2">
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
                                            <asp:TextBox ID="txtTdt" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" data-date-end-date="0d" placeholder="Select To Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                


                                <div class="col-md-1" style="margin-top: 20px;" runat="server">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <asp:Button runat="server"  CssClass="btn btn-primary" OnClick="btnSubmit_Click" ValidationGroup="a" ID="btnSubmit" Text="Search" AccessKey="S" />

                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </fieldset>
                    <fieldset>
                        <legend>Detail</legend>
                        <div class="row" id="div_print">
                            <div class="col-md-12">
                                <div class="form-group">
                                     <asp:Button runat="server" OnClientClick="if (!confirm('Are you sure you want Generate?')) return false;" Visible="false" CssClass="btn btn-primary" OnClick="btnGenerateInvoice_Click" ValidationGroup="save" ID="btnGenerateInvoice" Text="Generate Invoice" />
                                    <asp:Button runat="server"  OnClientClick="if (!confirm('Are you sure you want Reprocess?')) return false;" Visible="false" CssClass="btn btn-primary" OnClick="btnReprocess_Click"  ValidationGroup="save" ID="btnReprocess" Text="Reprocess" />
                                    <asp:Button runat="server"  OnClientClick="if (!confirm('Are you sure you want Final Submit?')) return false;" Visible="false" CssClass="btn btn-Success"  ValidationGroup="save" OnClick="btnFinalSubmit_Click" ID="btnFinalSubmit" Text="Final Submit" />
									 <asp:Button runat="server" Visible="false"  ID="btnPrintInvoice" Text="Invoice Print" CssClass="btn btn-default pull-right" AccessKey="p"  OnClick="btnPrintInvoice_Click"/>
                                </div>
                            </div>
                            
                        <div class="col-md-12">
                            <div class="table-responsive">
                            <asp:GridView ID="GridView1" runat="server" ShowFooter="true" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover GridView1"
                                OnRowCommand="GridView1_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowno" runat="server" Text='<%# Container.DataItemIndex+ 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField>
                                                <HeaderTemplate>
                                                   <asp:CheckBox ID="checkAll" runat="server" ClientIDMode="static"/>
                                                    All
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" Visible='<%# Eval("MilkCollectionInvoice_ID").ToString()=="0"?true:false%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                   
                                   
                                     <asp:TemplateField HeaderText="Society">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSociety" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                <asp:Label ID="lblI_OfficeID" CssClass="hidden" runat="server" Text='<%# Eval("I_OfficeID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
										 <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Milk Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMilkValue" runat="server" Text='<%# Eval("MilkValue") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Commission">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCommission" runat="server" Text='<%# Eval("Commission") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="GrossEarning">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrossEarning" runat="server" Text='<%# Eval("GrossEarning") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Deduction/Addition">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeductionAdditionValue" runat="server" Text='<%# Eval("DeductionAdditionValue") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                  <%--  <asp:TemplateField HeaderText="Producer Payment">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProducerPayment" runat="server" Text='<%# Eval("ProducerPayment") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Producer Adjustment Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProducerAdjPayment" runat="server" Text='<%# Eval("ProducerAdjPayment") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Society Adjustment Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSocietyAdjPayment" runat="server" Text='<%# Eval("SocietyAdjPayment") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                     <asp:TemplateField HeaderText="Head Load Charges">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHeadLoadCharges" runat="server" Text='<%# Eval("HeadLoadCharges") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Chilling Cost">
                                            <ItemTemplate>
                                                <asp:Label ID="lblChillingCost" runat="server" Text='<%# Eval("ChillingCost") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                  

                                    <asp:TemplateField HeaderText="NetAmount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNetAmount" runat="server" Text='<%# Eval("NetAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    
                                    <asp:TemplateField HeaderText="View Invoice">
                                        <ItemTemplate>
                                           <asp:LinkButton ID="lblviewresult" CommandName="ViewInvoice" Visible='<%# Eval("MilkCollectionInvoice_ID").ToString()=="0"?true:false %>'  CommandArgument='<%# Eval("I_OfficeID").ToString()%>' runat="server" CausesValidation="False"><i class="fa fa-print" aria-hidden="true"></i></asp:LinkButton> 
                                            <asp:LinkButton ID="lnkPrint" OnClick="lNKpRINT_Click" CommandName="PrintInvoice" Visible='<%# Eval("MilkCollectionInvoice_ID").ToString()=="0"?false:true %>'  CommandArgument='<%# Eval("MilkCollectionInvoice_ID").ToString()%>' runat="server" CausesValidation="False"><i class="fa fa-print" aria-hidden="true"></i></asp:LinkButton> 
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

             <div class="modal fade" id="mymodal" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Society Invoice Details </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                    <div class="col-md-12">

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

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDt_Date" runat="server" Text='<%# (Convert.ToDateTime(Eval("Date"))).ToString("dd") %>'></asp:Label>
                                                <asp:Label ID="lbllblDt_Date_F" Visible="false" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblmsgDt_Date" Text="Total" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Shift">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_Shift" runat="server" Text='<%# Eval("Shift") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblmsgV_Shift" Text="-" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_MilkType" runat="server" Text='<%# Eval("MilkType") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblmsgV_MilkType" Text="-" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Fat %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFat" runat="server" Text='<%# Eval("FatPer") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotal_Fat" Text="-" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SNF %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("SnfPer") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblTotal_SNF" Text="-" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk Quantity (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblI_MilkSupplyQty" runat="server" Text='<%# Eval("MilkQuantity") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblI_MilkSupplyQtyTotal" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="FAT (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFAT_IN_KG" runat="server" Text='<%# Eval("FatInKg") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblTotal_FAT_IN_KG" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SNF (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF_IN_KG" runat="server" Text='<%# Eval("SnfInKg") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblTotal_SNF_IN_KG" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Value (In INR)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblValue" runat="server" Text='<%# Eval("MilkValue") %>'></asp:Label>
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
                                                <asp:Label ID="lblTotal_CLR" Text="-" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk Quality">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuality" runat="server" Text='<%# Eval("MilkQuality") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblmsgQuality" Text="-" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Rate Per Ltr">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRate_Per_Ltr" runat="server" Text='<%# Eval("RatePerLtr") %>'></asp:Label>
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

                                <asp:GridView ID="gv_SocietyBufData" runat="server" OnRowDataBound="gv_SocietyMorningData_RowDataBound" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover"
                                    ShowHeader="True">
                                    <Columns>


                                        <asp:TemplateField HeaderText="Milk Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_Shift" runat="server" Text="Buf"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk Quality">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmsgQuality" Text='<%# Eval("MilkQuality") %>' runat="server" Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Fat %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFat" runat="server" Text='<%# Eval("FatPer") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SNF  %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("SnfPer") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk Quantity (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblI_MilkSupplyQty" runat="server" Text='<%# Eval("MilkQuantity") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="FAT (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFAT_IN_KG" runat="server" Text='<%# Eval("FatInKg") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="SNF (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF_IN_KG" runat="server" Text='<%# Eval("SnfInKg") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CLR">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("CLR") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Value (In INR)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblValue" runat="server" Text='<%# Eval("MilkValue") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>

                                <hr />

                                <asp:GridView ID="gv_SocietyCowData" OnRowDataBound="gv_SocietyEveningData_RowDataBound" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover"
                                    ShowHeader="True">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Milk Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_Shift" runat="server" Text="Cow"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Milk Quality">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmsgQuality" Text='<%# Eval("MilkQuality") %>' runat="server" Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Fat %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFat" runat="server" Text='<%# Eval("FatPer") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SNF  %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("SnfPer") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk Quantity (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblI_MilkSupplyQty" runat="server" Text='<%# Eval("MilkQuantity") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="FAT (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFAT_IN_KG" runat="server" Text='<%# Eval("FatInKg") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="SNF (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF_IN_KG" runat="server" Text='<%# Eval("SnfInKg") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CLR">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("CLR") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Value (In INR)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblValue" runat="server" Text='<%# Eval("MilkValue") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>

                                <hr />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Label ID="lblNarration" Font-Bold="true" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                </div>
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
                                            <asp:Label ID="lblItemBillingHead_ID" runat="server" CssClass="hidden" Text='<%# Eval("ItemBillingHead_ID") %>'></asp:Label>
											<asp:Label ID="lblAddtionsDeducEntry_ID" runat="server" CssClass="hidden" Text='<%# Eval("AddtionsDeducEntry_ID") %>'></asp:Label>
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
                                         <asp:TemplateField HeaderText="Remark">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                             
                                            </ItemTemplate>
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
                                        <br />
                                        <asp:Label ID="lblMilkValue" Font-Bold="true" runat="server"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Commission -  </label>
                                        :<br />
                                        <asp:Label ID="lblCommission" Font-Bold="true" runat="server"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Gross Earning </label>
                                        :<br />
                                        <asp:Label ID="lblGrossEarning" Font-Bold="true" runat="server"></asp:Label>
                                    </div>
                                </div>


                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Deduction/Addition Value </label>
                                        :<br />
                                        <asp:Label ID="lbldeductionadditionValue" Font-Bold="true" runat="server"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3 hidden">
                                    <div class="form-group">
                                        <label>Producer Payment </label>
                                        :<br />
                                        <asp:Label ID="lblProducerPayment" Font-Bold="true" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-3 hidden">
                                    <div class="form-group">
                                        <label>Producer Adjust Amount</label>
                                        :<br />
                                        <asp:Label ID="lblProcAdjustAmount" Font-Bold="true" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-3 hidden">
                                    <div class="form-group">
                                        <label>Society Adjust Amount</label>
                                        :<br />
                                        <asp:Label ID="lblSocAdjustAmount" Font-Bold="true" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Chiling Cost</label>
                                        :<br />
                                        <asp:Label ID="lblCC" Font-Bold="true" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Head Load Charges</label>
                                        :<br />
                                        <asp:Label ID="lblHeadLoadCharges" Font-Bold="true" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">


                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Net Amount </label>
                                        :<br />
                                        <asp:Label ID="lblnetamount" Font-Bold="true" runat="server"></asp:Label>
                                    </div>
                                </div>


                            </div>
                        </div>


                    </fieldset>

                    <hr />
                   
                    <div class="modal-footer">
                       
                    </div>

                </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <%--<asp:Button runat="server" Text="Add" ID="btnBillByBillSave" OnClick="btnBillByBillSave_Click" ClientIDMode="Static" CssClass="btn btn-success"></asp:Button>--%>

                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
            <asp:ValidationSummary ID="v1" runat="server" ValidationGroup="save" ShowMessageBox="true" ShowSummary="false" />

            

        </section>
         <section class="content">
            <div id="dvReport" class="NonPrintable" runat="server">
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

        function ShowInvoice() {
            $('#mymodal').modal('show');
            
        }

    </script>
    <link href="../../../mis/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../mis/js/bootstrap-multiselect.js" type="text/javascript"></script>

    <script>
        $('#checkAll').click(function () {

            var inputList = document.querySelectorAll('.GridView1 tbody input[type="checkbox"]:not(:disabled)');

            for (var i = 0; i < inputList.length; i++) {
                if (document.getElementById('checkAll').checked) {
                    inputList[i].checked = true;

                    //ValidatorEnable(Amount,true)
                }
                else {
                    inputList[i].checked = false;
                }
            }
        });
        $(function () {
            $('[id*=ddlSociety]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });
        });
    </script>

</asp:Content>
