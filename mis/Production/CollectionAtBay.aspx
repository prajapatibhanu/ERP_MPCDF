<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CollectionAtBay.aspx.cs" Inherits="mis_Production_CollectionAtBay" %>

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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" Style="margin-top: 20px; width: 50px;" />
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
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Collection (at Bay)</h3>
                            <span id="ctl00_ContentBody_lblmsg"></span>
                        </div>
                        <div class="box-body">
                              <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblMsg" CssClass="clr" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Date<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="red" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtDate" ErrorMessage="Enter Date." Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" ForeColor="Red" runat="server" Display="Dynamic" ControlToValidate="txtDate" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtDate" onkeypress="return false;" runat="server" placeholder="dd/mm/yyyy" class="form-control" data-date-end-date="+1d" autocomplete="off" data-date-format="dd/mm/yyyy" data-date-autoclose="true" data-provide="datepicker" data-date-start-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Shift<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvShift" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlShift" InitialValue="0" ErrorMessage="Select Shift." Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlshift" runat="server" CssClass="form-control select2">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Morning</asp:ListItem>
                                            <asp:ListItem>Evening</asp:ListItem>
                                            <asp:ListItem>Special</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Centre Name (CC/BMC/MDP)<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvCentreName" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlCentreName" InitialValue="0" ErrorMessage="Select Centre Name (CC/BMC/MDP)." Text="<i class='fa fa-exclamation-circle' title='Select Centre Name (CC/BMC/MDP) !'></i>">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlCentreName" runat="server" CssClass="form-control select2">
                                           <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>CC</asp:ListItem>
                                            <asp:ListItem>BMC</asp:ListItem>
                                            <asp:ListItem>MDP</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                             <div class="form-group">
                                        <label>Route No.<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvRouteNo" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlRouteNo" InitialValue="0" ErrorMessage="Select Route No." Text="<i class='fa fa-exclamation-circle' title='Select Route No !'></i>">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlRouteNo" runat="server" CssClass="form-control select2">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>R1</asp:ListItem>
                                            <asp:ListItem>R2</asp:ListItem>
                                            <asp:ListItem>R3</asp:ListItem>
                                        </asp:DropDownList>
                                    </div> 
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                      <div class="form-group">
                                        <asp:Label ID="lblTruckNo" Text="Truck No." Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvDCSCode" ValidationGroup="a"
                                                ErrorMessage="Enter Truck No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Truck No.!'></i>"
                                                ControlToValidate="txtTruckNo" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtTruckNo" MaxLength="150" placeholder="Enter Truck No" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                  </div>
                                <div class="col-md-3">
                                     <div class="form-group">
                                        <asp:Label ID="lblTruckDriverName" Text="Truck Driver Name" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvTruckDriverName" ValidationGroup="a"
                                                ErrorMessage="Enter Truck Driver Name" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Truck Driver Name. !'></i>"
                                                ControlToValidate="txtTruckDriverName" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtTruckDriverName" MaxLength="150" placeholder="Enter Truck Driver Name" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                        <div class="form-group">
                                        <asp:Label ID="lblTruckWeight" Text="Truck Driver Name" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvTruckWeight" ValidationGroup="a"
                                                ErrorMessage="Enter Truck Weight" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Truck Weight. !'></i>"
                                                ControlToValidate="txtTruckWeight" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtTruckWeight" MaxLength="150" placeholder="Enter Truck Weight" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                         <div class="form-group">
                                        <asp:Label ID="lblMilkQty" Text="Milk Qty.(KG)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                ErrorMessage="Enter Milk Qty.(KG)" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Qty.(KG). !'></i>"
                                                ControlToValidate="txtMilkQty" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtMilkQty" MaxLength="150" placeholder="Enter Milk Qty.(KG)" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblFat" Text="Fat %" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvFat" ValidationGroup="a"
                                                ErrorMessage="Enter Fat %" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Fat %. !'></i>"
                                                ControlToValidate="txtFat" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtFat" MaxLength="150" placeholder="Enter Fat %" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                         <div class="form-group">
                                        <asp:Label ID="lblSNF" Text="SNF %" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvSNF" ValidationGroup="a"
                                                ErrorMessage="Enter SNF %" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter SNF %. !'></i>"
                                                ControlToValidate="txtSNF" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtSNF" MaxLength="150" placeholder="Enter SNF %" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                     <div class="form-group">
                                        <asp:Label ID="lblValue" Text="Value(Rs.)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvValue" ValidationGroup="a"
                                                ErrorMessage="Enter Value(Rs.)" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Value(Rs.) !'></i>"
                                                ControlToValidate="txtValue" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtValue" MaxLength="150" placeholder="Enter Value(Rs.)" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row"> 
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-block btn-default" />
                                    </div>
                                </div>
                            </div>
                            </div>
                         </div>
                    </div>
                </div>
          
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Collection (at Bay) Details </h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">

                            <div class="table-responsive">
                                <div>
                                    <table class="datatable table table-striped table-bordered table-hover" cellspacing="0" rules="all" border="1" id="ctl00_ContentBody_gvOpeningStock" style="border-collapse: collapse;">
                                        <thead>
                                            <tr>
                                                <th scope="col">S.No.<br />
                                                </th>
                                                <th scope="col">Date
                                                    <br />
                                                </th>
                                                <th scope="col">Shift<br />
                                                </th>
                                                <th scope="col">Centre Name
                                                    <br />
                                                    (CC/BMC/MDP)
                                                    <br />
                                                </th>
                                                <th scope="col">Route No.<br />
                                                </th>
                                                <th scope="col">Truck No. </th>
                                                <th scope="col">Truck Driver Name </th>
                                                <th scope="col">Truck Weight </th>
                                                <th scope="col">Milk Qty. (KG)
                                                    <br />
                                                </th>
                                                <th scope="col">Fat %
                                                    <br />
                                                </th>
                                                <th scope="col">SNF %
                                                    <br />
                                                </th>
                                                <th scope="col">Value(Rs.)
                                                    <br />
                                                </th>

                                                <th scope="col">Action
                                                    <br />
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>1
                                                </td>
                                                <td>28/01/2019
                                                </td>
                                                <td>Morning
                                                </td>
                                                <td>CC
                                                </td>
                                                <td>R1
                                                </td>
                                                <td>MP 04 FS 4654
                                                </td>
                                                <td>Mohan Lal
                                                </td>
                                                <td>500 Tonne
                                                </td>
                                                <td>50
                                                </td>
                                                <td>6.5
                                                </td>
                                                <td>7.5
                                                </td>
                                                <td>4500
                                                </td>

                                                <td>
                                                    <a id="ctl00_ContentBody_gvOpeningStock_ctl02_lnkUpdate" title="Update" href="javascript:__doPostBack(&#39;ctl00$ContentBody$gvOpeningStock$ctl02$lnkUpdate&#39;,&#39;&#39;)"><i class="fa fa-pencil"></i></a>
                                                    &nbsp;&nbsp;&nbsp;<a onclick="return confirm(&#39;Are you sure to Delete?&#39;);" id="ctl00_ContentBody_gvOpeningStock_ctl02_lnkDelete" title="Delete" href="javascript:__doPostBack(&#39;ctl00$ContentBody$gvOpeningStock$ctl02$lnkDelete&#39;,&#39;&#39;)" style="color: red;"><i class="fa fa-trash"></i></a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>2
                                                </td>
                                                <td>28/01/2019
                                                </td>
                                                <td>Morning
                                                </td>
                                                <td>BMC
                                                </td>
                                                <td>R2
                                                </td>
                                                <td>MP 04 FS 4654
                                                </td>
                                                <td>Tolaram Modi
                                                </td>
                                                <td>450 Tonne
                                                </td>
                                                <td>50
                                                </td>
                                                <td>5.6
                                                </td>
                                                <td>9.3
                                                </td>
                                                <td>6800
                                                </td>

                                                <td>
                                                    <a id="ctl00_ContentBody_gvOpeningStock_ctl02_lnkUpdate" title="Update" href="javascript:__doPostBack(&#39;ctl00$ContentBody$gvOpeningStock$ctl02$lnkUpdate&#39;,&#39;&#39;)"><i class="fa fa-pencil"></i></a>
                                                    &nbsp;&nbsp;&nbsp;<a onclick="return confirm(&#39;Are you sure to Delete?&#39;);" id="ctl00_ContentBody_gvOpeningStock_ctl02_lnkDelete" title="Delete" href="javascript:__doPostBack(&#39;ctl00$ContentBody$gvOpeningStock$ctl02$lnkDelete&#39;,&#39;&#39;)" style="color: red;"><i class="fa fa-trash"></i></a>
                                                </td>
                                            </tr>


                                        </tbody>
                                    </table>
                                </div>
                            </div>
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
                                <h4 class="modal-title" id="myModalLabel2">Milk Collection Detail's</h4>
                            </div>
                            <div class="clearfix"></div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Label ID="Label1" Text="Member ID [Name] : " runat="server"></asp:Label>
                                        <asp:Label ID="lblMemberId" Font-Bold="true" ForeColor="Green" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvPopup_ViewMilkCollectionDetails" ShowHeader="true" ShowFooter="true" FooterStyle-BackColor="#eaeaea" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover" runat="server">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Milk Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMilkType_id" runat="server" Visible="false" Text='<%# Eval("MilkType_id") %>'></asp:Label>
                                                                <asp:Label ID="lblMilkType" runat="server" Text='<%# Eval("MilkTypeName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="FAT %">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFAT" runat="server" Text='<%# Eval("FAT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("SNF") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CLR">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("CLR") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblFooter" runat="server" Font-Bold="true" Text="Grand Total"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Milk Qty.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMilkQty" runat="server" Text='<%# Eval("MilkQty") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalMilkQty" runat="server" Font-Bold="true" Text="0.00"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUnit_id" runat="server" Visible="false" Text='<%# Eval("Unit_id") %>'></asp:Label>
                                                                <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("UQCCode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rate/Ltr.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRate" runat="server" Text='<%# Eval("RatePerLtr", "{0:0.00}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Value (In Rs.)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblValue" runat="server" Text='<%# Eval("TotalValue", "{0:0.00}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalValue" runat="server" Font-Bold="true" Text="0.00"></asp:Label>
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
</asp:Content>

