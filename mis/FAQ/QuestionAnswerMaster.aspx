<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="QuestionAnswerMaster.aspx.cs" Inherits="FAQ_QuestionAnswerMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        th.sorting, th.sorting_asc, th.sorting_desc {
            background: teal !important;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">QUESTION AND ANSWERS Entry</h3>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="box-body">
                    <fieldset>
                        <legend>Question and Answer Entry</legend>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Section<span style="color: red"><b> *</b></span></label>
                                    <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="1">Plant Operation</asp:ListItem>
                                        <asp:ListItem Value="2">Field Operation</asp:ListItem>
                                        <asp:ListItem Value="3">Quality Control</asp:ListItem>
                                        <asp:ListItem Value="4">Marketing</asp:ListItem>
                                        <asp:ListItem Value="5">Others</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSection" runat="server" InitialValue="0" ForeColor="Red" Font-Bold="true" Font-Size="Small" ErrorMessage="Mandatory Field" ValidationGroup="Save" ControlToValidate="ddlSection"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Question<span style="color: red"><b> *</b></span></label>
                                    <asp:TextBox ID="txtQuestion" runat="server" CssClass="form-control" MaxLength="250" placeholder="Question" AutoComplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvQuestion" runat="server" ControlToValidate="txtQuestion" ValidationGroup="Save" ForeColor="Red" Font-Bold="true" Font-Size="Small" ErrorMessage="Reauired Field"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-9">
                                <div class="form-group">
                                    <label>Answer<span style="color: red"><b> *</b></span></label>
                                    <asp:TextBox ID="txtAnswer" runat="server" CssClass="form-control" MaxLength="500" Style="height: 60px;" placeholder="Answer" TextMode="MultiLine" AutoComplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txtAnswer" ValidationGroup="Save" ForeColor="Red" Font-Bold="true" Font-Size="Small" ErrorMessage="Reauired Field"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" ValidationGroup="Save" Text="Save" Style="margin-left: 5px" OnClick="btnSave_Click" />
                                    <a href="QuestionAnswerMaster.aspx" class="btn btn-warning">Reset</a>
                                </div>
                            </div>
                        </div>
                    </fieldset>

                    <fieldset>
                        <legend>Report</legend>
                        <div class="row">
                            <div class="table-responsive">
                                <asp:GridView ID="grdQustionAnswer" runat="server" AutoGenerateColumns="false" DataKeyNames="Qna_ID" CssClass="datatable table table-bordered table-striped" OnSelectedIndexChanged="grdQustionAnswer_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrownumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Qna_ID").ToString()%>'></asp:Label>
                                                <asp:Label ID="lblid" runat="server" Visible="false" Text='<%# Eval("Qna_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Section_Name" HeaderText="Section" />
                                        <asp:BoundField DataField="Question" HeaderText="Question" />
                                        <asp:BoundField DataField="Answer" HeaderText="Answer" />
                                       <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" HeaderText="Active">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chActive" runat="server" OnCheckedChanged="chActive_CheckedChanged" ToolTip='<%# Eval("Qna_ID").ToString()%>' Checked='<%# Eval("Isactive").ToString()=="1" ? true : false %>' AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnSelect" runat="server" CssClass="label label-default" CausesValidation="false" CommandName="Select" Text="Edit"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <link href="../Finance/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../Finance/css/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="../Finance/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="../Finance/js/jquery.dataTables.min.js"></script>
    <script src="../Finance/js/jquery.dataTables.min.js"></script>
    <script src="../Finance/js/dataTables.bootstrap.min.js"></script>
    <script src="../Finance/js/dataTables.buttons.min.js"></script>
    <script src="../Finance/js/buttons.flash.min.js"></script>
    <script src="../Finance/js/jszip.min.js"></script>
    <script src="../Finance/js/pdfmake.min.js"></script>
    <script src="../Finance/js/vfs_fonts.js"></script>
    <script src="../Finance/js/buttons.html5.min.js"></script>
    <script src="../Finance/js/buttons.print.min.js"></script>
    <script src="../Finance/js/buttons.colVis.min.js"></script>
    <script>
        $('.datatable').DataTable({
            paging: true,
            pageLength: 50,
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
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3]
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
    </script>
</asp:Content>

