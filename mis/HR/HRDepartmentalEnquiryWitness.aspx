<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HRDepartmentalEnquiryWitness.aspx.cs" Inherits="mis_HR_HRDepartmentalEnquiryWitness" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="css/hrcustom.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper" style="min-height: 960px;">
        <section class="content-header">
            <h1>Witness Details
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Witness Details</li>
            </ol>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <table class="table">
	
                        <tr>
                            <td>EMPLOYEE NAME </td>
                            <td>Demo Employee</td>
                            <td>ORDER NO</td>
                            <td>HRD/APX/2020/1520 </td>
                        </tr>
                        <tr>
                            <td>ENQUIRY OFFICER</td>
                            <td>Demo Officer I</td>
                            <td>PRESENTING OFFICER</td>
                            <td>Demo Officer II</td>
                        </tr>
                    </table>
                </div>



                <div class="col-md-12 main-form">
                    <div class="box box-pramod">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <div class="form-group">
                                        <label>Witness Name<span class="text-red">*</span></label>
                                        <div class="input-group">
                                            <div class="input-group-addon">
                                                <i class="fa fa-user"></i>
                                            </div>
                                            <asp:TextBox ID="TextBox2" runat="server" placeholder="Witness Name" class="form-control" autocomplete="off" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Witness Mobile<span class="text-red">*</span></label>
                                        <div class="input-group">
                                            <div class="input-group-addon">
                                                <i class="fa fa-mobile"></i>
                                            </div>
                                            <asp:TextBox ID="TextBox1" runat="server" placeholder="Witness Mobile" class="form-control" autocomplete="off" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="applytour" class="btn btn-block btn-success" runat="server" Text="Save Details" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <table class="table">
                        <tr>
                            <th>S No.</th>
                            <th>Witness Name</th>
                            <th>Witness Mobile</th>
                            <th>Action</th>
                        </tr>
                        <tr>
                            <td>1</td>
                            <td>MR.Name Surname</td>
                            <td>9876543210</td>
                            <td><a class="badge bg-red">Delete</a></td>
                        </tr>
                    </table>
                </div>

            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            Showhide();
        });

        <%--function Showhide() {
            debugger;
            var FromDate = "";
            var ToDate = "";
            var e = document.getElementById("ddlTourTpye");
            var strUser = e.options[e.selectedIndex].value;
            var RadioValue
            FromDate = document.getElementById('<%=txtFromDate.ClientID%>').value.trim();
            ToDate = document.getElementById('<%=txtToDate.ClientID%>').value.trim();
            $("div[id*=divTourDay]").hide();
            if (strUser == 2) {
                if (FromDate == ToDate) {
                    $("div[id*=divTourDay]").show();
                }
            }
            else {
                $("div[id*=divTourDay]").hide();
            }
            if (strUser == 3) {
                document.getElementById("spnAsterisk").style.display = 'block';
                //spanid.show();

            }
            else {
                document.getElementById("spnAsterisk").style.display = 'none';
                //spanid.hide();
            }
        }


        function Radiobutton() {
            var TourDays = "";
            TourDays = $('input[name=RbTourDays]:checked').text;
            if (TourDays == "Half Day") {

            }
        }--%>
        function validateTourForm() {
            var msg = "";
            var TourType = "";
            <%--if (document.getElementById('<%=txtFromDate.ClientID%>').value.trim() == "") {
                msg = msg + "कृपया दिनांक से चुने | \n";
            }--%>

            if (msg != "") {
                alert(msg);
                return false
            }
            else {
                if (confirm("Do you really want to apply tour.?")) {
                    return true;
                }
                else {
                    return false;
                }
            }

        }
        //$('#txtFromDate').change(function () {
        //    debugger;
        //    var start = $('#txtFromDate').datepicker('getDate');
        //    var end = $('#txtToDate').datepicker('getDate');

        //    if ($('#txtToDate').val() != "") {
        //        if (start > end) {

        //            if ($('#txtFromDate').val() != "") {
        //                alert("From date should not be greater than To Date.");
        //                $('#txtFromDate').val("");
        //            }
        //        }
        //    }
        //    Showhide();
        //});
        //$('#txtToDate').change(function () {
        //    debugger;
        //    var start = $('#txtFromDate').datepicker('getDate');
        //    var end = $('#txtToDate').datepicker('getDate');

        //    if (start > end) {

        //        if ($('#txtToDate').val() != "") {
        //            alert("To Date can not be less than From Date.");
        //            $('#txtToDate').val("");
        //        }
        //    }
        //    Showhide();
        //});

        function UploadControlValidationForLenthAndFileFormat(maxLengthFileName, validFileFormaString, that) {
            //ex---------------
            //maxLengthFileName=50;
            //validFileFormaString=JPG*JPEG*PDF*DOCX
            //uploadControlId=upSaveBill
            //ex---------------
            var msg = '';
            if (document.getElementById(that.id).value != '') {
                var size = document.getElementById(that.id);

                var fileName = document.getElementById(that.id).value;
                var lengthFileName = parseInt(document.getElementById(that.id).value.length)

                var fileExtacntionArray = new Array();
                fileExtacntionArray = fileName.split('.');

                if (fileExtacntionArray.length == 2) {

                    var fileExtacntion = fileExtacntionArray[fileExtacntionArray.length - 1];


                    if (lengthFileName >= parseInt(maxLengthFileName) + parseInt(1)) {
                        msg += '- File name should be less than ' + maxLengthFileName + ' characters. \n';
                    }
                    for (i = 0; i <= (fileName.length - 1) ; i++) {
                        var charFileName = '';

                        charFileName = fileName.substring(i, i + 1);

                        if ((charFileName == '~') || (charFileName == '!') || (charFileName == '@') || (charFileName == '#') || (charFileName == '$') || (charFileName == '%') || (charFileName == '&') || (charFileName == '*') || (charFileName == '{') || (charFileName == '}') || (charFileName == '|') || (charFileName == '<') || (charFileName == '>') || (charFileName == '?')) {

                            msg += '- Special character not allowed in file name. \n';
                            break;
                        }

                    }
                    var isFileFormatCorrect = false;
                    var strValidFormates = '';

                    if (validFileFormaString != "") {

                        var fileFormatArray = new Array();
                        fileFormatArray = validFileFormaString.split('*');

                        for (var j = 0; j < fileFormatArray.length; j++) {
                            if (fileFormatArray[j].toUpperCase() == fileExtacntion.toUpperCase()) {
                                isFileFormatCorrect = true;
                            }

                            if (j == fileFormatArray.length - 1) {
                                strValidFormates += '.' + fileFormatArray[j].toLowerCase();

                            }
                            else {
                                strValidFormates += '.' + fileFormatArray[j].toLowerCase() + '/';

                            }
                        }

                        if (isFileFormatCorrect == false) {
                            msg += '- File format is not correct (only ' + strValidFormates + ').\n';
                        }
                    }

                }
                else {
                    msg += '- File name is incorrect';
                }
                if (msg != '') {
                    document.getElementById(that.id).value = "";
                    alert(msg);
                    return false;
                }
                else {
                    return true;
                }

            }
        }
        function ValidateFileSize(a) {

            var uploadcontrol = document.getElementById(a.id);
            if (uploadcontrol.files[0].size > 2097152) {
                alert('File size should not greater than 2 mb.');
                document.getElementById(a.id).value = '';
                return false;
            }
            else {
                return true;
            }

        }
    </script>
</asp:Content>

