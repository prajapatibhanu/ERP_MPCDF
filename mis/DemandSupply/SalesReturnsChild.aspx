<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SalesReturnsChild.aspx.cs" Inherits="mis_DemandSupply_SalesReturnsChild" %>

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
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    
   
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="c" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">


                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Sales Return - Retailer/Institution List  </h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <fieldset>
                                <legend>Sales Return - Retailer/Institution List
                                </legend>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-10">
                                        <div class="form-group">
                                            Details of :<span id="spanRDIName" style="color: red" runat="server"></span>&nbsp;&nbsp; 
                               Date :<span id="SpanDate" style="color: red" runat="server"></span>&nbsp;&nbsp; 
                               Shift : <span id="spanShift" style="color: red" runat="server"></span>&nbsp;&nbsp;
                               Category : <span id="spanCategory" style="color: red" runat="server"></span>
                                        </div>
                                    </div>
                                    <div class="col-md-2 pull-right">
                                        <div class="form-group">
                                            <asp:Button ID="lnkbtnback" PostBackUrl="~/mis/DemandSupply/SalesReturns.aspx" Text="Back" CssClass="btn btn-secondary" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                </div>
                                <div class="row" id="pnlparlourdetails" runat="server">

                                    <div class="col-md-12">

                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" ShowFooter="false" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                AutoGenerateColumns="false" EmptyDataText="No Record Found." OnRowDataBound="GridView1_RowDataBound" DataKeyNames="MilkOrProductDemandId" EnableModelValidation="True">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                            <asp:Label ID="lbMilkOrProductReturnId" Visible="false"  Text='<%# Eval("MilkOrProductReturnId") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Challan No">
                                                        <ItemTemplate>
                                                             <asp:LinkButton ID="lnkActiveChallanNo" CssClass="btn btn-secondary" CommandName="Challanno" CommandArgument='<%#Eval("MilkOrProductDemandId") %>' Text='<%#Eval("ChallanNo") %>' runat="server"></asp:LinkButton>
                                                             <asp:LinkButton ID="lnkDeActiveChallanNo" Visible="false" CommandName="ChallannoUpdate" CommandArgument='<%#Eval("MilkOrProductDemandId") %>' CssClass="btn btn-warning" Text='<%#Eval("ChallanNo") %>' runat="server"></asp:LinkButton>
                                                            </ItemTemplate>
                                                         </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Retailer/Institution Name" HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBandOName" Text='<%# Eval("BandOName") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Delivery Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDelivaryDate" Text='<%# Eval("Delivary_Date") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delivery Shift">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeliveryShiftName" Text='<%# Eval("DeliveryShiftName") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Order ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOrderId" Text='<%# Eval("OrderId") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Order Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDemandDate" Text='<%# Eval("Demand_Date") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order Shift">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDemandShiftName" Text='<%# Eval("DemandShiftName") %>' runat="server" />
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

            <div class="modal" id="ItemDetailsModal">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">×</span></button>
                                    <h4 class="modal-title">Item Details for <span id="modalBoothName" style="color: red" runat="server"></span>&nbsp;&nbsp;
                             Delivery Date :<span id="modaldate" style="color: red" runat="server"></span>&nbsp;&nbsp;Delivery Shift :<span id="modalshift" style="color: red" runat="server"></span></h4>
                                </div>
                                <div class="modal-body">
                                    <div id="divitem" runat="server">
                                        <asp:Label ID="lblModalMsg" runat="server" Text=""></asp:Label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <fieldset>
                                                    <legend>Item Details for Sales Return</legend>
                                                    <div class="row">
                                                         <div class="col-md-4">
                                     <div class="form-group">
                                       <label> Sales Return Date / बिक्री वापसी दिनांक<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                ErrorMessage="Enter Sales Return Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Sales Return Date !'></i>"
                                                ControlToValidate="txtSalesReturnDate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtSalesReturnDate"
                                                ErrorMessage="Invalid Sales Return Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Sales Return Date !'></i>" SetFocusOnError="true"
                                                ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                        </span>
                                        <%--<asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtSalesReturnDate"  MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-end-date="1d" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>--%>
                                         <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtSalesReturnDate"  MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                     </div>
                                                    </div>
                                                    <div class="row" style="height: 280px; overflow: scroll;">
                                                        <div class="col-md-12">
                                                            <div class="table-responsive">
                                                                <asp:GridView ID="GridView2" runat="server" OnRowDataBound="GridView2_RowDataBound" EmptyDataText="No Record Found" class="table table-striped table-bordered" AllowPaging="false"
                                                                    AutoGenerateColumns="False" EnableModelValidation="True" DataKeyNames="MilkOrProductDemandChildId">
                                                                    <Columns>
                                                                          <asp:TemplateField HeaderText="" ItemStyle-Width="30" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                                               <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="a" ErrorMessage="Please select at least one record."
                                            ClientValidationFunction="Validate" Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Please select at least one record. !'></i>" ForeColor="Red"></asp:CustomValidator>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="SNo./ क्र." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Item Category / वस्तु वर्ग">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblItemCatName" Text='<%# Eval("ItemCatName")%>' runat="server" />
                                                                                <asp:Label ID="lblItemid" Visible="false" Text='<%# Eval("Item_id")%>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Item Name / वस्तु नाम">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblIName" Text='<%# Eval("ItemName")%>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                           <asp:TemplateField HeaderText="Total Supply Qty./ कुल आपूर्ति मात्रा">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSupplyTotalQty" Text='<%# Eval("SupplyTotalQty")%>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Total Return Qty./ कुल वापसी मात्रा <asp:CustomValidator ID="CustomValidator2" runat="server" Text="<i class='fa fa-exclamation-circle' title='Please Enter at least one Supply Return Qty. !'></i>" ValidationGroup="a" ErrorMessage="Please Enter at least one Supply Return Qty."
                                            ClientValidationFunction="ReturnQtyValidate" ForeColor="Red"></asp:CustomValidator>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>                                                          
                                                                                <asp:RegularExpressionValidator ID="rev1" Enabled="false" runat="server" Display="Dynamic" ValidationGroup="a"
                                                                                    ErrorMessage="Enter Valid Number In Quantity Field !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Number In Quantity Field. !'></i>" ControlToValidate="txtTotalReturnQty"
                                                                                    ValidationExpression="^[0-9]*$">
                                                                                </asp:RegularExpressionValidator>
                                                                                </span>
                                                                          <asp:TextBox runat="server" autocomplete="off" Text="0" CausesValidation="true" onpaste="return false;"  CssClass="form-control" ID="txtTotalReturnQty" MaxLength="5" onkeypress="return validateNum(event);" placeholder="Enter Total Return Qty." ClientIDMode="Static"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                               Remark/ टिप्पणी    <%--<asp:CustomValidator ID="CustomValidator3" runat="server" Text="<i class='fa fa-exclamation-circle' title='Please Enter at least one Remark.. !'></i>" ValidationGroup="a" ErrorMessage="Please Enter at least one Remark."
                                            ClientValidationFunction="RemarkValidate" ForeColor="Red"></asp:CustomValidator>--%>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>  
                                                                          <asp:TextBox runat="server" autocomplete="off" CausesValidation="true"  CssClass="form-control" ID="txtRemark" MaxLength="200" placeholder="Enter Remark" ClientIDMode="Static"></asp:TextBox>
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
                                <div class="modal-footer">
                                  
                                    <%--<button type="button" class="btn btn-default" data-dismiss="modal">CLOSE </button>--%>
                                     

                                         <div class="row">
                                         
                                    <div class="col-md-2" style="margin-top:20px;">
                                        <div class="form-group">
                                            <asp:Button runat="server" Visible="false" CssClass="btn btn-block btn-primary" ValidationGroup="a" OnClientClick="return ValidatePage();" AccessKey="S" ID="btnSubmit" Text="Submit" />
                                        </div>
                                    </div>
                                             </div>
                                            
                                </div>
                                </div>
                            </div>
                            <!-- /.modal-content -->
                        </div>

            <div class="modal" id="ItemDetailsModal1">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">×</span></button>
                                    <h4 class="modal-title">Item Details for <span id="modalBoothName1" style="color: red" runat="server"></span>&nbsp;&nbsp;
                             Delivery Date :<span id="modaldate1" style="color: red" runat="server"></span>&nbsp;&nbsp;Delivery Shift :<span id="modalshift1" style="color: red" runat="server"></span></h4>
                                </div>
                                <div class="modal-body">
                                    <div id="div1" runat="server">
                                        <asp:Label ID="lblModalMsg1" runat="server" Text=""></asp:Label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <fieldset>
                                                    <legend>Item Details for Sales Return</legend>
                                                    <div class="row" style="height: 280px; overflow: scroll;">
                                                        <div class="col-md-12">
                                                            <div class="table-responsive">
                                                                <asp:GridView ID="GridView3" runat="server" OnRowDataBound="GridView3_RowDataBound" OnRowCommand="GridView3_RowCommand" EmptyDataText="No Record Found" class="table table-striped table-bordered" AllowPaging="false"
                                                                    AutoGenerateColumns="False" EnableModelValidation="True" DataKeyNames="MilkOrProductReturnChildId">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="SNo./ क्र." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Item Category / वस्तु वर्ग">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblItemCatName" Text='<%# Eval("ItemCatName")%>' runat="server" />
                                                                                <asp:Label ID="lblItemCat_id" Visible="false" Text='<%# Eval("ItemCat_id")%>' runat="server" />
                                                                                <asp:Label ID="lblLastUpdatedStatus" Visible="false" Text='<%# Eval("LastUpdatedStatus")%>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Item Name / वस्तु नाम">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblIName" Text='<%# Eval("ItemName")%>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                          <asp:TemplateField HeaderText="Total Supply Qty./ कुल आपूर्ति मात्रा">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSupplyTotalQty" Text='<%# Eval("SupplyTotalQty")%>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Total Return Qty./ कुल वापसी मात्रा">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTotalReturnQty" Text='<%# Eval("TotalReturnQty")%>' runat="server" />
                                                                                <asp:RequiredFieldValidator ID="rfvtrq" ValidationGroup="c"
                                                                                    ErrorMessage="Enter Total Return Qty." Enabled="false" Text="<i class='fa fa-exclamation-circle' title='Enter Total Return Qty. !'></i>"
                                                                                    ControlToValidate="txtTotalReturnQty" ForeColor="Red" Display="Dynamic" runat="server">
                                                                                </asp:RequiredFieldValidator>
                                                                                <asp:RegularExpressionValidator ID="revtrq" Enabled="false" runat="server" Display="Dynamic" ValidationGroup="c"
                                                                                    ErrorMessage="Enter Valid Number In Total Return Qty. Field" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Number In Total Return Qty. Field!'></i>" ControlToValidate="txtTotalReturnQty"
                                                                                    ValidationExpression="^[0-9]*$">
                                                                                </asp:RegularExpressionValidator>
                                                                                <asp:CustomValidator ID="CustomValidator1" Enabled="false" runat="server" Display="Dynamic" ForeColor="red" ErrorMessage="Total Return Qyt. lest than or equal to Total Supply Qty." Text="<i class='fa fa-exclamation-circle' title='Total Return Qyt. lest than or equal to Total Supply Qty. !'></i>" OnServerValidate="CustomValidator1_ServerValidate" ValidateEmptyText="true" ValidationGroup="c"></asp:CustomValidator>
                                                                                </span>
                                                                               <asp:TextBox runat="server" autocomplete="off" Visible="false" CausesValidation="true" Text='<%# Eval("TotalReturnQty")%>' CssClass="form-control" ID="txtTotalReturnQty" MaxLength="5" onpaste="return false;" onkeypress="return validateNum(event);" placeholder="Enter Total Return Qty." ClientIDMode="Static"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Sales Return Date / बिक्री वापसी दिनांक">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSalesReturnDate" Text='<%# Eval("SalesReturnDate")%>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Action / कार्यवाही करें">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkEdit" CommandName="RecordEdit" Visible='<%# Eval("LastUpdatedStatus").ToString()=="" ? true: false %>' CommandArgument='<%#Eval("MilkOrProductReturnChildId") %>' runat="server" ToolTip="Edit"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                                                &nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkUpdate" Visible="false" ValidationGroup="c" CausesValidation="true" CommandName="RecordUpdate" CommandArgument='<%#Eval("MilkOrProductReturnChildId") %>' runat="server" ToolTip="Update" Style="color: darkgreen;" OnClientClick="return confirm('Are you sure to Update?')"><i class="fa fa-check"></i></asp:LinkButton>
                                                                                &nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkReset" Visible="false" CommandName="RecordReset" CommandArgument='<%#Eval("MilkOrProductReturnChildId") %>' runat="server" ToolTip="Reset" Style="color: gray;"><i class="fa fa-times"></i></asp:LinkButton>
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
                                <div class="modal-footer">
                                  
                                    <%--<button type="button" class="btn btn-default" data-dismiss="modal">CLOSE </button>--%>
                                     

                                         <div class="row">
                                         
                                    <div class="col-md-2" style="margin-top:20px;">
                                        <div class="form-group">
                                            <asp:Button runat="server" Visible="false" CssClass="btn btn-block btn-primary" ValidationGroup="a" OnClientClick="return ValidatePage();" AccessKey="S" ID="Button1" Text="Submit" />
                                        </div>
                                    </div>
                                             </div>
                                            
                                </div>
                                </div>
                            </div>
                            <!-- /.modal-content -->
                        </div>
                        <!-- /.modal-dialog -->
                   <%--Confirmation Modal Start --%>
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
        </section>
        <!-- /.content -->

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
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
            var gridView = document.getElementById("<%=GridView2.ClientID %>");
            var checkBoxes = gridView.getElementsByTagName("input");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
        function ReturnQtyValidate(sender, args) {
            var gridView = document.getElementById("<%=GridView2.ClientID %>");
            var txtreturnqty = gridView.getElementsByTagName("input");
            for (var i = 0; i < txtreturnqty.length; i++) {
                    if (txtreturnqty[i].value != "" && txtreturnqty[i].type == "text") {
                        args.IsValid = true;
                        return;
                    }
            }
            args.IsValid = false;
        }
        <%--function RemarkValidate(sender, args) {
            var gridView = document.getElementById("<%=GridView2.ClientID %>");
            var txtremark = gridView.getElementsByTagName("input");
            for (var i = 0; i < txtremark.length; i++) {
                if (txtremark[i].value != "" && txtremark[i].type == "text") {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }--%>
        function myItemDetailsModal() {
            $("#ItemDetailsModal").modal('show');

        }
        function myItemDetailsModal1() {
            $("#ItemDetailsModal1").modal('show');

        }
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {
                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Submit") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
    </script>
</asp:Content>

