<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DeleteRMRDChallanEntry.aspx.cs" Inherits="mis_MilkCollection_DeleteRMRDChallanEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Delete Challan Entry</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>Filter</legend>
                                 <div class="col-md-2">
                            <div class="form-group">
                                <label>Date</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">
                                            <i class="far fa-calendar-alt"></i>
                                        </span>
                                    </div>
                                    <asp:TextBox ID="txtDate" autocomplete="off"  CssClass="form-control DateAdd" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                   <div class="col-md-2">
                                            <div class="form-group">
                                                <label>CC<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvRoot" ValidationGroup="a"
                                                        InitialValue="0" ErrorMessage="Select Chilling Center" Text="<i class='fa fa-exclamation-circle' title='Select Chilling Center !'></i>"
                                                        ControlToValidate="ddlCC" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator></span>
                                                <asp:DropDownList ID="ddlCC" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlCC_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                       <%-- <div class="col-md-2">
                                    <div class="form-group">
                                        <label>BMC Root</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator144" ValidationGroup="a"
                                                InitialValue="0" ErrorMessage="Select BMC Root" Text="<i class='fa fa-exclamation-circle' title='Select BMC Root !'></i>"
                                                ControlToValidate="ddlBMCTankerRootName" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlBMCTankerRootName" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlBMCTankerRootName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>--%>
                                <div class="col-md-2">
                                    <label>Society<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvSociety" runat="server" Display="Dynamic" ValidationGroup="a" ControlToValidate="ddlSociety" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Society!'></i>" ErrorMessage="Select Society" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlSociety" CssClass="form-control select2" runat="server">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div> 
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSearch" runat="server" style="margin-top:21px;" CssClass="btn btn-success" Text="Search"  OnClick="btnSearch_Click" ValidationGroup="a"/>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Entry</legend>
                                <div class="col-md-3 pull-left">
                                    <div class="form-group">
                                        <asp:Button ID="btnDelete" Visible="false" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="btnDelete_Click" OnClientClick="return confirm('Do you really want to delete?')"/>
                                    </div>
                                    
                                </div>
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gv_MilkCollectionChallanEntryDetails"  ShowHeader="true" EmptyDataText="No Record Found" EmptyDataRowStyle-ForeColor="Red" AutoGenerateColumns="false" CssClass="datatable table table-bordered" runat="server">
                                <Columns>
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
                                        <asp:TemplateField HeaderText="Shift">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_Shift" runat="server" Text='<%# Eval("Shift") %>'></asp:Label>
                                            </ItemTemplate>                      
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_MilkType" runat="server" Text='<%# Eval("MilkType") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Fat %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFat" runat="server" Text='<%# Eval("Fat") %>'></asp:Label>
                                            </ItemTemplate>                                           
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SNF %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("Snf") %>'></asp:Label>
                                            </ItemTemplate>                                          
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblI_MilkSupplyQty" runat="server" Text='<%# Eval("MilkQuantity") %>'></asp:Label>
                                            </ItemTemplate>
                                            
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="FAT (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFAT_IN_KG" runat="server" Text='<%# Eval("FatInKg") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SNF (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF_IN_KG" runat="server" Text='<%# Eval("SnfInKg") %>'></asp:Label>
                                            </ItemTemplate>
                                         
                                        </asp:TemplateField>
                                  
                                        <asp:TemplateField HeaderText="CLR">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("CLR") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk Quality">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuality" runat="server" Text='<%# Eval("MilkQuality") %>'></asp:Label>
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
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
</asp:Content>

