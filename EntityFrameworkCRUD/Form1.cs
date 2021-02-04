using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameworkCRUD
{
    public partial class Form1 : Form
    {
        ContactEntities contactEntities;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            contactEntities = new ContactEntities();
            contactBindingSource.DataSource = contactEntities.Contacts.ToList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using(formAddEditContact formAddEditContact = new formAddEditContact(null))
            {
                if(formAddEditContact.ShowDialog() == DialogResult.OK)
                {
                    contactBindingSource.DataSource = contactEntities.Contacts.ToList();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(contactBindingSource.Current == null)
            {
                return;
            }

            using (formAddEditContact formAddEditContact = new formAddEditContact(contactBindingSource.Current as Contact))
            {
                if (formAddEditContact.ShowDialog() == DialogResult.OK)
                {
                    contactBindingSource.DataSource = contactEntities.Contacts.ToList();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(contactBindingSource.Current != null)
            {
                if(MessageBox.Show("Estas seguro de borrar el contacto?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) 
                    == DialogResult.Yes)
                {
                    contactEntities.Contacts.Remove(contactBindingSource.Current as Contact);
                    contactBindingSource.RemoveCurrent();
                    contactEntities.SaveChanges();
                }
            }
        }
    }
}
