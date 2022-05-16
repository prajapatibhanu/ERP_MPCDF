<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="LooseProductProductionEntry.aspx.cs" Inherits="mis_dailyplan_LooseProductProductionEntry" %>

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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSave_Click"  Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="Save" ShowMessageBox="true" ShowSummary="false" />
        <div class="content-wrapper">
            <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                          <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-header">
                            <h3 class="box-title">Loose Product Production Entry</h3>
                          
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Loose product Production</legend>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Date<span class="text-danger">*</span></label>
                                        <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtDate" onkeypress="javascript: return false;"  Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static" OnTextChanged="txtDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                             <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvDugdhSangh" runat="server" Display="Dynamic" ControlToValidate="ddlDS" Text="<i class='fa fa-exclamation-circle' title='Select Dugdh Sangh!'></i>" ErrorMessage="Select Dugdh Sangh" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                            <asp:DropDownList ID="ddlDS" runat="server" CssClass="form-control"></asp:DropDownList>
                                            <small><span id="valddlDS" class="text-danger"></span></small>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="col-md-2">
                            <div class="form-group">
                                <label>Production Section<span class="text-danger">*</span></label>
                                 <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvProductionSection" runat="server" Display="Dynamic" ControlToValidate="ddlPSection" Text="<i class='fa fa-exclamation-circle' title='Select Production Section!'></i>" ErrorMessage="Select Production Section" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save" InitialValue="0"></asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlPSection" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPSection_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                <small><span id="valddlDSPS" class="text-danger"></span></small>
                            </div>
                        </div>
                                <div class="col-md-3">
                            <div class="form-group">
                                <label>Product<span class="text-danger">*</span></label>
                                 <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvVariant" runat="server" Display="Dynamic" ControlToValidate="ddlVariant" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Product!'></i>" ErrorMessage="Select Product" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlVariant" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                <small><span id="valrfvVariant" class="text-danger"></span></small>
                            </div>
                        </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Quantity<span class="text-danger">*</span></label>
                                        <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" Display="Dynamic" ControlToValidate="txtQuantity" Text="<i class='fa fa-exclamation-circle' title='Enter Quantity!'></i>" ErrorMessage="Enter Quantity" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                        <asp:TextBox ID="txtQuantity" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                    </div>
                                </div>
                                
                                <div class="col-md-2">
                            <div class="form-group">
                                <label>Unit<span class="text-danger">*</span></label>
                                 <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvddlUnit" runat="server" Display="Dynamic" ControlToValidate="ddlUnit" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Unit!'></i>" ErrorMessage="Select Unit" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                <small><span id="valddlUnit" class="text-danger"></span></small>
                            </div>
                        </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>BatchNo</label>
                                        <asp:TextBox ID="txtBatchNo" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>LotNo</label>
                                        <asp:TextBox ID="txtLotNo" autocomplete="off" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Remark</label>
                                        <asp:TextBox ID="txtRemark" autocomplete="off" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" ValidationGroup="Save" OnClientClick="return ValidatePage();"  Text="Save"/>
                                    </div>
                                </div>
                        </div>
                                </fieldset>
                            <fieldset>
                                <legend>LOOSE PRODUCT PRODUCTION DETAIL</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                        <asp:GridView ID="Gridview1" PageSize="50" AllowPaging="True" runat="server" DataKeyNames="LoosePP_Id" CssClass="table table-bordered table-condensed" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" OnRowCommand="Gridview1_RowCommand" OnPageIndexChanging="Gridview1_PageIndexChanging">
                                            <Columns>
                                                <asp:BoundField HeaderText="Date" DataField="Date" />
                                                <asp:BoundField HeaderText="Product" DataField="Product" />
                                                <asp:BoundField HeaderText="Quantity" DataField="Quantity" />
                                                <asp:BoundField HeaderText="Batch No" DataField="Batch_No" />
                                                <asp:BoundField HeaderText="Lot No" DataField="Lot_No" />
                                                <asp:BoundField HeaderText="Remark" DataField="Remark" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="label label-default" Text="Edit" CommandName="EditDetail" CommandArgument='<%# Eval("LoosePP_Id") %>'></asp:LinkButton>
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
                 </section>
            </div>
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('Save');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
                        document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                        $('#myModal').modal('show');
                        return false;
                    }
                    if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                        document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
        </script>
</asp:Content>

