using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Comentarios
{
    class Comentario
    {
        public string Id { get; set; }
        public string Autor { get; set; }
        public int Año { get; set; }
        public int Mes { get; set; }
        public int Dia { get; set; }
        public string Comentario1 { get; set; }
        public decimal Ip { get; set; }
        public string Inapropiado { get; set; }
        public int Likes { get; set; }

        public override string ToString()
        {
            return string.Format($"{Id} - {Autor} - {Dia}/{Mes}/{Año} -  {Comentario1} - { Ip} - {Inapropiado} - {Likes} Likes");
        }
    }

    class ComentariosDB
    {
        public static void SaveToFile(List<Comentario> comentarios, string path)
        {
            StreamWriter textoArchivo = null;
            try
            {
                textoArchivo = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write));
                foreach (var comentario2 in comentarios)
                {
                    textoArchivo.Write(comentario2.Id + "|");
                    textoArchivo.Write(comentario2.Autor + "|");
                    textoArchivo.Write(comentario2.Dia + "|");
                    textoArchivo.Write(comentario2.Mes + "|");
                    textoArchivo.Write(comentario2.Año + "|");
                    textoArchivo.Write(comentario2.Comentario1 + "|");
                    textoArchivo.Write(comentario2.Ip + "|");
                    textoArchivo.Write(comentario2.Inapropiado + "|");
                    textoArchivo.WriteLine(comentario2.Likes );
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Existente");

            }
            catch (Exception)
            {
                Console.WriteLine("ocurrió algo");
            }
            finally
            {
                if (textoArchivo != null)
                    textoArchivo.Close();
            }
        }
        public static  List<Comentario> ReadFromFile(string path)
        {
            StreamReader textoConsola= null;
            List<Comentario> comentarios = new List<Comentario>();
            try
            {
                
                textoConsola = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read));
                while (textoConsola.Peek() != -1)
                {
                    string row = textoConsola.ReadLine();
                    string[] posicion = row.Split('|');
                    Comentario coment = new Comentario();
                    coment.Id = posicion[0];
                    coment.Autor = posicion[1];
                    coment.Dia = int.Parse(posicion[2]);
                    coment.Mes = int.Parse(posicion[3]);
                    coment.Año = int.Parse(posicion[4]);
                    coment.Comentario1 = posicion[5];
                    coment.Ip = decimal.Parse(posicion[6]);
                    coment.Inapropiado = posicion[7];
                    coment.Likes = int.Parse(posicion[8]);
                    comentarios.Add(coment);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No se encuentra el archivo");
            }

            catch (Exception)
            {
                Console.WriteLine("ocurrió algo");
            }
            finally
            {
                if (textoConsola != null)
                    textoConsola.Close();
            }
            return comentarios;
        }
        public static void SaveToFile1(string path)
        {
            string NewComent = Console.ReadLine();
            StreamWriter textoArchivo = null;
            try
            {
                textoArchivo = new StreamWriter(new FileStream(path, FileMode.Append, FileAccess.Write));
                
                textoArchivo.Write(NewComent);

            }
            catch (NotSupportedException)
            {
                Console.WriteLine("Ocurrió un error");
            }
            catch (Exception)
            {
                Console.WriteLine("Ocurrió algo");
            }
            finally
            {
                if (textoArchivo != null)
                    textoArchivo.Close();
            }
        }
        public static void OrdenarLikes(string path)
        {
            List<Comentario> comentarios;
            
            comentarios = ReadFromFile(path);
            
            var ordenLikes = from coment in comentarios
                             orderby coment.Likes descending
                             select coment;
            foreach (var coment in ordenLikes)
                Console.WriteLine(coment);
        }

        public static void OrdenarFecha(string path)
        {
            List<Comentario> comentarios;
            comentarios = ReadFromFile(path);
            var ordenFecha = comentarios.OrderByDescending(coment => coment.Año).ThenByDescending(coment => coment.Mes).ThenByDescending(coment=> coment.Dia).ToList();
            foreach (var coment in ordenFecha)
                Console.WriteLine(coment);
                             

        }
        public static void Ocultar( string path)
        {
            List<Comentario> comentarios;

                comentarios = ReadFromFile(path);

            var comentinapropiado = from coment in comentarios
                                   where coment.Inapropiado == "NO"
                                   select coment;
        
            foreach (var coment in comentinapropiado)
           
                Console.WriteLine(coment);
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            /*List<Comentario> comentarios = new List<Comentario>();
            comentarios.Add(new Comentario() { Id = "123abc", Autor = "Jorge333", Dia=2,Mes=6,Año=2021, Comentario1 = "Buena publicación", Ip = 123.23123m, Inapropiado = "NO", Likes = 25 });
            comentarios.Add(new Comentario() { Id = "456abc", Autor = "pepito87", Dia=5,Mes=9,Año=2021, Comentario1 = "Gracias por informar", Ip = 124.23123m, Inapropiado = "NO", Likes = 12 });
            comentarios.Add(new Comentario() { Id = "789abc", Autor = "carlitos27", Dia=10,Mes=7,Año=2019, Comentario1 = "No me gusta", Ip = 125.23123m, Inapropiado = "SI", Likes = 23 });
            comentarios.Add(new Comentario() { Id = "101112abc", Autor = "paola21", Dia=8,Mes=9,Año=2021, Comentario1 = "opino lo mismo", Ip = 126.23123m, Inapropiado = "NO", Likes = 5 });
            ComentariosDB.SaveToFile(comentarios, @"C:\Users\jorge\comentarios.txt");
            Console.WriteLine("Agrega comentario");
            ComentariosDB.SaveToFile1( @"C:\Users\jorge\comentarios.txt");*/


            List<Comentario> comentarios = ComentariosDB.ReadFromFile(@"C:\Users\jorge\comentarios.txt");
            foreach(var coment in comentarios)
            {
                Console.WriteLine(coment);
            }
            Console.WriteLine();
            Console.WriteLine("Por likes:");
            ComentariosDB.OrdenarLikes(@"C:\Users\jorge\comentarios.txt");
            Console.WriteLine();
            Console.WriteLine("Por fecha:");
            ComentariosDB.OrdenarFecha(@"C:\Users\jorge\comentarios.txt");
            Console.WriteLine();
            Console.WriteLine("Sin comentarios inapropiados:");
            ComentariosDB.Ocultar(@"C:\Users\jorge\comentarios.txt");
            

            Console.ReadKey();
        }
    }
}
