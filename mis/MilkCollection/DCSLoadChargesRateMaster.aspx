<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DCSLoadChargesRateMaster.aspx.cs" Inherits="mis_MilkCollection_DCSLoadChargesRateMaster" %>

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
                        <asp:Button runat="server" CssClass="btn btn-success" OnClick="btnSave_Click" Text="Yes" ID="btnYes" Style="margin-top: 20px; width: 50px;" />
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
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Head Load Charges Rate Master</h3>
                        </div>
                        <asp:Label ID="lblMsg" Text="" runat="server"></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>Filter
                                </legend>
                                <div class="row">
                                    <div class="col-md-2">
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
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Effective Date<span style="color: red">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="far fa-calendar-alt"></i>
                                                    </span>
                                                </div>
                                                <asp:TextBox ID="txtDate" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Milk Quanity Range<span style="color: red">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                                    ControlToValidate="ddlMilkQuantity" InitialValue="0"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Milk Quanity Range!'></i>"
                                                    ErrorMessage="Select Milk Quanity Range." SetFocusOnError="true" ForeColor="Red"
                                                    ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlMilkQuantity" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="25-50">25-50</asp:ListItem>
                                                <asp:ListItem Value="51-100">51-100</asp:ListItem>
                                                <asp:ListItem Value="101-200">101-200</asp:ListItem>
                                                <asp:ListItem Value="201-300">201-300</asp:ListItem>
                                                <asp:ListItem Value="301-400">301-400</asp:ListItem>
                                                <asp:ListItem Value="Above 401">Above 401</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <fieldset>
                                    <legend>Add Distance</legend>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Distance In Km<span style="color: red">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                                                        ControlToValidate="ddlDistanceInKm" InitialValue="0"
                                                        Text="<i class='fa fa-exclamation-circle' title='Select Distance In Km!'></i>"
                                                        ErrorMessage="Select Distance In Km." SetFocusOnError="true" ForeColor="Red"
                                                        ValidationGroup="a"></asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlDistanceInKm" runat="server" CssClass="form-control select2">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="16">16 कि.मी प्रतिदिन तक(4 कि.मी एक तरफ से) </asp:ListItem>
                                                    <asp:ListItem Value="16.1-28">16.1 से 28 कि.मी प्रतिदिन तक(4.1 से 7 कि.मी एक तरफ से)</asp:ListItem>
                                                    <asp:ListItem Value="28.1-40">28.1 से 40 कि.मी प्रतिदिन तक(7.1 से 10 कि.मी एक तरफ से)</asp:ListItem>
                                                    <asp:ListItem Value="Above 40.1">40.1 कि.मी प्रतिदिन तक(10.1 कि.मी एक तरफ से)</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Rate<span style="color: red">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"
                                                        ControlToValidate="txtRate"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter Rate!'></i>"
                                                        ErrorMessage="Enter Rate." SetFocusOnError="true" ForeColor="Red"
                                                        ValidationGroup="a"></asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox ID="txtRate" autocomplete="off" MaxLength="20" onkeypress="return validateDec(this,event);" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Button ID="btnAdd" runat="server" Style="margin-top: 21px;" ValidationGroup="a" CssClass="btn btn-primary" Text="Add" OnClick="btnAdd_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvDetail" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="gvDetail_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Distance In Km">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDist" runat="server" Text='<%# Eval("DistanceKmRange")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Maximum" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaximum" runat="server" Text='<%# Eval("MaxDistance")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Minimum" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMinimum" runat="server" Text='<%# Eval("MinDistance")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rate">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteRecord" CausesValidation="false" CommandArgument='<%# Eval("DistanceKmRange")%>'><i class="fa fa-trash"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button ID="btnSave" runat="server" Style="margin-top: 21px;" CssClass="btn btn-primary" ValidationGroup="Save" Text="Save" OnClientClick="return ValidatePage();" OnClick="btnSave_Click" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Head Load Charges Rate Details</legend>
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvReports" CssClass="table table-bordered" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnRowCommand="gvReports_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                        <asp:Label ID="lblHeadLoadCharges_ID" CssClass="hidden" runat="server" Text='<%# Eval("HeadLoadCharges_ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Effective Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEffectiveDate" runat="server" Text='<%# Eval("EffectiveDate") %>'></asp:Label>
                                                        <span class="pull-right">
                                                            <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Update"></asp:RequiredFieldValidator>
                                                        </span>
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <i class="far fa-calendar-alt"></i>
                                                                </span>
                                                            </div>
                                                            <asp:TextBox ID="txtDate" autocomplete="off" Visible="false" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Milk Quality Range">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMilkQualityRange" runat="server" Text='<%# Eval("MilkQualityRange") %>'></asp:Label>
                                                        <span class="pull-right">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                                                ControlToValidate="ddlMilkQuantity" InitialValue="0"
                                                                Text="<i class='fa fa-exclamation-circle' title='Select Milk Quanity Range!'></i>"
                                                                ErrorMessage="Select Milk Quanity Range." SetFocusOnError="true" ForeColor="Red"
                                                                ValidationGroup="Update"></asp:RequiredFieldValidator>
                                                        </span>
                                                        <asp:DropDownList ID="ddlMilkQuantity" Visible="false" runat="server" CssClass="form-control select2">
                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                            <asp:ListItem Value="25-50">25-50</asp:ListItem>
                                                            <asp:ListItem Value="51-100">51-100</asp:ListItem>
                                                            <asp:ListItem Value="101-200">101-200</asp:ListItem>
                                                            <asp:ListItem Value="201-300">201-300</asp:ListItem>
                                                            <asp:ListItem Value="301-400">301-400</asp:ListItem>
                                                            <asp:ListItem Value="Above 401">Above 401</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Distance Range">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDistanceKmRange" runat="server" Text='<%# Eval("DistanceKmRange") %>'></asp:Label>
                                                        <span class="pull-right">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                                                                ControlToValidate="ddlDistanceInKm" InitialValue="0"
                                                                Text="<i class='fa fa-exclamation-circle' title='Select Distance In Km!'></i>"
                                                                ErrorMessage="Select Distance In Km." SetFocusOnError="true" ForeColor="Red"
                                                                ValidationGroup="Update"></asp:RequiredFieldValidator>
                                                        </span>
                                                        <asp:DropDownList ID="ddlDistanceInKm" Visible="false" runat="server" CssClass="form-control select2">
                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                            <asp:ListItem Value="16">16</asp:ListItem>
                                                            <asp:ListItem Value="16.1-28">16.1-28</asp:ListItem>
                                                            <asp:ListItem Value="28.1-40">28.1-40</asp:ListItem>
                                                            <asp:ListItem Value="Above 40.1">Above 40.1</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate") %>'></asp:Label>
                                                        <span class="pull-right">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"
                                                                ControlToValidate="txtRate"
                                                                Text="<i class='fa fa-exclamation-circle' title='Enter Rate!'></i>"
                                                                ErrorMessage="Enter Rate." SetFocusOnError="true" ForeColor="Red"
                                                                ValidationGroup="Update"></asp:RequiredFieldValidator>
                                                        </span>
                                                        <asp:TextBox ID="txtRate" Visible="false" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditRecord" CausesValidation="false" CommandArgument='<%# Container.DataItemIndex %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkUpdate" ValidationGroup="Update" runat="server" CommandName="UpdateRecord" Visible="false" CausesValidation="false" CommandArgument='<%# Eval("HeadLoadChargesChild_ID") %>'>Update</asp:LinkButton>
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
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('Save');
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

    </script>
</asp:Content>

