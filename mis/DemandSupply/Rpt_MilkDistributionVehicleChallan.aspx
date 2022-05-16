<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Rpt_MilkDistributionVehicleChallan.aspx.cs" Inherits="mis_DemandSupply_Rpt_MilkDistributionVehicleChallan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
     <link href="../Finance/css/dataTables.bootstrap.min.css" rel="stylesheet" />
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
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content noprint">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Milk Delivery  Challan Report</h3>


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


                            <div class="row">
                                <fieldset>
                                    <legend>FromDate,ToDate,Shift,Location,Route
                                    </legend>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>From Date /दिनांक से<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a" ControlToValidate="txtFromDate"
                                                    ErrorMessage="Enter FromDate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter FromDate !'></i>"
                                                    Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtFromDate"
                                                    ErrorMessage="Invalid From Date" Text="<i class='fa fa-exclamation-circle' title='Invalid From Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtFromDate" MaxLength="10" placeholder="Enter FromDate" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>To Date / दिनांक तक<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a" ControlToValidate="txtToDate"
                                                    ErrorMessage="Enter ToDate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter ToDate !'></i>"
                                                    Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtToDate"
                                                    ErrorMessage="Invalid ToDate" Text="<i class='fa fa-exclamation-circle' title='Invalid ToDate !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtToDate" MaxLength="10" placeholder="Enter Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Location / स्थान<span style="color: red;"> *</span></label>
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
                                            <label>Route No / रूट <%--<span style="color: red;"> *</span>--%></label>
                                            <%-- <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Route" Text="<i class='fa fa-exclamation-circle' title='Select Route !'></i>"
                                                    ControlToValidate="ddlRoute" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                            </span>--%>
                                            <asp:DropDownList ID="ddlRoute" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Shift / शिफ्ट</label>
                                            <%-- <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Shift" Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"
                                                    ControlToValidate="ddlShift" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>--%>
                                            <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <%--<asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />--%>
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" OnClick="btnSearch_Click" ID="btnSearch" Text="Search" AccessKey="S" />

                                        </div>
                                    </div>
                                    <div class="col-md-1" style="margin-top: 20px;">
                                        <div class="form-group">

                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-default" />
                                        </div>
                                    </div>
                                </fieldset>
                            </div>


                        </div>

                    </div>
                </div>

                <div class="col-md-12" id="pnldata" runat="server" visible="false">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Milk Delivery Challan Report</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" class="datatable table table-striped table-bordered" AllowPaging="false"
                                            AutoGenerateColumns="false" DataKeyNames="VehicleDispId" EmptyDataText="No Record Found." EnableModelValidation="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Challan No." HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVDChallanNo" Text='<%#Eval("VDChallanNo") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDelivary_Date" Text='<%#Eval("Delivary_Date") %>' runat="server" />
                                                         <asp:Label ID="lblDelivaryShiftid" Visible="false" Text='<%#Eval("DelivaryShift_id") %>' runat="server" />
                                                         <asp:Label ID="lblAreaId" Visible="false" Text='<%#Eval("AreaId") %>' runat="server" />
                                                          <asp:Label ID="lblRouteId" Visible="false" Text='<%#Eval("RouteId") %>' runat="server" />
                                                        <asp:Label ID="lblProductDMStatus" Visible="false" Text='<%#Eval("ProductDMStatus") %>' runat="server" />
                                                        <asp:Label ID="lblMilkOrProductDemandId" Visible="false" Text='<%#Eval("MilkOrProductDemandId") %>' runat="server" />
                                                    
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
                                                <asp:TemplateField HeaderText="Route" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRName" Text='<%#Eval("RName") %>' runat="server" />
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
                                                  <asp:TemplateField HeaderText="Crate Status" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCrateStatus" Text='<%#Eval("CrateStatus") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Status" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPDMStatus" Text='<%#Eval("PDMStatus") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGPStatus" Text='<%#Eval("GPStatus") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Print / प्रिंट करें" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkprint" CommandName="RecordPrint" Visible='<%#Eval("GenStatus").ToString()=="1" ? true :false %>' CommandArgument='<%#Eval("VehicleDispId") %>' runat="server" ToolTip="Print"><i class="fa fa-print"></i></asp:LinkButton>
                                                         <asp:LinkButton ID="lnkbtn" CommandName="RecordRedirected" Visible='<%#Eval("GenStatus").ToString()=="1" ? false :true %>' runat="server" ToolTip="Generate Challan"><i class="fa fa-external-link"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkDelete" CommandName="DeleteChallan" Visible='<%#Eval("GenStatus").ToString()=="1" ? true :false %>' CommandArgument='<%#Eval("VehicleDispId") %>' runat="server" OnClientClick="return confirm('Are you sure to Delete?')"  ToolTip="Delete Challan"><i style="color:red;" class="fa fa-trash"></i></asp:LinkButton>
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
                    title: ('Milk Distribution Challan Report').bold().fontsize(3).toUpperCase(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8,9]
                    },
                    footer: false,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    title: ('Milk Distribution Challan Report').bold().fontsize(3).toUpperCase(),
                    filename: 'MilkDistributionChallanReport',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',

                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8,9]
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
        function myItemDetailsModal() {
            $("#ItemDetailsModal").modal('show');

        }
    </script>
</asp:Content>
