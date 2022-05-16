<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ProductDMGenerate.aspx.cs" Inherits="mis_DemandSupply_ProductDMGenerate" %>

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
    </style>
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
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
     <asp:ValidationSummary ID="v1" runat="server" ValidationGroup="ModalSave" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content noprint">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Regular DM Generate</h3>


                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>
                            </div>
                            <fieldset>
                                <legend>Regular DM Generate
                                </legend>
                                <div class="row">

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Date / दिनांक<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    ControlToValidate="txtDate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtDate"
                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" OnTextChanged="txtDate_TextChanged" AutoPostBack="true" CssClass="form-control" ID="txtDate" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                      <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Shift / शिफ्ट</label>
                                            <asp:DropDownList ID="ddlShift" runat="server" ClientIDMode="Static" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Location<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Location" Text="<i class='fa fa-exclamation-circle' title='Select Location !'></i>"
                                                    ControlToValidate="ddlLocation" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Route No<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Route" Text="<i class='fa fa-exclamation-circle' title='Select Route !'></i>"
                                                    ControlToValidate="ddlRoute" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlRoute" runat="server" OnSelectedIndexChanged="ddlRoute_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control select2">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2" id="pnldistss" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>Dist./SS Name <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Distributor/Superstockist Name" Text="<i class='fa fa-exclamation-circle' title='Select Distributor/Superstockist Name !'></i>"
                                                    ControlToValidate="ddlDitributor" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlDitributor" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class="col-md-2" id="pnlorderno" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>Order No <span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Order No" Text="<i class='fa fa-exclamation-circle' title='Select Order No !'></i>"
                                                    ControlToValidate="ddlOrderNo" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ValidationGroup="b"
                                                    InitialValue="0" ErrorMessage="Select Order No" Text="<i class='fa fa-exclamation-circle' title='Select Order No!'></i>"
                                                    ControlToValidate="ddlOrderNo" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlOrderNo" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <%--<asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />--%>
                                            <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btnSearch_Click" ID="btnSearch" Text="Search" AccessKey="S" />
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-default" />
                                             <%-- &nbsp;&nbsp;--%>
                                            <%--<asp:Button ID="btnprint" runat="server" OnClick="btnprint_Click" Text="Clear" CssClass="btn btn-default" />--%>

                                        </div>
                                    </div>

                                </div>
                                <div class="row" id="pnlvehicledetail" runat="server" visible="false">


                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Vehicle No. <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:LinkButton ID="lnkVehicle" CausesValidation="false" OnClick="lnkVehicle_Click" ToolTip="Add New Vehicle Details" runat="server"><b>[Add]</b></asp:LinkButton>
                                            <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="b"
                                                    InitialValue="0" ErrorMessage="Select Vehicle No." Text="<i class='fa fa-exclamation-circle' title='Select Vehicle No. !'></i>"
                                                    ControlToValidate="ddlVehicleNo" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>--%>

                                            </span>
                                            <asp:DropDownList ID="ddlVehicleNo" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class="col-md-2">
                                        <div class="form-group">
                                            <label>PO Number</label>
                                            <span class="pull-right">
                                            </span>
                                           <asp:TextBox ID="txtPONumber" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>PO Date</label>
                                             <span class="pull-right">
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtPODate"
                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                           <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtPODate" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="col-md-3">
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
                                     <div class="col-md-2">
                                    <div class="form-group">
                                         <label>Name</label>
                                         <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtname" MaxLength="50" placeholder="Enter Name" ClientIDMode="Static"></asp:TextBox>

                                    </div>
                                </div>
                                    <%--<div class="col-md-2">
                                            <div class="form-group">
                                                <label>In Time</label>

                                                <div class="input-group bootstrap-timepicker timepicker">
                                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control timepicker" Text="00:00 AM" ID="txtInTime" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                                    <span class="input-group-addon"><i class="fa fa-clock-o"></i></span>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Out Time</label>

                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="b"
                                                        ErrorMessage="Enter Oute Time" Text="<i class='fa fa-exclamation-circle' title='Enter Oute Time !'></i>"
                                                        ControlToValidate="txtOutTime" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <div class="input-group bootstrap-timepicker timepicker">
                                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control timepicker" ID="txtOutTime" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                                    <span class="input-group-addon"><i class="fa fa-clock-o"></i></span>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Supervisor Name </label>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtSupervisorName" MaxLength="80" placeholder="Enter Supervisor Name" ClientIDMode="Static"></asp:TextBox>

                                            </div>
                                        </div>--%>
                                    <div class="col-md-1" style="margin-top: 20px;">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="b" CausesValidation="false" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />

                                        </div>
                                    </div>
                                </div>
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
                                                            <%--   <asp:Label ID="lblGhee_itemstatus"  Visible="false" Text='<%#Eval("Ghee_itemstatus") %>' runat="server" />--%>
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
                                                            <asp:Textbox ID="txtFTotalBox" Width="60px" ReadOnly="true" autocompleted="off" MaxLength="5" onkeypress="return validateNum(event);" Font-Bold="true" runat="server"></asp:Textbox>
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
                                                            <asp:Label ID="lblFAmount" Font-Bold="true" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                </fieldset>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
                 <div class="modal" id="VehicleDetailsModal">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 340px;">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Vehicle Details</h4>
                        </div>
                        <div class="modal-body">


                            <div class="row" style="height: 200px; overflow: scroll;">

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Type<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="ModalSave"
                                                InitialValue="0" ErrorMessage="Select Type" Text="<i class='fa fa-exclamation-circle' title='Select Type !'></i>"
                                                ControlToValidate="ddlVendorType" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlVendorType" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="ddlVendorType_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Name<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="ModalSave"
                                                InitialValue="0" ErrorMessage="Select Name" Text="<i class='fa fa-exclamation-circle' title='Select Name !'></i>"
                                                ControlToValidate="ddlVendorName" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlVendorName" Width="200px" runat="server" CssClass="form-control select2">
                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Vehicle No.<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="ModalSave"
                                                ErrorMessage="Enter Vehicle No." Text="<i class='fa fa-exclamation-circle' title='Enter Vehicle No. !'></i>"
                                                ControlToValidate="txtVehicleNo" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtVehicleNo" Display="Dynamic"
                                                ValidationExpression="^[A-Z|a-z]{2}-\d{2}-[A-Z|a-z]{1,2}-\d{4}$" runat="server"
                                                Text="<i class='fa fa-exclamation-circle' title='Invalid vehicle no. format (XX-00-XX-0000)!'></i>"
                                                ErrorMessage="Invalid vehicle no. format (XX-00-XX-0000)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="ModalSave">
                                            </asp:RegularExpressionValidator>

                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" MaxLength="13" ID="txtVehicleNo" placeholder="XX-00-XX-0000" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Vehicle Type<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="ModalSave"
                                                ErrorMessage="Enter Vehicle Type" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Vehicle Type. !'></i>"
                                                ControlToValidate="txtVehicleType" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ValidationGroup="ModalSave"
                                                ErrorMessage="Invalid Vehicle Type. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Invalid Vehicle Type !'></i>" ControlToValidate="txtVehicleType"
                                                ValidationExpression="^[a-zA-Z0-9\s]+$">
                                            </asp:RegularExpressionValidator>

                                        </span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtVehicleType" MaxLength="10" placeholder="Enter Vehicle Type"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <label>IsActive</label>

                                        <asp:CheckBox ID="chkIsActive" CssClass="form-control" Checked="true" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-11">
                                    <asp:Label ID="lblModalMsg1" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="ModalSave" ID="btnSaveVehicleDetails" Text="Submit" OnClick="btnSaveVehicleDetails_Click" />

                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
        </section>
        <section class="content">
            <div id="Print" runat="server" class="NonPrintable"></div>        
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
      <asp:Label ID="lbldistributerMONO" runat="server" Visible="false"></asp:Label>
    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
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

