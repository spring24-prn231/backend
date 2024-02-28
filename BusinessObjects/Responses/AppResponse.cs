using BusinessObjects.Common;
using BusinessObjects.Common.Constants;

namespace BusinessObjects.Responses
{
    public class AppResponse<TData> where TData : class
    {
        public StatusResponse Status { get; set; } = StatusResponse.Success;
        public string? Message { get; set; }
        public IDictionary<string, string[]>? Errors { get; set; }
        public TData? Data { get; set; }
    }
}
