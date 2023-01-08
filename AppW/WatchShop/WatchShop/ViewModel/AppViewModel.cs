using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using WatchShop.Models;
using WatchShop.View;
using Xamarin.Forms;

namespace WatchShop.ViewModel
{
    public class AppViewModel : BaseViewModel
    {

        private bool isAdmin;
        public bool IsAdmin { get => isAdmin; set => SetProperty(ref isAdmin, value); }
        private bool isAccount;

        public bool IsAccount { get => isAccount; set => SetProperty(ref isAccount, value); }

        public string Dangnhap { get => dangnhap; set => SetProperty(ref dangnhap, value); }
        private string dangnhap;
        public  AppViewModel()
        {
            /*
            if (NguoiDung.nguoidung.MAND == null || NguoiDung.nguoidung.MAND == "")
            {
                Dangnhap = "Đăng Nhập";
            }
            else
            {
                Dangnhap = "Đăng Xuất";

            }*/
            MessagingCenter.Subscribe<LoginPage>(this, "", (sender) =>
            {
                Dangnhap = "Đăng Nhập";
                IsAdmin = false;
                IsAccount = false;
                
            });

            MessagingCenter.Subscribe<LoginPage>(this, "ADMIN", (sender) =>
            {
                Dangnhap = "Đăng Xuất";
                IsAdmin = true;
                IsAccount= false;
            });

            MessagingCenter.Subscribe<LoginPage>(this, "USER", (sender) =>
            {
                Dangnhap = "Đăng Xuất";
                IsAccount = true;
                IsAdmin = false;
            });
            /*
            if (NguoiDung.nguoidung.QUYEN == "ADMIN" )
            {
                IsAdmin = true;
            }
            else
            {
                IsAdmin = false;

            }*/

        }

        //public string Dangnhap { get => dangnhap; set => SetProperty() }

        public async void Check()
        {

            if (NguoiDung.nguoidung.MAND == null || NguoiDung.nguoidung.MAND == "")
            {
                Dangnhap = "Đăng Nhập";
            }
            else
            {
                Dangnhap = "Đăng Xuất";

            }
        }
    }
}