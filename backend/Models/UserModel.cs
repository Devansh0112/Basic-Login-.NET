using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace backend.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("Email")]
    public required string Username { get; set; }

    public required string Password { get; set; }

    // Additional properties can be added as needed
}
