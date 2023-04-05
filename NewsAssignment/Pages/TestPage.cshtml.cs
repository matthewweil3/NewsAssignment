using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewsAssignment.Pages
{
    public class TestPageModel : PageModel
    {
        //private readonly ILogger<IndexModel> _logger;

        //public IndexModel(ILogger<IndexModel> logger)
        //{
        //    _logger = logger;
        //}

        //public IList<Test> tests { get; set; }
        //public void OnGet()
        //{
        //    tests = new List<Test>();
        //}

        //[BindProperty]
        //public string topic { get; set; }
        //public async Task<IActionResult> OnPost()
        //{
        //    Uri mb = new Uri(" https://musicbrainz.org/ws/2/artist/?query=" + topic + "&fmt=json");

        //    HttpClient client = new HttpClient();
        //    //not necessary
        //    client.DefaultRequestHeaders.Add("User-Agent", "tmu.edu");

        //    HttpResponseMessage response = await client.GetAsync(mb.ToString());

        //    if (response.IsSuccessStatusCode)
        //    {
        //        string data = await response.Content.ReadAsStringAsync();
        //        var TestResults = JsonConvert.DeserializeObject<List<Test>>(data);
        //        tests = TestResults;
        //    }
        //    //TestList retval = new TestList();
        //    //retval.tests = tests;
        //    //return Page();
        //}
    }
}
