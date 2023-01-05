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
        public DetailsPage()
        {
            InitializeComponent();
        }

        public DetailsPage(SanPham product)
        {
            InitializeComponent();
            //GetBooksBySubjectId(product.MASP);
            BindingContext= product;
        }

        private void BackTapped(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }
        /*
        async void GetBooksBySubjectId(string subjectId)
        {
            HttpClient httpClient = new HttpClient();
            var booklist = await httpClient.GetStringAsync(Host.url + "/nhan/api/ServiceController/GetBooksBySubjectID?macd="
                + subjectId.ToString());
            var booklistConverted = JsonConvert.DeserializeObject<List<SanPham>>(booklist);

            //Productdetail.ItemsSource = booklistConverted;
        }*/
    }
}