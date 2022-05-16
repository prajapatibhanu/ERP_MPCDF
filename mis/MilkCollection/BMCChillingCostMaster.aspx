<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="BMCChillingCostMaster.aspx.cs" Inherits="mis_MilkCollection_BMCChillingCostMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
      <style>
        .customCSS td {
            padding: 0px !important;
        }

        table {
            white-space: nowrap;
        }
    </style>
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" Style="margin-top: 20px; width: 50px;" OnClick="btnSave_Click"/>
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class=" box box-success">
                        <div class="box-header">
                            <h3 class="box-title">BMC Chilling Cost Master</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>Filter</legend>
                                <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Effective Date</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <i class="far fa-calendar-alt"></i>
                                                </span>
                                            </div>
                                            <asp:TextBox ID="txtDate" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server" OnTextChanged="txtDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                    <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Temp<span style="color: red">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Save"
                                                InitialValue="0" ErrorMessage="Select Temp" Text="<i class='fa fa-exclamation-circle' title='Select Temp !'></i>"
                                                ControlToValidate="ddlTemp" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlTemp" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlTemp_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="0-4" Selected="True">0-4 °C</asp:ListItem>
                                            <asp:ListItem Value="Above 4">Above 4 °C</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                    <div class="col-md-2">
                                    <label>Milk Collection Unit<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ValidationGroup="Save" ControlToValidate="ddlMilkCollectionUnit" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Milk Collection Unit!'></i>" ErrorMessage="Select Milk Collection Unit" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlMilkCollectionUnit" AutoPostBack="true" OnSelectedIndexChanged="ddlMilkCollectionUnit_SelectedIndexChanged" CssClass="form-control select2" runat="server">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="5">BMC</asp:ListItem>
                                            <asp:ListItem Value="6">DCS</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Society<span style="color: red">*</span></label>
                                      <%--  <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvBMC" ValidationGroup="Save"
                                                InitialValue="0" ErrorMessage="Select BMC" Text="<i class='fa fa-exclamation-circle' title='Select BMC !'></i>"
                                                ControlToValidate="ddLBMC" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>--%>
                                        <%--<asp:DropDownList ID="ddLBMC" runat="server" CssClass="form-control select2">
                                        </asp:DropDownList>--%>
                                        <asp:ListBox runat="server" ID="ddLBMC" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                    </div>
                                </div>
                                
                                <%--<div class="col-md-2">
                                        <div class="form-group">
                                            <label>In Paise Or Rupees<span style="color: red">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Save"
                                                    InitialValue="0" ErrorMessage="Select Paise Or Rupees" Text="<i class='fa fa-exclamation-circle' title='Select Paise Or Rupees !'></i>"
                                                    ControlToValidate="ddlPaiseorRupees" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlPaiseorRupees" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                            <asp:ListItem Value="Paise">Paise</asp:ListItem>
                                                <asp:ListItem Value="Rupees">Rupees</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>--%>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Chilling Cost(In Paise)<span style="color: red">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Save"
                                                ErrorMessage="Enter Chilling Cost" Text="<i class='fa fa-exclamation-circle' title='Enter Chilling Cost !'></i>"
                                                ControlToValidate="txtChillingCost" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:TextBox ID="txtChillingCost" autocomplete="off" MaxLength="20" onkeypress="return validateDec(this,event);" runat="server" CssClass="form-control">                                            
                                        </asp:TextBox>
                                    </div>
                                </div>
                                    </div>
                                <%--<div class="row">
                                    <div class="col-md-12">
                                        <fieldset>
                                            <legend>
                                                <asp:CheckBox ID="chkSocietyAll" runat="server" Text="Society" onclick="CheckAllSociety();" /></legend>
                                            <div class="table-responsive">
                                                <asp:CheckBoxList ID="chkSociety" runat="server" ClientIDMode="Static" CssClass="table District customCSS cbl_all_Office" RepeatColumns="6" RepeatDirection="Horizontal" onclick="CheckUncheckOfficeAllDistrict();">
                                                </asp:CheckBoxList>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>--%>
                                <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" Style="margin-top: 21px;" runat="server" Text="Save" CssClass="btn btn-success" ValidationGroup="Save" OnClick="btnSave_Click" OnClientClick="return ValidatePage();"/>
                                    </div>
                                </div>
                            </div>
                            </fieldset>
                            
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>BMC Chilling Cost Details</legend>
                                <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gvReport" runat="server" EmptyDataText="No Record Found" CssClass="table table-bordered" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.no">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# Container.DataItemIndex +1  %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Effective Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEffectiveDate" runat="server" Text='<%# (Convert.ToDateTime(Eval("EffectiveDate"))).ToString("dd-MM-yyyy hh:mm:ss tt") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Society">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSociety" runat="server" Text='<%# Eval("Society")  %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                          
                                            <asp:TemplateField HeaderText="Temp">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTemp" runat="server" Text='<%# Eval("Temp")  %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ChillingCost(In Paise)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblChillingCost" runat="server" Text='<%# Eval("ChillingCost")  %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          <%--  <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="EditRecord" CausesValidation="false" CommandArgument='<%# Eval("ChillingCost_Id") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
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
    <script type="text/javascript">
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('Save');
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
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../js/bootstrap-multiselect.js"></script>
    <script>

        $(function () {
            $('[id*=ddLBMC]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',
            });
        });
    </script>
</asp:Content>

