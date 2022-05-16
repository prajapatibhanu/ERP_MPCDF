<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MilkOpeningStockForProduction.aspx.cs" Inherits="mis_dailyplan_MilkOpeningStockForProduction" %>

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
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-header">
                    <h3 class="box-title">Shift Wise Milk Collection Detail</h3>
                </div>
                <div class="box-body">


                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlDS" runat="server" CssClass="form-control"></asp:DropDownList>
                                <small><span id="valddlDS" class="text-danger"></span></small>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Date</label>
                                <asp:TextBox ID="txtDate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>

                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Shift</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="ddlShift" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Shift!'></i>" ErrorMessage="Select Shift" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlShift" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" Style="margin-top: 20px;" runat="server" CssClass="btn btn-block btn-success" Text="Search" ClientIDMode="Static" OnClick="btnSubmit_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>

                    </div>

                    <hr />

                    <fieldset>
                        <legend>Milk In Stock</legend>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group table-responsive">
                                    <asp:GridView ID="gvReceivedMilkDetail" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                        EmptyDataText="No Record Found.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Shift Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Milk Qty In KG">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCr" runat="server" Text='<%# Eval("Cr") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="FAT In Kg">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFat" runat="server" Text='<%# Eval("Fat") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="SNF In KG">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSnf" runat="server" Text='<%# Eval("Snf") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton3" ToolTip="Receive" class="small-box-footer" OnClick="LinkButton3_Click" runat="server"> 
                                                   View More 
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                    </fieldset>



                </div>

            </div>


            <div class="modal" id="CCModel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 430px;">
                        <div class="modal-header">
                            <asp:LinkButton runat="server" class="close" ID="LinkButton2" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>

                            <h4 class="modal-title">Milk In Stock Date/Shift Wise </h4>
                        </div>
                        <div class="modal-body">

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row" style="height: 350px; overflow: scroll;">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <section class="content">
                                                    <div class="box box-Manish" style="min-height: 300px;">
                                                        <div class="box-header">
                                                            <h3 class="box-title">Milk In Stock Date/Shift Wise</h3>
                                                        </div>
                                                        <!-- /.box-header -->
                                                        <div class="box-body">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="table-responsive">
                                                                <asp:GridView ID="GVCC" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                    EmptyDataText="No Record Found." ShowFooter="true">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sr.No.">
                                                                            <ItemTemplate>
                                                                                <%#Container.DataItemIndex+1 %>
                                                                            </ItemTemplate>

                                                                            <FooterTemplate>Total</FooterTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                                                                            </ItemTemplate>

                                                                            <FooterTemplate></FooterTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Shift Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                            </ItemTemplate>

                                                                            <FooterTemplate></FooterTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Milk Qty In KG">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCr" runat="server" Text='<%# Eval("Cr") %>'></asp:Label>
                                                                            </ItemTemplate>

                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblTMQty" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="FAT In Kg">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFat" runat="server" Text='<%# Eval("Fat") %>'></asp:Label>
                                                                            </ItemTemplate>

                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblTFQty" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="SNF In KG">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSnf" runat="server" Text='<%# Eval("Snf") %>'></asp:Label>
                                                                            </ItemTemplate>

                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblTSQty" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Vehicle No">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblV_VehicleNo" runat="server" Text='<%# Eval("V_VehicleNo") %>'></asp:Label>
                                                                            </ItemTemplate>

                                                                            <FooterTemplate></FooterTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Updated On">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblUpdatedOn" runat="server" Text='<%# (Convert.ToDateTime(Eval("UpdatedOn"))).ToString("dd-MM-yyyy hh:mm:ss tt") %>'></asp:Label>
                                                                            </ItemTemplate>

                                                                            <FooterTemplate></FooterTemplate>
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>

                                                        </div>

                                                    </div>
                                                    <!-- /.box-body -->

                                                </section>
                                                <!-- /.content -->
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>

        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>

        function CCModelF() {
            $("#CCModel").modal('show');

        }

    </script>
</asp:Content>

