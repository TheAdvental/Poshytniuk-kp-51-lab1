namespace Task
{
    public class Record
    {
        public int PublicationId { get; set; }
        public string AuthorSurname { get; set; }
        public string Title { get; set; }
        public int CitationCount { get; set; }

        public Record(int publicationId, string authorSurname, string title, int citationCount)
        {
            PublicationId = publicationId;
            AuthorSurname = authorSurname;
            Title = title;
            CitationCount = citationCount;
        }
    }
}
