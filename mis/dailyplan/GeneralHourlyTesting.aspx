<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="GeneralHourlyTesting.aspx.cs" Inherits="mis_dailyplan_GeneralHourlyTesting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
     <%--Confirmation Modal Start --%>

    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #d9d9d9;">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                        </button>
                        <h4 class="modal-title" id="myModalLabel">Confirmation</h4>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <p>
                            <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp;
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSave_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
     <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">General Hourly Testing</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text="">
                           
                        </asp:Label>
                        <div class="box-body">
                            <div class="row">
                        <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Date</label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="rfvArrivalDate" runat="server" Display="Dynamic" ControlToValidate="txtEntryDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                    </span>
                                                    <div class="input-group">
                                                        <div class="input-group-prepend">
                                                            <span class="input-group-text">
                                                                <i class="far fa-calendar-alt"></i>
                                                            </span>
                                                        </div>
                                                        <asp:TextBox ID="txtEntryDate" autocomplete="off" placeholder="Tanker Arrival Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>   
                                </div>                                         
                            <fieldset>
                                <legend>Temperature ( °C )</legend>
                                <div class="row">
                                    <div class="col-md-2">
                                                
                                                <div class="form-group">
                                                    <label>At(Time)</label>
                                                    <div class="input-group bootstrap-timepicker timepicker">
                                                        <asp:TextBox ID="txtTempAt" class="form-control input-small" runat="server" ClientIDMode="Static"></asp:TextBox>
                                                        <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                                    </div>
                                                </div>
                                            </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Hot Water</label>
                                            <asp:TextBox ID="txtHotWater_Temp" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>CIP (Caustic)</label>
                                            <asp:TextBox ID="txtCIP_Caustic_Temp" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>CIP (Acid)</label>
                                            <asp:TextBox ID="txtCIP_Acid_Temp" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Crate Washer(1.)</label>
                                            <asp:TextBox ID="txtCrateWasher1_Temp" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Crate Washer(2.)</label>
                                            <asp:TextBox ID="txtCrateWasher2_Temp" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                      <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Crate Washer(3.)</label>
                                            <asp:TextBox ID="txtCrateWasher3_Temp" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                      <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Crate Washer(4.)</label>
                                            <asp:TextBox ID="txtCrateWasher4_Temp" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Can Washer</label>
                                            <asp:TextBox ID="txtCanWasher_Temp" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Strength of detergent ( % )</legend>
                                <div class="row">
                                    <div class="col-md-2">
                                               
                                                <div class="form-group">
                                                    <label>At(Time)</label>
                                                    <div class="input-group bootstrap-timepicker timepicker">
                                                        <asp:TextBox ID="txtStengthofdetAt" class="form-control input-small" runat="server" ClientIDMode="Static"></asp:TextBox>
                                                        <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                                    </div>
                                                </div>
                                            </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Hot Water</label>
                                            <asp:TextBox ID="txtHotWater_Stengthofdet" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>CIP (Caustic)</label>
                                            <asp:TextBox ID="txtCIP_Caustic_Stengthofdet" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>CIP (Acid)</label>
                                            <asp:TextBox ID="txtCIP_Acid_Stengthofdet" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Crate Washer(1.)</label>
                                            <asp:TextBox ID="txtCrateWasher1_Stengthofdet" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Crate Washer(2.)</label>
                                            <asp:TextBox ID="txtCrateWasher2_Stengthofdet" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                      <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Crate Washer(3.)</label>
                                            <asp:TextBox ID="txtCrateWasher3_Stengthofdet" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                      <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Crate Washer(4.)</label>
                                            <asp:TextBox ID="txtCrateWasher4_Stengthofdet" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Can Washer</label>
                                            <asp:TextBox ID="txtCanWasher_Stengthofdet" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Hardness of Water (PPM)</legend>
                                <div class="row">
                                    <div class="col-md-2">
                                               
                                                <div class="form-group">
                                                    <label>At(Time)</label>
                                                    <div class="input-group bootstrap-timepicker timepicker">
                                                        <asp:TextBox ID="txtHardnessofWaterAt" class="form-control input-small" runat="server" ClientIDMode="Static"></asp:TextBox>
                                                        <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                                    </div>
                                                </div>
                                            </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>TW</label>
                                            <asp:TextBox ID="txtTW_HardnessofWater" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Softner I</label>
                                            <asp:TextBox ID="txtSoftner1_HardnessofWater" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Softner II</label>
                                            <asp:TextBox ID="txtSoftner2_HardnessofWater" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Temp of Cold Storage ( °C )</legend>
                                <div class="row">
                                    <div class="col-md-2">
                                               
                                                <div class="form-group">
                                                    <label>At(Time)</label>
                                                    <div class="input-group bootstrap-timepicker timepicker">
                                                        <asp:TextBox ID="txtTempofcoldstorageAt" class="form-control input-small" runat="server" ClientIDMode="Static"></asp:TextBox>
                                                        <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                                    </div>
                                                </div>
                                            </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>M.C.S.R I</label>
                                            <asp:TextBox ID="txtMCSR1_Tempofcoldstorage" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>M.C.S.R II</label>
                                            <asp:TextBox ID="txtMCSR2_Tempofcoldstorage" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    
                                    
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Temp of Product Cold Storage ( °C )</legend>
                                <div class="row">
                                    <div class="col-md-2">
                                               <%-- <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ControlToValidate="txtEntryTime" Text="<i class='fa fa-exclamation-circle' title='Enter Time!'></i>" ErrorMessage="Enter Time" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                                </span>--%>
                                                <div class="form-group">
                                                    <label>At(Time)</label>
                                                    <div class="input-group bootstrap-timepicker timepicker">
                                                        <asp:TextBox ID="txtTempofProdcoldstorageAt" class="form-control input-small" runat="server" ClientIDMode="Static"></asp:TextBox>
                                                        <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                                    </div>
                                                </div>
                                            </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Product C/R I</label>
                                            <asp:TextBox ID="txtProductCR1_Tempofcoldstorage" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Product C/R II</label>
                                            <asp:TextBox ID="txtProductCR2_Tempofcoldstorage" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Product C/R III</label>
                                            <asp:TextBox ID="txtProductCR3_Tempofcoldstorage" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Temp of Buffer Deep Freezer ( °C )</legend>
                                <div class="row">
                                    <div class="col-md-2">
                                               
                                                <div class="form-group">
                                                   <label>At(Time)</label>
                                                    <div class="input-group bootstrap-timepicker timepicker">
                                                        <asp:TextBox ID="txtTempofbufferdeepfreezerAt" class="form-control input-small" runat="server" ClientIDMode="Static"></asp:TextBox>
                                                        <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                                    </div>
                                                </div>
                                            </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Deep Freezer I</label>
                                            <asp:TextBox ID="txtDeepFreezer1_Tempofbufferdeepfreezer" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Deep Freezer II</label>
                                            <asp:TextBox ID="txtDeepFreezer2_Tempofbufferdeepfreezer" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    
                                    
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Temp Chilled Water At ( °C )</legend>
                                <div class="row">
                                    <div class="col-md-2">
                                               
                                                <div class="form-group">
                                                    <label>At(Time)</label>
                                                    <div class="input-group bootstrap-timepicker timepicker">
                                                        <asp:TextBox ID="txtTempChilledWaterAt" class="form-control input-small" runat="server" ClientIDMode="Static"></asp:TextBox>
                                                        <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                                    </div>
                                                </div>
                                            </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Tank(1)</label>
                                            <asp:TextBox ID="txtTank1_TempChilledWater" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Tank(2)</label>
                                            <asp:TextBox ID="txtTank2_TempChilledWater" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Tank(3)</label>
                                            <asp:TextBox ID="txtTank3_TempChilledWater" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Tank(4)</label>
                                            <asp:TextBox ID="txtTank4_TempChilledWater" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" runat="server" ValidationGroup="a" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" OnClientClick="return ValidatePage();"/>
                                       <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-default" />
                                         <%--<a href="GeneralHourlyTesting.aspx" runat="server">Clear</a>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Report</h3>
                        </div>
                        <asp:Label ID="gridlblmsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                          
                            <div class="row">
                        <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Date</label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txtFilterDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </span>
                                                    <div class="input-group">
                                                        <div class="input-group-prepend">
                                                            <span class="input-group-text">
                                                                <i class="far fa-calendar-alt"></i>
                                                            </span>
                                                        </div>
                                                        <asp:TextBox ID="txtFilterDate" autocomplete="off" placeholder="Tanker Arrival Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server" OnTextChanged="txtFilterDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>   
                                </div>
                            <div class="row">
                                <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Button ID="btnExcel" Visible="false" runat="server" Text="Export" CssClass="btn btn-primary" OnClick="btnExcel_Click"/>
                                 <asp:Button ID="btnShowntoplant" runat="server"  Visible="false" CssClass="btn btn-primary" Text="Shown To Plant" OnClick="btnShowntoplant_Click"/>
                                </div>
                            </div>
                            </div>
                              
                            <div class="table-responsive">
                            <asp:GridView ID="gvDetail" runat="server" CssClass="table table-bordered gvDetail" AutoGenerateColumns="false" EmptyDataText="No Record Found" OnRowCreated="gvDetail_RowCreated">
                                 <HeaderStyle BackColor="Tan" Font-Bold="True" ForeColor="Black" HorizontalAlign="Center"/>
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="checkAll" runat="server" ClientIDMode="static"/>
                                                    Shown To Plant
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Eval("ShowntoPlant").ToString()=="1"?true:false %>' Enabled='<%# Eval("ShowntoPlant").ToString()=="0"?true:false %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>     
                                      <asp:TemplateField HeaderText="Date" HeaderStyle-Width="12%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDate" runat="server" Text='<%# (Convert.ToDateTime(Eval("Date"))).ToString("dd-MM-yyyy") %>'></asp:Label>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("GeneralHourlyTestingId").ToString()%>' runat="server" />
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
    <script src="../js/bootstrap-timepicker.js"></script>
    <script>
        $('#txtTempAt').timepicker();
        $('#txtStengthofdetAt').timepicker();
        $('#txtHardnessofWaterAt').timepicker();
        $('#txtTempofcoldstorageAt').timepicker();
        $('#txtTempofbufferdeepfreezerAt').timepicker();
        $('#txtTempChilledWaterAt').timepicker();
        $('#txtTempofProdcoldstorageAt').timepicker();
       
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
    </script>
	<script type="text/javascript">
       $('#checkAll').click(function () {

            var inputList = document.querySelectorAll('.gvDetail tbody input[type="checkbox"]:not(:disabled)');

            for (var i = 0; i < inputList.length; i++) {
                if (document.getElementById('checkAll').checked) {
                    inputList[i].checked = true;

                    //ValidatorEnable(Amount,true)
                }
                else {
                    inputList[i].checked = false;
                }
            }
        });


    </script>
</asp:Content>

