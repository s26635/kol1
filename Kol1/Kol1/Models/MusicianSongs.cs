using System.Collections;

namespace Kol1.Models;

public class MusicianSongs
{
    public int IdMusician { set; get; }
    public string Name { set; get; }
    public string Surname { set; get; }
    public string SceneName { set; get; }
    public Song Songs { set; get; }
    
}