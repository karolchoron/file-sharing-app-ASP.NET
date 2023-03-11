using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab4
{
    public partial class About : Page
    {
        static string nazwaWybranegoPliku;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string sciezka = Server.MapPath("~/uploads");
                List<ListItem> pliczki = new List<ListItem>();
                foreach (string plik in Directory.GetFiles(sciezka))
                {
                    pliczki.Add(new ListItem { Text = Path.GetFileName(plik), Value = plik });
                }
                foreach (string katalog in Directory.GetDirectories(sciezka))
                {
                    foreach (string plik in Directory.GetFiles(katalog))
                    {
                        pliczki.Add(new ListItem { Text = Path.GetFileName(plik), Value = plik });
                    }
                }

                ListaDropDown.DataSource = pliczki;
                ListaDropDown.DataBind();
                ListaDropDown.SelectedValue = nazwaWybranegoPliku;
            }
        }


        protected void ListaDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            nazwaWybranegoPliku = ListaDropDown.SelectedValue;

            ListaDropDown.SelectedValue = nazwaWybranegoPliku;


        }
        
        protected void Pobieranie_Click(object sender, EventArgs e)
        {
            nazwaWybranegoPliku = ListaDropDown.SelectedValue;
            string sciezkaPlikow = Path.Combine(Server.MapPath("~/uploads/"), nazwaWybranegoPliku);

            // Sprawdzanie czy plik istnieje na serwerze
            if (File.Exists(sciezkaPlikow))
            {
                // Ustawienie nagłówka odpowiedzi HTTP
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + nazwaWybranegoPliku);
                Response.TransmitFile(sciezkaPlikow);
                Response.End();
            }
            else
            {
                //blad pobierania
            }
        }
        
    }
}