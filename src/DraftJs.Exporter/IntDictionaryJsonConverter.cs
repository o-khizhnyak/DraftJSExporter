using System;
using System.Buffers.Text;
using System.Globalization;
using System.Text.Json;

namespace DraftJs.Exporter
{
    public class IntDictionaryJsonConverter : CustomDictionaryJsonConverter<int>
    {
        protected override int ParseKey(ref Utf8JsonReader reader)
        {
            var propertyName = Read(ref reader);
            if (!Utf8Parser.TryParse(propertyName, out int value, out var bytesConsumed)
                || bytesConsumed != propertyName.Length)
            {
                throw new InvalidOperationException("Failed to parse GUID key");
            }
            return value;
        }

        protected override void WriteKey(Utf8JsonWriter writer, int key)
        {
            writer.WritePropertyName(key.ToString(CultureInfo.InvariantCulture));
        }
    }
}