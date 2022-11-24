using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Delfos.Models;
using Delfos.Views;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace Delfos.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        #region Attributes
        private object notes;
        #endregion

        #region Properties
        public object Notes
        {
            get { return this.notes; }
            set { SetValue(ref this.notes, value); }
        }
        #endregion

        #region Commands
        public ICommand NavigateToNoteCreationPageCommand
        {
            get
            {
                return new RelayCommand(NavigateToNoteCreationPage);
            }
        }

        public ICommand LogoutCommand
        {
            get
            {
                return new RelayCommand(Logout);
            }
        }
        #endregion

        #region Methods
        private async void NavigateToNoteCreationPage()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new NoteCreation());
        }

        public async void LoadNotes()
        {
            int userId = (int)Application.Current.Properties["userId"];
            string query = $"SELECT * FROM Note WHERE userId = {userId}";
            Notes = await App.Database.getConnection().QueryAsync<Note>(query);
        }

        public async void Logout()
        {
            bool answer = await Application.Current.MainPage.DisplayAlert("Are you sure?", "Are you sure you want to logout?", "Yes", "No");

            if (!answer) return;

            Application.Current.Properties["isAuthenticated"] = false;
            Application.Current.Properties["username"] = null;
            Application.Current.Properties["userId"] = null;

            // Clear navigation stack
            await Application.Current.MainPage.Navigation.PushAsync(new Login());
            int pagesCount = Application.Current.MainPage.Navigation.NavigationStack.ToList().Count();

            for (int i = 0; i < pagesCount - 1; i++)
            {
                Page page = Application.Current.MainPage.Navigation.NavigationStack[i];
                Application.Current.MainPage.Navigation.RemovePage(page);
            }
        }
        #endregion

        #region Constructor
        public HomeViewModel()
        {
            LoadNotes();
        }
        #endregion
    }
}
