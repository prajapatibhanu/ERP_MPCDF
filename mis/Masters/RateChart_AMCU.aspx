<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RateChart_AMCU.aspx.cs" Inherits="mis_Masters_RateChart_AMCU" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        table {
            font-size: 14px;
            border: 1px solid #CCC;
        }

        td {
            padding: 4px;
            margin: 3px;
            border: 1px solid #CCC;
        }

        th {
            background-color: #104E8B;
            color: #000;
            font-weight: bold;
        }

        td:first-child {
            font-weight: bold;
        }
    </style>


    <script type="text/javascript">
        function confirmation() {
            if (confirm('Are you sure you want to save rate chart on excel format ?')) {
                return true;
            } else {
                return false;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content" runat="server" id="SAMCU" visible="false">
            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">AMCU Milk Collection Rate Chart [ FAT vs SNF ]</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">

                        <div class="row">
                            <div class="col-md-12">

                                <div class="col-md-3">
                                    <label><b>Formula Based on :</b> </label>
                                    <asp:Label ID="lblpurchaserate_SNF" runat="server"></asp:Label>
                                </div>

                                <div class="col-md-3">
                                    <label><b>Deduction :</b></label>
                                    <asp:Label ID="lblDeductionRate1_SNF" runat="server"></asp:Label>
                                    &nbsp;&&nbsp;
                                     <asp:Label ID="lbllblDeductionRate2_SNF" runat="server"></asp:Label>
                                </div>

                                <div class="col-md-3">
                                    <label><b>Effective From: </b></label>
                                    <asp:Label ID="lblEffectiveDate_SNF" runat="server"></asp:Label>
                                </div>
                                 
                                 
                            </div>
                        </div>


                        <hr />
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Button ID="btnSave" runat="server" Text="Export To Excel" OnClientClick="return confirmation();" CssClass="btn btn-primary btn-block" OnClick="btnSave_Click" />
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="row table-responsive">
                        <asp:GridView ID="GridView2" PageSize="50" runat="server"
                            class="table table-hover table-bordered pagination-ys"
                            ShowHeaderWhenEmpty="true">
                        </asp:GridView>
                    </div>

                </div>
            </div>
        </section>

        <section class="content" runat="server" id="SNONAMCU" visible="false">
            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Non AMCU Milk Collection Rate Chart [ FAT vs CLR ]</h3>
                </div>
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">

                        <div class="row">
                            <div class="col-md-12">
                                 
                                <div class="col-md-3">
                                    <label><b>Formula Based on :</b> </label>
                                    <asp:Label ID="lblpurchaserate" runat="server"></asp:Label>
                                </div>

                                <div class="col-md-3">
                                    <label><b>Deduction :</b></label>
                                    <asp:Label ID="lblDeductionRate1" runat="server"></asp:Label>
                                    &nbsp;&&nbsp;
                                     <asp:Label ID="lbllblDeductionRate2" runat="server"></asp:Label>
                                </div>

                                <div class="col-md-3">
                                    <label><b>Effective From: </b></label>
                                    <asp:Label ID="lblEffectiveDate" runat="server"></asp:Label>
                                </div>
                                 

                            </div>
                        </div>


                        <hr />
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Button ID="btnNonAmcu" runat="server" Text="Export To Excel" OnClientClick="return confirmation();" CssClass="btn btn-primary btn-block" OnClick="btnNonAmcu_Click" />
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="row table-responsive">
                        <asp:GridView ID="GridView3" PageSize="50" runat="server"
                            class="table table-hover table-bordered pagination-ys"
                            ShowHeaderWhenEmpty="true">
                        </asp:GridView>
                    </div>

                </div>
            </div>
        </section>


    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>
