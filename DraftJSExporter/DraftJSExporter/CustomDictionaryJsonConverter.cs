using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DraftJSExporter
{
    public abstract class CustomDictionaryJsonConverter<TKey> : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsGenericType
                   && IsDictionary(typeToConvert.GetGenericTypeDefinition())
                   && typeToConvert.GenericTypeArguments[0] == typeof(TKey);
        }

        public static bool IsDictionary(Type type)
        {
            return type == typeof(IReadOnlyDictionary<,>)
                   || type == typeof(Dictionary<,>);
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var valueType = typeToConvert.GenericTypeArguments[1];
            var dictGenericType = typeToConvert.GetGenericTypeDefinition();
            var converterGenericType = GetConverterGenericType(dictGenericType);
            var converterType = converterGenericType.MakeGenericType(typeof(TKey), valueType);
            return (JsonConverter)Activator.CreateInstance(
                converterType,
                new object[]
                {
                    this,
                });
        }

        private Type GetConverterGenericType(Type dictGenericType)
        {
            if (dictGenericType == typeof(Dictionary<,>))
            {
                return typeof(DictionaryConverter<>);
            }
            if (dictGenericType == typeof(IReadOnlyDictionary<,>))
            {
                return typeof(ReadOnlyDictionaryInterfaceConverter<>);
            }
            throw new NotSupportedException(dictGenericType.FullName);
        }

        protected abstract TKey ParseKey(ref Utf8JsonReader reader);
        protected abstract void WriteKey(Utf8JsonWriter writer, TKey key);

        protected ReadOnlySpan<byte> Read(ref Utf8JsonReader reader)
        {
            return reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;
        }

        public Dictionary<TKey, TValue> ReadDictionary<TValue>(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new InvalidOperationException($"Unexpected token type {reader.TokenType}, expected {JsonTokenType.StartObject}");
            }

            var result = new Dictionary<TKey, TValue>();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return result;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new InvalidOperationException($"Unexpected token type {reader.TokenType}, expected {JsonTokenType.PropertyName}");
                }

                var key = ParseKey(ref reader);
                if (!reader.Read())
                {
                    throw new InvalidOperationException($"Expecting key value");
                }
                var value = JsonSerializer.Deserialize<TValue>(ref reader, options);
                result[key] = value;
            }
            throw new InvalidOperationException($"Expecting token {JsonTokenType.EndObject}");
        }

        public void WriteDictionary<TValue>(Utf8JsonWriter writer, IReadOnlyDictionary<TKey, TValue> value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStartObject();
            foreach (var kv in value)
            {
                WriteKey(writer, kv.Key);
                JsonSerializer.Serialize(writer, kv.Value, options);
            }
            writer.WriteEndObject();
        }

        public class DictionaryConverter<TValue> : JsonConverter<Dictionary<TKey, TValue>>
        {
            private readonly CustomDictionaryJsonConverter<TKey> _converter;

            public DictionaryConverter(
                CustomDictionaryJsonConverter<TKey> converter)
            {
                _converter = converter ?? throw new ArgumentNullException(nameof(converter));
            }

            public override Dictionary<TKey, TValue> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return _converter.ReadDictionary<TValue>(ref reader, options);
            }

            public override void Write(Utf8JsonWriter writer, Dictionary<TKey, TValue> value, JsonSerializerOptions options)
            {
                _converter.WriteDictionary(writer, value, options);
            }
        }

        public class ReadOnlyDictionaryInterfaceConverter<TValue> : JsonConverter<IReadOnlyDictionary<TKey, TValue>>
        {
            private readonly CustomDictionaryJsonConverter<TKey> _converter;

            public ReadOnlyDictionaryInterfaceConverter(CustomDictionaryJsonConverter<TKey> converter)
            {
                _converter = converter ?? throw new ArgumentNullException(nameof(converter));
            }

            public override IReadOnlyDictionary<TKey, TValue> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return _converter.ReadDictionary<TValue>(ref reader, options);
            }

            public override void Write(Utf8JsonWriter writer, IReadOnlyDictionary<TKey, TValue> value, JsonSerializerOptions options)
            {
                _converter.WriteDictionary(writer, value, options);
            }
        }
    }
}