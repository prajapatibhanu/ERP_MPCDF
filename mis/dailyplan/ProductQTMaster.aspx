<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ProductQTMaster.aspx.cs" Inherits="mis_dailyplan_ProductQTMaster" %>

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
            margin-top: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">QC Testing Parameter Master</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Product / Item Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control select2" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Test Parameter Name<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtParameter" runat="server" placeholder="Enter Parameter..." class="form-control" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Calculation Method<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlCalculationMethod" runat="server" CssClass="form-control select2" ClientIDMode="Static" OnSelectedIndexChanged="ddlCalculationMethod_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Selected="true" Value="Value">Value</asp:ListItem>
                                    <asp:ListItem Value="Option">Option</asp:ListItem>
                                    <asp:ListItem Value="Use By Date">Use By Date</asp:ListItem>
                                    <asp:ListItem Value="Manufacturing Date">Manufacturing Date</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Min. Value<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtMinValue" runat="server" placeholder="Enter Min. Value..." class="form-control"  onkeypress="return validate3Dec(this,event);" MaxLength="12"></asp:TextBox>
                                <asp:DropDownList ID="ddlMinValue" runat="server" CssClass="form-control" ClientIDMode="Static">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem Value="Curd">Curd</asp:ListItem>
                                    <asp:ListItem Value="Sour">Sour</asp:ListItem>
                                    <asp:ListItem Value="Slightly Off Taste">Slightly Off Taste</asp:ListItem>
                                    <asp:ListItem Value="Off Taste">Off Taste</asp:ListItem>
                                    <asp:ListItem Value="Satisfactory">Satisfactory</asp:ListItem>
                                    <asp:ListItem Value="Good">Good</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Max. Value<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtMaxValue" runat="server" placeholder="Enter Max. Value..." class="form-control"  onkeypress="return validate3Dec(this,event);" MaxLength="12"></asp:TextBox>
                                <asp:DropDownList ID="ddlMaxValue" runat="server" CssClass="form-control" ClientIDMode="Static">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem Value="Curd">Curd</asp:ListItem>
                                    <asp:ListItem Value="Sour">Sour</asp:ListItem>
                                    <asp:ListItem Value="Slightly Off Taste">Slightly Off Taste</asp:ListItem>
                                    <asp:ListItem Value="Off Taste">Off Taste</asp:ListItem>
                                    <asp:ListItem Value="Satisfactory">Satisfactory</asp:ListItem>
                                    <asp:ListItem Value="Good">Good</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2" runat="server" id="DivUnit">
                            <div class="form-group">
                                <label>Unit<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control" ClientIDMode="Static">
                                    <asp:ListItem Value="NA">NA</asp:ListItem>
                                    <asp:ListItem Value="%">%</asp:ListItem>
                                    <asp:ListItem Value="No.">No.</asp:ListItem>
                                    <asp:ListItem Value="Ltr.">Ltr.</asp:ListItem>
                                    <asp:ListItem Value="Kg.">Kg.</asp:ListItem>
                                    <asp:ListItem Value="Per Btl">Per Btl.</asp:ListItem>
                                    <asp:ListItem Value="Ml.">Ml.</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-block btn-success btnmargin" Text="Submit" ClientIDMode="Static" OnClick="btnSubmit_Click" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a href="ProductQTMaster.aspx" class="btn btn-block btn-default btnmargin">Cancel</a>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table table-responsive">
                                <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" class="table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" AllowPaging="False" DataKeyNames="QCParameterID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("QCParameterID").ToString()%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="QCParameterName" HeaderText="Parameter Name" />
                                        <asp:BoundField DataField="CalculationMethod" HeaderText="Calculation Method" />
                                        <asp:BoundField DataField="MinValue" HeaderText="Min. Value" />
                                        <asp:BoundField DataField="MaxValue" HeaderText="Max. Value" />
                                        <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Select" runat="server" CssClass="label label-default" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="30" HeaderText="Active">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" OnCheckedChanged="chkSelect_CheckedChanged" runat="server" ToolTip='<%# Eval("QCParameterID").ToString()%>' Checked='<%# Eval("IsActive").ToString()=="1" ? true : false %>' AutoPostBack="true" />
                                            </ItemTemplate>
                                            <ItemStyle Width="30px"></ItemStyle>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
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
        // onkeypress="return validate3Dec(this,event)"
        function validate3Dec(el, evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
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
            if (caratPos > dotPos && dotPos > -1 && (number[1].length > 2)) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
