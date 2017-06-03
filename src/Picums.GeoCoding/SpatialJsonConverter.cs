//using System;
//using Microsoft.Spatial;
//using Newtonsoft.Json;

//namespace Picums.GeoCoding
//{
//    public sealed class SpatialJsonConverter : JsonConverter
//    {
//        public override bool CanConvert(Type objectType)
//            => typeof(GeographyPoint).Equals(objectType);

//        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
//        {
//            reader.Read();
//            var latitude = reader.ReadAsDecimal();

//            reader.Read();
//            var longitude = reader.ReadAsDecimal();

//            return GeographyPoint.Create((double)latitude.Value, (double)longitude.Value);
//        }

//        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
//            => serializer.Serialize(writer, value);
//    }
//}