namespace MoogleEngine
{
    public class Folder
    {
        public string folder { get; private set; }
        public string[] filesroute { get; private set; }
        public static int numberOfFiles { get; private set; }
        public File[] Files { get; set; }
        public static Dictionary<string, Dictionary<string, float>> AllTFs { get; private set; }
        public Dictionary<string, Dictionary<string, float>> Relevance { get; private set; }

        public Folder(string folderpath)
        {

            folder = folderpath;
            filesroute = Directory.EnumerateFiles(folder, "*.txt").ToArray();
            Files = new File[filesroute.Length];
            int count = 0;
            foreach (string filesroutePath in filesroute)//el foreach llena el array con objetos tipo File creados a partir de una ruta
            {
                File file = new File(filesroutePath);
                Files[count] = file;
                count++;

                System.Console.WriteLine("Cargando archivo" + file.fileName);
            }
            numberOfFiles = filesroute.Length;
            AllTFs = new Dictionary<string, Dictionary<string, float>>();
            foreach (File file in Files)//al diccionario  le agregamos el nombre de un archivo como llave y el diccionario que contiene sus TF como su valor
            {
                AllTFs.Add(file.fileName, file.TF);
            }
            Relevance = new Dictionary<string, Dictionary<string, float>>();
            foreach (File file in Files)
            {
                Relevance.Add(file.fileName, file.TF);
            }
            foreach (string fileName in Relevance.Keys)
            {
                foreach (string word in Relevance[fileName].Keys)
                {
                    Relevance[fileName][word] = Relevance[fileName][word] * GetIDF(word);
                }
            }
        }
        public static float GetIDF(string word)// funcion que da el idf de una palabra
        {
            float idf = (float)Math.Log10(numberOfFiles / GetNumberOfAppearences(word));
            return idf;
        }
        private static float GetNumberOfAppearences(string word)//metodo para obtener la cantidad de documentos en los q aparece una palabra
        {
            float appearences = 0;
            foreach (string fileName in AllTFs.Keys)
            {
                if (AllTFs[fileName].ContainsKey(word))
                {
                    appearences++;
                }
            }
            return appearences;

        }
    }
}