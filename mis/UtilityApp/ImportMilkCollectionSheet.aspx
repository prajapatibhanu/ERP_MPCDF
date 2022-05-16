<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ImportMilkCollectionSheet.aspx.cs" Inherits="mis_UtilityApp_ImportMilkCollectionSheet" %>

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
                    <h3 class="box-title">Milk Collection - Data Import Utility</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
<%--                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Office Type (कार्यालय का प्रकार)<span style="color: red;">*</span></label>
                                    <asp:DropDownList ID="ddlOfficeType_Title" runat="server" required="required"  class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlOfficeType_Title_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Office (कार्यालय)<span style="color: red;">*</span></label>
                                    <asp:DropDownList ID="ddlOffice_ID" runat="server" required="required"  class="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>--%>
<%--                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Office Name</label><span style="color: red">*</span>
                                    <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control" ClientIDMode="Static" Enabled="false">
                                    </asp:DropDownList>
                                </div>
                            </div>--%>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Office Name</label><span style="color: red">*</span>
      <%--                              <asp:DropDownList runat="server" ID="DropDownList1" CssClass="form-control" ClientIDMode="Static" Enabled="false">
                                    </asp:DropDownList>--%>
                                    <asp:TextBox ID="txtOfficeName" runat="server" CssClass="form-control"  Enabled="false" ></asp:TextBox>            
                                    <asp:HiddenField ID="hfParent_Office_ID" runat="server" />
                                    <asp:HiddenField ID="hfOffice_ID" runat="server" />
                                    <asp:HiddenField ID="hfOffice_Code" runat="server" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>File Format<span class="text-danger">*</span></label>
                                     <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvFileForamt" runat="server" Display="Dynamic" ValidationGroup="a" ControlToValidate="ddlFileForamt" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select File Format!'></i>" ErrorMessage="Select Society" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddlFileForamt" runat="server"  CssClass="form-control">
                                       <asp:ListItem Value="0">Select</asp:ListItem>
                                       <asp:ListItem Value="1">NDDB(Hindi Format)</asp:ListItem>
                                        <asp:ListItem Value="2">NDDB(English Format)</asp:ListItem>
                                       <asp:ListItem Value="3">PROMPT</asp:ListItem>
									    <asp:ListItem Value="4">REIL</asp:ListItem>
                                    </asp:DropDownList>

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
                    <%--<div class="row">
                        <div class="col-md-12">
                            <div class="col-md-12">
                                <p>
                                    <b>Mandatory Columns For Milk collection sheet</b>:
                                    M_SAMPLE_NO, M_CODE, M_NAME, M_DATE, M_SHIFT, M_MILKTYPE, M_QUANTITY, M_FAT_PER, M_SNF_PER, M_CLR, M_RATE_P_L, 
M_AMOUNT, M_WATER_PER, M_PROTEIN, M_DENSITY, M_LACTOSE
                                </p>
                            </div>
                        </div>
                    </div>--%>

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

