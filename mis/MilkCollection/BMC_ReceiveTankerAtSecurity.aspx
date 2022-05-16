<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="BMC_ReceiveTankerAtSecurity.aspx.cs" Inherits="mis_MilkCollection_BMC_ReceiveTankerAtSecurity" %>
 
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
    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-Manish" style="min-height: 250px;">
                <div class="box-header">
                    <h3 class="box-title">BMC/MDP Update Tanker Gross Weight</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </div>

                    <fieldset>
                        <legend>Update Gross Weight</legend>
                        <div class="row">

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Reference No.<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvReferenceNo" runat="server" Display="Dynamic" ValidationGroup="a" ControlToValidate="ddlReferenceNo" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Enter Reference No!'></i>" ErrorMessage="Enter Reference No." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddlReferenceNo" Width="100%" AutoPostBack="true" CssClass="form-control select2" runat="server" OnSelectedIndexChanged="ddlReferenceNo_SelectedIndexChanged">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>


                            <div class="col-md-2" id="dv_TankerType" runat="server">
                                <label>MilkDispatch Type<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvTankerType" runat="server" Display="Dynamic" ControlToValidate="ddlMilkDispatchtype" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select MilkDispatch Type!'></i>" ErrorMessage="Select MilkDispatch Type" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>
                                </span>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlMilkDispatchtype" Enabled="false" AutoPostBack="true" CssClass="form-control" runat="server">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Single Chamber" Value="S"></asp:ListItem>
                                        <asp:ListItem Text="Dual Chamber" Value="D"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Tanker Arrival Date</label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvArrivalDate" runat="server" Display="Dynamic" ControlToValidate="txtArrivalDate" Text="<i class='fa fa-exclamation-circle' title='Enter Tanker Arrival Date!'></i>" ErrorMessage="Enter Tanker Arrival Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">
                                                <i class="far fa-calendar-alt"></i>
                                            </span>
                                        </div>
                                        <asp:TextBox ID="txtArrivalDate" Enabled="false" autocomplete="off" placeholder="Tanker Arrival Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Gross Weight (In KG) <span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" ValidationGroup="a"
                                            ErrorMessage="Enter Gross Weight" Text="<i class='fa fa-exclamation-circle' title='Enter Gross Weight !'></i>"
                                            ControlToValidate="txtD_GrossWeight" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <asp:RangeValidator ID="rvalidator" runat="server" ControlToValidate="txtD_GrossWeight" Type="Double" ErrorMessage="Required Minimum Value 999!" Text="<i class='fa fa-exclamation-circle' title='Required Minimum Value 999!'></i>" SetFocusOnError="true" ForeColor="Red" Display="Dynamic" ValidationGroup="a" MinimumValue="999" MaximumValue="9999999999"></asp:RangeValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtD_GrossWeight" ErrorMessage="Gross Weight Required" Text="<i class='fa fa-exclamation-circle' title='Gross Weight Required !'></i>" SetFocusOnError="true" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                                    <asp:TextBox runat="server" AutoPostBack="true" OnTextChanged="txtD_GrossWeight_TextChanged" autocomplete="off" CssClass="form-control" ID="txtD_GrossWeight" ClientIDMode="Static" onkeypress="return validateDec(this,event)" MaxLength="10" placeholder="Enter Gross Weight"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Receipt No. <span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" ValidationGroup="a"
                                            ErrorMessage="Enter Receipt No." Text="<i class='fa fa-exclamation-circle' title='Enter Receipt No.!'></i>"
                                            ControlToValidate="txtReceiptNo" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>

                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtReceiptNo" ClientIDMode="Static" MaxLength="12" placeholder="Enter Receipt No."></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </fieldset>

                   <%-- <div class="row" runat="server" visible="false" id="divsealdetail">
                        <div class="col-lg-12">
                            <fieldset>
                                <legend>Tanker Seal Details</legend>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Seal Type </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="Dynamic" ControlToValidate="ddlSealtype" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Sale Type!'></i>" ErrorMessage="Select Sale Type." SetFocusOnError="true" ForeColor="Red" ValidationGroup="c"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlSealtype" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="Old">Old</asp:ListItem>
                                            <asp:ListItem Value="Broken">Broken</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Chamber Type </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Display="Dynamic" ControlToValidate="ddlChamberType" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Seal Color!'></i>" ErrorMessage="Select Seal Color." SetFocusOnError="true" ForeColor="Red" ValidationGroup="c"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlChamberType" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="Chamber">Chamber</asp:ListItem>
                                            <asp:ListItem Value="ValveBox">ValveBox</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Seal Color </label>
                                        <asp:RequiredFieldValidator ID="rfvSealColor" runat="server" Display="Dynamic" ControlToValidate="ddlSealColor" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Seal Color!'></i>" ErrorMessage="Select Seal Color." SetFocusOnError="true" ForeColor="Red" ValidationGroup="c"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlSealColor" OnInit="ddlSealColor_Init" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Seal No.</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="c"
                                                ErrorMessage="Enter Seal No" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Seal No !'></i>"
                                                ControlToValidate="txtV_SealNo" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic" ValidationExpression="^[0-9a-z-A-Z]+$" ValidationGroup="c" runat="server" ControlToValidate="txtV_SealNo" ErrorMessage="Enter Valid Seal Number." Text="<i class='fa fa-exclamation-circle' title='Enter Valid Seal Number. !'></i>"></asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox autocomplete="off" runat="server" CssClass="form-control" ID="txtV_SealNo" MaxLength="20" placeholder="Enter Seal No"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Seal Remark </label>
                                        <asp:TextBox autocomplete="off" runat="server" CssClass="form-control" ID="txtV_SealRemark" MaxLength="200" placeholder="Enter Seal Remark"></asp:TextBox>
                                    </div>
                                </div>



                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnTankerSealDetails_Click" Style="margin-top: 20px;" ValidationGroup="c" ID="btnTankerSealDetails" Text="Add Seal" />
                                    </div>
                                </div>

                                <hr />

                                <asp:GridView ID="gv_SealInfo" ShowHeader="true" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSealNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Seal Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSealtype" runat="server" Text='<%# Eval("Sealtype") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Chamber Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblChamberType" runat="server" Text='<%# Eval("ChamberType") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Seal Color">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_SealColor" runat="server" Text='<%# Eval("V_SealColor") %>'></asp:Label>
                                                <asp:Label ID="lblTI_SealColor" Visible="false" runat="server" Text='<%# Eval("TI_SealColor") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Seal No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_SealNo" runat="server" Text='<%# Eval("V_SealNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Seal Remark">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_SealRemark" runat="server" Text='<%# Eval("V_SealRemark") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" ToolTip="DeleteSeal" OnClick="lnkDelete_Click" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete this record?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>

                            </fieldset>
                        </div>
                    </div>


                    <div class="row">

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Seal Verification Documents [Panchnama]</label>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">
                                            <i class="far fa-calendar-alt"></i>
                                        </span>
                                    </div>
                                    <asp:FileUpload runat="server" ID="FuSealVerificationfile" CssClass="form-control"></asp:FileUpload>
                                </div>
                            </div>
                        </div>

                    </div>--%>

                    <fieldset id="fs_action" runat="server" visible="true">
                        <legend>Action</legend>
                        <div class="row">
                            <div class="col-md-1">
                                <div class="form-group">
                                    <asp:Button runat="server" Style="margin-top: 20px;" Enabled="true" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Update" OnClientClick="return ValidatePage();" AccessKey="S" />
                                </div>
                            </div>
                            <div class="col-md-1">
                                <div class="form-group">
                                    <asp:Button ID="btnClear" runat="server" Style="margin-top: 20px;" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-block btn-default" />
                                </div>
                            </div>
                        </div>
                    </fieldset>

                </div>

            </div>
            <!-- /.box-body -->

            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Updated Tanker Gross Weight by Security</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Arrival Date</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">
                                            <i class="far fa-calendar-alt"></i>
                                        </span>
                                    </div>
                                    <asp:TextBox ID="txtDate" autocomplete="off" AutoPostBack="true" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="table-responsive">

                            <asp:GridView ID="gv_TodayReceivedTankerDetails" ShowHeader="true" EmptyDataText="No Record Found" EmptyDataRowStyle-ForeColor="Red" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSealNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Arrival Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDT_TankerArrivalDate" runat="server" Text='<%# (Convert.ToDateTime(Eval("DT_TankerArrivalDate"))).ToString("dd-MM-yyyy hh:mm:ss tt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Reference No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblC_ReferenceNo" runat="server" Text='<%# Eval("C_ReferenceNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Challan No/Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblChallan_No" runat="server" Text='<%# Eval("Challan_No") %>'></asp:Label>
                                            <asp:Label ID="lbltcs" ForeColor="Red" Font-Bold="true" runat="server" Text='<%# Eval("Challan_No").ToString() == "" ? "Pending" : "" %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Office Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Vehicle No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblV_VehicleNo" runat="server" Text='<%# Eval("V_VehicleNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Driver Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblV_DriverName" runat="server" Text='<%# Eval("V_DriverName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Driver Mobile No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblV_DriverMobileNo" runat="server" Text='<%# Eval("V_DriverMobileNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Gross Weight (In KG)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblD_GrossWeight" runat="server" Text='<%# Eval("D_GrossWeight") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Gross Weight ReceiptNo">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWeightReceiptNo" runat="server" Text='<%# Eval("WeightReceiptNo") %>'></asp:Label>
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

    <script>
        function GetLocation() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(success);
            } else {
                alert("There is Some Problem on your current browser to get Geo Location!");
            }
        }

        function success(position) {
            var lat = position.coords.latitude;
            var long = position.coords.longitude;
        }
    </script>

</asp:Content>

