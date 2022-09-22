using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Ingeloop.WPF.Design
{
	public class MouseUtilities
	{
		[StructLayout(LayoutKind.Sequential)]
		private struct Win32Point
		{
			public Int32 X;
			public Int32 Y;
		};

		[DllImport("user32.dll")]
		private static extern bool GetCursorPos(ref Win32Point pt);

		[DllImport("user32.dll")]
		private static extern bool ScreenToClient(IntPtr hwnd, ref Win32Point pt);

		public static Point GetMousePosition(Visual relativeTo)
		{
			Win32Point mouse = new Win32Point();
			GetCursorPos(ref mouse);

			return relativeTo.PointFromScreen(new Point((double)mouse.X, (double)mouse.Y));
		}
	}
}
