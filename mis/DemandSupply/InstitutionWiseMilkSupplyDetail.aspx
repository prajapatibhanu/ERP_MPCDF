<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="InstitutionWiseMilkSupplyDetail.aspx.cs" Inherits="mis_DemandSupply_InstitutionWiseMilkSupplyDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
    <style>
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
            padding: 2px 5px;
           
        } 
         @media print {
             
              .noprint {
                display: none;
            }
               
          }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSearch_Click"  Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content noprint">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">
                <div class="col-md-12 no-print">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Institution Wise Milk Supply Detail</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <fieldset>
                                <legend>FILTER
                                </legend>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </div>
                                </div>


                                <div class="row no-print">

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>From Date <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter From Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter From Date !'></i>"
                                                    ControlToValidate="txtFromDate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtFromDate"
                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtFromDate" MaxLength="10" placeholder="Select From Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>  
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>To Date <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                    ErrorMessage="Enter To Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter To Date !'></i>"
                                                    ControlToValidate="txtToDate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtToDate"
                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtToDate" MaxLength="10" placeholder="Select To Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>                                
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Item Category<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvic" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Item Category" Text="<i class='fa fa-exclamation-circle' title='Select Item Category !'></i>"
                                                    ControlToValidate="ddlItemCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <%--<asp:DropDownList ID="ddlItemCategory" AutoPostBack="true" OnInit="ddlItemCategory_Init" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged" runat="server" CssClass="form-control select2"></asp:DropDownList>--%>
                                            <asp:DropDownList ID="ddlItemCategory" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class="col-md-3">
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
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Route<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Route" Text="<i class='fa fa-exclamation-circle' title='Select Route !'></i>"
                                                    ControlToValidate="ddlRoute" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlRoute" AutoPostBack="true" OnSelectedIndexChanged="ddlRoute_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                                 <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>                                  
                                    
                                       <div class="col-md-3"  runat="server" >
                                        <div class="form-group">
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Institution" Text="<i class='fa fa-exclamation-circle' title='Select Institution !'></i>"
                                                    ControlToValidate="ddlInstitution" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <label>Institution <span style="color: red;"> *</span></label>
                                            <asp:DropDownList ID="ddlInstitution" runat="server" CssClass="form-control select2">
                                                 <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                              </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" OnClick="btnSearch_Click" ValidationGroup="a" ID="btnSearch" Text="Search" />

                                        </div>
                                    </div>
                                    <div class="col-md-1" style="margin-top: 20px;">
                                        <div class="form-group">

                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-block btn btn-default" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                        </div>

                    </div>
                </div>
                
            </div>
        </section>
        <!-- /.content -->
        <section class="content">
             
                      <div class="row noprint">
                          <div class="col-md-12 pull-left">
                              <asp:Button ID="btnPrint" CssClass="btn btn-primary" Visible="false" Text="Print" runat="server" OnClientClick="Print()" />
                              <asp:Button ID="btnExcel" CssClass="btn btn-primary" Visible="false" Text="Excel" runat="server" OnClick="btnExcel_Click" />
                          </div>
                         
                      </div>
                      <div class="row">
                          <div class="col-md-12">
                               <div id="div_page_content" runat="server" class="NonPrintable"></div>
                          </div>
                      </div>
                
              
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);

        
        function Print() {
            window.print();
        }
    </script>
</asp:Content>

