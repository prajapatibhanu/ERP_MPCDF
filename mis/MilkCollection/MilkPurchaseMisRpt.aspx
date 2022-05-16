﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MilkPurchaseMisRpt.aspx.cs" Inherits="mis_MilkCollection_MilkPurchaseMisRpt" %>

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
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Milk Purchase Mis Report</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                             <div class="col-md-3">
                                <div class="form-group">
                                    <label>Office Name</label><span style="color: red">*</span>
                                    <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control" ClientIDMode="Static" Enabled="false">
                                    </asp:DropDownList>
                                </div>
                            </div>
                                      <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Month </label>
                                        <span style="color: red">*</span>
                                        <span class="pull-right">
                                            <asp:requiredfieldvalidator id="rfv1" runat="server" validationgroup="a" display="Dynamic" controltovalidate="txtFdt" errormessage="Please Enter From Date." text="<i class='fa fa-exclamation-circle' title='Please Enter From Date !'></i>"></asp:requiredfieldvalidator>
                                           
                                        </span>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:textbox id="txtFdt" onkeypress="javascript: return false;" width="100%" maxlength="10" onpaste="return false;" placeholder="Select From Date" runat="server" autocomplete="off" cssclass="form-control" data-provide="datepicker" data-date-format="mm/yyyy" data-date-autoclose="true" clientidmode="Static"></asp:textbox>
                                        </div>
                                    </div>
                                </div>

                                


                                <div class="col-md-1" style="margin-top: 20px;" runat="server">
                                    <div class="form-group">
                                        <asp:button runat="server" cssclass="btn btn-primary"  validationgroup="a" id="btnSubmit" OnClick="btnSubmit_Click" text="Search" accesskey="S" />
                                    </div>
                                </div>
                            <div class="row" id="divshow" runat="server" visible="false">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="btn btn-primary" OnClick="btnExport_Click"/>
                                             <asp:Button ID="btnprint" Text="Print" runat="server" CssClass="btn btn-primary" OnClientClick="window.print();" />
                                        </div>
                                    </div>
                                <div class="col-md-12">
                                <div id="divreport" runat="server">

                                </div>
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
    <script>
        $("#txtFdt").datepicker({
            format: "mm/yyyy",
            viewMode: "months",
            minViewMode: "months",
            autoclose: true,
        });
    </script>
</asp:Content>

