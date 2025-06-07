using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Username { get; set; }

    [BsonElement("dni")]
    public string Dni { get; set; }

    public string PasswordHash { get; set; }
}
