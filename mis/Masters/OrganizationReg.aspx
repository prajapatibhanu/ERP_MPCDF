<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="OrganizationReg.aspx.cs" Inherits="mis_Masters_OrganizationReg" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
      <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <style>
        .columngreen {
            background-color: #aee6a3 !important;
        }
        .columnred {
            background-color: #f05959 !important;
        }
         .columnmilk {
            background-color: #bfc7c5 !important;
        }
        .columnproduct {
            background-color:#f5f376 !important;
        }
    </style>
  
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <%--Confirmation Modal Start --%>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
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
                    <h3 class="box-title">Institution Registration</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" CssClass="Autoclr" runat="server"></asp:Label>
                        </div>
                    </div>
                    <fieldset>
                        <legend>Institution Registration Details
                        </legend>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Route No<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlRoute" InitialValue="0" ErrorMessage="Select Route." Text="<i class='fa fa-exclamation-circle' title='Select Route Sangh !'></i>"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddlRoute" OnInit="ddlRoute_Init" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                </div>
                            </div>
                             <div class="col-md-3">
                                <div class="form-group">
                                    <label>Institution Type<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlOrganizationType" InitialValue="0" ErrorMessage="Select Institution Type." Text="<i class='fa fa-exclamation-circle' title='Select Institution Type !'></i>"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddlOrganizationType"  runat="server" CssClass="form-control select2">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem> 
                                           <asp:ListItem Text="Government" Value="1"></asp:ListItem> 
                                           <asp:ListItem Text="Private" Value="2"></asp:ListItem> 
                                         <asp:ListItem Text="Others" Value="3"></asp:ListItem> 
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Institution Name<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvofficename" ValidationGroup="a"
                                            ErrorMessage="Enter Institution Name" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Institution Name !'></i>"
                                            ControlToValidate="txtOrganizationName" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                      <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtOrganizationName"
                                            ErrorMessage="Only Alphabet allow in Organization Name" Text="<i class='fa fa-exclamation-circle' title='Only Alphabet allow in Organization Name !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[a-z-A-Z\s]+$">
                                        </asp:RegularExpressionValidator>--%>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOrganizationName" MaxLength="60" placeholder="Enter Organization Name" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            
                             <div class="col-md-3">
                               <div class="form-group">
                                <label>Institution Code<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtICode" ErrorMessage="Enter Institution Code" Text="<i class='fa fa-exclamation-circle' title='Enter Institution Code !'></i>"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtICode" MaxLength="10" placeholder="Enter Institution Code" ClientIDMode="Static"></asp:TextBox>
                           </div>
                                    </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Contact Person<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                            ErrorMessage="Enter Contact Person" Text="<i class='fa fa-exclamation-circle' title='Enter Contact Person !'></i>"
                                            ControlToValidate="txtContactPerson" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtContactPerson" ErrorMessage="Alphabet and space allow" Text="<i class='fa fa-exclamation-circle' title='Alphabet and space allow !'></i>" SetFocusOnError="true" ValidationExpression="^[a-zA-Z\s]+$"></asp:RegularExpressionValidator>--%>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtContactPerson" MaxLength="60" placeholder="Enter Contact Person"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Contact Person Mobile No.<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                            ErrorMessage="Enter Contact Person Mobile No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Person Mobile No. !'></i>"
                                            ControlToValidate="txtContactPersonMobileNo" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" Display="Dynamic" ValidationGroup="a"
                                            ErrorMessage="Enter Valid Contact Person Mobile No. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Contact Person Mobile No. !'></i>" ControlToValidate="txtContactPersonMobileNo"
                                            ValidationExpression="^[6-9]{1}[0-9]{9}$">
                                        </asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control  MobileNo" ID="txtContactPersonMobileNo" MaxLength="10" onkeypress="return validateNum(event);" placeholder="Enter Contact Person Mobile No."></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Delivery Type<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                            InitialValue="0" ForeColor="Red" ErrorMessage="Select Delivery Type" Text="<i class='fa fa-exclamation-circle' title='Select Delivery Type !'></i>"
                                            ControlToValidate="ddlDeliveryType" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlDeliveryType" runat="server" CssClass="form-control select2" ClientIDMode="Static">
                                          <asp:ListItem Text="Select" Value="0" ></asp:ListItem>
                                              <asp:ListItem Text="Distributor/Super Stockist" Value="8" ></asp:ListItem>
                                              <asp:ListItem Text="Sub-Distributor" Value="9" ></asp:ListItem>
                                         <asp:ListItem Text="Self" Value="1" ></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Divison<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfv2" ValidationGroup="a"
                                            InitialValue="0" ForeColor="Red" ErrorMessage="Select Division" Text="<i class='fa fa-exclamation-circle' title='Select Division !'></i>"
                                            ControlToValidate="ddlDivision" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlDivision" OnInit="ddlDivision_Init" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                            
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>District<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfv3" ValidationGroup="a"
                                            InitialValue="0" ForeColor="Red" ErrorMessage="Select District" Text="<i class='fa fa-exclamation-circle' title='Select !'></i>"
                                            ControlToValidate="ddlDistrict" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" 
                                        CssClass="form-control select2" ClientIDMode="Static">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                            <div class="form-group">
                                <label>Block Name<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfv4" ValidationGroup="a"
                                            InitialValue="0" ForeColor="Red" 
                                            ErrorMessage="Select Block" Text="<i class='fa fa-exclamation-circle' title='Select Block !'></i>"
                                            ControlToValidate="ddlBlock_Name" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlBlock_Name" runat="server"  CssClass="form-control select2">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>City/Village<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a"
                                            ErrorMessage="Enter City/village" Text="<i class='fa fa-exclamation-circle' title='Enter City/village !'></i>"
                                            ControlToValidate="txtTownOrvillage" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                         </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtTownOrvillage" MaxLength="80" placeholder="Enter City Or Village"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Address<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvofficeaddress" ValidationGroup="a"
                                            ErrorMessage="Enter Address" Text="<i class='fa fa-exclamation-circle' title='Enter Address !'></i>"
                                            ControlToValidate="txtAddress" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <%--<asp:RegularExpressionValidator ID="revofficeaddress" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" 
                                            ControlToValidate="txtAddress"   ErrorMessage="Alphanumeric,space and some special symbols like '.,/-:' allowed"
                                             Text="<i class='fa fa-exclamation-circle' title='Alphanumeric ,space and some special .,/-: allowed'></i>"
                                             SetFocusOnError="true" ValidationExpression="^[0-9a-zA-Z\s.,-:_/-:]+$"></asp:RegularExpressionValidator> --%> 
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtAddress" MaxLength="140" placeholder="Enter Address"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Pincode<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvofficepincode" ValidationGroup="a"
                                            ErrorMessage="Enter Pincode" Text="<i class='fa fa-exclamation-circle' title='Enter Office Pincode !'></i>"
                                            ControlToValidate="txtPincode" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revofficepincode" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtPincode" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtPincode" MaxLength="6" placeholder="Enter Pincode" onkeypress="return validateNum(event);"></asp:TextBox>
                                </div>
                            </div>
                          
                        </div>
                    </fieldset>
                    
                    <div class="row">
                        <hr />
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-block btn-default" />
                            </div>
                        </div>
                    </div>

                </div>

            </div>
            <!-- /.box-body -->

            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Institution Registration Details</h3>
                    
      
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1"  OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" 
                            EmptyDataText="No Record Found." PageSize="50" runat="server" 
                            class="datatable table table-hover table-bordered pagination-ys"
                            ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" DataKeyNames="OrganizationId">
                            <Columns>
                                
                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Office_ID").ToString()%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Institution Name">
                                    <ItemTemplate>
                                         <asp:Label ID="lblRouteId" Visible="false" runat="server" Text='<%# Eval("RouteId") %>' />
                                        <asp:Label ID="lblOrganization_Type" Visible="false" runat="server" Text='<%# Eval("Organization_Type") %>' />
                                        
                                        <asp:Label ID="lblOrganization_Name" runat="server" Text='<%# Eval("Organization_Name") %>' />
                                        <asp:Label ID="lblDivision_ID" Visible="false" runat="server" Text='<%# Eval("Division_ID") %>' />
                                        <asp:Label ID="lblDistrict_ID" Visible="false" runat="server" Text='<%# Eval("District_ID") %>' />
                                        <asp:Label ID="lblBlock_ID" Visible="false" runat="server" Text='<%# Eval("Block_ID") %>' />
                                        <asp:Label ID="lblTownOrVillage" runat="server" Visible="false" Text='<%# Eval("TownOrVillage") %>' />
                                      
                                        <asp:Label ID="lblBAddress" Visible="false" runat="server" Text='<%# Eval("OAddress") %>' />
                                        <asp:Label ID="lblBPincode" Visible="false" runat="server" Text='<%# Eval("OPincode") %>' />
                                        <asp:Label ID="lblRetailerTypeID" Visible="false" runat="server" Text='<%# Eval("RetailerTypeID") %>' />
                                       
                                         <asp:Label ID="lblIsActive" Text='<%# Eval("IsActive")%>' runat="server" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Route Name/No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRName" runat="server" Text='<%# Eval("RName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Institution Code">
                                    <ItemTemplate>
                                       <asp:Label ID="lblICode" runat="server" Text='<%# Eval("ICode") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Institution Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrganization_TypeName" runat="server" Text='<%# Eval("Organization_Type").ToString()=="1" ? "Government" :"Private" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact Person">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCPersonName" runat="server" Text='<%# Eval("CPersonName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact Person Mobile No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCPersonMobileNo" runat="server" Text='<%# Eval("CPersonMobileNo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Delivery Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeliveryType" Visible="false"  runat="server" Text='<%# Eval("Delivery_Type") %>' />
                                    <asp:Label ID="lblDelliveryTypeName"  runat="server" Text='<%# Eval("Delivery_Type") %>' />
                                 
                                          </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("OrganizationId") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("OrganizationId") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

            </div>
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
<script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>
    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
        $('.datatable').DataTable({
            paging: true,
            lengthMenu: [10, 25, 50],
            columnDefs: [{
                targets: 'no-sort',
                orderable: false,
            }],
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6]
                    },
                    footer: false,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    filename: 'Organization Registration Details',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',

                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6]
                    },
                    footer: false
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

