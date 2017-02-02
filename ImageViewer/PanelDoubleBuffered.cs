using DevExpress.XtraEditors;
namespace ImageViewer
{
    public class PanelDoubleBuffered : PanelControl
    {
        public PanelDoubleBuffered()
        {
            this.DoubleBuffered = true;
            this.UpdateStyles();
        }
    }
}
