<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ModifyMilkOrProductOrderByDistributor.aspx.cs" Inherits="mis_Demand_ModifyMilkOrProductOrderByDistributor" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

  <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="col-md-6">
                <div class="box box-Manish">
                    <div class="box-header">
                        <h3 class="box-title">Modify Daily Placed Order / दैनिक आर्डर को संशोधित करें</h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <fieldset>
                            <legend>Date ,Shift ,Category / दिनांक ,शिफ्ट , वर्ग
                            </legend>
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>
                            </div>


                            <div class="row">

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Date / दिनांक<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                ControlToValidate="txtOrderDate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="vgdmissue" runat="server" Display="Dynamic" ControlToValidate="txtOrderDate"
                                                ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOrderDate" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Shift / शिफ्ट<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                InitialValue="0" ErrorMessage="Select Shift" Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"
                                                ControlToValidate="ddlShift" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlShift" OnInit="ddlShift_Init" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>



                                
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Retailer/विक्रेता<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                        InitialValue="0" ErrorMessage="Select Retailer" Text="<i class='fa fa-exclamation-circle' title='Select Retailer !'></i>"
                                        ControlToValidate="ddlRetailer" ForeColor="Red" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator></span>

                                <asp:DropDownList ID="ddlRetailer" OnInit="ddlRetailer_Init" runat="server" CssClass="form-control select2"></asp:DropDownList>
                              

                            </div>
                        </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Category / वर्ग</label>
                                       <%-- <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                InitialValue="0" ErrorMessage="Select Category" Text="<i class='fa fa-exclamation-circle' title='Select Category !'></i>"
                                                ControlToValidate="ddlItemCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>

                                        </span>--%>
                                        <asp:DropDownList ID="ddlItemCategory" OnInit="ddlItemCategory_Init" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2" style="margin-top: 22px;">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-primary" ID="btnSearch" ValidationGroup="a" Text="Search" OnClick="btnSearch_Click" AccessKey="S" />
                                    </div>
                                </div>
                                <div class="col-md-2" style="margin-top: 22px;">
                                    <div class="form-group">
                                        <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-block btn-default" />
                                    </div>
                                </div>
                            </div>

                        </fieldset>

                    </div>

                </div>
            </div>
            <div class="col-md-6" id="pnldata" runat="server" visible="false">
                <div class="box box-Manish">
                    <div class="box-header">
                        <h3 class="box-title">Modify Item Details / वस्तु के विवरण को संशोधित करें </h3>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <asp:Label ID="lblUpdateMsg" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="row">
                            
                                <div class="col-lg-12">
                                    <fieldset>
                                <legend>Item Details / वस्तु का विवरण</legend>

                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" EmptyDataText="No Record Found" class="table table-striped table-bordered" AllowPaging="false"
                                            AutoGenerateColumns="False" EnableModelValidation="True" DataKeyNames="MilkOrProductDemandChildId">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo./ क्र." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
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
                                                        <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                            ErrorMessage="Enter Item Qty." Enabled="false" Text="<i class='fa fa-exclamation-circle' title='Enter Item Qty. !'></i>"
                                                            ControlToValidate="txtItemQty" ForeColor="Red" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="rev1" Enabled="false" runat="server" Display="Dynamic" ValidationGroup="a"
                                                            ErrorMessage="Invalid Item Qty, only numeric allow. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Invalid Item Qty, only numeric allow. !'></i>" ControlToValidate="txtItemQty"
                                                            ValidationExpression="^[0-9]+$">
                                                        </asp:RegularExpressionValidator>
                                                        </span>
                                               <asp:TextBox runat="server" autocomplete="off" Visible="false" CausesValidation="true" Text='<%# Eval("ItemQty")%>' CssClass="form-control" ID="txtItemQty" MaxLength="5" onkeypress="return validateNum(event);" placeholder="Enter Item Qty." ClientIDMode="Static"></asp:TextBox>
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
                                                <asp:TemplateField HeaderText="Action / कार्यवाही करें">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" CommandName="RecordEdit" CommandArgument='<%#Eval("MilkOrProductDemandChildId") %>' runat="server" ToolTip="Edit"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                        &nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkUpdate" Visible="false" ValidationGroup="a" CausesValidation="true" CommandName="RecordUpdate" CommandArgument='<%#Eval("MilkOrProductDemandChildId") %>' runat="server" ToolTip="Update" Style="color:darkgreen;" OnClientClick="return confirm('Are you sure to Update?')"><i class="fa fa-check"></i></asp:LinkButton>
                                                        &nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkReset" Visible="false" CommandName="RecordReset" CommandArgument='<%#Eval("MilkOrProductDemandChildId") %>' runat="server" ToolTip="Reset" Style="color: gray;"><i class="fa fa-times"></i></asp:LinkButton>
                                                        &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("MilkOrProductDemandChildId") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                          </fieldset>
                                </div>
                          
                        </div>

                    </div>
                </div>
            </div>
        </section>
        <!-- /.content -->

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        $("#txtOrderDate").datepicker({
            autoclose: true
        });
    </script>
</asp:Content>
