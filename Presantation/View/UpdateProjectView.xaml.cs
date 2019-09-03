using Presantation.ViewModel;
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
    /// Interaction logic for UpdateProjectView.xaml
    /// </summary>
    public partial class UpdateProjectView : Window , IDisposable
    {
        public UpdateProjectViewModel updateProjectViewModel;
        public UpdateProjectView()
        {
            updateProjectViewModel = new UpdateProjectViewModel(this);
            InitializeComponent();

            this.DataContext = updateProjectViewModel;
        }

        
        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
        }

    }
}
