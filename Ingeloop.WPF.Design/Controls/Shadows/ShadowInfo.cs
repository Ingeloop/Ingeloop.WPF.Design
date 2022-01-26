﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Effects;

namespace Ingeloop.WPF.Design
{
    internal static class ShadowInfo
    {
        private static readonly IDictionary<ShadowDepth, DropShadowEffect> ShadowsDictionary;

        static ShadowInfo()
        {
            var resourceDictionary = new ResourceDictionary { Source = new Uri("pack://application:,,,/Ingeloop.WPF.Design;component/Styles/Shadows.xaml", UriKind.Absolute) };

            ShadowsDictionary = new Dictionary<ShadowDepth, DropShadowEffect>
            {
                { ShadowDepth.Depth0, null },
                { ShadowDepth.Depth1, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth1"] },
                { ShadowDepth.Depth2, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth2"] },
                { ShadowDepth.Depth3, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth3"] },
                { ShadowDepth.Depth4, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth4"] },
                { ShadowDepth.Depth5, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth5"] },
            };
        }

        public static DropShadowEffect GetDropShadow(ShadowDepth depth)
        {
            return ShadowsDictionary[depth];
        }
    }
}