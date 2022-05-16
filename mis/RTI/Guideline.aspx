<%@ Page Title="" Language="C#" MasterPageFile="~/mis/RTIMaster.master" AutoEventWireup="true" CodeFile="Guideline.aspx.cs" Inherits="RTI_Guideline" %>

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
                <div class="col-md-12">
                    <!-- general form elements -->
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="HEnglish" runat="server">GUIDELINES FOR USE OF RTI ONLINE PORTAL</h3>
                            <h3 class="box-title" id="HHindi" runat="server">आरटीआई ऑनलाइन पोर्टल के उपयोग के लिए दिशानिर्देश</h3>
                            <asp:Label ID="lblMsg" runat="server" Text="" Visible="true"></asp:Label>
                            <asp:RadioButtonList ID="rbtnLanguage" runat="server" CssClass=" pull-right" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtnLanguage_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Selected="True">Hindi</asp:ListItem>
                 <asp:ListItem>English</asp:ListItem>
             </asp:RadioButtonList>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
            <div class="clo-md-12">
              <div class="box-body" runat="server" id="EnglishContent">
               
				<div id="content">

<ol class="para" style="
    line-height:  25px;
">
<li class="line">
This Web Portal can be used by Indian citizens to file RTI application online and also to make payment for RTI application online. First appeal can also be filed online.</li>

<li class="line">An applicant who desires to obtain any information under the RTI Act can make a request through this Web Portal to the Ministries/Departments of Government of India.</li>
 
<li class="line">On clicking at "Submit Request", the applicant has to fill the required details on the page that will appear. <br> The fields marked <samp style="color:#FF0000"><b>*</b></samp> are mandatory while the others are optional.</li>

<li class="line">The text of the application may be written at the prescribed column.</li>

<li class="line">At present, the text of an application that can be uploaded at the prescribed column is confined to 3000 characters only.</li>

<li class="line">In case an application contains more than 3000 characters, it can be uploaded as an attachment, by using column <br>"Supporting document".</li>

<li class="line">After filling the first page, the applicant has to click on "Make Payment" to make payment of the prescribed fee.</li>

<li class="line">The applicant can pay the prescribed fee through the following modes:<br>
(a)	Internet banking through SBI and its associated banks;<br>
(b)	Using credit/debit card of Master/Visa;<br>
(c)	Using RuPay Card.</li>

<li class="line">Fee for making an application is as prescribed in the RTI Rules, 2012.</li>
<li class="line">After making payment, an application can be submitted.</li>

<li class="line">No RTI fee is required to be paid by any citizen who is below poverty line as per RTI Rules, 2012. However, the applicant must attach a copy of the certificate issued by the appropriate government in this regard, alongwith the application.</li>

<li class="line">On submission of an application, a unique registration number would be issued, which may be referred by the applicant for any references in future.</li>

<li class="line">The application filed through this Web Portal would reach electronically to the "Nodal Officer" of concerned Ministry/Department, who would transmit the RTI application electronically to the concerned CPIO.</li>

<li class="line">In case additional fee is required representing the cost for providing information, the CPIO would intimate the applicant through this portal. This intimation can be seen by the applicant through Status Report or through his/her e-mail alert.</li>

<li class="line">For making an appeal to the first Appellate Authority, the applicant has to click at "Submit First Appeal" and fill up the page that will appear.</li>

<li class="line">The registration number of original application has to be used for reference.</li>

<li class="line">As per RTI Act, no fee has to be paid for first appeal.</li>

<li class="line">The applicant/the appellant should submit his/her mobile number to receive SMS alert.</li>

<li class="line">Status of the RTI application/first appeal filed online can be seen by the applicant/appellant by clicking at "View Status".
</li>

<li class="line">All the requirements for filing an RTI application and first appeal as well as other provisions regarding time limit, exemptions etc., as provided in the RTI Act, 2005 will continue to apply.</li>

</ol>
</div>
<p  style="color: #CC0000;"><%--<asp:CheckBox ID="chkEnglish" runat="server" ClientIDMode="Static" Text="I have read and understood the above guidelines." />--%>
 	 <input type="checkbox" id="chk1" class="cbox" />I have read and understood the above guidelines.</p>
	
			</div>

                   <%--Hindi Content--%>
                <div class="box-body" runat="server" id="HindiContent">
               
								<div id="content1">

<ol class="para" style="
    line-height:  25px;
">
<li class="line">भारतीय वेब नागरिकों द्वारा आरटीआई आवेदन ऑनलाइन दर्ज करने और ऑनलाइन आरटीआई आवेदन के लिए भुगतान करने के लिए इस वेब पोर्टल का उपयोग किया जा सकता है। पहली अपील भी ऑनलाइन दायर की जा सकती है।.</li>

<li class="line">एक आवेदक जो आरटीआई अधिनियम के तहत कोई जानकारी प्राप्त करना चाहता है, इस वेब पोर्टल के माध्यम से भारत सरकार के मंत्रालयों / विभागों के लिए अनुरोध कर सकता है।.</li>
 
<li class="line">."सबमिट अनुरोध" पर क्लिक करने पर, आवेदक को उस पृष्ठ पर आवश्यक विवरण भरना होगा जो दिखाई देगा। <br> चिन्हित फ़ील्ड * अनिवार्य हैं जबकि अन्य वैकल्पिक हैं।</li>

<li class="line">आवेदन का पाठ निर्धारित कॉलम पर लिखा जा सकता है।</li>

<li class="line">वर्तमान में, निर्धारित कॉलम पर अपलोड किए जा सकने वाले एप्लिकेशन का टेक्स्ट केवल 3000 अक्षरों तक ही सीमित है।.</li>

