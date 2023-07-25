namespace MoogleEngine;


public static class Moogle
{
    static Folder DataBase;
    public static bool Initied = false;

    public static SearchResult Query(string query)
    {
        Init();

        if (!string.IsNullOrEmpty(query)&& DataBase.filesroute != null)
        {
            SearchResult result = Engine.Searcher(DataBase, query);
            return result;
        }
        if(string.IsNullOrEmpty(query))
        {
            SearchItem queryEmpty = new SearchItem("Por favor introduzca una palabra o frase para realizar una búsqueda", "", 1);
            SearchItem[] arrayQueryEmpty = new SearchItem[1];
            arrayQueryEmpty[0] = queryEmpty;
            return new SearchResult(arrayQueryEmpty);
        }
        else
            return new SearchResult();
    }

    public static void Init()
    {
        if (Initied == false)
        {   
            Initied = true;
            DataBase = new Folder("../Content");
        }
    }
}