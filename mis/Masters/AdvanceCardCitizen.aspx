<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="AdvanceCardCitizen.aspx.cs" Inherits="mis_AdvanceCardCitizen" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <%--Confirmation Modal Start --%>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                    </button>
                    <h4 class="modal-title" id="myModalLabel">Confirmation</h4>
                </div>
                <div class="clearfix"></div>
                <div class="modal-body">
                    <p>
                        <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp; 
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                    </p>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSubmit_Click" Style="margin-top: 20px; width: 50px;" />
                    <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>

    <%--Confirmation Modal Start for Mapping Item --%>
    <div class="modal fade" id="myModalMaping" tabindex="-1" role="dialog" aria-labelledby="myModalLabelMapping" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                    </button>
                    <h4 class="modal-title" id="myModalLabelMapping">Confirmation</h4>
                </div>
                <div class="clearfix"></div>
                <div class="modal-body">
                    <p>
                        <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp; 
                            <asp:Label ID="lblPopupAlertMapping" runat="server"></asp:Label>
                    </p>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnMYes" OnClick="btnMSubmit_Click" Style="margin-top: 20px; width: 50px;" />
                    <asp:Button ID="btnMNo" ValidationGroup="noo" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="c" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <div class="col-md-6">
                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">Citizen Advance Card Details / नागरिक एडवांस कार्ड विवरण</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <fieldset>
                                    <legend>Citizen Advance Card Registration / नागरिक एडवांस कार्ड पंजीयन</legend>
                                    <div class="row">
                                          <div class="col-md-12">
                                               <div class="form-group">
                                        <span style="color:red">Note: Registration of Citizen allowed between 10th to 15th of Every Month</span>
                                                   </div>
                                              </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Citizen Name / नागरिक नाम<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                        ErrorMessage="Enter Citizen (Citizen Name)" Text="<i class='fa fa-exclamation-circle' title='Enter Citizen (Citizen Name) !'></i>"
                                                        ControlToValidate="txtCitizenName" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtCitizenName"
                                                        ErrorMessage="Only Alphabet allow in Citizen Name" Text="<i class='fa fa-exclamation-circle' title='Only Alphabet allow in Citizen Name !'></i>"
                                                        SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[a-z-A-Z\s]+$">
                                                    </asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtCitizenName" placeholder="Enter Citizen Name" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Mobile Number / मोबाइल नंबर<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                        ErrorMessage="Mobile Number" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Mobile Number!'></i>"
                                                        ControlToValidate="txtMobNo" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ValidationGroup="a"
                                                        ErrorMessage="Invalid Contact Number (Mobile). !" Text="<i class='fa fa-exclamation-circle' title='Contact Mobile Number!'></i>" ControlToValidate="txtMobNo"
                                                        ValidationExpression="[6-9]{1}[0-9]{9}">
                                                    </asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control MobileNo" ID="txtMobNo" MaxLength="10" onkeypress="return validateNum(event);" placeholder="Enter Mobile Number"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />

                                                <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-default" />
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <fieldset>
                                    <legend>Citizen Advance Card Registration Details</legend>
                                    
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                    EmptyDataText="No Record Found." DataKeyNames="CitizenId">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No / क्र.">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-- <asp:TemplateField HeaderText="Company Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCitizenId" runat="server" Visible="false" Text='<%# Eval("CitizenId") %>'></asp:Label>
                                                <asp:Label ID="lblCitizenName" runat="server" Visible="false" Text='<%# Eval("CitizenName") %>'></asp:Label>
                                                <asp:Label ID="lblMobNo" runat="server" Visible="false" Text='<%# Eval("MobNo") %>'></asp:Label>
                                                <asp:Label ID="lblCitizenCode" Visible="false" runat="server" Text='<%# Eval("CitizenCode") %>' />
                                                <asp:Label ID="lblEffectiveFromDate" Visible="false" runat="server" Text='<%# Eval("EffectiveFromDate") %>' />
                                                <asp:Label ID="lblEffectiveToDate" Visible="false" runat="server" Text='<%# Eval("EffectiveToDate") %>' />
                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Citizen Name / नागरिक नाम">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCitizenName" runat="server" Text='<%# Eval("CitizenName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Mobile / मोबाइल नंबर">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMobNo" runat="server" Text='<%# Eval("MobNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Citizen Code / नागरिक कोड">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCitizenCode" runat="server" Text='<%# Eval("CitizenCode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-- <asp:TemplateField HeaderText="Effective From Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEffectiveFromDate" runat="server" Text='<%# Eval("EffectiveFromDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Effective To Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEffectiveToDate" runat="server" Text='<%# Eval("EffectiveToDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Action / कार्यवाही करें">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("CitizenId") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>

                                                               <%-- <asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("CitizenId") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash" style="color: red;"></i></asp:LinkButton>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="col-md-6" id="pnlItemMapping" runat="server" visible="false">
                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">Citizen Item Mapping Details / नागरिक एवं वस्तु प्रतिचित्रण का विवरण</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMappingMsg" runat="server"></asp:Label>
                                </div>
                            </div>
                            <fieldset>
                                <legend>Citizen Name & Citizen Item Mapping / नागरिक नाम और नागरिक वस्तु प्रतिचित्रण</legend>
                                 <div class="row">
                                          <div class="col-md-12">
                                               <div class="form-group">
                                        <span style="color:red">Note: Item Mapping of Citizen allowed between 10th to 15th of Every Month</span>
                                                   </div>
                                              </div>
                                    </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Citizen Name<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="b"
                                                    InitialValue="0" ErrorMessage="Select Citizen Name" Text="<i class='fa fa-exclamation-circle' title='Select Citizen Name !'></i>"
                                                    ControlToValidate="ddlCitizenName" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>

                                            <asp:DropDownList ID="ddlCitizenName" runat="server" OnSelectedIndexChanged="ddlCitizenName_SelectedIndexChanged" ClientIDMode="Static" AutoPostBack="true" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <asp:Panel ID="pnlCitizenItemMapping" runat="server" Visible="false">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Item Category/वस्तु वर्ग<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="b"
                                                        InitialValue="0" ErrorMessage="Select Type" Text="<i class='fa fa-exclamation-circle' title='Select Type !'></i>"
                                                        ControlToValidate="ddlItemCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator></span>

                                                <asp:DropDownList ID="ddlItemCategory" OnInit="ddlItemCategory_Init" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged" runat="server" AutoPostBack="true" CssClass="form-control select2"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Item Name/वस्तु नाम<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="b"
                                                        InitialValue="0" ErrorMessage="Select Item" Text="<i class='fa fa-exclamation-circle' title='Select Item !'></i>"
                                                        ControlToValidate="ddlItem" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator></span>
                                                <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-control select2">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Item Qty / वस्तु मात्रा<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="b"
                                                        ErrorMessage="Enter Item Qty." Text="<i class='fa fa-exclamation-circle' title='Enter Item Qty. !'></i>"
                                                        ControlToValidate="txtItemQty" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic" ValidationGroup="b"
                                                        ErrorMessage="Invalid Item Qty, only numeric allow. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Invalid Item Qty, only numeric allow. !'></i>" ControlToValidate="txtItemQty"
                                                        ValidationExpression="^[0-9]+$">
                                                    </asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtItemQty" MaxLength="5" onkeypress="return validateNum(event);" placeholder="Enter Item Qty." ClientIDMode="Static"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Shift / शिफ्ट <span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="b"
                                                        InitialValue="0" ErrorMessage="Select Shift" Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"
                                                        ControlToValidate="ddlShift" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator></span>
                                                <asp:DropDownList ID="ddlShift" OnInit="ddlShift_Init" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Effective From Date / दिनांक से प्रभावी<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="b"
                                                        ErrorMessage="Enter Effective From Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Effective From Date !'></i>"
                                                        ControlToValidate="txtEffectiveFromDate" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>

                                                    <asp:RegularExpressionValidator ID="revdate" ValidationGroup="b" runat="server" Display="Dynamic" ControlToValidate="txtEffectiveFromDate"
                                                        ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                        ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>

                                                </span>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtEffectiveFromDate" OnTextChanged="txtEffectiveFromDate_TextChanged" AutoPostBack="true" MaxLength="10" placeholder="Enter Effective From Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Effective To Date / दिनांक तक प्रभावी<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="b"
                                                        ErrorMessage="Enter Effective To Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Effective To Date !'></i>"
                                                        ControlToValidate="txtEffectiveToDate" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="b" runat="server" Display="Dynamic" ControlToValidate="txtEffectiveToDate"
                                                        ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                        ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>

                                                </span>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtEffectiveToDate" OnTextChanged="txtEffectiveToDate_TextChanged" AutoPostBack="true" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" onpaste="return false;" onkeypress="return false;" MaxLength="10" placeholder="Enter Effective From Date" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>


                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="b" ID="btnMSubmit" Text="Save" OnClientClick="return MappingValidatePage();" AccessKey="V" />

                                                <asp:Button ID="btnMClear" runat="server" OnClick="btnMClear_Click" Text="Clear" CssClass="btn btn-default" />
                                            </div>
                                        </div>
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Citizen Item Mapping Details</legend>
                                <div class="row">
                                          
                                       <%--<div class="col-md-12" style="font-style:normal;color:red">Note-: Item Quantity can only be Updated </div>
                                    <br />--%>
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                EmptyDataText="No Record Found." OnRowCommand="GridView2_RowCommand" DataKeyNames="CitizenItemMappingId">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No /क्र.">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Citizen Name/नागरिक नाम">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCitizenName" runat="server" Text='<%# Eval("CName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name/वस्तु नाम">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Qty/वस्तु मात्रा">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalItemQty" runat="server" Text='<%# Eval("Total_ItemQty") %>'></asp:Label>
                                                            <asp:RequiredFieldValidator ID="rfv10" ValidationGroup="c"
                                                                ErrorMessage="Enter Item Qty." Enabled="false" Text="<i class='fa fa-exclamation-circle' title='Enter Item Qty. !'></i>"
                                                                ControlToValidate="txtTotalItemQty" ForeColor="Red" Display="Dynamic" runat="server">
                                                            </asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="rev10" Enabled="false" runat="server" Display="Dynamic" ValidationGroup="c"
                                                                ErrorMessage="Invalid Item Qty, Accept Number only. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Invalid Item Qty, Accept Number only. !'></i>" ControlToValidate="txtTotalItemQty"
                                                                ValidationExpression="^[0-9]+$">
                                                            </asp:RegularExpressionValidator>
                                                            </span>
                                               <asp:TextBox runat="server" autocomplete="off" Visible="false" CausesValidation="true" Text='<%# Eval("Total_ItemQty")%>' CssClass="form-control" ID="txtTotalItemQty" MaxLength="5" onkeypress="return validateNum(event);" placeholder="Enter Item Qty." ClientIDMode="Static"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shift/शिफ्ट">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblShiftName" runat="server" Text='<%# Eval("ShiftName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Effective FromDate/दिनांक से प्रभावी ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEffectiveFromDate" runat="server" Text='<%# Eval("EffectiveFromDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Effective ToDate/दिनांक तक प्रभावी">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEffectiveToDate" runat="server" Text='<%# Eval("EffectiveToDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action/कार्यवाही करें">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" CommandName="RecordEdit" CommandArgument='<%#Eval("CitizenItemMappingId") %>' runat="server" ToolTip="Edit"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                            &nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkUpdate" Visible="false" ValidationGroup="c" CausesValidation="true" CommandName="RecordUpdate" CommandArgument='<%#Eval("CitizenItemMappingId") %>' runat="server" ToolTip="Update" Style="color: darkgreen;" OnClientClick="return confirm('Are you sure to Update?')"><i class="fa fa-check"></i></asp:LinkButton>
                                                            &nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkReset" Visible="false" CommandName="RecordReset" CommandArgument='<%#Eval("CitizenItemMappingId") %>' runat="server" ToolTip="Reset" Style="color: gray;"><i class="fa fa-times"></i></asp:LinkButton>
                                                           <%-- &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkMappingDelete" CommandArgument='<%#Eval("CitizenItemMappingId") %>' CommandName="MappingRecordDel" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                            </fieldset>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }

        function MappingValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('b');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnMSubmit.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlertMapping.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModalMaping').modal('show');
                    return false;
                }
            }
        }
        $("#txtEffectiveFromDate").datepicker({
            autoclose: true
        });
        $("#txtEffectiveToDate").datepicker({
            autoclose: true
        });

    </script>
</asp:Content>

