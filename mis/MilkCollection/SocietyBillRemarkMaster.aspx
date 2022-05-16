<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SocietyBillRemarkMaster.aspx.cs" Inherits="mis_MilkCollection_SocietyBillRemarkMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
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
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Society Bill Remark Master</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>Society Bill Remark Master</legend>
                                <div class="row">
                                      
                                    <div class="col-md-2">
                                <div class="form-group">
                                    <label>CC<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select CC" Text="<i class='fa fa-exclamation-circle' title='Select CC !'></i>"
                                            ControlToValidate="ddlccbmcdetail" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlccbmcdetail"  runat="server" CssClass="form-control select2" ClientIDMode="Static" OnSelectedIndexChanged="ddlccbmcdetail_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                                 
                                <div class="col-md-2">
                                    <label>Society<span style="color: red;">*</span></label>
                                      <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Save"
                                                    InitialValue="0" ErrorMessage="Select Society"  Text="<i class='fa fa-exclamation-circle' title='Select Society !'></i>"
                                                    ControlToValidate="ddlSociety" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                    <div class="form-group">
                                      
                                        <asp:DropDownList ID="ddlSociety" CssClass="form-control select2" runat="server">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                    
                                     
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Billing Cycle</label>
                                            <asp:DropDownList ID="ddlBillingCycle" ClientIDMode="Static" runat="server" CssClass="form-control select2" OnTextChanged="ddlBillingCycle_TextChanged" AutoPostBack="true">
                                                <asp:ListItem Value="5 days">5 days</asp:ListItem>
                                                <asp:ListItem Value="10 days">10 days</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Date<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="far fa-calendar-alt"></i>
                                                    </span>
                                                </div>
                                                <asp:TextBox ID="txtDate" ClientIDMode="Static" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server" OnTextChanged="txtDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Billing Cycle From Date<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic" ControlToValidate="txtBillingCycleToDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="far fa-calendar-alt"></i>
                                                    </span>
                                                </div>
                                                <asp:TextBox ID="txtBillingCycleFromDate" ClientIDMode="Static" Enabled="false" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Billing Cycle To Date<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="Dynamic" ControlToValidate="txtBillingCycleToDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="far fa-calendar-alt"></i>
                                                    </span>
                                                </div>
                                                <asp:TextBox ID="txtBillingCycleToDate" ClientIDMode="Static" Enabled="false" autocomplete="off" CssClass="form-control DateAdd" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-10">
                                        <div class="form-group">
                                            <label>Remark<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvtxtRemark"  runat="server" Display="Dynamic" ControlToValidate="txtRemark" Text="<i class='fa fa-exclamation-circle' title='Enter Remark!'></i>" ErrorMessage="Enter Remark" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox ID="txtRemark" TextMode="MultiLine" Columns="2" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                      <div class="col-md-2" style="margin-top: 20px;" runat="server">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-primary" OnClientClick="return ValidatePage();" OnClick="btnSubmit_Click" ValidationGroup="Save" ID="btnSubmit" Text="Save" AccessKey="S" />
                                    </div>
                            </div>
                                </div>
                            </fieldset>

                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Society Bill Remark Detail</legend>
                                <div class="row">
                                   <div class="col-md-2">
                                <div class="form-group">
                                    <label>CC<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select CC" Text="<i class='fa fa-exclamation-circle' title='Select CC !'></i>"
                                            ControlToValidate="ddlccbmcdetail_flt" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlccbmcdetail_flt"  runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                                </div>
                                <div class="row">

                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <%--<asp:GridView ID="gvDetail" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnRowCommand="gvDetail_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRow" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Effective Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEffectiveDate" runat="server" Text='<%# Eval("EffectiveDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CC">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCC" runat="server" Text='<%# Eval("CC") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Society">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                            <asp:Label ID="lblOffice_ID" Visible="false" runat="server" Text='<%# Eval("Office_ID") %>'></asp:Label>
                                                            <asp:Label ID="lblOfficeType_ID" Visible="false" runat="server" Text='<%# Eval("OfficeType_ID") %>'></asp:Label>
                                                             <asp:Label ID="lblCC_ID" Visible="false" runat="server" Text='<%# Eval("CC_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Rate Per Kg">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRateperkg" runat="server" Text='<%# Eval("Rateperkg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditRecord" CommandArgument='<%# Eval("AdditionalCharges_ID") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>--%>
                                        </div>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
     <script type="text/javascript">
         function ValidatePage() {

             if (typeof (Page_ClientValidate) == 'function') {
                 Page_ClientValidate('Save');
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

