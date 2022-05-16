<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="VehicleMaster.aspx.cs" Inherits="mis_Common_VehicleMaster" %>

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
            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Vehicle Master</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Transporter (Company) Name<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                        InitialValue="0" ErrorMessage="Select Transporter Name" Text="<i class='fa fa-exclamation-circle' title='Select Transporter Name !'></i>"
                                        ControlToValidate="ddlTransCompName" ForeColor="Red" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator></span>
                                <asp:DropDownList ID="ddlTransCompName" OnInit="ddlTransCompName_Init" ClientIDMode="Static" AutoPostBack="true" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Vehicle Type<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                        InitialValue="0" ErrorMessage="Select Transporter Name" Text="<i class='fa fa-exclamation-circle' title='Select Transporter Name !'></i>"
                                        ControlToValidate="ddlVehicleType" ForeColor="Red" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator></span>
                                <asp:DropDownList ID="ddlVehicleType" OnInit="ddlVehicleType_Init" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Vehicle RC Number<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                        ErrorMessage="Enter Vehicle Number" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Vehicle Number !'></i>"
                                        ControlToValidate="txtVehicleNo" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ValidationGroup="a"
                                        ErrorMessage="Invalid Vehicle Number. !" Text="<i class='fa fa-exclamation-circle' title='Invalid Vehicle Number. !'></i>" ControlToValidate="txtVehicleNo"
                                        ValidationExpression="^[0-9]+$">
                                    </asp:RegularExpressionValidator>
                                </span>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtVehicleNo" MaxLength="10" placeholder="Enter Vehicle RC Number"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Vehicle RC Validity<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="red" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtVehicleRCValidity_Date" ErrorMessage="Enter Vehicle RC Validity Date." Text="<i class='fa fa-exclamation-circle' title='Enter Vehicle RC Validity Date !'></i>"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revdate" ValidationGroup="a" ForeColor="Red" runat="server" Display="Dynamic" ControlToValidate="txtVehicleRCValidity_Date" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                </span>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>

                                    <asp:TextBox ID="txtVehicleRCValidity_Date" onkeypress="return false;" runat="server" placeholder="Select Vehicle RC Validity." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-start-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Insurance No.<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvpincode" ValidationGroup="a"
                                        ErrorMessage="Enter Vehicle Capacity" Text="<i class='fa fa-exclamation-circle' title='Enter Insurance No !'></i>"
                                        ControlToValidate="txtInsuranceNo" ForeColor="Red" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtInsuranceNo" MaxLength="6" placeholder="Enter Insurance No."></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Insurance Validity<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="red" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtInsuranceValidity_date" ErrorMessage="Enter Insurance Validity." Text="<i class='fa fa-exclamation-circle' title='Enter Insurance Validity !'></i>"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" ForeColor="Red" runat="server" Display="Dynamic" ControlToValidate="txtInsuranceValidity_date" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                </span>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>

                                    <asp:TextBox ID="txtInsuranceValidity_date" onkeypress="return false;" runat="server" placeholder="Select Insurance Validity..." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-start-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Driver Name<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a"
                                        ErrorMessage="Enter Driver Name" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Driver Name !'></i>"
                                        ControlToValidate="txtDriverName" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDriverName"
                                        ErrorMessage="Only alphabet allow" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Only alphabet allow. !'></i>"
                                        SetFocusOnError="true" ValidationExpression="^[a-zA-Z\s]+$">
                                    </asp:RegularExpressionValidator>
                                </span>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDriverName" placeholder="Enter Driver Name."></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Driver Mobile Number<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvContactPerson" ValidationGroup="a"
                                        ErrorMessage="Enter Driver Mobile No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Driver Mobile No. !'></i>"
                                        ControlToValidate="txtDriverMobileNo" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="Dynamic" ValidationGroup="a"
                                        ErrorMessage="Enter Driver Mobile No. !" Text="<i class='fa fa-exclamation-circle' title='Enter Contact Person !'></i>" ControlToValidate="txtDriverMobileNo"
                                        ValidationExpression="^[0-9]+$">
                                    </asp:RegularExpressionValidator>
                                </span>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDriverMobileNo" MaxLength="11" onkeypress="return validateNum(event);" placeholder="Driver Mobile Number."></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Driver License No.(Ex:DL-0420110487XXXX)<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="a"
                                        ErrorMessage="Enter Driver License No" Text="<i class='fa fa-exclamation-circle' title='Enter Driver License No!'></i>"
                                        ControlToValidate="txtDriverLicenseNo" ForeColor="Red" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDriverLicenseNo" MaxLength="16" placeholder="Driver License No."></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>License Validity<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ForeColor="red" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtLicenseValidity" ErrorMessage="Enter License Validity." Text="<i class='fa fa-exclamation-circle' title='Enter License Validity !'></i>"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" ForeColor="Red" runat="server" Display="Dynamic" ControlToValidate="txtLicenseValidity" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                </span>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtLicenseValidity" onkeypress="return false;" runat="server" placeholder="Select License Validity..." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-start-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Vehicle Load Capcity.<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="a"
                                        ErrorMessage="Enter Vehicle Load Capcity" Text="<i class='fa fa-exclamation-circle' title='Enter Vehicle Load Capcity !'></i>"
                                        ControlToValidate="txtVehicleLoadCapcity" ForeColor="Red" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>
                                </span>
                                <div class="input-group">
                                    <div class="input-group-btn">
                                        <asp:DropDownList ID="ddlUnit" runat="server" CssClass="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                            <asp:ListItem Text="KG" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Ton" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="Gm" Value="18"></asp:ListItem>
                                            <asp:ListItem Text="Ltr" Value="6"></asp:ListItem>
                                        </asp:DropDownList>
                                        <span class="fa fa-caret-down"></span>
                                    </div>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtVehicleLoadCapcity" MaxLength="6" placeholder="Vehicle Load Capcity."></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <hr />
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-block btn-default" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- /.box-body -->
            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Vehicle Master Details</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" OnRowCommand="GridView1_RowCommand" CssClass="datatable table table-striped table-bordered table-hover"
                                    EmptyDataText="No Record Found." DataKeyNames="VehicleTypeMaster_ID">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Transporter (Company) Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVehicleTypeMaster_ID" runat="server" Visible="false" Text='<%# Eval("VehicleTypeMaster_ID") %>'></asp:Label>
                                                <asp:Label ID="lblTransporterId" runat="server" Visible="false" Text='<%# Eval("TransporterId") %>'></asp:Label>
                                                <asp:Label ID="lblUnitid" runat="server" Visible="false" Text='<%# Eval("Unit_id") %>'></asp:Label>
                                                <asp:Label ID="lblVehicleLoadCapcity" runat="server" Visible="false" Text='<%# Eval("Vehicle_Load_Capcity") %>'></asp:Label>
                                                <asp:Label ID="lblTransporterCompany" runat="server" Text='<%# Eval("Transporter_Company") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vehicle Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVehicleType_ID" Visible="false" runat="server" Text='<%# Eval("VehicleType_ID") %>'></asp:Label>
                                                <asp:Label ID="lblVehicleTypeName" runat="server" Text='<%# Eval("VehicleType_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Vehicle RC Number">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVehicleRCNumber" runat="server" Text='<%# Eval("Vehicle_RC_Number") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Vehicle RC Validity date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVehicleRCValiditydate" runat="server" Text='<%# Eval("Vehicle_RC_Validity_date") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Insurance No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInsuranceNo" runat="server" Text='<%# Eval("Insurance_No") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Insurance Validity date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInsuranceValiditydate" runat="server" Text='<%# Eval("Insurance_Validity_date") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Driver Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDriverName" runat="server" Text='<%# Eval("Driver_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Driver Mobile Number">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDriverMobileNumber" runat="server" Text='<%# Eval("Driver_Mobile_Number") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Driver License No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDriverLicenseNo" runat="server" Text='<%# Eval("Driver_License_No") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                              <asp:TemplateField HeaderText="License Validity date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLicenseValiditydate" runat="server" Text='<%# Eval("License_Validity_date") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("VehicleTypeMaster_ID") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                &nbsp;&nbsp;&nbsp;
                                            &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("VehicleTypeMaster_ID") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
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
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
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

