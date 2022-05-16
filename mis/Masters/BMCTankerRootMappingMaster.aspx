<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="BMCTankerRootMappingMaster.aspx.cs" Inherits="mis_Masters_BMCTankerRootMappingMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/bootstrap-timepicker.css" rel="stylesheet" />
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnYes_Click" Style="margin-top: 20px; width: 50px;" />
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
        <section class="content">
            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">BMC Tanker Root Mapping Master</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>DS Name</label>
                                    <asp:DropDownList ID="ddldsname" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Root Name</label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="b"
                                            InitialValue="0" ErrorMessage="Select Tanker RootName" Text="<i class='fa fa-exclamation-circle' title='Select Root Name !'></i>"
                                            ControlToValidate="ddlBMCTankerRootName" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlBMCTankerRootName" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlBMCTankerRootName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>

                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12">

                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>CC/MDP<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="b"
                                            InitialValue="0" ErrorMessage="Select CC / MDP Name" Text="<i class='fa fa-exclamation-circle' title='Select CC / MDP Name !'></i>"
                                            ControlToValidate="ddlccbmcdetail" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlccbmcdetail" AutoPostBack="true" OnSelectedIndexChanged="ddlccbmcdetail_SelectedIndexChanged" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
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
                                </div>    
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>BMC<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="b"
                                            InitialValue="0" ErrorMessage="Select BMC Name" Text="<i class='fa fa-exclamation-circle' title='Select BMC Name !'></i>"
                                            ControlToValidate="ddlmcudetails" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlmcudetails" OnInit="ddlmcudetails_Init" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>


                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Distance In Km<span style="color: red;"> *</span></label>
                                    <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="b"
                                            ErrorMessage="Enter Distance In Km" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Sequence No !'></i>"
                                            ControlToValidate="txtDistanceInKm" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" Display="Dynamic" ValidationExpression="\d{1,10}" ValidationGroup="b" runat="server" ControlToValidate="txtDistanceInKm" ErrorMessage="Enter Valid Distance In Km." Text="<i class='fa fa-exclamation-circle' title='Enter Valid Distance In Km. !'></i>"></asp:RegularExpressionValidator>
                                        <asp:RangeValidator ID="rValidator" Display="Dynamic" MinimumValue="1" MaximumValue="100" ValidationGroup="b" runat="server" ControlToValidate="txtDistanceInKm" Type="Integer" SetFocusOnError="true" ErrorMessage="Invalid Value (Allow value 1-100 only)" Text="<i class='fa fa-exclamation-circle' title='Invalid Value (Allow value 1-100 only)!'></i>"></asp:RangeValidator>
                                    </span>--%>
                                    <asp:TextBox autocomplete="off" runat="server" CssClass="form-control" ID="txtDistanceInKm" MaxLength="2" placeholder="Enter Distance In Km"></asp:TextBox>
                                </div>
                            </div>


                            <div class="col-md-2">
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvArrivalTime" runat="server" Display="Dynamic" ControlToValidate="txtArrivalTime" Text="<i class='fa fa-exclamation-circle' title='Enter Arrival Time!'></i>" ErrorMessage="Enter Arrival Time" SetFocusOnError="true" ForeColor="Red" ValidationGroup="b"></asp:RequiredFieldValidator>
                                </span>
                                <div class="form-group">
                                    <label>Tanker Arrival Time<span style="color: red;">*</span></label>
                                    <div class="input-group bootstrap-timepicker timepicker">
                                        <asp:TextBox ID="txtArrivalTime" class="form-control input-small" runat="server" ClientIDMode="Static"></asp:TextBox>
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                    </div>
                                </div>
                            </div>


                            <div class="col-md-2">
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtDispatchTime" Text="<i class='fa fa-exclamation-circle' title='Enter Tanker Dispatch Time!'></i>" ErrorMessage="Enter Tanker Dispatch Time" SetFocusOnError="true" ForeColor="Red" ValidationGroup="b"></asp:RequiredFieldValidator>
                                </span>
                                <div class="form-group">
                                    <label>Tanker Dispatch Time<span style="color: red;">*</span></label>
                                    <div class="input-group bootstrap-timepicker timepicker">
                                        <asp:TextBox ID="txtDispatchTime" class="form-control input-small" runat="server" ClientIDMode="Static"></asp:TextBox>
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                    </div>
                                </div>
                            </div>


                            <div class="col-md-1">
                                <div class="form-group">
                                    <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnAddcc_Click" Style="margin-top: 20px;" ValidationGroup="b" ID="btnAddcc" Text="Add" />
                                </div>
                            </div>

                            <hr />

                            <asp:GridView ID="gv_CCDetails" ShowHeader="true" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server">
                                <Columns>

                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="BMC Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblI_OfficeID" Visible="false" runat="server" Text='<%# Eval("I_OfficeID") %>'></asp:Label>
                                            <asp:Label ID="lblccname" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Distance In KM">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDistanceInKm" runat="server" Text='<%# Eval("DistanceInKm") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Arrival Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lblArrivalDateTime" runat="server" Text='<%# Eval("ArrivalDateTime") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="DispatchDateTime">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDispatchDateTime" runat="server" Text='<%# Eval("DispatchDateTime") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDeleteCC" OnClick="lnkDeleteCC_Click" runat="server" ToolTip="DeleteCC" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete this record?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>

                    <hr />

                    <div class="row">
                        <div class="col-lg-12">
                            <div class="col-md-1">
                                <div class="form-group">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary btn-block" OnClientClick="return ValidatePage(); " />
                                </div>
                            </div>
                        </div>
                    </div>

                     <hr />
                     
                      <b>MAPPED BMC DETAIL</b>
                    <hr />
                    <asp:GridView ID="GridView1" ShowHeader="true" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server">
                                <Columns>

                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="BMC Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblI_OfficeID" Visible="false" runat="server" Text='<%# Eval("Office_ID") %>'></asp:Label>
                                            <asp:Label ID="lblBMCName" runat="server" Text='<%# Eval("BMCName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Distance In KM">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDistanceInKm" runat="server" Text='<%# Eval("DistanceInKm") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Arrival Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lblArrivalDateTime" runat="server" Text='<%# Eval("ArrivalDateTime") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="DispatchDateTime">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDispatchDateTime" runat="server" Text='<%# Eval("DispatchDateTime") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="AttachedToCC Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCCName" runat="server" Text='<%# Eval("CCName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblremovebmc" CommandArgument='<%# Eval("BMCTankerRootMapping_Id") %>' OnClick="lblremovebmc_Click" runat="server" ToolTip="Remove Mapping" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete this record?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>

                </div>

            </div>
        </section>
    </div>


     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script src="../js/bootstrap-timepicker.js"></script>
    <script>
        $('#txtArrivalTime').timepicker();
        $('#txtDispatchTime').timepicker();
        $('.select2').select2()
        $('.DateAdd').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        })
    </script>


    <script type="text/javascript">
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

</asp:Content>

