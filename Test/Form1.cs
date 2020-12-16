using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Test
{
    public partial class Form1 : Form
    {
        static string strConn = "datasource=localhost; database = student; port = 3306; username = root; password = test1234!";
        MySqlConnection conn = new MySqlConnection(strConn);
        private void list(int grade, int cclass, int no, string name, string score)
        {
            dataGridView1.Rows.Add(grade, cclass, no, name, score);
        }

        public Form1()
        {
          
            InitializeComponent();
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "grade";
            dataGridView1.Columns[1].Name = "cclass";
            dataGridView1.Columns[2].Name = "no";
            dataGridView1.Columns[3].Name = "name";
            dataGridView1.Columns[4].Name = "score";

        }

        private void dataGridView(object sender, DataGridViewCellEventArgs e)
        {
          
        }        
        private void Read(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            string selectStatement = "select * from student;";
            MySqlCommand cmd = new MySqlCommand(selectStatement, conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                adapter.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    list(Int32.Parse(row[0].ToString()), Int32.Parse(row[1].ToString()), Int32.Parse(row[2].ToString()), row[3].ToString(), row[4].ToString());
                }

                conn.Close();
                dt.Rows.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
        }


        private void Create(object sender, EventArgs e)
        {
            string insertStatement = "INSERT INTO student(grade,cclass,no,name,score) VALUES(@grade,@cclass,@no,@name,@score)";
            MySqlCommand cmd = new MySqlCommand(insertStatement, conn);
            int index = dataGridView1.Rows.Count - 1;

            try
            {
                int grade = Int32.Parse(dataGridView1.Rows[index - 1].Cells[0].Value.ToString());
                int cclass = Int32.Parse(dataGridView1.Rows[index - 1].Cells[1].Value.ToString());
                int no = Int32.Parse(dataGridView1.Rows[index - 1].Cells[2].Value.ToString());
                string name = dataGridView1.Rows[index - 1].Cells[3].Value.ToString();
                string score = dataGridView1.Rows[index - 1].Cells[4].Value.ToString();


                conn.Open();
                cmd.Parameters.Add("@grade", MySqlDbType.VarChar).Value = grade;
                cmd.Parameters.Add("@cclass", MySqlDbType.VarChar).Value = cclass;
                cmd.Parameters.Add("@no", MySqlDbType.VarChar).Value = no;
                cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
                cmd.Parameters.Add("@score", MySqlDbType.VarChar).Value = score;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("성공");
                }

                conn.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
        }


        private void Update(object sender, EventArgs e)
        {
            string updateStatement = "UPDATE student SET score = @score WHERE name = @name";
            MySqlCommand cmd = new MySqlCommand(updateStatement, conn);

            int rowIndex = dataGridView1.CurrentRow.Index;
            Console.WriteLine(rowIndex);
            try
            {

                string name = dataGridView1.Rows[rowIndex].Cells[3].Value.ToString();
                string score = dataGridView1.Rows[rowIndex].Cells[4].Value.ToString();

                conn.Open();

                cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
                cmd.Parameters.Add("@score", MySqlDbType.VarChar).Value = score;


                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("성공");
                }
                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
        }

        private void Remove(object sender, EventArgs e)
        {
            string deleteStatement = "DELETE FROM student WHERE no = @no";
            MySqlCommand cmd = new MySqlCommand(deleteStatement, conn);

            int rowIndex = dataGridView1.CurrentRow.Index;
            Console.WriteLine(rowIndex);

            try
            {
                int no = Int32.Parse(dataGridView1.Rows[rowIndex].Cells[2].Value.ToString());

                conn.Open();

                cmd.Parameters.Add("@no", MySqlDbType.VarChar).Value = no;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("성공");
                }

                dataGridView1.Rows.Remove(dataGridView1.Rows[rowIndex]);
                conn.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }

        }
    }
}
