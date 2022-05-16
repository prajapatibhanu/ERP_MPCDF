<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Tanker_Master.aspx.cs" Inherits="mis_Masters_Tanker_Master" %>
 
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
    <asp:ValidationSummary ID="vs1" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="vs2" runat="server" ValidationGroup="c" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-Manish" style="min-height: 250px;">
                <div class="box-header">
                    <h3 class="box-title">Tanker Details</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </div>

                    <fieldset>
                        <legend>Select Vehicle/Tanker  </legend>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Vehicle Type<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select Tanker Type" Text="<i class='fa fa-exclamation-circle' title='Select Vehicle Type !'></i>"
                                            ControlToValidate="ddlTankerDetail" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlTankerDetail" AutoPostBack="true" runat="server" CssClass="form-control select2" ClientIDMode="Static" OnSelectedIndexChanged="ddlTankerDetail_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="S">Single Chamber</asp:ListItem>
                                        <asp:ListItem Value="D">Dual Chamber</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Vehicle No<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                            ErrorMessage="Enter Vehicle No" Text="<i class='fa fa-exclamation-circle' title='Enter Vehicle No !'></i>"
                                            ControlToValidate="txtV_VehicleNo" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtV_VehicleNo" Display="Dynamic" ValidationExpression="^[A-Z|a-z]{2}-\d{2}-[A-Z|a-z]{1,2}-\d{4}$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid vehicle no. format (XX-00-XX-0000)!'></i>" ErrorMessage="Invalid vehicle no. format (XX-00-XX-0000)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtV_VehicleNo" ClientIDMode="Static" MaxLength="13" placeholder="XX-00-XX-0000"></asp:TextBox>
                                </div>
                            </div>
                            <div id="divchamber" runat="server" visible="false">
                                <div class="col-md-2">
                                <div class="form-group">
                                    <label>Front Chamber Capacity<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a"
                                            ErrorMessage="Enter Single Chamber Capacity" Text="<i class='fa fa-exclamation-circle' title='Enter front Capacity !'></i>"
                                            ControlToValidate="txtSingleChamberCapacity" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtSingleChamberCapacity" ErrorMessage="Invalid Vehicle Capacity" Text="<i class='fa fa-exclamation-circle' title='Invalid Vehicle Capacity !'></i>" SetFocusOnError="true" ValidationExpression="^[0-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtSingleChamberCapacity" ClientIDMode="Static" onkeypress="return validateDec(this,event)" placeholder="Enter front Capacity" onchange="TotalCapacity();"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Rear Chamber Capacity<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a"
                                            ErrorMessage="Enter Dual Chamber Capacity" Text="<i class='fa fa-exclamation-circle' title='Enter Rear Capacity !'></i>"
                                            ControlToValidate="txtDualChamberCapacity" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDualChamberCapacity" ErrorMessage="Invalid Vehicle Capacity" Text="<i class='fa fa-exclamation-circle' title='Invalid Vehicle Capacity !'></i>" SetFocusOnError="true" ValidationExpression="^[0-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDualChamberCapacity" ClientIDMode="Static" onkeypress="return validateDec(this,event)" placeholder="Enter Rear Capacity" onchange="TotalCapacity();"></asp:TextBox>
                                </div>
                            </div>
                            </div>
                            
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Vehicle Capacity<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                            ErrorMessage="Enter Vehicle Capacity" Text="<i class='fa fa-exclamation-circle' title='Enter Vehicle Capacity !'></i>"
                                            ControlToValidate="txtD_VehicleCapacity" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtD_VehicleCapacity" ErrorMessage="Invalid Vehicle Capacity" Text="<i class='fa fa-exclamation-circle' title='Invalid Vehicle Capacity !'></i>" SetFocusOnError="true" ValidationExpression="^[0-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtD_VehicleCapacity" ClientIDMode="Static" onkeypress="return validateDec(this,event)" placeholder="Enter Vehicle Capacity"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Vendor Name<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                            ErrorMessage="Enter Vendor Name" Text="<i class='fa fa-exclamation-circle' title='Enter Vendor Name !'></i>"
                                            ControlToValidate="txtV_VenderName" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtV_VenderName" ErrorMessage="Invalid Vendor Name" Text="<i class='fa fa-exclamation-circle' title='Invalid Vendor Name !'></i>" SetFocusOnError="true" ValidationExpression="^[a-zA-Z'.\s]{1,200}$"></asp:RegularExpressionValidator>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtV_VenderName" ClientIDMode="Static" ReadOnly="false" placeholder="Enter Vendor Name"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Vendor Contact No<span style="color: red;"> *</span></label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                        ErrorMessage="Enter Vendor Contact No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Vendor Contact No. !'></i>"
                                        ControlToValidate="txtV_VendorContactNo" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" Display="Dynamic" ValidationGroup="a"
                                        ErrorMessage="Enter Valid Vendor Contact No. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Vendor Contact No. !'></i>" ControlToValidate="txtV_VendorContactNo"
                                        ValidationExpression="^[6-9]{1}[0-9]{9}$">
                                    </asp:RegularExpressionValidator>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtV_VendorContactNo" ClientIDMode="Static" ReadOnly="false" placeholder="Enter Vendor Contact No"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Milk Collection From<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select Select Milk Collection From" Text="<i class='fa fa-exclamation-circle' title='Select Milk Collection From !'></i>"
                                            ControlToValidate="ddlMilkCollectionFrom" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlMilkCollectionFrom" Width="100%" AutoPostBack="false" runat="server" CssClass="form-control select2" ClientIDMode="Static">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="CC">CC</asp:ListItem>
                                        <asp:ListItem Value="BMC">BMC</asp:ListItem>
                                        <asp:ListItem Value="MDP">MDP</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Active Status<span style="color: red;"> *</span></label><br />
                                    <asp:CheckBox runat="server" ID="chkIsActive" Checked="true" />
                                </div>
                            </div>

                        </div>
                    </fieldset>



                    <div class="row" runat="server">
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
                    <h3 class="box-title">Tanker Details</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="table-responsive">
                        <asp:GridView ID="gv_TankerDetails" OnRowDataBound="gv_TankerDetails_RowDataBound" ShowHeader="true" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server" OnRowCommand="gv_TankerDetails_RowCommand" DataKeyNames="I_TankerID">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSealNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vehicle Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTType" runat="server" Text='<%# Eval("TType") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vendor Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblV_Vendor" runat="server" Text='<%# Eval("V_VenderName") %>'></asp:Label>
                                        <asp:Label ID="LblvendorType" runat="server" Text='<%# Eval("V_VehicleType") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblTankerStatus" runat="server" Text='<%# Eval("TankerStatus") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vendor ContactNo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblV_VendorContact" runat="server" Text='<%# Eval("V_VendorContactNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vehicle No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblV_VehicleNo" runat="server" Text='<%# Eval("V_VehicleNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Single Chamber Capacity" Visible =" false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblV_SingleChambCapacity" runat="server" Text='<%# Eval("V_SingleChambCapacity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dual Chamber Capacity" Visible =" false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblV_DualChambCapacity" runat="server" Text='<%# Eval("V_DualChambCapacity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vehicle Capacity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblV_VehicleCapacity" runat="server" Text='<%# Eval("D_VehicleCapacity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Active Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsActive" runat="server" Text='<%# Eval("IsActive").ToString() == "False" ? "InActive" : "Active" %>'></asp:Label>
                                        <asp:Label ID="lblTstatus" Visible="false" runat="server" Text='<%# Eval("IsActive") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Tanker Current Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltcs" runat="server" Text='<%# Eval("TankerStatus").ToString() == "False" ? "In Process" : "Available" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Milk Collection From">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMilkCollectionFrom" runat="server" Text='<%# Eval("MilkCollectionFrom") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action" Visible="true">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("I_TankerID") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
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
        function TotalCapacity()
        {
            debugger;
            var SinglChambCapacity = document.getElementById('<%= txtSingleChamberCapacity.ClientID%>').value;
            var DualChambCapacity = document.getElementById('<%= txtDualChamberCapacity.ClientID%>').value;
            if (SinglChambCapacity == '')
            {
                SinglChambCapacity = 0;
            }
            if (DualChambCapacity == '') {
                DualChambCapacity = 0;
            }
            var TotalCapacity = parseFloat(parseFloat(SinglChambCapacity) + parseFloat(DualChambCapacity));
            document.getElementById('<%= txtD_VehicleCapacity.ClientID%>').value = TotalCapacity;
        }
    </script>

</asp:Content>


