﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ItemMaster_Production.aspx.cs" Inherits="mis_Admin_ItemMaster_Production" %>

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
                    <h3 class="box-title">Stock Item Creation</h3>
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
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Item Name<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtItemName" runat="server" placeholder="Item Name" CssClass="form-control capitalize  ui-autocomplete-12" MaxLength="255" ClientIDMode="Static"></asp:TextBox>
                                <asp:HiddenField ID="hfItemName" runat="server" ClientIDMode="Static" />
                                <small><span id="valtxtItemName" class="text-danger"></span></small>
                                <%-- <asp:RequiredFieldValidator ID="rfvitemname" runat="server" ControlToValidate="txtItemName" ErrorMessage="Enter Item Name" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Item Name(In Hindi)<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtItemNameHindi" runat="server" placeholder="Item Name in Hindi" CssClass="form-control" MaxLength="255" ClientIDMode="Static"></asp:TextBox>
                                <small><span id="valtxtItemNameHindi" class="text-danger"></span></small>
                                <%-- <asp:RequiredFieldValidator ID="rfvitemname" runat="server" ControlToValidate="txtItemName" ErrorMessage="Enter Item Name" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Item Code(Alias)</label>
                                <asp:TextBox ID="txtitemaliscode" ToolTip="Stock Keeping Unit" autocomplete="off" runat="server" MaxLength="50" placeholder="Item Code" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                <small><span id="valtxtitemaliscode" class="text-danger"></span></small>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtitemaliscode" ErrorMessage="Enter Item Alias/Code" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Item Category<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ItemGroup" runat="server" AutoPostBack="true" CssClass="form-control select2" OnSelectedIndexChanged="ItemGroup_SelectedIndexChanged">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                                <small><span id="valItemGroup" class="text-danger"></span></small>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ItemGroup" ErrorMessage="Select Item Group" ForeColor="Red" SetFocusOnError="true" InitialValue="Select"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Item Group<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlItemCategory" AutoPostBack="true" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                                <small><span id="valddlItemCategory" class="text-danger"></span></small>
                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlItemCategory" ErrorMessage="Select Item Catagory" ForeColor="Red" SetFocusOnError="true" InitialValue="Select"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div> 
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Item Unit<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control select2">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                                <small><span id="valddlUnit" class="text-danger"></span></small>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlUnit" ErrorMessage="Select Unit" ForeColor="Red" SetFocusOnError="true" InitialValue="Select"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Packaging Mode</label>
                                <asp:DropDownList ID="ddlPackgngMode" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Packaging Size</label>
                                <asp:TextBox ID="txtPackgngSz" runat="server" CssClass="form-control" placeholder="Enter Packaging Size" MaxLength="50">                                   
                                </asp:TextBox>
                            </div>
                        </div>                      
                    </div>                                
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Item Order No</label>
                                <asp:TextBox ID="txtItemOrderNo" runat="server" CssClass="form-control" placeholder="Enter Item Order No" onkeypress="return validateNum(event);">                                   
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-9">
                            <div class="form-group">
                                <label>Item Specification</label>
                                <asp:TextBox ID="txtItemSpecification" autocomplete="off" runat="server" placeholder="Item Specification" CssClass="form-control capitalize" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    
                    <div class="row hidden">
                                    <div class="col-md-12">
                                        <fieldset>
                                            <legend>
                                                <asp:CheckBox ID="chkDistrict" runat="server" Text="ALL Dugdh Sangh" onclick="CheckOfficeAllDistrict();" /></legend>
                                            <div class="table-responsive">
                                                <asp:CheckBoxList ID="chkOffice" runat="server" ClientIDMode="Static" CssClass="table District customCSS cbl_all_Office" RepeatColumns="3" RepeatDirection="Horizontal" onclick="CheckUncheckOfficeAllDistrict();">
                                                </asp:CheckBoxList>
                                            </div>
                                            
                                        </fieldset>
                                        <small><span id="valchkOffice" class="text-danger"></span></small>
                                    </div>
                                </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-block btn-success" Text="Submit" ClientIDMode="Static" OnClick="btnSubmit_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a href="ItemMaster.aspx" class="btn btn-block btn-default">Clear</a>
                            </div>
                        </div>
                        <div class="col-md-3"></div>
                    </div>
                </div>
                <div class="box-body">
                    <div class="col-md-12">
                        <asp:GridView ID="GVItemDetail" ShowHeaderWhenEmpty="true" EmptyDataText="Record Not Found!" EmptyDataRowStyle-ForeColor="Red" CssClass="datatable table table-hover table-bordered" AutoGenerateColumns="False" DataKeyNames="Item_Id" runat="server" OnRowDeleting="GVItemDetail_RowDeleting" OnSelectedIndexChanged="GVItemDetail_SelectedIndexChanged">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="10" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemName" Text='<%# Eval("ItemName") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemTemplate>

                                        <asp:Label ID="lblID" Text='<%# Eval("Item_id").ToString() %>' CssClass="hidden" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="ItemName" HeaderText="Item Name" />--%>
                                <%--<asp:BoundField DataField="ItemCatName" HeaderText="Nature" />--%>
                                <asp:BoundField DataField="ItemTypeName" HeaderText="Item Group" />
                                <asp:BoundField DataField="UnitName" HeaderText="Item Unit" />
                                <asp:BoundField DataField="ItemAliasCode" HeaderText="Item Code(Alias)" />                                
                                <%--  <asp:TemplateField HeaderText="Office Name" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ViewOffice" runat="server" CssClass="label label-default" ToolTip='<%# Eval("Item_Id").ToString() %>' CausesValidation="False" Text="View" OnClick="ViewOffice_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnEdit" runat="server" CssClass="label Aselect1 label-info" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
                                        <asp:LinkButton ID="Delete" Visible="false" runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return getConfirmation();"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>

                        <%--Sow Offices Modal  --%>
                        <%--<div id="myModel" class="modal fade" role="dialog">
                            <div class="modal-dialog modal-sm" role="document">
                                <div class="modal-content">
                                    <div class="modal-header" style="padding: 10px 15px 5px;">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span></button>
                                        <h4 class="modal-title">Item - Office</h4>
                                    </div>
                                    <div class="modal-body">
                                        <div class="row">
                                            
                                        </div>
                                    </div>
                                    <div class="modal-footer" style="padding: 0px; text-align: left; padding-bottom: 10px;">
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                    </div>

                </div>


            </div>

        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
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
    <script>
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
                        columns: [0, 1, 2, 3, 4, 5]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('.box-title').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
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

        function CheckOfficeAllDistrict() {
            if (document.getElementById('<%=chkDistrict.ClientID%>').checked == true) {
                $('.District').each(function () {

                    $(this).closest('table').find('input[type=checkbox]').prop('checked', true);
                });
            }
            else {

                $('.District').each(function () {
                    $(this).closest('table').find('input[type=checkbox]').prop('checked', false);
                });

            }
            return false;
        }




        function CheckUncheckOfficeAllDistrict() {

            //Determine the reference CheckBox in Header row.

            var chkDistrict = document.getElementById('<%=chkDistrict.ClientID%>');


            chkAll.checked = true;
            chkDistrict.checked = true;

            var sList = "";
            $('.District').each(function () {
                debugger
                sList += "(" + $(this).val() + (this.checked ? "checked" : "not checked") + ")";
                if (sList == "(not checked)") {
                    chkAll.checked = false;
                    chkDistrict.checked = false;

                }

            });



            //Execute loop on all rows excluding the Header row.

        };




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

            if (document.getElementById('<%=txtItemName.ClientID%>').value.trim() == "") {
                msg += "Enter Item Name. \n"
                $("#valtxtItemName").html("Enter Item Name");
            }
            if (document.getElementById('<%=ItemGroup.ClientID%>').selectedIndex == 0) {
                msg += "Select Nature. \n"
                $("#valItemGroup").html("Select Nature");
            }
            if (document.getElementById('<%=ddlItemCategory.ClientID%>').selectedIndex == 0) {
                msg += "Select Item Group. \n"
                $("#valddlItemCategory").html("Select Item Group");
            }
            if (document.getElementById('<%=ddlUnit.ClientID%>').selectedIndex == 0) {
                msg += "Select Unit \n"
                $("#valddlUnit").html("Select Unit");
            }

            //if ($('#chkOffice input:checked').length == 0) {
            //    msg += "Select atleast one Office. \n"
            //    $("#valchkOffice").html("Select atleast one Office");
            //}


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

    </script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script>

        <%--$(document).ready(function () {
            debugger;


            $("#<%=txtItemName.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({

                        url: '<%=ResolveUrl("ItemMaster.aspx/SearchCustomers") %>',
                        data: "{ 'ItemName': '" + $('#txtItemName').val() + "'}",
                        //  var param = { ItemName: $('#txtItem').val() };
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            debugger;
                            response($.map(data.d, function (item) {
                                return {
                                    label: item
                                    //val: item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $("#<%=hfItemName.ClientID %>").val(i.item.val);
                },
                minLength: 1

            });

        });--%>

    </script>
</asp:Content>


