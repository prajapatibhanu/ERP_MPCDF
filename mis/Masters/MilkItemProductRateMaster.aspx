<%@ Page Title="" Language="C#" Culture="en-IN" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MilkItemProductRateMaster.aspx.cs" Inherits="mis_Masters_MilkItemProductRateMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <style> 
        th.sorting, th.sorting_asc, th.sorting_desc {
            background: #1062ab !important;
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

        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('images/prgrs.gif') 50% 50% no-repeat rgba(0, 0, 0, 0.3);
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSubmit_Click" Style="margin-top: 20px; width: 50px;" />
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
            <!-- SELECT2 EXAMPLE -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Rate Master</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>
                            </div>
                            <fieldset>
                                <legend>Office Type Details
                                </legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Office Type<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Office Type" Text="<i class='fa fa-exclamation-circle' title='Select Office Type !'></i>"
                                                    ControlToValidate="ddlOfficeType" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlOfficeType" OnInit="ddlOfficeType_Init" OnSelectedIndexChanged="ddlOfficeType_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3" id="spanDivision" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>Divison<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv2" ValidationGroup="a"
                                                    InitialValue="0" ForeColor="Red" ErrorMessage="Select Division" Text="<i class='fa fa-exclamation-circle' title='Select Division !'></i>"
                                                    ControlToValidate="ddlDivision" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlDivision" OnInit="ddlDivision_Init" runat="server" CssClass="form-control select2" AutoPostBack="true" ClientIDMode="Static"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3" id="spanDistrict" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>District<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv3" ValidationGroup="a"
                                                    InitialValue="0" ForeColor="Red" ErrorMessage="Please Select District" Text="<i class='fa fa-exclamation-circle' title='Select !'></i>"
                                                    ControlToValidate="ddlDistrict" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control select2" AutoPostBack="true" ClientIDMode="Static">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Dugdh Sangh<span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlOffice" runat="server" Enabled="false" CssClass="form-control select2">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Product Details
                                </legend>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label>Item Type<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                    ErrorMessage="Select Product Category" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Product Category !'></i>"
                                                    ControlToValidate="ddlProductCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlProductCategory" CssClass="form-control select2" AutoPostBack="true" OnInit="ddlProductCategory_Init" OnSelectedIndexChanged="ddlProductCategory_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label>Item name<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                    ErrorMessage="Select Product Name" Text="<i class='fa fa-exclamation-circle' title='Select Product Name !'></i>"
                                                    ControlToValidate="ddlProductName" InitialValue="0" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtProductName"
                                                        ErrorMessage="Only alphabet allow" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Only alphabet allow. !'></i>"
                                                        SetFocusOnError="true" ValidationExpression="^[a-zA-Z\s]+$">
                                                    </asp:RegularExpressionValidator>--%>
                                            </span>
                                            <asp:DropDownList ID="ddlProductName" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <%--   <div class="col-sm-4">
                                <div class="form-group">
                                        <label>Unit<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                ErrorMessage="Select Unit" Text="<i class='fa fa-exclamation-circle' title='Select Unit !'></i>"
                                                ControlToValidate="ddlUnit" InitialValue="0" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                       <%--   <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtProductName"
                                                        ErrorMessage="Only alphabet allow" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Only alphabet allow. !'></i>"
                                                        SetFocusOnError="true" ValidationExpression="^[a-zA-Z\s]+$">
                                                    </asp:RegularExpressionValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlUnit" runat="server" OnInit="ddlUnit_Init" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>--%>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label>Item Size<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter Packet Size" Text="<i class='fa fa-exclamation-circle' title='Enter Packet Size !'></i>"
                                                    ControlToValidate="txtPacketSize" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtPacketSize"
                                                    ErrorMessage="Only Numeric allow" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Only Numeric allow. !'></i>"
                                                    SetFocusOnError="true" ValidationExpression="^[0-9]+$">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox ID="txtPacketSize" onkeypress="return validateNum(event);" placeholder="Enter Packet Size" CssClass="form-control" MaxLength="5" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label>Item Rate<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvmarprate" ValidationGroup="a"
                                                    ErrorMessage="Enter MRP Rate" Text="<i class='fa fa-exclamation-circle' title='Enter MRP Rate !'></i>"
                                                    ControlToValidate="txtMRPRate" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtMRPRate"
                                                    ErrorMessage="Only alphabnumeric and space allow." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Only alphabnumeric and space allow. !'></i>"
                                                    SetFocusOnError="true" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox ID="txtMRPRate" CssClass="form-control" placeholder="Enter Item Rate" onkeypress="return validateDec(this,event);" MaxLength="10" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label>Item Sale Rate<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a"
                                                    ErrorMessage="Enter Sale Rate" Text="<i class='fa fa-exclamation-circle' title='Enter Sale Rate !'></i>"
                                                    ControlToValidate="txtSaleRate" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtSaleRate"
                                                    ErrorMessage="Only alphabnumeric and space allow." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Only alphabnumeric and space allow. !'></i>"
                                                    SetFocusOnError="true" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox ID="txtSaleRate" CssClass="form-control" placeholder="Enter Item Sale Rate" onkeypress="return validateDec(this,event);" MaxLength="10" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label>Item Commission Rate<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a"
                                                    ErrorMessage="Enter Commission Rate" Text="<i class='fa fa-exclamation-circle' title='Enter Commission Rate !'></i>"
                                                    ControlToValidate="txtCommissionRate" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtCommissionRate"
                                                    ErrorMessage="Only alphabnumeric and space allow." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Only alphabnumeric and space allow. !'></i>"
                                                    SetFocusOnError="true" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox ID="txtCommissionRate" CssClass="form-control" placeholder="Enter Item Commission Rate" onkeypress="return validateDec(this,event);" MaxLength="10" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label>Effective from Date<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="red" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtEffectiveDate" ErrorMessage="Enter Effective Date." Text="<i class='fa fa-exclamation-circle' title='Enter Effective Date !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revdate" ValidationGroup="a" ForeColor="Red" runat="server" Display="Dynamic" ControlToValidate="txtEffectiveDate" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtEffectiveDate" onkeypress="return false;" runat="server" placeholder="dd/mm/yyyy" class="form-control" data-date-end-date="+7d" autocomplete="off" data-date-format="dd/mm/yyyy" data-date-autoclose="true" data-provide="datepicker" data-date-start-date="-365d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-block btn-primary" ValidationGroup="a" Text="Save" OnClientClick="return ValidatePage();" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnClear" OnClick="btnClear_Click" runat="server" Text="Clear" CssClass="btn btn-block btn-default" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12" >
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Milk Rate Master Details</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="table-responsive">
                                <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                    EmptyDataText="No Record Found." DataKeyNames="ProductId">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Item Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCat_id" Visible="false" runat="server" Text='<%# Eval("Cat_id") %>' />
                                                <asp:Label ID="lblCategory_Name" runat="server" Text='<%# Eval("Category_Name") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductId" Visible="false" runat="server" Text='<%# Eval("ProductId") %>' />
                                                <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Size">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPacketSize" runat="server" Text='<%# Eval("PacketSize") %>' />
                                               <%-- <asp:Label ID="lblUnit" Visible="false" runat="server" Text='<%# Eval("Unit_id") %>' />
                                                <asp:Label ID="lblUQCCode" runat="server" Text='<%# Eval("UQCCode") %>' />--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMRPRate" runat="server" Text='<%# Eval("MRPRate") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Effective from Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEffectiveDate" runat="server" Text='<%# Eval("EffectiveDate","{0:dd/MM/yyyy}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkView" CommandName="RecordView" CommandArgument='<%#Eval("VariantId") %>' runat="server" ForeColor="OrangeRed" ToolTip="Rate History."><i class="fa fa-history"></i></asp:LinkButton>
                                                &nbsp;<asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("VariantId") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                &nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("VariantId") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.box-body -->
            <%--Rate History popup Modal Start --%>
            <div class="modal fade" id="ViewModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div style="display: table; height: 100%; width: 100%;">
                    <div class="modal-dialog" style="width: 60%; display: table-cell; vertical-align: middle;">
                        <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                            <div class="modal-header" style="background-color: #d9d9d9;">
                                <button type="button" class="close" data-dismiss="modal">
                                    <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                                </button>
                                <h4 class="modal-title" id="myModalLabel2">
                                    <asp:Label ID="lblViewDetailHeader" runat="server"></asp:Label></h4>
                            </div>
                            <div class="clearfix"></div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView runat="server" AutoGenerateColumns="false" EmptyDataText="No History Found" ShowHeaderWhenEmpty="true" CssClass="table table-bordered" ID="gvRateHistroy">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSerial" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product Name">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblProductName" Text='<%# Eval("ProductName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product Size">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblProductSize" Text='<%# Eval("PacketSize") + " " + Eval("UQCCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate (In Rs.)">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblRate" Text='<%# Eval("MRPRate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Effective Date">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblEffectiveDate" Text='<%# Eval("EffectiveDate","{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="form-group">
                                                <%--<asp:Button ID="Button1" runat="server" ValidationGroup="Remark" Text="Save" OnClientClick="return ValidatePage();" CssClass="btn btn-success pull-left" />
                                                &nbsp;
                                                <button type="button" class="btn btn-default pull-right" data-dismiss="modal">CLOSE </button>--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%-- <div class="modal-footer">
                                
                            </div>--%>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
            </div>
            <%--Rate History popup Modal End --%>
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>
    <script>
        $('.datatable').DataTable({
            paging: true,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false,
            }],
            dom: '<"row"<"col-sm-10 pull-left"fB>l<"col-sm-2">>' +
                '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
                '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: ('Milk Product Variant Master Details').fontsize(5),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    filename: 'ProductVariant_Report',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: ('Milk Product Variant Master Details').fontsize(5),
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

        function ViewDetailPopup() {
            $('#ViewModal').modal('show');
            return false;
        }
    </script>
    <script type="text/javascript">
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
    </script>
</asp:Content>


