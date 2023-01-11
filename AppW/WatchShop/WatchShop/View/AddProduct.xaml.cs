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
        SanPham sp =new SanPham();
        SanPham spt = new SanPham();
        public AddProduct()
        {
            InitializeComponent();
            reset();
        }

        public AddProduct(SanPham sps)
        {
            InitializeComponent();
            tsp.Text = "Sửa sản phẩm";
            btnthem.Text = "Sửa sản phẩm";
            tensp.Text = sps.TENSP;
            gia.Text = sps.DONGIA.ToString();
            mota.Text = sps.MOTA;
            hinh.Text = sps.HINH;
            namsx.Text = sps.NAMSX.ToString();
            hangsx.Text = sps.HANGSX;
            sp = sps;
            if (sps.GIOITINH =="Nam")
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
            spt.TENSP = tensp.Text;
            spt.DONGIA = int.Parse(gia.Text);
            spt.MOTA = mota.Text;
            spt.HINH = hinh.Text;
            spt.NAMSX = int.Parse(namsx.Text);
            spt.HANGSX = hangsx.Text;
            spt.GIOITINH = gioitinh.SelectedItem.ToString();
            spt.MASP = sp.MASP;

            HttpClient http = new HttpClient();
            string jsonlh = JsonConvert.SerializeObject(spt);
            StringContent httcontent = new StringContent(jsonlh, Encoding.UTF8, "application/json");
            HttpResponseMessage kq;
            if (sp.MASP == "" || sp.MASP == null)
                kq = await http.PostAsync(Host.url.ToString() + "api/MainController/ThemSanPham", httcontent);
            else
            {
                
                kq = await http.PostAsync(Host.url.ToString() + "api/MainController/CapNhatSanPham", httcontent);
            }
            var kqtv = await kq.Content.ReadAsStringAsync();
            var sptv = JsonConvert.DeserializeObject<SanPham>(kqtv);
            if (sptv.MASP != "" && sptv.MASP != null)
            {
                await DisplayAlert("Thông báo", "Cập nhật dữ liệu thành công" + sptv.MASP, "ok");
                await this.Navigation.PopAsync();
                reset();
            }
            else
                await DisplayAlert("Thông báo", "Cập nhật dữ liệu Lỗi", "ok");
        }
    }
}