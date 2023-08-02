using HtmlAgilityPack;

namespace WebbScraping
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string url = "https://books.toscrape.com/catalogue/category/books/romance_8/index.html";
            var web = new HtmlWeb();
            var doc = web.Load(url);
            List<Book> books = new List<Book>();

            // XPath to select <li> (list item) elements that contain a <p> (paragraph) tag, with a class attribute containing both 'star-rating' and 'Five'.
            // <li> stands for list item in HTML, <p> is a paragraph tag, and @class is an attribute of the <p> tag. <h3> is a header tag.
            string listXPath = "//li[.//p[contains(@class, 'star-rating') and contains(@class, 'Five')]]";
            string titleXPath = ".//h3/a";
            string priceXPath = ".//p[contains(@class, 'price_color')]";
            string ratingXPath = ".//p[contains(@class, 'star-rating')]";

            foreach (var li in doc.DocumentNode.SelectNodes(listXPath))
            {
                string title = li.SelectSingleNode(titleXPath).GetAttributeValue("title", string.Empty);
                string price = li.SelectSingleNode(priceXPath).InnerText.Replace("Â", string.Empty);
                string rating = li.SelectSingleNode(ratingXPath).GetAttributeValue("class", string.Empty);

                books.Add(new Book(title, price, rating));

            }

            books.ForEach(Console.WriteLine);
            Console.ReadLine();
        }

    }
    public record Book(string Title, string Price, string Rating);
}


