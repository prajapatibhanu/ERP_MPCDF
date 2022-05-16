<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ImportTruckSheet.aspx.cs" Inherits="mis_UtilityApp_ImportTruckSheet" %>

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
                    <h3 class="box-title">Truck Sheet - Data Import Utility</h3>
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
                                    <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-success btnsubmit" Text="Upload" OnClick="btnUpload_Click" />
                                </div>
                            </div>
                        </div>


                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-12">
                                <p>
                                    <b>Mandatory Columns For Truck sheet</b>:
                                    T_UN_CD, T_SOC_CD, T_DATE, T_SHIFT, T_BFCW_IND, T_CATG, T_QTY, T_FAT, T_SNF, T_CLR
                                </p>
                            </div>
                        </div>
                    </div>

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

