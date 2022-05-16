<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DeleteCCWiseCanesCollEntry.aspx.cs" Inherits="mis_MilkCollection_DeleteCCWiseCanesCollEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">

    <div  class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box primary">
                        <div class="box-header">
                            <h3 class="box-title">Delete CC Wise Canes Collection Entry</h3>
                        </div>
                        <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>Filter</legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Office Name<span style="color: red">*</span></label>
                                            <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control" ClientIDMode="Static" Enabled="false">
                                            </asp:DropDownList>
                                             </div>
                                        </div>
                                           
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>CC<span style="color:red"> *</span></label>
                                                        <span class="pull-right">
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select CC" Text="<i class='fa fa-exclamation-circle' title='Select CC !'></i>"
                                            ControlToValidate="ddlccbmcdetail" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                                        </span>
                                                        <asp:DropDownList ID="ddlccbmcdetail"  runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                                    </div>
                                                </div>
                                          
                                    <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Date </label>
                                        <span style="color: red">*</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtFdt" ErrorMessage="Please Enter Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter Date !'></i>"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revdate" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtFdt" ErrorMessage="Invalid  Date" Text="<i class='fa fa-exclamation-circle' title='Invalid  Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                        </span>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtFdt"  onkeypress="javascript: return false;" Width="100%" MaxLength="10" data-date-end-date="0d" onpaste="return false;" placeholder="Select  Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                       
                                <div class="col-md-1" style="margin-top: 20px;" runat="server">
                                   <div class="form-group">  
                                 <div class="form-group">      
                              <asp:Button runat="server" CssClass="btn btn-primary"  ValidationGroup="a" ID="btnSearch" Text="Search" AccessKey="S" OnClick="btnSearch_Click"/>

                                  </div>  
                                   </div> 
                                </div>

                                </div>                                                              
                            </fieldset>


                            <fieldset>
                                <legend>Details</legend>
                                 <div class="col-md-12">
                                    <div class="form-group">
                                         <asp:Button ID="btnDelete" runat="server" Visible="false" OnClientClick="return confirm('Do youreallly want to delete Record?');" Text="Delete" CssClass="btn btn-primary" OnClick="btnDelete_Click"/>

                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvEntryList" ShowFooter="true" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" runat="server" CssClass="datatable table table-bordered" AutoGenerateColumns="false" >
                                            <Columns>

                                                <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Shift">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblShift" runat="server" Text='<%# Eval("Shift") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="C_ReferenceNo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblC_ReferenceNo" runat="server" Text='<%# Eval("C_ReferenceNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="V_SampleNo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblV_SampleNo" runat="server" Text='<%# Eval("V_SampleNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Society">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSociety" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Society Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSocietyCode" runat="server" Text='<%# Eval("Office_Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="B/C">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMilkType" runat="server" Text='<%# Eval("V_MilkType") %>'></asp:Label>
                                                  
                                                         </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText="Milk Quality">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMilkQuality" runat="server" Text='<%# Eval("V_MilkQuality") %>'></asp:Label>
                                                   
                                                         </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Milk Quantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMilkQuantity" runat="server" Text='<%# Eval("I_MilkQuantity") %>'></asp:Label>
                                                       
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                      <%--          <asp:TemplateField HeaderText="Fat">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFat" runat="server" Text='<%# Eval("Fat") %>'></asp:Label>
                                                       
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Snf">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSnf" runat="server" Text='<%# Eval("Snf") %>'></asp:Label>
                                                      
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Clr">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblClr" runat="server" Text='<%# Eval("Clr") %>'></asp:Label>
                                                       
                                                       
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fat(In Kg)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFatInKg" runat="server" Text='<%# Eval("FatInKg") %>'></asp:Label>  <asp:TextBox ID="txtFatInKg" Enabled ="false" Visible="false" CssClass="form-control" runat="server" Text='<%# Eval("FatInKg") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Snf(In Kg)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSnfInKg" runat="server" Text='<%# Eval("SnfInKg") %>'></asp:Label>
                                                       
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                
                                                
                                            </Columns>
                                        </asp:GridView>
										
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
</asp:Content>

