//add to nuget
using MLS.Framework.Common.Enums;

namespace MLS.Framework.Common.ResponseModel
{
    public class MlsResponse<TData>
    {
        public ResponseCode? ResponseCode { get; set; }

        public TData? Data { get; set; }
    }
}