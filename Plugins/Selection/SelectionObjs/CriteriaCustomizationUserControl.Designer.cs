namespace SelectionObjects {
    partial class CriteriaCustomizationUserControl {
        /// <summary> 
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent() {
            this.teName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.beExpression = new DevExpress.XtraEditors.ButtonEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.phC = new DevExpress.XtraLayout.EmptySpaceItem();
            this.phNominal = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beExpression.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phNominal)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.beExpression);
            this.layoutControl1.Controls.Add(this.teName);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.phC,
            this.phNominal});
            // 
            // teName
            // 
            this.teName.Location = new System.Drawing.Point(109, 12);
            this.teName.Name = "teName";
            this.teName.Size = new System.Drawing.Size(451, 20);
            this.teName.StyleController = this.layoutControl1;
            this.teName.TabIndex = 4;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.teName;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(552, 24);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(93, 13);
            // 
            // beExpression
            // 
            this.beExpression.Location = new System.Drawing.Point(109, 36);
            this.beExpression.Name = "beExpression";
            this.beExpression.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.beExpression.Size = new System.Drawing.Size(451, 20);
            this.beExpression.StyleController = this.layoutControl1;
            this.beExpression.TabIndex = 5;
            this.beExpression.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.beExpression_ButtonClick);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.beExpression;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(552, 24);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(93, 13);
            // 
            // phC
            // 
            this.phC.AllowHotTrack = false;
            this.phC.CustomizationFormText = "phC";
            this.phC.Location = new System.Drawing.Point(0, 48);
            this.phC.Name = "phC";
            this.phC.Size = new System.Drawing.Size(552, 149);
            this.phC.TextSize = new System.Drawing.Size(0, 0);
            // 
            // phNominal
            // 
            this.phNominal.AllowHotTrack = false;
            this.phNominal.CustomizationFormText = "phNominal";
            this.phNominal.Location = new System.Drawing.Point(0, 197);
            this.phNominal.Name = "phNominal";
            this.phNominal.Size = new System.Drawing.Size(552, 152);
            this.phNominal.TextSize = new System.Drawing.Size(0, 0);
            // 
            // CriteriaCustomizationUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CriteriaCustomizationUserControl";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beExpression.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phNominal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit teName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.ButtonEdit beExpression;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem phC;
        private DevExpress.XtraLayout.EmptySpaceItem phNominal;
    }
}
