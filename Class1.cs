using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PicCapLib
{
    public class ImageSource
    {
        int indexStart, indexEnd;
        String page, matchStart, matchEnd;

        
        public ImageSource(String pageSource, String matchStart, String matchEnd)
        {
            this.page = pageSource;
            this.matchStart = matchStart;
            this.matchEnd = matchEnd;
        }
        
        int getIndexStart()
        {
            Regex rx = new Regex(matchStart);
            Match i = rx.Match(page);

            indexStart = i.Index + matchStart.Length;

            return indexStart;
        }
        int getIndexEnd()
        {
            Regex rx = new Regex(matchEnd);
            Match i = rx.Match(page);

            indexEnd = i.Index;

            return indexEnd;
        }

        public String getImageSource()
        {
            indexStart = getIndexStart();
            indexEnd = getIndexEnd();
            return page.Substring(indexStart, (indexEnd - indexStart));
        }
    }

    public static class PictureOfDay
    {
        static public void get_NASA_APOD()
        {
            WebClient webClient = new WebClient();
            var page = webClient.DownloadString("http://apod.nasa.gov/apod/astropix.html");

            String matchStart = "<a href=\"image";
            String matchEnd = "\">"+ '\n' + "<IMG SRC";
            ImageSource imgSrc = new ImageSource(page, matchStart, matchEnd);
            String link = "http://apod.nasa.gov/apod/image" + imgSrc.getImageSource();
            DateTime today = DateTime.Today;
            var name = @"C:\Users\Dima\Dropbox\PC-Notebook\misc\Background\APOD\" + today.ToString("dd-MM-yyyy") + ".jpg";

            webClient.DownloadFile(link, name);
        }

        static public void get_Bing_Img()
        {
            WebClient webClient = new WebClient();
            var page = webClient.DownloadString("http://www.bing.com/");
        
            String matchStart = "g_img={url:'";
            String matchEnd = "',id:'bgDiv'";
            ImageSource imgSrc = new ImageSource(page, matchStart, matchEnd);
            String link = "http://www.bing.com" + imgSrc.getImageSource();
            DateTime today = DateTime.Today;
            var name = @"C:\Users\Dima\Dropbox\PC-Notebook\misc\Background\Bing\" + today.ToString("dd-MM-yyyy")+".jpg";

            webClient.DownloadFile(link, name);
        }
    }
}
