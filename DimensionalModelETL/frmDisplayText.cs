using System.Windows.Forms;

namespace DimensionalModelETL
{
    public partial class frmDisplayText : Form
    {
        public frmDisplayText(string results)
        {
            InitializeComponent();
            txtBox.Text = results;
        }
    }
}
