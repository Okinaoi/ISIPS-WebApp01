using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public static class StaticValues
    {
        // muvaise pratique mais pas de prise de tete au niveau de la config dans le fichier startup, c'est la connection string ! c'est grace à elle qu'on
        // se connecte a notre server SQL, le champ "date source" sera différent pour nous tous vu que c'est le nom de votre machine, initial catalog c'est le nom
        // de la db en question et integrated security défini le type d'authentification que vous avez choisis pour votre server sql
        public static string IsipsDbConnectionString { get => "data source=DESKTOP-1QV8HCJ; Initial Catalog =Database_ISIPS-App; Integrated Security = SSPI;"; }
    }
}
