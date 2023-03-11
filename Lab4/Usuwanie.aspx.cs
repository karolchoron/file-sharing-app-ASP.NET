using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab4
{
    public partial class Contact : Page
    {
        public static string nazwaPlikuDoUsuniecia;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                nazwaPlikuDoUsuniecia = DropDownListUsuwanie.SelectedValue;
                string nazwaZalogowanegoUzytkownika = User.Identity.Name;

                //otwarcie polaczenia z baza

                string connectionString = SqlDataSourceUsuwanieNAZWA.ConnectionString;
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string sprIdUsera = "SELECT [Id] FROM [AspNetUsers] WHERE [UserName] = '" + nazwaZalogowanegoUzytkownika + "'";
                SqlCommand zapytanieSprawdzenie = new SqlCommand(sprIdUsera, connection);
                string idUsera = zapytanieSprawdzenie.ExecuteScalar().ToString();
                connection.Close();

                string pol2 = SQLPlik.ConnectionString;
                SqlConnection conn2 = new SqlConnection(pol2);
                conn2.Open();
                string czySaPliki = "SELECT COUNT(*) FROM[Pliki] WHERE [IdUsera] = '" + idUsera + "'";
                SqlCommand zapytanieCzySaPliki = new SqlCommand(czySaPliki, conn2);
                int czyPlikiSaJeszcze = Int32.Parse(zapytanieCzySaPliki.ExecuteScalar().ToString());
                conn2.Close();

                if (czyPlikiSaJeszcze > 0)
                {
                    SQLPlik.SelectCommand = "SELECT [NazwaPliku] FROM [Pliki] WHERE [IdUsera] = '" + idUsera + "'";
                    DropDownListUsuwanie.DataSourceID = "SQLPlik";
                    //DropDownListUsuwanie.SelectedValue = nazwaPlikuDoUsuniecia;
                    DropDownListUsuwanie.Visible = true;
                }
                else
                {
                    DropDownListUsuwanie.Visible = false;
                    BrakPlikowError.Text = "Brak wgranych plików przez użytkownika! Jeśli chcesz coś usunąć, musisz najpierw coś dodać!";
                }

                
            }

        }

        protected void ButtonUsun_Click(object sender, EventArgs e)
        {
            string nazwaZalogowanegoUzytkownika = User.Identity.Name;

            //otwarcie polaczenia z baza
            string connectionString = SqlDataSourceUsuwanieNAZWA.ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            
            string sprIdUsera = "SELECT [Id] FROM [AspNetUsers] WHERE [UserName] = '" + nazwaZalogowanegoUzytkownika + "'";
            SqlCommand zapytanieSprawdzenie = new SqlCommand(sprIdUsera, connection);
            string idUsera = zapytanieSprawdzenie.ExecuteScalar().ToString();

            connection.Close();

            nazwaPlikuDoUsuniecia = DropDownListUsuwanie.SelectedValue;

            string filePath = Path.Combine(Server.MapPath("~/uploads"), nazwaPlikuDoUsuniecia);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                
                string connStr = SQLPlik.ConnectionString;
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();

                
                String zapytanieDelete = "DELETE FROM [Pliki] WHERE [NazwaPliku] = '"+nazwaPlikuDoUsuniecia+"'";
                SqlCommand sqlKomenda = new SqlCommand(zapytanieDelete, conn);
                sqlKomenda.ExecuteNonQuery();

                conn.Close();


                string polaczenieNazwa = SqlDataSourceUsuwanieNAZWA.ConnectionString;
                SqlConnection connNazwa = new SqlConnection(polaczenieNazwa);
                connNazwa.Open();
                string ileWgranych = "SELECT [LiczbaPlikow] FROM [AspNetUsers] WHERE [UserName] = '" + nazwaZalogowanegoUzytkownika + "'";
                SqlCommand zapytanieIleWgranych = new SqlCommand(ileWgranych, connNazwa);
                int ilePlikowWgrancych = Int32.Parse(zapytanieIleWgranych.ExecuteScalar().ToString());

                int nowaIloscPlikowWgranych = ilePlikowWgrancych - 1;
                String zapytanieUpdate = "UPDATE [AspNetUsers] SET [LiczbaPlikow] = '"+ nowaIloscPlikowWgranych + "' WHERE [UserName] = '" + nazwaZalogowanegoUzytkownika + "'";
                SqlCommand sqlKomendaUpdate = new SqlCommand(zapytanieUpdate, connNazwa);
                sqlKomendaUpdate.ExecuteNonQuery();
                
                connNazwa.Close();
                UsunLabel.Text = "Usunięto plik z serwera";
            }
            else
            {
                UsunLabel.Text = "Nie udało się usunąć pliku";
            }
            

        }

        protected void DropDownListUsuwanie_SelectedIndexChanged(object sender, EventArgs e)
        {
            nazwaPlikuDoUsuniecia = DropDownListUsuwanie.SelectedValue;
            DropDownListUsuwanie.SelectedValue = nazwaPlikuDoUsuniecia;
        }
    }
}