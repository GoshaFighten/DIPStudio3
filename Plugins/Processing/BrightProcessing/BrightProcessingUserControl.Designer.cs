namespace BrightProcessing
{
    partial class BrightProcessingUserControl
    {
        /// <summary> 
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.frameComboBox1 = new CustomEditor.FrameComboBox();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.seX1 = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.seY1 = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.seX2 = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.seY2 = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.peGraf = new DevExpress.XtraEditors.PictureEdit();
            this.seriesView1 = new DIPStudioUICore.SeriesView();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.frameComboBox1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seX1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seY1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seX2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seY2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peGraf.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.seriesView1);
            this.layoutControl1.Controls.Add(this.peGraf);
            this.layoutControl1.Controls.Add(this.textEdit1);
            this.layoutControl1.Controls.Add(this.seY2);
            this.layoutControl1.Controls.Add(this.seX2);
            this.layoutControl1.Controls.Add(this.seY1);
            this.layoutControl1.Controls.Add(this.seX1);
            this.layoutControl1.Controls.Add(this.frameComboBox1);
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(614, 250, 389, 350);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem7,
            this.layoutControlGroup2});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Text = "Root";
            // 
            // frameComboBox1
            // 
            this.frameComboBox1.Location = new System.Drawing.Point(130, 12);
            this.frameComboBox1.Name = "frameComboBox1";
            this.frameComboBox1.Properties.Application = null;
            this.frameComboBox1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.frameComboBox1.Size = new System.Drawing.Size(430, 20);
            this.frameComboBox1.StyleController = this.layoutControl1;
            this.frameComboBox1.TabIndex = 4;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.frameComboBox1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(552, 24);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(114, 13);
            // 
            // seX1
            // 
            this.seX1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seX1.Location = new System.Drawing.Point(142, 229);
            this.seX1.Name = "seX1";
            this.seX1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seX1.Properties.IsFloatValue = false;
            this.seX1.Properties.Mask.EditMask = "N00";
            this.seX1.Size = new System.Drawing.Size(406, 20);
            this.seX1.StyleController = this.layoutControl1;
            this.seX1.TabIndex = 5;
            this.seX1.EditValueChanged += new System.EventHandler(this.se_EditValueChanged);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.seX1;
            this.layoutControlItem2.CustomizationFormText = "layout2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 181);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(528, 24);
            this.layoutControlItem2.Text = "layout2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(114, 13);
            // 
            // seY1
            // 
            this.seY1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seY1.Location = new System.Drawing.Point(142, 253);
            this.seY1.Name = "seY1";
            this.seY1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seY1.Properties.IsFloatValue = false;
            this.seY1.Properties.Mask.EditMask = "N00";
            this.seY1.Size = new System.Drawing.Size(406, 20);
            this.seY1.StyleController = this.layoutControl1;
            this.seY1.TabIndex = 6;
            this.seY1.EditValueChanged += new System.EventHandler(this.se_EditValueChanged);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.seY1;
            this.layoutControlItem3.CustomizationFormText = "layout3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 205);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(528, 24);
            this.layoutControlItem3.Text = "layout3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(114, 13);
            // 
            // seX2
            // 
            this.seX2.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seX2.Location = new System.Drawing.Point(142, 277);
            this.seX2.Name = "seX2";
            this.seX2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seX2.Properties.IsFloatValue = false;
            this.seX2.Properties.Mask.EditMask = "N00";
            this.seX2.Size = new System.Drawing.Size(406, 20);
            this.seX2.StyleController = this.layoutControl1;
            this.seX2.TabIndex = 7;
            this.seX2.EditValueChanged += new System.EventHandler(this.se_EditValueChanged);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.seX2;
            this.layoutControlItem4.CustomizationFormText = "layout4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 229);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(528, 24);
            this.layoutControlItem4.Text = "layout4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(114, 13);
            // 
            // seY2
            // 
            this.seY2.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seY2.Location = new System.Drawing.Point(142, 301);
            this.seY2.Name = "seY2";
            this.seY2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seY2.Properties.IsFloatValue = false;
            this.seY2.Properties.Mask.EditMask = "N00";
            this.seY2.Size = new System.Drawing.Size(406, 20);
            this.seY2.StyleController = this.layoutControl1;
            this.seY2.TabIndex = 8;
            this.seY2.EditValueChanged += new System.EventHandler(this.se_EditValueChanged);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.seY2;
            this.layoutControlItem5.CustomizationFormText = "layout5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 253);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(528, 24);
            this.layoutControlItem5.Text = "layout5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(114, 13);
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(130, 337);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(430, 20);
            this.textEdit1.StyleController = this.layoutControl1;
            this.textEdit1.TabIndex = 10;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.textEdit1;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 325);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(552, 24);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(114, 13);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem8,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(552, 301);
            this.layoutControlGroup2.Text = "layoutControlGroup2";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.peGraf;
            this.layoutControlItem8.CustomizationFormText = "хз, че это за график??";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(284, 181);
            this.layoutControlItem8.Text = "Коэффициент яркости";
            this.layoutControlItem8.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(114, 13);
            // 
            // peGraf
            // 
            this.peGraf.Location = new System.Drawing.Point(24, 64);
            this.peGraf.Name = "peGraf";
            this.peGraf.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.peGraf.Size = new System.Drawing.Size(280, 161);
            this.peGraf.StyleController = this.layoutControl1;
            this.peGraf.TabIndex = 11;
            // 
            // seriesView1
            // 
            this.seriesView1.Location = new System.Drawing.Point(308, 64);
            this.seriesView1.Name = "seriesView1";
            this.seriesView1.Number = 0;
            this.seriesView1.Size = new System.Drawing.Size(240, 161);
            this.seriesView1.TabIndex = 12;
            this.seriesView1.UpdateControl += se_EditValueChanged;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.seriesView1;
            this.layoutControlItem6.CustomizationFormText = "Результат обработки";
            this.layoutControlItem6.Location = new System.Drawing.Point(284, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(244, 181);
            this.layoutControlItem6.Text = "Результат обработки";
            this.layoutControlItem6.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(114, 13);
            // 
            // BrightProcessingUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "BrightProcessingUserControl";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.frameComboBox1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seX1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seY1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seX2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seY2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peGraf.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomEditor.FrameComboBox frameComboBox1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.SpinEdit seY2;
        private DevExpress.XtraEditors.SpinEdit seX2;
        private DevExpress.XtraEditors.SpinEdit seY1;
        private DevExpress.XtraEditors.SpinEdit seX1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraEditors.PictureEdit peGraf;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DIPStudioUICore.SeriesView seriesView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}
