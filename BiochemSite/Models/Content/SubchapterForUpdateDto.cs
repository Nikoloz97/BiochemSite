using System.ComponentModel.DataAnnotations;

namespace BiochemSite.Models.Content
{
    public class SubchapterForUpdateDto
    {
     
        public int Number { get; set; }

        public string Title { get; set; } = string.Empty;


        public List<List<string>> Paragraphs { get; set; } = new List<List<string>>();
    }
}
