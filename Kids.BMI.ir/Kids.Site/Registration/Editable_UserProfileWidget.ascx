<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="Editable_UserProfileWidget.ascx.cs"
    Inherits="Site.Kids.bmi.ir.Registration.Editable_UserProfileWidget" %>
<%@ Register Src="../UserControls/ucDatePicker.ascx" TagName="ucDatePicker" TagPrefix="uc1" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.7.123, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="uc1" TagName="KidsUserSateWidget" Src="~/Registration/KidsUserSateWidget.ascx" %>
<table width="100%">
    <tr>
        <td>
            <fieldset>
                <legend>اطلاعات کودک</legend>
                <table style="width: 100%;">
                    <tr>
                        <td align="right">
                            وضعیت عضویت :
                        </td>
                        <td colspan="4" align="right">
                            <uc1:KidsUserSateWidget ID="ucKidsUserSateWidget" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            نام کاربری کودک:
                        </td>
                        <td align="right" colspan="3">
                            <asp:Label ID="lblSSOUserName" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            کد معرف :
                        </td>
                        <td align="right" colspan="3">
                            <asp:Label ID="txtIntruducerId" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            وضعیت :
                        </td>
                        <td align="right" colspan="3">
                            <asp:DropDownList ID="drpUserState" runat="server" Width="350px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="5">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            شماره حساب آرزو :
                        </td>
                        <td>
                            <asp:Label ID="lblWishAccountnumber" runat="server"></asp:Label>
                        </td>
                        <td>
                            مبلغ مانده حساب آرزو :
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblWishAccountRemain" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            کد شعبه افتتاح حساب
                        </td>
                        <td align="right">
                            <asp:Label ID="lblWishAccountBranch" runat="server"></asp:Label>
                        </td>
                        <td width="120" align="left" colspan="2">
                           
                        </td>
                        <td align="right">
                            
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="5">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="159">
                            نام کودک:
                        </td>
                        <td align="right">
                            <asp:TextBox ID="txtChildName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtChildName"
                                Display="Dynamic" ErrorMessage="!" ForeColor="Red" SetFocusOnError="True" ValidationGroup="Save"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            نام خانوادگی کودک :
                        </td>
                        <td align="right">
                            <asp:TextBox ID="txtChildFamily" runat="server"></asp:TextBox>
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" Display="Dynamic"
                                ErrorMessage="!" ForeColor="Red" SetFocusOnError="True" ControlToValidate="txtChildFamily"
                                ValidationGroup="Save"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            جنسیت :
                        </td>
                        <td align="right">
                            <asp:DropDownList ID="drpSex" runat="server">
                                <asp:ListItem Value="-1">---</asp:ListItem>
                                <asp:ListItem Value="False">دختر</asp:ListItem>
                                <asp:ListItem Value="True">پسر</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" Display="Dynamic"
                                ErrorMessage="!" ForeColor="Red" SetFocusOnError="True" ControlToValidate="drpSex"
                                ValidationGroup="Save" InitialValue="-1"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            نام پدر :
                        </td>
                        <td align="right">
                            <asp:TextBox ID="txtChildFatherName" runat="server"></asp:TextBox>
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" Display="Dynamic"
                                ErrorMessage="!" ForeColor="Red" SetFocusOnError="True" ControlToValidate="txtChildFatherName"
                                ValidationGroup="Save"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            محل تولد :
                        </td>
                        <td align="right">
                            <asp:TextBox ID="txtChildBirthLocation" runat="server"></asp:TextBox>
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" Display="Dynamic"
                                ErrorMessage="!" ForeColor="Red" SetFocusOnError="True" ControlToValidate="txtChildBirthLocation"
                                ValidationGroup="Save" Enabled="False"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            شماره شناسنامه :&nbsp;
                        </td>
                        <td align="right">
                            <asp:TextBox ID="txtChildIdentityNo" runat="server"></asp:TextBox>
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtChildIdentityNo"
                                Display="Dynamic" ErrorMessage="!" ForeColor="Red" SetFocusOnError="True" ValidationGroup="Save"></asp:RequiredFieldValidator>
                            &nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtChildIdentityNo"
                                Display="Dynamic" ErrorMessage="مقدار نامعتبر است" ForeColor="Red" Operator="DataTypeCheck"
                                SetFocusOnError="True" Type="Integer"></asp:CompareValidator>
                        </td>
                        <td>
                            کدملی :
                        </td>
                        <td align="right">
                            <asp:TextBox ID="txtChildMelliCode" runat="server"></asp:TextBox>
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtChildMelliCode"
                                Display="Dynamic" ErrorMessage="!" ForeColor="Red" SetFocusOnError="True" ValidationGroup="Save"></asp:RequiredFieldValidator>
                            &nbsp;<asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtChildMelliCode"
                                Display="Dynamic" ErrorMessage="مقدار نامعتبر است" ForeColor="Red" Operator="DataTypeCheck"
                                SetFocusOnError="True" Type="Double"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            سریال شناسنامه :
                        </td>
                        <td colspan="3" align="right">
                            <asp:TextBox ID="txtChildIdentitySerial3" runat="server" MaxLength="6" Width="78px"></asp:TextBox>
                            -<asp:DropDownList ID="drpChildIdentitySerial2" runat="server">
                                <asp:ListItem Value="">---</asp:ListItem>
                                <asp:ListItem>01</asp:ListItem>
                                <asp:ListItem>02</asp:ListItem>
                                <asp:ListItem>03</asp:ListItem>
                                <asp:ListItem>04</asp:ListItem>
                                <asp:ListItem>05</asp:ListItem>
                                <asp:ListItem>06</asp:ListItem>
                                <asp:ListItem>07</asp:ListItem>
                                <asp:ListItem>08</asp:ListItem>
                                <asp:ListItem>09</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                                <asp:ListItem>24</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>26</asp:ListItem>
                                <asp:ListItem>27</asp:ListItem>
                                <asp:ListItem>28</asp:ListItem>
                                <asp:ListItem>29</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>31</asp:ListItem>
                                <asp:ListItem>32</asp:ListItem>
                                <asp:ListItem>33</asp:ListItem>
                                <asp:ListItem>34</asp:ListItem>
                                <asp:ListItem>35</asp:ListItem>
                                <asp:ListItem>36</asp:ListItem>
                                <asp:ListItem>37</asp:ListItem>
                                <asp:ListItem>38</asp:ListItem>
                                <asp:ListItem>39</asp:ListItem>
                                <asp:ListItem>40</asp:ListItem>
                                <asp:ListItem>41</asp:ListItem>
                                <asp:ListItem>42</asp:ListItem>
                                <asp:ListItem>43</asp:ListItem>
                                <asp:ListItem>44</asp:ListItem>
                                <asp:ListItem>45</asp:ListItem>
                                <asp:ListItem>46</asp:ListItem>
                                <asp:ListItem>47</asp:ListItem>
                                <asp:ListItem>47</asp:ListItem>
                                <asp:ListItem>49</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>51</asp:ListItem>
                                <asp:ListItem>52</asp:ListItem>
                                <asp:ListItem>53</asp:ListItem>
                                <asp:ListItem>54</asp:ListItem>
                                <asp:ListItem>55</asp:ListItem>
                                <asp:ListItem>56</asp:ListItem>
                                <asp:ListItem>57</asp:ListItem>
                                <asp:ListItem>58</asp:ListItem>
                                <asp:ListItem>59</asp:ListItem>
                                <asp:ListItem>60</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;/
                            <asp:DropDownList ID="drpChildIdentitySerial1" runat="server">
                                <asp:ListItem Value="">---</asp:ListItem>
                                <asp:ListItem>الف</asp:ListItem>
                                <asp:ListItem>ب</asp:ListItem>
                                <asp:ListItem>پ</asp:ListItem>
                                <asp:ListItem>چ</asp:ListItem>
                                <asp:ListItem>ج</asp:ListItem>
                                <asp:ListItem>ح</asp:ListItem>
                                <asp:ListItem>خ</asp:ListItem>
                                <asp:ListItem>ه</asp:ListItem>
                                <asp:ListItem>ع</asp:ListItem>
                                <asp:ListItem>غ</asp:ListItem>
                                <asp:ListItem>ف</asp:ListItem>
                                <asp:ListItem>ق</asp:ListItem>
                                <asp:ListItem>ث</asp:ListItem>
                                <asp:ListItem>ص</asp:ListItem>
                                <asp:ListItem>ض</asp:ListItem>
                                <asp:ListItem>گ</asp:ListItem>
                                <asp:ListItem>ک</asp:ListItem>
                                <asp:ListItem>م</asp:ListItem>
                                <asp:ListItem>ن</asp:ListItem>
                                <asp:ListItem>ت</asp:ListItem>
                                <asp:ListItem>ا</asp:ListItem>
                                <asp:ListItem>ل</asp:ListItem>
                                <asp:ListItem>ب</asp:ListItem>
                                <asp:ListItem>ی</asp:ListItem>
                                <asp:ListItem>س</asp:ListItem>
                                <asp:ListItem>ش</asp:ListItem>
                                <asp:ListItem>د</asp:ListItem>
                                <asp:ListItem>ذ</asp:ListItem>
                                <asp:ListItem>ر</asp:ListItem>
                                <asp:ListItem>ز</asp:ListItem>
                                <asp:ListItem>ط</asp:ListItem>
                                <asp:ListItem>ظ</asp:ListItem>
                                <asp:ListItem>ژ</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" Display="Dynamic"
                                ErrorMessage="!" ForeColor="Red" SetFocusOnError="True" ControlToValidate="txtChildIdentitySerial3"
                                ValidationGroup="Save" Enabled="False"></asp:RequiredFieldValidator>
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" Display="Dynamic"
                                ErrorMessage="!" ForeColor="Red" SetFocusOnError="True" ValidationGroup="Save"
                                ControlToValidate="drpChildIdentitySerial2" InitialValue="" Enabled="False"></asp:RequiredFieldValidator>
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" Display="Dynamic"
                                ErrorMessage="!" ForeColor="Red" SetFocusOnError="True" ValidationGroup="Save"
                                ControlToValidate="drpChildIdentitySerial1" InitialValue="" Enabled="False"></asp:RequiredFieldValidator>
                            &nbsp;<asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtChildIdentitySerial3"
                                Display="Dynamic" ErrorMessage="مقدار نامعتبر است" ForeColor="Red" Operator="DataTypeCheck"
                                SetFocusOnError="True" Type="Integer"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            تاریخ تولد :
                        </td>
                        <td colspan="3" align="right">
                            <uc1:ucDatePicker ID="ucBirthDate" runat="server" ShowTime="False" IsRequired="True"
                                Enabled="True" ValidationGroup="Save" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            شماره حساب آرزو :
                        </td>
                        <td colspan="3" align="right">
                            <asp:TextBox ID="txtChildAccNo" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            کد شعبه حساب آرزو :
                        </td>
                        <td colspan="3" align="right">
                            <asp:TextBox ID="txtChildAccBranchNo" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            شماره تماس کودک :
                        </td>
                        <td align="right">
                            <asp:TextBox ID="txtChildPhoneNumber2" runat="server"></asp:TextBox>-
                            <asp:TextBox ID="txtChildPhoneNumber1" runat="server" Width="39px"></asp:TextBox>
                            (021-22448833)
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" Display="Dynamic"
                                ErrorMessage="!" ForeColor="Red" SetFocusOnError="True" ControlToValidate="txtChildPhoneNumber1"
                                ValidationGroup="Save" Enabled="False"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" Display="Dynamic"
                                ErrorMessage="!" ForeColor="Red" SetFocusOnError="True" ControlToValidate="txtChildPhoneNumber2"
                                ValidationGroup="Save" Enabled="False"></asp:RequiredFieldValidator>
                            &nbsp;<asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtChildPhoneNumber1"
                                Display="Dynamic" ErrorMessage="مقدار نامعتبر است" ForeColor="Red" Operator="DataTypeCheck"
                                SetFocusOnError="True" Type="Double"></asp:CompareValidator>
                            &nbsp;<asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="txtChildPhoneNumber2"
                                Display="Dynamic" ErrorMessage="مقدار نامعتبر است" ForeColor="Red" Operator="DataTypeCheck"
                                SetFocusOnError="True" Type="Double"></asp:CompareValidator>
                        </td>
                        <td>
                            &nbsp; شماره تلفن همراه کودک :
                        </td>
                        <td align="right">
                            <asp:TextBox ID="txtChildMobileNumber" runat="server"></asp:TextBox>
                            &nbsp;(09123456789)
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" Display="Dynamic"
                                ErrorMessage="!" ForeColor="Red" SetFocusOnError="True" ControlToValidate="txtChildMobileNumber"
                                ValidationGroup="Save" Enabled="False"></asp:RequiredFieldValidator>
                            &nbsp;<asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="txtChildMobileNumber"
                                Display="Dynamic" ErrorMessage="مقدار نامعتبر است" ForeColor="Red" Operator="DataTypeCheck"
                                SetFocusOnError="True" Type="Double"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            آدرس ایمیل کودک(معتبر) :
                        </td>
                        <td align="right">
                            <asp:TextBox ID="txtChildEmailAddress" runat="server"></asp:TextBox>
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" Display="Dynamic"
                                ErrorMessage="!" ForeColor="Red" SetFocusOnError="True" ControlToValidate="txtChildEmailAddress"
                                ValidationGroup="Save" Enabled="False"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtChildEmailAddress"
                                Display="Dynamic" ErrorMessage="آدرس آیمیل نامعتبر است" ForeColor="Red" SetFocusOnError="True"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Save"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            کد پستی کودک :
                        </td>
                        <td align="right">
                            <asp:TextBox ID="txtChildPostCode" runat="server"></asp:TextBox>
                            &nbsp;<asp:CompareValidator ID="CompareValidator7" runat="server" ControlToValidate="txtChildPostCode"
                                Display="Dynamic" ErrorMessage="مقدار نامعتبر است" ForeColor="Red" Operator="DataTypeCheck"
                                SetFocusOnError="True" Type="Double"></asp:CompareValidator>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            آدرس پستی محل سکونت کودک :
                        </td>
                        <td colspan="3" align="right">
                            <asp:TextBox ID="txtChildAddress" runat="server" TextMode="MultiLine" Height="40px"
                                Width="400px"></asp:TextBox>
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtChildAddress"
                                Display="Dynamic" ErrorMessage="!" ForeColor="Red" SetFocusOnError="True" ValidationGroup="Save"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="4">
                            <hr /></td>
                    </tr>
                    <tr>
                        <td align="right">
                            تصویر کودک :
                        </td>
                        <td colspan="3" align="right">
                            <asp:AsyncFileUpload UploaderStyle="Modern" ID="fupChildPic" runat="server" OnUploadedComplete="AsyncFileUpload_UploadedComplete"
                                OnUploadedFileError="AsyncFileUpload_UploadedFileError" OnClientUploadError="uploaderror"
                                UploadingBackColor="#66CCFF" ThrobberID="Throbber1" />
                            (حداکثر 500 کیلو بایت)
                            <asp:Label ID="Throbber1" runat="server" Style="display: none">
