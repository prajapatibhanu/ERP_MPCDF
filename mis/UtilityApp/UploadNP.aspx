<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="UploadNP.aspx.cs" Inherits="mis_UtilityApp_UploadNP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .StringDiv {
            font-size: 13px !important;
            font-family: monospace !important;
        }

        table td, table th {
            border-bottom: 1px dotted #c1c0c0 !important;
            padding: 5px !important;
            border-right: 1px dotted #c1c0c0 !important;
            margin: 0px !important;
        }

        .right_align {
            text-align: right;
        }

        .left_align {
            text-align: left;
        }

        .center_align {
            text-align: left;
        }

        table th {
            background: #9e9e9e !important;
            padding: 2px 5px !important;
            word-break: break-all !important;
            border-bottom: 1px dotted #ddd !important;
        }

        .btnsubmit {
            margin-top: 18px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">


                <div class="box-header">
                    <h3 class="box-title">Update Office Bank Details</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">


                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Office Name</label><span style="color: red">*</span>
                                    <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control" ClientIDMode="Static" Enabled="false">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>CC<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select CC" Text="<i class='fa fa-exclamation-circle' title='Select CC !'></i>"
                                            ControlToValidate="ddlccbmcdetail" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlccbmcdetail"  runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
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
                                        <label>From Date  </label>
                                        <span style="color: red">*</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtFdt" ErrorMessage="Please Enter From Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter From Date !'></i>"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revdate" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtFdt" ErrorMessage="Invalid From Date" Text="<i class='fa fa-exclamation-circle' title='Invalid From Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                        </span>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtFdt" Enabled="false" onkeypress="javascript: return false;" Width="100%" MaxLength="10" data-date-end-date="0d" onpaste="return false;" placeholder="Select From Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>To Date  </label>
                                        <span style="color: red">*</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtTdt" ErrorMessage="Please Enter To Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter To Date !'></i>"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtTdt" ErrorMessage="Invalid To Date" Text="<i class='fa fa-exclamation-circle' title='Invalid To Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                        </span>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtTdt" Enabled="false" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" data-date-end-date="0d" placeholder="Select To Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Choose File<span class="text-danger">*</span></label>
                                    <asp:FileUpload ID="FileUpload1" runat="server" required="required" CssClass="form-control" />
                                </div>
                            </div>

                            <%--                          <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" Text="Sheet Type ?"></asp:Label>
                                    <asp:DropDownList ID="rbSheetType" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Milk Collection" Value="Collection" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Truck Sheet" Value="Truck"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>--%>


                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                    <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-success btnsubmit" ValidationGroup="a" Text="Upload" OnClick="btnUpload_Click" />
                                </div>
                            </div>
                        </div>


                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-12">
                                <p>
                                    <b>Mandatory Columns For Update Bank Detail</b>:
                                    M_SNo, M_OFC_CD, M_BANK_AC, M_IFSC_CD, C_ACHOLDER
                                </p>
                            </div>
                        </div>
                    </div>-

                    <hr />
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                    <%--<asp:GridView ID="GridView1" runat="server">
                    </asp:GridView>--%>
                    <div class="col-md-12">
                        <div runat="server" id="StringDiv" class="StringDiv table-responsive"></div>
                    </div>

                </div>
            </div>

        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

