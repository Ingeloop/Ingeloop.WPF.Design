using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace Ingeloop.WPF.Design
{
	public class DragAdorner : Adorner
	{
		private UIElement child = null;
		private double offsetLeft = 0;
		private double offsetTop = 0;

		public DragAdorner(UIElement adornedElement, Size size, Brush brush)
			: base(adornedElement)
		{
			Rectangle rect = new Rectangle();
			rect.Fill = brush;
			rect.Width = size.Width;
			rect.Height = size.Height;
			rect.IsHitTestVisible = false;

			Border border = new Border();
			border.CornerRadius = new CornerRadius(0);
			border.Width = size.Width;
			border.Height = size.Height;
			border.Background = brush;
			border.Effect = new DropShadowEffect
			{
				ShadowDepth = 0,
				BlurRadius = 15,
				Color = Color.FromRgb(150, 150, 150)
			};

			Grid grid = new Grid();
			grid.Width = size.Width + 4;
			grid.Height = size.Height + 5;
			grid.IsHitTestVisible = false;

			grid.Children.Add(border);
			grid.Children.Add(rect);

			this.child = grid;
		}

		public override GeneralTransform GetDesiredTransform(GeneralTransform transform)
		{
			GeneralTransformGroup result = new GeneralTransformGroup();
			result.Children.Add(base.GetDesiredTransform(transform));
			result.Children.Add(new TranslateTransform(this.offsetLeft, this.offsetTop));
			return result;
		}

		public double OffsetLeft
		{
			get { return this.offsetLeft; }
			set
			{
				this.offsetLeft = value;
				UpdateLocation();
			}
		}

		public void SetOffsets(double left, double top)
		{
			this.offsetLeft = left;
			this.offsetTop = top;
			this.UpdateLocation();
		}

		public double OffsetTop
		{
			get { return this.offsetTop; }
			set
			{
				this.offsetTop = value;
				UpdateLocation();
			}
		}

		protected override Size MeasureOverride(Size constraint)
		{
			this.child.Measure(constraint);
			return this.child.DesiredSize;
		}

		protected override Size ArrangeOverride(Size finalSize)
		{
			this.child.Arrange(new Rect(finalSize));
			return finalSize;
		}

		protected override Visual GetVisualChild(int index)
		{
			return this.child;
		}

		protected override int VisualChildrenCount
		{
			get { return 1; }
		}

		private void UpdateLocation()
		{
			AdornerLayer adornerLayer = this.Parent as AdornerLayer;
			if (adornerLayer != null)
				adornerLayer.Update(this.AdornedElement);
		}
	}
}
