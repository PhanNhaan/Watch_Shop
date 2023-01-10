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
	public partial class OrderPage : ContentPage
	{
		public OrderPage ()
		{
			InitializeComponent ();
            ListViewInit();

        }

        async void ListViewInit()
        {
            listdonhang.ItemsSource = null;

            HttpClient httpClient = new HttpClient();

            //var productlist = await httpClient.GetStringAsync(Host.url.ToString() + "api/CartController/LayGioHang?mand=" + NguoiDung.nguoidung.MAND.ToString());

            var orderlist = await httpClient.GetStringAsync(Host.url.ToString() + "api/OrderController/TatCaDonHangTheoNguoiDung?mand=" + NguoiDung.nguoidung.MAND.ToString());

            var orderlistConverted = JsonConvert.DeserializeObject<List<DonHang>>(orderlist);

            //listsp = productlistConverted;

            listdonhang.ItemsSource = orderlistConverted;
        }
        private void listdonhang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //SanPham sp = qlsanpham.SelectedItem as SanPham;
            //await this.Navigation.PushAsync(new DetailsPage(sp));
        }
    }
}