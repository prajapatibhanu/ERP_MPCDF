<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="LedgerMasterForSale.aspx.cs" Inherits="mis_Finance_LedgerMasterForSale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .material-switch > input[type="checkbox"] {
            display: none;
        }

        .material-switch > label {
            cursor: pointer;
            height: 0px;
            position: relative;
            width: 40px;
        }

            .material-switch > label::before {
                background: rgb(0, 0, 0);
                box-shadow: inset 0px 0px 10px rgba(0, 0, 0, 0.5);
                border-radius: 8px;
                content: '';
                height: 16px;
                margin-top: -8px;
                position: absolute;
                opacity: 0.3;
                transition: all 0.4s ease-in-out;
                width: 40px;
            }

            .material-switch > label::after {
                background: rgb(255, 255, 255);
                border-radius: 16px;
                box-shadow: 0px 0px 5px rgba(0, 0, 0, 0.3);
                content: '';
                height: 24px;
                left: -4px;
                margin-top: -8px;
                position: absolute;
                top: -4px;
                transition: all 0.3s ease-in-out;
                width: 24px;
            }

        .material-switch > input[type="checkbox"]:checked + label::before {
            background: inherit;
            opacity: 0.5;
        }

        .material-switch > input[type="checkbox"]:checked + label::after {
            background: inherit;
            left: 20px;
        }

        .customCSS td {
            padding: 0px !important;
        }

        table {
            white-space: nowrap;
        }

        .CapitalClass {
            text-transform: uppercase;
        }

        .capitalize {
            text-transform: capitalize;
        }

        input[type="text"]:focus {
            background-color: #3c8dbc7a;
            outline: 2px solid #00a1ff;
        }

        input[type="file"]:focus, input[type="radio"]:focus, input[type="checkbox"]:focus {
            outline: 2px solid #00a1ff;
        }

        select2-container:focus-within {
            background: lightyellow;
        }

        .select2-container *:focus {
            outline: 2px solid #00a1ff;
        }

        ul.ui-autocomplete.ui-menu.ui-widget.ui-widget-content.ui-corner-all {
            height: 197px !important;
            overflow-y: scroll !important;
            width: 520px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Ledger Creation</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3"></div>
                        <div class="col-md-3"></div>
                        <div class="col-md-3"></div>
                       
                        <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Date<span style="color: red;"> *</span></label>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox runat="server" ClientIDMode="Static" data-date-end-date="0d"  CssClass="form-control DateAdd" ID="txtVoucherTx_Date" placeholder="DD/MM/YYYY" Autocomplete="off" OnTextChanged="txtVoucherTx_Date_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <small><span id="valtxtVoucherTx_Date" style="color: red;"></span></small>
                                                </div>
                                            </div>
                    </div>
                    <asp:Panel ID="pnbody" runat="server">
                        <asp:Panel ID="pnlOB" runat="server">
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend>Ledger Detail</legend>
                                        <div class="row">

                                            <div class="col-md-5">
                                                <div class="form-group">
                                                    <label>Ledger Name<span style="color: red;"> *</span></label>
                                                    <asp:TextBox runat="server" CssClass="form-control capitalize ui-autocomplete-12" placeholder="Enter Ledger Name" onblur="fillAcntholdername();" ID="txtLedgerName" MaxLength="255" ClientIDMode="Static"></asp:TextBox>
                                                    <asp:HiddenField ID="hfLedgerName" runat="server" ClientIDMode="Static" />
                                                    <small><span id="valtxtLedgerName" style="color: red;"></span></small>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Alias</label>
                                                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Enter Alias" ID="txtLedgerAlias" ClientIDMode="Static" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Group Name<span style="color: red;"> *</span></label>
                                                    <asp:DropDownList runat="server" CssClass="form-control select2" ID="ddlHeadName" ClientIDMode="Static">
                                                        <asp:ListItem>Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <small><span id="valddlHeadName" style="color: red;"></span></small>
                                                </div>
                                            </div>
                                            
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                            

                        </asp:Panel>
                        
                        <div class="row">
                            <div class="col-md-12">
                                <fieldset>
                                    <legend>Opening Balance</legend>
                                    <div class="row">

                                        <div class="col-md-3" id="inventoryvalue" runat="server">
                                            <div class="form-group">
                                                <label>Inventory Values are affected<span style="color: red;"> *</span></label>
                                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlInventoryAffected">
                                                    <%--<asp:ListItem Value="NA">Select</asp:ListItem>--%>
                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="No">No</asp:ListItem>
                                                </asp:DropDownList>
                                                <small><span id="valddlInventoryAffected" style="color: red;"></span></small>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Maintain Balances BillByBill<span style="color: red;">*</span></label>
                                                <asp:DropDownList ID="ddlBalBillByBill" runat="server" CssClass="form-control">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="No">No</asp:ListItem>
                                                </asp:DropDownList>
                                                <small><span id="valddlBalBillByBill" style="color: red;"></span></small>
                                            </div>
                                        </div>
                                        <div class="col-md-3 hidden" runat="server">
                                            <div class="form-group">
                                                <label>Financial Year <span style="color: red;">*</span></label>
                                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlFinancialYear">
                                                </asp:DropDownList>
                                                <small><span id="valddlFinancialYear" style="color: red;"></span></small>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Dr./Cr.</label>
                                                <asp:DropDownList runat="server" ID="ddlDrCr" CssClass="form-control">
                                                    <asp:ListItem Value="Credit">Credit</asp:ListItem>
                                                    <asp:ListItem Value="Debit">Debit</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Opening Balance <span style="color: red;">*</span></label>
                                                <asp:TextBox runat="server" CssClass="form-control" MaxLength="15" onkeypress="return validateDec(this,event);" placeholder="Enter Opening Balance" ID="txtOpeningBalance" Text="0"></asp:TextBox>
                                                <small><span id="valtxtOpeningBalance" style="color: red;"></span></small>
                                            </div>
                                        </div>


                                    </div>
                                </fieldset>
                            </div>
                        </div>

                        

                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-block btn-success" Text="Save & Next" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                                </div>
                            </div>
                            <div class="col-md-2" id="clear" runat="server">
                                <div class="form-group">
                                    <a href="LedgerMasterB.aspx" class="btn btn-block btn-default">Clear</a>
                                </div>
                            </div>
                        </div>

                    </asp:Panel>
                </div>
            </div>
            <asp:HiddenField ID="hfvalue" runat="server" />
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        // \d{2}[A-Z]{5}\d{4}[A-Z]{1}[A-Z\d]{1}[Z]{1}[A-Z\d]{1}
        //===========GST NUMBER VALIDATION START ====================
        $('.GSTNo').blur(function () {

            //var reg = /^(d{2}[A-Z]{5}\d{4}[A-Z]{1}[A-Z\d]{1}[Z]{1}[A-Z\d]{1})$/;
            //var reg = /^(\d{2})([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})(\d{1})([a-zA-Z]{1})(\d{1})$/;
            var reg = /^(\d{2})([A-Z]{5})(\d{4})([A-Z]{1})([0-9A-Z]{1})([Z]{1})([0-9A-Z]{1})$/;
            if (document.getElementById('txtGSTNo').value != "") {
                if (reg.test(document.getElementById('txtGSTNo').value) == false) {
                    alert("Invalid GST Number.");
                    document.getElementById('txtGSTNo').value = "";
                }
            }

        });

        //===========GST NUMBER VALIDATION END====================
        $('.PanCard').blur(function () {
            debugger;
            var Obj = $('.PanCard').val();
            if (Obj == null) Obj = window.event.srcElement;
            if (Obj != "") {
                ObjVal = Obj;
                //var panPat = /^([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})$/;
                var panPat = /^([A-Z]{5})(\d{4})([A-Z]{1})$/;
                var code = /([C,P,H,F,A,T,B,L,J,G])/;
                var code_chk = ObjVal.substring(3, 4);
                if (ObjVal.search(panPat) == -1) {
                    alert("Invalid Pan No");
                    //message_error("Error", "Invalid Pan Card.");
                    //Obj.focus();
                    $('.PanCard').val('');
                    return false;
                }
                if (code.test(code_chk) == false) {
                    alert("Invaild PAN Card No.");
                    //message_error("Error", "Invalid Pan Card.");
                    $('.PanCard').val('');
                    return false;
                }
            }
        });
        $('.PinCode').blur(function () {
            var Obj = $('.PinCode').val();
            if (Obj == null) Obj = window.event.srcElement;
            if (Obj != "") {
                ObjVal = Obj;
                var panPat = /^([1-9]{1})([0-9]{5})$/;
                var code_chk = ObjVal.substring(3, 4);
                if (ObjVal.search(panPat) == -1) {
                    alert("Invalid PinCode");
                    //message_error("Error", "Invalid Pan Card.");
                    //Obj.focus();
                    $('.PinCode').val('');
                    return false;
                }
                if (code.test(code_chk) == false) {
                    alert("Invaild PinCode.");
                    //message_error("Error", "Invalid Pan Card.");
                    $('.PinCode').val('');
                    return false;
                }
            }
        });
        $('.IFSC').blur(function () {
            debugger;
            var Obj = $('.IFSC').val();
            if (Obj == null) Obj = window.event.srcElement;
            if (Obj != "") {
                ObjVal = Obj;
                //var IFSC = /^[A-Za-z]{4}[0]{1}[0-9A-Za-z]{6}$/;
                var IFSC = /^[A-Z]{4}[0]{1}[0-9A-Z]{6}$/;
                var code_chk = ObjVal.substring(3, 4);
                if (ObjVal.search(IFSC) == -1) {
                    alert("Invalid IFSC Code");
                    //message_error("Error", "Invalid IFSC Code.");
                    //Obj.focus();
                    $('.IFSC').val('');
                    return false;
                }
                if (code.test(code_chk) == false) {
                    alert("Invaild IFSC Code.");
                    //message_error("Error", "Invalid IFSC Code.");
                    $('.IFSC').val('');
                    return false;
                }

                if ($('.IFSC').val.length != 11) {
                    alert("Invaild IFSC Code.");
                    return false;
                }
            }
        });
        $('.MobileNo').blur(function () {
            debugger;
            var Obj = $('.MobileNo').val();
            if (Obj == null) Obj = window.event.srcElement;
            if (Obj != "") {
                ObjVal = Obj;
                var MobileNo = /^[6-9]{1}[0-9]{9}$/;
                var code_chk = ObjVal.substring(3, 4);
                if (ObjVal.search(MobileNo) == -1) {
                    alert("Invalid Mobile No.");
                    //message_error("Error", "Invalid IFSC Code.");
                    //Obj.focus();
                    $('.MobileNo').val('');
                    return false;
                }
                if (code.test(code_chk) == false) {
                    alert("Invaild Mobile No.");
                    //message_error("Error", "Invalid IFSC Code.");
                    $('.MobileNo').val('');
                    return false;
                }
            }
        });                    
            function ShowModal() {
                $('#myModal').modal('show');
                return true;
            }
            function ShowBillDetailModal() {
                $('#myModal').modal('show');
                $("#ddlBillByBillTx_Ref").hide();
            }
            function ShowRefDetailModal() {
                $('#AgstRefModal').modal('show');
            }


            function validateform() {
                debugger;
                var msg = "";
                $("#valddlHeadName").html("");
                $("#valtxtLedgerName").html("");
                $("#valddlFinancialYear").html("");
                $("#valddltypeofsupply").html("");
                $("#valddlTaxability").html("");
                $("#valddlHSNCode").html("");
                $("#valddlBalBillByBill").html("");
                $("#valddlRegistrationType").html("");
                $("#valddlState").html("");
                if (document.getElementById('<%=txtVoucherTx_Date.ClientID%>').value.trim() == "") {
                    msg = msg + "Enter Date . \n";
                    $("#valtxtVoucherTx_Date").html("Enter Date ");
                }
                if (document.getElementById('<%=txtLedgerName.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Ledger Name. \n";
                $("#valtxtLedgerName").html("Enter Ledger Name");
            }
            if (document.getElementById('<%=ddlHeadName.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Group Name. \n";
                $("#valddlHeadName").html("Select Group Name");
            }       
            if (document.getElementById('<%=ddlBalBillByBill.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Maintain Balances BillByBill.\n";
                $("#valddlBalBillByBill").html("Select Maintain Balances BillByBill");
            }

           
            if (document.getElementById('<%=ddlFinancialYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Financial Year.\n";
                $("#valddlFinancialYear").html("Select Financial Year");
            }
            if (document.getElementById('<%=txtOpeningBalance.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Opening Balance.\n";
                $("#valtxtOpeningBalance").html("Enter Opening Balance");
            }            
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save & Next") {
                    document.querySelector('.popup-wrapper').style.display = 'block';
                    return true;
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {

                    document.querySelector('.popup-wrapper').style.display = 'block';
                    return true;
                }
            }
        }
        $('.Email').blur(function () {
            debugger;
            //var filter = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-])+\.([A-Za-z]{2,4})$/;
            var filter = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-])+\.([A-Za-z]{2,4})$/;
            if ($('.Email').val() != "") {
                if (!filter.test($('.Email').val())) {
                    //alert('Please provide a valid email address');
                    alert("Please provide a valid email address");
                    //$('.Email').focus();
                    $('.Email').val("");
                    return false;
                }
            }
        });
        $('input').keydown(function (e) {
            var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
            if (key == 13) {
                e.preventDefault();
                var inputs = $(this).closest('form').find(':input:visible');
                inputs.eq(inputs.index(this) + 1).focus();
            }
        });
        function allowNegativeNumber(e) {
            var charCode = (e.which) ? e.which : event.keyCode
            if (charCode > 31 && (charCode < 45 || charCode > 57)) {
                return false;
            }
            return true;

        }       
    </script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script>
        $(document).ready(function () {

            debugger;
            $("#<%=txtLedgerName.ClientID %>").autocomplete({

                    source: function (request, response) {
                        $.ajax({

                            url: '<%=ResolveUrl("LedgerMasterB.aspx/SearchCustomers") %>',
                        data: "{ 'Ledger_Name': '" + $('#txtLedgerName').val() + "'}",
                        //  var param = { ItemName: $('#txtItem').val() };
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {

                            response($.map(data.d, function (item) {
                                return {
                                    label: item
                                    //val: item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                    select: function (e, i) {
                        $("#<%=hfLedgerName.ClientID %>").val(i.item.val);
                },
                minLength: 1

                });

            });
    </script>

    <script src="../js/ValidationJs.js"></script>
    
</asp:Content>




