using MyTour.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyTour.Services
{
    public class KhachHangService
    {
        public static KhachHang _CurrentUser = null;
        public static KhachHang GetCurrentUser
        {
            get//để to
            {

                return _CurrentUser;
            }
        }

        public static void LoadUser(KhachHang user)
        {
            if (_CurrentUser == null)
                _CurrentUser = user;

        }
    }
}
