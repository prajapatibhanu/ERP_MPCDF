<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RetailerAdvancedCard.aspx.cs" Inherits="mis_Demand_RetailerAdvancedCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
     <link href="../Finance/css/jquery.dataTables.min.css" rel="stylesheet" />
    <style>
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
    <div class="modal fade" id="myModalMaping" tabindex="-1" role="dialog" aria-labelledby="myModalLabelMapping" aria-hidden="true">
         <div style="display: table; height: 100%; width: 100%;">
           <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
             <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                 <div class="modal-header" style="background-color: #d9d9d9;">
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                    </button>
                    <h4 class="modal-title" id="myModalLabelMapping">Confirmation</h4>
                </div>
                <div class="clearfix"></div>
                <div class="modal-body">
                    <p>
                        <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp; 
                            <asp:Label ID="lblPopupAlertMapping" runat="server"></asp:Label>
                    </p>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnMYes" OnClick="btnMSubmit_Click" Style="margin-top: 20px; width: 50px;" />
                    <asp:Button ID="btnMNo" ValidationGroup="noo" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                </div>
                <div class="clearfix"></div>
            </div>
        </div>
      </div>
    </div>
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="c" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="d" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                
              
                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">Retailer Advanced Card /  विक्रेता एडवांस्ड कार्ड </h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>
                            </div>
                            <fieldset>
                                <legend>Retailer Advanced Card /  विक्रेता एडवांस्ड कार्ड</legend>
                                 <div class="row">
                                          <div class="col-md-12">
                                               <div class="form-group">
                                        <span style="color:red">Note: Retailer Item Mapping allowed between 10th to 15th of Every Month</span>
                                                   </div>
                                              </div>
                                    </div>
                                <div class="row">
                                      <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Effective From Date / दिनांक से प्रभावी<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="b"
                                                        ErrorMessage="Enter Effective From Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Effective From Date !'></i>"
                                                        ControlToValidate="txtEffectiveFromDate" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>

                                                    <asp:RegularExpressionValidator ID="revdate" ValidationGroup="b" runat="server" Display="Dynamic" ControlToValidate="txtEffectiveFromDate"
                                                        ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                        ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>

                                                </span>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtEffectiveFromDate" OnTextChanged="txtEffectiveFromDate_TextChanged" AutoPostBack="false" MaxLength="10" placeholder="Enter Effective From Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Effective To Date / दिनांक तक प्रभावी<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="b"
                                                        ErrorMessage="Enter Effective To Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Effective To Date !'></i>"
                                                        ControlToValidate="txtEffectiveToDate" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="b" runat="server" Display="Dynamic" ControlToValidate="txtEffectiveToDate"
                                                        ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                        ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>

                                                </span>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtEffectiveToDate" OnTextChanged="txtEffectiveToDate_TextChanged" AutoPostBack="false" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" onpaste="return false;" onkeypress="return false;" MaxLength="10" placeholder="Enter Effective From Date" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                     <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Shift / शिफ्ट <span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="b"
                                                        InitialValue="0" ErrorMessage="Select Shift" Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"
                                                        ControlToValidate="ddlShift" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator></span>
                                                <asp:DropDownList ID="ddlShift" OnInit="ddlShift_Init" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                            </div>
                                        </div>
                                     <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Item Category/वस्तु वर्ग<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="b"
                                                        InitialValue="0" ErrorMessage="Select Type" Text="<i class='fa fa-exclamation-circle' title='Select Type !'></i>"
                                                        ControlToValidate="ddlItemCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator></span>

                                                <asp:DropDownList ID="ddlItemCategory" OnInit="ddlItemCategory_Init" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                            </div>
                                        </div>
                                   <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Location / लोकेशन <span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="b"
                                                    InitialValue="0" ErrorMessage="Select Location" Text="<i class='fa fa-exclamation-circle' title='Select Location !'></i>"
                                                    ControlToValidate="ddlLocation" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Route / रूट<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="b"
                                                    InitialValue="0" ErrorMessage="Select Route" Text="<i class='fa fa-exclamation-circle' title='Select Route !'></i>"
                                                    ControlToValidate="ddlRoute" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                            </span>
                                            <asp:DropDownList ID="ddlRoute" AutoPostBack="true" OnSelectedIndexChanged="ddlRoute_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                  <%--  <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Retailer Name / विक्रेता नाम<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="b"
                                                    InitialValue="0" ErrorMessage="Select Retailer" Text="<i class='fa fa-exclamation-circle' title='Select Retailer !'></i>"
                                                    ControlToValidate="ddlRetailer" ForeColor="Red" Display="None" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlRetailer" runat="server" CssClass="form-control select2">
                                                 <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div> --%>                                      
                                       
                                      
                                    

                                       <%-- <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Item Name/वस्तु नाम<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="b"
                                                        InitialValue="0" ErrorMessage="Select Item" Text="<i class='fa fa-exclamation-circle' title='Select Item !'></i>"
                                                        ControlToValidate="ddlItem" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator></span>
                                                <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-control select2">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>--%>
                                       <%-- <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Item Qty / वस्तु मात्रा<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="b"
                                                        ErrorMessage="Enter Item Qty." Text="<i class='fa fa-exclamation-circle' title='Enter Item Qty. !'></i>"
                                                        ControlToValidate="txtItemQty" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic" ValidationGroup="b"
                                                        ErrorMessage="Invalid Item Qty, only numeric allow. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Invalid Item Qty, only numeric allow. !'></i>" ControlToValidate="txtItemQty"
                                                        ValidationExpression="^[0-9]+$">
                                                    </asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtItemQty" MaxLength="5" onkeypress="return validateNum(event);" placeholder="Enter Item Qty." ClientIDMode="Static"></asp:TextBox>

                                            </div>
                                        </div>--%>
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover"
                                                EmptyDataText="No Record Found" OnRowDataBound="GridView1_RowDataBound">
                                                  <Columns>

                                                     <asp:TemplateField HeaderText="" ItemStyle-Width="10" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                                               <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="b" ErrorMessage="Please select at least one record."
                                                                    ClientValidationFunction="Validate" Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Please select at least one record. !'></i>" ForeColor="Red"></asp:CustomValidator>
                                                        </HeaderTemplate>
                                                    </asp:TemplateField>
                                                      
                                                      <asp:TemplateField HeaderText="Retailer Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBandOName" Text='<%#Eval("BandOName") %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblBoothId" Visible="false" Text='<%#Eval("BoothId") %>' runat="server"></asp:Label>
                                                               
                                                                <asp:Label ID="lblRetailerTypeID" Visible="false" Text='<%#Eval("RetailerTypeID") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                     </asp:TemplateField>
                                                       
                                                    </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="b" ID="btnMSubmit" Text="Save" OnClientClick="return MappingValidatePage();" AccessKey="V" />

                                                <asp:Button ID="btnMClear" runat="server" OnClick="btnMClear_Click" Text="Clear" CssClass="btn btn-default" />
                                            </div>
                                        </div>
                                </div>
                            </fieldset>
                            <div class="row" id="pnldata">

                           
                            <fieldset>
                                <legend>Retailer Item Mapping Details</legend>
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Month<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="d"
                                                    ErrorMessage="Enter Month" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Month !'></i>"
                                                    ControlToValidate="txtMonth" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtMonth" MaxLength="10" placeholder="Enter Month" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Item Category<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="d"
                                                        InitialValue="0" ErrorMessage="Select Category" Text="<i class='fa fa-exclamation-circle' title='Select Category !'></i>"
                                                        ControlToValidate="ddlSearchItemCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator></span>

                                                <asp:DropDownList ID="ddlSearchItemCategory" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                            </div>
                                        </div>
                                      <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Location<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="d"
                                                    InitialValue="0" ErrorMessage="Select Location" Text="<i class='fa fa-exclamation-circle' title='Select Location !'></i>"
                                                    ControlToValidate="ddlSearchLocation" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlSearchLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchLocation_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                      <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Route</label>
                                            <asp:DropDownList ID="ddlSearchRoute" runat="server" CssClass="form-control select2">
                                                 <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2" style="margin-top:20px;">
                                            <div class="form-group">
                                                <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" ValidationGroup="d" ID="btnSearch" Text="Search" />

                                            </div>
                                        </div>
                                </div>
                                <div class="row">
                                          
                                       <%--<div class="col-md-12" style="font-style:normal;color:red">Note-: Item Quantity can only be Updated </div>
                                    <br />--%>
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                EmptyDataText="No Record Found." OnRowCommand="GridView2_RowCommand" DataKeyNames="RetailerAdvanceCardId">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No /क्र.">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Location / लोकेशन">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAreaName" runat="server" Text='<%# Eval("AreaName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Route / रूट">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRName" runat="server" Text='<%# Eval("RName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Retailer Name / विक्रेता नाम">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBoothName" runat="server" Text='<%# Eval("BoothName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name/वस्तु नाम">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Qty/वस्तु मात्रा">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalItemQty" runat="server" Text='<%# Eval("Total_ItemQty") %>'></asp:Label>
                                                           <%-- <asp:RequiredFieldValidator ID="riq" ValidationGroup="c"
                                                                ErrorMessage="Enter Item Qty." Enabled="false" Text="<i class='fa fa-exclamation-circle' title='Enter Item Qty. !'></i>"
                                                                ControlToValidate="txtTotalItemQty" ForeColor="Red" Display="Dynamic" runat="server">
                                                            </asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="rfvtiq" Enabled="false" runat="server" Display="Dynamic" ValidationGroup="c"
                                                                ErrorMessage="Invalid Item Qty, Accept Number only. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Invalid Item Qty, Accept Number only. !'></i>" ControlToValidate="txtTotalItemQty"
                                                                ValidationExpression="^[0-9]+$">
                                                            </asp:RegularExpressionValidator>--%>
                                                            </span>
                                                        <asp:TextBox runat="server" autocomplete="off" Visible="false" Text='<%# Eval("Total_ItemQty")%>' CssClass="form-control" ID="txtTotalItemQty" MaxLength="5" onkeypress="return validateNum(event);" placeholder="Enter Item Qty." ClientIDMode="Static"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shift/शिफ्ट">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblShiftName" runat="server" Text='<%# Eval("ShiftName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Effective FromDate/दिनांक से प्रभावी ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEffectiveFromDate" runat="server" Text='<%# Eval("EffectiveFromDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Effective ToDate/दिनांक तक प्रभावी">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEffectiveToDate" runat="server" Text='<%# Eval("EffectiveToDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action/कार्यवाही करें">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" CommandName="RecordEdit" CommandArgument='<%#Eval("RetailerAdvanceCardId") %>' CausesValidation="false" runat="server" ToolTip="Edit"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                            &nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkUpdate" Visible="false" ValidationGroup="c" CausesValidation="true" CommandName="RecordUpdate" CommandArgument='<%#Eval("RetailerAdvanceCardId") %>' runat="server" ToolTip="Update" Style="color: darkgreen;" OnClientClick="return confirm('Are you sure to Update?')"><i class="fa fa-check"></i></asp:LinkButton>
                                                            &nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkReset" Visible="false" CommandName="RecordReset" CommandArgument='<%#Eval("RetailerAdvanceCardId") %>' runat="server" ToolTip="Reset" Style="color: gray;"><i class="fa fa-times"></i></asp:LinkButton>
                                                            &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkMappingDelete" CommandArgument='<%#Eval("RetailerAdvanceCardId") %>' CommandName="MappingRecordDel" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
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
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
  
     <script src="../Finance/js/jquery.dataTables.min.js"></script>
    <script src="../Finance/js/dataTables.bootstrap.min.js"></script>
    <script src="../Finance/js/dataTables.buttons.min.js"></script>
    <script src="../Finance/js/buttons.flash.min.js"></script>
    <script src="../Finance/js/jszip.min.js"></script>
    <script src="../Finance/js/pdfmake.min.js"></script>
    <script src="../Finance/js/vfs_fonts.js"></script>
    <script src="../Finance/js/buttons.html5.min.js"></script>
    <script src="../Finance/js/buttons.print.min.js"></script>
   <script src="js/buttons.colVis.min.js"></script>
 
    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
        $(document).ready(function () {
            $('.loader').fadeOut();
        });
        function MappingValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('b');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnMSubmit.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlertMapping.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModalMaping').modal('show');
                    return false;
                }
            }
        }
        $("#txtMonth").datepicker({
            format: "mm/yyyy",
            viewMode: "months",
            minViewMode: "months",
            autoclose: true,
        });
        //$("#txtEffectiveFromDate").datepicker({
        //    autoclose: true
        //});
        //$("#txtEffectiveToDate").datepicker({
        //    autoclose: true
        //});
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
            var gridView = document.getElementById("<%=GridView1.ClientID %>");
            var checkBoxes = gridView.getElementsByTagName("input");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
        $('.datatable').DataTable({
            paging: true,
            lengthMenu: [10, 25, 50, 100, 200, 500,1000],
            iDisplayLength: 200,
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
                    title: ('Advanced Card List').bold().fontsize(5).toUpperCase(),
                    text: '<i class="fa fa-print"></i> Print',
                    exportOptions: {
                        columns: [1, 2, 3, 4, 5, 6, 7, 8]
                    },
                    footer: false,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    title: ('Advanced Card List').bold().fontsize(5).toUpperCase(),
                    filename: 'AdvancedList',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',

                    exportOptions: {
                        columns: [1, 2, 3, 4, 5, 6, 7, 8]
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
    </script>
</asp:Content>
