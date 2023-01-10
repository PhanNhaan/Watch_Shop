using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchShop.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WatchShop.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace WatchShop.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailOrderPage : ContentPage
    {
        DonHang donhang =new DonHang();
        public DetailOrderPage()
        {
            InitializeComponent();
        }

        public DetailOrderPage(DonHang dh)
        {
            InitializeComponent();
            donhang = dh;
            ListViewInit();
        }

        protected override void OnAppearing()
        {
            ListViewInit();
            base.OnAppearing();
        }
        async void ListViewInit()
        {
            ten.Text = donhang.TENDN;
            ma.Text = donhang.MADH;
            ngay.Text = donhang.NGAYLAP;
            tien.Text = donhang.GIATRI.ToString();
            
            listdonhang.ItemsSource = null;

            HttpClient httpClient = new HttpClient();

            var ctdhlist = await httpClient.GetStringAsync(Host.url.ToString() + "api/OrderController/LayCTDH?madh=" + donhang.MADH.ToString());

            //var productlist = await httpClient.GetStringAsync(Host.url.ToString() + "api/MainController/GetAllProduct");

            var ctdhlistConverted = JsonConvert.DeserializeObject<List<SanPham>>(ctdhlist);

            listdonhang.ItemsSource = ctdhlistConverted;
        }
    }
}