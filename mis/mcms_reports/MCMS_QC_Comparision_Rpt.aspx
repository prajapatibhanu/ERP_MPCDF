<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MCMS_QC_Comparision_Rpt.aspx.cs" Inherits="mis_mcms_reports_MCMS_QC_Comparision_Rpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">

    <script type="text/javascript">
        function confirmation() {
            if (confirm('Are you sure you want to save QC Comparision Report on excel format ?')) {
                return true;
            } else {
                return false;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <asp:ValidationSummary ID="vSummary" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="CT" />
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">QC Comparision Report </h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">

                            <div class="col-md-2">
                                <label>Dugdh Sangh</label><span style="color: red;"> *</span>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvDSName3" runat="server" Display="Dynamic" ControlToValidate="ddlDSName3" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select DS Name!'></i>" ErrorMessage="Select DS Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="CT"></asp:RequiredFieldValidator>
                                </span>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlDSName3" AutoPostBack="true" CssClass="form-control select2" runat="server" OnSelectedIndexChanged="ddlDSName3_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-2">
                                <label>Chilling Centre</label><span style="color: red;"> *</span>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlCCName3" CssClass="form-control select2" runat="server">
                                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>From Date  </label>
                                    <span style="color: red">*</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfv1" runat="server" ValidationGroup="CT" Display="Dynamic" ControlToValidate="txtFdt" ErrorMessage="Please Enter From Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter From Date !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revdate" ValidationGroup="CT" runat="server" Display="Dynamic" ControlToValidate="txtFdt" ErrorMessage="Invalid From Date" Text="<i class='fa fa-exclamation-circle' title='Invalid From Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtFdt" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select From Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>To Date  </label>
                                    <span style="color: red">*</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="CT" Display="Dynamic" ControlToValidate="txtTdt" ErrorMessage="Please Enter To Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter To Date !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="CT" runat="server" Display="Dynamic" ControlToValidate="txtTdt" ErrorMessage="Invalid To Date" Text="<i class='fa fa-exclamation-circle' title='Invalid To Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtTdt" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select To Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-1">
                                <div class="form-group">
                                    <asp:Button ID="btnCCWiseTankerQCReport" CssClass="btn btn-secondary button-mini" Style="margin-top: 20px;" runat="server" Text="Search" OnClick="btnCCWiseTankerQCReport_Click" ValidationGroup="CT" />
                                </div>
                            </div>

                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" runat="server" Text="Export To Excel" OnClientClick="return confirmation();" CssClass="btn btn-primary btn-block" OnClick="btnSave_Click" />
                            </div>
                        </div>
                        <hr />

                        <div class="col-md-12">



                            <div class="table-responsive">

                                <asp:GridView ID="gv_CCWiseTankerSealReport" runat="server" AutoGenerateColumns="false" ShowHeader="true" EmptyDataRowStyle-ForeColor="Red" CssClass="table table-bordered table-borderedLBrown" EmptyDataText="No Data Found">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CC_NAME">
                                            <ItemTemplate>
                                                <%# Eval("CC_NAME") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MilkDispatchDate">
                                            <ItemTemplate>
                                                <%# (Convert.ToDateTime(Eval("MilkDispatchDate"))).ToString("dd-MM-yyyy") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="DispatchTime">
                                            <ItemTemplate>
                                                <%# Eval("DispatchTime") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="VehicleNo">
                                            <ItemTemplate>
                                                <%# Eval("VehicleNo") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ChamberType">
                                            <ItemTemplate>
                                                <%# Eval("ChamberType").ToString() == "S" ? "Single" : Eval("ChamberType").ToString() == "F" ? "Front" : "Rear" %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ReferenceNo">
                                            <ItemTemplate>
                                                <%# Eval("ReferenceNo") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ChallanNo">
                                            <ItemTemplate>
                                                <%# Eval("ChallanNo") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MilkDispatchQtyInKg">
                                            <ItemTemplate>
                                                <%# Eval("MilkDispatchQtyInKg") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

										<asp:TemplateField HeaderText="MilkDispatchQuality">
                                            <ItemTemplate>
                                                <%# Eval("V_MilkQualityCC") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FAT_CC">
                                            <ItemTemplate>
                                                <%# Eval("FAT_CC") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SNF_CC">
                                            <ItemTemplate>
                                                <%# Eval("SNF_CC") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CLR_CC">
                                            <ItemTemplate>
                                                <%# Eval("CLR_CC") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Temp_CC">
                                            <ItemTemplate>
                                                <%# Eval("Temp_CC") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Acidity_CC">
                                            <ItemTemplate>
                                                <%# Eval("Acidity_CC") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="ArrivalDate">
                                            <ItemTemplate>
                                                <%# Eval("ArrivalDate").ToString() == "" ? "" : (Convert.ToDateTime(Eval("ArrivalDate"))).ToString("dd-MM-yyyy") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ArrivalTime">
                                            <ItemTemplate>
                                                <%# Eval("ArrivalTime") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MilkReceivedQtyInKg">
                                            <ItemTemplate>
                                                <%# Eval("MilkReceivedQtyInKg") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
									<asp:TemplateField HeaderText="MilkReceivedQuality">
                                            <ItemTemplate>
                                                <%# Eval("V_MilkQualityDS") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FAT_DS">
                                            <ItemTemplate>
                                                <%# Eval("FAT_DS") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SNF_DS">
                                            <ItemTemplate>
                                                <%# Eval("SNF_DS") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CLR_DS">
                                            <ItemTemplate>
                                                <%# Eval("CLR_DS") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Acidity_DS">
                                            <ItemTemplate>
                                                <%# Eval("Acidity_DS") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk_Diff">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMilk_Diff" runat="server" Style="padding: 3px; border-radius: 3px; font-size: 12px;" CssClass='<%# Convert.ToDecimal(Eval("Milk_Diff")) >= 0 ? "label label-success" : "label label-warning" %>' Text='<%# Eval("Milk_Diff") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Fat_Diff">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFat_Diff" runat="server" Style="padding: 3px; border-radius: 3px; font-size: 12px;" CssClass='<%# Convert.ToDecimal(Eval("Fat_Diff")) >= 0 ? "label label-success" : "label label-warning" %>' Text='<%# Eval("Fat_Diff") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SNF_Diff">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF_Diff" runat="server" Style="padding: 3px; border-radius: 3px; font-size: 12px;" CssClass='<%# Convert.ToDecimal(Eval("SNF_Diff")) >= 0 ? "label label-success" : "label label-warning" %>' Text='<%# Eval("SNF_Diff") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CLR_Diff">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCLR_Diff" runat="server" Style="padding: 3px; border-radius: 3px; font-size: 12px;" CssClass='<%# Convert.ToDecimal(Eval("CLR_Diff")) >= 0 ? "label label-success" : "label label-warning" %>' Text='<%# Eval("CLR_Diff") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Temp_Diff">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTemp_Diff" runat="server" Style="padding: 3px; border-radius: 3px; font-size: 12px;" CssClass='<%# Convert.ToDecimal(Eval("Temp_Diff")) >= 0 ? "label label-success" : "label label-warning" %>' Text='<%# Eval("Temp_Diff") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Acidity_Diff">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAcidity_Diff" runat="server" Style="padding: 3px; border-radius: 3px; font-size: 12px;" CssClass='<%# Convert.ToDecimal(Eval("Acidity_Diff")) > 0 ? "label label-success" : "label label-warning" %>' Text='<%# Eval("Acidity_Diff") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="TotalSeal_CC">
                                            <ItemTemplate>
                                                <%# Eval("TotalSeal_CC") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="TotalSeal_DS">
                                            <ItemTemplate>
                                                <%# Eval("TotalSeal_DS") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Seal_Diff">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMilk_Diff" runat="server" Style="padding: 3px; border-radius: 3px; font-size: 12px;" CssClass='<%# Eval("Seal_Diff").ToString() == "0" ? "label label-success" : "label label-warning" %>' Text='<%# Eval("Seal_Diff") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SealStatus">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSealStatus" runat="server" Style="padding: 3px; border-radius: 3px; font-size: 12px;" CssClass='<%# Eval("SealStatus").ToString() == "OK" ? "label label-success" : "label label-warning" %>' Text='<%# Eval("SealStatus") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Time Taken In H">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTimeTaken" runat="server" Style="padding: 3px; border-radius: 3px; font-size: 12px;" Text='<%# Eval("TimeTaken") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

        </section>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

