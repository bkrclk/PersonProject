using Data.Models;
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
    /// Interaction logic for UpdateUserView.xaml
    /// </summary>
    public partial class UpdateUserView : Window, IDisposable
    {
        public  UpdateUserViewModel updateUserViewModel;
        public UpdateUserView(User user)
        {
            updateUserViewModel = new UpdateUserViewModel(this,  user);
            InitializeComponent();

            this.DataContext = updateUserViewModel;
        }
        
        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
        }
    }
}
