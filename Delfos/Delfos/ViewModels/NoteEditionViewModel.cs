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
    public class NoteEditionViewModel : BaseViewModel
    {
        #region Attributes
        private int id = 0;
        private string title = "";
        private string description = "";
        private int userId = 0;
        #endregion

        #region Properties
        public int Id
        {
            get { return id; }
            set
            {
                SetValue(ref this.id, value);
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                this.title.Trim();
                SetValue(ref this.title, value);
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                this.description.Trim();
                SetValue(ref this.description, value);
            }
        }

        public int UserId
        {
            get { return userId; }
            set
            {
                SetValue(ref this.userId, value);
            }
        }
        #endregion

        #region Commands
        public ICommand NavigateBackCommand
        {
            get
            {
                return new RelayCommand(NavigateBack);
            }
        }

        public ICommand UpdateNoteCommand
        {
            get
            {
                return new RelayCommand(UpdateNote);
            }
        }
        #endregion

        #region Methods
        private async void NavigateBack()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void UpdateNote()
        {
            bool answer = await Application.Current.MainPage.DisplayAlert("Are you sure?", "Are you sure you want to edit this note?", "Yes", "No");

            if (!answer) return;

            Note updatedNote = new Note();
            updatedNote.Id = Id;
            updatedNote.Title = Title;
            updatedNote.Description = Description;
            updatedNote.UserId = UserId;
            int rowsAdded = await App.Database.UpdateAsync(updatedNote);
            if (rowsAdded == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Note could be not edited", "Ok");
                return;
            }
            await Application.Current.MainPage.DisplayAlert("Success", "Note successfully edited", "Ok");
            NavigateBack();
        }
        #endregion

        public NoteEditionViewModel(Note note)
        {
            Id = note.Id;
            Title = note.Title;
            Description = note.Description;
            UserId = note.UserId;
        }
    }
}
