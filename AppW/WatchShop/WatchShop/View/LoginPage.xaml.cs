using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WatchShop.Models;

namespace WatchShop.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void BackTapped(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }

        private void btnDangKi(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new RegisterPage());
        }

        public async void cmddangnhap_Clicked(object sender, EventArgs e)
        {
            HttpClient http = new HttpClient();
            var kq = await http.GetStringAsync
                (Host.url.ToString() + "api/LoginController/DangNhap?TenDangNhap=" +
                txttendn.Text + "&&MatKhau=" + txtmatkhau.Text);
            var nd = JsonConvert.DeserializeObject<Models.NguoiDung>(kq);
            if (nd.TENDN != "" && nd.TENDN != null)
            {
                //await DisplayAlert("TB", " Chào Ban :" + nd.TENDN, "OK");
                NguoiDung.nguoidung = nd;

                //ViewModel.AppViewModel.Check();
                MessagingCenter.Send<LoginPage>(this, NguoiDung.nguoidung.QUYEN);
                MessagingCenter.Send<LoginPage>(this, NguoiDung.nguoidung.MAND);

                // MessagingCenter.Send<LoginPage>(this,
                //(NguoiDung.nguoidung.QUYEN == "ADMIN") ? "ADMIN" : "USER");

                await Shell.Current.GoToAsync("//main");

                //await Navigation.PushAsync(new AppShell());
                //Appearing new AppShell();
                //Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                //Navigation.InsertPageBefore(new AppShell(), this);
                //await Navigation.PopAsync();
                //await Shell.Current.Navigation.PushAsync(new AppShell());
                //Shell.Current.Navigation.PushAsync(new AppShell());
            }
            else
                await DisplayAlert("TB", " Đăng Nhập Sai :", "OK");


        }
    }
}