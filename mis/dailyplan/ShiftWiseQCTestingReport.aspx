<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ShiftWiseQCTestingReport.aspx.cs" Inherits="mis_dailyplan_ShiftWiseQCTestingReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Shift Wise QC Testing Report</h3>
                        </div>
                        <asp:Label ID="lblMsg" Text="" runat="server"></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Date</label>
                                        <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtDate" runat="server" placeholder="Select Date..." class="form-control DateAdd" onpaste="return false"></asp:TextBox>

                                </div>
                            </div>
                                    </div>
                                <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Shift</label>
                                           <%-- <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ControlToValidate="ddlShift" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Shift!'></i>" ErrorMessage="Select Shift" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </span>--%>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlShift" CssClass="form-control select2" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success btn-block" style="margin-top:22px;" Text="Search" OnClick="btnSearch_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                 <div class="col-md-12 pull-right noprint">
                            <div class="form-group">
                                <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn btn-default" Text="Excel" OnClick="btnExport_Click"/>
                             
                            </div>      
                            </div>
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvReport" CssClass="table table-bordered table-condensed" runat="server" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField HeaderText="Date" DataField="TestRequest_DT" />
									 <asp:BoundField HeaderText="Time" DataField="Time" />
									 <asp:TemplateField HeaderText="Shift" HeaderStyle-Width="9%">                                       
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Shift") %>'></asp:Label>
                                        </ItemTemplate>
                                       
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="TestRequestType" DataField="TestRequestType" />
                                    <asp:BoundField HeaderText="TestRequestFor" DataField="TestRequestFor" />
                                    <asp:BoundField HeaderText="Oral Test" DataField="Oral Test" />
                                    <asp:BoundField HeaderText="Temperature" DataField="Temperature" />
                                    <asp:BoundField HeaderText="FAT" DataField="FAT" />
                                    <asp:BoundField HeaderText="CLR" DataField="CLR" />
                                    <asp:BoundField HeaderText="SNF" DataField="SNF" />
                                    <asp:BoundField HeaderText="COB" DataField="COB" />
                                     <asp:BoundField HeaderText="Acidity" DataField="Acidity" />
                                     <asp:BoundField HeaderText="TestingType" DataField="TestingType" />
                                     <asp:BoundField HeaderText="Result" DataField="TestRequest_Result_Status" />
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
</asp:Content>

