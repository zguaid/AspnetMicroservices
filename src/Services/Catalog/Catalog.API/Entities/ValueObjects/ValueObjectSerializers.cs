using System;
using FluentResults;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Catalog.API.Entities.ValueObjects
{
    public class StringValueObjectSerializer<T> : SerializerBase<T> where T : ValueObject
    {
        private readonly Func<string, Result<T>> _factory;
        private readonly Func<T, string> _getter;

        public StringValueObjectSerializer(Func<string, Result<T>> factory, Func<T, string> getter)
        {
            _factory = factory;
            _getter = getter;
        }

        public override T Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var value = context.Reader.ReadString();
            return _factory(value).Value;
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, T value)
        {
            context.Writer.WriteString(_getter(value));
        }
    }

    public class DecimalValueObjectSerializer<T> : SerializerBase<T> where T : ValueObject
    {
        private readonly Func<decimal, Result<T>> _factory;
        private readonly Func<T, decimal> _getter;

        public DecimalValueObjectSerializer(Func<decimal, Result<T>> factory, Func<T, decimal> getter)
        {
            _factory = factory;
            _getter = getter;
        }

        public override T Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var value = context.Reader.ReadDecimal128().ToDecimal();
            return _factory(value).Value;
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, T value)
        {
            context.Writer.WriteDecimal128(new Decimal128(_getter(value)));
        }
    }

    public class ProductIdSerializer : SerializerBase<ProductId>
    {
        public override ProductId Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var objectId = context.Reader.ReadObjectId();
            return ProductId.Create(objectId.ToString()).Value;
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, ProductId value)
        {
            context.Writer.WriteObjectId(ObjectId.Parse(value.Value));
        }
    }
}
