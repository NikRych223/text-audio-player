using NAudio.Wave;

namespace IdiotProjectOne
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainMenu();
        }

        static void MainMenu()
        {
            Console.WriteLine("\n--------------------------------");
            Console.WriteLine("| Welcome to text file Player! |");
            Console.WriteLine("--------------------------------");

            while (true)
            {
                Console.Write("\n(1) - audio to file\n(2) - play text file\n(E) - exit\n\nYour chose: ");
                string menuInput;
                do
                {
                    menuInput = Console.ReadLine().ToLower();
                } while (menuInput != "1" && menuInput != "2" && menuInput != "E");

                if (menuInput == "1")
                {
                    ConvertAudioToFile();
                }

                if (menuInput == "2")
                {
                    PlayTextFile();
                }

                if (menuInput != "e") { }
                {
                    Environment.Exit(0);
                }
            }
        }

        static void ConvertAudioToFile() 
        {
            try
            {
                Console.Write("\nInput path to audio: ");
                string urlToFile;

                do
                {
                    urlToFile = Console.ReadLine();
                } while (!File.Exists(urlToFile));

                byte[] fileData = File.ReadAllBytes(urlToFile);

                Console.Write("\n[");
                string progressBar = "";

                for (int i = 0; i <= 5; i++)
                {
                    progressBar += "*";
                    Console.Write(progressBar);
                    Thread.Sleep(100);
                }
                Console.Write("]");

                Console.Write("\n\nInput save text path: ");
                string urlToSave;

                do
                {
                    urlToSave = Console.ReadLine();
                } while (urlToSave == null);

                File.WriteAllText(urlToSave, Convert.ToBase64String(fileData));
                Console.WriteLine($"Your text file save in: {urlToSave}");

                MainMenu();

            } catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        static void PlayTextFile()
        {
            try
            {
                Console.Write("\nInput path to text: ");
                string urlToFile;

                do
                {
                    urlToFile = Console.ReadLine();
                } while (!File.Exists(urlToFile));

                byte[] fileData = Convert.FromBase64String(File.ReadAllText(urlToFile));
                File.WriteAllBytes("\\tempFile.mp3", fileData);

                var audioFile = new AudioFileReader("\\tempFile.mp3");
                var outputDevice = new WaveOutEvent();

                outputDevice.Init(audioFile);
                outputDevice.Play();

                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    Console.WriteLine($"\nTotal play time: {audioFile.TotalTime}");
                    Console.Write($"\nInput (S) if stop it: ");
                    string userInpit = Console.ReadLine().ToLower();

                    if (userInpit == "s")
                    {
                        outputDevice.Stop();
                    }
                }

                MainMenu();

            } catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}