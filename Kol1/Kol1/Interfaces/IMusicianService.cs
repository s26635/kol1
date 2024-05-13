using System.Threading.Tasks;
using Kol1.DTO;
using Kol1.Models;

namespace Kol1.Interfaces;

public interface IMusicianService
{
    Task<MusicianSongs> GetMusicianSongsById(int id);
    
    Task<MusicianSongs> AddMusicianSongs(MusicianSongsDTO musicianSongsDto);
}