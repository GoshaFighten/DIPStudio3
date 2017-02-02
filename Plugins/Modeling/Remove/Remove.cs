using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioUICore;
using DIPStudioCore;

namespace Remove
{
    public class Remove : Plugin <RemoveSettings, RemoveUserControl>
    {
        public Remove()
            : base(ProjectKind.Modeling, "Удаление", "[УД]")
        {
        }
        protected override void Run(int t, int index, int tFinish)
        {
            if (t == tFinish)
            {
                RemoveSettings settings = this.PluginSettings;
                DIPApplication application = DIPApplication.GetInstance();
                if (settings.InputSeries != String.Empty)
                {
                    Series inputSeries = application.GetSeriesByName( settings.InputSeries);
                    application.RemoveSeries(inputSeries);
                    
                }
                if (settings.InputTable != String.Empty)
                {
                    DIPStudioCore.Data.Table inputTable = application.GetTableByName(ShortName + settings.InputTable);
                    application.RemoveTable(inputTable);
                }                

            }
        }

        protected override string GetHelpString()
        {
            return "Отчищает оперативную память от промежуточных результатов работы плагинов. выберите что хотите удалить и плагин будет на каждом шагу полность удалять данные или изображения";
        }
    }
}
