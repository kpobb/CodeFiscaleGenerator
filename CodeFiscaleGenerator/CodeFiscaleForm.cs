using System;
using System.Windows.Forms;
using CodeFiscaleGenerator.Configurations;
using CodeFiscaleGenerator.Infrastucture;
using CodeFiscaleGenerator.Properties;
using CodeFiscaleGenerator.Services;

namespace CodeFiscaleGenerator
{
    public partial class CodeFiscaleForm : Form
    {
        private readonly ViewState _viewState;
        private readonly CodeFiscaleCalculator _calculator;
        private readonly PlatformStubService _stubService;

        public CodeFiscaleForm()
        {
            InitializeComponent();

            _viewState = new ViewState(labelCbox, registrationCbox, subregistrationCbox, fiscaleCodeTbox, cloneCBox, message);
            _viewState.SetupTrustRelationshipForSSL();
            Text = string.Format("CodeFiscaleGenerator v{0}.{1}.{2}", _viewState.AssemblyVersion.Major, _viewState.AssemblyVersion.Minor, _viewState.AssemblyVersion.Build);

            _calculator = new CodeFiscaleCalculator();

            _stubService = new PlatformStubService(new HttpRequestHandler(), new StubConfiguration());
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private void RegisterNewCodeFiscale_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_viewState.CodeFiscale))
            {
                _viewState.AddMessage(Resources.PleaseEnterCodeFiscale);

                return;
            }

            try
            {
                var response = _stubService.GetCodeFiscaleList(_viewState.SelectedLabelId);

                if (response.HasCodeFiscale(_viewState.CodeFiscale))
                {
                    _viewState.AddMessage(Resources.CodeFiscaleAlreadyRegistered);
                }
                else
                {
                    RegisterCodeFiscale(_viewState.CodeFiscale);

                    response = _stubService.GetCodeFiscaleList(_viewState.SelectedLabelId);

                    if (response.HasCodeFiscale(_viewState.CodeFiscale))
                    {
                        _viewState.AddMessage(Resources.CodeFiscaleSuccesfullyCreated);
                    }
                    else
                    {
                        _viewState.AddMessage(Resources.InternalErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                _viewState.AddMessage(ex.Message);
            }
        }

        private void GenerateCodeFiscale_Click(object sender, EventArgs e)
        {
            var codeFiscale = GenerateNewCodeFiscale();

            _viewState.SetCodeFiscale(codeFiscale);

            _viewState.ClearMessage();
        }

        private void Check_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_viewState.CodeFiscale))
            {
                _viewState.AddMessage(Resources.PleaseEnterCodeFiscale);

                return;
            }

            try
            {
                var response = _stubService.GetCodeFiscaleList(_viewState.SelectedLabelId);

                if (response.HasCodeFiscale(_viewState.CodeFiscale))
                {
                    var data = response.FindCodeFiscaleData(_viewState.CodeFiscale);

                    _viewState.SetRegistrationId(data.IndividualAccountOpeningResponseCode);
                    _viewState.SetSubRegistrationId(data.SubregistrationResponseCode);
                    _viewState.SetCloneState(false);

                    _viewState.AddMessage(Resources.CodeFiscaleWasFound);
                }
                else
                {
                    _viewState.AddMessage(Resources.CodeFiscaleWasNotFound);
                }
            }
            catch (Exception ex)
            {
                _viewState.AddMessage(ex.Message);
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

        private void DeleteCodeFiscale_Click(object sender, EventArgs e)      
        {
            if (string.IsNullOrWhiteSpace(_viewState.CodeFiscale))
            {
                _viewState.AddMessage(Resources.PleaseEnterCodeFiscale);

                return;
            }

            try
            {
                _stubService.DeleteCodeFiscale(_viewState.SelectedLabelId, _viewState.CodeFiscale);

                _viewState.AddMessage(Resources.CodeFiscaleWasSuccesfullyDeleted);
            }
            catch
            {
                _viewState.AddMessage(Resources.CodeFiscaleWasNotFound);
            }
        }

        private void CopyBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_viewState.CodeFiscale))
            {
                Clipboard.SetText(_viewState.CodeFiscale);
            }
        }

        private string GenerateNewCodeFiscale()
        {
            return _calculator.GenerateFiscaleCode(_viewState.SelectedLabelId, _viewState.SelectedRegistrationId, _viewState.SelectedSubregistrationId);
        }

        private void RegisterCodeFiscale(string codeFiscale)
        {
            _stubService.RegisterNewCodeFiscale(codeFiscale, _viewState.SelectedRegistrationId, _viewState.SelectedSubregistrationId, _viewState.SelectedLabelId);
        }
    }
}