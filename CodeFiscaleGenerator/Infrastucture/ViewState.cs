using System;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace CodeFiscaleGenerator.Infrastucture
{
    internal sealed class ViewState
    {
        private readonly ComboBox _label;
        private readonly ComboBox _registrationStatus;
        private readonly ComboBox _subRegistrationStatus;
        private readonly TextBox _codeFiscale;
        private readonly CheckBox _clone;
        private readonly Label _message;

        public ViewState(ComboBox label, ComboBox registrationStatus, ComboBox subRegistrationStatus, TextBox codeFiscale, CheckBox clone, Label message)
        {
            _label = label;
            _registrationStatus = registrationStatus;
            _subRegistrationStatus = subRegistrationStatus;
            _codeFiscale = codeFiscale;
            _clone = clone;
            _message = message;

            label.Items.Add(new ComboBoxItem(1, "Gioco"));
            label.Items.Add(new ComboBoxItem(4, "Italy"));
            label.Items.Add(new ComboBoxItem(5, "Party"));
            label.SelectedIndex = 0;

            object[] possibleErros =
            {
                new ComboBoxItem(1024, "OK"),
                new ComboBoxItem(1025, "KO"),
                new ComboBoxItem(1402, "TO")
            };

            FillComboBox(registrationStatus, possibleErros);
            FillComboBox(subRegistrationStatus, possibleErros);
        }

        public int SelectedSubregistrationId
        {
            get { return ((ComboBoxItem)_subRegistrationStatus.SelectedItem).Id; }
        }

        public int SelectedRegistrationId
        {
            get { return ((ComboBoxItem)_registrationStatus.SelectedItem).Id; }
        }

        public int SelectedLabelId
        {
            get { return ((ComboBoxItem)_label.SelectedItem).Id; }
        }

        public string CodeFiscale
        {
            get { return _codeFiscale.Text; }
        }

        public Version AssemblyVersion
        {
            get
            {
                return Assembly.GetEntryAssembly().GetName().Version;
            }
        }

        public void SetCodeFiscale(string code)
        {
            _codeFiscale.Text = code;
        }

        public void SetRegistrationId(int id)
        {
            SetComboBoxById(_registrationStatus, id);
        }

        public void SetSubRegistrationId(int id)
        {
            SetComboBoxById(_subRegistrationStatus, id);
        }

        public void SetupTrustRelationshipForSSL()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate
            {
                return true;
            };
        }

        public void SetCloneState(bool state)
        {
            _clone.Checked = state;
        }

        public void AddMessage(string message)
        {
            _message.Text = message;
        }

        public void ClearMessage()
        {
            _message.Text = string.Empty;
        }

        private void FillComboBox(ComboBox cbox, object[] values)
        {
            cbox.Items.AddRange(values);
            cbox.SelectedIndex = 0;
        }

        private void SetComboBoxById(ComboBox cbox, int id)
        {
            foreach (var item in cbox.Items)
            {
                if (((ComboBoxItem)item).Id.Equals(id))
                {
                    cbox.SelectedItem = item;
                }
            }
        }
    }
}
