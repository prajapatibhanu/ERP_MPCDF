<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="R_DProgressReportInDS.aspx.cs" Inherits="mis_ResearchandDev_R_DProgressReportInDS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <script type="text/javascript">

        function ShowPopupAddDates() {
            $('#addModalDates').modal('show');
        }</script>
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Progress Report of R & D Work</h3>
                            <asp:label id="lblmsg" runat="server" text=""></asp:label>
                        </div>
                        <div class="box-body">
                             <fieldset>
                            <legend>View Complete Progress of Research & Development Plan (अनुसंधान और विकास योजना की जानकारी देखें)
                            </legend>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">

                                        <asp:dropdownlist id="ddlDS" runat="server" cssclass="form-control select2">
                                        </asp:dropdownlist>
                                        <asp:requiredfieldvalidator id="Rqtype" runat="server" display="Dynamic" errormessage="*" forecolor="Red" backcolor="Yellow" validationgroup="a" initialvalue="0" tooltip="Please Select DS" controltovalidate="ddlDS"></asp:requiredfieldvalidator>

                                    </div>

                                </div>
                                <div class="col-md-6" style="vertical-align: bottom">
                                    <asp:button id="btnlist" runat="server" text="view" causesvalidation="true" validationgroup="a" cssclass="btn btn-primary" onclick="btnlist_Click" />
                                </div>
                            </div>
                            <div class="col-md-12">
                                <asp:gridview id="grdlist" cssclass="table table-bordered" runat="server" width="100%" autogeneratecolumns="false" headerstyle-horizontalalign="Center" onrowcommand="grdlist_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ResearchTitle" HeaderText="Project Title" />
                                        <asp:BoundField DataField="ImplementatonStatus" HeaderText="Status" />
                                        <asp:BoundField DataField="ImplementionStartDate" HeaderText="Implemention Start Date" />
                                        <asp:BoundField DataField="ImplementionENDDate" HeaderText="ImplementionENDDate" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnview" runat="server" Text="View" ToolTip="View Progrss of Research & Development Plan" CssClass="btn btn-info" CommandArgument='<%#Eval("RDPlanID")+"|"+Eval("RDPlanImplementationID") %>' CommandName="Detail" ><i class="fa  fa-area-chart"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:gridview>
                            </div></fieldset>
                        </div>
                    </div>
                </div>
                <div class="modal fade" id="addModalDates" tabindex="-1" role="dialog" aria-labelledby="addModalDates">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <span style="color: white">Project Details</span>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            </div>
                            <div class="modal-body">
                                <table class="table table-bordered">
                                    <tr>
                                        <td style="width: 50%">Research Type:
                                        <asp:label id="lblRDType" runat="server" forecolor="Maroon">                                   
                                        </asp:label>
                                        </td>
                                        <td style="width: 50%">Title:
                                        <asp:label id="lblTitle" runat="server" forecolor="Maroon">                                   
                                        </asp:label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 100%" colspan="2">Details:<asp:label id="lbldetails" runat="server" forecolor="Maroon">                                   
                                        </asp:label></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%" colspan="2">Actual OutCome:<asp:label id="lblOutcomes" runat="server" forecolor="Maroon">                                   
                                        </asp:label></td>
                                    </tr>
                                </table>
                                <span style="color: maroon">Implementation Details</span>
                                <table class="table table-bordered">
                                    <tr>
                                        <td style="width: 100%" colspan="2">
                                            <asp:gridview id="grd" cssclass="table table-bordered" runat="server" emptydatatext="No Data Available" width="100%" autogeneratecolumns="false" headerstyle-horizontalalign="Center" onrowcommand="grdlist_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   <%-- <asp:BoundField DataField="ResearchTitle" HeaderText="Project Title" />--%>
                                                    <asp:BoundField DataField="ImplementionStartDate" HeaderText="Implemention Start Date" />
                                                    <asp:BoundField DataField="ImplementionENDDate" HeaderText="Implemention END Date" />
                                                    <asp:TemplateField HeaderText="Document">
                                                        <ItemTemplate>
                                                               <asp:HyperLink ID="hyperfirstdocument" runat="server" NavigateUrl='<%#Eval("ImplementedDOC") %>' Target="_blank" Text="Download" Visible='<%#Convert.ToBoolean(Eval("isImplementedDOC")) %>'></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>

                                            </asp:gridview>
                                        </td>
                                    </tr>
                                </table>


                            </div>
                            <%-- OnClick="btn_save_Click"--%>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                                <%-- <asp:Button runat="server" ID="Button1" class="btn btn-success" Text="Save"></asp:Button>--%>
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

