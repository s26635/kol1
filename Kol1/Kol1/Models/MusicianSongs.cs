using System.Collections;
using System.Collections.Generic;

namespace Kol1.Models;

public class MusicianSongs
{
    public int IdMusician { set; get; }
    public string Name { set; get; }
    public string Surname { set; get; }
    public string SceneName { set; get; }
    public IList<Song> Songs { set; get; }
    
}