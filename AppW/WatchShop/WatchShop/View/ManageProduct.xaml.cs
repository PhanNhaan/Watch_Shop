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
    public partial class ManageProduct : ContentPage
    {
        public ManageProduct()
        {
            InitializeComponent();
            ListViewInit();
        }

        async void ListViewInit()
        {
            qlsanpham.ItemsSource = null;

            HttpClient httpClient = new HttpClient();

            var productlist = await httpClient.GetStringAsync(Host.url.ToString() + "api/MainController/GetAllProduct");

            var productlistConverted = JsonConvert.DeserializeObject<List<SanPham>>(productlist);

            //listsp = productlistConverted;

            qlsanpham.ItemsSource = productlistConverted;
        }

        protected override void OnAppearing()
        {
            ListViewInit();
            base.OnAppearing();
        }
        private async void sw_Delete_Invoked(object sender, EventArgs e)
        {
            var swipeitem = sender as SwipeItem;
            var sp = swipeitem.CommandParameter as SanPham;

            var tb = await DisplayAlert("Thông Báo", "Bạn có muốn xóa "
                + sp.TENSP + " không?", "Đồng Ý", "Không");
            if (tb)
            {

                SanPham spx = new SanPham();
                spx.MASP = sp.MASP;
                HttpClient http = new HttpClient();
                string jsonlh = JsonConvert.SerializeObject(spx);
                StringContent httcontent = new StringContent(jsonlh, Encoding.UTF8, "application/json");
                HttpResponseMessage kq = await http.PostAsync(Host.url.ToString() + "api/MainController/XoaSanPham", httcontent);

                var ct = kq.Content.ReadAsStringAsync().Result;
                var ctString = JsonConvert.DeserializeObject<string>(ct);
                if (ctString == null || ctString == "")
                {
                    await DisplayAlert("Thông Báo", "Xóa không thành công", "ok");
                    return;
                }
                await DisplayAlert("Thông Báo", "Xóa Thành Công", "Không");
                ListViewInit();
                
            }
        }

        private async void sw_Update_Invoked(object sender, EventArgs e)
        {
            var swipeitem = sender as SwipeItem;
            var sp = swipeitem.CommandParameter as SanPham;

            await this.Navigation.PushAsync(new AddProduct(sp));
        }

        private async void qlsanpham_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SanPham sp = qlsanpham.SelectedItem as SanPham;
            await this.Navigation.PushAsync(new DetailsPage(sp));

        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new AddProduct());
        }
    }
}