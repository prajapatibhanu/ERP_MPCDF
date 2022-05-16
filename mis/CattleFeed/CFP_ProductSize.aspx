<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CFP_ProductSize.aspx.cs" Inherits="mis_CattleFeed_CFP_ProductSize" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">

                        <div class="box-header">
                            <h3 class="box-title">Product Size Master (प्रोडक्ट आकार मास्टर)</h3>
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Product Size Master Entry (प्रोडक्ट आकार मास्टर प्रविष्टि)
                                </legend>
                                <div class="col-md-12">
                                    <asp:Label ID="lblMsg" CssClass="Autoclr" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Production Unit Name<span class="hindi">(उत्पादन इकाई नाम)</span><span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvpro" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlcfp" InitialValue="0" ErrorMessage="Please Select Product Name." Text="<i class='fa fa-exclamation-circle' title='Please Select Production Unit !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlcfp" AutoPostBack="true" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlcfp_SelectedIndexChanged">
                                                <asp:ListItem Text="-- Select Prodution Unit --" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Product Name(प्रोडक्ट का नाम) <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv1" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlProd" InitialValue="0" ErrorMessage="Please Select Product Name." Text="<i class='fa fa-exclamation-circle' title='Please Select Product Name !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlProd" AutoPostBack="true" OnSelectedIndexChanged="ddlProd_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Text="-- Select Product --" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Product Unit<span class="text-danger">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlUnit" InitialValue="0" ErrorMessage="Please Select Product Unit." Text="<i class='fa fa-exclamation-circle' title='Please Select Product Unit !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlUnit" Enabled="false" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="-- Select Unit --" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Packaging Size (Per Bag(in Kg.))(पैकेट आकार प्रति बैग)<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv3" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtPackagSize" ErrorMessage="Please Enter Packaging Size (Per Bag)." Text="<i class='fa fa-exclamation-circle' title='Please Enter Packaging Size (Per Bag) !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="rev" Display="Dynamic" ValidationExpression="^[1-9][0-9]*$" ValidationGroup="a" runat="server" ControlToValidate="txtPackagSize" ErrorMessage="Please Enter Valid Number." Text="<i class='fa fa-exclamation-circle' title='Please Enter Valid Number !'></i>"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox ID="txtPackagSize" TabIndex="2" Width="66%" onkeypress="return validateNum(event);" onpaste="return false" autocomplete="off" runat="server" MaxLength="4" placeholder="Packaging Size" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4" style="text-align:left;">
                                        <div class="form-group">
                                            <label>Effective Date (प्रभावी दिनांक) </label>
                                            <span style="color: red">*</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtfromDt" ErrorMessage="Please Enter Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter Date !'></i>"></asp:RequiredFieldValidator>
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
                                    <%--   <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Rate/दर (Ex- factor rate per bag.)<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv6" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtRate" ErrorMessage="Please Enter Rate (Per Packet)." Text="<i class='fa fa-exclamation-circle' title='Please Enter Rate (Per Packet) !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="a" runat="server" ControlToValidate="txtRate" ErrorMessage="Please Enter Valid Number or two decimal value." Text="<i class='fa fa-exclamation-circle' title='Please Enter Valid Number or two decimal value. !'></i>"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox ID="txtRate" TabIndex="3" onkeypress="return validateDec(this,event);" autocomplete="off" onpaste="return false" runat="server" MaxLength="8" placeholder="Rate (Per Packet)" CssClass="form-control NameNumOnly"></asp:TextBox>
                                        </div>
                                    </div>--%>
                                </div>
                                <div class="col-md-12" style="text-align: center;">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button Text="Save" ID="btnSubmit" ValidationGroup="a" CssClass="btn btn-block btn-success" runat="server" OnClick="btnSubmit_Click" />
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button Text="Clear" ID="btnReset" CssClass="btn btn-block btn-default" runat="server" OnClick="btnReset_Click" CausesValidation="false" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Product Size Master Detail
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:HiddenField ID="hdnvalue" runat="server" Value="0" />
                                            <asp:GridView ID="gvOpeningStock" DataKeyNames="CFP_ProductSize_ID" runat="server" EmptyDataText="No records Found" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover" OnRowCommand="gvOpeningStock_RowCommand" PageSize="20" AllowPaging="true" OnPageIndexChanging="gvOpeningStock_PageIndexChanging">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.<br />(क्रं.)">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ItemName" HeaderText="Product Name (प्रोडक्ट का नाम)" />
                                                    <asp:BoundField DataField="Packaging_Size" HeaderText="Packaging Size(in Kg.)" />
                                                    <asp:BoundField DataField="UnitName" HeaderText="Unit Name" />
                                                    <asp:BoundField DataField="EffectiveDate" HeaderText="Effective Date" />
                                                    <asp:TemplateField HeaderText="Action <br />(कार्य)">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("CFP_ProductSize_ID") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                            &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("CFP_ProductSize_ID") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
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

