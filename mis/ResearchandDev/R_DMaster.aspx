<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="R_DMaster.aspx.cs" Inherits="mis_ResearchandDev_R_DMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <script src="../js/jquery.js"></script>
    <script>
        var hdnupload;
        function ShowPopupAddDates() {
            $('#addModalDates').modal('show');
        }
        $(document).ready(function () {
            hdnupload = $("#<%=hdnupload.ClientID%>");
            if (hdnupload.val() == 1) {
                $('#addModalDates').modal('show');
            }

        });
    </script>
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Research & Development Plan</h3>
                        </div>
                        <div class="box-body">
                            <fieldset>
                            <legend>Add / Update Research & Development Plan (नवीन अनुसंधान और विकास योजना की प्रविष्टी)
                            </legend>
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" CssClass="Autoclr" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:RadioButtonList runat="server" ID="rbtnRDType" RepeatDirection="Horizontal" CellPadding="3" CellSpacing="2"></asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Research & Development Plan for</label>
                                        <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control select2">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">New Product</asp:ListItem>
                                            <asp:ListItem Value="2">Existing Product</asp:ListItem>
                                            <asp:ListItem Value="3">Other Product</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="Rqtype" runat="server" Display="Dynamic" ErrorMessage="*" ForeColor="Red" BackColor="Yellow" ValidationGroup="a" InitialValue="0" ToolTip="Please Select Type" ControlToValidate="ddlType"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Start Date</label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false"></asp:TextBox>
                                        </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ErrorMessage="*" ForeColor="Red" BackColor="Yellow" ValidationGroup="a" ToolTip="Please Enter Start Date" ControlToValidate="txtStartDate"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>End Date</label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false"></asp:TextBox>
                                        </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ErrorMessage="*" ForeColor="Red" BackColor="Yellow" ValidationGroup="a" ToolTip="Please Enter End Date" ControlToValidate="txtEndDate"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>Research Title</label>
                                    <asp:TextBox ID="txtTilte" runat="server" CssClass="form-control">                                   
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="*" ForeColor="Red" BackColor="Yellow" ValidationGroup="a" ToolTip="Please Enter Title" ControlToValidate="txtTilte"></asp:RequiredFieldValidator>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>Research Details</label>
                                    <asp:TextBox ID="txtResearchDetails" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="*" ForeColor="Red" BackColor="Yellow" ValidationGroup="a" ToolTip="Please Enter Research Details" ControlToValidate="txtResearchDetails"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <%--  <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Research Document 1</label>
                                        <asp:fileupload id="fileupload" runat="server" cssclass="form-control"></asp:fileupload>
                                    </div>

                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Research Document 2</label>
                                        <asp:fileupload id="fileupload1" runat="server" cssclass="form-control"></asp:fileupload>
                                    </div>

                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Research Document 3</label>
                                        <asp:fileupload id="fileupload2" runat="server" cssclass="form-control"></asp:fileupload>
                                    </div>

                                </div>
                            </div>--%>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>Expected Outcomes</label>
                                    <asp:TextBox ID="txtoutcome" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control">                                   
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" runat="server" ErrorMessage="*" ForeColor="Red" BackColor="Yellow" ValidationGroup="a" ToolTip="Please Enter Expected OutCome" ControlToValidate="txtoutcome"></asp:RequiredFieldValidator>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>Special Note</label>
                                    <asp:TextBox ID="txtnote" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control">                                   
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" runat="server" ErrorMessage="*" ForeColor="Red" BackColor="Yellow" ValidationGroup="a" ToolTip="Please Enter Expected OutCome" ControlToValidate="txtnote"></asp:RequiredFieldValidator>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>Special Instruction</label>
                                    <asp:TextBox ID="txtinstruction" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control">                                   
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" Display="Dynamic" runat="server" ErrorMessage="*" ForeColor="Red" BackColor="Yellow" ValidationGroup="a" ToolTip="Please Enter Expected OutCome" ControlToValidate="txtinstruction"></asp:RequiredFieldValidator>
                                </div>

                            </div>

                            <div class="col-md-12">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info" Text="Submit" OnClick="btnSave_Click" CausesValidation="true" ValidationGroup="a" />
                                <asp:Button ID="btnClear" runat="server" CssClass="btn btn-primary" Text="Reset" OnClick="btnClear_Click" />
                                <asp:HiddenField ID="hdnupload" runat="server" Value="0" />
                                <asp:HiddenField ID="hdnvalue" runat="server" Value="0" />
                            </div>
                                </fieldset>
                           
                            <fieldset>
                            <legend>Regitered Research & Development Plan (अनुसंधान और विकास योजना की सूची)
                            </legend>
                            <div class="col-md-12">
                                <asp:GridView ID="grdlist" CssClass="table table-hover table-bordered pagination-ys" runat="server" Width="100%" PageSize="20" AllowPaging="true" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Center" OnRowCommand="grdlist_RowCommand" OnPageIndexChanging="grdlist_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RDType" HeaderText="Type" ItemStyle-Width="15%" />
                                       <%-- <asp:BoundField DataField="PlanType" HeaderText="Plan Type" />--%>
                                        <asp:BoundField DataField="ResearchTitle" HeaderText="Title" />
                                        <asp:BoundField DataField="StartDate" HeaderText="Start Date" ItemStyle-Width="10%"  />
                                        <asp:BoundField DataField="RDStatus" HeaderText="Status" ItemStyle-Width="8%"  />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnview" runat="server" Text="Edit" ToolTip="Edit"  CommandArgument='<%#Eval("RDPlanID") %>' CommandName="Change" ><i class="fa fa-pencil"></i></asp:LinkButton>
                                                <asp:LinkButton ID="btndoc" runat="server" Text="Upload Document(डॉक्यूमेंट अपलोड करें)" ToolTip="Upload Document(डॉक्यूमेंट अपलोड करें)" CommandArgument='<%#Eval("RDPlanID") %>' CommandName="UploadDoc" Style="color:maroon" ><i class="fa fa-upload"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div></fieldset>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="addModalDates" tabindex="-1" role="dialog" aria-labelledby="addModalDates">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <span style="color: white" id="uploadmsg" runat="server">अनुसंधान और विकास योजना के संबंधित डॉक्यूमेंट</span>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body">
                             
                            <div class="col-lg-12">
                                <asp:Label ID="lblPopupMsg" runat="server"></asp:Label>
                            </div>
                            <div class="col-lg-12" id="uploader" runat="server">
                                 <fieldset>
                            <legend>अनुसंधान और विकास योजना के संबंधित डॉक्यूमेंट अपलोड करें
                            </legend>
                                <div class="col-lg-6">
                                    <asp:FileUpload ID="FileUpload3" runat="server" />
                                </div>
                                <div class="col-lg-6">
                                    <asp:Button ID="btnUpload" runat="server" Text="Upload Document" OnClick="UploadFile" />
                                </div></fieldset>
                            </div>
                            <hr />
                            <table id="uploadedgrd" runat="server" class="table table-bordered" style="width: 100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="false" EmptyDataText="No document is available">
                                            <Columns>
                                                <asp:BoundField DataField="ImageType" HeaderText="Document Name" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <%----%>
                                                        <asp:LinkButton ID="lnkDownload" Text="Download" CommandArgument='<%# Eval("ImagePath") %>' runat="server" OnClick="DownloadFile" Style="color:blue" ><i class="fa fa-download"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkDelete" Text="Delete" CommandArgument='<%# Eval("ImagePath") %>' runat="server" OnClick="DeleteFile" Style="color:red" ><i class="fa fa-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="30%" HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>

                            </table>
                            <table id="uploadedDoc" runat="server" class="table table-bordered" style="width: 100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" EmptyDataText="No files uploaded" Width="100%">
                                            <Columns>
                                                <asp:BoundField DataField="ImageType" HeaderText="Document Name" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <%----%>
                                                        <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("ImagePath") %>' ToolTip="Download" runat="server" OnClick="DownloadFile" Style="color:blue"><i class="fa fa-download"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
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
                            <%--OnClick="btnupdate_Click"--%>
                            <asp:Button runat="server" ID="btnupdate" class="btn btn-success" Text="Save Document" OnClick="btnupdate_Click"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>


