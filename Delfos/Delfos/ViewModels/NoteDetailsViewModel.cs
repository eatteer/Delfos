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
    public class NoteDetailsViewModel : BaseViewModel
    {
        #region Attributes
        private int id = 0;
        private string title = "";
        private string description = "";
        private Note note = null;
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

        public Note Note
        {
            get { return note; }
            set
            {
                SetValue(ref this.note, value);
            }
        }
        #endregion

        #region Commands
        public ICommand NavigateToNoteEditionPageCommand
        {
            get
            {
                return new RelayCommand(NavigateToNoteEditionPage);
            }
        }

        public ICommand DeleteNoteCommand
        {
            get
            {
                return new RelayCommand(DeleteNote);
            }
        }
        #endregion

        #region Methods
        private async void NavigateToNoteEditionPage()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new NoteEdition());
        }

        private async void DeleteNote()
        {
            bool answer = await Application.Current.MainPage.DisplayAlert("Are you sure?", "Are you sure you want to delete this note?", "Yes", "No");

            if (!answer) return;

            int deletedRows = await App.Database.DeleteAsync(Note);
            if (deletedRows == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Note could be not deleted", "Ok");

                return;
            }
            await Application.Current.MainPage.DisplayAlert("Success", "Note sucessfully deleted", "Ok");
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        #endregion

        public NoteDetailsViewModel(int id)
        {
            Id = id;
            LoadNote();
        }

        public async void LoadNote()
        {
            string query = $"SELECT * FROM Note where id = {Id}";
            List<Note> notes = await App.Database.QueryAsync<Note>(query);
            Note note = notes[0];
            Note = note;
            Title = note.Title;
            Description = note.Description;
        }
    }
}

