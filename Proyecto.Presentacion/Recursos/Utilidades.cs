﻿using System.Security.Cryptography;
using System.Text;

namespace FRONT_web_personal_saving.Recursos
{
    //para la configuracion de servicios:

    public class Utilidades
    {

        public static string EncriptarContra(string contraseña)
        {

            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;

                byte[] result = hash.ComputeHash(enc.GetBytes(contraseña));

                foreach (byte b in result)
                    sb.Append(b.ToString("x2"));
            }

            return sb.ToString();

        }

    }
}
