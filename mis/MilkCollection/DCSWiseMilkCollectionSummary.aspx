<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DCSWiseMilkCollectionSummary.aspx.cs" Inherits="mis_MilkCollection_DCSWiseMilkCollectionSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
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
              
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <section class="content noprint">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">BMC/DCS Wise Milk Collection Summary</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body ">
                           <fieldset>
                               <legend>FILTER</legend>

                               <div class="col-md-2">
                                <div class="form-group">
                                    <label>CC<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="Save"
                                            InitialValue="0" ErrorMessage="Select CC" Text="<i class='fa fa-exclamation-circle' title='Select CC !'></i>"
                                            ControlToValidate="ddlccbmcdetail" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlccbmcdetail"  runat="server" CssClass="form-control select2" ClientIDMode="Static" OnSelectedIndexChanged="ddlccbmcdetail_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                                 
                                <div class="col-md-2">
                                    <label>Society<span style="color: red;">*</span></label>
                                      <%--<span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Save"
                                                    InitialValue="0" ErrorMessage="Select Society"  Text="<i class='fa fa-exclamation-circle' title='Select Society !'></i>"
                                                    ControlToValidate="ddlSociety" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>--%>
                                    <div class="form-group">
                                      
                                        <asp:DropDownList ID="ddlSociety" CssClass="form-control select2" runat="server">
                                            <asp:ListItem Value="0">All</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    </div>

                               <div class="col-md-2">
                                    <label>Quality Type<span style="color: red;">*</span></label>                                  
                                    <div class="form-group">
                                      
                                        <asp:DropDownList ID="ddlQualityType" CssClass="form-control select2" runat="server">
                                            <asp:ListItem Value="0">All</asp:ListItem>
                                            <asp:ListItem Value="Good">Good</asp:ListItem>
                                            <asp:ListItem Value="Sour">Sour</asp:ListItem>
                                            <asp:ListItem Value="Curdle">Curdle</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    </div>

                               <div class="col-md-2">
                                        <div class="form-group">
                                            <label>From Date<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtFDate" Text="<i class='fa fa-exclamation-circle' title='Enter From Date!'></i>" ErrorMessage="Enter From Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="far fa-calendar-alt"></i>
                                                    </span>
                                                </div>
                                                <asp:TextBox ID="txtFDate" ClientIDMode="Static" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                               <div class="col-md-2">
                                        <div class="form-group">
                                            <label>To Date<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtTDate" Text="<i class='fa fa-exclamation-circle' title='Enter To Date!'></i>" ErrorMessage="Enter To Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="far fa-calendar-alt"></i>
                                                    </span>
                                                </div>
                                                <asp:TextBox ID="txtTDate" ClientIDMode="Static" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                 <div class="col-md-2">
                                    <label>Shift<span style="color: red;">*</span></label>                                  
                                    <div class="form-group">
                                      
                                        <asp:DropDownList ID="ddlShift" CssClass="form-control select2" runat="server">
                                            <asp:ListItem Value="0">All</asp:ListItem>
                                            <asp:ListItem Value="Morning">Morning</asp:ListItem>
                                            <asp:ListItem Value="Evening">Evening</asp:ListItem>
                                          
                                        </asp:DropDownList>
                                    </div>
                                    </div>
                               <div class="col-md-2">
                                   <div class="form-group">
                                       <asp:Button ID="btnSearch" runat="server"  Text="Search" ValidationGroup="Save" CssClass="btn btn-success" OnClick="btnSearch_Click"/>
                                   </div>
                               </div>
                           </fieldset>
                            <fieldset>
                                    <legend>Report</legend>
                                    <div class="row" id="divshow" runat="server">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Button ID="btnExport" runat="server" Visible="false" Text="Export" CssClass="btn btn-primary" OnClick="btnExport_Click"/>
                                             <asp:Button ID="btnprint" Text="Print" Visible="false" runat="server" CssClass="btn btn-primary" OnClientClick="window.print();" />
                                        </div>
                                    </div>
                                </div>
                                   
                                <table id="tblreport" runat="server" visible="false" style="width:100%">
                       <tr>
                           <td style="text-align:center"><span id="Span1"  runat="server"></span></td>
                       </tr>
                        <tr>
                           <td style="text-align:center"> <span id="Span2" style="text-align:center"  runat="server"></span></td>
                       </tr>
                        <tr>
                           <td style="text-align:center">BMC/DCS Wise Milk Collection Summary</td>
                       </tr>
                        <tr>
                           <td style="text-align:center"><span id="Span3" runat="server"></span></td>
                       </tr>
                       <tr>
                           <td>
                               <asp:GridView ID="GvReport" ShowFooter="true" CssClass="table table-bordered table-responsive" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found">
                                          <Columns>
                                              <asp:TemplateField HeaderText="S.No">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Soc.Name">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Office_Name_E") %>'></asp:Label>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Soc.Code">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Office_Code") %>'></asp:Label>
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
                                               <asp:TemplateField HeaderText="Milk Type">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lblMilkType" runat="server" Text='<%# Eval("MilkType") %>'></asp:Label>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Quality">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lblQuality" runat="server" Text='<%# Eval("MilkQuality") %>'></asp:Label>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Quantity">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("MilkQuantity") %>'></asp:Label>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Fat">
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
                                                <asp:TemplateField HeaderText="FatKg">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lblFatKg" runat="server" Text='<%# Eval("FatInKg") %>'></asp:Label>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SnfKg">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lblSnfKg" runat="server" Text='<%# Eval("SnfInKg") %>'></asp:Label>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                          </Columns>
                                        </asp:GridView>
                           </td>
                       </tr>
                   </table>
                                        
                                    
                                </fieldset>
                        </div>
                        
                    </div>
                </div>
            </div>
        </section>
        <section class="content NonPrintable">
           <div class="row">
               <div class="col-md-12">
                   <table style="width:100%">
                       <tr>
                           <td style="text-align:center"><span id="spnofc"  runat="server"></span></td>
                       </tr>
                        <tr>
                           <td style="text-align:center"> <span id="spnsociety" style="text-align:center"  runat="server"></span></td>
                       </tr>
                        <tr>
                           <td style="text-align:center">BMC/DCS Wise Milk Collection Summary</td>
                       </tr>
                        <tr>
                           <td style="text-align:center"><span id="spndate" runat="server"></span></td>
                       </tr>
                       <tr>
                           <td>
                               <asp:GridView ID="GvReport1" CssClass="table table-bordered table-responsive" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found">
                                          <Columns>
                                              <asp:TemplateField HeaderText="S.No">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Soc.Name">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Office_Name_E") %>'></asp:Label>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Soc.Code">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Office_Code") %>'></asp:Label>
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
                                               <asp:TemplateField HeaderText="Milk Type">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lblMilkType" runat="server" Text='<%# Eval("MilkType") %>'></asp:Label>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Quality">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lblQuality" runat="server" Text='<%# Eval("MilkQuality") %>'></asp:Label>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Quantity">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("MilkQuantity") %>'></asp:Label>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Fat">
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
                                                <asp:TemplateField HeaderText="FatKg">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lblFatKg" runat="server" Text='<%# Eval("FatInKg") %>'></asp:Label>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SnfKg">
                                                  <ItemTemplate>
                                                      <asp:Label ID="lblSnfKg" runat="server" Text='<%# Eval("SnfInKg") %>'></asp:Label>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                          </Columns>
                                        </asp:GridView>
                           </td>
                       </tr>
                   </table>
                  
                  
                   
               </div>
           </div>
                     
                            
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
</asp:Content>

