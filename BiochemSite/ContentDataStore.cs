using BiochemSite.Models.Content;

namespace BiochemSite
{
    public class ContentDataStore
    {
        public List<ContentDto> Contents { get; set; }
        public static ContentDataStore Instance { get; } = new ContentDataStore();

        public ContentDataStore()
        {
            Contents = new List<ContentDto>()
            {

                    new ContentDto() {Id = 1, ChapterNum = 1, SubChapterNum = 1, ChapDesc = "Amino acids Introduction"},
                    new ContentDto() {Id = 2,ChapterNum = 1, SubChapterNum = 2, ChapDesc = "pKa and pI points"},
                    new ContentDto() {Id = 3, ChapterNum = 2,SubChapterNum = 1, ChapDesc = "Electronegativity"},

        };
        }
    }
}
