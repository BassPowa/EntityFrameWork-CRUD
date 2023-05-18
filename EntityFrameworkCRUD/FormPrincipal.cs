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
    public partial class FormPrincipal : Form
    {
        ContactEntities contactEntities;
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            contactEntities = new ContactEntities();
            contactBindingSource.DataSource = contactEntities.Contacts.ToList();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using(FormAddEditContact formAddEditContact = new FormAddEditContact(null))
            {
                if(formAddEditContact.ShowDialog() == DialogResult.OK)
                {
                    contactBindingSource.DataSource = contactEntities.Contacts.ToList();
                }
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if(contactBindingSource.Current == null)
            {
                return;
            }

            using (FormAddEditContact formAddEditContact = new FormAddEditContact(contactBindingSource.Current as Contact))
            {
                if (formAddEditContact.ShowDialog() == DialogResult.OK)
                {
                    contactBindingSource.DataSource = contactEntities.Contacts.ToList();
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
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
