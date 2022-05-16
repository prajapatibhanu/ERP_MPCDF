<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="AMCU_DailyMilkCollection.aspx.cs" Inherits="mis_MilkCollection_AMCU_DailyMilkCollection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .NonPrintable {
                  display: none;
              }
      
        @media print {
              .NonPrintable {
                  display: block;
              }
              .noprint {
                display: none;
            }
               .pagebreak { page-break-before: always; }
          }
		  /*@page { size:1in 1in; }
     #spn
     {
         width: 80px !important;
    
    display: inline-block;
}
     @page {
                size: auto;
                margin: 0;
            }*/
            @page {
      size: 9.65cm 6.35cm;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content noprint">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">AMCU - Daily Milk Collection</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <fieldset>
                        <legend>Milk Collection Type/Office</legend>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Milk Collection Office(दूध संग्रह कार्यालय)</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txtSociatyName" Text="<i class='fa fa-exclamation-circle' title='Enter Sociaty Name!'></i>" ErrorMessage="Enter Sociaty Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtSociatyName" Enabled="false" autocomplete="off" OnTextChanged="txtSociatyName_TextChanged" AutoPostBack="true" placeholder="Enter Sociaty Name" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Block(विकासखंड)<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtBlock" CssClass="form-control" MaxLength="20" placeholder="Block" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Date(दिनांक)</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>

                                <asp:TextBox ID="txtDate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Shift(शिफ्ट)</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="ddlShift" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Shift!'></i>" ErrorMessage="Select Shift" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlShift" OnInit="ddlShift_Init" OnSelectedIndexChanged="ddlShift_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="Morning">Morning</asp:ListItem>
                                        <asp:ListItem Value="Evening">Evening</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Producer Id(दुग्ध उत्पादक क्र.)<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ControlToValidate="txtProducerId" Text="<i class='fa fa-exclamation-circle' title='Enter Code!'></i>" ErrorMessage="Enter Code" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtProducerId" Enabled="false" CssClass="form-control" MaxLength="13" placeholder="Code" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <label>Producer(दुग्ध उत्पादक)<span style="color: red;">*</span></label>
                            <span class="pull-right">
                                <asp:RequiredFieldValidator ID="rfvFarmer" runat="server" Display="Dynamic" ControlToValidate="ddlFarmer" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Farmer!'></i>" ErrorMessage="Select Farmer" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                            </span>
                            <div class="form-group">
                                <asp:DropDownList ID="ddlFarmer" AutoPostBack="true" CssClass="form-control select2" runat="server" OnSelectedIndexChanged="ddlFarmer_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Milk Type(दुग्ध का प्रकार)<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlMilkType" Text="<i class='fa fa-exclamation-circle' title='Select Milk Type!'></i>" ErrorMessage="Select Milk Type" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlMilkType" CssClass="form-control select2" runat="server"> 
                                    <asp:ListItem Value="Buffalo">Buffalo</asp:ListItem>
                                    <asp:ListItem Value="Cow">Cow</asp:ListItem>
                                    <asp:ListItem Value="Mix">Mix</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Milk Quality(दूध की गुणवत्ता)<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlQuality" Text="<i class='fa fa-exclamation-circle' title='Select Milk Quality!'></i>" ErrorMessage="Select Milk Quality" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlQuality" runat="server" CssClass="form-control select2"> 
                                    <asp:ListItem Value="Good">Good</asp:ListItem>
                                    <asp:ListItem Value="Sour">Sour</asp:ListItem>
                                    <asp:ListItem Value="Curd">Curd</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                    </fieldset>


                    <fieldset>
                        <legend>Milk Collection Entry</legend>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Milk Qty(प्रदाय मात्रा) (In Kg)<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5_N" ValidationGroup="Submit"
                                        ErrorMessage="Enter Net Quantity (In KG)" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Net Quantity (In KG)!'></i>"
                                        ControlToValidate="txtI_MilkQuantity" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5_N" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="Submit" runat="server" ControlToValidate="txtI_MilkQuantity" ErrorMessage="Enter Valid Milk Quantity" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Milk Quantity!'></i>"></asp:RegularExpressionValidator>

                                </span>
                                <asp:TextBox ID="txtI_MilkQuantity" AutoPostBack="true" OnTextChanged="txtI_MilkQuantity_TextChanged" autocomplete="off" Width="100%" onkeypress="return validateDec(this,event)" CssClass="form-control" placeholder="Milk Quantity" runat="server" MaxLength="12"></asp:TextBox>
                            </div>

                        </div>

                       <div class="col-md-3">
                            <div class="form-group">
                                <label>Fat(फैट) % (2.0 - 12)<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvFat" runat="server" Display="Dynamic" ControlToValidate="txtNetFat" Text="<i class='fa fa-exclamation-circle' title='Enter Fat %!'></i>" ErrorMessage="Enter Fat %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ID="revFat_S" ControlToValidate="txtNetFat" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,1})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RegularExpressionValidator>

                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Minimum FAT % required 2.0 and maximum 12." Display="Dynamic" ControlToValidate="txtNetFat" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 2.0 and maximum 12!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit" Type="Double" MinimumValue="2.0" MaximumValue="12"></asp:RangeValidator>

                                </span>
                                <asp:TextBox ID="txtNetFat" autocomplete="off" Width="100%" onkeypress="return validateDec(this,event)" AutoPostBack="true" OnTextChanged="txtNetFat_TextChanged" CssClass="form-control" placeholder="Fat %" runat="server" MaxLength="6"></asp:TextBox>

                            </div>
                        </div>


                        <div class="col-md-3">
                            <div class="form-group">
                                <label>SNF(एस.एन.एफ) %<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvSNF" runat="server" Display="Dynamic" ControlToValidate="txtnetsnf" Text="<i class='fa fa-exclamation-circle' title='Enter SNF %!'></i>" ErrorMessage="Enter SNF %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ID="revSNF_S" ControlToValidate="txtnetsnf" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RegularExpressionValidator>
									
									<asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Minimum SNF required 7.0 and maximum 10.9." Display="Dynamic" ControlToValidate="txtnetsnf" Text="<i class='fa fa-exclamation-circle' title='Minimum SNF required 7.0 and maximum 10.9.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit" Type="Double" MinimumValue="7.0" MaximumValue="10.9"></asp:RangeValidator>
                                </span>
                                <asp:TextBox ID="txtnetsnf" Width="100%" onkeypress="return validateDec(this,event)" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtnetsnf_TextChanged" placeholder="SNF %" runat="server" MaxLength="6"></asp:TextBox>

                            </div>
                        </div>


                        <div class="col-md-3">
                            <div class="form-group">
                                <label>CLR(सी.एल.आर) (16 - 35)<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvCLR" runat="server" Display="Dynamic" ControlToValidate="txtNetCLR" Text="<i class='fa fa-exclamation-circle' title='Enter CLR!'></i>" ErrorMessage="Enter CLR" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ID="revCLR" ControlToValidate="txtNetCLR" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RegularExpressionValidator>

                                    <asp:RangeValidator ID="RangeValidator6" runat="server" ErrorMessage="Minimum CLR required 16 and maximum 35." Display="Dynamic" ControlToValidate="txtNetCLR" Text="<i class='fa fa-exclamation-circle' title='Minimum CLR required 16 and maximum 35.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit" Type="Double" MinimumValue="16" MaximumValue="35"></asp:RangeValidator>
                                </span>
                                <asp:TextBox ID="txtNetCLR" Enabled="false" autocomplete="off" Width="100%" onkeypress="return validateDec(this,event)" CssClass="form-control" placeholder="CLR" runat="server" MaxLength="6"></asp:TextBox>

                            </div>
                        </div>



                        <%--<div class="col-md-3">
                            <div class="form-group">
                                <label>FAT(In Kg)<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ControlToValidate="txtfatinkg" Text="<i class='fa fa-exclamation-circle' title='Enter FAT In Kg!'></i>" ErrorMessage="Enter FAT In Kg" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtfatinkg" Width="100%" Enabled="false" onkeypress="return validateDec(this,event)" CssClass="form-control" placeholder="SNF In Kg" runat="server" MaxLength="6"></asp:TextBox>

                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>SNF(In Kg)<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" ControlToValidate="txtsnfinkg" Text="<i class='fa fa-exclamation-circle' title='Enter SNF In Kg!'></i>" ErrorMessage="Enter SNF In Kg" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtsnfinkg" Width="100%" Enabled="false" onkeypress="return validateDec(this,event)" CssClass="form-control" placeholder="SNF In Kg" runat="server" MaxLength="6"></asp:TextBox>

                            </div>
                        </div>--%>



                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Remark</label>
                                <asp:TextBox ID="txtRemark" TextMode="MultiLine" CssClass="form-control" placeholder="Remark" runat="server"></asp:TextBox>
                            </div>
                        </div>

                    </fieldset>

                    <div class="col-md-12">
                        <div class="form-group">
                            <asp:Button ID="btnSubmit" CssClass="btn btn-success" runat="server" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="Submit" />
                        </div>
                    </div>

                </div>
            </div>

            <div class="box box-success" runat="server" visible="false" id="div_milkdetails">
                <div class="box-header">
                    <h3 class="box-title">Daily Milk Collection Details</h3>
                </div>
                <div class="box-body">
                    <div class="col-md-12">
                        <div class="fancy-title  title-dotted-border">
                            <h5 class="box-title">
                                <b>Milk Collection Office -</b>
                                <asp:Label ID="lblDcsname" runat="server"></asp:Label>&nbsp; (<asp:Label ID="lblblockname" runat="server"></asp:Label>)
                                    &nbsp;&nbsp; <b>Date -</b> &nbsp;&nbsp;<asp:Label ID="lbldate" runat="server"></asp:Label>&nbsp;&nbsp; <b>Shift -</b>  &nbsp;&nbsp;<asp:Label ID="shift" runat="server"></asp:Label>
                                &nbsp;&nbsp; <b>Net Milk Quantity (In Ltr.) -</b>  &nbsp;&nbsp;<asp:Label ID="lbltotalmilkqty" ForeColor="Red" runat="server"></asp:Label>
                            </h5>
                        </div>
                    </div>
                    <div class="col-md-12" style="margin-top: 20px;" runat="server" id="dv_gvMilkQualityDeails">
                        <div class="form-group table-responsive">
                            <asp:GridView ID="gvMilkCollection" runat="server" AutoGenerateColumns="false" CssClass="dataTable table table-striped table-bordered table-hover"
                                EmptyDataText="No Record Found." ShowFooter="true" DataKeyNames="I_CollectionID">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No.">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Producer Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvV_Code" ToolTip='<%# Eval("I_Producer_ID") %>' runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFV_Code" Text="-" runat="server" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Producer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvV_Name" runat="server" Text='<%# Eval("ProducerName") %>'></asp:Label>
                                            (<asp:Label ID="lblProducerCardNo" runat="server" Text='<%# Eval("ProducerCardNo") %>'></asp:Label>)
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFV_Name" Text="-" runat="server" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Milk Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvI_MilkType" runat="server" Text='<%# Eval("V_MilkType") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFI_MilkType" Text="-" runat="server" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Milk Quality">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuality" runat="server" Text='<%# Eval("Quality") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFQuality" Text="-" runat="server" Font-Bold="true"></asp:Label>
                                        </FooterTemplate> 
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Milk Quantity <br/>(In Ltr.)" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="txtI_MilkSupplyQty" runat="server" Text='<%# Eval("I_MilkSupplyQty") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblF_Qty" runat="server" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="FAT">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFat" ToolTip='<%# Eval("TotalFatInKg") %>' runat="server" Text='<%# Eval("Fat") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblF_Fat" runat="server" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="SNF">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsnf" ToolTip='<%# Eval("TotalSNFInKg") %>' runat="server" Text='<%# Eval("SNF") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblF_SNF" runat="server" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="CLR">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("CLR") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblF_CLR" runat="server" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <%--<asp:TemplateField HeaderText="FAT<br/>(In Kg)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalFatInKg" runat="server" Text='<%# Eval("TotalFatInKg") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblF_FatInKg" runat="server" Font-Bold="true"></asp:Label>
                                        </FooterTemplate> 
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="SNF<br/>(In Kg)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalSNFInKg" runat="server" Text='<%# Eval("TotalSNFInKg") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblF_SNFInKg" runat="server" Font-Bold="true"></asp:Label>
                                        </FooterTemplate> 
                                    </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblF_Rate" runat="server" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblF_Amount" runat="server" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbledit" CommandArgument='<%# Eval("I_CollectionID") %>' OnClick="lbledit_Click" runat="server">
                                                <i class="fa fa-pencil"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lblPrint" CommandArgument='<%# Eval("I_CollectionID") %>' OnClick="lblPrint_Click" runat="server">
                                                <i class="fa fa-print"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblF_Action" Text="-" runat="server" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Sr.No." Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNo" runat="server" ToolTip='<%# Eval("Remark") %>' Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                            <asp:Label ID="lblgvI_CollectionID" runat="server" Visible="false" Text='<%# Eval("I_CollectionID") %>'></asp:Label>
                                            <asp:Label ID="lblgvV_SocietyName" runat="server" Visible="false" Text='<%# Eval("V_SocietyName") %>'></asp:Label>
                                            <asp:Label ID="lblDt_Date" runat="server" Visible="false" Text='<%# (Convert.ToDateTime(Eval("Dt_Date"))).ToString("dd/MM/yyyy") %>'></asp:Label>
                                            <asp:Label ID="lblgvV_Shift" runat="server" Visible="false" Text='<%# Eval("V_Shift") %>'></asp:Label>

                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lblFTotal" Text="Total" runat="server" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>

                                    </asp:TemplateField>



                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="content">
            <div id="Print" runat="server" class="NonPrintable" style="padding-left:30px; padding-right:30px;"></div>             
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>
    <script>
        $('.datatable').DataTable({
            paging: true,
            pageLength: 100,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],
            dom: '<"row"<"col-sm-6"B><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
                    },
                    footer: true
                }],
                dom: {
                    container: {
                        className: 'dt-buttons'
                    },
                    button: {
                        className: 'btn btn-default'
                    }
                }
            }
        });
        $(document).on('focus', '.select2-selection.select2-selection--single', function (e) {
            $(this).closest(".select2-container").siblings('select:enabled').select2('open');
        });
    </script>

</asp:Content>
