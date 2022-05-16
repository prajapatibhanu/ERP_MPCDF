<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="UnionMaster.aspx.cs" Inherits="mis_DCSInformation_UnionMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Union Master</h3>
                           
                        </div>
                        <div class="box-body">
                             <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Milk Union<span style="color:red">*</span></label>
                                        <asp:DropDownList id="ddlOffice_ID" CssClass="form-control select2" ClientIDMode="Static" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>No of Member's in Milk Union<span style="color:red">*</span></label>
                                        <asp:textbox id="txttotalnoofunion" runat="server" ClientIDMode="Static" CssClass="form-control" autocomplete="off"  onkeypress="return validateNum(event)" placeholder="Enter No of Member's in Milk Union"></asp:textbox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Save" style="margin-top:20px;" OnClientClick="return validateform();" OnClick="btnSave_Click"/>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table table-responsive">
                                        <asp:GridView ID="GridView1" DataKeyNames="Union_ID" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S. No" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text='<%#Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Milk Union" DataField="MilkUnion"/>
                                                <asp:BoundField HeaderText="No of Memeber's in Milk Union" DataField="NoofMemebrsinUnion"/>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnEdit" runat="server"  CommandName="Select" CausesValidation="false" Text="Edit" Style="color: blue"><i class="fa fa-edit"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function validateform()
        {
            debugger;
            var msg = "";
            if(document.getElementById('<%= ddlOffice_ID.ClientID%>').selectedIndex == 0)
            {
                msg += "Select Milk Union.\n";
            }
            if (document.getElementById('<%= txttotalnoofunion.ClientID%>').value == "")
            {
                msg += "Enter No of Member's in Milk Union.\n";
            }
            if(msg != "")
            {
                alert(msg);
                return false;
            }
            else {
                return true;
            }
        }
    </script>
</asp:Content>

