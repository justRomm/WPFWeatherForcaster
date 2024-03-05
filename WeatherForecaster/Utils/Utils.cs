using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace WeatherForecaster.Utils
{
    public static class Utils
    {
        public static bool TryFindVisualChildByName<TChild>(
            this DependencyObject parent,
            string childElementName,
            out TChild childElement,
            bool isCaseSensitive = false)
            where TChild : FrameworkElement
        {
            childElement = null;

            // Popup.Child content is not part of the visual tree.
            // To prevent traversal from breaking when parent is a Popup,
            // we need to explicitly extract the content.
            if (parent is Popup popup)
            {
                parent = popup.Child;
            }

            if (parent == null)
            {
                return false;
            }

            var stringComparison = isCaseSensitive
                ? StringComparison.Ordinal
                : StringComparison.OrdinalIgnoreCase;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is TChild resultElement
                    && resultElement.Name.Equals(childElementName, stringComparison))
                {
                    childElement = resultElement;
                    return true;
                }

                if (child.TryFindVisualChildByName(childElementName, out childElement))
                {
                    return true;
                }
            }

            return false;
        }


        public static T FindControl<T>(UIElement parent, Type targetType, string ControlName) where T : FrameworkElement
        {
            if (parent == null)
            {
                return null;
            }

            if (parent.GetType() == targetType && ((T)parent).Name == ControlName)
            {
                return (T)parent;
            }

            T result = null;
            int count = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < count; i++)
            {
                UIElement child = (UIElement)VisualTreeHelper.GetChild(parent, i);

                if (FindControl<T>(child, targetType, ControlName) != null)
                {
                    result = FindControl<T>(child, targetType, ControlName);
                    break;
                }
            }

            return result;
        }

    }
}
