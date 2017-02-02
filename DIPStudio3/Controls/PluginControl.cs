using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DIPStudioUICore;
using DIPStudioCore;

namespace DIPStudio3.Controls {
    public partial class PluginControl : BaseUserControl {
        public PluginControl() {
            InitializeComponent();
            nbgModeling.Caption = "Моделирование изображений";
            nbgModeling.SmallImage = DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/additem_16x16.png");
            nbgModeling.LargeImage = DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/additem_32x32.png");
            nbgModeling.GroupCaptionUseImage = DevExpress.XtraNavBar.NavBarImage.Large;
            nbgProcessing.Caption = "Обработка изображений";
            nbgProcessing.SmallImage = DevExpress.Images.ImageResourceCache.Default.GetImage("images/programming/ide_16x16.png");
            nbgProcessing.LargeImage = DevExpress.Images.ImageResourceCache.Default.GetImage("images/programming/ide_32x32.png");
            nbgProcessing.GroupCaptionUseImage = DevExpress.XtraNavBar.NavBarImage.Large;
            nbgSelection.Caption = "Селекция";
            nbgSelection.SmallImage = DevExpress.Images.ImageResourceCache.Default.GetImage("images/chart/line_16x16.png");
            nbgSelection.LargeImage = DevExpress.Images.ImageResourceCache.Default.GetImage("images/chart/line_32x32.png");
            nbgSelection.GroupCaptionUseImage = DevExpress.XtraNavBar.NavBarImage.Large;
            nbgView.Caption = "Представление результатов";
            nbgView.SmallImage = DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/show_16x16.png");
            nbgView.LargeImage = DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/show_32x32.png");
            nbgView.GroupCaptionUseImage = DevExpress.XtraNavBar.NavBarImage.Large;
        }

        public List<IPlugin> Plugins {
            set {
                foreach(IPlugin plugin in value) {
                    DevExpress.XtraNavBar.NavBarItemLink item;
                    switch(plugin.Kind) {
                        case ProjectKind.Modeling:
                            item = nbgModeling.AddItem();
                            break;
                        case ProjectKind.Processing:
                            item = nbgProcessing.AddItem();
                            break;
                        case ProjectKind.Selection:
                            item = nbgSelection.AddItem();
                            break;
                        case ProjectKind.View:
                            item = nbgView.AddItem();
                            break;
                        default:
                            throw new DIPStudiogException("Unexpected plugin type");
                    }
                    item.Item.SmallImage = DevExpress.Images.ImageResourceCache.Default.GetImage("images/reports/addfooter_16x16.png");
                    item.Item.Tag = plugin.Key;
                    item.Item.Caption = plugin.ShortName + plugin.Caption;
                }
            }
        }

        private void navBarControl1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e) {
            string plugin = (string)e.Link.Item.Tag;
            RaisePluginChosen(plugin);
        }

        private void RaisePluginChosen(string plugin) {
            if(PluginChosen != null) {
                PluginChosen(this, new PluginEventArgs() { Key = plugin
                });
            }
        }

        public event EventHandler<PluginEventArgs> PluginChosen;
    }
}
