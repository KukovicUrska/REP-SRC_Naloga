namespace SRC_Naloga.Models
{
    public class Movie
    {
        public string idMovie { get; set; }
        public string title { get; set; }
        public int year { get; set; }
        public string description { get; set; }
        public byte[] Picture { get; set; }
        public string FkActor { get; set; }
    }
}
