<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptMIS__MilkProcSale.aspx.cs" Inherits="mis_Mis_Reports_MilkProcSale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
    <style>
        .NonPrintable {
                  display: none;
              }
	    .header {
                display: table-header-group;
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
                <div class="box box-primary">
                    <div class="box-header">
                        <h3 class="box-title">Year Wise Milk Procurement and Sale Report</h3>
                    </div>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                    <div class="box-body">
                       <div class="row">
                           <fieldset>
                               <legend>Filter</legend>
                               <div class="row mainborder">
                                   <div class="col-md-12">
                                       
                                   </div>

                                   <div class="row">
                                   <div class="col-md-12">
                                   <div class="row">
                                   <div class="col-md-2">
                                       <div class="form-group">
                                        <label>Year:<span style="color:red">*</span></label>
                                           <span class="pull-right">
                                           <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Select Year" InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Year !'></i>"
                                                    ControlToValidate="ddlFiancialYear" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>--%>
                                           </span>
                                            
                                           <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control select2 ">
                                            </asp:DropDownList>
                                           
                                       </div>
                                   </div>
                                       <div class="col-md-2">
                                           <div class="form-group">
                                               <asp:Button ID="btnSearch" runat="server" Text="Search" style="margin-top:20px;" CssClass="btn btn-success" OnClick="btnSearch_Click"/>
                                           </div>
                                       </div>
                                       </div>

                                    
                                       </div>
                                       </div>
                               </div>
                           </fieldset>
                       </div>
                        <div class="row">
                             <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Button ID="btnExport" Visible="false" runat="server" Text="Export" CssClass="btn btn-primary" OnClick="btnExport_Click"/>
                                             <asp:Button ID="btnprint" Visible="false" Text="Print" runat="server" CssClass="btn btn-primary" OnClientClick="window.print();" />
                                        </div>
                                    </div>
                            <div class="col-md-12">
                                <div id="divReport" runat="server">

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
         <section class="content">
            <div id="divprint" class="NonPrintable" runat="server"></div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
   
</asp:Content>

