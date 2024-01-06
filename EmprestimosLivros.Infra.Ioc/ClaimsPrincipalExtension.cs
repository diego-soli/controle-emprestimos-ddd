using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmprestimosLivros.Infra.Ioc
{
    public static class ClaimsPrincipalExtension
    {
        public static int GetId(this ClaimsPrincipal user)
        {
           return  int.Parse(user.FindFirst("id").Value); //converte para inteiro
        }
        public static string GetEmail(this ClaimsPrincipal user)
        {
           return  user.FindFirst("email").Value; //converte para inteiro
        }
    }
}
