<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="R_DProgressReportInDS1.aspx.cs" Inherits="mis_ResearchandDev_R_DProgressReportInDS1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Dugdh Sangh Progress Report Of R & D</h3>
                            <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">

                                <table class="table table-bordered">
                                    <tr>
                                        <th>S.no</th>
                                        <th>Project Name</th>
                                        <th>Received Date</th>
                                        <th>Details</th>                                        
                                    </tr>
                                    <tr>
                                        <td>1</td>
                                        <td>Testing</td>
                                        <td>30/05/2020</td>
                                        <td>
                                            <asp:LinkButton ID="lnkbtnStatus" runat="server" Text="View" data-toggle="modal" data-target="#myModal"></asp:LinkButton></td>

                                    </tr>
                                </table>


                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="myModal" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">View Project Details</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <fieldset>
                                    <legend>Project Details</legend>
                                    <table class="table table-bordered">
                                        <tr>
                                            <td>Project Name:</td> 
                                            <td>Testing</td>                                           
                                        </tr>
                                        <tr>                                            
                                            <td>Project Outcomes:</td>  
                                            <td></td>                                                                                       
                                        </tr>
                                        <tr>                                                                                   
                                            <td>Supporting Document:</td>
                                            <td><a href="">Download</a></td>  
                                        </tr>
                                    </table>
                                </fieldset>
                                
                            </div>
                            <div class="row">                              
                                <div class="col-md-12">
                                <div class="form-group">
                                    <label>Reset Details</label>
                                    <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control">                                   
                                    </asp:TextBox>
                                </div>

                            </div>
                                </div>  
                            <div class="row">
                            <div class="col-md-6">
                                  <div class="form-group">
                                        <label>Support Doc1</label>
                                        <asp:FileUpload ID="fileupload1" runat="server" CssClass="form-control" />
                                    </div>
                            </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Support Doc2</label>
                                        <asp:FileUpload ID="fileupload" runat="server" CssClass="form-control" />
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <label>Action Taken</label>
                                    <div class="form-group">
                                        <asp:DropDownList runat="server" ID="ddlAction" ClientIDMode="Static" CssClass="form-control">                                              
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem>Under Trial</asp:ListItem>
                                                <asp:ListItem>will use after Some time </asp:ListItem>
                                                <asp:ListItem>Other's</asp:ListItem>                                               
                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>                       
                        </div>
                        <div class="modal-footer">

                            <asp:Button runat="server" Text="Save" ID="Button2" ClientIDMode="Static" CssClass="btn btn-primary"></asp:Button>
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function myModal() {
            $('#myModal').modal('show');

        }
    </script>
</asp:Content>


