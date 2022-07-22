using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace FirstTaskSecondPart
{
    class Program
    {
        static void Main(string[] args)
        {
            ButtonLogic bl = new ButtonLogic();
            bl.Button_Click();
        }
       
    }

    public class ButtonLogic {

        const string Url = "https://apod.nasa.gov/apod/image/1806/IMG_5938Mauduit_2048.jpg";
        const string File = "file.jpg";

        private Task IoBoundOperationGoodAsync(int i)
        {
            return new WebClient().DownloadFileTaskAsync(Url, i + File);
        }

        public async void Button_Click()
        {
            Button thisBytton = new Button();
            thisBytton.isEnabled = false;

            var tasks = new List<Task>();

            for (int i = 0; i < 10; i++)
                tasks.Add(IoBoundOperationGoodAsync(i)); 

            var watch = Stopwatch.StartNew();

            await Task.WhenAll(tasks);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            thisBytton.isEnabled = true;
            Console.WriteLine(
                $"Async task completed in {elapsedMs / 1000} seconds!",
                "Information");
        }
    }


    public class Button
    {
       public bool isEnabled { get; set; }
    }
}
