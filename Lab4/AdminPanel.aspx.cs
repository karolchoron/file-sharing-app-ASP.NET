using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab4
{

    public partial class AdminPanel : System.Web.UI.Page
    {
        static string idWybraneagoUsera;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DropDownListNAZWYuserow_SelectedIndexChanged(object sender, EventArgs e)
        {
            idWybraneagoUsera = DropDownListNAZWYuserow.SelectedValue;
            DropDownListNAZWYuserow.SelectedValue = idWybraneagoUsera;

            RolaLabel.Text = idWybraneagoUsera;

        }

        protected void NadajRoleButton_Click1(object sender, EventArgs e)
        {
            RolaLabel.Text = idWybraneagoUsera;
            //otwarcie polaczenia z baza danych
            string connectionString = SqlDataROLEUserow.ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string sprawdzenie = "SELECT COUNT(*) FROM [AspNetUserRoles] WHERE [UserId] = '"+ idWybraneagoUsera + "'";
            SqlCommand zapytanieSprawdzenie = new SqlCommand(sprawdzenie, connection);

            int ileWBazie = Int32.Parse(zapytanieSprawdzenie.ExecuteScalar().ToString());
            
            if (ileWBazie == 0)
            {
                String dodawanieDoBazy = "INSERT INTO [AspNetUserRoles] VALUES ('" + idWybraneagoUsera + "', 1)";
                SqlCommand Dodanie = new SqlCommand(dodawanieDoBazy, connection);
                Dodanie.ExecuteNonQuery();
                RolaLabel.Text = "Użytkownik został dodany do roli Administratora";
            }
            else
            {
                RolaLabel.Text = "Użytkownik już ma rolę Administratora!";
            }

            connection.Close();
            
        }
    }
}