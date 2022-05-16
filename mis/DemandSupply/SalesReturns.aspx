<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SalesReturns.aspx.cs" Inherits="mis_Demand_Supply_SalesReturns" %>

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
            border: 1px dashed #000000 !important;
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
            padding: 2px 5px;
           
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
  <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content noprint">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">           
                  <div class="col-md-12">
                <div class="box box-Manish">
                    <div class="box-header">
                        <h3 class="box-title">Sales Return</h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <fieldset>
                            <legend>Sales Return 
                            </legend>
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>
                            </div>


                            <div class="row">
                               
                                 <div class="col-md-2">
                                     <div class="form-group">
                                        <label>Date / दिनांक<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                ControlToValidate="txtSupplyDate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtSupplyDate"
                                                ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                        </span>
                                       <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtSupplyDate" OnTextChanged="txtSupplyDate_TextChanged" AutoPostBack="true" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>--%>
                                          <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtSupplyDate" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                     </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Shift / शिफ्ट<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                InitialValue="0" ErrorMessage="Select Shift" Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"
                                                ControlToValidate="ddlShift" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                       <%-- <asp:DropDownList ID="ddlShift" runat="server" OnInit="ddlShift_Init" OnSelectedIndexChanged="ddlShift_SelectedIndexChanged"  AutoPostBack="true" CssClass="form-control select2"></asp:DropDownList>--%>
                                         <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div> 
                               <%-- <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Item Category/वस्तू वर्ग<span style="color: red;"> *</span></label>
                                          <span class="pull-right">
                                          <asp:RequiredFieldValidator ID="rfvic" ValidationGroup="a"
                                                InitialValue="0" ErrorMessage="Select Item Category" Text="<i class='fa fa-exclamation-circle' title='Select ItemCategory !'></i>"
                                                ControlToValidate="ddlItemCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlItemCategory" OnInit="ddlItemCategory_Init" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div> --%> 
                                 <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Location / लोकेशन <span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Location" Text="<i class='fa fa-exclamation-circle' title='Select Location !'></i>"
                                                    ControlToValidate="ddlLocation" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="col-md-4" id="pnlSearchBy" runat="server" visible="true">
                                <div class="form-group">
                                        <label></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                ErrorMessage="Select anyone from Route,Distributor & Institution" Text="<i class='fa fa-exclamation-circle' title='Select anyone from Route,Distributor & Institution !'></i>"
                                                ControlToValidate="rblReportType" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:RadioButtonList ID="rblReportType"  runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem class="radio-inline" Text="Route wise&nbsp;&nbsp;" Value="1"></asp:ListItem> 
                                            <asp:ListItem class="radio-inline" Text="Distributor wise&nbsp;&nbsp;" Value="2"></asp:ListItem>
                                             <asp:ListItem class="radio-inline" Text="Institution wise&nbsp;&nbsp;" Value="3"></asp:ListItem>
                                            
                                        </asp:RadioButtonList>
                                    </div>
                                </div> 
                                                        
                            </div> 
                            <div class="row">
                                 <div class="col-md-1">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" OnClick="btnSearch_Click" ValidationGroup="a" ID="btnSearch" Text="Search" />

                                        </div>
                                    </div>
                                    <div class="col-md-1" style="margin-top: 20px;">
                                        <div class="form-group">

                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-default" />
                                        </div>
                                    </div>
                            </div>                         
                        </fieldset>
                               
                    </div>

                </div>
            </div>
                 <div class="col-md-12" id="pnlData" runat="server" visible="false">
                <div class="box box-Manish">
                    <div class="box-header">
                        <h3 class="box-title">Sales Return</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblReportMsg" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="row" id="pnlrouteOrDistOrInstwisedata" runat="server" visible="false">  
                                  <asp:Button ID="btnPrintRoutWise" runat="server" CssClass="btn btn-primary pull-right" Text="Print" OnClick="btnPrintRoutWise_Click" /> 
                                <div class="col-md-12">
                                        <fieldset>
                                            <legend><span id="pnllegand" runat="server"></span></legend>
                                            <div class="col-md-12">
                                               
                                                    <div class="form-group pull-right">
                                                        <asp:LinkButton ID="btnViewSalesReturnDetails" Text="<i class='fa fa-eye'></i> View Sales Return" OnClick="btnViewSalesReturnDetails_Click" Visible="false" CssClass="btn btn-success" runat="server"></asp:LinkButton>
                                                </div>
                                                </div>
                                                <div class="col-md-12">
                                                   <div class="table-responsive">
                                            <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand" ShowFooter="true" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                AutoGenerateColumns="true" EmptyDataText="No Record Found." EnableModelValidation="True">
                                               <Columns>
                                                   <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Route">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnRoute" CssClass="btn btn-block btn-secondary" Text='<%#Eval("Route") %>' CommandName="RoutwiseBooth" CommandArgument='<%#Eval("RouteId") %>' runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               </Columns>
                                            </asp:GridView>
                                                        <asp:GridView ID="GridView2" runat="server" OnRowDataBound="GridView2_RowDataBound" OnRowCommand="GridView2_RowCommand" ShowFooter="true" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                AutoGenerateColumns="true" EmptyDataText="No Record Found."  EnableModelValidation="True">
                                                <Columns>
                                                   <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Distributor Name">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnDistributor" CssClass="btn btn-block btn-secondary" Text='<%#Eval("Distributor Name") %>' CommandName="DistwiseBooth" CommandArgument='<%#Eval("DistributorId") %>' runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               </Columns>
                                               
                                            </asp:GridView>
                                                    <asp:GridView ID="GridView3" runat="server" OnRowDataBound="GridView3_RowDataBound" OnRowCommand="GridView3_RowCommand" ShowFooter="true" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                AutoGenerateColumns="true" EmptyDataText="No Record Found."  EnableModelValidation="True">
                                                <Columns>
                                                   <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Institution Name">
                                                    <ItemTemplate>
                                                       <%-- <asp:LinkButton ID="lnkbtnOrganization" CssClass="btn btn-block btn-secondary" Text='<%#Eval("Organization Name") %>' CommandName="Orgwise" CommandArgument='<%#Eval("OrganizationId") %>' runat="server"></asp:LinkButton>--%>
                                                         <asp:LinkButton ID="lnkbtnOrganization" CssClass="btn btn-block btn-secondary" Text='<%#Eval("Institution Name") %>' CommandName="Orgwise" CommandArgument='<%#Eval("BoothId") %>' runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               </Columns>
                                               
                                            </asp:GridView>  
                                                </div>
                                            </div>                     
                                        </fieldset>
                                    </div>
                            </div>
                        <div class="modal" id="ItemDetailsModal">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">×</span></button>
                                    <h4 class="modal-title"><span id="modalRDIName" style="color: red" runat="server"></span> <br />
                             Supply Date : <span id="modaldate" style="color: red" runat="server"></span>&nbsp;&nbsp;Supply Shift : <span id="modalshift" style="color: red" runat="server"></span>
                                        &nbsp;&nbsp;Category : <span id="modalcategory" style="color: red" runat="server"></span>
                                    </h4>
                                </div>
                                <div class="modal-body">
                                    <div id="divitem" runat="server">
                                        <asp:Label ID="lblModalMsg" runat="server" Text=""></asp:Label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <fieldset>
                                                    <legend>Item Details for Sales Return</legend>
                                                     <div class="row" style="height: 250px; overflow: scroll;">
                                                    <div class="col-md-12">
                                                        <div class="table-responsive">
                                                    <div id="divStringBuilder" runat="server"></div>
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
                                   <button type="button" class="btn btn-primary" id="btnPrint" runat="server"  onclick="Print()">Print</button>
                                  
                                    
                                </div>
                                </div>
                            </div>
                            <!-- /.modal-content -->
                        </div>
                               
                    </div>

                </div>
            </div>
             </div>
        </section>
        <!-- /.content -->
         <section class="content">
            <div id="Print" runat="server" class="NonPrintable"></div>
             <div id="Print1" runat="server" class="NonPrintable"></div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
        function myItemDetailsModal() {
            $("#ItemDetailsModal").modal('show');

        }
        function Print() {
           
            window.print();
            
        }
    </script>
</asp:Content>

