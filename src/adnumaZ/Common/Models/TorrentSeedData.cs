namespace adnumaZ.Common.Models
{
    public class TorrentSeedData
    {
        public string Hash { get; set; }
        
        public int Seeders { get; set; }
        
        public int Peers { get; set; }
    }
}
