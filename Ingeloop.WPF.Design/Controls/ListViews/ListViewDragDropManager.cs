using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Ingeloop.WPF.Design
{
	public class ListViewDragDropManager
	{
		bool canInitiateDrag;
		DragAdorner dragAdorner;
		int indexToSelect;
		bool isDragInProgress;
		ListView listView;
		object itemUnderDragCursor;
		Point ptMouseDown;
		bool showDragAdorner;

		public double DragAdornerOpacity { get; set; }
		public bool AutoReorder { get; set; }

		bool CanStartDragOperation
		{
			get
			{
				if (Mouse.LeftButton != MouseButtonState.Pressed)
					return false;

				if (!this.canInitiateDrag)
					return false;

				if (this.indexToSelect == -1)
					return false;

				if (!this.HasCursorLeftDragThreshold)
					return false;

				return true;
			}
		}

		bool HasCursorLeftDragThreshold
		{
			get
			{
				if (this.indexToSelect < 0)
					return false;

				ListViewItem item = this.GetListViewItem(this.indexToSelect);
				Rect bounds = VisualTreeHelper.GetDescendantBounds(item);
				Point ptInItem = this.listView.TranslatePoint(this.ptMouseDown, item);

				double topOffset = Math.Abs(ptInItem.Y);
				double btmOffset = Math.Abs(bounds.Height - ptInItem.Y);
				double vertOffset = Math.Min(topOffset, btmOffset);

				double width = SystemParameters.MinimumHorizontalDragDistance * 2;
				double height = Math.Min(SystemParameters.MinimumVerticalDragDistance, vertOffset) * 2;
				Size szThreshold = new Size(width, height);

				Rect rect = new Rect(this.ptMouseDown, szThreshold);
				rect.Offset(szThreshold.Width / -2, szThreshold.Height / -2);
				Point ptInListView = MouseUtilities.GetMousePosition(this.listView);
				return !rect.Contains(ptInListView);
			}
		}

		int IndexUnderDragCursor
		{
			get
			{
				int index = -1;
				for (int i = 0; i < this.listView.Items.Count; ++i)
				{
					ListViewItem item = this.GetListViewItem(i);
					if (this.IsMouseOver(item))
					{
						index = i;
						break;
					}
				}
				return index;
			}
		}

		public ListViewDragDropManager()
		{
			this.canInitiateDrag = false;
			this.indexToSelect = -1;
			this.showDragAdorner = true;

			DragAdornerOpacity = 0.9;
			AutoReorder = false;
		}

		public ListViewDragDropManager(ListView listView) : this()
		{
			this.ListView = listView;
		}

		public bool IsDragInProgress
		{
			get { return this.isDragInProgress; }
			private set { this.isDragInProgress = value; }
		}

		public ListView ListView
		{
			get { return listView; }
			set
			{
				if (this.IsDragInProgress)
					throw new InvalidOperationException("Cannot set the ListView property during a drag operation.");

				if (this.listView != null)
				{
					this.listView.PreviewMouseLeftButtonDown -= ListView_PreviewMouseLeftButtonDown;
					this.listView.PreviewMouseMove -= ListView_PreviewMouseMove;
					this.listView.DragOver -= ListView_DragOver;
					this.listView.DragLeave -= ListView_DragLeave;
					this.listView.DragEnter -= ListView_DragEnter;
					this.listView.Drop -= ListView_Drop;
				}

				this.listView = value;

				if (this.listView != null)
				{
					if (!this.listView.AllowDrop) this.listView.AllowDrop = true;

					this.listView.PreviewMouseLeftButtonDown += ListView_PreviewMouseLeftButtonDown;
					this.listView.PreviewMouseMove += ListView_PreviewMouseMove;
					this.listView.DragOver += ListView_DragOver;
					this.listView.DragLeave += ListView_DragLeave;
					this.listView.DragEnter += ListView_DragEnter;
					this.listView.Drop += ListView_Drop;
				}
			}
		}

		public bool ShowDragAdorner
		{
			get { return this.showDragAdorner; }
			set
			{
				if (this.IsDragInProgress)
					throw new InvalidOperationException("Cannot set the ShowDragAdorner property during a drag operation.");

				this.showDragAdorner = value;
			}
		}

		void ListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (this.IsMouseOverScrollbar)
			{
				this.canInitiateDrag = false;
				return;
			}

			int index = this.IndexUnderDragCursor;
			this.canInitiateDrag = index > -1;

			if (this.canInitiateDrag)
			{
				this.ptMouseDown = MouseUtilities.GetMousePosition(this.listView);
				this.indexToSelect = index;
			}
			else
			{
				this.ptMouseDown = new Point(-10000, -10000);
				this.indexToSelect = -1;
			}
		}

		void ListView_PreviewMouseMove(object sender, MouseEventArgs e)
		{
			if (!this.CanStartDragOperation)
				return;

			if (this.listView.SelectedIndex != this.indexToSelect)
				this.listView.SelectedIndex = this.indexToSelect;

			if (this.listView.SelectedItem == null)
				return;

			ListViewItem itemToDrag = this.GetListViewItem(this.listView.SelectedIndex);
			if (itemToDrag == null)
				return;

			AdornerLayer adornerLayer = this.ShowDragAdornerResolved ? this.InitializeAdornerLayer(itemToDrag) : null;

			this.InitializeDragOperation(itemToDrag);
			this.PerformDragOperation();
			this.FinishDragOperation(itemToDrag, adornerLayer);
		}

		void ListView_DragOver(object sender, DragEventArgs e)
		{
			e.Effects = DragDropEffects.Move;

			if (this.ShowDragAdornerResolved)
				this.UpdateDragAdornerLocation();

			int hoverIndex = this.IndexUnderDragCursor;

			this.ItemUnderDragCursor = hoverIndex < 0 ? null : this.ListView.Items[hoverIndex];

			if (AutoReorder && listView.SelectedIndex != hoverIndex)
			{
				object data = e.Data.GetData(listView.Items[0].GetType());
				Reorder(data, true);
			}
		}

		void ListView_DragLeave(object sender, DragEventArgs e)
		{
			if (!this.IsMouseOver(this.listView))
			{
				if (this.ItemUnderDragCursor != null)
					this.ItemUnderDragCursor = null;

				if (this.dragAdorner != null)
					this.dragAdorner.Visibility = Visibility.Collapsed;
			}
		}

		void ListView_DragEnter(object sender, DragEventArgs e)
		{
			if (this.dragAdorner != null && this.dragAdorner.Visibility != Visibility.Visible)
			{
				this.UpdateDragAdornerLocation();
				this.dragAdorner.Visibility = Visibility.Visible;
			}
		}

		void ListView_Drop(object sender, DragEventArgs e)
		{
			if (!AutoReorder)
			{
				object data = e.Data.GetData(listView.Items[0].GetType());
				Reorder(data);
			}
		}

		private void Reorder(object data, bool autoReorder = false)
		{
			if (this.ItemUnderDragCursor != null)
				this.ItemUnderDragCursor = null;

			if (data == null)
				return;

			List<object> itemsSourceAsObject = this.listView.ItemsSource.Cast<object>().ToList();

			int oldIndex = itemsSourceAsObject.IndexOf(data);
			int newIndex = this.IndexUnderDragCursor;

			if (newIndex < 0)
			{
				if (itemsSourceAsObject.Count == 0)
					newIndex = 0;

				else if (oldIndex < 0)
					newIndex = itemsSourceAsObject.Count;
				else
					return;
			}

			if (oldIndex == newIndex)
				return;

			if (oldIndex > -1)
			{
				this.listView.ItemsSource.GetType()?.GetMethod("Move")?.Invoke(this.listView.ItemsSource, new object[] { oldIndex, newIndex });
				//itemsSource.Move(oldIndex, newIndex);
			}
			else
			{
				this.listView.ItemsSource.GetType()?.GetMethod("Insert")?.Invoke(this.listView.ItemsSource, new object[] { newIndex, data });
				//itemsSource.Insert(newIndex, data);
			}
		}

		void FinishDragOperation(ListViewItem draggedItem, AdornerLayer adornerLayer)
		{
			ListViewItemDragState.SetIsBeingDragged(draggedItem, false);

			this.IsDragInProgress = false;

			if (this.ItemUnderDragCursor != null)
				this.ItemUnderDragCursor = null;

			if (adornerLayer != null)
			{
				adornerLayer.Remove(this.dragAdorner);
				this.dragAdorner = null;
			}
		}

		ListViewItem GetListViewItem(int index)
		{
			if (this.listView.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
				return null;

			return this.listView.ItemContainerGenerator.ContainerFromIndex(index) as ListViewItem;
		}

		ListViewItem GetListViewItem(object dataItem)
		{
			if (this.listView.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
				return null;

			return this.listView.ItemContainerGenerator.ContainerFromItem(dataItem) as ListViewItem;
		}

		AdornerLayer InitializeAdornerLayer(ListViewItem itemToDrag)
		{
			VisualBrush brush = new VisualBrush(itemToDrag);
			this.dragAdorner = new DragAdorner(this.listView, itemToDrag.RenderSize, brush);
			this.dragAdorner.Opacity = this.DragAdornerOpacity;
			AdornerLayer layer = AdornerLayer.GetAdornerLayer(this.listView);
			layer.Add(dragAdorner);
			this.ptMouseDown = MouseUtilities.GetMousePosition(this.listView);

			return layer;
		}

		void InitializeDragOperation(ListViewItem itemToDrag)
		{
			this.IsDragInProgress = true;
			this.canInitiateDrag = false;
			ListViewItemDragState.SetIsBeingDragged(itemToDrag, true);
		}

		bool IsMouseOver(Visual target)
		{
			if (target == null) return false;
			Rect bounds = VisualTreeHelper.GetDescendantBounds(target);
			Point mousePos = MouseUtilities.GetMousePosition(target);
			return bounds.Contains(mousePos);
		}

		bool IsMouseOverScrollbar
		{
			get
			{
				Point ptMouse = MouseUtilities.GetMousePosition(this.listView);
				HitTestResult res = VisualTreeHelper.HitTest(this.listView, ptMouse);
				if (res == null)
					return false;

				DependencyObject depObj = res.VisualHit;
				while (depObj != null)
				{
					if (depObj is ScrollBar)
						return true;

					if (depObj is Visual || depObj is System.Windows.Media.Media3D.Visual3D)
						depObj = VisualTreeHelper.GetParent(depObj);
					else
						depObj = LogicalTreeHelper.GetParent(depObj);
				}

				return false;
			}
		}

		object ItemUnderDragCursor
		{
			get { return this.itemUnderDragCursor; }
			set
			{
				if (this.itemUnderDragCursor == value)
					return;

				for (int i = 0; i < 2; ++i)
				{
					if (i == 1)
						this.itemUnderDragCursor = value;

					if (this.itemUnderDragCursor != null)
					{
						ListViewItem listViewItem = this.GetListViewItem(this.itemUnderDragCursor);
						if (listViewItem != null)
						{
							ListViewItemDragState.SetIsUnderDragCursor(listViewItem, i == 1);
						}
					}
				}
			}
		}

		void PerformDragOperation()
		{
			object selectedItem = this.listView.SelectedItem;
			DragDropEffects allowedEffects = DragDropEffects.Move | DragDropEffects.Move | DragDropEffects.Link;
			if (DragDrop.DoDragDrop(this.listView, selectedItem, allowedEffects) != DragDropEffects.None)
			{
				this.listView.SelectedItem = selectedItem;
			}
		}

		bool ShowDragAdornerResolved
		{
			get { return this.ShowDragAdorner && this.DragAdornerOpacity > 0.0; }
		}

		void UpdateDragAdornerLocation()
		{
			if (this.dragAdorner != null)
			{
				Point ptCursor = MouseUtilities.GetMousePosition(this.ListView);

				//double left = ptCursor.X - this.ptMouseDown.X;
				double left = 0;
				ListViewItem itemBeingDragged = this.GetListViewItem(this.indexToSelect);
				Point itemLoc = itemBeingDragged.TranslatePoint(new Point(0, 0), this.ListView);
				double top = itemLoc.Y + ptCursor.Y - this.ptMouseDown.Y;

				this.dragAdorner.SetOffsets(left, top);
			}
		}
	}
}
