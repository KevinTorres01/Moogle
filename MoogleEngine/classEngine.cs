namespace MoogleEngine
{
    class Engine
    {
        public static SearchResult Searcher(Folder DataBase, string InputQuery)
        {
            Query query = new Query(InputQuery);
            string suggestion = InputQuery;
            Dictionary<string, Dictionary<string, float>> Matrix = DataBase.Relevance;
            List<SearchItem> items = new List<SearchItem>();

            foreach (File file in DataBase.Files)
            {
                string title = file.fileName;
                string snippet = GetSnippet(query, file);
                float score = GetScore(query, Matrix, title);

                SearchItem item = new SearchItem(title, snippet, score);
               if(item.Snippet != null)
               {
                items.Add(item);
               }
            }
             SearchItem[] Items = items.ToArray();
            for (int i = 0; i < Items.Length; i++)
            {
                for (int j = 0; j < Items.Length; j++)
                {
                    if (Items[j].Score < Items[i].Score)
                    {
                        SearchItem Temp = Items[j];
                        Items[j] = Items[i];
                        Items[i] = Temp;
                    }
                }
            }

            SearchItem[] finalItems = new SearchItem[Items.Length];

            for (int i = 0; i < finalItems.Length; i++)
                finalItems[i] = Items[i];

            SearchResult result = new SearchResult(finalItems, suggestion);
            return result;
        }
        static private float GetScore(Query query, Dictionary<string, Dictionary<string, float>> Matrix, string title)
        {
            float Dotproduct = 0;
            float Norm1 = 0;
            float Norm2 = 0;

            foreach (string word in Matrix[title].Keys)
            {
                if (!query.QueryRelevance.ContainsKey(word))
                    Dotproduct += 0;
                else
                {
                    Dotproduct += query.QueryRelevance[word] * Matrix[title][word];
                    Norm2 += (float)Math.Pow(query.QueryRelevance[word], 2);
                }
                Norm1 += (float)Math.Pow(Matrix[title][word], 2);
            }

            return Dotproduct / ((Norm1 == 0 || Norm2 == 0) ? 1 : (float)(Math.Sqrt(Norm1) * Math.Sqrt(Norm2)));
        }

        private static string GetSnippet(Query query, File file)// metodo que devuelve el snnipet, q es la query en el content de los archivos, en negrita y con contexto
        {
            StreamReader sr = new StreamReader(file.fileRoute);
            string content = sr.ReadToEnd();
            sr.Close();

            string queryWords = query.QueryContent;

            int index = content.IndexOf(queryWords, StringComparison.OrdinalIgnoreCase);
            if (index == -1)
            {
                return null;
            }
           
            int init = Math.Max(0, index - 50);
            int end = Math.Min(content.Length - 1, index + queryWords.Length + 50);

            string snippet = content.Substring(init, 50);
            snippet += "<strong>";
            snippet += content.Substring(index, queryWords.Length)+"</strong>";
            snippet += content.Substring(index+queryWords.Length,50);
            return snippet;
        }

        private static string[] WordsImportance(string[] wordsInArray)
        {
            List<string> result = new List<string>();

            foreach (string word in wordsInArray)
            {
                if (Folder.AllTFs.Keys.Contains(word) && Folder.GetIDF(word) != 0 && word.Length > 3)
                    result.Add(word);
            }
            return result.ToArray();
        }
        private static string[] Partition(string[] words, int straindex, int endindex)
        {
            string[] result = new string[endindex - straindex];
            int position = 0;

            for (int i = straindex; i < endindex; i++)
            {
                result[position] = words[i];
                position++;
            }

            return result;
        }

    }
}