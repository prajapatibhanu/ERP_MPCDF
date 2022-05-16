<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MilkOrProductOrderList_IUSDS.aspx.cs" Inherits="mis_Demand_MilkOrProductOrderList_IUSDS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
     <link href="../Finance/css/jquery.dataTables.min.css" rel="stylesheet" />
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
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="c" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="d" ShowMessageBox="true" ShowSummary="false" />
     <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="ModalSave" ShowMessageBox="true" ShowSummary="false" />

    <div class="content-wrapper">
        <section class="content">
            <div class="row">


                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Milk Or Product Order Status </h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" CssClass="Autoclr" runat="server"></asp:Label>
                                </div>
                            </div>
                            <fieldset>
                                <legend>Date,Shift,Category,PartyName
                                </legend>
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Date<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    ControlToValidate="txtOrderDate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtOrderDate"
                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOrderDate" MaxLength="10" placeholder="Enter Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Shift</label>
                                            <asp:DropDownList ID="ddlShift" runat="server" ClientIDMode="Static" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                        <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Item Category</label>
                                            <asp:DropDownList ID="ddlItemCategory" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Party Name</label>
                                                   
                                           
                                                    <asp:DropDownList ID="ddlPartyName"  runat="server" CssClass="form-control select2">
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                     <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Status </label>
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                                 <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
                                                 <asp:ListItem Text="Rejected" Value="2"></asp:ListItem>
                                                 <asp:ListItem Text="Approved" Value="3"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-1">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" OnClick="btnSearch_Click" ValidationGroup="a" ID="btnSearch" Text="Search" />
                                        </div>
                                    </div>
                                    <div class="col-md-1" style="margin-top: 20px;">
                                        <div class="form-group">
                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Reset" CssClass="btn btn-block btn-secondary" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                       
                           <fieldset>
                                    <legend>Milk Or Product Order List
                                    </legend>
                        <div class="row">
                          

                            <div class="col-md-12" id="pnlsearch" runat="server" visible="false">
                               
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" class="datatable table table-striped table-bordered" AllowPaging="false"
                                            AutoGenerateColumns="False" EmptyDataText="No Record Found." OnRowCommand="GridView1_RowCommand" EnableModelValidation="True" DataKeyNames="MilkOrProductDemandId">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                         <asp:Label ID="lblMilkOrProductDemandId" Visible="false" Text='<%# Eval("MilkOrProductDemandId")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order Id">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblOrderId" CommandName="ItemOrdered" CommandArgument='<%#Eval("MilkOrProductDemandId") %>' Text='<%#Eval("OrderId") %>' runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Retailer/Institution Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBandOName" Text='<%# Eval("BandOName") %> ' runat="server" />
                                                        <asp:Label ID="lblItemCatid" Visible="false" Text='<%# Eval("ItemCat_id")%>' runat="server" />
                                                        <asp:Label ID="lblDStatus" Visible="false" Text='<%# Eval("Demand_Status")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Vehicle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVehicleNo" Text='<%# Eval("VehicleNo")%>' runat="server" />
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
                                                <asp:TemplateField HeaderText="Order Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDemandDate" Text='<%# Eval("Demand_Date")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Order Type" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPDMStatus" Text='<%#Eval("PDMStatus") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDemandStatus" Text='<%# Eval("DStatus")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View Details" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkViewDetails" CssClass="btn btn-primary btn-primary" CommandName="ItemOrdered" CommandArgument='<%#Eval("MilkOrProductDemandId") %>' runat="server"><i class="fa fa-eye"></i> View </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                 
                              

                                <div class="modal" id="ItemDetailsModal">
                                    <div class="modal-dialog modal-lg">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">×</span></button>
                                                <h4 class="modal-title">Item Details for <span id="modalBoothName" style="color: red" runat="server"></span>&nbsp;&nbsp;
                             Order Date :<span id="modaldate" style="color: red" runat="server"></span>&nbsp;&nbsp;Order Shift :<span id="modalshift" style="color: red" runat="server"></span>
                                                    &nbsp;&nbsp;Order Status :<span id="modalorderStatus" runat="server"></span>
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
                                                                            <asp:GridView ID="GridView4" runat="server" OnRowDataBound="GridView4_RowDataBound" EmptyDataText="No Record Found" class="table table-striped table-bordered" AllowPaging="false"
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
                                                                                             <asp:Label ID="lblMilkCurDMCrateIsueStatus" Visible="false" Text='<%# Eval("MilkCurDMCrateIsueStatus")%>' runat="server" />
                                                                                            <asp:Label ID="lblProductDMStatus" Visible="false" Text='<%# Eval("ProductDMStatus")%>' runat="server" />
                                                                                             <asp:Label ID="lblMilkOrProductDemandChildId" Visible="false" Text='<%# Eval("MilkOrProductDemandChildId")%>' runat="server" />
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
                                                                                                ErrorMessage="Enter Valid Number In Quantity Field & First digit can't be 0(Zero)!" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Number In Quantity Field & First digit can't be 0(Zero)!'></i>" ControlToValidate="txtItemQty"
                                                                                                ValidationExpression="^[0-9]*$">
                                                                                            </asp:RegularExpressionValidator>
                                                                                            </span>
                                               <asp:TextBox runat="server" autocomplete="off" Visible="false" CausesValidation="true" Text='<%# Eval("ItemQty")%>' CssClass="form-control" ID="txtItemQty" MaxLength="5" onpaste="return false;" onkeypress="return validateNum(event);" placeholder="Enter Item Qty." ClientIDMode="Static"></asp:TextBox>
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
                                                                                    <asp:TemplateField HeaderText="Remark/ टिप्पणी">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblRemarkAtOrderApproval" runat="server" Text='<%#Eval("RemarkAtOrderApproval") %>' />

                                                                                            <asp:TextBox runat="server" autocomplete="off" Visible="false" CssClass="form-control" ID="txtRemarkAtOrderApproval" MaxLength="200" placeholder="Enter Remark" ClientIDMode="Static"></asp:TextBox>
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
                                    </div>
                                 
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
   <script src="..js/buttons.colVis.min.js"></script>
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
            lengthMenu: [10, 25, 50, 100, 200, 500],
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
                    title: ('Order Approval List').bold().fontsize(5).toUpperCase(),
                    text: '<i class="fa fa-print"></i> Print',
                    exportOptions: {
                        columns: [0,1, 2, 3, 4, 5, 6, 7, 8]
                    },
                    footer: false,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    title: ('Order Approval List').bold().fontsize(5).toUpperCase(),
                    filename: 'Order List',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',

                    exportOptions: {
                        columns: [0,1, 2, 3, 4, 5, 6, 7, 8]
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
