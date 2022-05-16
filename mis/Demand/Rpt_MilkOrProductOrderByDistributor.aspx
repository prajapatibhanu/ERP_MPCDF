<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Rpt_MilkOrProductOrderByDistributor.aspx.cs" Inherits="mis_Demand_Rpt_MilkOrProductOrderByDistributor" %>


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
        .NonPrintable {
                  display: none;
              }
          @media print {
             
              .noprint {
                display: none;
            }
              .NonPrintable {
                  display: block;
              }  
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
      <div class="loader"></div>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">View Order Report</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                 <legend>Date ,Shift ,Item Category / दिनांक ,शिफ्ट,वस्तु वर्ग
                                </legend>
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>From Date /दिनांक से<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a" ControlToValidate="txtFromDate"
                                                    ErrorMessage="Enter From Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter From Date !'></i>"
                                                    Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtFromDate"
                                                    ErrorMessage="Invalid From Date" Text="<i class='fa fa-exclamation-circle' title='Invalid From Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtFromDate" MaxLength="10" placeholder="Enter From Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>To Date / दिनांक तक<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator" ValidationGroup="a" ControlToValidate="txtToDate"
                                                    ErrorMessage="Enter To Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter To Date !'></i>"
                                                    Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtToDate"
                                                    ErrorMessage="Invalid To Date" Text="<i class='fa fa-exclamation-circle' title='Invalid To Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtToDate" MaxLength="10" placeholder="Enter To Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Shift / शिफ्ट</label>
                                            <%-- <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                InitialValue="0" ErrorMessage="Select Shift" Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"
                                                ControlToValidate="ddlShift" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>--%>
                                            <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Item Category /वस्तु वर्ग<span style="color: red;"> *</span></label>
                                          <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                InitialValue="0" ErrorMessage="Select Item Category" Text="<i class='fa fa-exclamation-circle' title='Select Item Category !'></i>"
                                                ControlToValidate="ddlItemCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlItemCategory" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Retailer/विक्रेता</label>
                                            <%-- <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                        InitialValue="0" ErrorMessage="Select Retailer" Text="<i class='fa fa-exclamation-circle' title='Select Retailer !'></i>"
                                        ControlToValidate="ddlRetailer" ForeColor="Red" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator></span>--%>
                                            <%--<asp:DropDownList ID="ddlRetailer" OnInit="ddlRetailer_Init" runat="server" CssClass="form-control select2"></asp:DropDownList>--%>
                                            <asp:DropDownList ID="ddlRetailer" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                      
                                    <div class="col-md-2" style="margin-top: 22px;">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="a" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" AccessKey="S" />
                                        
                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-default" />
                                        </div>
                                    </div>
                                </div>

                            </fieldset>
                            <div class="row" id="pnldata" runat="server" visible="false">
                                <div class="col-md-3 pull-left noprint">
                            <div class="form-group">
                               
                                <asp:Button ID="btnExport" runat="server" CssClass="btn btn-default" Text="Excel" OnClick="btnExport_Click" />
                                
                            </div>
                                
                            </div>
                                <div class="col-lg-12">
                                    <fieldset>
                                        <legend>View Order Report</legend>

                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" runat="server" class="table table-striped table-bordered"
                                                AutoGenerateColumns="False" EmptyDataText="No Record Found." OnRowCommand="GridView1_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo./ क्र." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Retailer Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBoothName" Text='<%# Eval("BoothName")%>' runat="server" />
                                                             <asp:Label ID="lblItemCatid" Visible="false" Text='<%# Eval("ItemCat_id")%>' runat="server" />
                                                            <asp:HiddenField ID="hfitemcagid" runat="server" Value='<%# Eval("ItemCat_id")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Order Id">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkOrderId" CommandName="ItemOrdered" CommandArgument='<%#Eval("MilkOrProductDemandId") %>' Text='<%#Eval("OrderId") %>' runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDemandDate" Text='<%# Eval("Demand_Date")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order Shift">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblShiftName" Text='<%# Eval("ShiftName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Category">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblItemCatName" Text='<%# Eval("ItemCatName")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Delivery Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeliveryDate" Text='<%# Eval("Delivery_Date")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delivery Shift">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDelivaryShift" Text='<%# Eval("DelivaryShift")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" Text='<%# Eval("DStatus")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderText="Order Id" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOrderId" Text='<%# Eval("OrderId")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Status"  Visible="false" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblDDStatus" Text='<%# Eval("DDStatus")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderText="Detail" Visible="false"  HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                                                <ItemTemplate>
                                                   <asp:Panel ID="pnlResult" runat="server"  Width="100%"><span class="HideRecord">(As Per Details)</span>
                                                   <asp:GridView ID="GridView3" runat="server" EmptyDataText="No Record Found" class="table table-striped table-bordered" AllowPaging="false"
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
                                                                                            <asp:Label ID="lblTotalQty" Text='<%# Eval("TotalQty")%>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                     
                                                </asp:Panel>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="View Details" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkViewDetails" CssClass="btn btn-xs btn-outline" CommandName="ItemOrdered" CommandArgument='<%#Eval("MilkOrProductDemandId") %>' runat="server"><i class="fa fa-eye"></i> View</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    <%--     <asp:TemplateField HeaderText="Demand Status" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDStatus" Text='<%# Eval("DStatus")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View Details" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkViewDetails" CssClass="btn btn-primary btn-primary" Visible='<%# Eval("DStatus").ToString() == "Yes" ? true : false %>' CommandName="ItemOrdered" CommandArgument='<%#Eval("tmp_MilkOrProductDemandId") %>' runat="server"><i class="fa fa-eye"></i> View Details</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>
                                            
                                        </div>
                                    </fieldset>
                                </div>
                                <div class="modal" id="ItemDetailsModal">
                                    <div class="modal-dialog modal-lg">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">×</span></button>
                                                <h4 class="modal-title">Item Details for <span id="modalBoothName" style="color: red" runat="server"></span>&nbsp;&nbsp;Order Date :<span id="modaldate" style="color: red" runat="server"></span>&nbsp;&nbsp;Order Shift : <span id="modelShift" style="color: red" runat="server"></span></h4>
                                            </div>
                                            <div class="modal-body">
                                                <div id="divitem" runat="server">
                                                    <asp:Label ID="lblModalMsg" runat="server" Text=""></asp:Label>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                                <div style="height: 250px; overflow: scroll;">
                                                                    <div class="col-md-12">
                                                                        <div class="table-responsive">
                                                                            <asp:GridView ID="GridView2" runat="server" OnRowDataBound="GridView2_RowDataBound" EmptyDataText="No Record Found" class="table table-striped table-bordered" AllowPaging="false"
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
                                                                                                ErrorMessage="Invalid Item Qty, Accept Number only. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Invalid Item Qty, Accept Number only. !'></i>" ControlToValidate="txtItemQty"
                                                                                                ValidationExpression="^[0-9]+$">
                                                                                            </asp:RegularExpressionValidator>
                                                                                            </span>
                                               <asp:TextBox runat="server" autocomplete="off" Visible="false" CausesValidation="true" Text='<%# Eval("ItemQty")%>' CssClass="form-control" ID="txtItemQty" MaxLength="5" onkeypress="return validateNum(event);" placeholder="Enter Item Qty." ClientIDMode="Static"></asp:TextBox>
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
                                        <!-- /.modal-content -->
                                    </div>
                                    <!-- /.modal-dialog -->
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </section>
        <section class="content">
            <asp:GridView ID="GridView4" runat="server" Visible="false" OnRowDataBound="GridView4_RowDataBound" class="table table-striped table-bordered"
                         AutoGenerateColumns="False" EmptyDataText="No Record Found.">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo./ क्र." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Retailer Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBoothName" Text='<%# Eval("BoothName")%>' runat="server" />
                                                             <asp:Label ID="lblItemCatid" Text='<%# Eval("ItemCat_id")%>' runat="server" />
                                                              <asp:Label ID="lblMilkOrProductDemandId" Text='<%# Eval("MilkOrProductDemandId")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Order Id">
                                                    <ItemTemplate>
                                                         <asp:Label ID="lblOrderId" Text='<%# Eval("MilkOrProductDemandId")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDemandDate" Text='<%# Eval("Demand_Date")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order Shift">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblShiftName" Text='<%# Eval("ShiftName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Category">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblItemCatName" Text='<%# Eval("ItemCatName")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Delivery Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeliveryDate" Text='<%# Eval("Delivery_Date")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delivery Shift">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDelivaryShift" Text='<%# Eval("DelivaryShift")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                   
                                                     <asp:TemplateField HeaderText="Status"  ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblDDStatus" Text='<%# Eval("DDStatus")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Detail" HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                                                <ItemTemplate>
                                                   <asp:Panel ID="pnlResult" runat="server"  Width="100%"><span class="HideRecord">(As Per Details)</span>
                                                   <asp:GridView ID="GridView3" runat="server" EmptyDataText="No Record Found" class="table table-striped table-bordered" AllowPaging="false"
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
                                                                                            <asp:Label ID="lblTotalQty" Text='<%# Eval("TotalQty")%>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                </asp:Panel>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
        </section>
        <!-- /.content -->

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
        $(document).ready(function () {
            $('.loader').fadeOut();
            $("#<%=btnSearch.ClientID%>").click((function () {

                if (Page_IsValid) {
                    $('.loader').show();
                    return true;

                }
            }));

         });
        $('.datatable').DataTable({
            paging: true,
            iDisplayLength: 100,
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
                    title: ('View Order Report').bold().fontsize(5).toUpperCase(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4,5,6,7,8]
                    },
                    footer: false,
                    autoPrint: true
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
        function myItemDetailsModal() {
            $("#ItemDetailsModal").modal('show');

        }
    </script>
</asp:Content>
