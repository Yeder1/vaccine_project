
namespace Vaccination.BussinessLogic.DTOs.Responses
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; } // Mã trạng thái HTTP
        public T Data { get; set; } // Dữ liệu trả về (có thể là bất kỳ kiểu dữ liệu nào)
        public string Message { get; set; } // Thông điệp trả về

        // Constructor không tham số
        public ApiResponse()
        {
        }

        // Constructor với tham số khởi tạo
        public ApiResponse(int statusCode, T data, string message)
        {
            StatusCode = statusCode;
            Data = data;
            Message = message;
        }

        // Phương thức tạo phản hồi thành công
        public static ApiResponse<T> Success(T data, string message)
        {
            return new ApiResponse<T>
            {
                StatusCode = 200, // HTTP 200 OK
                Data = data,
                Message = message
            };
        }

        // Phương thức tạo phản hồi thất bại
        public static ApiResponse<T> Failure(string message)
        {
            return new ApiResponse<T>
            {
                StatusCode = default,
                Data = default(T), // Không có dữ liệu
                Message = message
            };
        }
    }

}
