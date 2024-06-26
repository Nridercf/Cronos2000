﻿using System.Text;

namespace DFRCronos2000.Factories
{
    internal class Hashage
    {
        public static string Hash(string mdp) // fonction pour hasher un mot de passe
        {
            byte[] data = Encoding.UTF8.GetBytes(mdp); // convertir le mot de passe en tableau de byte
            var dataH = new System.Security.Cryptography.SHA256Managed().ComputeHash(data); // hasher le mot de passe // ne pas changer le type de hashage = conflit avec la base de données
            var res = ByteArrayToString(dataH); // convertir le tableau de byte en string
            return res; // retourner le mot de passe hashé en string
        }

        private static string ByteArrayToString(byte[] arrInput) // convertir un tableau de byte en string
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length - 1; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }
    }
}
