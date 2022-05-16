<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DistWiseProductCrate_Rpt.aspx.cs" Inherits="mis_Demand_DistWiseProductCrate_Rpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Crate Summary Report</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <fieldset>
                        <legend>DATE,SHIFT</legend>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>From Date<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                        ErrorMessage="Enter From Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter From Date !'></i>"
                                        ControlToValidate="txtFromDate" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtFromDate"
                                        ErrorMessage="Invalid From Date" Text="<i class='fa fa-exclamation-circle' title='Invalid From Date !'></i>" SetFocusOnError="true"
                                        ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                </span>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtFromDate" MaxLength="10" placeholder="Select From Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>To Date<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                        ErrorMessage="Enter To Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter To Date !'></i>"
                                        ControlToValidate="txtToDate" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtToDate"
                                        ErrorMessage="Invalid To Date" Text="<i class='fa fa-exclamation-circle' title='Invalid To Date !'></i>" SetFocusOnError="true"
                                        ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                </span>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtToDate" MaxLength="10" placeholder="Select To Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2" style="margin-top: 20px;">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
                            </div>
                        </div>
                        <div class="col-md-2" style="margin-top: 20px;">
                            <div class="form-group">
                                <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-block btn btn-default" />
                            </div>
                        </div>
                    </fieldset>

                </div>
                <div class="box-body" id="pnldata" runat="server" visible="false">
                    <fieldset>
                        <legend>Crate Summary Report Details</legend>

                        <div class="col-md-1 pull-left">
                            <div class="form-group">
                                <asp:Button ID="btnExport" runat="server" CssClass="btn btn-success" OnClick="btnExport_Click" Text="Export" />
                            </div>
                        </div>



                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" AutoGenerateColumns="false" ShowFooter="true">
                            <Columns>
                                                        <asp:TemplateField HeaderText="SNo.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIRDate"  Text='<%#Eval("IRDate") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Shift">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblShiftName" Text='<%#Eval("ShiftName") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Distirbutor Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRName" Text='<%#Eval("RName") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Issue Crate">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIssueCrate" Text='<%#Eval("IssueCrate") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Return Crate">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblReturnCrate" Text='<%#Eval("ReturnCrate") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Balance Crate">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblShortExcess" Text='<%#Eval("ShortExcessCrate") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DM Challan No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDMChallanNo" Text='<%#Eval("DMChallanNo") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Challan No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblChallanNo" Text='<%#Eval("ChallanNo") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                        </asp:GridView>

                    </fieldset>

                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

