using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections.Generic;
using System.Drawing;

namespace LocalizationStorage {
    public class FilterPanel {
        List<FilterPanelItem> items = new List<FilterPanelItem>();
        GridView view;
        Bar bar;
        public FilterPanel(GridView view, Bar bar) {
            this.view = view;
            this.bar = bar;
            view.ColumnFilterChanged += (s, e) =>
                items.ForEach(item => item.SetDownState(view.ActiveFilterString));
        }
        public void AddFilterItem(string caption, string filter, string tooltip = null, string image = null) {
            items.Add(new FilterPanelItem(view, bar, caption, filter, tooltip, image));
        }
    }
    internal class FilterPanelItem {
        BarButtonItem buttonItem = null;
        string filterString;
        public FilterPanelItem(GridView view, Bar bar, string caption, string filterString, string description, string image) {
            this.filterString = filterString;
            buttonItem = new BarButtonItem();
            buttonItem.ButtonStyle = BarButtonStyle.Check;
            buttonItem.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            buttonItem.Caption = caption;
            buttonItem.PaintStyle = BarItemPaintStyle.CaptionGlyph;
            //buttonItem.ItemAppearance.Normal.BackColor = Color.LightGray;
            if(!string.IsNullOrEmpty(image))
                buttonItem.ImageOptions.ImageUri = image;
            if(!string.IsNullOrEmpty(description)) {
                SuperToolTip toolTip = new SuperToolTip();
                toolTip.Items.Add(description);
                buttonItem.SuperTip = toolTip;
            }
            buttonItem.ItemClick += (s, e) => {
                if(buttonItem.Down)
                    view.ActiveFilterCriteria = CriteriaOperator.Parse(filterString);
                else view.ActiveFilterCriteria = null;
            };
            bar.AddItem(buttonItem);
        }
        public void SetDownState(string filter) {
            buttonItem.Down = filterString.Equals(filter);
        }
    }
}
