<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CCwiseSocietyInvoiceDetails.aspx.cs" Inherits="mis_MilkCollection_CCwiseSocietyInvoiceDetails" %>

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

    <script type="text/javascript">
        function confirmation() {
            if (confirm('Are you sure you want to save Society Payment In excel format ?')) {
                return true;
            } else {
                return false;
            }
        }
   
<%--         function CheckAll(oCheckbox) {
             var GridView2 = document.getElementById("<%=gv_SocietyMilkDispatchDetail.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }--%>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
   
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content noprint">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">
                        <asp:Label ID="lblUnitName" Text="CC Wise Society Invoice" ForeColor="White" ClientIDMode="Static" Font-Size="Medium" Font-Bold="true" runat="server"></asp:Label></h3>
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


                                <div class="col-md-1" style="margin-top: 20px;" runat="server">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click" ValidationGroup="a" ID="btnSubmit" Text="Search" AccessKey="S" />
                                    </div>
                            </div>

                        </div>
                </div>
                </fieldset>

                    <asp:Label ID="lblmsgshow" Visible="false" runat="server"></asp:Label>

                <fieldset runat="server" visible="false" id="FS_DailyReport">
                    <legend>Detail</legend>

                    <div class="row">

                       
                       
                        <div class="col-md-2 pull-right">
                            <div class="form-group">
                                <asp:Button ID="btnSave" runat="server" Text="Export To Excel" OnClientClick="return confirmation();" CssClass="btn btn-primary btn-block" OnClick="btnSave_Click" />
                            </div>
                        </div>
                        <div class="col-md-2 pull-right">
                            <div class="form-group">
                                <asp:Button runat="server" ID="btnPrint" Text="Print (Ctrl + P)" CssClass="btn btn-default pull-right" AccessKey="p" OnClientClick="printdiv('div_print');" />
                            </div>
                        </div>
                         <div class="col-md-2 pull-right">
                            <div class="form-group">
                                <asp:Button runat="server" style="margin-right:-50px;"  ID="btnPrintInvoice" Text="Invoice Print" CssClass="btn btn-default pull-right" AccessKey="p"  OnClick="btnPrintInvoice_Click"/>
                            </div>
                        </div>
                         
                    </div>


                    <div class="row">

                        <div class="col-md-12">
                            <div class="table-responsive">
                            <asp:GridView ID="gv_SocietyMilkDispatchDetail" ShowFooter="true" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover"
                                >
                                <Columns>
                                    <asp:TemplateField ControlStyle-CssClass="noprint">
                                                <HeaderTemplate>
                                                    <input id="Checkbox2" type="checkbox" onclick="checkAllbox(this)" runat="server" />
                                                    All
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" Visible='<%# Eval("NP_ProducerUTRNo").ToString()==""?true:false%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                     <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowno" runat="server" Text='<%# Container.DataItemIndex+ 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMilkCollectionInvoiceNo" runat="server" Text='<%# Eval("MilkCollectionInvoiceNo") %>'></asp:Label>
                                             <asp:Label ID="lblMilkCollectionInvoice_ID" Visible="false" runat="server" Text='<%# Eval("MilkCollectionInvoice_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Society Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSocietyCode" runat="server" Text='<%# Eval("Office_Code") %>'></asp:Label>
                                            
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
									  <asp:TemplateField HeaderText="Milk Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMilkQuantity" runat="server" Text='<%# Eval("MilkQuantity") %>'></asp:Label>
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
										<asp:TemplateField HeaderText="Earning">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEarning" runat="server" Text='<%# Eval("Earning") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GrossEarning">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrossEarning" runat="server" Text='<%# Eval("GrossEarning") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Deduction">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeductionAdditionValue" runat="server" Text='<%# Eval("Deduction") %>'></asp:Label>
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
                                 

                                    <asp:TemplateField HeaderText="NetAmount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNetAmount" runat="server" Text='<%# Eval("NetAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                                                      
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
<div class="row" id="div_print">

                        <div class="col-md-12">
                            
                            <asp:GridView ID="GridView1"  ShowFooter="true" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover NonPrintable"
                                >
                                <Columns>
                                   

                                     <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowno" runat="server" Text='<%# Container.DataItemIndex+ 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMilkCollectionInvoiceNo" runat="server" Text='<%# Eval("MilkCollectionInvoiceNo") %>'></asp:Label>
                                             <asp:Label ID="lblMilkCollectionInvoice_ID" Visible="false" runat="server" Text='<%# Eval("MilkCollectionInvoice_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Society Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSocietyCode" runat="server" Text='<%# Eval("Office_Code") %>'></asp:Label>
                                            
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
									  <asp:TemplateField HeaderText="Milk Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMilkQuantity" runat="server" Text='<%# Eval("MilkQuantity") %>'></asp:Label>
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
										<asp:TemplateField HeaderText="Earning">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEarning" runat="server" Text='<%# Eval("Earning") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GrossEarning">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrossEarning" runat="server" Text='<%# Eval("GrossEarning") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Deduction">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeductionAdditionValue" runat="server" Text='<%# Eval("Deduction") %>'></asp:Label>
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
                                   

                                    <asp:TemplateField HeaderText="NetAmount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNetAmount" runat="server" Text='<%# Eval("NetAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    

                                  
                                </Columns>
                            </asp:GridView>
                            
                        </div>
                    </div>
                </fieldset>


            </div>
    </div>

	
	</section>
        <%--Confirmation Modal Start --%>

    
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="Save" ShowMessageBox="true" ShowSummary="false" />
        <section class="content">
            <div id="dvReport" class="NonPrintable" runat="server">
            </div>
        </section>
		 <asp:HiddenField ID="hfOffice" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="hfcc" ClientIDMode="Static"  runat="server" />
        <asp:HiddenField ID="hfBillingPeriod" ClientIDMode="Static"  runat="server" />
    </div>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script lang="javascript">
        function printdiv(printpage) {
		var OfcName = document.getElementById('<%= hfOffice.ClientID %>').value;
            var CC = document.getElementById('<%= hfcc.ClientID %>').value;
            var BillingPeriod = document.getElementById('<%= hfBillingPeriod.ClientID %>').value;
            var headstr = "<html><head><title>" + OfcName + "<br/>" + CC + "<br/>" + BillingPeriod + "</title></head><body>";
            //var headstr = "<html><head><title>Reported Society Invoice</title></head><body>";
            var footstr = "</body>";
            var newstr = "<center><h5>" + OfcName + "<br/>CC: " + CC + "<br/>" + BillingPeriod + "</h5></center><br/>" + document.all.item(printpage).innerHTML;
            var oldstr = document.body.innerHTML;
            document.body.innerHTML = headstr + newstr + footstr;
            window.print();
            document.body.innerHTML = oldstr;
            return false;
        }
      
      

    </script>

</asp:Content>

