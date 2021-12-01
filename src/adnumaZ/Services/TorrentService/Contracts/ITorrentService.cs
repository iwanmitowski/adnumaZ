using adnumaZ.Models;
using adnumaZ.ViewModels;
using System.IO;
using System.Threading.Tasks;

namespace adnumaZ.Services.TorrentService.Contracts
{
    public interface ITorrentService
    {
        public Task<BencodeNET.Torrents.Torrent> GetTorrentObjectAsync(Stream torrentStream);
        public double SetTorrentSize(BencodeNET.Torrents.Torrent torrentObject);
        public string SetTorrentHash(BencodeNET.Torrents.Torrent torrentObject);
        public string GenerateFileName();
        public void SetTorrentFilePath(Torrent torrent, string saveToPath);
        public Task CreateTorrentInTheGivenDirectory(string saveToPath, UploadTorrentViewModel torrentDTO);
        public void AsignTorrentToUser(Torrent torrent, User user);
    }
}
