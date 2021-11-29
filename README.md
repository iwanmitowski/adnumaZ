
<h3 align="center">adnumaZ</h3>

<div align="center">

  [![Status](https://img.shields.io/badge/status-active-success.svg)]() 
  [![GitHub Issues](https://img.shields.io/github/issues/iwanmitowski/adnumaZ.svg)](https://github.com/iwanmitowski/adnumaZ/issues)
  [![GitHub Pull Requests](https://img.shields.io/github/issues-pr/iwanmitowski/adnumaZ.svg)](https://github.com/iwanmitowski/adnumaZ/pulls)
  [![License](https://img.shields.io/badge/license-MIT-blue.svg)](/LICENSE)

</div>

---

<p align="center"> adnumaZ - your best torrent tracker
    <br> 
</p>

## 📝 Table of Contents
- [About](#about)
- [Getting Started](#getting_started)
- [Deployment](#deployment)
- [Built Using](#built_using)
- [Authors](#authors)
- [Acknowledgments](#acknowledgement)

## 🧐 About <a name = "about"></a>
AdnumaZ gives you opportunity easily to share torrent files with friends, comment their work using P2P connection.

## 🏁 Getting Started <a name = "getting_started"></a>

### Register and Login pages

```
Scaffolded register and login pages
```
```
Admin role 
```
```
Custom profile custom image, username
```
### Home controller

#### Index
```
The most recent 5 torrents
```

#### Privacy policy, security, disclaimer 
```
Inform website visitors for our policies
```

### Torrents controller

#### Upload
```
Enter torrent info - Title, Description, image
```
```
Saved in database with custom name avoiding privacy issues
```

#### Download
```
Downloading the torrent file from the server file system
```
```
Changing the custom name to the actual torrent name
```

#### All
```
Torrent info - Title, uploader, size, seeders, peers, upload date
```
```
Pagination
```
```
Custom routing
```
#### By Id
```
Actions - Download, Edit, Comment torrent, Answering comments, Delete comments
```
```
Admin Actions - Edit, Approve torrent, Delete comments
```
```
Torrent info - Title, description, image, uploader, size, seeders, peers, upload date
```
```
Torrent comments- Title, description, image, uploader, size, seeders, peers, upload date
```

#### Approve - REST service - ADMIN ONLY
```
Approve torrent with jQuery post request
```

#### Waiting approval torrents - API controller - ADMIN ONLY
```
Sending post request to the server, marking torrent as approved
```
### Comments controller

#### Create
```
Asigning comment to the torrent and the user
```
#### Delete
```
Marking comment as deleted
```
### Users controller

#### By Id
```
User profile information - uploaded GBs, downloaded GBs, favourited torrents, role, created on
```
#### All registered users - ADMIN ONLY
```
Showing registered users
```

#### Ban - ADMIN ONLY
```
Banning user, reason
```
#### Banned users - ADMIN ONLY
```
All the banned users
```
### Dashboard controller - ADMIN ONLY
```
Site info banned users, torrents, waiting approval count, total downloaded/uploaded GBs
```
```
Most recent comments, opportunity to delete them
```
### Error controller 
```
Custom 404 error page
```
```
Dynamic status codes page, with easter eggs 🥚
```

## 🚀 Deployment <a name = "deployment"></a>
To start the project in development mode, run in the project directory:

```bash
$ docker-compose -f docker-compose.yml -f docker-compose.dev.yml up
```

To start in production mode run:

```bash
$ docker-compose -f docker-compose.yml -f docker-compose.prod.yml up
```

## ⛏️ Built Using <a name = "built_using"></a>
* [ASP.NET 5.0](https://github.com/dotnet/aspnetcore)
* [Visual Studio 2022](https://github.com/github/VisualStudio)
* [Entity Framework Core 5.0](https://github.com/dotnet/efcore)
* [Sql Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
* [AutoMapper](https://github.com/AutoMapper/AutoMapper)
* [Bootstrap](https://github.com/twbs/bootstrap)
* [Particles.js](https://github.com/VincentGarreau/particles.js/)
* [BencodeNET](https://github.com/Krusen/BencodeNET)

## ✍️ Authors <a name = "authors"></a>
- [@iwanmitowski](https://github.com/iwanmitowski) - Idea & Initial work
- [@kaykayehnn](https://github.com/kaykayehnn) - Initial work & Hosting

## 🎉 Acknowledgements <a name = "acknowledgement"></a>
- Knowledge - [@NikolayIT](https://github.com/NikolayIT)
- Inspiration - [@zamunda](http://zelka.org/browse.php)
- All the youtube and stackoverflow guys ❤️
