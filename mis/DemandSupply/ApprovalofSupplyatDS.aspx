<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ApprovalofSupplyatDS.aspx.cs" Inherits="mis_DemandSupply_ApprovalofSupplyatDS" %>

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
    <script>
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
        
    </script>
     <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <script type="text/javascript">
        //$("[src*=plus]").live("click", function () {
        //    $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
        //    $(this).attr("src", "../../images/minus.png");
        //});
        //$("[src*=minus]").live("click", function () {
        //    $(this).attr("src", "../../images/plus.png");
        //    $(this).closest("tr").next().remove();
        //});
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
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
   <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="c" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">


                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Approval of Supply -  List of Retailer/Institution </h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <fieldset>
                                <legend>Date,Shift,Category
                                </legend>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-10">
                                        <div class="form-group">
                                            Details of :<span id="spanRDIName" style="color: red" runat="server"></span>&nbsp;&nbsp; 
                               Date :<span id="SpanDate" style="color: red" runat="server"></span>&nbsp;&nbsp; 
                               Shift : <span id="spanShift" style="color: red" runat="server"></span>&nbsp;&nbsp;
                               Category : <span id="spanCategory" style="color: red" runat="server"></span>
                                        </div>
                                    </div>
                                    <div class="col-md-2 pull-right">
                                        <div class="form-group">
                                             <asp:Button ID="lnkbtnback" OnClick="lnkbtnback_Click" Text="Back" CssClass="btn btn-secondary" runat="server" />
                                           <%-- <asp:Button ID="lnkbtnback" PostBackUrl="~/mis/DemandSupply/SupplyListRouteOrDistWise.aspx" Text="Back" CssClass="btn btn-secondary" runat="server" />--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                </div>
                                <div class="row" id="pnlparlourdetails" runat="server">

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
                                                            <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order Id">
                                                        <ItemTemplate>
                                                             <asp:LinkButton ID="lblOrderId" CommandName="ItemOrdered" CommandArgument='<%#Eval("tmp_MilkOrProductDemandId") %>' Text='<%#Eval("tmp_OrderId") %>' CssClass="btn btn-secondary" runat="server"></asp:LinkButton>
                                                         <%--   <img alt="" style="cursor: pointer" src="../../images/plus.png" />--%>
                                                            <%--<asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                                                <asp:GridView ID="GridView2" runat="server" OnRowDataBound="GridView2_RowDataBound" EmptyDataText="No Record Found" class="table table-striped table-bordered" AllowPaging="false"
                                                                    AutoGenerateColumns="False" EnableModelValidation="True" DataKeyNames="MilkOrProductDemandChildId">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="SNo" HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Item Category">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblItemCatName" Text='<%# Eval("ItemCatName")%>' runat="server" />
                                                                                <asp:Label ID="lblItemCat_id" Visible="false" Text='<%# Eval("ItemCat_id")%>' runat="server" />
                                                                                <asp:Label ID="lblDemandStatus" Visible="false" Text='<%# Eval("Demand_Status")%>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Item Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblIName" Text='<%# Eval("ItemName")%>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Total Demand Qty.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTotalQty" Text='<%# Eval("TotalQty")%>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Total Supply Qty.">
                                                                            <ItemTemplate>
                                                                                 <asp:Label ID="lblSupplyTotalQty" runat="server" Text='<%# Eval("SupplyTotalQty")%>'/>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </asp:Panel>--%>
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
                                    <%-- <div class="col-md-12">
                                    <div class="table-responsive">
                                        <div id="divStringBuilder" runat="server"></div>
                                    </div>
                                </div>--%>
                                 <%--   <div class="col-md-12">
                                                      <label>  Total Demand :</label>
                                                        <asp:Label ID="lblTotalDemandValue" Font-Bold="true" runat="server"></asp:Label>
                                                    </div>--%>
                                  <%--  <div class="col-md-3" id="pnlSearchBy" runat="server" visible="true">
                                <div class="form-group">
                                        <label></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                ErrorMessage="Select anyone from Self Or Transportation" Text="<i class='fa fa-exclamation-circle' title='Select anyone from Self Or Transportation !'></i>"
                                                ControlToValidate="rblSorT" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:RadioButtonList ID="rblSorT" AutoPostBack="true" OnSelectedIndexChanged="rblSorT_SelectedIndexChanged" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem class="radio-inline" Text="Self &nbsp;&nbsp;" Value="1"></asp:ListItem> 
                                            <asp:ListItem class="radio-inline" Text="Transportation" Value="2"></asp:ListItem>
                                            
                                        </asp:RadioButtonList>
                                    </div>
                                </div> 
                                       <div class="col-md-2" id="pnlvehicleno" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>Vehicle No. </label>
                                            <span class="pull-right">
                                                <asp:LinkButton ID="lnkVehicle" CausesValidation="false" OnClick="lnkVehicle_Click" ToolTip="Add New Vehicle Details" runat="server"><b>[Add]</b></asp:LinkButton>
                                                <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Vehicle No." Text="<i class='fa fa-exclamation-circle' title='Select Vehicle No. !'></i>"
                                                    ControlToValidate="ddlVehicleNo" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                            </span>
                                            <asp:DropDownList ID="ddlVehicleNo" OnInit="ddlVehicleNo_Init" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>--%>
                                                    
                                                  
                                </div>
                                <div class="row">
                                    
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" OnClientClick="return ValidatePage();" AccessKey="S" ID="btnSubmit" Text="Approve" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                        </div>

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
                                                                     <asp:TemplateField HeaderText="Remark/ टिप्पणी">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRemarkAtSupply" Text='<%# Eval("RemarkAtSupply")%>' runat="server" />
                                                                                 <asp:TextBox runat="server" autocomplete="off" Visible="false" CausesValidation="true" CssClass="form-control" ID="txtRemarkAtSupply" MaxLength="200" placeholder="Enter Remark" ClientIDMode="Static"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Action / कार्यवाही करें">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkEdit" CommandName="RecordEdit" CommandArgument='<%#Eval("MilkOrProductDemandChildId") %>' runat="server" ToolTip="Edit"><i class="fa fa-pencil"></i></asp:LinkButton>
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
                                  
                                    <button type="button" class="btn btn-default" data-dismiss="modal">CLOSE </button>
                                </div>
                            </div>
                            <!-- /.modal-content -->
                        </div>
                        <!-- /.modal-dialog -->
                    </div>
             <%--<asp:ValidationSummary ID="v1" runat="server" ValidationGroup="save" ShowMessageBox="true" ShowSummary="false" />

            <div class="modal" id="VehicleDetailsModal">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 340px;">
                        <div class="modal-header">
                              <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Vehicle Details</h4>
                        </div>
                        <div class="modal-body">

                          
                                    <div class="row" style="height: 200px; overflow: scroll;">
                                       
                                       <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Type<span style="color: red;"> *</span></label>
                                             <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="save"
                                                InitialValue="0" ErrorMessage="Select Type" Text="<i class='fa fa-exclamation-circle' title='Select Type !'></i>"
                                                ControlToValidate="ddlVendorType" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlVendorType" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="ddlVendorType_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                       </div>
                        <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Name<span style="color: red;"> *</span></label>
                                             <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="save"
                                                InitialValue="0" ErrorMessage="Select Name" Text="<i class='fa fa-exclamation-circle' title='Select Name !'></i>"
                                                ControlToValidate="ddlVendorName" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlVendorName" Width="200px" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                       </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Vehicle No.<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="save"
                                        ErrorMessage="Enter Vehicle No." Text="<i class='fa fa-exclamation-circle' title='Enter Vehicle No. !'></i>"
                                        ControlToValidate="txtVehicleNo" ForeColor="Red" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtVehicleNo" Display="Dynamic" 
                                        ValidationExpression="^[A-Z|a-z]{2}-\d{2}-[A-Z|a-z]{1,2}-\d{4}$" runat="server"
                                         Text="<i class='fa fa-exclamation-circle' title='Invalid vehicle no. format (XX-00-XX-0000)!'></i>"
                                         ErrorMessage="Invalid vehicle no. format (XX-00-XX-0000)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="save">
                                    </asp:RegularExpressionValidator>

                                </span>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtVehicleNo" placeholder="XX-00-XX-000" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                            <div class="col-md-3">
                            <div class="form-group">
                                <label>Vehicle Type<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="save"
                                        ErrorMessage="Enter Vehicle Type" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Vehicle Type. !'></i>"
                                        ControlToValidate="txtVehicleType" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ValidationGroup="save"
                                        ErrorMessage="Invalid Vehicle Type. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Invalid Vehicle Type !'></i>" ControlToValidate="txtVehicleType"
                                        ValidationExpression="^[a-zA-Z0-9\s]+$">
                                    </asp:RegularExpressionValidator>

                                </span>

                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtVehicleType" MaxLength="10" placeholder="Enter Vehicle Type"></asp:TextBox>
                            </div>
                        </div>
                         <div class="col-md-1">
                            <div class="form-group">
                               <label> IsActive</label>

                              <asp:CheckBox ID="chkIsActive" CssClass="form-control" Checked="true" runat="server" />
                            </div>
                        </div>
                                         <div class="col-md-11">
                                              <asp:Label ID="lblModalMsg1" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="save" ID="btnSaveVehicleDetails" Text="Save" OnClientClick="return ValidateT()" />
                           
                        </div>
                    </div>
                 
                </div>
            
            </div> --%>

      <%--<div class="modal fade" id="myModalT" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #d9d9d9;">
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                    </button>
                    <h4 class="modal-title" id="myModalLabelT">Confirmation</h4>
                </div>
                <div class="clearfix"></div>
                <div class="modal-body">
                    <p>
                        <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp;
                            <asp:Label ID="lblPopupT" runat="server"></asp:Label>
                    </p>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYesV" OnClick="btnYesV_Click" Style="margin-top: 20px; width: 50px;" />
                    <asp:Button ID="Button2" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>--%>

        </section>
     

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
   
    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../../images/minus.png");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "../../images/plus.png");
            $(this).closest("tr").next().remove();
        });
        function myItemDetailsModal() {
            $("#ItemDetailsModal").modal('show');

        }
        function myVehicleDetailsModal() {
            $("#VehicleDetailsModal").modal('show');

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

       <%-- function ValidateT() {
            debugger
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('save');
            }

            if (Page_IsValid) {

               
                if (document.getElementById('<%=btnSaveVehicleDetails.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupT.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModalT').modal('show');
                    return false;
                }
            }
        }--%>
        //function FetchData(button) {
        //    var row = button.parentNode.parentNode;
        //    var label1 = GetChildControl(row, "txtGEN").value;
        //    var label2 = GetChildControl(row, "txSCTarget").value;

        //    if (label1 == '') {
        //        label1 = 0;

        //    }
        //    if (label2 == '') {

        //        label2 = 0;
        //    }




        //    if (parseInt(label1) < parseInt(label1)) {
        //        alert('Total Target Loan not less than zero');
        //        GetChildControl(row, "txtGEN").value = ''
        //        GetChildControl(row, "txSCTarget").value = '';
        //        GetChildControl(row, "txtGEN").focus();

        //    }
        //    else {
        //        GetChildControl(row, "txtDistrictTarget").value = parseInt(label1);
        //        GetChildControl(row, "HFTotalDistrictTarget").value = Multi;

        //    }



        //    return false;
        //};

        //function GetChildControl(element, id) {
        //    var child_elements = element.getElementsByTagName("*");
        //    for (var i = 0; i < child_elements.length; i++) {
        //        if (child_elements[i].id.indexOf(id) != -1) {
        //            return child_elements[i];
        //        }
        //    }
        //};
        
    </script>
</asp:Content>
