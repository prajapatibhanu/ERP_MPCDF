<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptGeneralHourlyTesting.aspx.cs" Inherits="mis_QCReports_RptGeneralHourlyTesting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">General Hourly Testing Report</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                        <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>From Date</label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txtFromDate" Text="<i class='fa fa-exclamation-circle' title='Enter From Date!'></i>" ErrorMessage="Enter From Date" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </span>
                                                    <div class="input-group">
                                                        <div class="input-group-prepend">
                                                            <span class="input-group-text">
                                                                <i class="far fa-calendar-alt"></i>
                                                            </span>
                                                        </div>
                                                        <asp:TextBox ID="txtFromDate" autocomplete="off" placeholder="Enter From Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div> 
                                <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>To Date</label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtToDate" Text="<i class='fa fa-exclamation-circle' title='Enter To Date!'></i>" ErrorMessage="Enter To Date" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </span>
                                                    <div class="input-group">
                                                        <div class="input-group-prepend">
                                                            <span class="input-group-text">
                                                                <i class="far fa-calendar-alt"></i>
                                                            </span>
                                                        </div>
                                                        <asp:TextBox ID="txtToDate" autocomplete="off" placeholder="Enter To Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div> 
                                <div class="col-md-2">
                                    <div class="form-group">
                                         <asp:Button ID="btnSearch" style="margin-top:22px;"  runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click"/>
                                    </div>
                                </div>    
                                </div>
                            <div class="row">
                                <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Button ID="btnExcel" Visible="false" runat="server" Text="Export" CssClass="btn btn-primary" OnClick="btnExcel_Click"/>
                                </div>
                            </div>
                            </div>
                            <div class="table-responsive">
                            <asp:GridView ID="gvDetail" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Record Found" OnRowCreated="gvDetail_RowCreated">
                                 <HeaderStyle BackColor="Tan" Font-Bold="True" ForeColor="Black" HorizontalAlign="Center"/>
                                <Columns>                                
                                      <asp:TemplateField HeaderText="Date" HeaderStyle-Width="12%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDate" runat="server" Text='<%# (Convert.ToDateTime(Eval("Date"))).ToString("dd-MM-yyyy") %>'></asp:Label>
                                   
                                             </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="At(Time)" DataField="TempAt" />
                                    <asp:BoundField HeaderText="Hot Water" DataField="HotWater_Temp" />
                                   
                                    <asp:BoundField HeaderText="CIP (Caustic)" DataField="CIP_Caustic_Temp" />
                                    <asp:BoundField HeaderText="CIP (Acid)" DataField="CIP_Acid_Temp" />
                                    <asp:BoundField HeaderText="Crate Washer(1.)" DataField="CrateWasher1_Temp" />

                                    <asp:BoundField HeaderText="Crate Washer(2.)" DataField="CrateWasher2_Temp" />
                                    <asp:BoundField HeaderText="Crate Washer(3.)" DataField="CrateWasher3_Temp" />
                                    <asp:BoundField HeaderText="Crate Washer(4.)" DataField="CrateWasher4_Temp" />
                                     <asp:BoundField HeaderText="Can Washer" DataField="CanWasher_Temp" />
                                     <asp:BoundField HeaderText="At(Time)" DataField="StengthofdetAt" />
                                   <asp:BoundField HeaderText="Hot Water" DataField="HotWater_Stengthofdet" />
                                    <asp:BoundField HeaderText="CIP (Caustic)" DataField="CIP_Caustic_Stengthofdet" />
                                    <asp:BoundField HeaderText="CIP (Acid)" DataField="CIP_Acid_Stengthofdet" />
                                    <asp:BoundField HeaderText="Crate Washer(1.)" DataField="CrateWasher1_Stengthofdet" />

                                    <asp:BoundField HeaderText="Crate Washer(2.)" DataField="CrateWasher2_Stengthofdet" />
                                    <asp:BoundField HeaderText="Crate Washer(3.)" DataField="CrateWasher3_Stengthofdet" />
                                    <asp:BoundField HeaderText="Crate Washer(4.)" DataField="CrateWasher4_Stengthofdet" />
                                     <asp:BoundField HeaderText="Can Washer" DataField="CanWasher_Stengthofdet" />
                                    <asp:BoundField HeaderText="At(Time)" DataField="HardnessofWaterAt" />
                                    <asp:BoundField HeaderText="TW" DataField="TW_HardnessofWater" />

                                    <asp:BoundField HeaderText="Softner I" DataField="Softner1_HardnessofWater" />
                                    <asp:BoundField HeaderText="Softner II" DataField="Softner2_HardnessofWater" />
                                    <asp:BoundField HeaderText="At(Time)" DataField="TempofcoldstorageAt" />
                                     <asp:BoundField HeaderText="M.C.S.R I" DataField="MCSR1_Tempofcoldstorage" />
                                    <asp:BoundField HeaderText="M.C.S.R II" DataField="MCSR2_Tempofcoldstorage" />
                                    <asp:BoundField HeaderText="At(Time)" DataField="TempofProdcoldstorageAt" />
                                    <asp:BoundField HeaderText="Product C/R I" DataField="ProductCR1_Tempofcoldstorage" />

                                    <asp:BoundField HeaderText="Product C/R II" DataField="ProductCR2_Tempofcoldstorage" />
                                    <asp:BoundField HeaderText="Product C/R III" DataField="ProductCR3_Tempofcoldstorage" />
                                    <asp:BoundField HeaderText="At(Time)" DataField="TempofbufferdeepfreezerAt" />
                                     <asp:BoundField HeaderText="Deep Freezer I" DataField="DeepFreezer1_Tempofbufferdeepfreezer" />

                                    <asp:BoundField HeaderText="Deep Freezer II" DataField="DeepFreezer2_Tempofbufferdeepfreezer" />
                                     <asp:BoundField HeaderText="At(Time)" DataField="TempChilledWaterAt" />
                                     <asp:BoundField HeaderText="Tank(1)" DataField="Tank1_TempChilledWater" />

                                    <asp:BoundField HeaderText="Tank(2)" DataField="Tank2_TempChilledWater" />
                                    
                                     <asp:BoundField HeaderText="Tank(3)" DataField="Tank3_TempChilledWater" />

                                    <asp:BoundField HeaderText="Tank(4)" DataField="Tank4_TempChilledWater" />
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

