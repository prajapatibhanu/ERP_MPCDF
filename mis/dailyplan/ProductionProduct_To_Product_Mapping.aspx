<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ProductionProduct_To_Product_Mapping.aspx.cs" Inherits="mis_dailyplan_ProductionProduct_To_Product_Mapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .customCSS td {
            padding: 0px !important;
        }

        .customCSS label {
            padding-left: 10px;
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
                    <h3 class="box-title">Product To Product Mapping</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlDS" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDS_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                <small><span id="valddlDS" class="text-danger"></span></small>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Product Section<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlProductSection" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlProductSection_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                <small><span id="valddlProductSection" class="text-danger"></span></small>
                            </div>
                        </div>

                        <%--<div class="col-md-2">
                            <label>Product Category<span style="color: red;">*</span></label>
                            <span class="pull-right">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="ddlitemcategory" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Item Category!'></i>" ErrorMessage="Select Item Category" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </span>
                            <div class="form-group">
                                <asp:DropDownList ID="ddlitemcategory" OnSelectedIndexChanged="ddlitemcategory_SelectedIndexChanged" OnInit="ddlitemcategory_Init" AutoPostBack="true" CssClass="form-control select2" runat="server"></asp:DropDownList>
                            </div>
                        </div>--%>

                        <div class="col-md-2">
                            <label>Product Name<span style="color: red;">*</span></label>
                            <span class="pull-right">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ControlToValidate="ddlitemtype" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Item Type!'></i>" ErrorMessage="Select Item Type" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </span>
                            <div class="form-group">
                                <asp:DropDownList ID="ddlitemtype" AutoPostBack="true" OnSelectedIndexChanged="ddlitemtype_SelectedIndexChanged" OnInit="ddlitemtype_Init" CssClass="form-control select2" runat="server"></asp:DropDownList>
                            </div>
                        </div>


                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>
                                    <asp:CheckBox ID="chkAllItem" runat="server" Text="ALL Item" onclick="CheckAllItem();" />
                                </legend>
                                <div class="table-responsive">
                                    <asp:CheckBoxList ID="chkItem" Style="padding-left: 10px;" runat="server" ClientIDMode="Static" CssClass="table Item customCSS cbl_all_Office" RepeatColumns="3" RepeatDirection="Vertical" onclick="CheckUnCheckAllItem();">
                                    </asp:CheckBoxList>
                                </div>
                            </fieldset>
                            <small><span id="valchkItem" class="text-danger"></span></small>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-block btn-success" Text="Save" ClientIDMode="Static" OnClick="btnSubmit_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <a href="ProductionProduct_To_Product_Mapping.aspx" class="btn btn-block btn-default">Clear</a>
                            </div>
                        </div>
                        <div class="col-md-3"></div>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="GridView1" runat="server" ShowHeader="true" CssClass="datatable table table-bordered table-condensed" AutoGenerateColumns="false" DataKeyNames="ProductSection_ID" EmptyDataText="No Record Found">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:BoundField DataField="ProductSection_Name" HeaderText="Product Section Name" />
                                    <asp:BoundField DataField="ProductSection_NameHindi" HeaderText="Product Section Name(In Hindi)" />
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="label label-default" CommandName="Select">Edit</asp:LinkButton>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="label label-danger" CommandName="Delete" OnClientClick="return confirm('Record will be deleted. Are you sure want to continue?');">Delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
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
    <%--<link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>--%>
    <script src="../HR/js/jquery.dataTables.min.js"></script>
    <script src="../HR/js/dataTables.bootstrap.min.js"></script>
    <script src="../HR/js/dataTables.buttons.min.js"></script>
    <script src="../HR/js/jszip.min.js"></script>
    <script src="../HR/js/buttons.html5.min.js"></script>
    <script src="../HR/js/buttons.print.min.js"></script>
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

        function CheckAllItem() {
            if (document.getElementById('<%=chkAllItem.ClientID%>').checked == true) {
                $('.Item').each(function () {

                    $(this).closest('table').find('input[type=checkbox]').prop('checked', true);
                });
            }
            else {
                $('.Item').each(function () {
                    $(this).closest('table').find('input[type=checkbox]').prop('checked', false);
                });

            }
            return false;
        }
        function CheckUnCheckAllItem() {

            var chkAllItem = document.getElementById('<%=chkAllItem.ClientID%>');
            chkAllItem.checked = true;

            var sList = "";
            $('.Item').each(function () {
                debugger
                sList += "(" + $(this).val() + (this.checked ? "checked" : "not checked") + ")";
                if (sList == "(not checked)") {
                    chkAllItem.checked = false;

                }

            });

            //Execute loop on all rows excluding the Header row.

        };
        function validateform() {
            debugger;
            var msg = "";
            $("#valddlDS").html("");
            $("#valddlProductSection").html("");
            $("#valchkOffice").html("");
            if (document.getElementById('<%=ddlDS.ClientID%>').selectedIndex == 0) {
                msg += "Select Dugdh Sangh. \n"
                $("#valddlDS").html("Select Dugdh Sangh");
            }
            if (document.getElementById('<%=ddlProductSection.ClientID%>').selectedIndex == 0) {
                msg += "Select Product Section. \n"
                $("#valddlProductSection").html("Select Product Section");
            }
            //if ($('#chkItem input:checked').length == 0) {
            //    msg += "Select atleast one Item. \n"
            //    $("#valchkItem").html("Select atleast one Item");
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

