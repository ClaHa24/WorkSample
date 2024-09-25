using System.Windows;
using WorkSample.Client.ViewModels;

namespace WorkSample.Client
{
    /// <summary>
    /// Interaction logic for EditPerson.xaml
    /// </summary>
    public partial class EditPerson : Window
    {
        public EditPerson(int? id)
        {
            InitializeComponent();
            DataContext = new EditPersonViewModel(id);
        }
    }
}