<img src="/App_Themes/ajax-loader.gif" align="middle" alt="loading" />
                            </asp:Label>
                            <asp:Image ID="ImgChildPic" runat="server" Visible="false" ImageAlign="Middle" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            تصویر صفحه اول شناسنامه :
                        </td>
                        <td colspan="3" align="right">
                            <asp:AsyncFileUpload UploaderStyle="Modern" ID="fupChildIdentityPic" runat="server"
                                OnUploadedComplete="AsyncFileUpload_UploadedComplete" OnUploadedFileError="AsyncFileUpload_UploadedFileError"
                                OnClientUploadError="uploaderror" UploadingBackColor="#66CCFF" ThrobberID="Throbber2" />
                            (حداکثر 500 کیلو بایت)
                            <asp:Label ID="Throbber2" runat="server" Style="display: none">
<img src="/App_Themes/ajax-loader.gif" align="middle" alt="loading" />
                            </asp:Label>
                            <asp:Image ID="ImgChildIdentityPic" runat="server" Visible="false" ImageAlign="Middle" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            تصویر روی کارت ملی :
                        </td>
                        <td colspan="3" align="right">
                            <asp:AsyncFileUpload UploaderStyle="Modern" ID="fupChildNationalCardFaceUp" runat="server"
                                OnUploadedComplete="AsyncFileUpload_UploadedComplete" OnUploadedFileError="AsyncFileUpload_UploadedFileError"
                                OnClientUploadError="uploaderror" UploadingBackColor="#66CCFF" ThrobberID="Throbber3" />
                            (حداکثر 500 کیلو بایت)
                            <asp:Label ID="Throbber3" runat="server" Style="display: none">
