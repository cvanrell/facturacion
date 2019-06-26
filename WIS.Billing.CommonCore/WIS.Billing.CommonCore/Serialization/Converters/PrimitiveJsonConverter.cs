using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.CommonCore.Serialization.Converters
{
    public sealed class PrimitiveJsonConverter : JsonConverter
    {
        public PrimitiveJsonConverter()
        {

        }

        public override bool CanRead
        {
            get
            {
                return false;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType.IsPrimitive || objectType == typeof(decimal))
                return true;

            if (Nullable.GetUnderlyingType(objectType) != null)
            {
                if (Nullable.GetUnderlyingType(objectType).IsPrimitive || Nullable.GetUnderlyingType(objectType) == typeof(decimal))
                    return true;
            }

            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            switch (serializer.TypeNameHandling)
            {
                case TypeNameHandling.All:
                    writer.WriteStartObject();
                    writer.WritePropertyName("$type", false);

                    switch (serializer.TypeNameAssemblyFormatHandling)
                    {
                        case TypeNameAssemblyFormatHandling.Full:
                            writer.WriteValue(value.GetType().AssemblyQualifiedName);
                            break;
                        default:
                            writer.WriteValue(value.GetType().FullName);
                            break;
                    }

                    writer.WritePropertyName("$value", false);
                    writer.WriteValue(value);
                    writer.WriteEndObject();
                    break;
                default:
                    writer.WriteValue(value);
                    break;
            }
        }
    }
}
