<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CCWiseUpdateSocietyCategory.aspx.cs" Inherits="mis_UtilityApp_CCWiseUpdateSocietyCategory" %>

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
                    <h3 class="box-title">CC Wise Update Society Category</h3>
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
                                    M_SNo,M_SOC_CD, M_CATG
                                </p>
                            </div>
                        </div>
                    </div>-

                    <hr />
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                    <%--<asp:GridView ID="GridView1" runat="server">
                    </asp:GridView>--%>
                    <div class="col-md-12">
                        <asp:Button ID="btnExport" Visible="false" runat="server" Text="Export" CssClass="btn btn-primary" OnClick="btnExport_Click"/>
                        <div runat="server" id="StringDiv" class="StringDiv table-responsive"></div>
                    </div>

                </div>
            </div>

        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

