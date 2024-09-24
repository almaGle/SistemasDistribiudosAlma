using System.Runtime.Serialization;

namespace SoapApi.Dtos;
[DataContract]
public class UserUpdateRequestDto
{
    [DataMember]
    public Guid Id { get; set; }
    [DataMember]
    public string BirthDate {get; set;} = null!;
    [DataMember]
    public string FirstName {get; set; } = null!;
    [DataMember]
    public string LastName {get; set; } = null!;
   
}

