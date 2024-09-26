using System.Runtime.Serialization;

namespace SoapApi.Dtos
{
    [DataContract]
    public class BookResponseDto
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Author { get; set; }

        [DataMember]
        public DateTime PublishedDate { get; set; }
    }
}
