﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace Cloud_In_A_Box.Components.Areas.Components
{
    public class AutoTableComponentBase<T> : ComponentBase
    {
        [Parameter]
        protected string TableClass { get; set; } = "table table-striped";

        [Parameter]
        protected string SelectedClass { get; set; } = "bg-primary text-white";

        [Parameter]
        protected RenderFragment TableHeader { get; set; }

        [Parameter]
        protected RenderFragment<T> RowTemplate { get; set; }

        [Parameter]
        protected RenderFragment<T> ExpandedRowTemplate { get; set; }

        [Parameter]
        protected RenderFragment TableFooter { get; set; }

        [Parameter]
        protected IReadOnlyList<T> Items { get; set; }

        [Parameter]
        protected bool Selectable { get; set; } = true;

        [Parameter]
        protected bool Expandable { get; set; } = true;

        [Parameter]
        protected bool MultiSelect { get; set; } = false;

        [Parameter]
        protected bool MultiExpand { get; set; } = false;

        [Parameter]
        public IList<T> SelectedItems { get; private set; } = new List<T>();

        [Parameter]
        public IList<T> ExpandedItems { get; private set; } = new List<T>();

        public Action<T> ItemSelected { get; set; }
        public Action<T> ItemExpanded { get; set; }

        public void SelectItem(T item)
        {
            if (!Selectable) return;
            if (SelectedItems.Contains(item))
            {
                SelectedItems.Remove(item);
            }
            else
            {
                if (!MultiSelect)
                    SelectedItems = new List<T>();
                SelectedItems.Add(item);
            }
            ItemSelected?.Invoke(item);
        }

        public void ExpandItem(T item)
        {
            if (!Expandable) return;
            if (ExpandedItems.Contains(item))
            {
                ExpandedItems.Remove(item);
            }
            else
            {
                if (!MultiExpand)
                    ExpandedItems = new List<T>();
                ExpandedItems.Add(item);
            }
            ItemExpanded?.Invoke(item);
        }

        public bool IsExpanded(T item)
        {
            return ExpandedItems.Contains(item);
        }

        public bool IsSelected(T item)
        {
            return SelectedItems.Contains(item);
        }

        public string GetRowClass(T item)
        {
            return IsSelected(item) ? SelectedClass : "";
        }

        public void ExpandAll()
        {
            if (!MultiExpand) return;
            ExpandedItems = Items.ToList();
        }

        public void CollapseAll()
        {
            ExpandedItems = new List<T>();
        }

        public void DeselectAll()
        {
            SelectedItems = new List<T>();
        }

        public void SelectAll()
        {
            if (!MultiSelect) return;
            SelectedItems = Items.ToList();
        }
    }
}