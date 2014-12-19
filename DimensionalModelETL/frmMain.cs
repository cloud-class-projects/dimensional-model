using System.Data;
using System.Windows.Forms;
using DimensionalModelETL.WorkflowLayer;

namespace DimensionalModelETL
{
    public partial class frmMain : Form
    {
        private Configuration.Settings _settings;

        public frmMain()
        {
            InitializeComponent();

            // Initialize settings
            _settings = Configuration.SettingsLoader.LoadSettings("Configuration.xml");

            // Attach event handlers
            btnASTHistory.Click += (s, e) => new WF_ATS_UploadHistoricalProduct(_settings).ExecuteWF();
            btnASTCurrent.Click += (s, e) => new WF_ATS_UploadCurrentProduct(_settings, ShowTextResults).ExecuteWF();

            btnBlobHistory.Click += (s, e) => new WF_Blob_UploadHistoricalProduct(_settings).ExecuteWF();
            btnBlobDistinct.Click += (s, e) => new WF_Blob_UploadDistinctProduct(_settings).ExecuteWF();

            btnMapAST.Click += (s, e) => new WF_ATS_CreateProductDimension(_settings, ShowResults).ExecuteWF();
        }

        private void ShowResults(DataTable dt)
        {
            new frmDisplayResults(dt).ShowDialog();
        }

        private void ShowTextResults(string result)
        {
            new frmDisplayText(result).ShowDialog();
        }
    }
}
