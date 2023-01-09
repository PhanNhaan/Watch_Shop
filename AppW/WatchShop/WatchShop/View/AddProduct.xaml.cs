using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WatchShop.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace WatchShop.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProduct : ContentPage
    {
        SanPham sp;
        public AddProduct()
        {
            InitializeComponent();
            reset();
        }

        public AddProduct(SanPham sp)
        {
            InitializeComponent();
            tsp.Text = "Sửa sản phẩm";
            tensp.Text = sp.TENSP;
            gia.Text = sp.DONGIA.ToString();
            mota.Text = sp.MOTA;
            hinh.Text = sp.HINH;
            namsx.Text = sp.NAMSX.ToString();
            hangsx.Text = sp.HANGSX;
            if (sp.GIOITINH =="Nam")
                gioitinh.SelectedIndex = 0;
            else gioitinh.SelectedIndex = 1;
        }

        private void reset()
        {
            sp = new SanPham();
            tensp.Text = "";
            gia.Text = "";
            mota.Text = "";
            hinh.Text = "";
            namsx.Text = "";
            hangsx.Text = "";
            gioitinh.SelectedIndex = 0;
        }
        public async void btnthem_Clicked(object sender, EventArgs e)
        {
            
            if (tensp.Text == "" || gia.Text == "" || mota.Text == "" || hinh.Text == "" || namsx.Text == "" || hangsx.Text == "")
            {
                await DisplayAlert("Thông Báo", "Phải nhập đầy đủ thông tin", "ok");
                return;
            }
            sp.TENSP = tensp.Text;
            sp.DONGIA = int.Parse(gia.Text);
            sp.MOTA = mota.Text;
            sp.HINH = hinh.Text;
            sp.NAMSX = int.Parse(namsx.Text);
            sp.HANGSX = hangsx.Text;
            sp.GIOITINH = gioitinh.SelectedItem.ToString();

            HttpClient http = new HttpClient();
            string jsonlh = JsonConvert.SerializeObject(sp);
            StringContent httcontent = new StringContent(jsonlh, Encoding.UTF8, "application/json");
            HttpResponseMessage kq;
            if (sp.MASP != "" && sp.MASP != null)
                kq = await http.PostAsync(Host.url.ToString() + "api/MainController/CapNhatSanPham", httcontent);
            else
            {
                kq = await http.PostAsync(Host.url.ToString() + "api/MainController/ThemSanPham", httcontent);
            }
            var kqtv = await kq.Content.ReadAsStringAsync();
            sp = JsonConvert.DeserializeObject<SanPham>(kqtv);
            if (sp.MASP != "" && sp.MASP != null)
            {
                await DisplayAlert("Thông báo", "Cập nhật dữ liệu thành công" + sp.MASP, "ok");
                reset();
            }
            else
                await DisplayAlert("Thông báo", "Cập nhật dữ liệu Lỗi", "ok");
        }
    }
}