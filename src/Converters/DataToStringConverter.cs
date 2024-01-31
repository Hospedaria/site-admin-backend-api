using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System.Globalization;

namespace Hospedaria.Reservas.Api.Converters
{
    public class DataToStringConverter : IPropertyConverter
    {
        public object FromEntry(DynamoDBEntry entry)
        {
            return DateTime.ParseExact(entry.AsString(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

        public DynamoDBEntry ToEntry(object value)
        {
            var datetime = (DateTime)value;
            return new Primitive(datetime.ToString("yyyy-MM-dd"));
        }
    }
}
