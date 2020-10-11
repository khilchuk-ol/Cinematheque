using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Cinematheque.Data
{
    public static class DataHolder
    {
        public static List<Film> Films = new List<Film>();

        public static List<Actor> Actors = new List<Actor>();

        public static List<Genre> Genres = new List<Genre>();

        public static List<Director> Directors = new List<Director>();

        public static List<ActorInFilm> ActorsInFilms = new List<ActorInFilm>();

        public static List<GenreOfFilm> GenresOfFilms = new List<GenreOfFilm>();

        static DataHolder()
        {
            var test = new Actor("Actor", "Test", DateTime.Now, null, Gender.NotIdentified, new RegionInfo("US"),
                        "actorA.jpg", " Audrey Justine Tautou was born on August 9, 1976 in Beaumont, France, to Evelyne Marie Laure (Nuret), a teacher, and Bernard Tautou, a dental surgeon. Audrey showed an interest for comedy at an early age and started he");
            Actors.Add(test);

            var list = new List<RegionInfo>();
            list.Add(new RegionInfo("US"));
            if (Films.Count == 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    var letter = (char) (i + 65);

                    var film = new Film("Film " + letter,  DateTime.Now, list, TimeSpan.FromMinutes(90), 5, 
                        "film" + letter + ".jpg", letter + " This movie recounts the adventures of M. Gustave (Ralph Fiennes), a legendary concierge at a famous European hotel between the wars, and Zero Moustafa (Tony Revolori), the lobby boy who becomes his most trusted friend. The story involves the theft and recovery of a priceless Renaissance painting and the battle for an enormous family fortune - all against the backdrop of a suddenly and dramatically changing continent. ");
                    var actor = new Actor("Actor", letter.ToString(), DateTime.Now, null, Gender.NotIdentified, new RegionInfo("US"),
                        "actor" + letter + ".jpg", letter + " Audrey Justine Tautou was born on August 9, 1976 in Beaumont, France, to Evelyne Marie Laure (Nuret), a teacher, and Bernard Tautou, a dental surgeon. Audrey showed an interest for comedy at an early age and started her acting lessons at 'Cours Florent'. In 1998 she won the best young actress award in the ninth 'Jeune Comedien de Cinema Festival' in Bezier. Then, she came to the attention of Tonie Marshall who gave her a role in her film Venus Beauty (1999) for which she won a Best New Actress Cesar. It came as a surprise to even Audrey: 'I was so certain I could not be chosen that I told her that she probably dialled a wrong number.' The director chose her for her natural nature: 'She came, she gaffed, she turned reddish, her ears were in a funny position and her hair was relaxed.In five minutes, she gave me the heart of the character, a petite young girl who would like to be a lady and will become a woman.' In 2000, Audrey was again nominated for a Cesar and her movie Amélie (2001) has been a phenomenal success worldwide.");
                    var director = new Director("Director", letter.ToString(), DateTime.Now, null, Gender.NotIdentified, new RegionInfo("US"),
                        "director" + letter + ".jpg", letter + " Victor Fleming entered the film business as a stuntman in 1910, mainly doing stunt driving - which came easy to him, as he had been a mechanic and professional race-car driver. He became interested in working on the other side of the camera, and eventually got a job as a cameraman on many of the films of Douglas Fairbanks. He soon began directing, and his first big hit was The Virginian (1929). It was the movie that turned Gary Cooper into a star (a fact Cooper never forgot; he and Fleming remained friends for life). Fleming's star continued to rise during the '30s, and he was responsible for many of the films that would eventually be considered classics, such as Red Dust (1932), Blonde Bombshell (1933), Treasure Island (1934), and the two films that were the high marks of his career: Gone with the Wind (1939) and The Wizard of Oz (1939). Ironically Fleming was brought in on both pictures to replace other directors and smooth out the troubled productions, a feat he accomplished masterfully. His career took somewhat of a downturn in the '40s, and most of his films, with the exception of Dr. Jekyll and Mr. Hyde (1941), weren't particularly successful. He ended his career with the troubled production Joan of Arc (1948), which turned out to be a major critical and financial failure.");
                    var genre = new Genre("Genre " + letter);

                    Actors.Add(actor);
                    Directors.Add(director);
                    Films.Add(film);
                    Genres.Add(genre);

                    ActorsInFilms.Add(new ActorInFilm() { Actor = actor, Film = film });
                    ActorsInFilms.Add(new ActorInFilm() { Actor = test, Film = film });
                    GenresOfFilms.Add(new GenreOfFilm() { Genre = genre, Film = film });
                    film.Director = director;
                }
            }
        }

        public static bool AddGenreToFilm(Genre g, Film f)
        {
            if (GenresOfFilms.Find(gof => gof.Film.Equals(f) && gof.Genre.Equals(g)) != default(GenreOfFilm))
            {
                return false;
            }

            GenresOfFilms.Add(new GenreOfFilm() { Genre = g, Film = f });
            
            return true;
        }

        public static void AddFilmToGenre(Film f, Genre g) => AddGenreToFilm(g, f);

        public static bool AddActorToFilm(Actor a, Film f)
        {
            if (ActorsInFilms.Find(aif => aif.Actor.Equals(a) && aif.Film.Equals(f)) != default(ActorInFilm))
            {
                return false;
            }

            ActorsInFilms.Add(new ActorInFilm() { Actor = a, Film = f });
            return true;
        }

        public static bool AddFilmToActor(Film f, Actor a) => AddActorToFilm(a, f);

        public static void RemoveAllGenresOfFilm(Film f)
        {
            GenresOfFilms.RemoveAll(gof => gof.Film.Equals(f));
        }

        public static void RemoveAllActorsOfFilm(Film f)
        {
            ActorsInFilms.RemoveAll(aif => aif.Film.Equals(f));
        }

        public static void RemoveAllFilmsOfActor(Actor a)
        {
            ActorsInFilms.RemoveAll(aif => aif.Actor.Equals(a));
        }

        public static void RemoveGenreFromFilm(Genre g, Film f)
        {
            var toRemove = GenresOfFilms.Find(gof => gof.Film.Equals(f) && gof.Genre.Equals(g));

            GenresOfFilms.Remove(toRemove);
        }

        public static void RemoveFilmFromGenre(Film f, Genre g) => RemoveGenreFromFilm(g, f);

        public static void RemoveActorFromFilm(Actor a, Film f)
        {
            var toRemove = ActorsInFilms.Find(aif => aif.Film.Equals(f) && aif.Actor.Equals(a));

            ActorsInFilms.Remove(toRemove);
        }

        public static void RemoveFilmFromActor(Film f, Actor a) => RemoveActorFromFilm(a, f);

        public static void RemoveAllFilmsOfGenre(Genre g)
        {
            GenresOfFilms.RemoveAll(gof => gof.Genre.Equals(g));
        }

        public static List<Actor> AvailableActors(Film f)
        {
            return Actors.Where(a => !a.FilmsStared.Contains(f)).ToList();
        }

        public static List<Genre> AvailableGenres(Film f)
        {
            return Genres.Where(g => !g.FilmsOfGenre.Contains(f)).ToList();
        }

        public static List<Film> AvailableFilms(Actor a)
        {
            return Films.Where(f => !f.Actors.Contains(a)).ToList();
        }

        public static List<Film> AvailableFilms(Director d)
        {
            return Films.Where(f => f.Director != d).ToList();
        }

        public static Film GetFilmById(Guid filmID)
        {
            return Films.Where(f => f.ID == filmID).FirstOrDefault();
        }

        public static Actor GetActorById(Guid actorID)
        {
            return Actors.Where(a => a.ID == actorID).FirstOrDefault();
        }

        public static Genre GetGenreById(Guid genreID)
        {
            return Genres.Where(g => g.ID == genreID).FirstOrDefault();
        }

        public static Director GetDirectorById(Guid directorID)
        {
            return Directors.Where(d => d.ID == directorID).FirstOrDefault();
        }
    }
}
