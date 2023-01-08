//using Plugin.SharedTransitions;
using System;
using WatchShop.Models;
using WatchShop.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchShop
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            NguoiDung.nguoidung = new NguoiDung();
            //MainPage = new LoginPage();
            //MainPage = new AddProduct();
            MainPage = new Page1();

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
