<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DCS_semifixed_nature_info.aspx.cs" Inherits="mis_DCSInformationSystem_DCS_semifixed_nature_info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
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
                         <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSubmit_Click" Style="margin-top: 20px; width: 50px;" />
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
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-body">
                <div class="box-header">
                    <h3 class="box-title">Semi Fixed Nature Information of DCS</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            <asp:Label ID="lblSFN_DCS_ID" Visible="false" runat="server"></asp:Label>
                        </div>
                    </div>
                    <%--<fieldset>
                        <legend>Office Type Details
                        </legend>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Office Type:-</label>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvSContainerTypeName" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="RblContainerTypeName" ErrorMessage="Select Container Type" Text="<i class='fa fa-exclamation-circle' title='Select Container Type!'></i>">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <div style="margin-left: -70px;">
                                        <asp:RadioButtonList runat="server" ID="RblContainerTypeName" RepeatDirection="Horizontal" CssClass="pull-left" AutoPostBack="true" ClientIDMode="Static">
                                            <asp:ListItem Value="1">DS &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</asp:ListItem>
                                            <asp:ListItem Value="2">MCU</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>


                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Milk Collection Unit<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select Milk Collection Unit" Text="<i class='fa fa-exclamation-circle' title='Select Milk Collection Unit !'></i>"
                                            ControlToValidate="ddlMilkCollectionUnit" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlMilkCollectionUnit" OnInit="ddlMilkCollectionUnit_Init" OnSelectedIndexChanged="ddlMilkCollectionUnit_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group" id="snapName" runat="server" visible="false">
                                    <asp:Label ID="lblName" Text="Name" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                            InitialValue="0" ForeColor="Red" ErrorMessage="Select  Milk Collection Unit Name" Text="<i class='fa fa-exclamation-circle' title='Select Milk Collection Unit Name !'></i>"
                                            ControlToValidate="ddlMilkColUnitName" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlMilkColUnitName" runat="server" CssClass="form-control select2" AutoPostBack="true" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </fieldset>--%>
                    <fieldset>
                        <legend>DCS Semi Fixed Nature Information
                        </legend>
                        <div class="row">
                             <div class="col-md-2">
                                <div class="form-group">
                                    <label>CC<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator38" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select CC" Text="<i class='fa fa-exclamation-circle' title='Select CC !'></i>"
                                            ControlToValidate="ddlccbmcdetail" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlccbmcdetail"  runat="server" CssClass="form-control select2" ClientIDMode="Static" OnSelectedIndexChanged="ddlccbmcdetail_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                                <div class="col-md-2">
                                    <label>Society<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvBMC" runat="server" Display="Dynamic" ValidationGroup="a" ControlToValidate="ddlSociety" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Society!'></i>" ErrorMessage="Select Society" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlSociety" CssClass="form-control select2" runat="server" OnSelectedIndexChanged="ddlSociety_SelectedIndexChanged" AutoPostBack="true">
                                            
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="lblDCScode" Text="(1) DCS Code" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvrfvDCScode" ValidationGroup="a"
                                            ErrorMessage="Enter DCS Code" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Code !'></i>"
                                            ControlToValidate="txtDCScode" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revDCScode" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="Enter DCS Code" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label41" Text="Month" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
										 <asp:RequiredFieldValidator ID="rfvmonth" ValidationGroup="a"
                                            ErrorMessage="Select Month" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Month !'></i>"
                                            ControlToValidate="DDlMonth" InitialValue="0" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                            ErrorMessage="Enter DCS Code" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Code !'></i>"
                                            ControlToValidate="txtDCScode" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:DropDownList ID="DDlMonth" runat="server"  CssClass="form-control" OnSelectedIndexChanged="DDlMonth_SelectedIndexChanged">
                                             <asp:ListItem Value="0">--Select Month--</asp:ListItem>
                                            <asp:ListItem Value="1">January</asp:ListItem>
                                            <asp:ListItem Value="2">February</asp:ListItem>
                                            <asp:ListItem Value="3">March</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">June</asp:ListItem>
                                            <asp:ListItem Value="7">July</asp:ListItem>
                                            <asp:ListItem Value="8">August</asp:ListItem>
                                            <asp:ListItem Value="9">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4" style="display:none">
                                    <div class="form-group">
                                        <asp:Label ID="Label46" Text="Year" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                            ErrorMessage="Enter DCS Code" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Code !'></i>"
                                            ControlToValidate="txtDCScode" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:DropDownList ID="ddlyear" runat="server"  CssClass="form-control" >
                                            <%--<asp:ListItem Value="0">--Select Vehicle--</asp:ListItem>
                                                <asp:ListItem Value="1">Tanker</asp:ListItem>
                                                <asp:ListItem Value="2">Truck</asp:ListItem>
                                                <asp:ListItem Value="3">Jeeps & Cars</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                        </div>
                        <fieldset>
                            <legend>(2) Total Number Of Members
                            </legend>
                            <div class="row">
                                 <div class="col-md-12">
                                     <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" Text="(i) General Category" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 800;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                           <%-- <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter VEO" Text="<i class='fa fa-exclamation-circle' title='Enter Office Pincode !'></i>"
                                                    ControlToValidate="txtVEO" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>
                                            </span>--%>
     
                                        </div>
                                    </div>
                                     </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label2" Text="Landless Labour" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="a"
                                                    ErrorMessage="Enter Landless Labour" Text="<i class='fa fa-exclamation-circle' title='Enter Landless Labour !'></i>"
                                                    ControlToValidate="txtTNM_GC_LL" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                 <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtTNM_GC_LL" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label12" Text="Marginal Farmers" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                           
                                            <span class="pull-right">
                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="a"
                                                    ErrorMessage="Enter Marginal Farmers" Text="<i class='fa fa-exclamation-circle' title='Enter Marginal Farmers !'></i>"
                                                    ControlToValidate="txtTNM_GC_MF" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtTNM_GC_MF" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                              <asp:Label ID="Label13" Text="Small Farmers" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="a"
                                                    ErrorMessage="Enter Small Farmers" Text="<i class='fa fa-exclamation-circle' title='Enter Small Farmers !'></i>"
                                                    ControlToValidate="txtTNM_GC_SF" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                               <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtTNM_GC_SF" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                              <asp:Label ID="Label14" Text="Large Farmers & Others" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter Large Farmers & Others" Text="<i class='fa fa-exclamation-circle' title='Enter Large Farmers & Others !'></i>"
                                                    ControlToValidate="txtTNM_GC_LFO" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtTNM_GC_LFO" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                     <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label3" Text="(ii) Schedule Caste" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 800;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                           <%-- <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter VEO" Text="<i class='fa fa-exclamation-circle' title='Enter Office Pincode !'></i>"
                                                    ControlToValidate="txtVEO" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>
                                            </span>--%>
     
                                        </div>
                                    </div>
                                     </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label4" Text="Landless Labour" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                    ErrorMessage="Enter Landless Labour" Text="<i class='fa fa-exclamation-circle' title='Enter Landless Labour !'></i>"
                                                    ControlToValidate="txtTNM_SC_LL" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtTNM_SC_LL" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label15" Text="Marginal Farmers" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                           
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                    ErrorMessage="Enter Marginal Farmers" Text="<i class='fa fa-exclamation-circle' title='Enter Marginal Farmers !'></i>"
                                                    ControlToValidate="txtTNM_SC_MF" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtTNM_SC_MF" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                              <asp:Label ID="Label16" Text="Small Farmers" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                    ErrorMessage="Enter Small Farmers" Text="<i class='fa fa-exclamation-circle' title='Enter Small Farmers !'></i>"
                                                    ControlToValidate="txtTNM_SC_SF" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtTNM_SC_SF" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                              <asp:Label ID="Label17" Text="Large Farmers & Others" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a"
                                                    ErrorMessage="Enter Large Farmers & Others" Text="<i class='fa fa-exclamation-circle' title='Enter Large Farmers & Others !'></i>"
                                                    ControlToValidate="txtTNM_SC_LFO" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtTNM_SC_LFO" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                     <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label5" Text="(iii) Schedule Tribes" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 800;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                           <%-- <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter VEO" Text="<i class='fa fa-exclamation-circle' title='Enter Office Pincode !'></i>"
                                                    ControlToValidate="txtVEO" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>
                                            </span>--%>
     
                                        </div>
                                    </div>
                                     </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label6" Text="Landless Labour" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a"
                                                    ErrorMessage="Enter Landless Labour" Text="<i class='fa fa-exclamation-circle' title='Enter Landless Labour !'></i>"
                                                    ControlToValidate="txtTNM_ST_LL" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtTNM_ST_LL" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label18" Text="Marginal Farmers" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                           
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="a"
                                                    ErrorMessage="Enter Marginal Farmers" Text="<i class='fa fa-exclamation-circle' title='Enter Marginal Farmers !'></i>"
                                                    ControlToValidate="txtTNM_ST_MF" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtTNM_ST_MF" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                              <asp:Label ID="Label19" Text="Small Farmers" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a"
                                                    ErrorMessage="Enter Small Farmers" Text="<i class='fa fa-exclamation-circle' title='Enter Small Farmers !'></i>"
                                                    ControlToValidate="txtTNM_ST_SF" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtTNM_ST_SF" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                              <asp:Label ID="Label20" Text="Large Farmers & Others" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="a"
                                                    ErrorMessage="Enter Large Farmers & Others" Text="<i class='fa fa-exclamation-circle' title='Enter Large Farmers & Others !'></i>"
                                                    ControlToValidate="txtTNM_ST_LFO" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtTNM_ST_LFO" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                     <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label7" Text="(iv) Other Backward Class" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 800;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                           <%-- <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter VEO" Text="<i class='fa fa-exclamation-circle' title='Enter Office Pincode !'></i>"
                                                    ControlToValidate="txtVEO" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>
                                            </span>--%>
     
                                        </div>
                                    </div>
                                     </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label11" Text="Landless Labour" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="a"
                                                    ErrorMessage="Enter Landless Labour" Text="<i class='fa fa-exclamation-circle' title='Enter Landless Labour !'></i>"
                                                    ControlToValidate="txtTNM_OBC_LL" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtTNM_OBC_LL" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label21" Text="Marginal Farmers" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                           
                                            <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ValidationGroup="a"
                                                    ErrorMessage="Enter Marginal Farmers" Text="<i class='fa fa-exclamation-circle' title='Enter Marginal Farmers !'></i>"
                                                    ControlToValidate="txtTNM_OBC_MF" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtTNM_OBC_MF" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                              <asp:Label ID="Label22" Text="Small Farmers" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ValidationGroup="a"
                                                    ErrorMessage="Enter Small Farmers" Text="<i class='fa fa-exclamation-circle' title='Enter Small Farmers !'></i>"
                                                    ControlToValidate="txtTNM_OBC_SF" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtTNM_OBC_SF" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                              <asp:Label ID="Label23" Text="Large Farmers & Others" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ValidationGroup="a"
                                                    ErrorMessage="Enter Large Farmers & Others" Text="<i class='fa fa-exclamation-circle' title='Enter Large Farmers & Others !'></i>"
                                                    ControlToValidate="txtTNM_OBC_LFO" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtTNM_OBC_LFO" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>(3) Share Capital Amount in Rs
                            </legend>
                            <div class="row">
                                 <%--<div class="col-md-12">
                                     <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label41" Text="General Category" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                           <%-- <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter VEO" Text="<i class='fa fa-exclamation-circle' title='Enter Office Pincode !'></i>"
                                                    ControlToValidate="txtVEO" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>
                                            </span>
     
                                        </div>
                                    </div>
                                     </div>--%>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label42" Text="Landless Labour" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ValidationGroup="a"
                                                    ErrorMessage="Enter Landless Labour" Text="<i class='fa fa-exclamation-circle' title='Enter Landless Labour !'></i>"
                                                    ControlToValidate="txtSCA_LL" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateDec(this, event)" autocomplete="off" CssClass="form-control" ID="txtSCA_LL" placeholder="0" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label43" Text="Marginal Farmers" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                           
                                            <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ValidationGroup="a"
                                                    ErrorMessage="Enter Marginal Farmers" Text="<i class='fa fa-exclamation-circle' title='Enter Marginal Farmers !'></i>"
                                                    ControlToValidate="txtSCA_MF" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateDec(this, event)" autocomplete="off" CssClass="form-control" ID="txtSCA_MF" placeholder="0" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                              <asp:Label ID="Label44" Text="Small Farmers" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ValidationGroup="a"
                                                    ErrorMessage="Enter Small Farmers" Text="<i class='fa fa-exclamation-circle' title='Enter Small Farmers !'></i>"
                                                    ControlToValidate="txtSCA_SF" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateDec(this, event)" autocomplete="off" CssClass="form-control" ID="txtSCA_SF" placeholder="0" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                              <asp:Label ID="Label45" Text="Large Farmers & Others" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" ValidationGroup="a"
                                                    ErrorMessage="Enter Large Farmers & Others" Text="<i class='fa fa-exclamation-circle' title='Enter Large Farmers & Others !'></i>"
                                                    ControlToValidate="txtSCA_LFO" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateDec(this, event)" autocomplete="off" CssClass="form-control" ID="txtSCA_LFO" placeholder="0" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                </div>
                                </div>
                            </fieldset>
                        <fieldset>
                            <legend>Women Members
                            </legend>
                            <div class="row">
                                 <div class="col-md-12">
                                     <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label8" Text="General Category" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 800;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                           <%-- <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter VEO" Text="<i class='fa fa-exclamation-circle' title='Enter Office Pincode !'></i>"
                                                    ControlToValidate="txtVEO" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>
                                            </span>--%>
     
                                        </div>
                                    </div>
                                     </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label9" Text="Landless Labour" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ValidationGroup="a"
                                                    ErrorMessage="Enter Landless Labour" Text="<i class='fa fa-exclamation-circle' title='Enter Landless Labour !'></i>"
                                                    ControlToValidate="txtWM_GC_LL" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" ClientIDMode="Static" autocomplete="off" CssClass="form-control" ID="txtWM_GC_LL" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label10" Text="Marginal Farmers" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                           
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ValidationGroup="a"
                                                    ErrorMessage="Enter Marginal Farmers" Text="<i class='fa fa-exclamation-circle' title='Enter Marginal Farmers !'></i>"
                                                    ControlToValidate="txtWM_GC_MF" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtWM_GC_MF" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                              <asp:Label ID="Label24" Text="Small Farmers" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator23" ValidationGroup="a"
                                                    ErrorMessage="Enter Small Farmers" Text="<i class='fa fa-exclamation-circle' title='Enter Small Farmers !'></i>"
                                                    ControlToValidate="txtWM_GC_SF" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtWM_GC_SF" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                              <asp:Label ID="Label25" Text="Large Farmers & Others" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator24" ValidationGroup="a"
                                                    ErrorMessage="Enter Large Farmers & Others" Text="<i class='fa fa-exclamation-circle' title='Enter Large Farmers & Others !'></i>"
                                                    ControlToValidate="txtWM_GC_LFO" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtWM_GC_LFO" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                     <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label26" Text="Schedule Caste" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 800;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                           <%-- <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter VEO" Text="<i class='fa fa-exclamation-circle' title='Enter Office Pincode !'></i>"
                                                    ControlToValidate="txtVEO" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>
                                            </span>--%>
     
                                        </div>
                                    </div>
                                     </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label27" Text="Landless Labour" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator25" ValidationGroup="a"
                                                    ErrorMessage="Enter Landless Labour" Text="<i class='fa fa-exclamation-circle' title='Enter Landless Labour !'></i>"
                                                    ControlToValidate="txtWM_SC_LL" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtWM_SC_LL" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label28" Text="Marginal Farmers" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                           
                                            <span class="pull-right">
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator26" ValidationGroup="a"
                                                    ErrorMessage="Enter Marginal Farmers" Text="<i class='fa fa-exclamation-circle' title='Enter Marginal Farmers !'></i>"
                                                    ControlToValidate="txtWM_SC_MF" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtWM_SC_MF" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                              <asp:Label ID="Label29" Text="Small Farmers" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator27" ValidationGroup="a"
                                                    ErrorMessage="Enter Small Farmers" Text="<i class='fa fa-exclamation-circle' title='Enter Small Farmers !'></i>"
                                                    ControlToValidate="txtWM_SC_SF" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtWM_SC_SF" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                              <asp:Label ID="Label30" Text="Large Farmers & Others" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator28" ValidationGroup="a"
                                                    ErrorMessage="Enter Large Farmers & Others" Text="<i class='fa fa-exclamation-circle' title='Enter Large Farmers & Others !'></i>"
                                                    ControlToValidate="txtWM_SC_LFO" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtWM_SC_LFO" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                     <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label31" Text="Schedule Tribes" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 800;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                           <%-- <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter VEO" Text="<i class='fa fa-exclamation-circle' title='Enter Office Pincode !'></i>"
                                                    ControlToValidate="txtVEO" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>
                                            </span>--%>
     
                                        </div>
                                    </div>
                                     </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label32" Text="Landless Labour" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator29" ValidationGroup="a"
                                                    ErrorMessage="Enter Landless Labour" Text="<i class='fa fa-exclamation-circle' title='Enter Landless Labour !'></i>"
                                                    ControlToValidate="txtWM_ST_LL" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtWM_ST_LL" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label33" Text="Marginal Farmers" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                           
                                            <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator30" ValidationGroup="a"
                                                    ErrorMessage="Enter Marginal Farmers" Text="<i class='fa fa-exclamation-circle' title='Enter Marginal Farmers !'></i>"
                                                    ControlToValidate="txtWM_ST_MF" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtWM_ST_MF" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                              <asp:Label ID="Label34" Text="Small Farmers" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator31" ValidationGroup="a"
                                                    ErrorMessage="Enter Small Farmers" Text="<i class='fa fa-exclamation-circle' title='Enter Small Farmers !'></i>"
                                                    ControlToValidate="txtWM_ST_SF" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtWM_ST_SF" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                              <asp:Label ID="Label35" Text="Large Farmers & Others" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator32" ValidationGroup="a"
                                                    ErrorMessage="Enter Large Farmers & Others" Text="<i class='fa fa-exclamation-circle' title='Enter Large Farmers & Others !'></i>"
                                                    ControlToValidate="txtWM_ST_LFO" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtWM_ST_LFO" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                     <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label36" Text="Other Backward Class" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 800;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                           <%-- <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter VEO" Text="<i class='fa fa-exclamation-circle' title='Enter Office Pincode !'></i>"
                                                    ControlToValidate="txtVEO" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>
                                            </span>--%>
     
                                        </div>
                                    </div>
                                     </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label37" Text="Landless Labour" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator33" ValidationGroup="a"
                                                    ErrorMessage="Enter Landless Labour" Text="<i class='fa fa-exclamation-circle' title='Enter Landless Labour !'></i>"
                                                    ControlToValidate="txtWM_OBC_LL" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtWM_OBC_LL" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label38" Text="Marginal Farmers" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                           
                                            <span class="pull-right">
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator34" ValidationGroup="a"
                                                    ErrorMessage="Enter Marginal Farmers" Text="<i class='fa fa-exclamation-circle' title='Enter Marginal Farmers !'></i>"
                                                    ControlToValidate="txtWM_OBC_MF" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtWM_OBC_MF" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                              <asp:Label ID="Label39" Text="Small Farmers" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator35" ValidationGroup="a"
                                                    ErrorMessage="Enter Small Farmers" Text="<i class='fa fa-exclamation-circle' title='Enter Small Farmers !'></i>"
                                                    ControlToValidate="txtWM_OBC_SF" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtWM_OBC_SF" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                              <asp:Label ID="Label40" Text="Large Farmers & Others" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator36" ValidationGroup="a"
                                                    ErrorMessage="Enter Large Farmers & Others" Text="<i class='fa fa-exclamation-circle' title='Enter Large Farmers & Others !'></i>"
                                                    ControlToValidate="txtWM_OBC_LFO" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                               <asp:TextBox runat="server" Text="0" onkeypress="return validateNum(event)" autocomplete="off" CssClass="form-control" ID="txtWM_OBC_LFO" placeholder="" ></asp:TextBox>
     
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>

                         
                    </fieldset>

                    <div class="row">
                        <hr />
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" OnClick="btnSubmit_Click" />
                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" Visible="false" ValidationGroup="a" ID="btnupdate" Text="Update" OnClientClick="return ValidatePage();" AccessKey="S"  OnClick="btnupdate_Click"/>
                                 </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a class="btn btn-block btn-default" href="DCS_semifixed_nature_info.aspx">Clear</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.box-body -->
             <div class="box box-body" >
                <div class="box-header">
                    <h3 class="box-title">Semi Fixed Nature Information of DCS Detail</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="table-responsive">
                          <div class="col-md-12">
                              <div class="col-md-2">
                                <div class="form-group">
                                    <label>CC<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator39" ValidationGroup="Search"
                                            InitialValue="0" ErrorMessage="Select CC" Text="<i class='fa fa-exclamation-circle' title='Select CC !'></i>"
                                            ControlToValidate="ddlccbmcdetailflt" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlccbmcdetailflt"  runat="server" CssClass="form-control select2" ClientIDMode="Static" OnSelectedIndexChanged="ddlccbmcdetailflt_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                                <div class="col-md-2">
                                    <label>Society<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" Display="Dynamic" ValidationGroup="Save" ControlToValidate="ddlSocietyflt" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Society!'></i>" ErrorMessage="Select Society" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlSocietyflt" CssClass="form-control select2" runat="server">
                                            
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label47" Text="Month" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator37" ValidationGroup="Search"
                                            ErrorMessage="Select Month" InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Month !'></i>"
                                            ControlToValidate="DDlMonth2" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:DropDownList ID="DDlMonth2" runat="server" CssClass="form-control" OnSelectedIndexChanged="DDlMonth2_SelectedIndexChanged" >
                                             <asp:ListItem Value="0">--Select Month--</asp:ListItem>
                                            <asp:ListItem Value="1">January</asp:ListItem>
                                            <asp:ListItem Value="2">February</asp:ListItem>
                                            <asp:ListItem Value="3">March</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">June</asp:ListItem>
                                            <asp:ListItem Value="7">July</asp:ListItem>
                                            <asp:ListItem Value="8">August</asp:ListItem>
                                            <asp:ListItem Value="9">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label48" Text="Year" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                            ErrorMessage="Enter DCS Code" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Code !'></i>"
                                            ControlToValidate="txtDCScode" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:DropDownList ID="ddlyear2" runat="server" CssClass="form-control"  OnSelectedIndexChanged="DDlMonth2_SelectedIndexChanged" >
                                            <%--<asp:ListItem Value="0">--Select Vehicle--</asp:ListItem>
                                                <asp:ListItem Value="1">Tanker</asp:ListItem>
                                                <asp:ListItem Value="2">Truck</asp:ListItem>
                                                <asp:ListItem Value="3">Jeeps & Cars</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                              <div class="col-md-2">
                         <asp:Button runat="server" BackColor="#2e9eff" CssClass="btn btn-success" ValidationGroup="Search" Text="Search" ID="btnSearch" OnClick="btnSearch_Click" Style="margin-top: 20px; width: 80px;" />
                       </div>
                            </div>
                        
                                <div class="col-md-12">
                        <asp:GridView runat="server" ID="gvSFNI_detail" Visible="false"  CssClass="table table-bordered"   ShowHeader="true" ShowFooter="false" AllowPaging="false" AutoGenerateColumns="false" OnRowCommand="gvSFNI_detail_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S No." ItemStyle-Width="10">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblslNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                           <%--  <asp:Label Visible="false" ID="lblMPR_DP_Id" Text='<%# Eval("MPR_DP_Id").ToString()%>' runat="server" />--%>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="DCS Name">
                                                        <ItemTemplate>
                                                               <asp:Label  ID="lblDCSName" Text='<%# Eval("DCSName").ToString()%>' runat="server" />
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ID" Visible="false">
                                                        <ItemTemplate>
                                                               <asp:Label  ID="lblSFN_DCS_ID" Text='<%# Eval("SFN_DCS_ID").ToString()%>' runat="server" />

                                                     
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Year">
                                                        <ItemTemplate>
                                                              <asp:Label  ID="lblYear" Text='<%# Eval("Year").ToString()%>' runat="server" class="form-control"></asp:Label>

                                                     
                                                             </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText=" Month">
                                                        <ItemTemplate>
                                                         <asp:Label  ID="lblMonth" Text='<%# Eval("month_name").ToString()%>' runat="server" class="form-control"></asp:Label>
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Creation Date">
                                                        <ItemTemplate>
                                                              <asp:Label  ID="lblCreatedAt" Text='<%# Eval("CreatedAt").ToString()%>' runat="server" class="form-control"></asp:Label>
                                                        
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Un-Skilled *">
                                                        <ItemTemplate>
                                                              <asp:Label  ID="lblNum_unskilled" Text='<%# Eval("Num_unskilled").ToString()%>' runat="server" class="form-control"></asp:Label>
                                                       
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Semi Skilled">
                                                        <ItemTemplate>
                                                              <asp:Label  ID="lblNum_semi_skilled" Text='<%# Eval("Num_semi_skilled").ToString()%>' runat="server" class="form-control"></asp:Label>
                                                        
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Skilled *">
                                                        <ItemTemplate>
                                                               <asp:Label  ID="lblNum_skilled" Text='<%# Eval("Num_skilled").ToString()%>' runat="server" class="form-control"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Un-Skilled">
                                                        <ItemTemplate>
                                                              <asp:Label  ID="lblWBA_unskilled" Text='<%# Eval("WBA_unskilled").ToString()%>' runat="server" class="form-control"></asp:Label>
                                                       
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Semi Skilled">
                                                        <ItemTemplate>
                                                                <asp:Label  ID="lbWBA_semi_skilled" Text='<%# Eval("WBA_semi_skilled").ToString()%>' runat="server" class="form-control"></asp:Label>
                                                     
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Skilled *">
                                                        <ItemTemplate>
                                                               <asp:Label  ID="lblWBA_skilled" Text='<%# Eval("WBA_skilled").ToString()%>'  runat="server" class="form-control"></asp:Label>
                                                        
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                   
                                                    <asp:TemplateField HeaderStyle-Width="60px" ItemStyle-CssClass="text-center" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="EDIT" Enabled='<%# Eval("edit_status").ToString() =="1"?true:false %>' runat="server" Width="100%" CausesValidation="False"  ImageUrl="~/mis/image/edit.png" CommandName="select" CommandArgument='<%# Bind("SFN_DCS_ID") %>'></asp:ImageButton>
                                                                    </ItemTemplate>
                                                     </asp:TemplateField>

                                                </Columns>

                                            </asp:GridView>
                                    </div>
                    </div>
                </div>

            </div>
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
   <script type="text/javascript">
       function validateNum(evt) {
           evt = (evt) ? evt : window.event;
           var charCode = (evt.which) ? evt.which : evt.keyCode;
           if ((charCode > 32 && charCode < 48) || charCode > 57) {

               return false;
           }
           return true;
       }
    </script>
    <script type="text/javascript">
        function onlyNumber(ob) {
            var invalidChars = /\D+/g;
            if (invalidChars.test(ob.value)) {
                ob.value = ob.value.replace(invalidChars, "");
            }
        }

        function validateDec(el, evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            var number = el.value.split('.');
            var chkhyphen = el.value.split('-');
            //if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 45) {
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {

                return false;
            }
            //just one dot (thanks ddlab)
            if ((number.length > 1 && charCode == 46) || (chkhyphen.length > 1 && charCode == 45)) {
                return false;
            }
            //get the carat position
            var caratPos = getSelectionStart(el);
            var dotPos = el.value.indexOf(".");
            if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
                return false;
            }
            return true;
        }

      
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
    </script>
</asp:Content>

