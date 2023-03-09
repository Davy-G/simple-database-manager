using System;
using System.Data.SQLite;
using System.Drawing;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace database_manager_project
{
    public partial class databasemanager : Form
    {


        private static SQLiteConnection connection = null;
        public databasemanager()
        {
            InitializeComponent();
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Query_Box(object sender, EventArgs e)
        {
            
         
        }

        private void Load_Database_Button(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "(*.sqlite3) | *.sqlite3";
            DialogResult result = ofd.ShowDialog();


            switch (result.ToString())

            {
                case "OK":
                    {
                        MessageBox.Show($"selected file is {ofd.FileName}");
                        connection = new SQLiteConnection($"Data Source = {ofd.FileName}; Version = 3;");
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(
                            "SELECT name Tables FROM sqlite_master WHERE type = 'table' and name != 'sqlite_sequence'", connection
                            );

                        using (DataSet ds = new DataSet())
                            {
                                adapter.Fill(ds);
                                dataGridView1.DataSource = ds.Tables[0];
                                dataGridView1.Refresh();
                                button2.Visible = true;
                                closedatabase.Visible = true;
                            }
                        
                       
                        
                        break;
                    }

                case "Cancel":
                    {
                        MessageBox.Show("operation canceled by user");
                        break;
                    }


            }
            
        }

        private void Create_Database_And_Open_It_Button(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "(*.db) | *.db";
            DialogResult result = saveFileDialog.ShowDialog();
            try
            {
                switch (result.ToString())
                {


                    case "OK":
                        {
                            string path = saveFileDialog.FileName;
                            connection = new SQLiteConnection($"Data Source = {path}; Version = 3;");
                            button2.Visible = true;
                            closedatabase.Visible = true;
                            break;
                          
                        };

                    case "Cancel":
                        {
                            MessageBox.Show("operation canceled by user");
                            break;
                        };


 
                 };
            }
             
            catch (Exception ex)
            {
               LoggerWindow.Text = ex.Message; 
            }

           

           

        }

        private void Execute_query_button(object sender, EventArgs e)
        {

            string query = textBox1.Text;
            try
            {

                using (var sqlDataAdapt = new SQLiteDataAdapter(query, connection))
                {
                    using (DataTable dt = new DataTable())
                    {
                        sqlDataAdapt.Fill(dt);
                        dataGridView1.DataSource = dt;

                    }
                }
                
                LoggerWindow.Text = "";
            }

            catch (SQLiteException ex)
            {
                LoggerWindow.Text = ex.Message;
            }

          



        }
        
        private void Logger_Window(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         
        }

        private void closedatabase_Click(object sender, EventArgs e)
        {
     
            dataGridView1.DataSource = null;
            connection.Close();
            button2.Visible = false;
            closedatabase.Visible = false;
        
            

        }

        private void savedata_Click(object sender, EventArgs e)
        {
            
        }
    }
}
