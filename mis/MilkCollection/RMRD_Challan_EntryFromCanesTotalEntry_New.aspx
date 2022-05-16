<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RMRD_Challan_EntryFromCanesTotalEntry_New.aspx.cs" Inherits="mis_MilkCollection_RMRD_Challan_EntryFromCanesTotalEntry_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .SpaceInHeader {
            padding-left: 30px; /* this is example */
        }
    </style>
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
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
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
                                        <label>BMC Root<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvRoot" ValidationGroup="a"
                                                InitialValue="0" ErrorMessage="Select BMC Root" Text="<i class='fa fa-exclamation-circle' title='Select BMC Root !'></i>"
                                                ControlToValidate="ddlBMCTankerRootName" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlBMCTankerRootName" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlBMCTankerRootName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Milk Collection-Add </legend>
                       
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                                <asp:GridView ID="gvBMCMilkDetails" runat="server" CssClass="table table-bordered" Width="100%" AutoGenerateColumns="false" OnRowCreated="gvBMCMilkDetails_RowCreated">
                                                    <HeaderStyle BackColor="Tan" Font-Bold="True" ForeColor="Black" HorizontalAlign="Center" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="2%" HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" OnCheckedChanged="chkSelect_CheckedChanged" AutoPostBack="true"/>
                                                                <%--<asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>--%>
                                                                <%--<asp:Label ID="lblType" CssClass="hidden" runat="server" Text='<%# Eval("Type") %>'></asp:Label>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Society" ItemStyle-Width="5%" HeaderStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSociety" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                                <asp:Label ID="lblOffice_ID" CssClass="hidden" runat="server" Text='<%# Eval("Office_ID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Milk&nbsp;&nbsp; Quality">
                                                            <ItemTemplate>
                                                                <span class="pull-right">
                                                                    <asp:RequiredFieldValidator ID="rfvBufMilkQuality" runat="server" Display="Dynamic" InitialValue="0" Enabled="false" ControlToValidate="ddlBufMilkQuality" Text="<i class='fa fa-exclamation-circle' title='Select Milk Quality!'></i>" ErrorMessage="Select Milk Quality" ValidationGroup="a" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                </span>
                                                                <asp:DropDownList ID="ddlBufMilkQuality" runat="server" CssClass="form-control select2">
                                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                                    <asp:ListItem Selected="True" Value="Good">Good</asp:ListItem>
                                                                    <asp:ListItem Value="Sour">Sour</asp:ListItem>
                                                                    <asp:ListItem Value="Curdle">Curdle</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Temp">
                                                            <ItemTemplate>
                                                                <span class="pull-right">
                                                                    <asp:RequiredFieldValidator ID="rfvtxtBufTEMP" runat="server" Display="Dynamic"
                                                                        ControlToValidate="txtBufTEMP"
                                                                        Text="<i class='fa fa-exclamation-circle' title='Enter TEMP!'></i>"
                                                                        ErrorMessage="Enter TEMP" SetFocusOnError="true" ForeColor="Red"
                                                                        ValidationGroup="a"  Enabled="false"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="revTemprature" ControlToValidate="txtBufTEMP" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                                </span>
                                                                <asp:TextBox ID="txtBufTEMP" runat="server" autocomplete="off" Text="4" CssClass="form-control"></asp:TextBox>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <span class="pull-right">
                                                                    <asp:RequiredFieldValidator ID="rfvBufMilkQuantity" runat="server" Display="Dynamic"
                                                                        ControlToValidate="txtBufMilkQuantity"
                                                                        Text="<i class='fa fa-exclamation-circle' title='Enter Quantity!'></i>"
                                                                        ErrorMessage="Enter Milk Quantity" SetFocusOnError="true" ForeColor="Red"
                                                                        ValidationGroup="a" Enabled="false" ></asp:RequiredFieldValidator>
                                                                </span>
                                                                <asp:TextBox ID="txtBufMilkQuantity" runat="server" CssClass="form-control" OnTextChanged="txtBufMilkQuantity_TextChanged" AutoPostBack="true">
                                                                </asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fat(%)">
                                                            <ItemTemplate>
                                                                <span class="pull-right">
                                                                    <asp:RequiredFieldValidator ID="rfvBufFat" runat="server" Display="Dynamic"
                                                                        ControlToValidate="txtBufFat"
                                                                        Text="<i class='fa fa-exclamation-circle' title='Enter Fat!'></i>"
                                                                        ErrorMessage="Enter Fat" SetFocusOnError="true" ForeColor="Red"
                                                                        ValidationGroup="a" Enabled="false" ></asp:RequiredFieldValidator>
                                                                     <asp:RangeValidator ID="rvBufFat" runat="server" ErrorMessage="Minimum FAT % required 5.6 and maximum 11.0." Display="Dynamic" ControlToValidate="txtBufFat" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 5.6 and maximum 11.0!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a" Type="Double" MinimumValue="5.6" MaximumValue="11.0"></asp:RangeValidator>
                                                                </span>
                                                                <asp:TextBox ID="txtBufFat" runat="server" CssClass="form-control"  OnTextChanged="txtBufFat_TextChanged" AutoPostBack="true">
                                                                </asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="C.L.R">
                                                            <ItemTemplate>
                                                                <span class="pull-right">
                                                                    <asp:RequiredFieldValidator ID="rfvBufCLR" runat="server" Display="Dynamic"
                                                                        ControlToValidate="txtBufCLR"
                                                                        Text="<i class='fa fa-exclamation-circle' title='Enter CLR!'></i>"
                                                                        ErrorMessage="Enter CLR" SetFocusOnError="true" ForeColor="Red"
                                                                        ValidationGroup="a" Enabled="false" ></asp:RequiredFieldValidator>
                                                                </span>
                                                                <asp:TextBox ID="txtBufCLR" runat="server" CssClass="form-control"  OnTextChanged="txtBufCLR_TextChanged" AutoPostBack="true">
                                                                </asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText=" S.n.f&nbsp;&nbsp;&nbsp;&nbsp;">
                                                            <ItemTemplate>                                                              
                                                                <asp:TextBox ID="txtBufSnf" runat="server" CssClass="form-control" ReadOnly="true" ClientIDMode="Static">
                                                
                                                                </asp:TextBox>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fat(InKg)" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtBufFatinkg" runat="server" CssClass="form-control" ReadOnly="true" ClientIDMode="Static">
                                                                </asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Snf(InKg)" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtBufSnfinkg" runat="server" CssClass="form-control" ReadOnly="true" ClientIDMode="Static">
                                                                </asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total&nbsp;&nbsp; Can">
                                                            <ItemTemplate>                                                             
                                                                <asp:TextBox ID="txtBuftotalCan" runat="server" CssClass="form-control">
                                                
                                                                </asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Good&nbsp;&nbsp; Can">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtBufGoodCan" runat="server" CssClass="form-control">
                                                
                                                                </asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Milk&nbsp;&nbsp; Quality">
                                                            <ItemTemplate>
                                                                <span class="pull-right">
                                                                    <asp:RequiredFieldValidator ID="rfvCowMilkQuality" Enabled="false" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlCowMilkQuality" Text="<i class='fa fa-exclamation-circle' title='Select Milk Quality!'></i>" ErrorMessage="Select Milk Quality" ValidationGroup="a"  SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                </span>
                                                                <asp:DropDownList ID="ddlCowMilkQuality" runat="server" CssClass="form-control select2">
                                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                                    <asp:ListItem Selected="True" Value="Good">Good</asp:ListItem>
                                                                    <asp:ListItem Value="Sour">Sour</asp:ListItem>
                                                                    <asp:ListItem Value="Curdle">Curdle</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Temp">
                                                            <ItemTemplate>
                                                                <span class="pull-right">
                                                                    <asp:RequiredFieldValidator ID="rfvCowTEMP" runat="server" Display="Dynamic"
                                                                        ControlToValidate="txtCowTEMP"
                                                                        Text="<i class='fa fa-exclamation-circle' title='Enter TEMP!'></i>"
                                                                        ErrorMessage="Enter TEMP" SetFocusOnError="true" ForeColor="Red"
                                                                        ValidationGroup="a" Enabled="false"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="rfvCTemprature" ControlToValidate="txtBufTEMP" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                                </span>
                                                                <asp:TextBox ID="txtCowTEMP" runat="server" autocomplete="off" Text="4" CssClass="form-control"></asp:TextBox>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quantity">
                                                            <ItemTemplate>
                                                                <span class="pull-right">
                                                                    <asp:RequiredFieldValidator ID="rfvCowMilkQuantity" runat="server" Display="Dynamic"
                                                                        ControlToValidate="txtCowMilkQuantity"
                                                                        Text="<i class='fa fa-exclamation-circle' title='Enter Milk Quantity!'></i>"
                                                                        ErrorMessage="Enter Milk Quantity" SetFocusOnError="true" ForeColor="Red"
                                                                        ValidationGroup="a" Enabled="false"></asp:RequiredFieldValidator>
                                                                </span>
                                                                <asp:TextBox ID="txtCowMilkQuantity" runat="server" CssClass="form-control"  OnTextChanged="txtCowMilkQuantity_TextChanged" AutoPostBack="true">
                                                                </asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fat(%)">
                                                            <ItemTemplate>
                                                                <span class="pull-right">
                                                                    <asp:RequiredFieldValidator ID="rfvCowFat" runat="server" Display="Dynamic"
                                                                        ControlToValidate="txtCowFat"
                                                                        Text="<i class='fa fa-exclamation-circle' title='Enter Fat!'></i>"
                                                                        ErrorMessage="Enter Fat" SetFocusOnError="true" ForeColor="Red"
                                                                        ValidationGroup="a" Enabled="false"></asp:RequiredFieldValidator>
                                                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Minimum FAT % required 3.2 and maximum 5.5." Display="Dynamic" ControlToValidate="txtCowFat" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 3.2 and maximum 5.5!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a" Type="Double" MinimumValue="3.2" MaximumValue="5.5"></asp:RangeValidator>
                                                                </span>
                                                                <asp:TextBox ID="txtCowFat" runat="server" CssClass="form-control"  OnTextChanged="txtCowFat_TextChanged" AutoPostBack="true">
                                                                </asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="C.L.R">
                                                            <ItemTemplate>
                                                                <span class="pull-right">
                                                                    <asp:RequiredFieldValidator ID="rfvCowCLR" runat="server" Display="Dynamic"
                                                                        ControlToValidate="txtCowCLR"
                                                                        Text="<i class='fa fa-exclamation-circle' title='Enter CLR!'></i>"
                                                                        ErrorMessage="Enter CLR" SetFocusOnError="true" ForeColor="Red"
                                                                        ValidationGroup="a" Enabled="false"></asp:RequiredFieldValidator>
                                                                   
                                                                </span>
                                                                <asp:TextBox ID="txtCowCLR" runat="server" CssClass="form-control"  OnTextChanged="txtCowCLR_TextChanged" AutoPostBack="true">
                                                                </asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="S.n.f&nbsp;&nbsp;&nbsp;&nbsp;">
                                                            <ItemTemplate>                                                             
                                                                <asp:TextBox ID="txtCowSnf" ReadOnly="true" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                
                                                                </asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fat(InKg)" Visible="false">
                                                            <ItemTemplate>                                                               
                                                                <asp:TextBox ID="txtCowFatinkg" runat="server" CssClass="form-control" ReadOnly="true" ClientIDMode="Static">
                                                                </asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Snf(InKg)" Visible="false">
                                                            <ItemTemplate>                                                            
                                                                <asp:TextBox ID="txtCowSnfinkg" runat="server" CssClass="form-control" ReadOnly="true" ClientIDMode="Static">
                                                                </asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total&nbsp;&nbsp; Can">
                                                            <ItemTemplate>                                                             
                                                                <asp:TextBox ID="txtCowtotalCan" runat="server" CssClass="form-control">
                                                                </asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Good&nbsp;&nbsp; Can">
                                                            <ItemTemplate>                                                               
                                                                <asp:TextBox ID="txtCowGoodCan" runat="server" CssClass="form-control">
                                                                </asp:TextBox>
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
                                        <asp:Button runat="server" CssClass="btn btn-primary" OnClientClick="return ValidatePage();" OnClick="btnSave_Click" Style="margin-top: 20px;" ValidationGroup="a" ID="btnSave" Text="Save" />
                                        <asp:Button runat="server" CssClass="btn btn-default" Style="margin-top: 20px;" ID="btnClear" Text="Clear" />
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
                                                <asp:TemplateField HeaderText="Good Can">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGoodCan" runat="server" Text='<%# Eval("GoodCan") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
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
                Page_ClientValidate('a');
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

