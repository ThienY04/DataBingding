using EnityFramework_DataBinding.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnityFramework_DataBinding
{
    public partial class Form1 : Form
    {
        private readonly SchoolContext db = new SchoolContext(); 
        private Student currentStudent;
        public Form1()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            dgvSinhvien.DataSource = db.Students.ToList();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'schoolDBDataSet.Students' table. You can move, or remove it, as needed.
            this.studentsTableAdapter.Fill(this.schoolDBDataSet.Students);
            LoadData();

        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text) ||!int.TryParse(txtAge.Text, out int age) || cboMajor.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin: Họ tên, Tuổi và Ngành học.");
                return;
            }
            var sv = new Student 
            {
                FullName = txtFullName.Text,
                Age = age,
                Major = cboMajor.SelectedItem.ToString()
            };
            db.Students.Add(sv);
            db.SaveChanges();
            LoadData();
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (currentStudent != null)
            {
                var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    db.Students.Remove(currentStudent);
                    db.SaveChanges();
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để xóa.");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (currentStudent != null)
            {
                if (string.IsNullOrWhiteSpace(txtFullName.Text) ||
                    !int.TryParse(txtAge.Text, out int age) ||
                    cboMajor.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin: Họ tên, Tuổi và Ngành học.");
                    return;
                }
                currentStudent.FullName = txtFullName.Text;
                currentStudent.Age = age;
                currentStudent.Major = cboMajor.SelectedItem.ToString();

                db.SaveChanges();
                LoadData();
            }
        }

        private void dgvSinhvien_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSinhvien.CurrentRow != null)
            {
                currentStudent = (Student)dgvSinhvien.CurrentRow.DataBoundItem;
                txtFullName.Text = currentStudent.FullName;
                txtAge.Text = currentStudent.Age.ToString();
                cboMajor.SelectedItem = currentStudent.Major;
            }
        }
    }
}
