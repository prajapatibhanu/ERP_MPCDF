<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CurrentDayMilkDMEntry.aspx.cs" Inherits="mis_Demand_CurrentDayMilkDMEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
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

    <style>
        th.sorting, th.sorting_asc, th.sorting_desc {
            background: #0f62ac !important;
            color: white !important;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
        }

        a.btn.btn-default.buttons-excel.buttons-html5 {
            background: #ff5722c2;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-pdf.buttons-html5 {
            background: #009688c9;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-print {
            background: #e91e639e;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
        }

            a.btn.btn-default.buttons-print:hover, a.btn.btn-default.buttons-pdf.buttons-html5:hover, a.btn.btn-default.buttons-excel.buttons-html5:hover {
                box-shadow: 1px 1px 1px #808080;
            }

            a.btn.btn-default.buttons-print:active, a.btn.btn-default.buttons-pdf.buttons-html5:active, a.btn.btn-default.buttons-excel.buttons-html5:active {
                box-shadow: 1px 1px 1px #808080;
            }

        .box.box-pramod {
            border-top-color: #1ca79a;
        }

        .box {
            min-height: auto;
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
    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="d" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <!-- SELECT2 EXAMPLE -->
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Milk Current Demand</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <fieldset>
                                <legend>Date,Route,Retailer Name
                                </legend>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Date<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    ControlToValidate="txtOrderDate" Display="None" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtOrderDate"
                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                               
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOrderDate" MaxLength="10" placeholder="Enter Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Shift <%--/ शिफ्ट--%><span style="color: red;"> *</span></label>
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
                                            <label>Location <%--/ लोकेशन--%> <span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
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
                                                <label>Route <span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlRoute" InitialValue="0" ErrorMessage="Select Route" Text="<i class='fa fa-exclamation-circle' title='Select Route Sangh !'></i>"></asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlRoute" OnSelectedIndexChanged="ddlRoute_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                          <div class="col-md-2">
                                              <div class="form-group">
                                                <label>Retailer Name <span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                        InitialValue="0" ErrorMessage="Select Retailer Name" Text="<i class='fa fa-exclamation-circle' title='Select Retailer !'></i>"
                                                        ControlToValidate="ddlRetailer" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator></span>
                                                <asp:DropDownList ID="ddlRetailer" AutoPostBack="true" OnSelectedIndexChanged="ddlRetailer_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-2" style="margin-top:20px;">
                                            <div class="form-group">
                                                <asp:LinkButton ID="lnkAddDemand" runat="server" ValidationGroup="a" OnClick="lnkAddDemand_Click" CssClass="btn btn-primary" ClientIDMode="Static" AccessKey="V"><i class="fa fa-plus"></i> Add Demand</asp:LinkButton>
                                            </div>
                                        </div>
                                </div>
                            </fieldset>
                            <div class="row" id="pnlProduct" runat="server" visible="false">
                                <fieldset>
                                    <legend>
                                        <asp:Label ID="lblCartMsg" Text="" runat="server"></asp:Label>
                                    </legend>

                                    <div class="col-md-12">


                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" ShowFooter="true" runat="server" class="table table-hover table-bordered pagination-ys"
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
                                                                <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="c"
                                                                    ErrorMessage="Enter Item Qty." Enabled="false" Text="<i class='fa fa-exclamation-circle' title='Enter Item Qty. !'></i>"
                                                                    ControlToValidate="gv_txtQty" ForeColor="Red" Display="Dynamic" runat="server">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="rev1" runat="server"
                                                                    ControlToValidate="gv_txtQty" ValidationGroup="a"
                                                                    ErrorMessage="Enter Valid Quantity !" ValidationExpression="^[0-9]*$"
                                                                    Text="<i class='fa fa-exclamation-circle' title='Enter Valid Quantity !'></i>"
                                                                    SetFocusOnError="true" ForeColor="Red" Display="None">

                                                                </asp:RegularExpressionValidator>
                                                            </span>
                                                            <asp:TextBox ID="gv_txtQty" onfocusout="FetchData(this)" Text="0" onkeypress="return validateNum(event);" runat="server" onfocus="ppclass" autocomplete="off" CssClass="form-control" MaxLength="8" placeholder="Enter Qty"></asp:TextBox>
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
                                </fieldset>

                            </div>

                            <div class="row">
                                <div class="col-md-2" id="pnlSubmit" runat="server" visible="false">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                                    </div>
                                </div>
                                <div class="col-md-2" id="pnlClear" runat="server" visible="false">
                                    <div class="form-group">
                                        <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Reset" CssClass="btn btn-block btn-primary" />
                                    </div>
                                </div>
                            </div>



                        </div>

                    </div>
                </div>
            </div>
        </section>
        <!-- /.content -->

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
    <script src="js/buttons.colVis.min.js"></script>
    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);

        $(document).ready(function () {
            $('.loader').fadeOut();
        });

        function pp() {

            $('.ppclass').val('');
        }
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
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {


                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }

        //$("#txtOrderDate").datepicker({
        //    autoclose: true ,
        //    startDate: "1d",
        //});

        function myItemDetailsModal() {
            $("#ItemDetailsModal").modal('show');

        }

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

            if (hfpercrateqty != '0' && hfcratenotissue != '0') {


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
            else {
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
