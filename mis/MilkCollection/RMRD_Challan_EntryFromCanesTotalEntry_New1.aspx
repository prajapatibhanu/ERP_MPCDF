<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RMRD_Challan_EntryFromCanesTotalEntry_New1.aspx.cs" Inherits="mis_MilkCollection_RMRD_Challan_EntryFromCanesTotalEntry_New1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../Finance/css/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="../Finance/css/dataTables.bootstrap.min.css" rel="stylesheet" />
     <link href="../Finance/css/buttons.dataTables.min.css" rel="stylesheet" />
    
    <style>
        .customCSS td {
            padding: 0px !important;
        }

        ul.ui-autocomplete.ui-menu.ui-widget.ui-widget-content.ui-corner-all {
            height: 197px !important;
            overflow-y: scroll !important;
            width: 520px !important;
        }

        .capitalize {
            text-transform: capitalize;
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
   <%--  <div class="loader"></div>--%>
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" OnClick="btnSave_Click" ID="btnYes" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
            <div class="row">
                <div class="col-md-12">

                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Truck Sheet</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>CC Detail</legend>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Date</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <i class="far fa-calendar-alt"></i>
                                                </span>
                                            </div>
                                            <asp:TextBox ID="txtDate" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>


                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Shift<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvShift" runat="server" Display="Dynamic" OnInit="rfvShift_Init" InitialValue="0" ControlToValidate="ddlShift" Text="<i class='fa fa-exclamation-circle' title='Select Shift!'></i>" ErrorMessage="Select Shift" ValidationGroup="a" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="Morning">Morning</asp:ListItem>
                                            <asp:ListItem Value="Evening">Evening</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>CC<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvRoot" ValidationGroup="Save"
                                                InitialValue="0" ErrorMessage="Select Chilling Center" Text="<i class='fa fa-exclamation-circle' title='Select Chilling Center !'></i>"
                                                ControlToValidate="ddlCC" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlCC" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                                <%--<div class="col-md-2">
                                    <div class="form-group">
                                        <label>BMC Root<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvRoot" ValidationGroup="Save"
                                                InitialValue="0" ErrorMessage="Select BMC Root" Text="<i class='fa fa-exclamation-circle' title='Select BMC Root !'></i>"
                                                ControlToValidate="ddlBMCTankerRootName" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlBMCTankerRootName"  runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>--%>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success" ValidationGroup="Save" Style="margin-top: 20px;" Text="Add" OnClick="btnAdd_Click" />
                                    </div>
                                </div>
                                <%--<div class="col-md-2">
                                    <label>Milk Collection Unit<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ValidationGroup="a" ControlToValidate="ddlMilkCollectionUnit" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Milk Collection Unit!'></i>" ErrorMessage="Select Milk Collection Unit" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlMilkCollectionUnit" AutoPostBack="true" OnSelectedIndexChanged="ddlMilkCollectionUnit_SelectedIndexChanged" CssClass="form-control select2" runat="server">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="5">BMC</asp:ListItem>
                                            <asp:ListItem Value="6">DCS</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>--%>
                            </fieldset>
                            <asp:Label ID="lblErrMsg" runat="server" Text=""></asp:Label>
                            <div id="divDetails" runat="server" visible="false">
                                <fieldset>
                                    <legend>Milk Collection-Add<br />Note:<br />1.मात्रा शून्य नहीं होनी चाहिए।<br />2.(F.A.T %) 3 से 12 के बीच होना चाहिए।<br />3.(C.L.R) 24 से 32 के बीच होना चाहिए। </legend>
                                    <div class="col-md-12 pull-right">
                                        <div class="form-group">
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvBMCMilkDetails" ShowFooter="true" runat="server" CssClass="table table-bordered" Width="100%" AutoGenerateColumns="false" OnRowCreated="gvBMCMilkDetails_RowCreated" OnRowCommand="gvBMCMilkDetails_RowCommand">
                                                <HeaderStyle BackColor="Tan" Font-Bold="True" ForeColor="Black" HorizontalAlign="Center" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderText="Society">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSociety" style="width:15px;" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                                <asp:Label ID="lblOffice_ID" CssClass="hidden" runat="server" Text='<%# Eval("Office_ID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Society Code" HeaderStyle-Width="200px">
                                                        <ItemTemplate>
                                                            <asp:RequiredFieldValidator ID="rfvSociety" runat="server" Display="Dynamic"
                                                                ControlToValidate="txtSociety"
                                                                Text="<i class='fa fa-exclamation-circle' title='कृपया मान्य सोसायटी कोड दर्ज करें।'></i>"
                                                                ErrorMessage="कृपया मान्य सोसायटी कोड दर्ज करें।" SetFocusOnError="true" ForeColor="Red"
                                                                ValidationGroup="a" Enabled="true"></asp:RequiredFieldValidator>
                                                            </span>
                                                                <asp:TextBox ID="txtSociety" CssClass="form-control" runat="server" autocompleteoff="false" OnTextChanged="txtSociety_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            <asp:HiddenField ID="hfOffice_Name" runat="server" ClientIDMode="Static" />
                                                            <asp:Label id="lblError" runat="server" style="color: red;" Text=""></asp:Label>
                                                           <%-- <span>
                                                                <asp:RequiredFieldValidator ID="rfvSocietyName" runat="server" Display="Dynamic"
                                                                    ControlToValidate="txtSocietyName"
                                                                    Text="<i class='fa fa-exclamation-circle' title='Please enter valid Society code.!'></i>"
                                                                    ErrorMessage="Please enter valid Society code." SetFocusOnError="true" ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator>
                                                            </span>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Society Name" HeaderStyle-Width="200px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtSocietyName" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            <asp:HiddenField ID="hfOffice_ID" runat="server" ClientIDMode="Static" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quality" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <span class="pull-right">
                                                                <asp:RequiredFieldValidator ID="rfvtBufMilkQuality" runat="server" Display="Dynamic"
                                                                    ControlToValidate="ddlBufMilkQuality"
                                                                    Text="<i class='fa fa-exclamation-circle' title='Select Quality!'></i>"
                                                                    ErrorMessage="Select Milk Quality" SetFocusOnError="true" ForeColor="Red"
                                                                    ValidationGroup="a" Enabled="false"></asp:RequiredFieldValidator>
                                                            </span>
                                                            <asp:DropDownList ID="ddlBufMilkQuality" ClientIDMode="Static" runat="server" CssClass="form-control">
                                                                <asp:ListItem>Good</asp:ListItem>
                                                                <asp:ListItem>Sour</asp:ListItem>
                                                                <asp:ListItem>Curd</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <span class="pull-right">
                                                                <asp:RequiredFieldValidator ID="rfvBufMilkQuantity" runat="server" Display="Dynamic"
                                                                    ControlToValidate="txtBufMilkQuantity"
                                                                    Text="<i class='fa fa-exclamation-circle' title='दूध की मात्रा दर्ज करें'></i>"
                                                                    ErrorMessage="दूध की मात्रा दर्ज करें'" SetFocusOnError="true" ForeColor="Red"
                                                                    ValidationGroup="a" Enabled="false"></asp:RequiredFieldValidator>
                                                              <asp:RangeValidator ID="rvBufMilkQuantity" runat="server" ErrorMessage="मात्रा शून्य नहीं होनी चाहिए।" Display="Dynamic" ControlToValidate="txtBufMilkQuantity" Text="<i class='fa fa-exclamation-circle' title='मात्रा शून्य नहीं होनी चाहिए।'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a" Type="Double" MinimumValue="1" MaximumValue="10000"></asp:RangeValidator>
                                                            </span>
                                                            <asp:TextBox ID="txtBufMilkQuantity" onkeypress="return validateDec(this,event);" Enabled="false" runat="server" CssClass="form-control" OnTextChanged="txtBufMilkQuantity_TextChanged" AutoPostBack="true">
                                                            </asp:TextBox>
                                                            
                                                             <asp:Label id="lblBufMilkQuantity" runat="server" style="color: red;" visible="false">मात्रा शून्य नहीं होनी चाहिए।</asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Fat(%)<br/>(5.6 से 12)">
                                                        <ItemTemplate>
                                                            <span class="pull-right">
                                                                <asp:RequiredFieldValidator ID="rfvBufFat" runat="server" Display="Dynamic"
                                                                    ControlToValidate="txtBufFat"
                                                                    Text="<i class='fa fa-exclamation-circle' title='Fat दर्ज करें!'></i>"
                                                                    ErrorMessage="Fat दर्ज करें!" SetFocusOnError="true" ForeColor="Red"
                                                                    ValidationGroup="a" Enabled="false"></asp:RequiredFieldValidator>
                                                                <%--<asp:RangeValidator ID="rvBufFat" runat="server" ErrorMessage="Minimum FAT % required 5.6 and maximum 11.0." Display="Dynamic" ControlToValidate="txtBufFat" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 5.6 and maximum 11.0!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a" Type="Double" MinimumValue="5.6" MaximumValue="11.0"></asp:RangeValidator>--%>
                                                                 <asp:RangeValidator ID="rvBufFat" runat="server" ErrorMessage="(F.A.T %) 5.6 से 12 के बीच होना चाहिए।" Display="Dynamic" ControlToValidate="txtBufFat" Text="<i class='fa fa-exclamation-circle' title='(F.A.T %) 5.6 से 12 के बीच होना चाहिए।'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a" Type="Double" MinimumValue="5.6" MaximumValue="12.0"></asp:RangeValidator>

                                                            </span>
                                                            <asp:TextBox ID="txtBufFat" onkeypress="return validateDec(this,event);" Enabled="false" runat="server" CssClass="form-control">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="C.L.R<br/>(15 से 32)">
                                                        <ItemTemplate>
                                                            <span class="pull-right">
                                                                <asp:RequiredFieldValidator ID="rfvBufCLR" runat="server" Display="Dynamic"
                                                                    ControlToValidate="txtBufCLR"
                                                                    Text="<i class='fa fa-exclamation-circle' title='CLR दर्ज करें!'></i>"
                                                                    ErrorMessage="CLR दर्ज करें!" SetFocusOnError="true" ForeColor="Red"
                                                                    ValidationGroup="a" Enabled="false"></asp:RequiredFieldValidator>
                                                                <asp:RangeValidator ID="rvBufCLR" runat="server" ErrorMessage="(C.L.R) 15 से 32 के बीच होना चाहिए।" Display="Dynamic" ControlToValidate="txtBufCLR" Text="<i class='fa fa-exclamation-circle' title='(C.L.R) 15 से 32 के बीच होना चाहिए।'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a" Type="Double" MinimumValue="15" MaximumValue="32"></asp:RangeValidator>
                                                            </span>
                                                            <asp:TextBox ID="txtBufCLR" onkeypress="return validateDec(this,event);" Enabled="false" runat="server" CssClass="form-control">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <%--  <asp:TemplateField HeaderText="Total&nbsp;&nbsp; Can">
                                                            <ItemTemplate>                                                             
                                                                <asp:TextBox ID="txtBuftotalCan" runat="server" CssClass="form-control">
                                                
                                                                </asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Good&nbsp;&nbsp; Can">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtBufGoodCan" runat="server" CssClass="form-control">
                                                
                                                                </asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderText="Quality" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <span class="pull-right">
                                                                <asp:RequiredFieldValidator ID="rfvtCowMilkQuality" runat="server" Display="Dynamic"
                                                                    ControlToValidate="ddlCowMilkQuality"
                                                                    Text="<i class='fa fa-exclamation-circle' title='Select Quality!'></i>"
                                                                    ErrorMessage="Select Milk Quality" SetFocusOnError="true" ForeColor="Red"
                                                                    ValidationGroup="a" Enabled="false"></asp:RequiredFieldValidator>
                                                            </span>
                                                            <asp:DropDownList ID="ddlCowMilkQuality" ClientIDMode="Static" runat="server" CssClass="form-control">

                                                                <asp:ListItem Selected="True">Good</asp:ListItem>
                                                                <asp:ListItem>Sour</asp:ListItem>
                                                                <asp:ListItem>Curd</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <span class="pull-right">
                                                                <asp:RequiredFieldValidator ID="rfvCowMilkQuantity" runat="server" Display="Dynamic"
                                                                    ControlToValidate="txtCowMilkQuantity"
                                                                    Text="<i class='fa fa-exclamation-circle' title='दूध की मात्रा दर्ज करें!'></i>"
                                                                    ErrorMessage="दूध की मात्रा दर्ज करें'" SetFocusOnError="true" ForeColor="Red"
                                                                    ValidationGroup="a" Enabled="false" ></asp:RequiredFieldValidator>
                                                             <asp:RangeValidator ID="rvCowMilkQuantity" runat="server" ErrorMessage="मात्रा शून्य नहीं होनी चाहिए।" Display="Dynamic" ControlToValidate="txtCowMilkQuantity" Text="<i class='fa fa-exclamation-circle' title='मात्रा शून्य नहीं होनी चाहिए।'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a" Type="Double" MinimumValue="1" MaximumValue="10000"></asp:RangeValidator>
                                                            </span>
                                                            <asp:TextBox ID="txtCowMilkQuantity" onkeypress="return validateDec(this,event);" Enabled="false" runat="server" CssClass="form-control" OnTextChanged="txtCowMilkQuantity_TextChanged" AutoPostBack="true">
                                                            </asp:TextBox>
                                                            <asp:Label id="lblCowMilkQuantity" runat="server" style="color: red;" visible="false">मात्रा शून्य नहीं होनी चाहिए।</asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fat(%)<br/>(1.5 से 5.5)">
                                                        <ItemTemplate>
                                                            <span class="pull-right">
                                                                <asp:RequiredFieldValidator ID="rfvCowFat" runat="server" Display="Dynamic"
                                                                    ControlToValidate="txtCowFat"
                                                                    Text="<i class='fa fa-exclamation-circle' title='Fat दर्ज करें!'></i>"
                                                                    ErrorMessage="Fat दर्ज करें!" SetFocusOnError="true" ForeColor="Red"
                                                                    ValidationGroup="a" Enabled="false"></asp:RequiredFieldValidator>
                                                                <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="(F.A.T%) 1.5 से 5.5 के बीच होना चाहिए।" Display="Dynamic" ControlToValidate="txtCowFat" Text="<i class='fa fa-exclamation-circle' title='(F.A.T %) 1.5 से 5.5 के बीच होना चाहिए।'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a" Type="Double" MinimumValue="1.5" MaximumValue="5.5"></asp:RangeValidator>
                                                            </span>
                                                            <asp:TextBox ID="txtCowFat" Enabled="false" runat="server" onkeypress="return validateDec(this,event);" CssClass="form-control">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="C.L.R<br/>(15 से 32)">
                                                        <ItemTemplate>
                                                            <span class="pull-right">
                                                                <asp:RequiredFieldValidator ID="rfvCowCLR" runat="server" Display="Dynamic"
                                                                    ControlToValidate="txtCowCLR"
                                                                    Text="<i class='fa fa-exclamation-circle' title='CLR दर्ज करें!'></i>"
                                                                    ErrorMessage="CLR दर्ज करें!" SetFocusOnError="true" ForeColor="Red"
                                                                    ValidationGroup="a" Enabled="false"></asp:RequiredFieldValidator>
                                                           <asp:RangeValidator ID="rvCowCLR" runat="server" ErrorMessage="(C.L.R) 15 से 32 के बीच होना चाहिए।" Display="Dynamic" ControlToValidate="txtCowCLR" Text="<i class='fa fa-exclamation-circle' title='(C.L.R) 15 से 32 के बीच होना चाहिए।'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a" Type="Double" MinimumValue="15" MaximumValue="32"></asp:RangeValidator>
                                                            </span>
                                                            <asp:TextBox ID="txtCowCLR" Enabled="false" onkeypress="return validateDec(this,event);" runat="server" CssClass="form-control">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="S.n.f&nbsp;&nbsp;&nbsp;&nbsp;" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCowSnf" ReadOnly="true" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:TemplateField HeaderText="Total&nbsp;&nbsp; Can">
                                                            <ItemTemplate>                                                             
                                                                <asp:TextBox ID="txtCowtotalCan" runat="server" CssClass="form-control">
                                                                </asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Good&nbsp;&nbsp; Can">
                                                            <ItemTemplate>                                                               
                                                                <asp:TextBox ID="txtCowGoodCan" runat="server" CssClass="form-control">
                                                                </asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnRemove" runat="server" CausesValidation="false" Text="-" OnClick="btnRemove_Click" CssClass="btn btn-warning" />
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Button ID="ButtonAdd" runat="server" ValidationGroup="a" Text="+" CommandArgument='<%# Container.DataItemIndex %>' CommandName="SaveRecord" CssClass="btn btn-success" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                </fieldset>
                                <div class="row">
                                    <div class="col-md-12 pull-left">
                                        <div class="form-group">
                                            <asp:Button ID="btnGetTotal" Style="margin-top: 20px;" CausesValidation="false" runat="server" CssClass="btn btn-success" Text="GetTotal" OnClick="btnGetTotal_Click" />
                                            <asp:Button runat="server" CssClass="btn btn-primary"  OnClientClick="return ValidatePage();" OnClick="btnSave_Click" Style="margin-top: 20px;" ValidationGroup="a" ID="btnSave" Text="Save" />
                                            <asp:Button runat="server" CssClass="btn btn-default" Style="margin-top: 20px;" ID="btnClear" Text="Clear" />
                                        </div>
                                    </div>

                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
                      </ContentTemplate>
                </asp:UpdatePanel>
            <div class="row">
			
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Entry List</h3>
                        </div>
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Date</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <i class="far fa-calendar-alt"></i>
                                                </span>
                                            </div>
                                            <asp:TextBox ID="txtfilterdate" autocomplete="off"  CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>CC<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Search"
                                                InitialValue="0" ErrorMessage="Select Chilling Center" Text="<i class='fa fa-exclamation-circle' title='Select Chilling Center !'></i>"
                                                ControlToValidate="ddlCCflt" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlCCflt" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSearch" style="margin-top:20px;" ValidationGroup="Search" runat="server" Text="Search" CssClass="btn btn-success" OnClick="btnSearch_Click" AccessKey="S"/>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
							 <div class="col-md-12">
                                    <div class="form-group">
                                         <asp:Button ID="btnExport" runat="server" Visible="false" Text="Export to dbf" CssClass="btn btn-primary" OnClick="btnExport_Click"/>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvEntryList" ShowFooter="true" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" runat="server" CssClass="datatable table table-bordered" AutoGenerateColumns="false" OnRowCommand="gvEntryList_RowCommand" OnRowDataBound="gvEntryList_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Shift">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblShift" runat="server" Text='<%# Eval("Shift") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Chiling Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCC" runat="server" Text='<%# Eval("CC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Society">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSociety" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Society Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSocietyCode" runat="server" Text='<%# Eval("Office_Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="B/C">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMilkType" runat="server" Text='<%# Eval("MilkType") %>'></asp:Label>
                                                  
                                                         </ItemTemplate>
                                                </asp:TemplateField>
                                               <%-- <asp:TemplateField HeaderText="Temp">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTemp" runat="server" Text='<%# Eval("Temp") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Milk Quality">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMilkQuality" runat="server" Text='<%# Eval("MilkQuality") %>'></asp:Label>
                                                    <asp:DropDownList ID="ddlMilkQuality" Visible="false" ClientIDMode="Static" runat="server" CssClass="form-control">
                                                                <asp:ListItem>Good</asp:ListItem>
                                                                <asp:ListItem>Sour</asp:ListItem>
                                                                <asp:ListItem>Curd</asp:ListItem>
                                                            </asp:DropDownList>
                                                         </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Milk Quantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMilkQuantity" runat="server" Text='<%# Eval("MilkQuantity") %>'></asp:Label>
                                                        <span class="pull-right">
                                                                <asp:RequiredFieldValidator ID="rfvMilkQuantity" runat="server" Display="Dynamic"
                                                                    ControlToValidate="txtMilkQuantity"
                                                                    Text="<i class='fa fa-exclamation-circle' title='दूध की मात्रा दर्ज करें!'></i>"
                                                                    ErrorMessage="दूध की मात्रा दर्ज करें'" SetFocusOnError="true" ForeColor="Red"
                                                                    ValidationGroup="Update" Enabled="true" ></asp:RequiredFieldValidator>
                                                             <asp:RangeValidator ID="rvCMilkQuantity" runat="server" ErrorMessage="मात्रा शून्य नहीं होनी चाहिए।" Display="Dynamic" ControlToValidate="txtMilkQuantity" Text="<i class='fa fa-exclamation-circle' title='मात्रा शून्य नहीं होनी चाहिए।'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a" Type="Double" MinimumValue="1" MaximumValue="1000"></asp:RangeValidator>
                                                            </span>
                                                        <asp:TextBox ID="txtMilkQuantity" onkeypress="return validateDec(this,event);" CssClass="form-control"  Visible="false" runat="server" Text='<%# Eval("MilkQuantity") %>' AutoPostBack="true" OnTextChanged="txtMilkQuantity_TextChanged"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fat">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFat" runat="server" Text='<%# Eval("Fat") %>'></asp:Label>
                                                        <span class="pull-right">
                                                                <asp:RequiredFieldValidator ID="rfvFat" runat="server" Display="Dynamic"
                                                                    ControlToValidate="txtFat"
                                                                    Text="<i class='fa fa-exclamation-circle' title='Fat दर्ज करें!'></i>"
                                                                    ErrorMessage="Fat दर्ज करें!" SetFocusOnError="true" ForeColor="Red"
                                                                    ValidationGroup="Update" Enabled="true"></asp:RequiredFieldValidator>
                                                                <%--<asp:RangeValidator ID="rvBufFat" runat="server" ErrorMessage="Minimum FAT % required 5.6 and maximum 11.0." Display="Dynamic" ControlToValidate="txtBufFat" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 5.6 and maximum 11.0!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a" Type="Double" MinimumValue="5.6" MaximumValue="11.0"></asp:RangeValidator>--%>
                                                                 <asp:RangeValidator ID="rvfFat" runat="server" Display="Dynamic" ControlToValidate="txtFat"  SetFocusOnError="true" ForeColor="Red" ValidationGroup="Update" Type="Double"></asp:RangeValidator>

                                                            </span>
                                                        <asp:TextBox ID="txtFat" onkeypress="return validateDec(this,event);" Visible="false"  CssClass="form-control" runat="server" Text='<%# Eval("Fat") %>' AutoPostBack="true" OnTextChanged="txtFat_TextChanged"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Snf">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSnf" runat="server" Text='<%# Eval("Snf") %>'></asp:Label>
                                                        <asp:TextBox ID="txtSnf" Enabled ="false" Visible="false" CssClass="form-control" runat="server" Text='<%# Eval("Snf") %>' ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Clr">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblClr" runat="server" Text='<%# Eval("Clr") %>'></asp:Label>
                                                        <span class="pull-right">
                                                                <asp:RequiredFieldValidator ID="rfvCLR" runat="server" Display="Dynamic"
                                                                    ControlToValidate="txtClr"
                                                                    Text="<i class='fa fa-exclamation-circle' title='CLR दर्ज करें!'></i>"
                                                                    ErrorMessage="CLR दर्ज करें!" SetFocusOnError="true" ForeColor="Red"
                                                                    ValidationGroup="Update" Enabled="true"></asp:RequiredFieldValidator>
                                                           <asp:RangeValidator ID="rvCLR" runat="server" ErrorMessage="(C.L.R) 15 से 32 के बीच होना चाहिए।" Display="Dynamic" ControlToValidate="txtClr" Text="<i class='fa fa-exclamation-circle' title='(C.L.R) 15 से 32 के बीच होना चाहिए।'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Update" Type="Double" MinimumValue="15" MaximumValue="32"></asp:RangeValidator>
                                                            </span>
                                                        <asp:TextBox ID="txtClr" onkeypress="return validateDec(this,event);"  Visible="false" CssClass="form-control" runat="server" Text='<%# Eval("Clr") %>' AutoPostBack="true" OnTextChanged="txtClr_TextChanged"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fat(In Kg)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFatInKg" runat="server" Text='<%# Eval("FatInKg") %>'></asp:Label>
                                                        <asp:TextBox ID="txtFatInKg" Enabled ="false" Visible="false" CssClass="form-control" runat="server" Text='<%# Eval("FatInKg") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Snf(In Kg)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSnfInKg" runat="server" Text='<%# Eval("SnfInKg") %>'></asp:Label>
                                                        <asp:TextBox ID="txtSnfInKg" Enabled ="false" Visible="false" CssClass="form-control" runat="server" Text='<%# Eval("SnfInKg") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField HeaderText="Total Can">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalCan" runat="server" Text='<%# Eval("TotalCan") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Good Can">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGoodCan" runat="server" Text='<%# Eval("GoodCan") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" Visible='<%# Eval("Count").ToString()=="0"?true:false%>' CommandName="EditRecord" CommandArgument='<%# Eval("MilkCollectionViaCanesChallan_ID") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkDelete" CausesValidation="false" runat="server" Visible='<%# Eval("Count").ToString()=="0"?true:false%>' CommandName="DeleteRecord" CommandArgument='<%# Eval("MilkCollectionViaCanesChallan_ID") %>' OnClientClick="return confirm('Do you really want to delete record?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkUpdate" Visible ="false"  ValidationGroup="Update" runat="server"  CssClass="btn btn-default" CommandName="UpdateRecord" CommandArgument='<%# Eval("MilkCollectionViaCanesChallan_ID") %>' OnClientClick="return confirm('Do you really want to Update record?')">Update</i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--  <asp:TemplateField HeaderText="Kilo">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblI_MilkQuantity" runat="server" Text='<%# Eval("I_MilkQuantity") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
										<div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvDbf" Visible="false"  ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" runat="server" CssClass="datatable table table-bordered" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="T_UN_CD">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblT_UN_CD" runat="server" Text='<%# Eval("T_UN_CD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="T_SOC_CD">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblT_SOC_CD" runat="server" Text='<%# Eval("T_SOC_CD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText="T_DATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblT_DATE" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="T_SHIFT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblT_SHIFT" runat="server" Text='<%# Eval("T_SHIFT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            

                                                <asp:TemplateField HeaderText="T_BFCW_IND">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblT_BFCW_IND" runat="server" Text='<%# Eval("T_BFCW_IND") %>'></asp:Label>
                                                  
                                                         </ItemTemplate>
                                                </asp:TemplateField>
                                              
                                                <asp:TemplateField HeaderText="T_CATG">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblT_CATG" runat="server" Text='<%# Eval("T_CATG") %>'></asp:Label>
                                                   
                                                         </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="T_QTY">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblT_QTY" runat="server" Text='<%# Eval("MilkQuantity") %>'></asp:Label>                                                   
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="T_FAT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblT_FAT" runat="server" Text='<%# Eval("Fat") %>'></asp:Label>                                                                                  
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="T_SNF">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblT_SNF" runat="server" Text='<%# Eval("Snf") %>'></asp:Label>                            
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="T_CLR">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblT_CLR" runat="server" Text='<%# Eval("Clr") %>'></asp:Label>                                                                                                 
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                    </div>
                                </div>
                            </div>
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
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {


                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }

    </script>

    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
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

                        columns: [0, 1, 2, 3, 4, 5, 6,7,8,9,10,11,12,13]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12,13]
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

        
  
        $(function () {
            $(".Society").autocomplete({

                source: function (request, response) {
                    debugger
                    $.ajax({

                        url: 'RMRD_Challan_EntryFromCanesTotalEntry_New1.aspx/SearchSociety',
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
            }).focus(function () {
                $(this).autocomplete("search");
            });
        });
       
        <%--$(document).ready(function () {
            $('.loader').fadeOut();
            $("#<%=btnSearch.ClientID%>").click((function () {

                if (Page_IsValid) {
                    $('.loader').show();
                    return true;

                }
                
                  

                
            }));
        });--%>

      
        ////$(document).on('focus', '.select2-selection.select2-selection--single', function (e) {
        ////    $(this).closest(".select2-container").siblings('select:enabled').select2('open');
        ////});
    </script>
</asp:Content>

