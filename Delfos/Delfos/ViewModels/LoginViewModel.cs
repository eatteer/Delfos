using System;
using System.Collections.Generic;
using System.Text;
using Delfos.Models;
using System.Windows.Input;
using Delfos.Views;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;
using System.Linq;

namespace Delfos.ViewModels
{
    public class LoginViewModel : BaseViewModel
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
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        public ICommand NavigateToRegisterPageCommand
        {
            get
            {
                return new RelayCommand(NavigateToRegisterPage);
            }
        }
        #endregion

        #region Methods
        private async void Login()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Fields cannot be empty", "Ok");
                return;
            }

            string query = $"SELECT * FROM User WHERE (username = '{Username}' OR email = '{Username}') AND password = '{Password}'";
            var foundUsers = await App.Database.getConnection().QueryAsync<User>(query);

            if (foundUsers.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Username or password do not match", "Ok");
                return;
            }

            await Application.Current.MainPage.DisplayAlert("Login", "Successful Login", "Ok");

            // https://stackoverflow.com/questions/46987449/keep-user-logged-into-xamarin-forms-app-unless-log-out-is-clicked
            // Application.Current.Properties["username"] = foundUsers[0].Username;

            // Insert home page before login page, then remove login from stack, so user cannot
            // go back to login page
            var last = Application.Current.MainPage.Navigation.NavigationStack.Last();
            Application.Current.MainPage.Navigation.InsertPageBefore(new Home(), last);
            await Application.Current.MainPage.Navigation.PopAsync();

        }

        private async void NavigateToRegisterPage()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Register());
        }
        #endregion
    }
}
