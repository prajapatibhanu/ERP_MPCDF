<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HSNMaster.aspx.cs" Inherits="mis_Masters_HSNMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="col-md-12">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <div class="row">
            <div class="col-md-6">
                <div class="card card-primary">
                    <div class="card-header">
                        <h3 class="card-title">HSN Master Entry</h3>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Type of Supply<span style="color: red;"> *</span></label>
                                    <asp:DropDownList ID="ddlTypeOfSupply" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlTypeOfSupply_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="Goods">Goods</asp:ListItem>
                                        <asp:ListItem Value="Services">Services</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>HSN/SAC Code <span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtHSNCode" ErrorMessage="Enter HSN Code." Text="<i class='fa fa-exclamation-circle' title='Enter HSNCode !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rev" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtHSNCode"
                                            ValidationExpression="^[0-9\s]+$"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Only Numbers Allow !'></i>" ErrorMessage="Only Numbers Allow" />
                                    </span>
                                    <asp:TextBox ID="txtHSNCode" MaxLength="5" runat="server" placeholder="HSN/SAC Code" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <asp:Panel ID="pnldate" runat="server" Visible="false">
                                <div class="col-md-4 hidden">
                                    <div class="form-group">
                                        <label>HSN/SAC Effective Date<span style="color: red;"> *</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtHSNEffectiveDate" placeholder="DD/MM/YYYY" data-date-start-date="0d" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <small><span id="valtxtHSNEffectiveDate" style="color: red;"></span></small>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <fieldset>
                                    <legend>IGST/SGST/CGST %</legend>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Integrated Tax / IGST<span style="color: red;"> *</span></label>
                                                <%-- <asp:TextBox ID="txtIntegratedTax" runat="server" placeholder="Integrated Tax / IGST" CssClass="form-control Amount" OnTextChanged="txtIntegratedTax_TextChanged" AutoPostBack="true"></asp:TextBox>--%>
                                                <asp:DropDownList ID="ddlIntegratedTax" runat="server" CssClass="form-control Amount" OnSelectedIndexChanged="ddlIntegratedTax_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem Value="0">0% (Nil Rated)</asp:ListItem>
                                                    <asp:ListItem Value="5">5%</asp:ListItem>
                                                    <asp:ListItem Value="12">12%</asp:ListItem>
                                                    <asp:ListItem Value="18">18%</asp:ListItem>
                                                    <asp:ListItem Value="28">28%</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>CGST%<span style="color: red;"> *</span></label>
                                                <asp:TextBox ID="txtCGST" runat="server" placeholder="CGST%" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>SGST %<span style="color: red;"> *</span></label>
                                                <asp:TextBox ID="txtSGST" runat="server" placeholder="SGST %" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Button ID="btnSave" CssClass="btn btn-primary mt-2" ValidationGroup="a" runat="server" Text="Save" OnClick="btnSubmit_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="card card-primary">
                    <div class="card-header">
                        <h3 class="card-title">HSN Master List</h3>
                    </div>
                    <div class="card-body">
                        <div class="table-responsive">
                            <asp:GridView ID="GridView1" DataKeyNames="HSN_ID" runat="server" class="datatable table table-hover table-bordered" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="5%"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="HSN_TypeOfSupply" HeaderText="Type Of Supply" />
                                    <asp:BoundField DataField="HSN_Code" HeaderText="HSN/SAC Code" />
                                    <asp:BoundField DataField="HSN_IntegratedTax" HeaderText="Integrated Tax/IGST" />
                                    <asp:BoundField DataField="HSN_CGST" HeaderText="CGST %" />
                                    <asp:BoundField DataField="HSN_SGST" HeaderText="SGST %" />
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Select" runat="server" CssClass="label label-default" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div id="myModal" class="modal fade" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">HSN Code Detail</h4>
                            <asp:Label ID="lblmodalmsg" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>HSN/SAC Code</label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txt_HSNCode" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Integrated Tax / IGST (%)<span style="color: red">*</span></label>
                                        <%-- <asp:TextBox runat="server" CssClass="form-control Amount" ID="txt_IntegratedTax" OnTextChanged="txt_IntegratedTax_TextChanged" AutoPostBack="true"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddl__IntegratedTax" runat="server" CssClass="form-control Amount" OnSelectedIndexChanged="ddl__IntegratedTax_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem Value="0">0% (Nil Rated)</asp:ListItem>
                                            <asp:ListItem Value="5">5%</asp:ListItem>
                                            <asp:ListItem Value="12">12%</asp:ListItem>
                                            <asp:ListItem Value="18">18%</asp:ListItem>
                                            <asp:ListItem Value="28">28%</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>CGST(%)</label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txt_CGST" placeholder="CGST%" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>SGST(%)</label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txt_SGST" placeholder="SGST%" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3 hidden">
                                    <div class="form-group">
                                        <label>HSN/SAC Effective Date<span style="color: red;"> *</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txt_HSNEffectiveDate" placeholder="DD/MM/YYYY" data-date-start-date="0d" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <small><span id="valtxt_HSNEffectiveDtae" style="color: red;"></span></small>
                                    </div>
                                </div>
                                <%--<div class="col-md-3">
                                <div class="form-group">
                                    <label>Texiblity <span style="color: red;">*</span></label><br />
                                    <asp:DropDownList ID="ddlTaxabilityEdit" runat="server" Width="100%" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlTaxabilityEdit_SelectedIndexChanged">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Taxable" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Exempt" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Nil Rated" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>--%>
                            </div>
                            <div class="row">
                                <%--<div class="col-md-3">
                                <div class="form-group">
                                    <label>Effective From</label>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox runat="server" CssClass="form-control DateAdd" ClientIDMode="Static" ID="EffectiveDate" autocomplete="off" placeholder="Enter Effective Date"></asp:TextBox>
                                    </div>
                                </div>
                            </div>--%>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-success" Text="Update" ClientIDMode="Static" OnClick="btnUpdate_Click" OnClientClick="return validatemodalform();" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <strong>
                                            <asp:Label ID="Label1" runat="server" Text="HSN History"></asp:Label></strong>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">

                                    <asp:GridView ID="GridView2" runat="server" class="table table-hover table-bordered" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="5%"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="HSNChild_EffectiveDate" HeaderText="Updated Date" />
                                            <%--<asp:BoundField DataField="Texiblity" HeaderText="Texiblity" />--%>
                                            <asp:BoundField DataField="HSNChild_IntegratedTax" HeaderText="Integrated Tax/IGST" />
                                            <asp:BoundField DataField="HSNChild_CGST" HeaderText="CGST %" />
                                            <asp:BoundField DataField="HSNChild_SGST" HeaderText="SGST %" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
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
            paging: false,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
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
        function CallModal() {
            $('#myModal').modal('show');
        }

        function validateNum(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function validateform() {
            var msg = "";
            var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

            if (document.getElementById('<%=ddlTypeOfSupply.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Type of Supply. \n";
            }
            if (document.getElementById('<%=txtHSNCode.ClientID%>').value.trim() == "") {
                msg = msg + "Enter HSN/SAC Code. \n";
            }
            if (document.getElementById('<%=txtHSNEffectiveDate.ClientID%>').value.trim() == "") {
                msg = msg + "Enter HSN/SAC Effective Date. \n";
            }
            if (document.getElementById('<%=ddlIntegratedTax.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Integrated Tax/ IGST. \n";
            }



            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    if (confirm("Do you really want to Save Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }

            }
        }
        function validatemodalform() {
            var msg = "";
            if (document.getElementById('<%=ddl__IntegratedTax.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Integrated Tax / IGST(%). \n";
            }
           <%-- if (document.getElementById('<%=txt_HSNEffectiveDate.ClientID%>').value.trim() == "") {
                msg = msg + "Enter HSN/SAC Effective Date. \n";
            }--%>

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=btnUpdate.ClientID%>').value.trim() == "Update") {
                    if (confirm("Do you really want to Update Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }

            }
        }
        $('.Amount').keypress(function (evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;

            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46) {
                // alert("Please enter only Numbers.");
                return false;
            }
        });
    </script>
</asp:Content>

