using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Delfos.ViewModels;
using Delfos.Models;

namespace Delfos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel();
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Note note = e.SelectedItem as Note;
            await Application.Current.MainPage.Navigation.PushAsync(new NoteDetails(note.Id));
        }

        protected override void OnAppearing()
        {
            HomeViewModel homeViewModel = BindingContext as HomeViewModel;
            homeViewModel.LoadNotes();
            base.OnAppearing();
        }
    }
}