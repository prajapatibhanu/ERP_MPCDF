<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MilkCollectionAdditionDeductionEntry_New.aspx.cs" Inherits="mis_MilkCollection_MilkCollectionAdditionDeductionEntry_New" MaintainScrollPositionOnPostback="true" %>

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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
    
    <div class="content-wrapper">
        <section class="content">
           
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Addition Deduction Entry</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

                        <div class="box-body">
                            <fieldset>
                                <legend>Filter</legend>
                                <asp:Panel ID="Panelfltr" runat="server">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Billing Cycle</label>
                                                <asp:DropDownList ID="ddlBillingCycle" ClientIDMode="Static" runat="server" CssClass="form-control select2" OnTextChanged="ddlBillingCycle_TextChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="5 days">5 days</asp:ListItem>
                                                    <asp:ListItem Value="10 days">10 days</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Date<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                </span>
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">
                                                            <i class="far fa-calendar-alt"></i>
                                                        </span>
                                                    </div>
                                                    <asp:TextBox ID="txtDate" ClientIDMode="Static" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server" OnTextChanged="txtDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Billing Cycle From Date<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic" ControlToValidate="txtBillingCycleToDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                </span>
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">
                                                            <i class="far fa-calendar-alt"></i>
                                                        </span>
                                                    </div>
                                                    <asp:TextBox ID="txtBillingCycleFromDate"  ClientIDMode="Static" Enabled="false" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Billing Cycle To Date<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9"  runat="server" Display="Dynamic" ControlToValidate="txtBillingCycleToDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                </span>
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">
                                                            <i class="far fa-calendar-alt"></i>
                                                        </span>
                                                    </div>
                                                    <asp:TextBox ID="txtBillingCycleToDate" ClientIDMode="Static" Enabled="false" autocomplete="off" CssClass="form-control DateAdd" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2 hidden" >
                                            <div class="form-group">
                                                <label>Entry Type</label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Add"
                                                        InitialValue="0" ErrorMessage="Select Entry Type" Text="<i class='fa fa-exclamation-circle' title='Select Entry Type !'></i>"
                                                        ControlToValidate="ddlEntryType" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator></span>
                                                <asp:DropDownList ID="ddlEntryType" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlEntryType_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem>Route Wise</asp:ListItem>
                                                    <asp:ListItem Selected="True">Chilling Center Wise</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2" id="colroot" runat="server" visible="false">
                                            <div class="form-group">
                                                <label>BMC Root<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvBMCRoot" ValidationGroup="Add"
                                                        InitialValue="0" ErrorMessage="Select BMC Root" Text="<i class='fa fa-exclamation-circle' title='Select BMC Root !'></i>"
                                                        ControlToValidate="ddlBMCTankerRootName" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator></span>
                                                <asp:DropDownList ID="ddlBMCTankerRootName" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlBMCTankerRootName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2" id="colcc" runat="server" visible="false">
                                            <div class="form-group">
                                                <label>CC<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvRoot" ValidationGroup="Add"
                                                        InitialValue="0" ErrorMessage="Select Chilling Center" Text="<i class='fa fa-exclamation-circle' title='Select Chilling Center !'></i>"
                                                        ControlToValidate="ddlCC" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator></span>
                                                <asp:DropDownList ID="ddlCC" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlCC_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Head Type<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Add"
                                                        InitialValue="0" ErrorMessage="Select Head Type" Text="<i class='fa fa-exclamation-circle' title='Select Head Type !'></i>"
                                                        ControlToValidate="ddlHeadType" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator></span>
                                                <asp:DropDownList ID="ddlHeadType" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlHeadType_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="ADDITION">ADDITIONS</asp:ListItem>
                                                    <asp:ListItem Value="DEDUCTION">DEDUCTIONS</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Head Name<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Add"
                                                        InitialValue="0" ErrorMessage="Select Head Name" Text="<i class='fa fa-exclamation-circle' title='Select Head Name !'></i>"
                                                        ControlToValidate="ddlHeaddetails" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator></span>
                                                <asp:DropDownList ID="ddlHeaddetails" OnInit="ddlHeaddetails_Init" Width="150px" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success" Style="margin-top: 20px;" ValidationGroup="Add" Text="Add" OnClick="btnAdd_Click1" />
                                        </div>
                                    </div>
                                </asp:Panel>
                            </fieldset>
                            <div id="divEntry" runat="server" visible="false">
                                <fieldset>
                                    <legend>Fill Details</legend>

                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvDetails" AutoGenerateColumns="false" ShowFooter="true" runat="server" CssClass="table table-bordered">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Society Code" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:RequiredFieldValidator ID="rfvSociety" runat="server" Display="Dynamic"
                                                                    ControlToValidate="txtSociety"
                                                                    Text="<i class='fa fa-exclamation-circle' title='कृपया मान्य सोसायटी कोड दर्ज करें।'></i>"
                                                                    ErrorMessage="कृपया मान्य सोसायटी कोड दर्ज करें।" SetFocusOnError="true" ForeColor="Red"
                                                                    ValidationGroup="a" Enabled="true"></asp:RequiredFieldValidator>
                                                                </span>
                                                                <asp:TextBox ID="txtSociety" CssClass="form-control" runat="server" autocompleteoff="false" OnTextChanged="txtSociety_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                <asp:HiddenField ID="hfOffice_Name" runat="server" ClientIDMode="Static" />
                                                                <asp:Label ID="lblError" runat="server" Style="color: red;" Text=""></asp:Label>
                                                                <%-- <span>
                                                                <asp:RequiredFieldValidator ID="rfvSocietyName" runat="server" Display="Dynamic"
                                                                    ControlToValidate="txtSocietyName"
                                                                    Text="<i class='fa fa-exclamation-circle' title='Please enter valid Society code.!'></i>"
                                                                    ErrorMessage="Please enter valid Society code." SetFocusOnError="true" ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator>
                                                            </span>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Society Name" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtSocietyName" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                <asp:HiddenField ID="hfOffice_ID" runat="server" ClientIDMode="Static" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <span class="pull-right">
                                                                    <asp:RequiredFieldValidator ID="rfvAmount" runat="server" Display="Dynamic"
                                                                        ControlToValidate="txtAmount"
                                                                        Text="<i class='fa fa-exclamation-circle' title='दूध की मात्रा दर्ज करें'></i>"
                                                                        ErrorMessage="दूध की मात्रा दर्ज करें'" SetFocusOnError="true" ForeColor="Red"
                                                                        ValidationGroup="a"></asp:RequiredFieldValidator>
                                                                    <asp:RangeValidator ID="rvAmount" runat="server" ErrorMessage="मात्रा शून्य नहीं होनी चाहिए।" Display="Dynamic" ControlToValidate="txtAmount" Text="<i class='fa fa-exclamation-circle' title='मात्रा शून्य नहीं होनी चाहिए।'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a" Type="Double" MinimumValue="0" MaximumValue="100000000"></asp:RangeValidator>
                                                                </span>
                                                                <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control">
                                                                </asp:TextBox>


                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remark" HeaderStyle-Width="20%">
                                                            <ItemTemplate>

                                                                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control">
                                                                </asp:TextBox>


                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnRemove" runat="server" CausesValidation="false" Text="-" OnClick="btnRemove_Click" CssClass="btn btn-warning" />
                                                            </ItemTemplate>

                                                            <FooterStyle HorizontalAlign="left" />
                                                            <FooterTemplate>
                                                                <asp:Button ID="ButtonAdd" runat="server" CssClass="btn btn-success" ValidationGroup="a" Text="+" OnClick="ButtonAdd_Click" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Button ID="btnGetTotal" runat="server" CssClass="btn btn-success" Text="GetTotal" OnClick="btnGetTotal_Click" />
                                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" />
                                            <a href="MilkCollectionAdditionDeductionEntry_New.aspx" class="btn btn-success">Reset</a>

                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Report</h3>
                        </div>
                        <asp:Label ID="lblRptMsg" runat="server" Text=""></asp:Label>

                        <div class="box-body">
                            <fieldset>
                                <legend>Filter</legend>
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>From Date<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="txtFilterFromDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter From Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="far fa-calendar-alt"></i>
                                                    </span>
                                                </div>
                                                <asp:TextBox ID="txtFilterFromDate" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>To Date<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Display="Dynamic" ControlToValidate="txtFilterToDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter To Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="far fa-calendar-alt"></i>
                                                    </span>
                                                </div>
                                                <asp:TextBox ID="txtFilterToDate" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Entry Type</label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="Search"
                                                    InitialValue="0" ErrorMessage="Select Entry Type" Text="<i class='fa fa-exclamation-circle' title='Select Entry Type !'></i>"
                                                    ControlToValidate="ddlfltEntryType" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlfltEntryType" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlfltEntryType_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem>Route Wise</asp:ListItem>
                                                <asp:ListItem Selected="True">Chilling Center Wise</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2" id="Div1" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>BMC Root<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="Search"
                                                    InitialValue="0" ErrorMessage="Select BMC Root" Text="<i class='fa fa-exclamation-circle' title='Select BMC Root !'></i>"
                                                    ControlToValidate="ddlFltrddlBMCTankerRootName" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlFltrddlBMCTankerRootName" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2" id="Div2" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>CC<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="Search"
                                                    InitialValue="0" ErrorMessage="Select Chilling Center" Text="<i class='fa fa-exclamation-circle' title='Select Chilling Center !'></i>"
                                                    ControlToValidate="ddlFltrCC" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlFltrCC" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <%--<div class="col-md-2">
                                        <div class="form-group">
                                            <label>Head Type<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="Search"
                                                    InitialValue="0" ErrorMessage="Select Head Type" Text="<i class='fa fa-exclamation-circle' title='Select Head Type !'></i>"
                                                    ControlToValidate="ddlFltHeadType" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlFltHeadType" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlFltHeadType_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="ADDITION">ADDITIONS</asp:ListItem>
                                                <asp:ListItem Value="DEDUCTION">DEDUCTIONS</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>--%>
                                    <%--<div class="col-md-2">
                                        <div class="form-group">
                                            <label>Head Name<span style="color: red;"> *</span></label>
                                            <asp:DropDownList ID="ddlFltHeaddetails" Width="150px" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                        </div>
                                    </div>--%>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button ID="btnFltSearch" runat="server" Style="margin-top: 22px;" Text="Search" CssClass="btn btn-success" ValidationGroup="Search" OnClick="btnFltSearch_Click" />
                                        </div>
                                    </div>
                                </div>
								<div class="col-md-12">
                                    <div class="form-group">
                                         <asp:Button ID="btnExport" runat="server" Visible="false" Text="Export to dbf" CssClass="btn btn-primary" OnClick="btnExport_Click"/>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="table-responsive">

                                        <asp:GridView ID="GridView1" ShowFooter="true"  ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" CssClass="datatable
                                             table table-bordered"
                                            AutoGenerateColumns="false" runat="server" OnRowCommand="GridView1_RowCommand">

                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Office Name">
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name")%>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Head Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemBillingHead_Type" runat="server" Text='<%# Eval("ItemBillingHead_Type")%>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Head Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemBillingHead_Name" runat="server" Text='<%# Eval("ItemBillingHead_Name")%>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHeadAmount" runat="server" Text='<%# Eval("HeadAmount")%>'></asp:Label>
                                                        <span class="pull-right">
                                                            <asp:RequiredFieldValidator ID="rfvHeadAmount" ValidationGroup="Update"
                                                                ErrorMessage="Enter Head Amount" Text="<i class='fa fa-exclamation-circle' title='Enter Head Amount !'></i>"
                                                                ControlToValidate="txtHeadAmount" Enabled="false" ForeColor="Red" Display="Dynamic" runat="server">
                                                            </asp:RequiredFieldValidator></span>
                                                        <asp:TextBox ID="txtHeadAmount" Visible="false" Text='<%# Eval("HeadAmount")%>' onkeypress="return validateDec(this,event)" MaxLength="25" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                                        <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="rfvQuantity" ValidationGroup="Update"
                                                            ErrorMessage="Enter Qunatity" Text="<i class='fa fa-exclamation-circle' title='Enter Qunatity !'></i>"
                                                            ControlToValidate="txtQuantity" Enabled="false" ForeColor="Red" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator></span>                                                     
                                                        <asp:TextBox ID="txtQuantity" Visible="false" Text='<%# Eval("Quantity")%>' onkeypress="return validateDec(this,event)" MaxLength="25" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Remark">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("HeadRemark")%>'></asp:Label>
                                                        <%--<span class="pull-right">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                                    ErrorMessage="Enter Quantity" Text="<i class='fa fa-exclamation-circle' title='Enter Quantity !'></i>"
                                                                    ControlToValidate="txtRemark" Enabled="false" ForeColor="Red" Display="Dynamic" runat="server">
                                                                </asp:RequiredFieldValidator></span>  --%>
                                                        <asp:TextBox ID="txtHeadRemark" Visible="false" Text='<%# Eval("HeadRemark")%>' runat="server" CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditRecord" CommandArgument='<%# Eval("AddtionsDeducEntry_ID")%>' Enabled='<%# Eval("Count").ToString()=="0"?true:false%>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkUpdate" runat="server" CausesValidation="false" ValidationGroup="Update" Visible="false" CommandName="UpdateRecord" CommandArgument='<%# Eval("AddtionsDeducEntry_ID")%>' OnClientClick="return confirm('Do you really want to update record?')">Update</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete" CausesValidation="false" runat="server" CommandName="DeleteRecord" CommandArgument='<%# Eval("AddtionsDeducEntry_ID")%>' Visible='<%# Eval("Status").ToString()=="0"?true:false%>' OnClientClick="return confirm('Do you really want to Delete record?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
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
            paging: false,
            
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],
            "bSort": true,
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

                        // javascript: print(),

                        columns: [0, 1, 2, 3, 4, 5, 6]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6]
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

        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
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
        $(document).on('focus', '.select2-selection.select2-selection--single', function (e) {
            $(this).closest(".select2-container").siblings('select:enabled').select2('open');
        });
    </script>

    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
      <script src="../js/jQuery-2.2.0.min.js"></script>
    <script src="../js/bootstrap-datepicker.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $(".Society").autocomplete({

                source: function (request, response) {
                    debugger
                    $.ajax({

                        url: 'MilkCollectionAdditionDeductionEntry_New.aspx/SearchSociety',
                        //data: "{ 'Office_Name': '" + $('#txtSociety').val() + "'}",
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
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
                    debugger
                    // $(this).children("td").eq(1).find('input[type=hidden]').val(i.item.val);
                    $(this).parent().find("input[type=hidden]").val(i.item.val);


                },
                minLength: 1
                //}).focus(function () {
                //    $(this).autocomplete("search");
            });
        });
      
      
        <%--function BillingCycleDate() {
            debugger;
            var SelectedFromDate;
            var SelectedToDate;
            var EntryDate = document.getElementById('<%= txtDate.ClientID%>').value;
            
            var BillingCycle = document.getElementById('<%= ddlBillingCycle.ClientID%>').value;
            var arr1 = EntryDate.split('/');
            var day = arr1[0];
            var Month = arr1[1];
            var Year = arr1[2];
            var d = new Date(day, Month, Year);
            if (BillingCycle == "5 days") {
                if (day >= 1 && day < 6) {
                    SelectedFromDate = 01;
                    SelectedToDate = 05
                }
                else if (day > 5 && day < 11) {
                    SelectedFromDate = 6;
                    SelectedToDate = 10
                }
                else if (day > 10 && day < 16) {
                    SelectedFromDate = 11;
                    SelectedToDate = 15
                }
                else if (day > 15 && day < 21) {
                    SelectedFromDate = 16;
                    SelectedToDate = 20
                }
                else if (day > 20 && day < 26) {
                    SelectedFromDate = 21;
                    SelectedToDate = 25
                }
                else if (day > 25 && day <= 31) {
                    SelectedFromDate = 26;
                    if (Month == 1 || Month == 3 || Month == 5 || Month == 7 || Month == 8 || Month == 10 || Month == 12) {
                        SelectedToDate = 31
                    }
                    else if (Month == 2) {
                        SelectedToDate = 28
                    }
                    else {
                        SelectedToDate = 30
                    }

                }
                SelectedFromDate = SelectedFromDate + "/" + Month + '/' + Year;
                SelectedToDate = SelectedToDate + "/" + Month + '/' + Year;

            }
            else {
                if (day >= 1 && day < 11) {
                    SelectedFromDate = 01;
                    SelectedToDate = 10
                }
                else if (day > 10 && day < 21) {
                    SelectedFromDate = 11;
                    SelectedToDate = 20
                }
                else if (day > 20 && day <= 31) {
                    SelectedFromDate = 21;
                    if (Month == 1 || Month == 3 || Month == 5 || Month == 7 || Month == 8 || Month == 10 || Month == 12) {
                        SelectedToDate = 31
                    }
                    else if (Month == 2) {
                        SelectedToDate = 28
                    }
                    else {
                        SelectedToDate = 30
                    }
                }

                SelectedFromDate = SelectedFromDate + "/" + Month + '/' + Year;
                SelectedToDate = SelectedToDate + "/" + Month + '/' + Year;
            }
            
            //$("#txtBillingCycleFromDate").datepicker({ dateFormat: "dd/mm/yy" });
            $("#txtBillingCycleFromDate").datepicker("setDate", SelectedFromDate);
            //$("#txtBillingCycleToDate").datepicker({ dateFormat: "dd/mm/yy" });
            $("#txtBillingCycleToDate").datepicker("setDate", SelectedToDate);
           
        }--%>
    </script>
</asp:Content>

