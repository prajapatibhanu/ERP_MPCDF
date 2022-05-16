<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="UpdateRandDStatus.aspx.cs" Inherits="mis_ResearchandDev_UpdateRandDStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <script src="../js/jquery.js"></script>
    <script type="text/javascript">
        var ddlStatus, pdate, AoutCome, hdnstatus, fileoutcome, rem, txtRemark, txtProgressDate, txtActualoutcome;
        function ShowPopupAddDates() {
            $('#addModalDates').modal('show');
        }
        function ShowPopupProgress() {
            $('#Progressmodal').modal('show');
        }

        $(document).ready(function () {
            ddlStatus = $("#<%=ddlStatus.ClientID%>");
            pdate = $("#pdate");
            AoutCome = $("#AoutCome");
            fileoutcome = $("#fileoutcome");
            hdnstatus = $("#<%=hdnstatus.ClientID%>");
            rem = $("#rem");
            txtRemark = $("#<%=txtRemark.ClientID%>");
            txtProgressDate = $("#<%=txtProgressDate.ClientID%>");
            txtActualoutcome = $("#<%=txtActualoutcome.ClientID%>");
            pdate.hide();
            AoutCome.hide();
            fileoutcome.hide();
            rem.hide();
            if (hdnstatus.val() > 0) {
                ddlStatus.val(hdnstatus.val());
                if (hdnstatus.val() == 1) {

                    pdate.show();
                    rem.show();
                    // txtRemark.val('');
                    // txtProgressDate.val('');
                    txtActualoutcome.val('');
                    AoutCome.hide();
                    fileoutcome.show();
                }
                else if (hdnstatus.val() == 2) {
                    pdate.hide();
                    rem.show();
                    AoutCome.show();
                    fileoutcome.show();
                    //txtRemark.val('');
                    txtProgressDate.val('');
                    //txtActualoutcome.val('');
                }
                else if (hdnstatus.val() == 3) {

                    pdate.hide();
                    rem.show();
                    // txtRemark.val('');
                    txtProgressDate.val('');
                    txtActualoutcome.val('');
                    AoutCome.hide();
                    fileoutcome.show();
                }
                else if (hdnstatus.val() == 4) {

                    pdate.hide();
                    rem.show();
                    // txtRemark.val('');
                    txtProgressDate.val('');
                    txtActualoutcome.val('');
                    AoutCome.hide();
                    fileoutcome.hide();
                }
            }
            ddlStatus.change(function () {
                hdnstatus.val(ddlStatus.val());
                if (hdnstatus.val() > 0) {
                    if (hdnstatus.val() == 1) {

                        pdate.show();
                        rem.show();
                        txtRemark.val('');
                        txtProgressDate.val('');
                        txtActualoutcome.val('');
                        AoutCome.hide();
                        fileoutcome.show();
                    }
                    else if (hdnstatus.val() == 2) {
                        pdate.hide();
                        rem.show();
                        AoutCome.show();
                        fileoutcome.show();
                        txtRemark.val('');
                        txtProgressDate.val('');
                        txtActualoutcome.val('');
                    }
                    else if (hdnstatus.val() == 3) {

                        pdate.hide();
                        rem.show();
                        txtRemark.val('');
                        txtProgressDate.val('');
                        txtActualoutcome.val('');
                        AoutCome.hide();
                        fileoutcome.show();
                    }
                    else if (hdnstatus.val() == 4) {

                        pdate.hide();
                        rem.show();
                        txtRemark.val('');
                        txtProgressDate.val('');
                        txtActualoutcome.val('');
                        AoutCome.hide();
                        fileoutcome.hide();
                    }
                }

                else {
                    pdate.hide();
                    AoutCome.hide();
                    fileoutcome.hide();
                    rem.hide();
                    txtRemark.val('');
                    txtProgressDate.val('');
                    txtActualoutcome.val('');
                }

            });
        });
    </script>
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Update Research & Development Status</h3>
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Update Research & Development Status (अनुसंधान और विकास योजना की स्थिति प्रविष्टी)
                                </legend>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblMsg" CssClass="Autoclr" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:HiddenField ID="hdnvalue" runat="server" Value="0" />
                                        <asp:GridView ID="grdlist" CssClass="table table-bordered" PageSize="20" AllowPaging="true" runat="server" Width="100%" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Center" OnRowCommand="grdlist_RowCommand" OnPageIndexChanging="grdlist_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="RDType" HeaderText="Type" ItemStyle-Width="15%" />
                                                <%--  <asp:BoundField DataField="PlanType" HeaderText="Plan Type" />--%>
                                                <asp:BoundField DataField="ResearchTitle" HeaderText="Title" />
                                                <asp:BoundField DataField="StartDate" HeaderText="Start Date" ItemStyle-Width="10%" />
                                                <asp:BoundField DataField="RDStatus" HeaderText="Status" ItemStyle-Width="8%" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnProgress" runat="server" Text="Progress" ToolTip="View Progress" CssClass="btn btn-info" CommandArgument='<%#Eval("RDPlanID") %>' CommandName="Progress"><i class="fa  fa-area-chart"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="btnview" runat="server" Text="Update Progress Status" ToolTip="Update Progress Status" CssClass="btn btn-primary" CommandArgument='<%#Eval("RDPlanID") %>' CommandName="Detail"><i class="fa  fa-book"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </fieldset>
                            <div class="modal fade" id="addModalDates" tabindex="-1" role="dialog" aria-labelledby="addModalDates">
                                <div class="modal-dialog" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <span style="color: white">Update Research & Development Status</span>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                        </div>
                                        <div class="modal-body">
                                            <table class="table table-bordered">
                                                <tr>
                                                    <td style="width: 50%">Research Type:<asp:Label ID="lblRDType" runat="server" ForeColor="Maroon"></asp:Label></td>
                                                   <td style="width: 50%">Start Date:<asp:Label ID="lblStartDate" runat="server" ForeColor="Maroon"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100%" colspan="2">Title:<asp:Label ID="lblTitle" runat="server" ForeColor="Maroon"></asp:Label></td>
                                                    
                                                </tr>
                                                <tr>
                                                    <td style="width: 100%" colspan="2">Details:<asp:Label ID="lbldetails" runat="server" ForeColor="Maroon">                                   
                                                    </asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100%" colspan="2">Expected OutCome:<asp:Label ID="lblOutcomes" runat="server" ForeColor="Maroon">                                   
                                                    </asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100%" colspan="2">Progress Status:
                                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="0" Selected="True" Text="-- Selet --"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="Progress"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Complete"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="With Held"></asp:ListItem>
                                                            <asp:ListItem Value="4" Text="Other"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="*" ForeColor="Red" BackColor="Yellow" ValidationGroup="n" ToolTip="Please select status" InitialValue="0" ControlToValidate="ddlStatus"></asp:RequiredFieldValidator></td>
                                                </tr>
                                                <tr id="pdate">
                                                    <td style="width: 100%" colspan="2">
                                                        <div class="col-md-6">
                                                            <label>Progress Date</label>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtProgressDate" runat="server" CssClass="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr id="rem">
                                                    <td style="width: 100%" colspan="2">Remark:<asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control">                                   
                                                    </asp:TextBox></td>
                                                </tr>
                                                <tr id="AoutCome">
                                                    <td style="width: 100%" colspan="2">Actual OutCome:<asp:TextBox ID="txtActualoutcome" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control">                                   
                                                    </asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr id="fileoutcome">
                                                    <td style="width: 100%" colspan="2">
                                                        <div class="col-md-12">
                                                            <div class="col-md-8">
                                                                <asp:FileUpload ID="fileupload" runat="server" CssClass="form-control"></asp:FileUpload>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="UploadFile" />
                                                            </div>
                                                        </div>
                                                        <hr />
                                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Width="100%" EmptyDataText="No document is uploaded">
                                                            <Columns>
                                                                <asp:BoundField DataField="ImageType" HeaderText="File Name" />
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <%----%>
                                                                        <asp:LinkButton ID="lnkDownload" Text="Download" ToolTip="Download file" CommandArgument='<%# Eval("ImagePath") %>' runat="server" OnClick="DownloadFile" Style="color: maroon"><i class="fa fa-download"></i></asp:LinkButton>
                                                                        <asp:LinkButton ID="lnkDelete" Text="Delete" ToolTip="Delete file" CommandArgument='<%# Eval("ImagePath") %>' runat="server" OnClick="DeleteFile" Style="color: red"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <%-- OnClick="btn_save_Click"--%>
                                        <div class="modal-footer">
                                            <asp:HiddenField ID="hdnstatus" runat="server" Value="0" />
                                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                                            <asp:Button runat="server" ID="btnupdate" class="btn btn-success" Text="Update Status" CausesValidation="true" ValidationGroup="n" OnClick="btnupdate_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal fade" id="Progressmodal" tabindex="-1" role="dialog" aria-labelledby="addModalDates">
                                <div class="modal-dialog" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <span style="color: white">View Research & Development Status</span>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                        </div>
                                        <div class="modal-body">
                                            <fieldset>
                                                <legend>Research & Development Status (अनुसंधान और विकास योजना की स्थिति )
                                                </legend>
                                                <table class="table table-bordered">
                                                    <tr>
                                                        <td style="width: 50%">Research Type:
                                        <asp:Label ID="lbltype1" runat="server" ForeColor="Maroon">                                   
                                        </asp:Label>
                                                        </td>
                                                        <td style="width: 50%">Title:
                                        <asp:Label ID="lbltitle1" runat="server" ForeColor="Maroon">                                   
                                        </asp:Label>
                                                        </td>
                                                    </tr>

                                                </table>
                                            </fieldset>
                                            <span style="color: maroon">Progress Details</span>
                                            <table class="table table-bordered">
                                                <tr>
                                                    <td style="width: 100%" colspan="2">
                                                        <asp:GridView ID="grdProgress" CssClass="table table-bordered" runat="server" EmptyDataText="No Data Available" Width="100%" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Center" OnRowCommand="grdlist_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S.No">
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex+1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%-- <asp:BoundField DataField="ResearchTitle" HeaderText="Project Title" />--%>
                                                                <asp:BoundField DataField="ProgressDate" HeaderText="Progress Date" />
                                                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                                                <asp:BoundField DataField="RDStatus" HeaderText="Status" />

                                                            </Columns>

                                                        </asp:GridView>
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
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

