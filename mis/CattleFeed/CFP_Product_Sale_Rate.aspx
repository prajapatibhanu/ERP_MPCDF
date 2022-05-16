<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CFP_Product_Sale_Rate.aspx.cs" Inherits="mis_CattleFeed_CFP_Product_Sale_Rate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">

                        <div class="box-header">
                            <h3 class="box-title">Product Sale Master (प्रोडक्ट सेल रेट मास्टर)</h3>
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Product Sale Master Entry (प्रोडक्ट सेल रेट मास्टर प्रविष्टि)
                                </legend>
                                <div class="col-md-12">
                                    <asp:Label ID="lblMsg" CssClass="Autoclr" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Cattel Feed Plant <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ControlToValidate="ddlcfp" ValidationGroup="a" InitialValue="0" ErrorMessage="Select CFP." Text="<i class='fa fa-exclamation-circle' title='Select CFP !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlcfp" runat="server" Width="100%" CssClass="form-control select2" AutoPostBack="true" >
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Product Name(प्रोडक्ट का नाम) <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv1" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlProd" InitialValue="0" ErrorMessage="Please Select Product Name." Text="<i class='fa fa-exclamation-circle' title='Please Select Product Name !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlProd" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlProd_SelectedIndexChanged">
                                                <asp:ListItem Text="-- Select Product --" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Packaging Size(in Kg.)<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlpackaging" InitialValue="0" ErrorMessage="Please Select Packaging size." Text="<i class='fa fa-exclamation-circle' title='Please Select Product Name !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlpackaging" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Text="-- Select Packaging Size --" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                     
                                    
                                </div>
                                <div class="col-md-12">
                                   <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Office Type <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlOfficeType" InitialValue="0" ErrorMessage="Please Select Product Name." Text="<i class='fa fa-exclamation-circle' title='Please Select Product Name !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlOfficeType" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlOfficeType_SelectedIndexChanged">
                                                <asp:ListItem Text="-- Select office Type --" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4" id="office" runat="server">
                                        <div class="form-group">
                                            <label>Office <span style="color: red;">*</span></label>
                                           <%-- <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlProd" InitialValue="0" ErrorMessage="Please Select Product Name." Text="<i class='fa fa-exclamation-circle' title='Please Select Product Name !'></i>"></asp:RequiredFieldValidator>
                                            </span>--%>
                                            <asp:DropDownList ID="ddlOffice" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Text="-- Select Office --" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Rate/दर (Ex- factor rate per bag.)<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv6" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtRate" ErrorMessage="Please Enter Rate (Per Packet)." Text="<i class='fa fa-exclamation-circle' title='Please Enter Rate (Per Packet) !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="a" runat="server" ControlToValidate="txtRate" ErrorMessage="Please Enter Valid Number or two decimal value." Text="<i class='fa fa-exclamation-circle' title='Please Enter Valid Number or two decimal value. !'></i>"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox ID="txtRate" TabIndex="3" onkeypress="return validateDec(this,event);" autocomplete="off" onpaste="return false" runat="server" MaxLength="8" placeholder="Rate (Per Packet)" CssClass="form-control NameNumOnly"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Effective Date (प्रभावी दिनांक) </label>
                                            <span style="color: red">*</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtfromDt" ErrorMessage="Please Enter Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter Date !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revdate" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtfromDt" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtfromDt" onkeypress="javascript: return false;" Width="100%" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="col-md-12">

                                    <asp:Button Text="Save/सुरक्षित" ID="btnsave" ValidationGroup="a" CausesValidation="true" CssClass="btn btn-success" runat="server" OnClick="btnsave_Click" />
                                    &nbsp;&nbsp;<asp:Button ID="btnClear" runat="server" CausesValidation="false" CssClass="btn btn-default" Text="Reset/रीसेट" OnClick="btnClear_Click" />

                                </div>
                            </fieldset>
                            <div class="col-md-12">
                                <div class="box box-Manish">
                                    <div class="box-body">
                                        <fieldset>
                                            <legend>Registered Product Sale
                                            </legend>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:HiddenField ID="hdnproductsize" runat="server" Value="0" />
                                                    <asp:HiddenField ID="hdnvalue" runat="server" Value="0" />
                                                    <asp:GridView ID="grdCatlist" PageSize="20" runat="server" class="datatable table table-hover table-bordered pagination-ys" EmptyDataText="No Record Available" AutoGenerateColumns="False" AllowPaging="True" OnRowCommand="grdCatlist_RowCommand" OnPageIndexChanging="grdCatlist_PageIndexChanging">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="ProductName" HeaderText="Product" ItemStyle-Width="20%" />
                                                            <asp:BoundField DataField="PackagingSize" HeaderText="Packaging Size(in Kg.)" />
                                                            <asp:BoundField DataField="OfficeTypeName" HeaderText="Office Type" ItemStyle-Width="20%" />
                                                            <asp:BoundField DataField="OfficeName" HeaderText="Office Name" ItemStyle-Width="20%" />
                                                            <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-Width="10%" />
                                                            <asp:BoundField DataField="EffectiveDate" HeaderText="Effective Date" ItemStyle-Width="15%" />
                                                            <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LnkSelect" runat="server" CausesValidation="false" CommandName="RecordUpdate" CommandArgument='<%#Eval("ProductSaleID") %>' Text="Edit" OnClientClick="return confirm('CFP Entry will be edit. Are you sure want to continue?');"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="LnkDelete" runat="server" CausesValidation="false" CommandName="RecordDelete" CommandArgument='<%#Eval("ProductSaleID") %>' Text="Delete" Style="color: red;" OnClientClick="return confirm('CFP Entry will be deleted. Are you sure want to continue?');" Visible="false"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </fieldset>
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
    <script src="../../js/jquery-1.10.2.js"></script>
     <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>
     <script>
         $('.datatable').DataTable({
             paging: true,
             pageLength: 50,
             columnDefs: [{
                 targets: 'no-sort',
                 orderable: false
             }],
             dom: '<"row"<"col-sm-6"B><"col-sm-6"f>>' +
               '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
               '<"row"<"col-sm-5"i><"col-sm-7"p>>',
             fixedHeader: {
                 header: true
             },
             buttons: {
                 buttons: [{
                     extend: 'print',
                     text: '<i class="fa fa-print" style="display:none;"></i> Print',
                     title: $('h1').text(),
                     exportOptions: {
                         columns: [0, 1, 2, 3, 4, 5, 6]
                     },
                     footer: true,
                     autoPrint: true
                 }, {
                     extend: 'excel',
                     text: '<i class="fa fa-file-excel-o" style="display:none;"></i> Excel',
                     title: $('h1').text(),
                     exportOptions: {
                         columns: [0, 1, 2, 3, 4, 5, 6]
                     },
                     footer: true
                 }],
                 dom: {
                     container: {
                         className: 'dt-buttons'
                     },
                     button: {
                         className: 'btn btn-default'
                     }
                 }
             }
         });
    </script>
</asp:Content>

