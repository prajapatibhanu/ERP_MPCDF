<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RMRD_Challan_Entry.aspx.cs" Inherits="mis_MilkCollection_RMRD_Challan_Entry" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        @media print {
             
              .noprint {
                display: none;
            }
             table tfoot {
                display: table-row-group;
            }  
          }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

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
    <asp:ValidationSummary ID="vs1" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="vs2" runat="server" ValidationGroup="c" ShowMessageBox="true" ShowSummary="false" />

    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success noprint">
                <div class="box-header noprint">
                    <h3 class="box-title">RMRD Challan Entry</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                   
                    <fieldset>
                        <legend>Filter</legend>
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
                                                        <asp:TextBox ID="txtDate" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Shift<span style="color: red;"> *</span></label>     
                                        <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic" OnInit="rfvShift_Init" InitialValue="0" ControlToValidate="ddlShift" Text="<i class='fa fa-exclamation-circle' title='Select Shift!'></i>" ErrorMessage="Select Shift" ValidationGroup="a" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>                                 
                                        <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="Morning">Morning</asp:ListItem>
                                            <asp:ListItem Value="Evening">Evening</asp:ListItem>                                           
                                        </asp:DropDownList>
                                    </div>
                                </div>
                        </div>
                         <asp:Panel ID="panel1" runat="server">
                        <div class="row">

                            <div class="col-md-12">
                                
                                     <%--<div class="col-md-2">
                                        <label>Milk Collection Unit<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ValidationGroup="a" ControlToValidate="ddlMilkCollectionUnit" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Milk Collection Unit!'></i>" ErrorMessage="Select Milk Collection Unit" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlMilkCollectionUnit" AutoPostBack="true" OnSelectedIndexChanged="ddlMilkCollectionUnit_SelectedIndexChanged" CssClass="form-control select2" runat="server">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="5">BMC</asp:ListItem>
                                                <asp:ListItem Value="6">DCS</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>--%>  
										<div class="col-md-2">
                                            <div class="form-group">
                                                <label>CC<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvRoot" ValidationGroup="a"
                                                        InitialValue="0" ErrorMessage="Select Chilling Center" Text="<i class='fa fa-exclamation-circle' title='Select Chilling Center !'></i>"
                                                        ControlToValidate="ddlCC" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator></span>
                                                <asp:DropDownList ID="ddlCC" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlCC_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>     									
                                <div class="col-md-3">
                                    <label>Society<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvFarmer" runat="server" Display="Dynamic" ValidationGroup="a" ControlToValidate="ddlSociety" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Society!'></i>" ErrorMessage="Select Society" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
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
                                       <%-- <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator144" ValidationGroup="a"
                                                InitialValue="0" ErrorMessage="Select BMC Root" Text="<i class='fa fa-exclamation-circle' title='Select BMC Root !'></i>"
                                                ControlToValidate="ddlBMCTankerRootName" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>--%>
                                        <asp:DropDownList ID="ddlBMCTankerRootName" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlBMCTankerRootName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
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
                                                        ValidationGroup="a" Enabled="false"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revTemprature" ControlToValidate="txtTEMP" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                </span>
                                            <asp:TextBox ID="txtTEMP" runat="server" autocomplete="off" Text="4" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Milk Qty(In Kg)<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5_N" ValidationGroup="a"
                                                ErrorMessage="Enter Net Quantity (In KG)" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Net Quantity (In KG)!'></i>"
                                                ControlToValidate="txtI_MilkQuantity" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5_N" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="a" runat="server" ControlToValidate="txtI_MilkQuantity" ErrorMessage="Enter Valid Milk Quantity" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Milk Quantity!'></i>"></asp:RegularExpressionValidator>

                                        </span>
                                        <asp:TextBox ID="txtI_MilkQuantity" AutoPostBack="true" OnTextChanged="txtI_MilkQuantity_TextChanged" autocomplete="off" Width="100%" onkeypress="return validateDec(this,event)" CssClass="form-control" placeholder="Milk Quantity" runat="server" MaxLength="12"></asp:TextBox>
                                    </div>

                                </div>


                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Fat % (3.2 - 10)<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvFat" runat="server" Display="Dynamic" ControlToValidate="txtNetFat" Text="<i class='fa fa-exclamation-circle' title='Enter Fat %!'></i>" ErrorMessage="Enter Fat %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator ID="revFat_S" ControlToValidate="txtNetFat" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>

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
                                 <div class="col-md-1">
                                <div class="form-group">
                                    <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnAdd_Click"  Style="margin-top: 20px;" ValidationGroup="a" ID="btnAdd" Text="Add" />
                                </div>
                            </div>

                            </div>
                        </div>
                               </asp:Panel>
                    </fieldset>
                    
                    <asp:Panel ID="panel2"  runat="server" Visible="false">
                        <fieldset>
                        <legend>Society </legend>
                        <div class="row">
                            
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>BMC/DCS<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="b"
                                            InitialValue="0" ErrorMessage="Select BMC/DCS" Text="<i class='fa fa-exclamation-circle' title='Select BMC/DCS !'></i>"
                                            ControlToValidate="ddlBMCDCS" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList runat="server" ID="ddlBMCDCS" CssClass="form-control select2"  >
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Milk Type<span style="color: red;"> *</span></label>
                                    <%--<span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="b"
                                            InitialValue="0" ErrorMessage="Select Head Name" Text="<i class='fa fa-exclamation-circle' title='Select Head Name !'></i>"
                                            ControlToValidate="ddlHeaddetails" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>--%>
                                    <asp:DropDownList ID="ddlMilkType"  runat="server" CssClass="form-control select2" ClientIDMode="Static" >
                                         <asp:ListItem Selected="True" Value="1">Buf</asp:ListItem>
                                         <asp:ListItem Value="2">Cow</asp:ListItem>                                     
                                    </asp:DropDownList>
                                </div>
                            </div>
                            
                            <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Shift<span style="color: red;"> *</span></label>     
                                        <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvShift" runat="server" Display="Dynamic" OnInit="rfvShift_Init" InitialValue="0" ControlToValidate="ddlBMCDCSShift" Text="<i class='fa fa-exclamation-circle' title='Select Shift!'></i>" ErrorMessage="Select Shift" ValidationGroup="b" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>                                 
                                        <asp:DropDownList ID="ddlBMCDCSShift" runat="server" CssClass="form-control select2">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="Morning">Morning</asp:ListItem>
                                            <asp:ListItem Value="Evening">Evening</asp:ListItem>                                           
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Milk Quality<span style="color: red;"> *</span></label>     
                                        <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic"  InitialValue="0" ControlToValidate="ddlBMCDCSMilkQuality" Text="<i class='fa fa-exclamation-circle' title='Select Milk Quality!'></i>" ErrorMessage="Select Milk Quality" ValidationGroup="b" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>                                 
                                        <asp:DropDownList ID="ddlBMCDCSMilkQuality" runat="server" CssClass="form-control select2">
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
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Display="Dynamic"
                                                        ControlToValidate="txtBMCDCSTemp" 
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter TEMP!'></i>"
                                                        ErrorMessage="Enter TEMP" SetFocusOnError="true" ForeColor="Red"
                                                        ValidationGroup="b" Enabled="false"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtBMCDCSTemp" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="b"></asp:RegularExpressionValidator>
                                                </span>
                                            <asp:TextBox ID="txtBMCDCSTemp" runat="server" autocomplete="off" Text="4" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Quantity<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="b"
                                            ErrorMessage="Enter Quantity" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Quantity !'></i>"
                                            ControlToValidate="txtQuantity" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <asp:TextBox autocomplete="off" ClientIDMode="Static" onkeypress="return validateDec(this,event)" MaxLength="10" runat="server" CssClass="form-control" ID="txtQuantity" placeholder="Enter Quantity"></asp:TextBox>
                                </div>
                            </div>
                           <div class="col-md-2">
                                <div class="form-group">
                                    <label>Fat % (2.0 - 10)<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                          <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtFAT" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="b"></asp:RegularExpressionValidator>

                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Minimum FAT % required 2.0 and maximum 10." Display="Dynamic" ControlToValidate="txtFAT" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 2.0 and maximum 10.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="b" Type="Double" MinimumValue="2.0" MaximumValue="10"></asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="b"
                                            ErrorMessage="Enter Fat" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Fat !'></i>"
                                            ControlToValidate="txtFAT" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <asp:TextBox autocomplete="off" ClientIDMode="Static" onkeypress="return validateDec(this,event)" MaxLength="10" runat="server" CssClass="form-control" ID="txtFAT" placeholder="Enter FAT" OnTextChanged="txtFAT_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>CLR (15 - 35)<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                           <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtLR" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="b"></asp:RegularExpressionValidator>

                                            <asp:RangeValidator ID="RangeValidator3" runat="server" ErrorMessage="Minimum CLR % required 15 and maximum 35." Display="Dynamic" ControlToValidate="txtLR" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 15 and maximum 35.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="b" Type="Double" MinimumValue="15" MaximumValue="35"></asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="b"
                                            ErrorMessage="Enter LR" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter LR !'></i>"
                                            ControlToValidate="txtLR" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <asp:TextBox autocomplete="off" ClientIDMode="Static" onkeypress="return validateDec(this,event)" MaxLength="10" runat="server" CssClass="form-control" ID="txtLR" placeholder="Enter CLR"></asp:TextBox>
                                </div>
                            </div>                                
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnAddSocietyDetails_Click" Style="margin-top: 20px;" ValidationGroup="b" ID="btnAddSocietyDetails" Text="Add" />
                                </div>
                            </div>
                            
                            <div class="col-md-12">
                                <hr />
                                <div class="table-responsive">
                                    <asp:GridView ID="gv_HeadDetails" ShowFooter="true" ShowHeader="true" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server" OnRowDataBound="gv_HeadDetails_RowDataBound">
                                    <Columns>

                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOffice_ID" Visible="false" runat="server" Text='<%# Eval("Office_ID") %>'></asp:Label>
                                                <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                           <asp:Label ID="lblEntry" CssClass="hidden" runat="server" Text='<%# Eval("FirstEntry") %>'></asp:Label>
                                                 </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Milk Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMilkType" runat="server" Text='<%# Eval("MilkType") %>'></asp:Label>
                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Shift">
                                            <ItemTemplate>
                                                <asp:Label ID="lblShift" runat="server" Text='<%# Eval("Shift") %>'></asp:Label>
                                               
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
                                                <asp:Label ID="lblFat" runat="server" Text='<%# Eval("Fat") %>'></asp:Label>
                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="LR">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLR" runat="server" Text='<%# Eval("LR") %>'></asp:Label>
                                            </ItemTemplate>
                                             </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Snf">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSnf" runat="server" Text='<%# Eval("Snf") %>'></asp:Label>
                                            </ItemTemplate>
                                             </asp:TemplateField>
                                        <asp:TemplateField HeaderText="kg Fat">
                                            <ItemTemplate>
                                                <asp:Label ID="lblkgFat" runat="server" Text='<%# Eval("kgFat") %>'></asp:Label>
                                            </ItemTemplate>
                                             </asp:TemplateField>
                                        <asp:TemplateField HeaderText="kg snf">
                                            <ItemTemplate>
                                                <asp:Label ID="lblkgSnf" runat="server" Text='<%# Eval("kgSnf") %>'></asp:Label>
                                            </ItemTemplate>
                                             </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnDelete" runat="server" OnClick="lnkbtnDelete_Click" Style="color: red;" CommandName="RecordDelete" OnClientClick="return confirm('Are you sure to Delete this record?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                       <%-- <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" CssClass='<%# Eval("ItemBillingHead_Type").ToString() == "ADDITION" ? "label label-success" : "label label-danger" %>' runat="server" Text='<%# Eval("TotalAmount") %>'></asp:Label>
                                                <asp:Label ID="lblTotalPrice" Visible="false" runat="server" Text='<%# Eval("TotalAmount") %>'></asp:Label>
                                            </ItemTemplate>


                                            <FooterTemplate>

                                                <div>
                                                    <asp:Label ID="lblGrandTotal" Font-Bold="true" runat="server" />
                                                </div>
                                            </FooterTemplate>

                                        </asp:TemplateField>--%>

                                       <%-- <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDeleteHead" OnClick="lnkDeleteHead_Click" runat="server" ToolTip="DeleteHead" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete this record?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                    </Columns>
                                </asp:GridView>
                                </div>
                                
                            </div>
                        </div>
                            <div class="row">
                                <div class="col-md-1">
                                <div class="form-group">
                                    <asp:Button runat="server" CssClass="btn btn-primary" OnClientClick="return ValidatePage();"  Style="margin-top: 20px;" OnClick="btnSave_Click" ValidationGroup="c"  ID="btnSave" Text="Save" />
                                </div>
                            </div>
                            </div>
                    </fieldset>
                    </asp:Panel>
                    

                </div>
            </div>

            <div class="box box-Manish">
                <div class="box-header noprint">
                    <h3 class="box-title">Report</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row noprint">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Entry Date</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Display="Dynamic" ControlToValidate="txtEntryDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="search"></asp:RequiredFieldValidator>
                                </span>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">
                                            <i class="far fa-calendar-alt"></i>
                                        </span>
                                    </div>
                                    <asp:TextBox ID="txtEntryDate" autocomplete="off"  CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>CC<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="search"
                                                        InitialValue="0" ErrorMessage="Select Chilling Center" Text="<i class='fa fa-exclamation-circle' title='Select Chilling Center !'></i>"
                                                        ControlToValidate="ddlFltrCC" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator></span>
                                                <asp:DropDownList ID="ddlFltrCC" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                            </div>
                                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" runat="server" style="margin-top:20px;" ValidationGroup="search" Text="Search" CssClass="btn btn-success" OnClick="btnSearch_Click"/>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                       <div class="col-md-12">
                                    <div class="form-group">
                                         <asp:Button ID="btnExport" runat="server" Visible="false" Text="Export to dbf" CssClass="btn btn-primary" OnClick="btnExport_Click"/>
                                    </div>
                                </div>
                        <div class="col-md-12">
                            <div class="table-responsive">

                            <asp:GridView ID="gv_MilkCollectionChallanEntryDetails" ShowHeader="true" ShowFooter="true"  EmptyDataText="No Record Found" EmptyDataRowStyle-ForeColor="Red" AutoGenerateColumns="false" CssClass="datatable table table-bordered" runat="server">
                                <Columns>
                                      <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>'  runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDt_Date" runat="server" Text='<%# (Convert.ToDateTime(Eval("EntryDate"))).ToString("dd/MM/yyyy") %>'></asp:Label>                                            
                                            </ItemTemplate>                                         
                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Milk Collection Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMCU" runat="server" Text='<%# Eval("OfficeTypeName") %>'></asp:Label>
                                            </ItemTemplate>                      
                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Society">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                            </ItemTemplate>                      
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Shift">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_Shift" runat="server" Text='<%# Eval("Shift") %>'></asp:Label>
                                            </ItemTemplate>                      
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_MilkType" runat="server" Text='<%# Eval("MilkType") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Milk Quality">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuality" runat="server" Text='<%# Eval("MilkQuality") %>'></asp:Label>
                                            </ItemTemplate>
                                          
                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Milk Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblI_MilkSupplyQty" runat="server" Text='<%# Eval("MilkQuantity") %>'></asp:Label>
                                            </ItemTemplate>
                                            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fat %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFat" runat="server" Text='<%# Eval("Fat") %>'></asp:Label>
                                            </ItemTemplate>                                           
                                        </asp:TemplateField>
                                     <asp:TemplateField HeaderText="CLR">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("CLR") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SNF %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("Snf") %>'></asp:Label>
                                            </ItemTemplate>                                          
                                        </asp:TemplateField>

                                        

                                        <asp:TemplateField HeaderText="FAT (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFAT_IN_KG" runat="server" Text='<%# Eval("FatInKg") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SNF (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF_IN_KG" runat="server" Text='<%# Eval("SnfInKg") %>'></asp:Label>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">

        function allnumeric(inputtxt) {
            var numbers = /^[0-9]+$/;
            if (inputtxt.value.match(numbers)) {
                alert('only number has accepted....');
                document.form1.text1.focus();
                return true;
            }
            else {
                alert('Please input numeric value only');
                document.form1.text1.focus();
                return false;
            }
        }

        function myItemDetailsModal() {
            $("#ItemDetailsModal").modal('show');

        }
		$(document).on('focus', '.select2-selection.select2-selection--single', function (e) {
            $(this).closest(".select2-container").siblings('select:enabled').select2('open');
        });
    </script>
    <script type="text/javascript">
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('c');
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
      <link href="../Finance/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="../Finance/js/jquery.dataTables.min.js"></script>
    <script src="../Finance/js/dataTables.bootstrap.min.js"></script>
    <script src="../Finance/js/dataTables.buttons.min.js"></script>
    <script src="../Finance/js/buttons.flash.min.js"></script>
    <script src="../Finance/js/jszip.min.js"></script>
    <script src="../Finance/js/pdfmake.min.js"></script>
    <script src="../Finance/js/vfs_fonts.js"></script>
    <script src="../Finance/js/buttons.html5.min.js"></script>
    <script src="../Finance/js/buttons.print.min.js"></script>
   <script src="js/buttons.colVis.min.js"></script>
   <%-- <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>--%>
    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
        $('.datatable').DataTable({
            paging: true,
            lengthMenu: [10, 25, 50, 100],
            iDisplayLength: 50,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false,
            }],
			 "bSort": false,
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true,
				footer:true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: ('RMRD Challan Entry Details').bold().fontsize(3).toUpperCase(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    title: ('RMRD Challan Entry Details').bold().fontsize(3).toUpperCase(),
                    filename: 'RMRDChallanEntryDetails',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',

                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11]
                    },
                    footer: true
                }],
                dom: {
                    container: {
                        className: 'dt-buttons'
                    },
                    button: {
                        className: 'btn btn-default'
                    }
                }
            }
        });
</script>
</asp:Content>

