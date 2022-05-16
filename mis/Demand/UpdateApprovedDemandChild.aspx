<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="UpdateApprovedDemandChild.aspx.cs" Inherits="mis_Demand_UpdateApprovedDemandChild" %>

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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
   <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
   <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="c" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">


                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Update Approved Demand -  List of Retailer/Institution </h3>
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
                                            <asp:Button ID="lnkbtnback" PostBackUrl="~/mis/Demand/UpdateApprovedDemand.aspx" Text="Back" CssClass="btn btn-secondary" runat="server" />
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
                                                    <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order Id">
                                                        <ItemTemplate>
                                                             <asp:LinkButton ID="lblOrderId" CommandName="ItemOrdered" CssClass="btn btn-secondary" CommandArgument='<%#Eval("tmp_MilkOrProductDemandId") %>' Text='<%#Eval("tmp_OrderId") %>' runat="server"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Retailer /Institution Name" HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBandOName" Text='<%# Eval("BandOName") %>' runat="server" />
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
                                                                         <asp:TemplateField HeaderText="Total Supply Qty./ कुल आपूर्ति मात्रा">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSupplyTotalQty" Text='<%# Eval("SupplyTotalQty")%>' runat="server" />                                                                               
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                           <asp:TemplateField HeaderText="Total Qty./ कुल मात्रा">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTotalQty" Text='<%# Eval("TotalQty")%>' runat="server" />
                                                                                <span class="pull-right">
                                                                                 <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="c"
                                                                                    ErrorMessage="Enter Total Qty." Enabled="false" Text="<i class='fa fa-exclamation-circle' title='Total Qty. !'></i>"
                                                                                    ControlToValidate="txtTotalQty" ForeColor="Red" Display="Dynamic" runat="server">
                                                                                </asp:RequiredFieldValidator>
                                                                                <asp:RegularExpressionValidator ID="rev1" Enabled="false" runat="server" Display="Dynamic" ValidationGroup="c"
                                                                                    ErrorMessage="Enter Valid Number In Quantity Field !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Number In Quantity Field !'></i>" ControlToValidate="txtTotalQty"
                                                                                    ValidationExpression="^[0-9]*$">
                                                                                </asp:RegularExpressionValidator>
                                                                                    <asp:CustomValidator ID="CustomValidator1" Enabled="false" runat="server" Display="Dynamic" ForeColor="red" ErrorMessage="Total Qyt. is equal to Total Supply Qty." Text="<i class='fa fa-exclamation-circle' title='Total Qyt. is equal to Total Supply Qty. !'></i>" OnServerValidate="CustomValidator1_ServerValidate" ValidateEmptyText="true" ValidationGroup="c"></asp:CustomValidator>
                                                                                 </span>
                                                                                 <asp:TextBox runat="server" autocomplete="off" Visible="false" CausesValidation="true" Text='<%# Eval("TotalQty")%>' CssClass="form-control" ID="txtTotalQty" MaxLength="5" onpaste="return false;" onkeypress="return validateNum(event);" placeholder="Enter Total Qty." ClientIDMode="Static"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                       
                                                                         <asp:TemplateField HeaderText="Remark/ टिप्पणी">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFTQRemark" Text='<%# Eval("FinalTotalQtyRemark")%>' runat="server" />
                                                                               <span class="pull-right">
                                                                                 <asp:RequiredFieldValidator ID="rfv2" ValidationGroup="c"
                                                                                    ErrorMessage="Enter Remark" Enabled="false" Text="<i class='fa fa-exclamation-circle' title='Enter Remark !'></i>"
                                                                                    ControlToValidate="txtTotalQtyRemark" ForeColor="Red" Display="Dynamic" runat="server">
                                                                                </asp:RequiredFieldValidator>
                                                                                 </span>
                                                                                 <asp:TextBox runat="server" autocomplete="off" Visible="false" CausesValidation="true" CssClass="form-control" ID="txtTotalQtyRemark" MaxLength="200" placeholder="Enter Remark" ClientIDMode="Static"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Action / कार्यवाही करें">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkEdit" CommandName="RecordEdit" Visible='<%# Eval("FinalTotalQtyRemark").ToString()=="" ? true: false %>' CommandArgument='<%#Eval("MilkOrProductDemandChildId") %>' runat="server" ToolTip="Edit"><i class="fa fa-pencil"></i></asp:LinkButton>
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
        </section>
        <!-- /.content -->

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
   
    <script type="text/javascript">
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
