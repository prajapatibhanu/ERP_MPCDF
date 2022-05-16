<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DemandAtDock.aspx.cs" Inherits="mis_Demand_DemandAtDock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
  <link href="../Finance/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <style>
        .columngreen {
            background-color: #aee6a3 !important;
        }

        .columnred {
            background-color: #ffb4b4 !important;
        }

        .columnmilk {
            background-color: #bfc7c5 !important;
        }

        .columnproduct {
            background-color: #f5f376 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <%--Confirmation Modal Start --%>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
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
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="c" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="d" ShowMessageBox="true" ShowSummary="false" />
     <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="e" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="row">  
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                          
                <div class="col-md-12">
                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">Milk Or Product Demand </h3>
                        </div>
                        
                        <div class="box-body">
                            <fieldset>
                                <legend>Date ,Shift ,Category</legend>
                              
                                <div class="row">

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Date <span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    ControlToValidate="txtOrderDate" Display="None" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="b"
                                                    ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    ControlToValidate="txtOrderDate" Display="None" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="d"
                                                    ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    ControlToValidate="txtOrderDate" Display="None" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="None" ControlToValidate="txtOrderDate"
                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="b" runat="server" Display="None" ControlToValidate="txtOrderDate"
                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="d" runat="server" Display="None" ControlToValidate="txtOrderDate"
                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                             <%--<asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOrderDate" MaxLength="10" placeholder="Enter Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" data-date-start-date="-d" ClientIDMode="Static"></asp:TextBox>--%>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOrderDate" MaxLength="10" placeholder="Enter Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                           
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Shift</label>
                                             <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                       <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Item Category</label>
                                            <asp:DropDownList ID="ddlItemCategory" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                   
                                      <div class="col-md-2">
                                        <div class="form-group">
                                             <label>DM Type</label>
                                            <asp:DropDownList ID="ddlDMType" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Text="Regular Demand" Value="0"></asp:ListItem>
                                                 <asp:ListItem Text="Current Demand" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                            </div>
                                    </div>
                                     <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Vehicle No<span style="color: red;"> *</span></label>
                                           <span class="pull-right">
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Vehicle" Text="<i class='fa fa-exclamation-circle' title='Select Vehicle !'></i>"
                                                    ControlToValidate="ddlVehicleNo" ForeColor="Red" Display="None" runat="server">
                                                </asp:RequiredFieldValidator>
                                               </span>
                                         <asp:DropDownList ID="ddlVehicleNo" runat="server" CssClass="form-control select2">
                                         </asp:DropDownList>
                                        </div>
                                        </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Location<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Location" Text="<i class='fa fa-exclamation-circle' title='Select Location !'></i>"
                                                    ControlToValidate="ddlLocation" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="b"
                                                    InitialValue="0" ErrorMessage="Select Location" Text="<i class='fa fa-exclamation-circle' title='Select Location !'></i>"
                                                    ControlToValidate="ddlLocation" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                            </span>
                                            <asp:DropDownList ID="ddlLocation" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                     <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Distributor <span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="b"
                                                    InitialValue="0" ErrorMessage="Select Distributor" Text="<i class='fa fa-exclamation-circle' title='Select Retailer !'></i>"
                                                    ControlToValidate="ddlDistributor" ForeColor="Red" Display="None" runat="server">
                                                </asp:RequiredFieldValidator>
                                                </span>
                                            <asp:DropDownList ID="ddlDistributor" OnSelectedIndexChanged="ddlDistributor_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                          </div>
                                         <div class="col-md-2" runat="server" id="divstatus" visible="false">
                                        <div class="form-group">
                                            <label>Pariyojana Adhikari Status <span style="color: red;"> *</span></label>
                                           <br />
                                            <asp:CheckBox ID="chkstatus" runat="server"   Checked="false"/>
                                        </div>
                                    </div>
                                   
                                </div>

                            </fieldset>
                            <div class="row" id="pnlProduct" runat="server" visible="false">
                                <fieldset>
                                        <legend>
                                            <asp:Label ID="lblCartMsg" Text="" runat="server"></asp:Label>
                                        </legend>
                                 <%-- <div class="col-md-4 pull-right">
                                        <div class="form-group">
                                    <asp:LinkButton ID="lnkPreviousOrder" ValidationGroup="d" OnClick="lnkPreviousOrder_Click" CssClass="btn btn-block btn-primary"  runat="server" Text="Previous Demand"></asp:LinkButton>
                                            </div>
                                         </div>--%>
                                <div class="col-md-12">
                                    
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" ShowFooter="true" runat="server" class="table table-hover table-bordered"
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
                                                                <asp:RegularExpressionValidator ID="rev1" runat="server" ControlToValidate="gv_txtQty" ValidationGroup="b" ErrorMessage="Enter Valid Number In Quantity Field" ValidationExpression="^[0-9]*$" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Number In Quantity Field !'></i>" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                                                            </span>
                                                            <asp:TextBox ID="gv_txtQty" onfocusout="FetchData(this)" onpaste="return false;" Text='<%# ddlItemCategory.SelectedValue=="3" ? "0" : null %>' onkeypress="return validateNum(event);" runat="server" autocomplete="off" CssClass="form-control" MaxLength="8" placeholder="Enter Qty"></asp:TextBox>
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
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                   
                                </div>
                                      <div class="col-md-4" id="pnlSubmit" runat="server" visible="false">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="b" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                                    </div>
                                </div>
                               
                                     </fieldset>
                            </div>
                            



                        </div>

                    </div>
                </div>
            </div>
                            <div class="row">
                               <div class="col-md-4" id="pnlClear" runat="server" visible="false">
                                    <div class="form-group">
                                        <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Reset" CssClass="btn btn-primary" />
                                    </div>
                                </div>
                            </div>

            <%--<div class="modal" id="ItemDetailsModalPreDmand" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">×</span></button>
                                    <h4 class="modal-title">Retailer Details for  Date :<span id="modalpreviousDate" style="color: red" runat="server"></span>&nbsp;&nbsp;Item Shift : <span id="modalpreviousShift" style="color: red" runat="server"></span>
                                        &nbsp;&nbsp;Category : <span id="modalPreviousCategory" style="color: red" runat="server"></span><br />
                                    </h4>
                                </div>
                                <div class="modal-body">
                                    <div id="div1" runat="server">
                                        <asp:Label ID="lbldModalMsgPreDemand" runat="server" Text=""></asp:Label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div style="height: 450px; overflow: scroll;">
                                                    <div class="col-md-12">
                                                        <div class="table-responsive">
                                                             <asp:GridView ID="GridViewPreviousDemand" OnRowDataBound="GridViewPreviousDemand_RowDataBound" CssClass="table table-striped table-bordered table-hover"
                                                                  HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
                                                    runat="server" AutoGenerateColumns="false" >
                                                    <Columns>

                                                     <asp:TemplateField HeaderText="" ItemStyle-Width="10" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                                               <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="e" ErrorMessage="Please select at least one record."
                                                                    ClientValidationFunction="Validate" Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Please select at least one record. !'></i>" ForeColor="Red"></asp:CustomValidator>
                                                        </HeaderTemplate>
                                                    </asp:TemplateField>
                                                      
                                                      <asp:TemplateField HeaderText="Retailer Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBandOName" Text='<%#Eval("BandOName") %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblBoothId" Visible="false" Text='<%#Eval("BoothId") %>' runat="server"></asp:Label>
                                                               
                                                                <asp:Label ID="lblRetailerTypeID" Visible="false" Text='<%#Eval("RetailerTypeID") %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblRouteId" Visible="false" Text='<%#Eval("RouteId") %>' runat="server"></asp:Label>
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
                                <div class="modal-footer">
                                    <asp:Button ID="btnsave" CssClass="btn btn-primary" ValidationGroup="e" runat="server" Text="Save" OnClick="btnsave_Click" />
                                </div>
                            </div>
                        </div>
                    </div>--%>
    </section>
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
    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
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

        function myItemDetailsModal() {
            $("#ItemDetailsModal").modal('show');

        }
        function myRetailerListModal() {
            $("#ItemDetailsModalPreDmand").modal('show');

        }
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
        <%--function Validate(sender, args) {
            var gridView = document.getElementById("<%=GridViewPreviousDemand.ClientID %>");
            var checkBoxes = gridView.getElementsByTagName("input");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }--%>

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
           
            if (hfpercrateqty != '0' && hfcratenotissue != '0')
            {

            
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
            else
            {
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
