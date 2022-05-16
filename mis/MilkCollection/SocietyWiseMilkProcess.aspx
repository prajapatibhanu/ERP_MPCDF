<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SocietyWiseMilkProcess.aspx.cs" Inherits="mis_MilkCollection_SocietyWiseMilkProcess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #d9d9d9;">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                             
                        </button>
                        <h4 class="modal-title" id="myModalLabel">Confirmation</h4>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <p>
                            <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp;
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnYes_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />

    <div class="content-wrapper">

        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Milk Process (दुग्ध स्तिथी)</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">

                            <div class="col-md-3">
                                <label><b>Formula</b> </label>
                            </div>

                            <div class="col-md-3">
                                <label><b>FAT(%) :</b> </label>
                                <asp:Label ID="lblFAT1" runat="server" Text="3.2"></asp:Label>
                                to
                                <asp:Label ID="lblFAT2" runat="server" Text="10.9"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <label><b>CLR :</b></label>
                                <asp:Label ID="lblCLR1" runat="server" Text="16"></asp:Label>
                                to
                                <asp:Label ID="lblCLR2" runat="server" Text="35.0"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <label><b>SNF = </b></label>
                                <asp:Label ID="lblSNF1" runat="server"></asp:Label>
                            </div>
                            <hr />
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Milk Collection Office (दूध संग्रह कार्यालय)</label>
                                    <asp:TextBox ID="txtSociatyName" Enabled="false" autocomplete="off" placeholder="Enter Sociaty Name" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Block (विकासखंड)<span style="color: red;">*</span></label>
                                    <asp:TextBox ID="txtBlock" Enabled="false" CssClass="form-control" MaxLength="20" placeholder="Block" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <label>Date (दिनांक)</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                </span>
                                <div class="form-group">
                                    <asp:TextBox ID="txtDate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Shift(शिफ्ट)</label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvShift" runat="server" Display="Dynamic" OnInit="rfvShift_Init" InitialValue="0" ControlToValidate="ddlShift" Text="<i class='fa fa-exclamation-circle' title='Select Shift!'></i>" ErrorMessage="Select Shift" ValidationGroup="a" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddlShift" CssClass="form-control" OnSelectedIndexChanged="ddlShift_SelectedIndexChanged" AutoPostBack="true" CausesValidation="true" ValidationGroup="a" runat="server">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="Morning">Morning</asp:ListItem>
                                        <asp:ListItem Value="Evening">Evening</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>


                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <div id="DivGrid" runat="server">

                                <div class="form-group table-responsive">
                                    <asp:GridView ID="gvSocietyMilkCollection" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                        EmptyDataText="No Record Found." DataKeyNames="I_CollectionID">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvI_CollectionID" runat="server" Visible="false" Text='<%# Eval("I_CollectionID") %>'></asp:Label>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Producer Id">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvV_Code" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Producer Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvI_Producer_ID" runat="server" Visible="false" Text='<%# Eval("I_Producer_ID") %>'></asp:Label>
                                                    <asp:Label ID="lblgvV_Name" runat="server" Text='<%# Eval("ProducerName") %>'></asp:Label>
                                                     (<asp:Label ID="lblProducerCardNo" runat="server" Text='<%# Eval("ProducerCardNo") %>'></asp:Label>)
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Milk Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvV_MilkType" runat="server" Text='<%# Eval("V_MilkType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Milk Quantity(In Ltr.)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvV_Quantity" runat="server" Text='<%# Eval("I_MilkSupplyQty") %>'></asp:Label>
                                                    <asp:Label ID="lblmilkRate" Visible="false" runat="server"></asp:Label>
                                                    <asp:Label ID="lblnetAmount" Visible="false" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quality" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlQuality" Text="<i class='fa fa-exclamation-circle' title='Select Quality!'></i>" ErrorMessage="Select Quality" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                    <asp:DropDownList ID="ddlQuality" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="Good">Good</asp:ListItem>
                                                        <asp:ListItem Value="Sour">Sour</asp:ListItem>
                                                        <asp:ListItem Value="Curd">Curd</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="FAT (%)" ItemStyle-Width="7%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvV_Fat" AutoComplete="off" onkeypress="return validateDec(this,event)" OnTextChanged="txtgvV_Fat_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ID="rfvgvV_Fat" runat="server" Display="Dynamic" ControlToValidate="txtgvV_Fat" Text="<i class='fa fa-exclamation-circle' title='Enter Fat.!'></i>" ErrorMessage="Enter Fat" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>--%>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtgvV_Fat" Display="Dynamic" ValidationExpression="^(\d*\.)?\d+$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Character'></i>" ErrorMessage="Fat should be between 3.2 to 10.9" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtgvV_Fat" Text="<i class='fa fa-exclamation-circle' title='Fat should be between 3.2 to 10.9 range'></i>" ErrorMessage="Fat should be between 3.2 to 10.9 range" ForeColor="Red" MaximumValue="10.9" MinimumValue="3.2" SetFocusOnError="True" ValidationGroup="a" Type="Double"></asp:RangeValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CLR" ItemStyle-Width="7%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvV_Clr" AutoComplete="off" runat="server" onkeypress="return validateDec(this,event)" CssClass="form-control" OnTextChanged="txtgvV_Clr_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ID="rfvgvV_Clr" runat="server" Display="Dynamic" ControlToValidate="txtgvV_Clr" Text="<i class='fa fa-exclamation-circle' title='Enter CLR.!'></i>" ErrorMessage="Enter CLR" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>--%>
                                                    <asp:RegularExpressionValidator ID="rxvV_Clr" ControlToValidate="txtgvV_Clr" Display="Dynamic" ValidationExpression="^(\d*\.)?\d+$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid CLR'></i>" ErrorMessage="CLR should be between 16.0 to 35.0" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                    <asp:RangeValidator ID="rvV_Clr" runat="server" ControlToValidate="txtgvV_Clr" Text="<i class='fa fa-exclamation-circle' title='CLR should be between 16.0 to 35.0 range'></i>" ErrorMessage="CLR should be between 16.0 to 35.0 range" ForeColor="Red" MaximumValue="35.0" MinimumValue="16.0" ValidationGroup="a" SetFocusOnError="True" Type="Double"></asp:RangeValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="SNF (%)" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvV_SNF" runat="server" CssClass="form-control"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Remark">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvV_Remark" AutoComplete="off" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>

                                    <div runat="server" class="pull-right" style="padding-top: 2px;" id="div2">
                                        <asp:Button ID="btnSave" runat="server" ValidationGroup="a" OnClientClick="return ValidatePage();" CssClass="btn btn-success" Text="Save" />
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

            <div class="box box-success" runat="server" visible="false" id="div_milkdetails">
                <div class="box-header">
                    <h3 class="box-title">Milk Process Details(उत्पादन वार दुग्ध की स्तिथी)</h3>
                </div>
                <div class="box-body">

                    <div class="col-md-12">
                        <div class="fancy-title  title-dotted-border">
                            <h5 class="box-title">
                                <b>Milk Collection Office -</b>
                                <asp:Label ID="lblDcsname" runat="server"></asp:Label>(<asp:Label ID="lblblockname" runat="server"></asp:Label>)
                                     <b>Date -</b>
                                <asp:Label ID="lbldate" runat="server"></asp:Label>&nbsp;<b>Shift -</b>  &nbsp;<asp:Label ID="shift" runat="server"></asp:Label>
                                <b>Total Milk Quantity(In Kg) -</b>  &nbsp;<asp:Label ID="lblMqinkg" ForeColor="Red" runat="server" Font-Bold="true"></asp:Label>
                                <b>Total FAT (In Kg) -</b>  &nbsp;<asp:Label ID="lblFAT_IN_KG" ForeColor="Red" runat="server" Font-Bold="true"></asp:Label>
                                <b>Total SNF (In Kg) -</b>  &nbsp;<asp:Label ID="lblSNF_IN_KG" ForeColor="Red" runat="server" Font-Bold="true"></asp:Label>
                                
                            </h5>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <asp:GridView ID="GrdCollectionDetails" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                            EmptyDataText="No Record Found." DataKeyNames="MilkProcessID">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvI_CollectionID" runat="server" Visible="false" Text='<%# Eval("MilkProcessID") %>'></asp:Label>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Producer Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvV_Code" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ProducerName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProducerName" runat="server" Text='<%# Eval("ProducerName") %>'></asp:Label>
                                         (<asp:Label ID="lblProducerCardNo" runat="server" Text='<%# Eval("ProducerCardNo") %>'></asp:Label>)
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Milk Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvV_MilkType" runat="server" Text='<%# Eval("MilkType") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Milk Quantity(In Ltr.)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblI_MilkSupplyQty" runat="server" Text='<%# Eval("I_MilkSupplyQty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Milk Quality">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuality" runat="server" Text='<%# Eval("Quality") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="FAT (%)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFat" runat="server" Text='<%# Eval("Fat") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="CLR">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("CLR") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="SNF (%)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("SNF") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Rate Per Ltr">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("Rate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>



                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script>

        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }
            debugger;
            if (Page_IsValid) {

                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }

    </script>

</asp:Content>

