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
    public partial class formAddEditContact : Form
    {
        ContactEntities contactEntities;
        public formAddEditContact(Contact contact)
        {
            /* una vez llamado el form, se verifica si el contacto existe, en caso de que exista trae todos los datos, de lo contrario se puede crear uno */
            InitializeComponent();
            contactEntities = new ContactEntities();
            if(contact == null)
            {
                contactBindingSource.DataSource = new Contact();
                contactEntities.Contacts.Add(contactBindingSource.Current as Contact);
            }
            else
            {
                contactBindingSource.DataSource = contact;
                contactEntities.Contacts.Attach(contactBindingSource.Current as Contact);
            }
        }

        private void formAddEditContact_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(DialogResult == DialogResult.OK)
            {
                if (String.IsNullOrEmpty(txtContactName.Text))
                {
                    MessageBox.Show("Inserta un nombre por favor", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtContactName.Focus();
                    e.Cancel = true;
                    return;
                }
                contactEntities.SaveChanges();
                e.Cancel = false;
            }
            e.Cancel = false;
        }
    }
}
