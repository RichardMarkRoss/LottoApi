using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

public class LottoNumbers
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string _id { get; set; }
    public string DrawDate { get; set; }
    public string NextDrawDate { get; set; }
    public string GameType { get; set; }
    public string DrawNumber { get; set; }
    public string Ball1 { get; set; }
    public string Ball2 { get; set; }
    public string Ball3 { get; set; }
    public string Ball4 { get; set; }
    public string Ball5 { get; set; }
    public string Ball6 { get; set; }
    public string BonusBall { get; set; }
    public string Div1Winners { get; set; }
    public string Div1Payout { get; set; }
    public string Div2Winners { get; set; }
    public string Div2Payout { get; set; }
    public string Div3Winners { get; set; }
    public string Div3Payout { get; set; }
    public string Div4Winners { get; set; }
    public string Div4Payout { get; set; }
    public string Div5Winners { get; set; }
    public string Div5Payout { get; set; }
    public string Div6Winners { get; set; }
    public string Div6Payout { get; set; }
    public string Div7Winners { get; set; }
    public string Div7Payout { get; set; }
    public string Div8Winners { get; set; }
    public string Div8Payout { get; set; }
    public string RolloverAmount { get; set; }
    public string RolloverNumber { get; set; }
    public string TotalPrizePool { get; set; }
    public string TotalSales { get; set; }
    public string EstimatedJackpot { get; set; }
    public string GuaranteedJackpot { get; set; }
    public string DrawMachine { get; set; }
    public string BallSet { get; set; }
    public string GPWinners { get; set; }
    public string WCWinners { get; set; }
    public string NCWinners { get; set; }
    public string ECWinners { get; set; }
    public string MPWinners { get; set; }
    public string LPWinners { get; set; }
    public string FSWinners { get; set; }
    public string KZNWinners { get; set; }
    public string NWWinners { get; set; }
}



