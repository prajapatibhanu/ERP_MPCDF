<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DistrictWiseDCSReport.aspx.cs" Inherits="mis_DCSInformation_DistrictWiseDCSReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-6">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">District Wise DCS Information Report</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
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
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>District</label>
                                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control">                                        
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button runat="server" ID="Button1" Style="margin-top: 20px;" CssClass="btn btn-success" Text="Search"></asp:Button>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <table class="table table-bordered">
                                            <tr>
                                                <th>S.NO</th>
                                                <th>Bank Name</th>
                                                <th>No of Acknowledgement received form Banks</th>
                                                <th>Total Cards Issued/Limit Extended</th>
                                            </tr>
                                            <tr>
                                                <td>1</td>
                                                <td>State Bank of India</td>
                                                <td>6000</td>
                                                <td>300</td>
                                            </tr>
                                            <tr>
                                                <td>2</td>
                                                <td>Union Bank of India</td>
                                                <td>4000</td>
                                                <td>500</td>
                                            </tr>
                                            <tr>
                                                <td>3</td>
                                                <td>Canara Bank</td>
                                                <td>2000</td>
                                                <td>100</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="text-align: right"><b>Total</b></td>
                                                <td>12000</td>
                                                <td>900</td>
                                            </tr>
                                        </table>
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
</asp:Content>




