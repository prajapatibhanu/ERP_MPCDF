<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MilkRateMaster.aspx.cs" Inherits="mis_Common_MilkRateMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .dropdown-menu {
            position: absolute;
            top: 100%;
            left: 0;
            z-index: 1000;
            display: none;
            float: left;
            min-width: 160px;
            padding: 5px 0;
            margin: 5px 0 0 !important;
            font-size: 14px;
            z-index: 3000 !important;
            text-align: left;
            list-style: none;
            background-color: #fff;
            -webkit-background-clip: padding-box;
            background-clip: padding-box;
            border: 1px solid #ccc;
            border: 1px solid rgba(0, 0, 0, .15);
            border-radius: 4px;
            -webkit-box-shadow: 0 6px 12px rgba(0, 0, 0, .175);
            box-shadow: 0 6px 12px rgba(0, 0, 0, .175);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div>

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
                        <h3 class="box-title">Milk Rate Master(For milk Collection)</h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>


                                <%--                            <div class="col-md-6">
                                <label>Dugdh Sangh <span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rq1" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlDS" InitialValue="0" ErrorMessage="Select Dugdh Sangh." Text="<i class='fa fa-exclamation-circle' title='Please Dugdh Sangh !'></i>"></asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlDS" runat="server" OnInit="ddlDS_Init" CssClass="form-control select2"></asp:DropDownList>
                            </div>--%>
                                <div class="col-md-4" style="display:none;">
                                    <label>
                                        <asp:Label ID="lblOfficeTypeName" runat="server" Text="Dugdh Sangh"></asp:Label>
                                        <span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlDS" InitialValue="0" ErrorMessage="Select Dairy Corporative Society." Text="<i class='fa fa-exclamation-circle' title='Select Dairy Corporative Society !'></i>"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddlDS" runat="server" OnInit="ddlDS_Init" CssClass="form-control select2"></asp:DropDownList>
                                </div>


                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Applicable From date<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ForeColor="red" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtEffective_Date" ErrorMessage="Enter Effective Date." Text="<i class='fa fa-exclamation-circle' title='Enter Effective Date !'></i>"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revdate" ValidationGroup="a" ForeColor="Red" runat="server" Display="Dynamic" ControlToValidate="txtEffective_Date" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                        </span>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>

                                            <asp:TextBox ID="txtEffective_Date" onkeypress="return false;" runat="server" placeholder="Select Applicable From date..." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-start-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Milk Type<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlMilk_category" InitialValue="0" ErrorMessage="Select Milk Category." Text="<i class='fa fa-exclamation-circle' title='Select Milk Category !'></i>"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlMilk_category" OnInit="ddlMilk_category_Init" runat="server" class="form-control" onchange="Validation();">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Cow</asp:ListItem>
                                            <asp:ListItem>Buffalo</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Fat%<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlMilk_category" InitialValue="0" ErrorMessage="Select Milk Category." Text="<i class='fa fa-exclamation-circle' title='Select Milk Category !'></i>"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="DropDownList1" OnInit="ddlMilk_category_Init" runat="server" class="form-control" onchange="Validation();">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>CLR</asp:ListItem>
                                            <asp:ListItem>SNF</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label2" Text="Temperature" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                ErrorMessage="Enter Rate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Rate !'></i>"
                                                ControlToValidate="txtRate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ValidationGroup="a"
                                                ErrorMessage="Enter Valid Rate !" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Rate !'></i>" ControlToValidate="txtRate"
                                                ValidationExpression="^[0-9]+$">
                                            </asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control  MobileNo" ID="TextBox2" MaxLength="11" onkeypress="return validateNum(event);" placeholder="Enter Temperature"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" Text="Milk Quality" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                ErrorMessage="Enter Rate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Rate !'></i>"
                                                ControlToValidate="txtRate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ValidationGroup="a"
                                                ErrorMessage="Enter Valid Rate !" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Rate !'></i>" ControlToValidate="txtRate"
                                                ValidationExpression="^[0-9]+$">
                                            </asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control  MobileNo" ID="TextBox1" MaxLength="11" onkeypress="return validateNum(event);" placeholder="Enter Milk Quality"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="lblRate" Text="Rate (Per/KG)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvRate" ValidationGroup="a"
                                                ErrorMessage="Enter Rate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Rate !'></i>"
                                                ControlToValidate="txtRate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revRate" runat="server" Display="Dynamic" ValidationGroup="a"
                                                ErrorMessage="Enter Valid Rate !" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Rate !'></i>" ControlToValidate="txtRate"
                                                ValidationExpression="^[0-9]+$">
                                            </asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control  MobileNo" ID="txtRate" MaxLength="11" onkeypress="return validateNum(event);" placeholder="Enter Rate"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSave" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-block btn-default" OnClick="btnClear_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.box-body -->

                <div class="box box-Manish">
                    <div class="box-header">
                        <h3 class="box-title">Milk Rate Master(For milk Collection) Details</h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:GridView ID="GridView1" PageSize="50" runat="server" OnRowCommand="GridView1_RowCommand" class="table table-hover table-bordered pagination-ys"
                                ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" DataKeyNames="MilkRateId">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Effective Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMilkRateId" Visible="false" runat="server" Text='<%# Eval("MilkRateId") %>' />
                                            <asp:Label ID="lblEffectiveDate" runat="server" Text='<%# Eval("EffectiveDate","{0:dd-MMM-yyyy}") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Milk Category">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMilkCategory" runat="server" Text='<%# Eval("MilkTypeName") %>' />
                                            <asp:Label ID="lblMilkType_id" Visible="false" runat="server" Text='<%# Eval("MilkType_id") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate (Per/KG)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate","{0:0.00}") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkView" CommandName="ViewRow" CommandArgument='<%#Eval("MilkRateId") %>' Style="color: gray;" runat="server" ToolTip="Click to View Milk Rate Detail's"><i class="fa fa-eye"></i></asp:LinkButton>
                                            <%--<asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("MilkRateId") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>--%>
                                            &nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("MilkRateId") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <%--Milk Details Modal Start --%>
                <div class="modal fade" id="ViewModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div style="display: table; height: 100%; width: 100%;">
                        <div class="modal-dialog" style="width: 60%; display: table-cell; vertical-align: middle;">
                            <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                                <div class="modal-header" style="background-color: #d9d9d9;">
                                    <button type="button" class="close" data-dismiss="modal">
                                        <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                                    </button>
                                    <h4 class="modal-title" id="myModalLabel2">Milk Rate Detail's</h4>
                                </div>
                                <div class="clearfix"></div>
                                <div class="modal-body">
                                    <%--<div class="row">
                                    <div class="col-md-12">
                                        <asp:Label ID="Label1" Text="Member ID [Name] : " runat="server"></asp:Label>
                                        <asp:Label ID="lblMemberId" Font-Bold="true" ForeColor="Green" runat="server"></asp:Label>
                                    </div>
                                </div>--%>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="gvPopup_ViewMilkRateDetails" ShowHeader="true" EmptyDataText="No Record Found for current selection." ShowFooter="false" FooterStyle-BackColor="#eaeaea" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover" runat="server">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Effective Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEffectiveDate" runat="server" Text='<%# Eval("EffectiveDate","{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Milk Category">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMilkTypeName" runat="server" Text='<%# Eval("MilkTypeName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Rate (Per/KG)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate","{0:0.00}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <div class="form-group">
                                                    <button type="button" class="btn btn-default pull-right" data-dismiss="modal">CLOSE </button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <!-- /.content -->

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
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
        function ViewDetailModal() {
            $('#ViewModal').modal('show');
            return false;
        }
    </script>
</asp:Content>

