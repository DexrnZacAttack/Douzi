﻿using PintoNS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoNS.General
{
    public class ContactsManager
    {
        private MainForm mainForm;
        private DataGridView dgvContacts;
        private List<Contact> contacts = new List<Contact>();
        public event EventHandler OnChange = new EventHandler((object sender, EventArgs e) => { });

        public ContactsManager(MainForm mainForm) 
        {
            this.mainForm = mainForm;
            dgvContacts = mainForm.dgvContacts;
        }

        private DataGridViewRow GetContactListEntry(string name) 
        {
            if (name == null) return null;

            foreach (DataGridViewRow row in dgvContacts.Rows) 
            {
                if (((string)row.Cells[1].Value) == name) 
                    return row;
            }

            return null;
        }

        private void AddContactListEntry(Contact contact) 
        {
            if (GetContactListEntry(contact.Name) == null) 
            {
                dgvContacts.Rows.Add(User.StatusToBitmap(contact.Status), contact.Name);
                OnChange.Invoke(this, EventArgs.Empty);
            }
        }

        private void RemoveContactListEntry(Contact contact)
        {
            DataGridViewRow row;
            if ((row = GetContactListEntry(contact.Name)) != null) 
            {
                dgvContacts.Rows.Remove(row);
                OnChange.Invoke(this, EventArgs.Empty);
            }
        }

        private void UpdateContactListEntry(Contact contact) 
        {
            DataGridViewRow row;
            if ((row = GetContactListEntry(contact.Name)) != null)
            {
                row.Cells[0].Value = User.StatusToBitmap(contact.Status);
                row.Cells[1].Value = contact.Name;
                OnChange.Invoke(this, EventArgs.Empty);
            }
        }

        public string GetContactNameFromRow(int rowIndex) 
        {
            foreach (DataGridViewRow row in dgvContacts.Rows)
            {
                if (row.Index == rowIndex)
                    return (string)row.Cells[1].Value;
            }

            return null;
        }

        public Contact GetContact(string name) 
        {
            if (name == null) return null;

            foreach (Contact contact in contacts.ToArray()) 
            {
                if (contact.Name == name)
                    return contact;
            }

            return null;
        }

        public void AddContact(Contact contact)
        {
            if (GetContact(contact.Name) == null) 
            {
                AddContactListEntry(contact);
                contacts.Add(contact);
                OnChange.Invoke(this, EventArgs.Empty);
            }
        }

        public void RemoveContact(Contact contact)
        {
            if (GetContact(contact.Name) != null)
            {
                RemoveContactListEntry(contact);
                contacts.Remove(contact);
                OnChange.Invoke(this, EventArgs.Empty);
            }
        }

        public void UpdateContact(Contact contact) 
        {
            if (GetContact(contact.Name) != null)
            {
                UpdateContactListEntry(contact);
                contacts.Remove(GetContact(contact.Name));
                contacts.Add(contact);
                OnChange.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
