﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DMDashboard.aspx.cs" Inherits="mis_Dashboard_DMDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success"  style="height:80vh;">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">DM Dashboard</h3>
                            <asp:Label ID="lblMsg" runat="server" Text="" Visible="true"></asp:Label>
                        </div>
                    
                        <div class="box-body">

                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
</asp:Content>

