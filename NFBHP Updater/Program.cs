using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

class Program
{
	static void Main ()
	{
		Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
		Console.WriteLine("■ NFBHP Updater     Made With Love By niceEli. ■");
		Console.WriteLine("■ Press any key to download the resource pack. ■");
		Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
		Console.WriteLine();
		Console.WriteLine("                     V1.0.0");
		Console.ReadKey();

		// Specify the URL of the remote zip file
		string remoteFileUrl = "https://github.com/niceEli/niceFilmsBetterHypixelPack/releases/latest/download/NFBHP.Latest.zip";

		// Specify the path to the Minecraft resource packs folder
		string resourcePacksFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".minecraft", "resourcepacks");

		try
		{
			// Create the resource packs folder if it doesn't exist
			if (!Directory.Exists(resourcePacksFolder))
				Directory.CreateDirectory(resourcePacksFolder);

			// Download the zip file and move it to the resource packs folder with a loading screen
			DownloadAndMoveResourcePack(remoteFileUrl, resourcePacksFolder).Wait();

			Console.WriteLine("Resource pack downloaded successfully!");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"An error occurred while downloading and moving the resource pack: {ex.Message}");
		}

		Console.WriteLine("Press any key to exit.");
		Console.ReadKey();
	}

	private static async Task DownloadAndMoveResourcePack (string remoteFileUrl, string resourcePacksFolder)
	{
		using (WebClient webClient = new WebClient())
		{
			webClient.DownloadProgressChanged += (sender, e) =>
			{
				Console.Clear();
				Console.WriteLine("Downloading and moving the resource pack...");
				Console.WriteLine($"Progress: {e.ProgressPercentage}%");
			};

			// Download the zip file to a temporary location
			string tempFilePath = Path.Combine(Path.GetTempPath(), "resourcepack.zip");
			await webClient.DownloadFileTaskAsync(new Uri(remoteFileUrl), tempFilePath);

			// Move the zip file to the resource packs folder
			string destinationPath = Path.Combine(resourcePacksFolder, Path.GetFileName(remoteFileUrl));
			File.Move(tempFilePath, destinationPath);
		}
	}
}