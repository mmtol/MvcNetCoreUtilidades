using System.Security.Cryptography;
using System.Text;

namespace MvcNetCoreUtilidades.Helpers
{
    public class HelperCriptography
    {
        //creamos un string para el salt
        public static string Salt { get; set; }

        //creamos los metodos static

        //metodo para generar un salt
        private static string GenerateSalt()
        {
            Random random = new Random();
            string salt = "";
            for (int i  = 1; i <= 30; ++i)
            {
                //generamo un aleatorio
                int num = random.Next(1, 255);
                char letra = Convert.ToChar(num);
                salt += letra;
            }

            return salt;
        }

        //creamos un metodo eficiente para el cifrado
        public static string EncriptarTxtBasicoSalt(string contenido, bool comparar)
        {
            if (comparar == false)
            {
                //no queremos comparar, solo cifrar
                //creamos salt
                Salt = GenerateSalt();
            }

            //realizamos el cifrado
            string contenidoSalt = contenido + Salt;
            //utilizamos el obj grande para cifrar
            byte[] salida;
            SHA512 managed = SHA512.Create();
            UnicodeEncoding encoding = new UnicodeEncoding();
            salida = encoding.GetBytes(contenido);
            //realizar n iteraciones sobre el propio cifrado
            for (int i = 1; i <= 50; ++i)
            {
                //cifrado sobre cifrado
                salida = managed.ComputeHash(salida);
            }
            //debemos liberar la memoria
            managed.Clear();
            string resultado = encoding.GetString(salida);
            return resultado;
        }

        //simplemente vamos a devolver un texto cifrado
        public static string EncriptarTxtBasico(string contenido)
        {
            //el cifrado se realiza a nivel de bytes
            //debemos convertir el txt de entrada a bytes[]
            byte[] entrada;
            //despues de cifrar los bytes, os dará una salida de bytes[]
            byte[] salida;
            //necesitamos una clase para convertir de byte a strng y viceversa
            UnicodeEncoding encoding = new UnicodeEncoding();
            //necesitamos un obj para cifrar el contenido
            SHA1 managed = SHA1.Create();
            //convertimos el txt a bytes
            entrada = encoding.GetBytes(contenido);
            //los objetos de cifrado tienen un metodo llamado ComputerHash()
            //que recibe un byte[], realizan acciones internas y devuelven el byte[] cifrado
            salida = managed.ComputeHash(entrada);
            //convertimos los bytes[] a txt
            string resultado = encoding.GetString(salida);
            return resultado;
        }
    }
}
