using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace NewsAssignment.Pages.Jokes
{
    public class JokeModel : PageModel
    {
        public int NumOfJokes { get; set; } //limit
        public List<Label> Labels { get; set; }
        public class Label
        {
            public string name { get; set; }
            public string id { get; set; }
            public string disambiguation { get; set; }
        }

        public async Task<PageResult> OnPostAsync()
        {
            Uri mb = new Uri("https://api.api-ninjas.com/v1/dadjokes?limit=" + NumOfJokes);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("+pstQyak7kkQIm1yJCuIlA==uGsyupWKQbsUGkUP", "");
            HttpResponseMessage response = await client.GetAsync(mb.ToString());

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var labelResults = JsonConvert.DeserializeObject<List<Label>>(data);
                Labels = labelResults;
            }
            return Page();
        }
        public void OnGet()
        {
            Labels = new List<Label>();
        }
    }
}
