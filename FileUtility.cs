using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderCrawler
{
    public class FileUtility
    {

        /// <summary>
        /// Avvia la procedura di crawling
        /// </summary>
        /// <param name="arrayPaths"></param>
        /// <param name="ext"></param>
        public static void initFileCrawler(JArray arrayPaths, string ext = "")
        {
            Log.WriteInfo("Inizio operazioni sui paths impostati");
            foreach (string s in arrayPaths) {
                Log.WriteInfo("-------------------------------------------------------------------");
                Log.WriteInfo("Inizio elaborazione path e sub-paths di: " + s);
                // Controllo che il path esista
                if (Directory.Exists(s))
                {
                    getFileFromFolder(s, ext);
                }
                else
                {
                    Log.WriteError("Il path attuale non esiste: " + s);
                }
                Log.WriteInfo("Fine elaborazione path e sub-paths di: " + s);
            }
            Log.WriteInfo("-------------------------------------------------------------------");
            Log.WriteInfo("Fine operazioni sui paths impostati");
        }

        /// <summary>
        /// Estraggo tutti i file dal path passato
        /// Una volta passati i file si cicla le cartelle
        /// </summary>
        /// <param name="path"></param>
        /// <param name="ext"></param>
        /// <returns></returns>
        private static void getFileFromFolder(string path, string ext)
        {
            foreach (string s in Directory.GetFiles(path))
            {
                if (ext != "")
                {
                    if (Path.GetExtension(s).Substring(1) == ext)
                    {
                        Log.WriteInfo("Eliminazione file: " + s);
                        try {
                            File.Delete(s);
                            Log.WriteInfo("Eliminazione completata");
                        }
                        catch (Exception ex) {
                            Log.WriteError("Errore durante l'eliminazione del file: " + s);
                            Log.WriteError("Errore rilevato: " + ex);
                        }
                    }
                }
                else
                {
                    Log.WriteInfo("Eliminazione file: " + s);
                    try {
                        File.Delete(s);
                        Log.WriteInfo("Eliminazione completata");
                    } catch (Exception ex) {
                        Log.WriteError("Errore durante l'eliminazione del file con path: " + s);
                        Log.WriteError("Errore rilevato: " + ex);
                    }
                }
            }

            // Ciclo tutte le cartelle nella cartella passata
            getDirectoryFromFolder(path, ext);
        }


        /// <summary>
        /// Cicla le sottocartelle dal path passato
        /// </summary>
        /// <param name="path"></param>
        /// <param name="ext"></param>
        /// <returns></returns>
        private static void getDirectoryFromFolder(string path, string ext)
        {
            // Ciclo tutte le cartelle e sottocartelle
            foreach (string s in Directory.GetDirectories(path))
            {
                getFileFromFolder(s, ext);
            }
        }


    }
}
