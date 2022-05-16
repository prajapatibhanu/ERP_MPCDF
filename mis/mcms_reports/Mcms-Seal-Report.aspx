<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Mcms-Seal-Report.aspx.cs" Inherits="mis_mcms_reports_Mcms_Seal_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <asp:ValidationSummary ID="vSummary" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Tanker Wise Seal Report [DS] </h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">

                        <div class="col-md-6">
                            <div class="box-body" style="border: 1px solid #ced4da; padding: 10px 5px;">
 
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

                            </div>

                        </div>


                        <div class="col-md-6">
                            <div class="box-body" style="border: 1px solid #ced4da; padding: 10px 5px;">

                               
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
            <%--Confirmation Modal Start --%>
            <div class="modal fade" id="pu_TankerSealAndSealLocationDetails" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog" style="width: 550px;">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="myModalLabeldf"><span style="font-size: medium;">Challan No. :</span>
                                <asp:Label ID="lblVehicleNo" ForeColor="White" Font-Size="Medium" Font-Bold="true" runat="server"></asp:Label>

                                <span style="font-size: medium;">Chamber Type :</span>
                                <asp:Label ID="lblctype" ForeColor="White" Font-Size="Medium" Font-Bold="true" runat="server"></asp:Label>

                                <span style="font-size: medium;">CC Count :</span>
                                <asp:Label ID="lblcccount" ForeColor="White" Font-Size="Medium" Font-Bold="true" runat="server"></asp:Label>

                            </h5>
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
                                    <h5 style="margin-top: 10px;">At Dugdh Sangh (Seal Display When Process Is Completed)</h5>
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
