<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="BMCDCSRootWiseTestingReport.aspx.cs" Inherits="mis_MilkCollection_BMCDCSRootWiseTestingReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">BMC/DCS RootWise Testing Report</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text="">
                </asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-2">
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
                                    <asp:TextBox ID="txtDate" autocomplete="off"  CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" runat="server" style="margin-top:22px;" Text="Search" CssClass="btn btn-success" ValidationGroup="Save" OnClick="btnSearch_Click"/>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                       <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Button ID="btnExcel" Visible="false" runat="server" Text="Export" CssClass="btn btn-primary" OnClick="btnExcel_Click"/>                            
                                    </div>
                                </div>
                        <div class="col-md-12">
                            <div class="table-responsive">
                                 
                                        <asp:GridView ID="gvReport" CssClass="table table-bordered"  AutoGenerateColumns="false" runat="server" EmptyDataText="No Record Found">
                                    
                                     <Columns>
                                         <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                               
                                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("EntryDate").ToString() %>'></asp:Label>
                                            
                                                 </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Time">
                                            <ItemTemplate>
                                               
                                                <asp:Label ID="lblEntryTime" runat="server" Text='<%# Eval("EntryTime").ToString() %>'></asp:Label>
                                            
                                                 </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Particulars">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOffice_Name_E" runat="server" Text='<%# Eval("V_VehicleNo") %>'></asp:Label>                                                                       
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="O.T">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOralTest" runat="server" Text='<%# Eval("OralTest") %>'></asp:Label>                                                                       
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Temp">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTemp" runat="server" Text='<%# Eval("Temp") %>'></asp:Label>                                                                       
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Fat %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFat" runat="server" Text='<%# Eval("FAT") %>'></asp:Label>                                                                       
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="C.L.R">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("CLR") %>'></asp:Label>                                                                       
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="S.N.F %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSnf" runat="server" Text='<%# Eval("SNF") %>'></asp:Label>                                                                       
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="COB">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCOB" runat="server" Text='<%# Eval("COB") %>'></asp:Label>                                                                       
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Acidity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAcidity" runat="server" Text='<%# Eval("Acidity") %>'></asp:Label>                                                                       
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Urea">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUrea" runat="server" Text='<%# Eval("Urea") %>'></asp:Label>                                                                       
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Neutralizer">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNeutralizer" runat="server" Text='<%# Eval("Neutralizer") %>'></asp:Label>                                                                       
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Maltodextrin">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaltodextrin" runat="server" Text='<%# Eval("Maltodextrin") %>'></asp:Label>                                                                       
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Glucose">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGlucose" runat="server" Text='<%# Eval("Glucose") %>'></asp:Label>                                                                       
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Sucrose">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSucrose" runat="server" Text='<%# Eval("Sucrose") %>'></asp:Label>                                                                       
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Salt">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSalt" runat="server" Text='<%# Eval("Salt") %>'></asp:Label>                                                                       
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Starch">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStarch" runat="server" Text='<%# Eval("Starch") %>'></asp:Label>                                                                       
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Detergent">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDetergent" runat="server" Text='<%# Eval("Detergent") %>'></asp:Label>                                                                       
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Nitrate Test">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNitrateTest" runat="server" Text='<%# Eval("NitrateTest") %>'></asp:Label>                                                                       
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

