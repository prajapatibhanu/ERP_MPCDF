<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ImportTruckSheetDbf.aspx.cs" Inherits="mis_UtilityApp_ImportTruckSheetDbf" %>

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

        /* loading dots */

        .one {
            opacity: 0;
            -webkit-animation: dot 1.3s infinite;
            -webkit-animation-delay: 0.0s;
            animation: dot 1.3s infinite;
            animation-delay: 0.0s;
        }

        .two {
            opacity: 0;
            -webkit-animation: dot 1.3s infinite;
            -webkit-animation-delay: 0.2s;
            animation: dot 1.3s infinite;
            animation-delay: 0.2s;
        }

        .three {
            opacity: 0;
            -webkit-animation: dot 1.3s infinite;
            -webkit-animation-delay: 0.3s;
            animation: dot 1.3s infinite;
            animation-delay: 0.3s;
        }

        @-webkit-keyframes dot {
            0% {
                opacity: 0;
            }

            50% {
                opacity: 0;
            }

            100% {
                opacity: 1;
            }
        }

        @keyframes dot {
            0% {
                opacity: 0;
            }

            50% {
                opacity: 0;
            }

            100% {
                opacity: 1;
            }
        }

        p.loading {
    font-weight: 900;
    font-size: 22px;
    text-align: center;
    color: #e91e63;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
<%--Confirmation Modal Start --%>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnUpload_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
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
                                    <asp:Button ID="btnUpload" ValidationGroup="a" runat="server" CssClass="btn btn-success btnsubmit" Text="Upload" OnClick="btnUpload_Click" OnClientClick="return ValidatePage();"/>
                                </div>
                            </div>
                        </div>


                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-12">
                                <p>
                                    <b>Mandatory Columns For Truck sheet</b>: T_UN_CD, T_SOC_CD, T_DATE, T_SHIFT, T_BFCW_IND, T_CATG, T_QTY, T_FAT, T_SNF, T_CLR
                                </p>
                                
                                
                            </div>
                        </div>
                    </div>
                    <asp:Label ID="lbluploadingData" runat="server" Text=""></asp:Label>
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
<script type="text/javascript">
         function ValidatePage() {

             if (typeof (Page_ClientValidate) == 'function') {
                 Page_ClientValidate('a');
             }

             if (Page_IsValid) {

                 
                 if (document.getElementById('<%=btnUpload.ClientID%>').value.trim() == "Upload") {
                     document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                     $('#myModal').modal('show');
                     return false;
                 }
             }
         }
    </script>
</asp:Content>

