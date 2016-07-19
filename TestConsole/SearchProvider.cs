namespace TestConsole
{
    class SearchProvider
    {
        public SearchProvider(string name, string url)
        {
            Name = name;
            Url = url;
        }

        public string Name { get; private set; }
        public string Url { get; private set; }

        public override string ToString()
        {
            return Name;
        }
    }
}