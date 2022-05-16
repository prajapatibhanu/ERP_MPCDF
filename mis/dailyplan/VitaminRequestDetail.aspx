<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="VitaminRequestDetail.aspx.cs" Inherits="mis_dailyplan_VitaminRequestDetail" %>

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

                        <div class="col-md-2">
                                <div class="form-group">
                                    <label>From Date</label><span class="text-danger">*</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" Display="Dynamic" ControlToValidate="txtFromDate" Text="<i class='fa fa-exclamation-circle' title='Enter From Date!'></i>" ErrorMessage="Enter From Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">
                                                <i class="far fa-calendar-alt"></i>
                                            </span>
                                        </div>
                                        <asp:TextBox ID="txtFromDate" autocomplete="off" placeholder="Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>To Date</label><span class="text-danger">*</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvToDate" runat="server" Display="Dynamic" ControlToValidate="txtToDate" Text="<i class='fa fa-exclamation-circle' title='Enter To Date!'></i>" ErrorMessage="Enter To Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">
                                                <i class="far fa-calendar-alt"></i>
                                            </span>
                                        </div>
                                        <asp:TextBox ID="txtToDate" autocomplete="off" placeholder="Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Button ID="btnSearch" CssClass="btn btn-success" Style="margin-top: 21px;" runat="server" Text="Search" OnClick="btnSearch_Click" />

                                </div>
                            </div>

                        <hr />
                      
                        <div class="row">
                            <div class="col-md-12 pull-right noprint">
                            <div class="form-group">
                                <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn btn-default" Text="Excel" OnClick="btnExport_Click"/>
                                <asp:Button ID="btnPrint" runat="server"  Visible="false" CssClass="btn btn-default" Text="Print" OnClientClick="window.print();"/>
                                  <asp:Button ID="btnShowntoplant" runat="server" Visible="false" CssClass="btn btn-primary" Text="Shown To Plant"  OnClick="btnShowntoplant_Click" OnClientClick="return confirm('Do you really want to update this record?')"/>
                            </div>      
                            </div>
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView1" EmptyDataText="No Record Found" runat="server" CssClass="table table-bordered GridView1" AutoGenerateColumns="false" OnRowCommand="GridView1_RowCommand">
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
											<asp:TemplateField HeaderText="Status" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
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
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnDispatch" CssClass="label label-default" Enabled='<%# Eval("Status").ToString()=="Pending"?true:false %>' runat="server" CommandName="Dispatch" Text="Dispatch"></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkSave" CssClass="label label-default" Visible="false" runat="server"  ValidationGroup="Save" OnClientClick="return confirm('do you really want to Dispatch?')"  Text="Save" CommandName="Save" CommandArgument='<%# Eval("VitaminRequest_ID") %>'></asp:LinkButton>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="2%">
                                            <HeaderTemplate>
                                               <%-- <input id="Checkbox2" type="checkbox" onclick="CheckAll(this)" runat="server" />--%>
                                                 <asp:CheckBox ID="check_All" runat="server" ClientIDMode="static" />
                                                All
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkselect" Checked='<%# Eval("ShownToPlant").ToString()=="1"?true:false %>' runat="server" ToolTip='<%# Eval("VitaminRequest_ID") %>'/>
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

