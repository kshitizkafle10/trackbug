using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace BugTrace
{
    public partial class Form2 : Form
    {
        int abc; //initiliazing data
        string[] store = new string[10];//initiliazing dataS
        MySqlConnection con = new MySqlConnection("server=localhost;database = reporter;username =buggy;password = "); //setting up a profile to establish connection between c# and mysql

        /// <summary>
        /// here  fettch th id for editing bug
        /// </summary>
        /// <param name="abc"> returning a data to parameter constructor</param>
        public Form2(int abc)
        {
            InitializeComponent();
            
            this.abc = abc;//this refers to current instance
            getBug(); //current method
            


             
         }
        /// <summary>
        /// opening connection
        /// select query implementation
        /// opening a method by reading every singgle data from database
        /// </summary>
        public void getBug()
        {

            connection.Open();
            string sql = "select project_name,line_num_start,line_num_end,class_name,method,issued_date,description,author,source_file,image from product where  productct_id = '" + abc + "'";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            MessageBox.Show(sql);

            /*
             * has rows chekcing whetheer it has multiples rows or not
             * counting next stored data
             * storing data from database
             * */
            while (reader.HasRows)
            {
                while (reader.Read())
                {


                    store[0] = reader["project_name"].ToString();
                    store[1] = reader["line_num_start"].ToString();
                    store[2] = reader["line_num_end"].ToString();
                    store[3] = reader["class_name"].ToString();
                    store[4] = reader["method"].ToString();
                    store[5] = reader["issued_date"].ToString();
                    store[6] = reader["description"].ToString();
                    store[7] = reader["author"].ToString();
                    store[8] = reader["source_file"].ToString();
              


                    pname.Text = store[0];
                    pstart.Text = store[1];
                    pend.Text = store[2];
                    pclass.Text = store[3];
                    pmethod.Text = store[4];
                    pdate.Text = store[5];
                    pdesc.Text = store[6];
                    aname.Text = store[7];
                    psource.Text = store[8];

                 
                }
                reader.NextResult();
                connection.Close();
               
            }
        }

        /// <summary>
        /// updating the data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection("server=localhost; database=reporter; username=jonish; password =jonish "); //setting up a profile to establish connection between c# and mysql
            connection.Open();

        

            string sql = "update product set project_name='" + pname.Text + "',line_num_start='" + pstart.Text + "',line_num_end='" + pend.Text + "',class_name='" + pclass.Text + "',method='" + pmethod.Text + "',issued_date='" + pdate.Text + "',description='" + pdesc.Text + "',author='" + aname.Text + "',source_file='" + psource.Text + "'where productct_id =" + abc + ""; //update qury
            MessageBox.Show("Upadted");
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            connection.Close();
            this.Close();
          
        }

        private void pdesc_Load(object sender, EventArgs e)
        {

        }
    }
    }


