namespace MoogleEngine
{
    public class Query
    {
        public Dictionary<string, float> QueryRelevance { get; private set; }
        public string QueryContent;
        public string[] QueryContentInArray;

        public Query(string inputquery)
        {
            QueryContent = inputquery;
            QueryContentInArray = Tokenize.StringToArray(QueryContent);

            QueryRelevance = new Dictionary<string, float>();
            foreach (string word in QueryContentInArray)
            {
                if (QueryRelevance.ContainsKey(word))
                {
                    QueryRelevance[word]++;
                }
                else QueryRelevance.Add(word, 1);
            }
            foreach (string word in QueryRelevance.Keys)
            {
                QueryRelevance[word] = QueryRelevance[word] / QueryContentInArray.Length;
                QueryRelevance[word] = QueryRelevance[word]*Folder.GetIDF(word);
            }
            
            
        }
        
            
    }
}