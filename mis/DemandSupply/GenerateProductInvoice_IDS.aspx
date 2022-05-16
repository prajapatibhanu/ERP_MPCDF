<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" EnableEventValidation="true" AutoEventWireup="true" CodeFile="GenerateProductInvoice_IDS.aspx.cs" Inherits="mis_DemandSupply_GenerateProductInvoice_IDS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
         .columngreen {
            background-color: #aee6a3 !important;
        }

        .columnred {
            background-color: #f05959 !important;
        }

        .columnmilk {
            background-color: #bfc7c5 !important;
        }

        .columnproduct {
            background-color: #f5f376 !important;
        }
        .NonPrintable {
                  display: none;
              }
        @media print {
              .NonPrintable {
                  display: block;
              }
               @page {
                size: landscape;
            }
              
              .noprint {
                display: none;
            }
               .pagebreak { page-break-before: always; }
          }

       
         .table1-bordered > thead > tr > th, .table1-bordered > tbody > tr > th, .table1-bordered > tfoot > tr > th, .table1-bordered > thead > tr > td, .table1-bordered > tbody > tr > td, .table1-bordered > tfoot > tr > td {
            border: 1px solid black !important;
           
        }
        .thead
        {
            display:table-header-group;
        }
        .text-center{
            text-align: center;
        }
        .text-right{
            text-align: right;
        }
        .table1 > tbody > tr > td, .table1 > tbody > tr > th, .table1 > tfoot > tr > td, .table1 > tfoot > tr > th, .table1 > thead > tr > td, .table1 > thead > tr > th {
           padding: 5px ;
           
           font-size:15px;
              border: 1px solid black !important;
        }   
        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('../image/loader/ProgressImage.gif') 50% 50% no-repeat rgba(0, 0, 0, 0.3);
        }  
        .table1 > tbody > tr > td, .table1 > tbody > tr > th, .table1 > tfoot > tr > td, .table1 > tfoot > tr > th, .table1 > thead > tr > td, .table1 > thead > tr > th {
    padding: 1px;
    font-size: 10px;
    border: 1px solid black !important;
    font-family: verdana;
}    
    </style>
    <script>
        function checkAllbox(objRef) {
            var GridView = document.getElementById("<%=GridViewOrderDetails.ClientID %>");
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
        function Validate(sender, args) {
            var gridView = document.getElementById("<%=GridViewOrderDetails.ClientID %>");
            var checkBoxes = gridView.getElementsByTagName("input");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }


        function CheckOne(obj) {
            var grid = obj.parentNode.parentNode.parentNode;
            var inputs = grid.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox") {
                    if (obj.checked && inputs[i] != obj && inputs[i].checked) {
                        inputs[i].checked = false;
                    }
                }
            }
        }
    </script>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
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
    <asp:validationsummary id="vs" runat="server" validationgroup="a" showmessagebox="true" showsummary="false" />
    <asp:validationsummary id="ValidationSummary1" runat="server" validationgroup="b" showmessagebox="true" showsummary="false" />
    <asp:validationsummary id="v1" runat="server" validationgroup="ModalSave" showmessagebox="true" showsummary="false" />

    <div class="content-wrapper">
        <section class="content noprint">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Generate Product Invoice </h3>


                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:label id="lblMsg" runat="server"></asp:label>
                                </div>
                            </div>
                            <fieldset>
                                <legend>Generate Product Invoice
                                </legend>
                                <div class="row">

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Date<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:requiredfieldvalidator id="RequiredFieldValidator1" validationgroup="a"
                                                    errormessage="Enter Date" forecolor="Red" text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    controltovalidate="txtDate" display="Dynamic" runat="server">
                                                </asp:requiredfieldvalidator>
                                                <asp:regularexpressionvalidator id="RegularExpressionValidator3" validationgroup="b" runat="server" display="Dynamic" controltovalidate="txtDate"
                                                    errormessage="Invalid Date" text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" setfocusonerror="true"
                                                    validationexpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:regularexpressionvalidator>
                                            </span>
                                            <asp:textbox runat="server" TabIndex="1" AutoPostBack="true" OnTextChanged="txtDate_TextChanged" autocomplete="off" cssclass="form-control" id="txtDate" maxlength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" clientidmode="Static"></asp:textbox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Shift </label>
                                            <span class="pull-right">
                                                <asp:requiredfieldvalidator id="rfv1" validationgroup="a"
                                                    initialvalue="0" errormessage="Select Shift" text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"
                                                    controltovalidate="ddlShift" forecolor="Red" display="Dynamic" runat="server">
                                                </asp:requiredfieldvalidator>
                                            </span>
                                            <asp:dropdownlist id="ddlShift" TabIndex="2" OnSelectedIndexChanged="ddlShift_SelectedIndexChanged" AutoPostBack="true" runat="server" clientidmode="Static" cssclass="form-control select2"></asp:dropdownlist>
                                        </div>
                                    </div>
                                   <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Distributor Name<span style="color: red;">*</span></label>
                                         <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select Distributor Name" Text="<i class='fa fa-exclamation-circle' title='Select Distributor Name !'></i>"
                                            ControlToValidate="ddlDistributorName" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                         <asp:DropDownList ID="ddlDistributorName" ClientIDMode="Static" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                                      <div class="col-md-1" style="margin-top: 20px;">
                                            <div class="form-group">
                                                <asp:Button runat="server" TabIndex="5" CssClass="btn btn-primary" OnClick="btnSearch_Click" ValidationGroup="a" ID="btnSearch" Text="Search" />

                                            </div>
                                        </div>
                                      </div>
                                    <asp:panel id="pnloderdetails" runat="server" visible="false">
                             <asp:GridView ID="GridViewOrderDetails" runat="server" class="table table-striped table-bordered" AllowPaging="false"
                                                AutoGenerateColumns="false" EmptyDataText="No Record Found." EnableModelValidation="True">
                                                <Columns>
                                                <asp:TemplateField HeaderText="" ItemStyle-Width="30" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="b" ErrorMessage="Please select at least one record."
                                                            ClientValidationFunction="Validate" Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Please select at least one record. !'></i>" ForeColor="Red"></asp:CustomValidator>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" onclick="CheckOne(this)" OnCheckedChanged="chkSelect_CheckedChanged"  AutoPostBack="true" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order Id" HeaderStyle-Width="5px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOrderId" Text='<%#Eval("OrderId") %>' runat="server" />
                                                            <asp:Label ID="lblMilkOrProductDemandId" Visible="false" Text='<%# Eval("MilkOrProductDemandId") %>' runat="server" />
                                                        </ItemTemplate>
                                                        </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Retailer Name" HeaderStyle-Width="5px">
                                                         <ItemTemplate>
                                                            <asp:Label ID="lblBName" Text='<%#Eval("BName") %>' runat="server" />
                                                            <asp:Label ID="lblBoothId" Visible="false" Text='<%#Eval("BoothId") %>' runat="server" />
                                                        </ItemTemplate>
                                                        </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Vehicle No" HeaderStyle-Width="5px">
                                                         <ItemTemplate>
                                                            <asp:Label ID="lblVehicleNo" Text='<%#Eval("VehicleNo") %>' runat="server" />
                                                            <asp:Label ID="lblVehicleMilkOrProduct_ID" Visible="false" Text='<%#Eval("VehicleMilkOrProduct_ID") %>' runat="server" />
                                                             <asp:Label ID="lblAreaId" Visible="false" Text='<%#Eval("AreaId") %>' runat="server" />
                                                              <asp:Label ID="lblRouteId" Visible="false" Text='<%#Eval("RouteId") %>' runat="server" />
                                                             <asp:Label ID="lblDistributorId" Visible="false" Text='<%#Eval("DistributorId") %>' runat="server" />
                                                             <asp:Label ID="lblSuperStockistId" Visible="false" Text='<%#Eval("SuperStockistId") %>' runat="server" />
                                                        </ItemTemplate>
                                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DM Type" HeaderStyle-Width="5px">
                                                         <ItemTemplate>
                                                            <asp:Label ID="lblPDMStatus" Text='<%#Eval("PDMStatus") %>' runat="server" />
                                                              <asp:Label ID="lblProductDMStatus" Visible="false" Text='<%#Eval("ProductDMStatus") %>' runat="server" />
                                                        </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    </asp:GridView>
                                   </asp:panel>
                                   
                                   
                            </fieldset>
                            <div class="row" id="pnldata" runat="server" visible="false">
                                <fieldset>
                                    <legend>Item Details
                                    </legend>
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" ShowFooter="true" runat="server" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound" EmptyDataText="No Record Found." EnableModelValidation="True">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product" HeaderStyle-Width="5px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemName" Text='<%#Eval("ItemName") %>' runat="server" />
                                                            <asp:Label ID="lblItem_id" Visible="false" Text='<%#Eval("Item_id") %>' runat="server" />
                                                            <asp:Label ID="lblIntegratedTax" Visible="false" Text='<%#Eval("IntegratedTax") %>' runat="server" />
                                                            <asp:Label ID="lblCGST" Visible="false" Text='<%#Eval("CGST") %>' runat="server" />
                                                            <asp:Label ID="lblSGST" Visible="false" Text='<%#Eval("SGST") %>' runat="server" />
                                                            <asp:Label ID="lblCGSTAmt" Visible="false" Text='<%#Eval("CGSTAmt") %>' runat="server" />
                                                            <asp:Label ID="lblSGSTAmt" Visible="false" Text='<%#Eval("SGSTAmt") %>' runat="server" />
                                                            <asp:Label ID="lblRate"  Visible="false" Text='<%#Eval("Rate") %>' runat="server" />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <b>Total</b>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit" HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQty" Text='<%#Eval("TotalQty") %>' runat="server" />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFTotalMilkQty" Font-Bold="true" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Qty (In Ltr/KG)" HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQtyInLtr" Text='<%#Eval("QtyInLtr") %>' runat="server" />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                             <asp:Textbox ID="txtFQtyInLtre" Width="60px" autocompleted="off" MaxLength="5" Font-Bold="true" runat="server"></asp:Textbox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Crate Qty." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                           <asp:Label ID="lblCrate" Text='<%# (Eval("CarriageModeID").ToString()!="2" ? Eval("Crate") :"0") %>' runat="server" />
                                                        </ItemTemplate>
                                                      <FooterTemplate>
                                                            <asp:Textbox ID="txtFTotalCrate" Width="60px" autocompleted="off" MaxLength="5" onkeypress="return validateNum(event);" Font-Bold="true" runat="server"></asp:Textbox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Box/Jar Qty." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBox" Text='<%# (Eval("CarriageModeID").ToString()!="1" ? Eval("Box") :"0") %>' runat="server" />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Textbox ID="txtFTotalBox" Width="60px" autocompleted="off" MaxLength="5" onkeypress="return validateNum(event);" Font-Bold="true" runat="server"></asp:Textbox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
													 <asp:TemplateField HeaderText="Extra/Short Pkt" HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblExtraShort" Text='<%# Eval("ExtraShort").ToString() %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Integrated Tax" HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIntegratedTax" Text='<%#Eval("IntegratedTax") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CGST %" HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCGST" Text='<%#Eval("CGST") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SGST " HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSGST" Text='<%#Eval("SGST") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CGST Amt" HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCGSTAmt" Text='<%#Eval("CGSTAmt") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SGST Amt" HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSGSTAmt" Text='<%#Eval("SGSTAmt") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRate" Text='<%#Eval("Rate") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Rate (Including GST)" HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRateincludingGST" Text='<%#Eval("RateIncludingGST") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" Text='<%#Eval("Amount") %>' runat="server" />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFAmount" Font-Bold="true"  runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                            <div class="col-md-3 pull-right">
                                                <table class="table table1-bordered">
                                                    <tr>
                                                        <td>
                                                            Tcs on Sales @ <asp:Label ID="lblTcsTax" Font-Bold="true" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblTcsTaxAmt" Font-Bold="true" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                           Final Payble Amount
                                                        </td>
                                                        <td>
                                                           <asp:Label ID="lblFinalPaybleAmount" Font-Bold="true" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                 
                                            </div>
                                        </div>
                                     <div class="col-md-12">
                                    <div class="form-group">
                                         <label>Remark</label>
                                        <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ForeColor="Red"
                                            ValidationGroup="b" Display="Dynamic" runat="server"
                                            ControlToValidate="txtRemark"
                                            ErrorMessage="Alphanumeric ,space and some special symbols like '.,/-:' allow"
                                            Text="<i class='fa fa-exclamation-circle' title='Alphanumeric ,space and some special symbols like '.,/-:' allow !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^[0-9a-zA-Z\s.,'%/:-]+$">

                                        </asp:RegularExpressionValidator>

                                    </span>
                                      <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtRemark" MaxLength="200" placeholder="Enter Remark" ClientIDMode="Static"></asp:TextBox>

                                    </div>
                                </div>
                                        <div class="col-md-1" style="margin-top: 20px;">
                                            <div class="form-group">
                                                <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="b" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />

                                            </div>
                                            </div>
                                        <div class="col-md-1" style="margin-top: 20px;">
                                        <div class="form-group">
                                             <asp:button id="btnClear" Visible="false" onclick="btnClear_Click" cssclass="btn btn-default" text="Clear" runat="server" />
                                            </div>
                                         </div>
                               
                                </fieldset>
                            </div>
                            
                        </div>

                    </div>

                </div>
            </div>
        </section>
          <section class="content">
            <div class="col-md-6">
            <div id="Print" runat="server" class="NonPrintable"></div>  
                </div>  
             <div class="col-md-6"> 
             <div id="Print1" runat="server" class="NonPrintable"></div>   
                 </div>
        </section>


    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
     <asp:Label ID="lbldistributerMONO" runat="server" Visible="false"></asp:Label>
    <script>
                  function myVehicleDetailsModal() {
                $("#VehicleDetailsModal").modal('show');

            }
            function ValidatePage() {

                if (typeof (Page_ClientValidate) == 'function') {
                    Page_ClientValidate('b');
                }

                if (Page_IsValid) {
                    debugger;
                    if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
    </script>
     
</asp:Content>
