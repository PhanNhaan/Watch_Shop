using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Windows.Input;
using Xamarin.Essentials;
using System.Threading;
using WatchShop.Models;
using WatchShop.View;

namespace WatchShop
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AppShell : Xamarin.Forms.Shell
    {
		public AppShell ()
		{
			InitializeComponent ();
            Routing.RegisterRoute("login", typeof(LoginPage));
            btndang.Text = "Đăng Xuất";
            GoToAsync("login");
        }

        public void checkaccount()
        {
if (NguoiDung.nguoidung == null)
            {
                btndang.Text = "Đăng Nhập";
            }
            else
            {
                btndang.Text = "Đăng Xuất";

            }
        }
  
        protected override void OnAppearing()
        {
           // checkaccount();

            base.OnAppearing();
            
            
        }

        public async void btndangxuat_Clicked(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = false;
            if (NguoiDung.nguoidung.MAND == null || NguoiDung.nguoidung.MAND == "")
            {
                await Shell.Current.GoToAsync("login");
            }
            else
            {
                NguoiDung.nguoidung = new NguoiDung();
                //new AppShell();
                //new App();
                //admin.IsVisible= false;
                bool a = await DisplayAlert("TB", "Bạn có muốn đăng xuất không!!", "Có", "Không");
                if (a == true)
                    await Shell.Current.GoToAsync("login");
            }
        }

        private async void test_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("TB", " Chào Ban :" + NguoiDung.nguoidung.MAND, "OK");

        }
    }

    public class HiddenItem : ShellItem
    {

    }
}