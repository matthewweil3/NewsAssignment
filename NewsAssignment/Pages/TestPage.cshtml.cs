using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsAssignment.Models;
using Newtonsoft.Json;

namespace NewsAssignment.Pages
{
    public class TestPageModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public TestPageModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public List<Test> tests { get; set; }
        public void OnGet()
        {
            tests = new List<Test>();
        }

        [BindProperty]
        public string topic { get; set; }
        public async Task<IActionResult> OnPost()
        {
            Uri mb = new Uri("https://newsdata.io/api/1/news?apikey=pub_197475128a14bc8630f52229fdabc71b75c8d&category=" + topic + "&fmt=json");

            HttpClient client = new HttpClient();
            //not necessary
            client.DefaultRequestHeaders.Add("User-Agent", "tmu.edu");

            HttpResponseMessage response = await client.GetAsync(mb.ToString());

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var TestResults = JsonConvert.DeserializeObject<List<Test>>(data);
                tests = TestResults;
            }
            TestList retval = new TestList();
            retval.test = tests;
            return Page();
        }
    }
}
