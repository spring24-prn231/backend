using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Common.Enums
{
    public static class MessageResponse
    {
        public static string CreateSuccess = "Tạo thành công";
        public static string UpdateSuccess = "Cập nhật thành công";
        public static string DeleteSuccess = "Xoá thành công";
        public static string ServerError = "Đã xảy ra lỗi";
        public static string ConflictError = "Dữ liệu đã tồn tại";
        public static string BadRequestError = "Yêu cầu không hợp lệ";
        public static string NotFoundError = "Không tìm thấy dữ liệu";
        public static string Unauthorized = "Xác thực không thành công";
        public static string Authenticated = "Xác thực thành công";
    }
}
