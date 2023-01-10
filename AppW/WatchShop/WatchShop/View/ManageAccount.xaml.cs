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
    public partial class ManageAccount : ContentPage
    {
        public ManageAccount()
        {
            InitializeComponent();
            ListViewInit();
        }

        async void ListViewInit()
        {
            qltaikhoan.ItemsSource = null;

            HttpClient httpClient = new HttpClient();

            //var productlist = await httpClient.GetStringAsync(Host.url.ToString() + "api/CartController/LayGioHang?mand=" + NguoiDung.nguoidung.MAND.ToString());

            var accountlist = await httpClient.GetStringAsync(Host.url.ToString() + "api/LoginController/TatCaNguoiDung");

            var accountlistConverted = JsonConvert.DeserializeObject<List<NguoiDung>>(accountlist);

            //listsp = productlistConverted;

            qltaikhoan.ItemsSource = accountlistConverted;
        }

        private async void sw_Delete_Invoked(object sender, EventArgs e)
        {
            var swipeitem = sender as SwipeItem;
            var nd = swipeitem.CommandParameter as NguoiDung;

            if (nd.MAND == NguoiDung.nguoidung.MAND)
            {
                await DisplayAlert("Thông Báo", "Không thể xóa tài khoản này", "ok");
            }

            var tb = await DisplayAlert("Thông Báo", "Bạn có muốn xóa "
                + nd.TENDN + " không?", "Đồng Ý", "Không");
            if (tb)
            {
                NguoiDung ndx = new NguoiDung();
                ndx.MAND = nd.MAND;
                HttpClient http = new HttpClient();
                string jsonlh = JsonConvert.SerializeObject(ndx);
                StringContent httcontent = new StringContent(jsonlh, Encoding.UTF8, "application/json");
                HttpResponseMessage kq = await http.PostAsync(Host.url.ToString() + "api/LoginController/XoaNguoiDung", httcontent);
                //var ct = await kq.Content.ReadAsStringAsync();
                var ct = kq.Content.ReadAsStringAsync().Result;
                var ctString = JsonConvert.DeserializeObject<string>(ct);
                if (ctString == null || ctString == "")
                {
                    await DisplayAlert("Thông Báo", "Xóa không thành công", "ok");
                    return;
                }
                await DisplayAlert("Thông Báo", "Xóa Thành Công", "ok");
                ListViewInit();

            }
        }
        /*
        private async void sw_Update_Invoked(object sender, EventArgs e)
        {
            var swipeitem = sender as SwipeItem;
            var sp = swipeitem.CommandParameter as SanPham;

            await this.Navigation.PushAsync(new AddProduct(sp));
        }
        */
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new RegisterPage());
        }
    }
}