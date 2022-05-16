 <%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MilkQualityAssurance.aspx.cs" Inherits="mis_MilkCollection_MilkQualityAssurance" %>

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
                                <h3 class="box-title">Milk Quality Asssurance</h3>
                                <span id="ctl00_ContentBody_lblmsg"></span>
                            </div>
                            <div class="box-body">
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
                                                <asp:TextBox ID="txtDate" onkeypress="return false;" runat="server" placeholder="dd/mm/yyyy" class="form-control" data-date-end-date="+7d" autocomplete="off" data-date-format="dd/mm/yyyy" data-date-autoclose="true" data-provide="datepicker" data-date-start-date="-365d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
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
                                        <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Morning</asp:ListItem>
                                            <asp:ListItem>Evening</asp:ListItem>
                                            <asp:ListItem>Special</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    </div>
                                    <div class="col-md-3">
                                            <div class="form-group">
                                        <label>Center Type (CC/BMC/MDP)<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvCenterType" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlCenterType" InitialValue="0" ErrorMessage="Select Center Type (CC/BMC/MDP)." Text="<i class='fa fa-exclamation-circle' title='Select Center Type (CC/BMC/MDP) !'></i>">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlCenterType" runat="server" CssClass="form-control select2">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>CC</asp:ListItem>
                                            <asp:ListItem>BMC</asp:ListItem>
                                            <asp:ListItem>MDP</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    </div>
                                    <div class="col-md-3">
                                          <div class="form-group">
                                        <asp:Label ID="lblCenterName" Text="Center Name" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvCenterName" ValidationGroup="a"
                                                ErrorMessage="Enter Center Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Center Name.!'></i>"
                                                ControlToValidate="txtCenterName" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtCenterName" MaxLength="150" placeholder="Enter Center Name" ClientIDMode="Static"></asp:TextBox>
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
                                    <div class="col-md-3">
                                           <div class="form-group">
                                        <asp:Label ID="lblVehicleNo" Text="Vehicle No." Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvVehicleNo" ValidationGroup="a"
                                                ErrorMessage="Enter Vehicle No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Vehicle No. !'></i>"
                                                ControlToValidate="txtVehicleNo" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtVehicleNo" MaxLength="150" placeholder="Enter Vehicle No." ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    </div>
                                   <div class="col-md-3">
                                         <div class="form-group">
                                        <asp:Label ID="lblMilkSampleCode" Text="Milk Sample Code" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvMilkSampleCode" ValidationGroup="a"
                                                ErrorMessage="Enter Milk Sample Code" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Sample Code !'></i>"
                                                ControlToValidate="txtMilkSampleCode" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtMilkSampleCode" MaxLength="150" placeholder="Enter Milk Sample Code" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    </div>

                                    <div class="col-md-3">
                                            <div class="form-group">
                                        <asp:Label ID="lblSampleQuantity" Text="Sample Quantity (Kg)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvSampleQuantity" ValidationGroup="a"
                                                ErrorMessage="Enter Sample Quantity (Kg)" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Sample Quantity (Kg) !'></i>"
                                                ControlToValidate="txtSampleQuantity" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtSampleQuantity" MaxLength="150" placeholder="Enter Sample Quantity (Kg)" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    </div>
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
                                        <asp:Label ID="lblNutritionValue" Text="Nutrition Value" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvNutritionValue" ValidationGroup="a"
                                                ErrorMessage="Enter Nutrition Value" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Nutrition Value !'></i>"
                                                ControlToValidate="txtNutritionValue" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtNutritionValue" MaxLength="150" placeholder="Enter Nutrition Value" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    </div>
                                    <div class="col-md-3">
                                         <div class="form-group">
                                        <asp:Label ID="lblPHValue" Text="PH Value" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvPHValue" ValidationGroup="a"
                                                ErrorMessage="Enter PH Value" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter PH Value !'></i>"
                                                ControlToValidate="txtPHValue" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtPHValue" MaxLength="150" placeholder="Enter PH Value" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    </div>
                                    <div class="col-md-3">
                                          <div class="form-group">
                                        <label>Test Result<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvTestResult" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlTestResult" InitialValue="0" ErrorMessage="Select Test Result" Text="<i class='fa fa-exclamation-circle' title='Select Test Result !'></i>">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlTestResult" runat="server" CssClass="form-control select2">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Pass</asp:ListItem>
                                            <asp:ListItem>Fail</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    </div>
                                    <div class="col-md-3">
                                         <div class="form-group">
                                        <asp:Label ID="lblFeedback" Text="Feedback" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvFeedback" ValidationGroup="a"
                                                ErrorMessage="Enter Feedback" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Feedback !'></i>"
                                                ControlToValidate="txtFeedback" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtFeedback" MaxLength="150" placeholder="Enter Feedback" ClientIDMode="Static"></asp:TextBox>
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
                        <h3 class="box-title">Quality Asssurance Details </h3>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <div>
                                        <table class="datatable table table-striped table-bordered table-hover" cellspacing="0" rules="all" border="1" id="ctl00_ContentBody_gvOpeningStock" style="border-collapse:collapse;">
                                            <thead>
                                                <tr>
                                                    <th scope="col">S.No.<br /></th>
                                                    <th scope="col">Date <br /></th>
                                                    <th scope="col">Shift<br /></th>
                                                    <th scope="col">Center&nbsp;Type <br /></th>
                                                    <th scope="col">Center&nbsp;Name <br /></th>
                                                    <th scope="col">Sample&nbsp;Name </th>
                                                    <th scope="col">Route&nbsp;No. </th>
                                                    <th scope="col">Vehicle&nbsp;No.</th>
                                                    <th scope="col">Milk&nbsp;Sample&nbsp;Code</th>
                                                    <th scope="col">Sample&nbsp;Quantity&nbsp;(Kg)</th>
                                                    <th scope="col">Fat&nbsp;% <br /></th>
                                                    <th scope="col">SNF&nbsp;% <br /></th>
                                                    <th scope="col">Nutrition&nbsp;Value <br /></th>
                                                    <th scope="col">PH&nbsp;Value<br /></th>
                                                    <th scope="col">Test&nbsp;Result <br /></th>
                                                    <th scope="col">Feedback <br /></th>
                                                    <th scope="col">Action <br /></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        1
                                                    </td>
                                                    <td>
                                                        28/01/2019
                                                    </td>
                                                    <td>
                                                        Morning
                                                    </td>
                                                    <td>
                                                        CC
                                                    </td>
                                                    <td>
                                                        Sanchi Sai Center
                                                    </td>
                                                    <td>
                                                        R1
                                                    </td>
                                                    <td>
                                                       MP 04 T 9999
                                                    </td>
                                                    <td>
                                                        12
                                                    </td>
                                                    <td>
                                                        S011
                                                    </td>
                                                    <td>
                                                       1 KG
                                                    </td>
                                                    <td>
                                                       5.6
                                                    </td>
                                                    <td>
                                                        7.8
                                                    </td>
                                                    <td>
                                                        145
                                                    </td>
                                                    <td>
                                                        6
                                                    </td>
                                                    <td><p class="label label-success">Pass</p> </td>
                                                    <td>
                                                        Good
                                                    </td>
                                                    <td>
                                                        <a id="ctl00_ContentBody_gvOpeningStock_ctl02_lnkUpdate" title="Update" href="javascript:__doPostBack(&#39;ctl00$ContentBody$gvOpeningStock$ctl02$lnkUpdate&#39;,&#39;&#39;)"><i class="fa fa-pencil"></i></a>
                                                        &nbsp;&nbsp;&nbsp;<a onclick="return confirm(&#39;Are you sure to Delete?&#39;);" id="ctl00_ContentBody_gvOpeningStock_ctl02_lnkDelete" title="Delete" href="javascript:__doPostBack(&#39;ctl00$ContentBody$gvOpeningStock$ctl02$lnkDelete&#39;,&#39;&#39;)" style="color: red;"><i class="fa fa-trash"></i></a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        2
                                                    </td>
                                                    <td>
                                                        27/01/2019
                                                    </td>
                                                    <td>
                                                        Evening
                                                    </td>
                                                    <td>
                                                        BMC
                                                    </td>
                                                    <td>
                                                        Sanchi Guru kripa Center
                                                    </td>
                                                    <td>
                                                        R3
                                                    </td>
                                                    <td>
                                                        MP 04 K 1111
                                                    </td>
                                                    <td>
                                                        11
                                                    </td>
                                                    <td>
                                                        S012
                                                    </td>
                                                    <td>
                                                        0.75 KG
                                                    </td>
                                                    <td>
                                                        5.4
                                                    </td>
                                                    <td>
                                                        7.0
                                                    </td>
                                                    <td>
                                                        130
                                                    </td>
                                                    <td>
                                                        2
                                                    </td>
                                                    <td><p class="label label-success">Pass</p> </td>
                                                    <td>
                                                        Good
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

            </section>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
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

