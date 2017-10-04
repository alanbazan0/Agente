using System;
using System.Collections.Generic;
using System.Text;
using AgenteApp.Views;
using AgenteApp.Models;

namespace AgenteApp.Presenters
{
    public class LoginPresenter
    {
        private ILoginView view;
        private User user;
        public LoginPresenter(ILoginView view)
        {
            this.view = view;
        }

        public async void Login()
        {
            UserDataAccess dataAccess = new UserDataAccess();
            user = await dataAccess.Find(view.Username, view.Password);
         
            if(user!=null)
            {
                view.ShowMessage("Access granted!");
                view.LoadMenu();
            }
            else
                view.ShowMessage("Access denied!");


        }
    }
}
