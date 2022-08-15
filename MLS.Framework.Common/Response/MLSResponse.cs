namespace MLS.Framework.Common.Response
{
    public class MlsResponse<TData>
    {
        public ResponseCode? ResponseCode { get; set; }

        public TData? Data { get; set; }
    }
}