using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Users
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name {get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string IdentityNumber { get; set; }
    public string wallet { get; set; }
    public string Credit { get; set; }
    public string Fica { get; set; }
    public string AccessLevel { get; set; }
}