<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Grievance_Dashboard.aspx.cs" Inherits="mis_Grievance_Grievance_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">

                    <!-- general form elements -->
                    <div class="box box-success">
                        <div class="box-header with-border">

                            <div class="row">
                                <div class="col-md-10">
                                    <%--<h3 class="box-title" id="Label1">Grievance Report</h3>--%>
                                </div>
                            </div>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <label>Grievance / Feedback Type (शिकायत / प्रतिक्रिया प्रकार)<span style="color: red;">*</span></label>
                                    <asp:DropDownList runat="server" ID="ddlGrvType" CssClass="form-control">
                                       <asp:ListItem Value="0">All</asp:ListItem>
                                       <asp:ListItem Value="1">उत्पाद की गुणवत्ता के बारे में</asp:ListItem>
                                       <asp:ListItem Value="2">उत्पाद की उपलब्धता के बारे में</asp:ListItem>
                                       <asp:ListItem Value="3">समिति भुगतान के सम्बन्ध में</asp:ListItem>
                                       <asp:ListItem Value="4">एजेंसी / बूथ / पार्लर / डीपो सम्बन्धित शिकायत </asp:ListItem>
                                       <asp:ListItem Value="5">वितरक / परिवहनकर्ता सम्बन्धित शिकायत</asp:ListItem>
                                       <asp:ListItem Value="6">दूध उत्पादक समिति से सम्बंधित</asp:ListItem>
                                       <asp:ListItem Value="7">क्रय सामग्री प्रदायक द्वारा शिकायत </asp:ListItem>
                                       <asp:ListItem Value="8">सामग्री प्रदाय से सम्बंधित</asp:ListItem>
                                       <asp:ListItem Value="9">अन्य सुझाव</asp:ListItem>
                                       <asp:ListItem Value="10">अन्य सुझाव (जानकारी प्राप्त करने हेतु)</asp:ListItem>
                                    </asp:DropDownList>
                                    <small><span id="valddlGrvType" class="text-danger"></span></small>
                                </div>
                                <div class="col-md-3"></div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button runat="server" CssClass="btn btn-success btn-block" Text="Show" ID="btnShow" OnClick="btnShow_Click" OnClientClick="return validateform();" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <a href="Grievance_Dashboard.aspx" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="GridView1" runat="server" DataKeyNames="Office_ID" class="table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Office_Name" HeaderText="OFFICE NAME" />
                                            <asp:TemplateField HeaderText="OPEN GRIEVANCE">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblOpenGrv" CssClass="label label-warning" runat="server" Text='<%# Eval("OpenGrv").ToString()%>' ToolTip='<%# Eval("Office_ID").ToString()%>' OnClick="lblOpenGrv_Click">LinkButton</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CLOSE GRIEVANCE">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblCloseGrv" CssClass="label label-success" runat="server" Text='<%# Eval("CloseGrv").ToString()%>' ToolTip='<%# Eval("Office_ID").ToString()%>' OnClick="lblCloseGrv_Click">LinkButton</asp:LinkButton>
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
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

