using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LocalIntranet.Util
{
    public static class FileUtil
    {
        public static List<FileSystemInfo> listaContenidoFileSystem(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);

            var FileList = new List<FileSystemInfo>();

            try
            {
                //mostrar Directorio
                foreach (DirectoryInfo fi in di.EnumerateDirectories())
                {
                    if (!fi.Attributes.Equals(System.IO.FileAttributes.Hidden | System.IO.FileAttributes.Directory))
                    {
                        FileList.Add(fi);
                    }
                }

                //mostrar Archivos
                foreach (FileInfo fi in di.EnumerateFiles())
                {
                    if (!fi.Attributes.Equals(System.IO.FileAttributes.Hidden | System.IO.FileAttributes.Archive))
                    {
                        if (!fi.Extension.Equals(".db")) FileList.Add(fi);
                    }
                }
            }
            catch (System.IO.IOException e)
            {

            }

            return FileList;



            //DirectoryInfo di = new DirectoryInfo(path);

            //var FileList = new List<FileSystemInfo>();

            ////mostrar Directorio
            //foreach (DirectoryInfo fi in di.EnumerateDirectories())
            //{
            //    if (!fi.Attributes.Equals(System.IO.FileAttributes.Hidden | System.IO.FileAttributes.Directory))
            //    {
            //        FileList.Add(fi);
            //    }
            //}

            ////mostrar Archivos
            //foreach (FileInfo fi in di.EnumerateFiles())
            //{
            //    if (!fi.Attributes.Equals(System.IO.FileAttributes.Hidden | System.IO.FileAttributes.Archive))
            //    {
            //        if (!fi.Extension.Equals(".db")) FileList.Add(fi);
            //    }
            //}
            //return FileList;
        }

        //Verificar si existe el directorio sino lo creara.
        public static List<FileSystemInfo> crearDirectorio(string path, String placa)
        {
            DirectoryInfo di = new DirectoryInfo(path);

            var FileList = new List<FileSystemInfo>();

            if (!Directory.Exists(path))
            {
                // Create the directory it does not exist.
                Directory.CreateDirectory(path);
            }

            //Nombre del Archivo

            return FileList;
        }

        //Listar Imagenes
        public static List<FileSystemInfo> listaContenidoFile(String path)
        {
            DirectoryInfo di = new DirectoryInfo(path);

            var FileList = new List<FileSystemInfo>();

            //mostrar Archivos
            foreach (FileInfo fi in di.EnumerateFiles())
            {
                if (!fi.Extension.Equals(".db"))// | System.IO.FileAttributes.Archive))
                {
                    FileList.Add(fi);
                }
            }
            return FileList;
        }
    }
}