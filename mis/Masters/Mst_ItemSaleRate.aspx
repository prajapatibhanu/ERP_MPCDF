<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Mst_ItemSaleRate.aspx.cs" Inherits="mis_Masters_Mst_ItemSaleRate" %>

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
         function CheckAll(oCheckbox) {
             var GridView2 = document.getElementById("<%=GridView1.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
         }
         function CheckAll2(oCheckbox) {
             var GridView2 = document.getElementById("<%=GridView2.ClientID %>");
              for (i = 1; i < GridView2.rows.length; i++) {
                  GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
              }
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
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />


    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <!-- SELECT2 EXAMPLE -->
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Item Sale Rate </h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <fieldset>

                                <legend>Item Sale Rate</legend>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                      <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Item Category<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="b"
                                                    InitialValue="0" ErrorMessage="Select Category" Text="<i class='fa fa-exclamation-circle' title='Select Category !'></i>"
                                                    ControlToValidate="ddlItemCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlItemCategory" Enabled="false" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Location<span style="color: red;"> *</span></label>
                                             <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="b"
                                                InitialValue="0" ErrorMessage="Select Location" Text="<i class='fa fa-exclamation-circle' title='Select Location !'></i>"
                                                ControlToValidate="ddlLocation" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlLocation" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="col-md-2" id="pnlroute">
                                        <div class="form-group">
                                            <label>Route No<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="b"
                                                InitialValue="0" ErrorMessage="Select Route" Text="<i class='fa fa-exclamation-circle' title='Select Route !'></i>"
                                                ControlToValidate="ddlRoute" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlRoute" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>       
                                       <div class="col-md-2">
                                <div class="form-group">
                                        <label>For Institution</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="b" InitialValue="0"
                                               ErrorMessage="Select For Institution" Text="<i class='fa fa-exclamation-circle' title='Select For Institution !'></i>"
                                                ControlToValidate="ddlForInstitution" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                      <asp:DropDownList ID="ddlForInstitution" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlForInstitution_SelectedIndexChanged" runat="server">
                                          <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                          <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                          <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                      </asp:DropDownList>
                                    </div>
                                </div>   
                                     <div class="col-md-2" id="pnlIist" runat="server" visible="false">
                            <div class="form-group">
                                <label>Institution Name<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="b"
                                        InitialValue="0" ErrorMessage="Select Institution Name" Text="<i class='fa fa-exclamation-circle' title='Select Institution Name !'></i>"
                                        ControlToValidate="ddlIntitution" ForeColor="Red" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator></span>
                                <asp:DropDownList ID="ddlIntitution" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                        </div>
                                  <div class="col-md-1" style="margin-top:20px;">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-primary" OnClick="btnSearch_Click" ValidationGroup="b" ID="btnSearch" Text="Search" AccessKey="S" />
                                    </div>
                                </div> 
                                     <div class="col-md-1" style="margin-top:20px;">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-secondary" OnClick="btnClear_Click" ID="btnClear" Text="Clear" AccessKey="C" />
                                    </div>
                                </div>    
                                
                                </div>
                               
                                <div class="row" id="pnlproduct" runat="server" visible="false">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" EmptyDataText="No records Found" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover">
                                    <Columns>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <input id="Checkbox2" type="checkbox" onclick="CheckAll(this)" runat="server" />
                                                All
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Name">
                                            <ItemTemplate>
                                                  <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                  <asp:Label ID="lblItemCat_id" Visible="false" runat="server" Text='<%# Eval("ItemCat_id") %>'></asp:Label>
                                                <asp:Label ID="lblItem_id" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Consumer Rate" HeaderStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtConsumerRate" Width="50px" onfocusout="FetchData1(this)" autocomplete="off" onkeypress="return validateDecThreeplace(this,event)" MaxLength="10" placeholder="Enter Consumer Rate" ValidationGroup="a" Text='<%# Eval("ConsumerRate") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator104I" ValidationGroup="a"
                                                        ErrorMessage="Enter Consumer Rate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Consumer Rate!'></i>"
                                                        ControlToValidate="txtConsumerRate" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator54J" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,3})?$" ValidationGroup="a" runat="server" ControlToValidate="txtConsumerRate" ErrorMessage="Enter Valid Consumer Rate" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Consumer Rate!'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Retailer Margin" HeaderStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRetailerComm" Width="50px" onfocusout="FetchData1(this)" autocomplete="off" onkeypress="return validateDecThreeplace(this,event)" MaxLength="10" placeholder="Enter Retailer Comm." ValidationGroup="a" Text='<%# Eval("RetailerComm") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator103I" ValidationGroup="a"
                                                        ErrorMessage="Enter Retailer Margin" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Retailer Margin!'></i>"
                                                        ControlToValidate="txtRetailerComm" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator53J" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,3})?$" ValidationGroup="a" runat="server" ControlToValidate="txtRetailerComm" ErrorMessage="Enter Valid Retailer Comm." Text="<i class='fa fa-exclamation-circle' title='Enter Valid Retailer Comm.!'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Retailer Rate" HeaderStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRetailerRate" Width="50px" autocomplete="off" onkeypress="return validateDecThreeplace(this,event)" MaxLength="10" placeholder="Enter Retailer Rate" ValidationGroup="a" Text='<%# Eval("RetailerRate") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator102G" ValidationGroup="a"
                                                        ErrorMessage="Enter Retailer Rate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Retailer Rate!'></i>"
                                                        ControlToValidate="txtRetailerRate" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator52H" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,3})?$" ValidationGroup="a" runat="server" ControlToValidate="txtRetailerRate" ErrorMessage="Enter Valid Retailer Rate" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Retailer Rate!'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Transport Margin" HeaderStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtTransComm" Width="50px" onfocusout="FetchData1(this)" autocomplete="off" onkeypress="return validateDecThreeplace(this,event)" MaxLength="10" placeholder="Enter Margin." ValidationGroup="a" Text='<%# Eval("TransComm") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="pull-right">
                                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator101E" ValidationGroup="a"
                                                        ErrorMessage="Enter Transportation Comm." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Transportation Comm.'></i>"
                                                        ControlToValidate="txtTransComm" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>--%>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator51F" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,3})?$" ValidationGroup="a" runat="server" ControlToValidate="txtTransComm" ErrorMessage="Enter Valid Transport Margin." Text="<i class='fa fa-exclamation-circle' title='Enter Valid Transport Margin!'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Distributor Margin" HeaderStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDistOrSSComm" Width="50px" onfocusout="FetchData1(this)" autocomplete="off" onkeypress="return validateDecThreeplace(this,event)" MaxLength="10" placeholder="Enter Distributor / SuperStockist Comm." ValidationGroup="a" Text='<%# Eval("DistOrSSComm") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator102C" ValidationGroup="a"
                                                        ErrorMessage="Enter Distributor Margin" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Distributor Margin!'></i>"
                                                        ControlToValidate="txtDistOrSSComm" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator52D" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,3})?$" ValidationGroup="a" runat="server" ControlToValidate="txtDistOrSSComm" ErrorMessage="Enter Valid Distributor Margin" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Distributor Margin!'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Distributor Rate" HeaderStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDistOrSSRate" Width="50px" autocomplete="off" onkeypress="return validateDecThreeplace(this,event)" MaxLength="10" placeholder="Enter Distributor Rate" ValidationGroup="a" Text='<%# Eval("DistOrSSRate") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator101A" ValidationGroup="a"
                                                        ErrorMessage="Enter Distributor Rate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Distributor Rate!'></i>"
                                                        ControlToValidate="txtDistOrSSRate" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator51B" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,3})?$" ValidationGroup="a" runat="server" ControlToValidate="txtDistOrSSRate" ErrorMessage="Enter Valid Distributor Rate" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Distributor Rate!'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="SS Trans Margin" HeaderStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSSTransMargin" Width="50px" onfocusout="FetchData2(this)" autocomplete="off" onkeypress="return validateDecThreeplace(this,event)" MaxLength="10" placeholder="Enter SS Tran Margin" ValidationGroup="a" Text='<%# Eval("SSTransMargin") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="pull-right">
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator51SSTM" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,3})?$" ValidationGroup="a" runat="server" ControlToValidate="txtSSTransMargin" ErrorMessage="Enter Valid SS Tran Margin" Text="<i class='fa fa-exclamation-circle' title='Enter Valid SS Tran Margin!'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SS Margin" HeaderStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSSMargin" Width="50px" onfocusout="FetchData2(this)" autocomplete="off" onkeypress="return validateDecThreeplace(this,event)" MaxLength="10" placeholder="Enter SS Margin" ValidationGroup="a" Text='<%# Eval("SSMargin") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="pull-right">
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator51SSM" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,3})?$" ValidationGroup="a" runat="server" ControlToValidate="txtSSMargin" ErrorMessage="Enter Valid SS Margin" Text="<i class='fa fa-exclamation-circle' title='Enter Valid SS Margin!'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SS Rate" HeaderStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSSRate" Width="50px" autocomplete="off" onkeypress="return validateDecThreeplace(this,event)" MaxLength="10" placeholder="Enter SS SSRate" ValidationGroup="a" Text='<%# Eval("SSRate") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="pull-right">
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator51SSR" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,3})?$" ValidationGroup="a" runat="server" ControlToValidate="txtSSRate" ErrorMessage="Enter Valid SS Rate" Text="<i class='fa fa-exclamation-circle' title='Enter Valid SS Rate!'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Advanced Card Rebate" HeaderStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAdvCardRebateComm" Width="50px" autocomplete="off" onkeypress="return validateDecThreeplace(this,event)" MaxLength="10" placeholder="Enter Advanced Card Rebate Margin" ValidationGroup="a" Text='<%# Eval("AdvCardRebateComm") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="pull-right">
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator51C" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,3})?$" ValidationGroup="a" runat="server" ControlToValidate="txtAdvCardRebateComm" ErrorMessage="Enter Valid Advanced Card Rebate Margin" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Advanced Card Rebate Margin!'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Special Rebate" HeaderStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="SpecialRebateMargin" Width="50px" autocomplete="off" onkeypress="return validateDecThreeplace(this,event)" MaxLength="10" placeholder="Enter Special Rebate Margin" ValidationGroup="a" Text='<%# Eval("SpecialRebateMargin") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="pull-right">
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator51D" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,3})?$" ValidationGroup="a" runat="server" ControlToValidate="SpecialRebateMargin" ErrorMessage="Enter Valid Special Rebate Margin" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Advanced Card Rebate Margin!'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Effective from Date">
                                            <ItemTemplate>
                                                <div class="form-group">
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtEffectiveDate" Text='<%# Eval("EffectiveDate") %>' onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select Effective from Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                        <asp:GridView ID="GridView2" runat="server" EmptyDataText="No records Found" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover">
                                    <Columns>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <input id="Checkbox4" type="checkbox" onclick="CheckAll2(this)" runat="server" />
                                                All
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox3" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Name">
                                            <ItemTemplate>
                                                  <asp:Label ID="lblItemName2" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                  <asp:Label ID="lblItemCat_id2" Visible="false" runat="server" Text='<%# Eval("ItemCat_id") %>'></asp:Label>
                                                <asp:Label ID="lblItem_id2" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Distributor / SuperStockist Rate">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDistOrSSRate2" onfocusout="FetchData(this)" autocomplete="off" onkeypress="return validateDecThreeplace(this,event)" AutoPostBack="false" OnTextChanged="txtDistOrSSRate2_TextChanged" MaxLength="10" placeholder="Enter Distributor / SuperStockist Rate" ValidationGroup="a" Text='<%# Eval("DistOrSSRate") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator101A" ValidationGroup="a"
                                                        ErrorMessage="Enter Distributor / SuperStockist Rate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Distributor / SuperStockist Rate!'></i>"
                                                        ControlToValidate="txtDistOrSSRate2" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator51B" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,3})?$" ValidationGroup="a" runat="server" ControlToValidate="txtDistOrSSRate2" ErrorMessage="Enter Valid Distributor / SuperStockist Rate" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Distributor / SuperStockist Rate!'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Distributor / SuperStockist Comm.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDistOrSSComm2" onfocusout="FetchData(this)" autocomplete="off" onkeypress="return validateDecThreeplace(this,event)" AutoPostBack="false" OnTextChanged="txtDistOrSSComm2_TextChanged" MaxLength="10" placeholder="Enter Distributor / SuperStockist Comm." ValidationGroup="a" Text='<%# Eval("DistOrSSComm") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator102C" ValidationGroup="a"
                                                        ErrorMessage="Enter Distributor / SuperStockist Comm." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Distributor / SuperStockist Comm.e!'></i>"
                                                        ControlToValidate="txtDistOrSSComm2" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator52D" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,3})?$" ValidationGroup="a" runat="server" ControlToValidate="txtDistOrSSComm2" ErrorMessage="Enter Valid Distributor / SuperStockist Comm." Text="<i class='fa fa-exclamation-circle' title='Enter Valid Distributor / SuperStockist Comm.!'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Transportation Comm.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtTransComm2" onfocusout="FetchData(this)" autocomplete="off" onkeypress="return validateDecThreeplace(this,event)" OnTextChanged="txtTransComm2_TextChanged" AutoPostBack="false" MaxLength="10" placeholder="Enter Transportation Comm." ValidationGroup="a" Text='<%# Eval("TransComm") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="pull-right">
                                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator101E" ValidationGroup="a"
                                                        ErrorMessage="Enter Transportation Comm." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Transportation Comm.'></i>"
                                                        ControlToValidate="txtTransComm" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>--%>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator51F" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,3})?$" ValidationGroup="a" runat="server" ControlToValidate="txtTransComm2" ErrorMessage="Enter Valid Transportation Comm." Text="<i class='fa fa-exclamation-circle' title='Enter ValidTransportation Comm.!'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Retailer Rate">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRetailerRate2" autocomplete="off" onkeypress="return validateDecThreeplace(this,event)" AutoPostBack="false" MaxLength="10" placeholder="Enter Retailer Rate" ValidationGroup="a" Text='<%# Eval("RetailerRate") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator102G" ValidationGroup="a"
                                                        ErrorMessage="Enter Retailer Rate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Retailer Rate!'></i>"
                                                        ControlToValidate="txtRetailerRate2" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator52H" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,3})?$" ValidationGroup="a" runat="server" ControlToValidate="txtRetailerRate2" ErrorMessage="Enter Valid Retailer Rate" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Retailer Rate!'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Retailer Comm.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRetailerComm2" onfocusout="FetchData(this)" autocomplete="off" AutoPostBack="false" OnTextChanged="txtRetailerComm2_TextChanged" onkeypress="return validateDecThreeplace(this,event)" MaxLength="10" placeholder="Enter Retailer Comm." ValidationGroup="a" Text='<%# Eval("RetailerComm") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator103I" ValidationGroup="a"
                                                        ErrorMessage="Enter Retailer Comm." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Retailer Comm.!'></i>"
                                                        ControlToValidate="txtRetailerComm2" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator53J" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,3})?$" ValidationGroup="a" runat="server" ControlToValidate="txtRetailerComm2" ErrorMessage="Enter Valid Retailer Comm." Text="<i class='fa fa-exclamation-circle' title='Enter Valid Retailer Comm.!'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Consumer Rate">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtConsumerRate2" autocomplete="off" onkeypress="return validateDecThreeplace(this,event)" MaxLength="10" placeholder="Enter Consumer Rate" ValidationGroup="a" Text='<%# Eval("ConsumerRate") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator104I" ValidationGroup="a"
                                                        ErrorMessage="Enter Consumer Rate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Consumer Rate!'></i>"
                                                        ControlToValidate="txtConsumerRate2" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator54J" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,3})?$" ValidationGroup="a" runat="server" ControlToValidate="txtConsumerRate2" ErrorMessage="Enter Valid Consumer Rate" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Consumer Rate!'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Effective from Date">
                                            <ItemTemplate>
                                                <div class="form-group">
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtEffectiveDate2" Text='<%# Eval("EffectiveDate") %>' onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select Effective from Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                    </div>
                                </div>

                                 <div class="row" id="pnlbtn" runat="server" visible="false">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                                    </div>
                                </div>
                               <%-- <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-block btn-primary" />
                                    </div>
                                </div>--%>
                            </div>
                            </fieldset>
                           
                        </div>

                    </div>

                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
     <script type="text/javascript">
         window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
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

         function validateDecThreeplace(el, evt) {
             var digit = 3;
             var charCode = (evt.which) ? evt.which : event.keyCode;
             if (digit == 0 && charCode == 46) {
                 return false;
             }

             var number = el.value.split('.');
             if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                 return false;
             }
             //just one dot (thanks ddlab)
             if (number.length > 1 && charCode == 46) {
                 return false;
             }
             //get the carat position
             var caratPos = getSelectionStart(el);
             var dotPos = el.value.indexOf(".");
             if (caratPos > dotPos && dotPos > -1 && (number[1].length > digit - 1)) {
                 return false;
             }
             return true;
         }


         function FetchData2(button) {
             debugger;
             var row = button.parentNode.parentNode;
             var DSR = GetChildControl2(row, "txtDistOrSSRate").value;
             var SSTM = GetChildControl2(row, "txtSSTransMargin").value;
             var SSM = GetChildControl2(row, "txtSSMargin").value;
             var SSR = GetChildControl2(row, "txtSSRate").value;

             if (DSR == '') {

                 DSR = 0.000;
             }
             if (SSTM == '') {

                 SSTM = 0.000;
             }

             if (SSM == '') {

                 SSM = 0.000;
             }
             if (SSR == '') {

                 SSR = 0.000;
             }


             if (SSTM == '' && SSM == '') {
                 alert('Total Target Loan not less than zero');
                 GetChildControl2(row, "txtSSTransMargin").value = ''
                 GetChildControl2(row, "txtSSMargin").value = '';
                 GetChildControl1(row, "txtSSTransMargin").focus();
             }
             else {

                 var MultiSSMilk = (parseFloat(DSR) - parseFloat(SSTM) - parseFloat(SSM));
                 GetChildControl2(row, "txtSSRate").value = parseFloat(MultiSSMilk).toFixed(3);
             }

             return false;
         };

         function GetChildControl2(element, id) {
             var child_elements = element.getElementsByTagName("*");
             for (var i = 0; i < child_elements.length; i++) {
                 if (child_elements[i].id.indexOf(id) != -1) {
                     return child_elements[i];
                 }
             }
         };

         function FetchData1(button) {
             debugger;
             var row = button.parentNode.parentNode;
             var CR = GetChildControl(row, "txtConsumerRate").value;
             var RC = GetChildControl(row, "txtRetailerComm").value;
             var RR = GetChildControl(row, "txtRetailerRate").value;
             var TC = GetChildControl(row, "txtTransComm").value;
             var DSC = GetChildControl(row, "txtDistOrSSComm").value;
             var DSR = GetChildControl(row, "txtDistOrSSRate").value;
            
             if (CR == '') {

                 CR = 0.000;
             }
             if (RC == '') {

                 RC2 = 0.000;
             }

             if (RR == '') {

                 RR = 0.000;
             }
             if (TC == '') {

                 TC = 0.000;
             }
             if (DSC == '') {

                 DSC = 0.000;
             }
             if (DSR == '') {
                 DSR = 0.000;

             }
            
            
            
             if (CR == '' && RC == '') {
                 alert('Total Target Loan not less than zero');
                 GetChildControl1(row, "txtConsumerRate").value = ''
                 GetChildControl1(row, "txtRetailerComm").value = '';
                 GetChildControl1(row, "txtConsumerRate").focus();
             }
             else {

                 var MultiRetailerMilk = (parseFloat(CR) - parseFloat(RC));
                
                 GetChildControl1(row, "txtRetailerRate").value = parseFloat(MultiRetailerMilk).toFixed(3);
             }
             if (DSC == '' && TC == '') {
                 alert('Consumer Rate is not empty or less than zero');
                 GetChildControl1(row, "txtRetailerComm").value = '';
                 GetChildControl1(row, "txtTransComm").value = '';
                 GetChildControl1(row, "txtDistOrSSComm").value = '';
                 GetChildControl1(row, "txtTransComm").focus();

             }
             else {
                 var RR = GetChildControl1(row, "txtRetailerRate").value;
                 var MultiDistMilk = (parseFloat(RR) - parseFloat(TC) - parseFloat(DSC));
                 GetChildControl1(row, "txtDistOrSSRate").value = parseFloat(MultiDistMilk).toFixed(3);
                
                 

             }

             return false;
         };

         function GetChildControl1(element, id) {
             var child_elements = element.getElementsByTagName("*");
             for (var i = 0; i < child_elements.length; i++) {
                 if (child_elements[i].id.indexOf(id) != -1) {
                     return child_elements[i];
                 }
             }
         };
         // end of milk
         // for product
         function FetchData(button) {
             var row = button.parentNode.parentNode;
             var DSR2 = GetChildControl(row, "txtDistOrSSRate2").value;
             var DSC2 = GetChildControl(row, "txtDistOrSSComm2").value;
             var TC2 = GetChildControl(row, "txtTransComm2").value;
             var RR2 = GetChildControl(row, "txtRetailerRate2").value;
             var RC2 = GetChildControl(row, "txtRetailerComm2").value;
             var CR2 = GetChildControl(row, "txtConsumerRate2").value;

             if (DSR2 == '') {
                 DSR2 = 0.000;

             }
             if (DSC2 == '') {

                 DSC2 = 0.000;
             }
             if (TC2 == '') {

                 TC2 = 0.000;
             }
             
             if (RR2 == '') {

                 RR2 = 0.000;
             }
             if (RC2 == '') {

                 RC2 = 0.000;
             }
             if (CR2 == '') {

                 CR2 = 0.000;
             }

             if (DSC2 == '' && TC2 == '') {
                 alert('Total Target Loan not less than zero');
                 GetChildControl(row, "txtDistOrSSComm2").value = '';
                 GetChildControl(row, "txtTransComm2").value = '';
                 GetChildControl(row, "txtDistOrSSRate2").focus();

             }
             else {
                 var MultiDist = (parseFloat(DSR2) + parseFloat(DSC2) + parseFloat(TC2));
                 GetChildControl(row, "txtRetailerRate2").value = parseFloat(MultiDist).toFixed(3);

             }

             
             if (RR2 == '' && RC2=='') {
                 alert('DistOrSS Rate is not empty or less than zero');
                 GetChildControl(row, "txtRetailerRate2").value = ''
                 GetChildControl(row, "txtRetailerComm2").value = '';
                 GetChildControl(row, "txtRetailerComm2").focus();
             }
             else {
                 var RR2 = GetChildControl(row, "txtRetailerRate2").value;

                 var MultiRetailer = (parseFloat(RR2) + parseFloat(RC2));
                 GetChildControl(row, "txtConsumerRate2").value = parseFloat(MultiRetailer).toFixed(3);

             }
            
             return false;
         };

         function GetChildControl(element, id) {
             var child_elements = element.getElementsByTagName("*");
             for (var i = 0; i < child_elements.length; i++) {
                 if (child_elements[i].id.indexOf(id) != -1) {
                     return child_elements[i];
                 }
             }
         };
         // end of product
		 
		  function doCheck() {

            var keyCode = (event.which) ? event.which : event.keyCode;
            if ((keyCode == 8) || (keyCode == 46))
                event.returnValue = false;

        }
 </script>
</asp:Content>

