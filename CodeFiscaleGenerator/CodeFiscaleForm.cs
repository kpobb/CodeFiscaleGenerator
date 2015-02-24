using System;
using System.Windows.Forms;
using CodeFiscaleGenerator.Entities.Stub;
using CodeFiscaleGenerator.Infrastucture;
using CodeFiscaleGenerator.Properties;
using CodeFiscaleGenerator.Services;

namespace CodeFiscaleGenerator
{
    public partial class CodeFiscaleForm : Form
    {
        private const int MaxRequestAttempts = 20;
        private readonly ViewState _viewState;
        private readonly CodeFiscaleCalculator _calculator;
        private readonly PlatformStubService _stubService;

        public CodeFiscaleForm()
        {
            InitializeComponent();

            _viewState = new ViewState(labelCbox, registrationCbox, subregistrationCbox, fiscaleCodeTbox);
            _viewState.SetupTrustRelationshipForSSL();

            Text = string.Format("CodeFiscaleGenerator v{0}.{1}", _viewState.AssemblyVersion.Major, _viewState.AssemblyVersion.Minor);

            _calculator = new CodeFiscaleCalculator();
            _stubService = new PlatformStubService(new HttpRequestHandler());
        }

        private enum Action
        {
            Check,
            Delete
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private void Create_Click(object sender, EventArgs e)
        {
            string codeFiscale;

            StubResponse response;

            try
            {
                codeFiscale = RegisterNewCodeFiscale();

                response = GetCodeFiscaleList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (!response.HasCode(codeFiscale))
            {
                fiscaleCodeTbox.Text = codeFiscale;

                MessageBox.Show("Fiscale code was successfully created!", Resources.InformationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Internal error. Probably Platform issue. Please try again later.", Resources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Check_Click(object sender, EventArgs e)
        {
            ExecuteAction(Action.Check, "Code fiscale was FOUND on remote server!", "Code fiscale was NOT found.");
        }

        private void CloneCbox_CheckedChanged(object sender, EventArgs e)
        {
            subregistrationCbox.Enabled = !cloneCBox.Checked;

            if (cloneCBox.Checked)
            {
                subregistrationCbox.SelectedIndex = registrationCbox.SelectedIndex;
            }
        }

        private void RegistrationCbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (subregistrationCbox.SelectedIndex == -1)
            {
                subregistrationCbox.Enabled = !cloneCBox.Checked;
            }

            if (cloneCBox.Checked && subregistrationCbox.SelectedIndex != -1)
            {
                subregistrationCbox.SelectedIndex = registrationCbox.SelectedIndex;
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            ExecuteAction(Action.Delete, "Code fiscale was succesfully deleted.", "Code fiscale was NOT found.");
        }

        private void CopyBtn_Click(object sender, EventArgs e)
        {
            _viewState.PutCodeFiscaleToBuffer();
        }

        private void ExecuteAction(Action action, string successMessage, string failedMessage)
        {
            if (string.IsNullOrWhiteSpace(_viewState.CodeFiscale))
            {
                MessageBox.Show("Please enter code fiscale.", Resources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            try
            {
                var codeFiscaleList = GetCodeFiscaleList();

                if (!codeFiscaleList.HasCode(_viewState.CodeFiscale))
                {
                    switch (action)
                    {
                        case Action.Delete:
                            _stubService.DeleteCodeFiscale(_viewState.SelectedLabelId, _viewState.CodeFiscale);
                            break;
                    }

                    MessageBox.Show(successMessage, Resources.InformationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(failedMessage, Resources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string RegisterNewCodeFiscale()
        {
            var requestAttempts = MaxRequestAttempts;

            string codeFiscale;

            var codeFiscaleList = GetCodeFiscaleList();

            do
            {
                codeFiscale = _calculator.GenerateFiscaleCode(_viewState.SelectedLabelId, _viewState.SelectedRegistrationId, _viewState.SelectedSubregistrationId);

                if (codeFiscaleList.HasCode(codeFiscale))
                {
                    _stubService.RegisterNewCodeFiscale(codeFiscale, _viewState.SelectedRegistrationId, _viewState.SelectedSubregistrationId, _viewState.SelectedLabelId);

                    break;
                }

                requestAttempts--;
            }
            while (requestAttempts != 0);

            if (requestAttempts == 0 && string.IsNullOrWhiteSpace(codeFiscale))
            {
                throw new Exception(string.Format("Can't generate a unique code fiscale after {0} attempts.", MaxRequestAttempts));
            }

            return codeFiscale;
        }

        private StubResponse GetCodeFiscaleList()
        {
            return _stubService.GetCodeFiscaleList(_viewState.SelectedLabelId);
        }
    }
}