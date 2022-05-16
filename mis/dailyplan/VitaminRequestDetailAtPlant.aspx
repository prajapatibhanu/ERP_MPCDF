<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="VitaminRequestDetailAtPlant.aspx.cs" Inherits="mis_dailyplan_VitaminRequestDetailAtPlant" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Vitamin Request Details</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                           <fieldset runat="server">
                <legend>Vitamin Request Details</legend>

                <div class="row">
                    <div class="col-md-12">

                        <div class="col-md-3 noprint">
                            <div class="form-group">
                                <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlFilterOffice" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2 noprint">
                            <div class="input-group">
                                <label>From Date<span class="text-danger">*</span></label>
                                <div class="input-group-prepend">
                                    <span class="input-group-text">
                                        <i class="far fa-calendar-alt"></i>
                                    </span>
                                </div>
                                <asp:TextBox ID="txtFromDate" autocomplete="off"  CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                            </div>
                        </div>
                         <div class="col-md-2 noprint">
                            <div class="input-group">
                                <label>To Date<span class="text-danger">*</span></label>
                                <div class="input-group-prepend">
                                    <span class="input-group-text">
                                        <i class="far fa-calendar-alt"></i>
                                    </span>
                                </div>
                                <asp:TextBox ID="txtToDate" autocomplete="off"  CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                        <div class="form-group">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success" style="margin-top:20px;" Text="Search" OnClick="btnSearch_Click"/>
                        </div>
                        </div>
                        <hr />
                      
                        <div class="row">
                            <div class="col-md-12 pull-right noprint">
                            <div class="form-group">
                                <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn btn-default" Text="Excel" OnClick="btnExport_Click"/>
                                
                            </div>      
                            </div>
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered GridView1" AutoGenerateColumns="false" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Request No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVitaminRequest_No" runat="server" Text='<%# Eval("VitaminRequest_No") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Shift">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblShift" runat="server" Text='<%# Eval("ShiftName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Production Section">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductSection_Name" runat="server" Text='<%# Eval("ProductSection_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Request Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestType" runat="server" Text='<%# Eval("RequestType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Request For">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestFor" runat="server" Text='<%# Eval("RequestFor") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Variant">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVariant" runat="server" Text='<%# Eval("Variant") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Milk Qty(In Kg)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMilkQuantity" runat="server" Text='<%# Eval("MilkQuantity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          
                                             <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequest_Status" runat="server" Text='<%# Eval("Request_Status") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remark">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequest_Remark" runat="server" Text='<%# Eval("Request_Remark") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vitamin Dispatch Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVitaminReceived_DT" runat="server" Text='<%# Eval("VitaminReceived_DT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Vitamin Dispatch Qty(In Kg)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVitaminReceivedQuantity" runat="server" Text='<%# Eval("VitaminReceivedQuantity") %>'></asp:Label>
                                                    <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="Save"
                                                    ErrorMessage="Enter Vitamin Dispatch Quantity" Text="<i class='fa fa-exclamation-circle' title='Enter Vitamin Dispatch Quantity !'></i>"
                                                    ControlToValidate="txtVitaminReceivedQuantity" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                                    <asp:TextBox ID="txtVitaminReceivedQuantity" Visible="false" Minimum="1" runat="server" Text='<%# Eval("VitaminReceivedQuantity") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          
                                            
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
    <script>
        $('#check_All').click(function () {

            var inputList = document.querySelectorAll('.GridView1 tbody input[type="checkbox"]:not(:disabled)');

            for (var i = 0; i < inputList.length; i++) {
                if (document.getElementById('check_All').checked) {
                    inputList[i].checked = true;

                    //ValidatorEnable(Amount,true)
                }
                else {
                    inputList[i].checked = false;
                }
            }
        });
    </script>
</asp:Content>

