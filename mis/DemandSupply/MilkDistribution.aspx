<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MilkDistribution.aspx.cs" Inherits="mis_Demand_Supply_MilkDistribution" %>

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
                            <h3 class="box-title">Milk Distribution</h3>
                            <span id="ctl00_ContentBody_lblmsg"></span>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Date<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvDate" runat="server" ForeColor="red" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtDate" ErrorMessage="Enter Date." Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"></asp:RequiredFieldValidator>
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
                                        <label>Time</label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvTime" runat="server" ForeColor="red" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtTime" ErrorMessage="Enter Time." Text="<i class='fa fa-exclamation-circle' title='Enter Time !'></i>"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" ForeColor="Red" runat="server" Display="Dynamic" ControlToValidate="txtTime" ErrorMessage="Invalid Time" Text="<i class='fa fa-exclamation-circle' title='Invalid Time!'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                        </span>
                                        <div class="input-group bootstrap-timepicker timepicker">
                                            <asp:TextBox ID="txtTime" onkeypress="return false;" runat="server" placeholder="dd/mm/yyyy" class="form-control input-small" autocomplete="off" value="09:30 AM" data-date-autoclose="true" data-provide="timepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-clock-o"></i></span>
                                        </div>
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
                              </div>
                            <div class="row">
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
                                        <label>Distributor Type<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvDistributorType" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlDistributorType" InitialValue="0" ErrorMessage="Select Distributor Type." Text="<i class='fa fa-exclamation-circle' title='Select Distributor Type !'></i>">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlDistributorType" runat="server" CssClass="form-control select2">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Distributor</asp:ListItem>
                                            <asp:ListItem>Sub Distributor</asp:ListItem>
                                            <asp:ListItem>Sanchi Parlour</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Distributor Name<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvDistributorName" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlDistributorName" InitialValue="0" ErrorMessage="Select Distributor Name." Text="<i class='fa fa-exclamation-circle' title='Select Distributor Name !'></i>">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlDistributorName" runat="server" CssClass="form-control select2">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Sanchi Sai Distributor</asp:ListItem>
                                            <asp:ListItem>Sanchi Guru Kripa Parlour</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblBatchNo" Text="Batch No" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvBatchNo" ValidationGroup="a"
                                                ErrorMessage="Enter Batch No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Batch No.!'></i>"
                                                ControlToValidate="txtBatchNo" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtBatchNo" MaxLength="150" placeholder="Enter Batch No" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Category<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvCategory" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlCategory" InitialValue="0" ErrorMessage="Select Category." Text="<i class='fa fa-exclamation-circle' title='Select Category !'></i>">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control select2">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Milk</asp:ListItem>
                                            <asp:ListItem>Milk Product</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Variant<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvVariant" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlVariant" InitialValue="0" ErrorMessage="Select Variant." Text="<i class='fa fa-exclamation-circle' title='Select Variant !'></i>">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlVariant" runat="server" CssClass="form-control select2">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>STD</asp:ListItem>
                                            <asp:ListItem>DTM</asp:ListItem>
                                            <asp:ListItem>HF Loose</asp:ListItem>
                                            <asp:ListItem>Light</asp:ListItem>
                                            <asp:ListItem>FCM</asp:ListItem>
                                            <asp:ListItem>TM</asp:ListItem>
                                            <asp:ListItem>CCM</asp:ListItem>
                                            <asp:ListItem>Chaha</asp:ListItem>
                                            <asp:ListItem>Chai Spl</asp:ListItem>
                                            <asp:ListItem>STD Loose</asp:ListItem>
                                            <asp:ListItem>Cow</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Milk Packet Size<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvMilkPacketSize" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlMilkPacketSize" InitialValue="0" ErrorMessage="Select Milk Packet Size." Text="<i class='fa fa-exclamation-circle' title='Select Milk Packet Size !'></i>">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlMilkPacketSize" runat="server" CssClass="form-control select2">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>200 ml</asp:ListItem>
                                            <asp:ListItem>500 ml</asp:ListItem>
                                            <asp:ListItem>1 Ltr</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblNoofPackets" Text="No. of Packets" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvNoofPackets" ValidationGroup="a"
                                                ErrorMessage="Enter No. of Packets." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter No. of Packets.!'></i>"
                                                ControlToValidate="txtNoofPackets" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtNoofPackets" MaxLength="150" placeholder="Enter No. of Packets" ClientIDMode="Static"></asp:TextBox>
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
                    <h3 class="box-title">Milk Distribution Details </h3>
                </div>
                <div class="box-body">
                    <div class="row">
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <table class="datatable table table-striped table-bordered table-hover" cellspacing="0" rules="all" border="1" id="ctl00_ContentBody_gvOpeningStock" style="border-collapse: collapse;">
                                    <thead>
                                        <tr>
                                            <th scope="col">S.No.<br />
                                            </th>
                                            <th scope="col">Date<br />
                                            </th>
                                            <th scope="col">Time </th>
                                            <th scope="col">Route&nbsp;No.</th>
                                            <th scope="col">Shift
                                                <br />
                                            </th>
                                            <th scope="col">Vehicle&nbsp;No.<br />
                                            </th>
                                            <th scope="col">Distributor&nbsp;Type
                                                <br />
                                            </th>
                                            <th scope="col">Distributor&nbsp;Name
                                                <br />
                                            </th>
                                            <th scope="col">Batch&nbsp;No.
                                                <br />
                                            </th>
                                            <th scope="col">Category
                                                <br />
                                            </th>
                                            <th scope="col">Milk&nbsp;Variant
                                                <br />
                                            </th>
                                            <th scope="col">Milk&nbsp;Packet&nbsp;Size
                                                <br />
                                            </th>
                                            <th scope="col">No.&nbsp;of&nbsp;Packets<br />
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
                                            <td>15-Feb-2019
                                            </td>
                                            <td>08:20 AM
                                            </td>
                                            <td>R1
                                            </td>
                                            <td>Morning
                                            </td>
                                            <td>MP 04 K 9999
                                            </td>
                                            <td>Distributor
                                            </td>
                                            <td>Sanchi Sai Distributor
                                            </td>
                                            <td>BS2815
                                            </td>
                                            <td>Milk Product
                                            </td>
                                            <td>DTM
                                            </td>
                                            <td>500 ml
                                            </td>
                                            <td>5000
                                            </td>
                                            <td>
                                                <a id="ctl00_ContentBody_gvOpeningStock_ctl02_lnkUpdate" title="Update" href="javascript:__doPostBack(&#39;ctl00$ContentBody$gvOpeningStock$ctl02$lnkUpdate&#39;,&#39;&#39;)"><i class="fa fa-pencil"></i></a>
                                                &nbsp;&nbsp;&nbsp;<a onclick="return confirm(&#39;Are you sure to Delete?&#39;);" id="ctl00_ContentBody_gvOpeningStock_ctl02_lnkDelete" title="Delete" href="javascript:__doPostBack(&#39;ctl00$ContentBody$gvOpeningStock$ctl02$lnkDelete&#39;,&#39;&#39;)" style="color: red;"><i class="fa fa-trash"></i></a>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>2
                                            </td>
                                            <td>14-Feb-2019
                                            </td>
                                            <td>09:20 AM
                                            </td>
                                            <td>R2
                                            </td>
                                            <td>Evening
                                            </td>
                                            <td>MP 04 T 8458
                                            </td>
                                            <td>Sanchi Parlour
                                            </td>
                                            <td>Sanchi Guru Kripa Parlour
                                            </td>
                                            <td>BS2111
                                            </td>
                                            <td>Milk Product
                                            </td>
                                            <td>FCM
                                            </td>
                                            <td>200 ml
                                            </td>
                                            <td>15000
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

