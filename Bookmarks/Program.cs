using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var book = new Bookmarks("https://johnpapa.net/");

            if(book.UrlBuild())
            {
                book.UrlBuild();
                Console.WriteLine(book.Title);
                Console.WriteLine("----");
                Console.WriteLine(book.Description);
                Console.WriteLine("----");
                Console.WriteLine(book.UrlImage);
            } 
            
            Console.ReadLine();
        }
    }
}
