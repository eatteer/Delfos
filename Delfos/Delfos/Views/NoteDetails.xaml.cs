using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Delfos.Models;
using Delfos.ViewModels;

namespace Delfos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteDetails : ContentPage
    {
        public NoteDetails()
        {
            InitializeComponent();
        }

        public NoteDetails(int id)
        {
            InitializeComponent();
            BindingContext = new NoteDetailsViewModel(id);
        }

        protected override void OnAppearing()
        {
            NoteDetailsViewModel noteDetailsViewModel = BindingContext as NoteDetailsViewModel;
            noteDetailsViewModel.LoadNote();
            base.OnAppearing();
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            NoteDetailsViewModel noteDetailsViewModel = BindingContext as NoteDetailsViewModel;
            Note note = noteDetailsViewModel.Note;
            await Application.Current.MainPage.Navigation.PushAsync(new NoteEdition(note));
        }
    }
}