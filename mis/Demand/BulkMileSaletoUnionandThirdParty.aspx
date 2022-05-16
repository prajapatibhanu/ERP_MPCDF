<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="BulkMileSaletoUnionandThirdParty.aspx.cs" Inherits="mis_dailyplan_BulkMileSaletoUnionandThirdParty" %>

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

        .table-bordered1 > thead > tr > th, .table-bordered1 > tbody > tr > th, .table-bordered1 > tfoot > tr > th, .table-bordered1 > thead > tr > td, .table-bordered1 > tbody > tr > td, .table-bordered1 > tfoot > tr > td {
            border: 1px solid black !important;
            font-size: 13px;
        }

        .NonPrintable {
            display: none;
        }

        @media print {
            .NonPrintable {
                display: block;
            }

            .noprint {
                display: none;
            }

            @page {
                size: landscape;
            }
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnYes_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="Save" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Search" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content noprint">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">BULK MILK/PRODUCT SALE TO UNION/CC/MDP & THIRD PARTY</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <fieldset>
                        <legend>BULK MILK/PRODUCT SALE TO UNION/CC/MDP & THIRD PARTY DETAIL</legend>
                        <div class="row">

                            <div class="col-md-7">
                                <div class="form-group">
                                    <asp:RadioButtonList ID="rbtnTransferType" runat="server" RepeatDirection="Horizontal" Style="margin-top: 20px;" OnSelectedIndexChanged="rbtnsaleto_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="1" Selected="True">&nbsp;&nbsp;Union To Union&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="2">&nbsp;&nbsp;Union To Third Party&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="3">&nbsp;&nbsp;Union To CC/MDP</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Date<span class="text-danger">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:TextBox ID="txtDate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <div class="form-group">
                                        <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvDugdhSangh" runat="server" Display="Dynamic" ControlToValidate="ddlDS" Text="<i class='fa fa-exclamation-circle' title='Select Dugdh Sangh!'></i>" ErrorMessage="Select Dugdh Sangh" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlDS" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        <small><span id="valddlDS" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3" id="union" runat="server">
                                <div class="form-group">
                                    <div class="form-group">
                                        <label>Union<span class="text-danger">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvddlUnion" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlUnion" Text="<i class='fa fa-exclamation-circle' title='Select Union!'></i>" ErrorMessage="Select Union" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlUnion" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        <small><span id="valddlUnion" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3" id="thirdparty" runat="server">
                                <div class="form-group">
                                    <div class="form-group">
                                        <label>Third Party<span class="text-danger">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvddlThirdparty" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlThirdparty" Text="<i class='fa fa-exclamation-circle' title='Select Third Party!'></i>" ErrorMessage="Select Third Party" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlThirdparty" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        <small><span id="valddlThirdparty" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3" id="MDP" runat="server">
                                <div class="form-group">
                                    <div class="form-group">
                                        <label>CC/Mini Dairy Plant<span class="text-danger">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvddlMDP" runat="server" InitialValue="0" Display="Dynamic" ControlToValidate="ddlMDP" Text="<i class='fa fa-exclamation-circle' title='Select CC/Mini Dairy Plant!'></i>" ErrorMessage="Select CC/MPD" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlMDP" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        <small><span id="valddlMDP" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Gatepass No.</label>
                                    <asp:TextBox ID="txtGatepassNo" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>DM No.</label>
                                    <asp:TextBox ID="txtDMNo" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Shift</label>
                                    <asp:DropDownList ID="ddlshift" runat="server" CssClass="form-control select2"></asp:DropDownList>
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
                                    <div class="form-group">
                                        <label>Item Name<span class="text-danger">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0" Display="Dynamic" ControlToValidate="ddItemName" Text="<i class='fa fa-exclamation-circle' title='Select Item Name!'></i>" ErrorMessage="Select Item Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddItemName" runat="server" CssClass="form-control select2"></asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <div class="form-group">
                                        <label>Supply Unit</label>
                                        <asp:DropDownList ID="ddlSupplyUnitType" onchange="SupplyType(this)" Enabled="false" runat="server" CssClass="form-control select2">
                                            <asp:ListItem Text="KG" Value="2"></asp:ListItem>
                                           <%-- <asp:ListItem Text="Ltr" Value="1"></asp:ListItem>--%>

                                        </asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2" style="display: none">
                                <div class="form-group">
                                    <label>Quantity (In Ltr)<span class="text-danger">*</span></label>
                                    <span class="pull-right">
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtQuantityInLtr" Text="<i class='fa fa-exclamation-circle' title='Enter Quantity(In Ltr)!'></i>" ErrorMessage="Enter Quantity(In Ltr)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                                    </span>
                                    <asp:TextBox ID="txtQuantityInLtr" onblur="GetQtyInKg(this),FatInKg(this),SnfInKg(this)" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <fieldset>
                                <legend>Perticular
                                </legend>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Quantity (In KG)<span class="text-danger">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" Display="Dynamic" ControlToValidate="txtQuantityInKG" Text="<i class='fa fa-exclamation-circle' title='Enter Quantity(In KG)!'></i>" ErrorMessage="Enter Quantity(In KG)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <%-- <asp:TextBox ID="txtQuantityInKG" onblur="GetQtyInLtr(this),FatInKg(this),SnfInKg(this)" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event)"></asp:TextBox>--%>
                                        <asp:TextBox ID="txtQuantityInKG" onblur="CalculateAmount(this)" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>FAT %<span class="text-danger">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="txtFATPer" Text="<i class='fa fa-exclamation-circle' title='Enter FAT % !'></i>" ErrorMessage="Enter FAT %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtFATPer" onblur="FatInKg(this)" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                    </div>
                                </div>
                                <asp:Panel runat="server" ID="PanelMilk">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>SNF %<span class="text-danger">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ControlToValidate="txtSNFPer" Text="<i class='fa fa-exclamation-circle' title='Enter SNF % !'></i>" ErrorMessage="Enter SNF %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox ID="txtSNFPer" onblur="SnfInKg(this)" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>FAT In KG<span class="text-danger">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvFAT" runat="server" Display="Dynamic" ControlToValidate="txtFATInKG" Text="<i class='fa fa-exclamation-circle' title='Enter FAT (In KG)!'></i>" ErrorMessage="Enter FAT (In KG)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox ID="txtFATInKG" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>SNF In KG<span class="text-danger">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvSNF" runat="server" Display="Dynamic" ControlToValidate="txtSNFInKG" Text="<i class='fa fa-exclamation-circle' title='Enter SNF (In KG)!'></i>" ErrorMessage="Enter SNF (In KG)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox ID="txtSNFInKG" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>FAT Rate <span id="Span1" runat="server">Per kg</span><span class="text-danger">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ControlToValidate="txtFATRate" Text="<i class='fa fa-exclamation-circle' title='Enter Rate !'></i>" ErrorMessage="Enter Rate" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox ID="txtFATRate" onblur="CalculateAmount(this)" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>SNF Rate <span id="Span2" runat="server">Per Kg</span><span class="text-danger">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ControlToValidate="txtSNFRate" Text="<i class='fa fa-exclamation-circle' title='Enter Rate !'></i>" ErrorMessage="Enter Rate" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox ID="txtSNFRate" onblur="CalculateAmount(this)" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="Panelproduct" Visible="false">
                                    <div class="col-md-2" style="display: block">
                                        <div class="form-group">
                                            <%--<label>Rate /Kg<span id="pnlrateper" runat="server">Per Ltr</span><span class="text-danger">*</span></label>--%>
                                            <label>Rate /Kg<span id="pnlrateper" runat="server"></span><span class="text-danger">*</span></label>
                                            <span class="pull-right">
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ControlToValidate="txtRate" Text="<i class='fa fa-exclamation-circle' title='Enter Rate !'></i>" ErrorMessage="Enter Rate" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                                            </span>
                                            <asp:TextBox ID="txtRate" onblur="CalculateAmount(this)" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Amount <span class="text-danger">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic" ControlToValidate="txtAmount" Text="<i class='fa fa-exclamation-circle' title='Enter Amount !'></i>" ErrorMessage="Enter Amount" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtAmount" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div class="row">
                            <fieldset>
                                <legend>Tax
                                </legend>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>GST Type</label>
                                        <asp:DropDownList ID="dddlgsttype"  runat="server" CssClass="form-control select2">
                                            <asp:ListItem Text="SGST & CGST" Value="SGST & CGST"></asp:ListItem>
                                            <asp:ListItem Text="IGST" Value="IGST"></asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Total GST %</label>
                                        <asp:TextBox ID="txtGST_Per" runat="server" onblur="CalculateGST(this)" autocomplete="off" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>GST Amount</label>
                                        <asp:TextBox ID="txtGST_Amt" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2" style="display: none">
                                    <div class="form-group">
                                        <label>TCSTAX % </label>
                                        <asp:TextBox ID="txtTCSTAX" onblur="CalculateTCSTAX(this)" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDecThreeplace(this,event)"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2" style="display: none">
                                    <div class="form-group">
                                        <label>TCSTAX Amount</label>
                                        <asp:TextBox ID="txtTCSTAXAmt" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Total Amount</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="Dynamic" ControlToValidate="txtTotalAmount" Text="<i class='fa fa-exclamation-circle' title='Enter Total Amount !'></i>" ErrorMessage="Enter Total Amount" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtTotalAmount" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>Remark</label>
                                    <asp:TextBox ID="txtRemark" runat="server" Rows="3" TextMode="MultiLine" autocomplete="off" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" ValidationGroup="Save" OnClientClick="return ValidatePage();" Text="Save" />
                                    <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-default" />
                                </div>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset>
                        <legend>BULK MILK/PRODUCT SALE DETAIL
                        </legend>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label> From Date<span class="text-danger">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" ControlToValidate="txtSearchDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Search"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtSearchDate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="From Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label> To Date<span class="text-danger">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txttodate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Search"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txttodate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="To Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2" style="margin-top: 20px;">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" ValidationGroup="Search" Text="Search" />
                                <asp:Button runat="server" CssClass="btn btn-success" Visible="false" ID="btnExport" OnClick="btnExport_Click" Text="Export" />
                            </div>
                        </div>
                        <div class="table table-responsive">
                            <asp:GridView ID="GridView1" runat="server" ShowFooter="true" OnRowDataBound="GridView1_RowDataBound"
                                DataKeyNames="BilkMilkSale_Id" class="datatable table table-hover table-bordered table-striped pagination-ys " ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" ClientIDMode="Static" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                <Columns>

                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%# Eval("Remark") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transfer Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date") %>' ToolTip='<%# Eval("Remark") %>'></asp:Label>
                                            <asp:Label ID="lblSaleFromOffice_Id" Visible="false" runat="server" Text='<%# Eval("SaleFromOffice_Id") %>'></asp:Label>
                                            <asp:Label ID="lblSaleToOffice_Id" Visible="false" runat="server" Text='<%# Eval("SaleToOffice_Id") %>'></asp:Label>
                                            <asp:Label ID="lblMilkTrasferType" Visible="false" runat="server" Text='<%# Eval("MilkTrasferType") %>'></asp:Label>
                                            <asp:Label ID="lblItemType_id" Visible="false" runat="server" Text='<%# Eval("ItemType_id") %>'></asp:Label>
                                            <asp:Label ID="lblSupplyTypeInLtrOrKG" Visible="false" runat="server" Text='<%# Eval("SupplyTypeInLtrOrKG") %>'></asp:Label>
                                            <asp:Label ID="lblFAT_Per" Visible="false" runat="server" Text='<%# Eval("FAT_Per") %>'></asp:Label>
                                            <asp:Label ID="lblSNF_Per" Visible="false" runat="server" Text='<%# Eval("SNF_Per") %>'></asp:Label>
                                            <asp:Label ID="lblRate" Visible="false" runat="server" Text='<%# Eval("Rate") %>'></asp:Label>
                                            <asp:Label ID="lblRemark" Visible="false" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                            <asp:Label ID="lblItemCat_id" Visible="false" runat="server" Text='<%# Eval("ItemCat_id") %>'></asp:Label>
                                            <asp:Label ID="lblshift_id" Visible="false" runat="server" Text='<%# Eval("shift_id") %>'></asp:Label>
                                            <asp:Label ID="lblGate_passno" Visible="false" runat="server" Text='<%# Eval("Gate_passno") %>'></asp:Label>
                                            <asp:Label ID="lblDM_No" Visible="false" runat="server" Text='<%# Eval("DM_No") %>'></asp:Label>
                                            <asp:Label ID="lblGST_TYpe" Visible="false" runat="server" Text='<%# Eval("GST_TYpe") %>'></asp:Label>
                                            <asp:Label ID="lblFATRate_KG" Visible="false" runat="server" Text='<%# Eval("FATRate_KG") %>'></asp:Label>
                                            <asp:Label ID="lblSNFRate_KG" Visible="false" runat="server" Text='<%# Eval("SNFRate_KG") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transfer From">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransferfrom" runat="server" Text='<%# Eval("TransferFrom") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transfer To">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransferTo" runat="server" Text='<%# Eval("TransferTo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transfer Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMilkTrasferTypeName" runat="server" Text='<%# Eval("MilkTrasferTypeName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Milk Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemTypeName" runat="server" Text='<%# Eval("ItemTypeName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <b>Total</b>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity In Ltr" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuantityInLtr" runat="server" Text='<%# Eval("QuantityInLtr") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="FlblQtyInLtr" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity In KG">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuantityInKG" runat="server" Text='<%# Eval("QuantityInKG") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="FlblQtyInKG" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FAT In KG">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFAT_InKG" runat="server" Text='<%# Eval("FAT_InKG") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="FlblFATInKG" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SNF In KG">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNF_InKG" runat="server" Text='<%# Eval("SNF_InKG") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="FlblSNFInKG" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRatePer" runat="server" Text='<%# Eval("Rate") + (Eval("Rate").ToString()=="1" ? " Per Ltr" : " Per KG") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="FlblAmount" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GST %">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGST_Per" runat="server" Text='<%# Eval("GST_Per") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GST Amt">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGST_Amt" runat="server" Text='<%# Eval("GST_Amt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="FlblGST_Amt" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TCS TAX %">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTCSTAX_Per" runat="server" Text='<%# Eval("TCSTAX_Per") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TCSTAX Amt">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTCSTAX_Amt" runat="server" Text='<%# Eval("TCSTAX_Amt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="FlblTCSTAX_Amt" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Amt">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalAmount" runat="server" Text='<%# Eval("TotalAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="FlblTAmount" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPrint" runat="server" CssClass="label label-info" CommandName="RecordPrint" CommandArgument='<%# Eval("BilkMilkSale_Id") %>'><i class="fa fa-print"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkDMPrint" Visible="false" runat="server" Text="DM Print" CssClass="label label-info" CommandName="DMPrint" CommandArgument='<%# Eval("BilkMilkSale_Id") %>'></asp:LinkButton>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="label label-success" CommandName="EditRecord" CommandArgument='<%# Eval("BilkMilkSale_Id") %>'><i class="fa fa-pencil"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </fieldset>
                </div>
            </div>
        </section>
        <section class="content">
            <div id="divprint" class="NonPrintable" runat="server"></div>
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
        function validateDecThreeplace(el, evt) {
            var digit = 3;
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (digit == 0 && charCode == 46) {
                return false;
            }

            var number = el.value.split('.');
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            //just one dot (thanks ddlab)
            if (number.length > 1 && charCode == 46) {
                return false;
            }
            //get the carat position
            var caratPos = getSelectionStart(el);
            var dotPos = el.value.indexOf(".");
            if (caratPos > dotPos && dotPos > -1 && (number[1].length > digit - 1)) {
                return false;
            }
            return true;
        }
       <%-- function GetQtyInKg() {
            debugger;
            var QtyInLtr = document.getElementById('<%=txtQuantityInLtr.ClientID%>').value;
            var QtyInKg = 0;
            if (QtyInLtr == "")
                QtyInLtr = 0;
            if (QtyInLtr != "") {
                QtyInKg = parseFloat(QtyInLtr * 1.030).toFixed(2);
                document.getElementById('<%=txtQuantityInKG.ClientID%>').value = QtyInKg;

            }
        }

        function GetQtyInLtr() {
            debugger;
            var QtyInKg = document.getElementById('<%=txtQuantityInKG.ClientID%>').value;
            var QtyInLtr = 0;
            if (QtyInKg == "")
                QtyInKg = 0;
            if (QtyInKg != "") {
                QtyInLtr = parseFloat(QtyInKg / 1.030).toFixed(2);
                document.getElementById('<%=txtQuantityInLtr.ClientID%>').value = QtyInLtr;
            }


        }--%>

        function FatInKg() {
            debugger;
            var QtyInKg = document.getElementById('<%=txtQuantityInKG.ClientID%>').value;
            var FatInKg = 0;
            var FatPer = document.getElementById('<%=txtFATPer.ClientID%>').value;
            if (QtyInKg == "")
                QtyInKg = 0;
            if (FatPer == "")
                FatPer = 0;
            if (QtyInKg != "") {
                FatInKg = parseFloat((QtyInKg / 100) * FatPer).toFixed(2);
                document.getElementById('<%=txtFATInKG.ClientID%>').value = FatInKg;
            }
        }

        function SnfInKg() {
            debugger;
            var QtyInKg = document.getElementById('<%=txtQuantityInKG.ClientID%>').value;
            var SnfInKg = 0;
            var SnfPer = document.getElementById('<%=txtSNFPer.ClientID%>').value;
            if (QtyInKg == "")
                QtyInKg = 0;
            if (SnfPer == "")
                SnfPer = 0;
            if (QtyInKg != "") {
                SnfInKg = parseFloat((QtyInKg / 100) * SnfPer).toFixed(2);
                document.getElementById('<%=txtSNFInKG.ClientID%>').value = SnfInKg;
            }
        }

        function SupplyType() {
            debugger;
            var SupoplyT = document.getElementById('<%=ddlSupplyUnitType.ClientID%>').value;
            if (SupoplyT == "1") {
                document.getElementById('<%=pnlrateper.ClientID%>').innerHTML = "Per Ltr"
            }
            else {
                document.getElementById('<%=pnlrateper.ClientID%>').innerHTML = "Per KG"
            }
        }

        function CalculateAmount() {
            debugger;
            var SupoplyT = document.getElementById('<%=ddlSupplyUnitType.ClientID%>').value;
            var Itemcategory = document.getElementById('<%=ddlItemCategory.ClientID%>').value;
           
            
            var Amount = 0;
            

           <%-- if (SupoplyT == "1") {

                var QtyInLtr = document.getElementById('<%=txtQuantityInLtr.ClientID%>').value;

                if (QtyInLtr == "")
                    QtyInLtr = 0;

                if (Rate != "") {
                    Amount = parseFloat(QtyInLtr * Rate).toFixed(2);
                    document.getElementById('<%=txtAmount.ClientID%>').value = Amount;
                    document.getElementById('<%=txtTotalAmount.ClientID%>').value = Amount;

                }
            }
            else {--%>

                if (Itemcategory == "3") {

                    var QtyInKg = document.getElementById('<%=txtQuantityInKG.ClientID%>').value;

                    if (QtyInKg == "")
                        QtyInKg = 0;

                    var FATRate = document.getElementById('<%=txtFATRate.ClientID%>').value;
                    var SNFRate = document.getElementById('<%=txtSNFRate.ClientID%>').value;
                    var QtyFATInKG = document.getElementById('<%=txtFATInKG.ClientID%>').value;
                    var QtySNFInKG = document.getElementById('<%=txtSNFInKG.ClientID%>').value;
                    var FATAmount = 0
                    var SNFAmount = 0;
                    

                    if (FATRate == "")
                        FATRate = 0;
                    if (SNFRate == "")
                        SNFRate = 0;
                    if (QtyFATInKG == "")
                        QtyFATInKG = 0;
                    if (QtySNFInKG == "")
                        QtySNFInKG = 0;


                    if (QtyFATInKG != "") {
                        FATAmount = parseFloat(QtyFATInKG * FATRate).toFixed(2);

                        if (QtySNFInKG != "") {
                            SNFAmount = parseFloat(QtySNFInKG * SNFRate).toFixed(2);


                            <%--document.getElementById('<%=txtAmount.ClientID%>').value = Amount;
                    document.getElementById('<%=txtTotalAmount.ClientID%>').value = Amount;--%>
                            Amount = parseFloat(FATAmount) + parseFloat(SNFAmount);
                            document.getElementById('<%=txtAmount.ClientID%>').value = Amount;
                            document.getElementById('<%=txtTotalAmount.ClientID%>').value = Amount;

                        }

                    }
                }

                else if (Itemcategory == "2") {
                    debugger;
                    //if (QtyFATInKG != "") {
                    //    FATAmount = parseFloat(QtyFATInKG * FATRate).toFixed(2);

                    //    if (QtySNFInKG != "") {
                    //        SNFAmount = parseFloat(QtySNFInKG * SNFRate).toFixed(2);

                    var PQtyInKg = document.getElementById('<%=txtQuantityInKG.ClientID%>').value;

                    if (PQtyInKg == "")
                        PQtyInKg = 0;
                    var Rate = document.getElementById('<%=txtRate.ClientID%>').value;

                    if (Rate == "")
                        Rate = 0;
                    if (PQtyInKg != "") {
                        Amount = parseFloat(PQtyInKg * Rate).toFixed(2);

                            document.getElementById('<%=txtAmount.ClientID%>').value = Amount;
                            document.getElementById('<%=txtTotalAmount.ClientID%>').value = Amount;
                           <%-- Amount = parseFloat(FATAmount) + parseFloat(SNFAmount);
                            document.getElementById('<%=txtAmount.ClientID%>').value = Amount;
                            document.getElementById('<%=txtTotalAmount.ClientID%>').value = Amount;--%>

                        }

                    }
                //}
            }
        
        function CalculateGST() {
            debugger;
            var Amount = document.getElementById('<%=txtAmount.ClientID%>').value;
            var GST_PER = document.getElementById('<%=txtGST_Per.ClientID%>').value;
            var GSTAmt = 0;
            if (Amount == "")
                Amount = 0;
            if (GST_PER == "")
                GST_PER = 0;

            if (GST_PER != "") {
                GSTAmt = parseFloat((Amount * GST_PER).toFixed(2) / 100).toFixed(2);
                document.getElementById('<%=txtGST_Amt.ClientID%>').value = GSTAmt;
                    var tamt = parseFloat(Amount) + parseFloat(GSTAmt);
                    document.getElementById('<%=txtTotalAmount.ClientID%>').value = tamt;

                }
                else {
                    document.getElementById('<%=txtTCSTAXAmt.ClientID%>').value = "";
                    document.getElementById('<%=txtTotalAmount.ClientID%>').value = Amount;
                }
            }
            function CalculateTCSTAX() {
                debugger;
                var Amount = document.getElementById('<%=txtAmount.ClientID%>').value;
                var GST_Amt = document.getElementById('<%=txtGST_Amt.ClientID%>').value;
                var TCSTAX = document.getElementById('<%=txtTCSTAX.ClientID%>').value;
                var TCSTAXAmt = 0;
                if (Amount == "")
                    Amount = 0;
                if (GST_Amt == "")
                    GST_Amt = 0;
                if (TCSTAX == "")
                    TCSTAX = 0;

                if (TCSTAX != "") {
                    var tmp_amtplusgst = (parseFloat(Amount) + parseFloat(GST_Amt));
                    TCSTAXAmt = parseFloat((parseFloat(tmp_amtplusgst) * TCSTAX).toFixed(2) / 100).toFixed(2);
                    document.getElementById('<%=txtTCSTAXAmt.ClientID%>').value = TCSTAXAmt;
                    var tamt = parseFloat(Amount) + parseFloat(GST_Amt) + parseFloat(TCSTAXAmt);
                    document.getElementById('<%=txtTotalAmount.ClientID%>').value = tamt;

                }
                else {
                    document.getElementById('<%=txtTCSTAXAmt.ClientID%>').value = "";
                    if (GST_Amt == "0") {
                        document.getElementById('<%=txtTotalAmount.ClientID%>').value = Amount;
                    }

                }
            }
            function Print() {
                debugger;
                window.print();
            }

    </script>

</asp:Content>

