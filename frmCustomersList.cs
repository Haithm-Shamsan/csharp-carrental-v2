using CarRental_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRental_V2_
{
    public partial class frmCustomersList : Form
    {
        public frmCustomersList()
        {
            InitializeComponent();
        }
        DataTable CustomersList = clsCustomers.GetCustomer();
        private void frmCustomersList_Load(object sender, EventArgs e)
        {
            _Refreash();
        }


        void _Refreash()
        {
            dgvCustomers.DataSource = clsCustomers.GetCustomer() ;
            lblRecordNumber.Text=dgvCustomers.RowCount.ToString();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            frmAddNewEditCustomer frm = new frmAddNewEditCustomer(-1);
            frm.ShowDialog();
            _Refreash();
        }

        private void editInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewEditCustomer frm=new frmAddNewEditCustomer((int)dgvCustomers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _Refreash();
        }

        private void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void customerInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsCustomers.Find((int)dgvCustomers.CurrentRow.Cells[0].Value).PersonID;

            frmShowPersonInfo frm=new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
//        None
//CustomerID
//Driving LicenseID
//NationalNo
//FullName
//Phone
        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Visible = (cmFilter.Text != "None");

            if (txtSearch.Visible)
            {
                txtSearch.Text = "";
                txtSearch.Focus();
            }

            CustomersList.DefaultView.RowFilter = "";
            lblRecordNumber.Text = dgvCustomers.Rows.Count.ToString();


           

           


        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                _Refreash();
                return;
            }
            txtSearch.Visible = true;
            string CoulmnName = "";
  
            switch (cmFilter.SelectedItem)
            {
                case "None":
                    CoulmnName = "None";

                    break;
                case "CustomerID":
                    CoulmnName = "CustomerID";
                    break;
                case "Driving LicenseID":
                    CoulmnName = "DrivingLicenseID";
                    break;
                case "NationalNo":
                    CoulmnName = "NationalNo";
                    break;
                case "FullName":
                    CoulmnName = "FullName";
                    break;
                case "Phone":
                    CoulmnName = "Phone";
                    break;
                default:
                    CoulmnName = "None";
                    break;


            }

          
            if (txtSearch.Text.Trim()==""||CoulmnName == "None")
            {
                CustomersList.DefaultView.RowFilter = "";
                _Refreash();
                return;
            }

            if (CoulmnName == "CustomerID")
            {
               CustomersList.DefaultView.RowFilter =  string.Format("[{0}]={1}", CoulmnName, txtSearch.Text);

            }
            else
            {
                CustomersList.DefaultView.RowFilter = string.Format("[{0}] Like'{1}%'", CoulmnName, txtSearch.Text);
            }
          
            dgvCustomers.DataSource = CustomersList;
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cmFilter.Text== "CustomerID")
            {
                e.Handled=!char.IsDigit(e.KeyChar)&&!char.IsControl(e.KeyChar);
            }
        }
    }
}
