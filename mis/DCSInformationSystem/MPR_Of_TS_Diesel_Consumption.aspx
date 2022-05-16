<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MPR_Of_TS_Diesel_Consumption.aspx.cs" Inherits="mis_DCSInformationSystem_Diesel_Consumption" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                    <h3 class="box-title">Diesel Consumption</h3>

                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                               <asp:Label ID="lblDC_ID" Visible="false" runat="server"></asp:Label>
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
                 <%--   <asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
                        <ContentTemplate>
                            <fieldset>
                                <legend>Diesel Consumption
                                </legend>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Label ID="lblVehicleType" Text="Vehicle Type" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvrfvDCScode" ValidationGroup="a"
                                                    ErrorMessage="Select Vehicle Type" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Code !'></i>"
                                                    ControlToValidate="ddlVehicleType" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                            </span>
                                            <asp:DropDownList ID="ddlVehicleType" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlVehicleType_SelectedIndexChanged">
                                                <asp:ListItem Value="0">--Select Vehicle--</asp:ListItem>
                                                <asp:ListItem Value="1">Tanker</asp:ListItem>
                                                <asp:ListItem Value="2">Truck</asp:ListItem>
                                                <asp:ListItem Value="3">Jeeps & Cars</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <%--<div class="col-md-4">
                                        <div class="form-group">
                                            
                                        </div>
                                    </div>--%>
                                    <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label41" Text="Month" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                            ErrorMessage="Enter DCS Code" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Code !'></i>"
                                            ControlToValidate="txtDCScode" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:DropDownList ID="DDlMonth" runat="server" CssClass="form-control" >
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
                                <div class="col-md-4">
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
                                        <asp:DropDownList ID="ddlyear" runat="server"  CssClass="form-control">
                                            <%--<asp:ListItem Value="0">--Select Vehicle--</asp:ListItem>
                                                <asp:ListItem Value="1">Tanker</asp:ListItem>
                                                <asp:ListItem Value="2">Truck</asp:ListItem>
                                                <asp:ListItem Value="3">Jeeps & Cars</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                </div>
                                <div class="row" id="divtanker" runat="server" visible="false">

                                    <fieldset>
                                        <legend>Tanker
                                        </legend>

                                        <div class="col-md-12">

                                            <div style="float: right">
                                                <%--  <asp:Label ID="Label2" runat="server" Text="You can add maximum 10 rows" Style="color: #ff0000"></asp:Label>--%>
                                                <asp:Button ID="btnAddtanker" runat="server" Text="+ Add New Row" CssClass="btn btn-block btn-primary" OnClick="btnAddtanker_Click" Width="150px" Style="padding: 5px; font-size: 14px" />
                                            </div>

                                            <asp:GridView runat="server" ID="gvtanker" CssClass="table table-bordered" OnDataBound="gvtanker_DataBound" ShowHeader="true" ShowFooter="false" AllowPaging="false" AutoGenerateColumns="false" OnRowDeleting="gvtanker_RowDeleting">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S No." ItemStyle-Width="10">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblslNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vehicle No.">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtVehicleNo" Text='<%# Eval("VehicleNo").ToString()%>' runat="server" class="form-control" ></asp:TextBox>

                                                         <asp:RequiredFieldValidator ID="rfvVehicleNo" ValidationGroup="a"
                                                    ErrorMessage="Enter Vehicle No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Vehicle No. !'></i>"
                                                    ControlToValidate="txtVehicleNo" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                             </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText=" Opening">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOpening" Text='<%# Eval("Opening").ToString()%>'  onkeypress="return validateDec(this, event)" runat="server" Height="10%"  class="form-control" />
                                                        <asp:RequiredFieldValidator ID="rfvOpening" ValidationGroup="a"
                                                    ErrorMessage="Enter Opening Amount." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Opening Amount !'></i>"
                                                    ControlToValidate="txtOpening" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText=" Closing">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtClosing" Text='<%# Eval("Closing").ToString()%>'  onkeypress="return validateDec(this, event)" runat="server" Height="10%"  class="form-control" />
                                                         <asp:RequiredFieldValidator ID="rfvClosing" ValidationGroup="a"
                                                    ErrorMessage="Enter Closing Amount." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Closing Amount !'></i>"
                                                    ControlToValidate="txtClosing" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Op. Bal.(Ltr)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtopenbal" Text='<%# Eval("openbal").ToString()%>'  onkeypress="return validateDec(this, event)" runat="server" class="form-control" />
                                                         <asp:RequiredFieldValidator ID="rfvopenbal" ValidationGroup="a"
                                                    ErrorMessage="Enter Op. Bal." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Op. Bal. !'></i>"
                                                    ControlToValidate="txtopenbal" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Own Pump(Ltr)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOwnPump" Text='<%# Eval("OwnPump").ToString()%>'  onkeypress="return validateDec(this, event)" runat="server" class="form-control" MaxLength="10"  />
                                                        <asp:RequiredFieldValidator ID="rfvOwnPump" ValidationGroup="a"
                                                    ErrorMessage="Enter Own Pump" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Own Pump !'></i>"
                                                    ControlToValidate="txtOwnPump" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="From CC(Ltr)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtfromcc" Text='<%# Eval("fromcc").ToString()%>'  onkeypress="return validateDec(this, event)" runat="server" class="form-control" />
                                                        <asp:RequiredFieldValidator ID="rfvfromcc" ValidationGroup="a"
                                                    ErrorMessage="Enter From CC" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter From CC !'></i>"
                                                    ControlToValidate="txtfromcc" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Market Purchase Ltr">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtMarketPurchaseLtr" Text='<%# Eval("MarketPurchaseLtr").ToString()%>'  onkeypress="return validateDec(this, event)" runat="server" class="form-control" ClientIDMode="Static" MaxLength="9"  />
                                                       <asp:RequiredFieldValidator ID="rfvMarketPurchaseLtr" ValidationGroup="a"
                                                    ErrorMessage="Enter Market Purchase Ltr" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Market Purchase Ltr!'></i>"
                                                    ControlToValidate="txtMarketPurchaseLtr" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Market Purchase Rs">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtMarketPurchaseRs" Text='<%# Eval("MarketPurchaseRs").ToString()%>'  onkeypress="return validateDec(this, event)" runat="server" class="form-control" ClientIDMode="Static" MaxLength="9"  />
                                                          <asp:RequiredFieldValidator ID="rfvMarketPurchaseRs" ValidationGroup="a"
                                                    ErrorMessage="Enter Market Purchase Rs" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Market Purchase Rs!'></i>"
                                                    ControlToValidate="txtMarketPurchaseRs" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cl. Bal.(Ltr)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtclosingbal" Text='<%# Eval("closingbal").ToString()%>'  onkeypress="return validateDec(this, event)" runat="server" class="form-control"  />
                                                        <asp:RequiredFieldValidator ID="rfvclosingbal" ValidationGroup="a"
                                                    ErrorMessage="Enter Cl. Bal." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Cl. Bal.!'></i>"
                                                    ControlToValidate="txtclosingbal" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Width="50px" ItemStyle-CssClass="text-center">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="Delete" runat="server" Width="50%" CausesValidation="False" ImageUrl="~/mis/image/Del.png" CommandName="Delete" Text="Delete Row" OnClientClick="return confirm('The item will be deleted. Are you sure want to continue?');"></asp:ImageButton>
                                                                    </ItemTemplate>
                                                     </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>


                                        </div>


                                    </fieldset>

                                </div>
                                <div class="row" id="divtruck" runat="server" visible="false">
                                    <fieldset>
                                        <legend>Truck
                                        </legend>
                                        <div class="col-md-12">

                                            <div style="float: right">
                                                <%--  <asp:Label ID="Label2" runat="server" Text="You can add maximum 10 rows" Style="color: #ff0000"></asp:Label>--%>
                                                <asp:Button ID="btnaddtruck" runat="server" Text="+ Add New Row" CssClass="btn btn-block btn-primary" OnClick="btnaddtruck_Click" Width="150px" Style="padding: 5px; font-size: 14px" />
                                            </div>

                                            <asp:GridView runat="server" ID="gvtruck" CssClass="table table-bordered" OnDataBound="gvtruck_DataBound" ShowHeader="true" ShowFooter="false" AllowPaging="false" AutoGenerateColumns="false" OnRowDeleting="gvtruck_RowDeleting">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S No." ItemStyle-Width="10">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblslNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vehicle No.">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtVehicleNo" Text='<%# Eval("VehicleNo").ToString()%>' runat="server" class="form-control" ></asp:TextBox>

                                                         <asp:RequiredFieldValidator ID="rfvVehicleNo" ValidationGroup="a"
                                                    ErrorMessage="Enter Vehicle No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Vehicle No. !'></i>"
                                                    ControlToValidate="txtVehicleNo" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                             </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText=" Opening">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOpening" Text='<%# Eval("Opening").ToString()%>'  onkeypress="return validateDec(this, event)" runat="server" Height="10%"  class="form-control" />
                                                        <asp:RequiredFieldValidator ID="rfvOpening" ValidationGroup="a"
                                                    ErrorMessage="Enter Opening Amount." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Opening Amount !'></i>"
                                                    ControlToValidate="txtOpening" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText=" Closing">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtClosing" Text='<%# Eval("Closing").ToString()%>'  onkeypress="return validateDec(this, event)" runat="server" Height="10%"  class="form-control" />
                                                         <asp:RequiredFieldValidator ID="rfvClosing" ValidationGroup="a"
                                                    ErrorMessage="Enter Closing Amount." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Closing Amount !'></i>"
                                                    ControlToValidate="txtClosing" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Op. Bal.(Ltr)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtopenbal" Text='<%# Eval("openbal").ToString()%>'  onkeypress="return validateDec(this, event)" runat="server" class="form-control" />
                                                         <asp:RequiredFieldValidator ID="rfvopenbal" ValidationGroup="a"
                                                    ErrorMessage="Enter Op. Bal." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Op. Bal. !'></i>"
                                                    ControlToValidate="txtopenbal" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Own Pump(Ltr)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOwnPump" Text='<%# Eval("OwnPump").ToString()%>'  onkeypress="return validateDec(this, event)" runat="server" class="form-control" MaxLength="10"  />
                                                        <asp:RequiredFieldValidator ID="rfvOwnPump" ValidationGroup="a"
                                                    ErrorMessage="Enter Own Pump" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Own Pump !'></i>"
                                                    ControlToValidate="txtOwnPump" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="From CC(Ltr)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtfromcc" Text='<%# Eval("fromcc").ToString()%>'  onkeypress="return validateDec(this, event)" runat="server" class="form-control" />
                                                        <asp:RequiredFieldValidator ID="rfvfromcc" ValidationGroup="a"
                                                    ErrorMessage="Enter From CC" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter From CC !'></i>"
                                                    ControlToValidate="txtfromcc" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Market Purchase Ltr">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtMarketPurchaseLtr" Text='<%# Eval("MarketPurchaseLtr").ToString()%>'  onkeypress="return validateDec(this, event)" runat="server" class="form-control" ClientIDMode="Static" MaxLength="9"  />
                                                       <asp:RequiredFieldValidator ID="rfvMarketPurchaseLtr" ValidationGroup="a"
                                                    ErrorMessage="Enter Market Purchase Ltr" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Market Purchase Ltr!'></i>"
                                                    ControlToValidate="txtMarketPurchaseLtr" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Market Purchase Rs">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtMarketPurchaseRs" Text='<%# Eval("MarketPurchaseRs").ToString()%>'  onkeypress="return validateDec(this, event)" runat="server" class="form-control" ClientIDMode="Static" MaxLength="9"  />
                                                          <asp:RequiredFieldValidator ID="rfvMarketPurchaseRs" ValidationGroup="a"
                                                    ErrorMessage="Enter Market Purchase Rs" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Market Purchase Rs!'></i>"
                                                    ControlToValidate="txtMarketPurchaseRs" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cl. Bal.(Ltr)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtclosingbal" Text='<%# Eval("closingbal").ToString()%>'  onkeypress="return validateDec(this, event)" runat="server" class="form-control"  />
                                                        <asp:RequiredFieldValidator ID="rfvclosingbal" ValidationGroup="a"
                                                    ErrorMessage="Enter Cl. Bal." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Cl. Bal.!'></i>"
                                                    ControlToValidate="txtclosingbal" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Width="50px" ItemStyle-CssClass="text-center">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="Delete" runat="server" Width="50%" CausesValidation="False" ImageUrl="~/mis/image/Del.png" CommandName="Delete" Text="Delete Row" OnClientClick="return confirm('The item will be deleted. Are you sure want to continue?');"></asp:ImageButton>
                                                                    </ItemTemplate>
                                                     </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>


                                        </div>

                                    </fieldset>


                                </div>
                                <div class="row" id="divjeepcar" runat="server" visible="false">
                                    <fieldset>
                                        <legend>Jeeps & Cars
                                        </legend>
                                        <div class="col-md-12">

                                            <div style="float: right">
                                                <%--  <asp:Label ID="Label2" runat="server" Text="You can add maximum 10 rows" Style="color: #ff0000"></asp:Label>--%>
                                                <asp:Button ID="btnaddjeepcar" runat="server" Text="+ Add New Row" CssClass="btn btn-block btn-primary" OnClick="btnaddjeepcar_Click" Width="150px" Style="padding: 5px; font-size: 14px" />
                                            </div>

                                            <asp:GridView runat="server" ID="gvjeepcar" CssClass="table table-bordered" OnDataBound="gvjeepcar_DataBound" ShowHeader="true" ShowFooter="false" AllowPaging="false" AutoGenerateColumns="false" OnRowDeleting="gvjeepcar_RowDeleting">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S No." ItemStyle-Width="10">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblslNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vehicle No.">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtVehicleNo" Text='<%# Eval("VehicleNo").ToString()%>' runat="server" class="form-control" ></asp:TextBox>

                                                         <asp:RequiredFieldValidator ID="rfvVehicleNo" ValidationGroup="a"
                                                    ErrorMessage="Enter Vehicle No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Vehicle No. !'></i>"
                                                    ControlToValidate="txtVehicleNo" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                             </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText=" Opening">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOpening" Text='<%# Eval("Opening").ToString()%>'  onkeypress="return validateDec(this, event)" runat="server" Height="10%"  class="form-control" />
                                                        <asp:RequiredFieldValidator ID="rfvOpening" ValidationGroup="a"
                                                    ErrorMessage="Enter Opening Amount." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Opening Amount !'></i>"
                                                    ControlToValidate="txtOpening" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText=" Closing">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtClosing" Text='<%# Eval("Closing").ToString()%>'  onkeypress="return validateDec(this, event)" runat="server" Height="10%"  class="form-control" />
                                                         <asp:RequiredFieldValidator ID="rfvClosing" ValidationGroup="a"
                                                    ErrorMessage="Enter Closing Amount." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Closing Amount !'></i>"
                                                    ControlToValidate="txtClosing" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Op. Bal.(Ltr)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtopenbal" Text='<%# Eval("openbal").ToString()%>'  onkeypress="return validateDec(this, event)" runat="server" class="form-control" />
                                                         <asp:RequiredFieldValidator ID="rfvopenbal" ValidationGroup="a"
                                                    ErrorMessage="Enter Op. Bal." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Op. Bal. !'></i>"
                                                    ControlToValidate="txtopenbal" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Own Pump(Ltr)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOwnPump" Text='<%# Eval("OwnPump").ToString()%>'  onkeypress="return validateDec(this, event)" runat="server" class="form-control" MaxLength="10"  />
                                                        <asp:RequiredFieldValidator ID="rfvOwnPump" ValidationGroup="a"
                                                    ErrorMessage="Enter Own Pump" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Own Pump !'></i>"
                                                    ControlToValidate="txtOwnPump" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="From CC(Ltr)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtfromcc" Text='<%# Eval("fromcc").ToString()%>'  onkeypress="return validateDec(this, event)" runat="server" class="form-control" />
                                                        <asp:RequiredFieldValidator ID="rfvfromcc" ValidationGroup="a"
                                                    ErrorMessage="Enter From CC" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter From CC !'></i>"
                                                    ControlToValidate="txtfromcc" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Market Purchase Ltr">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtMarketPurchaseLtr" Text='<%# Eval("MarketPurchaseLtr").ToString()%>'  onkeypress="return validateDec(this, event)" runat="server" class="form-control" ClientIDMode="Static"   />
                                                       <asp:RequiredFieldValidator ID="rfvMarketPurchaseLtr" ValidationGroup="a"
                                                    ErrorMessage="Enter Market Purchase Ltr" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Market Purchase Ltr!'></i>"
                                                    ControlToValidate="txtMarketPurchaseLtr" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Market Purchase Rs">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtMarketPurchaseRs" Text='<%# Eval("MarketPurchaseRs").ToString()%>'  onkeypress="return validateDec(this, event)" runat="server" class="form-control" ClientIDMode="Static"  />
                                                          <asp:RequiredFieldValidator ID="rfvMarketPurchaseRs" ValidationGroup="a"
                                                    ErrorMessage="Enter Market Purchase Rs" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Market Purchase Rs!'></i>"
                                                    ControlToValidate="txtMarketPurchaseRs" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cl. Bal.(Ltr)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtclosingbal" Text='<%# Eval("closingbal").ToString()%>'  onkeypress="return validateDec(this, event)" runat="server" class="form-control"  />
                                                        <asp:RequiredFieldValidator ID="rfvclosingbal" ValidationGroup="a"
                                                    ErrorMessage="Enter Cl. Bal." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Cl. Bal.!'></i>"
                                                    ControlToValidate="txtclosingbal" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Width="50px" ItemStyle-CssClass="text-center">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="Delete" runat="server" Width="50%" CausesValidation="False" ImageUrl="~/mis/image/Del.png" CommandName="Delete" Text="Delete Row" OnClientClick="return confirm('The item will be deleted. Are you sure want to continue?');"></asp:ImageButton>
                                                                    </ItemTemplate>
                                                     </asp:TemplateField>

                                                </Columns>

                                            </asp:GridView>


                                        </div>
                                </div>

                                <div class="row" id="divbutton" runat="server" visible="false">
                                    <hr />
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" OnClick="btnSubmit_Click"/>
                                       <asp:Button runat="server" CssClass="btn btn-block btn-primary" Visible="false" ValidationGroup="a" ID="btnupdate" Text="Update" OnClientClick="return ValidatePage();" AccessKey="S"  OnClick="btnupdate_Click"/>
                                             </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <a class="btn btn-block btn-default" href="MPR_Of_TS_Diesel_Consumption.aspx">Clear</a>
                                        </div>
                                    </div>
                                </div>


                            </fieldset>
                        </ContentTemplate>
                  <%--  </asp:UpdatePanel>--%>

                </div>
            </div>
            <!-- /.box-body -->
            <div class="box box-body" >
                <div class="box-header">
                    <h3 class="box-title"> Diesel Consumption Detail</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="table-responsive">
                         <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" Text="Month" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                            ErrorMessage="Select Month" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Month !'></i>"
                                            ControlToValidate="DDlMonth" Display="Dynamic" runat="server">
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
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label2" Text="Year" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
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
                         <asp:Button runat="server" BackColor="#2e9eff" CssClass="btn btn-success" Text="Search" ID="btnSearch" OnClick="btnSearch_Click" Style="margin-top: 20px; width: 80px;" />
                       </div>
                            </div>
                        
                                <div class="col-md-12">
                        <asp:GridView runat="server" ID="gvDCdetail" Visible="false"  CssClass="table table-bordered"   ShowHeader="true" ShowFooter="false" AllowPaging="false" AutoGenerateColumns="false"  OnRowCommand="gvDCdetail_RowCommand" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S No." ItemStyle-Width="50">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblslNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                           <%--  <asp:Label Visible="false" ID="lblMPR_DP_Id" Text='<%# Eval("MPR_DP_Id").ToString()%>' runat="server" />--%>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ID" Visible="false">
                                                        <ItemTemplate>
                                                               <asp:Label Visible="false" ID="lblDC_Id" Text='<%# Eval("DC_Id").ToString()%>' runat="server" />

                                                     
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
                                                  <asp:TemplateField HeaderText="Vehicle ">
                                                        <ItemTemplate>
                                                              <asp:Label  ID="lblVehicle_Type_ID" Visible="false" Text='<%# Eval("Vehicle_Type_ID").ToString()%>' runat="server" class="form-control"></asp:Label>
                                                             <asp:Label  ID="lblVehicle_Type_Name" Text='<%# Eval("Vehicle_Type_Name").ToString()%>' runat="server" class="form-control"></asp:Label>
                                                       
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="Creation Date">
                                                        <ItemTemplate>
                                                              <asp:Label  ID="lblCreatedAt" Text='<%# Eval("CreatedAt").ToString()%>' runat="server" class="form-control"></asp:Label>
                                                        
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                        <%--<asp:TemplateField HeaderText="Skilled *">
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
                                                                        <asp:ImageButton ID="EDIT" runat="server" Width="100%" CausesValidation="False"  ImageUrl="~/mis/image/edit.png" CommandName="select" CommandArgument='<%# Bind("DC_Id") %>'></asp:ImageButton>
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
