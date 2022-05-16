<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/mis/MainMaster.master" CodeFile="MilkorProduct_RebatePerLiter_master.aspx.cs" Inherits="mis_Masters_MilkorProduct_RebatePerLiter_master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <script src="https://www.google.com/jsapi" type="text/javascript">
    </script>
    <script type="text/javascript">
        google.load("elements", "1", { packages: "transliteration" });

        function onLoad() {
            var options = {
                //Source Language
                sourceLanguage: google.elements.transliteration.LanguageCode.ENGLISH,
                // Destination language to Transliterate
                destinationLanguage: [google.elements.transliteration.LanguageCode.HINDI],
                shortcutKey: 'ctrl+g',
                transliterationEnabled: true
            };

            var control = new google.elements.transliteration.TransliterationControl(options);
            control.makeTransliteratable(['txtItemNameHindi']);

        }
        google.setOnLoadCallback(onLoad);
    </script>
    <script type="text/javascript">
        function validatename(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122) && charCode != 32) {
                return false;
            }
            return true;
        }
    </script>
    <style>
        .customCSS td {
            padding: 0px !important;
        }

        table {
            white-space: nowrap;
        }

        .capitalize {
            text-transform: capitalize;
        }

        ul.ui-autocomplete.ui-menu.ui-widget.ui-widget-content.ui-corner-all {
            height: 197px !important;
            overflow-y: scroll !important;
            width: 520px !important;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-header">
                    <h3 class="box-title">Set Milk Product Special Rebate</h3>
                </div>
                <div class="box-body">
                    <%--  <table>
                        <tr>
                            
                            <td>

                                <asp:TextBox ID="txtItem" class="form-control ui-autocomplete-12" Rows="10" runat="server" ClientIDMode="Static"></asp:TextBox>
                                <asp:HiddenField ID="hfItemName" runat="server" ClientIDMode="Static" />
                            </td>
                        </tr>
                    </table>--%>
                    <fieldset>
                        <legend>Milk/Product Special Rebate</legend>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Location<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Location" Text="<i class='fa fa-exclamation-circle' title='Select Location !'></i>"
                                                    ControlToValidate="ddlLocation" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>--%>
                                    </span>
                                    <asp:DropDownList ID="ddlLocation" AutoPostBack="true" runat="server" CssClass="form-control select2">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Rebate Amount/Liter<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtRebate_Amount" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Enter Rebate Amount" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ItemGroup" ErrorMessage="Select Item Group" ForeColor="Red" SetFocusOnError="true" InitialValue="Select"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Effective Date<span class="text-danger">*</span></label>

                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtEffectiveDate" Text='<%# Eval("EffectiveDate") %>' onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select Effective from Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                    </div>

                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ItemGroup" ErrorMessage="Select Item Group" ForeColor="Red" SetFocusOnError="true" InitialValue="Select"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="col-md-3" style="display: none">
                                <div class="form-group">
                                    <label>Item Category<span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="ddlItemCategory" runat="server" AutoPostBack="true" CssClass="form-control select2">
                                        <asp:ListItem>Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <small><span id="valItemGroup" class="text-danger"></span></small>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ItemGroup" ErrorMessage="Select Item Group" ForeColor="Red" SetFocusOnError="true" InitialValue="Select"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="col-md-1">
                                <div class="form-group">
                                     <label></label>
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-block btn-success" Text="Submit" ClientIDMode="Static" OnClick="btnSubmit_Click" OnClientClick="return validateform();" />
                                </div>
                            </div>
                            <div class="col-md-1">
                                <div class="form-group">
                                    <label></label>
                                    <a href="MilkorProduct_RebatePerLiter_master.aspx" class="btn btn-block btn-default">Clear</a>
                                </div>
                            </div>
                        </div>
                        


                        <div class="row">
                            
                            <div class="col-md-3"></div>
                        </div>
                    </fieldset>

                </div>
                <div class="box-body">
                    <fieldset>
                        <legend>Milk/Product Special Rebate Detail</legend>

                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="GVrebateDetail" ShowHeaderWhenEmpty="true" EmptyDataText="Record Not Found!" EmptyDataRowStyle-ForeColor="Red" CssClass="datatable table table-hover table-bordered" AutoGenerateColumns="False" DataKeyNames="MilkorProduct_Rebate_Id" runat="server" OnRowCommand="GVrebateDetail_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Location">
                                            <ItemTemplate>
                                                <asp:Label ID="lblloctionid" Visible="false" Text='<%# Eval("AreaName") %>' runat="server" />
                                                <asp:Label ID="lbllocation" Text='<%# Eval("AreaName") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rebate Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrebateamt" Text='<%# Eval("RebatePerLiter_Amt") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Effective Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbleffectivedate" Text='<%# Eval("EffectiveDate") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkViewDetails" CssClass="btn btn-primary btn-primary" CommandName="View" CommandArgument='<%#Eval("MilkorProduct_Rebate_Id") %>' runat="server"><i class="fa fa-eye"></i> View </asp:LinkButton>
                                                <asp:LinkButton ID="lnkUpdate" CssClass="btn btn-primary btn-primary" CommandName="RecordUpdate" CommandArgument='<%#Eval("MilkorProduct_Rebate_Id") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>


                        </div>
                    </fieldset>
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
                                                    <legend>Milk/Product Special Rebate Detail</legend>

                                                    <div class="row" style="height: 250px; overflow: scroll;">
                                                        <div class="col-md-12">
                                                            <div class="table-responsive">
                                                                <asp:GridView ID="GVrebateallDetail" runat="server"  EmptyDataText="No Record Found" class="table table-striped table-bordered" AllowPaging="false"
                                                                    AutoGenerateColumns="False" EnableModelValidation="True" DataKeyNames="MilkorProduct_Rebate_ChildId">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Location">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblloctionid" Visible="false" Text='<%# Eval("AreaName") %>' runat="server" />
                                                                                <asp:Label ID="lbllocation" Text='<%# Eval("AreaName") %>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Rebate Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblrebateamt" Text='<%# Eval("RebatePerLiter_Amt") %>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Effective Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbleffectivedate" Text='<%# Eval("EffectiveDate") %>' runat="server" />
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
                                <%--<div class="modal-footer">
                                                <div class="row">
                                                     <div class="col-md-5"></div>
                                                    <%-- <div class="col-md-2">
                                                        <div class="form-group">
                                                            <asp:Button ID="btnApproved" CssClass="btn-block btn btn-success" ValidationGroup="d" OnClick="btnApproved_Click" Text="Approve" runat="server" />
                                                          
                                                        </div>

                                                    </div>--%>
                                <%--<div class="col-md-2">
                                                        <div class="form-group">
                                                            <asp:Button ID="btnReject" CssClass="btn-block btn btn-danger" OnClientClick="return confirm('Do you want to Reject Order?')" OnClick="btnReject_Click" Text="Reject" runat="server" />
                                                        </div>

                                                    </div>
                                                   

                                                </div>
                                                <%--<button type="button" class="btn btn-default" data-dismiss="modal">CLOSE </button>
                                            </div>--%>
                            </div>
                            <!-- /.modal-content -->
                        </div>
                        <!-- /.modal-dialog -->
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
   <script src="js/buttons.colVis.min.js"></script>
    <asp:Label ID="lblMilkorProduct_Rebate_Id" Visible="false" runat="server" />
    <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
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
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
        $('.datatable').DataTable({
            paging: false


            ,
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
                    title: $('.box-title').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('.box-title').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                    },
                    footer: true
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







        function callalert() {
            debugger;
            $("#OfficeModal").modal('show');
        }
        function validateform() {
            debugger;
            var msg = "";
            $("#valtxtItemName").html("");
            $("#valItemGroup").html("");
            $("#valddlItemCategory").html("");
            $("#valddlUnit").html("");
            $("#valtxtitemaliscode").html("");
            $("#valddlHsnCode").html("");
            $("#valchkOffice").html("");

            if (document.getElementById('<%=txtRebate_Amount.ClientID%>').value.trim() == "") {
                msg += "Enter Rebate Amount. \n"
                $("#valtxtItemName").html("Enter Rebate Amount.");
            }
            if (document.getElementById('<%=txtEffectiveDate.ClientID%>').selectedIndex == 0) {
                msg += "Select Nature. \n"
                $("#valItemGroup").html("Select Effective Date");
            }
            <%--if (document.getElementById('<%=ddlItemCategory.ClientID%>').selectedIndex == 0) {
                msg += "Select Item Group. \n"
                $("#valddlItemCategory").html("Select Item Group");
            }
            if (document.getElementById('<%=ddlUnit.ClientID%>').selectedIndex == 0) {
                msg += "Select Unit \n"
                $("#valddlUnit").html("Select Unit");
            }
            if (document.getElementById('<%=txtItemOrderNo.ClientID%>').value.trim() == "") {
                msg += "Enter Item Order No \n"
                $("#valtxtItemOrderNo").html("Enter Item Order No");
            }
            if (document.getElementById('<%=ddlHsnCode.ClientID%>').selectedIndex == "0") {
                msg += "Select HSN Code \n"
                $("#valddlHsnCode").html("Select HSN Code");
            }--%>



            if (msg != "") {
                alert(msg)
                return false

            }
            else {
                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Submit") {

                    document.querySelector('.popup-wrapper').style.display = 'block';
                    return true

                }
                else if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Update") {

                    document.querySelector('.popup-wrapper').style.display = 'block';
                    return true

                }
            }
        }
        function myItemDetailsModal() {
            $("#ItemDetailsModal").modal('show');

        }
       <%-- function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('b');
            }

            if (Page_IsValid) {
                if (document.getElementById('<%=btnApp.ClientID%>').value.trim() == "Approved") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Approved this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }--%>

    </script>

</asp:Content>


