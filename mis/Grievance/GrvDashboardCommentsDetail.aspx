<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="GrvDashboardCommentsDetail.aspx.cs" Inherits="mis_Grievance_GrvDashboardCommentsDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <section class="content">
            <div class="row">
                <div class="col-md-6">

                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label2">Grievance / Feedback Details</h3>
                            <asp:Label ID="lblMsg" runat="server" Text="" Visible="true"></asp:Label>
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Grievance Detail</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="false" class="table table-striped table-bordered">
                                            <Fields>
                                                <asp:TemplateField HeaderText="Grievance Status" HeaderStyle-Font-Bold="true">
                                                    <ItemTemplate>
                                                        <asp:Label Text='<%# Eval("Application_GrvStatus").ToString()%>' runat="server" ID="Application_GrvStatus"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ApplicationApply_Date" HeaderText="Apply Date" HeaderStyle-Font-Bold="true" />
                                                <asp:BoundField DataField="Application_RefNo" HeaderText="Reference No / Complaint No" HeaderStyle-Font-Bold="true" />
                                                <asp:BoundField DataField="District_Name" HeaderText="District Name" HeaderStyle-Font-Bold="true" />
                                                <asp:BoundField DataField="Location" HeaderText="Location" HeaderStyle-Font-Bold="true" />
                                                <asp:BoundField DataField="Application_GrievanceType" HeaderText="Grievance Type" HeaderStyle-Font-Bold="true" />
                                                <asp:BoundField DataField="Complaint_Name" HeaderText="Complaint Name" HeaderStyle-Font-Bold="true" />
                                                <asp:BoundField DataField="ContactNo" HeaderText="Contact No" HeaderStyle-Font-Bold="true" />
                                            </Fields>
                                        </asp:DetailsView>
                                        <div class="form-group">
                                            <label>Grievance Description</label>
                                            <br />
                                            <asp:TextBox ID="Application_Descritption" runat="server" TextMode="MultiLine" Width="100%" Rows="10" Enabled="false"></asp:TextBox>
                                            <%--<div id="Application_Descritption" runat="server" style="word-wrap: break-word; text-align: justify">--%>
                                        </div>
                                        <div class="pull-right">
                                            <div class="form-group">
                                                <asp:HyperLink ID="hyprApplication_Doc1" Visible="true" runat="server" CommandName="View" Text="Attachment 1 " Target="_blank"></asp:HyperLink>
                                                |
                                                    <asp:HyperLink ID="hyprApplication_Doc2" Visible="true" runat="server" CommandName="View" Text="Attachment 2" Target="_blank"></asp:HyperLink>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <!-- general form elements -->
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title">Department Reply</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <!-- DIRECT CHAT -->
                                    <!-- Conversations are loaded here -->
                                    <div class="direct-chat-messages" style="height: 100%;">
                                        <div id="dvChat" runat="server"></div>
                                        <asp:Label ID="lblDepartmentRecord" runat="server" ClientIDMode="Static"></asp:Label>
                                        <!--For Chat-->
                                    </div>
                                    <!-- /.box-body -->
                                    <!-- /.box-footer-->
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <fieldset>
                        <legend>Internal Discussion</legend>
                        <asp:Label ID="lblCommentRecord" runat="server" Text=""></asp:Label>
                        <div class="direct-chat-messages" style="height: 100%;">
                            <div id="divchat" runat="server"></div>
                            <!--For Chat-->
                        </div>
                    </fieldset>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

