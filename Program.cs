using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderCrawler
{
	class Program
	{

		static void Main(string[] args)
		{
			Log.WriteInfo("Avvio applicazione");

			Log.WriteInfo("Inizializzazione e lettura del file arrayPath.json");
			try {
				string pathJArrayPath = ConfigurationManager.AppSettings["arrayPath"].ToString();
				JArray arrayPath = JArray.Parse(File.ReadAllText(pathJArrayPath));
				Log.WriteInfo("Inizializzazione e lettura del file arrayPath.json completata");

				if (arrayPath.Count > 0) {
					FileUtility.getFileVideoFromFolder(arrayPath);
				} else { 
					Log.WriteError("Errore all'avvio della procedura di cancellazione file: nessun path presente");
				}
			} catch (Exception ex) {
				Log.WriteError("Errore durante l'inizializzazione del file arrayPath.json. Errore: " + ex.ToString());
			}

			Log.WriteInfo("Termine applicazione");
		}

	}
}
