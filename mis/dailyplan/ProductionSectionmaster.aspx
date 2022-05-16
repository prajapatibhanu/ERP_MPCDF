<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ProductionSectionmaster.aspx.cs" Inherits="mis_dailyplan_ProductionSectionmaster" %>

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
            control.makeTransliteratable(['txtProdSecNameH']);

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
                    <h3 class="box-title">Production Section Master</h3>
                </div>
                <div class="box-body">
                    <div class="row">

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlDS" runat="server" CssClass="form-control"></asp:DropDownList>
                                <small><span id="valddlDS" class="text-danger"></span></small>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Product Section Name<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtProdSecName" runat="server" placeholder="Product Section Name" CssClass="form-control" MaxLength="255" ClientIDMode="Static"></asp:TextBox>
                                <small><span id="valtxtProdSecName" class="text-danger"></span></small>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Product Section Name(In Hindi)</label>
                                <asp:TextBox ID="txtProdSecNameH" runat="server" placeholder="Product Section Name in Hindi" CssClass="form-control" MaxLength="255" ClientIDMode="Static"></asp:TextBox>
                                <small><span id="valtxtProdSecNameH" class="text-danger"></span></small>
                            </div>
                        </div>
                    </div>
                    <div class="row hidden">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>
                                    <asp:CheckBox ID="chkDugdhSangh" runat="server" Text="ALL Dugdh Sangh" onclick="CheckOfficeAllDugdhSangh();" /></legend>
                                <div class="table-responsive">
                                    <asp:CheckBoxList ID="chkOffice" runat="server" ClientIDMode="Static" CssClass="table DugdhSangh customCSS cbl_all_Office" RepeatColumns="3" RepeatDirection="Horizontal" onclick="CheckUncheckOfficeAllDugdhSangh();">
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
                                <a href="ProductionSectionmaster.aspx" class="btn btn-block btn-default">Clear</a>
                            </div>
                        </div>
                        <div class="col-md-3"></div>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="GridView1" runat="server" CssClass="datatable table table-bordered table-condensed" AutoGenerateColumns="false" DataKeyNames="ProductSection_ID" EmptyDataText="No Record Found" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDeleting="GridView1_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ProductSection_Name" HeaderText="Product Section Name" />
                                    <asp:BoundField DataField="ProductSection_NameHindi" HeaderText="Product Section Name(In Hindi)" />
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="label label-default" CommandName="Select">Edit</asp:LinkButton>
                                            <asp:LinkButton ID="lnkDelete" Visible="false" runat="server" CssClass="label label-danger" CommandName="Delete" OnClientClick="return confirm('Record will be deleted. Are you sure want to continue?');">Delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
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

        function CheckOfficeAllDugdhSangh() {
            if (document.getElementById('<%=chkDugdhSangh.ClientID%>').checked == true) {
                $('.DugdhSangh').each(function () {

                    $(this).closest('table').find('input[type=checkbox]').prop('checked', true);
                });
            }
            else {
                $('.DugdhSangh').each(function () {
                    $(this).closest('table').find('input[type=checkbox]').prop('checked', false);
                });

            }
            return false;
        }
        function CheckUncheckOfficeAllDugdhSangh() {

            var chkDugdhSangh = document.getElementById('<%=chkDugdhSangh.ClientID%>');
            chkDugdhSangh.checked = true;

            var sList = "";
            $('.DugdhSangh').each(function () {
                debugger
                sList += "(" + $(this).val() + (this.checked ? "checked" : "not checked") + ")";
                if (sList == "(not checked)") {
                    chkDugdhSangh.checked = false;

                }

            });

            //Execute loop on all rows excluding the Header row.

        };
        function validateform() {
            debugger;
            var msg = "";
            $("#valtxtProdSecName").html("");
            $("#valchkOffice").html("");
            if (document.getElementById('<%=txtProdSecName.ClientID%>').value.trim() == "") {
                msg += "Enter Product Section Name. \n"
                $("#valtxtProdSecName").html("Enter Product Section Name");
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
</asp:Content>

