<%@ Page Title="" Language="C#" MasterPageFile="~/mis/RTIMaster.master" AutoEventWireup="true" CodeFile="FirstAppealDetails.aspx.cs" Inherits="RTI_FirstAppealDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <%--<section class="content-header">
            <div class="row">
                <!-- left column -->
                <div class="col-md-10 col-md-offset-1">
                    
                </div>
            </div>
        </section>--%>

        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-6 ">
                    <!-- general form elements -->
                    <div class="box box-success " id="DetailDiv" runat="server">
                        <div class="box-header with-border">
                            <h3 class="box-title">First Appeal/RTI Detail</h3>
                            <asp:Label ID="lblMsg" runat="server" ClientIDMode="Static" Text=""></asp:Label>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body ">
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend style="margin-bottom: 12px;">First Appeal Detail</legend>
                                        <div class="form-group">
                                            <table class="table table-responsive table-striped table-bordered table-hover ">
                                                <tbody>
                                                    <tr>
                                                        <td>Status</td>
                                                        <td>
                                                            <label class="label label-warning">Open</label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Registartion No.</td>
                                                        <td id="tdreg" runat="server">123/AAS/2541</td>
                                                    </tr>

                                                    <tr>
                                                        <td>First Appeal Date</td>
                                                        <td>18/08/2018</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Ground for First Appeal</td>
                                                        <td>Refused access to information Requested</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                        <div class="form-group">
                                            <div id="Div1" runat="server" style="word-wrap: break-word; text-align: justify">
                                                RTI Act has been made by legislation of Parliament of India on 15 June 2005. The Act came into effect on 12 October 2005 and has been implemented ever since to provide information to crores of Indian citizens. All the constitutional authorities come under this Act, making it one of the most powerful laws of the country.
                                                   The Act covers the whole of India except Jammu and Kashmir, where J&K Right to Information Act is in force. It covers all the constitutional authorities, including executive, legislature and judiciary; any institution or body established or constituted by an act of Parliament or a state legislature. It is also defined in the Act that bodies or authorities established or constituted by order or notification of appropriate government including bodies "owned, controlled or substantially financed" by government, or non-Government organizations "substantially financed, directly or indirectly by funds".
                                                        
                                            </div>
                                            <div class="pull-right">
                                                <div class="form-group">
                                                    <asp:HyperLink ID="HyperLink4" Visible="true" runat="server" CommandName="View" Text="Attachment 1 " Target="_blank"></asp:HyperLink>

                                                </div>
                                            </div>

                                        </div>
                                    </fieldset>
                                </div>
                                <!--Start RTI Request Detail-->

                                <div class="col-md-12">
                                    <fieldset>
                                        <legend style="margin-bottom: 12px;">RTI Detail</legend>
                                        <div class="form-group">
                                            <table class="table table-responsive table-striped table-bordered table-hover ">
                                                <tbody>
                                                    <tr>
                                                        <td>Status</td>
                                                        <td>
                                                            <label class="label label-success">Closed</label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Registartion No.</td>
                                                        <td id="td1" runat="server">123/AAS/2541</td>
                                                    </tr>

                                                    <tr>
                                                        <td>RTI Filed Date</td>
                                                        <td>12/08/2018</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                        <div class="form-group">
                                            <div id="RTIDetails" runat="server" style="word-wrap: break-word; text-align: justify">
                                                RTI Act has been made by legislation of Parliament of India on 15 June 2005. The Act came into effect on 12 October 2005 and has been implemented ever since to provide information to crores of Indian citizens. All the constitutional authorities come under this Act, making it one of the most powerful laws of the country.
                                                   The Act covers the whole of India except Jammu and Kashmir, where J&K Right to Information Act is in force. It covers all the constitutional authorities, including executive, legislature and judiciary; any institution or body established or constituted by an act of Parliament or a state legislature. It is also defined in the Act that bodies or authorities established or constituted by order or notification of appropriate government including bodies "owned, controlled or substantially financed" by government, or non-Government organizations "substantially financed, directly or indirectly by funds".
                                                        
                                            </div>
                                            <div class="pull-right">
                                                <div class="form-group">
                                                    <asp:HyperLink ID="hyprReqDoc" Visible="true" runat="server" CommandName="View" Text="Attachment 1 " Target="_blank"></asp:HyperLink>
                                                    |
                                                             <asp:HyperLink ID="HyperLink2" Visible="true" runat="server" CommandName="View" Text="Attachment 2" Target="_blank"></asp:HyperLink>
                                                </div>
                                            </div>

                                        </div>
                                    </fieldset>
                                </div>


                            </div>

                        </div>

                    </div>
                </div>

                <div class="col-md-6 ">
                    <div class="box box-success direct-chat">
                        <div class="box-header with-border">
                            <h3 class="box-title">Departmental Reply</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body form-group">
                            <!-- Conversations are loaded here -->
                            <div class="direct-chat-messages" style="height: 100%;">
                                <!-- Message. Default to the left -->
                                <div class="direct-chat-msg">
                                    <div class="direct-chat-info clearfix">
                                        <span class="direct-chat-name pull-left">Ankur Jain(MP Agro)</span>
                                        <span class="direct-chat-timestamp pull-right">16 Aug 2:00 pm</span>
                                    </div>
                                    <!-- /.direct-chat-info -->
                                    <img class="direct-chat-img" src="../image/User1.png" alt="message user image">
                                    <!-- /.direct-chat-img -->
                                    <div class="direct-chat-text form-group">
                                        RTI Act has been made by legislation of Parliament of India on 15 June 2005
                                         RTI Act has been made by legislation of Parliament of India on 15 June 2005.
                                         RTI Act has been made by legislation of Parliament of India on 15 June 2005.
                                        .
                         
                                        <div class="attachment text-right">
                                            <br />
                                            <asp:HyperLink ID="hyprDoc1" runat="server" Text="Attachment 1"></asp:HyperLink>
                                            |
                              <asp:HyperLink ID="hyprDoc2" runat="server" Text="Attachment 2"></asp:HyperLink>
                                        </div>

                                    </div>
                                    <!-- /.direct-chat-text -->
                                </div>

                                <div class="direct-chat-msg">
                                    <div class="direct-chat-info clearfix">
                                        <span class="direct-chat-name pull-left">Ankur Jain(MP Agro)</span>
                                        <span class="direct-chat-timestamp pull-right">18 Aug 2:00 pm</span>
                                    </div>
                                    <!-- /.direct-chat-info -->
                                    <img class="direct-chat-img" src="../image/User1.png" alt="message user image">
                                    <!-- /.direct-chat-img -->
                                    <div class="direct-chat-text form-group">
                                        RTI Act has been made by legislation of Parliament of India on 15 June 2005
                                         RTI Act has been made by legislation of Parliament of India on 15 June 2005.
                                         RTI Act has been made by legislation of Parliament of India on 15 June 2005.
                                        .
                         
                                        <div class="attachment text-right">
                                            <br />
                                            <asp:HyperLink ID="HyperLink1" runat="server" Text="Attachment 1"></asp:HyperLink>
                                            |
                              <asp:HyperLink ID="HyperLink3" runat="server" Text="Attachment 2"></asp:HyperLink>
                                        </div>

                                    </div>
                                    <!-- /.direct-chat-text -->
                                </div>



                            </div>
                        </div>
                        <!-- /.box-body -->
                        <!-- /.box-footer-->
                    </div>
                </div>
            </div>
        </section>
        <!-- /.content -->
    </div>




</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

