<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RMRD_Challan_EntryFromCanesTotalEntry.aspx.cs" Inherits="mis_MilkCollection_RMRD_Challan_EntryFromCanesTotalEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" OnClick="btnSave_Click" ID="btnYes" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="Save" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Canes Collection at CC</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>Route Detail</legend>
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
                                            <asp:TextBox ID="txtDate" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                  <div class="col-md-2">
                                    <label>Milk Collection Unit<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ValidationGroup="Save" ControlToValidate="ddlMilkCollectionUnit" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Milk Collection Unit!'></i>" ErrorMessage="Select Milk Collection Unit" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlMilkCollectionUnit" AutoPostBack="true" OnSelectedIndexChanged="ddlMilkCollectionUnit_SelectedIndexChanged" CssClass="form-control select2" runat="server">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="5">BMC</asp:ListItem>
                                            <asp:ListItem Value="6">DCS</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <label>Society<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvBMC" runat="server" Display="Dynamic" ValidationGroup="Save" ControlToValidate="ddlSociety" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Society!'></i>" ErrorMessage="Select Society" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlSociety" CssClass="form-control select2" runat="server" OnSelectedIndexChanged="ddlSociety_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>BMC Root<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvRoot" ValidationGroup="Save"
                                                InitialValue="0" ErrorMessage="Select BMC Root" Text="<i class='fa fa-exclamation-circle' title='Select BMC Root !'></i>"
                                                ControlToValidate="ddlBMCTankerRootName" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlBMCTankerRootName" Enabled="false" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlBMCTankerRootName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Milk Collection-Add </legend>
                                    <div class="row">
                                        <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Shift<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvShift" runat="server" Display="Dynamic" OnInit="rfvShift_Init" InitialValue="0" ControlToValidate="ddlShift" Text="<i class='fa fa-exclamation-circle' title='Select Shift!'></i>" ErrorMessage="Select Shift" ValidationGroup="a" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="Morning">Morning</asp:ListItem>
                                            <asp:ListItem Value="Evening">Evening</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Milk Type<span style="color: red;"> *</span></label>
                                                  <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic"  InitialValue="0" ControlToValidate="ddlMilkType" Text="<i class='fa fa-exclamation-circle' title='Select Milk Type!'></i>" ErrorMessage="Select Milk Type" ValidationGroup="a" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>   
                                                <asp:DropDownList ID="ddlMilkType" runat="server" CssClass="form-control select2" ClientIDMode="Static">
                                                    <asp:ListItem Selected="True" Value="1">Buf</asp:ListItem>
                                                    <asp:ListItem Value="2">Cow</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Milk Quality<span style="color: red;"> *</span></label>     
                                        <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="Dynamic"  InitialValue="0" ControlToValidate="ddlMilkQuality" Text="<i class='fa fa-exclamation-circle' title='Select Milk Quality!'></i>" ErrorMessage="Select Milk Quality" ValidationGroup="a" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>                                 
                                        <asp:DropDownList ID="ddlMilkQuality" runat="server" CssClass="form-control select2">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="Good">Good</asp:ListItem>
                                            <asp:ListItem Value="Sour">Sour</asp:ListItem> 
                                            <asp:ListItem Value="Curdle">Curdle</asp:ListItem>                                           
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                        <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Temp<span style="color: red;"> *</span></label>
                                             <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvtxtTEMP" runat="server" Display="Dynamic"
                                                        ControlToValidate="txtTEMP" 
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter TEMP!'></i>"
                                                        ErrorMessage="Enter TEMP" SetFocusOnError="true" ForeColor="Red"
                                                        ValidationGroup="a" Enabled="true"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revTemprature" ControlToValidate="txtTEMP" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                </span>
                                            <asp:TextBox ID="txtTEMP" runat="server" autocomplete="off" Text="4" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                
                                                <label>Milk Quantity (KG)<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic"
                                                        ControlToValidate="txtMilkQuantity" 
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter TEMP!'></i>"
                                                        ErrorMessage="Enter Milk Quantity" SetFocusOnError="true" ForeColor="Red"
                                                        ValidationGroup="a" Enabled="true"></asp:RequiredFieldValidator>
                                                     </span>
                                                <asp:TextBox ID="txtMilkQuantity" runat="server" CssClass="form-control" ClientIDMode="Static" OnTextChanged="txtMilkQuantity_TextChanged" AutoPostBack="true">
                                                
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Fat % (3.2 - 10)<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvFat" runat="server" Display="Dynamic" ControlToValidate="txtNetFat" Text="<i class='fa fa-exclamation-circle' title='Enter Fat %!'></i>" ErrorMessage="Enter Fat %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator ID="revFat_S" ControlToValidate="txtNetFat" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,1})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>

                                            <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Minimum FAT % required 3.2 and maximum 10." Display="Dynamic" ControlToValidate="txtNetFat" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 3.2 and maximum 10.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a" Type="Double" MinimumValue="3.2" MaximumValue="10"></asp:RangeValidator>

                                        </span>
                                        <asp:TextBox ID="txtNetFat" autocomplete="off" Width="100%" onkeypress="return validateDec(this,event)" AutoPostBack="true" OnTextChanged="txtNetFat_TextChanged" CssClass="form-control" placeholder="Fat %" runat="server" MaxLength="6"></asp:TextBox>

                                    </div>

                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>CLR (20 - 30)<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvCLR" runat="server" Display="Dynamic" ControlToValidate="txtNetCLR" Text="<i class='fa fa-exclamation-circle' title='Enter CLR!'></i>" ErrorMessage="Enter CLR" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator ID="revCLR" ControlToValidate="txtNetCLR" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>

                                            <asp:RangeValidator ID="RangeValidator6" runat="server" ErrorMessage="Minimum CLR % required 20 and maximum 30." Display="Dynamic" ControlToValidate="txtNetCLR" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 20 and maximum 30.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a" Type="Double" MinimumValue="20" MaximumValue="30"></asp:RangeValidator>
                                        </span>
                                        <asp:TextBox ID="txtNetCLR" autocomplete="off" Width="100%" onkeypress="return validateDec(this,event)" AutoPostBack="true" OnTextChanged="txtNetCLR_TextChanged" CssClass="form-control" placeholder="CLR" runat="server" MaxLength="6"></asp:TextBox>

                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>SNF %<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvSNF" runat="server" Display="Dynamic" ControlToValidate="txtnetsnf" Text="<i class='fa fa-exclamation-circle' title='Enter SNF %!'></i>" ErrorMessage="Enter SNF %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator ID="revSNF_S" ControlToValidate="txtnetsnf" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox ID="txtnetsnf" Width="100%" Enabled="false" onkeypress="return validateDec(this,event)" CssClass="form-control" placeholder="SNF %" runat="server" MaxLength="6"></asp:TextBox>

                                    </div>
                                </div>


                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>FAT(In Kg)<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txtfatinkg" Text="<i class='fa fa-exclamation-circle' title='Enter FAT In Kg!'></i>" ErrorMessage="Enter FAT In Kg" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtfatinkg" Width="100%" Enabled="false" onkeypress="return validateDec(this,event)" CssClass="form-control" placeholder="FAT %" runat="server" MaxLength="6"></asp:TextBox>

                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>SNF(In Kg)<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ControlToValidate="txtsnfinkg" Text="<i class='fa fa-exclamation-circle' title='Enter SNF In Kg!'></i>" ErrorMessage="Enter SNF In Kg" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtsnfinkg" Width="100%" Enabled="false" onkeypress="return validateDec(this,event)" CssClass="form-control" placeholder="SNF In Kg" runat="server" MaxLength="6"></asp:TextBox>

                                    </div>
                                </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ControlToValidate="txtTotalCan" Text="<i class='fa fa-exclamation-circle' title='Enter Total Can'></i>" ErrorMessage="Enter Total Can" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                                <label>Total Can<span style="color: red;"> *</span></label>
                                                <asp:TextBox ID="txtTotalCan" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                
                                                </asp:TextBox>
                                            </div>
                                        </div>                                       
                                      <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Good Can</label>
                                                <asp:TextBox ID="txtGoodCan" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                
                                                </asp:TextBox>
                                            </div>
                                        </div>  
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Button runat="server" CssClass="btn btn-primary" Style="margin-top: 20px;" ValidationGroup="a" ID="btnAddSocietyDetails" Text="Add" OnClick="btnAddSocietyDetails_Click" />
                                            </div>
                                        </div>
                                    </div>     
                                                          
                                   <div class="col-md-12">
                                       <div class="table-responsive">
                                           <asp:GridView ID="gv_MilkDetails" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                                               <Columns>
                                                   
                                                 <asp:TemplateField HeaderText="Shift">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblShift" runat="server" Text='<%# Eval("Shift") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Milk Type">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblMilkType" runat="server" Text='<%# Eval("MilkType") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Quality">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblQuality" runat="server" Text='<%# Eval("Quality") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Temp">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblTemp" runat="server" Text='<%# Eval("Temp") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Quantity">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fat">
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblFat" runat="server" Text='<%# Eval("TotalCan") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Snf">
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblSnf" runat="server" Text='<%# Eval("Snf") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Clr">
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblClr" runat="server" Text='<%# Eval("LR") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Fat(InKg)">
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblkgFat" runat="server" Text='<%# Eval("kgFat") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Snf(InKg)">
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblkgSnf" runat="server" Text='<%# Eval("kgSnf") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                    <asp:TemplateField  HeaderText="Total Can">
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblTotalCan" runat="server" Text='<%# Eval("TotalCan") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField  HeaderText="Good Can">
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblGoodCan" runat="server" Text='<%# Eval("GoodCan") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                               </Columns>
                                           </asp:GridView>
                                       </div>
                                   </div>
                                    
                            </fieldset>
                            <div class="row">
                                        <div class="col-md-2 pull-right">
                                            <div class="form-group">
                                                <asp:Button runat="server" CssClass="btn btn-primary" OnClientClick="return ValidatePage();" OnClick="btnSave_Click" Style="margin-top: 20px;" ValidationGroup="Save" ID="btnSave" Text="Save" />
                                                 <asp:Button runat="server" CssClass="btn btn-default"  Style="margin-top: 20px;"  ID="btnClear" Text="Clear" />
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
                            <h3 class="box-title">Entry List</h3>
                        </div>
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Date</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">
                                            <i class="far fa-calendar-alt"></i>
                                        </span>
                                    </div>
                                    <asp:TextBox ID="txtfilterdate" autocomplete="off" AutoPostBack="true" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server" OnTextChanged="txtfilterdate_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                            <div class="row">
                           <div class="col-md-12">
                                       <div class="table-responsive">
                                           <asp:GridView ID="gvEntryList" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="gvEntryList_RowCommand">
                                               <Columns>
                                                   
                                                 <asp:TemplateField HeaderText="Date">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shift">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblShift" runat="server" Text='<%# Eval("Shift") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                 
                                                    <asp:TemplateField HeaderText="Society">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblSociety" runat="server" Text='<%# Eval("Society") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                  
                                                   <asp:TemplateField HeaderText="B/C">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblMilkType" runat="server" Text='<%# Eval("MilkType") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Temp">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblTemp" runat="server" Text='<%# Eval("Temp") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Milk Quality">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblMilkQuality" runat="server" Text='<%# Eval("MilkQuality") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Milk Quantity">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblMilkQuantity" runat="server" Text='<%# Eval("MilkQuantity") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fat">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblFat" runat="server" Text='<%# Eval("Fat") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Snf">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblSnf" runat="server" Text='<%# Eval("Snf") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Clr">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblClr" runat="server" Text='<%# Eval("Clr") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fat(In Kg)">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblFatInKg" runat="server" Text='<%# Eval("FatInKg") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Snf(In Kg)">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblSnfInKg" runat="server" Text='<%# Eval("SnfInKg") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Total Can">
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblTotalCan" runat="server" Text='<%# Eval("TotalCan") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField  HeaderText="Good Can">
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblGoodCan" runat="server" Text='<%# Eval("GoodCan") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                    <asp:TemplateField  HeaderText="Action">
                                                       <ItemTemplate>
                                                          <%-- <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditRecord" CommandArgument='<%# Eval("MilkCollectionViaCanesChallan_ID") %>'><i class="fa fa-edit"></i></asp:LinkButton>--%>
                                                           <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteRecord" CommandArgument='<%# Eval("MilkCollectionViaCanesChallan_ID") %>' OnClientClick="return confirm('Do you really want to delete record?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                 <%--  <asp:TemplateField HeaderText="Kilo">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblI_MilkQuantity" runat="server" Text='<%# Eval("I_MilkQuantity") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>--%>
                                               </Columns>
                                           </asp:GridView>
                                       </div>
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
     <script type="text/javascript">
         function ValidatePage() {

             if (typeof (Page_ClientValidate) == 'function') {
                 Page_ClientValidate('Save');
             }

             if (Page_IsValid) {

                
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
         }
         $(document).on('focus', '.select2-selection.select2-selection--single', function (e) {
             $(this).closest(".select2-container").siblings('select:enabled').select2('open');
         });
    </script>
</asp:Content>

