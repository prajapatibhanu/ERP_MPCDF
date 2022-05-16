<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="mis_Dashboard_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="assets/css/Dashboard.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header ui-sortable-handle" style="cursor: move;">
                            <h3 class="box-title">Dashboard</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body" style="">
                            <div class="row">
                                <div class="col-md-12">
                                    <h4 class="box-title" id="h4" runat="server">Circular / Mail Detail</h4>
                                    <div class="form-group">
                                        <div class="table table-responsive">
                                            <asp:GridView ID="gvCirculerDetails" AutoGenerateColumns="false" runat="server" class="table table-hover table-bordered pagination-ys">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Sent On" DataField="EmailSentOn" />
                                                    <asp:BoundField HeaderText="Subject" DataField="EmailSubject" />
                                                    <asp:BoundField HeaderText="Body" DataField="EmailBody" />
                                                    <asp:BoundField HeaderText="Sent To" DataField="EmailSentTo" />
                                                    <asp:TemplateField HeaderText="View" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hypUploadedDoc" Target="_blank" NavigateUrl='<%# Eval("EmailAttachment") %>' runat="server">View Doc</asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%-- <div class="row">
                                <div class="col-md-6">
                                    <div class="info-box">
                                        <a href="../Grievance/Grievance_List.aspx">
                                            <span class="info-box-icon bg-aqua"><i class="fa fa-edit" aria-hidden="true"></i></span>
                                            <div class="info-box-content">
                                                <span class="info-box-text">आपके द्वारा दायर की गई शिकायत
                                                </span>
                                                <asp:Label ID="lblTotalGrievance" runat="server" class="info-box-number" Text=""></asp:Label>
                                            </div>
                                        </a>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="info-box">
                                        <a href="../filetracking/FTComposeFileList.aspx">
                                            <span class="info-box-icon bg-green"><i class="fa fa-file-text" aria-hidden="true"></i></span>
                                            <div class="info-box-content">
                                                <span class="info-box-text">मेरी मेज पर फ़ाइल/नोटशीट की संख्या </span>
                                                <asp:Label ID="lblFileOnDesk" runat="server" class="info-box-number" Text=""></asp:Label>
                                            </div>
                                        </a>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="info-box">
                                        <a href="../HR/HREmpWiseLeaveDetail.aspx">
                                            <span class="info-box-icon bg-yellow"><i class="fa fa-calendar" aria-hidden="true"></i></span>

                                            <div class="info-box-content">
                                                <span class="info-box-text">आपकी छुट्टी का आवेदन अनुमोदन के लिए लंबित</span>
                                                <asp:Label ID="lblMyPendingLeave" runat="server" class="info-box-number" Text=""></asp:Label>
                                            </div>
                                        </a>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="info-box">
                                        <a href="../HR/HREmpLeaveRequests.aspx">
                                            <span class="info-box-icon bg-red"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>

                                            <div class="info-box-content">
                                                <span class="info-box-text">आपके पास अनुमोदन के लिए लंबित छुट्टी के आवेदन </span>
                                                <asp:Label ID="lblOtherPendingLeave" runat="server" class="info-box-number" Text=""></asp:Label>
                                            </div>
                                        </a>
                                    </div>
                                </div>


                                <div class="col-md-6">
                                    <div class="form-group">
                                        <div class="schedule-warp">
                                            <div class="day-one">
                                                आगामी बैठक (Upcoming Meeting) :
                             <asp:Label ID="lblToDay" runat="server" Text=""></asp:Label>
                                            </div>
                                            <asp:Repeater ID="RepeaterTodayMeeting" runat="server">
                                                <ItemTemplate>
                                                    <div class="schedule-card">
                                                        <div class="insidebox">
                                                            <span class="schedule-tag"><%# Eval("Meeting_StartTime").ToString()%></span>
                                                            <span class="schedule-start"><%# Eval("Meeting_Subject").ToString()%></span>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <div class="schedule-warp">
                                            <div class="day-one">
                                                आगामी माइलस्टोन (Upcoming MileStone)
                                <asp:Label ID="lblTomorrow" runat="server" Text=""></asp:Label>
                                            </div>
                                            <asp:Repeater ID="RepeaterTomorrowMeeting" runat="server">
                                                <ItemTemplate>
                                                    <div class="schedule-card">
                                                        <div class="insidebox">
                                                            <span class="schedule-tag"><%# Eval("TLDate").ToString()%></span>
                                                            <span class="schedule-start"><%# Eval("ImportantSubject").ToString()%></span>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <div class="schedule-warp">
                                            <div class="day-one">
                                                Salary Drawn In 
                                                <asp:Label ID="lblSalaryMonth" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="lblSalaryYear" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="schedule-card">
                                                <div class="insidebox">
                                                    <span class="schedule-tag">Total Earnings</span>
                                                    <span class="schedule-start"><i class="fa fa-inr"></i>
                                                        <asp:Label ID="lblTotalEarnings" runat="server" Text=""></asp:Label></span>
                                                </div>
                                            </div>
                                            <div class="schedule-card">
                                                <div class="insidebox">
                                                    <span class="schedule-tag">Total Deductions</span>
                                                    <span class="schedule-start"><i class="fa fa-inr"></i>
                                                        <asp:Label ID="lblTotalDeductions" runat="server" Text=""></asp:Label></span>
                                                </div>
                                            </div>
                                            <div class="schedule-card">
                                                <div class="insidebox">
                                                    <span class="schedule-tag">Net Salary</span>
                                                    <span class="schedule-start"><i class="fa fa-inr"></i>
                                                        <asp:Label ID="lblNetSalary" runat="server" Text=""></asp:Label></span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="box">
                                        <div class="box-header with-border">

                                            <h3 class="box-title">
                                                <img src="assets_dashboard/cake.png" alt="scheme" style="width: 30px;">&nbsp; &nbsp; &nbsp;Birthdays</h3>
                                        </div>
                                        <div class="box-body">
                                            <asp:GridView ID="GridViewBirth" runat="server" class="table table-bordered table-dark table-hover" AutoGenerateColumns="False" DataKeyNames="Emp_ID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                                    <asp:BoundField DataField="EmpOffice" HeaderText="Office  Name" />
                                                    <asp:BoundField DataField="Birthdate" HeaderText="Birth Date" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                    </div>
                                </div>


                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </section>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

