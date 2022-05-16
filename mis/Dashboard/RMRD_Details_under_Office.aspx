<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RMRD_Details_under_Office.aspx.cs" Inherits="mis_Dashboard_RMRD_Details_under_Office" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
     <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">RMRD</h3>
                </div>
                <div class="col-lg-12">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <fieldset>
                        <legend>RMRD Details report</legend>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Date</label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">
                                                <i class="far fa-calendar-alt"></i>
                                            </span>
                                        </div>
                                        <asp:TextBox ID="txtDate" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-danger" Style="margin-top: 19px;" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <asp:Label ID="Label1" runat="server" Text="" Style="font-size: 17px;"></asp:Label>
                            <asp:GridView ID="GridView1" runat="server" PageSize="100" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" class="datatable table table-striped table-hover table-bordered pagination-ys" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Office" HeaderText="Office" />
                                    <asp:BoundField DataField="MilkCollectionEntryDate" HeaderText="Entry Date" />
                                    <asp:BoundField DataField="MilkType" HeaderText="Milk Type" />
                                    <asp:BoundField DataField="MilkQuantity" HeaderText="Milk Quantity" />
                                    <asp:BoundField DataField="MilkQuality" HeaderText="Quality" />
                                    <asp:BoundField DataField="Fat" HeaderText="Fat" />
                                    <asp:BoundField DataField="FatInKg" HeaderText="Fat(In Kg.)" />
                                    <asp:BoundField DataField="Clr" HeaderText="ClR" />
                                    <asp:BoundField DataField="snf" HeaderText="SNF" />
                                    <asp:BoundField DataField="SnfInKg" HeaderText="SNF(In Kg.)" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </fieldset>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
</asp:Content>

