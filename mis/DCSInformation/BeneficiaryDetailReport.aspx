<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="BeneficiaryDetailReport.aspx.cs" Inherits="mis_DCSInformation_BeneficiaryDetailReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Benificiary Report</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <%-- <div class="col-md-3">
                                <div class="form-group">
                                    <label>From Date<span style="color: red;">*</span></label>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtFromDate" runat="server" placeholder="Select From Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>To Date<span style="color: red;">*</span></label>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtToDate" runat="server" placeholder="Select To Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>--%>
                                <%--<div class="col-md-3">
                                <div class="form-group">
                                    <asp:Button runat="server" ID="btnSearch" Style="margin-top: 20px;" CssClass="btn btn-success" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateform();"></asp:Button>
                                </div>
                            </div>--%>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvDetails" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Record" ShowHeaderWhenEmpty="true" OnRowCommand="gvDetails_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text='<%#Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Date" DataField="Date" />
                                                <asp:BoundField HeaderText="District" DataField="District" />
                                                <asp:BoundField HeaderText="Beneficiary Name" DataField="BeneficiaryName" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnView" runat="server" CommandName="View" CommandArgument='<%# Eval("Beneficiary_ID") %>'>View</asp:LinkButton>
                                                        <asp:LinkButton ID="lnkbtnEdit" Visible="false" runat="server" CommandName="Edit" CommandArgument='<%# Eval("Beneficiary_ID") %>'></asp:LinkButton>
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
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog modal-lg">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Benificiary Detail</h4>
                    </div>
                    <div class="modal-body">
                        <!--Box content-->
                        <asp:Label ID="lblmodel" runat="server" Text=""></asp:Label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table table-responsive">
                                    <asp:DetailsView ID="dvDetail" runat="server" CssClass="table table-bordered" AutoGenerateRows="false">
                                        <Fields>
                                            <asp:BoundField DataField="District" HeaderText="District" />
                                            <asp:BoundField DataField="DCS_Name" HeaderText="DCS Name" />
                                            <asp:BoundField DataField="CC_Name" HeaderText="CC" />
                                            <asp:BoundField DataField="BeneficiaryName" HeaderText="Beneficiary Name" />
                                            <asp:BoundField DataField="Gender" HeaderText="Gender" />
                                            <asp:BoundField DataField="BankName" HeaderText="Bank Name" />
                                            <asp:BoundField DataField="BranchName" HeaderText="Branch Name" />
                                            <asp:BoundField DataField="IFSCCode" HeaderText="IFSC Code" />
                                            <asp:BoundField DataField="AccountNo" HeaderText="Account No" />
                                            <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" />
                                            <asp:BoundField DataField="Submitforincreaseinlimit" HeaderText="Submit for Increase in Limit" />
                                            <asp:BoundField DataField="ApplicationforNewKCChavingland" HeaderText="Application for New KCC having land(having no KCC)" />
                                            <asp:BoundField DataField="ApplicationforNewKCChavingnoland" HeaderText="Application for New KCC having no land(पूर्व KCC ना हो)" />
                                            <asp:BoundField DataField="CardIssued" HeaderText="Card Issued" />
                                        </Fields>
                                    </asp:DetailsView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <!--Button to dismiss popup--->
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function ShowModal() {
            $('#myModal').modal('show');
        }
    </script>
</asp:Content>

