using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab4
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                string path = Server.MapPath("~/uploads");
                List<ListItem> files = new List<ListItem>();
                foreach (string file in Directory.GetFiles(path))
                {
                    files.Add(new ListItem { Text = System.IO.Path.GetFileName(file), Value = file });
                }
                foreach (string dir in Directory.GetDirectories(path))
                {
                    foreach (string file in Directory.GetFiles(dir))
                    {
                        files.Add(new ListItem { Text = System.IO.Path.GetFileName(file), Value = file });
                    }
                }

                ListView1.DataSource = files;
                ListView1.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
        
        }
    }
}