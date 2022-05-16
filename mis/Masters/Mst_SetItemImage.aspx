<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Mst_SetItemImage.aspx.cs" Inherits="mis_Masters_Mst_SetItemImage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="C" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="row">

                <div class="box box-Manish">
                    <div class="box-header">
                        <h3 class="box-title">Set Item Image/ Advance Card Rate</h3>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Item Category / वस्तू वर्ग</label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvic" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select Item Category" Text="<i class='fa fa-exclamation-circle' title='Select ItemCategory !'></i>"
                                            ControlToValidate="ddlItemCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <%--<asp:DropDownList ID="ddlItemCategory" AutoPostBack="true" OnInit="ddlItemCategory_Init" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged"  runat="server" CssClass="form-control select2"></asp:DropDownList>--%>
                                    <asp:DropDownList ID="ddlItemCategory" OnInit="ddlItemCategory_Init" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-1">
                                <div class="form-group" style="margin-top: 20px;">
                                    <asp:Button runat="server" CssClass="btn btn-block btn-primary" OnClick="btnSearch_Click" ValidationGroup="a" ID="btnSearch" Text="Search" />

                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView1" OnRowCommand="GridView1_RowCommand" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                        EmptyDataText="No Record Found." DataKeyNames="ItemOffice_ID">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ItemCatName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItem_id" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                    <asp:Label ID="lblItemCatName" runat="server" Text='<%# Eval("ItemCatName") %>'></asp:Label>
                                                    <asp:Label ID="lblItemImgOriginal" Visible="false" runat="server" Text='<%# Eval("ItemImg_Original") %>'></asp:Label>
                                                    <asp:Label ID="lblItemImgThumb" Visible="false" runat="server" Text='<%# Eval("ItemImg_Thumb") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ItemName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgthumb" Height="65px" Width="65px" ImageUrl='<%# Convert.ToString(Eval("ItemImg_Thumb")) == "" ? "~/mis/image/Thumb_ItemImg/No-image-available.png" : "~/mis/image/Thumb_ItemImg/" + Eval("ItemImg_Thumb") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Adv. Card Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemAdvCardRate" runat="server" Text='<%# Eval("ItemAdvCardRate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="HFItemAdvCart_Status" Value='<%#Eval("ItemAdvCart_Status") %>' runat="server" />
                                                    <asp:LinkButton ID="BtnStatus" CommandArgument='<%# Eval("ItemOffice_ID") %>' CommandName="ChangeStatus" OnClientClick="return confirm('Are you sure to Update Item Status?')" CssClass='<%# Eval("ItemAdvCart_Status").ToString()=="False" ? "label label-danger" : "label label-success" %>' runat="server" Text='<%# Eval("ItemAdvCart_Status").ToString()=="False" ? "Not Active" : "Active" %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("ItemOffice_ID") %>' CssClass="btn button-mini button-green" runat="server" ToolTip="Update Item Image"><i class="btn btn-success fa fa-picture-o"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="LinkButton1" CommandName="RecordAdvCard" CommandArgument='<%#Eval("ItemOffice_ID") %>' CssClass="btn button-mini button-green" runat="server" ToolTip="Update Adv Card Rate"><i class="btn btn-warning fa fa-inr"></i></asp:LinkButton>
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
            <div class="modal" id="ItemDetailsModal">
                <div style="display: table; height: 100%; width: 100%;">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Item Category : <span id="modelcategory" style="color: red" runat="server"></span>
                                    &nbsp;&nbsp;Item Name : <span id="modelitem" style="color: red" runat="server"></span>
                                </h4>
                            </div>
                            <div class="modal-body">
                                <div id="divitem" runat="server">
                                    <asp:Label ID="lblModalMsg" runat="server" Text=""></asp:Label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <fieldset>
                                                <legend>Upload Image</legend>
                                                <div class="row" style="height: 125px; overflow: scroll;">

                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>Image<span style="color: red;"> *</span></label>
                                                            <span class="pull-right">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="b" runat="server" ControlToValidate="fuImage" ErrorMessage="Please Select Image" Text="<i class='fa fa-exclamation-circle' title='Please Select Image !'></i>"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="b" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.PNG|.jpg|.JPG|.jpeg|.JPEG)$"
                                                                    ControlToValidate="fuImage" runat="server" ForeColor="Red" ErrorMessage="select only .png,.jpeg,.jpg file."
                                                                    Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Please select only .png,.jpeg,.jpg file. !'></i>" />
                                                            </span>
                                                            <asp:FileUpload ID="fuImage" runat="server" ClientIDMode="Static" />
                                                        </div>
                                                    </div>
                                                    <%--  <div class="col-md-4">
                                                            <div class="form-group">
                                                            <label>Adv Card Rate</label>
                                                                <asp:TextBox ID="txtAdvCardRate" MaxLength="8" runat="server"></asp:TextBox>
                                                            </div>
                                                    </div>--%>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <%-- <button type="button" class="btn btn-default" data-dismiss="modal">CLOSE </button>--%>
                                <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="b" ID="btnSubmit" Text="Update" OnClientClick="return ValidatePage();" AccessKey="S" />
                            </div>
                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->

                </div>
            </div>

            <div class="modal" id="ItemDetailsModalRate">
                <div style="display: table; height: 100%; width: 100%;">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Item Category : <span id="modelcategory1" style="color: red" runat="server"></span>
                                    &nbsp;&nbsp;Item Name : <span id="modelitem1" style="color: red" runat="server"></span>
                                </h4>
                            </div>
                            <div class="modal-body">
                                <div id="div1" runat="server">
                                    <asp:Label ID="lblModalMsg1" runat="server" Text=""></asp:Label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <fieldset>
                                                <legend>Update Item Adv. Card Rate</legend>
                                                <div class="row" style="height: 125px; overflow: scroll;">
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>Adv Card Rate</label>
                                                            <span class="pull-right">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="c"
                                                                    ErrorMessage="Enter Adv Card Rate" Text="<i class='fa fa-exclamation-circle' title='Enter Adv Card Rate !'></i>"
                                                                    ControlToValidate="txtAdvCardRate" ForeColor="Red" Display="Dynamic" runat="server">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="c" Display="Dynamic" runat="server" ControlToValidate="txtAdvCardRate"
                                                                    ErrorMessage="Only Numeric or Decimal with two place allow" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Only Numeric or Decimal with two place allow. !'></i>"
                                                                    SetFocusOnError="true" ValidationExpression="^(0|[1-9]\d*)(\.\d+)?$">
                                                                </asp:RegularExpressionValidator>
                                                            </span>
                                                            <asp:TextBox ID="txtAdvCardRate" autocomplete="off" onkeypress="return validateDecTwoplace(this,event)" CssClass="form-control" MaxLength="8" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <%-- <button type="button" class="btn btn-default" data-dismiss="modal">CLOSE </button>--%>
                                <asp:Button runat="server" CssClass="btn btn-primary" OnClientClick="return ValidatePage1();" ValidationGroup="c" ID="btnUpdateRate" Text="Update" AccessKey="S" />
                            </div>
                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->

                </div>
            </div>

            <%--Confirmation Modal Start --%>
            <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div style="display: table; height: 100%; width: 100%;">
                    <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                        <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">
                                    <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                                </button>
                                <h4 class="modal-title" id="myModalLabel">Confirmation</h4>
                            </div>
                            <div class="clearfix"></div>
                            <div class="modal-body">
                                <p>
                                    <i class="fa fa-2x fa-question-circle"></i>
                                    <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                                </p>
                            </div>
                            <div class="modal-footer" style="text-align: center;">
                                <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" />
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
            </div>
            <%--ConfirmationModal End --%>

            <%--Confirmation Modal Start --%>
            <div class="modal fade" id="myModal1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div style="display: table; height: 100%; width: 100%;">
                    <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                        <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">
                                    <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                                </button>
                                <h4 class="modal-title" id="myModalLabel1">Confirmation</h4>
                            </div>
                            <div class="clearfix"></div>
                            <div class="modal-body">
                                <p>
                                    <i class="fa fa-2x fa-question-circle"></i>
                                    <asp:Label ID="lblPopupAlert1" runat="server"></asp:Label>
                                </p>
                            </div>
                            <div class="modal-footer" style="text-align: center;">
                                <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnyesno" OnClick="btnUpdateRate_Click" />
                                <asp:Button ID="Button2" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" />
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
            </div>
            <%--ConfirmationModal End --%>
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('b');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this Image?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }

        function ValidatePage1() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('c');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnUpdateRate.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert1.ClientID%>').textContent = "Are you sure you want to Update this Adance Card Rate?";
                    $('#myModal1').modal('show');
                    return false;
                }
            }
        }
        // function validateDecThreeplace(el, evt) {
        function validateDecTwoplace(el, evt) {
            var digit = 2;
            //var digit = 3;
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (digit == 0 && charCode == 46) {
                return false;
            }

            var number = el.value.split('.');
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            //just one dot (thanks ddlab)
            if (number.length > 1 && charCode == 46) {
                return false;
            }
            //get the carat position
            var caratPos = getSelectionStart(el);
            var dotPos = el.value.indexOf(".");
            if (caratPos > dotPos && dotPos > -1 && (number[1].length > digit - 1)) {
                return false;
            }
            return true;
        }
        function myItemDetailsModal() {
            $("#ItemDetailsModal").modal('show');

        }

        function myItemDetailsModal1() {
            $("#ItemDetailsModalRate").modal('show');

        }
    </script>
</asp:Content>

