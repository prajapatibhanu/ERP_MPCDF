<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="EditMilkProductionEntry_frm.aspx.cs" Inherits="mis_dailyplan_EditMilkProductionEntry_frm" %>

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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <%--ConfirmationModal End --%>

    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">

                <div class="box-header">
                    <h3 class="box-title">Edit Daily Disposal Sheet Entry
                        <asp:Label ID="lblvname" Font-Bold="true" runat="server"></asp:Label></h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">

                    <div class="row">
                        <div class="col-sm-12">
                            <ol class="breadcrumb float-sm-right">
                                <li class="breadcrumb-item"><a href="EditMilkProductionEntry.aspx">Go Back</a></li>
                                <li class="breadcrumb-item active">Edit Daily Disposal Sheet Entry</li>
                            </ol>
                        </div>
                    </div>

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
                                <label>Production Section<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlPSection" AutoPostBack="true" OnSelectedIndexChanged="ddlPSection_SelectedIndexChanged" runat="server" CssClass="form-control"></asp:DropDownList>
                                <small><span id="valddlDSPS" class="text-danger"></span></small>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Date</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtDate" onkeypress="javascript: return false;" AutoPostBack="true" OnTextChanged="txtDate_TextChanged" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Shift</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="ddlShift" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Shift!'></i>" ErrorMessage="Select Shift" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlShift" AutoPostBack="true" OnTextChanged="ddlShift_TextChanged" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>


                    </div>

                    <div class="row">

                        <asp:UpdatePanel ID="updatepnl" runat="server">
                            <ContentTemplate>
                                <div class="col-md-12">

                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <section class="content">

                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <asp:Label ID="lblPopupMsg" runat="server"></asp:Label>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <table class="table table-bordered">
                                                            <%-- <tr class="text-center">
                                                    <th colspan="4">
                                                        </th>
                                                </tr>--%>
                                                            <tr>
                                                                <th>Receipt</th>
                                                                <th>Qty.</th>
                                                                <th>Issued</th>
                                                                <th>Qty.</th>
                                                            </tr>
                                                            <tr>
                                                                <td>Opening Balance</td>
                                                                <td>
                                                                    <asp:Label ID="lblopeningBalance" Width="20%" Text="0" runat="server"></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="txtIssuedforstes_Text1" Text="Issued For Sale 1" runat="server"></asp:Label>

                                                                </td>
                                                                <td>
                                                                    <table class="datatable table table-striped table-bordered table-hover">
                                                                        <tr>
                                                                            <td>
                                                                                <div class="col-md-12">
                                                                                    <div class="form-group">
                                                                                        <label>Unit <span style="color: red;">*</span></label>
                                                                                        <asp:DropDownList ID="ddlVariant1" AutoPostBack="true" OnSelectedIndexChanged="ddlVariant1_SelectedIndexChanged" CssClass="form-control" runat="server">
                                                                                            <asp:ListItem Value="1">1 Liter</asp:ListItem>
                                                                                            <asp:ListItem Value="500">500 ML</asp:ListItem>
                                                                                            <asp:ListItem Value="200">200 ML</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                <div class="col-md-12">
                                                                                    <div class="form-group">
                                                                                        <label>Qty. (In Pkt) <span style="color: red;">*</span></label>
                                                                                        <asp:TextBox ID="txtIssuedforstesQtyinPkt1" AutoPostBack="true" OnTextChanged="txtIssuedforstesQtyinPkt1_TextChanged" autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                                </div>

                                                                            </td>
                                                                            <td>
                                                                                <div class="col-md-12">
                                                                                    <div class="form-group">
                                                                                        <label>Qty. (In Ltr) <span style="color: red;">*</span></label>
                                                                                        <asp:TextBox ID="txtIssuedforstes" Enabled="false" autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                                </div>

                                                                            </td>
                                                                        </tr>
                                                                    </table>

                                                                </td>

                                                            </tr>

                                                            <tr>
                                                                <td colspan="2"></td>
                                                                <td>
                                                                    <asp:Label ID="txtIssuedforstes_Text2" Text="Issued For Sale 2" runat="server"></asp:Label>

                                                                </td>
                                                                <td>

                                                                    <table class="datatable table table-striped table-bordered table-hover">
                                                                        <tr>
                                                                            <td>
                                                                                <div class="col-md-12">
                                                                                    <div class="form-group">
                                                                                        <label>Unit <span style="color: red;">*</span></label>
                                                                                        <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="ddlVariant2_SelectedIndexChanged" ID="ddlVariant2" CssClass="form-control" runat="server">
                                                                                            <asp:ListItem Value="1">1 Liter</asp:ListItem>
                                                                                            <asp:ListItem Value="500">500 ML</asp:ListItem>
                                                                                            <asp:ListItem Value="200">200 ML</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                <div class="col-md-12">
                                                                                    <div class="form-group">
                                                                                        <label>Qty. (In Pkt) <span style="color: red;">*</span></label>
                                                                                        <asp:TextBox ID="txtIssuedforstesQtyinPkt2" AutoPostBack="true" OnTextChanged="txtIssuedforstesQtyinPkt2_TextChanged" autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                                </div>

                                                                            </td>
                                                                            <td>
                                                                                <div class="col-md-12">
                                                                                    <div class="form-group">
                                                                                        <label>Qty. (In Ltr) <span style="color: red;">*</span></label>
                                                                                        <asp:TextBox ID="txtIssuedforstes2" Enabled="false" autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                                </div>

                                                                            </td>
                                                                        </tr>
                                                                    </table>


                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td colspan="2"></td>
                                                                <td>
                                                                    <asp:Label ID="txtIssuedforstes_Text3" Text="Issued For Sale 3" runat="server"></asp:Label>

                                                                </td>
                                                                <td>
                                                                    <table class="datatable table table-striped table-bordered table-hover">
                                                                        <tr>
                                                                            <td>
                                                                                <div class="col-md-12">
                                                                                    <div class="form-group">
                                                                                        <label>Unit <span style="color: red;">*</span></label>
                                                                                        <asp:DropDownList ID="ddlVariant3" AutoPostBack="true" OnSelectedIndexChanged="ddlVariant3_SelectedIndexChanged" CssClass="form-control" runat="server">
                                                                                            <asp:ListItem Value="1">1 Liter</asp:ListItem>
                                                                                            <asp:ListItem Value="500">500 ML</asp:ListItem>
                                                                                            <asp:ListItem Value="200">200 ML</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                <div class="col-md-12">
                                                                                    <div class="form-group">
                                                                                        <label>Qty. (In Pkt) <span style="color: red;">*</span></label>
                                                                                        <asp:TextBox ID="txtIssuedforstesQtyinPkt3" autocomplete="off" AutoPostBack="true" OnTextChanged="txtIssuedforstesQtyinPkt3_TextChanged" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                                </div>

                                                                            </td>
                                                                            <td>
                                                                                <div class="col-md-12">
                                                                                    <div class="form-group">
                                                                                        <label>Qty. (In Ltr) <span style="color: red;">*</span></label>
                                                                                        <asp:TextBox ID="txtIssuedforstes3" autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                                </div>

                                                                            </td>
                                                                        </tr>
                                                                    </table>

                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td colspan="2"></td>
                                                                <td>
                                                                    <asp:Label ID="txtIssuedforstes_Text4" Text="Issued For Sale 4" runat="server"></asp:Label>

                                                                </td>
                                                                <td>
                                                                    <table class="datatable table table-striped table-bordered table-hover">
                                                                        <tr>
                                                                            <td>
                                                                                <div class="col-md-12">
                                                                                    <div class="form-group">
                                                                                        <label>Unit <span style="color: red;">*</span></label>
                                                                                        <asp:DropDownList ID="ddlVariant4" AutoPostBack="true" OnSelectedIndexChanged="ddlVariant4_SelectedIndexChanged" CssClass="form-control" runat="server">
                                                                                            <asp:ListItem Value="1">1 Liter</asp:ListItem>
                                                                                            <asp:ListItem Value="500">500 ML</asp:ListItem>
                                                                                            <asp:ListItem Value="200">200 ML</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                <div class="col-md-12">
                                                                                    <div class="form-group">
                                                                                        <label>Qty. (In Pkt) <span style="color: red;">*</span></label>
                                                                                        <asp:TextBox ID="txtIssuedforstesQtyinPkt4" AutoPostBack="true" OnTextChanged="txtIssuedforstesQtyinPkt4_TextChanged" autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                                </div>

                                                                            </td>
                                                                            <td>
                                                                                <div class="col-md-12">
                                                                                    <div class="form-group">
                                                                                        <label>Qty. (In Ltr) <span style="color: red;">*</span></label>
                                                                                        <asp:TextBox ID="txtIssuedforstes4" autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                                </div>

                                                                            </td>
                                                                        </tr>
                                                                    </table>

                                                                </td>
                                                            </tr>



                                                            <tr>


                                                                <td colspan="2">

                                                                    <asp:GridView ID="gvMilkinContainer_opening" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                        EmptyDataText="No Record Found.">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Container Name" HeaderStyle-Width="37%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblV_MCName" runat="server" Text='<%# Eval("V_MCName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Qty.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="txtQtyInKg" Text='<%# Eval("QtyInKg") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                        </Columns>
                                                                    </asp:GridView>

                                                                    <asp:GridView ID="gv_SMTDetails_Opening" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                        EmptyDataText="No Record Found.">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Tanker No" HeaderStyle-Width="37%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblV_VehicleNo" runat="server" Text='<%# Eval("V_VehicleNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Qty. (In Ltr)">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="txtQtyInLtr" CssClass="form-control" Text='<%# Eval("QtyInLtr") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>


                                                                </td>

                                                                <td colspan="2">
                                                                    <fieldset>
                                                                        <legend>Add Tanker</legend>
                                                                        <asp:Label ID="lblmsgTanker" runat="server" Text=""></asp:Label>
                                                                        <table class="datatable table table-striped table-bordered table-hover" runat="server" visible="false" id="FSTanker">
                                                                            <tr>
                                                                                <td>

                                                                                    <div class="col-md-12">
                                                                                        <div class="form-group">
                                                                                            <label>Tanker No <span style="color: red;">*</span></label>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="d" runat="server" Display="Dynamic" ControlToValidate="ddlTankerNo" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Enter Tanker No!'></i>" ErrorMessage="Enter Tanker No." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                            <asp:DropDownList ID="ddlTankerNo" CssClass="form-control select2" runat="server">
                                                                                            </asp:DropDownList>
                                                                                        </div>
                                                                                    </div>



                                                                                </td>
                                                                                <td>

                                                                                    <div class="col-md-12">
                                                                                        <div class="form-group">
                                                                                            <label>Qty. (In Ltr) <span style="color: red;">*</span></label>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="d"
                                                                                                ErrorMessage="Enter Qty. (In Ltr)" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Qty. (In Ltr) !'></i>"
                                                                                                ControlToValidate="txtQtyInLtrTanker" Display="Dynamic" runat="server">
                                                                                            </asp:RequiredFieldValidator>
                                                                                            <asp:TextBox autocomplete="off" runat="server" CssClass="form-control" onkeypress="return validateDec(this,event)" ID="txtQtyInLtrTanker" MaxLength="20" placeholder="Enter Qty. (In Ltr)"></asp:TextBox>

                                                                                        </div>
                                                                                    </div>

                                                                                </td>
                                                                                <td>
                                                                                    <div class="col-md-2">
                                                                                        <div class="form-group">
                                                                                            <asp:Button runat="server" CssClass="btn btn-primary" Style="margin-top: 20px;" OnClick="btnLtrTanker_Click" ValidationGroup="d" ID="btnLtrTanker" Text="Add Tanker" />
                                                                                        </div>
                                                                                    </div>

                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <hr />
                                                                        <asp:GridView ID="gv_SMTDetails" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                            EmptyDataText="No Record Found.">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Tanker No" HeaderStyle-Width="50%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblV_VehicleNo" runat="server" Text='<%# Eval("V_VehicleNo") %>'></asp:Label>
                                                                                        <asp:Label ID="lblI_TankerID" Visible="false" runat="server" Text='<%# Eval("I_TankerID") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Qty. (In Ltr)">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtQtyInLtr" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this,event)" Text='<%# Eval("QtyInLtr") %>' runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Action">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnkDelete" runat="server" ToolTip="DeleteSeal" OnClick="lnkDelete_Click" Style="color: red; margin-top: 20px;" OnClientClick="return confirm('Are you sure to Delete this record?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                            </Columns>
                                                                        </asp:GridView>


                                                                        <asp:GridView ID="gv_SMTDetails1" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                            EmptyDataText="No Record Found.">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Tanker No" HeaderStyle-Width="50%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblV_VehicleNo" runat="server" Text='<%# Eval("V_VehicleNo") %>'></asp:Label>
                                                                                        <asp:Label ID="lblI_TankerID" Visible="false" runat="server" Text='<%# Eval("I_TankerID") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Qty. (In Ltr)">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtQtyInLtr" Enabled="false" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this,event)" Text='<%# Eval("QtyInLtr") %>' runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>


                                                                            </Columns>
                                                                        </asp:GridView>



                                                                    </fieldset>

                                                                </td>
                                                            </tr>

                                                            <tr runat="server" visible="false">
                                                                <td>Received</td>
                                                                <td>
                                                                    <asp:Label ID="lblReceived" Width="50%" Text="0" runat="server"></asp:Label>
                                                                </td>

                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                            </tr>


                                                            <tr>
                                                                <td><span id="spnPreparedtext" runat="server"></span></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPrepared" Width="50%" autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="save"
                                                                        ErrorMessage="Enter Prepared" Text="<i class='fa fa-exclamation-circle' title='Enter Prepared!'></i>"
                                                                        ControlToValidate="txtClosingBalance" ForeColor="Red" Display="Dynamic" runat="server">
                                                                    </asp:RequiredFieldValidator>--%>
                                                                </td>

                                                                <td>Issued For Whole Milk</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtIssuedforWH" Width="50%" autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="save"
                                                                        ErrorMessage="Enter Issued For Whole Milk" Text="<i class='fa fa-exclamation-circle' title='Enter Issued For Whole Milk!'></i>"
                                                                        ControlToValidate="txtClosingBalance" ForeColor="Red" Display="Dynamic" runat="server">
                                                                    </asp:RequiredFieldValidator>--%>
                                                                </td>
                                                            </tr>
															 <tr id="trPrepared" runat="server">
                                                                <td>Prepared for Separation</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPreparedforSeparation" Width="50%" autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="save"
                                                                        ErrorMessage="Enter Prepared" Text="<i class='fa fa-exclamation-circle' title='Enter Prepared!'></i>"
                                                                        ControlToValidate="txtClosingBalance" ForeColor="Red" Display="Dynamic" runat="server">
                                                                    </asp:RequiredFieldValidator>--%>
                                                                </td>

                                                                
                                                            </tr>
                                                            <tr>
                                                                <td>Return</td>
                                                                <td>
                                                                    <asp:TextBox ID="lblReturn" runat="server" autocomplete="off" onkeypress="return validateDec(this,event)" Width="50%"></asp:TextBox>
                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="save"
                                                                        ErrorMessage="Enter Return" Text="<i class='fa fa-exclamation-circle' title='Enter Return!'></i>"
                                                                        ControlToValidate="txtClosingBalance" ForeColor="Red" Display="Dynamic" runat="server">
                                                                    </asp:RequiredFieldValidator>--%>
                                                                </td>

                                                                <td>Issued For Product Section</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtIssuedforProductions" Enabled="false" autocomplete="off" onkeypress="return validateDec(this,event)" Width="50%" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="save"
                                                                        ErrorMessage="Enter Issued For Product Section" Text="<i class='fa fa-exclamation-circle' title='Enter Issued For Product Section!'></i>"
                                                                        ControlToValidate="txtClosingBalance" ForeColor="Red" Display="Dynamic" runat="server">
                                                                    </asp:RequiredFieldValidator>--%>
                                                                </td>

                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td></td>
                                                                <td>Issued For QC</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtLosses" Width="50%" autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="save"
                                                                        ErrorMessage="Enter Losses" Text="<i class='fa fa-exclamation-circle' title='Enter Losses!'></i>"
                                                                        ControlToValidate="txtClosingBalance" ForeColor="Red" Display="Dynamic" runat="server">
                                                                    </asp:RequiredFieldValidator>--%>
                                                                </td>
                                                            </tr>


                                                            <tr>
                                                                <td colspan="2"></td>

                                                                <td colspan="2">

                                                                    <fieldset>
                                                                        <legend>Add Container</legend>
                                                                        <asp:Label ID="lblmsgContainer" runat="server" Text=""></asp:Label>
                                                                        <table class="datatable table table-striped table-bordered table-hover" runat="server" visible="false" id="FSContainer">
                                                                            <tr>
                                                                                <td>

                                                                                    <div class="col-md-12">
                                                                                        <div class="form-group">
                                                                                            <label>Container Name <span style="color: red;">*</span></label>
                                                                                            <asp:RequiredFieldValidator ID="rfvtxtQtyInLtr" ValidationGroup="c" runat="server" Display="Dynamic" ControlToValidate="ddlContainer" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Enter Container Name!'></i>" ErrorMessage="Enter Container Name." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                            <asp:DropDownList ID="ddlContainer" CssClass="form-control" runat="server">
                                                                                            </asp:DropDownList>
                                                                                        </div>
                                                                                    </div>

                                                                                </td>
                                                                                <td>

                                                                                    <div class="col-md-12">
                                                                                        <div class="form-group">
                                                                                            <label>Qty. (In Ltr) <span style="color: red;">*</span></label>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="c"
                                                                                                ErrorMessage="Enter Qty. (In Ltr)" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Qty. (In Ltr) !'></i>"
                                                                                                ControlToValidate="txtQtyInLtr" Display="Dynamic" runat="server">
                                                                                            </asp:RequiredFieldValidator>
                                                                                            <asp:TextBox autocomplete="off" runat="server" CssClass="form-control" onkeypress="return validateDec(this,event)" ID="txtQtyInLtr" MaxLength="20" placeholder="Enter Qty. (In Ltr)"></asp:TextBox>

                                                                                        </div>
                                                                                    </div>

                                                                                </td>
                                                                                <td>
                                                                                    <div class="col-md-2">
                                                                                        <div class="form-group">
                                                                                            <asp:Button runat="server" CssClass="btn btn-primary" Style="margin-top: 20px;" OnClick="btnaddContainer_Click" ValidationGroup="c" ID="btnaddContainer" Text="Add Container" />
                                                                                        </div>
                                                                                    </div>

                                                                                </td>
                                                                            </tr>
                                                                        </table>

                                                                        <hr />

                                                                        <asp:GridView ID="gvMilkinContainer" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                            EmptyDataText="No Record Found.">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Container Name" HeaderStyle-Width="50%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblV_MCName" runat="server" Text='<%# Eval("V_MCName") %>'></asp:Label>
                                                                                        <asp:Label ID="lblI_MCID" Visible="false" runat="server" Text='<%# Eval("I_MCID") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Qty. (In Ltr)">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtQtyInKg" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this,event)" Text='<%# Eval("QtyInKg") %>' runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Action">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnkDelete" runat="server" Style="margin-top: 20px; color: red;" ToolTip="DeleteContainer" OnClick="lnkDelete_Click1" OnClientClick="return confirm('Are you sure to Delete this record?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                            </Columns>
                                                                        </asp:GridView>


                                                                         <asp:GridView ID="gvMilkinContainer1" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                            EmptyDataText="No Record Found.">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Container Name" HeaderStyle-Width="50%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblV_MCName" runat="server" Text='<%# Eval("V_MCName") %>'></asp:Label>
                                                                                        <asp:Label ID="lblI_MCID" Visible="false" runat="server" Text='<%# Eval("I_MCID") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Qty. (In Ltr)">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtQtyInKg" Enabled="false" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this,event)" Text='<%# Eval("QtyInKg") %>' runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                 
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                         
                                                                    </fieldset>



                                                                    <fieldset runat="server">
                                                                        <legend>Variant</legend>
                                                                        <asp:GridView ID="GVVariantDetail" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                            EmptyDataText="No Record Found.">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Variant Name" HeaderStyle-Width="50%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                                        <asp:Label ID="lblItem_id" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                                                        <asp:Label ID="lblPackagingSize" Visible="false" runat="server" Text='<%# Eval("PackagingSize") %>'></asp:Label>
                                                                                        <asp:Label ID="lblUnit_id" Visible="false" runat="server" Text='<%# Eval("Unit_id") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Qty. (In Pkt)">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtQtyInPkt" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this,event)" Text='<%# Eval("QtyInPkt") %>' runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Qty. (In Ltr)">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblQtyInLtr" Text="0" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </fieldset>


                                                                </td>
                                                            </tr>


                                                            <tr>
                                                                <td></td>
                                                                <td></td>
                                                                <td>Closing Balance</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtClosingBalance" Width="50%" autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="save"
                                                                        ErrorMessage="Enter Closing Balance" Text="<i class='fa fa-exclamation-circle' title='Enter Closing Balance!'></i>"
                                                                        ControlToValidate="txtClosingBalance" ForeColor="Red" Display="Dynamic" runat="server">
                                                                    </asp:RequiredFieldValidator>--%>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td>Total</td>
                                                                <td>
                                                                    <asp:Label ID="lblReceiptTotal" Font-Bold="true" runat="server"></asp:Label>
                                                                </td>
                                                                <td>Total</td>
                                                                <td>&nbsp; 
                                                                         <asp:Label ID="lblIssuedtotal" Font-Bold="true" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>

                                                </section>
                                                <!-- /.content -->
                                            </div>
                                        </div>
                                    </div>
                                </div>                             
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        
                    </div>

                </div>
                <div class="modal-footer">
                    <asp:Button ID="btngettotalvarient" OnClick="btngettotalvarient_Click" runat="server" CausesValidation="false" CssClass="btn btn-primary" Text="Get Total" />
                    <asp:Button ID="btnNext" Enabled="false" OnClientClick="return ValidateT()" Visible="false" runat="server" ValidationGroup="a" CssClass="btn btn-primary" Text="Next" OnClick="btnNext_Click"/>
                    <asp:Button ID="btnPopupSave" Enabled="false" OnClientClick="return ValidateT()" Visible="false" runat="server" ValidationGroup="a" CssClass="btn btn-primary" Text="Save" />
                </div>

            </div>

        </section>
    </div>
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="d" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="C" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="v1" runat="server" ValidationGroup="save" ShowMessageBox="true" ShowSummary="false" />

    <div class="modal fade" id="myModalT" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #d9d9d9;">
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                    </button>
                    <h4 class="modal-title" id="myModalLabelT">Confirmation</h4>
                </div>
                <div class="clearfix"></div>
                <div class="modal-body">
                    <p>
                        <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp;
                            <asp:Label ID="lblPopupT" runat="server"></asp:Label>
                    </p>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYesT" OnClick="btnYesT_Click" Style="margin-top: 20px; width: 50px;" />
                    <asp:Button ID="Button2" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script>
        function VarientModelF() {
            $("#VarientModel").modal('show');
        }

        function ValidateT() {
            debugger
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('save');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnPopupSave.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupT.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModalT').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnPopupSave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupT.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModalT').modal('show');
                    return false;
                }
            }
        }

    </script>

</asp:Content>
