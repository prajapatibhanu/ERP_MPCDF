<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MCMS_QC_ReTestReport.aspx.cs" Inherits="mis_mcms_reports_MCMS_QC_ReTestReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <asp:ValidationSummary ID="vSummary" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="CT" />
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">QC ReTest Report </h3>
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
                                    <asp:DropDownList ID="ddlDSName3" AutoPostBack="true" CssClass="form-control select2" runat="server">
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
                                    <asp:Button ID="btnCCWiseQCReTestReport" CssClass="btn btn-secondary button-mini" Style="margin-top: 20px;" runat="server" Text="Search" OnClick="btnCCWiseQCReTestReport_Click"  ValidationGroup="CT" />
                                </div>
                            </div>

                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                              <%--  <asp:Button ID="btnSave" runat="server" Text="Export To Excel" OnClientClick="return confirmation();" CssClass="btn btn-primary btn-block" OnClick="btnSave_Click" />--%>
                            </div>
                        </div>
                        </div>
                    <div class="row">
                         <div class="col-md-3 noprint">
                            <div class="form-group">
                                <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn btn-default" Text="Excel" OnClick ="btnExport_Click"/>
                               
                            </div>
                                
                            </div>
                        <div class="col-md-12">



                            <div class="table-responsive">

                                <asp:GridView ID="gv_CCWiseQCReTestReport" runat="server" AutoGenerateColumns="false" ShowHeader="true" EmptyDataRowStyle-ForeColor="Red" CssClass="table table-bordered table-borderedLBrown" EmptyDataText="No Data Found" OnRowCreated="gv_CCWiseQCReTestReport_RowCreated">
                                   <HeaderStyle BackColor="Tan" Font-Bold="True" ForeColor="Black" HorizontalAlign="Center"/>
                                     <Columns>
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="ReferenceNo">
                                            <ItemTemplate>
                                                <%# Eval("C_ReferenceNo") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Challan No">
                                            <ItemTemplate>
                                                <%# Eval("V_ReferenceCode") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="CC Name">
                                            <ItemTemplate>
                                                <%# Eval("Office_Name") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Seal Location">
                                            <ItemTemplate>
                                                <%# Eval("V_SealLocation") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Test Date">
                                            <ItemTemplate>
                                                <%# Eval("DT_EntryDate") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <asp:TemplateField HeaderText="MilkQuality">
                                            <ItemTemplate>
                                                <%# Eval("Test_V_MilkQuality") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                                                         
                                        <asp:TemplateField HeaderText="FAT">
                                            <ItemTemplate>
                                                <%# Eval("Test_D_FAT") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="CLR">
                                            <ItemTemplate>
                                                <%# Eval("Test_D_CLR") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SNF">
                                            <ItemTemplate>
                                                <%# Eval("Test_D_SNF") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                   
                                        <asp:TemplateField HeaderText="Temp">
                                            <ItemTemplate>
                                                <%# Eval("Test_V_Temp") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Acidity">
                                            <ItemTemplate>
                                                <%# Eval("Test_V_Acidity") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="COB">
                                            <ItemTemplate>
                                                <%# Eval("Test_V_COB") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MBRT">
                                            <ItemTemplate>
                                                <%# Eval("Test_V_MBRT") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Alcohol">
                                            <ItemTemplate>
                                                <%# Eval("Test_V_Alcohol") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="ReTest Date">
                                            <ItemTemplate>
                                                <%# Eval("ReTestEntryDate") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                 
                                       <asp:TemplateField HeaderText="MilkQuality">
                                            <ItemTemplate>
                                                <%# Eval("V_MilkQuality") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                                                         
                                        <asp:TemplateField HeaderText="FAT">
                                            <ItemTemplate>
                                                <%# Eval("D_FAT") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="CLR">
                                            <ItemTemplate>
                                                <%# Eval("D_CLR") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SNF">
                                            <ItemTemplate>
                                                <%# Eval("D_SNF") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                   
                                        <asp:TemplateField HeaderText="Temp">
                                            <ItemTemplate>
                                                <%# Eval("V_Temp") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Acidity">
                                            <ItemTemplate>
                                                <%# Eval("V_Acidity") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="COB">
                                            <ItemTemplate>
                                                <%# Eval("V_COB") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MBRT">
                                            <ItemTemplate>
                                                <%# Eval("V_MBRT") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Alcohol">
                                            <ItemTemplate>
                                                <%# Eval("V_Alcohol") %>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
</asp:Content>

