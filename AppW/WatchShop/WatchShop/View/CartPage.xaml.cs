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
    public partial class CartPage : ContentPage
    {
        List<SanPham> listsp = new List<SanPham>();
        public CartPage()
        {
            InitializeComponent();
            ListViewInit();
        }
        
        async void ListViewInit()
        {
            HttpClient httpClient = new HttpClient();

            var productlist = await httpClient.GetStringAsync(Host.url.ToString() + "api/CartController/LayGioHang?mand=" + NguoiDung.nguoidung.MAND.ToString());

            //var productlist = await httpClient.GetStringAsync(Host.url.ToString() + "api/MainController/GetAllProduct");

            var productlistConverted = JsonConvert.DeserializeObject<List<SanPham>>(productlist);

            listsp = productlistConverted;
            
            lstgiohang.ItemsSource = productlistConverted;
        }

        private async void sw_Delete_Invoked(object sender, EventArgs e)
        {
            var swipeitem = sender as SwipeItem;
            var spc = swipeitem.CommandParameter as SanPham;

            var tb = await DisplayAlert("Thông Báo", "Bạn có muốn xóa "
                + spc.TENSP + " trong giỏ hàng không?", "Đồng Ý", "Không");
            if (tb)
            {
                foreach (SanPham sp in listsp)
                {
                    if (spc.MASP == sp.MASP)
                    {
                        sp.SOLUONG = spc.SOLUONG;

                        ChiTietGioHang spm = new ChiTietGioHang { MAND = NguoiDung.nguoidung.MAND.ToString(), MASP = spc.MASP.ToString(), SOLUONG = spc.SOLUONG };
                        HttpClient http = new HttpClient();
                        string jsonlh = JsonConvert.SerializeObject(spm);
                        StringContent httcontent = new StringContent(jsonlh, Encoding.UTF8, "application/json");
                        HttpResponseMessage kq = await http.PostAsync(Host.url.ToString() + "api/CartController/XoaGioHang", httcontent);

                        lstgiohang.ItemsSource = null;
                        ListViewInit();

                        break;
                    }
                }
            }
        }

        private async void sw_Update_Invoked(object sender, EventArgs e)
        {
            var swipeitem = sender as SwipeItem;
            var spc = swipeitem.CommandParameter as SanPham;

            if (spc.SOLUONG == 0 || spc.SOLUONG == null)
            {
                await DisplayAlert("Thông Báo", "Phải nhập số lượng sản phẩm!", "ok");
                return;
            }

            foreach (SanPham sp in listsp)
            {
                if (spc.MASP == sp.MASP)
                {
                    sp.SOLUONG = spc.SOLUONG;

                    ChiTietGioHang spm = new ChiTietGioHang { MAND = NguoiDung.nguoidung.MAND.ToString(), MASP = spc.MASP.ToString(), SOLUONG = spc.SOLUONG };
                    HttpClient http = new HttpClient();
                    string jsonlh = JsonConvert.SerializeObject(spm);
                    StringContent httcontent = new StringContent(jsonlh, Encoding.UTF8, "application/json");
                    HttpResponseMessage kq = await http.PostAsync(Host.url.ToString() + "api/CartController/CapNhatGioHang", httcontent);
                    await DisplayAlert("Thông Báo", "Sửa thành công", "ok");
                    break;
                }
            }
        }
    }
}