<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Dispatch.aspx.cs" Inherits="mis_dailyplan_Dispatch" %>

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
                    <h3 class="box-title">Dispatch / Outward</h3>
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
                                <label>Shift /  पारी<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Product section / उत्पाद अनुभाग<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlProductSection" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlProductSection_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                          <div class="col-md-2">
                            <div class="form-group">
                                <asp:button id="btnSearch" runat="server" cssclass="btn btn-block btn-success btnmargin" text="Search" clientidmode="Static" onclick="btnSearch_Click" onclientclick="return validateformsearch();" />
                            </div>
                        </div>
                    </div>
                    <%-- <div class="row">

                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Where To dispatch / कहाँ भेजना है<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlProductSectionTo" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-8">
                            <div class="form-group">
                                <label>Dispatch Remark / टिप्पणी<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtRemark" class="form-control" placeholder="Enter Dispatch Remark..." runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>--%>
                    <div class="row">
                        <div class="col-md-12">
                            <h5 class="text-center" runat="server" visible="false" id="headingmilk"><b><u>Milk Section:</u></b></h5>
                            <div class="table table-responsive">
                                <asp:GridView ID="GridView1" runat="server" class="table table-hover table-bordered table-striped pagination-ys " ShowHeaderWhenEmpty="true" ClientIDMode="Static" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="checkAll" runat="server" ClientIDMode="static" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Particular">
                                            <ItemTemplate>
                                                <asp:Label ID="lblParticular" runat="server" Text='<%# Eval("Particular") %>' ToolTip='<%# Eval("Item_id")%>'></asp:Label>
                                                <asp:Label ID="lblItemid" runat="server" CssClass="hidden" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                <asp:Label ID="lblOpeningDate" runat="server"  CssClass="hidden"  Text='<%# Eval("OpeningTranDt") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Available Quantity (Packet)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAvalableItemStock" runat="server" Text='<%# Eval("AvalableItemStock") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Quality Test">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQualityTest" runat="server" CssClass='<%# Eval("Test_ResultClass") %>' Text='<%# Eval("Test_Result") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Batch No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBatchNo" runat="server" Text='<%# Eval("BatchNo") %>'></asp:Label>
                                                <%-- <asp:DropDownList ID="ddlBatchNo" runat="server" CssClass="form-control select2">
                                                </asp:DropDownList>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Serial/Lot No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLotNo" runat="server" Text='<%# Eval("LotNo") %>'></asp:Label>
                                                <%--  <asp:DropDownList ID="ddlSerialLotNo" runat="server" CssClass="form-control select2">
                                                </asp:DropDownList>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Where To dispatch / कहाँ भेजना है">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlProductSectionTo" runat="server" CssClass="form-control select2">
                                                </asp:DropDownList>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Outward Quantity (Packet)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtOutwardQuantity" runat="server" CssClass="form-control" placeholder="Enter Quantity..."></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remark">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" placeholder="Enter Remark..."></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="row" runat="server" id="divbtn">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-block btn-success" Text="Submit" ClientIDMode="Static" OnClick="btnSubmit_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a href="Dispatch.aspx" class="btn btn-block btn-default">Cancel</a>
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
            if (document.getElementById('<%=txtDispatchDate.ClientID%>').value.trim() == "") {
                msg = "Select Dispatch Date."
            }
            if (document.getElementById('<%=ddlShift.ClientID%>').selectedIndex == 0) {
                msg += "Select Shift. \n"
            }
            if (document.getElementById('<%=ddlProductSection.ClientID%>').selectedIndex == 0) {
                msg += "Select Product Section. \n"
            }
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
            if (document.getElementById('<%=txtDispatchDate.ClientID%>').value.trim() == "") {
                msg = "Select Dispatch Date."
            }
            if (document.getElementById('<%=ddlShift.ClientID%>').selectedIndex == 0) {
                 msg += "Select Shift. \n"
             }
             if (document.getElementById('<%=ddlProductSection.ClientID%>').selectedIndex == 0) {
                 msg += "Select Product Section. \n"
             }
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



