<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SupplyListRouteOrDistWise.aspx.cs" Inherits="mis_DemandSupply_SupplyListRouteOrDistWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
     <style>
        .columngreen {
            background-color: #aee6a3 !important;
        }

        .columnred {
            background-color: #f05959 !important;
        }

        .columnmilk {
            background-color: #bfc7c5 !important;
        }

        .columnproduct {
            background-color: #f5f376 !important;
        }
    </style>
    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }

        function checkedit(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < 1; i++) {
            //var rowscount = document.getElementByID('<%=GridView4.ClientID%>').rows.length; 
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }

        function Validate(sender, args) {
            var gridView = document.getElementById("<%=GridView1.ClientID %>");
            var checkBoxes = gridView.getElementsByTagName("input");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }

    </script>
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSubmit_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>

        </div>
    </div>
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
   <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="c" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Approval of Supply</h3>
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Date ,Shift / दिनांक ,शिफ्ट 
                                </legend>
                                <div class="row">
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    
                                </div>
                                <div class="row">

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Date / दिनांक<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="b"
                                                    ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    ControlToValidate="txtOrderDate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="b" runat="server" Display="Dynamic" ControlToValidate="txtOrderDate"
                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" OnTextChanged="txtOrderDate_TextChanged" AutoPostBack="true" CssClass="form-control" ID="txtOrderDate" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Shift / शिफ्ट</label>
                                            <%--<span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="b"
                                                    InitialValue="0" ErrorMessage="Select Shift" Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"
                                                    ControlToValidate="ddlShift" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>--%>
                                            <asp:DropDownList ID="ddlShift" runat="server" OnSelectedIndexChanged="ddlShift_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                   
                                     <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Location / लोकेशन <span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="b"
                                                    InitialValue="0" ErrorMessage="Select Location" Text="<i class='fa fa-exclamation-circle' title='Select Location !'></i>"
                                                    ControlToValidate="ddlLocation" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlLocation" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                     <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Route/रूट <span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="b"
                                                    InitialValue="0" ErrorMessage="Select Route" Text="<i class='fa fa-exclamation-circle' title='Select Route !'></i>"
                                                    ControlToValidate="ddlRoute" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlRoute" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" OnClick="btnSearch_Click" ValidationGroup="b" ID="btnSearch" Text="Search" />

                                        </div>
                                    </div>
                                    <div class="col-md-1" style="margin-top: 20px;">
                                        <div class="form-group">
                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-default" />
                                        </div>
                                    </div>
                                      
                                </div>
                            </fieldset>

                        </div>

                    </div>
                </div>
                <div class="col-md-12" id="pnlparlourdetails" runat="server" Visible="false">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Approval of Supply</h3>

                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblReportMsg" runat="server"></asp:Label>
                                </div>
                            </div>

                            <%--<div class="row" id="pnlData" runat="server" visible="false">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend>Route Wise Approval Of Supply</legend>

                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GridViewRoutwise" runat="server" OnRowDataBound="GridViewRoutwise_RowDataBound" OnRowCommand="GridViewRoutwise_RowCommand" ShowFooter="false" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                    AutoGenerateColumns="true" EmptyDataText="No Record Found." EnableModelValidation="True">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Route">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkbtnRoute" CssClass="btn btn-block btn-secondary" Text='<%#Eval("Route") %>' CommandName="RoutwiseBooth" CommandArgument='<%#Eval("RouteId") %>' runat="server"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                      
                                    </fieldset>
                                </div>

                            </div>--%>
                           
                             <div class="row">
                                 <div class="col-md-3">
                                     <div class="form-group">
                                     <asp:Label ID="lblroutename" runat="server"></asp:Label>
                                         </div>
                                 </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand" ShowFooter="true" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                AutoGenerateColumns="true" EmptyDataText="No Record Found." DataKeyNames="tmp_MilkOrProductDemandId" EnableModelValidation="True">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" ItemStyle-Width="30" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                                              <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="a" ErrorMessage="Please select at least one record."
                                            ClientValidationFunction="Validate" Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Please select at least one record. !'></i>" ForeColor="Red"></asp:CustomValidator>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order Id">
                                                        <ItemTemplate>
                                                             <asp:LinkButton ID="lblOrderId" CommandName="ItemOrdered" CommandArgument='<%#Eval("tmp_MilkOrProductDemandId") %>' Text='<%#Eval("tmp_OrderId") %>' CssClass="btn btn-secondary" runat="server"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Retailer /Institution Name" HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBandOName" Text='<%# Eval("BandOName") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>

                                             <div id="divtable" runat="server"></div>
                                        </div>

                                    </div>
                                </div>
                             <div class="row">
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" OnClientClick="return ValidatePage();" AccessKey="S" ID="btnSubmit" Text="Approve" />
                                        </div>
                                    </div>
                                </div>

                                <div class="modal" id="ItemDetailsModal">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">×</span></button>
                                    <h4 class="modal-title">Item Details for <span id="modalBoothName" style="color: red" runat="server"></span>&nbsp;&nbsp;
                             Order Date :<span id="modaldate" style="color: red" runat="server"></span>&nbsp;&nbsp;Order Shift :<span id="modalshift" style="color: red" runat="server"></span></h4>
                                </div>
                                <div class="modal-body">
                                    <div id="divitem" runat="server">
                                        <asp:Label ID="lblModalMsg" runat="server" Text=""></asp:Label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <fieldset>
                                                    <legend>Item Details</legend>
                                                    <div class="row" style="height: 250px; overflow: scroll;">
                                                        <div class="col-md-12">
                                                            <div class="table-responsive">
                                                                <asp:GridView ID="GridView4" runat="server" OnRowCommand="GridView4_RowCommand" OnRowDataBound="GridView4_RowDataBound" EmptyDataText="No Record Found" class="table table-striped table-bordered" AllowPaging="false"
                                                                    AutoGenerateColumns="False" EnableModelValidation="True" DataKeyNames="MilkOrProductDemandChildId">
                                                                    <Columns>
                                                                       
                                                                        <asp:TemplateField HeaderText="SNo./ क्र." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                                            
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Item Category / वस्तु वर्ग">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblItemCatName" Text='<%# Eval("ItemCatName")%>' runat="server" />
                                                                                <asp:Label ID="lblItemCat_id" Visible="false" Text='<%# Eval("ItemCat_id")%>' runat="server" />
                                                                                 <asp:Label ID="lblDemandStatus" Visible="false" Text='<%# Eval("Demand_Status")%>' runat="server" />
                                                                                 <asp:Label ID="lblMilkOrProductDemandChildId" Visible="false" Text='<%# Eval("MilkOrProductDemandChildId")%>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Item Name / वस्तु नाम">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblIName" Text='<%# Eval("ItemName")%>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                           <asp:TemplateField HeaderText="Total Qty./ कुल मात्रा">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTotalQty" Text='<%# Eval("TotalQty")%>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Total Supply Qty./ कुल आपूर्ति मात्रा">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSupplyTotalQty" Text='<%# Eval("SupplyTotalQty")%>' runat="server" />
                                                                                <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="c"
                                                                                    ErrorMessage="Enter Total Supply Qty." Enabled="false" Text="<i class='fa fa-exclamation-circle' title='Total Supply Qty. !'></i>"
                                                                                    ControlToValidate="txtSupplyTotalQty" ForeColor="Red" Display="Dynamic" runat="server">
                                                                                </asp:RequiredFieldValidator>
                                                                                <asp:RegularExpressionValidator ID="rev1" Enabled="false" runat="server" Display="Dynamic" ValidationGroup="c"
                                                                                    ErrorMessage="Enter Valid Number In Quantity Field !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Number In Quantity Field !'></i>" ControlToValidate="txtSupplyTotalQty"
                                                                                    ValidationExpression="^[0-9]*$">
                                                                                </asp:RegularExpressionValidator>
                                                                                </span>
                                                                          <asp:TextBox runat="server" autocomplete="off" Visible="false" CausesValidation="true" Text='<%# Eval("SupplyTotalQty")%>' CssClass="form-control" ID="txtSupplyTotalQty" MaxLength="5" onpaste="return false;" onkeypress="return validateNum(event);" placeholder="Enter Total Supply Qty." ClientIDMode="Static"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                         <asp:TemplateField>
                                                                              <HeaderTemplate>
                                                                                  Leakage Qty./ लीकेज मात्रा
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                               <asp:Label ID="lblLeakageQty" Text='<%# Eval("LeakageQty")%>' runat="server" />
                                                                                <asp:RegularExpressionValidator ID="releakqty" Enabled="false" runat="server" Display="Dynamic" ValidationGroup="c"
                                                                                    ErrorMessage="Enter Valid Number In Leakage Qty Field !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Number In Leakage Qty Field !'></i>" ControlToValidate="txtLeakageQty"
                                                                                    ValidationExpression="^[0-9]*$">
                                                                                </asp:RegularExpressionValidator>
                                                                                </span>
                                                                         <asp:TextBox runat="server" Visible="false" autocomplete="off" Text='<%# Eval("LeakageQty")%>' CausesValidation="true" CssClass="form-control" ID="txtLeakageQty" MaxLength="5" onpaste="return false;"  onkeypress="return validateNum(event);" placeholder="Enter Leakage Qty." ClientIDMode="Static"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Remark/ टिप्पणी">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRemarkAtSupply" Text='<%# Eval("RemarkAtSupply")%>' runat="server" />
                                                                                 <asp:TextBox runat="server" autocomplete="off" Visible="false" CausesValidation="true" CssClass="form-control" ID="txtRemarkAtSupply" MaxLength="200" placeholder="Enter Remark" ClientIDMode="Static"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                         <asp:TemplateField HeaderText="Select">
                                                                             <HeaderTemplate>
                                                                                 <asp:CheckBox ID="checkAll" Checked="false" runat="server" onclick="checkAll(this);" AutoPostBack="true" OnCheckedChanged="chkedit_CheckedChanged" />
                                                                            </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkedit"  AutoPostBack="true" runat="server" onclick="checkedit(this);" Checked="false" OnCheckedChanged="chkedit_CheckedChanged" />
                                                                       </ItemTemplate>
                                                                </asp:TemplateField>
                                                                       
                                                                        <asp:TemplateField HeaderText="Action / कार्यवाही करें" Visible="false"  >
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkEdit" Visible="false" CommandName="RecordEdit" CommandArgument='<%#Eval("MilkOrProductDemandChildId") %>' runat="server" ToolTip="Edit"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                                                &nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkUpdate" Visible="false" ValidationGroup="c" CausesValidation="true" CommandName="RecordUpdate" CommandArgument='<%#Eval("MilkOrProductDemandChildId") %>' runat="server" ToolTip="Update" Style="color: darkgreen;" OnClientClick="return confirm('Are you sure to Update?')"><i class="fa fa-check"></i></asp:LinkButton>
                                                                                &nbsp;&nbsp;&nbsp;  <asp:LinkButton ID="lnkReset" Visible="false" CommandName="RecordReset" CommandArgument='<%#Eval("MilkOrProductDemandChildId") %>' runat="server" ToolTip="Reset" Style="color: gray;"><i class="fa fa-times"></i></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </fieldset>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                   

                                   <asp:LinkButton ID="lnkupdate" OnClick="lnkupdate_Click"  CssClass="btn btn-danger" OnClientClick="return confirm('Are you sure to Update?')" Text="Update" runat="server"></asp:LinkButton>
                               
                           
                                    <button type="button" class="btn btn-default" data-dismiss="modal">CLOSE </button>
                                </div>
                            </div>
                           
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
     <asp:Label runat="server" Visible="false" ID="lblcheckcount"></asp:Label>
       <asp:Label runat="server" Visible="false" ID="lblMilkOrProductDemandId"></asp:Label>
    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
    
        function myItemDetailsModal() {
            $("#ItemDetailsModal").modal('show');

        }
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {


                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Approve") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Approve this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
        </script>
</asp:Content>
