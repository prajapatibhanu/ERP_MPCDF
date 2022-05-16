<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Next_Tanker_Required_CC_Rpt.aspx.cs" Inherits="mis_mcms_reports_Next_Tanker_Required_CC_Rpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <asp:ValidationSummary ID="vSummary" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Next Tanker Required Report At [CC] </h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">

                        <div class="col-md-12">
                            <div class="box-body" style="border: 1px solid #ced4da; padding: 10px 5px;">

                                <div class="row">

                                    <div class="col-md-3">
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
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Chilling Centre</label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvddlCCName3" runat="server" Display="Dynamic" ControlToValidate="ddlCCName3" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select CC Name!'></i>" ErrorMessage="Select CC Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="CT"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="input-group">
                                                <asp:DropDownList ID="ddlCCName3" style="width:200px;"  CssClass="form-control" Width="100%" runat="server">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>From Date</label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvCCWiseTankerSealReport" runat="server" Display="Dynamic" ControlToValidate="txtfromdate" Text="<i class='fa fa-exclamation-circle' title='Enter From Date!'></i>" ErrorMessage="Enter From Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="CT"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="input-group">
                                                <span class="input-group-addon">
                                                    <i class="glyphicon glyphicon-calendar"></i>
                                                </span>
                                                <asp:TextBox ID="txtfromdate" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>To Date</label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txtTodate" Text="<i class='fa fa-exclamation-circle' title='Enter To Date!'></i>" ErrorMessage="Enter To Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="CT"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="input-group">
                                                <span class="input-group-addon">
                                                    <i class="glyphicon glyphicon-calendar"></i>
                                                </span>
                                                <asp:TextBox ID="txtTodate" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                      <div class="col-md-1">
                                        <div class="form-group">
                                            <asp:Button ID="btnSearchRpt" CssClass="btn btn-secondary button-mini" Style="margin-top: 20px;" runat="server" Text="Search" OnClick="btnSearchRpt_Click" ValidationGroup="CT" />
                                        </div>
                                    </div>

                                </div>

                            </div>

                        </div>


                        <div class="col-md-12">
                            <div class="box-body" style="border: 1px solid #ced4da; padding: 10px 5px;">


                                <div class="table-responsive">
                                    <asp:GridView ID="gv_CCWiseNextTankerReport" runat="server" AutoGenerateColumns="false" ShowHeader="true" EmptyDataRowStyle-ForeColor="Red" CssClass="table table-bordered table-borderedLBrown" EmptyDataText="No Data Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Office Name">
                                                <ItemTemplate>
                                                    <%# Eval("Office_Name") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ChallanNo">
                                                <ItemTemplate>
                                                    <%# Eval("ChallanNo") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Tanker No.">
                                                <ItemTemplate>
                                                    <%# Eval("VehicleNo") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Dispatch Date">
                                                <ItemTemplate>
                                                    <%# Eval("DispatchDate") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="NextTanker Date">
                                                <ItemTemplate>
                                                    <%# Eval("NextTankerDate") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Tanker Capacity">
                                                <ItemTemplate>
                                                    <%# Eval("TankerCapacity") %>
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
        </section>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script>
        function ShowTankerSealDetails() {
            $('#pu_TankerSealAndSealLocationDetails').modal('show');
        }

    </script>
</asp:Content>
