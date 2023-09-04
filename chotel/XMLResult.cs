namespace chotel
{
    public class XMLResult<T> : IResult
    {
        private static readonly XmlSerializer _xmlSerializer = new (typeof(T));
        private readonly T _result;
        public XMLResult(T result) => _result = result;

        public Task ExecuteAsync(HttpContext httpContext)
        {
            using var ms = new MemoryStream();
            _xmlSerializer.Serialize(ms, _result);
            httpContext.Response.ContentType = "application/xml";
            ms.Position = 0;
            return ms.CopyToAsync(httpContext.Response.Body);
        }
    }

    static class XmlResultExtensions
    {
        public static IResult Xml<T>(this IResultExtensions _, T result) =>
            new XMLResult<T>(result);
    }
}
