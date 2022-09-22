using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ingeloop.WPF.Design
{
	public static class ListViewItemDragState
	{
		public static readonly DependencyProperty IsBeingDraggedProperty = DependencyProperty.RegisterAttached("IsBeingDragged", typeof(bool),
				typeof(ListViewItemDragState), new UIPropertyMetadata(false));

		public static bool GetIsBeingDragged(ListViewItem item)
		{
			return (bool)item.GetValue(IsBeingDraggedProperty);
		}

		internal static void SetIsBeingDragged(ListViewItem item, bool value)
		{
			item.SetValue(IsBeingDraggedProperty, value);
		}

		public static readonly DependencyProperty IsUnderDragCursorProperty = DependencyProperty.RegisterAttached("IsUnderDragCursor", typeof(bool),
				typeof(ListViewItemDragState), new UIPropertyMetadata(false));

		public static bool GetIsUnderDragCursor(ListViewItem item)
		{
			return (bool)item.GetValue(IsUnderDragCursorProperty);
		}

		internal static void SetIsUnderDragCursor(ListViewItem item, bool value)
		{
			item.SetValue(IsUnderDragCursorProperty, value);
		}
	}
}
