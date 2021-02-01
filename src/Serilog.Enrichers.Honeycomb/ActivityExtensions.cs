using System.Diagnostics;

namespace Serilog.Enrichers.Honeycomb
{
    /// <summary>
    /// <see cref="Activity"/> extension methods.
    /// </summary>
    internal static class ActivityExtensions
    {
        /// <summary>
        /// Gets the span unique identifier regardless of the activity identifier format.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <returns>The span unique identifier.</returns>
        public static string GetSpanId(this Activity activity)
        {
            var spanId = activity.IdFormat switch
            {
                ActivityIdFormat.Hierarchical => null,
                ActivityIdFormat.W3C => activity.SpanId.ToHexString(),
                ActivityIdFormat.Unknown => null,
                _ => null,
            };

            return spanId ?? string.Empty;
        }

        /// <summary>
        /// Gets the span trace unique identifier regardless of the activity identifier format.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <returns>The span trace unique identifier.</returns>
        public static string GetTraceId(this Activity activity)
        {
            var traceId = activity.IdFormat switch
            {
                ActivityIdFormat.Hierarchical => null,
                ActivityIdFormat.W3C => activity.TraceId.ToHexString(),
                ActivityIdFormat.Unknown => null,
                _ => null,
            };

            return traceId ?? string.Empty;
        }

        /// <summary>
        /// Gets the span parent unique identifier regardless of the activity identifier format.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <returns>The span parent unique identifier.</returns>
        public static string GetParentId(this Activity activity)
        {
            var parentId = activity.IdFormat switch
            {
                ActivityIdFormat.Hierarchical => activity.ParentId,
                ActivityIdFormat.W3C => activity.ParentSpanId.ToHexString(),
                ActivityIdFormat.Unknown => null,
                _ => null,
            };

            return string.Equals(parentId, "0000000000000000") ? string.Empty : parentId ?? string.Empty;
        }
    }
}
