using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace Hospedaria.Reservas.Api.Converters
{
    public class GuidToStringConverter : IPropertyConverter
    {
        public object FromEntry(DynamoDBEntry entry)
        {
            return entry.AsString();
        }

        public DynamoDBEntry ToEntry(object value)
        {
            return new Primitive(value.ToString());
        }
    }
}
