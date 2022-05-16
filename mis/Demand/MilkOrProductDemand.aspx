<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MilkOrProductDemand.aspx.cs" Inherits="mis_Demand_MilkOrProductDemand" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <asp:ValidationSummary ID="vs1" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" ShowMessageBox="true" ShowSummary="false" />
                    <div class="box box-primary">
                        <div class="box-header">
                            <div class="row">
                                <div class="col-md-7">
                                    <div class="form-group">
                                        <h3 class="box-title">Demand Entry at Dugdh Sangh</h3>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlDS" runat="server" Width="100%" OnInit="ddlDS_Init" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group pull-right">
                                        <asp:LinkButton runat="server" ID="btnSummary" Width="100%" CssClass="btn btn-info" data-toggle="modal" Text="<i class='fa fa-eye'> View</i>" ToolTip="Click to View Summary" data-target="#myModalnew" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblMsg" CssClass="clr" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Date </label>
                                        <span style="color: red">*</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" runat="server" ValidationGroup="save" Display="Dynamic" ControlToValidate="txtDate" ForeColor="Red" ErrorMessage="Enter Date." Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" SetFocusOnError="true" ValidationGroup="save" runat="server" Display="Dynamic" ControlToValidate="txtDate" ForeColor="Red" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                        </span>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox runat="server" ID="txtDate" placeholder="Select Date" MaxLength="10" CssClass="form-control" onkeypress="javascript: return false;" autocomplete="off" onpaste="return false;" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-end-date="0d" data-date-autoclose="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Mode Of Request</label><span style="color: red"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ForeColor="Red" Display="Dynamic" ValidationGroup="save" runat="server" ControlToValidate="ddlModeOfRequest" InitialValue="0" ErrorMessage="Select Mode Of Request." Text="<i class='fa fa-exclamation-circle' title='Select Mode Of Request !'></i>"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlModeOfRequest" runat="server" Width="100%" OnInit="ddlModeOfRequest_Init" CssClass="form-control select2">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Shift</label><span style="color: red"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ForeColor="Red" Display="Dynamic" ValidationGroup="save" runat="server" ControlToValidate="ddlshift" InitialValue="0" ErrorMessage="Select Shift." Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlshift" Width="100%" OnInit="ddlshift_Init" runat="server" CssClass="form-control select2">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>Enter Code<span style="color: red"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" SetFocusOnError="true" ValidationGroup="save" Display="Dynamic" ControlToValidate="txtCode" ForeColor="Red" ErrorMessage="Enter Distributor/Sub Distributor/Booth Code" Text="<i class='fa fa-exclamation-circle' title='Enter Distributor/Sub Distributor/Booth Code !'></i>"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtCode" CssClass="form-control" autocomplete="off" placeholder="X0000" ToolTip="Enter Distributor / Sub-Distributor/ Booth Code" runat="server" onkeypress="return validateusername(event)" AutoPostBack="true" OnTextChanged="txtCode_TextChanged"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            <asp:Label ID="lblDistributor" runat="server" Text="Distributor"></asp:Label>
                                            Name<asp:HiddenField ID="hfUserType_id" runat="server" />
                                        </label>
                                        <%--                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" SetFocusOnError="true" ValidationGroup="save"
                                                ErrorMessage="Select Distributor Name" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Product Category !'></i>"
                                                ControlToValidate="ddlDistributorName" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>--%>
                                        <asp:DropDownList ID="ddlDistributorName" Enabled="false" CssClass="form-control" runat="server">
                                            <asp:ListItem Text="-" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Route No.</label>
                                        <%-- <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" SetFocusOnError="true" ValidationGroup="save"
                                                ErrorMessage="Select Route No." InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Route No. !'></i>"
                                                ControlToValidate="ddlRouteNo" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>--%>
                                        <asp:DropDownList ID="ddlRouteNo" Enabled="false" CssClass="form-control" runat="server">
                                            <asp:ListItem Text="-" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <fieldset>
                                <legend>Product Detail</legend>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label>Product Category<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" ValidationGroup="a"
                                                    ErrorMessage="Select Product Category" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Product Category !'></i>"
                                                    ControlToValidate="ddlProductCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlProductCategory" Width="100%" OnInit="ddlProductCategory_Init" OnSelectedIndexChanged="ddlProductCategory_SelectedIndexChanged" CssClass="form-control select2" AutoPostBack="true" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label>Product Variant<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                    ErrorMessage="Select Product Variant" SetFocusOnError="true" Text="<i class='fa fa-exclamation-circle' title='Select Product Variant !'></i>"
                                                    ControlToValidate="ddlProductVariant" InitialValue="0" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlProductVariant" Width="100%" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label>Quantity<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter Quanity" SetFocusOnError="true" Text="<i class='fa fa-exclamation-circle' title='Enter Quanity !'></i>"
                                                    ControlToValidate="txtQuantity" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" SetFocusOnError="true" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtQuantity"
                                                    ErrorMessage="Only Numeric allow" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Only Numeric allow. !'></i>"
                                                    ValidationExpression="^[0-9]+$">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox ID="txtQuantity" onpaste="return false;" onkeypress="return validateNum(event);" Width="100%" placeholder="Enter Quantity" CssClass="form-control" MaxLength="5" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <asp:Button runat="server" ID="btnAdd" Style="margin-top: 25px;" ValidationGroup="a" OnClick="btnAdd_Click" CssClass="btn btn-block btn-primary" Text="Add" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView ID="GridView1" PageSize="50" runat="server" class="table table-hover table-bordered" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="15">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Product Category">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCat_id" runat="server" Visible="false" Text='<%# Bind("Cat_id") %>'></asp:Label>
                                                        <asp:Label ID="lblProductCategory" runat="server" Text='<%# Bind("CatName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Product Variant">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVariantId" runat="server" Visible="false" Text='<%# Bind("VariantId") %>'></asp:Label>
                                                        <asp:Label ID="lblProductVariant" runat="server" Text='<%# Bind("VariantName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("NoofPacekts") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductRate" runat="server" Text='<%# Bind("MRPRate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalAmount" runat="server" Text='<%# Eval("TotalValue") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClick="lnkDelete_Click" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </fieldset>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Remark<span style="color: red"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ForeColor="Red" Display="Dynamic" ValidationGroup="save" runat="server" ControlToValidate="txtRemark" ErrorMessage="Required to fill Remark." Text="<i class='fa fa-exclamation-circle' title='Required to fill Remark. !'></i>"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtRemark" TextMode="MultiLine" MaxLength="140" CssClass="form-control" placeholder="Enter Remark if any..." runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSubmit" Width="100%" ValidationGroup="save" CssClass="btn btn-primary" runat="server" Text="Save" OnClientClick="return ValidatePage();" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnClear" Width="100%" CssClass="btn btn-default" runat="server" Text="Clear" OnClick="btnClear_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Details of Product/Milk Demand</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gv_DemandDetails" ShowHeader="true" DataKeyNames="PUDemandId" FooterStyle-BackColor="#eaeaea" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover" runat="server" OnRowCommand="gv_DemandDetails_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Demand Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPUDemandId" runat="server" Visible="false" Text='<%# Eval("PUDemandId") %>'></asp:Label>
                                                <asp:Label ID="lblDemandDate" runat="server" Text='<%# Eval("DemandDate","{0:dd-MMM-yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mode Of Demand">
                                            <ItemTemplate>
                                                <asp:Label ID="lblModeOfDemand" runat="server" Text='<%# Eval("ModeName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Demand From">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDPersonName" runat="server" Text='<%# Eval("DPersonName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Shift">
                                            <ItemTemplate>
                                                <asp:Label ID="lblShiftName" runat="server" Text='<%# Eval("ShiftName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Amount (In Rs.)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalDemandValue" runat="server" Text='<%# Eval("TotalDemandValue") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkView" CommandName="ViewRow" CommandArgument='<%#Eval("PUDemandId") %>' Style="color: gray;" runat="server" ToolTip="Click to View Product Detail's"><i class="fa fa-eye"></i></asp:LinkButton>
                                                &nbsp;<asp:LinkButton ID="lnkDelete" runat="server" ToolTip="Delete" CommandName="DeleteRow" CommandArgument='<%# Eval("PUDemandId") %>' Style="color: red;" OnClientClick="return confirm('Are you sure to Delete this record?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="myModalnew" tabindex="-1" role="dialog" aria-labelledby="myModalLabel1" aria-hidden="true">
                <div style="display: table; height: 100%; width: 100%;">
                    <div class="modal-dialog" style="width: 40%; display: table-cell; vertical-align: middle;">
                        <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                            <div class="modal-header" style="background-color: #d9d9d9;">
                                <button type="button" class="close" data-dismiss="modal">
                                    <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                                </button>
                                <h4 class="modal-title" id="myModalLabel3">Shift-Wise Demand Summary Report</h4>
                            </div>
                            <div class="clearfix"></div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <table class="datatable table table-striped table-bordered table-hover" cellspacing="0" rules="all" border="1" id="ctl00_ContentBody_gvOpeningStock" style="border-collapse: collapse;">
                                                <tbody>
                                                    <tr>
                                                        <td>Total Milk Demand (No. of Ltr)</td>
                                                        <td>5000</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Total Milk Demand (No. of Packets)</td>
                                                        <td>15000</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Total Product Demand (in gms)</td>
                                                        <td>8500</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Total Product Demand (No. of Packets)</td>
                                                        <td>25000</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Total Demand (Value)</td>
                                                        <td>285515</td>
                                                    </tr>
                                                </tbody>

                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-default" Text="Close" data-dismiss="modal" Style="width: 50px;" />
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
            </div>
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
                                    <img src="../image/question-circle.png" width="30" />&nbsp;&nbsp; 
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                                </p>
                            </div>
                            <div class="modal-footer">
                                <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnYes_Click" Style="margin-top: 20px; width: 50px;" />
                                <asp:Button ID="Button2" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>

                </div>
            </div>
            <%--ConfirmationModal End --%>

            <%--Confirmation Modal Start --%>
            <div class="modal fade" id="ViewModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div style="display: table; height: 100%; width: 100%;">
                    <div class="modal-dialog" style="width: 60%; display: table-cell; vertical-align: middle;">
                        <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                            <div class="modal-header" style="background-color: #d9d9d9;">
                                <button type="button" class="close" data-dismiss="modal">
                                    <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                                </button>
                                <h4 class="modal-title" id="myModalLabel2">Product Detail's</h4>
                            </div>
                            <div class="clearfix"></div>
                            <div class="modal-body" style="height: 420px; overflow: scroll;">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Label ID="lblUserType" Text="Distributor" runat="server"></asp:Label>
                                        Name :
                                        <asp:Label ID="lblUserId" Font-Bold="true" ForeColor="Green" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gv_popup_ProductDetails" runat="server" ShowFooter="true" class="table table-hover table-bordered" AutoGenerateColumns="False" OnRowDataBound="gv_popup_ProductDetails_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="15">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Product Category">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProductCategory" runat="server" Text='<%# Bind("Category_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Product Variant">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProductVariant" runat="server" Text='<%# Bind("VName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblGrandTotalText" Text="Grand Total" Font-Bold="true" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quantity">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("NoofPacekts") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalQuantity" runat="server" Font-Bold="true" Text="0.00"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rate">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProductRate" runat="server" Text='<%# Bind("MRPRate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Amount (In Rs.)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTotalAmount" runat="server" Text='<%# Eval("TotalValue") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblGrandTotalAmount" runat="server" Font-Bold="true" Text="0"></asp:Label>
                                                            </FooterTemplate>
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
                                            <label>Remark</label>
                                            <asp:TextBox ID="lblRemark" Enabled="false" TextMode="MultiLine" Columns="2" MaxLength="140" CssClass="form-control" placeholder="Enter Remark if any..." runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="form-group">
                                                <button type="button" class="btn btn-default pull-right" data-dismiss="modal">Close </button>
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function ValidatePage() {
            if (typeof (Page_ClientValidate)) {
                Page_ClientValidate('save');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }

                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
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

