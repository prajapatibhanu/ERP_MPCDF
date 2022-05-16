<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ModifyMilkorProductDemandBeforeGatePass.aspx.cs" Inherits="mis_Demand_ModifyMilkorProductDemandBeforeGatePass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .CapitalClass {
            text-transform: uppercase;
        }

        .capitalize {
            text-transform: capitalize;
        }

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
         .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('../image/loader/ProgressImage.gif') 50% 50% no-repeat rgba(0, 0, 0, 0.3);
        } 
    </style>
        <script>
     function CheckOne(obj) {
            var grid = obj.parentNode.parentNode.parentNode;
            var inputs = grid.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox") {
                    if (obj.checked && inputs[i] != obj && inputs[i].checked) {
                        inputs[i].checked = false;
                    }
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
      <div class="loader"></div>
    <%--Confirmation Modal Start --%>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
     <asp:ValidationSummary ID="v1" runat="server" ValidationGroup="ModalSave" ShowMessageBox="true" ShowSummary="false" />
     <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="ModalAddItem" ShowMessageBox="true" ShowSummary="false" />

    <div class="content-wrapper">
        <section class="content noprint">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Modify Demand </h3>

                             
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>
                            </div>
                            <fieldset>
                                    <legend>Modify Demand
                                    </legend>
                            <div class="row">
                                
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Date<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    ControlToValidate="txtDate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="b" runat="server" Display="Dynamic" ControlToValidate="txtDate"
                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" OnTextChanged="txtDate_TextChanged" AutoPostBack="true" CssClass="form-control" ID="txtDate" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static" TabIndex="1" ></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Shift </label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Shift" Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"
                                                    ControlToValidate="ddlShift" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlShift" runat="server" OnSelectedIndexChanged="ddlShift_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static" CssClass="form-control select2" TabIndex="2"></asp:DropDownList>
                                        </div>
                                    </div>
                                 <div class="col-md-2">
                            <div class="form-group">
                                <label>Item Category <span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvic" ValidationGroup="a"
                                        InitialValue="0" ErrorMessage="Select Item Category" Text="<i class='fa fa-exclamation-circle' title='Select Item Category !'></i>"
                                        ControlToValidate="ddlItemCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator></span>
                                <asp:DropDownList ID="ddlItemCategory" AutoPostBack="true" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged" ClientIDMode="Static" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Distributor Name<span style="color: red;">*</span></label>
                                         <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select Distributor Name" Text="<i class='fa fa-exclamation-circle' title='Select Distributor Name !'></i>"
                                            ControlToValidate="ddlDistributorName" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                         <asp:DropDownList ID="ddlDistributorName" ClientIDMode="Static" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                      <%--  <asp:TextBox runat="server" autocomplete="off" CssClass="form-control capitalize ui-autocomplete-12" MaxLength="13" ID="txPartyName" ClientIDMode="Static"></asp:TextBox>
                                        <asp:HiddenField ID="hfvehicleNo" runat="server" ClientIDMode="Static" />
                                                    <small><span id="valtxtLedgerName" style="color: red;"></span></small>--%>
                                    </div>
                                </div>
                                <div class="col-md-1" style="margin-top:20px;">
                                        <div class="form-group">
                                            <asp:Button ID="btnSearch" ValidationGroup="a" OnClick="btnSearch_Click" CssClass="btn btn-primary" Text="Search" runat="server" />
                                            </div>
                                    </div>
                            </div>
                                 <asp:panel id="pnloderdetails" runat="server" visible="false">
                             <asp:GridView ID="GridViewOrderDetails" runat="server" OnRowCommand="GridViewOrderDetails_RowCommand" class="table table-striped table-bordered" AllowPaging="false"
                                                AutoGenerateColumns="false" EmptyDataText="No Record Found." EnableModelValidation="True">
                                                <Columns>
                                                <asp:TemplateField HeaderText="" ItemStyle-Width="30" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="b" ErrorMessage="Please select at least one record."
                                                            ClientValidationFunction="Validate" Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Please select at least one record. !'></i>" ForeColor="Red"></asp:CustomValidator>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" onclick="CheckOne(this)" OnCheckedChanged="chkSelect_CheckedChanged"  AutoPostBack="true" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order Id" HeaderStyle-Width="5px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOrderId" Text='<%#Eval("OrderId") %>' runat="server" />
                                                            <asp:Label ID="lblMilkOrProductDemandId" Visible="false" Text='<%# Eval("MilkOrProductDemandId") %>' runat="server" />
                                                        </ItemTemplate>
                                                        </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Retailer Name" HeaderStyle-Width="5px">
                                                         <ItemTemplate>
                                                            <asp:Label ID="lblBName" Text='<%#Eval("BName") %>' runat="server" />
                                                            <asp:Label ID="lblBoothId" Visible="false" Text='<%#Eval("BoothId") %>' runat="server" />
                                                        </ItemTemplate>
                                                        </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Vehicle No" HeaderStyle-Width="5px">
                                                         <ItemTemplate>
                                                            <asp:Label ID="lblVehicleNo" Text='<%#Eval("VehicleNo") %>' runat="server" />
                                                           
                                                             <asp:Label ID="lblAreaId" Visible="false" Text='<%#Eval("AreaId") %>' runat="server" />
                                                              <asp:Label ID="lblRouteId" Visible="false" Text='<%#Eval("RouteId") %>' runat="server" />
                                                             <asp:Label ID="lblDistributorId" Visible="false" Text='<%#Eval("DistributorId") %>' runat="server" />
                                                        </ItemTemplate>
                                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DM Type" HeaderStyle-Width="5px">
                                                         <ItemTemplate>
                                                            <asp:Label ID="lblPDMStatus" Text='<%#Eval("PDMStatus") %>' runat="server" />
                                                              <asp:Label ID="lblProductDMStatus" Visible="false" Text='<%#Eval("ProductDMStatus") %>' runat="server" />
                                                        </ItemTemplate>
                                                        </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="6px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkRejectDM" CssClass="btn btn-danger" OnClientClick="return confirm('Are you sure to Reject Order ?')" CommandName="DMReject" CommandArgument='<%#Eval("MilkOrProductDemandId") %>' runat="server"><i class="fa fa-trash-o"></i> Reject </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    </Columns>
                                                    </asp:GridView>
                                   </asp:panel>
                                </fieldset> 
                            <div class="row" id="pnldata" runat="server" visible="false">
                                <fieldset>
                                    <legend>Item Details
                                    </legend>
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <asp:LinkButton ID="lnkAddItem" runat="server" CssClass="btn btn-success" OnClick="lnkAddItem_Click" ClientIDMode="Static"><i class="fa fa- fa-cart-plus fa-2x"></i> Add Item</asp:LinkButton>
                                        </div>
                                         
                                    </div>
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
                                                                                             <asp:Label ID="lblMilkCurDMCrateIsueStatus" Visible="false" Text='<%# Eval("MilkCurDMCrateIsueStatus")%>' runat="server" />
                                                                                            <asp:Label ID="lblProductDMStatus" Visible="false" Text='<%# Eval("ProductDMStatus")%>' runat="server" />
                                                                                             <asp:Label ID="lblMilkOrProductDemandChildId" Visible="false" Text='<%# Eval("MilkOrProductDemandChildId")%>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Item Name / वस्तु नाम">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblIName" Text='<%# Eval("ItemName")%>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Qty./ मात्रा">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblItemQty" Text='<%# Eval("ItemQty")%>' runat="server" />
                                                                                            <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="c"
                                                                                                ErrorMessage="Enter Item Qty." Enabled="false" Text="<i class='fa fa-exclamation-circle' title='Enter Item Qty. !'></i>"
                                                                                                ControlToValidate="txtItemQty" ForeColor="Red" Display="Dynamic" runat="server">
                                                                                            </asp:RequiredFieldValidator>
                                                                                            <asp:RegularExpressionValidator ID="rev1" Enabled="false" runat="server" Display="Dynamic" ValidationGroup="c"
                                                                                                ErrorMessage="Enter Valid Number In Quantity Field & First digit can't be 0(Zero)!" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Number In Quantity Field & First digit can't be 0(Zero)!'></i>" ControlToValidate="txtItemQty"
                                                                                                ValidationExpression="^[0-9]*$">
                                                                                            </asp:RegularExpressionValidator>
                                                                                            </span>
                                               <asp:TextBox runat="server" autocomplete="off" Visible="false" CausesValidation="true" Text='<%# Eval("ItemQty")%>' CssClass="form-control" ID="txtItemQty" MaxLength="5" onpaste="return false;" onkeypress="return validateNum(event);" placeholder="Enter Item Qty." ClientIDMode="Static"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Advance Card / एडवांस कार्ड ">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblAdvCard" Text='<%# Eval("AdvCard")%>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Total Qty./ कुल मात्रा">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblTotalQty" Text='<%# Eval("TotalQty")%>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Remark/ टिप्पणी">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblRemarkAtOrderApproval" runat="server" Text='<%#Eval("RemarkAtOrderApproval") %>' />

                                                                                            <asp:TextBox runat="server" autocomplete="off" Visible="false" CssClass="form-control" ID="txtRemarkAtOrderApproval" MaxLength="200" placeholder="Enter Remark" ClientIDMode="Static"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Action / कार्यवाही करें">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="lnkEdit" CommandName="RecordEdit" CommandArgument='<%#Eval("MilkOrProductDemandChildId") %>' runat="server" ToolTip="Edit"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                                                            &nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkUpdate" Visible="false" ValidationGroup="c" CausesValidation="true" CommandName="RecordUpdate" CommandArgument='<%#Eval("MilkOrProductDemandChildId") %>' runat="server" ToolTip="Update" Style="color: darkgreen;" OnClientClick="return confirm('Are you sure to Update?')"><i class="fa fa-check"></i></asp:LinkButton>
                                                                                            &nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkReset" Visible="false" CommandName="RecordReset" CommandArgument='<%#Eval("MilkOrProductDemandChildId") %>' runat="server" ToolTip="Reset" Style="color: gray;"><i class="fa fa-times"></i></asp:LinkButton>
                                                                                            &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" Visible="false" CommandArgument='<%#Eval("MilkOrProductDemandChildId") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
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
                </div>
          <div class="modal" id="AddItemDetailsModal">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 280px;">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Add Item Details for <span id="modalparyname" style="color: orange" runat="server"></span>&nbsp;&nbsp;
                             Order Date :<span id="partymodaldate" style="color: orange" runat="server"></span>&nbsp;&nbsp;
                             &nbsp;&nbsp;Order No :<span id="partymodalOrderNo" style="color: orange" runat="server"></span>
                                  &nbsp;&nbsp;Shift :<span id="modalshift" style="color: orange" runat="server"></span>
                            </h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                             
                                   <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Item Name <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="ModalAddItem"
                                                    InitialValue="0" ErrorMessage="Select Item Name" Text="<i class='fa fa-exclamation-circle' title='Select Item Name !'></i>"
                                                    ControlToValidate="ddlItemName" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlItemName" Width="200px" CssClass="form-control select2" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                 <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Total Qty.<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="ModalAddItem"
                                                ErrorMessage="Enter Total Qty." Text="<i class='fa fa-exclamation-circle' title='Enter Total Qty. !'></i>"
                                                ControlToValidate="txtTotalQty" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ValidationGroup="ModalAddItem"
                                        ErrorMessage="Invalid Total Qty. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Invalid Total Qty. !'></i>" ControlToValidate="txtTotalQty"
                                        ValidationExpression="^[0-9]+$">
                                    </asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" onkeypress="return validateNum(event);" CssClass="form-control" MaxLength="6" ID="txtTotalQty" placeholder="Enter Total Qty." ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-1" style="margin-top:20px;">
                                      <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="ModalAddItem" OnClick="btnAddItem_Click" ID="btnAddItem" Text="Add" />
                                </div>
                               <div class="col-md-6">
                                   <asp:Label ID="lblModalMsg1" runat="server" Text=""></asp:Label>
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
        </section>

       

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
  <%--  <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
  <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script>
        $(document).ready(function () {

            debugger;
            $("#<%=txPartyName.ClientID %>").autocomplete({

                source: function (request, response) {
                    debugger
                  
                    $.ajax({
                        
                        url: '<%=ResolveUrl("ModifyMilkorProductDemandBeforeGatePass.aspx/SearchPartyName") %>',
                        data: "{ 'PartyName': '" + $('#txPartyName').val() + "','DDate': '" + $('#txtDate').val() + "','DShift': '" + $('#ddlShift').val() + "','Ditemcat': '" + $('#ddlItemCategory').val() + "'}",
                            //  var param = { ItemName: $('#txtItem').val() };
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {

                                response($.map(data.d, function (item) {
                                    return {
                                        label: item
                                        //val: item.split('-')[1]
                                    }
                                }))
                            },
                            error: function (response) {
                                alert(response.responseText);
                            },
                            failure: function (response) {
                                alert(response.responseText);
                            }
                        });
                    },
                select: function (e, i) {
                    $("#<%=hfvehicleNo.ClientID %>").val(i.item.val);
                    },
                    minLength: 1

            });

        });
    </script>--%>
    <script>

        $(document).ready(function () {
            $('.loader').fadeOut();
        });
        function AddItemDetailsModal() {
            $("#AddItemDetailsModal").modal('show');

        }
    </script>
</asp:Content>
