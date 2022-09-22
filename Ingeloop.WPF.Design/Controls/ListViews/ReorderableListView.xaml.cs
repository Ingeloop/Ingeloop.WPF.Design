﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ingeloop.WPF.Design
{
    /// <summary>
    /// Interaction logic for ReorderableListView.xaml
    /// </summary>
    public partial class ReorderableListView : ListView
    {
        public ReorderableListView()
        {
            InitializeComponent();
            new ListViewDragDropManager(this);
        }
    }
}