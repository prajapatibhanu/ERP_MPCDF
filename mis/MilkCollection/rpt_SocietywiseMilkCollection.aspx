<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="rpt_SocietywiseMilkCollection.aspx.cs" Inherits="mis_MilkCollection_rpt_SocietywiseMilkCollection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">समिति वार दुग्ध संकलन रिपोर्ट</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>दुग्ध संकलन : </label>
                                <asp:RadioButtonList ID="rbMilkCollection" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbMilkCollection_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="समिति वार" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="दुग्ध उत्पादक वार" Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="pnlSamiti" runat="server">
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>From Date<span style="color: red;">*</span> </label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" Display="Dynamic" ControlToValidate="txtFromDate" Text="<i class='fa fa-exclamation-circle' title='Enter From Date!'></i>" ErrorMessage="Enter From Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">
                                                <i class="far fa-calendar-alt"></i>
                                            </span>
                                        </div>
                                        <asp:TextBox ID="txtFromDate" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>To Date<span style="color: red;">*</span> </label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvToDate" runat="server" Display="Dynamic" ControlToValidate="txtFromDate" Text="<i class='fa fa-exclamation-circle' title='Enter To Date!'></i>" ErrorMessage="Enter To Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">
                                                <i class="far fa-calendar-alt"></i>
                                            </span>
                                        </div>
                                        <asp:TextBox ID="txtToDate" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlUtpadak" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Collection Date<span style="color: red;">*</span> </label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txtCollectionDate" Text="<i class='fa fa-exclamation-circle' title='Enter Collection Date!'></i>" ErrorMessage="Enter Collection Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">
                                                <i class="far fa-calendar-alt"></i>
                                            </span>
                                        </div>
                                        <asp:TextBox ID="txtCollectionDate" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="row">
                        <div class="col-md-2 ">
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" CssClass="btn btn-success" runat="server" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="a" />
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="pnlproducer" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group table-responsive">
                                    <asp:GridView ID="gvMilkCollection" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                        EmptyDataText="No Record Found.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="क्र.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="उत्पादक का नाम">
                                                <ItemTemplate>                                                   
                                                    <asp:Label ID="lblProducerName" runat="server" Text='<%# Eval("ProducerName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="दुग्ध का प्रकार" DataField="V_MilkType" />
                                            <asp:BoundField HeaderText="मात्रा" DataField="I_MilkSupplyQty" />
                                            <asp:BoundField HeaderText="फैट" DataField="Fat" />
                                            <asp:BoundField HeaderText="एस.एन.एफ." DataField="SNF" />
                                            <asp:BoundField HeaderText="रेट" DataField="Rate" />
                                            <asp:BoundField HeaderText="राशी" DataField="Amount" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlTotal" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group table-responsive">
                                    <asp:GridView ID="grdTotalMilk" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                        EmptyDataText="No Record Found.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="क्र.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="उत्पादक का नाम">
                                                <ItemTemplate>                                                   
                                                    <asp:Label ID="lblProducerName" runat="server" Text='<%# Eval("ProducerName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="दुग्ध का प्रकार" DataField="V_MilkType" />
                                            <asp:BoundField HeaderText="कुल दुग्ध प्रदाय" DataField="I_MilkSupplyQty" />
                                            <asp:BoundField HeaderText="राशि" DataField="Amount" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

