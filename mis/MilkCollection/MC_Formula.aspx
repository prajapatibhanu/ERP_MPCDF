<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MC_Formula.aspx.cs" Inherits="mis_MilkCollection_MC_Formula" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Calculate Formula</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">

                    <div class="row">
                         
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Fat % (3.2 - 10)<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvFat" runat="server" Display="Dynamic" ControlToValidate="txtNetFat" Text="<i class='fa fa-exclamation-circle' title='Enter Fat %!'></i>" ErrorMessage="Enter Fat %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ID="revFat_S" ControlToValidate="txtNetFat" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,1})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>

                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Minimum FAT % required 3.2 and maximum 10." Display="Dynamic" ControlToValidate="txtNetFat" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 3.2 and maximum 10.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a" Type="Double" MinimumValue="3.2" MaximumValue="10"></asp:RangeValidator>

                                </span>
                                <asp:TextBox ID="txtNetFat" autocomplete="off" Width="100%" onkeypress="return validateDec(this,event)" CssClass="form-control" placeholder="Fat %" runat="server" MaxLength="6"></asp:TextBox>

                            </div>

                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>CLR (20 - 30)<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvCLR" runat="server" Display="Dynamic" ControlToValidate="txtNetCLR" Text="<i class='fa fa-exclamation-circle' title='Enter CLR!'></i>" ErrorMessage="Enter CLR" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ID="revCLR" ControlToValidate="txtNetCLR" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>

                                    <asp:RangeValidator ID="RangeValidator6" runat="server" ErrorMessage="Minimum FAT % required 20 and maximum 30." Display="Dynamic" ControlToValidate="txtNetCLR" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 20 and maximum 30.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a" Type="Double" MinimumValue="20" MaximumValue="30"></asp:RangeValidator>
                                </span>
                                <asp:TextBox ID="txtNetCLR" autocomplete="off" Width="100%" onkeypress="return validateDec(this,event)" CssClass="form-control" placeholder="CLR" runat="server" MaxLength="6"></asp:TextBox>

                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Milk Quantity In Ltr<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5_N" ValidationGroup="a"
                                        ErrorMessage="Enter Net Quantity (In Ltr)" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Net Quantity (In KG)!'></i>"
                                        ControlToValidate="txtI_MilkQuantity" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5_N" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="a" runat="server" ControlToValidate="txtI_MilkQuantity" ErrorMessage="Enter Valid Milk Quantity" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Milk Quantity!'></i>"></asp:RegularExpressionValidator>

                                </span>
                                <asp:TextBox ID="txtI_MilkQuantity" CssClass="form-control" onkeypress="return validateDec(this,event)" placeholder="Milk In Ltr" runat="server"></asp:TextBox>

                            </div>
                        </div>


                        <div class="col-md-3">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:Button runat="server" OnClick="btnSubmit_Click" Style="margin-top: 20px;" CssClass="btn btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Calculate" AccessKey="S" />
                                </div>
                            </div>
                        </div>

                    </div>


                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>SNF % - 1 <span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtnetsnf_Dcs" Width="100%" Enabled="false" CssClass="form-control" placeholder="SNF %" runat="server" MaxLength="6"></asp:TextBox>

                            </div>
                        </div>

                        <div class="col-md-10">
                            <div class="form-group">
                                <label>Formula  (DCS/BMC/MDP Milk Collection/Dispatch and CC Milk Receive) <span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtdcsformula" Text="snf % = ((clr / 4) + (fat % * (0.2)) + (0.7));" Width="100%" Enabled="false" CssClass="form-control" placeholder="SNF %" runat="server" MaxLength="6"></asp:TextBox>

                            </div>
                        </div>

                    </div>


                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>SNF % - 2 <span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtnetsnf" Width="100%" Enabled="false" CssClass="form-control" placeholder="SNF %" runat="server" MaxLength="6"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-10">
                            <div class="form-group">
                                <label>Formula  (CC Dispatch & DS Receive)<span style="color: red;">*</span></label>
                                <asp:TextBox ID="TextBox1" Text="snf % = ((clr / 4) + (fat % * (0.25)) + (0.44));" Width="100%" Enabled="false" CssClass="form-control" placeholder="SNF %" runat="server" MaxLength="6"></asp:TextBox>

                            </div>
                        </div>

                    </div>


                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Milk Quantity In Kg <span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtmilkQuantityInkg" Width="100%" Enabled="false" CssClass="form-control" placeholder="Milk In KG" runat="server" MaxLength="6"></asp:TextBox>

                            </div>
                        </div>

                        <div class="col-md-10">
                            <div class="form-group">
                                <label>Formula<span style="color: red;">*</span></label>
                                <asp:TextBox ID="TextBox4" Text="Milk In KG = (Milk In Ltr * Milk Gravity((1+ (CLR/1000)))" Width="100%" Enabled="false" CssClass="form-control" placeholder="SNF %" runat="server" MaxLength="6"></asp:TextBox>

                            </div>
                        </div>


                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>FAT IN KG<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtfatinKG" Width="100%" Enabled="false" CssClass="form-control" placeholder="Fat In KG" runat="server" MaxLength="6"></asp:TextBox>

                            </div>
                        </div>

                        <div class="col-md-10">
                            <div class="form-group">
                                <label>Formula<span style="color: red;">*</span></label>
                                <asp:TextBox ID="TextBox2" Text="Fat In KG = (Fat % * Milk in KG) / 100" Width="100%" Enabled="false" CssClass="form-control" placeholder="SNF %" runat="server" MaxLength="6"></asp:TextBox>

                            </div>
                        </div>


                    </div>



                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>SNF IN KG (SNF % -1)<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtsnfinkg_Dcs" Width="100%" Enabled="false" CssClass="form-control" placeholder="SNF In KG" runat="server" MaxLength="6"></asp:TextBox>

                            </div>
                        </div>

                        <div class="col-md-10">
                            <div class="form-group">
                                <label>Formula<span style="color: red;">*</span></label>
                                <asp:TextBox ID="TextBox6" Text="SNF In KG = (snf %  * Milk in KG) / 100" Enabled="false" CssClass="form-control" placeholder="SNF %" runat="server" MaxLength="6"></asp:TextBox>

                            </div>
                        </div>

                    </div>



                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>SNF IN KG (SNF % -2)<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtsnfinkg" Width="100%" Enabled="false" CssClass="form-control" placeholder="SNF In KG" runat="server" MaxLength="6"></asp:TextBox>

                            </div>
                        </div>

                        <div class="col-md-10">
                            <div class="form-group">
                                <label>Formula<span style="color: red;">*</span></label>
                                <asp:TextBox ID="TextBox3" Text="SNF In KG = (snf % * Milk in KG) / 100" Enabled="false" CssClass="form-control" placeholder="SNF %" runat="server" MaxLength="6"></asp:TextBox>

                            </div>
                        </div>

                    </div>
                     
                </div>
            </div>


        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>
