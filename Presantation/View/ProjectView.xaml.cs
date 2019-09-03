using Presantation.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
    /// Interaction logic for ProjectView.xaml
    /// </summary>
    public partial class ProjectView : Window ,IDisposable
    {


        public  ProjectViewModel projectViewModel;

        public ProjectView()
        {
            
            projectViewModel = new ProjectViewModel(this);
            InitializeComponent();
        
            this.DataContext = projectViewModel;

           
        }

        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
        }

       
    }
}
