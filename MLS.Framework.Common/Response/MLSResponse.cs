using MLS.Framework.Common.Response;

namespace MLS.Framework.Common
{
    public class MlsResponse<TData>
    {
        public ResponseCode? ResponseCode { get; set; }

        public TData? Data { get; set; }
    }
}