<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DcsMilkCollectionRpt.aspx.cs" Inherits="mis_mcms_reports_DcsMilkCollectionRpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">

    <script type="text/javascript">
        function confirmation() {
            if (confirm('Are you sure you want to save rate chart on excel format ?')) {
                return true;
            } else {
                return false;
            }
        }
    </script>

    <style>
         .table2 > thead > tr > th, .table2 > tbody > tr > th, .table2 > tfoot > tr > th, .table2 > thead > tr > td, .table2 > tbody > tr > td, .table2 > tfoot > tr > td {
            padding: 6px;
            line-height: 1.42857143;
            vertical-align: top;
            border: 1px solid black;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">


    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />

    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Date Wise Milk Collection</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">

                    <fieldset>
                        <legend>Filter</legend>
                        <div class="row">
                            <div class="col-md-12">

                                <div class="col-md-3">
                                    <label>Society<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvFarmer" runat="server" Display="Dynamic" ValidationGroup="a" ControlToValidate="ddlSociety" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Society!'></i>" ErrorMessage="Select Society" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlSociety" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>


                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Date  </label>
                                        <span style="color: red">*</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtFdt" ErrorMessage="Please Enter From Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter From Date !'></i>"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revdate" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtFdt" ErrorMessage="Invalid From Date" Text="<i class='fa fa-exclamation-circle' title='Invalid From Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                        </span>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtFdt" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select From Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Shift</label>

                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlShift" CssClass="form-control select2" runat="server">
                                                <asp:ListItem Value="ALL">ALL</asp:ListItem>
                                                <asp:ListItem Value="Morning">Morning</asp:ListItem>
                                                <asp:ListItem Value="Evening">Evening</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
									<div class="col-md-2">
                                    <div class="form-group">
                                        <label>Entry Mode</label>

                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlEntryMode" CssClass="form-control select2" runat="server">
                                                <asp:ListItem Value="ALL">ALL</asp:ListItem>
                                                <asp:ListItem Value="Web">Web</asp:ListItem>
                                                <asp:ListItem Value="App">App</asp:ListItem>
                                                <asp:ListItem Value="PROMPT">PROMPT</asp:ListItem>
												<asp:ListItem Value="NDDB">NDDB</asp:ListItem>
												<asp:ListItem Value="REIL">REIL</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-1" style="margin-top: 20px;" runat="server">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click" ValidationGroup="a" ID="btnSubmit" Text="Search" AccessKey="S" />

                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </fieldset>

                    <asp:Label ID="lblmsgshow" Visible="false" runat="server"></asp:Label>

                    <fieldset runat="server" visible="false" id="FS_DailyReport">
                        <legend>Detail</legend>
                        <div class="row">
                            <div class="col-md-12">

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Society Name And Code </label>
                                        :
                                    <asp:Label ID="lblSociety" Font-Bold="true" runat="server"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Date & Shift </label>
                                        :
                                    <asp:Label ID="lbldateshift" Font-Bold="true" runat="server"></asp:Label>
                                    </div>
                                </div>




                            </div>
                        </div>
                        <hr />

                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Button ID="btnexporttoexcel" runat="server" Text="Export To Excel" OnClientClick="return confirmation();" CssClass="btn btn-primary btn-block" OnClick="btnexporttoexcel_Click" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">

                                <asp:GridView ID="gv_Milkcollectionrpt" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover"
                                    ShowFooter="true">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sr. No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmcid" runat="server" Text='<%#Container.DataItemIndex+1 %>' ToolTip='<%# Eval("I_CollectionID") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblmsg" Text="Total" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Producer Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProducerCardNo" runat="server" Text='<%# Eval("ProducerCardNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl1" Text="-" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Producer Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProducerName" runat="server" Text='<%# Eval("ProducerName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl2" Text="-" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_MilkTypes" runat="server" Text='<%# Eval("V_MilkType") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl3" Text="-" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk Quantity (In Ltr)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblI_MilkSupplyQty" runat="server" Text='<%# Eval("I_MilkSupplyQty") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblI_MilkSupplyQtyTotal" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Fat %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFat" runat="server" Text='<%# Eval("Fat") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotal_Fat" Text="-" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SNF %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("SNF") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblTotal_SNF" Text="-" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="CLR">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("CLR") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblTotal_CLR" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Rate Per Ltr">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRate_Per_Ltr" runat="server" Text='<%# Eval("Rate_Per_Ltr") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblmsgRate_Per_Ltr" Text="-" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Value (In INR)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblValue" runat="server" Text='<%# Eval("Value") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblNetValue" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                                <hr />

                                <asp:GridView ID="gv_milktypewiserpt" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover"
                                    ShowFooter="true">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sr. No.">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblmsg" Text="Total" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_MilkTypes" runat="server" Text='<%# Eval("V_MilkType") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl3" Text="-" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk Quantity (In Ltr)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblI_MilkSupplyQty_p" runat="server" Text='<%# Eval("I_MilkSupplyQty") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblI_MilkSupplyQtyTotal_P" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Producer Count">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_MilkType_Count" runat="server" Text='<%# Eval("V_MilkType_Count") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalV_MilkType_Count" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>



                                    </Columns>
                                </asp:GridView>

                                <hr />

                                <div class="row">
                                    <div class="col-lg-12">
                                        <div id="dvreport" runat="server"></div>
                                    </div>
                                </div>

                            </div>
                        </div>

                    </fieldset>


                </div>
            </div>

        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>
