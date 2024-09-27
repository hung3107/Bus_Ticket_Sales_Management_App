using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BanVeXeKhach
{
    public partial class frmTaoTaiKhoan : Form
    {
        DBConnect db = new DBConnect();
        DataTable da = new DataTable();
        DataColumn[] key = new DataColumn[1];
        string str = "select *from nhanvien";
        public frmTaoTaiKhoan()
        {
            InitializeComponent();

            da = db.getDataTable(str);
            //set primary key-----------
            key[0] = da.Columns[0];
            da.PrimaryKey = key;
            //--------------------------
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNhanVien.Text) || string.IsNullOrWhiteSpace(txtHoTen.Text) || string.IsNullOrWhiteSpace(txtMatKhau.Text) || string.IsNullOrWhiteSpace(txtTenDangNhap.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtSDT.Text) || string.IsNullOrWhiteSpace(txtCCCD.Text) || string.IsNullOrWhiteSpace(cboTinh.Text) || string.IsNullOrWhiteSpace(cboChucVu.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin để tạo tài khoản.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (radioButton1.Checked || radioButton2.Checked)
            {
                DataRow newrow = da.NewRow();
                newrow["IDNhanVien"] = txtMaNhanVien.Text;
                newrow["name"] = txtHoTen.Text;
                newrow["numberPhone"] = txtSDT.Text;
                newrow["username"] = txtTenDangNhap.Text;
                newrow["password"] = txtMatKhau.Text;
                newrow["email"] = txtEmail.Text;
                newrow["CCCD"] = txtCCCD.Text;
                newrow["chucVu"] = cboChucVu.Text;
                newrow["diaChi"] = cboTinh.Text;
                newrow["ngaySinh"] = dateTimePicker1.Text;
                if (radioButton1.Checked)
                {
                    newrow["gioiTinh"] = radioButton1.Text;
                }
                else
                {
                    newrow["gioiTinh"] = radioButton2.Text;
                }
                try
                {
                    da.Rows.Add(newrow);
                    int kq = db.updateDatabase(da, str);
                    MessageBox.Show("Tạo thành công");
                }
                catch
                {
                    MessageBox.Show("Tạo thất bại");

                }
            }

        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSDT_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtCCCD_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }


    }
}
