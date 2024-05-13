using System.Threading.Tasks;
using Kol1.Interfaces;
using Kol1.Models;

namespace Kol1.Services;

public class MusicianService : IMusicianService
{
    private IMusicianRepository _musicianRepository;

    public MusicianService(IMusicianRepository musicianRepository)
    {
        _musicianRepository = musicianRepository;
    }

    public Task<MusicianSongs> GetMusicianSongsById(int id)
    {
        return _musicianRepository.GetMusicianSongsById(id);
    }
}