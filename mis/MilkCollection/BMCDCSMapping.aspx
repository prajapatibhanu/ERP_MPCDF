<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="BMCDCSMapping.aspx.cs" Inherits="mis_MilkCollection_BMCDCSMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
     <style>
        @media print {
             
              .noprint {
                display: none;
            }
               
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnYes_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="Save" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary noprint">
                        <div class="box-header">
                            <h3 class="box-title">BMC DCS Mapping</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>BMC</legend>
                                <div class="row">
								 <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Milk Collection Unit<span style="color: red">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvMCU" ValidationGroup="Save"
                                                    InitialValue="0" ErrorMessage="Select BMC" Text="<i class='fa fa-exclamation-circle' title='Select BMC !'></i>"
                                                    ControlToValidate="ddlMCU" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlMCU" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlMCU_SelectedIndexChanged" AutoPostBack="true">
                                           <asp:ListItem Value="0">Select</asp:ListItem>
										   <asp:ListItem Value="4">CC</asp:ListItem>
                                           <asp:ListItem Value="5" Selected="True">BMC</asp:ListItem>
										   <asp:ListItem Value="6">DCS</asp:ListItem>
                                           
                                                 </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>BMC/DCS/CC<span style="color: red">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvBMC" ValidationGroup="Save"
                                                    InitialValue="0" ErrorMessage="Select BMC" Text="<i class='fa fa-exclamation-circle' title='Select BMC !'></i>"
                                                    ControlToValidate="ddLBMC" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddLBMC" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddLBMC_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>
                                                Milk Supply to<span style="color: red">*</span></label>

                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvMilkSupplyTo" ValidationGroup="Save"
                                                    InitialValue="0" ErrorMessage="Select Milk Supply to" Text="<i class='fa fa-exclamation-circle' title='Select Milk Supply to !'></i>"
                                                    ControlToValidate="ddlMilkSupply" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlMilkSupply" OnSelectedIndexChanged="ddlMilkSupply_SelectedIndexChanged" Width="100%" AutoPostBack="true" CssClass="form-control select2" runat="server">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Plant</asp:ListItem>
                                                <asp:ListItem Value="2">CC</asp:ListItem>
                                                <asp:ListItem Value="3">MDP</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>
                                                Supply Unit<span style="color: red">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvSupplyUnit" ValidationGroup="Save"
                                                    InitialValue="0" ErrorMessage="Select Supply Unit" Text="<i class='fa fa-exclamation-circle' title='Select Supply Unit !'></i>"
                                                    ControlToValidate="ddlSupplyUnit" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlSupplyUnit" Width="100%" CssClass="form-control select2" runat="server">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>
                                                Society Code</label>

                                            <span class="pull-right">
                                                <%-- <asp:RegularExpressionValidator ID="revBMSSocCode" runat="server" Display="Dynamic" ControlToValidate="txtBMCSocietyCode" ValidationExpression="^[a-zA-Z0-9]{1,40}$" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>--%>
                                               <%-- <asp:RequiredFieldValidator ID="rfvBMSSocCode" runat="server" Display="Dynamic" ControlToValidate="txtBMCSocietyCode" Text="<i class='fa fa-exclamation-circle' title='Enter Society Code.!'></i>" ErrorMessage="Enter Society Code." SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                                            </span>
                                            <asp:TextBox ID="txtBMCSocietyCode" Width="100%" CssClass="form-control" runat="server" MaxLength="10" placeholder="Enter Society Code" autocomplete="off" onkeypress="return validateNum(event)">
                                            </asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>
                                                BMC Name In English</label>

                                            <span class="pull-right">
                                                <%-- <asp:RegularExpressionValidator ID="revBMSSocCode" runat="server" Display="Dynamic" ControlToValidate="txtBMCSocietyCode" ValidationExpression="^[a-zA-Z0-9]{1,40}$" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>--%>
                                                <%-- <asp:RequiredFieldValidator ID="rfvBMCIn_E" runat="server" Display="Dynamic" ControlToValidate="txtBMCIn_E" Text="<i class='fa fa-exclamation-circle' title='Enter BMC Name In English.!'></i>" ErrorMessage="Enter BMC Name In English." SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                                            </span>
                                            <asp:TextBox ID="txtBMCIn_E" Width="100%" CssClass="form-control" runat="server" MaxLength="200" autocomplete="off" placeholder="DCS Name In English">
                                            </asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>
                                                Center<span style="color: red">*</span></label>

                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Save"
                                                    InitialValue="0" ErrorMessage="Select Center" Text="<i class='fa fa-exclamation-circle' title='Select Center !'></i>"
                                                    ControlToValidate="ddlCenter" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlCenter" Width="100%" CssClass="form-control select2" runat="server" OnSelectedIndexChanged="ddlCenter_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">With Center</asp:ListItem>
                                                <asp:ListItem Value="2">Without Center</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                </div>
                            </fieldset>
                            <asp:Panel ID="dcsPanel" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-md-12">
                                        <fieldset>
                                            <legend>Map DCS</legend>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>DCS<span style="color: red">*</span></label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="rfvDCS" ValidationGroup="a"
                                                            InitialValue="0" ErrorMessage="Select DCS" Text="<i class='fa fa-exclamation-circle' title='Select DCS !'></i>"
                                                            ControlToValidate="ddlDCS" ForeColor="Red" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator></span>
                                                    <asp:DropDownList ID="ddlDCS" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlDCS_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>
                                                        Society Code<span style="color: red">*</span></label>

                                                    <span class="pull-right">
                                                        <%-- <asp:RegularExpressionValidator ID="rfvDCSSocCodeexp" runat="server" Display="Dynamic" ControlToValidate="txtDCSSocietyCode" ValidationExpression="^[0-9]" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>--%>
                                                        <asp:RequiredFieldValidator ID="rfvDCSSocCode" runat="server" Display="Dynamic" ControlToValidate="txtDCSSocietyCode" Text="<i class='fa fa-exclamation-circle' title='Enter Sample No.!'></i>" ErrorMessage="Enter Sample No." SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                    </span>
                                                    <asp:TextBox ID="txtDCSSocietyCode" Width="100%" CssClass="form-control" runat="server" placeholder="Enter Society Code" MaxLength="10" autocomplete="off" onkeypress="return validateNum(event)">
                                                    </asp:TextBox>
                                                </div>

                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>
                                                        DCS Name In English<span style="color: red">*</span></label>

                                                    <span class="pull-right">
                                                        <%-- <asp:RegularExpressionValidator ID="revBMSSocCode" runat="server" Display="Dynamic" ControlToValidate="txtBMCSocietyCode" ValidationExpression="^[a-zA-Z0-9]{1,40}$" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>--%>
                                                        <asp:RequiredFieldValidator ID="rfvDCSIn_E" runat="server" Display="Dynamic" ControlToValidate="txtDCSIn_E" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Name In English.!'></i>" ErrorMessage="Enter DCS Name In English." SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                    </span>
                                                    <asp:TextBox ID="txtDCSIn_E" Width="100%" CssClass="form-control" runat="server" placeholder="Enter DCS Name In English" MaxLength="200" autocomplete="off">
                                                    </asp:TextBox>
                                                </div>

                                            </div>
                                            <div class="col-md-2">
                                                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="a" Style="margin-top: 20px;" OnClick="btnAdd_Click" />
                                            </div>
                                            <div class="col-md-12">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="gvDCSDetails" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="DCS">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDCSName" runat="server" Text='<%# Eval("DCSName") %>'></asp:Label>
                                                                    <asp:Label ID="lblDCSID" CssClass="hidden" runat="server" Text='<%# Eval("DCSID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="DCS Name in English">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDCSName_E" runat="server" Text='<%# Eval("DCSName_E") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Society Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSocietyCode" runat="server" Text='<%# Eval("SocietyCode") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" ToolTip="DeleteDCS" OnClick="lnkDelete_Click" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete this record?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="row" runat="server">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="Save" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                                        <a class="btn btn-default" href="BMCDCSMapping.aspx">Clear</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">B.M.C Code List</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-3 pull-left noprint">
                            <div class="form-group">
                                <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn btn-default" Text="Excel" OnClick="btnExport_Click"/>
                                <asp:Button ID="btnPrint" runat="server" Visible="false" CssClass="btn btn-default" Text="Print" OnClientClick="window.print();"/>
                            </div>
                                
                            </div>
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvbmccodedetails" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnDataBound="gvbmccodedetails_DataBound">
                                        <Columns>
                                          <%--  <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRow" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                             <asp:BoundField HeaderText="Route" DataField="BMCTankerRootName"/>
                                            <asp:BoundField HeaderText="BMC" DataField="BMC"/>
                                           
                                             <asp:BoundField HeaderText="BMC/DCS" DataField="BMCDCS"/>
											
                                            <%-- <asp:BoundField HeaderText="SocietyCode" DataField="SocietyCode"/>--%>
                                           
                                            
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>
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

