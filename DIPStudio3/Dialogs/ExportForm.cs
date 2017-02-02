using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DIPStudioUICore.Extensions;
using DIPStudioCore.Data;
using DIPStudioCore;

namespace DIPStudio3
{
    public partial class ExportForm : Form
    {
        public ExportForm(Table table)
        {
            InitializeComponent();
            gridView1.MakeGridReadOnly();
            gridControl1.DataSource = table;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (xlsxSFD.ShowDialog(this) != System.Windows.Forms.DialogResult.OK)
                return;
            gridControl1.ExportToXlsx(xlsxSFD.FileName);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (xlsSFD.ShowDialog(this) != System.Windows.Forms.DialogResult.OK)
                return;
            try {
                gridControl1.ExportToXls(xlsSFD.FileName);
            }
            catch (IOException){ 
                throw new PluginException("File use in other process"); 
            }
        }
    }
}
