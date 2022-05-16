<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ProductDemandAtDS.aspx.cs" Inherits="mis_Demand_ProductDemandAtDS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
      <%--<link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />--%>
    <link href="../Finance/css/dataTables.bootstrap.min.css" rel="stylesheet" />
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
  
    <style>
        th.sorting, th.sorting_asc, th.sorting_desc {
            background: #0f62ac !important;
            color: white !important;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
        }

        a.btn.btn-default.buttons-excel.buttons-html5 {
            background: #ff5722c2;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-pdf.buttons-html5 {
            background: #009688c9;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-print {
            background: #e91e639e;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
        }

            a.btn.btn-default.buttons-print:hover, a.btn.btn-default.buttons-pdf.buttons-html5:hover, a.btn.btn-default.buttons-excel.buttons-html5:hover {
                box-shadow: 1px 1px 1px #808080;
            }

            a.btn.btn-default.buttons-print:active, a.btn.btn-default.buttons-pdf.buttons-html5:active, a.btn.btn-default.buttons-excel.buttons-html5:active {
                box-shadow: 1px 1px 1px #808080;
            }

        .box.box-pramod {
            border-top-color: #1ca79a;
        }

        .box {
            min-height: auto;
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
                        <h3 class="box-title">Regular Demand - Daily Place/View Order <%--/ दैनिक आर्डर दें/देखें--%> </h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <fieldset>
                            <legend>Date ,Shift ,Category <%--/ दिनांक ,शिफ्ट ,वस्तु वर्ग--%>
                            </legend>
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Date <%--/ दिनांक--%><span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                ControlToValidate="txtOrderDate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="b"
                                                ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                ControlToValidate="txtOrderDate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtOrderDate"
                                                ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="b" runat="server" Display="Dynamic" ControlToValidate="txtOrderDate"
                                                ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                        </span>
                                        <%--<asp:TextBox runat="server" autocomplete="off" OnTextChanged="txtOrderDate_TextChanged" AutoPostBack="true" CssClass="form-control" ID="txtOrderDate" MaxLength="10" placeholder="Enter Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" data-date-start-date="1d" ClientIDMode="Static"></asp:TextBox>--%>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOrderDate" MaxLength="10" placeholder="Enter Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Shift <span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                InitialValue="0" ErrorMessage="Select Shift" Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"
                                                ControlToValidate="ddlShift" ForeColor="Red" Display="None" runat="server">
                                            </asp:RequiredFieldValidator>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="b"
                                                InitialValue="0" ErrorMessage="Select Shift" Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"
                                                ControlToValidate="ddlShift" ForeColor="Red" Display="None" runat="server">
                                            </asp:RequiredFieldValidator>

                                        </span>
                                        <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                                 <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Location <%--/ लोकेशन--%> <span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Location" Text="<i class='fa fa-exclamation-circle' title='Select Location !'></i>"
                                                    ControlToValidate="ddlLocation" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlLocation" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                 <div class="col-md-4">
                                <div class="form-group">
                                    <label>Route <%--/ रूट--%> <span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlRoute" InitialValue="0" ErrorMessage="Select Route." Text="<i class='fa fa-exclamation-circle' title='Select Route Sangh !'></i>"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddlRoute" OnSelectedIndexChanged="ddlRoute_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                </div>
                            </div>
                                <div class="col-md-4">
                            <div class="form-group">
                                <label>Retailer Name <%--/ विक्रेता का नाम--%> <span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                        InitialValue="0" ErrorMessage="Select Retailer Name" Text="<i class='fa fa-exclamation-circle' title='Select Retailer !'></i>"
                                        ControlToValidate="ddlRetailer" ForeColor="Red" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator></span>
                                <asp:DropDownList ID="ddlRetailer" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlRetailer_SelectedIndexChanged">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                                  <%--<div class="col-md-4">
                                    <div class="form-group">
                                        <label>Item Category <span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Category" Text="<i class='fa fa-exclamation-circle' title='Select Category !'></i>"
                                                    ControlToValidate="ddlItemCategory" ForeColor="Red" Display="None" runat="server">
                                                </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="b"
                                                InitialValue="0" ErrorMessage="Select Category" Text="<i class='fa fa-exclamation-circle' title='Select Category !'></i>"
                                                ControlToValidate="ddlItemCategory" ForeColor="Red" Display="None" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlItemCategory" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>--%>
                            
                            </div>
                              <div class="row">
                             <div id="pnlButton" runat="server">  
                             
                                  <div class="col-md-4" style="margin-top:20px;">
                                <div class="form-group">
                                    <asp:LinkButton ID="lnkSearch" runat="server" ValidationGroup="a" OnClick="lnkSearch_Click" CssClass="btn btn-block btn-secondary" ClientIDMode="Static" AccessKey="V"><i class="fa fa-eye"></i> View Demand</asp:LinkButton>
                                </div>
                                       </div>
                                <div class="col-lg-4" style="margin-top:20px;">
                                     <div class="form-group">
                                         <asp:LinkButton ID="lnkAddDemand" runat="server" ValidationGroup="a" OnClick="lnkAddDemand_Click" CssClass="btn btn-block btn-primary" ClientIDMode="Static" AccessKey="V"><i class="fa fa-plus"></i> Add Demand</asp:LinkButton>
                                         </div>
                                </div>
                            </div>
                            
                           </div>


                            <%--<div class="row" id="pnlSearchBy" runat="server" visible="false">

                           
                             <div class="col-md-12">
                                <div class="form-group">
                                        <label></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="b"
                                                InitialValue="0" ErrorMessage="Select Shift" Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"
                                                ControlToValidate="ddlShift" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                                    <asp:RadioButtonList ID="rblReportType"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblReportType_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                        <asp:ListItem class="radio-inline" Text="Route wise&nbsp;&nbsp;" Value="1"></asp:ListItem> 
                                                        <asp:ListItem class="radio-inline" Text="Distributor wise&nbsp;&nbsp;" Value="2"></asp:ListItem>
                                        
                                                    </asp:RadioButtonList>


                                    
                           
                                   
                                    </div>
                                </div> 
                                 </div>--%>     
                            <%--<div class="row" id="pnlRoute" runat="server" visible="false">
                         
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Route No<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ForeColor="Red" Display="Dynamic" ValidationGroup="b" runat="server" ControlToValidate="ddlRoute" InitialValue="0" ErrorMessage="Select Route." Text="<i class='fa fa-exclamation-circle' title='Select Route Sangh !'></i>"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddlRoute" AutoPostBack="true" OnInit="ddlRoute_Init" OnSelectedIndexChanged="ddlRoute_SelectedIndexChanged" runat="server"  CssClass="form-control select2"></asp:DropDownList>
                                </div>
                            </div>
                                </div>--%>
                            <%--<div class="row" id="panelDist" runat="server" visible="false">
                              <div class="col-md-6" >
                                    <div class="form-group">
                                        <label>Distributor Name<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="b"
                                                InitialValue="0" ErrorMessage="Select Distributor/Super Stockist Name" Text="<i class='fa fa-exclamation-circle' title='Select Distributor/Super Stockist Name !'></i>"
                                                ControlToValidate="ddlDist" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                      <asp:DropDownList ID="ddlDist" runat="server" AutoPostBack="true"  OnInit="ddlDist_Init" OnSelectedIndexChanged="ddlDist_SelectedIndexChanged" CssClass="form-control select2"></asp:DropDownList>
                                </div>
                             </div>
                                </div>--%>




                             <div class="row" id="pnlUserType" runat="server" visible="false">
                                
                                <%--<div class="col-md-6" >
                                    <div class="form-group">

                                        <label>Retailer Type/ विक्रेता प्रकार<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="d" 
                                                InitialValue="0" ErrorMessage="Select Retailer Type" Text="<i class='fa fa-exclamation-circle' title='Select Retailer Type !'></i>"
                                                ControlToValidate="ddlUserType" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>

                                        </span>
                                        <asp:DropDownList ID="ddlUserType" AutoPostBack="true" OnInit="ddlUserType_Init" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>--%>
                                  <%--<div class="col-md-6" >
                                      <div class="form-group">
                                            <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="d" 
                                                ErrorMessage="Enter Code" Text="<i class='fa fa-exclamation-circle' title='Enter Code !'></i>"
                                                ControlToValidate="txtTypeCode" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                                  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="d" Display="Dynamic" runat="server" ControlToValidate="txtTypeCode"
                                            ErrorMessage="Invalid Code" Text="<i class='fa fa-exclamation-circle' title='Invalid Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>
                                            </span>

                                           <label><span id="UserTypeCode" runat="server">

                                                  </span> Code <span style="color: red;"> *</span></label>

                                               <asp:TextBox ID="txtTypeCode" runat="server" autocomplete="off" CssClass="form-control" MaxLength="10" placeholder="Enter Code"></asp:TextBox>
                                                 
                                          </div>


                                      </div>--%>
                                
                              <%--   <div class="col-md-4" >
                                      <div class="form-group">

                                          <asp:Button runat="server" CssClass="btn btn-block btn-primary" OnClick="btnUserType_Click" ValidationGroup="d" ID="btnUserType" Text="Search"  />
                                 
                                         </div>
                                     </div>--%>


                               
                                      </div>
                            <div class="row" id="pnladddemand" runat="server" visible="false">

                                 

<%--                                <div class="col-md-6" >
                                    <div class="form-group">

                                        <label>Retailer/विक्रेता<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="d" 
                                                InitialValue="0" ErrorMessage="Select Retailer" Text="<i class='fa fa-exclamation-circle' title='Select Retailer !'></i>"
                                                ControlToValidate="ddlRetailer" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlRetailer" AutoPostBack="true"  OnSelectedIndexChanged="ddlRetailer_SelectedIndexChanged" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>--%>


                                
                            </div>
                             </fieldset>
                            <div class="row" id="pnlProduct" runat="server" visible="false">                               
                                      <fieldset>
                            <legend><asp:Label ID="lblCartMsg" Text="" runat="server"></asp:Label>
                            </legend>
                                            <div class="col-md-4 pull-right">
                                            <asp:LinkButton ID="lnkPreviousOrder" ValidationGroup="d" OnClick="lnkPreviousOrder_Click" CssClass="btn btn-block btn-primary" runat="server" Text="Previous Demand"></asp:LinkButton>
                                       
                                    </div>
                                          <div class="col-md-12">

                                          
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" ShowFooter="true" runat="server" class="table table-hover table-bordered pagination-ys"
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
                                                      <FooterTemplate>
                                                            <b> Total</b>
                                                         </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity / मात्रा">
                                                    <ItemTemplate>
                                                        <span class="pull-right">
                                                            <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="c"
                                                              ErrorMessage="Enter Item Qty." Enabled="false" Text="<i class='fa fa-exclamation-circle' title='Enter Item Qty. !'></i>"
                                                            ControlToValidate="gv_txtQty" ForeColor="Red" Display="Dynamic" runat="server">
                                                  </asp:RequiredFieldValidator>




                                                            <asp:RegularExpressionValidator ID="rev1" runat="server"
                                                                 ControlToValidate="gv_txtQty" ValidationGroup="a" 
                                                                ErrorMessage="Enter Valid Quantity !"  ValidationExpression="^[0-9]*$" 
                                                                Text="<i class='fa fa-exclamation-circle' title='Enter Valid Quantity !'></i>" 
                                                                SetFocusOnError="true" ForeColor="Red" Display="None">

                                                            </asp:RegularExpressionValidator>
                                                        </span>
                                                        <asp:TextBox ID="gv_txtQty" onfocusout="FetchData(this)" Text=""  onkeypress="return validateNum(event);" runat="server" onfocus="ppclass" autocomplete="off" CssClass="form-control" MaxLength="8" placeholder="Enter Qty"></asp:TextBox>
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
                                                  <asp:TemplateField HeaderText="Rate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Item_Rate") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                              </div>
                                           </fieldset>
                                
                            </div>
                          <div class="row" id="pnlmsg" runat="server" visible="false">
                                   
                                <div class="col-md-12">
                                      <fieldset>
                            <legend> View ordered item details </legend>
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView2" runat="server" class="table table-striped table-bordered" AllowPaging="false"
                                            AutoGenerateColumns="False" EnableModelValidation="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo./ क्र." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
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
                                                        <asp:Label ID="Label2" Text='<%# Eval("Msg")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                           </fieldset>
                                </div>
                               
                       
                            </div>
                            <div class="row">
                                <div class="col-md-4" id="pnlSubmit" runat="server" visible="false">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="b" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                                    </div>
                                </div>
                                <div class="col-md-4" id="pnlClear" runat="server" visible="false">
                                    <div class="form-group">
                                        <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Reset" CssClass="btn btn-block btn-primary" />
                                    </div>
                                </div>
                            </div>
                       
                         
                           
                    </div>

                </div>
            </div>
            <div class="col-md-6" id="pnlsearchdata" runat="server" visible="false">
                <div class="box box-Manish">
                    <div class="box-header">
                        <h3 class="box-title">Regular Demand - View Order Status</h3>
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
                            <legend>View Order Status
                            </legend>
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView3" runat="server" class="datatable table table-striped table-bordered" AllowPaging="false"
                                            AutoGenerateColumns="False" EmptyDataText="No Record Found." OnRowDataBound="GridView3_RowDataBound" OnRowCommand="GridView3_RowCommand" EnableModelValidation="True" DataKeyNames="tmp_MilkOrProductDemandId">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                          <asp:Label ID="lblDemandDate" Visible="false" Text='<%# Eval("Demand_Date")%>' runat="server" />
                                                          <asp:Label ID="lblShiftName" Visible="false" Text='<%# Eval("tmp_ShiftName")%>' runat="server" />
                                                        <asp:Label ID="lblBoothName" Visible="false" Text='<%# Eval("BoothName")%>' runat="server" />
                                                         <asp:Label ID="lblItemCatid" Visible="false" Text='<%# Eval("tmp_ItemCat_id")%>' runat="server" />
                                                         <asp:Label ID="lbltmpDStatus" Visible="false" Text='<%# Eval("tmp_DStatus")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <%--<asp:TemplateField HeaderText="Retailer / Institution Name ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPAI" Text='<%# Eval("RetailerTypeID").ToString() == "3" ? Eval("Organization_Name") : Eval("BoothName") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Retailer / Institution Name ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPAI" Text='<%# Eval("BoothName") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <%-- <asp:TemplateField HeaderText="Organization" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrganizationName" Text='<%# Eval("Organization_Name")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                 <asp:TemplateField HeaderText="Demand Status" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDStatus" Text='<%# Eval("DStatus")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View Details" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkViewDetails" CssClass="btn btn-xs btn-outline" Text='<%# Eval("tmp_OrderId") %>' Visible='<%# Eval("DStatus").ToString() == "Yes" ? true : false %>' CommandName="ItemOrdered" CommandArgument='<%#Eval("tmp_MilkOrProductDemandId") %>' runat="server"></asp:LinkButton>
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
                            <h4 class="modal-title">
                                Item Details for <span id="modalBoothName" style="color:red" runat="server">

                                                 </span>&nbsp;&nbsp;Date :<span id="modaldate" style="color:red" runat="server">
                            </span>&nbsp;&nbsp;Item Category : <span id="modelShift" style="color:red" runat="server"></span>
                                &nbsp;&nbsp;Order Status : <span id="modelorderstatus" style="color:red" runat="server"></span>
                            </h4>
                        </div>
                        <div class="modal-body">
                            <div id="divitem" runat="server">
                                <asp:Label ID="lblModalMsg" runat="server" Text=""></asp:Label>
                                <div class="row">
                                    <div class="col-md-12">
                                        <fieldset>
                                            <legend>Item Details</legend>
                                            <div class="row" style="height:250px;overflow:scroll;">
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
                                                            ErrorMessage="Enter Valid Quantity!" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Quantity!'></i>" ControlToValidate="txtItemQty"
                                                            ValidationExpression="^[0-9]*$">
                                                        </asp:RegularExpressionValidator>
                                                        </span>
                                               <asp:TextBox runat="server" autocomplete="off" Visible="false" CausesValidation="true" Text='<%# Eval("ItemQty")%>' CssClass="form-control" ID="txtItemQty" MaxLength="8" onkeypress="return validateNum(event);" placeholder="Enter Item Qty." ClientIDMode="Static"></asp:TextBox>
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
                                                        <asp:LinkButton ID="lnkUpdate" Visible="false" ValidationGroup="c" CausesValidation="true" CommandName="RecordUpdate" CommandArgument='<%#Eval("MilkOrProductDemandChildId") %>' runat="server" ToolTip="Update" Style="color:darkgreen;" OnClientClick="return confirm('Are you sure to Update?')"><i class="fa fa-check"></i></asp:LinkButton>
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
    <asp:Label ID="lbldistributerMONO" runat="server" Visible="false"></asp:Label>
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
    <%-- <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>--%>
    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);

        $(document).ready(function () {
            $('.loader').fadeOut();
        });

        function pp() {

            $('.ppclass').val('');
        }
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

        //$("#txtOrderDate").datepicker({
        //    autoclose: true ,
        //    startDate: "1d",
        //});

        function myItemDetailsModal() {
            $("#ItemDetailsModal").modal('show');

        }
        // for product
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
