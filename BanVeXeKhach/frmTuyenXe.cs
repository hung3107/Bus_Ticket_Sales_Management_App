using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BanVeXeKhach
{
    public partial class frmTuyenXe : Form
    {
        DBConnect db = new DBConnect();
        DataTable dt = new DataTable();
        string sql = "select*from TuyenXe";
        public frmTuyenXe()
        {
            InitializeComponent();
            dt = db.getDataTable(sql);
            DataColumn[] key = new DataColumn[1];
            key[0] = dt.Columns["IDTuyenXe"];
            dt.PrimaryKey = key;
            DataBingding(dt);
        }
        public void load_datagridview()
        {
            string sql = "select*from TuyenXe";
            DataTable dt = db.getDataTable(sql);
            dataGridView1.DataSource = dt;
            DataBingding(dt);
        }

        public void DataBingding(DataTable pDT)
        {
            txtMaTuyen.DataBindings.Clear();
            txtNoiDi.DataBindings.Clear();
            txtNoiDen.DataBindings.Clear();

            txtMaTuyen.DataBindings.Add("Text", pDT, "IDTuyenXe");
            txtNoiDi.DataBindings.Add("Text", pDT, "diem1");
            txtNoiDen.DataBindings.Add("Text", pDT, "diem2");
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtMaTuyen.Clear();
            txtNoiDi.Clear();
            txtNoiDen.Clear();
            string sql = "select*from TuyenXe";
            dataGridView1.DataSource = db.getDataTable(sql);
            load_datagridview();
        }
        private void btnThem_Click_1(object sender, EventArgs e)
        {
            DataRow newrow = dt.NewRow();
            newrow["IDTuyenXe"] = txtMaTuyen.Text;
            newrow["diem1"] = txtNoiDi.Text;
            newrow["diem2"] = txtNoiDen.Text;
            try
            {
                dt.Rows.Add(newrow);
                int kq = db.updateDatabase(dt, sql);
                MessageBox.Show("Thêm thành công");
            }
            catch
            {
                MessageBox.Show("Thêm thất bại");
            }
            load_datagridview();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //load_datagridview();
            string sql = "select*from TuyenXe where IDTuyenXe='" + txtMaTuyen.Text + "'";
            dataGridView1.DataSource = db.getDataTable(sql);
        }

        private void frmTuyenXe_Load_1(object sender, EventArgs e)
        {
            load_datagridview();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Xoa")
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Chắc chắn", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string sql = "Select * From TuyenXe";
                    DataRow r = dt.Rows.Find(txtMaTuyen.Text);
                    if (r != null)
                    {
                        r.Delete();
                    }
                    try
                    {
                        int kq = db.updateDatabase(dt, sql);
                        MessageBox.Show("Xóa thành công");
                    }

                    catch
                    {
                        MessageBox.Show("Xóa thất bại");
                    }
                }
                load_datagridview();
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Sua")
            {

                if (MessageBox.Show("Bạn có chắc chắn muốn sửa không?", "Chắc chắn", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string sql = "Select * From TuyenXe";
                    DataRow r = dt.Rows.Find(txtMaTuyen.Text);
                    if (r != null)
                    {
                        r["diem1"] = txtNoiDi.Text;
                        r["diem2"] = txtNoiDen.Text;
                    }
                    try
                    {
                        int kq = db.updateDatabase(dt, sql);
                        MessageBox.Show("Sửa thành công");
                    }
                    catch
                    {
                        MessageBox.Show("Sửa thất bại");
                    }

                }
                load_datagridview();
            }
        }
    }
}
