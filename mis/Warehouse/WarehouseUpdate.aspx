<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="WarehouseUpdate.aspx.cs" Inherits="mis_Warehouse_WarehouseUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <script type="text/javascript">
        function textboxMultilineMaxNumber(txt, maxLen) {
            try {
                if (txt.value.length > (maxLen - 1)) return false;
            } catch (e) { }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">
                    <!-- general form elements -->
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">Edit Warehouse detail / गोदाम विवरण संपादित करे</h3>
                            <asp:Label ID="lblMsg" runat="server" Text="" Visible="true"></asp:Label>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">

                            <fieldset>
                                <legend>Warehouse  Detail</legend>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Warehouse Name / गोदाम का नाम<span style="color: red">*</span></label>
                                            
                                            <asp:TextBox runat="server" autocomplete="off" placeholder="Enter Warehouse Name" ID="txtWarehouseName" MaxLength="50" CssClass="form-control" ClientIDMode="Static" onkeypress="return validatename(event)"></asp:TextBox>
                                            <small><span id="valtxtWarehouseName" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Area (In Sqft.) / क्षेत्र (वर्गफुट में)<span style="color: red">*</span></label>
                                            <asp:TextBox runat="server" autocomplete="off" placeholder="Enter Area" ID="txtArea" MaxLength="20" CssClass="form-control" ClientIDMode="Static" onkeypress="return validateNum(event)"></asp:TextBox>
                                            <small><span id="valtxtArea" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label>Warehouse Capacity (In Tonne)/ गोदाम की क्षमता (टन में)<span style="color: red">*</span></label>
                                            <asp:TextBox runat="server" autocomplete="off" placeholder="Enter Warehouse Capacity" ID="txtCapacity" MaxLength="25" CssClass="form-control" ClientIDMode="Static" onkeypress="return validateNum(event)"></asp:TextBox>
                                            <small><span id="valtxtCapacity" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Address / पता<span style="color: red">*</span></label>
                                            <asp:TextBox runat="server" autocomplete="off" placeholder="Enter Address" ID="txt_Address" MaxLength="200" TextMode="MultiLine" CssClass="form-control" ClientIDMode="Static" onkeypress="return textboxMultilineMaxNumber(this,200);"></asp:TextBox>
                                            <small><span id="valtxt_Address" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Incharge Detail</legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Incharge Name / प्रभारी नाम<span style="color: red">*</span></label>
                                            <asp:TextBox runat="server" autocomplete="off" placeholder="Enter Incharge Name" ID="txtInchageName" MaxLength="30" CssClass="form-control" ClientIDMode="Static" onkeypress="return validatename(event)"></asp:TextBox>
                                            <small><span id="valtxtInchageName" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Incharge Mobile No. / प्रभारी मोबाइल<span style="color: red">*</span></label>
                                            <asp:TextBox runat="server" autocomplete="off" placeholder="Enter Incharge Mobile No." ID="txtMobileNo" MaxLength="10" CssClass="form-control" ClientIDMode="Static" onkeypress="return validateNum(event)"></asp:TextBox>
                                            <small><span id="valtxtMobileNo" style="color: red;"></span>
                                           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ControlToValidate="txtMobileNo" ValidationExpression="^[6789]\d{9}$" SetFocusOnError="true" ErrorMessage="Invalid Mobile Number" ForeColor="Red"></asp:RegularExpressionValidator></small>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Incharge Email / प्रभारी ईमेल</label>
                                            <asp:TextBox runat="server" autocomplete="off" placeholder="Enter Incharge Email" ID="txtemail" MaxLength="50" CausesValidation="true" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                            <small><span id="valtxtemail" style="color:red;"></span></small>
                                         </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Office (Supervision By)<span style="color: red">*</span></label>
                                            <asp:DropDownList runat="server" ID="ddloffice" CssClass="form-control select2" ClientIDMode="static">
                                                <asp:ListItem>Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <small><span id="valddloffice" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                            <fieldset>
                                <legend>Warehouse Type (Owned / Rented)</legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label><span style="color: red">*</span>Owned-Rented
                                                <br />
                                                स्वामित्व - किराए पर</label>
                                            <asp:DropDownList runat="server" ID="ddlOwnedrented" CssClass="form-control" OnSelectedIndexChanged="ddlOwnedrented_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                                <asp:ListItem Value="Owned">Owned</asp:ListItem>
                                                <asp:ListItem Value="Rented">Rented</asp:ListItem>
                                            </asp:DropDownList>
                                            <small><span id="valddlOwnedrented" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label><span style="color: red">*</span>Occupancy From
                                                <br />
                                                अधिभोग फ्रॉम</label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox runat="server" autocomplete="off" placeholder="MM/DD/YYYY" AutoPostBack="true" class="form-control pull-right" data-provide="datepicker" data-date-end-date="-1d" ID="txtOccupancyForm" ClientIDMode="Static" onpaste="return false" onkeypress="return validateNum(event)" OnTextChanged="txtOccupancyForm_TextChanged"></asp:TextBox>
                                            </div>
                                            <small><span id="valtxtOccupancyForm" style="color: red;"></span></small>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                           <label><span style="color: red">*</span>Occupancy To
                                                <br />
                                                अधिभोग तक</label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtOccupancyTo" placeholder="MM/DD/YYYY" AutoPostBack="true" runat="server" autocomplete="off" class="form-control pull-right" data-provide="datepicker" data-date-start-date="0d" ClientIDMode="Static" onpaste="return false" onkeypress="return validateNum(event)" OnTextChanged="txtOccupancyTo_TextChanged"></asp:TextBox>

                                            </div>
                                            <small><span id="valtxtOccupancyTo" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label><span style="color: red">*</span>Agreement Periods
                                                <br />
                                                समझौता अवधि</label>
                                            <asp:TextBox runat="server" placeholder="Agreement Periods" Enabled="false" autocomplete="off" ID="txtPeriod" MaxLength="3" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                            <small><span id="valtxtPeriod" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label><span style="color: red">*</span>Attached Agreement
                                                <br />
                                                संलग्न समझौता</label>
                                            <asp:FileUpload ID="FUAgreement" runat="server" ClientIDMode="Static" CssClass="form-control" onchange="UploadControlValidationForLenthAndFileFormat(100, 'DOCX*DOC*PDF', this),ValidateFileSize(this)" />
                                            <asp:HyperLink ID="hypAttach" Text="Download Agreement" ToolTip="Click to download attached Agreement file!" runat="server"></asp:HyperLink>
                                            <small><span id="valFUAgreement" style="color: red;"></span></small>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label><span style="color: red">*</span>Monthly Rent
                                                <br />
                                                मासिक किराया (सभी खर्चों के साथ जैसे - बिजली / पानी, अन्य)</label>
                                            <asp:TextBox runat="server" placeholder="Enter Monthly Rent" autocomplete="off" ID="txtRent" MaxLength="10" CssClass="form-control" ClientIDMode="Static" onkeypress="return validateDec(this,event);"></asp:TextBox>
                                            <small><span id="valtxtRent" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                            <div class="box-footer">
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Button ID="btnYes" runat="server" Text="Update" CssClass="btn btn-success form-control" OnClientClick="return validateform();" />
                                    </div>
                                    &nbsp;
                                    <div class="col-md-2">
                                        <asp:Button runat="server" CssClass="btn btn-default form-control" Text="Cancel" ID="BtnClear" OnClientClick="return validate()" OnClick="BtnClear_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->
                </div>
            </div>
            <!-- /.row -->
            <%--Confirmation Modal Start --%>
            <div id="myModelNew" class="modal fade" role="dialog">
                <div class="modal-dialog modal-sm" role="document">
                    <div class="modal-content">
                        <div class="modal-header" style="padding: 10px 15px 5px;">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">Confirmation Box</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <p>Are you sure you want to Update this record?</p>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer" style="padding: 0px; text-align: left; padding-bottom: 10px;">
                            <div class="col-md-12">
                                <asp:Button runat="server" CssClass="btn action-button" Text="Yes" ID="BtnSubmit" OnClick="BtnSubmit_Click" Style="margin-top: 20px; width: 50px;" />&nbsp;&nbsp;<asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn action-button" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />
                            </div>
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
    <script>

        $("#txtOccupancyForm").datepicker({
            format: 'mm/dd/yyyy',
            endDate: '-1d',
            autoclose: true
        });

        $("#txtOccupancyTo").datepicker({
            format: 'mm/dd/yyyy',
            startDate: '0d',
            autoclose: true
        });

        function validateform() {
            var msg = "";

            $("#valtxtWarehouseName").html("");
            $("#valtxtArea").html("");
            $("#valtxtCapacity").html("");
            $("#valtxt_Address").html("");
            $("#valtxtInchageName").html("");
            $("#valtxtMobileNo").html("");
            $("#valtxtemail").html("");
            $("#valddloffice").html("");
            $("#valddlOwnedrented").html("");
            $("#valtxtOccupancyForm").html("");
            $("#valtxtOccupancyTo").html("");
            //$("#valtxtPeriod").html("");
            $("#valFUAgreement").html("");
            $("#valtxtRent").html("");

            if (document.getElementById('<%=txtWarehouseName.ClientID%>').value.trim() == "") {
                msg += "Enter Warehouse Name \n";
                $("#valtxtWarehouseName").html("Enter Warehouse Name");
            }

            if (document.getElementById('<%=txtArea.ClientID%>').value.trim() == "") {
                msg += "Enter Area \n";
                $("#valtxtArea").html("Enter Area ");
            }

            if (document.getElementById('<%=txtCapacity.ClientID%>').value.trim() == "") {
                msg += "Enter Warehouse Capacity \n";
                $("#valtxtCapacity").html("Enter Warehouse Capacity ");
            }
            if (document.getElementById('<%=txt_Address.ClientID%>').value.trim() == "") {
                msg += "Enter Address \n";
                $("#valtxt_Address").html("Enter Address");
            }
            if (document.getElementById('<%=txtInchageName.ClientID%>').value.trim() == "") {
                msg += "Enter Incharge Name \n";
                $("#valtxtInchageName").html("Enter Incharge Name");
            }
            if (document.getElementById('<%=txtMobileNo.ClientID%>').value.trim() == "") {
                msg += "Enter Incharge Mobile Number\n";
                $("#valtxtMobileNo").html("Enter Incharge Mobile Number");
            }
            <%-- if (document.getElementById('<%=txtemail.ClientID%>').value.trim() == "") {
                msg += "Enter Incharge Email id \n";
                $("#valtxtemail").html("Enter Incharge Email id");
            }--%>
            var email = document.getElementById('txtemail');
            var filter = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            if (email.value != "") {
                if (!filter.test(email.value)) {
                    msg += "Invalid Email Address \n";
                    $("#valtxtemail").html("Invalid Email Address");
                    //alert('Please provide a valid email address');
                    //email.focus;
                    //return false;
                }
            }
            if (document.getElementById('<%=ddloffice.ClientID%>').value.trim() == "0") {
                msg += "Select Office \n";
                $("#valddloffice").html("Select Office");
            }
            if (document.getElementById('<%=ddlOwnedrented.ClientID%>').value.trim() == "Select") {
                msg += "Select Warehouse type (Owned / Rented) \n";
                $("#valddlOwnedrented").html("Select Warehouse type (Owned / Rented)");
            }
            
            if (document.getElementById('<%=ddlOwnedrented.ClientID%>').value.trim() == "Rented") {
                if (document.getElementById('<%=txtOccupancyForm.ClientID %>').value.trim() == "") {
                    msg += "Enter Occupancy from date \n";
                    $("#valtxtOccupancyForm").html("Enter Occupancy from date");
                }
                if (document.getElementById('<%=txtOccupancyTo.ClientID %>').value.trim() == "") {
                    msg += "Enter Occupancy To date \n";
                    $("#valtxtOccupancyTo").html("Enter Occupancy To date");
                }
                if (document.getElementById('<%=txtPeriod.ClientID %>').value.trim() == "") {
                    msg += "Enter Agreement Periods (In Months)\n";
                    $("#valtxtPeriod").html("Enter Agreement Periods (In Months)");
                }
                <%--if (document.getElementById('<%=FUAgreement.ClientID %>').value.trim() == "") {
                    msg += "Attached Agreement\n";
                    $("#valFUAgreement").html("Attached Agreement");
                }--%>

                <%--if (document.getElementById('<%=hypAttach.NavigateUrl %>').value == "") {
                    msg += "Attached Agreement\n";
                    $("#valFUAgreement").html("Attached Agreement 1");
                }--%>

                if (document.getElementById('<%=txtRent.ClientID %>').value.trim() == "") {
                    msg += "Enter Monthly Rent\n";
                    $("#valtxtRent").html("Enter Monthly Rent");
                }
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=btnYes.ClientID%>').value.trim() == "Update") {
                    $('#myModelNew').modal('show');
                    return false;
                    //if (confirm("Do you really want to Save Details ?")) {
                    //    return true;
                    //}
                    //else {
                    //    return false;
                    //}
                }
                if (document.getElementById('<%=btnYes.ClientID%>').value.trim() == "Edit") {
                    //if (confirm("Do you really want to Edit Details ?")) {
                    //    return true;
                    //}
                    //else {
                    //    return false;
                    //}
                }
            }
        }
    </script>
</asp:Content>

