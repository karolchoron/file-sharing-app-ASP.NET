using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using Lab4.Models;
using System.IO;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Lab4.Account
{
    public partial class Manage : System.Web.UI.Page
    {

        protected string SuccessMessage
        {
            get;
            private set;
        }

        private bool HasPassword(ApplicationUserManager manager)
        {
            return manager.HasPassword(User.Identity.GetUserId());
        }

        public bool HasPhoneNumber { get; private set; }

        public bool TwoFactorEnabled { get; private set; }

        public bool TwoFactorBrowserRemembered { get; private set; }

        public int LoginsCount { get; set; }

        protected void Page_Load()
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            HasPhoneNumber = String.IsNullOrEmpty(manager.GetPhoneNumber(User.Identity.GetUserId()));

            // Enable this after setting up two-factor authentientication
            //PhoneNumber.Text = manager.GetPhoneNumber(User.Identity.GetUserId()) ?? String.Empty;

            TwoFactorEnabled = manager.GetTwoFactorEnabled(User.Identity.GetUserId());

            LoginsCount = manager.GetLogins(User.Identity.GetUserId()).Count;

            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

            if (!IsPostBack)
            {
                // Determine the sections to render
                if (HasPassword(manager))
                {
                    ChangePassword.Visible = true;
                }
                else
                {
                    CreatePassword.Visible = true;
                    ChangePassword.Visible = false;
                }

                // Render success message
                var message = Request.QueryString["m"];
                if (message != null)
                {
                    // Strip the query string from action
                    Form.Action = ResolveUrl("~/Account/Manage");

                    SuccessMessage =
                        message == "ChangePwdSuccess" ? "Your password has been changed."
                        : message == "SetPwdSuccess" ? "Your password has been set."
                        : message == "RemoveLoginSuccess" ? "The account was removed."
                        : message == "AddPhoneNumberSuccess" ? "Phone number has been added"
                        : message == "RemovePhoneNumberSuccess" ? "Phone number was removed"
                        : String.Empty;
                    successMessage.Visible = !String.IsNullOrEmpty(SuccessMessage);
                }
            }


            
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        // Remove phonenumber from user
        protected void RemovePhone_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var result = manager.SetPhoneNumber(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return;
            }
            var user = manager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                Response.Redirect("/Account/Manage?m=RemovePhoneNumberSuccess");
            }
        }

        // DisableTwoFactorAuthentication
        protected void TwoFactorDisable_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            manager.SetTwoFactorEnabled(User.Identity.GetUserId(), false);

            Response.Redirect("/Account/Manage");
        }

        //EnableTwoFactorAuthentication 
        protected void TwoFactorEnable_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            manager.SetTwoFactorEnabled(User.Identity.GetUserId(), true);

            Response.Redirect("/Account/Manage");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            //pobranie informacji o tym ktory uzytkowik jest aktualnie zalogowany
            //jest to zrobione dzieki authentication mode="Forms" w Webconfig
            string nazwaZalogowanegoUzytkownika = User.Identity.Name;

            

            //otwarcie polaczenia z baza
            string connectionString = SqlDataSource1.ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string zapytanie = "SELECT [LiczbaPlikow] FROM [AspNetUsers] WHERE [Email] = '" + nazwaZalogowanegoUzytkownika + "'";
            SqlCommand zapytanieIlePolozenie = new SqlCommand(zapytanie, connection);

            var liczba = zapytanieIlePolozenie.ExecuteScalar().ToString();
            Int32.TryParse(liczba, out int liczbaPlikowNaSerwerze);

            LabelIloscPlikow.Text = "Liczba plików wgranych przez Ciebie: " + liczbaPlikowNaSerwerze.ToString();
            //Label1.Text = "Liczba plików wgranych przez Ciebie: " + liczbaPlikowNaSerwerze.ToString();




            // Sprawdzenie, czy plik został wybrany
            if (FileUploadKontrolka.HasFile)
            {
                // Sprawdzenie maksymalnej wielkości pliku
                if (FileUploadKontrolka.PostedFile.ContentLength <= 3000)
                {
                    // Sprawdzenie ilości plików znajdujących się w folderze
                    //string[] files = Directory.GetFiles(Server.MapPath("~/uploads/"));




                    string sprawdzenie = "SELECT [Id] FROM [AspNetUsers] WHERE [UserName] = '" + nazwaZalogowanegoUzytkownika + "'";
                    SqlCommand zapytanieSprawdzenie = new SqlCommand(sprawdzenie, connection);
                    string idUsera = zapytanieSprawdzenie.ExecuteScalar().ToString();

                    
                    if (liczbaPlikowNaSerwerze < 4)
                    {
                        // Sprawdzenie czy plik o tej samej nazwie już istnieje
                        string fileName = Path.GetFileName(FileUploadKontrolka.FileName);
                        if (!File.Exists(Server.MapPath("~/uploads/") + fileName))
                        {
                            // zapisanie pliku
                            FileUploadKontrolka.SaveAs(Server.MapPath("~/uploads/") + fileName);
                            UploadStatusLabel.Text = "Plik został przesłany pomyślnie.";

                            int nowaLiczbaPlikowNaSerwerze = liczbaPlikowNaSerwerze + 1;
                            String zapytanieUpdate = "UPDATE [AspNetUsers] SET [LiczbaPlikow] = " + nowaLiczbaPlikowNaSerwerze + " WHERE [Email] = '" + nazwaZalogowanegoUzytkownika + "'";
                            SqlCommand SQL_Command = new SqlCommand(zapytanieUpdate, connection);
                            SQL_Command.ExecuteNonQuery();


                            string sprawdzenieOstatnieID = "SELECT MAX(IdPliku) FROM [Pliki]";
                            SqlCommand zapytanieOstatnieID = new SqlCommand(sprawdzenieOstatnieID, connection);
                            string ostatnieId = zapytanieOstatnieID.ExecuteScalar().ToString();

                            int idPliku;
                            
                            if (String.IsNullOrEmpty(ostatnieId))
                            {
                                idPliku = 1;
                            }
                            else
                            {
                                int x = Int32.Parse(ostatnieId);
                                idPliku = x + 1;
                            }

                            String dodawanieDoBazy = "INSERT INTO [Pliki] VALUES ('"+ idPliku + "', '" + idUsera+"', '"+fileName+ "')";
                            SqlCommand Dodanie = new SqlCommand(dodawanieDoBazy, connection);
                            Dodanie.ExecuteNonQuery();
                            
                            LabelIloscPlikow.Text = "Liczba plików wgranych przez Ciebie: " + nowaLiczbaPlikowNaSerwerze.ToString();


                        }
                        else
                        {
                            UploadStatusLabel.Text = "Plik o tej samej nazwie już istnieje!";
                        }
                    }
                    else
                    {
                        UploadStatusLabel.Text = "Osiągnięto maksymalną liczbę plików (4).";
                    }
                }
                else
                {
                    UploadStatusLabel.Text = "Maksymalna wielkość pliku to 3 KB.";
                }
            }
            else
            {
                UploadStatusLabel.Text = "Nie wybrano pliku.";
            }

            connection.Close();

        }


    }
}