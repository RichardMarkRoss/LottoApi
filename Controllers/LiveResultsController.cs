using System.Net.Http;
using System.Xml;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;

[ApiController]
[Route("api/[controller]")]
public class LiveResultsController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public LiveResultsController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet("dailylotto")]
    public async Task<IActionResult> GetDailyLotto()
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
                var json = new JObject();

                // Add the GameType key and value
                json.Add("GameType", "dailylotto");

                foreach (var field in xmlDoc.Descendants("field"))
                {
                    var name = field.Attribute("name")?.Value;
                    var value = field.Attribute("value")?.Value;

                    if (name != null && value != null)
                    {
                        json.Add(name, value);
                    }
                }

                return Ok(json.ToString());
            }
            catch (Exception)
            {
                return BadRequest("The XML data is invalid.");
            }
        }
    }
    [HttpGet("lotto")]
    public async Task<IActionResult> GetLotto()
    {
        var url = "https://www.nationallottery.co.za/xmlfeed/lotto.xml";

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
                var json = new JObject();

                json.Add("GameType", "lotto");

                foreach (var field in xmlDoc.Descendants("field"))
                {
                    var name = field.Attribute("name")?.Value;
                    var value = field.Attribute("value")?.Value;

                    if (name != null && value != null)
                    {
                        json.Add(name, value);
                    }
                }

                return Ok(json.ToString());
            }
            catch (Exception)
            {
                return BadRequest("The XML data is invalid.");
            }
        }
    }
    [HttpGet("lottoplus")]
    public async Task<IActionResult> GetLottoPlus()
    {
        var url = "https://www.nationallottery.co.za/xmlfeed/lottoplus.xml";

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
                var json = new JObject();

                json.Add("GameType", "lottoplus");

                foreach (var field in xmlDoc.Descendants("field"))
                {
                    var name = field.Attribute("name")?.Value;
                    var value = field.Attribute("value")?.Value;

                    if (name != null && value != null)
                    {
                        json.Add(name, value);
                    }
                }

                return Ok(json.ToString());
            }
            catch (Exception)
            {
                return BadRequest("The XML data is invalid.");
            }
        }
    }
    [HttpGet("lottoplus2")]
    public async Task<IActionResult> GetLottoPlus2()
    {
        var url = "https://www.nationallottery.co.za/xmlfeed/lottoplus2.xml";

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
                var json = new JObject();

                json.Add("GameType", "lottoplus2");

                foreach (var field in xmlDoc.Descendants("field"))
                {
                    var name = field.Attribute("name")?.Value;
                    var value = field.Attribute("value")?.Value;

                    if (name != null && value != null)
                    {
                        json.Add(name, value);
                    }
                }

                return Ok(json.ToString());
            }
            catch (Exception)
            {
                return BadRequest("The XML data is invalid.");
            }
        }
    }
    [HttpGet("powerball")]
    public async Task<IActionResult> GetPowerball()
    {
        var url = "https://www.nationallottery.co.za/xmlfeed/powerball.xml";

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
                var json = new JObject();

                json.Add("GameType", "powerball");

                foreach (var field in xmlDoc.Descendants("field"))
                {
                    var name = field.Attribute("name")?.Value;
                    var value = field.Attribute("value")?.Value;

                    if (name != null && value != null)
                    {
                        json.Add(name, value);
                    }
                }

                return Ok(json.ToString());
            }
            catch (Exception)
            {
                return BadRequest("The XML data is invalid.");
            }
        }
    }

    [HttpGet("powerballplus")]
    public async Task<IActionResult> GetPowerballPlus()
    {
        var url = "https://www.nationallottery.co.za/xmlfeed/powerballplus.xml";

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
                var json = new JObject();

                json.Add("GameType", "powerballplus");

                foreach (var field in xmlDoc.Descendants("field"))
                {
                    var name = field.Attribute("name")?.Value;
                    var value = field.Attribute("value")?.Value;

                    if (name != null && value != null)
                    {
                        json.Add(name, value);
                    }
                }

                return Ok(json.ToString());
            }
            catch (Exception)
            {
                return BadRequest("The XML data is invalid.");
            }
        }
    }

    [HttpGet("{type}")]
    public async Task<IActionResult> Get(string type)
    {
        var url = "https://www.nationallottery.co.za/xmlfeed/" + type + ".xml";
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
                var json = new JObject();

                json.Add("GameType", type);

                foreach (var field in xmlDoc.Descendants("field"))
                {
                    var name = field.Attribute("name")?.Value;
                    var value = field.Attribute("value")?.Value;

                    if (name != null && value != null)
                    {
                        json.Add(name, value);
                    }
                }

                return Ok(json.ToString());
            }
            catch (Exception)
            {
                return BadRequest("The XML data is invalid.");
            }
        }
    }
}