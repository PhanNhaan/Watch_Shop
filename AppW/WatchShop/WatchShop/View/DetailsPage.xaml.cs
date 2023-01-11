using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WatchShop.Models;
using WatchShop.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchShop.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailsPage : ContentPage
	{
        SanPham sp = new SanPham();
        public DetailsPage()
        {
            InitializeComponent();
            if (NguoiDung.nguoidung.QUYEN == "ADMIN" || NguoiDung.nguoidung.QUYEN == "") { gh.IsVisible = false; }
            else { gh.IsVisible = true; }
        }

        public DetailsPage(SanPham product)
        {
            BindingContext= product;
            InitializeComponent();
            if (NguoiDung.nguoidung.QUYEN == "ADMIN" || NguoiDung.nguoidung.QUYEN == null || NguoiDung.nguoidung.QUYEN == "") 
            { 
                gh.IsVisible = false; 
            }
            else { gh.IsVisible = true; }
            
            sp = product;
        }

        private void BackTapped(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }

        private void tru_Clicked(object sender, EventArgs e)
        {
            if (kq.Text != null && kq.Text != "")
            {
                int so = int.Parse(kq.Text) - 1;
                if (so > 0)
                {
                    kq.Text = so.ToString();
                }
            }
        }

        private void cong_Clicked(object sender, EventArgs e)
        {
            if (kq.Text != null && kq.Text != "")
            {
                int so = int.Parse(kq.Text) + 1;
                kq.Text = so.ToString();
            }

        }

        private async void themgh_Clicked(object sender, EventArgs e)
        {
            if (kq.Text == "0" || kq.Text == "")
            {
                await DisplayAlert("Thông Báo", "Phải nhập số lượng sản phẩm!", "ok");
                return;
            }

            ChiTietGioHang spm = new ChiTietGioHang { MAND = NguoiDung.nguoidung.MAND.ToString(), MASP = sp.MASP.ToString(), SOLUONG = int.Parse(kq.Text.ToString()) };
            HttpClient http = new HttpClient();
            string jsonlh = JsonConvert.SerializeObject(spm);
            StringContent httcontent = new StringContent(jsonlh, Encoding.UTF8, "application/json");
            HttpResponseMessage kqtv = await http.PostAsync(Host.url.ToString() + "api/CartController/ThemVaoGioHang", httcontent);
            var sptv = await kqtv.Content.ReadAsStringAsync();
            var spt = JsonConvert.DeserializeObject<ChiTietGioHang>(sptv);
            if (spt.MAND == "" || spt.MAND == null)
            {
                await DisplayAlert("Thông Báo", "Thêm Không thành công", "ok");
            }
            else 
                await DisplayAlert("Thông Báo", "Thêm thành công", "ok");
        }
    }
}