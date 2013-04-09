using System;
using System.Windows.Forms;
using Cryptography;
using Microsoft.Web.Services3.Security.Tokens;
using TestingWebService.BMISSOService1;

//////////////////////////////////////////////////////////////////
// 
// How to use the BMI Membership Web Service in your application:
//
// Step 1- Download and Install Web Service Enhancement 3 from 
// Microsoft Website:
// ( http://www.microsoft.com/downloads/details.aspx?FamilyID=018a09fd-3a74-43c5-8ec1-8d789091255d )
//
// Step 2- Add a reference to the webservice.
//
// Step 3- Right-Click on your website or application and click on
// WSE Settings 3.0
// Step 4- On 'General' Tab click the first checkbox. Then Click OK
//
// Step 5- Create an object from the webservice class, but not the normal class.
// Notice there is a class with the 'Wse' in the end. That is the class you
// are going to use.
//
// Step 6- Create a UsernameToken object from Microsoft.Web.Services3.Security.Tokens
// namespace.
//
// Step 7- Set the username and password of that object.
//
// Step 8- Pass the UsernameToken Object to SoapContext.
//
// Step 9- You are ready to use the WebService methods.
//
// Below is an exmaple of how you acheive this goal.
namespace TestingWebService
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
            Capture = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // NOTE: Web Service Enhancement Version
                BMISSOServiceWse a = new BMISSOServiceWse(); // Notice the 'Wse' postfix at the end!

                a.RequestSoapContext.Security.Tokens.Add(
                    new UsernameToken("jafarzadeh"
                                      , CryptographyClass.CalculateHash("123", CryptographyClass.HashMode.SHA1)
                                      , PasswordOption.SendHashed));

                UserProfile test = a.Authenticate(textBoxuser.Text, textBoxpass.Text);
                propertyGridShort.SelectedObject = test;

                UserProfile up = a.GetUserInfo(textBoxuser.Text);
                propertyGridLong.SelectedObject = up;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}