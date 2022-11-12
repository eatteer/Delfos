using System;
using System.Collections.Generic;
using System.Text;
using Delfos.Models;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;
using System.Linq;

namespace Delfos.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {

        #region Attributes
        private string username = "";
        private string password = "";
        #endregion

        #region Properties
        public string Username
        {
            get { return username; }
            set
            {
                this.username.Trim();
                SetValue(ref this.username, value);
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                this.password.Trim();
                SetValue(ref this.password, value);
            }
        }
        #endregion

        #region Commands
        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(Register);
            }
        }
        #endregion

        #region Methods
        private async void Register()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Fields cannot be empty", "Ok");
                return;
            }

            string query = $"SELECT * FROM User WHERE username = '{Username}'";
            var foundUsers = await App.Database.getConnection().QueryAsync<User>(query);
            if (foundUsers.Count > 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "The username is already taken", "Ok");
                return;
            }

            User user = new User();
            user.Username = Username;
            user.Password = Password;

            int rowsInserted = await App.Database.getConnection().InsertAsync(user);
            if (rowsInserted == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "User could not be registered", "Ok");
                return;
            }
                
            await Application.Current.MainPage.DisplayAlert("Success", "User successfully registered", "Ok");
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        #endregion
    }
}
