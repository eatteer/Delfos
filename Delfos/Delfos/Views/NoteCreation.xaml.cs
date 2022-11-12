using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Delfos.ViewModels;

namespace Delfos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteCreation : ContentPage
    {
        public NoteCreation()
        {
            InitializeComponent();
            BindingContext = new NoteCreationViewModel();
        }
    }
}