<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="EditRMRDChallanEntry.aspx.cs" Inherits="mis_MilkCollection_EditRMRDChallanEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Edit Society Challan</h3>
                        </div>
                        <asp:label id="lblMsg" runat="server" text=""></asp:label>
                        <div class="box-body">
                            <fieldset>
                                <legend>Filter</legend>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Date</label>
                                        <span class="pull-right">
                                            <asp:requiredfieldvalidator id="RequiredFieldValidator10" runat="server" display="Dynamic" controltovalidate="txtDate" text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" errormessage="Enter Date" setfocusonerror="true" forecolor="Red" validationgroup="Save"></asp:requiredfieldvalidator>
                                        </span>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <i class="far fa-calendar-alt"></i>
                                                </span>
                                            </div>
                                            <asp:textbox id="txtDate" autocomplete="off" cssclass="form-control DateAdd"  runat="server"></asp:textbox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>BMC Root</label>
                                        <span class="pull-right">
                                            <asp:requiredfieldvalidator id="RequiredFieldValidator144" validationgroup="a"
                                                initialvalue="0" errormessage="Select BMC Root" text="<i class='fa fa-exclamation-circle' title='Select BMC Root !'></i>"
                                                controltovalidate="ddlBMCTankerRootName" forecolor="Red" display="Dynamic" runat="server">
                                            </asp:requiredfieldvalidator>
                                        </span>
                                        <asp:dropdownlist id="ddlBMCTankerRootName" runat="server" cssclass="form-control select2" onselectedindexchanged="ddlBMCTankerRootName_SelectedIndexChanged" autopostback="true"></asp:dropdownlist>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <label>Society<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:requiredfieldvalidator id="rfvSociety" runat="server" display="Dynamic" validationgroup="a" controltovalidate="ddlSociety" initialvalue="0" text="<i class='fa fa-exclamation-circle' title='Select Society!'></i>" errormessage="Select Society" setfocusonerror="true" forecolor="Red"></asp:requiredfieldvalidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:dropdownlist id="ddlSociety" cssclass="form-control select2" runat="server">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                        </asp:dropdownlist>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:button id="btnSearch" runat="server" style="margin-top: 21px;" cssclass="btn btn-success" text="Search" onclick="btnSearch_Click" validationgroup="a" />
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Entry</legend>
                                <div class="col-md-3 pull-left">
                                    <div class="form-group">
                                        <asp:button id="btnEdit" visible="false" runat="server" OnClick="btnEdit_Click" cssclass="btn btn-danger" text="Edit"  onclientclick="return confirm('Do you really want to Edit?')" />
                                    </div>

                                </div>
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:gridview id="gv_MilkCollectionChallanEntryDetails" showheader="true" emptydatatext="No Record Found" emptydatarowstyle-forecolor="Red" autogeneratecolumns="false" cssclass="datatable table table-bordered gv_MilkCollectionChallanEntryDetails" runat="server" OnRowDataBound="gv_MilkCollectionChallanEntryDetails_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-Width="2%">
                                             <HeaderTemplate>
                                               <%-- <input id="Checkbox2" type="checkbox" onclick="CheckAll(this)" runat="server" />--%>
                                                 <asp:CheckBox ID="check_All" runat="server" ClientIDMode="static" />
                                                All
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkselect" runat="server" OnCheckedChanged="chkselect_CheckedChanged" AutoPostBack="true"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                     <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>'  runat="server" />
                                            <asp:Label ID="lblMilkCollectionChallan_ID" CssClass="hidden" Text='<%# Eval("MilkCollectionChallan_ID") %>'  runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDt_Date" runat="server" Text='<%# (Convert.ToDateTime(Eval("EntryDate"))).ToString("dd/MM/yyyy") %>'></asp:Label>                                            
                                            </ItemTemplate>                                         
                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Society">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                            </ItemTemplate>                      
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Shift" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_Shift"  Visible="false" runat="server" Text='<%# Eval("Shift") %>'></asp:Label>
                                                <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic"  InitialValue="0" ControlToValidate="ddlShift" Text="<i class='fa fa-exclamation-circle' title='Select Shift!'></i>" ErrorMessage="Select Shift" ValidationGroup="a" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>                                 
                                        <asp:DropDownList ID="ddlShift" Enabled="false" runat="server" CssClass="form-control select2">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="Morning">Morning</asp:ListItem>
                                            <asp:ListItem Value="Evening">Evening</asp:ListItem>                                           
                                        </asp:DropDownList>
                                            </ItemTemplate>                      
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk Type" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_MilkType"  Visible="false" runat="server" Text='<%# Eval("MilkType") %>'></asp:Label>
                                            <asp:DropDownList ID="ddlMilkType" Enabled="false"  runat="server" CssClass="form-control select2" ClientIDMode="Static">
                                         <asp:ListItem Value="Buf">Buf</asp:ListItem>
                                         <asp:ListItem Value="Cow">Cow</asp:ListItem>                                     
                                    </asp:DropDownList>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Milk Quantity">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtI_MilkSupplyQty" Enabled="false" CssClass="form-control" runat="server" Text='<%# Eval("MilkQuantity") %>' OnTextChanged="txtI_MilkSupplyQty_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </ItemTemplate>
                                            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fat %">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtFat" Enabled="false" CssClass="form-control" runat="server" Text='<%# Eval("Fat") %>' OnTextChanged="txtFat_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </ItemTemplate>                                           
                                        </asp:TemplateField>
                                    
                                        <asp:TemplateField HeaderText="CLR">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCLR" Enabled="false" CssClass="form-control" runat="server" Text='<%# Eval("CLR") %>' OnTextChanged="txtCLR_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SNF %">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSNF"  Enabled="false" CssClass="form-control" runat="server" Text='<%# Eval("Snf") %>'></asp:TextBox>
                                            </ItemTemplate>                                          
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FAT (In KG)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtFAT_IN_KG" Enabled="false" CssClass="form-control" runat="server" Text='<%# Eval("FatInKg") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SNF (In KG)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSNF_IN_KG"  Enabled="false" CssClass="form-control" runat="server" Text='<%# Eval("SnfInKg") %>'></asp:TextBox>
                                            </ItemTemplate>
                                         
                                        </asp:TemplateField>
                                  

                                        <asp:TemplateField HeaderText="Milk Quality" HeaderStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuality" Visible="false" runat="server" Text='<%# Eval("MilkQuality") %>'></asp:Label>
                                            <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="Dynamic"  InitialValue="0" ControlToValidate="ddlMilkQuality" Text="<i class='fa fa-exclamation-circle' title='Select Milk Quality!'></i>" ErrorMessage="Select Milk Quality" ValidationGroup="a" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>                                 
                                        <asp:DropDownList ID="ddlMilkQuality" Enabled="false"  runat="server" CssClass="form-control select2">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="Good">Good</asp:ListItem>
                                            <asp:ListItem Value="Sour">Sour</asp:ListItem> 
                                            <asp:ListItem Value="Curdle">Curdle</asp:ListItem>                                           
                                        </asp:DropDownList>
                                            </ItemTemplate>
                                          
                                        </asp:TemplateField>

                                    </Columns>
                            </asp:gridview>
                                    </div>
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
    <script>
        $('#check_All').click(function () {

            var inputList = document.querySelectorAll('.gv_MilkCollectionChallanEntryDetails tbody input[type="checkbox"]:not(:disabled)');

            for (var i = 0; i < inputList.length; i++) {
                if (document.getElementById('check_All').checked) {
                    inputList[i].checked = true;

                    //ValidatorEnable(Amount,true)
                }
                else {
                    inputList[i].checked = false;
                }
            }
        });
    </script>
</asp:Content>

