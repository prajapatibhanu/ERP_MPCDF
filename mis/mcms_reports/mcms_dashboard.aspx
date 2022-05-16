<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="mcms_dashboard.aspx.cs" Inherits="mis_mcms_reports_mcms_dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .highlight {
            background-color: yellow;
            font: bold;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-Manish">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <div class="box-header">
                    <h3 class="box-title">Dashboard</h3>
                    <asp:ValidationSummary ID="vSummary" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="box-body" style="border: 1px solid #ced4da; padding: 10px 5px;">
                            <div class="fancy-title  title-dotted-border">
                                <h5>Overall QC Report</h5>
                            </div>

                            <hr />
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Report Date</label>
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="glyphicon glyphicon-calendar"></i>
                                            </span>
                                            <asp:TextBox ID="txtOverallReportDate" AutoPostBack="true" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server" OnTextChanged="txtOverallReportDate_TextChanged"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" ID="gvDailyMilkCollection" CssClass="table table-bordered table-borderedBlue" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" EmptyDataText="No Record Found." OnRowCommand="gvDailyMilkCollection_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="Office_Name" HeaderText="Dugdh Sangh" />
                                                <%--<asp:BoundField DataField="DT_ReportDate" HeaderText="Report Date" />--%>
                                                <asp:TemplateField HeaderText="Reported CC" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnView" runat="server" ToolTip="View Reported Unit Name" CssClass="label label-info" CommandName="ViewData" CommandArgument='<%# Eval("OfficeID") %>' Text='<%# Eval("DispatchCCCount") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="I_MilkQuantityCC" HeaderText="Milk Dispatched from CC" />
                                                <asp:BoundField DataField="D_FATCC" HeaderText="FAT % at CC" />
                                                <asp:BoundField DataField="D_SNFCC" HeaderText="SNF % at CC" />

                                                <asp:BoundField DataField="I_MilkQuantityDS" HeaderText="Milk Received at DS" />
                                                <asp:BoundField DataField="D_FATDS" HeaderText="FAT % at DS" />
                                                <asp:BoundField DataField="D_SNFDS" HeaderText="SNF % at DS" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-6">
                        <div class="box-body" style="border: 1px solid #ced4da; padding: 10px 5px;">
                            <div class="fancy-title  title-dotted-border">
                                <h5>DS Wise QC Report</h5>
                            </div>

                            <hr />
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Date</label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvtxtDSWiseQCDate" runat="server" Display="Dynamic" ControlToValidate="txtDSWiseQCDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="DS"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="glyphicon glyphicon-calendar"></i>
                                            </span>
                                            <asp:TextBox ID="txtDSWiseQCDate" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Dugdh Sangh</label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="ddlDSName2" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select DS Name!'></i>" ErrorMessage="Select DS Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="DS"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="input-group">

                                            <asp:DropDownList ID="ddlDSName2" Width="100%" OnInit="ddlDSName_Init" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:Button ID="btnDSWiseQCReport" CssClass="btn btn-secondary button-mini" Style="margin-top: 20px;" runat="server" Text="Search" OnClick="btnDSWiseQCReport_Click" ValidationGroup="DS" />
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="pnldisplay" runat="server">
                                <%-- <div class="col-md-10">
                                    <div class="col-md-2">
                                        <input type="button" id="btnExport" class="btn btn-secondary button-mini" value="PDF" onclick="ExportCCWiseQCReport()" />
                                    </div>
                                </div>--%>
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <table class="table table-bordered table-borderedOrange" id="tblccwiseqcreport">

                                            <tr>
                                                <th rowspan="2" style="text-align: center; vertical-align: middle;">Particular</th>
                                                <th colspan="3" style="text-align: center;">From CC</th>
                                                <th colspan="3" style="text-align: center;">At DS</th>
                                                <th colspan="3" style="text-align: center;">Difference</th>
                                            </tr>
                                            <tr>
                                                <th>Front</th>
                                                <th>Rear</th>
                                                <th>Single</th>
                                                <th>Front</th>
                                                <th>Rear</th>
                                                <th>Single</th>
                                                <th>Front</th>
                                                <th>Rear</th>
                                                <th>Single</th>
                                            </tr>
                                            <tr>
                                                <td class="auto-style12">Milk Quantity</td>
                                                <td>
                                                    <asp:Label ID="lblMilkQuantityDugdhSangh_CC_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQuantityDugdhSangh_CC_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQuantityDugdhSangh_CC_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQuantityDugdhSangh_DS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQuantityDugdhSangh_DS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQuantityDugdhSangh_DS_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQuantityDugdhSangh_CCDS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQuantityDugdhSangh_CCDS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQuantityDugdhSangh_CCDS_S" Text="-" runat="server"></asp:Label></td>
                                            </tr>

                                            <tr>
                                                <td class="auto-style12">FAT (%)</td>
                                                <td>
                                                    <asp:Label ID="lblMilkFATDugdhSangh_CC_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkFATDugdhSangh_CC_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkFATDugdhSangh_CC_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkFATDugdhSangh_DS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkFATDugdhSangh_DS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkFATDugdhSangh_DS_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkFATDugdhSangh_CCDS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkFATDugdhSangh_CCDS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkFATDugdhSangh_CCDS_S" Text="-" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style12">SNF (%)</td>
                                                <td>
                                                    <asp:Label ID="lblMilkSNFDugdhSangh_CC_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkSNFDugdhSangh_CC_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkSNFDugdhSangh_CC_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkSNFDugdhSangh_DS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkSNFDugdhSangh_DS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkSNFDugdhSangh_DS_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkSNFDugdhSangh_CCDS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkSNFDugdhSangh_CCDS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkSNFDugdhSangh_CCDS_S" Text="-" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style12">CLR<span style="mso-spacerun: yes">&nbsp;</span></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCLRDugdhSangh_CC_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCLRDugdhSangh_CC_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCLRDugdhSangh_CC_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCLRDugdhSangh_DS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCLRDugdhSangh_DS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCLRDugdhSangh_DS_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCLRDugdhSangh_CCDS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCLRDugdhSangh_CCDS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCLRDugdhSangh_CCDS_S" Text="-" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style21">Temp</td>
                                                <td>
                                                    <asp:Label ID="lblMilkTempDugdhSangh_CC_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkTempDugdhSangh_CC_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkTempDugdhSangh_CC_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkTempDugdhSangh_DS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkTempDugdhSangh_DS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkTempDugdhSangh_DS_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkTempDugdhSangh_CCDS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkTempDugdhSangh_CCDS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkTempDugdhSangh_CCDS_S" Text="-" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style12">Acidity (%)</td>
                                                <td>
                                                    <asp:Label ID="lblMilkAcidityDugdhSangh_CC_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkAcidityDugdhSangh_CC_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkAcidityDugdhSangh_CC_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkAcidityDugdhSangh_DS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkAcidityDugdhSangh_DS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkAcidityDugdhSangh_DS_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkAcidityDugdhSangh_CCDS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkAcidityDugdhSangh_CCDS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkAcidityDugdhSangh_CCDS_S" Text="-" runat="server"></asp:Label></td>
                                            </tr>

                                            <tr>
                                                <td class="auto-style12">MBRT</td>
                                                <td>
                                                    <asp:Label ID="lblMilkMBRTDugdhSangh_CC_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkMBRTDugdhSangh_CC_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkMBRTDugdhSangh_CC_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkMBRTDugdhSangh_DS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkMBRTDugdhSangh_DS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkMBRTDugdhSangh_DS_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkMBRTDugdhSangh_CCDS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkMBRTDugdhSangh_CCDS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkMBRTDugdhSangh_CCDS_S" Text="-" runat="server"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="box-body" style="border: 1px solid #ced4da; padding: 10px 5px;">
                            <div class="fancy-title  title-dotted-border">
                                <h5>CC Wise QC Report</h5>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Date</label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvCCWiseQCDate" runat="server" Display="Dynamic" ControlToValidate="txtCCWiseQCDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="CC"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="glyphicon glyphicon-calendar"></i>
                                            </span>
                                            <asp:TextBox ID="txtCCWiseQCDate" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Dugdh Sangh</label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvDSName1" runat="server" Display="Dynamic" ControlToValidate="ddlDSName1" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select DS Name!'></i>" ErrorMessage="Select DS Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="CC"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="input-group">

                                            <asp:DropDownList ID="ddlDSName1" Width="100%" AutoPostBack="true" OnInit="ddlDSName_Init" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlDSName1_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Chilling Centre</label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvCCName1" runat="server" Display="Dynamic" ControlToValidate="ddlCCName1" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select CC Name!'></i>" ErrorMessage="Select CC Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="CC"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="input-group">

                                            <asp:DropDownList ID="ddlCCName1" Width="100%" CssClass="form-control" runat="server">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:Button ID="btnCCWiseQCReport" CssClass="btn btn-secondary button-mini" Style="margin-top: 20px;" runat="server" Text="Search" OnClick="btnCCWiseQCReport_Click" ValidationGroup="CC" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <table class="table table-bordered table-borderedGreen">
                                            <tr>
                                                <th rowspan="2" style="text-align: center; vertical-align: middle;">Particular</th>
                                                <th colspan="3" style="text-align: center;">From CC</th>
                                                <th colspan="3" style="text-align: center;">At DS</th>
                                                <th colspan="3" style="text-align: center;">Difference</th>
                                            </tr>
                                            <tr>
                                                <th>Front</th>
                                                <th>Rear</th>
                                                <th>Single</th>
                                                <th>Front</th>
                                                <th>Rear</th>
                                                <th>Single</th>
                                                <th>Front</th>
                                                <th>Rear</th>
                                                <th>Single</th>
                                            </tr>
                                            <tr>
                                                <td class="auto-style12">Milk Quantity</td>
                                                <td>
                                                    <asp:Label ID="lblMilkQuantityChillingCentre_CC_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQuantityChillingCentre_CC_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQuantityChillingCentre_CC_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQuantityChillingCentre_DS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQuantityChillingCentre_DS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQuantityChillingCentre_DS_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQuantityChillingCentre_CCDS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQuantityChillingCentre_CCDS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQuantityChillingCentre_CCDS_S" Text="-" runat="server"></asp:Label></td>
                                            </tr>

                                            <tr>
                                                <td class="auto-style12">FAT (%)</td>
                                                <td>
                                                    <asp:Label ID="lblMilkFATChillingCentre_CC_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkFATChillingCentre_CC_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkFATChillingCentre_CC_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkFATChillingCentre_DS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkFATChillingCentre_DS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkFATChillingCentre_DS_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkFATChillingCentre_CCDS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkFATChillingCentre_CCDS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkFATChillingCentre_CCDS_S" Text="-" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style12">SNF (%)</td>
                                                <td>
                                                    <asp:Label ID="lblMilkSNFChillingCentre_CC_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkSNFChillingCentre_CC_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkSNFChillingCentre_CC_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkSNFChillingCentre_DS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkSNFChillingCentre_DS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkSNFChillingCentre_DS_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkSNFChillingCentre_CCDS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkSNFChillingCentre_CCDS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkSNFChillingCentre_CCDS_S" Text="-" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style12">CLR<span style="mso-spacerun: yes">&nbsp;</span></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCLRChillingCentre_CC_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCLRChillingCentre_CC_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCLRChillingCentre_CC_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCLRChillingCentre_DS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCLRChillingCentre_DS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCLRChillingCentre_DS_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCLRChillingCentre_CCDS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCLRChillingCentre_CCDS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCLRChillingCentre_CCDS_S" Text="-" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style21">Temp</td>
                                                <td>
                                                    <asp:Label ID="lblMilkTempChillingCentre_CC_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkTempChillingCentre_CC_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkTempChillingCentre_CC_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkTempChillingCentre_DS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkTempChillingCentre_DS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkTempChillingCentre_DS_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkTempChillingCentre_CCDS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkTempChillingCentre_CCDS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkTempChillingCentre_CCDS_S" Text="-" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style12">Acidity (%)</td>
                                                <td>
                                                    <asp:Label ID="lblMilkAcidityChillingCentre_CC_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkAcidityChillingCentre_CC_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkAcidityChillingCentre_CC_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkAcidityChillingCentre_DS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkAcidityChillingCentre_DS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkAcidityChillingCentre_DS_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkAcidityChillingCentre_CCDS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkAcidityChillingCentre_CCDS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkAcidityChillingCentre_CCDS_S" Text="-" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style12">MBRT</td>
                                                <td>
                                                    <asp:Label ID="lblMilkMBRTChillingCentre_CC_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkMBRTChillingCentre_CC_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkMBRTChillingCentre_CC_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkMBRTChillingCentre_DS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkMBRTChillingCentre_DS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkMBRTChillingCentre_DS_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkMBRTChillingCentre_CCDS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkMBRTChillingCentre_CCDS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkMBRTChillingCentre_CCDS_S" Text="-" runat="server"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-6">
                        <div class="box-body" style="border: 1px solid #ced4da; padding: 10px 5px;">
                            <div class="fancy-title  title-dotted-border">
                                <h5>Tanker Wise QC Report</h5>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Date</label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvDateTankerWiseQCReport" runat="server" Display="Dynamic" ControlToValidate="txtDateTankerWiseQCReport" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="glyphicon glyphicon-calendar"></i>
                                            </span>
                                            <asp:TextBox ID="txtDateTankerWiseQCReport" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Dugdh Sangh</label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvDSName" runat="server" Display="Dynamic" ControlToValidate="ddlDSName" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select DS Name!'></i>" ErrorMessage="Select DS Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="input-group">

                                            <asp:DropDownList ID="ddlDSName" Width="100%" AutoPostBack="true" OnInit="ddlDSName_Init" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlDSName_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Chilling Centre</label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvCCName" runat="server" Display="Dynamic" ControlToValidate="ddlCCName" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select CC Name!'></i>" ErrorMessage="Select CC Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="input-group">

                                            <asp:DropDownList ID="ddlCCName" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCCName_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Tanker No.</label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvTankerNo" runat="server" Display="Dynamic" ControlToValidate="ddlTankerNo" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Tanker No.!'></i>" ErrorMessage="Select Tanker No." SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="input-group">

                                            <asp:DropDownList ID="ddlTankerNo" CssClass="form-control" Width="100%" runat="server">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:Button ID="btnSearchTankerWiseReport" CssClass="btn btn-secondary button-mini" Style="margin-top: 20px;" runat="server" Text="Search" OnClick="btnSearchTankerWiseReport_Click" ValidationGroup="Save" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <table class="table table-bordered table-borderedLBlue">

                                            <tr>
                                                <th rowspan="2" style="vertical-align: middle; text-align: center;">Particular</th>
                                                <th colspan="3" style="text-align: center;">From CC</th>
                                                <th colspan="3" style="text-align: center;">At DS</th>
                                                <th colspan="3" style="text-align: center;">Difference</th>
                                            </tr>
                                            <tr>
                                                <th>Front</th>
                                                <th>Rear</th>
                                                <th>Single</th>
                                                <th>Front</th>
                                                <th>Rear</th>
                                                <th>Single</th>
                                                <th>Front</th>
                                                <th>Rear</th>
                                                <th>Single</th>
                                            </tr>
                                            <tr>
                                                <td class="auto-style12">Milk Quantity</td>
                                                <td>
                                                    <asp:Label ID="lblMilkQuantityTanker_CC_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQuantityTanker_CC_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQuantityTanker_CC_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQuantityTanker_DS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQuantityTanker_DS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQuantityTanker_DS_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQuantityTanker_CCDS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQuantityTanker_CCDS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQuantityTanker_CCDS_S" Text="-" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style12">Milk Quality</td>
                                                <td>
                                                    <asp:Label ID="lblMilkQualityTanker_CC_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQualityTanker_CC_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQualityTanker_CC_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQualityTanker_DS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQualityTanker_DS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQualityTanker_DS_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQualityTanker_CCDS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQualityTanker_CCDS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkQualityTanker_CCDS_S" Text="-" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style12">FAT (%)</td>
                                                <td>
                                                    <asp:Label ID="lblMilkFATTanker_CC_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkFATTanker_CC_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkFATTanker_CC_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkFATTanker_DS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkFATTanker_DS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkFATTanker_DS_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkFATTanker_CCDS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkFATTanker_CCDS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkFATTanker_CCDS_S" Text="-" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style12">SNF (%)</td>
                                                <td>
                                                    <asp:Label ID="lblMilkSNFTanker_CC_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkSNFTanker_CC_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkSNFTanker_CC_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkSNFTanker_DS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkSNFTanker_DS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkSNFTanker_DS_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkSNFTanker_CCDS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkSNFTanker_CCDS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkSNFTanker_CCDS_S" Text="-" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style12">CLR<span style="mso-spacerun: yes">&nbsp;</span></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCLRTanker_CC_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCLRTanker_CC_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCLRTanker_CC_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCLRTanker_DS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCLRTanker_DS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCLRTanker_DS_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCLRTanker_CCDS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCLRTanker_CCDS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCLRTanker_CCDS_S" Text="-" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style21">Temp</td>
                                                <td>
                                                    <asp:Label ID="lblMilkTempTanker_CC_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkTempTanker_CC_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkTempTanker_CC_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkTempTanker_DS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkTempTanker_DS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkTempTanker_DS_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkTempTanker_CCDS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkTempTanker_CCDS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkTempTanker_CCDS_S" Text="-" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style12">Acidity (%)</td>
                                                <td>
                                                    <asp:Label ID="lblMilkAcidityTanker_CC_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkAcidityTanker_CC_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkAcidityTanker_CC_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkAcidityTanker_DS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkAcidityTanker_DS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkAcidityTanker_DS_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkAcidityTanker_CCDS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkAcidityTanker_CCDS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkAcidityTanker_CCDS_S" Text="-" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style12">COB</td>
                                                <td>
                                                    <asp:Label ID="lblMilkCOBTanker_CC_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCOBTanker_CC_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCOBTanker_CC_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCOBTanker_DS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCOBTanker_DS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCOBTanker_DS_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCOBTanker_CCDS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCOBTanker_CCDS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkCOBTanker_CCDS_S" Text="-" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style12">MBRT</td>
                                                <td>
                                                    <asp:Label ID="lblMilkMBRTTanker_CC_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkMBRTTanker_CC_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkMBRTTanker_CC_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkMBRTTanker_DS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkMBRTTanker_DS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkMBRTTanker_DS_S" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkMBRTTanker_CCDS_F" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkMBRTTanker_CCDS_R" Text="-" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblMilkMBRTTanker_CCDS_S" Text="-" runat="server"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="box-body" style="border: 1px solid #ced4da; padding: 10px 5px;">
                            <div class="fancy-title  title-dotted-border">
                                <h5>CC Wise Tanker Seal Report</h5>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Date</label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvCCWiseTankerSealReport" runat="server" Display="Dynamic" ControlToValidate="txtCCWiseTankerSealReport" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="CT"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="glyphicon glyphicon-calendar"></i>
                                            </span>
                                            <asp:TextBox ID="txtCCWiseTankerSealReport" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Dugdh Sangh</label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvDSName3" runat="server" Display="Dynamic" ControlToValidate="ddlDSName3" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select DS Name!'></i>" ErrorMessage="Select DS Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="CT"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="input-group">

                                            <asp:DropDownList ID="ddlDSName3" AutoPostBack="true" Width="100%" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlDSName3_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Chilling Centre</label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvddlCCName3" runat="server" Display="Dynamic" ControlToValidate="ddlCCName3" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select CC Name!'></i>" ErrorMessage="Select CC Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="CT"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="input-group">

                                            <asp:DropDownList ID="ddlCCName3" CssClass="form-control" Width="100%" runat="server">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:Button ID="btnCCWiseTankerQCReport" CssClass="btn btn-secondary button-mini" Style="margin-top: 20px;" runat="server" Text="Search" OnClick="btnCCWiseTankerQCReport_Click" ValidationGroup="CT" />
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gv_CCWiseTankerSealReport" runat="server" AutoGenerateColumns="false" ShowHeader="true" EmptyDataRowStyle-ForeColor="Red" CssClass="table table-bordered table-borderedLBrown" EmptyDataText="No Data Found" OnRowCommand="GridView1_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tanker No.">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnTankerNo" ToolTip="Click for More Info." CssClass="label label-warning" runat="server" Text='<%# Eval("V_VehicleNo") + " [ " + Eval("V_ReferenceCode") + " ]" %>' CommandName="ShowTankerSealDetails" CommandArgument='<%# Eval("V_ReferenceCode") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Seal Count at CC">
                                                    <ItemTemplate>
                                                        <%# Eval("SealCountCC") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Seal Count at DS">
                                                    <ItemTemplate>
                                                        <%# Eval("SealCountDS") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Difference">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSealCountDiff" Style='<%# "color:" + Eval("color").ToString() %>' Font-Bold="true" runat="server" Text='<%# Eval("SealCountDiff") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
            </div>
            <%--Confirmation Modal Start --%>
            <div class="modal fade" id="pu_TankerSealAndSealLocationDetails" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog" style="width: 360px;">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="myModalLabeldf"><span style="font-size: medium;">Challan No. :</span>
                                <asp:Label ID="lblVehicleNo" ForeColor="White" Font-Size="Medium" Font-Bold="true" runat="server"></asp:Label></h5>
                            <button type="button" class="close pull-right" data-dismiss="modal">
                                <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                            </button>

                        </div>
                        <div class="clearfix"></div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <h5>At Chilling Centre</h5>
                                </div>
                                <div class="col-md-12">
                                    <asp:GridView ID="gvTankerSealDetailsForCC" runat="server" Width="100%" AutoGenerateColumns="false" EmptyDataRowStyle-ForeColor="Red" CssClass="table table-bordered" EmptyDataText="No Data Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="V_SealNoCC" HeaderText="Seal No." />
                                            <asp:BoundField DataField="V_SealLocationCC" HeaderText="Seal Location" />
                                            <asp:BoundField DataField="V_SealColor" HeaderText="Seal Color" />
                                            <asp:BoundField DataField="V_SealRemarkCC" HeaderText="Seal Remark" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-md-12 text-center">
                                    <h5 style="margin-top: 10px;">At Dugdh Sangh</h5>
                                </div>
                                <div class="col-md-12">
                                    <asp:GridView ID="gvTankerSealDetailsForDS" runat="server" CssClass="table table-bordered" Width="100%" AutoGenerateColumns="false" EmptyDataRowStyle-ForeColor="Red" EmptyDataText="No Data Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="V_SealNoDS" HeaderText="Seal No." />
                                            <asp:BoundField DataField="V_SealLocationDS" HeaderText="Seal Location" />
                                            <asp:BoundField DataField="V_SealColor" HeaderText="Seal Color" />
                                            <asp:BoundField DataField="V_SealRemarkDS" HeaderText="Seal Remark" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
            <%--ConfirmationModal End --%>

            <%--Confirmation Modal Start --%>
            <div class="modal fade" id="pu_ReportedCC" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="myModalLabel"><span style="font-size: medium;">Dugdh Sangh :</span>
                                <asp:Label ID="lblUnitName" ForeColor="White" ClientIDMode="Static" Font-Size="Medium" Font-Bold="true" runat="server"></asp:Label></h5>
                            <button type="button" class="close pull-right" data-dismiss="modal">
                                <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                            </button>

                        </div>
                        <div class="clearfix"></div>
                        <div class="box-body" style="max-height: 420px; overflow: scroll;">
                            <div class="row">
                                <div class="col-md-12">
                                    <h5>Reported Chilling Centre Details
                                        <asp:Button runat="server" ID="btnPrint" Text="Print (Alt + P)" CssClass="btn btn-default pull-right" AccessKey="p" OnClientClick="printdiv('div_print');" /></h5>
                                    <br />
                                </div>
                                <div class="col-md-12">
                                    <div id="div_print">
                                        <%--<div id="dv_headerDetails" runat="server"></div>--%>
                                        <asp:GridView ID="gv_ReportedCC" runat="server" Width="100%" AutoGenerateColumns="false" EmptyDataRowStyle-ForeColor="Red" CssClass="table table-bordered" EmptyDataText="No Data Found">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Convert.ToInt32(Eval("Status")) > 0 ? "Received" : "Pending" %>' CssClass='<%# Convert.ToInt32(Eval("Status")) > 0 ? "label label-success" : "label label-warning" %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Chilling Centre">
                                                    <ItemTemplate>
                                                        <%# Eval("Office_Name") + " [ " + Eval("V_ReferenceCode") + " ]" %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dispatch Quantity (In KG)">
                                                    <ItemTemplate>
                                                        <%# Eval("I_MilkQuantity") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Received Quantity (In KG)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" CssClass='<%# Convert.ToDecimal(Eval("Rec_MilkQuantity")) > Convert.ToDecimal(Eval("I_MilkQuantity")) || Convert.ToDecimal(Eval("Rec_MilkQuantity")) < Convert.ToDecimal(Eval("I_MilkQuantity")) && Eval("Rec_MilkQuantity").ToString() != "0.00" ? "highlight" : "" %>' Text='<%# Eval("Rec_MilkQuantity") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Difference (DS - CC)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDiff" runat="server" CssClass='<%# Convert.ToDecimal(Eval("Rec_MilkQuantity")) > Convert.ToDecimal(Eval("I_MilkQuantity")) || Convert.ToDecimal(Eval("Rec_MilkQuantity")) < Convert.ToDecimal(Eval("I_MilkQuantity")) && Eval("Rec_MilkQuantity").ToString() != "0.00" ? "highlight" : "" %>' Text='<%# Eval("Rec_MilkQuantity").ToString() == "0.00" ? "-" : (Convert.ToDecimal(Eval("Rec_MilkQuantity").ToString()) - Convert.ToDecimal(Eval("I_MilkQuantity").ToString())).ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Milk Received Date">
                                                    <ItemTemplate>
                                                         <%# Eval("DT_Date", "{0:dd-MMM-yyyy hh:mm tt}") %>
                                                        </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
            <%--ConfirmationModal End --%>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script>
        function ShowTankerSealDetails() {
            $('#pu_TankerSealAndSealLocationDetails').modal('show');
        }

        function ShowReportedCC() {
            $('#pu_ReportedCC').modal('show');
        }


    </script>

    <script lang="javascript">
        function printdiv(printpage) {
            var headstr = "<html><head><title>Reported Chilling Centre Details</title></head><body>";
            var footstr = "</body>";
            var newstr = "<center><h5>" + document.getElementById('<%= lblUnitName.ClientID %>').textContent + "</h5></center><br/>" + document.all.item(printpage).innerHTML;
            var oldstr = document.body.innerHTML;
            document.body.innerHTML = headstr + newstr + footstr;
            window.print();
            document.body.innerHTML = oldstr;
            return false;
        }
    </script>
</asp:Content>

