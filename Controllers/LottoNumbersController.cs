using LottoApi.Data;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LottoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LottoNumbersController : ControllerBase
    {
        private readonly MongoDbContext _dbContext;
        private readonly IMongoCollection<LottoNumbers> _collection;
        private readonly IHttpClientFactory _httpClientFactory;

        public LottoNumbersController(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
            _collection = _dbContext.LottoNumbers;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LottoNumbers>>> GetAllLottoNumbers()
        {
            var lottoNumbers = await _collection.Find(ln => true).ToListAsync();
            return Ok(lottoNumbers);
        }

        [HttpPost]
        public async Task<ActionResult<LottoNumbers>> CreateTicketHistory(LottoNumbers LottoNumbers)
        {
            await _collection.InsertOneAsync(LottoNumbers);
            return CreatedAtAction(nameof(GetAllLottoNumbers), new { id = LottoNumbers._id }, LottoNumbers);
        }
        [HttpPost("dailylotto")]
        public async Task<IActionResult> CreateTickeDailyLottoHistory()
        {
            var url = "https://www.nationallottery.co.za/xmlfeed/dailylotto.xml";

            using (var client = _httpClientFactory.CreateClient())
            {
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest("Unable to retrieve the XML data from the specified URL.");
                }
                var xmlContent = await response.Content.ReadAsStringAsync();
                try
                {
                    var xmlDoc = XDocument.Parse(xmlContent);

                    var lottoNumbers = new LottoNumbers();     
                    lottoNumbers.GameType = "DailyLotto";
                    foreach (var field in xmlDoc.Descendants("field"))
                    {
                        var name = field.Attribute("name")?.Value;
                        var value = field.Attribute("value")?.Value;

                        if (name != null && value != null)
                        {
                            switch (name)
                            {
                                case "DrawDate":
                                    lottoNumbers.DrawDate = value;
                                    break;
                                case "NextDrawDate":
                                    lottoNumbers.NextDrawDate = value;
                                    break;
                                case "DrawNumber":
                                    lottoNumbers.DrawNumber = value;
                                    break;
                                case "Ball1":
                                    lottoNumbers.Ball1 = value;
                                    break;
                                case "Ball2":
                                    lottoNumbers.Ball2 = value;
                                    break;
                                case "Ball3":
                                    lottoNumbers.Ball3 = value;
                                    break;
                                case "Ball4":
                                    lottoNumbers.Ball4 = value;
                                    break;
                                case "Ball5":
                                    lottoNumbers.Ball5 = value;
                                    break;
                                case "Ball6":
                                    lottoNumbers.Ball6 = "";
                                    break;
                                case "BonusBall":
                                    lottoNumbers.BonusBall = "";
                                    break;
                                case "Div1Winners":
                                    lottoNumbers.Div1Winners = value;
                                    break;
                                case "Div1Payout":
                                    lottoNumbers.Div1Payout = value;
                                    break;
                                case "Div2Winners":
                                    lottoNumbers.Div2Winners = value;
                                    break;
                                case "Div2Payout":
                                    lottoNumbers.Div2Payout = value;
                                    break;
                                case "Div3Winners":
                                    lottoNumbers.Div3Winners = value;
                                    break;
                                case "Div3Payout":
                                    lottoNumbers.Div3Payout = value;
                                    break;
                                case "Div4Winners":
                                    lottoNumbers.Div4Winners = value;
                                    break;
                                case "Div4Payout":
                                    lottoNumbers.Div4Payout = value;
                                    break;
                                case "Div5Winners":
                                    lottoNumbers.Div5Winners = "";
                                    break;
                                case "Div5Payout":
                                    lottoNumbers.Div5Payout = "";
                                    break;
                                case "Div6Winners":
                                    lottoNumbers.Div6Winners = "";
                                    break;
                                case "Div6Payout":
                                    lottoNumbers.Div6Payout = "";
                                    break;
                                case "Div7Winners":
                                    lottoNumbers.Div7Winners = "";
                                    break;
                                case "Div7Payout":
                                    lottoNumbers.Div7Payout = "";
                                    break;
                                case "Div8Winners":
                                    lottoNumbers.Div8Winners = "";
                                    break;
                                case "Div8Payout":
                                    lottoNumbers.Div8Payout = "";
                                    break;
                                case "RolloverAmount":
                                    lottoNumbers.RolloverAmount = value;
                                    break;
                                case "RolloverNumber":
                                    lottoNumbers.RolloverNumber = value;
                                    break;
                                case "TotalPrizePool":
                                    lottoNumbers.TotalPrizePool = value;
                                    break;
                                case "TotalSales":
                                    lottoNumbers.TotalSales = value;
                                    break;
                                case "EstimatedJackpot":
                                    lottoNumbers.EstimatedJackpot = value;
                                    break;
                                case "GuaranteedJackpot":
                                    lottoNumbers.GuaranteedJackpot = value;
                                    break;
                                case "DrawMachine":
                                    lottoNumbers.DrawMachine = "RNG2";
                                    break;
                                case "BallSet":
                                    lottoNumbers.BallSet = value;
                                    break;
                                case "GPWinners":
                                    lottoNumbers.GPWinners = value;
                                    break;
                                case "WCWinners":
                                    lottoNumbers.WCWinners = value;
                                    break;
                                case "NCWinners":
                                    lottoNumbers.NCWinners = value;
                                    break;
                                case "ECWinners":
                                    lottoNumbers.ECWinners = value;
                                    break;
                                case "MPWinners":
                                    lottoNumbers.MPWinners = value;
                                    break;
                                case "LPWinners":
                                    lottoNumbers.LPWinners = value;
                                    break;
                                case "FSWinners":
                                    lottoNumbers.FSWinners = value;
                                    break;
                                case "KZNWinners":
                                    lottoNumbers.KZNWinners = value;
                                    break;
                                case "NWWinners":
                                    lottoNumbers.NWWinners = value;
                                    break;
                            }
                        }
                    }

                    // Save the lottoNumbers object to the database
                    await _collection.InsertOneAsync(lottoNumbers);

                    return CreatedAtAction(nameof(GetAllLottoNumbers), new { id = lottoNumbers._id }, lottoNumbers);
                }
                catch (Exception)
                {
                    return BadRequest("The XML data is invalid.");
                }
            }
        }


        [HttpPost("Lotto")]
        public async Task<IActionResult> CreateTicketLottoHistory()
        {
            var url = "https://www.nationallottery.co.za/xmlfeed/Lotto.xml";

            using (var client = _httpClientFactory.CreateClient())
            {
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest("Unable to retrieve the XML data from the specified URL.");
                }
                var xmlContent = await response.Content.ReadAsStringAsync();
                try
                {
                    var xmlDoc = XDocument.Parse(xmlContent);

                    var lottoNumbers = new LottoNumbers();
                    lottoNumbers.GameType = "Lotto";
                    foreach (var field in xmlDoc.Descendants("field"))
                    {
                        var name = field.Attribute("name")?.Value;
                        var value = field.Attribute("value")?.Value;

                        if (name != null && value != null)
                        {
                            switch (name)
                            {
                                case "DrawDate":
                                    lottoNumbers.DrawDate = value;
                                    break;
                                case "NextDrawDate":
                                    lottoNumbers.NextDrawDate = value;
                                    break;
                                case "DrawNumber":
                                    lottoNumbers.DrawNumber = value;
                                    break;
                                case "Ball1":
                                    lottoNumbers.Ball1 = value;
                                    break;
                                case "Ball2":
                                    lottoNumbers.Ball2 = value;
                                    break;
                                case "Ball3":
                                    lottoNumbers.Ball3 = value;
                                    break;
                                case "Ball4":
                                    lottoNumbers.Ball4 = value;
                                    break;
                                case "Ball5":
                                    lottoNumbers.Ball5 = value;
                                    break;
                                case "Ball6":
                                    lottoNumbers.Ball6 = value;
                                    break;
                                case "BonusBall":
                                    lottoNumbers.BonusBall = value;
                                    break;
                                case "Div1Winners":
                                    lottoNumbers.Div1Winners = value;
                                    break;
                                case "Div1Payout":
                                    lottoNumbers.Div1Payout = value;
                                    break;
                                case "Div2Winners":
                                    lottoNumbers.Div2Winners = value;
                                    break;
                                case "Div2Payout":
                                    lottoNumbers.Div2Payout = value;
                                    break;
                                case "Div3Winners":
                                    lottoNumbers.Div3Winners = value;
                                    break;
                                case "Div3Payout":
                                    lottoNumbers.Div3Payout = value;
                                    break;
                                case "Div4Winners":
                                    lottoNumbers.Div4Winners = value;
                                    break;
                                case "Div4Payout":
                                    lottoNumbers.Div4Payout = value;
                                    break;
                                case "Div5Winners":
                                    lottoNumbers.Div5Winners = value;
                                    break;
                                case "Div5Payout":
                                    lottoNumbers.Div5Payout = value;
                                    break;
                                case "Div6Winners":
                                    lottoNumbers.Div6Winners = value;
                                    break;
                                case "Div6Payout":
                                    lottoNumbers.Div6Payout = value;
                                    break;
                                case "Div7Winners":
                                    lottoNumbers.Div7Winners = value;
                                    break;
                                case "Div7Payout":
                                    lottoNumbers.Div7Payout = value;
                                    break;
                                case "Div8Winners":
                                    lottoNumbers.Div8Winners = value;
                                    break;
                                case "Div8Payout":
                                    lottoNumbers.Div8Payout = value;
                                    break;
                                case "RolloverAmount":
                                    lottoNumbers.RolloverAmount = value;
                                    break;
                                case "RolloverNumber":
                                    lottoNumbers.RolloverNumber = value;
                                    break;
                                case "TotalPrizePool":
                                    lottoNumbers.TotalPrizePool = value;
                                    break;
                                case "TotalSales":
                                    lottoNumbers.TotalSales = value;
                                    break;
                                case "EstimatedJackpot":
                                    lottoNumbers.EstimatedJackpot = value;
                                    break;
                                case "GuaranteedJackpot":
                                    lottoNumbers.GuaranteedJackpot = value;
                                    break;
                                case "DrawMachine":
                                    lottoNumbers.DrawMachine = value;
                                    break;
                                case "BallSet":
                                    lottoNumbers.BallSet = value;
                                    break;
                                case "GPWinners":
                                    lottoNumbers.GPWinners = value;
                                    break;
                                case "WCWinners":
                                    lottoNumbers.WCWinners = value;
                                    break;
                                case "NCWinners":
                                    lottoNumbers.NCWinners = value;
                                    break;
                                case "ECWinners":
                                    lottoNumbers.ECWinners = value;
                                    break;
                                case "MPWinners":
                                    lottoNumbers.MPWinners = value;
                                    break;
                                case "LPWinners":
                                    lottoNumbers.LPWinners = value;
                                    break;
                                case "FSWinners":
                                    lottoNumbers.FSWinners = value;
                                    break;
                                case "KZNWinners":
                                    lottoNumbers.KZNWinners = value;
                                    break;
                                case "NWWinners":
                                    lottoNumbers.NWWinners = value;
                                    break;
                            }
                        }
                    }

                    // Save the lottoNumbers object to the database
                    await _collection.InsertOneAsync(lottoNumbers);

                    return CreatedAtAction(nameof(GetAllLottoNumbers), new { id = lottoNumbers._id }, lottoNumbers);
                }
                catch (Exception)
                {
                    return BadRequest("The XML data is invalid.");
                }
            }
        }



        // GET: api/LottoNumbers/search?drawDate=2023-07-01&gameType=Daily
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<LottoNumbers>>> GetFilteredTicketHistory(string drawDate, string gameType)
        {
            var lottoNumbers = await _collection.Find(th => th.DrawDate == drawDate && th.GameType == gameType).ToListAsync();

            if (lottoNumbers.Count() == 0)
                return NotFound();

            return Ok(lottoNumbers);
        }
    }
}