using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace WatchShop.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            Products = GetProducts();
            MenuList = GetMenus();
        }

        private Product selectedProduct;

        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set { selectedProduct = value; }
        }


        private ObservableCollection<Product> products;
        public ObservableCollection<Product> Products
        {
            get { return products; }
            set { products = value; }
        }

        private ObservableCollection<Menu> menuList;
        public ObservableCollection<Menu> MenuList
        {
            get { return menuList; }
            set { menuList = value; }
        }

        public void ShowDetails()
        {
            var page = new View.DetailsPage() { BindingContext = new DetailsViewModel { SelectedProduct = SelectedProduct } };
            App.Current.MainPage.Navigation.PushAsync(page);
        }

        private ObservableCollection<Menu> GetMenus()
        {
            return new ObservableCollection<Menu>
            {
                new Menu { Icon = "order.png", Name = "Danh sách đặt hàng"},
                new Menu { Icon = "favorite.png", Name = "Danh sách yêu thích "},
                new Menu { Icon = "shopping.png", Name = "Giỏ hàng"},
                new Menu { Icon = "settings.png", Name = "Cài đặt"}
            };
        }

        private ObservableCollection<Product> GetProducts()
        {
            return new ObservableCollection<Product>
            {
                new Product { Name = "Đồng hồ Hilfi", Price = 5000000 , Image = "charlesWatch.png", Model = "Mẫu năm 2011", Rating = 3.7, Views = 4.5, Description = "Được sản xuất vào năm 2011, sản phẩm với kiểu dáng sang trọng và gọn gàng quý phái, phù hợp với mọi người."},
                new Product { Name = "Đồng hồ Gold", Price = 7000000 , Image = "johnWatch.png", Model = "Mẫu năm 2009", Rating = 4.2, Views = 4.2, Description = "là sản phẩm được những người thợ làm đồng hồ ra đời với bàn tay tinh xảo, đem lại sự thoải mái và dễ sử dụng."},
                new Product { Name = "Đồng hồ Pierre LD", Price = 1200000, Image = "marekWatch.png", Model = "Mẫu năm 2007", Rating = 5.0, Views = 4.9, Description = "sản phẩm đồng hồ nam thời trang FOURRON Japan 688 Nam tính , các chàng trai hoàn toàn có thể hô biến nó thành món phụ kiện nổi bật cho phong cách thời trang riêng của mình và kết hợp với nhiều trang phục khác nhau. "},
                new Product { Name = "Đồng hồ Omega RD", Price = 8500000, Image = "rutgeWatch.png", Model = "Mẫu năm 1997", Rating = 4.6, Views = 4.7, Description = "ừ thời trang công sở lịch lãm với quần âu, áo sơ mi, vest cho đến thời trang casual hàng ngày với quần Jeans, áo pull hoặc ăn vận như một fashionita với skinny Jeans rách, áo Jacket da, kính mắt... Lựa chọn đồng hồ nam FOURRON Japan 688 Chuẩn Man, bạn chắc chắn sẽ là người đàn ông đích thực và “chuẩn không cần chỉnh”."},
            };
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }

    public class Product
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string Image { get; set; }
        public double Rating { get; set; }
        public double Views { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
    }

    public class Menu
    {
        public string Name { get; set; }
        public string Icon { get; set; }
    }
}
