<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MilkOrProductOrderByBooth.aspx.cs" Inherits="mis_MilkOrProductOrderByBooth" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .columngreen {
            background-color: #aee6a3 !important;
        }

        .columnred {
            background-color: #F5BB3E !important;
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

    <%--Confirmation Modal Start --%>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 340px;">
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
                    <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSubmit_Click" Style="margin-top: 20px; width: 50px;" />
                    <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

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
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <!-- SELECT2 EXAMPLE -->
                <div class="col-md-6">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Place/View Order / आर्डर दें/देखें</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <fieldset>
                                <legend>Date, Shift, Category / दिनांक, शिफ्ट, वस्तु वर्ग
                                </legend>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Date / दिनांक<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter Date" ForeColor="Red" Text="*"
                                                    ControlToValidate="txtOrderDate" Display="None" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="b"
                                                    ErrorMessage="Enter Date" ForeColor="Red" Text="*"
                                                    ControlToValidate="txtOrderDate" Display="None" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="None" ControlToValidate="txtOrderDate"
                                                    ErrorMessage="Invalid Date" Text="*" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="b" runat="server" Display="None" ControlToValidate="txtOrderDate"
                                                    ErrorMessage="Invalid Date" Text="*" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <%--<asp:TextBox runat="server" autocomplete="off" OnTextChanged="txtOrderDate_TextChanged" AutoPostBack="true" CssClass="form-control" ID="txtOrderDate" MaxLength="10" placeholder="Enter Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" data-date-start-date="-5d" ClientIDMode="Static"></asp:TextBox>--%>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOrderDate" MaxLength="10" placeholder="Enter Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" data-date-start-date="-d" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Shift / शिफ्ट<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Shift" Text="*"
                                                    ControlToValidate="ddlShift" ForeColor="Red" Display="None" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="b"
                                                    InitialValue="0" ErrorMessage="Select Shift" Text="*"
                                                    ControlToValidate="ddlShift" ForeColor="Red" Display="None" runat="server">
                                                </asp:RequiredFieldValidator>

                                            </span>
                                            <%--<asp:DropDownList ID="ddlShift" OnInit="ddlShift_Init" AutoPostBack="true" OnSelectedIndexChanged="ddlShift_SelectedIndexChanged" runat="server" CssClass="form-control select2"></asp:DropDownList>--%>
                                            <asp:DropDownList ID="ddlShift" OnInit="ddlShift_Init" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Item Category /वस्तु वर्ग<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Category" Text="*"
                                                    ControlToValidate="ddlItemCategory" ForeColor="Red" Display="None" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="b"
                                                    InitialValue="0" ErrorMessage="Select Category" Text="*"
                                                    ControlToValidate="ddlItemCategory" ForeColor="Red" Display="None" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="d"
                                                    InitialValue="0" ErrorMessage="Select Category" Text="*"
                                                    ControlToValidate="ddlItemCategory" ForeColor="Red" Display="None" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <%--<asp:DropDownList ID="ddlItemCategory" AutoPostBack="true" OnInit="ddlItemCategory_Init" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged" runat="server" CssClass="form-control select2"></asp:DropDownList>--%>
                                            <asp:DropDownList ID="ddlItemCategory" OnInit="ddlItemCategory_Init" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>



                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:LinkButton ID="lnkSearch" UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Plese Wait....'" runat="server" ValidationGroup="a" OnClick="lnkSearch_Click" CssClass="btn btn-block btn-secondary" ClientIDMode="Static" AccessKey="V"><i class="fa fa-eye"></i> View Demand</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:LinkButton ID="lnkAddDemand" ValidationGroup="a" runat="server" OnClick="lnkAddDemand_Click" CssClass="btn btn-block btn-primary" ClientIDMode="Static" AccessKey="V"><i class="fa fa-plus"></i> Add Demand</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                 <div class="row" id="pnlmilkOrProducttimeline" runat="server" visible="false">
                                          <div class="col-md-12">
                                               <div class="form-group">
                                        <span style="color:red">Note : Milk Morning Demand (12:00 pm to 04:30 pm).<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Milk Evening Demand (08:00 am to 11:30 am).<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Product Morning Demand (07:00 am to 10:00 am).<br /></span>
                                                   </div>
                                              </div>
                                    </div>
                                <%--  <div class="row" id="pnlAddDemand" runat="server" visible="false">
                                   
                                   
                                </div>--%>
                            </fieldset>
                            <div class="row" id="pnlProduct" runat="server" visible="false">
                                <fieldset>
                                    <legend>
                                        <asp:Label ID="lblCartMsg" Text="" runat="server"></asp:Label>
                                    </legend>
                                    
                                    <div class="col-md-4 pull-right">
                                            <asp:LinkButton ID="lnkPreviousOrder" ValidationGroup="d" OnClick="lnkPreviousOrder_Click" CssClass="btn btn-block btn-primary" runat="server" Text="Previous Demand"></asp:LinkButton>
                                       
                                    </div>
                                    <div class="col-md-12">


                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" runat="server" class="table table-hover table-bordered pagination-ys"
                                                ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" DataKeyNames="Item_id">
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
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity / मात्रा">
                                                        <ItemTemplate>
                                                            <span class="pull-right">
                                                                <asp:RegularExpressionValidator ID="rev1" runat="server" ControlToValidate="gv_txtQty" ValidationGroup="b" ErrorMessage="Enter Valid Number In Quantity Field !" ValidationExpression="^[0-9]*$" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Number In Quantity Field!'></i>" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                                                            </span>
                                                             <asp:TextBox ID="gv_txtQty" onpaste="return false;" onfocusout="FetchData(this)" Text='<%# ddlItemCategory.SelectedValue=="3" ? "0" : null %>'  onkeypress="return validateNum(event);" runat="server" autocomplete="off" CssClass="form-control" MaxLength="8" placeholder="Enter Qty"></asp:TextBox>
                                                        </ItemTemplate>
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
                                       
                                </fieldset>
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
                        <div class="col-md-2" id="pnlSubmit" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="b" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                            </div>
                        </div>
                        <div class="col-md-2" id="pnlClear" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Reset" CssClass="btn btn-block btn-primary" />
                            </div>
                        </div>
                    </div>

                </div>


                <div class="col-md-6" id="pnlsearchdata" runat="server" visible="false">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">View Placed Order</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblSearchMsg" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <fieldset>
                                    <legend>View Placed Order
                                    </legend>
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView3" runat="server" class="table table-striped table-bordered" AllowPaging="false"
                                            AutoGenerateColumns="False" EmptyDataText="No Record Found." OnRowCommand="GridView3_RowCommand" EnableModelValidation="True" DataKeyNames="MilkOrProductDemandId">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo./ क्र." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order Id">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkViewDetails" CssClass="btn btn-block btn-secondary" Text='<%# Eval("OrderId")%>' CommandName="ItemOrdered" CommandArgument='<%#Eval("MilkOrProductDemandId") %>' runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDemandDate" Text='<%# Eval("Demand_Date")%>' runat="server" />
                                                        <asp:Label ID="lblDeliveryDate" Visible="false" Text='<%# Eval("Delivary_Date")%>' runat="server" />
                                                        <asp:Label ID="lblDemandStatus" Visible="false" Text='<%# Eval("DStatus")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order Shift">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblShiftName" Text='<%# Eval("ShiftName")%>' runat="server" />
                                                        <asp:Label ID="lblDeliveryShift" Visible="false" Text='<%# Eval("DelivaryShift")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--  <asp:TemplateField HeaderText="Delivery Date">
                                                    <ItemTemplate>
                                                       
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delivery Shift">
                                                    <ItemTemplate>
                                                       
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                     
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Category">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemCatid" Visible="false" Text='<%# Eval("ItemCat_id")%>' runat="server" />
                                                        <asp:Label ID="lblItemCatName" Text='<%# Eval("ItemCatName")%>' runat="server" />
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
                                    <h4 class="modal-title">Order Date :<span id="modaldate" style="color: red" runat="server"></span>&nbsp;&nbsp;Order Shift : <span id="modelShift" style="color: red" runat="server"></span>&nbsp;&nbsp;Item Category : <span id="modelcategory" style="color: red" runat="server"></span>
                                        &nbsp;&nbsp;Status : <span id="modalstatus" style="color: red" runat="server"></span>&nbsp;&nbsp;Delivery Date : <span id="modalDevliveryDate" style="color: red" runat="server"></span>&nbsp;&nbsp;Delivery Shift : <span id="modalDelivaryShift" style="color: red" runat="server"></span>
                                    </h4>
                                </div>
                                <div class="modal-body">
                                    <div id="divitem" runat="server">
                                        <asp:Label ID="lblModalMsg" runat="server" Text=""></asp:Label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <fieldset>
                                                    <legend>Item Details</legend>
                                                    <div class="row" style="height: 250px; overflow: scroll;">
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
                                                                                    ErrorMessage="Enter Valid Number In Quantity Field !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Number In Quantity Field !'></i>" ControlToValidate="txtItemQty"
                                                                                    ValidationExpression="^[0-9]*$">
                                                                                </asp:RegularExpressionValidator>
                                                                                </span>
                                                                               <asp:TextBox runat="server" autocomplete="off" Visible="false" CausesValidation="true" Text='<%# Eval("ItemQty")%>' CssClass="form-control" ID="txtItemQty" MaxLength="8" onpaste="return false;" onkeypress="return validateNum(event);" placeholder="Enter Item Qty." ClientIDMode="Static"></asp:TextBox>
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
                                                </fieldset>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">CLOSE </button>
                                </div>
                            </div>
                            <!-- /.modal-content -->
                        </div>
                        <!-- /.modal-dialog -->
                    </div>
                </div>

            </div>
    </section>
        <!-- /.content -->

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
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
    </script>
</asp:Content>

