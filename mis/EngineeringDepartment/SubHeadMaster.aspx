<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SubHeadMaster.aspx.cs" Inherits="mis_EngneeringDepartment_SubHeadMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:scriptmanager id="ScriptManager1" runat="server" enablepagemethods="true"></asp:scriptmanager>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #d9d9d9;">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                        </button>
                        <h4 class="modal-title" id="myModalLabel">Confirmation</h4>

                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <p>
                            <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp;
                            <asp:label id="lblPopupAlert" runat="server"></asp:label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:button runat="server" cssclass="btn btn-success" text="Yes" id="btnYes" onclick="btnSubmit_Click" style="margin-top: 20px; width: 50px;" />
                        <asp:button id="btnNo" validationgroup="no" runat="server" cssclass="btn btn-danger" text="No" data-dismiss="modal" style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>

        </div>
    </div>
    <%--ConfirmationModal End --%>
    <asp:validationsummary id="vs" runat="server" validationgroup="a" showmessagebox="true" showsummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Sub Head Master</h3>
                        </div>
                        <asp:label id="lblMsg" runat="server" text=""></asp:label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Sub Head Name<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:requiredfieldvalidator id="RequiredFieldValidator4" validationgroup="a"
                                                errormessage="Enter Sub Head Name" text="<i class='fa fa-exclamation-circle' title='Enter Sub Head Name !'></i>"
                                                controltovalidate="txtSubHeadName" forecolor="Red" display="Dynamic" runat="server">
                                            </asp:requiredfieldvalidator>
                                        </span>
                                        <asp:textbox id="txtSubHeadName" cssclass="form-control" runat="server" placeholder="Enter Head Name"></asp:textbox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:button id="btnSubmit" runat="server" text="SUBMIT" style="margin-top : 20px;" cssclass="btn btn-primary" validationgroup="a" onclick="btnSubmit_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <asp:label id="Label1" runat="server" text=""></asp:label>
                        <div class="box-body">
                            <div class="table table-responsive">
                                <asp:GridView ID="GridView1" OnRowCommand="GridView1_RowCommand" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                            EmptyDataText="No Record Found." DataKeyNames="ENGSubHeadId">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Sub Head Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHeadName" runat="server" Text='<%# Eval("ENGSubHeadName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                

                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("ENGSubHeadId") %>' runat="server" Visible='<%# Eval("ENGSectionEntrycount").ToString() == "0" && Eval("ENGMappingcount").ToString() == "0" ? true : false  %>' ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                        &nbsp;&nbsp;&nbsp;
                                            &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("ENGSubHeadId") %>' CommandName="RecordDelete" runat="server" Text='<%# Eval("IsActive").ToString() == "True" ? "Active" : "Deactive"  %>' ToolTip="Delete" Style="color: red;" Visible='<%# Eval("ENGSectionEntrycount").ToString() == "0" && Eval("ENGMappingcount").ToString() == "0" ? true : false  %>' OnClientClick="return confirm('Are you sure You want to Change Status?')"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

