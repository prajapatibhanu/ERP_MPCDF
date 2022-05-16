<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ReceivedTankerOutBySecurity.aspx.cs" Inherits="mis_MilkCollection_ReceivedTankerOutBySecurity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
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
                    <h3 class="box-title">Update Net Weight of Received Milk</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            <asp:ValidationSummary ID="vsummaru" runat="server" ValidationGroup="add" ShowMessageBox="true" ShowSummary="false" />
                        </div>
                    </div>

                    <fieldset>
                        <legend>Tanker Details</legend>
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
                                <label>Tanker Type<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvTankerType" runat="server" Display="Dynamic" ControlToValidate="ddlTankerType" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Tanker Type!'></i>" ErrorMessage="Select Tanker Type" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>
                                </span>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlTankerType" Enabled="false" AutoPostBack="true" CssClass="form-control" runat="server">
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                            ErrorMessage="Enter Gross Weight" Text="<i class='fa fa-exclamation-circle' title='Enter Gross Weight !'></i>"
                                            ControlToValidate="txtD_GrossWeight" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <asp:RangeValidator ID="rvalidator" runat="server" ControlToValidate="txtD_GrossWeight" Type="Double" ErrorMessage="Required Minimum Value 999!" Text="<i class='fa fa-exclamation-circle' title='Required Minimum Value 999!'></i>" SetFocusOnError="true" ForeColor="Red" Display="Dynamic" ValidationGroup="a" MinimumValue="999" MaximumValue="9999999999"></asp:RangeValidator>

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtD_GrossWeight" ErrorMessage="Gross Weight Required" Text="<i class='fa fa-exclamation-circle' title='Gross Weight Required !'></i>" SetFocusOnError="true" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>

                                    <asp:TextBox runat="server" Enabled="false" autocomplete="off" CssClass="form-control" ID="txtD_GrossWeight" ClientIDMode="Static" onkeypress="return validateDec(this,event)" MaxLength="10" placeholder="Enter Gross Weight"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Receipt No. <span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" SetFocusOnError="true" ValidationGroup="a"
                                            ErrorMessage="Enter Receipt No." Text="<i class='fa fa-exclamation-circle' title='Enter Receipt No.!'></i>"
                                            ControlToValidate="txtReceiptNo" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>

                                    <asp:TextBox runat="server" autocomplete="off" Enabled="false" CssClass="form-control" ID="txtReceiptNo" ClientIDMode="Static" MaxLength="12" placeholder="Enter Receipt No."></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </fieldset>

                    <div class="row">
                        <div class="col-md-12" id="div_SealVerification_Single_Challan" runat="server" visible="false">
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend>Update Net Weight
                                        </legend>
                                        <div class="mb-1">
                                            <i style="color: red;"><b>Note</b>:-
                                            <br />
                                                (a)<b>Single Chamber:</b> Enter Net weight Of Single Compartment.<br />
                                                (b)<b>Dual Chamber:</b> Enter Net weight Of Front/Rear Compartment.<br />
                                            </i>
                                            <hr />
                                        </div>

                                        <asp:GridView ID="gv_ViewChallanDetail" Visible="false" ShowHeader="true" ShowFooter="true" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Challan No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblI_EntryID" Visible="false" runat="server" Text='<%# Eval("I_EntryID") %>'></asp:Label>
                                                        <asp:Label ID="lblChallanNo" runat="server" Text='<%# Eval("ChallanNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <b>Grand Total</b>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Chamber Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblV_SealLocation" Visible="false" runat="server" Text='<%# Eval("V_SealLocation") %>'></asp:Label>
                                                        <asp:Label ID="lblSealLocationText" runat="server" Text='<%# Eval("SealLocationText") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Milk Quantity (In KG)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtI_MilkQuantity" AutoPostBack="true" OnTextChanged="txtI_MilkQuantity_TextChanged" onkeypress="return validateDec(this,event)" runat="server" Text='<%# Eval("I_MilkQuantity") %>'></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5_N" ValidationGroup="a"
                                                            ErrorMessage="Enter Net Quantity (In KG)" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Net Quantity (In KG)!'></i>"
                                                            ControlToValidate="txtI_MilkQuantity" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5_N" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="a" runat="server" ControlToValidate="txtI_MilkQuantity" ErrorMessage="Enter Valid Milk Quantity" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Milk Quantity!'></i>"></asp:RegularExpressionValidator>

                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotal_I_MilkQuantity" Font-Bold="true" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>


                                    </fieldset>
                                </div>
                            </div>
                        </div>
                    </div>

                    <fieldset id="fs_action" runat="server" visible="false">
                        <legend>Action</legend>
                        <div class="row">
                            <div class="col-md-1">
                                <div class="form-group">
                                    <asp:Button runat="server" Style="margin-top: 20px;" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Update" Visible="false" OnClientClick="return ValidatePage();" AccessKey="S" />
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
            <div class="box box-Manish" runat="server" visible="false">
                <div class="box-header">
                    <h3 class="box-title">Reference Details</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">

                    <div class="table-responsive">

                        <asp:GridView ID="gv_viewreferenceno" ShowHeader="true" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSealNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reference No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblC_ReferenceNo" runat="server" Text='<%# Eval("C_ReferenceNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Generate From">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Generate Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDT_TankerDispatchDate" runat="server" Text='<%# Eval("DT_TankerDispatchDate") %>'></asp:Label>
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

                                <asp:TemplateField HeaderText="Tanker Arrival Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDT_TankerArrivalDate" runat="server" Text='<%# Eval("DT_TankerArrivalDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Gross Weight (In KG)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblD_GrossWeight" runat="server" Text='<%# Eval("D_GrossWeight") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

            </div>

            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Received Tanker Details by Security</h3>
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
                                    <asp:TextBox ID="txtDate" autocomplete="off" AutoPostBack="true"  OnTextChanged="txtDate_TextChanged" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
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

                                    <asp:TemplateField HeaderText="Reference No">
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

                                    <asp:TemplateField HeaderText="Net Weight (In KG)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblD_NetWeight" runat="server" Text='<%# Eval("D_NetWeight") %>'></asp:Label>
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


