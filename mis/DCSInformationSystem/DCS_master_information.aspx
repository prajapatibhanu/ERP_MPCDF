<%@ Page Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DCS_master_information.aspx.cs" Inherits="mis_DCSInformationSystem_DCS_master_information" %>

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
                    <h3 class="box-title">Master Information of DCS</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                             <asp:Label Visible="false" ID="lblDCS_IS_ID" runat="server"></asp:Label>
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
                        <legend>DCS Information Details
                        </legend>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label11" Text="Month" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
										 <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a"
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
                                        <asp:DropDownList ID="DDlMonth" runat="server" CssClass="form-control" OnSelectedIndexChanged="DDlMonth_SelectedIndexChanged">
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
                                        <asp:Label ID="Label12" Text="Year" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
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
                                        <asp:DropDownList ID="ddlyear" runat="server" CssClass="form-control">
                                            <%--<asp:ListItem Value="0">--Select Vehicle--</asp:ListItem>
                                                <asp:ListItem Value="1">Tanker</asp:ListItem>
                                                <asp:ListItem Value="2">Truck</asp:ListItem>
                                                <asp:ListItem Value="3">Jeeps & Cars</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            </br>
                              <div class="col-md-12">
                                  
                                   <div class="col-md-2">
                                <div class="form-group">
                                    <label>CC<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="a"
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
                                  <div class="col-md-3">
                                      <div class="form-group">
                                          <asp:Label ID="lblDCScode" Text="DCS Code" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                          <span class="pull-right">
                                              <asp:RequiredFieldValidator ID="rfvrfvDCScode" ValidationGroup="a"
                                                  ErrorMessage="Enter DCS Code" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Code !'></i>"
                                                  ControlToValidate="txtDCScode" Display="Dynamic" runat="server">
                                              </asp:RequiredFieldValidator>
                                             
                                          </span>
                                          <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode"  placeholder="Enter DCS Code" ClientIDMode="Static"></asp:TextBox>
                                      </div>
                                  </div>
                                  <div class="col-md-3">
                                      <div class="form-group">
                                          <asp:Label ID="lblvillagename" Text="Village Name" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                          <span class="pull-right"></span>
                                          <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtvillagename"  placeholder="Enter Village Name" ClientIDMode="Static"></asp:TextBox>
                                      </div>
                                  </div>
                                  <div class="col-md-3">
                                      <div class="form-group">
                                          <asp:Label ID="lblvgiscode" Text="Village GIS Code" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                          <span class="pull-right">
                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                  ErrorMessage="Enter Village GIS Code" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Secretary Person !'></i>"
                                                  ControlToValidate="txtvgiscode" Display="Dynamic" runat="server">
                                              </asp:RequiredFieldValidator>
                                              <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator9" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtSecretaryPerson"
                                        ErrorMessage="Only alphabet allow" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Only alphabet allow. !'></i>"
                                        SetFocusOnError="true" ValidationExpression="^[a-zA-Z\s]+$">
                                    </asp:RegularExpressionValidator>--%>
                                          </span>
                                          <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtvgiscode" placeholder="Enter Village GIS Code"></asp:TextBox>
                                      </div>
                                  </div>
                                  <div class="col-md-3">
                                      <div class="form-group">
                                          <asp:Label ID="lblMilkRoute" Text="Milk Route" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                          <span class="pull-right">
                                              <asp:RequiredFieldValidator ID="rfvContactPerson" ValidationGroup="a"
                                                  ErrorMessage="Enter Milk Route." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Contact Person. !'></i>"
                                                  ControlToValidate="txtMilkRoute" Display="Dynamic" runat="server">
                                              </asp:RequiredFieldValidator>
                                              <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="Dynamic" ValidationGroup="a"
                                            ErrorMessage="Enter Contact Person. !" Text="<i class='fa fa-exclamation-circle' title='Enter Contact Person !'></i>" ControlToValidate="txtContactPerson"
                                            ValidationExpression="^[0-9]+$">
                                        </asp:RegularExpressionValidator>--%>
                                          </span>
                                          <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtMilkRoute"  placeholder="Enter Milk Route"></asp:TextBox>
                                      </div>
                                  </div>
                              </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;">Affiliated Chilling Centre<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfv2" ValidationGroup="a"
                                                InitialValue="0" ForeColor="Red" ErrorMessage="Enter Affiliated Chilling Centre" Text="<i class='fa fa-exclamation-circle' title='Select Division !'></i>"
                                                ControlToValidate="txtacc" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtacc"  placeholder="Enter Chilling Centre"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;">District<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfv3" ValidationGroup="a"
                                                InitialValue="0" ForeColor="Red" ErrorMessage="Please Select District" Text="<i class='fa fa-exclamation-circle' title='Select !'></i>"
                                                ControlToValidate="txtDistrict" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <%--<asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control select2" AutoPostBack="true" ClientIDMode="Static">
                                    </asp:DropDownList>--%>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDistrict"  placeholder="Enter District Name"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblTehsil" Text="Tehsil" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvTehsil" ValidationGroup="a"
                                                ErrorMessage="Enter DCS Contact No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Contact No. !'></i>"
                                                ControlToValidate="txtTehsil" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ValidationGroup="a"
                                            ErrorMessage="Enter Valid Contact No. !" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Contact No !'></i>" ControlToValidate="txtDCSContactNo"
                                            ValidationExpression="^[0-9]+$">
                                        </asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtTehsil" placeholder="Enter Tehsil Name"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblBlock" Text="Block" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfDCSEmail" ValidationGroup="a"
                                                ErrorMessage="Enter Block" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Office Email !'></i>"
                                                ControlToValidate="txtBlock" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <%--   <asp:RegularExpressionValidator ID="revemail" runat="server" ForeColor="Red" ControlToValidate="txtDCS_Email"
                                            ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                            Display="Dynamic" ErrorMessage="Invalid Email" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle' title='Invalid Email !'></i>" />--%>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtBlock" placeholder="Enter Block Name" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblFieldOfficer" Text="Field Officer" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvofficeaddress" ValidationGroup="a"
                                                ErrorMessage="Enter Field Officer" Text="<i class='fa fa-exclamation-circle' title='Enter Office Address !'></i>"
                                                ControlToValidate="txtFieldOfficer" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <%--<asp:RegularExpressionValidator ID="revofficeaddress" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCSAddress" ErrorMessage="Alphanumeric ,space and some special symbols like .,/-: allow" Text="<i class='fa fa-exclamation-circle' title='Alphanumeric ,space and some special symbols like .,/-: allow !'></i>" SetFocusOnError="true" ValidationExpression="^[0-9a-zA-Z\s.,/-:]+$"></asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtFieldOfficer" placeholder="Field Officer"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblVEO" Text="VEO" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvVEO" ValidationGroup="a"
                                                ErrorMessage="Enter VEO" Text="<i class='fa fa-exclamation-circle' title='Enter Office Pincode !'></i>"
                                                ControlToValidate="txtVEO" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                          <%--  <asp:RegularExpressionValidator ID="revDCSepincode" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtVEO" placeholder="VEO" ></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;">Scheme In Which DCS Open<%--<span style="color: red;"> *</span>--%></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfv7" ValidationGroup="a"
                                                ErrorMessage="Please Enter Scheme In Which DCS Open" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Please Enter Scheme In Which DCS Open !'></i>"
                                                ControlToValidate="txtsiwdcso" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtGstNumber"
                                            ErrorMessage="Invalid GST Number" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Invalid GST Number !'></i>"
                                            SetFocusOnError="true" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:TextBox autocomplete="off" runat="server" CssClass="form-control" ID="txtsiwdcso"  placeholder="Scheme Name"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" Text="Registered As Women" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                           <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a"
                                                ErrorMessage="Enter VEO" Text="<i class='fa fa-exclamation-circle' title='Enter Office Pincode !'></i>"
                                                ControlToValidate="txtVEO" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:CheckBox runat="server" CssClass="form-control" ID="chkrw" Text="Registered As Women" />
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;">Cooperative Registration<%--<span style="color: red;"> *</span>--%></label>
                                        <span class="pull-right">

                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a"
                                    ErrorMessage="Please Enter PAN Number" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Please Enter PAN Number !'></i>"
                                    ControlToValidate="txtPanNumber" Display="Dynamic" runat="server">
                                   </asp:RequiredFieldValidator>--%>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;">Number<span style="color: red;"> *</span></label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="rfvCR_Num" ValidationGroup="a"
                                                            InitialValue="0" ErrorMessage="Enter Cooperative Regn No." Text="<i class='fa fa-exclamation-circle' title='Enter Cooperative Regn No. !'></i>"
                                                            ControlToValidate="txtCR_Num" ForeColor="Red" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>
                                                    </span>
                                                    <asp:TextBox autocomplete="off" runat="server"  CssClass="form-control CapitalClass" ID="txtCR_Num"   ></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;">Date<span style="color: red;"> *</span></label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="rfvCR_Date" ValidationGroup="a"
                                                            InitialValue="0" ErrorMessage="Select Cooperative Regn Date" Text="<i class='fa fa-exclamation-circle' title='Select Cooperative Regn Date !'></i>"
                                                            ControlToValidate="txtcrdate" ForeColor="Red" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>
                                                    </span>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>

                                                        <asp:TextBox ID="txtcrdate" runat="server"  Enabled="false" class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;">Union Membership<%--<span style="color: red;"> *</span>--%></label>
                                        <span class="pull-right">

                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a"
                                    ErrorMessage="Please Enter PAN Number" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Please Enter PAN Number !'></i>"
                                    ControlToValidate="txtPanNumber" Display="Dynamic" runat="server">
                                   </asp:RequiredFieldValidator>--%>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;">Number<span style="color: red;"> *</span></label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a"
                                                            InitialValue="0" ErrorMessage="Enter Union Membership Number" Text="<i class='fa fa-exclamation-circle' title='Enter Union Membership Number !'></i>"
                                                            ControlToValidate="txtum_num" ForeColor="Red" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>
                                                    </span>
                                                    <asp:TextBox autocomplete="off" runat="server" CssClass="form-control CapitalClass" ID="txtum_num"   ></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;">Date<span style="color: red;"> *</span></label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="a"
                                                            InitialValue="0" ErrorMessage="Enter Union Membership Date" Text="<i class='fa fa-exclamation-circle' title='Enter Union Membership Date !'></i>"
                                                            ControlToValidate="txtumdate" ForeColor="Red" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>
                                                    </span>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>

                                                        <asp:TextBox ID="txtumdate" runat="server"  class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;">AI Center<%--<span style="color: red;"> *</span>--%></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                ErrorMessage="Please Select AI Center" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Please Select AI Center !'></i>"
                                                ControlToValidate="ddlaic" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtGstNumber"
                                            ErrorMessage="Invalid GST Number" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Invalid GST Number !'></i>"
                                            SetFocusOnError="true" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:DropDownList ID="ddlaic" CssClass="form-control" runat="server">
                                           <%-- <asp:ListItem Value="0">--Select--</asp:ListItem>--%>
                                             <asp:ListItem Value="Cluster">Cluster</asp:ListItem>
                                            <asp:ListItem Value="Single">Single</asp:ListItem>
                                           
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblAWB" Text="Affiliated With BMC" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvAWB" ValidationGroup="a"
                                                ErrorMessage="Enter Affiliated With BMC" Text="<i class='fa fa-exclamation-circle' title='Enter Affiliated With BMC !'></i>"
                                                ControlToValidate="txtAWB" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtAWB" placeholder="Affiliated With BMC"  ></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblAssemblyArea" Text="Assembly Area" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvAssemblyArea" ValidationGroup="a"
                                                ErrorMessage="Enter Assembly Area" Text="<i class='fa fa-exclamation-circle' title='Enter Assembly Area !'></i>"
                                                ControlToValidate="txtAssemblyArea" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                         <%--   <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtAssemblyArea" placeholder="Assembly Area"  ></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblParliamentArea" Text="Parliament Area" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvParliamentArea" ValidationGroup="a"
                                                ErrorMessage="Enter Parliament Area" Text="<i class='fa fa-exclamation-circle' title='Enter Parliament Area !'></i>"
                                                ControlToValidate="txtParliamentArea" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                           <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtParliamentArea" placeholder="Parliament Area"  ></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <fieldset>
                                <legend>Available Electronic Facility
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">

                                        <div style="float: right">
                                            <%--  <asp:Label ID="Label2" runat="server" Text="You can add maximum 10 rows" Style="color: #ff0000"></asp:Label>--%>
                                            <asp:Button ID="btnAddrow" runat="server" Text="+ Add New Row" CssClass="btn btn-block btn-primary" OnClick="btnAddrow_Click" Width="150px" Style="padding: 5px; font-size: 14px" />
                                        </div>

                                        <asp:GridView runat="server" ID="gvAvailableElectronicFacility" CssClass="table table-bordered" OnDataBound="gvAvailableElectronicFacility_DataBound" ShowHeader="true" ShowFooter="false" AllowPaging="false" AutoGenerateColumns="false" OnRowDeleting="gvAvailableElectronicFacility_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S No." ItemStyle-Width="10">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblslNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        <asp:Label ID="lblsno" Visible="false" Text='<%# Eval("Sno").ToString()%>' runat="server" />

                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Facility Name">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAEFFacility_Name" Text='<%# Eval("AEFFacility_Name").ToString()%>' runat="server" class="form-control" ></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvAEF_FacilityName" ValidationGroup="a"
                                                            ErrorMessage="Enter  Facility Name" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Facility Name !'></i>"
                                                            ControlToValidate="txtAEFFacility_Name" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>
                                                        <%-- <asp:RegularExpressionValidator  ID="rEvPurchase_date" ValidationGroup="a"
                                                            ErrorMessage="Enter date in correct formate" ValidationExpression="(0?[1-9]|[12][0-9]|3[01])-(0?[1-9]|1[012])-((19|20|21)[0-9][0-9])" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date in correct formate !'></i>"
                                                            ControlToValidate="txtPurchase_date" Display="Dynamic" runat="server">

                                                         </asp:RegularExpressionValidator>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="OWN(Yes/No)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAEF_OWN" Text='<%# Eval("AEF_OWN").ToString()%>' runat="server" Height="10%" class="form-control" />
                                                        <asp:RequiredFieldValidator ID="rfvAEF_OWN" ValidationGroup="a"
                                                            ErrorMessage="Enter OWN" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter OWN !'></i>"
                                                            ControlToValidate="txtAEF_OWN" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Scheme">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAEF_Scheme" Text='<%# Eval("AEF_Scheme").ToString()%>' runat="server" Height="10%" class="form-control" />
                                                        <asp:RequiredFieldValidator ID="rfvAEF_Scheme" ValidationGroup="a"
                                                            ErrorMessage="Enter Scheme" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Scheme !'></i>"
                                                            ControlToValidate="txtAEF_Scheme" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Inst.Date">
                                                    <ItemTemplate>
                                                         <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                        <asp:TextBox ID="txtAEF_InstDate" Text='<%# Eval("AEF_InstDate").ToString()%>' runat="server" class="form-control DateAdd" autocomplete="off" data-provide="datepicker"  onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvAEF_InstDate" ValidationGroup="a"
                                                            ErrorMessage="Enter Inst.Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Inst.Date !'></i>"
                                                            ControlToValidate="txtAEF_InstDate" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>                                                      
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="50px" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="Delete" runat="server" Width="50%" CausesValidation="False" ImageUrl="~/mis/image/Del.png" CommandName="Delete" OnClientClick="return confirm('The item will be deleted. Are you sure want to continue?');"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Bulk Milk Cooler
                                </legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label8" Text="Capacity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvBMCcapacity" ValidationGroup="a"
                                                    ErrorMessage="Enter BMC Capacity" Text="<i class='fa fa-exclamation-circle' title='Enter BMC Capacity !'></i>"
                                                    ControlToValidate="txtBMCcapacity" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                               <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator29" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtBMCcapacity" onkeypress="return validateDec(this, event)"   ></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label9" Text="Scheme" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RvfBMCScheme" ValidationGroup="a"
                                                    ErrorMessage="Enter BMC Scheme" Text="<i class='fa fa-exclamation-circle' title='Enter BMC Scheme !'></i>"
                                                    ControlToValidate="txtBMCScheme" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                              <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator30" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtBMCScheme" placeholder=""  ></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label10" Text="Inst.Date" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvBMCInstDate" ValidationGroup="a"
                                                    ErrorMessage="Enter BMC Inst.Date" Text="<i class='fa fa-exclamation-circle' title='Enter BMC Inst.Date !'></i>"
                                                    ControlToValidate="txtBMCInstDate" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator31" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVEO" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>--%>
                                            </span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>

                                                <asp:TextBox ID="txtBMCInstDate" runat="server" placeholder="" class="form-control DateAdd" autocomplete="off" data-provide="datepicker"  onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
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
                                <a class="btn btn-block btn-default" href="DCS_master_information.aspx">Clear</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.box-body -->
            <div class="box box-body" >
                <div class="box-header">
                    <h3 class="box-title">Master Information of DCS Info</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="table-responsive">
                          <div class="col-md-12">
                               <div class="col-md-2">
                                <div class="form-group">
                                    <label>CC<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Search"
                                            InitialValue="0" ErrorMessage="Select CC" Text="<i class='fa fa-exclamation-circle' title='Select CC !'></i>"
                                            ControlToValidate="ddlccbmcdetailflt" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlccbmcdetailflt"  runat="server" CssClass="form-control select2" ClientIDMode="Static" OnSelectedIndexChanged="ddlccbmcdetailflt_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                                <div class="col-md-2">
                                    <label>Society<span style="color: red;">*</span></label>
                                   <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ValidationGroup="a" ControlToValidate="ddlSocietyflt" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Society!'></i>" ErrorMessage="Select Society" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>--%>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlSocietyflt" CssClass="form-control select2" runat="server">
                                            
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label2" Text="Month" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Search"
                                            ErrorMessage="Select Month" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Month !'></i>"
                                            ControlToValidate="DDlMonth2" InitialValue="0" Display="Dynamic" runat="server">
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
                                        <asp:Label ID="Label3" Text="Year"  Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
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
                              <div class="col-md-4">
                         <asp:Button runat="server" BackColor="#2e9eff" CssClass="btn btn-success" ValidationGroup="Search" Text="Search" ID="btnSearch" OnClick="btnSearch_Click" Style="margin-top: 20px; width: 80px;" />
                       </div>
                            </div>
                        
                                <div class="col-md-12">
                        <asp:GridView runat="server" ID="gvDCS_detail" Visible="false"  CssClass="table table-bordered"   ShowHeader="true" ShowFooter="false" AllowPaging="false" AutoGenerateColumns="false"  OnRowCommand="gvDCS_detail_RowCommand" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S No." ItemStyle-Width="10">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblslNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                           <%--  <asp:Label Visible="false" ID="lblMPR_DP_Id" Text='<%# Eval("MPR_DP_Id").ToString()%>' runat="server" />--%>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ID" Visible="false" >
                                                        <ItemTemplate>
                                                               <asp:Label ID="lblDCS_IS_ID" Text='<%# Eval("DCS_IS_ID").ToString()%>' runat="server" />

                                                     
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DCS Name">
                                                        <ItemTemplate>
                                                         <asp:Label  ID="lblDCSName" Text='<%# Eval("DCSName").ToString()%>' runat="server" class="form-control"></asp:Label>
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
                                                                        <asp:ImageButton ID="EDIT" runat="server" Width="100%" Enabled='<%# Eval("edit_status").ToString() =="1"?true:false %>' CausesValidation="False"  ImageUrl="~/mis/image/edit.png" CommandName="select" CommandArgument='<%# Bind("DCS_IS_ID") %>'></asp:ImageButton>
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
