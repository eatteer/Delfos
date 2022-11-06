using System;
using System.Collections.Generic;
using System.Text;
using Delfos.Models;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;
using System.Linq;
using System.Net.Mail;

namespace Delfos.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {

        #region Attributes
        private string username = "";
        private string password = "";
        private string repeatPwd = "";
        private string email = "";
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

        public string RepeatPwd
        {
            get { return repeatPwd; }
            set
            {
                this.repeatPwd.Trim();
                SetValue(ref this.repeatPwd, value);
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                this.email.Trim();
                SetValue(ref this.email, value);
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
            // Check empty fields
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(RepeatPwd))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Fields cannot be empty", "Ok");
                return;
            }

            // Check if passwords match
            if (Password != repeatPwd)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Passwords don't match", "Ok");
                return;
            }

            // Check if the E-Mail address is valid
            try
            {
                MailAddress m = new MailAddress(Email);
            }
            catch (FormatException)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Invalid E-Mail address", "Ok");
                return;
            }

            string query = $"SELECT * FROM User WHERE username = '{Username}' OR email = '{Email}'";
            var foundUsers = await App.Database.getConnection().QueryAsync<User>(query);
            if (foundUsers.Count > 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "This user is already registered", "Ok");
                return;
            }

            User user = new User();
            user.Username = Username;
            user.Email = Email;
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
