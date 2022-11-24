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
    public class NoteCreationViewModel : BaseViewModel
    {
        #region Attributes
        private string title = "";
        private string description = "";
        #endregion

        #region Properties
        public string Title
        {
            get { return title.Trim(); }
            set
            {
                value.Trim();
                SetValue(ref this.title, value);
            }
        }

        public string Description
        {
            get { return description.Trim(); }
            set
            {
                value.Trim();
                SetValue(ref this.description, value);
            }
        }
        #endregion

        #region Commands
        public ICommand CreateNoteCommand
        {
            get
            {
                return new RelayCommand(CreateNote);
            }
        }
        #endregion

        #region Methods
        private async void CreateNote()
        {
            if (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Description))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Fields cannot be empty", "Ok");
                return;
            }

            Note note = new Note();
            note.Title = Title;
            note.Description = Description;
            note.UserId = (int)Application.Current.Properties["userId"];

            int rowsInserted = await App.Database.getConnection().InsertAsync(note);
            if (rowsInserted == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Note could not be created", "Ok");
                return;
            }

            await Application.Current.MainPage.DisplayAlert("Success", "Note successfully created", "Ok");
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        #endregion
    }
}
