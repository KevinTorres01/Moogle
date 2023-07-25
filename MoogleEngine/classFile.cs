namespace MoogleEngine
{
    public class File
    {
        public string fileName { get; private set; }
        public string content { get; private set; }
        public Dictionary<string, float> TF { get; private set; }
        public string[] wordsInArray { get; private set; }

        public string fileRoute { get; private set; }


        public File(string filesroutePath)
        {   
            fileRoute = filesroutePath;
            fileName = GetFileName(filesroutePath);
            content = GetFileContent(filesroutePath);
            wordsInArray = Tokenize.StringToArray(content);
            TF = new Dictionary<string, float>();
            TF = WordsCounter(wordsInArray);
        }
        public static string GetFileName(string filesroutePath)//metodo q devuelve el nombre de un archivo
        {
            FileInfo Fi = new FileInfo(filesroutePath);
            string fileName = Fi.Name;
            fileName = Path.GetFileNameWithoutExtension(fileName);

            return fileName;
        }
        public static string GetFileContent(string filesroutePath)//metodo que devuelve el contenio de un archivo
        {
            StreamReader reader = new StreamReader(filesroutePath);
            string Content = reader.ReadToEnd();
            reader.Close();
            return Content;

        }
        static Dictionary<string, float> WordsCounter(string[] wordsInArray)//calcular el TF a partir de um array creado por el string content
        {
            Dictionary<string, float> TF = new Dictionary<string, float>();
            foreach (string word in wordsInArray)
            {
                if (!TF.ContainsKey(word))
                {
                    TF.Add(word, 1);
                }
                else TF[word]++;
            }
            foreach (string word in TF.Keys)
            {
                TF[word] = TF[word] / wordsInArray.Length;
            }
            return TF;
        }
    }
}