using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delfos.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Delfos.ViewModels;

namespace Delfos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteEdition : ContentPage
    {
        public NoteEdition()
        {
            InitializeComponent();
        }

        public NoteEdition(Note note)
        {
            InitializeComponent();
            BindingContext = new NoteEditionViewModel(note);
        }
    }
}