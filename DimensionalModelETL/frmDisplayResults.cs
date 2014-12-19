using System.Data;
using System.Windows.Forms;

namespace DimensionalModelETL
{
    public partial class frmDisplayResults : Form
    {
        public frmDisplayResults(DataTable dt)
        {
            InitializeComponent();

            grdMain.DataSource = dt;
        }
    }
}
