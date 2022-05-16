<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ProductOrderApprovalList.aspx.cs" Inherits="mis_Demand_ProductOrderApprovalList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <%--<link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />--%>
    <link href="../Finance/css/jquery.dataTables.min.css" rel="stylesheet" />
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

         function chkedit(objRef) {
             var GridView = objRef.parentNode.parentNode.parentNode;
             var inputList = GridView.getElementsByTagName("input");
             for (var i = 0; i < 1; i++) {
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
         </script>
    <script>
        function checkAllbox(objRef) {
            var GridView = document.getElementById("<%=GridView1.ClientID %>");
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
    <div class="loader"></div>
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnApp_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>

        </div>
    </div>
    <%--ConfirmationModal End --%>

    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="c" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="d" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="ModalSave" ShowMessageBox="true" ShowSummary="false" />

    <div class="content-wrapper">
        <section class="content">
            <div class="row">


                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Product Approval List </h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" CssClass="Autoclr" runat="server"></asp:Label>
                                </div>
                            </div>
                            <fieldset>
                                <legend>Route,Date,Shift,Item Category
                                </legend>
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Date / दिनांक<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    ControlToValidate="txtOrderDate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtOrderDate"
                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOrderDate" MaxLength="10" placeholder="Enter Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Shift / शिफ्ट</label>

                                            <%--<asp:DropDownList ID="ddlShift" OnInit="ddlShift_Init" runat="server" ClientIDMode="Static" CssClass="form-control select2"></asp:DropDownList>--%>
                                            <asp:DropDownList ID="ddlShift" runat="server" ClientIDMode="Static" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Item Category /वस्तु वर्ग</label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                    ErrorMessage="Select Item Category" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Item Category !'></i>"
                                                    ControlToValidate="ddlItemCategory" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <%--<asp:DropDownList ID="ddlItemCategory" OnInit="ddlItemCategory_Init" runat="server" CssClass="form-control select2"></asp:DropDownList>--%>
                                            <asp:DropDownList ID="ddlItemCategory" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Location<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Location" Text="<i class='fa fa-exclamation-circle' title='Select Location !'></i>"
                                                    ControlToValidate="ddlLocation" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Route  / रूट </label>
                                            <%--<asp:DropDownList ID="ddlRoute" OnInit="ddlRoute_Init" runat="server" CssClass="form-control select2"></asp:DropDownList>--%>
                                            <asp:DropDownList ID="ddlRoute" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>DM Type</label>
                                            <%-- <asp:DropDownList ID="ddlDMType" runat="server" TabIndex="4" CssClass="form-control select2">
                                               
                                            </asp:DropDownList>--%>
                                            <asp:ListBox runat="server" ID="ddlDMType" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple">
                                                <asp:ListItem Text="Regular Demand" Selected="True" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Current Demand" Selected="True" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Current Ghee Demand" Selected="True" Value="2"></asp:ListItem>

                                            </asp:ListBox>
                                        </div>
                                    </div>

                                    <div class="col-md-1">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" OnClick="btnSearch_Click" ValidationGroup="a" ID="btnSearch" Text="Search" />
                                        </div>
                                    </div>
                                    <div class="col-md-1" style="margin-top: 20px;">
                                        <div class="form-group">
                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Reset" CssClass="btn btn-block btn-secondary" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div class="row">


                            <div class="col-md-12" id="pnlsearch" runat="server" visible="false">
                                <fieldset>
                                    <legend>Order Approval List
                                    </legend>
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" class="datatable table table-striped table-bordered" AllowPaging="false"
                                            AutoGenerateColumns="False" EmptyDataText="No Record Found." OnRowCommand="GridView1_RowCommand" EnableModelValidation="True" DataKeyNames="MilkOrProductDemandId">
                                            <Columns>
                                                <asp:TemplateField HeaderText="" ItemStyle-Width="30" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="checkAll" runat="server" onclick="checkAllbox(this);" />
                                                        <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="b" ErrorMessage="Please select at least one record."
                                                            ClientValidationFunction="Validate" Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Please select at least one record. !'></i>" ForeColor="Red"></asp:CustomValidator>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" Visible='<%# Convert.ToInt32(Eval("Demand_Status")) == 1 ? true : false %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        <asp:Label ID="lblMilkOrProductDemandId" Visible="false" Text='<%# Eval("MilkOrProductDemandId")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order Id">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblOrderId" CommandName="ItemOrdered" CommandArgument='<%#Eval("MilkOrProductDemandId") %>' Text='<%#Eval("OrderId") %>' runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Route No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRName" Text='<%# Eval("RName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Retailer/Institution Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBandOName" Text='<%# Eval("BandOName")%>' runat="server" />
                                                        <asp:Label ID="lblItemCatid" Visible="false" Text='<%# Eval("ItemCat_id")%>' runat="server" />
                                                        <asp:Label ID="lblDStatus" Visible="false" Text='<%# Eval("Demand_Status")%>' runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order Shift">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblShiftName" Text='<%# Eval("ShiftName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Category" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemCatName" Text='<%# Eval("ItemCatName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDemandDate" Text='<%# Eval("Demand_Date")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order Type" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPDMStatus" Text='<%#Eval("PDMStatus") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order Status" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDemandStatus" Text='<%# Eval("DStatus")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkViewDetails" CommandName="ItemOrdered" CommandArgument='<%#Eval("MilkOrProductDemandId") %>' runat="server"><i class="fa fa-eye fa"></i></asp:LinkButton>
                                                        &nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkAddItem" Visible='<%# Eval("Demand_Status").ToString()=="1" ? true: false %>' CssClass="text-aqua" CommandName="AddOrdered" CommandArgument='<%#Eval("MilkOrProductDemandId") %>' runat="server"><i class="fa fa- fa-cart-plus fa-2x"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <%--<div class="row" id="pnlapproval" runat="server" visible="false">
                                        
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Route No<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="b"
                                                        ErrorMessage="Select Route No" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Route No !'></i>"
                                                        ControlToValidate="ddlRouteApproved" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlRouteApproved" OnSelectedIndexChanged="ddlRouteApproved_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Distributor/SuperStockist Name<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="b"
                                                        ErrorMessage="Select Distributor/SuperStockist" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Distributor/SuperStockist !'></i>"
                                                        ControlToValidate="ddlDistOrSSName" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlDistOrSSName" runat="server" CssClass="form-control select2">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        
                                    </div>--%>
                                    <div class="col-md-2" style="margin-top: 20px;">
                                        <div class="form-group">
                                            <asp:Button ID="btnApp" runat="server" OnClientClick="return ValidatePage();" ValidationGroup="b" Text="Approved" CssClass="btn btn-block btn-primary" />
                                        </div>
                                    </div>
                                </fieldset>

                                <div class="modal" id="ItemDetailsModal">
                                    <div class="modal-dialog modal-lg">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">×</span></button>
                                                <h4 class="modal-title">Item Details for <span id="modalBoothName" style="color: red" runat="server"></span>&nbsp;&nbsp;
                             Order Date :<span id="modaldate" style="color: red" runat="server"></span>&nbsp;&nbsp;Order Shift :<span id="modalshift" style="color: red" runat="server"></span>
                                                    &nbsp;&nbsp;Order Status :<span id="modalorderStatus" runat="server"></span>
                                                </h4>
                                            </div>
                                            <div class="modal-body">
                                                <div id="divitem" runat="server">
                                                    <asp:Label ID="lblModalMsg" runat="server" Text=""></asp:Label>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <fieldset>
                                                                <legend>Item Details</legend>
                                                                <%--<div class="row">
                                                                    <asp:ScriptManager ID="sm1" runat="server"></asp:ScriptManager>
                                                                    <asp:UpdatePanel ID="pnlroutiedis" runat="server">
                                                                        <ContentTemplate>

                                                                      
                                                                    <div class="col-md-4">
                                                                        <label>Route No<span style="color: red;"> *</span></label>
                                                                        <div class="form-group">
                                                                            <span class="pull-right">
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="d"
                                                                                    ErrorMessage="Select Route No" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Route No !'></i>"
                                                                                    ControlToValidate="ddlModalRoute" Display="Dynamic" runat="server" InitialValue="0">
                                                                                </asp:RequiredFieldValidator>
                                                                            </span>
                                                                           
                                                                            <asp:DropDownList ID="ddlModalRoute" AutoPostBack="true" OnSelectedIndexChanged="ddlModalRoute_SelectedIndexChanged" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <label>Distributor/SuperStockist Name<span style="color: red;"> *</span></label>
                                                                        <div class="form-group">
                                                                            <span class="pull-right">
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="d"
                                                                                    ErrorMessage="Select Distributor/SuperStockist" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Distributor/SuperStockist !'></i>"
                                                                                    ControlToValidate="ddlModalDistOrSS" Display="Dynamic" runat="server" InitialValue="0">
                                                                                </asp:RequiredFieldValidator>
                                                                            </span>
                                                                            <asp:DropDownList ID="ddlModalDistOrSS" runat="server" CssClass="form-control">
                                                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                              </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                </div>--%>
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
                                                                                    <asp:TemplateField HeaderText="Select">
                                                                                         <HeaderTemplate>
                                                                                 <asp:CheckBox ID="checkAll" Checked="false" runat="server" onclick="checkAll(this);" AutoPostBack="true" OnCheckedChanged="chkedit_CheckedChanged" />
                                                                            </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkedit" AutoPostBack="true" runat="server" Checked="false" onclick="chkedit(this);" OnCheckedChanged="chkedit_CheckedChanged" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Action / कार्यवाही करें" Visible="false">
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
                                                                </div>
                                                            </fieldset>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <div class="row">
                                                    <div class="col-md-5"></div>
                                                    <%-- <div class="col-md-2">
                                                        <div class="form-group">
                                                            <asp:Button ID="btnApproved" CssClass="btn-block btn btn-success" ValidationGroup="d" OnClick="btnApproved_Click" Text="Approve" runat="server" />
                                                          
                                                        </div>

                                                    </div>--%>
                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                             <asp:Button ID="lnkupdate" OnClick="lnkupdate_Click" Visible="false" CssClass="btn btn-success" OnClientClick="return confirm('Are you sure to Update?')" Text="Update" runat="server"></asp:Button> <br />
                                                            <asp:Button ID="btnReject" CssClass="btn-block btn btn-danger" OnClientClick="return confirm('Do you want to Reject Order?')" OnClick="btnReject_Click" Text="Reject" runat="server" />
                                                        </div>

                                                    </div>


                                                </div>
                                                <%--<button type="button" class="btn btn-default" data-dismiss="modal">CLOSE </button>--%>
                                            </div>
                                        </div>
                                        <!-- /.modal-content -->
                                    </div>
                                    <!-- /.modal-dialog -->
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal" id="AddItemDetailsModal">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 250px;">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Add Item Details for <span id="modalpartyname" style="color: red" runat="server"></span>&nbsp;&nbsp;
                             Order Date :<span id="partymodaldate" style="color: red" runat="server"></span>&nbsp;&nbsp;
                                                    &nbsp;&nbsp;Order Status :<span id="partymodalstatus" runat="server"></span>
                            </h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Item Name <span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="ModalSave"
                                                InitialValue="0" ErrorMessage="Select Item Name" Text="<i class='fa fa-exclamation-circle' title='Select Item Name !'></i>"
                                                ControlToValidate="ddlItemName" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlItemName" Width="200px" CssClass="form-control select2" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Total Qty.<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="ModalSave"
                                                ErrorMessage="Enter Total Qty." Text="<i class='fa fa-exclamation-circle' title='Enter Total Qty. !'></i>"
                                                ControlToValidate="txtTotalQty" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ValidationGroup="ModalSave"
                                                ErrorMessage="Invalid Total Qty. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Invalid Total Qty. !'></i>" ControlToValidate="txtTotalQty"
                                                ValidationExpression="^[0-9]+$">
                                            </asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" onkeypress="return validateNum(event);" CssClass="form-control" MaxLength="6" ID="txtTotalQty" placeholder="Enter Total Qty." ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-1" style="margin-top: 20px;">
                                    <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="ModalSave" OnClick="btnAddItem_Click" ID="btnAddItem" Text="Add" />
                                </div>
                                <div class="col-md-5">
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
            <%--Confirmation Modal Start --%>
            <%--<div class="modal fade" id="myModal1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel1" aria-hidden="true">
                <div style="display: table; height: 100%; width: 100%;">
                    <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                        <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                            <div class="modal-header" style="background-color: #d9d9d9;">
                                <button type="button" class="close" data-dismiss="modal">
                                    <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                                </button>
                                <h4 class="modal-title" id="myModalLabel1">Confirmation</h4>
                            </div>
                            <div class="clearfix"></div>
                            <div class="modal-body">
                                <p>
                                    <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp; 
                            <asp:Label ID="lblPopupAlert1" runat="server"></asp:Label>
                                </p>
                            </div>
                            <div class="modal-footer">
                                <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="Button1" OnClick="btnApproved_Click" Style="margin-top: 20px; width: 50px;" />
                                <asp:Button ID="Button2" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>

                </div>
            </div>--%>
            <%--ConfirmationModal End --%>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
     <asp:Label runat="server" Visible="false" ID="lblcheckcount"></asp:Label>
       <asp:Label runat="server" Visible="false" ID="lblMilkOrProductDemandId"></asp:Label>
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../js/bootstrap-multiselect.js"></script>
    <script src="../Finance/js/jquery.dataTables.min.js"></script>
    <script src="../Finance/js/dataTables.bootstrap.min.js"></script>
    <script src="../Finance/js/dataTables.buttons.min.js"></script>
    <script src="../Finance/js/buttons.flash.min.js"></script>
    <script src="../Finance/js/jszip.min.js"></script>
    <script src="../Finance/js/pdfmake.min.js"></script>
    <script src="../Finance/js/vfs_fonts.js"></script>
    <script src="../Finance/js/buttons.html5.min.js"></script>
    <script src="../Finance/js/buttons.print.min.js"></script>
    <script src="js/buttons.colVis.min.js"></script>
    <%--<script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>--%>
    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);

        $(document).ready(function () {
            $('.loader').fadeOut();
        });

        $('.datatable').DataTable({
            paging: true,
            iDisplayLength: 500,
            lengthMenu: [10, 50, 100, 500, 1000],
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    title: ('Product Approval List').bold().fontsize(5).toUpperCase(),
                    text: '<i class="fa fa-print"></i> Print',
                    exportOptions: {
                        columns: [1, 2, 3, 4, 5, 6, 7, 8, 9]
                    },
                    footer: false,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    title: ('Product Approval List').bold().fontsize(5).toUpperCase(),
                    filename: 'Product List',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',

                    exportOptions: {
                        columns: [1, 2, 3, 4, 5, 6, 7, 8, 9]
                    },
                    footer: false
                }],
                dom: {
                    container: {
                        className: 'dt-buttons'
                    },
                    button: {
                        className: 'btn btn-default'
                    }
                }
            }
        });
        function myItemDetailsModal() {
            $("#ItemDetailsModal").modal('show');

        }

        function AddItemDetailsModal() {
            $("#AddItemDetailsModal").modal('show');

        }


        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('b');
            }

            if (Page_IsValid) {
                if (document.getElementById('<%=btnApp.ClientID%>').value.trim() == "Approved") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Approved this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }

        $(function () {
            $('[id*=ddlDMType]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });

        });
        <%--function ValidatePage1() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('d');
            }

            if (Page_IsValid) {
                if (document.getElementById('<%=btnApproved.ClientID%>').value.trim() == "Approve") {
                    document.getElementById('<%=lblPopupAlert1.ClientID%>').textContent = "Are you sure you want to Approved this record?";
                    $('#myModal1').modal('show');
                    return false;
                }
            }
        }--%>

    </script>
</asp:Content>


