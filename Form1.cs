using Microsoft.VisualBasic.ApplicationServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLCH_sach
{
    public partial class Form1 : Form
    {
        List<User> users = new List<User>();

        BindingSource bindingSource = new BindingSource();
        public Form1()
        {
            InitializeComponent();
            bindingSource.DataSource = users;

            dataGridViewUser.DataSource = bindingSource;
        }




        private void XoattGiaoDien1()
        {
            txtLoaiSach.Text = "";
            txtTenSach.Text = "";
            txtTacGia.Text = "";
            txtNXB.Text = "";
            txtSoLuong.Text = "";
            txtGiaTien.Text = "";
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            if (txtLoaiSach.Text != "" && txtTenSach.Text != "" && txtTacGia.Text != "" && txtNXB.Text != "" && txtSoLuong.Text != "" && txtGiaTien.Text != "")
            {
                try
                {
                    int soluong = Convert.ToInt32(txtSoLuong.Text);
                    try
                    {
                        int giatien = Convert.ToInt32(txtGiaTien.Text);
                        dataGridViewKho.Rows.Add(txtLoaiSach.Text, txtTenSach.Text, txtTacGia.Text, txtNXB.Text, txtSoLuong.Text, txtGiaTien.Text);
                        XoattGiaoDien1();
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Nhap sai du lieu gia tien ");
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Nhap sai du lieu so luong");
                }

            }
            else
            {
                MessageBox.Show("Bạn cần nhập đầy đủ thông tin");
            }
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            int vitri = dataGridViewKho.CurrentCell.RowIndex;
            dataGridViewKho[0, vitri].Value = txtLoaiSach.Text;
            dataGridViewKho[1, vitri].Value = txtTenSach.Text;
            dataGridViewKho[2, vitri].Value = txtTacGia.Text;
            dataGridViewKho[3, vitri].Value = txtNXB.Text;
            dataGridViewKho[4, vitri].Value = txtSoLuong.Text;
            dataGridViewKho[5, vitri].Value = txtGiaTien.Text;
            XoattGiaoDien1();
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            int vitri = dataGridViewKho.CurrentCell.RowIndex;
            if (MessageBox.Show("Ban co muon xoa thong tin nay", "Thong Bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dataGridViewKho.Rows.RemoveAt(vitri);
            }
        }

        private void dataGridViewKho_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewKho.SelectedCells.Count != 0)
            {
                DataGridViewRow row = dataGridViewKho.Rows[dataGridViewKho.CurrentCell.RowIndex];
                txtLoaiSach.Text = row.Cells[0].Value.ToString();
                txtLoaiSach.Text = row.Cells[1].Value.ToString();
                txtLoaiSach.Text = row.Cells[2].Value.ToString();
                txtLoaiSach.Text = row.Cells[3].Value.ToString();
                txtLoaiSach.Text = row.Cells[4].Value.ToString();
                txtLoaiSach.Text = row.Cells[5].Value.ToString();
            }
        }

        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            dataGridViewTimKiem.Rows.Clear();

            for (int i = 0; i < dataGridViewKho.Rows.Count - 1; i++)
            {
                bool loaiSachMatch = checkBox1.Checked && dataGridViewKho[0, i].Value.ToString().Equals(txtLoaiSach1.Text, StringComparison.OrdinalIgnoreCase);
                bool tacGiaMatch = checkBox2.Checked && dataGridViewKho[2, i].Value.ToString().Equals(txtTacGia1.Text, StringComparison.OrdinalIgnoreCase);

                if (checkBox1.Checked && checkBox2.Checked)
                {
                    if (loaiSachMatch && tacGiaMatch)
                    {
                        dataGridViewTimKiem.Rows.Add(dataGridViewKho[0, i].Value, dataGridViewKho[1, i].Value, dataGridViewKho[2, i].Value);
                    }
                }
                else if (checkBox1.Checked)
                {
                    if (loaiSachMatch)
                    {
                        dataGridViewTimKiem.Rows.Add(dataGridViewKho[0, i].Value, dataGridViewKho[1, i].Value, dataGridViewKho[2, i].Value);
                    }
                }
                else if (checkBox2.Checked)
                {
                    if (tacGiaMatch)
                    {
                        dataGridViewTimKiem.Rows.Add(dataGridViewKho[0, i].Value, dataGridViewKho[1, i].Value, dataGridViewKho[2, i].Value);
                    }
                }
            }
        }

        private void btnMua_Click_1(object sender, EventArgs e)
        {
            if (txtTenSach1.Text != "" && txtSoLuong1.Text != "" && txtGiaTien1.Text != "")
            {
                try
                {
                    int SoLuong = Convert.ToInt32(txtSoLuong1.Text);
                    try
                    {
                        int GiaTien = Convert.ToInt32(txtGiaTien1.Text);
                        for (int i = 0; i < dataGridViewKho.Rows.Count - 1; i++)
                        {
                            // Nếu sách có trong kho 
                            if (dataGridViewKho[1, i].Value.ToString() == txtTenSach1.Text)
                            {
                                //Điều kiện số sách trong kho còn
                                if (Convert.ToInt32(dataGridViewKho[4, i].Value) - Convert.ToInt32(txtSoLuong1.Text) >= 0)
                                {
                                    int gia1 = Convert.ToInt32(dataGridViewKho[5, i].Value) / Convert.ToInt32(dataGridViewKho[4, i].Value);
                                    int gia2 = Convert.ToInt32(txtGiaTien1.Text) / Convert.ToInt32(txtSoLuong1.Text);
                                    int TienLai = (gia2 - gia1) * Convert.ToInt32(txtSoLuong1.Text);
                                    dataGridViewMua.Rows.Add(dataGridViewKho[0, i].Value, txtTenSach1.Text, txtSoLuong1.Text, TienLai.ToString(), dateTimePicker1.Text);
                                    dataGridViewKho[4, i].Value = Convert.ToInt32(dataGridViewKho[4, i].Value) - Convert.ToInt32(txtSoLuong1.Text);
                                    dataGridViewKho[5, i].Value = Convert.ToInt32(dataGridViewKho[4, i].Value) * gia1;
                                }
                                else
                                {
                                    MessageBox.Show("Sach khong du, chi con " + dataGridViewKho[4, i].Value);
                                }

                            }
                        }
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Nhap sai du lieu gia tien");
                    }
                }
                catch
                {
                    MessageBox.Show("Nhap sai du lieu so luong");
                }
            }
        }

        private void btnKiemTra_Click(object sender, EventArgs e)
        {
            dataGridViewLichSu.Rows.Clear();

            if (checkBox3.Checked && checkBox4.Checked)
            {
                for (int i = 0; i < dataGridViewMua.Rows.Count - 1; i++)
                {
                    if (dataGridViewMua[0, i].Value.ToString() == txtLoaiSach2.Text && dataGridViewMua[4, i].Value.ToString() == dateTimePicker2.Text)
                    {
                        dataGridViewLichSu.Rows.Add(dataGridViewMua[0, i].Value, dataGridViewMua[1, i].Value, dataGridViewMua[2, i].Value, dataGridViewMua[3, i].Value, dataGridViewMua[4, i].Value);
                    }
                }
            }
            else if (checkBox3.Checked)
            {
                for (int i = 0; i < dataGridViewMua.Rows.Count - 1; i++)
                {
                    if (dataGridViewMua[0, i].Value.ToString() == txtLoaiSach2.Text)
                    {
                        dataGridViewLichSu.Rows.Add(dataGridViewMua[0, i].Value, dataGridViewMua[1, i].Value, dataGridViewMua[2, i].Value, dataGridViewMua[3, i].Value, dataGridViewMua[4, i].Value);
                    }
                }
            }
            else if (checkBox4.Checked)
            {
                for (int i = 0; i < dataGridViewMua.Rows.Count - 1; i++)
                {
                    if (dataGridViewMua[4, i].Value.ToString() == dateTimePicker2.Text)
                    {
                        dataGridViewLichSu.Rows.Add(dataGridViewMua[0, i].Value, dataGridViewMua[1, i].Value, dataGridViewMua[2, i].Value, dataGridViewMua[3, i].Value, dataGridViewMua[4, i].Value);
                    }
                }
            }
        }


        private void dataGridViewKho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewKho.SelectedCells.Count != 0)
            {
                DataGridViewRow row = dataGridViewKho.Rows[dataGridViewKho.CurrentCell.RowIndex];
                txtLoaiSach.Text = row.Cells[0].Value.ToString();
                txtTenSach.Text = row.Cells[1].Value.ToString();
                txtTacGia.Text = row.Cells[2].Value.ToString();
                txtNXB.Text = row.Cells[3].Value.ToString();
                txtSoLuong.Text = row.Cells[4].Value.ToString();
                txtGiaTien.Text = row.Cells[5].Value.ToString();
            }
        }


        class User
        {
            // data fields
            public string Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public double Bonus { get; set; }

            public User(string id, string name, int age)
            {
                Id = id;
                Name = name;
                Age = age;
                Bonus = 0.0;
            }

        }

        private double calculateBonus(int userAge)
        {
            double bonus = 0.0;

            if (userAge >= 10 && userAge < 18)
            {
                bonus = 0.2;
            }
            else if (userAge >= 18 && userAge < 25)
            {
                bonus = 0.1;
            }
            else if (userAge >= 25 && userAge < 55)
            {
                bonus = 0;
            }
            else
            {
                bonus = 0.3;
            }

            return bonus;
        }


        private void btnCreate_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các textbox
            string id = txtID.Text;
            string userName = txtUserName.Text;
            int age = Convert.ToInt32(txtUserAge.Text);
            // Tạo đối tượng User mới và thêm vào danh sách
            User newUser = new User(id, userName, age);
            newUser.Bonus = calculateBonus(age);
            users.Add(newUser);
            // Cập nhật BindingSource để hiển thị dữ liệu mới
            bindingSource.ResetBindings(false);
        }



        private void bntDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewUser.CurrentRow != null)
            { // Lấy chỉ số của dòng hiện tại
                int rowIndex = dataGridViewUser.CurrentRow.Index;
                // Xóa đối tượng User tương ứng từ danh sách
                users.RemoveAt(rowIndex);
                // Cập nhật BindingSource để hiển thị dữ liệu mới
                bindingSource.ResetBindings(false);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa.");
            }
        }
    }
}
