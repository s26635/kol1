using System.Net.Sockets;
using System.Threading.Tasks;
using Kol1.DTO;
using Kol1.Models;

namespace Kol1.Interfaces;

public interface IMusicianRepository
{
    Task<MusicianSongs> GetMusicianSongsById(int id);
    public Task<MusicianSongs> AddMusicianSongs(MusicianSongsDTO musicianSongsDto);
}
