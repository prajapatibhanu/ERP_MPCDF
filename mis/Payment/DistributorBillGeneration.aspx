<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DistributorBillGeneration.aspx.cs" Inherits="mis_Payment_DistributorBillGeneration" %>

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
                                <h3 class="box-title">Distributor Bill Generation</h3>
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
                                        <asp:Label ID="lblDistributorNo" Text="Distributor No." Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvDistributorNo" ValidationGroup="a"
                                                ErrorMessage="Enter Distributor No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Distributor No. !'></i>"
                                                ControlToValidate="txtDistributorNo" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDistributorNo" MaxLength="150" placeholder="Enter Distributor No." ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                        <asp:Label ID="lblDistributorName" Text="Distributor Name" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvDistributorName" ValidationGroup="a"
                                                ErrorMessage="Enter Distributor Name" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Distributor Name. !'></i>"
                                                ControlToValidate="txtDistributorName" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDistributorName" MaxLength="150" placeholder="Enter Distributor Name." ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    </div>
                                </div>
                                <div class="row">
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
                                        <asp:Label ID="lblGatePassNo" Text="Gate Pass No." Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvGatePassNo" ValidationGroup="a"
                                                ErrorMessage="Enter Gate Pass No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Gate Pass No. !'></i>"
                                                ControlToValidate="txtGatePassNo" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtGatePassNo" MaxLength="150" placeholder="Enter Gate Pass No." ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    </div>
                                    <div class="col-md-3">
                                          <div class="form-group">
                                        <asp:Label ID="lblGST" Text="GST%" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvGST" ValidationGroup="a"
                                                ErrorMessage="Enter GST%" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter GST% !'></i>"
                                                ControlToValidate="txtGST" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtGST" MaxLength="150" placeholder="Enter GST%" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    </div>
                                </div>
                                <div class="row">
                                   <div class="col-md-3">
                                        <div class="form-group">
                                        <asp:Label ID="lblPaymentUTRNo" Text="Payment/UTR No." Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvPaymentUTRNo" ValidationGroup="a"
                                                ErrorMessage="Enter Payment/UTR No" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Payment/UTR No. !'></i>"
                                                ControlToValidate="txtPaymentUTRNo" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtPaymentUTRNo" MaxLength="150" placeholder="Enter Payment/UTR No." ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                        <label>Category<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvCategory" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlCategory" InitialValue="0" ErrorMessage="Select Category" Text="<i class='fa fa-exclamation-circle' title='Select Category !'></i>">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control select2">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Milk</asp:ListItem>
                                            <asp:ListItem>Milk Products</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                        <asp:Label ID="lblProductSize" Text="Product Size" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvProductSize" ValidationGroup="a"
                                                ErrorMessage="Enter Product Size" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Product Size !'></i>"
                                                ControlToValidate="txtProductSize" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtProductSize" MaxLength="150" placeholder="Enter Product Size" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    </div>
                                    <div class="col-md-3">
                                         <div class="form-group">
                                        <asp:Label ID="lblValue" Text="Value(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvValue" ValidationGroup="a"
                                                ErrorMessage="Enter Value(Rs)" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Value(Rs) !'></i>"
                                                ControlToValidate="txtValue" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtValue" MaxLength="150" placeholder="Enter Value(Rs)" ClientIDMode="Static"></asp:TextBox>
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
                        <h3 class="box-title">Distributor Bill Generation Details</h3>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <div class="row">
                                        <div class="col-md-1 pull-right">
                                            <div class="form-group">
                                                <input type="submit" name="ctl00$ContentBody$btnSave" value="Export" id="btnSave" class="btn btn-block btn-defult" />
                                            </div>
                                        </div>
                                        <div class="col-md-1 pull-right">
                                            <div class="form-group">
                                                <input type="submit" name="ctl00$ContentBody$btnSave" value="Print" id="btnSave" class="btn btn-block btn-defult" />
                                            </div>
                                        </div>

                                    </div>
                                    <div>
                                        <table class="datatable table table-striped table-bordered table-hover" cellspacing="0" rules="all" border="1" id="ctl00_ContentBody_gvOpeningStock" style="border-collapse:collapse;">
                                            <thead>
                                                <tr>
                                                    <th scope="col">S.No.<br /></th>
                                                    <th scope="col">Date<br /></th>
                                                    <th scope="col">Time</th>
                                                    <th scope="col">Distributor No.<br /></th>
                                                    <th scope="col">Distributor Name<br /></th>
                                                    <th scope="col">Route No.<br /></th>
                                                    <th scope="col">Vehicle No.<br /></th>
                                                    <th scope="col">Gate Pass No.<br /></th>
                                                    <th scope="col">GST %<br /></th>
                                                    <th scope="col">Payment/UTR No.<br /></th>
                                                    <th scope="col">Category<br /></th>
                                                    <th scope="col">Product Size<br /></th>
                                                    <th scope="col">Value(Rs)<br /></th>
                                                    <th scope="col">Actions</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>1</td>
                                                    <td>26/01/2019</td>
                                                    <td>09:36 AM</td>
                                                    <td>D1821</td>
                                                    <td>Sanchi Sai Distributor</td>
                                                    <td>R1</td>
                                                    <td>MP 04 T 9999</td>
                                                    <td>G5155</td>
                                                    <td>18%</td>
                                                    <td>UTR18551</td>
                                                    <td>Milk Product</td>
                                                    <td>200 ml</td>
                                                    <td>5000</td>
                                                    <td>
                                                        <a id="ctl00_ContentBody_gvOpeningStock_ctl02_lnkUpdate" title="Update" href="javascript:__doPostBack(&#39;ctl00$ContentBody$gvOpeningStock$ctl02$lnkUpdate&#39;,&#39;&#39;)"><i class="fa fa-pencil"></i></a>
                                                        &nbsp;&nbsp;&nbsp;<a onclick="return confirm(&#39;Are you sure to Delete?&#39;);" id="ctl00_ContentBody_gvOpeningStock_ctl02_lnkDelete" title="Delete" href="javascript:__doPostBack(&#39;ctl00$ContentBody$gvOpeningStock$ctl02$lnkDelete&#39;,&#39;&#39;)" style="color: red;"><i class="fa fa-trash"></i></a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>2</td>
                                                    <td>25/01/2019</td>
                                                    <td>09:30 AM</td>
                                                    <td>D1822</td>
                                                    <td>Sanchi Guru Kripa Distributor</td>
                                                    <td>R3</td>
                                                    <td>MP 04 K 1111</td>
                                                    <td>G5166</td>
                                                    <td>12%</td>
                                                    <td>UTR1811</td>
                                                    <td>Milk Product</td>
                                                    <td>500 ml</td>
                                                    <td>8000</td>
                                                    <td>
                                                        <a id="ctl00_ContentBody_gvOpeningStock_ctl02_lnkUpdate" title="Update" href="javascript:__doPostBack(&#39;ctl00$ContentBody$gvOpeningStock$ctl02$lnkUpdate&#39;,&#39;&#39;)"><i class="fa fa-pencil"></i></a>
                                                        &nbsp;&nbsp;&nbsp;<a onclick="return confirm(&#39;Are you sure to Delete?&#39;);" id="ctl00_ContentBody_gvOpeningStock_ctl02_lnkDelete" title="Delete" href="javascript:__doPostBack(&#39;ctl00$ContentBody$gvOpeningStock$ctl02$lnkDelete&#39;,&#39;&#39;)" style="color: red;"><i class="fa fa-trash"></i></a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="12" style="text-align: center;  font-weight: bold">Total Value(Rs.)</td>
                                                    <td>13000</td>
                                                    <td></td>
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

