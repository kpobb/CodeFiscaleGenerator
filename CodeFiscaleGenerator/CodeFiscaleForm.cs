using System;
using System.Linq;
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

            if (!_calculator.Find(response, codeFiscale))
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
            if (!_calculator.IsCodeFiscaleValid(_viewState.CodeFiscale))
            {
                MessageBox.Show("Please enter code fiscale.", Resources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            try
            {
                var response = GetCodeFiscaleList();

                if (response == null || !response.Items.Any())
                {
                    MessageBox.Show("Remote server doesn't contain any records.", Resources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!_calculator.Find(response, _viewState.CodeFiscale))
                {
                    MessageBox.Show("Code fiscale was FOUND on remote server!", Resources.InformationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Code fiscale was NOT found.", Resources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            if (!_calculator.IsCodeFiscaleValid(_viewState.CodeFiscale))
            {
                MessageBox.Show("Please enter code fiscale.", Resources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            try
            {
                var codeFiscaleList = GetCodeFiscaleList();

                if (codeFiscaleList == null || !codeFiscaleList.Items.Any())
                {
                    MessageBox.Show("Remote server doesn't contain any records.", Resources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!_calculator.Find(codeFiscaleList, _viewState.CodeFiscale))
                {
                    _stubService.DeleteCodeFiscale(_viewState.SelectedLabelId, _viewState.CodeFiscale);

                    MessageBox.Show("Code fiscale was succesfully deleted.", Resources.InformationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Code fiscale was NOT found.", Resources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CopyBtn_Click(object sender, EventArgs e)
        {
            _viewState.PutCodeFiscaleToBuffer();
        }

        private string RegisterNewCodeFiscale()
        {
            var requestAttempts = MaxRequestAttempts;

            string codeFiscale;

            var codeFiscaleList = GetCodeFiscaleList();

            do
            {
                codeFiscale = _calculator.GenerateFiscaleCode(_viewState.SelectedLabelId, _viewState.SelectedRegistrationId, _viewState.SelectedSubregistrationId);

                if (_calculator.Find(codeFiscaleList, codeFiscale))
                {
                    _stubService.RegisterNewCodeFiscale(codeFiscale, _viewState.SelectedRegistrationId, _viewState.SelectedSubregistrationId, _viewState.SelectedLabelId);

                    break;
                }

                requestAttempts--;
            }
            while (requestAttempts != 0);

            if (requestAttempts == 0 && string.IsNullOrWhiteSpace(codeFiscale))
            {
                throw new Exception(string.Format("Impossible to generate a unique code fiscale after {0} attempts. ", MaxRequestAttempts));
            }

            return codeFiscale;
        }

        private StubResponse GetCodeFiscaleList()
        {
            return _stubService.GetCodeFiscaleList(_viewState.SelectedLabelId);
        }
    }
}