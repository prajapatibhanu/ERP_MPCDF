<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptDateWiseBulkMilkSupply.aspx.cs" Inherits="mis_dailyplan_RptDateWiseBulkMilkSupply" %>

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
                    <h3 class="box-title">BULK MILK SALE</h3>
                    
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                 
                <div class="box-body">
                    <fieldset>
                        <legend>BULK MILK SALE</legend>
                    <div class="row">
                        <div class="col-md-5">
                            <div class="form-group">
                               <asp:RadioButtonList ID="rbtnTransferType" runat="server" RepeatDirection="Horizontal" ClientIDMode="Static" style="margin-top:20px;" OnSelectedIndexChanged="rbtnsaleto_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="1" Selected="True">&nbsp;&nbsp;Union To Union&nbsp;&nbsp;</asp:ListItem>
                                         <asp:ListItem Value="2">&nbsp;&nbsp;Union To Third Party&nbsp;&nbsp;</asp:ListItem>
                                         <asp:ListItem Value="3">&nbsp;&nbsp;Union To MDP</asp:ListItem>
                                    </asp:RadioButtonList>
                            </div>
                        </div>
                        
                    </div>
                    <div class="row">                      
                       <div class="col-md-3">
                            <div class="form-group">
                                <label>From Date<span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtFromDate" runat="server" placeholder="Select From Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>To Date</label><span style="color: red">*</span>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtToDate" runat="server" placeholder="Select To Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                       <div class="col-md-3" id="union" runat="server">
                            <div class="form-group">
                                <div class="form-group">
                                    <label>Union<span class="text-danger">*</span></label>
                                   <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvddlUnion" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlUnion" Text="<i class='fa fa-exclamation-circle' title='Select Union!'></i>" ErrorMessage="Select Union" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </span>--%>
                                    <asp:DropDownList ID="ddlUnion" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                    <small><span id="valddlUnion" class="text-danger"></span></small>
                                </div>
                            </div>
                        </div>
                            <div class="col-md-3" id="thirdparty" runat="server">
                            <div class="form-group">
                                <div class="form-group">
                                    <label>Third Party<span class="text-danger">*</span></label>
                                   <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvddlThirdparty" runat="server" Display="Dynamic" InitialValue="0"  ControlToValidate="ddlThirdparty" Text="<i class='fa fa-exclamation-circle' title='Select Third Party!'></i>" ErrorMessage="Select Third Party" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </span>--%>
                                    <asp:DropDownList ID="ddlThirdparty" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                    <small><span id="valddlThirdparty" class="text-danger"></span></small>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3" id="MDP" runat="server">
                            <div class="form-group">
                                <div class="form-group">
                                    <label>Mini Dairy Plant<span class="text-danger">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvddlMDP" runat="server" InitialValue="0" Display="Dynamic" ControlToValidate="ddlMDP" Text="<i class='fa fa-exclamation-circle' title='Select Mini Dairy Plant!'></i>" ErrorMessage="Select Mini Dairy Plant" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddlMDP" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    <small><span id="valddlMDP" class="text-danger"></span></small>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-block btn-primary btnmargin" Text="Search" ClientIDMode="Static" OnClick="btnSearch_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>
                    </fieldset>
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>BULK MILK SALE DETAIL</legend>
                            <div class="table table-responsive">
                                <asp:GridView ID="GridView1" runat="server" class="table table-hover table-bordered table-striped pagination-ys " ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" ClientIDMode="Static" AutoGenerateColumns="False">
                                    <Columns>

                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%# Eval("Remark") %>'/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Transfer Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transfer From">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransferfrom" runat="server" Text='<%# Eval("TransferFrom") %>'></asp:Label>                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Transfer To">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransferTo" runat="server" Text='<%# Eval("TransferTo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transfer Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMilkTrasferType" runat="server" Text='<%# Eval("MilkTrasferType") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity In KG">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FAT In KG">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFAT" runat="server" Text='<%# Eval("FAT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SNF In KG">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("SNF") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
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
    <script>
        function validateform() {
            debugger;
            var msg = "";          
            if (document.getElementById('<%=txtFromDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select From Date. \n";
            }
            if (document.getElementById('<%=txtToDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select To Date. \n";
            }
           if (document.getElementById('<%=rbtnTransferType.ClientID%>').selectedIndex == 0)
            {
                if(document.getElementById('<%=ddlUnion.ClientID%>').selectedIndex == 0)
                {
                    msg = msg + "Select Union \n";
                }
            }
            else
            {
                if (document.getElementById('<%=ddlThirdparty.ClientID%>').selectedIndex == 0)
                {
                    msg = msg + "Select Third party \n";
                }

            }
            
            var y = document.getElementById("txtFromDate").value; //This is a STRING, not a Date
            if (y != "") {
                var dateParts = y.split("/");   //Will split in 3 parts: day, month and year
                var yday = dateParts[0];
                var ymonth = dateParts[1];
                var yyear = dateParts[2];


                var yd = new Date(yyear, parseInt(ymonth, 10) - 1, yday);
            }
            else {
                var yd = "";
            }

            var z = document.getElementById("txtToDate").value; //This is a STRING, not a Date
            if (z != "") {
                var dateParts = z.split("/");   //Will split in 3 parts: day, month and year
                var zday = dateParts[0];
                var zmonth = dateParts[1];
                var zyear = dateParts[2];


                var zd = new Date(zyear, parseInt(zmonth, 10) - 1, zday);
            }
            else {
                var zd = "";
            }
            if (yd != "" && zd != "") {
                if (yd > zd) {
                    msg += "To Date should be greater than From Date ";
                }

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


