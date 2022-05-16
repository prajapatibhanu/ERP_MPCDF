<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CrateMgmtAtDS.aspx.cs" Inherits="mis_DemandSupply_CrateMgmtAtDS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">

   <link href="../Finance/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <style>
        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('../image/loader/ProgressImage.gif') 50% 50% no-repeat rgba(0, 0, 0, 0.3);
    </style>
    <script type="text/javascript">
        function CalculateShortExcess() {
            var ic = document.getElementById("<%=txtIssueCrate.ClientID %>").value
            var rc = document.getElementById("<%=txtReturnCrate.ClientID %>").value;


            if (ic == "") {
                ic = 0;
            }
            if (rc == "") {
                rc = 0;
            }

            var finalsevalue = (parseInt(ic) - parseInt(rc));
            if (!isNaN(finalsevalue)) {

                if (finalsevalue < 0) {
                    document.getElementById("<%=txtShortExcess.ClientID %>").value = parseInt(Math.abs(finalsevalue));
                }
                else {
                    document.getElementById("<%=txtShortExcess.ClientID %>").value = parseInt(-Math.abs(finalsevalue));
                }

            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="loader"></div>
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
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Crate Management</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <fieldset>
                                <legend>Crate Management
                                </legend>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </div>
                                </div>


                                <div class="row">

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Date <%--/ दिनांक--%><span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    ControlToValidate="txtDate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtDate"
                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDate" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Shift <%--/ शिफ्ट--%></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Shift" Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"
                                                    ControlToValidate="ddlShift" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                   <%-- <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Item Category<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Type" Text="<i class='fa fa-exclamation-circle' title='Select Type !'></i>"
                                                    ControlToValidate="ddlItemCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>

                                            <asp:DropDownList ID="ddlItemCategory" Enabled="false" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>--%>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Location<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Location" Text="<i class='fa fa-exclamation-circle' title='Select Location !'></i>"
                                                    ControlToValidate="ddlLocation" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Route No<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Route" Text="<i class='fa fa-exclamation-circle' title='Select Route !'></i>"
                                                    ControlToValidate="ddlRoute" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlRoute" runat="server" OnSelectedIndexChanged="ddlRoute_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control select2">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Dist./SS Name <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Distributor/Superstockist Name" Text="<i class='fa fa-exclamation-circle' title='Select Distributor/Superstockist Name !'></i>"
                                                    ControlToValidate="ddlDitributor" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlDitributor" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Issue Crate<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                    ErrorMessage="Enter Issue Crate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Issue Crate !'></i>"
                                                    ControlToValidate="txtIssueCrate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="Dynamic" ValidationGroup="a"
                                                    ErrorMessage="Enter Valid number, Issue Crate. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Valid number, Issue Crate. !'></i>" ControlToValidate="txtIssueCrate"
                                                    ValidationExpression="^[0-9]+$">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" onfocusout="CalculateShortExcess()" autocomplete="off" CssClass="form-control" ID="txtIssueCrate" MaxLength="5" onkeypress="return validateNum(event);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Return Crate<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                    ErrorMessage="Enter Return Crate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Return Crate !'></i>"
                                                    ControlToValidate="txtReturnCrate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ValidationGroup="a"
                                                    ErrorMessage="Enter Valid number, Return Crate. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Valid number, Return Crate. !'></i>" ControlToValidate="txtReturnCrate"
                                                    ValidationExpression="^[0-9]+$">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" onfocusout="CalculateShortExcess()" autocomplete="off" CssClass="form-control" ID="txtReturnCrate" MaxLength="5" onkeypress="return validateNum(event);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Short / Excess<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a"
                                                    ErrorMessage="Enter Short / Excess" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Short / Excess !'></i>"
                                                    ControlToValidate="txtShortExcess" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtShortExcess" MaxLength="5" onkeypress="return validateNum(event);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Challan No.</label>
                                            <span class="pull-right">
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ValidationGroup="a"
                                                    ErrorMessage="Enter Valid Challan No. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Challan No. !'></i>" ControlToValidate="txtChallano"
                                                    ValidationExpression="^[a-zA-Z0-9\s./-]+$">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtChallano" MaxLength="20"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Remark</label>
                                            <span class="pull-right">
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" Display="Dynamic" ValidationGroup="a"
                                                    ErrorMessage="Enter Valid Remark !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Remark !'></i>" ControlToValidate="txtCrateRemark"
                                                    ValidationExpression="^[a-zA-Z0-9\s./-]+$">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtCrateRemark" MaxLength="200"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />

                                        </div>
                                    </div>
                                    <div class="col-md-1" style="margin-top: 20px;">
                                        <div class="form-group">

                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-default" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                        </div>

                    </div>
                </div>
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Crate Management Details</h3>

                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblReportMsg" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend>Crate Management Details</legend>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Date <%--/ दिनांक--%><span style="color: red;"> *</span></label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="b"
                                                            ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                            ControlToValidate="txtSerchDate" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>

                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="b" runat="server" Display="Dynamic" ControlToValidate="txtDate"
                                                            ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                            ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                                    </span>
                                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtSerchDate" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                            </div>
                                           <%-- <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Item Category<span style="color: red;"> *</span></label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="b"
                                                            InitialValue="0" ErrorMessage="Select Type" Text="<i class='fa fa-exclamation-circle' title='Select Type !'></i>"
                                                            ControlToValidate="ddlItemCat" ForeColor="Red" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator></span>

                                                    <asp:DropDownList ID="ddlItemCat" runat="server" Enabled="false" CssClass="form-control select2"></asp:DropDownList>
                                                </div>
                                            </div>--%>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Location<span style="color: red;"> *</span></label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="b"
                                                            InitialValue="0" ErrorMessage="Select Location" Text="<i class='fa fa-exclamation-circle' title='Select Location !'></i>"
                                                            ControlToValidate="ddlLocationSearch" ForeColor="Red" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator></span>
                                                    <asp:DropDownList ID="ddlLocationSearch" AutoPostBack="true" OnSelectedIndexChanged="ddlLocationSearch_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Route No</label>

                                                    <asp:DropDownList ID="ddlRouteSearch" runat="server" CssClass="form-control select2">
                                                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-1">
                                                <div class="form-group" style="margin-top: 20px;">
                                                    <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="b" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />

                                                </div>
                                            </div>
                                            <div class="col-md-1" style="margin-top: 20px;">
                                                <div class="form-group">

                                                    <asp:Button ID="btnClearSearch" runat="server" OnClick="btnClearSearch_Click" Text="Clear" CssClass="btn btn-default" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GridView1" OnRowCommand="GridView1_RowCommand" runat="server" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                    AutoGenerateColumns="false" EmptyDataText="No Record Found." DataKeyNames="MilkCrateMgmtId" EnableModelValidation="True">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                <asp:Label ID="lblCatId" Visible="false" Text='<%#Eval("ItemCat_id") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIRDate" Text='<%#Eval("IRDate") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Shift" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblShiftName" Text='<%#Eval("ShiftName") %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblShiftId" Visible="false" Text='<%#Eval("ShiftId") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Location" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAreaName" Text='<%#Eval("AreaName") %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblAreaID" Visible="false" Text='<%#Eval("AreaID") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Route" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRName" Text='<%#Eval("RName") %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblRouteId" Visible="false" Text='<%#Eval("RouteId") %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblDistributorId" Visible="false" Text='<%#Eval("DistributorId") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Issue Crate" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIssueCrate" Text='<%#Eval("IssueCrate") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Return Crate" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblReturnCrate" Text='<%#Eval("ReturnCrate") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Short / Excess" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblShortExcess" Text='<%#Eval("ShortExcessCrate") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Challan No." HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblChallanNo" Text='<%#Eval("ChallanNo") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Remark" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCrateRemark" Text='<%#Eval("CrateRemark") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("MilkCrateMgmtId") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>

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
    <%-- <script src="js/buttons.colVis.min.js"></script>--%>


    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);

        $(document).ready(function () {
            $('.loader').fadeOut();
        });


        $('.datatable').DataTable({
            paging: true,
            iDisplayLength: 100,
            lengthMenu: [10, 25, 50,100],
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
                    title: ('Crate Management').bold().fontsize(5).toUpperCase(),
                    exportOptions: {
                        columns: [0, 1, 2, 3,4,5,6,7,8,9]
                    },
                    footer: false,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    title: ('Crate Management').bold().fontsize(5).toUpperCase(),
                    filename: 'CrateManagement',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',

                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7,8,9]
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
