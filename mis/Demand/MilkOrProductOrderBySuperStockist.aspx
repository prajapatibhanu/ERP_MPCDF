<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MilkOrProductOrderBySuperStockist.aspx.cs" Inherits="mis_Demand_MilkOrProductOrderBySuperStockist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
  <link href="../Finance/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <style>
        .columngreen {
            background-color: #aee6a3 !important;
        }

        .columnred {
            background-color: #ffb4b4 !important;
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
    <asp:Label ID="lbldistributerMONO" runat="server" Visible="false"></asp:Label>
    <%--Confirmation Modal Start --%>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
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
                    <asp:Button runat="server" CssClass="btn btn-success"  TabIndex="8" Text="Yes" ID="btnYes" OnClick="btnSubmit_Click" Style="margin-top: 20px; width: 50px;" />
                    <asp:Button ID="btnNo" ValidationGroup="no" runat="server" TabIndex="9" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="c" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="d" ShowMessageBox="true" ShowSummary="false" />
     <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="e" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="row">      
                         
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                          
                <div class="col-md-6">
                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">Daily Place/View Order / दैनिक आर्डर दें / देखें </h3>
                        </div>
                        
                        <div class="box-body">
                            <fieldset>
                                <legend>Date ,Shift ,Category / दिनांक, शिफ्ट, वस्तु वर्ग</legend>
                              
                                <div class="row">

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Date<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    ControlToValidate="txtOrderDate" Display="None" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="b"
                                                    ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    ControlToValidate="txtOrderDate" Display="None" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="d"
                                                    ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    ControlToValidate="txtOrderDate" Display="None" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="None" ControlToValidate="txtOrderDate"
                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="b" runat="server" Display="None" ControlToValidate="txtOrderDate"
                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="d" runat="server" Display="None" ControlToValidate="txtOrderDate"
                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                           <%-- <asp:TextBox runat="server" autocomplete="off" OnTextChanged="txtOrderDate_TextChanged" AutoPostBack="true" CssClass="form-control" ID="txtOrderDate" MaxLength="10" placeholder="Enter Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" data-date-start-date="-5d" ClientIDMode="Static"></asp:TextBox>--%>
                                             <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOrderDate" MaxLength="10" placeholder="Enter Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static" TabIndex="1"></asp:TextBox>
                                           
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Shift <%--/ शिफ्ट--%><span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Shift" Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"
                                                    ControlToValidate="ddlShift" ForeColor="Red" Display="None" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="b"
                                                    InitialValue="0" ErrorMessage="Select Shift" Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"
                                                    ControlToValidate="ddlShift" ForeColor="Red" Display="None" runat="server">
                                                </asp:RequiredFieldValidator>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="d"
                                                    InitialValue="0" ErrorMessage="Select Shift" Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"
                                                    ControlToValidate="ddlShift" ForeColor="Red" Display="None" runat="server">
                                                </asp:RequiredFieldValidator>

                                            </span>
                                           <%-- <asp:DropDownList ID="ddlShift" OnInit="ddlShift_Init" AutoPostBack="true" OnSelectedIndexChanged="ddlShift_SelectedIndexChanged" runat="server" CssClass="form-control select2"></asp:DropDownList>--%>
                                             <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                       <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Item Category <%--/ वस्तु वर्ग--%><span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Category" Text="<i class='fa fa-exclamation-circle' title='Select Category !'></i>"
                                                    ControlToValidate="ddlItemCategory" ForeColor="Red" Display="None" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="b"
                                                    InitialValue="0" ErrorMessage="Select Category" Text="<i class='fa fa-exclamation-circle' title='Select Category !'></i>"
                                                    ControlToValidate="ddlItemCategory" ForeColor="Red" Display="None" runat="server">
                                                </asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="d"
                                                    InitialValue="0" ErrorMessage="Select Category" Text="<i class='fa fa-exclamation-circle' title='Select Category !'></i>"
                                                    ControlToValidate="ddlItemCategory" ForeColor="Red" Display="None" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <%--<asp:DropDownList ID="ddlItemCategory" AutoPostBack="true" OnInit="ddlItemCategory_Init" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged" runat="server" CssClass="form-control select2"></asp:DropDownList>--%>
                                            <asp:DropDownList ID="ddlItemCategory" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged" AutoPostBack="true" runat="server" TabIndex="2" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Vehicle No<span style="color: red;"> *</span></label>
                                           <span class="pull-right">
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Vehicle" Text="<i class='fa fa-exclamation-circle' title='Select Vehicle !'></i>"
                                                    ControlToValidate="ddlVehicleNo" ForeColor="Red" Display="None" runat="server">
                                                </asp:RequiredFieldValidator>
                                               </span>
                                         <asp:DropDownList ID="ddlVehicleNo" runat="server" TabIndex="3" CssClass="form-control select2">
                                         </asp:DropDownList>
                                        </div>
                                        </div>
                                      <div class="col-md-4">
                                        <div class="form-group">
                                             <label>DM Type<span style="color: red;"> *</span></label>
                                            <asp:DropDownList ID="ddlDMType" runat="server" TabIndex="4" CssClass="form-control select2">
                                                <asp:ListItem Text="Regular Demand" Value="0"></asp:ListItem>
                                                 <asp:ListItem Text="Current Demand" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                            </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:LinkButton ID="lnkSearch" runat="server" TabIndex="5" ValidationGroup="a" OnClick="lnkSearch_Click" CssClass="btn btn-secondary" ClientIDMode="Static" AccessKey="V"><i class="fa fa-eye"></i> View Demand</asp:LinkButton>

                                            <asp:LinkButton ID="lnkAddDemand" ValidationGroup="a"  TabIndex="6" runat="server" OnClick="lnkAddDemand_Click" CssClass="btn btn-primary" ClientIDMode="Static" AccessKey="A"><i class="fa fa-plus"></i> Add Demand</asp:LinkButton>

                                            <%-- <asp:LinkButton ID="lnkPreviousOrder" ValidationGroup="d" OnClick="lnkPreviousOrder_Click" CssClass="btn btn-success"  runat="server" AccessKey=""><i class="fa fa-backward"></i> Previous Demand </asp:LinkButton>--%>
                                        </div>
                                    </div>
                                </div>
                                    <div class="row" id="pnlmilktimeline" runat="server" visible="false">
                                          <div class="col-md-12">
                                               <div class="form-group">
                                        <span style="color:red" id="pnlmilkMD" runat="server"></span>
                                                <br />   <span style="color:red" id="pnlmilkED" runat="server"></span>
                                                   </div>
                                              </div>
                                    </div>
                                 <div class="row" id="pnlproducttimeline" runat="server" visible="false">
                                          <div class="col-md-12">
                                               <div class="form-group">
                                        <span style="color:red" id="pnlproductMD" runat="server"></span>
                                                   </div>
                                              </div>
                                    </div>
                                <div class="row" id="pnladddemand" runat="server" visible="false">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Retailer / विक्रेता<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="b"
                                                    InitialValue="0" ErrorMessage="Select Retailer" Text="<i class='fa fa-exclamation-circle' title='Select Retailer !'></i>"
                                                    ControlToValidate="ddlRetailer" ForeColor="Red" Display="None" runat="server">
                                                </asp:RequiredFieldValidator>
                                               <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="d"
                                                    InitialValue="0" ErrorMessage="Select Retailer" Text="<i class='fa fa-exclamation-circle' title='Select Retailer !'></i>"
                                                    ControlToValidate="ddlRetailer" ForeColor="Red" Display="None" runat="server">
                                                </asp:RequiredFieldValidator>--%>

                                            </span>
                                            <%--<asp:DropDownList ID="ddlRetailer" AutoPostBack="true" OnInit="ddlRetailer_Init" OnSelectedIndexChanged="ddlRetailer_SelectedIndexChanged" runat="server" CssClass="form-control select2"></asp:DropDownList>--%>
                                            <asp:DropDownList ID="ddlRetailer" AutoPostBack="true"  TabIndex="6" OnSelectedIndexChanged="ddlRetailer_SelectedIndexChanged" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>


                                 
                                   
                                </div>

                            </fieldset>
                            <div class="row" id="pnlProduct" runat="server" visible="false">
                                <fieldset>
                                        <legend>
                                            <asp:Label ID="lblCartMsg" Text="" runat="server"></asp:Label>
                                        </legend>
                                 <%-- <div class="col-md-4 pull-right">
                                        <div class="form-group">
                                    <asp:LinkButton ID="lnkPreviousOrder" ValidationGroup="d" OnClick="lnkPreviousOrder_Click" CssClass="btn btn-block btn-primary"  runat="server" Text="Previous Demand"></asp:LinkButton>
                                            </div>
                                         </div>--%>
                                <div class="col-md-12">
                                    
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" runat="server" class="table table-hover table-bordered"
                                                ShowHeaderWhenEmpty="true" ShowFooter="true" AutoGenerateColumns="False" DataKeyNames="Item_id">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo. / क्र." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                            <asp:Label ID="lblItemid" Visible="false" runat="server" Text='<%# Eval("Item_id") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name / वस्तु नाम">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ItemName" runat="server" Text='<%# Eval("ItemName") %>' />
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                            <b> Total</b>
                                                         </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity / मात्रा">
                                                        <ItemTemplate>
                                                            <span class="pull-right">
                                                                <asp:RegularExpressionValidator ID="rev1" runat="server" ControlToValidate="gv_txtQty" ValidationGroup="b" ErrorMessage="Enter Valid Number In Quantity Field" ValidationExpression="^[0-9]*$" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Number In Quantity Field !'></i>" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                                                            </span>
                                                            <asp:TextBox ID="gv_txtQty" onfocusout="FetchData(this)" onpaste="return false;" Text='<%# ddlItemCategory.SelectedValue=="3" ? "0" : null %>' onkeypress="return validateNum(event);" runat="server" autocomplete="off" CssClass="form-control" MaxLength="8" placeholder="Enter Qty"></asp:TextBox>
                                                        </ItemTemplate>
                                                           <FooterTemplate>
                                                            <asp:Label ID="lblTotalQty" Font-Bold="true" runat="server"></asp:Label>
                                                         </FooterTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Crate / क्रैट">
                                                        <ItemTemplate>
                                                            <span class="pull-right">
                                                                <asp:RegularExpressionValidator ID="revcrate" runat="server" ControlToValidate="gv_crateQty" ValidationGroup="b" ErrorMessage="Enter Valid Number In Quantity Field" ValidationExpression="^[0-9]*$" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Number In Quantity Field !'></i>" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                                                            </span>
                                                            <asp:TextBox ID="gv_crateQty" onpaste="return false;" Enabled="false" runat="server" CssClass="form-control" MaxLength="8" placeholder="Enter Qty"></asp:TextBox>
                                                            <asp:HiddenField ID="hfcratesize" runat="server" Value='<%# Eval("FiItemQtyByCarriageMode") %>' />
                                                             <asp:HiddenField ID="hfcratenotissue" runat="server" Value='<%# Eval("FiNotIssueQty") %>' />
                                                           
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                            <asp:Label ID="lblTotalCrate" Font-Bold="true" runat="server"></asp:Label>
                                                         </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Advanced Card / एडवांस कार्ड">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAdvanceCard" runat="server" Text='<%# Eval("AdvanceCard") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                   
                                </div>
                                      <div class="col-md-4" id="pnlSubmit" runat="server" visible="false">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="b"  TabIndex="7" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                                    </div>
                                </div>
                               
                                     </fieldset>
                            </div>
                            



                        </div>

                    </div>
                </div>
                <div class="col-md-6" id="pnlsearchdata" runat="server" visible="false">
                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">View Order Status</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblSearchMsg" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <fieldset>
                                    <legend>View Order Status</legend>
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView3" runat="server" class="datatable table table-bordered" AllowPaging="false"
                                            AutoGenerateColumns="False" EmptyDataText="No Record Found." OnRowDataBound="GridView3_RowDataBound" OnRowCommand="GridView3_RowCommand" EnableModelValidation="True" DataKeyNames="tmp_MilkOrProductDemandId">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Order Id" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkViewDetails" CssClass="btn btn-xs btn-outline" Visible='<%# Eval("DStatus").ToString() == "Yes" ? true : false %>' CommandName="ItemOrdered" CommandArgument='<%#Eval("tmp_MilkOrProductDemandId") %>' runat="server" Text='<%# Eval("tmp_OrderId")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Order Type" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPDMStatus" Text='<%#Eval("PDMStatus") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Retailer Name ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBoothName" Text='<%# Eval("BoothName")%>' runat="server" />
                                                        <asp:Label ID="lblDemandDate" Visible="false" Text='<%# Eval("tmp_Demand_Date")%>' runat="server" />
                                                        <asp:Label ID="lblShiftName" Visible="false" Text='<%# Eval("tmp_ShiftName")%>' runat="server" />
                                                          <asp:Label ID="lblItemCatid" Visible="false" Text='<%# Eval("tmp_ItemCat_id")%>' runat="server" />
                                                         <asp:Label ID="lblOrderId" Visible="false" Text='<%# Eval("tmp_OrderId")%>' runat="server" />
                                                        <asp:Label ID="lblDemandStatus" Visible="false" Text='<%# Eval("tmp_Demand_Status")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Vehicle No" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVehicleNo" Text='<%# Eval("VehicleNo")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Demand Status" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDStatus" Text='<%# Eval("DStatus")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </fieldset>
                            </div>

                        </div>
                    </div>

                    <div class="modal" id="ItemDetailsModal">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">×</span></button>
                                    <h4 class="modal-title">Item Details for <span id="modalBoothName" style="color: red" runat="server"></span>&nbsp;&nbsp;Date :<span id="modaldate" style="color: red" runat="server"></span>&nbsp;&nbsp;Item Shift : <span id="modelShift" style="color: red" runat="server"></span><br />OrderId :<span id="modalorderid" style="color: red" runat="server"></span>
                                        &nbsp;&nbsp;Order Status : <span id="modalorderstatus" runat="server"></span>
                                         &nbsp;&nbsp;Vehicle No : <span id="modalvehicle" style="color: red" runat="server"></span>
                                    </h4>
                                </div>
                                <div class="modal-body">
                                    <div id="divitem" runat="server">
                                        <asp:Label ID="lblModalMsg" runat="server" Text=""></asp:Label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div style="height: 250px; overflow: scroll;">
                                                    <div class="col-md-12">
                                                        <div class="table-responsive">
                                                            <asp:GridView ID="GridView4" runat="server" OnRowCommand="GridView4_RowCommand" OnRowDataBound="GridView4_RowDataBound" EmptyDataText="No Record Found" class="table table-striped table-bordered" AllowPaging="false"
                                                                AutoGenerateColumns="False" EnableModelValidation="True" DataKeyNames="MilkOrProductDemandChildId">
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
                                                                             <asp:Label ID="lblDemandStatus" Visible="false" Text='<%# Eval("Demand_Status")%>' runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Item Name / वस्तु नाम">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblIName" Text='<%# Eval("ItemName")%>' runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Qty./ मात्रा">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblItemQty" Text='<%# Eval("ItemQty")%>' runat="server" />
                                                                            <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="c"
                                                                                ErrorMessage="Enter Item Qty." Enabled="false" Text="<i class='fa fa-exclamation-circle' title='Enter Item Qty. !'></i>"
                                                                                ControlToValidate="txtItemQty" ForeColor="Red" Display="Dynamic" runat="server">
                                                                            </asp:RequiredFieldValidator>
                                                                            <asp:RegularExpressionValidator ID="rev1" Enabled="false" runat="server" Display="Dynamic" ValidationGroup="c"
                                                                                ErrorMessage="Enter Valid Number In Quantity Field!" ForeColor="Red" 
                                                                                Text="<i class='fa fa-exclamation-circle' title='Enter Valid Number In Quantity Field !'></i>"
                                                                                 ControlToValidate="txtItemQty"
                                                                                ValidationExpression="^[0-9]*$">
                                                                            </asp:RegularExpressionValidator>
                                                                            </span>
                                                                              <asp:TextBox runat="server" autocomplete="off" Visible="false" CausesValidation="true" Text='<%# Eval("ItemQty")%>' CssClass="form-control" ID="txtItemQty" MaxLength="8" onpaste="return false;"  onkeypress="return validateNum(event);" placeholder="Enter Item Qty." ClientIDMode="Static"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Advance Card / एडवांस कार्ड ">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAdvCard" Text='<%# Eval("AdvCard")%>' runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Total Qty./ कुल मात्रा">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTotalQty" Text='<%# Eval("TotalQty")%>' runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Action / कार्यवाही करें">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkEdit" CommandName="RecordEdit" CommandArgument='<%#Eval("MilkOrProductDemandChildId") %>' runat="server" ToolTip="Edit"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                                            &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="lnkUpdate" Visible="false" ValidationGroup="c" CausesValidation="true" CommandName="RecordUpdate" CommandArgument='<%#Eval("MilkOrProductDemandChildId") %>' runat="server" ToolTip="Update" Style="color: darkgreen;" OnClientClick="return confirm('Are you sure to Update?')"><i class="fa fa-check"></i></asp:LinkButton>
                                                                            &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="lnkReset" Visible="false" CommandName="RecordReset" CommandArgument='<%#Eval("MilkOrProductDemandChildId") %>' runat="server" ToolTip="Reset" Style="color: gray;"><i class="fa fa-times"></i></asp:LinkButton>
                                                                            &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" Visible="false" CommandArgument='<%#Eval("MilkOrProductDemandChildId") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">CLOSE </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" id="pnlmsg" runat="server" visible="false">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend>View ordered item details </legend>
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView2" runat="server" class="table table-striped table-bordered" AllowPaging="false"
                                                AutoGenerateColumns="False" EnableModelValidation="True">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo./ क्र." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name / वस्तु नाम">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIName" Text='<%# Eval("ItemName")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty./ मात्रा">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemQty" Text='<%# Eval("ItemQty")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Advance Card / एडवांस कार्ड ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAdvCard" Text='<%# Eval("AdvCard")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Qty./ कुल मात्रा">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalItemQty" Text='<%# Eval("TotalItemQty")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status / स्थिती">
                                                        <ItemTemplate>
                                                            <asp:Image ID="imgProduct" runat="server" Height="25px" Width="25px" ImageUrl='<%# Convert.ToInt32(Eval("Status")) == 1 ? "~/mis/image/check.png" : "~/mis/image/cross.png" %>' />
                                                            <%--   <asp:Label ID="lblFStatus" Text='<%# Eval("Status").ToString() == "1" ?   : "" %>' runat="server" />--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remark / टिप्पणी ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMsg" Text='<%# Eval("Msg")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </fieldset>
                                </div>


                            </div>
                            <div class="row">
                               <div class="col-md-4" id="pnlClear" runat="server" visible="false">
                                    <div class="form-group">
                                        <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Reset" CssClass="btn btn-primary" />
                                    </div>
                                </div>
                            </div>

            <%--<div class="modal" id="ItemDetailsModalPreDmand" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">×</span></button>
                                    <h4 class="modal-title">Retailer Details for  Date :<span id="modalpreviousDate" style="color: red" runat="server"></span>&nbsp;&nbsp;Item Shift : <span id="modalpreviousShift" style="color: red" runat="server"></span>
                                        &nbsp;&nbsp;Category : <span id="modalPreviousCategory" style="color: red" runat="server"></span><br />
                                    </h4>
                                </div>
                                <div class="modal-body">
                                    <div id="div1" runat="server">
                                        <asp:Label ID="lbldModalMsgPreDemand" runat="server" Text=""></asp:Label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div style="height: 450px; overflow: scroll;">
                                                    <div class="col-md-12">
                                                        <div class="table-responsive">
                                                             <asp:GridView ID="GridViewPreviousDemand" OnRowDataBound="GridViewPreviousDemand_RowDataBound" CssClass="table table-striped table-bordered table-hover"
                                                                  HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
                                                    runat="server" AutoGenerateColumns="false" >
                                                    <Columns>

                                                     <asp:TemplateField HeaderText="" ItemStyle-Width="10" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                                               <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="e" ErrorMessage="Please select at least one record."
                                                                    ClientValidationFunction="Validate" Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Please select at least one record. !'></i>" ForeColor="Red"></asp:CustomValidator>
                                                        </HeaderTemplate>
                                                    </asp:TemplateField>
                                                      
                                                      <asp:TemplateField HeaderText="Retailer Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBandOName" Text='<%#Eval("BandOName") %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblBoothId" Visible="false" Text='<%#Eval("BoothId") %>' runat="server"></asp:Label>
                                                               
                                                                <asp:Label ID="lblRetailerTypeID" Visible="false" Text='<%#Eval("RetailerTypeID") %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblRouteId" Visible="false" Text='<%#Eval("RouteId") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                     </asp:TemplateField>
                                                       
                                                    </Columns>
                                                </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnsave" CssClass="btn btn-primary" ValidationGroup="e" runat="server" Text="Save" OnClick="btnsave_Click" />
                                </div>
                            </div>
                        </div>
                    </div>--%>
    </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
   <link href="../Finance/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="../Finance/js/jquery.dataTables.min.js"></script>
    <script src="../Finance/js/dataTables.bootstrap.min.js"></script>
    <script src="../Finance/js/dataTables.buttons.min.js"></script>
    <script src="../Finance/js/buttons.flash.min.js"></script>
    <script src="../Finance/js/jszip.min.js"></script>
    <script src="../Finance/js/pdfmake.min.js"></script>
    <script src="../Finance/js/vfs_fonts.js"></script>
    <script src="../Finance/js/buttons.html5.min.js"></script>
    <script src="../Finance/js/buttons.print.min.js"></script>
    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
        $('.datatable').DataTable({
            paging: false,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    exportOptions: {
                        columns: [0, 1, 2]
                    },
                    footer: false,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    filename: 'Demand Status',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',

                    exportOptions: {
                        columns: [0, 1, 2]
                    },
                    footer: false
                }],
                dom: {
                    container: {
                        className: 'dt-buttons'
                    },
                    button: {
                        className: 'btn btn-default'
                    }
                }
            }
        });
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('b');
            }

            if (Page_IsValid) {


                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }

        function myItemDetailsModal() {
            $("#ItemDetailsModal").modal('show');

        }
        function myRetailerListModal() {
            $("#ItemDetailsModalPreDmand").modal('show');

        }
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
        <%--function Validate(sender, args) {
            var gridView = document.getElementById("<%=GridViewPreviousDemand.ClientID %>");
            var checkBoxes = gridView.getElementsByTagName("input");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }--%>
        function FetchData(button) {
            debugger;
            var row = button.parentNode.parentNode;
            var Qty = GetChildControl(row, "gv_txtQty").value;
            var hfpercrateqty = GetChildControl(row, "hfcratesize").value;
            var hfcratenotissue = GetChildControl(row, "hfcratenotissue").value;
            var crateqty = GetChildControl(row, "gv_crateQty").value;

            if (Qty == '') {
                Qty = 0;

            }
            if (hfpercrateqty == '') {
                hfpercrateqty = 0;

            }
            if (hfcratenotissue == '') {
                hfcratenotissue = 0;

            }


            var Actualcrate = '0', remenderCrate = '0', FinalCrate = '0', Extrapacket = '0';

            if (hfpercrateqty != '0' && hfcratenotissue != '0') {


                Actualcrate = parseInt(Qty) / parseInt(hfpercrateqty);
                remenderCrate = parseInt(Qty) % parseInt(hfpercrateqty);

                if (parseInt(remenderCrate) <= parseInt(hfcratenotissue)) {
                    FinalCrate = Actualcrate;
                    GetChildControl(row, "gv_crateQty").value = parseInt(FinalCrate);

                }
                else {
                    FinalCrate = parseInt(Actualcrate) + 1;

                    GetChildControl(row, "gv_crateQty").value = parseInt(FinalCrate);

                }

            }
            else {
                GetChildControl(row, "gv_crateQty").value = '0';
            }
            // start total qty  in footer
            var Qtytotal = 0;
            $($("[id*=GridView1] [id*=gv_txtQty]")).each(function () {
                if (!isNaN(parseInt($(this).val()))) {
                    Qtytotal += parseInt($(this).val());
                }
            });
            $("[id*=GridView1] [id*=lblTotalQty]").html(Qtytotal);
            // end of total qty in footer


            // start crate total in footer
            var total = 0;
            $($("[id*=GridView1] [id*=gv_crateQty]")).each(function () {
                if (!isNaN(parseInt($(this).val()))) {
                    total += parseInt($(this).val());
                }
            });
            $("[id*=GridView1] [id*=lblTotalCrate]").html(total);
            // end of crate total in footer

            return false;
        };

        function GetChildControl(element, id) {
            var child_elements = element.getElementsByTagName("*");
            for (var i = 0; i < child_elements.length; i++) {
                if (child_elements[i].id.indexOf(id) != -1) {
                    return child_elements[i];
                }
            }
        };
    </script>
</asp:Content>
