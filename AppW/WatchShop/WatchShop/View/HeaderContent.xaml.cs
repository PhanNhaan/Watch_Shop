using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchShop.Models;
using WatchShop.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchShop.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HeaderContent : ContentView
	{
		public HeaderContent ()
		{
			InitializeComponent ();

            //BindingContext = NguoiDung.nguoidung;
            /*
            if (NguoiDung.nguoidung.TENDN == null || NguoiDung.nguoidung.TENDN == "")
            {
                tennd.Text = "Vui lòng đăng nhập";
            }
            else tennd.Text = "Chào " + NguoiDung.nguoidung.TENDN;*/
        }

    }
}