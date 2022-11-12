using System;
using System.Collections.Generic;
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
        #endregion

        #region Methods
        private async void NavigateToNoteCreationPage()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new NoteCreation());
        }

        private async void LoadNotes()
        {
            int userId = (int)Application.Current.Properties["userId"];
            string query = $"SELECT * FROM Note WHERE userId = {userId}";
            Notes = await App.Database.getConnection().QueryAsync<Note>(query);
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
