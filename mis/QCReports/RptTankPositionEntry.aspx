<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptTankPositionEntry.aspx.cs" Inherits="mis_QCReports_RptTankPositionEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Tank Position Entry Report</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                        <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>From Date</label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txtFromDate" Text="<i class='fa fa-exclamation-circle' title='Enter From Date!'></i>" ErrorMessage="Enter From Date" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </span>
                                                    <div class="input-group">
                                                        <div class="input-group-prepend">
                                                            <span class="input-group-text">
                                                                <i class="far fa-calendar-alt"></i>
                                                            </span>
                                                        </div>
                                                        <asp:TextBox ID="txtFromDate" autocomplete="off" placeholder="Enter From Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div> 
                                <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>To Date</label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtToDate" Text="<i class='fa fa-exclamation-circle' title='Enter To Date!'></i>" ErrorMessage="Enter To Date" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </span>
                                                    <div class="input-group">
                                                        <div class="input-group-prepend">
                                                            <span class="input-group-text">
                                                                <i class="far fa-calendar-alt"></i>
                                                            </span>
                                                        </div>
                                                        <asp:TextBox ID="txtToDate" autocomplete="off" placeholder="Enter To Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div> 
                                <div class="col-md-2">
                                    <div class="form-group">
                                         <asp:Button ID="btnSearch" style="margin-top:22px;"  runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click"/>
                                    </div>
                                </div> 
                                </div>
                            <div class="row">
                                <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Button ID="btnExcel" Visible="false" runat="server" Text="Export" CssClass="btn btn-primary" OnClick="btnExcel_Click"/>
                                </div>
                            </div>
                            </div>
                            <div class="table-responsive">
                            <asp:GridView ID="gvDetail" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Record Found">
                                
                                <Columns>
                                      <asp:TemplateField HeaderText="Date" HeaderStyle-Width="12%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Time" DataField="EntryTIme" />
                                    <asp:BoundField HeaderText="Source" DataField="Source" />
                                    <asp:BoundField HeaderText="Tank Position" DataField="TankPosition" />
                                    <asp:BoundField HeaderText="Variant" DataField="ItemTypeName" />
                                    <asp:BoundField HeaderText="OT" DataField="OT" />
                                    <asp:BoundField HeaderText="TEMP" DataField="TEMP" />
                                    <asp:BoundField HeaderText="FAT" DataField="FAT" />

                                    <asp:BoundField HeaderText="CLR" DataField="CLR" />
                                    <asp:BoundField HeaderText="SNF" DataField="SNF" />
                                    <asp:BoundField HeaderText="Acidity" DataField="Acidity" /> 
                                     <asp:BoundField HeaderText="COB" DataField="COB" />  
                                    <asp:BoundField HeaderText="Remark" DataField="Remark" />                                 
                                </Columns>
                            </asp:GridView>
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

