using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Views
{
    public interface ILoginView
    {
        string Username { get; set; }
        string Password { get; set; }
        void Login();
        void LoadMenu();
        void ShowMessage(string message);
    }
}
