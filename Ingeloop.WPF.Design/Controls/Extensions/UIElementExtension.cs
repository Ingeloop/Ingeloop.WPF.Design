using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace Ingeloop.WPF.Design
{
    internal static class UIElementExtensions
    {
        public static void AddAdorner<TAdorner>(this UIElement element, TAdorner adorner) where TAdorner : Adorner
        {
            if (adorner is null)
            {
                throw new ArgumentNullException(nameof(adorner));
            }

            var adornerLayer = AdornerLayer.GetAdornerLayer(element)
                ?? throw new InvalidOperationException("This element has no adorner layer.");

            adornerLayer.Add(adorner);
        }

        public static void RemoveAdorners<TAdorner>(this UIElement element) where TAdorner : Adorner
        {
            var adornerLayer = AdornerLayer.GetAdornerLayer(element);
            var adorners = adornerLayer?.GetAdorners(element);

            if (adorners is null)
            {
                return;
            }

            foreach (var adorner in adorners.OfType<TAdorner>())
            {
                if (adorner != null)
                {
                    adornerLayer.Remove(adorner);
                }
            }
        }
    }
}