<li class="line">.यदि किसी एप्लिकेशन में 3000 से अधिक वर्ण होते हैं, तो इसे कॉलम का उपयोग करके अनुलग्नक के रूप में अपलोड किया जा सकता है "समर्थन दस्तावेज"।</li>

<li class="line">पहले पृष्ठ को भरने के बाद, आवेदक को निर्धारित शुल्क का भुगतान करने के लिए "भुगतान करें" पर क्लिक करना होगा।</li>

<li class="line">आवेदक निम्नलिखित मानों के माध्यम से निर्धारित शुल्क का भुगतान कर सकता है:<br>
(a)	एसबीआई और उसके संबंधित बैंकों के माध्यम से इंटरनेट बैंकिंग;<br>
(b)	मास्टर / वीजा के क्रेडिट / डेबिट कार्ड का उपयोग करना;<br>
(c) रुपे कार्ड का उपयोग करना</li>

<li class="line">आवेदन करने के लिए शुल्क आरटीआई नियम, 2012 में निर्धारित है।</li>
<li class="line">भुगतान करने के बाद, एक आवेदन जमा किया जा सकता है।</li>

<li class="line">आरटीआई नियम, 2012 के अनुसार गरीबी रेखा से नीचे किसी भी नागरिक द्वारा कोई आरटीआई शुल्क का भुगतान करने की आवश्यकता नहीं है। हालांकि, आवेदक को इस संबंध में उपयुक्त सरकार द्वारा जारी किए गए प्रमाण पत्र की एक प्रति संलग्न करनी होगी।</li>

<li class="line">आवेदन जमा करने पर, एक अद्वितीय पंजीकरण संख्या जारी की जाएगी, जिसे भविष्य में किसी भी संदर्भ के लिए आवेदक द्वारा संदर्भित किया जा सकता है।</li>

<li class="line">इस वेब पोर्टल के माध्यम से दायर आवेदन संबंधित मंत्रालय / विभाग के "नोडल अधिकारी" के लिए इलेक्ट्रॉनिक रूप से पहुंच जाएगा, जो संबंधित सीपीआईओ को इलेक्ट्रॉनिक रूप से आरटीआई आवेदन भेज देगा।</li>

<li class="line">यदि जानकारी प्रदान करने के लिए लागत का प्रतिनिधित्व करने के लिए अतिरिक्त शुल्क की आवश्यकता होती है, तो सीपीआईओ इस पोर्टल के माध्यम से आवेदक को अंतरंग करेगा। यह सूचना आवेदक द्वारा स्टेटस रिपोर्ट के माध्यम से या उसके ई-मेल अलर्ट के माध्यम से देखी जा सकती है।</li>

<li class="line">पहली अपीलीय प्राधिकारी को अपील करने के लिए, आवेदक को "पहली अपील सबमिट करें" पर क्लिक करना होगा और उस पृष्ठ को भरना होगा जो दिखाई देगा।</li>

<li class="line">संदर्भ के लिए मूल आवेदन की पंजीकरण संख्या का उपयोग किया जाना चाहिए।</li>

<li class="line">आरटीआई अधिनियम के अनुसार, पहली अपील के लिए कोई शुल्क नहीं देना पड़ता है।</li>

<li class="line">आवेदक / अपीलकर्ता को एसएमएस अलर्ट प्राप्त करने के लिए अपना मोबाइल नंबर जमा करना चाहिए।</li>

<li class="line">.ऑनलाइन देखे गए आरटीआई आवेदन / पहली अपील की स्थिति आवेदक / अपीलकर्ता द्वारा "स्थिति देखें" पर क्लिक करके देखी जा सकती है।
</li>

<li class="line">आरटीआई अधिनियम, 2005 में प्रदान की गई आरटीआई आवेदन और पहली अपील के साथ-साथ समय सीमा, छूट इत्यादि के संबंध में अन्य प्रावधानों के लिए सभी आवश्यकताओं को लागू करना जारी रहेगा।</li>

</ol>
</div>
<p  style="color: #CC0000;"> <input type="checkbox" id="chk2" class="cbox"  />
	मैंने उपरोक्त दिशानिर्देशों को पढ़ और समझ लिया है।</p>
	
			</div>

              <!-- /.box-body -->
              <div class="box-footer" style="text-align:  center;">
                <a href="online_request.php">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" Text="Next" OnClientClick="return CheckConfirmation()" OnClick="btnSubmit_Click" />
                   <%-- <button type="submit" class="btn btn-info" onclick="return CheckConfirmation()">Submit</button>--%></a>
                <button type="submit" class="btn btn-default">Clear</button>
              </div>
              <!-- /.box-footer -->
         
			</div>
          </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->
                </div>

            </div>
            <!-- /.row -->
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
     <script>
        
         function CheckConfirmation() {
             debugger
            // var value = $('chk1').val;
             var Echk = document.getElementById('chk1');  //document.getElementById('chk1').checked;  // $('chk1').checked == true
             var Hchk = document.getElementById('chk2');     //document.getElementById('chk2').checked;  //$('chk2').checked == true
             if (Echk != null)
             {
                 if (Echk.checked == false) {
                     alert("Please check checkbox");
                     return false;
                 }
                 else {
                     return true;
                 }
             }
             if (Hchk != null)
             {
                 if (Hchk.checked == false) {
                     alert("Please check checkbox");
                     return false;
                 }
                 else {
                     return true;
                 }
             }
             
         }


    </script>
</asp:Content>

