﻿using Presantation.ViewModel;
using System;
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
using System.Windows.Shapes;

namespace Presantation.View
{
    /// <summary>
    /// Interaction logic for AddProjectView.xaml
    /// </summary>
    public partial class AddProjectView : Window , IDisposable
    {
        public AddProjectViewModel addProjectViewModel;
        public AddProjectView()
        {
            addProjectViewModel = new AddProjectViewModel(this);
            InitializeComponent();

            this.DataContext = addProjectViewModel;
        }
        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
        }
    }
}
