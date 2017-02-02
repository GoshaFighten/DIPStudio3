namespace SelectionObjects {
    partial class SelectionObjsUserControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.cmbInputTable = new CustomEditor.TableComboBox();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gcCriterias = new DevExpress.XtraGrid.GridControl();
            this.criteriaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvCriterias = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExpression = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNominal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCoefficient = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.phStrob = new DevExpress.XtraLayout.EmptySpaceItem();
            this.phThreshold = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.cePrognosis = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ceFollowName = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ceIntermediate = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.teResult = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbInputTable.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCriterias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.criteriaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCriterias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phStrob)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cePrognosis.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceFollowName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceIntermediate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teResult.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ceIntermediate);
            this.layoutControl1.Controls.Add(this.ceFollowName);
            this.layoutControl1.Controls.Add(this.cePrognosis);
            this.layoutControl1.Controls.Add(this.teResult);
            this.layoutControl1.Controls.Add(this.gcCriterias);
            this.layoutControl1.Controls.Add(this.cmbInputTable);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(553, 190, 491, 350);
            this.layoutControl1.Size = new System.Drawing.Size(550, 450);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlGroup2,
            this.layoutControlItem3,
            this.layoutControlItem6,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(550, 450);
            this.layoutControlGroup1.Text = "Root";
            // 
            // cmbInputTable
            // 
            this.cmbInputTable.Location = new System.Drawing.Point(109, 12);
            this.cmbInputTable.Name = "cmbInputTable";
            this.cmbInputTable.Properties.Application = null;
            this.cmbInputTable.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbInputTable.Size = new System.Drawing.Size(429, 20);
            this.cmbInputTable.StyleController = this.layoutControl1;
            this.cmbInputTable.TabIndex = 4;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cmbInputTable;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(530, 24);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(93, 13);
            // 
            // gcCriterias
            // 
            this.gcCriterias.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcCriterias.DataSource = this.criteriaBindingSource;
            this.gcCriterias.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcCriterias.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcCriterias.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcCriterias.EmbeddedNavigator.Buttons.First.Visible = false;
            this.gcCriterias.EmbeddedNavigator.Buttons.Last.Visible = false;
            this.gcCriterias.EmbeddedNavigator.Buttons.Next.Visible = false;
            this.gcCriterias.EmbeddedNavigator.Buttons.NextPage.Visible = false;
            this.gcCriterias.EmbeddedNavigator.Buttons.Prev.Visible = false;
            this.gcCriterias.EmbeddedNavigator.Buttons.PrevPage.Visible = false;
            this.gcCriterias.EmbeddedNavigator.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.None;
            this.gcCriterias.EmbeddedNavigator.ButtonClick += new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(this.gcCriterias_EmbeddedNavigator_ButtonClick);
            this.gcCriterias.Location = new System.Drawing.Point(12, 36);
            this.gcCriterias.MainView = this.gvCriterias;
            this.gcCriterias.Name = "gcCriterias";
            this.gcCriterias.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1});
            this.gcCriterias.Size = new System.Drawing.Size(526, 255);
            this.gcCriterias.TabIndex = 5;
            this.gcCriterias.UseEmbeddedNavigator = true;
            this.gcCriterias.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCriterias});
            // 
            // criteriaBindingSource
            // 
            this.criteriaBindingSource.DataSource = typeof(SelectionObjects.Criteria);
            // 
            // gvCriterias
            // 
            this.gvCriterias.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colName,
            this.colExpression,
            this.colNominal,
            this.colCoefficient,
            this.gridColumn1});
            this.gvCriterias.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.gvCriterias.GridControl = this.gcCriterias;
            this.gvCriterias.Name = "gvCriterias";
            this.gvCriterias.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvCriterias.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gvCriterias.OptionsView.ShowGroupPanel = false;
            // 
            // colName
            // 
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            // 
            // colExpression
            // 
            this.colExpression.FieldName = "Expression";
            this.colExpression.Name = "colExpression";
            this.colExpression.Visible = true;
            this.colExpression.VisibleIndex = 1;
            // 
            // colNominal
            // 
            this.colNominal.FieldName = "Nominal";
            this.colNominal.Name = "colNominal";
            this.colNominal.Visible = true;
            this.colNominal.VisibleIndex = 2;
            // 
            // colCoefficient
            // 
            this.colCoefficient.FieldName = "Coefficient";
            this.colCoefficient.Name = "colCoefficient";
            this.colCoefficient.Visible = true;
            this.colCoefficient.VisibleIndex = 3;
            // 
            // gridColumn1
            // 
            this.gridColumn1.ColumnEdit = this.repositoryItemButtonEdit1;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 4;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Ред.", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repositoryItemButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit1_ButtonClick);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcCriterias;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(530, 259);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // phStrob
            // 
            this.phStrob.AllowHotTrack = false;
            this.phStrob.CustomizationFormText = "emptySpaceItem1";
            this.phStrob.Location = new System.Drawing.Point(0, 0);
            this.phStrob.Name = "emptySpaceItem1";
            this.phStrob.Size = new System.Drawing.Size(268, 100);
            this.phStrob.Text = "emptySpaceItem1";
            this.phStrob.TextSize = new System.Drawing.Size(0, 0);
            // 
            // phThreshold
            // 
            this.phThreshold.AllowHotTrack = false;
            this.phThreshold.CustomizationFormText = "emptySpaceItem2";
            this.phThreshold.Location = new System.Drawing.Point(268, 0);
            this.phThreshold.Name = "emptySpaceItem2";
            this.phThreshold.Size = new System.Drawing.Size(262, 100);
            this.phThreshold.Text = "emptySpaceItem2";
            this.phThreshold.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.phStrob,
            this.phThreshold});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 283);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(530, 100);
            this.layoutControlGroup2.Text = "layoutControlGroup2";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.cePrognosis;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(214, 383);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(183, 23);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // cePrognosis
            // 
            this.cePrognosis.Location = new System.Drawing.Point(226, 395);
            this.cePrognosis.Name = "cePrognosis";
            this.cePrognosis.Properties.Caption = "checkEdit1";
            this.cePrognosis.Size = new System.Drawing.Size(179, 19);
            this.cePrognosis.StyleController = this.layoutControl1;
            this.cePrognosis.TabIndex = 7;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.ceFollowName;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(397, 383);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(133, 23);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // ceFollowName
            // 
            this.ceFollowName.Location = new System.Drawing.Point(409, 395);
            this.ceFollowName.Name = "ceFollowName";
            this.ceFollowName.Properties.Caption = "checkEdit2";
            this.ceFollowName.Size = new System.Drawing.Size(129, 19);
            this.ceFollowName.StyleController = this.layoutControl1;
            this.ceFollowName.TabIndex = 8;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.ceIntermediate;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 383);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(214, 23);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // ceIntermediate
            // 
            this.ceIntermediate.Location = new System.Drawing.Point(12, 395);
            this.ceIntermediate.Name = "ceIntermediate";
            this.ceIntermediate.Properties.Caption = "checkEdit1";
            this.ceIntermediate.Size = new System.Drawing.Size(210, 19);
            this.ceIntermediate.StyleController = this.layoutControl1;
            this.ceIntermediate.TabIndex = 9;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.teResult;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 406);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(530, 24);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(93, 13);
            // 
            // teResult
            // 
            this.teResult.Location = new System.Drawing.Point(109, 418);
            this.teResult.Name = "teResult";
            this.teResult.Size = new System.Drawing.Size(429, 20);
            this.teResult.StyleController = this.layoutControl1;
            this.teResult.TabIndex = 6;
            // 
            // SelectionObjsUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SelectionObjsUserControl";
            this.Size = new System.Drawing.Size(550, 450);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbInputTable.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCriterias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.criteriaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCriterias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phStrob)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cePrognosis.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceFollowName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceIntermediate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teResult.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomEditor.TableComboBox cmbInputTable;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gcCriterias;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCriterias;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem phStrob;
        private DevExpress.XtraLayout.EmptySpaceItem phThreshold;
        private DevExpress.XtraEditors.TextEdit teResult;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.CheckEdit ceFollowName;
        private DevExpress.XtraEditors.CheckEdit cePrognosis;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.CheckEdit ceIntermediate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colExpression;
        private DevExpress.XtraGrid.Columns.GridColumn colNominal;
        private DevExpress.XtraGrid.Columns.GridColumn colCoefficient;
        private System.Windows.Forms.BindingSource criteriaBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
    }
}
