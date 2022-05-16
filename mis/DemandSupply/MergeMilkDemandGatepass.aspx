<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MergeMilkDemandGatepass.aspx.cs" Inherits="mis_DemandSupply_MergeMilkDemandGatepass" %>

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
            border: 1px solid #000000 !important;
           
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
           text-align:center;
           
        }      
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
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
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="c" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content noprint">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Merge Gate Pass</h3>


                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="pull-right">
                                        <asp:HyperLink runat="server" href="MilkDistributionVehicleChallan.aspx" ToolTip="Back" class="btn btn-primary"><i class="fa fa-arrow-left"> Back</i></asp:HyperLink>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>
                            </div>

                             <fieldset>
                                  <legend>Date,Shift,Location,Distributor
                                    </legend>
                            <div class="row">
                               
                                   
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Date<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a" ControlToValidate="txtDate"
                                                    ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtDate"
                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid From Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDate" MaxLength="10" placeholder="Enter Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Shift <span style="color: red;"> *</span></label>
                                             <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Shift" Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"
                                                    ControlToValidate="ddlShift" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Location <span style="color: red;"> *</span></label>
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
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Distributor </label>
                                            <asp:ListBox runat="server" ID="ddlDistributor" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                    </div>
                                    
                                    <div class="col-md-1">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" OnClick="btnSearch_Click" ID="btnSearch" Text="Search" AccessKey="S" />

                                        </div>
                                    </div>
                                    <div class="col-md-1" style="margin-top: 20px;">
                                        <div class="form-group">

                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-default" />
                                        </div>
                                    </div>
                               
                            </div>
                                <div class="row" ID="pnlvehicledetail" runat="server" visible="false">
                                     <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Vehicle No <span style="color: red;"> *</span></label>
                                                 <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                    ErrorMessage="Enter Vehicle No" Text="<i class='fa fa-exclamation-circle' title='Enter Vehicle No !'></i>"
                                                    ControlToValidate="txtVehicleNo" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                            </span>
                                                <asp:TextBox runat="server" Enabled="false" autocomplete="off" CssClass="form-control" ID="txtVehicleNo" MaxLength="20" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                     <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Supervisor Name</label>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtSupervisorName" MaxLength="80" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    <div class="col-md-2">
                                            <div class="form-group">
                                                <label>In Time</label>                                            
                                                <div class="input-group bootstrap-timepicker timepicker">
                                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control timepicker"  ID="txtInTime" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
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
                                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control timepicker" Text="00:00 AM" ID="txtOutTime" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                                    <span class="input-group-addon"><i class="fa fa-clock-o"></i></span>
                                                </div>

                                            </div>
                                        </div>
                                     <div class="col-md-2" style="margin-top: 20px;">
                                            <div class="form-group">
                                                <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="b" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />

                                            </div>
                                        </div>
                                    </div>
                             </fieldset>
                            <div class="row" id="pnldata" runat="server" visible="false">
                                <fieldset>
                                    <legend>Item Details
                                    </legend>

                                    <div class="col-md-6">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" ShowFooter="true" runat="server" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound" EmptyDataText="No Record Found." EnableModelValidation="True">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Milk Name" HeaderStyle-Width="5px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemName" Text='<%#Eval("ItemName") %>' runat="server" />
                                                            <asp:Label ID="lblItem_id" Visible="false" Text='<%#Eval("Item_id") %>' runat="server" />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <b>Total</b>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Milk Qty." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMilkQty" Text='<%#Eval("TotalSupplyQty") %>' runat="server" />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFTotalMilkQty" Font-Bold="true" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Crate Qty." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCrate" Text='<%#Eval("IssueCrate") %>' runat="server" />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFTotalCrate" Font-Bold="true" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Extra PKT(-/+)" HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblExtrapacket" Text='<%#Eval("Extrapacket") %>' runat="server" />
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

                    <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Merge Gate Pass Report</h3>


                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                             <fieldset>
                                    <legend>FromDate,ToDate,Shift,Location
                                    </legend>
                            <div class="row">
                               
                                    <div class="col-lg-12">
                                    <asp:Label ID="lblSearchMsg" runat="server"></asp:Label>
                                </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>From Date <span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="c" ControlToValidate="txtFromDate"
                                                    ErrorMessage="Enter FromDate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter FromDate !'></i>"
                                                    Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="c" runat="server" Display="Dynamic" ControlToValidate="txtFromDate"
                                                    ErrorMessage="Invalid From Date" Text="<i class='fa fa-exclamation-circle' title='Invalid From Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtFromDate" MaxLength="10" placeholder="Enter FromDate" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>To Date <span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="c" ControlToValidate="txtToDate"
                                                    ErrorMessage="Enter ToDate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter ToDate !'></i>"
                                                    Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="c" runat="server" Display="Dynamic" ControlToValidate="txtToDate"
                                                    ErrorMessage="Invalid ToDate" Text="<i class='fa fa-exclamation-circle' title='Invalid ToDate !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtToDate" MaxLength="10" placeholder="Enter ToDate" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Location</label>
                                           
                                            <asp:DropDownList ID="ddlSearchLocation" AutoPostBack="true" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Shift</label>
                                            <asp:DropDownList ID="ddlSearchShift" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" OnClick="btnReportSearch_Click"  ValidationGroup="c" ID="btnReportSearch" Text="Search" />

                                        </div>
                                    </div>
                                    <div class="col-md-1" style="margin-top: 20px;">
                                        <div class="form-group">

                                            <asp:Button ID="btnSearchClear" runat="server" Text="Clear" OnClick="btnSearchClear_Click" CssClass="btn btn-default" />
                                        </div>
                                    </div>
                               
                            </div>
                       
                             <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView2" runat="server" OnRowCommand="GridView2_RowCommand" class="datatable table table-striped table-bordered" AllowPaging="false"
                                            AutoGenerateColumns="false" DataKeyNames="MergeVehicleChallanId" EmptyDataText="No Record Found." EnableModelValidation="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Challan No." HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVDChallanNo" Text='<%#Eval("MergeVChallanNo") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDelivary_Date" Text='<%#Eval("Delivary_Date") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="In Time." HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVehicleIn_Time" Text='<%#Eval("VehicleIn_Time") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Out Time." HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVehicleOut_Time" Text='<%#Eval("VehicleOut_Time") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Shift" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblShiftName" Text='<%#Eval("ShiftName") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Vehicle No." HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVehicleNo" Text='<%#Eval("VehicleNo") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Supervisor Name" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSupervisorName" Text='<%#Eval("SupervisorName") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Distributor Name" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDistName" Text='<%#Eval("DistName") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Total Issue Crate" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRName" Text='<%#Eval("TotalIssueCrate") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkprint" CommandName="RecordPrint" CommandArgument='<%#Eval("MergeVehicleChallanId") %>' runat="server" ToolTip="Print"><i class="fa fa-print"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkDelete" CommandName="DeleteChallan" CommandArgument='<%#Eval("MergeVehicleChallanId") %>' runat="server" OnClientClick="return confirm('Are you sure to Delete?')"  ToolTip="Delte Challan"><i style="color:red;" class="fa fa-trash"></i></asp:LinkButton>
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
        </section>

        <!-- /.content -->
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
     <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../js/bootstrap-multiselect.js"></script>
   
     <script type="text/javascript">
         window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
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
        $(function () {
            $('[id*=ddlDistributor]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });


        });
         </script>
</asp:Content>
