using Newtonsoft.Json;
//using Plugin.SharedTransitions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WatchShop.Models;
using Xamarin.Forms;
using Newtonsoft.Json;
using WatchShop.View;
using System.Collections.ObjectModel;

namespace WatchShop
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public List<SanPham> listsp =new List<SanPham>();
        public MainPage()
        {
            InitializeComponent();
            ListViewInit();
        }

        protected override void OnAppearing()
        {
            //ListViewInit();
            base.OnAppearing();
        }

        async void ListViewInit()
        {
            HttpClient httpClient = new HttpClient();
            //var productlist = await httpClient.GetStringAsync("http://192.168.1.7/WatchShop/api/MainController/GetAllProduct");
            var productlist = await httpClient.GetStringAsync(Host.url.ToString() + "api/MainController/GetAllProduct");
            var productlistConverted = JsonConvert.DeserializeObject<List<SanPham>>(productlist);
            listsp = productlistConverted;
            ListProduct.ItemsSource = productlistConverted;
        }
        /*
        private void OpenMenu()
        {
            MenuGrid.IsVisible = true;

            Action<double> callback = input => MenuView.TranslationX = input;
            MenuView.Animate("anim", callback, -260, 0, 16, 300, Easing.CubicInOut);
        }

        private void CloseMenu()
        {
            Action<double> callback = input => MenuView.TranslationX = input;
            MenuView.Animate("anim", callback, 0, -260, 16, 300, Easing.CubicInOut);

            MenuGrid.IsVisible = false;
        }

        private void MenuTapped(object sender, EventArgs e)
        {
            OpenMenu();
        }

        private void OverlayTapped(object sender, EventArgs e)
        {
            CloseMenu();
        }
        */
        private async void ProductSelected(object sender, SelectionChangedEventArgs e)
        {
            //SanPham product = (SanPham)e.CurrentSelection;
            SanPham product = ListProduct.SelectedItem as SanPham;
            await Navigation.PushAsync(new DetailsPage(product));
            //SharedTransitionNavigationPage.SetTransitionSelectedGroup(this, vm.SelectedProduct.Name );
            //vm.ShowDetails();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = SearchBar.Text.ToUpper();
            ListProduct.ItemsSource = listsp.Where(i => i.TENSP.ToUpper().Contains(text));
        }
    }
}
