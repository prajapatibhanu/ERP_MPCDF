<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RejectGatePass_IDS.aspx.cs" Inherits="mis_Demand_RejectGatePass_IDS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
    <link href="../Finance/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <style>
       .NonPrintable {
                  display: none;
              }
        @media print {
              .NonPrintable {
                  display: block;
              }
               @page {
                size: portrait;
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
        .CapitalClass {
            text-transform: uppercase;
        }

        .capitalize {
            text-transform: capitalize;
        }

        .columngreen {
            background-color: #aee6a3 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
        <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content noprint">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Reject Gate Pass</h3>

                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">

                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>
                            </div>


                            <div class="row">
                                <fieldset>
                                    <legend>Date,Category,Vehicle No
                                    </legend>
                                 <div class="col-md-2">
                                        <div class="form-group">
                                            <label>From Date<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:requiredfieldvalidator id="RequiredFieldValidator1" validationgroup="a"
                                                    errormessage="Enter Date" forecolor="Red" text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    controltovalidate="txtFromDate" display="Dynamic" runat="server">
                                                </asp:requiredfieldvalidator>
                                                <asp:regularexpressionvalidator id="RegularExpressionValidator3" validationgroup="b" runat="server" display="Dynamic" controltovalidate="txtFromDate"
                                                    errormessage="Invalid Date" text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" setfocusonerror="true"
                                                    validationexpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:regularexpressionvalidator>
                                            </span>
                                            <asp:textbox runat="server" OnTextChanged="txtFromDate_TextChanged" AutoPostBack="true" autocomplete="off" cssclass="form-control" id="txtFromDate" maxlength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" clientidmode="Static" tabindex="1"></asp:textbox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>To Date<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:requiredfieldvalidator id="RequiredFieldValidator2" validationgroup="a"
                                                    errormessage="Enter Date" forecolor="Red" text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    controltovalidate="txtToDate" display="Dynamic" runat="server">
                                                </asp:requiredfieldvalidator>
                                                <asp:regularexpressionvalidator id="RegularExpressionValidator1" validationgroup="b" runat="server" display="Dynamic" controltovalidate="txtToDate"
                                                    errormessage="Invalid Date" text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" setfocusonerror="true"
                                                    validationexpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:regularexpressionvalidator>
                                            </span>
                                            <asp:textbox runat="server" OnTextChanged="txtFromDate_TextChanged" AutoPostBack="true" autocomplete="off" cssclass="form-control" id="txtToDate" maxlength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" clientidmode="Static" tabindex="1"></asp:textbox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Shift </label>
                                            <span class="pull-right">
                                                <asp:requiredfieldvalidator id="rfv1" validationgroup="a"
                                                    initialvalue="" errormessage="Select Shift" text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"
                                                    controltovalidate="ddlShift" forecolor="Red" display="Dynamic" runat="server">
                                                </asp:requiredfieldvalidator>
                                            </span>
                                            <asp:dropdownlist id="ddlShift" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlShift_SelectedIndexChanged" clientidmode="Static" cssclass="form-control select2" tabindex="2"></asp:dropdownlist>
                                        </div>
                                    </div>
                                       <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Item Category</label>
                                        <asp:DropDownList ID="ddlItemCategory" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Vehicle No.</label>
                                           <asp:DropDownList ID="ddlVehicleNo" ClientIDMode="Static" runat="server" CssClass="form-control select2"></asp:DropDownList>
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
                                </fieldset>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-12" id="pnldata" runat="server" visible="false">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Gate Pass Details</h3>
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
                                                <asp:TemplateField HeaderText="Gate Pass No." HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVDChallanNo" Text='<%#Eval("VDChallanNo") %>' runat="server" />
                                                         <asp:Label ID="lblMulti_MilkOrProductDemandId" Visible="false" Text='<%#Eval("Multi_MilkOrProductDemandId") %>' runat="server" />
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
                                                <asp:TemplateField HeaderText="Distributor Name" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDistName" Text='<%#Eval("DistName") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Supervisor Name" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSupervisorName" Text='<%#Eval("SupervisorName") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Crate Issue" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIssueCrate" Text='<%#Eval("IssueCrate") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Crate Status" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCrateStatus" Text='<%#Eval("CrateStatus") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                         <asp:LinkButton ID="lnkprint" CommandName="RecordPrint" CssClass="btn btn-success" CommandArgument='<%#Eval("VehicleDispId") %>' runat="server" ToolTip="Print"><i class="fa fa-print"> Print</i></asp:LinkButton>
                                                       &nbsp;&nbsp;&nbsp; <asp:LinkButton ID="lnkReject" CssClass="btn btn-danger" OnClientClick="return confirm('Are you sure to Reject Order ?')" CommandName="RecordReject" CommandArgument='<%#Eval("VehicleDispId") %>' runat="server" ToolTip="Reject"><i class="fa fa-trash"> Reject</i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:GridView ID="GridView2" runat="server" OnRowCommand="GridView2_RowCommand" class="datatable table table-striped table-bordered" AllowPaging="false"
                                            AutoGenerateColumns="false" DataKeyNames="ProductGatePassId" EmptyDataText="No Record Found." EnableModelValidation="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Gate Pass No." HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVDChallanNo" Text='<%#Eval("GatePassNo") %>' runat="server" />
                                                         <asp:Label ID="lblMulti_MilkOrProductDemandId" Visible="false" Text='<%#Eval("Multi_MilkOrProductDemandId") %>' runat="server" />
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
                                                   <asp:TemplateField HeaderText="Distributor Name" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDistName" Text='<%#Eval("DistName") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Supervisor Name" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSupervisorName" Text='<%#Eval("SupervisorName") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Crate Issue" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIssueCrate" Text='<%#Eval("IssueCrate") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Crate Status" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCrateStatus" Text='<%#Eval("CrateStatus") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkprint" CssClass="btn btn-success" CommandName="RecordPrint" CommandArgument='<%#Eval("ProductGatePassId") %>' runat="server" ToolTip="Print"><i class="fa fa-print"> Print</i></asp:LinkButton>
                                                         &nbsp;&nbsp;&nbsp; <asp:LinkButton ID="lnkReject" CssClass="btn btn-danger" OnClientClick="return confirm('Are you sure to Reject Order ?')" CommandName="RecordReject" CommandArgument='<%#Eval("ProductGatePassId") %>' runat="server" ToolTip="Reject"><i class="fa fa-trash"> Reject</i></asp:LinkButton>
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
          
            <div id="Print" runat="server" class="NonPrintable"></div> 
            <div id="Print1" runat="server" class="NonPrintable"></div>  
                
        </section>
     
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
   
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
                    title: ('Gate Pass').bold().fontsize(3).toUpperCase(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8,9]
                    },
                    footer: false,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    title: ('Gate Pass Report').bold().fontsize(3).toUpperCase(),
                    filename: 'Gate Pass',
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

