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

        private async void cmddangnhap_Clicked(object sender, EventArgs e)
        {
            HttpClient http = new HttpClient();
            var kq = await http.GetStringAsync
                (Host.url.ToString() + "WatchShop/api/LoginController/DangNhap?TenDangNhap=" +
                txttendn.Text + "&&MatKhau=" + txtmatkhau.Text);
            var nd = JsonConvert.DeserializeObject<Models.NguoiDung>(kq);
            if (nd.TENDN != "" && nd.TENDN != null)
            {
                await DisplayAlert("TB", " Chào Ban :" + nd.TENDN, "OK");
                //NguoiDung.nguoidung = nd;

            }
            else
                await DisplayAlert("TB", " Đăng Nhập Sai :", "OK");


        }
    }
}