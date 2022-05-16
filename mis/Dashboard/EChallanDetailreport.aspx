<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="EChallanDetailreport.aspx.cs" Inherits="mis_Dashboard_EChallanDetailreport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">E-Chaalan</h3>
                </div>
                <div class="col-lg-12">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <fieldset>
                        <legend>E-Chaalan Details report</legend>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Date</label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">
                                                <i class="far fa-calendar-alt"></i>
                                            </span>
                                        </div>
                                        <asp:TextBox ID="txtDate" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-danger" Style="margin-top: 19px;" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="table-responsive">

                                <asp:GridView ID="gv_viewreferenceno" ShowHeader="true" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSealNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Generate Gate Pass Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDT_TankerDispatchDate" runat="server" Text='<%# (Convert.ToDateTime(Eval("DT_CreatedOn"))).ToString("dd-MM-yyyy hh:mm:ss tt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reference No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblC_ReferenceNo" runat="server" Text='<%# Eval("C_ReferenceNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CC Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Challan No/Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblChallan_No" runat="server" Text='<%# Eval("Challan_No") %>'></asp:Label>
                                                <asp:Label ID="lbltcs" ForeColor="Red" Font-Bold="true" runat="server" Text='<%# Eval("Challan_No").ToString() == "" ? "Pending" : "" %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status Current">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" ToolTip='<%# Eval("RefCancelRemark") %>' runat="server" Text='<%# Eval("RefCancelStatus") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vehicle No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_VehicleNo" runat="server" Text='<%# Eval("V_VehicleNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Driver Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_DriverName" runat="server" Text='<%# Eval("V_DriverName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Driver Mobile No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_DriverMobileNo" runat="server" Text='<%# Eval("V_DriverMobileNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <a href='../MilkCollection/GatePassReferenceDetails.aspx?Rid=<%# new APIProcedure().Encrypt(Eval("BI_MilkInOutRefID").ToString()) %>' target="_blank" title="Print Gate Pass"><i class="fa fa-print"></i></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
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

