<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RD_Plan_Intimate_Implementation_To_DS.aspx.cs" Inherits="mis_ResearchandDev_RD_Plan_Intimate_Implementation_To_DS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <script type="text/javascript">

        function ShowPopupAddDates() {
            $('#addModalDates').modal('show');
        }
        function ShowPopupSurvery() {
            $('#SurveryModel').modal('show');
        }
        function ShowPopupProcess() {
            $('#ProcessModel').modal('show');
        }
    </script>
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Progress Report Of R & D Work</h3>
                            <asp:label id="lblmsg" runat="server" text=""></asp:label>
                        </div>
                        <div class="box-body">
                            <fieldset>
                            <legend>View Documents and start implementation of Research & Development Plan (अनुसंधान और विकास योजना के कार्यान्वयन की जानकारी प्रविष्ट करें)
                            </legend>
                            <div class="col-md-12">
                                <asp:gridview id="grdlist" cssclass="table table-bordered" runat="server" width="100%" autogeneratecolumns="false" headerstyle-horizontalalign="Center" onrowcommand="grdlist_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ResearchTitle" HeaderText="Project Title" />
                                        <asp:BoundField DataField="RDType" HeaderText="Type" />
                                        <asp:BoundField DataField="ImplementionStartDate" HeaderText="Implemention Start Date" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:Button ID="btnview" runat="server" Text="Document View" CssClass="btn btn-info" CommandArgument='<%#Eval("RDPlanID") +"|"+Eval("RDPlanImplementationID") %>' CommandName="Detail" />

                                                <asp:Button ID="btnSurveysave" runat="server" Text="Start Implementation" CssClass="btn btn-danger" CommandArgument='<%#Eval("RDPlanID")+"|"+Eval("RDPlanImplementationID")  %>' CommandName="implement" Visible='<%#!Convert.ToBoolean(Eval("ImplementatonStarted")) %>' />
                                                <asp:Button ID="btnProcessimp" runat="server" Text="Progress Status" CssClass="btn btn-danger" CommandArgument='<%#Eval("RDPlanID")+"|"+Eval("RDPlanImplementationID")  %>' CommandName="Process" Visible='<%#Convert.ToBoolean(Eval("ImplementatonStarted")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:gridview>
                                <asp:hiddenfield id="hdncount" runat="server" value="0" />
                                <asp:hiddenfield id="hdnValue" runat="server" value="0" />
                            </div></fieldset>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="addModalDates" tabindex="-1" role="dialog" aria-labelledby="addModalDates">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <span style="color: white">Project Documents</span>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body">
                            <span style="color: maroon">Document detail</span>
                            <asp:gridview id="grdDocument" runat="server" autogeneratecolumns="false" emptydatatext="No Data Available." cssclass="table table-bordered">
                                <Columns>
                                    <asp:BoundField DataField="DocumentType" HeaderText="Type" />
                                    <asp:TemplateField HeaderText="First Document">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" Text="Download" CommandArgument='<%# Eval("AttachedDoc") %>' runat="server" OnClick="DownloadFile" CssClass="label label-default"></asp:LinkButton>
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                </Columns>
                            </asp:gridview>


                        </div>
                        <%-- OnClick="btn_save_Click"--%>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                            <%-- <asp:Button runat="server" ID="Button1" class="btn btn-success" Text="Save"></asp:Button>--%>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="SurveryModel" tabindex="-1" role="dialog" aria-labelledby="SurveryModel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <span style="color: white">Project Details</span>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body">
                            <table class="table table-bordered">
                                <tr>
                                    <td style="width: 100%">Research Type:
                                        <asp:label id="lblRDType1" runat="server" forecolor="Maroon">                                   
                                        </asp:label>
                                    </td>

                                </tr>
                                <tr>
                                    <td style="width: 100%">Title:
                                        <asp:label id="lblTitle1" runat="server" forecolor="Maroon">                                   
                                        </asp:label>
                                    </td>

                                </tr>
                                <tr>
                                    <td style="width: 100%">Details:<asp:label id="lbldetails1" runat="server" forecolor="Maroon">                                   
                                    </asp:label></td>
                                </tr>
                                <%-- <tr>
                                    <td style="width: 100%" colspan="2">Expected OutCome:<asp:Label ID="lblOutcomes1" runat="server" ForeColor="Maroon">                                   
                                    </asp:Label></td>
                                </tr>--%>
                            </table>
                            <span style="color: maroon">Start Implementation</span>
                            <div class="row" id="SurveyEntry" runat="server">
                                <table class="table table-bordered">
                                    <tr>
                                        <td style="width: 100%">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Implementation Date</label>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:textbox id="txtimplementationDate" runat="server" cssclass="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false"></asp:textbox>
                                                    </div>
                                                    <asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" errormessage="*" controltovalidate="txtimplementationDate" backcolor="Yellow" forecolor="Maroon" validationgroup="b" tooltip="Please Enter Implementation Date"></asp:requiredfieldvalidator>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Remark</label>
                                                    <asp:textbox id="txtImplementationRemark" runat="server" textmode="MultiLine" rows="3" cssclass="form-control">                                   
                                                    </asp:textbox>
                                                    <asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" errormessage="*" controltovalidate="txtImplementationRemark" backcolor="Yellow" forecolor="Maroon" validationgroup="b" tooltip="Please Enter Remark"></asp:requiredfieldvalidator>
                                                </div>

                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:hiddenfield id="hdnRDPlanID" runat="server" />
                            <asp:hiddenfield id="hdnDSID" runat="server" />

                        </div>
                        <%-- OnClick="btn_save_Click"--%>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                            <asp:button runat="server" id="btnSurvey" class="btn btn-success" text="Save" onclick="btnSurvey_Click" causesvalidation="true" validationgroup="b"></asp:button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="ProcessModel" tabindex="-1" role="dialog" aria-labelledby="ProcessModel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <span style="color: white">Project Details</span>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body">
                            <table class="table table-bordered">
                                <tr>
                                    <td style="width: 100%">Research Type:
                                        <asp:label id="lblRDType2" runat="server" forecolor="Maroon">                                   
                                        </asp:label>
                                    </td>

                                </tr>
                                <tr>
                                    <td style="width: 100%">Title:
                                        <asp:label id="lblTitle2" runat="server" forecolor="Maroon">                                   
                                        </asp:label>
                                    </td>

                                </tr>
                                <tr>
                                    <td style="width: 100%">Details:<asp:label id="lbldetails2" runat="server" forecolor="Maroon">                                   
                                    </asp:label></td>
                                </tr>
                                <%-- <tr>
                                    <td style="width: 100%" colspan="2">Expected OutCome:<asp:Label ID="lblOutcomes1" runat="server" ForeColor="Maroon">                                   
                                    </asp:Label></td>
                                </tr>--%>
                            </table>
                            <span style="color: maroon">Final Progress Status</span>
                            <div class="row" id="ImplementationEntry" runat="server">
                                <table class="table table-bordered">
                                    <tr>
                                        <td style="width: 100%">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Finish Date</label>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:textbox id="txtEnddate" runat="server" cssclass="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false"></asp:textbox>
                                                    </div>
                                                    <asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" errormessage="*" controltovalidate="txtEnddate" backcolor="Yellow" forecolor="Maroon" validationgroup="n" tooltip="Please Enter Implementation Date"></asp:requiredfieldvalidator>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Actual Process Status</label>
                                                    <asp:textbox id="txtProcess" runat="server" textmode="MultiLine" rows="3" cssclass="form-control">                                   
                                        </asp:textbox>
                                                    <asp:requiredfieldvalidator id="RequiredFieldValidator4" runat="server" errormessage="*" controltovalidate="txtProcess" backcolor="Yellow" forecolor="Maroon" validationgroup="n" tooltip="Please Enter Remark"></asp:requiredfieldvalidator>
                                                </div>

                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <div class="form-group">
                                                        <label>Actual Implementation Document</label>
                                                        <asp:fileupload id="fileupload" runat="server" cssclass="form-control"></asp:fileupload>
                                                        <asp:button id="btnUpload" runat="server" text="Upload" onclick="UploadFile" />
                                                        <hr />
                                                        <asp:gridview id="GridView1" runat="server" autogeneratecolumns="false" width="100%" emptydatatext="No document is uploaded">
                                                            <Columns>
                                                                <asp:BoundField DataField="ImageType" HeaderText="File Name" />
                                                                <asp:TemplateField HeaderText="Action" >
                                                                    <ItemTemplate>
                                                                        <%----%>
                                                                        <asp:LinkButton ID="lnkDownload" Text="Download" CommandArgument='<%# Eval("ImagePath") %>' runat="server" OnClick="DownloadFile" CssClass="label label-default"></asp:LinkButton>
                                                                        <asp:LinkButton ID="lnkDelete" Text="Delete" CommandArgument='<%# Eval("ImagePath") %>' runat="server" OnClick="DeleteFile" CssClass="label label-default" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:gridview>
                                                    </div>
                                                </div>

                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="row" id="ImplementationDetail" runat="server">
                                <asp:gridview id="grdimplementation" runat="server" autogeneratecolumns="false" emptydatatext="No Data Available." cssclass="table table-bordered">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ResearchTitle" HeaderText="Project Title" />
                                        <asp:BoundField DataField="ImplementionStartDate" HeaderText="Start Date" />
                                        <asp:BoundField DataField="ImplementionENDDate" HeaderText="End Date" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hyperprogress" runat="server" NavigateUrl='<%#Eval("ImplementedDOC") %>' Target="_blank" Text="Implemented Document"></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:gridview>
                            </div>
                        </div>
                        <%-- OnClick="btn_save_Click"--%>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                            <asp:button runat="server" id="btnProcess" class="btn btn-success" text="Save" onclick="btnProcess_Click" causesvalidation="true" validationgroup="n"></asp:button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

