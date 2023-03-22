using BiochemSite.Models.Content;

namespace BiochemSite
{
    public class ContentDataStore
    {
        public List<ChapterDto> Contents { get; set; }
        public static ContentDataStore Instance { get; } = new ContentDataStore();

        public ContentDataStore()
        {
            Contents = new List<ChapterDto>()
            {
                    new ChapterDto() {Id = 1,
                                     Number = 1,
                                     Title = "Amino acids Introduction",
                                     Subchapters = new List<SubchapterDto>()
                                     {
                                        new SubchapterDto()
                                            {
                                            Id = 1,
                                            Number = 1,
                                            Title = "Tacos",
                                            Paragraphs = new List<List<string>>()
                                            {
                                                new List<string>()
                                                {
                                                    "This is a real life taco:",
                                                    "https://www.tacobueno.com/assets/food/tacos/Taco_Crispy_Beef_990x725.jpg"
                                                 },

                                                new List<string>()
                                                {
                                                    "Here are three cartoon tacos:",
                                                    "https://friendlystock.com/wp-content/uploads/2020/04/7-angry-mexican-taco-cartoon-clipart.jpg",
                                                    "https://image.spreadshirtmedia.com/image-server/v1/mp/products/T1459A839PA3861PT28D1020944639W8334H10000/views/1,width=1200,height=630,appearanceId=839,backgroundColor=F2F2F2/delicious-taco-cartoon-sticker.jpg",
                                                    "https://t3.ftcdn.net/jpg/02/68/53/36/360_F_268533698_fAlaxKnB18Tl7Kbo3UEZt05lr0vIF9nk.jpg"
                                                 },
                                            }

                                            },
                                    }},
                    new ChapterDto() {Id = 1,
                                     Number = 1,
                                     Title = "Amino acids Introduction",
                                     Subchapters = new List<SubchapterDto>()
                                     {
                                        new SubchapterDto()
                                            {
                                            Id = 1,
                                            Number = 1,
                                            Title = "Tacos",
                                            Paragraphs = new List<List<string>>()
                                            {
                                                new List<string>()
                                                {
                                                    "This is a real life taco:",
                                                    "https://www.tacobueno.com/assets/food/tacos/Taco_Crispy_Beef_990x725.jpg"
                                                 },

                                                new List<string>()
                                                {
                                                    "Here are three cartoon tacos:",
                                                    "https://friendlystock.com/wp-content/uploads/2020/04/7-angry-mexican-taco-cartoon-clipart.jpg",
                                                    "https://image.spreadshirtmedia.com/image-server/v1/mp/products/T1459A839PA3861PT28D1020944639W8334H10000/views/1,width=1200,height=630,appearanceId=839,backgroundColor=F2F2F2/delicious-taco-cartoon-sticker.jpg",
                                                    "https://t3.ftcdn.net/jpg/02/68/53/36/360_F_268533698_fAlaxKnB18Tl7Kbo3UEZt05lr0vIF9nk.jpg"
                                                 },
                                            }

                                            },
                                    }},







        };
        }
    }
}