<img src="/App_Themes/ajax-loader.gif" align="middle" alt="loading" />
                            </asp:Label>
                            <asp:Image ID="ImgChildNationalCardFaceUp" runat="server" Visible="false" ImageAlign="Middle" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            تصویر پشت کارت ملی :
                        </td>
                        <td colspan="3" align="right">
                            <asp:AsyncFileUpload UploaderStyle="Modern" ID="fupChildNationalCardFaceDown" runat="server"
                                OnUploadedComplete="AsyncFileUpload_UploadedComplete" OnUploadedFileError="AsyncFileUpload_UploadedFileError"
                                OnClientUploadError="uploaderror" UploadingBackColor="#66CCFF" ThrobberID="Throbber4" />
                            (حداکثر 500 کیلو بایت)
                            <asp:Label ID="Throbber4" runat="server" Style="display: none">
<img src="/App_Themes/ajax-loader.gif" align="middle" alt="loading" />
                            </asp:Label>
                            <asp:Image ID="ImgChildNationalCardFaceDown" runat="server" Visible="false" ImageAlign="Middle" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </td>
    </tr>
    <tr>
        <td>
            <fieldset>
                <legend>اطلاعات ولی کودک</legend>
                <table style="width: 100%;">
                    <tr>
                        <td align="right">
                            نام :
                        </td>
                        <td align="right">
                            <asp:TextBox ID="txtParentName" runat="server"></asp:TextBox>
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txtParentName"
                                Display="Dynamic" ErrorMessage="!" ForeColor="Red" SetFocusOnError="True" ValidationGroup="Save"></asp:RequiredFieldValidator>
                        </td>
                        <td align="right" width="120">
                            نام خانوادگی :</td>
                        <td align="right">
                            <asp:TextBox ID="txtParentFamily" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="txtParentFamily"
                                Display="Dynamic" ErrorMessage="!" ForeColor="Red" SetFocusOnError="True" ValidationGroup="Save"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            نسبت با کودک :
                        </td>
                        <td align="right">
                            <asp:DropDownList ID="drpParentRelationId" runat="server">
                            </asp:DropDownList>
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="drpParentRelationId"
                                InitialValue="-1" Display="Dynamic" ErrorMessage="!" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Save"></asp:RequiredFieldValidator>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            کد ملی :
                        </td>
                        <td align="right">
                            <asp:TextBox ID="txtParentMelliCode" runat="server"></asp:TextBox>
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="txtParentMelliCode"
                                Display="Dynamic" ErrorMessage="!" ForeColor="Red" SetFocusOnError="True" ValidationGroup="Save"></asp:RequiredFieldValidator>
                            &nbsp;<asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="txtParentMelliCode"
                                Display="Dynamic" ErrorMessage="مقدار نامعتبر است" ForeColor="Red" Operator="DataTypeCheck"
                                SetFocusOnError="True" Type="Double"></asp:CompareValidator>
                        </td>
                        <td align="right">
                            شماره شناسنامه :</td>
                        <td align="right">
                            <asp:TextBox ID="txtParentIdentityNo" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" Display="Dynamic"
                                ErrorMessage="!" ForeColor="Red" SetFocusOnError="True" ControlToValidate="txtParentIdentityNo"
                                ValidationGroup="Save" Enabled="False"></asp:RequiredFieldValidator>
                            &nbsp;<asp:CompareValidator ID="CompareValidator9" runat="server" ControlToValidate="txtParentIdentityNo"
                                Display="Dynamic" ErrorMessage="مقدار نامعتبر است" ForeColor="Red" Operator="DataTypeCheck"
                                SetFocusOnError="True" Type="Double"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            شماره حساب :
                        </td>
                        <td align="right">
                            <asp:TextBox ID="txtParentAccNo" runat="server"></asp:TextBox>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            آدرس ایمیل (معتبر)
                        </td>
                        <td align="right">
                            <asp:TextBox ID="txtParentEmailAddress" runat="server"></asp:TextBox>
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" Display="Dynamic"
                                ErrorMessage="!" ForeColor="Red" SetFocusOnError="True" ControlToValidate="txtParentEmailAddress"
                                ValidationGroup="Save" Enabled="False"></asp:RequiredFieldValidator>
                            &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                ControlToValidate="txtParentEmailAddress" Display="Dynamic" ErrorMessage="آدرس آیمیل نامعتبر است"
                                ForeColor="Red" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ValidationGroup="Save"></asp:RegularExpressionValidator>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            شماره تماس :
                        </td>
                        <td align="right">
                            <asp:TextBox ID="txtParentPhoneNumber2" runat="server"></asp:TextBox>
                            -
                            <asp:TextBox ID="txtParentPhoneNumber1" runat="server" Width="39px"></asp:TextBox>
                            (021-22448833)
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" Display="Dynamic"
                                ErrorMessage="!" ForeColor="Red" SetFocusOnError="True" ControlToValidate="txtParentPhoneNumber1"
                                ValidationGroup="Save" Enabled="False"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" Display="Dynamic"
                                ErrorMessage="!" ForeColor="Red" SetFocusOnError="True" ControlToValidate="txtParentPhoneNumber2"
                                ValidationGroup="Save" Enabled="False"></asp:RequiredFieldValidator>
                            &nbsp;<asp:CompareValidator ID="CompareValidator10" runat="server" ControlToValidate="txtParentPhoneNumber1"
                                Display="Dynamic" ErrorMessage="مقدار نامعتبر است" ForeColor="Red" Operator="DataTypeCheck"
                                SetFocusOnError="True" Type="Double"></asp:CompareValidator>
                            &nbsp;<asp:CompareValidator ID="CompareValidator12" runat="server" ControlToValidate="txtParentPhoneNumber2"
                                Display="Dynamic" ErrorMessage="مقدار نامعتبر است" ForeColor="Red" Operator="DataTypeCheck"
                                SetFocusOnError="True" Type="Double"></asp:CompareValidator>
                        </td>
                        <td align="right">
                            &nbsp; شماره تلفن همراه :</td>
                        <td align="right">
                            <asp:TextBox ID="txtParentMobileNumber" runat="server"></asp:TextBox>
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" Display="Dynamic"
                                ErrorMessage="!" ForeColor="Red" SetFocusOnError="True" ControlToValidate="txtParentMobileNumber"
                                ValidationGroup="Save" Enabled="False"></asp:RequiredFieldValidator>
                            &nbsp;<asp:CompareValidator ID="CompareValidator11" runat="server" ControlToValidate="txtParentMobileNumber"
                                Display="Dynamic" ErrorMessage="مقدار نامعتبر است" ForeColor="Red" Operator="DataTypeCheck"
                                SetFocusOnError="True" Type="Double"></asp:CompareValidator>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            کد پستی :
                        </td>
                        <td align="right" colspan="3">
                            <asp:TextBox ID="txtParentPostCode" runat="server"></asp:TextBox>
                            &nbsp; &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            آدرس پستی محل سکونت :
                        </td>
                        <td colspan="3" align="right">
                            <asp:TextBox ID="txtParentAddress" runat="server" TextMode="MultiLine" Height="40px"
                                Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </td>
    </tr>
   
</table>
