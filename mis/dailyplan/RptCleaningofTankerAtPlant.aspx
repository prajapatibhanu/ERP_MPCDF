<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptCleaningofTankerAtPlant.aspx.cs" Inherits="mis_dailyplan_RptCleaningofTankerAtPlant" %>

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
           <div class="box box-primary">
               <div class="box-header">
                   <h3 class="box-title">Cleaning Of Tanker Request</h3>
               </div>
               <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
               <div class="box-body">
                    <fieldset>
                        <legend>Cleaning Of Tanker</legend>
                        <div class="row">
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
                                    <asp:TextBox ID="txtToDate" autocomplete="off" placeholder="Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server" ></asp:TextBox>
                                </div>
                            </div>
                        </div>                          
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Button ID="btnSearch" CssClass="btn btn-success" style="margin-top:21px;" runat="server" ValidationGroup="Save" Text="Search" OnClick="btnSearch_Click"/>
                                   
                                </div>
                            </div>
                        </div>
                    </fieldset>
                </div>
               <div class="box-body">
                   <fieldset>
                       <legend>Cleaning Of Tanker Request Details</legend>
                       <div class="row">
                           <div class="col-md-12 pull-right noprint">
                            <div class="form-group">
                                <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn btn-default" Text="Excel" OnClick="btnExport_Click"/>                            
                                
                            </div>      
                            </div>
                           <div class="col-md-12">
                               <div class="table-responsive">
                                   <asp:GridView ID="GridView1" EmptyDataText="No Record Found" runat="server" ShowHeaderWhenEmpty ="true" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="GridView1_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Request Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRequestDate" runat="server" Text='<%# Eval("RequestDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tanker No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTankerNo" runat="server" Text='<%# Eval("V_VehicleNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvddlStatus" runat="server" Display="Dynamic" ControlToValidate="ddlStatus" Text="<i class='fa fa-exclamation-circle' title='Select Status!'></i>" ErrorMessage="Select Status" InitialValue="0" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Update"></asp:RequiredFieldValidator>
                                </span>
                                                <asp:DropDownList ID="ddlStatus" runat="server" Visible="false" CssClass="form-control select2">
                                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                                    <asp:ListItem Value="Pending">Pending</asp:ListItem>                                                
                                                     <asp:ListItem Value="Cleaned">Cleaned</asp:ListItem>
                                                </asp:DropDownList>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remark">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                                <asp:TextBox ID="txtRemark" Visible="false" runat="server" Text='<%# Eval("Remark") %>' CssClass="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    
                                                    <asp:LinkButton ID="lnkView" CssClass="label label-default" runat="server" CommandArgument='<%# Eval("CleaningOfTanker_ID") %>' Visible='<%# Eval("Status").ToString()=="Pending"?false:true %>' CommandName="ViewRecord">Print</asp:LinkButton>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                    </Columns> 
                                </asp:GridView>
                               </div>
                           </div>
                       </div>
                   </fieldset>
               </div>
           </div>
        </section>
    </div>
      <section class="content">
          <div id="divPrint"  class="NonPrintable" runat="server"></div>
      </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
   
</asp:Content>

