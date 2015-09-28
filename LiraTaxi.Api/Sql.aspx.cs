using LiraTaxi.Data.Access;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LiraTaxi.Api
{
    public partial class Sql : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                DataFunctions dbContext = new DataFunctions();
                if (rbtnExecute.Checked)
                {
                    string bl = dbContext.ExecuteNonQuery1(Query.Text);
                    lblMessage.Text = bl;
                }
                else
                {
                    DataTable dt = dbContext.GetDataTable(Query.Text);
                    grid.DataSource = dt;
                    grid.DataBind();
                    lblMessage.Text = "Rows counted " + dt.Rows.Count;
                }
            }
            catch (SqlException ex)
            {
                lblMessage.Text = ex.ToString();
            }
        }
    }
}