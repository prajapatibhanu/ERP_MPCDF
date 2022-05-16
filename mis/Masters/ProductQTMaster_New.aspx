<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ProductQTMaster_New.aspx.cs" Inherits="mis_dailyplan_ProductQTMaster_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <%--ConfirmationModal Start --%>
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
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">

                <div class="box-header">
                    <h3 class="box-title">QC Testing Parameter Master</h3>
                </div>
                <div class="box-body">
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

                    <fieldset runat="server">
                        <legend>QC Testing Parameter Details</legend>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group table-responsive">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                                    ControlToValidate="ddlDS" InitialValue="0"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select DS!'></i>"
                                                    ErrorMessage="Select DS." SetFocusOnError="true" ForeColor="Red"
                                                    ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>

                                            <asp:DropDownList ID="ddlDS" runat="server" CssClass="form-control select2"></asp:DropDownList>

                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Production Section<span class="text-danger">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                                    ControlToValidate="ddlPSection" InitialValue="0"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Section!'></i>"
                                                    ErrorMessage="Select Section." SetFocusOnError="true" ForeColor="Red"
                                                    ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlPSection" AutoPostBack="true" OnSelectedIndexChanged="ddlPSection_SelectedIndexChanged" runat="server" CssClass="form-control select2"></asp:DropDownList>

                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Product<span class="text-danger">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                                                    ControlToValidate="ddlProduct" InitialValue="0"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Product!'></i>"
                                                    ErrorMessage="Select Product." SetFocusOnError="true" ForeColor="Red"
                                                    ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlProduct" AutoPostBack="true" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" OnInit="ddlproductdetails_Init" runat="server" CssClass="form-control select2"></asp:DropDownList>

                                        </div>
                                    </div>


                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>
                                                Test Parameter Name<span class="text-danger">*</span>
                                                <asp:LinkButton ID="lblAddTanker" CausesValidation="false" ToolTip="Add New Tanker Details" OnClick="lblAddTanker_Click" runat="server"><b>[Add]</b></asp:LinkButton>
                                            </label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic"
                                                    ControlToValidate="ddlParameter" InitialValue="0"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Test Parameter Name!'></i>"
                                                    ErrorMessage="Select Test Parameter Name." SetFocusOnError="true" ForeColor="Red"
                                                    ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlParameter" OnInit="ddlParameter_Init" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>

                                        </div>
                                    </div>


                                    <%--  <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Test Parameter Name<span class="text-danger">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic"
                                                    ControlToValidate="txtParameter"
                                                    Text="<i class='fa fa-exclamation-circle' title='Enter Test Parameter Name!'></i>"
                                                    ErrorMessage="Enter Test Parameter Name." SetFocusOnError="true" ForeColor="Red"
                                                    ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span> 
                                            <asp:TextBox ID="txtParameter" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>--%>


                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Calculation Method<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"
                                                    ControlToValidate="ddlCalculationMethod" InitialValue="0"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Calculation Method!'></i>"
                                                    ErrorMessage="Select Calculation Method." SetFocusOnError="true" ForeColor="Red"
                                                    ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>

                                            <asp:DropDownList ID="ddlCalculationMethod" runat="server" CssClass="form-control select2" ClientIDMode="Static" OnSelectedIndexChanged="ddlCalculationMethod_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="Value">Value</asp:ListItem>
                                                <asp:ListItem Value="Use By Days">Use By Days</asp:ListItem>
                                                <asp:ListItem Value="Adulteration Test">Adulteration Test</asp:ListItem>
                                                <asp:ListItem Value="Manufacturing Date">Manufacturing Date</asp:ListItem>
                                                <asp:ListItem Value="OT">OT</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>


                                    <div class="col-md-3" runat="server" visible="false" id="DivCM_Value1">
                                        <div class="form-group">
                                            <label>Min. Value<span style="color: red;">*</span></label>

                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic"
                                                    ControlToValidate="txtMinValue"
                                                    Text="<i class='fa fa-exclamation-circle' title='Enter Min. Value!'></i>"
                                                    ErrorMessage="Enter Min. Value." SetFocusOnError="true" ForeColor="Red"
                                                    ValidationGroup="a"></asp:RequiredFieldValidator>

                                                <asp:RangeValidator ID="rvalidator" runat="server" ControlToValidate="txtMinValue" Type="Double" Display="Dynamic" ErrorMessage="Required Minimum Value 0!" Text="<i class='fa fa-exclamation-circle' title='Required Minimum Value 0!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a" MinimumValue="0" MaximumValue="100"></asp:RangeValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtMinValue" ErrorMessage="Minimum Value Required" Text="<i class='fa fa-exclamation-circle' title='Min. Value Required !'></i>" SetFocusOnError="true" ValidationExpression="((\d+)((\.\d{0,2})?))$"></asp:RegularExpressionValidator>

                                            </span>

                                            <asp:TextBox ID="txtMinValue" runat="server" placeholder="Enter Min. Value..." class="form-control" onkeypress="return validate3Dec(this,event);" MaxLength="12"></asp:TextBox>

                                        </div>
                                    </div>

                                    <div class="col-md-3" runat="server" visible="false" id="DivCM_Value2">
                                        <div class="form-group">
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic"
                                                    ControlToValidate="txtMaxValue"
                                                    Text="<i class='fa fa-exclamation-circle' title='Enter Max. Value!'></i>"
                                                    ErrorMessage="Enter Test Max. Value." SetFocusOnError="true" ForeColor="Red"
                                                    ValidationGroup="a"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtMaxValue" Type="Double" Display="Dynamic" ErrorMessage="Required Max Value 1!" Text="<i class='fa fa-exclamation-circle' title='Required Max Value 100!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a" MinimumValue="1" MaximumValue="100"></asp:RangeValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtMaxValue" ErrorMessage="Max Value Required" Text="<i class='fa fa-exclamation-circle' title='Max. Value Required !'></i>" SetFocusOnError="true" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>

                                            </span>
                                            <label>Max. Value<span style="color: red;">*</span></label>
                                            <asp:TextBox ID="txtMaxValue" runat="server" placeholder="Enter Max. Value..." class="form-control" onkeypress="return validate3Dec(this,event);" MaxLength="12"></asp:TextBox>

                                        </div>
                                    </div>







                                    <div class="col-md-3" runat="server" visible="false" id="DivCM_Option">
                                        <div class="form-group">
                                            <label>Calculation Option<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="Dynamic"
                                                    ControlToValidate="ddloption" InitialValue="0"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Calculation Option!'></i>"
                                                    ErrorMessage="Select Calculation Option." SetFocusOnError="true" ForeColor="Red"
                                                    ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddloption" CssClass="form-control  select2" runat="server">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>


                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Unit<span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlUnit" OnInit="ddlUnit_Init" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>
                    </fieldset>

                    <fieldset runat="server">
                        <legend>Action</legend>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" runat="server" ValidationGroup="a" OnClientClick="return ValidatePage();" CssClass="btn btn-primary" Text="Save" />
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <a href="ProductQTMaster_New.aspx" class="btn btn-default">Cancel</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </fieldset>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="table table-responsive">
                                <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="true"
                                    EmptyDataText="No Record Found" class="table table-hover table-bordered pagination-ys"
                                    AutoGenerateColumns="False" AllowPaging="False" DataKeyNames="QCParameterID"
                                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("QCParameterID").ToString()%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="QCParameterName" HeaderText="Parameter Name" />
                                        <asp:BoundField DataField="CalculationMethod" HeaderText="Calculation Method" />
                                        <asp:BoundField DataField="OptionDetails" HeaderText="Option" />
                                        <asp:BoundField DataField="MinValue" HeaderText="Min. Value" />
                                        <asp:BoundField DataField="MaxValue" HeaderText="Max. Value" />
                                        <%--<asp:TemplateField HeaderText="Edit" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Select" runat="server" CssClass="label label-default" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField ItemStyle-Width="30" HeaderText="Active">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" OnCheckedChanged="chkSelect_CheckedChanged"
                                                    runat="server" ToolTip='<%# Eval("QCParameterID").ToString()%>' Checked='<%# Eval("IsActive").ToString()=="1" ? true : false %>' AutoPostBack="true" />
                                            </ItemTemplate>
                                            <ItemStyle Width="30px"></ItemStyle>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>

            </div>


            <asp:ValidationSummary ID="v1" runat="server" ValidationGroup="save" ShowMessageBox="true" ShowSummary="false" />

            <div class="modal" id="ParameterModalDetail">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 420px;">
                        <div class="modal-header">
                            <asp:LinkButton runat="server" class="close" ID="btnCrossButton" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>
                            <%-- <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                </button>--%>
                            <h4 class="modal-title">Test Parameter Master</h4>
                        </div>
                        <div class="modal-body">

                            <asp:Label ID="lblModalMsg" runat="server" Text=""></asp:Label>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row" style="height: 280px; overflow: scroll;">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <section class="content">
                                                    <!-- SELECT2 EXAMPLE -->
                                                    <div class="box box-Manish" style="min-height: 200px;">
                                                        <div class="box-header">
                                                            <h3 class="box-title">Test Parameter Master</h3>
                                                        </div>
                                                        <!-- /.box-header -->
                                                        <div class="box-body">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <asp:Label ID="lblPopupMsg" runat="server"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <fieldset>
                                                                <legend>Testing Parameter  </legend>
                                                                <div class="row">

                                                                    <div class="col-md-8">
                                                                        <div class="form-group">
                                                                            <label>Parameter Name<span style="color: red;"> *</span></label>
                                                                            <span class="pull-right">
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="save"
                                                                                    ErrorMessage="Enter Parameter Name" Text="<i class='fa fa-exclamation-circle' title='Enter Parameter Name !'></i>"
                                                                                    ControlToValidate="txtV_ParameterName" ForeColor="Red" Display="Dynamic" runat="server">
                                                                                </asp:RequiredFieldValidator></span>
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="save" Display="Dynamic" runat="server" ControlToValidate="txtV_ParameterName" ErrorMessage="Invalid Parameter Name" Text="<i class='fa fa-exclamation-circle' title='Invalid Parameter Name !'></i>" SetFocusOnError="true" ValidationExpression="^[a-zA-Z'.\s]{1,200}$"></asp:RegularExpressionValidator>
                                                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtV_ParameterName" placeholder="Enter Parameter Name"></asp:TextBox>
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

                                                        </div>

                                                    </div>
                                                    <!-- /.box-body -->

                                                    <div class="box box-Manish">
                                                        <div class="box-header">
                                                            <h3 class="box-title">Parameter Details</h3>
                                                        </div>
                                                        <!-- /.box-header -->
                                                        <div class="box-body">
                                                            <div class="table-responsive">
                                                                <asp:GridView ID="gv_ParameterDetails" ShowHeader="true" OnRowCommand="gv_ParameterDetails_RowCommand" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server" DataKeyNames="QCParameterID">
                                                                    <Columns>

                                                                        <asp:TemplateField HeaderText="S.No.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField> 

                                                                        <asp:TemplateField HeaderText="Parameter Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblParameterName" runat="server" Text='<%# Eval("QCParameterName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField> 

                                                                        <asp:TemplateField HeaderText="Active Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblIsActive" runat="server" Text='<%# Eval("IsActive").ToString() == "0" ? "InActive" : "Active" %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField> 

                                                                        <asp:TemplateField HeaderText="Action">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("QCParameterID") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
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
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="save" ID="btnSavePMDetails" Text="Save" OnClientClick="return ValidateTPM()" />
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>


            <div class="modal fade" id="myModalT" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #d9d9d9;">
                            <button type="button" class="close" data-dismiss="modal">
                                <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                            </button>
                            <h4 class="modal-title" id="myModalLabelT">Confirmation</h4>
                        </div>
                        <div class="clearfix"></div>
                        <div class="modal-body">
                            <p>
                                <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp;
                            <asp:Label ID="lblPopupT" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYesT" OnClick="btnYesT_Click" Style="margin-top: 20px; width: 50px;" />
                            <asp:Button ID="Button2" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>

        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script>

        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }
            debugger;
            if (Page_IsValid) {

                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }

            }
        }

        function ParameterModal() {
            $("#ParameterModalDetail").modal('show');

        }


        function ValidateTPM() {
            debugger
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('save');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSavePMDetails.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupT.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModalT').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSavePMDetails.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupT.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModalT').modal('show');
                    return false;
                }
            }
        }

    </script>


</asp:Content>
