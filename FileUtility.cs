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
        /// Estrae i video contenuti nella cartella ftp, questa è inserita staticamente nel config
        /// </summary>
        /// <param name="ext">Filtra sull'estensione del file</param>
        /// <returns></returns>
        public static void getFileVideoFromFolder(JArray arrayPaths, string ext = "")
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
            //string fullPathFolder = ConfigurationManager.AppSettings["pathVideoFTP"].ToString();

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
            //JObject response = new JObject();

            //response["nomeFolder"] = Path.GetFileName(path) == "" ? Path.GetDirectoryName(path) : Path.GetFileName(path);
            //response["pathDirectory"] = path;

            // Ciclo tutti i file nella cartella
            //JArray fileInFolder = new JArray();
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
                        //JObject tmp = new JObject();
                        //tmp["nomeFile"] = Path.GetFileNameWithoutExtension(s);
                        //tmp["extFile"] = Path.GetExtension(s);
                        //tmp["pathFile"] = s;
                        //fileInFolder.Add(tmp);
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
                    //JObject tmp = new JObject();
                    //tmp["nomeFile"] = Path.GetFileNameWithoutExtension(s);
                    //tmp["extFile"] = Path.GetExtension(s);
                    //tmp["pathFile"] = s;
                    //fileInFolder.Add(tmp);
                }
            }
            //response["fileInFolder"] = fileInFolder;

            // Ciclo tutte le cartelle nella cartella passata
            //response["folders"] = getDirectoryFromFolder(path, ext);
            getDirectoryFromFolder(path, ext);

            //return response;
        }


        /// <summary>
        /// Cicla le sottocartelle dal path passato
        /// </summary>
        /// <param name="path"></param>
        /// <param name="ext"></param>
        /// <returns></returns>
        private static void getDirectoryFromFolder(string path, string ext)
        {
            //JArray respoonse = new JArray();

            // Ciclo tutte le cartelle e sottocartelle
            foreach (string s in Directory.GetDirectories(path))
            {
                getFileFromFolder(s, ext);
                //JObject tmp = getFileFromFolder(s, ext);
                //respoonse.Add(tmp);
            }

            //return respoonse;
        }


    }
}
