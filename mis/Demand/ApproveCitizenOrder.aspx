<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ApproveCitizenOrder.aspx.cs" Inherits="mis_Demand_ApproveCitizenOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
      <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
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
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes"  OnClick="btnSubmit_Click"
                            Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" 
                            Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>

        </div>
    </div>
     <%--ConfirmationModal End --%>

      <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />


     <div class="content-wrapper">
        <section class="content">
            <div class="row">

                
                <div class="col-md-12">
                <div class="box box-Manish">
                    <div class="box-header">
                            <h3 class="box-title">Approval List of Citizen</h3>
                        </div>
                          <!-- /.box-header -->
                     <div class="box-body">
                  
                    <fieldset>
                          <legend>Date,Category
                                </legend>
                          <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" CssClass="Autoclr" runat="server"></asp:Label>
                        </div>
                    </div>
                      
                            <div class="row">

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Date / दिनांक<span style="color: red;"> *</span></label>
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
                                            <label>Item Category /वस्तु वर्ग</label>
                                            <asp:DropDownList ID="ddlItemCategory" OnInit="ddlItemCategory_Init" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>


                               
                        <div class="col-md-1">
                                    <div class="form-group" style="margin-top:20px;">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-primary" OnClick="btnSearch_Click" ValidationGroup="a" ID="btnSearch" Text="Search"  />
                                    </div>
                                </div>
                                <div class="col-md-1" style="margin-top:20px;">
                                    <div class="form-group">
                                        <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Reset" CssClass="btn btn-block btn-secondary" />
                                    </div>
                                </div>
                       </div>
                            </fieldset>
                          </div>
                    <div class="box-body">
                      <fieldset>
                             <legend>Order List
                                    </legend>
                         <div class="row">

                        
                           
                           <div class="col-md-12" id="pnlsearch" runat="server" >
                              
                                 
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" OnRowCommand="GridView1_RowCommand" runat="server" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                AutoGenerateColumns="False" EmptyDataText="No Record Found." 
                                                EnableModelValidation="True" DataKeyNames="MobileNo">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo./ क्र." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                             <asp:Label ID="lblCitizenName" Visible="false" Text='<%# Eval("CitizenName")%>' runat="server" />
                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="InVoice No.">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblInvoiceNo" CommandName="ItemOrdered" CommandArgument='<%#Eval("MobileNo") %>' Text='<%#Eval("InvoiceNo") %>' runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                              
                                                     <asp:TemplateField HeaderText="Demand Date">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblDemand_Date" Text='<%# Eval("Demand_Date")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delivery Shift">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblDeliveryShift_id" Visible="false" Text='<%# Eval("DeliveryShift_id")%>' runat="server" />
                                                             <asp:Label ID="lblShiftName" Text='<%# Eval("ShiftName")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Delivery Date / वितरण दिनांक ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDelivery_Date" Text='<%# Eval("Delivery_Date","{0:dd/MM/yyyy}")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Payment Confirmation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDelivaryShift" Text='<%# Eval("DelivaryShift")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Approval Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDStatus" Text='<%# Eval("DStatus")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                   <%-- <asp:TemplateField HeaderText="View Details" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkViewDetails" CssClass="btn btn-xs btn-outline"
                                                                 CommandName="ItemOrdered" CommandArgument='<%#Eval("MobileNo") %>'
                                                                 runat="server"><i class="fa fa-eye"></i> View</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                         <%--<asp:TemplateField HeaderText="Demand Status" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDStatus" Text='<%# Eval("ItemCatName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View Details" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkViewDetails" CssClass="btn btn-primary btn-primary" Visible='<%# Eval("DStatus").ToString() == "Yes" ? true : false %>' CommandName="ItemOrdered" CommandArgument='<%#Eval("tmp_MilkOrProductDemandId") %>' runat="server"><i class="fa fa-eye"></i> View Details</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>


                                                       <asp:TemplateField HeaderText="View Details" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkViewDetails" CssClass="btn btn-primary btn-primary" CommandName="ItemOrdered" CommandArgument='<%#Eval("MobileNo") %>' runat="server"><i class="fa fa-eye"></i> View </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                    </div>
                                    
                                                 <div class="col-md-12">
                                        <%--<p>Total Crate Required :  <b>12</b></p>--%>
                                        <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="a" ErrorMessage="Please select at least one record."
                                            ClientValidationFunction="Validate" ForeColor="Red"></asp:CustomValidator>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" Visible="false" OnClientClick="return ValidatePage();" AccessKey="S" ID="btnSubmit"  Text="Approve" />
                                        </div>
                                    </div>
                                </div>
                              </div>
                                </fieldset>
                            </div>
       <div class="modal" id="ItemDetailsModal">
                                    <div class="modal-dialog modal-lg">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">×</span></button>
                                                <h4 class="modal-title">Citizen Name:<span id="modalBoothName" style="color: red" runat="server"></span>&nbsp;&nbsp;Order Date :<span id="modaldate" style="color: red" runat="server"></span>&nbsp;&nbsp;Order Shift : <span id="modelShift" style="color: red" runat="server"></span></h4>
                                            </div>
                                            <div class="modal-body">
                                                <div id="divitem" runat="server">
                                                    <asp:Label ID="lblModalMsg" runat="server" Text=""></asp:Label>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                                <div style="height: 250px; overflow: scroll;">
                                                                    <div class="col-md-12">
                                                                        <div class="table-responsive">
                                                                <asp:GridView ID="GridView4" runat="server" OnRowCommand="GridView4_RowCommand"  EmptyDataText="No Record Found" class="table table-striped table-bordered" AllowPaging="false"
                                                                    AutoGenerateColumns="False" EnableModelValidation="True" DataKeyNames="MobileNo">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="SNo./ क्र." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Item Category / वस्तु वर्ग">
                                                                            <ItemTemplate>
                                                                                 <asp:Label ID="lblMobileNo" runat="server" visible="false" Text='<%# Eval("MobileNo") %>' />
                                                                               
                                                                                <asp:Label ID="lblItemCatName" Text='<%# Eval("ItemCatName")%>' runat="server" />
                                                                                <asp:Label ID="lblItemCat_id" Visible="false" Text='<%# Eval("ItemCat_id")%>' runat="server" />
                                                                                 <asp:Label ID="lblCDemandStatus" Visible="false" Text='<%# Eval("CDemand_Status")%>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Item Name / वस्तु नाम">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblIName" Text='<%# Eval("ItemName")%>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                               <%--         <asp:TemplateField HeaderText="Qty./ मात्रा">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblItemQty" Text='<%# Eval("ItemQty")%>' runat="server" />
                                                                                <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="c"
                                                                                    ErrorMessage="Enter Item Qty." Enabled="false" Text="<i class='fa fa-exclamation-circle' title='Enter Item Qty. !'></i>"
                                                                                    ControlToValidate="txtItemQty" ForeColor="Red" Display="Dynamic" runat="server">
                                                                                </asp:RequiredFieldValidator>
                                                                                <asp:RegularExpressionValidator ID="rev1" Enabled="false" runat="server" Display="Dynamic" ValidationGroup="c"
                                                                                    ErrorMessage="Invalid Item Qty, Accept Number only. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Invalid Item Qty, Accept Number only. !'></i>" ControlToValidate="txtItemQty"
                                                                                    ValidationExpression="^[1-9][0-9]*$">
                                                                                </asp:RegularExpressionValidator>
                                                                                </span>
                                               <asp:TextBox runat="server" autocomplete="off" Visible="false" CausesValidation="true" Text='<%# Eval("ItemQty")%>' CssClass="form-control" ID="txtItemQty" MaxLength="5" onkeypress="return validateNum(event);" placeholder="Enter Item Qty." ClientIDMode="Static"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Advance Card / एडवांस कार्ड ">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAdvCard" Text='<%# Eval("AdvCard")%>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>
                                                                        <asp:TemplateField HeaderText="Total Qty./ कुल मात्रा">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTotalQty" Text='<%# Eval("QtyInNo")%>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--<asp:TemplateField HeaderText="Action / कार्यवाही करें">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkEdit" CommandName="RecordEdit" CommandArgument='<%#Eval("InVoiceNo") %>' runat="server" ToolTip="Edit"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                                                &nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkUpdate" Visible="false" ValidationGroup="c" CausesValidation="true" CommandName="RecordUpdate" CommandArgument='<%#Eval("MilkOrProductDemandChildId") %>' runat="server" ToolTip="Update" Style="color: darkgreen;" OnClientClick="return confirm('Are you sure to Update?')"><i class="fa fa-check"></i></asp:LinkButton>
                                                                                &nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkReset" Visible="false" CommandName="RecordReset" CommandArgument='<%#Eval("MilkOrProductDemandChildId") %>' runat="server" ToolTip="Reset" Style="color: gray;"><i class="fa fa-times"></i></asp:LinkButton>
                                                                                &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" Visible="false" CommandArgument='<%#Eval("MilkOrProductDemandChildId") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>
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
                                    <div class="row">
                                         <div class="col-md-2">
                                            <div class="form-group">
                                               <asp:Button ID="btnReject" CssClass="btn btn-danger" OnClientClick="return confirm('Do you want to Reject Order?')" OnClick="btnReject_Click" Text="Reject" runat="server" />
                                            </div>

                                        </div>
                                        <div class="col-md-2 pull-right">
                                            <div class="form-group">
                                               <asp:Button ID="btnApproved" CssClass="btn btn-success" OnClientClick="return confirm('Do you want to Approve Order?')" OnClick="btnApproved_Click" Text="Approve" runat="server" />
                                            </div>

                                        </div>
                                        
                                    </div>
                                    <%--<button type="button" class="btn btn-default" data-dismiss="modal">CLOSE </button>--%>
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
                </div>
            </section>
         </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">

    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    $("[src*=plus]").live("click", function () {
        $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
        $(this).attr("src", "images/minus.png");
    });
    $("[src*=minus]").live("click", function () {
        $(this).attr("src", "images/plus.png");
        $(this).closest("tr").next().remove();
    });


    function ValidatePage() {

        if (typeof (Page_ClientValidate) == 'function') {
            Page_ClientValidate('a');
        }

        if (Page_IsValid) {


            if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Approve") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
</script>--%>

     <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>
   <script type="text/javascript">
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
                       columns: [0, 1, 2, 3, 4, 5, 6, 7]
                   },
                   footer: false,
                   autoPrint: true
               }, {
                   extend: 'excel',
                   filename: 'Ordered Report',
                   text: '<i class="fa fa-file-excel-o"></i> Excel',

                   exportOptions: {
                       columns: [0, 1, 2, 3, 4, 5, 6, 7]
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