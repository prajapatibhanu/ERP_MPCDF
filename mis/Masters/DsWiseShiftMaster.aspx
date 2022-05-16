<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DsWiseShiftMaster.aspx.cs" Inherits="mis_Masters_DsWiseShiftMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <script src="../../js/jquery.min.js"></script>
    <script src="../../js/datatables.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".datatable").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <div class="card-header">
                    <h3 class="card-title">Shift Master</h3>
                </div>
                <div class="box-body">
                    <div class="col-md-6">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Dugdh Sangh<span style="color: red;"> *</span></label>
                                <asp:RequiredFieldValidator ID="rfvDivision" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlDs" InitialValue="0" ErrorMessage="Select Dugdh Sangh" Text="<i class='fa fa-exclamation-circle' title='Select Dugdh Sangh !'></i>"></asp:RequiredFieldValidator>
                                <asp:DropDownList runat="server" ID="ddlDs" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>From Time<span style="color: red;"> *</span></label>
                                <div class="input-group bootstrap-timepicker timepicker">
                                    <asp:TextBox runat="server" CssClass="form-control input-small" ClientIDMode="Static" autocomplete="off" ID="txtShiftStartTime"></asp:TextBox>
                                    <span class="input-group-addon"><i class="fa fa-clock-o"></i></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>To Time<span style="color: red;"> *</span></label>
                                <div class="input-group bootstrap-timepicker timepicker">
                                    <asp:TextBox runat="server" CssClass="form-control input-small" ClientIDMode="Static" autocomplete="off" ID="txtShiftEndTime"></asp:TextBox>
                                    <span class="input-group-addon"><i class="fa fa-clock-o"></i></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Shift<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" InitialValue="0" ControlToValidate="ddlShift" ErrorMessage="Select Shift." Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"></asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlShift" runat="server" placeholder="Enter Shift..." class="form-control">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="1">Shift 1</asp:ListItem>
                                    <asp:ListItem Value="2">Shift 2</asp:ListItem>
                                    <asp:ListItem Value="3">Shift 3</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" CssClass="btn btn-primary mt-2" ValidationGroup="a" Style="margin-top: 20px;" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="container-fluid">
                            <div class="box box-primary">
                                <div class="card-header">
                                    <h3 class="card-title">Shift Master List</h3>
                                </div>
                                <div class="card-body">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" class="table table-bordered" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Name" HeaderText="Name" />
                                                <asp:BoundField DataField="StartTime" HeaderText="Start Time" />
                                                <asp:BoundField DataField="EndTime" HeaderText="End Time" />
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ChkStatus" runat="server" Checked='<%# Eval("Status") %>' ToolTip='<%# Eval("DSWiseProdShift_Id").ToString()%>' OnCheckedChanged="ChkStatus_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
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
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script src="../js/bootstrap-timepicker.js"></script>
    <script>
        $('#txtShiftStartTime').timepicker();
        $('#txtShiftEndTime').timepicker();
    </script>
</asp:Content>

