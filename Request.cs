namespace ConsoleApplication
{
    public class Request
    {
        public string jsonrpc { get; set; }
        public string method { get; set; }
        public Params @params { get; set; }
        public string id { get; set; }
    }

    public class Params
    {
        public Item item { get; set; }
    }

    public class Item
    {
        public string file { get; set; }
    }
}