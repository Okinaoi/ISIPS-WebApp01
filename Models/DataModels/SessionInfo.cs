using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DataModels
{
    // cette classe est utilisée quand une personne se login sur notre site, si sont login est correcte on place l'id le prénom et le companyStatus de l'utilisateur
    // dans cet objet pour pouvoir ensuite initialisé une session sur le server, la session nous permet de savoir qui est connecté et quelle requete est envoyée par qui
    public class SessionInfo
    {
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public int Status { get; set; }
    }
}
