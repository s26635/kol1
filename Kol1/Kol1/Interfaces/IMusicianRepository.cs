using System.Threading.Tasks;
using Kol1.Models;

namespace Kol1.Interfaces;

public interface IMusicianRepository
{
    Task<MusicianSongs> GetMusicianSongsById(int id);
}