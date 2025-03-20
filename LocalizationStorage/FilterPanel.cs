using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;

namespace LocalizationStorage {
    public class FilterPanel {
        readonly List<FilterPanelItem> items = [];
        readonly GridView view;
        readonly Bar bar;
        BarEditItem filterEx = null;
        public FilterPanel(GridView view, Bar bar) {
            this.view = view;
            this.bar = bar;
            view.ColumnFilterChanged += (s, e) =>
                items.ForEach(item => item.SetDownState(view.ActiveFilterString));
        }
        public void AddFilterItem(string caption, string filter, string tooltip = null, string image = null, bool simpleFilter = false) {
            items.Add(new FilterPanelItem(view, bar, caption, filter, tooltip, image, simpleFilter, this));
        }
        public void AddFilterEx(List<Tuple<string, string>> filterItems, int focusItemIndex = 0) {
            if(filterEx != null) return;
            filterEx = new BarEditItem();
            var edit = new RepositoryItemImageComboBox();
            filterEx.Edit = edit;
            filterEx.EditWidth = 150;
            filterItems.ForEach(i => edit.Items.Add(i.Item2, i.Item1, -1));
            bar.ItemLinks.Add(filterEx, true);
            filterEx.EditValue = filterItems[focusItemIndex].Item1;
            edit.SelectedIndexChanged += (s, e) =>
               items.ForEach(item => item.RefreshFilter(view));
        }
        public string FilterEx => $"{filterEx?.EditValue}";
    }
    internal class FilterPanelItem {
        readonly BarButtonItem buttonItem = null;
        readonly string filterString = string.Empty;
        readonly FilterPanel filterPanel = null;
        readonly bool simpleFilter = false;
        public FilterPanelItem(GridView view, Bar bar, string caption, string filterString, string description, string image, bool simpleFilter, FilterPanel panel) {
            this.filterString = filterString;
            this.simpleFilter = simpleFilter;
            filterPanel = panel;
            buttonItem = new BarButtonItem {
                ButtonStyle = BarButtonStyle.Check,
                AllowHtmlText = DevExpress.Utils.DefaultBoolean.True,
                Caption = caption,
                PaintStyle = BarItemPaintStyle.CaptionGlyph
            };
            //buttonItem.ItemAppearance.Normal.BackColor = Color.LightGray;
            if(!string.IsNullOrEmpty(image))
                buttonItem.ImageOptions.ImageUri = image;
            if(!string.IsNullOrEmpty(description)) {
                SuperToolTip toolTip = new();
                toolTip.Items.Add(description);
                buttonItem.SuperTip = toolTip;
            }
            buttonItem.ItemClick += (s, e) => RefreshFilter(view, true);
            bar.AddItem(buttonItem);
        }
        public void RefreshFilter(GridView view, bool forceClear = false) {
            if(buttonItem.Down)
                view.ActiveFilterCriteria = CriteriaOperator.Parse(FilterString);
            else
                if(forceClear) view.ActiveFilterCriteria = null;
        }
        public bool Down => buttonItem.Down;
        string FilterString {
            get {
                if(string.IsNullOrEmpty(filterPanel.FilterEx) || simpleFilter) return filterString;
                return $"{filterString} And {filterPanel.FilterEx}";
            }
        }
        public void SetDownState(string filter) {
            buttonItem.Down = FilterString.Equals(filter) || FilterString.IndexOf(filter) == 1;
        }
    }
}
