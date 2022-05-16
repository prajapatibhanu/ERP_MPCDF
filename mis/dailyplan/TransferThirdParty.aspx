<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="TransferThirdParty.aspx.cs" Inherits="mis_dailyplan_TransferThirdParty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        li.header {
            font-size: 14px !important;
            color: #F44336 !important;
        }

        span#ctl00_spnUsername {
            text-transform: uppercase;
            font-weight: 600;
            font-size: 16px;
        }

        li.dropdown.tasks-menu.classhide a {
            padding: 4px 10px 0px 0px;
        }

        a.btn.btn-default.buttons-excel.buttons-html5 {
            background: #ff5722c2;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-pdf.buttons-html5 {
            background: #009688c9;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-print {
            background: #e91e639e;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
        }

        .navbar {
            background: #ebf4ff !important;
            box-shadow: 0px 0px 8px #0058a6;
            color: #0058a6;
        }

        .skin-green-light .main-header .logo {
            background: #fff !important;
        }


        a.btn.btn-default.buttons-print:hover, a.btn.btn-default.buttons-pdf.buttons-html5:hover, a.btn.btn-default.buttons-excel.buttons-html5:hover {
            box-shadow: 1px 1px 1px #808080;
        }

        a.btn.btn-default.buttons-print:active, a.btn.btn-default.buttons-pdf.buttons-html5:active, a.btn.btn-default.buttons-excel.buttons-html5:active {
            box-shadow: 1px 1px 1px #808080;
        }

        .btn-success {
            background-color: #1d7ce0;
            border-color: #1d7ce0;
        }

            .btn-success:hover, .btn-success:active, .btn-success.hover, .btn-success:focus, .btn-success.focus, .btn-success:active:focus {
                background-color: #1d7ce0;
                border-color: #1d7ce0;
            }

            .btn-success:hover {
                color: #fff;
                background-color: #1d7ce0;
                border-color: #1d7ce0;
            }

        fieldset {
            border: 1px solid #ff7836;
            padding: 15px;
            margin-bottom: 15px;
        }

        legend {
            width: initial;
            padding: 4px 15px;
            margin: 0;
            font-size: 12px;
            font-weight: bold;
            color: #00427b;
            text-transform: uppercase;
            border: 1px solid #ff7836;
        }

        table .select2 {
            width: 100px !important;
        }

        .btnmargin {
            margin-top: 18px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Transfer Of Item/Product</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>तारीख / Date<span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtDispatchDate" runat="server" placeholder="Select Date..." class="form-control DateAdd" onpaste="return false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>From Which section / अनुभाग<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlProductSection" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Item/Ingredients<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-control select2">
                                    <asp:ListItem Text="Select"></asp:ListItem>
                                    <asp:ListItem Text="Whole Milk (Liter)"></asp:ListItem>
                                    <asp:ListItem Text="Milk Powder  (KG)"></asp:ListItem>
                                </asp:DropDownList>
                                <small><span id="valddlItem" class="text-danger"></span></small>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Quantity <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"></asp:TextBox>
                                <small><span id="valtxtQuantity" class="text-danger"></span></small>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <label>FAT(Kg)<span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                                <small><span id="valtxtQuantity" class="text-danger"></span></small>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <label>SNF(Kg)<span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                                <small><span id="valtxtQuantity" class="text-danger"></span></small>
                            </div>
                        </div>

                    </div>
                    <div class="row">

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Where To Transfer / कहाँ भेजना है<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlProductSectionTo" runat="server" CssClass="form-control select2">
                                    <asp:ListItem Text="Select"></asp:ListItem>
                                    <asp:ListItem Text="Ujjain Dugdh Sangh"></asp:ListItem>
                                    <asp:ListItem Text="Jabalpur Dugdh Sangh"></asp:ListItem>
                                    <asp:ListItem Text="Gwalior Dugdh Sangh"></asp:ListItem>
                                    <asp:ListItem Text="indore Dugdh Sangh"></asp:ListItem>
                                    <asp:ListItem Text="Bundelkhand Dugdh Sangh"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Transfer Type<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control select2">
                                    <asp:ListItem Text="Select"></asp:ListItem>
                                    <asp:ListItem Text="Conversion Of Product"></asp:ListItem>
                                    <asp:ListItem Text="Sale"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Refrence No.<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtrefrence" runat="server" CssClass="form-control"></asp:TextBox>
                                <small><span id="valtxtQuantity" class="text-danger"></span></small>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Transfer Remark / टिप्पणी<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtRemark" class="form-control" placeholder="Enter Transfer Remark..." runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <h5 class="text-center" runat="server" visible="false" id="headingmilk"><b><u>Milk Section:</u></b></h5>
                            <div class="table table-responsive">
                            </div>
                        </div>
                    </div>
                    <div class="row" runat="server">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-block btn-success" Text="Submit" />
                            </div>
                        </div>
                    </div>

                    <div class="row">

                        <br />
                        <br />
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <table class="table">
                                    <tr>
                                        <th>SNo.</th>
                                        <th>Date</th>
                                        <th>From Location</th>
                                        <th>To Location</th>
                                        <th>Transfer Type</th>
                                        <th>Item/Material</th>
                                        <th>Quantity</th>
                                        <th>Fat</th>
                                        <th>Snf</th>
                                        <th>Transfer Remark</th>
                                        <th>Action</th>
                                    </tr>
                                    <tr>
                                        <td colspan="11">No Record Found..!!</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>

        $(document).ready(function () {
            $('.loader').fadeOut();
        });
        $('#checkAll').click(function () {
            var inputList = document.querySelectorAll('#GridView1 tbody input[type="checkbox"]:not(:disabled)');
            for (var i = 0; i < inputList.length; i++) {
                if (document.getElementById('checkAll').checked) {
                    inputList[i].checked = true;
                }
                else {
                    inputList[i].checked = false;
                }
            }
        });

        function validateform() {
            var msg = "";
            <%--if (document.getElementById('<%=txtDispatchDate.ClientID%>').value.trim() == "") {
                msg = "Select Dispatch Date."
            }--%>
            <%--if (document.getElementById('<%=ddlShift.ClientID%>').selectedIndex == 0) {
                msg += "Select Shift. \n"
            }--%>
            <%--if (document.getElementById('<%=ddlProductSection.ClientID%>').selectedIndex == 0) {
                msg += "Select Product Section. \n"
            }--%>
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (confirm("Do you really want to Submit Details ?")) {
                    return true;
                }
                else {
                    return false;
                }
            }

        }
        function validateformsearch() {
            var msg = "";
           <%-- if (document.getElementById('<%=txtDispatchDate.ClientID%>').value.trim() == "") {
                msg = "Select Dispatch Date."
            }
            if (document.getElementById('<%=ddlShift.ClientID%>').selectedIndex == 0) {
                 msg += "Select Shift. \n"
             }
             if (document.getElementById('<%=ddlProductSection.ClientID%>').selectedIndex == 0) {
                 msg += "Select Product Section. \n"
             }--%>
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
            }

        }
    </script>
</asp:Content>



