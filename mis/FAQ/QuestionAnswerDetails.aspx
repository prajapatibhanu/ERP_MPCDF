<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="QuestionAnswerDetails.aspx.cs" Inherits="FAQ_QuestionAnswerDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        section {
            font-family: Arial;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">QUESTION AND ANSWERS Details</h3>
                </div>
                <div class="row">
                    <div class="col-md-12">

                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="box-body">
                    <fieldset>
                        <legend>Question and Answer Details</legend>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Section</label>
                                    <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="1">Plant Operation</asp:ListItem>
                                        <asp:ListItem Value="2">Field Operation</asp:ListItem>
                                        <asp:ListItem Value="3">Quality Control</asp:ListItem>
                                        <asp:ListItem Value="4">Marketing</asp:ListItem>
                                        <asp:ListItem Value="5">Others</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="form-group">
                                    <label>Question<span style="color: red"><b> *</b></span></label>
                                    <asp:DropDownList ID="ddlQuestion" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlQuestion_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-7">
                                <div class="form-group">
                                    <label>Answers</label>
                                    <asp:TextBox ID="txtAnswer" runat="server" placeholder="Answer" TextMode="MultiLine" CssClass="form-control" Style="height: 110px"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                    </fieldset>

                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

