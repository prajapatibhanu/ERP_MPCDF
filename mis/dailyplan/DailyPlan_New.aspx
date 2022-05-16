<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DailyPlan_New.aspx.cs" Inherits="mis_dailyplan_DailyPlan_New" %>

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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnYes_Click" Style="margin-top: 20px; width: 50px;" />
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
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-header">
                    <h3 class="box-title">Daily Production Plan According To Demand and Recipe Details</h3>
                </div>
                <div class="box-body">

                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlDS" runat="server" CssClass="form-control select2"></asp:DropDownList>
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
                                    <asp:DropDownList ID="ddlShift" CssClass="form-control select2" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Production Section<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlPSection" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                <small><span id="valddlDSPS" class="text-danger"></span></small>
                            </div>
                        </div>


                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" Style="margin-top: 20px;" runat="server" CssClass="btn btn-block btn-success" Text="Search" ClientIDMode="Static" OnClick="btnSubmit_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>

                    </div>

                    <hr />

                    <fieldset runat="server" id="FSDEMANDDETAIl" visible="false">
                        <legend>Milk Demand Details</legend>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group table-responsive">
                                    <asp:GridView ID="gvReceivedMilkDetail" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                        EmptyDataText="No Record Found." ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Variant Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                    <asp:Label ID="lblItem_id" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    Total
                                                </FooterTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Prev. Demand </br>(In Pkt)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrev_Demand_InPkt" Text='<%# Eval("Prev_Demand_InPkt") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="Prev_Demand_InPkt_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Prev. Demand </br>(In Ltr)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrev_DemandInLtr" Text='<%# Eval("Prev_DemandInLtr") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblPrev_DemandInLtr_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Current Demand </br>(In Pkt)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCurrent_Demand_InPkt" runat="server" Text='<%# Eval("Current_Demand_InPkt") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblCurrent_Demand_InPkt_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Current Demand </br>(In Ltr)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCurrent_Demand_InLtr" runat="server" Text='<%# Eval("Current_Demand_InLtr") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblCurrent_Demand_InLtr_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Current Demand 10 % surplus </br>(In Ltr)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTenPerSurPlusQty" runat="server" Text='<%# Eval("TenPerSurPlusQty") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTenPerSurPlusQty_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton3" ToolTip="Receive" class="small-box-footer" OnClick="LinkButton3_Click" runat="server"> 
                                                   View Recipe 
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>

                                    <div runat="server" class="pull-right" style="padding-top: 2px;" id="div2">
                                        <asp:Button ID="btnSave" runat="server" ValidationGroup="a" OnClientClick="return ValidatePage();" CssClass="btn btn-success" Text="Save" />
                                    </div>

                                </div>
                            </div>
                        </div>

                    </fieldset>



                </div>

            </div>



            <div class="box box-success" runat="server" id="reportdiv"  visible="false">
                <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                <div class="box-header">
                    <h3 class="box-title">Daily Production Plan [Required inventory Information]</h3>
                </div>
                <div class="box-body">

                    <div class="row">
                        <div class="col-md-12">
                            <div class="fancy-title  title-dotted-border">
                                <h5 class="box-title">
                                    <b>DS Name -</b>&nbsp;&nbsp;<asp:Label ID="lblDcsname" runat="server"></asp:Label>&nbsp;&nbsp;
                                    <b>Date -</b>&nbsp;&nbsp;<asp:Label ID="lbldate" runat="server"></asp:Label>&nbsp;&nbsp;
                                        <b>Shift -</b> &nbsp;&nbsp;<asp:Label ID="lblshift" runat="server"></asp:Label>&nbsp;&nbsp;
                                    <b>Production Section -</b>  &nbsp;&nbsp;<asp:Label ID="lblProductionSection" runat="server"></asp:Label>
                                </h5>
                            </div>
                        </div>
                    </div>
                    <hr />
                    <fieldset>
                        <legend>Required inventory Information</legend>
                        <div class="row">

                            <div class="col-md-12">
                                <div class="form-group table-responsive">
                                    <asp:GridView ID="gvRID" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                        EmptyDataText="No Record Found." ShowFooter="true">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Sr.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Inventory (In KG)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVarient_Qty" runat="server" Text='<%# Eval("Varient_Qty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unit </br>(In Ltr)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUQCCode" runat="server" Text='<%# Eval("UQCCode") %>'></asp:Label>
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

                            <h4 class="modal-title">Recipe Details </h4>
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
                                                            <h3 class="box-title">Recipe Details</h3>
                                                        </div>
                                                        <!-- /.box-header -->
                                                        <div class="box-body">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="table-responsive">
                                                                <asp:GridView ID="gv_RecipeInfo" ShowHeader="true" AutoGenerateColumns="false"
                                                                    CssClass="table table-bordered" runat="server" EmptyDataText="No Recipe Found." ShowFooter="true">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="S.No.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Item Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="TotalQty_Req">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTotalQty_Req" runat="server" Text='<%# Eval("TotalQty_Req") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Unit Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("UnitName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                </asp:GridView>
                                                                <hr />
                                                                <asp:GridView ID="gvpackagingMaterial" ShowHeader="true" AutoGenerateColumns="false"
                                                                    CssClass="table table-bordered" runat="server" EmptyDataText="No Recipe Found." ShowFooter="true">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="S.No.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Item Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Total Quantity">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTotalPolyRoll" runat="server" Text='<%# Eval("Totalpackagingmaterial") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Unit Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("UnitName") %>'></asp:Label>
                                                                            </ItemTemplate>
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

    <script>

        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }
            debugger;
            if (Page_IsValid) {

                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }

    </script>


</asp:Content>

