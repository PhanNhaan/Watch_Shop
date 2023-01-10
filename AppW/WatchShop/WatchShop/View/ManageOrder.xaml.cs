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
    public partial class ManageOrder : ContentPage
    {
        public ManageOrder()
        {
            InitializeComponent();
            ListViewInit();

        }

        protected override void OnAppearing()
        {
            ListViewInit();
            base.OnAppearing();
        }

        async void ListViewInit()
        {
            listdonhang.ItemsSource = null;

            HttpClient httpClient = new HttpClient();

            //var productlist = await httpClient.GetStringAsync(Host.url.ToString() + "api/CartController/LayGioHang?mand=" + NguoiDung.nguoidung.MAND.ToString());

            var orderlist = await httpClient.GetStringAsync(Host.url.ToString() + "api/OrderController/TatCaDonHang");

            var orderlistConverted = JsonConvert.DeserializeObject<List<DonHang>>(orderlist);

            //listsp = productlistConverted;

            listdonhang.ItemsSource = orderlistConverted;
        }
        private async void listdonhang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DonHang dh = listdonhang.SelectedItem as DonHang;
            await this.Navigation.PushAsync(new DetailOrderPage(dh));
        }
    }
}