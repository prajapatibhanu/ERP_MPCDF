<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SocietyMilkCollectionInvoiceDetails.aspx.cs" Inherits="mis_MilkCollection_SocietyMilkCollectionInvoiceDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
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

    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content noprint">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">
                        <asp:label id="lblUnitName" text="Society Payment Invoice" forecolor="White" clientidmode="Static" font-size="Medium" font-bold="true" runat="server"></asp:label>
                    </h3>
                </div>
                <asp:label id="lblMsg" runat="server" text=""></asp:label>
                <div class="box-body">

                    <fieldset>
                        <legend>Filter</legend>
                        <div class="row">
                            <div class="col-md-12">



                                <div class="col-md-3">
                                    <label>Society<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:requiredfieldvalidator id="rfvFarmer" runat="server" display="Dynamic" validationgroup="a" controltovalidate="ddlSociety" initialvalue="0" text="<i class='fa fa-exclamation-circle' title='Select Society!'></i>" errormessage="Select Society" setfocusonerror="true" forecolor="Red"></asp:requiredfieldvalidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:dropdownlist id="ddlSociety" cssclass="form-control" runat="server"></asp:dropdownlist>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>From Date  </label>
                                        <span style="color: red">*</span>
                                        <span class="pull-right">
                                            <asp:requiredfieldvalidator id="rfv1" runat="server" validationgroup="a" display="Dynamic" controltovalidate="txtFdt" errormessage="Please Enter From Date." text="<i class='fa fa-exclamation-circle' title='Please Enter From Date !'></i>"></asp:requiredfieldvalidator>
                                            <asp:regularexpressionvalidator id="revdate" validationgroup="a" runat="server" display="Dynamic" controltovalidate="txtFdt" errormessage="Invalid From Date" text="<i class='fa fa-exclamation-circle' title='Invalid From Date !'></i>" setfocusonerror="true" validationexpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:regularexpressionvalidator>
                                        </span>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:textbox id="txtFdt" onkeypress="javascript: return false;" width="100%" maxlength="10" onpaste="return false;" placeholder="Select From Date" runat="server" autocomplete="off" cssclass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" clientidmode="Static"></asp:textbox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>To Date  </label>
                                        <span style="color: red">*</span>
                                        <span class="pull-right">
                                            <asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" validationgroup="a" display="Dynamic" controltovalidate="txtTdt" errormessage="Please Enter To Date." text="<i class='fa fa-exclamation-circle' title='Please Enter To Date !'></i>"></asp:requiredfieldvalidator>
                                            <asp:regularexpressionvalidator id="RegularExpressionValidator1" validationgroup="a" runat="server" display="Dynamic" controltovalidate="txtTdt" errormessage="Invalid To Date" text="<i class='fa fa-exclamation-circle' title='Invalid To Date !'></i>" setfocusonerror="true" validationexpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:regularexpressionvalidator>
                                        </span>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:textbox id="txtTdt" onkeypress="javascript: return false;" width="100%" maxlength="10" onpaste="return false;" placeholder="Select To Date" runat="server" autocomplete="off" cssclass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" clientidmode="Static"></asp:textbox>
                                        </div>
                                    </div>
                                </div>


                                <div class="col-md-1" style="margin-top: 20px;" runat="server">
                                    <div class="form-group">
                                        <asp:button runat="server" cssclass="btn btn-primary" onclick="btnSubmit_Click" validationgroup="a" id="btnSubmit" text="Search" accesskey="S" />
                                    </div>
                                </div>

                            </div>
                        </div>
                    </fieldset>

                    <asp:label id="lblmsgshow" visible="false" runat="server"></asp:label>

                    <fieldset runat="server" visible="false" id="FS_DailyReport">
                        <legend>Detail</legend>

                        


                        <div class="row" id="div_print">

                            <div class="col-md-12">
                                <div class="table-responsive">
                                   <asp:GridView ID="gv_SocietyMilkDispatchDetail" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover"
                                >
                                <Columns>
                                  
                                   
                                    <asp:TemplateField HeaderText="Invoice No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMilkCollectionInvoiceNo" runat="server" Text='<%# Eval("MilkCollectionInvoiceNo") %>'></asp:Label>
                                             <asp:Label ID="lblMilkCollectionInvoice_ID" CssClass="hidden" runat="server" Text='<%# Eval("MilkCollectionInvoice_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Billing From Date" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBillingCycleFromDate" runat="server" Text='<%# Eval("BillingCycleFromDate") %>'></asp:Label>
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Billing To Date" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBillingCycleToDate" runat="server" Text='<%# Eval("BillingCycleToDate") %>'></asp:Label>
                                           
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

                                    <asp:TemplateField HeaderText="Producer Payment">
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
                                        </asp:TemplateField>
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
                                   <%-- <asp:TemplateField HeaderText="Bank Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBankName" runat="server" Text='<%# Eval("BankName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="IFSC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIFSC" runat="server" Text='<%# Eval("IFSC") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Account No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBankAccountNo" runat="server" Text='<%# Eval("BankAccountNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="NetAmount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNetAmount" runat="server" Text='<%# Eval("NetAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="UTR No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUTRNo" runat="server" Text='<%# Eval("NP_ProducerUTRNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Payment Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaymentDate" runat="server" Text='<%# Eval("PaymentDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Style="padding: 3px; border-radius: 3px;" CssClass='<%# Eval("MilkCollectionInvoice_Status").ToString() == "1" ? "label label-success" : "label label-warning" %>' Text='<%# Eval("MilkCollectionInvoice_Status").ToString() == "1" ? "OK" : "Cancel" %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                 
                                     <asp:TemplateField HeaderText="View Report">
                                        <ItemTemplate>
                                           <asp:LinkButton ID="lblviewresult" OnClick="lblviewresult_Click"  CommandArgument='<%# Eval("MilkCollectionInvoice_ID").ToString()%>' runat="server" CausesValidation="False"><i class="fa fa-print" aria-hidden="true"></i></asp:LinkButton> 
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
            
        </section>
        
        <section class="content">
            <div id="dvReport" class="NonPrintable" runat="server">
            </div>
        </section>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script lang="javascript">
        function printdiv(printpage) {
            var headstr = "<html><head><title>Reported Society Invoice</title></head><body>";
            var footstr = "</body>";
            var newstr = "<center><h5>Society Invoice</h5></center><br/>" + document.all.item(printpage).innerHTML;
            var oldstr = document.body.innerHTML;
            document.body.innerHTML = headstr + newstr + footstr;
            window.print();
            document.body.innerHTML = oldstr;
            return false;
        }
        
    </script>

</asp:Content>

