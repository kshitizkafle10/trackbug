﻿using ICSharpCode.TextEditor.Document;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BugTrace
{

    /// <summary>
    /// This is a program about the bug tracking.
    /// It is a console application where bug is tracked and solved by different users.
    /// In this program bug is added by tester and admin. Programmer solved the bug.
    /// </summary>
    public partial class dashboard : Form
    {
        /*
         * intiliazing the variables
         * establishing the connection for the dashboard
         */

        string id;
        public string prid;
        string[] store = new string[6];



        MySqlConnection connection = new MySqlConnection("server=localhost; database=reporter; username=jonish; password =jonish "); //setting up a profile to establish connection between c# and mysql

        /// <summary>
        /// getting the username
        /// getting the password 
        /// getting role for associating credentials
        /// to display the profile with the specified id
        /// </summary>
        /// <param name="uname">fetching username for login process from register table</param>
        /// <param name="pword">fetching password for login process from register table</param>
        /// <param name="type">for differentiating the role of different users</param>
        /// <param name="id">To display the profile of specified user</param>
        public dashboard(string uname, string pword, string type, string id)
        {
            InitializeComponent();

            string file = Application.StartupPath;//getting the path of startup files
            FileSyntaxModeProvider fsmp;
            if (Directory.Exists(file))  //choosed the existed pathe of directory
            {

                fsmp = new FileSyntaxModeProvider(file);
                HighlightingManager.Manager.AddSyntaxModeFileProvider(fsmp); //highlighting and adding the file for the selected pfile
                pdesc.SetHighlighting("C/C++");//setting a syntax highlighting for the textbbox
                csol.SetHighlighting("C/C++");

            }

            this.id = id;
            connection.Open();
            string sql = "select Name,Email,Username,Password,gender,role from register where Username ='" + uname + "' and Password = '" + pword + "'";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            MySqlDataReader reader = cmd.ExecuteReader();


            while (reader.HasRows)
            {
                while (reader.Read())
                {

                    store[0] = reader["Name"].ToString();
                    store[1] = reader["Email"].ToString();
                    store[2] = reader["Username"].ToString();
                    store[3] = reader["Password"].ToString();
                    store[4] = reader["gender"].ToString();
                    store[5] = reader["role"].ToString();

                    textBox8.Text = store[0];
                    textBox9.Text = store[1];
                    textBox10.Text = store[2];
                    textBox11.Text = store[3];
                    textBox12.Text = store[4];
                    textBox13.Text = store[5];

                    textBox8.Enabled = false;
                    textBox9.Enabled = false;
                    textBox10.Enabled = false;
                    textBox11.Enabled = false;
                    textBox12.Enabled = false;
                    textBox13.Enabled = false;

                }
                reader.NextResult();
            }

            if (type.Equals("PROGRAMMER"))
            {
                // Removes all the tabs: tabControl1.TabPages.Clear(); 
                profile.TabPages.RemoveByKey("useracc"); // Removes all the tabs: tabControl1.TabPages.Clear();
            }
            else if (type.Equals("TESTER"))
            {
                profile.TabPages.RemoveByKey("solution"); //removing solution
                profile.TabPages.RemoveByKey("useracc");
            }
            else
            {
                profile.TabPages.RemoveByKey("solution"); //removing solution
            }
            connection.Close();

            //method been calling
            account();
            bugDisplay();
            bughistory();




        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// validation for empty
        /// inserting the script 
        /// uploading the image in database 
        /// caliing the different methods
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();

            if (pname.Text == string.Empty)
            {
                MessageBox.Show("name is required");
            }
            else if (pstart.Text == string.Empty)
            {
                MessageBox.Show("range is required");
            }
            else if (pend.Text == string.Empty)
            {
                MessageBox.Show("range is required");
            }
            else if (pclass.Text == string.Empty)
            {
                MessageBox.Show("class is required");
            }
            else if (pmethod.Text == string.Empty)
            {
                MessageBox.Show("method is required");
            }
            else if (pdate.Text == string.Empty)
            {
                MessageBox.Show("date is required");
            }
            else if (pdesc.Text == string.Empty)
            {
                MessageBox.Show("code is required");
            }
            else if (aname.Text == string.Empty)
            {
                MessageBox.Show("author name is required");
            }
            else if (psource.Text == string.Empty)
            {
                MessageBox.Show("source is required");
            }
            //  else if (pimage.Text == string.Empty)
            //{
            //   MessageBox.Show("image is required");
            //}

            else
            {

                MemoryStream mm = new MemoryStream();
                pimage.Image.Save(mm, pimage.Image.RawFormat);
                byte[] b = mm.ToArray();

                string qry = "insert into product (project_name,line_num_start,line_num_end,class_name,method,issued_date,description,author,source_file,image) "
    + "values (@pname,@pstart,@pend,@pclass,@pmethod,@pdate,@pdesc,@aname,@psource,@pimage)";
                MySqlCommand cmd = new MySqlCommand(qry, connection);
                cmd.Parameters.AddWithValue("@pname", pname.Text);
                cmd.Parameters.AddWithValue("@pstart", pstart.Text);
                cmd.Parameters.AddWithValue("@pend", pend.Text);
                cmd.Parameters.AddWithValue("@pclass", pclass.Text);
                cmd.Parameters.AddWithValue("@pmethod", pmethod.Text);
                cmd.Parameters.AddWithValue("@pdate", pdate.Text);
                cmd.Parameters.AddWithValue("@pdesc", pdesc.Text);
                cmd.Parameters.AddWithValue("@aname", aname.Text);
                cmd.Parameters.AddWithValue("@psource", psource.Text);
                cmd.Parameters.AddWithValue("@pimage", b);


                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("inserted");
                }
                else
                {
                    MessageBox.Show("not inserted");
                }
            }
            connection.Close();

            bugDisplay();
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void buggy_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pimage_Click(object sender, EventArgs e)
        {
            //picture
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// creating a file explorer for choosing the image of different format
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog(); //opening fileexplore

                open.Filter = "Choose Image(*.jpg; *.png; *." + "gif)|*.jpg; *.png; *.gif"; //filtering out the diffreent extension photos
                if (open.ShowDialog() == DialogResult.OK)
                {
                    pimage.Image = Image.FromFile(open.FileName); //choosing the the image
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void pmethod_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void history_Click(object sender, EventArgs e)
        {

        }





        private void pdesc_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {



        }
        /// <summary>
        /// enabling the parent and disabling child tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {

            textBox8.Enabled = true;
            textBox9.Enabled = true;
            textBox10.Enabled = true;
            textBox11.Enabled = true;
            textBox12.Enabled = true;
            textBox13.Enabled = true;
            button3.Visible = false;
            button2.Visible = true;

        }

        /// <summary>
        /// enalingand disabling the buttons
        /// updating the table with update query
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click_1(object sender, EventArgs e)
        {
            connection.Open();
            button3.Visible = true;
            button2.Visible = false;
            string sql = "update register set Name='" + textBox8.Text + "',Email='" + textBox9.Text + "',Username='" + textBox10.Text + "',Password='" + textBox11.Text + "',gender='" + textBox12.Text + "',role='" + textBox13.Text + "' where register_id ='" + id + "'";
            MessageBox.Show("Upadted");
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            connection.Close();


        }

        //adding solution for the bug 


        private void solution_Click(object sender, EventArgs e)
        {

        }

        private void useracc_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// This method is used for displaying data to create a proifle for the users.
        /// ListView class is used to display collection of items.
        /// Listviewitem is list out the items of the class listview
        /// subitems represents listviewitems
        /// </summary>
        public void account()
        {

            connection.Open();
            MySqlCommand cmd = new MySqlCommand("select Name,Email,Username,Gender,Role from register", connection); //selecting column name from database 
            /*
             * MySqlDataReader send the row of datas
             * To create mysqldatareader ,execute reader method must used to send a command to the connection.
             * */

            MySqlDataReader data = cmd.ExecuteReader();

            while (data.Read())
            {
                ListViewItem lvt = new ListViewItem(data["Name"].ToString());
                lvt.SubItems.Add(data["Email"].ToString());
                lvt.SubItems.Add(data["Username"].ToString());
                lvt.SubItems.Add(data["Gender"].ToString());
                lvt.SubItems.Add(data["Role"].ToString());
                listView1.Items.Add(lvt);//adding every rows data in the listview
            }
            connection.Close();//connection is closed
        }


        /// <summary>
        /// inserting the solution for the error receiveing code
        /// </summary>
        public void solve()
        {
            connection.Open();
            string qry = "insert into fixer(Name,fixed_date,Project,code)values " + "('" + author.Text + "', '" + date.Text + "','" + prname.Text + "','" + csol.Text + "')";
            MySqlCommand cmd = new MySqlCommand(qry, connection);

            if (cmd.ExecuteNonQuery() == 1) //return the number of row affected
            {
                MessageBox.Show("inserted");
            }
            else
            {
                MessageBox.Show("not inserted");
            }
            connection.Close();
        }

        //displaying bug in the list
        /// <summary>
        /// readin database command and connection and updating them
        /// create a table for the datas
        /// displaying the data source in the gridview
        /// </summary>
        public void bugDisplay()
        {

            MySqlCommand cod = new MySqlCommand("select * from product", connection); //pasing query and connection as parameters
            MySqlDataAdapter mda = new MySqlDataAdapter(cod); //a set of comnand and connection updating
            DataTable dt = new DataTable();//creating a memory slot
            mda.Fill(dt);
            connection.Open();
            MySqlDataReader dat = cod.ExecuteReader();
            dataGridView1.DataSource = dt;
            connection.Close();
            mda.Dispose(); //releasing the data
        }








        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }




        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void author_TextChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// inserting solution of error
        /// displaying differetn solution  in a listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            connection.Open();
            string qry = "insert into fixer(Name,fixed_date,Project,code)values " + "('" + author.Text + "', '" + date.Text + "','" + prname.Text + "','" + csol.Text + "')";
            MySqlCommand cmd = new MySqlCommand(qry, connection);

            if (cmd.ExecuteNonQuery() == 1) //return the number of row affected
            {
                MessageBox.Show("inserted");
            }
            else
            {
                MessageBox.Show("not inserted");
            }


            MySqlCommand cd = new MySqlCommand("select Name,fixed_date,Project,code from fixer", connection); //selecting column name from database 
            /*
             * MySqlDataReader send the row of datas
             * To create mysqldatareader ,execute reader method must used to send a command to the connection.
             * */

            MySqlDataReader data = cd.ExecuteReader();
            listView3.Items.Clear();
            while (data.Read())
            {
                ListViewItem lvt = new ListViewItem(data["Name"].ToString());
                lvt.SubItems.Add(data["fixed_date"].ToString());
                lvt.SubItems.Add(data["Project"].ToString());
                lvt.SubItems.Add(data["code"].ToString());
                listView3.Items.Add(lvt);//adding every rows data in the listview    
            }


            connection.Close();//connection is closed

        }




        private void pdesc_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {





        }
        /// <summary>
        /// searh qury
        /// data is storedin listview item
        /// like statemetn is used for searching
        /// clearing the data
        /// searching is case senesitive
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void usearch_TextChanged(object sender, EventArgs e)
        {


            String search_text = usearch.Text;


            MySqlCommand command = new MySqlCommand("select * from register where Name like '" + search_text + "%'", connection);
            connection.Open();
            MySqlDataReader daa = command.ExecuteReader();


            listView1.Items.Clear();
            while (daa.Read())
            {

                ListViewItem lvt = new ListViewItem(daa["Name"].ToString());
                lvt.SubItems.Add(daa["Email"].ToString());
                lvt.SubItems.Add(daa["Username"].ToString());
                lvt.SubItems.Add(daa["Gender"].ToString());
                lvt.SubItems.Add(daa["Role"].ToString());
                listView1.Items.Add(lvt);//adding every rows data in the listview
            }
            connection.Close();
        }

        private void psource_TextChanged(object sender, EventArgs e)
        {

        }


        private void date_TextChanged(object sender, EventArgs e)
        {

        }

        private void pclass_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// fetching the data from slected row in a gridveiw and sending to the new instance of different class
        /// showing and opeing the panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            int abc = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
            Form2 f = new Form2(abc);
            f.Show();
            Visible = false;

            //MessageBox.Show(abc);
        }



        private void profile_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textEditorControl1_Load(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void aname_TextChanged(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void pend_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pstart_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pdate_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void prname_TextChanged(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void csol_Load(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// searhing query
        /// reading aways a next upcoming data
        /// displaying dta  from dtabase with grid view
        /// searching the compopnent wiht like query
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void search_TextChanged(object sender, EventArgs e)
        {
            string seek = search.Text;
            MySqlCommand cod = new MySqlCommand("select * from product where project_name like '" + seek + "%' OR fixed_date like '" + seek + "%'", connection);
            MySqlDataAdapter mda = new MySqlDataAdapter(cod);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            connection.Open();
            MySqlDataReader dat = cod.ExecuteReader();

            dataGridView1.DataSource = dt;
            connection.Close();
            mda.Dispose();
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// searching the data with help of name and date
        /// and searching is case senstitive
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hsearch_TextChanged(object sender, EventArgs e)
        {



            MySqlCommand command = new MySqlCommand("select * from fixer where Name like '" + hsearch.Text + "%' OR fixed_date like '"+hsearch.Text+"%'" , connection);
            connection.Open();
            MySqlDataReader daataa = command.ExecuteReader();


            listView2.Items.Clear();
            while (daataa.Read())
            {

                ListViewItem lvt = new ListViewItem(daataa["Name"].ToString());
                lvt.SubItems.Add(daataa["fixed_date"].ToString());
                lvt.SubItems.Add(daataa["Project"].ToString());
                lvt.SubItems.Add(daataa["Code"].ToString());
             
                listView2.Items.Add(lvt);//adding every rows data in the listview
            }
            connection.Close();
        }
        /// <summary>
        /// displaying the bbug history
        /// displaying data in listview
        /// </summary>
        public void bughistory()
        {
            MySqlCommand cd = new MySqlCommand("select Name,fixed_date,Project,code from fixer", connection); //selecting column name from database 
                                                                                                              /*
                                                                                                               * MySqlDataReader send the row of datas
                                                                                                               * To create mysqldatareader ,execute reader method must used to send a command to the connection.
                                                                                                               * */
            connection.Open();
            MySqlDataReader data = cd.ExecuteReader();
            listView2.Items.Clear();
            while (data.Read())
            {
                ListViewItem lvt = new ListViewItem(data["Name"].ToString());
                lvt.SubItems.Add(data["fixed_date"].ToString());
                lvt.SubItems.Add(data["Project"].ToString());
                lvt.SubItems.Add(data["code"].ToString());
                listView2.Items.Add(lvt);//adding every rows data in the listview    
            }


            connection.Close();//connection is closed
        }

    }
}

