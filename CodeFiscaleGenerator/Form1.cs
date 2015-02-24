using System;
using System.Linq;
using System.Windows.Forms;
using CodeFiscaleGenerator.Entities.Stub;
using CodeFiscaleGenerator.Infrastucture;
using CodeFiscaleGenerator.Properties;

namespace CodeFiscaleGenerator
{
    public partial class Form1 : Form
    {
        private const int MaxCreateRequestAttempts = 20;
        private readonly ViewState _viewState;
        private readonly CodeFiscaleCalculator _calculator;
        private readonly PlatformStubService _stubService;

        public Form1()
        {
            InitializeComponent();

            _viewState = new ViewState(labelCbox, registrationCbox, subregistrationCbox, fiscaleCodeTbox);
            _viewState.SetupTrustRelationshipForSSL();

            Text = string.Format("CodeFiscaleGenerator v{0}.{1}", _viewState.AssemblyVersion.Major, _viewState.AssemblyVersion.Minor);
            
            _calculator = new CodeFiscaleCalculator();
            _stubService = new PlatformStubService(new HttpRequestHandler());
        }

        private void Create_Click(object sender, EventArgs e)
        {
            string codeFiscale;

            var requestAttempts = MaxCreateRequestAttempts;

            StubResponse fiscaleCodes = null;

            try
            {
                do
                {
                    if (fiscaleCodes == null || !fiscaleCodes.CodeFiscaleArray.Any())
                    {
                        fiscaleCodes = _stubService.GetCodeFiscaleList(_viewState.SelectedLabelId);
                    }

                    codeFiscale = _calculator.GenerateFiscaleCode(_viewState.SelectedLabelId, _viewState.SelectedRegistrationId,
                        _viewState.SelectedSubregistrationId);

                    if (_calculator.IsCodeFiscaleUnique(fiscaleCodes, codeFiscale))
                    {
                        _stubService.RegisterNewCodeFiscale(codeFiscale, _viewState.SelectedRegistrationId, _viewState.SelectedSubregistrationId, _viewState.SelectedLabelId);

                        break;
                    }

                    requestAttempts--;
                } 
                while (requestAttempts != 0);

                // getting list including generated code fiscale
                fiscaleCodes = _stubService.GetCodeFiscaleList(_viewState.SelectedLabelId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (!_calculator.IsCodeFiscaleUnique(fiscaleCodes, codeFiscale))
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
                var fiscaleCodes = _stubService.GetCodeFiscaleList(_viewState.SelectedLabelId);

                if (fiscaleCodes == null || !fiscaleCodes.CodeFiscaleArray.Any())
                {
                    MessageBox.Show("Remote server doesn't contain any records.", Resources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!_calculator.IsCodeFiscaleUnique(fiscaleCodes, _viewState.CodeFiscale))
                {
                    MessageBox.Show("Fiscale code was FOUND on remote server!", Resources.InformationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Fiscale code was NOT found on remote server.", Resources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                _stubService.DeleteCodeFiscale(_viewState.SelectedLabelId, _viewState.CodeFiscale);
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
    }
}