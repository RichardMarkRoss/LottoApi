using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

public class TicketNumbers
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string _id { get; set; }
    public string UserId { get; set; }
    public string TcketID { get; set; }
    public string PurchaseDate { get; set; }
    public string ExpirationDate { get; set; }
    public string GameType { get; set; }
    public string DrawNumber { get; set; }
    public string Amount { get; set; }
    public string Ball1 { get; set; }
    public string Ball2 { get; set; }
    public string Ball3 { get; set; }
    public string Ball4 { get; set; }
    public string Ball5 { get; set; }
    public string Ball6 { get; set; }
    public string BonusBall { get; set; }
    public string Div { get; set; }
}