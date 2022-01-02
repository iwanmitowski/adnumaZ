using adnumaZ.Areas.Administration.Controllers;
using adnumaZ.Common.Constants;
using adnumaZ.Common.Models;
using adnumaZ.Data;
using adnumaZ.Models;
using adnumaZ.Services.TorrentService.Contracts;
using adnumaZ.ViewModels;

using AutoMapper;

using BencodeNET.Parsing;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace adnumaZ.Services.TorrentService
{
    public class TorrentService : ITorrentService
    {
        private readonly IBencodeParser bencodeParser;
        private readonly ApplicationDbContext dbContext;
        private readonly IConfiguration configuration;

        public TorrentService(IBencodeParser bencodeParser, ApplicationDbContext dbContext, IConfiguration configuration)
        {
            this.bencodeParser = bencodeParser;
            this.dbContext = dbContext;
            this.configuration = configuration;
        }

        public void AsignTorrentToUser(Torrent torrent, User user)
        {
            user.UploadedTorrents.Add(torrent);
        }

        public string GenerateFileName()
        {
            int fileNumber = dbContext.Torrents.Count() + 1;

            return "File" + fileNumber + ".torrent";
        }

        public async Task<BencodeNET.Torrents.Torrent> GetTorrentObjectAsync(Stream torrentStream)
        {
            var torrentObject = await bencodeParser.ParseAsync<BencodeNET.Torrents.Torrent>(torrentStream);

            return torrentObject;
        }

        public void SetTorrentFilePath(Torrent torrent, string saveToPath)
        {
            torrent.TorrentFilePath = saveToPath;
        }

        public string SetTorrentHash(BencodeNET.Torrents.Torrent torrentObject)
        {
            return torrentObject.GetInfoHash().ToLower();
        }

        public double SetTorrentSize(BencodeNET.Torrents.Torrent torrentObject)
        {
            if (torrentObject.Files != null)
            {
                return torrentObject.Files.Sum(f => f.FileSize) / 1024d / 1024d / 1024d;
            }

            return torrentObject.File.FileSize / 1024d / 1024d / 1024d;
        }

        public async Task CreateTorrentInTheGivenDirectory(string saveToPath, UploadTorrentViewModel torrentDTO)
        {
            using (Stream fileStream = new FileStream(saveToPath, FileMode.Create))
            {
                await torrentDTO.File.CopyToAsync(fileStream);
            }
        }

        public List<Task<TorrentSeedData>> GetTorrentSeedData(string trackerApiPath, List<TorrentViewModel> torrents)
        {
            var torrentSeedDataTasks = new List<Task<TorrentSeedData>>();

            foreach (var torrent in torrents)
            {
                if (configuration["TrackerApiPath"] == null)
                {
                    torrentSeedDataTasks.Add(Task.FromResult(new TorrentSeedData() { Hash = torrent.Hash }));
                }
                else
                {
                    var task = Task.Run(async () =>
                    {
                        try
                        {
                            var httpClient = new HttpClient();

                            var response = await httpClient.GetStringAsync($"{trackerApiPath}/t/{torrent.Hash}");

                            var seedData = JsonConvert.DeserializeObject<TorrentSeedData>(response);

                            return seedData;
                        }
                        catch (Exception)
                        {
                            return new TorrentSeedData() { Hash = torrent.Hash, Seeders = 0, Peers = 0 };
                        }
                    });

                    torrentSeedDataTasks.Add(task);
                }
            }

            return torrentSeedDataTasks;
        }
    }
}
