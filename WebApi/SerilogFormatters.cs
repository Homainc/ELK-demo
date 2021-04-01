using Serilog.Formatting;
using Serilog.Formatting.Elasticsearch;

namespace WebApi
{
    public static class SerilogFormatters
    {
        public static ITextFormatter ExceptionAsObject => new ExceptionAsObjectJsonFormatter(renderMessage: true);
    }
}